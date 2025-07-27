using PKHeX.Core;
using PKHeX.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PKHeX.WinForms.Subforms
{
    // @todo: ideas: provide the evolution item with the pokemon when needed
    // this will help with Eevee's. If they come with the stone it's obvious which one was chosen
    public partial class TeamGenerator : Form, IDisposable
    {

        private SAVEditor editor;
        private SaveFile sav;
        Dictionary<int, int> maxSpeciesIdByGeneration;
        private readonly Random sharedRandom;

        private CancellationTokenSource? cancellationTokenSource;
        private GenerationSpeciesProvider? speciesProvider;
        private PokemonFactory? pokemonFactory;

        public TeamGenerator(SAVEditor editor)
        {
            InitializeComponent();
            this.editor = editor;
            this.sav = editor.SAV;
            this.sharedRandom = new Random();

            maxSpeciesIdByGeneration = new Dictionary<int, int>();
            for (int i = 0; i < sav.Version.GetGeneration(); i++)
            {
                cboGeneration.Items.Add($"Gen {i + 1}");
                GameVersion ver = GameUtil.GetVersion((byte)(i + 1));
                maxSpeciesIdByGeneration[i + 1] = GameUtil.GetMaxSpeciesID(ver);
            }
            // Add a hard-coded Generation 1 maximum since GameUtil might not handle Gen 1 properly
            maxSpeciesIdByGeneration[1] = 151;

            // Initialize the species provider for regional form support
            speciesProvider = new GenerationSpeciesProvider(sav);
            
            // Initialize the Pokemon factory
            pokemonFactory = new PokemonFactory(sav, sharedRandom);

            PopulateStarterList();
            PopulateHatchRates();
        }

        private void PopulateHatchRates()
        {
            cboHatchRate.Items.Clear();
            cboHatchRate.Items.Add("Fast");
            cboHatchRate.Items.Add("Medium");
            cboHatchRate.Items.Add("Slow");
            cboHatchRate.SelectedIndex = 1; // Default to Medium
        }

        private int GetHatchRateMultiplier()
        {
            return cboHatchRate.SelectedItem?.ToString() switch
            {
                "Fast" => TeamGeneratorConstants.HatchRates.Fast,
                "Medium" => TeamGeneratorConstants.HatchRates.Medium,
                "Slow" => TeamGeneratorConstants.HatchRates.Slow,
                _ => TeamGeneratorConstants.HatchRates.Medium // Default to Medium
            };
        }

        private void cboStarter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Count currently checked items
            int checkedCount = cboStarter.CheckedItems.Count;
            
            // If trying to check and already at max, prevent it
            if (e.NewValue == CheckState.Checked && checkedCount >= TeamGeneratorConstants.MaxTeamSize)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void sldTeamSize_ValueChanged(object sender, EventArgs e)
        {
            lblTeamSizeValue.Text = sldTeamSize.Value.ToString();
        }

        private void cboGeneration_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Use BeginInvoke to ensure the CheckedItems collection is updated
            BeginInvoke(new Action(() => PopulateStarterList()));
        }

        private void chkStatLimit_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkStatLimit.Checked;
            lblMinStatTotal.Enabled = enabled;
            numMinStatTotal.Enabled = enabled;
            lblMaxStatTotal.Enabled = enabled;
            numMaxStatTotal.Enabled = enabled;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            ShowHelpDialog();
        }

        private void ShowHelpDialog()
        {
            var helpForm = new TeamGeneratorHelp();
            helpForm.ShowDialog(this);
        }

        private void PopulateStarterList()
        {
            cboStarter.Items.Clear();

            // Get selected generations from the generation CheckedListBox
            var selectedGenerations = new List<int>();
            for (int i = 0; i < cboGeneration.CheckedItems.Count; i++)
            {
                string? item = cboGeneration.CheckedItems[i]?.ToString();
                if (!string.IsNullOrEmpty(item) && item.StartsWith("Gen ") && int.TryParse(item.Substring(4), out int gen))
                {
                    selectedGenerations.Add(gen);
                }
            }

            // If no generations are selected, don't show any Pokémon
            if (selectedGenerations.Count == 0)
            {
                return;
            }

            // Use the species provider to get Pokémon from selected generations
            if (speciesProvider != null)
            {
                var availableSpecies = speciesProvider.GetMultiGenerationPool(selectedGenerations);
                
                // Add base species first (form 0)
                var baseSpeciesAdded = new HashSet<ushort>();
                foreach (var speciesForm in availableSpecies)
                {
                    if (speciesForm.Form == 0 && !baseSpeciesAdded.Contains(speciesForm.Species))
                    {
                        cboStarter.Items.Add((Species)speciesForm.Species);
                        baseSpeciesAdded.Add(speciesForm.Species);
                    }
                }
                
                // Add regional forms as separate entries
                foreach (var speciesForm in availableSpecies)
                {
                    if (speciesForm.Form > 0)
                    {
                        string regionalFormName = GetRegionalFormName(speciesForm.Species, speciesForm.Form);
                        string formName = !string.IsNullOrEmpty(regionalFormName) 
                            ? $"{(Species)speciesForm.Species} ({regionalFormName})"
                            : $"{(Species)speciesForm.Species} (Form {speciesForm.Form})";
                        cboStarter.Items.Add(formName);
                    }
                }
            }
        }

        private List<PKM> CreatePokemon(Species species, bool isEgg)
        {
            return CreatePokemon(species, isEgg, false, false);
        }

        private List<PKM> CreatePokemon(Species species, bool isEgg, bool allowRegionalForms)
        {
            return CreatePokemon(species, isEgg, allowRegionalForms, false);
        }

        private List<PKM> CreatePokemon(Species species, bool isEgg, bool allowRegionalForms, bool useMaxIVs)
        {
            if (pokemonFactory == null)
                throw new InvalidOperationException("Pokemon factory not initialized");

            var config = PokemonCreationConfig.FromSpecies(species, isEgg, allowRegionalForms, useMaxIVs);
            var (pokemon, originalPokemon) = pokemonFactory.CreatePokemonPair(config);
            return new List<PKM> { pokemon, originalPokemon };
        }

        private List<PKM> CreatePokemonWithForm(SpeciesForm speciesForm, bool isEgg, bool useMaxIVs)
        {
            if (pokemonFactory == null)
                throw new InvalidOperationException("Pokemon factory not initialized");

            var config = PokemonCreationConfig.FromSpeciesForm(speciesForm, isEgg, useMaxIVs);
            var (pokemon, originalPokemon) = pokemonFactory.CreatePokemonPair(config);
            return new List<PKM> { pokemon, originalPokemon };
        }

        private async void Generate_Click(object sender, EventArgs e)
        {
            // Cancel any ongoing generation
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            
            // Initialize progress tracking
            ShowProgress(true);
            progressBar.Value = 0;
            progressBar.Maximum = sldTeamSize.Value;
            
            // Disable the generate button to prevent multiple clicks
            Generate.Enabled = false;
            
            try
            {
                await GenerateTeamWithProgressAsync(cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                // Generation was cancelled - this is expected
                MessageBox.Show("Team generation was cancelled.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                MessageBox.Show($"An error occurred during team generation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the generate button and hide progress
                Generate.Enabled = true;
                ShowProgress(false);
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
            }
        }

        private void ShowProgress(bool show)
        {
            progressBar.Visible = show;
            if (show)
            {
                progressBar.Value = 0;
                // Force immediate UI update
                progressBar.Refresh();
            }
        }

        private void UpdateProgress(int current, int total)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action<int, int>((c, t) => 
                {
                    progressBar.Value = Math.Min(c, progressBar.Maximum);
                }), current, total);
                return;
            }
            
            progressBar.Value = Math.Min(current, progressBar.Maximum);
        }

        private async Task GenerateTeamWithProgressAsync(CancellationToken cancellationToken)
        {
            int teamSize = sldTeamSize.Value;
            bool mustEvolve = chkMustEvolve.Checked;
            bool isEgg = chkEggs.Checked;
            bool isSecret = chkSecret.Checked;
            bool isBalanced = chkBalanced.Checked;
            bool allowRegionalForms = chkRegionalForms.Checked;
            bool hasStatLimit = chkStatLimit.Checked;
            bool useMaxIVs = chkMaxIVs.Checked;
            bool includeEvoItems = chkEvoItems.Checked;
            int minStatTotal = hasStatLimit ? (int)numMinStatTotal.Value : 0;
            int maxStatTotal = hasStatLimit ? (int)numMaxStatTotal.Value : 999;
            List<byte> types = new List<byte>();

            List<PKM> team = new List<PKM>();

            // Get selected starters - handle both Species objects and string representations of regional forms
            List<Species> selectedStarters = new List<Species>();
            foreach (object item in cboStarter.CheckedItems)
            {
                if (item is Species species)
                {
                    selectedStarters.Add(species);
                }
                // Note: Regional form strings will be handled by the generation pool system
                // We don't need to parse them here since they're already in the availableSpecies list
            }

            // Get selected generations from the generation CheckedListBox
            var selectedGenerations = new List<int>();
            for (int i = 0; i < cboGeneration.CheckedItems.Count; i++)
            {
                string? item = cboGeneration.CheckedItems[i]?.ToString();
                if (!string.IsNullOrEmpty(item) && item.StartsWith("Gen ") && int.TryParse(item.Substring(4), out int gen))
                {
                    selectedGenerations.Add(gen);
                }
            }
            
            // Use the new species provider for generation pools including regional forms
            List<SpeciesForm> availableSpecies;
            if (speciesProvider != null && selectedGenerations.Count > 0)
            {
                availableSpecies = speciesProvider.GetMultiGenerationPool(selectedGenerations);
            }
            else
            {
                // Fallback: use all generations if none selected
                int generation = sav.Version.GetGeneration();
                if (speciesProvider != null)
                {
                    availableSpecies = speciesProvider.GetGenerationPool(generation, false);
                }
                else
                {
                    // Last resort fallback to old method
                    int maxSpeciesId = sav.Version.GetMaxSpeciesID();
                    availableSpecies = new List<SpeciesForm>();
                    for (int i = 1; i <= maxSpeciesId; i++)
                    {
                        availableSpecies.Add(new SpeciesForm((ushort)i, 0));
                    }
                }
            }

            int starterIndex = 0;
            int attempts = 0;
            const int maxAttempts = TeamGeneratorConstants.MaxGenerationAttempts;
            
            // Initial progress update
            UpdateProgress(0, teamSize);

            while (team.Count() < teamSize && attempts < maxAttempts)
            {
                // Check for cancellation
                cancellationToken.ThrowIfCancellationRequested();
                
                // Yield control less frequently for better performance
                if (attempts % TeamGeneratorConstants.ProgressUpdateInterval == 0)
                {
                    await Task.Delay(1, cancellationToken);
                    // Also update progress during long generation attempts
                    UpdateProgress(team.Count, teamSize);
                }

                bool pokemonIsOkay = true;

                PKM pokemon;
                PKM originalPokemon;
                List<PKM> pokemons;
                
                // Use selected starters first, then random pokemon
                if (selectedStarters.Count > 0 && starterIndex < selectedStarters.Count)
                {
                    Species species = selectedStarters[starterIndex];
                    pokemons = CreatePokemon(species, false, allowRegionalForms, useMaxIVs);
                    starterIndex++;
                }
                else
                {
                    // Select from the available species pool, filtering regional forms if needed
                    if (availableSpecies.Count > 0)
                    {
                        // Filter out regional forms if the setting is disabled
                        var filteredSpecies = allowRegionalForms 
                            ? availableSpecies 
                            : availableSpecies.Where(sf => sf.Form == 0).ToList();
                        
                        if (filteredSpecies.Count > 0)
                        {
                            var randomIndex = sharedRandom.Next(filteredSpecies.Count);
                            var selectedSpecies = filteredSpecies[randomIndex];
                            pokemons = CreatePokemonWithForm(selectedSpecies, isEgg, useMaxIVs);
                        }
                        else
                        {
                            // Fallback if no base forms available
                            int rand = sharedRandom.Next(1, sav.Version.GetMaxSpeciesID());
                            Species species = (Species)rand;
                            pokemons = CreatePokemon(species, isEgg, allowRegionalForms, useMaxIVs);
                        }
                    }
                    else
                    {
                        // Fallback to old method if no species available
                        int rand = sharedRandom.Next(1, sav.Version.GetMaxSpeciesID());
                        Species species = (Species)rand;
                        pokemons = CreatePokemon(species, isEgg, allowRegionalForms, useMaxIVs);
                    }
                }

                pokemon = pokemons[0];
                originalPokemon = pokemons[1];

                EvolutionTree et = EvolutionTree.GetEvolutionTree(sav.Version.GetContext());
                var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
                var lastEvo = evos.Last();
                var firstEvo = evos.First();

                if (mustEvolve)
                {
                    // Must evolve: only include Pokémon that have evolution forms
                    pokemonIsOkay = pokemonIsOkay && evos.Count() > 1;
                }

                // @todo: this probably doesn't work for the Eevee line
                var lastEvoPokemon = CreatePokemon((Species)lastEvo.Species, isEgg, allowRegionalForms, useMaxIVs)[0];
                var firstEvoPokemon = CreatePokemon((Species)firstEvo.Species, isEgg, allowRegionalForms, useMaxIVs)[0];

                // Check stat total if limit is enabled
                if (hasStatLimit)
                {
                    int finalEvoStatTotal = lastEvoPokemon.PersonalInfo.GetBaseStatTotal();
                    pokemonIsOkay = pokemonIsOkay && (finalEvoStatTotal >= minStatTotal && finalEvoStatTotal <= maxStatTotal);
                }

                // check if the first evo pokemon is in the current available species
                // Check if this species is in the current generation pool
                bool isInGenerationPool = availableSpecies.Any(sf => sf.Species == firstEvoPokemon.Species);
                if (isInGenerationPool)
                {
                    // we're okay
                    pokemon = firstEvoPokemon;
                }

                if (isBalanced)
                {
                    pokemonIsOkay = pokemonIsOkay && !(types.Contains(lastEvoPokemon.PersonalInfo.Type1) || types.Contains(lastEvoPokemon.PersonalInfo.Type2));
                }

                if (pokemonIsOkay)
                {
                    if (team.Count() == 0 && teamSize == 6 && isEgg)
                    {
                        pokemon.IsEgg = false;
                        pokemon.ClearNickname();

                        if (sav.PartyData != null)
                        {
                            pokemon.CurrentLevel = sav.PartyData[0].CurrentLevel;
                        }
                        pokemon.EXP = Experience.GetEXP(pokemon.CurrentLevel, pokemon.PersonalInfo.EXPGrowth);
                        pokemon.ResetPartyStats();
                    }

                    types.Add(lastEvoPokemon.PersonalInfo.Type1);
                    types.Add(lastEvoPokemon.PersonalInfo.Type2);


                    if (pokemon.IsEgg)
                    {
                        // set hatch step counter
                        int hatchRateMultiplier = GetHatchRateMultiplier();
                        pokemon.CurrentFriendship = (byte)((team.Count() + 1) * hatchRateMultiplier);
                    }
                    else
                    {
                        pokemon.ClearNickname();
                        pokemon.Language = sav.Language;
                        pokemon.Nickname = ((Species)pokemon.Species).ToString();
                        pokemon.IsNicknamed = false; // Optional: mark as not nicknamed if needed
                    }

                    if (includeEvoItems)
                    {
                        ushort? evoItem = GetEvolutionItem(originalPokemon);
                        if (evoItem != null)
                        {
                            pokemon.HeldItem = (int)evoItem;
                        }
                    }

                    team.Add(pokemon);
                    
                    // Update progress immediately
                    UpdateProgress(team.Count, teamSize);
                }
                
                attempts++;
            }

            // Check if we couldn't generate the full team
            if (team.Count < teamSize)
            {
                throw new InvalidOperationException($"Could only generate {team.Count} out of {teamSize} Pokémon after {maxAttempts} attempts. Try relaxing your criteria.");
            }

            for (int i = 0; i < team.Count; i++)
            {
                sav.SetPartySlotAtIndex(team[i], i);

                SlotTouchType slotType = SlotTouchType.Set;
                ISlotInfo slot = new SlotInfoParty(i + 1);
                editor.NotifySlotChanged(slot, slotType, team[i]);
            }

            // Refresh the party display to show the new team
            editor.SetParty();

            if (team.Count > 0 && !isSecret)
            {
                string teamResults = string.Join(Environment.NewLine, team.Select(
                    x => GetPokemonDisplayName(x) + " (" + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type1, sav.Generation) + (x.PersonalInfo.Type1 == x.PersonalInfo.Type2 ? "" : ", " + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type2, sav.Generation)) + ")"
                ).ToArray());
                // MessageBox.Show(teamResults, "Generated Team", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (team.Count > 0)
            {
                // MessageBox.Show("Team generated successfully!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ushort? GetEvolutionItem(PKM pokemon)
        {
            // items
            var items = GameInfo.Strings.Item;
            // Get the evolution tree for the current context
            var et = EvolutionTree.GetEvolutionTree(pokemon.Context);
            // Get all possible evolutions for this species/form
            var evolutions = et.Forward.GetForward(pokemon.Species, pokemon.Form);

            foreach (var evo in evolutions.Span)
            {
                if (isItemEvo(evo.Method))
                {
                    return evo.Argument;
                }
            }

            return null;
        }

        private Boolean isItemEvo(EvolutionType type)
        {
            return type is EvolutionType.TradeHeldItem or
                EvolutionType.UseItem or
                EvolutionType.UseItemMale or
                EvolutionType.UseItemFemale or
                EvolutionType.LevelUpHeldItemDay or
                EvolutionType.LevelUpHeldItemNight or
                EvolutionType.LevelUpWormhole or
                EvolutionType.UseItemFullMoon;
        }

        /// <summary>
        /// Gets the display name for a Pokémon, including form information for regional variants.
        /// </summary>
        private string GetPokemonDisplayName(PKM pokemon)
        {
            string baseName = ((Species)pokemon.Species).ToString();
            
            if (pokemon.Form == 0)
            {
                return baseName;
            }
            
            // Get form-specific name for regional variants
            string formName = GetRegionalFormName(pokemon.Species, pokemon.Form);
            if (!string.IsNullOrEmpty(formName))
            {
                return $"{baseName} ({formName})";
            }
            
            // Fallback for other forms
            return $"{baseName} (Form {pokemon.Form})";
        }

        /// <summary>
        /// Gets the regional form name for known regional variants.
        /// </summary>
        private string GetRegionalFormName(ushort species, byte form)
        {
            // Handle special cases first
            if (species == (int)Species.Meowth && form == 2)
                return "Galarian";
            
            if (species == (int)Species.Tauros && form >= 1 && form <= 3)
            {
                return form switch
                {
                    1 => "Paldean Combat",
                    2 => "Paldean Blaze", 
                    3 => "Paldean Aqua",
                    _ => "Paldean"
                };
            }
            
            // Most regional forms are form 1
            if (form == 1)
            {
                // Check for Paldean forms first (Gen 9)
                if (IsKnownPaldeanSpecies(species))
                    return "Paldean";
                
                // Check for Galarian forms (Gen 8)
                if (IsKnownGalarianSpecies(species))
                    return "Galarian";
                
                // Check for Alolan forms (Gen 7)
                if (IsKnownAlolanSpecies(species))
                    return "Alolan";
            }
            
            return string.Empty;
        }

        /// <summary>
        /// Checks if a species has any regional forms available in the current context.
        /// </summary>
        private bool HasRegionalForm(ushort species, EntityContext context)
        {
            var regionalForms = GetAvailableRegionalForms(species, context);
            return regionalForms.Length > 0;
        }

        /// <summary>
        /// Gets the available regional forms for a species in the current context.
        /// </summary>
        private byte[] GetAvailableRegionalForms(ushort species, EntityContext context)
        {
            var availableForms = new List<byte>();
            
            // Check if the species has regional forms based on the game context
            var formCount = sav.Personal[species].FormCount;
            
            for (byte form = 1; form < formCount; form++)
            {
                if (IsRegionalFormAvailable(species, form, context))
                {
                    availableForms.Add(form);
                }
            }
            
            return availableForms.ToArray();
        }

        /// <summary>
        /// Determines if a specific regional form is available in the current context.
        /// </summary>
        private bool IsRegionalFormAvailable(ushort species, byte form, EntityContext context)
        {
            // Use the PersonalTable to check if this form is present in the game
            try 
            {
                if (!sav.Personal.IsPresentInGame(species, form))
                    return false;

                // Additional checks for specific regional forms based on context
                return context.Generation() switch
                {
                    7 => IsAlolanFormValid(species, form),
                    8 => IsGalarianOrAlolanFormValid(species, form, context),
                    9 => IsPaldeanGalarianOrAlolanFormValid(species, form),
                    _ => form == 1 // For older generations, only check basic alternate forms
                };
            }
            catch
            {
                return false;
            }
        }

        private bool IsAlolanFormValid(ushort species, byte form)
        {
            // Alolan forms are form 1 for most species
            return form == 1 && IsKnownAlolanSpecies(species);
        }

        private bool IsGalarianOrAlolanFormValid(ushort species, byte form, EntityContext context)
        {
            return species switch
            {
                // Meowth has both Alolan (form 1) and Galarian (form 2) variants
                (int)Species.Meowth => form <= 2,
                _ when IsKnownGalarianSpecies(species) => form == 1, // Galarian forms are typically form 1
                _ when IsKnownAlolanSpecies(species) => form == 1, // Alolan forms are form 1
                _ => false
            };
        }

        private bool IsPaldeanGalarianOrAlolanFormValid(ushort species, byte form)
        {
            return species switch
            {
                // Tauros has multiple Paldean forms (1, 2, 3)
                (int)Species.Tauros => form <= 3 && form >= 1,
                _ when IsKnownPaldeanSpecies(species) => form == 1,
                _ when IsKnownGalarianSpecies(species) => form == 1,
                _ when IsKnownAlolanSpecies(species) => form == 1,
                _ => false
            };
        }

        private bool IsKnownAlolanSpecies(ushort species)
        {
            return species switch
            {
                (int)Species.Rattata or (int)Species.Raticate or
                (int)Species.Raichu or
                (int)Species.Sandshrew or (int)Species.Sandslash or
                (int)Species.Vulpix or (int)Species.Ninetales or
                (int)Species.Diglett or (int)Species.Dugtrio or
                (int)Species.Meowth or (int)Species.Persian or
                (int)Species.Geodude or (int)Species.Graveler or (int)Species.Golem or
                (int)Species.Grimer or (int)Species.Muk or
                (int)Species.Exeggutor or
                (int)Species.Marowak => true,
                _ => false
            };
        }

        private bool IsKnownGalarianSpecies(ushort species)
        {
            return species switch
            {
                (int)Species.Meowth or (int)Species.Ponyta or (int)Species.Rapidash or
                (int)Species.Slowpoke or (int)Species.Slowbro or (int)Species.Slowking or
                (int)Species.Farfetchd or (int)Species.Weezing or
                (int)Species.MrMime or (int)Species.Articuno or (int)Species.Zapdos or
                (int)Species.Moltres or (int)Species.Corsola or (int)Species.Zigzagoon or
                (int)Species.Linoone or (int)Species.Darumaka or (int)Species.Darmanitan or
                (int)Species.Yamask or (int)Species.Stunfisk => true,
                _ => false
            };
        }

        private bool IsKnownPaldeanSpecies(ushort species)
        {
            return species switch
            {
                (int)Species.Tauros or (int)Species.Wooper => true,
                _ => false
            };
        }
    }

    public struct SpeciesForm
    {
        public ushort Species { get; }
        public byte Form { get; }

        public SpeciesForm(ushort species, byte form)
        {
            Species = species;
            Form = form;
        }

        public override string ToString()
        {
            return Form == 0 ? $"{(Species)Species}" : $"{(Species)Species}-{Form}";
        }
    }

    public class GenerationSpeciesProvider
    {
        private readonly SaveFile _saveFile;
        private readonly Dictionary<int, List<SpeciesForm>> _generationPools;
        private readonly Dictionary<(ushort species, byte form), int> _regionalFormOrigins;
        private static readonly Dictionary<EntityContext, Dictionary<(ushort, byte), int>> _cachedRegionalForms = new();

        public GenerationSpeciesProvider(SaveFile saveFile)
        {
            _saveFile = saveFile;
            _regionalFormOrigins = GetRegionalFormOrigins(saveFile.Version.GetContext());
            _generationPools = BuildGenerationPools();
        }

        private Dictionary<(ushort species, byte form), int> GetRegionalFormOrigins(EntityContext context)
        {
            if (_cachedRegionalForms.TryGetValue(context, out var cached))
                return cached;

            var regionalForms = new Dictionary<(ushort species, byte form), int>();
            var maxSpecies = _saveFile.Version.GetMaxSpeciesID();

            for (ushort species = 1; species <= maxSpecies; species++)
            {
                if (!_saveFile.Personal.IsSpeciesInGame(species))
                    continue;

                var personalInfo = _saveFile.Personal[species];
                var formCount = personalInfo.FormCount;

                for (byte form = 1; form < formCount; form++)
                {
                    if (!_saveFile.Personal.IsPresentInGame(species, form))
                        continue;

                    var generationIntroduced = GetRegionalFormGeneration(species, form, context);
                    if (generationIntroduced > 0)
                    {
                        regionalForms[(species, form)] = generationIntroduced;
                    }
                }
            }

            _cachedRegionalForms[context] = regionalForms;
            return regionalForms;
        }

        private int GetRegionalFormGeneration(ushort species, byte form, EntityContext context)
        {
            return context.Generation() switch
            {
                7 => IsAlolanForm(species, form) ? 7 : 0,
                8 => IsGalarianForm(species, form) ? 8 : (IsAlolanForm(species, form) ? 7 : 0),
                9 => IsPaldeanForm(species, form) ? 9 : (IsGalarianForm(species, form) ? 8 : (IsAlolanForm(species, form) ? 7 : 0)),
                _ => 0
            };
        }

        private bool IsAlolanForm(ushort species, byte form)
        {
            if (form != 1) return false;

            try
            {
                var baseForm = _saveFile.Personal[species, 0];
                var regionalForm = _saveFile.Personal[species, form];

                return baseForm.Type1 != regionalForm.Type1 || 
                       baseForm.Type2 != regionalForm.Type2 ||
                       HasSignificantStatDifference(baseForm, regionalForm);
            }
            catch
            {
                return false;
            }
        }

        private bool IsGalarianForm(ushort species, byte form)
        {
            if (form != 1 && !(species == (int)Species.Meowth && form == 2))
                return false;

            try
            {
                var baseForm = _saveFile.Personal[species, 0];
                var regionalForm = _saveFile.Personal[species, form];

                return _saveFile.Version.GetContext().Generation() >= 8 &&
                       (baseForm.Type1 != regionalForm.Type1 || 
                        baseForm.Type2 != regionalForm.Type2 ||
                        HasSignificantStatDifference(baseForm, regionalForm));
            }
            catch
            {
                return false;
            }
        }

        private bool IsPaldeanForm(ushort species, byte form)
        {
            if (_saveFile.Version.GetContext().Generation() < 9)
                return false;

            try
            {
                if (species == (int)Species.Tauros && form >= 1 && form <= 3)
                    return true;

                if (species == (int)Species.Wooper && form == 1)
                    return true;

                var baseForm = _saveFile.Personal[species, 0];
                var regionalForm = _saveFile.Personal[species, form];

                return baseForm.Type1 != regionalForm.Type1 || 
                       baseForm.Type2 != regionalForm.Type2 ||
                       HasSignificantStatDifference(baseForm, regionalForm);
            }
            catch
            {
                return false;
            }
        }

        private bool HasSignificantStatDifference(IPersonalInfo baseForm, IPersonalInfo regionalForm)
        {
            var baseTotal = baseForm.GetBaseStatTotal();
            var regionalTotal = regionalForm.GetBaseStatTotal();
            
            return Math.Abs(baseTotal - regionalTotal) > 50 ||
                   Math.Abs(baseForm.HP - regionalForm.HP) > 20 ||
                   Math.Abs(baseForm.ATK - regionalForm.ATK) > 30 ||
                   Math.Abs(baseForm.DEF - regionalForm.DEF) > 30;
        }

        private Dictionary<int, List<SpeciesForm>> BuildGenerationPools()
        {
            var pools = new Dictionary<int, List<SpeciesForm>>();
            
            for (int gen = 1; gen <= _saveFile.Version.GetGeneration(); gen++)
            {
                pools[gen] = new List<SpeciesForm>();
                
                int minSpecies = GetGenerationMinSpecies(gen);
                int maxSpecies = GetGenerationMaxSpecies(gen);
                
                // Add base species that were introduced in this generation
                for (int species = minSpecies; species <= maxSpecies; species++)
                {
                    if (_saveFile.Personal.IsSpeciesInGame((ushort)species))
                    {
                        pools[gen].Add(new SpeciesForm((ushort)species, 0));
                    }
                }
                
                // Add regional variants that originated in this generation
                AddRegionalVariantsForGeneration(pools[gen], gen);
                
                // IMPORTANT: Also add base species when their regional forms are introduced
                // This allows base species to appear in later generations when their regional forms debut
                AddBaseSpeciesForRegionalForms(pools[gen], gen);
            }
            
            return pools;
        }

        /// <summary>
        /// Adds base species (form 0) to a generation pool when their regional forms are introduced in that generation.
        /// This ensures that when you select Gen 7, you get both regular Geodude and Alolan Geodude.
        /// </summary>
        private void AddBaseSpeciesForRegionalForms(List<SpeciesForm> pool, int generation)
        {
            foreach (var ((species, form), originGeneration) in _regionalFormOrigins)
            {
                if (originGeneration == generation && form > 0)
                {
                    // Check if the base species (form 0) is already in this pool
                    bool baseSpeciesExists = pool.Any(sf => sf.Species == species && sf.Form == 0);
                    
                    if (!baseSpeciesExists && _saveFile.Personal.IsSpeciesInGame(species))
                    {
                        // Add the base species (form 0) to this generation's pool
                        pool.Add(new SpeciesForm(species, 0));
                    }
                }
            }
        }

        private void AddRegionalVariantsForGeneration(List<SpeciesForm> pool, int generation)
        {
            foreach (var ((species, form), originGeneration) in _regionalFormOrigins)
            {
                if (originGeneration == generation)
                {
                    pool.Add(new SpeciesForm(species, form));
                }
            }
        }

        public List<SpeciesForm> GetGenerationPool(int generation, bool limitToGeneration)
        {
            if (limitToGeneration)
            {
                return _generationPools.ContainsKey(generation) 
                    ? _generationPools[generation] 
                    : new List<SpeciesForm>();
            }
            
            var combinedPool = new List<SpeciesForm>();
            for (int gen = 1; gen <= generation; gen++)
            {
                if (_generationPools.ContainsKey(gen))
                {
                    combinedPool.AddRange(_generationPools[gen]);
                }
            }
            return combinedPool;
        }

        public List<SpeciesForm> GetMultiGenerationPool(List<int> selectedGenerations)
        {
            var combinedPool = new List<SpeciesForm>();
            foreach (int generation in selectedGenerations)
            {
                if (_generationPools.ContainsKey(generation))
                {
                    combinedPool.AddRange(_generationPools[generation]);
                }
            }
            return combinedPool;
        }

        private int GetGenerationMinSpecies(int generation)
        {
            var generationLimits = new Dictionary<int, int>
            {
                { 1, 151 }, { 2, 251 }, { 3, 386 }, { 4, 493 }, 
                { 5, 649 }, { 6, 721 }, { 7, 809 }, { 8, 905 }, { 9, 1010 }
            };
            
            return generation == 1 ? 1 : generationLimits[generation - 1] + 1;
        }

        private int GetGenerationMaxSpecies(int generation)
        {
            var generationLimits = new Dictionary<int, int>
            {
                { 1, 151 }, { 2, 251 }, { 3, 386 }, { 4, 493 }, 
                { 5, 649 }, { 6, 721 }, { 7, 809 }, { 8, 905 }, { 9, 1010 }
            };
            
            return generationLimits.ContainsKey(generation) 
                ? generationLimits[generation]
                : _saveFile.Version.GetMaxSpeciesID();
        }

        public static void ClearCache()
        {
            _cachedRegionalForms.Clear();
        }
    }
}

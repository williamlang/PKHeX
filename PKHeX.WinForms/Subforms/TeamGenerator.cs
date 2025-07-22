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
        private bool disposed;
        private CancellationTokenSource? cancellationTokenSource;

        public TeamGenerator(SAVEditor editor)
        {
            InitializeComponent();
            this.editor = editor;
            this.sav = editor.SAV;
            this.sharedRandom = new Random();

            maxSpeciesIdByGeneration = new Dictionary<int, int>();
            for (int i = 0; i < sav.Version.GetGeneration(); i++)
            {
                cboGeneration.Items.Add(i + 1);
                GameVersion ver = GameUtil.GetVersion((byte)(i + 1));
                maxSpeciesIdByGeneration[i + 1] = GameUtil.GetMaxSpeciesID(ver);
            }
            // Add a hard-coded Generation 1 maximum since GameUtil might not handle Gen 1 properly
            maxSpeciesIdByGeneration[1] = 151;

            PopulateStarterList();
            PopulatePresets();
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
                "Fast" => 1,
                "Medium" => 2,
                "Slow" => 3,
                _ => 2 // Default to Medium
            };
        }

        private void cboStarter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Count currently checked items
            int checkedCount = cboStarter.CheckedItems.Count;
            
            // If trying to check and already at max, prevent it
            if (e.NewValue == CheckState.Checked && checkedCount >= 6)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void sldTeamSize_ValueChanged(object sender, EventArgs e)
        {
            lblTeamSizeValue.Text = sldTeamSize.Value.ToString();
        }

        private void cboGeneration_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateStarterList();
        }

        private void chkLimit_CheckedChanged(object sender, EventArgs e)
        {
            PopulateStarterList();
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

            int maxSpeciesId = sav.Version.GetMaxSpeciesID();
            int minSpeciesId = 1;

            // If a generation is selected, use that generation's max
            if (cboGeneration.SelectedItem != null)
            {
                int selectedGeneration = (int)cboGeneration.SelectedItem;
                maxSpeciesId = maxSpeciesIdByGeneration[selectedGeneration];
                
                // If limit is checked, only show that specific generation
                if (chkLimit.Checked)
                {
                    // For Gen 1, start at 1. For other generations, start after the previous generation's max
                    minSpeciesId = selectedGeneration == 1 ? 1 : maxSpeciesIdByGeneration[selectedGeneration - 1] + 1;
                }
                // If limit is not checked, show all pokemon from gen 1 up to selected generation
                else
                {
                    minSpeciesId = 1;
                }
            }

            for (int i = minSpeciesId; i <= maxSpeciesId; i++)
            {
                cboStarter.Items.Add((Species)i);
            }
        }

        private void PopulatePresets()
        {
            cboPreset.Items.Clear();
            cboPreset.Items.Add("-- Select Preset --");
            cboPreset.Items.Add("Legendary Focus");
            cboPreset.Items.Add("Starter Pokemon Only");
            cboPreset.SelectedIndex = 0;
        }

        private void cboPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPreset.SelectedIndex <= 0) return;

            string preset = cboPreset.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(preset)) return;
            
            // Clear current selections
            for (int i = 0; i < cboStarter.Items.Count; i++)
            {
                cboStarter.SetItemChecked(i, false);
            }

            switch (preset)
            {
                case "Legendary Focus":
                    if (cboGeneration.Items.Count > 0)
                        cboGeneration.SelectedIndex = cboGeneration.Items.Count - 1;
                    chkLimit.Checked = false;
                    sldTeamSize.Value = 3;
                    SelectLegendaryPokemon();
                    break;
                    
                case "Starter Pokemon Only":
                    if (cboGeneration.Items.Count > 0)
                        cboGeneration.SelectedIndex = cboGeneration.Items.Count - 1;
                    chkLimit.Checked = false;
                    sldTeamSize.Value = 6;
                    SelectAllStarters();
                    break;
            }
        }

        private void SelectStartersBySpecies(int[] speciesIds)
        {
            for (int i = 0; i < cboStarter.Items.Count; i++)
            {
                Species species = (Species)cboStarter.Items[i];
                if (speciesIds.Contains((int)species))
                {
                    cboStarter.SetItemChecked(i, true);
                }
            }
        }

        private void SelectLegendaryPokemon()
        {
            // Define some common legendary Pokemon across generations
            int[] legendaryIds = { 144, 145, 146, 150, 151, // Gen 1: Articuno, Zapdos, Moltres, Mewtwo, Mew
                                   243, 244, 245, 249, 250, 251, // Gen 2: Raikou, Entei, Suicune, Lugia, Ho-Oh, Celebi
                                   377, 378, 379, 380, 381, 382, 383, 384, 385, 386 }; // Gen 3: Regis, Latios, Latias, Kyogre, Groudon, Rayquaza, Jirachi, Deoxys

            for (int i = 0; i < cboStarter.Items.Count && cboStarter.CheckedItems.Count < 6; i++)
            {
                Species species = (Species)cboStarter.Items[i];
                if (legendaryIds.Contains((int)species))
                {
                    cboStarter.SetItemChecked(i, true);
                }
            }
        }

        private void SelectAllStarters()
        {
            // Define starter Pokemon across generations
            int[] starterIds = { 1, 4, 7, // Gen 1
                                 152, 155, 158, // Gen 2
                                 252, 255, 258, // Gen 3
                                 387, 390, 393, // Gen 4
                                 495, 498, 501, // Gen 5
                                 650, 653, 656, // Gen 6
                                 722, 725, 728, // Gen 7
                                 810, 813, 816, // Gen 8
                                 906, 909, 912 }; // Gen 9

            int checkedCount = 0;
            for (int i = 0; i < cboStarter.Items.Count && checkedCount < 6; i++)
            {
                Species species = (Species)cboStarter.Items[i];
                if (starterIds.Contains((int)species))
                {
                    cboStarter.SetItemChecked(i, true);
                    checkedCount++;
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
            PKM pokemon = EntityBlank.GetBlank(sav.Generation, sav.Version);
            pokemon.Species = (ushort)species;
            pokemon.Form = (byte)sharedRandom.Next(0, pokemon.PersonalInfo.FormCount);
            pokemon.Language = sav.Language;

            // set trainer info
            TrainerInfoExtensions.ApplyTo(sav, pokemon);

            // get met information from first party pokemon?
            if (sav.PartyData.Count > 0)
            {
                if (sav.PartyData[0].MetDate != null)
                {
                    pokemon.MetDate = sav.PartyData[0].MetDate;
                }

                // set the MetLocation to the first pokemon in the party if there is one
                pokemon.MetLocation = sav.PartyData[0].MetLocation;
            }

            // @todo: set default MetLocation
            pokemon.MetLevel = 5;

            var originalPokemon = pokemon.Clone();

            // get the base pokemon to return
            EvolutionTree et = EvolutionTree.GetEvolutionTree(sav.Version.GetContext());
            var baby = et.GetBaseSpeciesForm((ushort)species, pokemon.Form);
            pokemon.Species = baby.Species;

            CommonEdits.ClearNickname(pokemon);

            if (pokemon.PersonalInfo.Genderless)
            {
                pokemon.Gender = (int)Gender.Genderless;
            }
            else if (pokemon.PersonalInfo.OnlyFemale)
            {
                pokemon.Gender = (int)Gender.Female;
            }
            else if (pokemon.PersonalInfo.OnlyMale)
            {
                pokemon.Gender = (int)Gender.Male;
            }
            else
            {
                pokemon.Gender = (byte)sharedRandom.Next(2); // 0 = Male, 1 = Female
            }

            if (pokemon.PersonalInfo.HasForms)
            {
                pokemon.Form = (byte)sharedRandom.Next(0, pokemon.PersonalInfo.FormCount - 1);
            }

            // Handle regional forms if enabled
            if (allowRegionalForms && HasRegionalForm(pokemon.Species, sav.Version.GetContext()))
            {
                var regionalForms = GetAvailableRegionalForms(pokemon.Species, sav.Version.GetContext());
                if (regionalForms.Length > 0)
                {
                    // Include the original form (0) plus regional forms
                    var allForms = new byte[regionalForms.Length + 1];
                    allForms[0] = 0; // Original form
                    Array.Copy(regionalForms, 0, allForms, 1, regionalForms.Length);
                    
                    // Randomly select from available forms
                    pokemon.Form = allForms[sharedRandom.Next(allForms.Length)];
                }
            }

            if (isEgg)
            {
                pokemon.MetLevel = 1;
                pokemon.CurrentLevel = 1;
                pokemon.EXP = Experience.GetEXP(pokemon.CurrentLevel, pokemon.PersonalInfo.EXPGrowth);
            }

            // stats, set IVs, EVs

            Span<int> ivs = stackalloc int[6];
            EffortValueGrade[] validGrades = { EffortValueGrade.MaxLegal, EffortValueGrade.MaxNearCap, EffortValueGrade.Half, EffortValueGrade.NearFull, EffortValueGrade.MaxEffective };

            if (sav.Generation <= 2)
            {
                validGrades = validGrades.Concat(new EffortValueGrade[] { EffortValueGrade.Quarter, EffortValueGrade.Illegal }).ToArray();
            }

            if (useMaxIVs)
            {
                // Set maximum IVs (31 for each stat)
                for (int i = 0; i < 6; i++)
                {
                    ivs[i] = 31;
                }
                pokemon.SetIVs(ivs);
            }
            else
            {
                while (!validGrades.Contains(EffortValues.GetGrade(ivs.ToArray().Sum()))) {
                    pokemon.SetRandomIVs();

                    for (int i = 0; i < 6; i++)
                    {
                        ivs[i] = pokemon.GetIV(i);
                    }
                }
            }

            Span<int> evs = stackalloc int[6];
            while (!validGrades.Contains(EffortValues.GetGrade(evs.ToArray().Sum()))) {
                EffortValues.SetRandom(evs, sav.Version.GetGeneration());
                pokemon.SetEVs(evs);
            }

            pokemon.ResetPartyStats();

            // nature
            pokemon.Nature = (Nature)sharedRandom.Next(0, 24);

            // ability
            pokemon.Ability = sharedRandom.Next(0, pokemon.PersonalInfo.AbilityCount);

            // moves
            LegalityAnalysis la = new LegalityAnalysis(pokemon);
            Span<ushort> moves = stackalloc ushort[4];
            la.GetSuggestedCurrentMoves(moves, MoveSourceType.None);
            pokemon.SetMoves(moves);

            pokemon.IsEgg = isEgg;
            if (pokemon.IsEgg)
            {
                pokemon.Nickname = "Egg";
                pokemon.IsNicknamed = true;
            }

            pokemon.PID = EntityPID.GetRandomPID(Util.Rand, pokemon.Species, pokemon.Gender, 0, pokemon.Nature, pokemon.Form, 0);
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
            }
        }

        private async Task UpdateProgressAsync(int current, int total)
        {
            if (progressBar.InvokeRequired)
            {
                await Task.Run(() => progressBar.Invoke(new Action<int, int>((c, t) => 
                {
                    progressBar.Value = Math.Min(c, progressBar.Maximum);
                }), current, total));
                return;
            }
            
            progressBar.Value = Math.Min(current, progressBar.Maximum);
        }

        private async Task GenerateTeamWithProgressAsync(CancellationToken cancellationToken)
        {
            int teamSize = sldTeamSize.Value;
            bool legendariesOk = chkLegendaries.Checked;
            bool mustEvolve = chkMustEvolve.Checked;
            bool isEgg = chkEggs.Checked;
            bool isSecret = chkSecret.Checked;
            bool isBalanced = chkBalanced.Checked;
            bool isLimit = chkLimit.Checked;
            bool allowRegionalForms = chkRegionalForms.Checked;
            bool hasStatLimit = chkStatLimit.Checked;
            bool useMaxIVs = chkMaxIVs.Checked;
            bool includeEvoItems = chkEvoItems.Checked;
            int minStatTotal = hasStatLimit ? (int)numMinStatTotal.Value : 0;
            int maxStatTotal = hasStatLimit ? (int)numMaxStatTotal.Value : 999;
            List<byte> types = new List<byte>();

            List<PKM> team = new List<PKM>();

            // Get selected starters
            List<Species> selectedStarters = new List<Species>();
            foreach (Species species in cboStarter.CheckedItems)
            {
                selectedStarters.Add(species);
            }

            int generation = cboGeneration.SelectedItem == null ? sav.Version.GetGeneration() : (int)cboGeneration.SelectedItem;
            int maxSpeciesId = maxSpeciesIdByGeneration[generation];
            int minSpeciesId = 1;
            
            // Use the same logic as PopulateStarterList for consistency
            if (cboGeneration.SelectedItem != null && isLimit)
            {
                // If limit is checked, only use that specific generation
                minSpeciesId = generation == 1 ? 1 : maxSpeciesIdByGeneration[generation - 1] + 1;
            }

            int starterIndex = 0;
            int attempts = 0;
            const int maxAttempts = 1000;
            
            // Initial progress update
            await UpdateProgressAsync(0, teamSize);

            while (team.Count() < teamSize && attempts < maxAttempts)
            {
                // Check for cancellation
                cancellationToken.ThrowIfCancellationRequested();
                
                // Yield control periodically to keep UI responsive
                if (attempts % 10 == 0)
                {
                    await Task.Delay(1, cancellationToken);
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
                    int rand = sharedRandom.Next(minSpeciesId, maxSpeciesId);

                    Species species = (Species)rand;
                    pokemons = CreatePokemon(species, isEgg, allowRegionalForms, useMaxIVs);
                }

                pokemon = pokemons[0];
                originalPokemon = pokemons[1];

                if (!legendariesOk)
                {
                    pokemonIsOkay = pokemonIsOkay && !(SpeciesCategory.IsMythical(pokemon.Species) || SpeciesCategory.IsLegendary(pokemon.Species) || SpeciesCategory.IsSubLegendary(pokemon.Species));
                }

                EvolutionTree et = EvolutionTree.GetEvolutionTree(sav.Version.GetContext());
                var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
                var lastEvo = evos.Last();
                var firstEvo = evos.First();

                if (mustEvolve)
                {
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

                // check if the first evo pokemon is in the generation and can be added if we're limited
                if (isLimit)
                {
                    if (firstEvoPokemon.Species >= minSpeciesId && firstEvoPokemon.Species <= maxSpeciesIdByGeneration[generation])
                    {
                        // we're okay
                        pokemon = firstEvoPokemon;
                    }
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
                    
                    // Update progress asynchronously
                    await UpdateProgressAsync(team.Count, teamSize);
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
                    x => (Species)x.Species + " (" + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type1, sav.Generation) + (x.PersonalInfo.Type1 == x.PersonalInfo.Type2 ? "" : ", " + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type2, sav.Generation)) + ")"
                ).ToArray());
                MessageBox.Show(teamResults, "Generated Team", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (team.Count > 0)
            {
                MessageBox.Show("Team generated successfully!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
}

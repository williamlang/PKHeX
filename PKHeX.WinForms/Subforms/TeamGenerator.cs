using PKHeX.Core;
using PKHeX.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PKHeX.WinForms.Subforms
{

    // @todo: ideas: provide the evolution item with the pokemon when needed
    // this will help with Eevee's. If they come with the stone it's obvious which one was chosen
    public partial class TeamGenerator : Form
    {

        private SAVEditor editor;
        private SaveFile sav;
        Dictionary<int, int> maxSpeciesIdByGeneration;

        public TeamGenerator(SAVEditor editor)
        {
            InitializeComponent();
            this.editor = editor;
            this.sav = editor.SAV;

            cboStarter.Items.Clear();

            for (int i = 0; i < sav.Version.GetMaxSpeciesID(); i++)
            {
                cboStarter.Items.Add((Species)i);
            }

            cboStarter.SelectedIndex = 0;

            cboGeneration.Items.Clear();

            maxSpeciesIdByGeneration = new Dictionary<int, int>();
            for (int i = 0; i < sav.Version.GetGeneration(); i++)
            {
                cboGeneration.Items.Add(i + 1);
                GameVersion ver = GameUtil.GetVersion((byte)(i + 1));
                maxSpeciesIdByGeneration[i + 1] = GameUtil.GetMaxSpeciesID(ver);
            }
        }

        private void clear()
        {
            txtDebug.Text = "";
        }

        private PKM CreatePokemon(Species species, bool isEgg)
        {
            Random rnd = new Random();
            PKM pokemon = EntityBlank.GetBlank(sav.Generation, sav.Version);
            pokemon.Species = (ushort)species;
            pokemon.Form = (byte)rnd.Next(0, pokemon.PersonalInfo.FormCount);
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

            // get the base pokemon to return
            EvolutionTree et = EvolutionTree.GetEvolutionTree(sav.Version.GetContext());
            var baby = et.GetBaseSpeciesForm((ushort)species, pokemon.Form);
            pokemon.Species = baby.Species;

            CommonEdits.ClearNickname(pokemon);

            pokemon.Gender = ((byte)rnd.Next(0, 1));
            if (pokemon.PersonalInfo.Genderless)
            {
                pokemon.Gender = (int)Gender.Random;
            }

            if (pokemon.PersonalInfo.OnlyFemale)
            {
                pokemon.Gender = (int)Gender.Female;
            }

            if (pokemon.PersonalInfo.OnlyMale)
            {
                pokemon.Gender = (int)Gender.Male;
            }

            if (pokemon.PersonalInfo.HasForms)
            {
                pokemon.Form = (byte)rnd.Next(0, pokemon.PersonalInfo.FormCount - 1);
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

            while (!validGrades.Contains(EffortValues.GetGrade(ivs.ToArray().Sum()))) {
                pokemon.SetRandomIVs();

                for (int i = 0; i < 6; i++)
                {
                    ivs[i] = pokemon.GetIV(i);
                }
            }

            Span<int> evs = stackalloc int[6];
            while (!validGrades.Contains(EffortValues.GetGrade(evs.ToArray().Sum()))) { 
                EffortValues.SetRandom(evs, sav.Version.GetGeneration());
                pokemon.SetEVs(evs);
            }

            pokemon.ResetPartyStats();

            // nature
            pokemon.Nature = (Nature)rnd.Next(0, 24);

            // ability
            pokemon.Ability = rnd.Next(0, pokemon.PersonalInfo.AbilityCount);

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
            return pokemon;
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            clear();

            int teamSize = (int)numTeamSize.Value;
            bool legendariesOk = chkLegendaries.Checked;
            bool mustEvolve = chkMustEvolve.Checked;
            bool isEgg = chkEggs.Checked;
            bool isSecret = chkSecret.Checked;
            bool isBalanced = chkBalanced.Checked;
            bool isLimit = chkLimit.Checked;
            List<byte> types = new List<byte>();

            List<PKM> team = new List<PKM>();
            Random rnd = new Random();

            int generation = cboGeneration.SelectedItem == null ? sav.Version.GetGeneration() : (int)cboGeneration.SelectedItem;
            int maxSpeciesId = maxSpeciesIdByGeneration[generation];
            int minSpeciesId = isLimit ? (generation > 1 ? maxSpeciesIdByGeneration[generation - 1] + 1 : 1) : 1;

            while (team.Count() < teamSize)
            {
                bool pokemonIsOkay = true;

                PKM pokemon;
                PKM originalPokemon;
                if (cboStarter.SelectedItem != null && (Species)cboStarter.SelectedItem != Species.None && team.Count == 0)
                {
                    Species species = (Species)cboStarter.SelectedItem;
                    pokemon = CreatePokemon(species, false);
                }
                else
                {
                    int rand = rnd.Next(minSpeciesId, maxSpeciesId);                  

                    Species species = (Species)rand;
                    pokemon = CreatePokemon(species, isEgg);   
                }

                originalPokemon = pokemon;

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
                var lastEvoPokemon = CreatePokemon((Species)lastEvo.Species, isEgg);
                var firstEvoPokemon = CreatePokemon((Species)firstEvo.Species, isEgg);

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
                        pokemon.CurrentFriendship = (byte)((team.Count() + 1) * 2);
                    }

                    pokemon.ClearNickname();
                    pokemon.Language = sav.Language;
                    // To set the Pokémon's name to its species name, assign the Nickname property to the default species name.
                    // Example:
                    pokemon.Nickname = ((Species)pokemon.Species).ToString();
                    pokemon.IsNicknamed = false; // Optional: mark as not nicknamed if needed
                    team.Add(pokemon);
                }
            }

            if (team.Count > 0 && !isSecret)
            {
                txtDebug.Text = string.Join(Environment.NewLine, team.Select(
                    x => (Species)x.Species + " (" + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type1, sav.Generation) + (x.PersonalInfo.Type1 == x.PersonalInfo.Type2 ? "" : ", " + MoveTypeExtensions.GetMoveTypeGeneration((MoveType)x.PersonalInfo.Type2, sav.Generation)) + ")"
                ).ToArray());
            }
            else
            {
                txtDebug.Text = "Done!";
            }

            for (int i = 0; i < team.Count; i++)
            {
                sav.SetPartySlotAtIndex(team[i], i);

                SlotTouchType slotType = SlotTouchType.Set;
                ISlotInfo slot = new SlotInfoParty(i + 1);
                editor.NotifySlotChanged(slot, slotType, team[i]);
            }
        }
    }
}

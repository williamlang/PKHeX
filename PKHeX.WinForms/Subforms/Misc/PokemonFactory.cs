using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PKHeX.WinForms.Subforms
{
    /// <summary>
    /// Factory class for creating Pokemon with specific configurations
    /// </summary>
    public class PokemonFactory
    {
        private readonly SaveFile _saveFile;
        private readonly Random _random;

        public PokemonFactory(SaveFile saveFile, Random random)
        {
            _saveFile = saveFile;
            _random = random;
        }

        /// <summary>
        /// Creates a Pokemon based on the provided configuration
        /// </summary>
        public PKM CreatePokemon(PokemonCreationConfig config)
        {
            var pokemon = InitializeBasePokemon(config);
            SetTrainerInfo(pokemon);
            SetMetadata(pokemon, config);
            SetPhysicalAttributes(pokemon, config);
            SetStats(pokemon, config);
            SetMoves(pokemon);
            SetEggProperties(pokemon, config);
            SetFinalProperties(pokemon);
            return pokemon;
        }

        /// <summary>
        /// Creates both the final Pokemon and its original form for comparison
        /// </summary>
        public (PKM pokemon, PKM originalPokemon) CreatePokemonPair(PokemonCreationConfig config)
        {
            var pokemon = InitializeBasePokemon(config);
            var originalPokemon = pokemon.Clone();
            
            SetTrainerInfo(pokemon);
            SetMetadata(pokemon, config);
            
            // Get the base species form
            var et = EvolutionTree.GetEvolutionTree(_saveFile.Version.GetContext());
            var baby = et.GetBaseSpeciesForm(config.Species, config.Form);
            pokemon.Species = baby.Species;
            pokemon.Form = baby.Form;
            
            CommonEdits.ClearNickname(pokemon);
            SetPhysicalAttributes(pokemon, config);
            SetStats(pokemon, config);
            SetMoves(pokemon);
            SetEggProperties(pokemon, config);
            SetFinalProperties(pokemon);
            
            return (pokemon, originalPokemon);
        }

        private PKM InitializeBasePokemon(PokemonCreationConfig config)
        {
            var pokemon = EntityBlank.GetBlank(_saveFile.Generation, _saveFile.Version);
            pokemon.Species = config.Species;
            pokemon.Form = config.Form;
            pokemon.Language = _saveFile.Language;
            return pokemon;
        }

        private void SetTrainerInfo(PKM pokemon)
        {
            TrainerInfoExtensions.ApplyTo(_saveFile, pokemon);
        }

        private void SetMetadata(PKM pokemon, PokemonCreationConfig config)
        {
            // Get met information from first party pokemon
            if (_saveFile.PartyData.Count > 0)
            {
                if (_saveFile.PartyData[0].MetDate != null)
                {
                    pokemon.MetDate = _saveFile.PartyData[0].MetDate;
                }
                pokemon.MetLocation = _saveFile.PartyData[0].MetLocation;
            }

            pokemon.MetLevel = (byte)(config.IsEgg ? TeamGeneratorConstants.EggMetLevel : TeamGeneratorConstants.DefaultMetLevel);
            
            if (config.IsEgg)
            {
                pokemon.CurrentLevel = 1;
                pokemon.EXP = Experience.GetEXP(pokemon.CurrentLevel, pokemon.PersonalInfo.EXPGrowth);
            }
        }

        private void SetPhysicalAttributes(PKM pokemon, PokemonCreationConfig config)
        {
            SetGender(pokemon);
            SetForm(pokemon, config);
            SetNature(pokemon);
            SetAbility(pokemon);
        }

        private void SetGender(PKM pokemon)
        {
            if (pokemon.PersonalInfo.Genderless)
                pokemon.Gender = (int)Gender.Genderless;
            else if (pokemon.PersonalInfo.OnlyFemale)
                pokemon.Gender = (int)Gender.Female;
            else if (pokemon.PersonalInfo.OnlyMale)
                pokemon.Gender = (int)Gender.Male;
            else
                pokemon.Gender = (byte)_random.Next(2);
        }

        private void SetForm(PKM pokemon, PokemonCreationConfig config)
        {
            // If form is explicitly set, don't randomize it
            if (config.Form > 0)
            {
                pokemon.Form = config.Form;
                return;
            }

            // Handle regional forms if allowed
            if (config.AllowRegionalForms && HasRegionalForm(pokemon.Species, _saveFile.Version.GetContext()))
            {
                var regionalForms = GetAvailableRegionalForms(pokemon.Species, _saveFile.Version.GetContext());
                if (regionalForms.Length > 0)
                {
                    // Include the original form (0) plus regional forms
                    var allForms = new byte[regionalForms.Length + 1];
                    allForms[0] = 0; // Original form
                    Array.Copy(regionalForms, 0, allForms, 1, regionalForms.Length);
                    
                    // Randomly select from available forms
                    pokemon.Form = allForms[_random.Next(allForms.Length)];
                }
            }
            else if (pokemon.PersonalInfo.HasForms)
            {
                // Only randomize forms if we're not handling regional forms specifically
                pokemon.Form = (byte)_random.Next(0, pokemon.PersonalInfo.FormCount);
            }
        }

        private void SetNature(PKM pokemon)
        {
            pokemon.Nature = (Nature)_random.Next(0, TeamGeneratorConstants.MaxNatureValue + 1);
        }

        private void SetAbility(PKM pokemon)
        {
            // Get a random ability index (0, 1, or 2 for hidden ability)
            int abilityIndex = _random.Next(0, pokemon.PersonalInfo.AbilityCount);
            
            // Get the actual ability ID for that index
            int abilityID = pokemon.PersonalInfo.GetAbilityAtIndex(abilityIndex);
            
            // Set the ability using PKHeX's CommonEdits helper method to ensure proper setup
            pokemon.SetAbility(abilityID);
        }

        private void SetStats(PKM pokemon, PokemonCreationConfig config)
        {
            if (config.UseMaxIVs)
            {
                SetMaxIVs(pokemon);
            }
            else
            {
                SetRandomLegalIVs(pokemon);
            }
            SetRandomLegalEVs(pokemon);
            pokemon.ResetPartyStats();
        }

        private void SetMaxIVs(PKM pokemon)
        {
            Span<int> ivs = stackalloc int[6];
            for (int i = 0; i < 6; i++)
            {
                ivs[i] = TeamGeneratorConstants.MaxIVValue;
            }
            pokemon.SetIVs(ivs);
        }

        private void SetRandomLegalIVs(PKM pokemon)
        {
            var validGrades = GetValidGrades();
            Span<int> ivs = stackalloc int[6];
            
            do
            {
                pokemon.SetRandomIVs();
                for (int i = 0; i < 6; i++)
                {
                    ivs[i] = pokemon.GetIV(i);
                }
            } while (!validGrades.Contains(EffortValues.GetGrade(ivs.ToArray().Sum())));
        }

        private void SetRandomLegalEVs(PKM pokemon)
        {
            var validGrades = GetValidGrades();
            Span<int> evs = stackalloc int[6];
            
            do
            {
                EffortValues.SetRandom(evs, _saveFile.Version.GetGeneration());
                pokemon.SetEVs(evs);
            } while (!validGrades.Contains(EffortValues.GetGrade(evs.ToArray().Sum())));
        }

        private EffortValueGrade[] GetValidGrades()
        {
            var validGrades = TeamGeneratorConstants.StandardValidGrades;
            
            if (_saveFile.Generation <= 2)
            {
                validGrades = validGrades.Concat(TeamGeneratorConstants.LegacyValidGrades).ToArray();
            }
            
            return validGrades;
        }

        private void SetMoves(PKM pokemon)
        {
            var la = new LegalityAnalysis(pokemon);
            Span<ushort> moves = stackalloc ushort[4];
            la.GetSuggestedCurrentMoves(moves, MoveSourceType.None);
            pokemon.SetMoves(moves);
        }

        private void SetEggProperties(PKM pokemon, PokemonCreationConfig config)
        {
            pokemon.IsEgg = config.IsEgg;
            if (pokemon.IsEgg)
            {
                pokemon.Nickname = "Egg";
                pokemon.IsNicknamed = true;
                
                // Set hatch step counter based on team position and rate
                pokemon.CurrentFriendship = (byte)((config.TeamPosition + 1) * config.HatchRateMultiplier);
            }
        }

        private void SetFinalProperties(PKM pokemon)
        {
            pokemon.PID = EntityPID.GetRandomPID(Util.Rand, pokemon.Species, pokemon.Gender, 0, pokemon.Nature, pokemon.Form, 0);
        }

        #region Regional Form Helper Methods
        
        private bool HasRegionalForm(ushort species, EntityContext context)
        {
            var regionalForms = GetAvailableRegionalForms(species, context);
            return regionalForms.Length > 0;
        }

        private byte[] GetAvailableRegionalForms(ushort species, EntityContext context)
        {
            var availableForms = new List<byte>();
            var formCount = _saveFile.Personal[species].FormCount;
            
            for (byte form = 1; form < formCount; form++)
            {
                if (IsRegionalFormAvailable(species, form, context))
                {
                    availableForms.Add(form);
                }
            }
            
            return availableForms.ToArray();
        }

        private bool IsRegionalFormAvailable(ushort species, byte form, EntityContext context)
        {
            try 
            {
                if (!_saveFile.Personal.IsPresentInGame(species, form))
                    return false;

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

        // These methods should be implemented based on the existing logic in TeamGenerator
        private bool IsAlolanFormValid(ushort species, byte form) => form == 1; // Placeholder
        private bool IsGalarianOrAlolanFormValid(ushort species, byte form, EntityContext context) => form == 1; // Placeholder
        private bool IsPaldeanGalarianOrAlolanFormValid(ushort species, byte form) => form == 1; // Placeholder

        #endregion
    }
}

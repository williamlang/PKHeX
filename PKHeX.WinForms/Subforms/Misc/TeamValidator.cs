using PKHeX.Core;
using System.Collections.Generic;
using System.Linq;

namespace PKHeX.WinForms.Subforms
{
    /// <summary>
    /// Configuration for team generation validation and filtering
    /// </summary>
    public class TeamGenerationConfig
    {
        public int TeamSize { get; set; } = TeamGeneratorConstants.MaxTeamSize;
        public bool LegendariesAllowed { get; set; } = true;
        public bool MustEvolve { get; set; } = false;
        public bool IsEgg { get; set; } = false;
        public bool IsBalanced { get; set; } = false;
        public bool AllowRegionalForms { get; set; } = false;
        public bool HasStatLimit { get; set; } = false;
        public bool UseMaxIVs { get; set; } = false;
        public bool IncludeEvolutionItems { get; set; } = false;
        public int MinStatTotal { get; set; } = 0;
        public int MaxStatTotal { get; set; } = 999;
        public int HatchRateMultiplier { get; set; } = TeamGeneratorConstants.HatchRates.Medium;
        public List<Species> RequiredStarters { get; set; } = new();
        public List<int> SelectedGenerations { get; set; } = new();
    }

    /// <summary>
    /// Validates Pokemon against team generation criteria
    /// </summary>
    public class TeamValidator
    {
        private readonly SaveFile _saveFile;
        private readonly PokemonFactory _pokemonFactory;

        public TeamValidator(SaveFile saveFile, PokemonFactory pokemonFactory)
        {
            _saveFile = saveFile;
            _pokemonFactory = pokemonFactory;
        }

        public bool ValidatePokemon(PKM pokemon, TeamGenerationConfig config, List<PKM> currentTeam, List<SpeciesForm> availableSpecies)
        {
            if (!ValidateLegendaryRestriction(pokemon, config))
                return false;

            if (!ValidateEvolutionRequirement(pokemon, config))
                return false;

            if (!ValidateStatLimits(pokemon, config))
                return false;

            if (!ValidateGenerationPool(pokemon, availableSpecies))
                return false;

            if (!ValidateTeamBalance(pokemon, config, currentTeam))
                return false;

            return true;
        }

        private bool ValidateLegendaryRestriction(PKM pokemon, TeamGenerationConfig config)
        {
            if (config.LegendariesAllowed)
                return true;

            return !(SpeciesCategory.IsMythical(pokemon.Species) || 
                     SpeciesCategory.IsLegendary(pokemon.Species) || 
                     SpeciesCategory.IsSubLegendary(pokemon.Species));
        }

        private bool ValidateEvolutionRequirement(PKM pokemon, TeamGenerationConfig config)
        {
            if (!config.MustEvolve)
                return true;

            var et = EvolutionTree.GetEvolutionTree(_saveFile.Version.GetContext());
            var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
            return evos.Count() > 1;
        }

        private bool ValidateStatLimits(PKM pokemon, TeamGenerationConfig config)
        {
            if (!config.HasStatLimit)
                return true;

            // Check the final evolution's stats
            var et = EvolutionTree.GetEvolutionTree(_saveFile.Version.GetContext());
            var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
            var lastEvo = evos.Last();

            var finalEvoConfig = PokemonCreationConfig.FromSpecies((Species)lastEvo.Species, config.IsEgg, config.AllowRegionalForms, config.UseMaxIVs);
            var finalEvoPokemon = _pokemonFactory.CreatePokemon(finalEvoConfig);
            
            int finalEvoStatTotal = finalEvoPokemon.PersonalInfo.GetBaseStatTotal();
            return finalEvoStatTotal >= config.MinStatTotal && finalEvoStatTotal <= config.MaxStatTotal;
        }

        private bool ValidateGenerationPool(PKM pokemon, List<SpeciesForm> availableSpecies)
        {
            // Check if the base species is in the current generation pool
            var et = EvolutionTree.GetEvolutionTree(_saveFile.Version.GetContext());
            var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
            var firstEvo = evos.First();

            return availableSpecies.Any(sf => sf.Species == firstEvo.Species);
        }

        private bool ValidateTeamBalance(PKM pokemon, TeamGenerationConfig config, List<PKM> currentTeam)
        {
            if (!config.IsBalanced)
                return true;

            if (currentTeam.Count == 0)
                return true;

            // Get the final evolution for type checking
            var et = EvolutionTree.GetEvolutionTree(_saveFile.Version.GetContext());
            var evos = et.GetEvolutionsAndPreEvolutions(pokemon.Species, pokemon.Form);
            var lastEvo = evos.Last();

            var finalEvoConfig = PokemonCreationConfig.FromSpecies((Species)lastEvo.Species, config.IsEgg, config.AllowRegionalForms, config.UseMaxIVs);
            var finalEvoPokemon = _pokemonFactory.CreatePokemon(finalEvoConfig);

            // Check if this type combination is already in the team
            var existingTypes = new HashSet<byte>();
            foreach (var teamMember in currentTeam)
            {
                var teamMemberEvos = et.GetEvolutionsAndPreEvolutions(teamMember.Species, teamMember.Form);
                var teamMemberLastEvo = teamMemberEvos.Last();
                var teamMemberFinalConfig = PokemonCreationConfig.FromSpecies((Species)teamMemberLastEvo.Species, config.IsEgg, config.AllowRegionalForms, config.UseMaxIVs);
                var teamMemberFinal = _pokemonFactory.CreatePokemon(teamMemberFinalConfig);
                
                existingTypes.Add(teamMemberFinal.PersonalInfo.Type1);
                existingTypes.Add(teamMemberFinal.PersonalInfo.Type2);
            }

            return !existingTypes.Contains(finalEvoPokemon.PersonalInfo.Type1) && 
                   !existingTypes.Contains(finalEvoPokemon.PersonalInfo.Type2);
        }
    }
}

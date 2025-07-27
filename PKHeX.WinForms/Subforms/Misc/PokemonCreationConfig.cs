using PKHeX.Core;

namespace PKHeX.WinForms.Subforms
{
    /// <summary>
    /// Configuration class for Pokemon creation to reduce parameter count
    /// </summary>
    public class PokemonCreationConfig
    {
        public ushort Species { get; set; }
        public byte Form { get; set; }
        public bool IsEgg { get; set; }
        public bool AllowRegionalForms { get; set; }
        public bool UseMaxIVs { get; set; }
        public int TeamPosition { get; set; }
        public int HatchRateMultiplier { get; set; } = TeamGeneratorConstants.HatchRates.Medium;
        
        public static PokemonCreationConfig FromSpecies(Species species, bool isEgg = false, bool allowRegionalForms = false, bool useMaxIVs = false)
        {
            return new PokemonCreationConfig
            {
                Species = (ushort)species,
                Form = 0,
                IsEgg = isEgg,
                AllowRegionalForms = allowRegionalForms,
                UseMaxIVs = useMaxIVs
            };
        }
        
        public static PokemonCreationConfig FromSpeciesForm(SpeciesForm speciesForm, bool isEgg = false, bool useMaxIVs = false)
        {
            return new PokemonCreationConfig
            {
                Species = speciesForm.Species,
                Form = speciesForm.Form,
                IsEgg = isEgg,
                AllowRegionalForms = false, // Form is already specified
                UseMaxIVs = useMaxIVs
            };
        }
    }
}

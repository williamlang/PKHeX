using PKHeX.Core;
using System.Collections.Generic;

namespace PKHeX.WinForms.Subforms
{
    /// <summary>
    /// Constants used throughout the Team Generator
    /// </summary>
    public static class TeamGeneratorConstants
    {
        public const int MaxTeamSize = 6;
        public const int MinTeamSize = 1;
        public const int DefaultMetLevel = 5;
        public const int EggMetLevel = 1;
        public const int MaxGenerationAttempts = 1000;
        public const int MaxIVValue = 31;
        public const int MaxNatureValue = 24;
        public const int ProgressUpdateInterval = 50;
        
        // Hatch rate multipliers
        public static class HatchRates
        {
            public const int Fast = 1;
            public const int Medium = 2;
            public const int Slow = 3;
        }
        
        // Generation limits by generation - used for regional form discovery
        public static readonly Dictionary<int, int> GenerationLimits = new()
        {
            { 1, 151 },   // Kanto
            { 2, 251 },   // Johto
            { 3, 386 },   // Hoenn
            { 4, 493 },   // Sinnoh
            { 5, 649 },   // Unova
            { 6, 721 },   // Kalos
            { 7, 809 },   // Alola
            { 8, 905 },   // Galar
            { 9, 1010 },  // Paldea
        };
        
        // Valid effort value grades
        public static readonly EffortValueGrade[] StandardValidGrades = 
        { 
            EffortValueGrade.MaxLegal, 
            EffortValueGrade.MaxNearCap, 
            EffortValueGrade.Half, 
            EffortValueGrade.NearFull, 
            EffortValueGrade.MaxEffective 
        };
        
        // Additional grades for older generations
        public static readonly EffortValueGrade[] LegacyValidGrades = 
        { 
            EffortValueGrade.Quarter, 
            EffortValueGrade.Illegal 
        };
    }
}

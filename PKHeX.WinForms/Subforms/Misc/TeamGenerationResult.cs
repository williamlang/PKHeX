using PKHeX.Core;
using System;
using System.Collections.Generic;

namespace PKHeX.WinForms.Subforms
{
    /// <summary>
    /// Result of a team generation operation with success status and error information
    /// </summary>
    public class TeamGenerationResult
    {
        public bool Success { get; set; }
        public List<PKM> Team { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public TimeSpan GenerationTime { get; set; }
        public int Attempts { get; set; }
        
        public static TeamGenerationResult CreateSuccess(List<PKM> team, TimeSpan generationTime, int attempts)
        {
            return new TeamGenerationResult
            {
                Success = true,
                Team = team,
                GenerationTime = generationTime,
                Attempts = attempts
            };
        }
        
        public static TeamGenerationResult CreateFailure(string errorMessage, TimeSpan generationTime, int attempts)
        {
            return new TeamGenerationResult
            {
                Success = false,
                ErrorMessage = errorMessage,
                GenerationTime = generationTime,
                Attempts = attempts
            };
        }
        
        public static TeamGenerationResult CreatePartialSuccess(List<PKM> team, string warning, TimeSpan generationTime, int attempts)
        {
            return new TeamGenerationResult
            {
                Success = team.Count > 0,
                Team = team,
                GenerationTime = generationTime,
                Attempts = attempts,
                Warnings = new List<string> { warning }
            };
        }
    }
    
    /// <summary>
    /// Progress information for team generation
    /// </summary>
    public class GenerationProgress
    {
        public int Current { get; set; }
        public int Target { get; set; }
        public int Attempts { get; set; }
        public string? CurrentOperation { get; set; }
        
        public double PercentComplete => Target > 0 ? (double)Current / Target * 100 : 0;
    }
}

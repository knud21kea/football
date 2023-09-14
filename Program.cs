using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        string teams = FileHandler.GetTeamAbbreviations("./CSV-files/SL/teams.csv");
        string[] abbreviations = teams.Split(';');
        for (int r = 0; r < 22; r++)
        {
            string round = RoundGenerator.GenerateRoundMatches22(abbreviations, r);
            File.WriteAllText("./CSV-files/SL/rounds/round-" + (r + 1) + ".csv", round);
        }
        // after 22 rounds we need to generate 10 more rounds
        // here the top 6 teams play each other home and away
        // start with an ordered list and use a modified
        // to test we assume that the first 6 remain in the top half
        string[] topAbbreviations = new string[6];
        string[] bottomAbbreviations = new string[6];
        Array.Copy(abbreviations, 0, topAbbreviations, 0, 6);
        Array.Copy(abbreviations, 6, bottomAbbreviations, 0, 6);
        
        /* for (int r = 0; r < 10; r++)
        {
            string upperRound = RoundGenerator.GenerateRoundMatches32(topAbbreviations, r);
            string lowerRound = RoundGenerator.GenerateRoundMatches32(bottomAbbreviations, r);
            string round = upperRound + lowerRound;
            File.WriteAllText("./CSV-files/SL/rounds/round-" + (r + 23) + ".csv", round);
        } */
    }

}
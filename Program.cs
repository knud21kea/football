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
            string round = RoundGenerator.GenerateRoundMatches(abbreviations, r);
            File.WriteAllText("./CSV-files/SL/rounds/round-" + (r + 1) + ".csv", round);
        }
    }
}

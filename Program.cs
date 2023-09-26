using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        Organisation DBU = new();

        // ---------------------------------------------------------------------------------------
        // Program parameters

        bool loadData = false; // set true to generate new match data
        int leagueId = 3; // 0:D1, 1:D2, 2:D3, 3:SL
        int requiredRounds = 3; // 1-32 gives number or round tables to display
        bool showMatches = false; // set true to output the chosen leagues match results

        // ----------------------------------------------------------------------------------------

        Setup.LoadData(DBU, loadData);
        League[] leagues = DBU.Leagues.ToArray();
        League selectedLeague = leagues[leagueId];

        if (showMatches)
        {
            ShowMatches(selectedLeague);
        }

        DataHandler.JustPlayingAround(selectedLeague, requiredRounds);        
    }

    private static void ShowMatches(League l)
    {
        foreach (Round r in l.Rounds)
        {
            foreach (Match m in r.Matches)
            {
                Console.WriteLine(l.Name + ": " + l[m.HomeAbbr].Name + " v " + l[m.AwayAbbr].Name + " > " + m.Score);
            }
        }
    }
    //Console.WriteLine(DBU["NordicBet Liga"].Id); // test of find league by name working       
    //CliMenu.MainMenu(leagues); // simple front end not used
}
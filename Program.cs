using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;
using System.Globalization;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        Organisation DBU = new();

        Setup.LoadData(DBU, false); // set false to skip loading new match data        
        League[] leagues = DBU.Leagues.ToArray();

        /* foreach (League l in DBU.Leagues)
        {
            foreach (Round r in l.Rounds)
            {
                foreach (Match m in r.Matches)
                {
                    string homeName = l.FindByAbbr(m.HomeAbbr).Name;
                    string awayName = l.FindByAbbr(m.AwayAbbr).Name;
                    Console.WriteLine(l.Name + ": " + homeName + " v " + awayName + " > " + m.Score);
                }
            }
        } */

        int league = 2; // 0:D1, 1:D2, 2:D3, 3:SL
        int requiredRounds = 32; // Number of rounds that should have standings displayed
        DataHandler.JustPlayingAround(leagues[league], requiredRounds);
    }
}
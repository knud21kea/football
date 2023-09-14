using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        //RoundGenerator.UpdateData();

        Dbu DBU = new();
        Setup.LoadData(DBU);
        foreach (League league in DBU.Leagues)
        {
            Console.WriteLine("**** " + league.Name + " ****");
            foreach (Team team in league.Teams)
            {
                Console.WriteLine(team.Abbr + " : " + team.Name);
            }
            int count = 0;
            foreach (Round round in league.Rounds)
            {
                foreach (Match match in round.Matches)
            {                
                string homeName = league.FindByAbbr(match.HomeAbbr).Name;
                string awayName = league.FindByAbbr(match.AwayAbbr).Name;
                string outputString = round.Id + ": " + count + ": " + homeName + " v " + awayName + " > " + match.Score;
                Console.WriteLine(outputString);
                count++;
            }
            
        }
    }
}}
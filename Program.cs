using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        Dbu DBU = new();
        Setup.LoadData(DBU);
        foreach (League league in DBU.Leagues)
        {
            
            RoundGenerator.UpdateData22(league);
            DataHandler.PredictStandingsAfter22(league);
            RoundGenerator.UpdateData32(league);

            //if (league.Name == "NordicBet Liga")
            if (league.Name == "3F Superliga")
            {
                DataHandler.JustPlayingAround(league);
            }
        }
    }
}
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        //RoundGenerator.UpdateData("SL"); // data for 32 rounds in Superliga
        //RoundGenerator.UpdateData("D1"); // data for 32 rounds in NordicBet liga

        Dbu DBU = new();
        Setup.LoadData(DBU);
        foreach (League league in DBU.Leagues)
        {
            if (league.Name == "3F Superliga")
            {
                DataHandler.JustPlayingAround(league);
            }
        }
    }
}
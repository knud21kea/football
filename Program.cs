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
            //if (league.Name == "2. Division")
            //if (league.Name == "NordicBet Liga")
            if (league.Name == "3F Superliga")
            {
                DataHandler.JustPlayingAround(league);
            }
        }
    }
}
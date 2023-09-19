using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Xml;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        Dbu DBU = new();
        Setup.LoadData(DBU, false); // set false to skip loading new match data
        League[] leagues = DBU.Leagues.ToArray();

        int league = 0; // 0:D1, 1:D2, 2:SL
        int requiredRounds = 32;
        DataHandler.JustPlayingAround(leagues[league], requiredRounds);
    }
}
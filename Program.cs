using System.Security.Cryptography.X509Certificates;
using System.IO;

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
            Console.WriteLine("** " + league.Name + " **");
            foreach (Team team in league.Teams)
            {
                Console.WriteLine(team.Name);
            }
            
        }
    }
}
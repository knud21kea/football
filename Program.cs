using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        //RoundGenerator.UpdateData();

        Dbu DBU = new();
        string[] setups = FileHandler.GetAllSetupFiles();
        foreach (string setup in setups)
        {
            if (!string.IsNullOrEmpty(setup))
            {
                DBU.AddLeague(setup);
            }
        }
    }
}
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        string[] teams = { "AGF", "BIF", "FCK", "HIF", "LBK", "FCM", "FCN", "OBK", "RFC", "SIF", "VBK", "VFF" };
        for (int r = 0; r < 22; r++)
        {
            string round = RoundGenerator.GenerateRoundMatches(teams, r);
            File.WriteAllText("./CSV-files/SL/round-" + (r + 1) + ".csv", round);
        }
        Console.WriteLine(17 % 11);
    }
}

using System.Security.Cryptography.X509Certificates;

namespace football;
class Program
{
    static void Main(string[] args)
    {
        string[] teams = { "AGF", "BIF", "FCK", "HIF", "LBK", "FCM", "FCN", "OBK", "RFC", "SIF", "VBK", "VFF" };
        RoundGenerator.GenerateRoundMatches(teams);
    }
}

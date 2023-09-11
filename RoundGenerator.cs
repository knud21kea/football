static class RoundGenerator
{

    public static void GenerateRoundMatches(string[] teams)
    {
        Console.WriteLine("Round 1");
        for (int m = 0; m < 6; m++)
        {
            string homeTeam = teams[m];
            string awayTeam = teams[m + 6];
            string[] twoTeams = {homeTeam, awayTeam};
            Console.WriteLine(GenerateRoundData(twoTeams)); 
        }
    }

    public static string GenerateRoundData(string[] twoTeams)
    {
        // int round = 1;
        string match = twoTeams[0] + ";" + twoTeams[1] + ";" + GetGoals() + "-" + GetGoals() + ";Other data;";
        return match;
    }

    public static int GetGoals()
    {
        Random random = new();
        double rand = random.NextDouble();
        if (rand < 0.3)
        {
            return 0;
        }
        else if (rand < 0.6)
        {
            return 1;
        }
        else if (rand < 0.8)
        {
            return 2;
        }
        else if (rand < 0.9)
        {
            return 3;
        }
        else if (rand < 0.97)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
}
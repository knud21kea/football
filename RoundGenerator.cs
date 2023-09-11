static class RoundGenerator
{

    public static string GenerateRoundMatches(string[] teams, int round)
    {
        string csvForRound = "";
        for (int m = 0; m < 6; m++)
        {
            string homeTeam = teams[m];
            string awayTeam = teams[(m + 6 + round) % 11];
            string[] twoTeams = { homeTeam, awayTeam };
            if (round % 2 == 1)
            {
                Array.Reverse(twoTeams);
            }
            csvForRound += GenerateRoundData(twoTeams);
        }
        return csvForRound;
    }

    public static string GenerateRoundData(string[] twoTeams)
    {
        // int round = 1;
        string match = twoTeams[0] + ";" + twoTeams[1] + ";" + GetGoals(0) + "-" + GetGoals(-0.1) + ";Other data;" + "\n";
        return match;
    }

    public static int GetGoals(double bias) // cant score more than 3 goals away
    {
        Random random = new();
        double rand = random.NextDouble() + bias; // goals are random weighted to lower values
        if (rand < 0.35)
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
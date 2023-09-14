static class RoundGenerator
{
    
    static readonly string scheduleCode = "ABCDEFGHIJKL";
    static readonly string[,] fixtures22 = {
    {"DF","CE","AK","HI","BJ","GL"},
    {"EL","CK","FJ","DI","BH","AG"},
    {"JK","CI","AD","EF","BG","HL"},
    {"CH","FK","GI","BE","AJ","DL"},
    {"CD","GJ","EI","BF","AH","KL"},
    {"IL","CJ","AE","DH","FG","BK"},
    {"HK","DJ","EG","BI","CL","AF"},
    {"HJ","AB","DE","IK","CG","FL"},
    {"DG","FH","AI","JL","BC","EK"},
    {"EH","AL","BD","GK","IJ","CF"},
    {"AC","FI","BL","DK","GH","EJ"}
    };

    static readonly string[,] fixtures32 = {
    {"AD","BE","CF"},  
    {"BD","AF","CE"},  
    {"CD","EF","AB"},  
    {"AE","BC","DF"},  
    {"BF","DE","AC"}  
    };

    public static string GenerateRoundMatches22(string[] teams, int round)
    {
        string csvForRound = "";
        for (int m = 0; m < 6; m++)
        {
            string codes = fixtures22[round%11,m];
            string homeTeam = teams[scheduleCode.IndexOf(codes.Substring(0,1))];
            string awayTeam = teams[scheduleCode.IndexOf(codes.Substring(1,1))];
            string[] twoTeams = { homeTeam, awayTeam };
            if (round > 10) {
                Array.Reverse(twoTeams);
            }
            csvForRound += GenerateRoundData(twoTeams);
        }
        return csvForRound;
    }

    public static string GenerateRoundMatches32(string[] teams, int round)
    {
        string csvForRound = "";
        for (int m = 0; m < 3; m++)
        {        
            string codes = fixtures32[round%5,m];
            string homeTeam = teams[scheduleCode.IndexOf(codes.Substring(0,1))];
            string awayTeam = teams[scheduleCode.IndexOf(codes.Substring(1,1))];
            string[] twoTeams = { homeTeam, awayTeam };
            if (round > 4)
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
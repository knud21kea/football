namespace football;

public static class DataHandler
{
    // We are just going to play for now
    // can we just get a round? Yes
    // can we add a rounds data to a team?

    public static void JustPlayingAround(League league)
    {
        Console.WriteLine(league.Name.ToUpper());
        Team[] leagueTeams = league.Teams;
        ResetTeamStats(league);
        int testRound = 2;
        MatchesInRound(league, testRound - 1);
        MatchesInRound(league, testRound + 10);
    }

    private static void MatchesInRound(League l, int r)
    {
        int matchId = 1;
        Console.WriteLine("\nRound: " + (r +1));
        foreach (Match match in l.Rounds[r].Matches)
        {
            string homeName = l.FindByAbbr(match.HomeAbbr).Name;
            string awayName = l.FindByAbbr(match.AwayAbbr).Name;
            string outputString = l.Rounds[r].Id + ": " + matchId + ": " + homeName + " v " + awayName + " > " + match.Score;
            Console.WriteLine(outputString);
            matchId++;
        }
    }

    private static void ResetTeamStats(League l)
    {
        foreach (Team team in l.Teams)
        {
            team.ResetStats();
        }
    }
}

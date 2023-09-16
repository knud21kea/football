public class Team
{
    public string Abbr { get; set; }
    public string Name { get; set; }
    public string? Special { get; set; }
    public int GamesPlayed { get; set; }
    public int GamesWon { get; set; }
    public int GamesDrawn { get; set; }
    public int GamesLost { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference { get; set; }
    public int PointsGained { get; set; }

    public Team(string abbr, string name, string special)
    {
        Abbr = abbr;
        Name = name;
        Special = special;
    }

    public void ResetStats()
    {
        GamesPlayed = 0;
        GamesWon = 0;
        GamesDrawn = 0;
        GamesLost = 0;
        GoalsFor = 0;
        GoalsAgainst = 0;
        GoalDifference = 0;
        PointsGained = 0;
    }
}
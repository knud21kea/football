public class Match
{
    public string HomeAbbr { get; set; }
    public string AwayAbbr { get; set; }
    public string Score { get; set; }
    public string Comment { get; set; }

    public Match(string home, string away, string score, string comment)
    {
        HomeAbbr = home;
        AwayAbbr = away;
        Score = score;
        Comment = comment;
    }
}

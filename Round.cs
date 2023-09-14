public class Round
{
    public int Id { get; set; }
    public List<Match> Matches { get; set; }

    public Round(int id)
    {
        Id = id;
        Matches = new List<Match>();
    }

    public void AddMatch(Match match)
    {
        Matches.Add(match);
    }
}

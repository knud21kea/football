using System.ComponentModel;

class Organisation
{
    public List<League> Leagues { get; set; }

    public Organisation()
    {
        Leagues = new List<League>();
    }

    public void AddLeague(League league)
    {
        Leagues.Add(league);
    }
}
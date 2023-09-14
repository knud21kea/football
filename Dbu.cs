using System.ComponentModel;

class Dbu
{
    public List<League> Leagues { get; set; }

    public Dbu()
    {
        Leagues = new List<League>();
    }

    public void AddLeague(League league)
    {
        Leagues.Add(league);
    }
}
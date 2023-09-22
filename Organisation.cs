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

    public League this[string name] {
        get {
        return Leagues.FirstOrDefault(l => l.Name == name) ?? new League("??", "Unkown league", 0, 0, 0, 0, 0);
        }
    }
}
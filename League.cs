class League
{
    public string Name { get; set; }
    public int ChampionsLeague { get; set; }
    public int EuropaLeague { get; set; }
    public int ConferenceLeague { get; set; }
    public int PromotionSlots { get; set; }
    public int RelegationSlots { get; set; }
    public List<Team> Teams { get; set; }

    public League(string name, int cha, int eur, int con, int pro, int rel)
    {
        Name = name;
        ChampionsLeague = cha;
        EuropaLeague = eur;
        ConferenceLeague = con;
        PromotionSlots = pro;
        RelegationSlots = rel;
        Teams = new List<Team>();
    }

    public void AddTeam(Team team)
    {
        Teams.Add(team);
    }
}
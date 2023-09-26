using System.Reflection.Metadata.Ecma335;
using System.Collections;

public class League
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int ChampionsLeague { get; set; }
    public int EuropaLeague { get; set; }
    public int ConferenceLeague { get; set; }
    public int PromotionSlots { get; set; }
    public int RelegationSlots { get; set; }
    public Team[] Teams { get; set; } // Array as needs fixed size
    public Team[] PromotionTeams { get; set; } // Array as needs fixed size
    public Team[] RelegationTeams { get; set; } // Array as needs fixed size
    public Round[] Rounds { get; set; } // Array as needs to be iterable
    public int RoundsPlayed { get; set; }
    public int TeamsEnrolled { get; set; }

    public League(string id, string name, int cha, int eur, int con, int pro, int rel)
    {
        Id = id;
        Name = name;
        ChampionsLeague = cha;
        EuropaLeague = eur;
        ConferenceLeague = con;
        PromotionSlots = pro;
        RelegationSlots = rel;
        Teams = new Team[12];
        PromotionTeams = new Team[6];
        RelegationTeams = new Team[6];
        Rounds = new Round[32];
        RoundsPlayed = 0;
        TeamsEnrolled = 0;
    }

    public void AddTeam(Team team)
    {
        if (TeamsEnrolled < 12)
        {
            Teams[TeamsEnrolled] = team;
            TeamsEnrolled++;
        }
    }

    public void AddRound(Round round)
    {
        if (RoundsPlayed < 32)
        {
            Rounds[round.Id] = round;
            RoundsPlayed++;
        }
    }

    public Team this[string abbr]
    {
        get
        {
            return Teams.FirstOrDefault(t => t.Abbr == abbr) ?? new Team("", "Unkown team", "");
        }
    }
}
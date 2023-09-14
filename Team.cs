public class Team
{
    public string Abbr { get; set; }
    public string Name { get; set; }
    public string? Special { get; set; }

    public Team(string abbr, string name, string special)
    {
        Abbr = abbr;
        Name = name;
        Special = special;
    }
}
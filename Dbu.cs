using System.ComponentModel;

class Dbu
{
    public List<League> Leagues { get; set; }

    public Dbu()
    {
        Leagues = new List<League>();
    }

    public void AddLeague(string league)
    {
        //Console.WriteLine(league);
        string[] values = league.Split(';');
        try
        {
            string name = values[0];
            int cha = Int32.Parse(values[1]);
            int eur = Int32.Parse(values[2]);
            int con = Int32.Parse(values[3]);
            int pro = Int32.Parse(values[4]);
            int rel = Int32.Parse(values[5]);
            Leagues.Add(new League(name, cha, eur, con, pro, rel));
        }
        catch (Exception)
        {
            Console.WriteLine($"Could not create league.");
        }
    }
}
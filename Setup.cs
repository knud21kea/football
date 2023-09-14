static class Setup
{
    private static League? currentLeague;

    // try to initialise everything
    // loop over all subfolders in CSV-files
    // load the setup.cs and use to create a League
    // add League to DBU
    // load the teams file and use it to create 12 Teams
    // add teams to League
    // loop over the rounds subfolder
    // each round contains 12 matches (do we need a match class)
    // add matches to round and round to league
    // every thing has to be done whilst still in the folder or we cant find which league the data is for

    public static void LoadData(Dbu dbu)
    {
        string rootFolder = @".\CSV-files";
        try
        {
            // Get all subfolders in the current folder
            string[] subfolders = Directory.GetDirectories(rootFolder);

            // Loop through each subfolder
            foreach (string subfolder in subfolders)
            {
                string setupData = GetSetupData(subfolder);
                // create League if able
                if (!string.IsNullOrEmpty(setupData))
                {
                    try
                    {
                        string[] values = setupData.Split(';');
                        string name = values[0];
                        int cha = Int32.Parse(values[1]);
                        int eur = Int32.Parse(values[2]);
                        int con = Int32.Parse(values[3]);
                        int pro = Int32.Parse(values[4]);
                        int rel = Int32.Parse(values[5]);
                        currentLeague = new(name, cha, eur, con, pro, rel);
                        dbu.AddLeague(currentLeague);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Could not create league." + e.Message);
                    }

                    List<string> teamData = GetTeamData(subfolder);
                    // add teams to league

                    foreach (string team in teamData)
                    {
                        try
                        {
                            string[] values = team.Split(';');
                            string abbr = values[0];
                            string name = values[1];
                            string spec = values[2];
                            Team newTeam = new(abbr, name, spec);
                            currentLeague?.AddTeam(newTeam);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while reading a team file." + e.Message);
                        }
                    }

                    string[] subSubfolders = Directory.GetDirectories(subfolder);
                    foreach (string subSubfolder in subSubfolders)
                    {
                        try
                        {
                            string[] files = Directory.GetFiles(subSubfolder);                            
                            int roundId = 0;
                            foreach (string file in files)
                            {
                                using StreamReader reader = new(file);
                                Round currentRound = new(roundId);
                                string? line;
                                while ((line = reader.ReadLine()) != null)
                                {
                                    try
                                    {
                                        string[] values = line.Split(';');
                                        string home = values[0];
                                        string away = values[1];
                                        string score = values[2];
                                        string comment = values[3];
                                        Match newMatch = new(home, away, score, comment);
                                        currentRound.AddMatch(newMatch);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("An error occurred while reading a round file." + e.Message);
                                    }
                                }
                                roundId++;
                                currentLeague?.AddRound(currentRound);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error: {e.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public static string GetSetupData(string subfolder)
    {
        string setupData = "";
        try
        {
            using StreamReader reader = new(subfolder + "/setup.csv");
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                setupData = line;
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("An error occurred while reading the setup file: " + e.Message);
        }
        return setupData;
    }

    public static List<string> GetTeamData(string subfolder)
    {
        List<string> teamData = new();
        try
        {
            using StreamReader reader = new(subfolder + "/teams.csv");
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                teamData.Add(line);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("An error occurred while reading the setup file: " + e.Message);
        }
        return teamData;
    }
}

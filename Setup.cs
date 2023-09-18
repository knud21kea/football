static class Setup
{

    public static League currentLeague = new("", "", 0, 0, 0, 0, 0);
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
                        League newLeague = new(
                        subfolder[^2..],
                        values[0],
                        Int32.Parse(values[1]),
                        Int32.Parse(values[2]),
                        Int32.Parse(values[3]),
                        Int32.Parse(values[4]),
                        Int32.Parse(values[5]));
                        dbu.AddLeague(newLeague);
                        currentLeague = newLeague;
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
                            currentLeague.AddTeam(newTeam);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occurred while reading a team file." + e.Message);
                        }
                    }

                    //RoundGenerator.UpdateData22(currentLeague); Now have 22 on file
                    // need to import them here
                    RoundsAndMatches(subfolder, 0);
                    football.DataHandler.PredictStandingsAfter22(currentLeague);
                    Array.Copy(currentLeague.Teams, 0, currentLeague.PromotionTeams, 0, 6);
                    Array.Copy(currentLeague.Teams, 6, currentLeague.RelegationTeams, 0, 6);
                    RoundGenerator.UpdateData32(currentLeague); // now we have the other 10 on file
                    // need to import them (and not the first 22 again)
                    
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    private static void RoundsAndMatches(string subfolder, int startRound)
    {
        string[] subSubfolders = Directory.GetDirectories(subfolder);
        foreach (string subSubfolder in subSubfolders)
        {
            try
            {
                string[] files = Directory.GetFiles(subSubfolder);
                foreach (string file in files)
                {
                    using StreamReader reader = new(file);
                    int start = file.LastIndexOf("-") + 1;
                    int length = file.LastIndexOf(".") - start;
                    int roundId = Int32.Parse(file.Substring(start, length));
                    if (roundId > startRound)
                    {
                        Round currentRound = new(roundId - 1);
                        string? line;
                        int lineNumber = 1;
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

                                if (roundId == 23)
                                {
                                    Console.WriteLine("Test : " + newMatch.HomeAbbr + " | " + newMatch.AwayAbbr + " | " + newMatch.Score);
                                }

                                currentRound.AddMatch(newMatch);
                                lineNumber++;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("An error occurred while reading a round file." + e.Message);
                            }
                        }
                        currentLeague.AddRound(currentRound);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

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

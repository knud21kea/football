static class FileHandler
{
    public static string GetTeamAbbreviations(string filePath)
    {
        string teamAbbrs = "";
        try
        {
            using StreamReader reader = new(filePath);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(';');
                if (values.Length > 0)
                {
                    string firstElement = values[0];
                    teamAbbrs += firstElement + ";";
                }
            }
            return teamAbbrs;
        }
        catch (IOException e)
        {
            Console.WriteLine("An error occurred while reading the file: " + e.Message);
        }
        return teamAbbrs;
    }    

    public static string[] GetAllSetupFiles()
    {
        string rootFolder = @"./CSV-files";
        string[] setups = new string[4]; // application shouldn't have to cope with more than 4 leagues
        try
        {
            // Get all subfolders in the current folder
            string[] subfolders = Directory.GetDirectories(rootFolder);

            int index = 0;

            // Loop through each subfolder
            foreach (string subfolder in subfolders)
            {
                string setupData = GetSetupData(subfolder);
                setups[index] = setupData;
                index++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return setups;
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
}
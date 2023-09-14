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
}
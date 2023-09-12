static class FileHandler
{
    public static string GetTeamAbbreviations(string filePath)
    {
        string teamAbbreviations = "";
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
                    teamAbbreviations += firstElement + ";";
                }
            }
            return teamAbbreviations;
        }        
        catch (IOException e)
        {
            Console.WriteLine("An error occurred while reading the file: " + e.Message);
        }
        return teamAbbreviations;
    }
}
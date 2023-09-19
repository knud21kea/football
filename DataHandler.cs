using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace football;

public static class DataHandler
{
    // We are just going to play for now
    // can we just get a round? Yes


    public static void JustPlayingAround(League league, int roundsPlayed)
    {
        ResetTeamStats(league);
        for (int i = 0; i < roundsPlayed; i++)
        {
            bool roundProcessed = ProcessOneRound(league, i);
            if (!roundProcessed)
            {
                return;
            }
            else if (i < 22)
            {
                OutputStandings22(league, i + 1);
            }
            else
            {
                OutputStandings32(league, i + 1);
            }
        }
    }

    public static void PredictStandingsAfter22(League league)
    {
        ResetTeamStats(league);
        int roundsPlayed = 22;
        for (int i = 0; i < roundsPlayed; i++)
        {
            ProcessOneRound(league, i);
        }
        Array.Sort(league.Teams, new TeamComparer());
    }

    private static bool ProcessOneRound(League league, int round)
    {
        int matchId = 1;
        int r = round;
        League l = league;
        try
        {
            foreach (Match m in l.Rounds[r].Matches)
            {
                Team homeTeam = l.FindByAbbr(m.HomeAbbr);
                Team awayTeam = l.FindByAbbr(m.AwayAbbr);
                string[] values = m.Score.Split('-');
                int homeGoals = Int32.Parse(values[0]);
                homeTeam.GoalsFor += homeGoals;
                awayTeam.GoalsAgainst += homeGoals;
                int awayGoals = Int32.Parse(values[1]);
                awayTeam.GoalsFor += awayGoals;
                homeTeam.GoalsAgainst += awayGoals;
                if (homeGoals > awayGoals)
                {
                    homeTeam.GamesWon++;
                    awayTeam.GamesLost++;
                    UpdateStreak(homeTeam, "W");
                    UpdateStreak(awayTeam, "L");
                    homeTeam.PointsGained += 3;

                }
                else if (homeGoals == awayGoals)
                {
                    homeTeam.GamesDrawn++;
                    awayTeam.GamesDrawn++;
                    UpdateStreak(homeTeam, "D");
                    UpdateStreak(awayTeam, "D");
                    homeTeam.PointsGained++;
                    awayTeam.PointsGained++;
                }
                else
                {
                    homeTeam.GamesLost++;
                    awayTeam.GamesWon++;
                    UpdateStreak(homeTeam, "L");
                    UpdateStreak(awayTeam, "W");
                    awayTeam.PointsGained += 3;
                }
                homeTeam.GamesPlayed++;
                awayTeam.GamesPlayed++;
                homeTeam.GoalDifference = homeTeam.GoalsFor - homeTeam.GoalsAgainst;
                awayTeam.GoalDifference = awayTeam.GoalsFor - awayTeam.GoalsAgainst;


                // Points
                matchId++;
            }
        }
        catch (Exception)
        {
            Console.WriteLine($"Something wrong with round data for round: {r + 1}");
            Console.WriteLine($"Check file exists and has name: round-{r + 1}.csv");
            return false;
        }
        return true;
    }

    private static void UpdateStreak(Team t, string r)
    {
        t.StreakFive = string.Concat(r, t.StreakFive.AsSpan(0, 4));
    }

    private static void OutputStandings22(League l, int r)
    {
        Array.Sort(l.Teams, new TeamComparer());

        Console.WriteLine("\n\x1B[36mStandings for league: " + l.Name + " after round: " + r + "\x1B[0m");
        string tableFormat = "|{0,3}|{1,4}|{2,-30}|{3,3}|{4,3}|{5,3}|{6,3}|{7,3}|{8,3}|{9,3}|{10,3}|{11,-6}|";
        Console.WriteLine(
        String.Format(tableFormat, "#", "S", "Team", "MP", "W", "D", "L", "GF", "GA", "GD", "Pts", "Form"));
        for (int i = 0; i < 12; i++)
        {
            OutputTableRow(l, l.Teams, i, 0, tableFormat);
        }
    }

    private static void OutputStandings32(League l, int r)
    {
        Array.Sort(l.PromotionTeams, new TeamComparer());
        Array.Sort(l.RelegationTeams, new TeamComparer());

        string tableFormat = "|{0,3}|{1,4}|{2,-30}|{3,3}|{4,3}|{5,3}|{6,3}|{7,3}|{8,3}|{9,3}|{10,3}|{11,-6}|";
        Console.WriteLine("\n\x1B[32mStandings for league: " + l.Name + " Promotion Group after round: " + r + "\x1B[0m");
        Console.WriteLine(
        String.Format(tableFormat, "#", "S", "Team", "MP", "W", "D", "L", "GF", "GA", "GD", "Pts", "Form"));
        for (int i = 0; i < 6; i++)
        {
            OutputTableRow(l, l.PromotionTeams, i, 0, tableFormat);
        }
        Console.WriteLine("\n\x1B[31mStandings for league: " + l.Name + " Relegation Group after round: " + r + "\x1B[0m");
        Console.WriteLine(
        String.Format(tableFormat, "#", "S", "Team", "MP", "W", "D", "L", "GF", "GA", "GD", "Pts", "Form"));
        for (int i = 0; i < 6; i++)
        {
            OutputTableRow(l, l.RelegationTeams, i, 6, tableFormat);
        }
    }

    private static void OutputTableRow(League l,Team[] teams, int i, int offset, string format)
    {
        Team t = teams[i];
        string position = (i + offset + 1).ToString();
        string pos = position; // for SL qualification colouring
        if (i > 1)
        {
            Team pt = teams[i - 1];
            if ((t.PointsGained == pt.PointsGained) && (t.GoalDifference == pt.GoalDifference) && (t.GoalsFor == pt.GoalsFor))
            {
                position = " -";
            }
        }
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Write(
        String.Format(format,
            ColourPosition(l, position, pos),
            "(" + t.Special + ")",
            t.Name + " (" + t.Abbr + ")",
            t.GamesPlayed,
            t.GamesWon,
            t.GamesDrawn,
            t.GamesLost,
            t.GoalsFor,
            t.GoalsAgainst,
            t.GoalDifference,
            t.PointsGained,
            ColourStreak(t.StreakFive ?? "-----")));
        Console.BackgroundColor = ConsoleColor.Black;
        Console.WriteLine();

    }

    private static string ColourStreak(string streak)
    {
        string[] s = new string[5];
        for (int i = 0; i < 5; i++)
        {
            s[i] = AddColours(streak.Substring(i, 1));
        }
        return s[0] + s[1] + s[2] + s[3] + s[4] + " \x1B[100m";
    }

    private static string ColourPosition(League l, string pos, string p)
    {
        if (p == "12" && l.RelegationSlots > 0 || p == "11" && l.RelegationSlots > 1 || p == "10" && l.RelegationSlots > 2)
        pos = "\x1B[101m " + pos + "\x1B[100m";
        else if (p == "1" && l.PromotionSlots > 0 || p == "2" && l.PromotionSlots > 1 || p == "3" && l.PromotionSlots > 2)
        pos = "\x1B[42m  " + pos + "\x1B[100m";
        else if (p == "1" && l.ChampionsLeague > 0)
        pos = "\x1B[44m  " + pos + "\x1B[100m";
        else if (p == "2" && l.EuropaLeague > 0)
        pos = "\x1B[106m  " + pos + "\x1B[100m";
        else if (p == "3" && l.EuropaLeague > 0)
        pos = "\x1B[105m  " + pos + "\x1B[100m";
        return pos;
    }

    private static string AddColours(string s)
    {
        if (s == "W")
            s = "\x1B[42m" + s;
        else if (s == "L")
        {
            s = "\x1B[101m" + s;
        }
        else
        {
            s = "\x1B[43m" + s;
        }
        return s;
    }

    private static void MatchesInRound(League l, int r)
    {
        int matchId = 1;
        Console.WriteLine("\nRound: " + (r + 1));
        foreach (Match match in l.Rounds[r].Matches)
        {
            string homeName = l.FindByAbbr(match.HomeAbbr).Name;
            string awayName = l.FindByAbbr(match.AwayAbbr).Name;
            string outputString = l.Rounds[r].Id + ": " + matchId + ": " + homeName + " v " + awayName + " > " + match.Score;
            Console.WriteLine(outputString);
            matchId++;
        }
    }

    private static void ResetTeamStats(League l)
    {
        foreach (Team team in l.Teams)
        {
            team.ResetStats();
        }
    }
}

internal class TeamComparer : IComparer<Team>
{
    public int Compare(Team? x, Team? y)
    {
        if (x is Team tX && y is Team tY)
        {
            int pointsComparison = (new CaseInsensitiveComparer()).Compare(tY.PointsGained, tX.PointsGained);
            if (pointsComparison == 0)
            {
                int goalDifferenceComparison = tY.GoalDifference.CompareTo(tX.GoalDifference);
                if (goalDifferenceComparison == 0)
                {
                    int goalsAgainstComparison = tY.GoalsAgainst.CompareTo(tX.GoalsAgainst);
                    if (goalsAgainstComparison == 0)
                    {
                        return tX.Name.CompareTo(tY.Name);
                    }
                    return goalsAgainstComparison;
                }
                return goalDifferenceComparison;
            }
            return pointsComparison;
        }
        else
        {
            throw new ArgumentException("Both objects must be of type Team");
        }
    }
}

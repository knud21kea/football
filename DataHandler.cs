using System;
using System.Collections;

namespace football;

public static class DataHandler
{
    // We are just going to play for now
    // can we just get a round? Yes


    public static void JustPlayingAround(League league)
    {
        Console.WriteLine("\n" + league.Name.ToUpper());
        ResetTeamStats(league);

        //MatchesInRound(league, testRound + 10);
        // can we add a rounds data to a team?
        // need to process the data in each match for a round
        // working for one round, try with two - success

        int roundsPlayed = 22;
        for (int i = 0; i < roundsPlayed; i++)
        {
            //MatchesInRound(league, i);
            ProcessOneRound(league, i);
            OutputStandings(league, i + 1);
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

    private static void ProcessOneRound(League league, int round)
    {
        int matchId = 1;
        int r = round;
        League l = league;
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

    private static void UpdateStreak(Team t, string r)
    {
        t.StreakFive = string.Concat(r, t.StreakFive.AsSpan(0, 4));
    }

    private static void OutputStandings(League l, int r)
    {
        Array.Sort(l.Teams, new TeamComparer());

        Console.WriteLine("Standings for league: " + l.Name + " after round: " + r);
        string tableFormat = "|{0,3}|{1,4}|{2,-30}|{3,3}|{4,3}|{5,3}|{6,3}|{7,3}|{8,3}|{9,3}|{10,3}|{11,-6}|";
        Console.WriteLine(
        String.Format(tableFormat, "#", "S", "Team", "MP", "W", "D", "L", "GF", "GA", "GD", "Pts", "Form"));

        string position;
        for (int i = 0; i < 12; i++)
        {
            Team t = l.Teams[i];
            position = (i + 1).ToString();
            if (i > 1)
            {
                Team pt = l.Teams[i - 1];
                if ((t.PointsGained == pt.PointsGained) && (t.GoalDifference == pt.GoalDifference))
                {
                    position = "-";
                }
            }
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(
            String.Format(tableFormat,
                position,
                "(" + t.Special + ")",
                t.Name + "(" + t.Abbr + ")",
                t.GamesPlayed,
                t.GamesWon,
                t.GamesDrawn,
                t.GamesLost,
                t.GoalsFor,
                t.GoalsAgainst,
                t.GoalDifference,
                t.PointsGained,
                t.StreakFive));
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
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

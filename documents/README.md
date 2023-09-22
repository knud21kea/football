Assignment: Football standings.

KEA mandatory C# project for 4. term Computer Science autumn 2023

All content @author Graham Heaven

-------------------------------------------------------------------------------------------------------

Overview:

The application generates a complete season's reults for each of 12 teams in each of 4 tiers.
The general process used is:
- (app) creates an Organisation object (DBU) with an empty list of League objects.
- (user) is required to set 4 parameters loadData, leagueId, requiredRounds and showMatches
- (app) generates, saves, imports appropriate data for all existing League
- (app) processes data for required rounds and updates stats for relevant Team
- (app) outputs sorted (PTS/GD/GF/Name) tables of standings for all Round up to required number

Models [Organisation(League(Team, Round(Match)))]
Sevices [Setup(RoundGenerator(FileHandler)), DataHandler(TeamComparer)]

----------------------------------------------------------------------------------------------------------

TLDR;

Setup class creates, saves and imports all data in correct format.
- find all setup.csv in any subfolder of CSV-files
- for each setup create League and add to Organisation
- for each found setup find teams.csv in same folder
- for each teams create Team and add to League. Cannot add more than 12
- create 32 Rounds and add to League
- for each Round generate puesdo random match data for 12 teams fullfilling requirements for schedule organisation
- from this data create 6 Match and add to Round (uses RoundGenerator)
- save each Round as .csv and import

DataHandler class processes and outputs data.
- for required League and rounds process Round sequentially, update stats for Team, output sorted table/tables
- some data is coloured to show potential promotion/relegation and form for last 5 matches

---------------------------------------------------------------------------------------------------

A call to the static class Setup's LoadData() then initialises all data by;
- looping over all subfolders in /CSV-files and processing any setup.csv files found.
- each setup is loaded, parsed and data used to create a League object which is added to DBUs list
- from the same folder any teams.csv file is attempted to be loaded and parsed
- from each line of teams.csv the data is used to create a Team object which is added to the leagues list of Teams
- if loadData was set true, RoundGenerato22 creates 22 sets of match results and saves them as round-(1-22).csv in /rounds subfolder by:
-- for 11 rounds looking up in a matrix (fixtures22) a home team and away team, then assigning a semi random score. This ensures that all teams play each other exactly once home and away in the first 11 rounds.
-- For the next 11 rounds the same matrix is used with home/away reversed and a new score.
- A call to DataHandler.PredictStandingsAfter22 gives the future standings after 22 rounds.
- The top 6 Teams there are copied to a new array of teams for the promotion group.
- The bottom 6 are copied to a new array of Teams for the relegation group.
- Round Generator32 creates the last 10 matches for both groups and saves them as round-(23-32).csv in /rounds
- regardless of if new data, Setup loops over the round-(1-32).csv files and uses them to create Round objects which are added to the leagues list of Rounds.
- each line in a round file is used to create a Match object which is added to the Rounds list of matches.

All data is now created, saved and imported.

-----------------------------------------------------------------------------------------------------------------

The user then configures the output by:
selecting one of 4 leagues to display by setting parameter leagueId
selecting how many rounds to process by setting parameter required rounds
selecting whether to show a list of all matches in this league by setting parameter showMatches
There is CLI to do this but it doesn't really add anything and is depreciated (CliMenu)

DataHandler.JustPlayingAround then processes and outputs the required rounds for the selected league.

------------------------------------------------------------------------------------------------------------------

Known issues and test see Test folder

-------------------------------------------------------------------------------------------------------------
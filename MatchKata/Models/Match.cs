using MatchKata.Enums;
using MatchKata.Services;

namespace MatchKata.Models
{
    public class Match
    {
        private const char SecondHalfSymbol = ';';
        private const string HomeGoalSymbol = "H";
        private const string AwayGoalSymbol = "A";
        public int Id { get; set; }
        public int LivePeriod { get; set; }
        public string GoalRecord { get; set; }

        public void ProcessGoalRecord(MatchEvent matchEvent)
        {
            if (IsNeedToAddHalfSymbol())
            {
                GoalRecord += ";";
            }

            if (matchEvent.EnumMatchEvent == EnumMatchEvent.HomeGoal)
            {
                AddRecord(HomeGoalSymbol);
            }
            else if (matchEvent.EnumMatchEvent == EnumMatchEvent.AwayGoal)
            {
                AddRecord(AwayGoalSymbol);
            }
            else if ((matchEvent.EnumMatchEvent == EnumMatchEvent.CancelHomeGoal && GoalRecord.EndsWith(HomeGoalSymbol)) || (matchEvent.EnumMatchEvent == EnumMatchEvent.CancelAwayGoal && GoalRecord.EndsWith(AwayGoalSymbol)))
            {
                CancelRecord();
            }
        }

        private string CancelRecord()
        {
            return GoalRecord = GoalRecord.Substring(0, GoalRecord.Length - 1);
        }

        private string AddRecord(string record)
        {
            return GoalRecord += record;
        }

        private bool IsNeedToAddHalfSymbol()
        {
            return LivePeriod == 2 && !GoalRecord.Contains(SecondHalfSymbol);
        }
    }
}
using System;
using System.Collections.Generic;
using MatchKata.Enums;
using MatchKata.Services;

namespace MatchKata.Models
{
    public class Match
    {
        private const string HomeGoalSymbol = "H";
        private const string AwayGoalSymbol = "A";
        private const char SecondHalfSymbol = ';';
        public int Id { get; set; }
        public int LivePeriod { get; set; }
        public string GoalRecord { get; set; }

        public void ProcessGoalRecord(MatchEvent matchEvent)
        {
            if (IsNeedToAddHalfSymbol())
            {
                GoalRecord += SecondHalfSymbol;
            }
            ProcessMatchEvent(matchEvent);
        }

        private void ProcessMatchEvent(MatchEvent matchEvent)
        {
            var processRecordLookup = new Dictionary<EnumMatchEvent, Action>()
            {
                [EnumMatchEvent.HomeGoal] = () => AddRecord(HomeGoalSymbol),
                [EnumMatchEvent.AwayGoal] = () => AddRecord(AwayGoalSymbol),
                [EnumMatchEvent.CancelHomeGoal] = () => CancelRecord(HomeGoalSymbol),
                [EnumMatchEvent.CancelAwayGoal] = () => CancelRecord(AwayGoalSymbol),
            };

            processRecordLookup[matchEvent.EnumMatchEvent]();
        }

        private void CancelRecord(string symbol)
        {
            if (GoalRecord.EndsWith(symbol + SecondHalfSymbol))
            {
                GoalRecord = GoalRecord.Remove(GoalRecord.Length - 2, 1);
            }
            else if (GoalRecord.EndsWith(symbol))
            {
                GoalRecord = GoalRecord.Remove(GoalRecord.Length - 1, 1);
            }
        }

        private void AddRecord(string symbol)
        {
            GoalRecord += symbol;
        }

        private bool IsNeedToAddHalfSymbol()
        {
            return LivePeriod == 2 && !GoalRecord.Contains(SecondHalfSymbol);
        }
    }
}
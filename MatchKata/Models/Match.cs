using System;
using System.Collections.Generic;
using MatchKata.Enums;
using MatchKata.Services;

namespace MatchKata.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int LivePeriod { get; set; }
        public string GoalRecord { get; set; }

        public void ProcessGoalRecord(MatchEvent matchEvent)
        {
            if (LivePeriod == 2 && !GoalRecord.Contains(';'))
            {
                GoalRecord += ';';
            }

            var processRecordLookup = new Dictionary<EnumMatchEvent, Action>()
            {
                [EnumMatchEvent.HomeGoal] = () =>
                {
                    GoalRecord += "H";
                },
                [EnumMatchEvent.AwayGoal] = () =>
                {
                    GoalRecord += "A";
                },
                [EnumMatchEvent.CancelHomeGoal] = () =>
                {
                    if (GoalRecord.EndsWith("H" + ';'))
                    {
                        GoalRecord = GoalRecord.Remove(GoalRecord.Length - 2, 1);
                    }
                    else if (GoalRecord.EndsWith("H"))
                    {
                        GoalRecord = GoalRecord.Remove(GoalRecord.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
                [EnumMatchEvent.CancelAwayGoal] = () =>
                {
                    if (GoalRecord.EndsWith("A" + ';'))
                    {
                        GoalRecord = GoalRecord.Remove(GoalRecord.Length - 2, 1);
                    }
                    else if (GoalRecord.EndsWith("A"))
                    {
                        GoalRecord = GoalRecord.Remove(GoalRecord.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
            };

            processRecordLookup[matchEvent.EnumMatchEvent]();
        }
    }
}
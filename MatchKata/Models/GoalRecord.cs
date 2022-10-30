using System;
using System.Collections.Generic;
using MatchKata.Enums;

namespace MatchKata.Models
{
    public class GoalRecord
    {
        private const string HomeGoalSymbol = "H";
        private const string AwayGoalSymbol = "A";
        private const char SecondHalfSymbol = ';';

        public GoalRecord(string record)
        {
            Record = record;
        }

        public string Record { get; private set; }

        public void AddEvent(EnumMatchEvent matchEvent, int livePeriod)
        {
            if (IsNeedToAddHalfSymbol(livePeriod))
            {
                Record += SecondHalfSymbol;
            }

            ProcessMatchEvent(matchEvent);
        }

        private void ProcessMatchEvent(EnumMatchEvent matchEvent)
        {
            var processRecordLookup = new Dictionary<EnumMatchEvent, Action>()
            {
                [EnumMatchEvent.HomeGoal] = () => { Add(HomeGoalSymbol); },
                [EnumMatchEvent.AwayGoal] = () => { Add(AwayGoalSymbol); },
                [EnumMatchEvent.CancelHomeGoal] = () => { Cancel(HomeGoalSymbol); },
                [EnumMatchEvent.CancelAwayGoal] = () => { Cancel(AwayGoalSymbol); },
            };

            processRecordLookup[matchEvent]();
        }

        private void Cancel(string symbol)
        {
            if (Record.EndsWith(symbol + SecondHalfSymbol))
            {
                Record = Record.Remove(Record.Length - 2, 1);
            }
            else if (Record.EndsWith(symbol))
            {
                Record = Record.Remove(Record.Length - 1, 1);
            }
            else
            {
                throw new Exception();
            }
        }

        private void Add(string symbol)
        {
            Record += symbol;
        }

        private bool IsNeedToAddHalfSymbol(int livePeriod)
        {
            return livePeriod == 2 && !Record.Contains(SecondHalfSymbol);
        }
    }
}
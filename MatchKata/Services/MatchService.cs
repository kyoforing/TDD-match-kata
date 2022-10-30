using System;
using System.Collections.Generic;
using MatchKata.Enums;
using MatchKata.Models;
using MatchKata.Repositories;

namespace MatchKata.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public void AddEvent(MatchEvent matchEvent)
        {
            var match = _matchRepository.GetMatch(matchEvent.Id);

            var goalRecord = new GoalRecord(match.GoalRecord);

            goalRecord.IsNeedToAddHalfSymbol(match);

            var processRecordLookup = new Dictionary<EnumMatchEvent, Action>()
            {
                [EnumMatchEvent.HomeGoal] = () =>
                {
                    goalRecord.Record += "H";
                },
                [EnumMatchEvent.AwayGoal] = () =>
                {
                    goalRecord.Record += "A";
                },
                [EnumMatchEvent.CancelHomeGoal] = () =>
                {
                    if (goalRecord.Record.EndsWith("H" + ';'))
                    {
                        goalRecord.Record = goalRecord.Record.Remove(goalRecord.Record.Length - 2, 1);
                    }
                    else if (goalRecord.Record.EndsWith("H"))
                    {
                        goalRecord.Record = goalRecord.Record.Remove(goalRecord.Record.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
                [EnumMatchEvent.CancelAwayGoal] = () =>
                {
                    if (goalRecord.Record.EndsWith("A" + ';'))
                    {
                        goalRecord.Record = goalRecord.Record.Remove(goalRecord.Record.Length - 2, 1);
                    }
                    else if (goalRecord.Record.EndsWith("A"))
                    {
                        goalRecord.Record = goalRecord.Record.Remove(goalRecord.Record.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
            };

            processRecordLookup[matchEvent.EnumMatchEvent]();
            match.GoalRecord = goalRecord.Record;
            
            _matchRepository.UpdateMatch(match);
        }
    }

    public class GoalRecord
    {
        public string Record { get; set; }
        
        public GoalRecord(string record)
        {
            Record = record;
        }

        public void IsNeedToAddHalfSymbol(Match match)
        {
            if (match.LivePeriod == 2 && !Record.Contains(';'))
            {
                Record += ';';
            }
        }
    }
}
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

            if (match.LivePeriod == 2 && !match.GoalRecord.Contains(';'))
            {
                match.GoalRecord += ';';
            }

            var processRecordLookup = new Dictionary<EnumMatchEvent, Action>()
            {
                [EnumMatchEvent.HomeGoal] = () =>
                {
                    match.GoalRecord += "H";
                },
                [EnumMatchEvent.AwayGoal] = () =>
                {
                    match.GoalRecord += "A";
                },
                [EnumMatchEvent.CancelHomeGoal] = () =>
                {
                    if (match.GoalRecord.EndsWith("H" + ';'))
                    {
                        match.GoalRecord = match.GoalRecord.Remove(match.GoalRecord.Length - 2, 1);
                    }
                    else if (match.GoalRecord.EndsWith("H"))
                    {
                        match.GoalRecord = match.GoalRecord.Remove(match.GoalRecord.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
                [EnumMatchEvent.CancelAwayGoal] = () =>
                {
                    if (match.GoalRecord.EndsWith("A" + ';'))
                    {
                        match.GoalRecord = match.GoalRecord.Remove(match.GoalRecord.Length - 2, 1);
                    }
                    else if (match.GoalRecord.EndsWith("A"))
                    {
                        match.GoalRecord = match.GoalRecord.Remove(match.GoalRecord.Length - 1, 1);
                    }
                    else
                    {
                        throw new Exception();
                    }
                },
            };

            processRecordLookup[matchEvent.EnumMatchEvent]();
            _matchRepository.UpdateMatch(match);
        }
    }
}
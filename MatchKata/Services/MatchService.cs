using MatchKata.Enums;
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
                match.GoalRecord += ";";
            }

            if (matchEvent.EnumMatchEvent == EnumMatchEvent.HomeGoal)
            {
                match.GoalRecord += "H";
            }
            else if (matchEvent.EnumMatchEvent == EnumMatchEvent.CancelHomeGoal && match.GoalRecord.EndsWith("H"))
            {
                match.GoalRecord = match.GoalRecord.Substring(0, match.GoalRecord.Length - 1);
            }
            else if (matchEvent.EnumMatchEvent == EnumMatchEvent.CancelAwayGoal && match.GoalRecord.EndsWith("A"))
            {
                match.GoalRecord = match.GoalRecord.Substring(0, match.GoalRecord.Length - 1);
            }
            else
            {
                match.GoalRecord += "A";
            }

            _matchRepository.UpdateMatch(match);
        }
    }
}
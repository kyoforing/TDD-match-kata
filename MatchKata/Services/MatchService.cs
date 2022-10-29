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
            else
            {
                match.GoalRecord += "A";
            }

            _matchRepository.UpdateMatch(match);
        }
    }
}
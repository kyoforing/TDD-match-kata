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
            goalRecord.AddEvent(matchEvent.EnumMatchEvent, match.LivePeriod);
            
            _matchRepository.UpdateGoalRecord(goalRecord);
        }
    }
}
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
            match.ProcessGoalRecord(matchEvent);
            _matchRepository.UpdateMatch(match);
        }
    }
}
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
            _matchRepository.UpdateMatch(new Match
            {
                GoalRecord = "H" 
            });
        }
    }
}
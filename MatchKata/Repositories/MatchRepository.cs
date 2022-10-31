using MatchKata.Models;

namespace MatchKata.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public void UpdateMatch(Match match)
        {
        }

        public Match GetMatch(int id)
        {
            return new Match();
        }
    }
}
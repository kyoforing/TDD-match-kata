using MatchKata.Models;

namespace MatchKata.Repositories
{
    public interface IMatchRepository
    {
        void UpdateMatch(Match match);
        Match GetMatch(int id);
    }
}
using MatchKata.Models;

namespace MatchKata.Repositories
{
    public interface IMatchRepository
    {
        void UpdateGoalRecord(GoalRecord match);
        Match GetMatch(int id);
    }
}
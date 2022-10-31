using MatchKata.Models;

namespace MatchKata.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        public void UpdateGoalRecord(GoalRecord goalRecord)
        {
        }

        public Match GetMatch(int id)
        {
            return new Match();
        }
    }
}
using MatchKata.Enums;

namespace MatchKata.Services
{
    public interface IMatchService
    {
        void AddEvent(int id, MatchEvent matchEvent);
    }
}
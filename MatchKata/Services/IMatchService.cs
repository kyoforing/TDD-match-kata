using MatchKata.Enums;

namespace MatchKata.Services
{
    public class MatchEvent
    {
        public int Id { get; set; }
        public EnumMatchEvent EnumMatchEvent { get; set; }
    }

    public interface IMatchService
    {
        void AddEvent(MatchEvent matchEvent);
    }
}
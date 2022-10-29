using MatchKata.Enums;
using MatchKata.Models;
using MatchKata.Repositories;
using MatchKata.Services;
using NSubstitute;
using NUnit.Framework;

namespace MatchKataTests
{
    public class MatchServiceTests
    {
        private MatchService _matchService;
        private IMatchRepository _matchRepository;

        [SetUp]
        public void SetUp()
        {
            _matchRepository = Substitute.For<IMatchRepository>();
            _matchService = new MatchService(_matchRepository);
        }

        [Test]
        public void first_home_goal_in_the_first_half()
        {
            _matchService.AddEvent(new MatchEvent
            {
                Id = 1,
                EnumMatchEvent = EnumMatchEvent.HomeGoal
            });
            GoalRecordShouldBe("H");
        }

        private void GoalRecordShouldBe(string goalRecord)
        {
            _matchRepository.Received().UpdateMatch(Arg.Is<Match>(match => match.GoalRecord == goalRecord));
        }
    }
}
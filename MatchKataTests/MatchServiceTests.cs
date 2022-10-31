using System;
using MatchKata.Enums;
using MatchKata.Models;
using MatchKata.Repositories;
using MatchKata.Services;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;

namespace MatchKataTests
{
    public class MatchServiceTests
    {
        private readonly int _matchId = 1;
        private IMatchRepository _matchRepository;
        private MatchService _matchService;

        [SetUp]
        public void SetUp()
        {
            _matchRepository = Substitute.For<IMatchRepository>();
            _matchService = new MatchService(_matchRepository);
        }

        [Test]
        public void first_home_goal_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = ""
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.HomeGoal
            });

            GoalRecordShouldBe("H");
        }

        [Test]
        public void home_goal_second_times_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = "H"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.HomeGoal
            });

            GoalRecordShouldBe("HH");
        }
        
        [Test]
        public void home_away_goal_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = "H"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.AwayGoal
            });

            GoalRecordShouldBe("HA");
        }

        [Test]
        public void away_goal_in_the_second_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 2,
                GoalRecord = "H"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.AwayGoal
            });

            GoalRecordShouldBe("H;A"); 
        }
        
        [Test]
        public void away_goal_second_times_in_the_second_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 2,
                GoalRecord = "H;A"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.AwayGoal
            });

            GoalRecordShouldBe("H;AA"); 
        }

        [Test]
        public void cancel_home_goal_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = "H"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.CancelHomeGoal
            });

            GoalRecordShouldBe("");
        }
        
        [Test]
        public void cancel_away_goal_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = "HA"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.CancelAwayGoal
            });

            GoalRecordShouldBe("H");
        }

        [Test]
        public void cancel_away_goal_in_the_second_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 2,
                GoalRecord = "HA"
            });

            _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.CancelAwayGoal
            });

            GoalRecordShouldBe("H;");
        }

        [Test]
        public void invalid_cancel_goal_in_the_first_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 1,
                GoalRecord = "HH"
            });

            Assert.Throws<Exception>(() => _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.CancelAwayGoal
            }));
        }
        
        [Test]
        public void invalid_cancel_goal_in_the_second_half()
        {
            GivenMatch(new Match
            {
                LivePeriod = 2,
                GoalRecord = "HH;"
            });

            Assert.Throws<Exception>(() => _matchService.AddEvent(new MatchEvent
            {
                Id = _matchId,
                EnumMatchEvent = EnumMatchEvent.CancelAwayGoal
            }));
        }

        private ConfiguredCall GivenMatch(Match match)
        {
            return _matchRepository.GetMatch(_matchId).Returns(match);
        }

        private void GoalRecordShouldBe(string goalRecord)
        {
            _matchRepository.Received().UpdateGoalRecord(Arg.Is<GoalRecord>(r => r.Record == goalRecord));
        }
    }
}
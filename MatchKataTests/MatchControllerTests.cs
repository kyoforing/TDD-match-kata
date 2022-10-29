using MatchKata.Controllers;
using MatchKata.Services;
using NSubstitute;
using NUnit.Framework;

namespace MatchKataTests
{
    public class MatchControllerTests
    {
        private MatchController _matchController;
        private IMatchService _matchService;

        [SetUp]
        public void SetUp()
        {
            _matchService = Substitute.For<IMatchService>();
            _matchController = new MatchController(_matchService);
        }

        [Test]
        public void call_update_method_when_receiving_api_request()
        {
            _matchController.UpdateMatchRecord();
            _matchService.Received().AddEvent();
        }
    }
}
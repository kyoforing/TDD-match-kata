using MatchKata.Controllers;
using NUnit.Framework;

namespace MatchKataTests
{
    public class MatchControllerTests
    {
        private MatchController _matchController;

        [SetUp]
        public void SetUp()
        {
            _matchController = new MatchController();
        }

        [Test]
        public void call_update_method_when_receiving_api_request()
        {
            _matchController.UpdateMatch();
        }
    }
}
using MatchKata.Enums;
using MatchKata.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatchKata.Controllers
{
    [ApiController]
    public class MatchController: ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPut("/match/{id}/event-record")]
        public void UpdateMatchRecord(int id, EnumMatchEvent enumMatchEvent)
        {
           _matchService.AddEvent(new MatchEvent
           {
               Id = id,
               EnumMatchEvent = enumMatchEvent
           }); 
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace MatchKata.Controllers
{
    [ApiController]
    public class MatchController: ControllerBase
    {
        [HttpPut("/match/{id}/event-record")]
        public void UpdateMatch()
        {
            
        }
    }
}
using DevCodingTest_StoriesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevCodingTest_StoriesAPI.Controllers
{
    [Route("api/stories")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public StoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStories([FromQuery] int best)
        {
            var _best = best > 0 ? best : 10;
            var stories = await _service.StoryService.GetStories(_best);

            return Ok(stories);
        }
    }
}

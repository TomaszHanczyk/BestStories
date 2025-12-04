using BestStories.Bal.Interfaces;
using BestStories.Common;
using Microsoft.AspNetCore.Mvc;

namespace BestStories.Controllers
{
    [ApiController]
    public class BestStoriesController(IBestStoriesService bestStoriesService) : ControllerBase
    {
        [HttpGet]
		[Route("api/[controller]/getbeststories")]
		public async Task<IActionResult> GetBestStoriesAsync(int bestStoriesCount)
		{
			List<BestStory> bestStories = await bestStoriesService.GetBestStoriesAsync(bestStoriesCount);

			if (bestStories.Count == 0)
			{
				return NotFound("Best stories not found.");
			}

			return Ok(bestStories);
		}
	}
}

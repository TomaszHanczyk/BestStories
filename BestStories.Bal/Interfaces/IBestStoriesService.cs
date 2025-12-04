using BestStories.Common;

namespace BestStories.Bal.Interfaces
{
	public interface IBestStoriesService
	{
		Task<List<BestStory>> GetBestStoriesAsync(int bestStoriesCount);
	}
}

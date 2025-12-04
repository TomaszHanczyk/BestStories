using BestStories.Bal.Interfaces;
using BestStories.Common;
using BestStories.Common.ApiModels;
using HackerNews.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BestStories.Bal
{
	public class BestStoriesService : IBestStoriesService
	{
		public async Task<List<BestStory>> GetBestStoriesAsync(int bestStoriesCount)
		{
			var bestStoriesIds = await GetBestStoriesIdsAsync();

			if (bestStoriesIds == null || bestStoriesIds.Count == 0)
				return [];

			// I'm assuming that BestStories api returns ids list in appropriate score value order.
			var topItems = bestStoriesIds.Take(bestStoriesCount);

			var bestStories = new List<BestStory>();

			foreach (var item in topItems)
			{
				var bestStory = await GetBestStoryAsync(item);

				if(bestStory == null)
					continue;

				bestStories.Add(bestStory);
			}

			return bestStories;
		}

		private static async Task<BestStory?> GetBestStoryAsync(int itemId)
		{
			Item? item = await GetItemAsync(itemId);

			if (item == null)
				return null;

			return new BestStory
			{
				Uri = item.Url,
				Title = item.Title,
				CommentCount = item.Descendants,
				PostedBy = item.By,
				Score = item.Score,
				Time = GetTime(item.Time),
			};
		}

		private static string GetTime(int unixTime)
		{
			var dateTime = TryToConvertToISO8601Format(unixTime);

			return string.IsNullOrWhiteSpace(dateTime) ? unixTime.ToString() : dateTime;
		}

		private static string TryToConvertToISO8601Format(int unixTime)
		{
			try
			{
				DateTimeOffset dateTimeOffSet = DateTimeOffset.FromUnixTimeSeconds(unixTime);

				// this is safier but returns decimal fraction of a second
				//return dateTimeOffSet.ToString("O");
				
				return dateTimeOffSet.ToString("yyyy-MM-ddTHH:mm:ssK");
			}
			catch (ArgumentOutOfRangeException ex)
			{
				return string.Empty;
			}
		}

		private static async Task<List<int>?> GetBestStoriesIdsAsync()
		{
			var bestStories = await ApiCalls.GetBestStoriesIdsAsync();
			return JsonConvert.DeserializeObject<List<int>>(bestStories);
		}

		private static async Task<Item?> GetItemAsync(int itemId)
		{
			var item = await ApiCalls.GetItemAsync(itemId);

			// this is by default anyway
			var serSettings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
			return JsonConvert.DeserializeObject<Item>(item, serSettings);
		}
	}
}

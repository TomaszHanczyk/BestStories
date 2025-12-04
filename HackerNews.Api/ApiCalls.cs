namespace HackerNews.Api
{
	public static class ApiCalls
	{
		private static readonly HttpClient Client = new();
		private const string Uri = "https://hacker-news.firebaseio.com/v0/";

		public static async Task<string> GetBestStoriesIdsAsync()
		{
			const string requestUri = $"{Uri}beststories.json";

			var response = await Client.GetAsync(requestUri);
			return await response.Content.ReadAsStringAsync();
		}

		public static async Task<string> GetItemAsync(int itemId)
		{
			string requestUri = $"{Uri}item/{itemId}.json";

			var response = await Client.GetAsync(requestUri);
			return await response.Content.ReadAsStringAsync();
		}
	}
}

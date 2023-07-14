using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class HackerNewsService : IHackerNewsService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;

    public HackerNewsService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
    {
        _httpClient = httpClientFactory.CreateClient("HackerNews");
        _cache = cache;
    }

    public async Task<IEnumerable<int>> GetBestStoryIds()
    {
        if (_cache.TryGetValue<IEnumerable<int>>("BestStoryIds", out var cachedStoryIds))
        {
            return cachedStoryIds;
        }

        var response = await _httpClient.GetAsync("beststories.json");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var storyIds = JsonConvert.DeserializeObject<IEnumerable<int>>(json);
        _cache.Set("BestStoryIds", storyIds, TimeSpan.FromMinutes(5)); // Cache the story IDs for 5 minutes
        return storyIds;
    }

    public async Task<Story> GetStoryDetails(int storyId)
    {
        if (_cache.TryGetValue<Story>($"Story:{storyId}", out var cachedStory))
        {
            return cachedStory;
        }

        var response = await _httpClient.GetAsync($"item/{storyId}.json");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var story = JsonConvert.DeserializeObject<Story>(json);
        _cache.Set($"Story:{storyId}", story, TimeSpan.FromMinutes(5)); // Cache the story details for 5 minutes
        return story;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;


[ApiController]
[Route("api/[controller]")]
public class StoriesController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;
    private readonly IMemoryCache _cache;

    public StoriesController(IHackerNewsService hackerNewsService, IMemoryCache cache)
    {
        _hackerNewsService = hackerNewsService;
        _cache = cache;
    }

    [HttpGet("{n}")]
    [ResponseCache(Duration = 60, VaryByQueryKeys = new[] { "n" })]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
    {
        if (_cache.TryGetValue<IEnumerable<Story>>($"BestStories:{n}", out var cachedStories))
        {
            return Ok(cachedStories);
        }

        var bestStoryIds = await _hackerNewsService.GetBestStoryIds();
        var storyTasks = bestStoryIds.Take(n).Select(id => _hackerNewsService.GetStoryDetails(id));
        var stories = await Task.WhenAll(storyTasks);
        var sortedStories = stories.OrderByDescending(s => s.Score).ToList();

        _cache.Set($"BestStories:{n}", sortedStories, TimeSpan.FromMinutes(5)); // Cache the stories for 5 minutes

        return Ok(sortedStories);
    }
}

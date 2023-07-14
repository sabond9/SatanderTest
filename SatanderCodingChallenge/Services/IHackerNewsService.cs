using System.Collections.Generic;
using System.Threading.Tasks;

public interface IHackerNewsService
{
    Task<IEnumerable<int>> GetBestStoryIds();
    Task<Story> GetStoryDetails(int storyId);
}

using DevCodingTest_StoriesAPI.Models.DataTransferObjects;

namespace DevCodingTest_StoriesAPI.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<StoryDto>> GetStories(int best);
    }
}

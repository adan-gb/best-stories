using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace DevCodingTest_StoriesAPI.Services.Implementations
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IStoryService> _storyService;

        public ServiceManager(IMemoryCache cache, IMapper mapper)
        {
            _storyService = new Lazy<IStoryService>(() => new StoryService(cache, mapper));
        }

        public IStoryService StoryService => _storyService.Value;
    }
}

using AutoMapper;
using DevCodingTest_StoriesAPI.Models;
using DevCodingTest_StoriesAPI.Models.DataTransferObjects;
using DevCodingTest_StoriesAPI.Models.Exceptions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace DevCodingTest_StoriesAPI.Services.Implementations
{
    public class StoryService : IStoryService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memory;
        private readonly HttpClient _httpClient;
       

        public StoryService(IMemoryCache cache, IMapper mapper)
        {
            _mapper = mapper;
            _memory = cache;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Constants.Constants.BEST_STORIES_PATH);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(Constants.Constants.JSON_MEDIA_TYPE));
        }

        public async Task<IEnumerable<StoryDto>> GetStories(int best)
        {
            var cacheKey = Constants.Constants.STORIES_CACHE_KEY;
            var stories = new List<Story>();

            if (!_memory.TryGetValue(cacheKey, out stories))
            {
                var apiResponse = await GetStoriesFromClient();

                SemaphoreSlim semaphore = new SemaphoreSlim(201);
                var tasks = new List<Task>();
                stories = new List<Story>();

                foreach (var id in apiResponse)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            var story = await CallApiAsync(id);
                            stories.Add(story);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));

                    await Task.WhenAll(tasks);
                }

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(300))
                    .SetPriority(CacheItemPriority.Normal);
                _memory.Set(cacheKey, stories, cacheOptions);
            }

            var bestStories = stories
                .OrderByDescending(st => st.Score)
                .Take(best)
                .Select(c => { c.TimeFormat = (c.Time > 0 ? DateTimeOffset.FromUnixTimeSeconds(c.Time).LocalDateTime : null); return c; })
                .ToList();

            var bestStoriesDto = _mapper.Map<List<StoryDto>>(bestStories);

            return bestStoriesDto;
        }

        private async Task<List<String>> GetStoriesFromClient()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<List<string>>(content);

                return apiResponse;
            }
            catch (Exception)
            {
                throw new StoriesNotFoundException();
            }
            
        }

        private async Task<Story> CallApiAsync(string id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(Constants.Constants.BEST_STORIES_ITEM_PATH + id + Constants.Constants.JSON_EXTENSION);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<Story>(content);

            return apiResponse;
        }

        private void ConfigureHttpClient()
        {

        }
    }
}

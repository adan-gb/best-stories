using AutoMapper;
using DevCodingTest_StoriesAPI.Models;
using DevCodingTest_StoriesAPI.Models.DataTransferObjects;

namespace DevCodingTest_StoriesAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Story, StoryDto>()
                .ForMember(story => story.PostedBy, opt => opt.MapFrom(x => x.By))
                .ForMember(story => story.Uri, opt => opt.MapFrom(x => x.Url))
                .ForMember(story => story.Time, opt => opt.MapFrom(x => x.TimeFormat));
        }
    }
}

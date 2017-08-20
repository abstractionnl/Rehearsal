using AutoMapper;
using Rehearsal.Messages;

namespace Rehearsal.WebApi
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<QuestionList, QuestionListOverviewModel>()
                .ForMember(x => x.QuestionCount, x => x.MapFrom(l => l.Questions.Count));
            CreateMap<QuestionList, QuestionListModel>();
            CreateMap<QuestionList.ListItem, QuestionListModel.Item>();
        }
    }
}
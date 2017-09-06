using AutoMapper;
using Rehearsal.Messages;

namespace Rehearsal.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<QuestionListProperties, QuestionListModel>()
                .ForMember(x => x.Id, c => c.Ignore());

            CreateMap<QuestionListProperties.Item, QuestionListModel.Item>();
            
            CreateMap<QuestionListProperties, QuestionListOverviewModel>()
                .ForMember(x => x.Id, c => c.Ignore());
        }
    }
}
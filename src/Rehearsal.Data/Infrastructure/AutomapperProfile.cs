using AutoMapper;
using Rehearsal.Messages;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Messages.Rehearsal;

namespace Rehearsal.Data.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<QuestionListProperties, QuestionListModel>()
                .ForMember(x => x.Id, c => c.Ignore())
                .ForMember(x => x.Version, c => c.Ignore());
            
            CreateMap<QuestionListProperties, QuestionListOverviewModel>()
                .ForMember(x => x.Id, c => c.Ignore())
                .ForMember(x => x.IsDeleted, c => c.UseValue(false));

            CreateMap<RehearsalStartedEvent, RehearsalSessionModel>();
        }
    }
}
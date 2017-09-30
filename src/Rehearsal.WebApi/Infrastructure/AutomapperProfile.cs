﻿using AutoMapper;
using Rehearsal.Messages;

namespace Rehearsal.WebApi.Infrastructure
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<QuestionList, QuestionListOverviewModel>()
                .ForMember(x => x.QuestionsCount, x => x.MapFrom(l => l.Questions.Count));
            CreateMap<QuestionList, QuestionListModel>();
            CreateMap<QuestionList.ListItem, QuestionListModel.Item>();
        }
    }
}
﻿using AutoMapper.Configuration;
using CoE.Ideas.Core.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoE.Ideas.Core
{
    internal class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile() : base()
        {
            CreateMap<Idea, IdeaInternal>();
            CreateMap<IdeaInternal, Idea>();
            CreateMap<Stakeholder, StakeholderInternal>();
            CreateMap<StakeholderInternal, Stakeholder>();
            CreateMap<StakeholderType, StakeholderTypeInternal>();
            CreateMap<StakeholderTypeInternal, StakeholderType>();
            CreateMap<Tag, TagInternal>();
            CreateMap<TagInternal, Tag>();
        }
    }
}
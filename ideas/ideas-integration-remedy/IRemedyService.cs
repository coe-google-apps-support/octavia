﻿using CoE.Ideas.Core;
using CoE.Ideas.Core.WordPress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoE.Ideas.Remedy
{
    public interface IRemedyService
    {
        Task PostNewIdeaAsync(Idea idea, string user3and3);
    }
}

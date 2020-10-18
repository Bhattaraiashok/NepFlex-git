﻿using NepFlex.Core.Entities.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Interfaces.Services
{
    public interface ISearchService
    {
        List<SearchResponse> GetSearchResponseList(string[] searchText);
    }
}

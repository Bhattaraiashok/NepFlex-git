﻿using NepFlex.Core.Entities.OnlinePasal;
using NepFlex.Core.Entities.ResourceModels;
using System.Collections.Generic;

namespace Core.Interfaces.Services
{
    public interface IMenuTopService
    {
       List<MenuTopContainer> GetMenuTopItems();
       List<MenuTopNav> GetMenuNav();
    }
}

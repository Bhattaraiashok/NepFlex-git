﻿using System.Collections.Generic;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Entities.OnlinePasal;

namespace Core.Interfaces.Repositories
{
   // public interface IMenuTopRepository:IRepository<MenuPopId, int>
    public interface IMenuTopRepository:IRepository<MenuTopContainer, int>
    {
        List<ClothingBrands> GetClothingBrands();
        List<MenuTopContainer> GetMenuTopItems();
        List<MenuTopNav> GetMenuNav();
        

    }
}

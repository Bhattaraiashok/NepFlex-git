using Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using NepFlex.DataAccess.Context;
using NepFlex.Core.Entities.ResourceModels;

namespace DataAccess.Repositories
{
    public class MenuTopRepository : Repository<MenuTopContainer, int>, IMenuTopRepository
    {
        private readonly IOnlinePasalContext _context;
        public MenuTopRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }
        //public List<MenuTopContainer> GetMenuTopItems()
        //{
        //    var results = _context.SpMenuContainer();
        //    return results.Select(a => new MenuTopContainer()
        //    {
        //        MenuID = a.MenuID,
        //        MenuPopID = a.MenuPopID,
        //        MenuName = a.MenuName,
        //        MenuUrl = a.MenuUrl,
        //        MenuPopContainer = a.MenuPopContainer,
        //        MenuPopUrl = a.MenuPopUrl,
        //        MenuStillActive = a.MenuStillActive,
        //        MenuContainerValidity = a.MenuContainerValidity
        //    }).ToList();
        //}

        public List<MenuTopNav> GetMenuNav()
        {
            var resultNav = new List<MenuTopNav>();
            //resultNav=_context.MenuNavigations;
            //return resultNav.Select(x => new MenuTopNav()
            //{
            //    MenuId = x.MenuId,
            //    MenuName = x.MenuName,
            //    MenuUrl = x.MenuUrl,
            //    Active = x.Active
            //}).ToList();
            return resultNav;
        }
        //public List<ClothingBrands> GetClothingBrands()
        //{
        //    var resultClothingBrand = _context.ClothingBrands;
        //    return resultClothingBrand.Select(x => new ClothingBrands()
        //    {
        //        BrandId = x.BrandId,
        //        MenuId = x.MenuId,
        //        BrandName = x.BrandName,
        //        BrandUrl = x.BrandUrl,
        //        Active = x.Active,
        //        DateInserted = x.DateInserted
        //    }).ToList();
        //}
        //public List<MenuClearence> GetMenuClearence()
        //{
        //    var resultClearence = _context.Clearences;
        //    return resultClearence.Select(y => new MenuClearence()
        //    {
        //        ClearenceId = y.ClearenceId,
        //        ClearenceName = y.ClearenceName,
        //        ClearenceUrl = y.ClearenceUrl,
        //        ClearencePercent = y.ClearencePercent,
        //        MenuId = y.MenuId,
        //        DateInserted = y.DateInserted,
        //        Active = y.Active,
        //        DateUpdate = y.DateUpdate,
        //        DateValidity = y.DateValidity,
        //        Remarks = y.Remarks
        //    }).ToList();
        //}
        //public List<NepFlex.Core.Entities.OnlinePasal.Fashion> GetFashion()
        //{
        //    var resultClearence = _context.Fashions;
        //    return resultClearence.Select(f => new NepFlex.Core.Entities.OnlinePasal.Fashion()
        //    {
        //        FashionId = f.FashionId,
        //        FashionName = f.FashionName,
        //        FashionUrl = f.FashionUrl,
        //        Active = f.Active,
        //        DateInserted = f.DateInserted,
        //        MenuId = f.MenuId,
        //        Remarks = f.Remarks,
        //        DateValidity = f.DateValidity,
        //        DateUpdate = f.DateUpdate
        //    }).ToList();
        //}
        //public List<NepFlex.Core.Entities.OnlinePasal.NewArrival> GetNewArrival()
        //{
        //    var resultClearence = _context.NewArrivals;
        //    return resultClearence.Select(n => new NepFlex.Core.Entities.OnlinePasal.NewArrival()
        //    {
        //        NewArrivalId=n.NewArrivalId,
        //        NewArivalBrand=n.NewArivalBrand,
        //        MenuId=n.MenuId,
        //        Active=n.Active,
        //        DateInserted=n.DateInserted,
        //        Url=n.Url,
        //        DateUpdated=n.DateUpdated,
        //        Remarks=n.Remarks
        //    }).ToList();
        //}
    }
}


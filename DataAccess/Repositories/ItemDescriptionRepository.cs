using DataAccess.Repositories;
using a=NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.DataAccess.Repositories
{
    public class ItemDescriptionRepository : Repository<a.ItemDescription, int>, IItemDescriptionRepository
    {
        private readonly IOnlinePasalContext _context;
        public ItemDescriptionRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }

        public List<a.ItemDescription> GetItemDescriptionList()
        {
            var results = _context.ItemDescriptions.Select(a => new a.ItemDescription()
            {
                //AutosBrand=a.AutosBrand,
                //Category=a.Category,
                //ExtraWarranty=a.ExtraWarranty,
                //Condition=a.Condition,
                //Warranty=a.Warranty,
                //Model=a.Model,
                //PhoneBrand=a.PhoneBrand,
                //MileKmph=a.MileKmph
                
            }).ToList();
            return results;
        }
    }
}

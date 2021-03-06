﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class DetailResponse
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Detail { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Other { get; set; }
        public string TopCategory { get; set; }
        public string SubCategory { get; set; }
        public decimal? Price { get; set; }
        public string Condition { get; set; }
        public string Brand { get; set; }
        public string Modal { get; set; }
        public string Mile_KMPH { get; set; }
        public string Warranty { get; set; }
        public string Extra_Warranty { get; set; }
        public DateTime? DateAdded { get; set; }
        public string ShowPhoneNumber { get; set; }
        public string ShowEmailID { get; set; }
        public string ProfilePicture { get; set; }
    }
}

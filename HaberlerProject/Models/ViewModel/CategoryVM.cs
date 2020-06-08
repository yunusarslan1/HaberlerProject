using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PageUrl { get; set; }
        public int MainId { get; set; }
        public string MainName { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesc { get; set; }
        public string SeoKeyword { get; set; }
        public bool IsActive { get; set; }
        public int RowNumber { get; set; }
        public bool IsmenuActive { get; set; }
        public string SiteUrl { get; set; }
        public string FirmaAd { get; set; }
        public string FirmaLogo { get; set; }
        public List<ProductVM> ProdList { get; set; }


    }
}
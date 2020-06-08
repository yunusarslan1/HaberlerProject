using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.ViewModel
{
    public class ProductVM
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public int CatMainId { get; set; }
        public string CatName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ChangeDate { get; set; }
        public string PageUrl { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDesc { get; set; }
        public String SeoKeyword { get; set; }
        public bool IsActive { get; set; }
        public int RowNumber { get; set; }
        public string[] SelectedTags { get; set; }
        public string ListImageUrl { get; set; }
        public bool IsSubMenuActive { get; set; }
        public List<GaleriVM> Galeries { get; set; }
        public List<TagVM> TagListUI { get; set; }
        public string SiteUrl { get; set; }
        public string FirmaAd { get; set; }
        public string FirmaLogo { get; set; }
    }
}
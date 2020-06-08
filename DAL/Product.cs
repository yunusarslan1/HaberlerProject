

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contents { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> GaleriId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }
        public string PageUrl { get; set; }
        public string SeoDesc { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> RowNumber { get; set; }
        public string ListImageUrl { get; set; }
        public Nullable<bool> IsMenuActive { get; set; }
    }
}

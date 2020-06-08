using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberlerProject.Content
{
    public class ProductTagVM
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public int ArticleId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Selected { get; set; }
    }
}
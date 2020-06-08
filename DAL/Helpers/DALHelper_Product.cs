using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static partial class DALHelper
    {

        public static List<Product> GetProductList()
        {
            using (var db = GetDB)
            {
                var prodList = db.Product.OrderByDescending(x => x.ChangeDate).ToList();
                return prodList;
            }
        }
        public static List<Product> GetProductListForActive()
        {
            using (var db = GetDB)
            {
                var prodList = db.Product.Where(x => x.IsActive.Value).ToList();
                return prodList;
            }
        }
        public static Product AddProduct(Product model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.Product.Add(model);
                    db.SaveChanges();
                    result = "";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return model;
            }

        }

        public static bool IsProductForUrl(string url)
        {
            using (var db = GetDB)
            {
                var IsCategory = db.Category.Any(x => x.PageUrl == url);
                if (IsCategory)
                {
                    return true;
                }
                return false;
            }

        }
        public static Product GetProductForUrl(string url)
        {
            using (var db = GetDB)
            {
                var prod = db.Product.FirstOrDefault(x => x.PageUrl == url);
                if (prod == null)
                {
                    return new Product();
                }
                return prod;
            }

        }
        public static Product GetProductForId(int prodId)
        {
            using (var db = GetDB)
            {
                var prod = db.Product.FirstOrDefault(x => x.Id == prodId);
                if (prod == null)
                {
                    return new Product();
                }
                return prod;
            }

        }

        public static Product GetProductActiveForUrl(string url)
        {
            using (var db = GetDB)
            {
                var prod = db.Product.FirstOrDefault(x => x.PageUrl == url);

                return prod;
            }

        }

        public static string EditProduct(Product model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
                return result;
            }

        }

        public static string DeleteProduct(Product model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.Entry(model).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }

        }


        public static List<Product> GetProductListForCatId(int catId)
        {
            using (var db = GetDB)
            {
                var prodList = db.Product.Where(x => x.CategoryId == catId).ToList();
                return prodList;
            }
        }

        //public static List<Product> DetaySayfasi(int? id)
        //{
        //    using (var db = GetDB)
        //    {
        //        var list = db.Product.Where(d => d.Id == id.Value).ToList();
        //        return list;
        //    }
        //}




    }
}

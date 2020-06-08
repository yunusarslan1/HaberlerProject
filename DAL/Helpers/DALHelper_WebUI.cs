using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static partial class DALHelper
    {
        public static List<Category> GetMainMenuForActive(int mainId)
        {
            using (var db = GetDB)
            {

                var list = db.Category.Where(x => x.MainId == mainId && x.IsActive.Value && x.IsMenuActive.Value).OrderByDescending(x => x.RowNumber).ToList();
                return list;
            }
        }
        public static List<Category> GetSubMenuActiveList(int catId)
        {
            using (var db = GetDB)
            {

                var list = db.Category.Where(x => x.MainId == catId && x.IsActive.Value && x.IsMenuActive.Value).OrderByDescending(x => x.RowNumber).ToList();
                return list;
            }
        }


        public static  List<Product> KarisikHaberListesi()
        {
            using (var db = GetDB)
            {

                var list = db.Product.Where(x=>x.IsActive.Value && x.CategoryId==2).OrderByDescending(x => x.CreateDate.Value).OrderBy(x=> Guid.NewGuid()).Take(10).ToList();
                return list;
            }
        }

        public static List<Product> GundemHaberListesi()
        {
            using (var db = GetDB)
            {
                var list = db.Product.Where(x => x.IsActive.Value && x.CategoryId == 3).OrderByDescending(x => x.CreateDate.Value).Take(4).ToList();
                return list;
            }
        }

        public static List<Product> EkonomiHaberListesi()
        {
            using (var db = GetDB)
            {
                var list = db.Product.Where(x => x.IsActive.Value && x.CategoryId == 4).OrderByDescending(x => x.CreateDate.Value).Take(4).ToList();
                return list;
            }
        }

        public static List<Product> SaglikHaberListesi()
        {
            using (var db = GetDB)
            {
                var list = db.Product.Where(x => x.IsActive.Value && x.CategoryId == 5).OrderByDescending(x => x.CreateDate.Value).Take(6).ToList();
                return list;
            }
        }

        public static List<Product> SporHaberListesi()
        {
            using (var db = GetDB)
            {
                var list = db.Product.Where(x => x.IsActive.Value && x.CategoryId == 6).Take(6).ToList();
                return list;
            }
        }

        public static List<Product> EnSonEklenenHaber()
        {
            using (var db = GetDB)
            {
                var list = db.Product.Where(x => x.IsActive.Value).OrderByDescending(x=>x.CreateDate.Value).Take(5).ToList();
                return list;
            }
        }

        public static List<Product> DetaySayfasi()
        {
       
            using (var db = GetDB)
            {
                
                var list = db.Product.Where(x => x.Id !=null).OrderByDescending(x => x.CreateDate.Value).Take(25).ToList();
                return list;
            }
        }



    }
}

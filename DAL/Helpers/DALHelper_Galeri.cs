using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static partial class DALHelper
    {
        public static string AddGaleri(Galeri model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.Galeri.Add(model);
                    db.SaveChanges();
                    result = "";
                }
                catch (Exception ex)
                {
                    result = "eklenemedi, ";
                }

                return result;
            }

        }

        public static List<Galeri> GetGaleriList(int id)
        {
            using (var db = GetDB)
            {
                var galeriList = db.Galeri.Where(x => x.ProductId == id).OrderByDescending(x => x.CreateDate).ToList();
                return galeriList;
            }
        }
        public static List<Galeri> GetGaleriListForActive(int id)
        {
            using (var db = GetDB)
            {
                var galeriList = db.Galeri.Where(x => x.ProductId == id&&x.IsActive.Value).OrderByDescending(x => x.CreateDate).ToList();
                return galeriList;
            }
        }
    }
}

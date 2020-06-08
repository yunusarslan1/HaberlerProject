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
        private static dbhaberlerEntities GetDB
        {
            get
            {
                var db = new dbhaberlerEntities();
              
                return db;
            }
        }

        public static List<Category> GetCategoryList()
        {
            using (var db = GetDB)
            {
                var catList = db.Category.OrderByDescending(x => x.CreateDate).ToList();
                return catList;
            }
        }
        public static List<Category> GetCategoryListForCatId(int catId)
        {
            using (var db = GetDB)
            {
                var catList = db.Category.Where(x=>x.MainId== catId).ToList();
                return catList;
            }
        }

        public static string AddCategory(Category model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.Category.Add(model);
                    db.SaveChanges();
                    result = "";
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }

        }
        public static List<Category> GetCategoryListForActive()
        {
            using (var db = GetDB)
            {
                var List = db.Category.OrderByDescending(x => x.CreateDate).Where(x => x.IsActive.Value).ToList();
                return List;
            }
        }

        public static List<Category> GetCategoryListActiveForId(int id)
        {
            using (var db = GetDB)
            {
                var List = db.Category.OrderByDescending(x => x.CreateDate).Where(x =>x.MainId==id&& x.IsActive.Value).ToList();
                return List;
            }
        }
        public static Category GetCategoryModelForId(int id)
        {
            using (var db = GetDB)
            {
                var model = db.Category.Find(id);

                return model;
            }

        }
        public static List<Category> GetMainCategoryModelForId(int id)
        {
            using (var db = GetDB)
            {
                var model = db.Category.Where(x=>x.MainId==id).ToList();

                return model;
            }

        }

        public static Category GetCategoryModelActiveForUrl(string id)
        {
            using (var db = GetDB)
            {
                var model = db.Category.FirstOrDefault(x=>x.PageUrl==id);

                return model;
            }

        }

        public static string EditCategory(Category model)
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

        public static string DeleteCategory(Category model)
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

        public static bool IsCategoryForUrl(string url)
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
        public static Category GetMainCategory()
        {
            using (var db = GetDB)
            {
                var categoryModel = db.Category.Where(x => x.MainId == x.Id).FirstOrDefault();

                return categoryModel;
            }

        }
    }
    class DALHelper_Category
    {
    }
}

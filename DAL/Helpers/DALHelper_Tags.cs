using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public static partial class DALHelper
    {
        public static int AddTagGetId(Tags model)
        {
            int result = 0;
            using (var db = GetDB)
            {
                try
                {
                    db.Tags.Add(model);
                    db.SaveChanges();
                    result = model.Id;
                }
                catch (Exception ex)
                {
                    result = 0;
                }

                return result;
            }

        }
        public static int AddTagIsTagControl(Tags model)
        {
            int result = 0;
            using (var db = GetDB)
            {
                try
                {
                    int tagId = GetTagId(model.Url);
                    if (tagId == 0)
                    {
                        db.Tags.Add(model);
                        int rss = db.SaveChanges();
                        if (rss > 0)
                        {
                            result = model.Id;
                        }
                    }
                    else
                    {
                        result = tagId;
                    }

                }
                catch (Exception ex)
                {
                    result = 0;
                }

                return model.Id;
            }

        }
        public static Tags GetTag(int id)
        {
            using (var db = GetDB)
            {
                var model = db.Tags.FirstOrDefault(x => x.Id == id);

                return model;
            }

        }

        public static int GetTagId(string tagUrl)
        {
            using (var db = GetDB)
            {
                int result = 0;
                var model = db.Tags.Where(x => x.Url == tagUrl).FirstOrDefault();
                if (model != null)
                {
                    result = model.Id;
                }
                return result;
            }

        }


        public static List<Tags> GetTagList()
        {
            using (var db = GetDB)
            {
                var model = db.Tags.ToList();
                return model;
            }
        }

        public static string AddProductTag(ProductTag model)
        {
            string result = "";
            using (var db = GetDB)
            {
                try
                {
                    db.ProductTag.Add(model);
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

        public static List<ProductTag> GetAllProductTag(int productId)
        {
            using (var db = GetDB)
            {
                var model = db.ProductTag.Where(x => x.ProductId == productId).ToList();
                return model;
            }
        }
        public static void DeleteProductTags(List<int> articleTagIds)
        {
            using (var db = GetDB)
            {
                var tags = db.ProductTag.Where(x => articleTagIds.Any(y => y == x.Id)).ToList();
                db.ProductTag.RemoveRange(tags);
                db.SaveChanges();
            }
        }
        public static int GetProductTagId(int productId, int tagId)
        {
            using (var db = GetDB)
            {
                int result = 0;
                var model = db.ProductTag.Where(x => x.ProductId == productId && x.TagId == tagId).FirstOrDefault();
                if (model != null)
                {
                    result = model.Id;
                }
                return result;
            }

        }
        public static List<ProductTag> GetProductTagListByProductId(int id)
        {
            using (var db = GetDB)
            {
                var model = (from t in db.Tags
                             join at in db.ProductTag on t.Id equals at.TagId
                             where at.ProductId == id
                             select at).ToList();
                return model;
            }
        }
        public static List<ProductTag> GetProductTagForId(int id)
        {
            var db = GetDB;
            return db.ProductTag.Where(x => x.ProductId == id).ToList();
        }




    }
}

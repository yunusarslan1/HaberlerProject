using DAL;
using DAL.Helpers;
using HaberlerProject.Models.Tool;
using HaberlerProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin»FullYonetim")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            var dbCategoryList = DALHelper.GetCategoryList();
            return View(dbCategoryList);
        }
        public ActionResult Create()
        {

            var model = new CategoryVM();
            var mainCatg = DALHelper.GetMainCategory();
            if (mainCatg != null)
            {
                model.MainId = mainCatg.MainId.Value;
                model.MainName = mainCatg.Name;
                model.IsActive = true;
                model.IsmenuActive = false;
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CategoryVM model)
        {

            if (ModelState.IsValid)
            {
                string pageUrl = Pages.SetUrl(model.Name);
                var isCatg = DALHelper.IsCategoryForUrl(pageUrl);
                if (isCatg == true)
                {
                    TempData["pnotify"] = "error,create," + model.Name + " adlı kategori sistemimizde kayıtlıdır.İlgili kayıt ";

                    return View(model);
                }

                //yeni kullanıcı bilgilerini async olarak ekler
                var dbModel = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    Contents = model.Content,
                    SeoDesc = model.SeoDesc,
                    SeoTitle = model.SeoTitle,
                    PageUrl = pageUrl,
                    ChangeDate = DateTime.Now,
                    CreateDate = DateTime.Now,
                    IsActive = model.IsActive,
                    MainId = model.MainId,
                    RowNumber = model.RowNumber,
                    SeoKeyword = model.SeoKeyword,
                    IsMenuActive = model.IsmenuActive
                };

                string result = DALHelper.AddCategory(dbModel);

                if (result == "")//başarılı bir şekilde eklediyse
                {
                    TempData["pnotify"] = "success,create," + model.Name;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pnotify"] = "error,create," + " İlgili kayıt (" + result + ") ";
                }

            }


            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            if (id != 0)
            {
                var catModel = DALHelper.GetCategoryModelForId(id);

                var model = new CategoryVM
                {
                    Id = catModel.Id,
                    IsActive = catModel.IsActive.Value,
                    Content = catModel.Contents,
                    MainId = catModel.MainId.Value,
                    Description = catModel.Description,
                    Name = catModel.Name,
                    PageUrl = catModel.PageUrl,
                    RowNumber = catModel.RowNumber.Value,
                    SeoDesc = catModel.SeoDesc,
                    SeoTitle = catModel.SeoTitle,
                    SeoKeyword = catModel.SeoKeyword,
                    IsmenuActive = catModel.IsMenuActive.Value
                };

                var mainCat = DALHelper.GetCategoryModelForId(catModel.MainId.Value);
                if (mainCat != null)
                {
                    model.MainName = mainCat.Name;
                }
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryVM model)
        {

            string result = "";
            var catModel = DALHelper.GetCategoryModelForId(model.Id);

            catModel.Name = model.Name;
            catModel.PageUrl = Pages.SetUrl(model.Name);
            catModel.RowNumber = model.RowNumber;
            catModel.SeoDesc = model.SeoDesc;
            catModel.SeoKeyword = model.SeoKeyword;
            catModel.ChangeDate = DateTime.Now;
            catModel.Contents = model.Content;
            catModel.Description = model.Description;
            catModel.MainId = model.MainId;
            catModel.IsActive = model.IsActive;
            catModel.SeoTitle = model.SeoTitle;
            catModel.IsMenuActive = model.IsmenuActive;


            result = DALHelper.EditCategory(catModel);

            if (result == "")
            {
                TempData["pnotify"] = "success,edit," + model.Name;

                return RedirectToAction("Index");
            }

            TempData["pnotify"] = "error,edit," + $"İlgili kayıt ( {result} )";
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int deletedId = 0)
        {
            if (deletedId != 0)
            {
                var dbModel = DALHelper.GetCategoryModelForId(deletedId);
                string result = DALHelper.DeleteCategory(dbModel);
                if (result == "")
                {
                    TempData["pnotify"] = "success,delete," + dbModel.Name;

                }
                else
                {
                    TempData["pnotify"] = "error,delete," + $"İlgili kayıt ( {result} ) ";
                }
            }
            else
            {
                TempData["pnotify"] = "error,delete," + "Böyle bir kayıt bulunmadığından ";
            }
            return RedirectToAction("Index");
        }

        #region TreeJs-Ana-Alt Kategori getirir

        public ActionResult _AllCategories()
        {
            return PartialView();
        }
        public JsonResult GetAllCategoriesToJson()
        {
            var catList = DALHelper.GetCategoryListForActive();

            var model = new List<JsTreeItemVM>();
            var modelItem = new JsTreeItemVM();
            foreach (var catItem in catList.Where(x => x.MainId == 1))
            {
                modelItem = new JsTreeItemVM()
                {
                    id = catItem.Id.ToString(),
                    state = new JsTreeState() { opened = true },
                    text = catItem.Name,
                    children = GetCategoryChildren(catItem, catList)
                };

                model.Add(modelItem);
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        private List<JsTreeItemVM> GetCategoryChildren(Category catItem, List<Category> catList)
        {
            var jsTreeList = new List<JsTreeItemVM>();
            JsTreeItemVM modelItem;
            var subCategories = catList.Where(x => x.MainId == catItem.Id).ToList();
            if (subCategories != null)
            {
                foreach (var subCatItem in subCategories)
                {
                    modelItem = new JsTreeItemVM()
                    {
                        id = subCatItem.Id.ToString(),
                        state = new JsTreeState() { opened = true },
                        text = subCatItem.Name,
                        children = GetCategoryChildren(subCatItem, catList)
                    };

                    jsTreeList.Add(modelItem);
                }
                return jsTreeList;
            }

            return null;
        }

        #endregion
    }
}
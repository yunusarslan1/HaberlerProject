using DAL;
using DAL.Helpers;
using HaberlerProject.Content;
using HaberlerProject.Models.Tool;
using HaberlerProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Areas.Admin.Controllers
{

    //[Authorize(Roles = "Admin»FullYonetim,Admin»AltYonetici")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()

        {
            var dbProdList = DALHelper.GetProductList();
            var prodListVM = new List<ProductVM>();
            var dbCatList = DALHelper.GetCategoryList();

            prodListVM.AddRange(dbProdList.Select(x => new ProductVM
            {

                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive.Value,
                RowNumber = x.RowNumber.Value,
                CatName = DALHelper.GetCategoryModelForId(x.CategoryId.Value).Name ?? "Yok"

            }));
            return View(prodListVM);
        }

        public ActionResult Create()
        {

            return View(new ProductVM { IsActive = true, RowNumber = 0, IsSubMenuActive = false });
        }


        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM model, HttpPostedFileBase ImageUrl, HttpPostedFileBase ListImageUrl, HttpPostedFileBase[] Galeri)
        {

            if (ModelState.IsValid)
            {
                string pageUrl = Pages.SetUrl(model.Name);
                var isProd = DALHelper.IsProductForUrl(pageUrl);
                if (isProd == true)
                {
                    TempData["pnotify"] = "error,create," + model.Name + " adlı ürün sistemimizde kayıtlıdır.İlgili kayıt ";

                    return View(model);
                }

                if (ImageUrl != null)
                {
                    var path = Pages.GetKeyForWebConfig("UploadFilePath") + "product/detay/";

                    model.ImageUrl = Process.SaveImageName(ImageUrl, path, model.Name);
                }
                if (ListImageUrl != null)
                {
                    var pathListImage = Pages.GetKeyForWebConfig("UploadFilePath") + "product/liste/";

                    model.ListImageUrl = Process.SaveImageName(ListImageUrl, pathListImage, model.Name);
                }

                var dbProduct = new Product
                {
                    PageUrl = pageUrl,
                    ImageUrl = model.ImageUrl,
                    IsActive = model.IsActive,
                    CategoryId = model.CatId,
                    RowNumber = model.RowNumber,
                    Contents = model.Content,
                    Description = model.Description,
                    Name = model.Name,
                    CreateDate = DateTime.Now,
                    SeoDesc = model.SeoDesc,
                    SeoTitle = model.SeoTitle,
                    ChangeDate = DateTime.Now,
                    SeoKeyword = model.SeoKeyword,
                    ListImageUrl = model.ListImageUrl,
                    IsMenuActive = model.IsSubMenuActive,

                };

                var resultDb = DALHelper.AddProduct(dbProduct);
                var productsTags = DALHelper.GetProductTagListByProductId(resultDb.Id);


                Tags tagModel;
                ProductTag prodTag;
                int tagId = 0;
                var selectedTags = new List<int>();
                if (model.SelectedTags != null)
                {
                    for (int i = 0; i < model.SelectedTags.Length; i++)
                    {

                        var resultTagId = DALHelper.GetTagId(Pages.SetUrl(model.SelectedTags[i].ToString()));
                        if (resultTagId == 0)
                        {
                            tagModel = new Tags { Name = model.SelectedTags[i].ToString(), Url = Pages.SetUrl(model.SelectedTags[i].ToString()) };
                            tagId = DALHelper.AddTagGetId(tagModel);
                        }
                        else
                        {
                            tagId = resultTagId;
                        }
                        selectedTags.Add(tagId);

                        if (!productsTags.Any(x => x.TagId == tagId))
                        {
                            prodTag = new ProductTag { ProductId = resultDb.Id, TagId = tagId };
                            var resultArticleTag2 = DALHelper.AddProductTag(prodTag);
                        }


                    }
                    var deletedTags = productsTags.Where(x => !selectedTags.Any(y => y == x.TagId)).Select(x => x.Id).ToList();
                    if (deletedTags.Count() != 0)
                    {
                        DALHelper.DeleteProductTags(deletedTags);
                    }

                }

                if (resultDb.Id != 0)//başarılı bir şekilde eklediyse
                {

                    string status = "";
                    if (Galeri != null)
                    {
                        var path = Pages.GetKeyForWebConfig("UploadFilePath") + "product/detay/galeri/";
                        int i = 0;
                        var dbProdList = DALHelper.GetProductForUrl(pageUrl);

                        foreach (HttpPostedFileBase image in Galeri)
                        {
                            i++;
                            var dbGaleri = new Galeri();
                            if (image != null)
                            {
                                dbGaleri.ImageUrl = Process.SaveImageName(image, path, model.Name + "-" + DateTime.Now.ToString("dd-MM-yyyy-H:mm:ss "));
                                dbGaleri.CreateDate = DateTime.Now;
                                dbGaleri.IsActive = true;
                                dbGaleri.ProductId = dbProdList.Id;

                            }
                            status += DALHelper.AddGaleri(dbGaleri);
                        }

                    }
                    TempData["pnotify"] = "success,create," + status + model.Name;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pnotify"] = "error,create," + " İlgili kayıt eklendi.";
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var dbmodel = DALHelper.GetProductForId(id);
            var cat = DALHelper.GetCategoryModelForId(dbmodel.CategoryId.Value);
            var galeriList = DALHelper.GetGaleriList(dbmodel.Id);
            GaleriVM galeri;

            var model = new ProductVM
            {
                Id = dbmodel.Id,
                CatId = dbmodel.CategoryId.Value,
                CatName = cat.Name,
                Name = dbmodel.Name,
                ImageUrl = dbmodel.ImageUrl,
                ListImageUrl = dbmodel.ListImageUrl,
                IsActive = dbmodel.IsActive.Value,
                Content = dbmodel.Contents,
                Description = dbmodel.Description,
                SeoDesc = dbmodel.SeoDesc,
                SeoTitle = dbmodel.SeoTitle,
                SeoKeyword = dbmodel.SeoKeyword,
                IsSubMenuActive = dbmodel.IsMenuActive.Value,
            };
            model.Galeries = new List<GaleriVM>();
            galeriList.ForEach(x =>
            {
                galeri = new GaleriVM
                {
                    Url = x.ImageUrl,
                    Id = x.Id
                };
                model.Galeries.Add(galeri);
            });



            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        //[Authorize(Roles = "Admin»FullYonetim,Servis»SefUye")]
        public ActionResult Edit(ProductVM model, HttpPostedFileBase ImageUrl, HttpPostedFileBase ListImageUrl, HttpPostedFileBase[] Galeri)
        {

            if (ModelState.IsValid)
            {
                string pageUrl = Pages.SetUrl(model.Name);


                var dbProduct = DALHelper.GetProductForId(model.Id);
                if (dbProduct.PageUrl != pageUrl)
                {
                    var isProd = DALHelper.IsProductForUrl(pageUrl);
                    if (isProd == true)
                    {
                        TempData["pnotify"] = "error,edit," + model.Name + " başlıklı arıza bilgisi sistemimizde kayıtlıdır.İlgili kayıt ";

                        return View(model);
                    }
                }

                dbProduct.Name = model.Name;
                dbProduct.PageUrl = pageUrl;
                dbProduct.ImageUrl = model.ImageUrl;
                dbProduct.IsActive = model.IsActive;
                dbProduct.RowNumber = model.RowNumber;
                dbProduct.CategoryId = model.CatId;
                dbProduct.ChangeDate = DateTime.Now;
                dbProduct.Description = model.Description;
                dbProduct.SeoTitle = model.SeoTitle;
                dbProduct.SeoDesc = model.SeoDesc;
                dbProduct.SeoKeyword = model.SeoKeyword;
                dbProduct.IsMenuActive = model.IsSubMenuActive;
                if (ImageUrl != null)
                {
                    var path = Pages.GetKeyForWebConfig("UploadFilePath") + "product/detay";

                    dbProduct.ImageUrl = Process.SaveImageName(ImageUrl, path, model.Name);
                }
                if (ListImageUrl != null)
                {
                    var path = Pages.GetKeyForWebConfig("UploadFilePath") + "product/liste";

                    dbProduct.ListImageUrl = Process.SaveImageName(ImageUrl, path, model.Name);
                }
                if (Galeri != null)
                {
                    var pathListImage = Pages.GetKeyForWebConfig("UploadFilePath") + "product/detay/galeri/";

                    foreach (var file in Galeri)
                    {


                        if (file.ContentLength > 0)
                        {

                            Process.SaveImageName(file, pathListImage, model.Name + "-" + DateTime.Now.ToString("dd-MM-yyyy-H:mm:ss "));
                        }
                    }
                }

                //galeriImage alanları yapılmalı????

                string result = DALHelper.EditProduct(dbProduct);


                var prodTags = DALHelper.GetProductTagListByProductId(model.Id);

                Tags tagModel;
                ProductTag productTag;
                int tagId = 0;
                var selectedTags = new List<int>();
                if (model.SelectedTags != null)
                {
                    for (int i = 0; i < model.SelectedTags.Length; i++)
                    {
                        if (int.TryParse(model.SelectedTags[i], out tagId))
                        {
                            selectedTags.Add(tagId);
                            if (!prodTags.Any(x => x.TagId == tagId))
                            {
                                productTag = new ProductTag
                                {
                                    ProductId = model.Id == 0 ? dbProduct.Id
                                        : model.Id,
                                    TagId = tagId
                                };
                                var resultArticleTag1 = DALHelper.AddProductTag(productTag);
                            }
                        }
                        else
                        {
                            var resultTagId = DALHelper.GetTagId(Pages.SetUrl(model.SelectedTags[i].ToString()));
                            if (resultTagId == 0)
                            {
                                tagModel = new Tags { Name = model.SelectedTags[i].ToString(), Url = Pages.SetUrl(model.SelectedTags[i].ToString()) };
                                tagId = DALHelper.AddTagGetId(tagModel);
                            }
                            else
                            {
                                tagId = resultTagId;
                            }
                            selectedTags.Add(tagId);
                            if (!prodTags.Any(x => x.TagId == tagId))
                            {
                                productTag = new ProductTag { ProductId = model.Id, TagId = tagId };
                                var resultArticleTag2 = DALHelper.AddProductTag(productTag);
                            }
                        }

                    }

                    var deletedTags = prodTags.Where(x => !selectedTags.Any(y => y == x.TagId)).Select(x => x.Id).ToList();
                    if (deletedTags.Count() != 0)
                    {
                        DALHelper.DeleteProductTags(deletedTags);
                    }

                }



                if (result == "")//başarılı bir şekilde eklediyse
                {
                    TempData["pnotify"] = "success,edit," + model.Name;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pnotify"] = "error,edit," + " İlgili kayıt (" + result + ") ";
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }




        [HttpPost]
        public JsonResult AddEditorImage()
        {
            var file = Request.Files[0];
            var path = "/content/upload/editor/";
            path = Process.SaveImage(file, path);
            return Json(path);
        }

        public ActionResult GaleriList(int id)
        {
            var dbProdList = DALHelper.GetGaleriList(id);
            return View(dbProdList);
        }
        public JsonResult GetProductTagData(int productId)
        {
            var producttagList = DALHelper.GetAllProductTag(productId);

            var tagList = DALHelper.GetTagList();
            ProductTagVM aTagVM;
            var tags = new List<ProductTagVM>();
            foreach (var item in tagList)
            {
                aTagVM = new ProductTagVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Url = item.Url,
                    Selected = producttagList.Any(x => x.TagId == item.Id)
                };
                tags.Add(aTagVM);
            }


            return Json(new { Tags = tags }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int deletedId = 0)
        {
            if (deletedId != 0)
            {
                var dbModel = DALHelper.GetProductForId(deletedId);
                string result = DALHelper.DeleteProduct(dbModel);
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

    }

}      
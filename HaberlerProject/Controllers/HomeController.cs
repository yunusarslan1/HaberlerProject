using DAL;
using DAL.Helpers;
using HaberlerProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Controllers
{
    public class HomeController : Controller
    {
        dbhaberlerEntities db = new dbhaberlerEntities();


        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Hakkimizda()
        {
            return View();
        }
        
        public ActionResult Iletisim()
        {
            return View();
        }




        public ActionResult Details()
        {
            // var detaysayfasi = db.Product.SingleOrDefault(x => x.SeoDesc.Equals(id));
            var detaysayfasi = DALHelper.DetaySayfasi();
            var liste = new List<ProductVM>();
            ProductVM haber;

            foreach (var item in detaysayfasi)
            {

                haber = new ProductVM
                {
                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value,
                    Description = item.Description,
                    Content = item.Contents
                };
                liste.Add(haber);
            }
            return PartialView(liste);
        }

        

    }






    
}
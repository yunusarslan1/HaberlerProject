using DAL.Helpers;
using HaberlerProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberlerProject.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult _slider()
        {
            var slaytHaberListesi = DALHelper.KarisikHaberListesi();
            var liste = new List<ProductVM>();
            ProductVM haber;


            foreach (var item in slaytHaberListesi)
            {

                haber = new ProductVM
                {

                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value

                };
                liste.Add(haber);
            }
            return PartialView(liste);
        }

        public ActionResult _gundem()
        {

            var gundemhaber = DALHelper.GundemHaberListesi();
            var list = new List<ProductVM>();
            ProductVM haber;

            foreach (var item in gundemhaber)
            {
                haber = new ProductVM
                {
                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value,
                    Description = item.Description


                };
                list.Add(haber);
            }
            return PartialView(list);
        }

        public ActionResult _ekonomi()
        {
            var ekonomihaber = DALHelper.EkonomiHaberListesi();
            var list = new List<ProductVM>();
            ProductVM haber;

            foreach (var item in ekonomihaber)
            {
                haber = new ProductVM
                {
                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value,
                    Description = item.Description
                };
                list.Add(haber);
            }
            return PartialView(list);
        }

        public ActionResult _saglik()
        {
            var saglikhaber = DALHelper.SaglikHaberListesi();
            var list = new List<ProductVM>();
            ProductVM haber;

            foreach (var item in saglikhaber)
            {
                haber = new ProductVM
                {
                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value,
                    Description = item.Description

                };
                list.Add(haber);
            }
            return PartialView(list);
        }

        //public ActionResult _spor()
        //{
        //    var sporhaber = DALHelper.SporHaberListesi();
        //    var list = new List<ProductVM>();
        //    ProductVM haber;

        //    foreach (var item in sporhaber)
        //    {
        //        haber = new ProductVM
        //        {
        //            Name = item.Name,
        //            ListImageUrl = item.ListImageUrl,
        //            PageUrl = item.PageUrl,
        //            CreateDate = item.CreateDate.Value,
        //            Description = item.Description

        //        };
        //        list.Add(haber);
        //    }
        //    return PartialView(list);
        //}

        public ActionResult _ensoneklenen()
        {
            var ensoneklenen = DALHelper.EnSonEklenenHaber();
            var list = new List<ProductVM>();
            ProductVM haber;

            foreach (var item in ensoneklenen)
            {
                haber = new ProductVM
                {
                    Name = item.Name,
                    ListImageUrl = item.ListImageUrl,
                    PageUrl = item.PageUrl,
                    CreateDate = item.CreateDate.Value,
                    Description = item.Description
                };
                list.Add(haber);
            }
            return PartialView(list);
        }
    }
}
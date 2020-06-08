using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.Tool
{
    public class Process
    {
        public static string SaveImage(HttpPostedFileBase files, string folderPath)
        {
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + folderPath)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + folderPath));

                var filePath = folderPath + files.FileName;
                if (File.Exists(HttpContext.Current.Server.MapPath("~" + filePath)))
                    File.Delete(HttpContext.Current.Server.MapPath("~" + filePath));

                files.SaveAs(HttpContext.Current.Server.MapPath("~" + filePath));
                return filePath;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string SaveImageName(HttpPostedFileBase files, string folderPath, string Name)
        {
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~" + folderPath)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~" + folderPath));
                string fileExt = "jpg";
                var fileName = "";
                if (files.ContentType == "image/png") { fileExt = ".png"; fileName = Pages.SetUrl(Name) + fileExt; }
                else if (files.ContentType == "image/jpeg") { fileExt = ".jpg"; fileName = Pages.SetUrl(Name) + fileExt; }
                else if (files.ContentType == "application/pdf") { fileExt = ".pdf"; fileName = Pages.SetUrl(Name) + fileExt; }
                else
                {
                    fileName = files.FileName;
                }



                var filePath = folderPath + fileName;
                if (File.Exists(HttpContext.Current.Server.MapPath("~" + filePath)))
                    File.Delete(HttpContext.Current.Server.MapPath("~" + filePath));

                files.SaveAs(HttpContext.Current.Server.MapPath("~" + filePath));
                return filePath;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
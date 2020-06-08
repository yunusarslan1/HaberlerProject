using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.ViewModel
{
    public class MenuBarVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<SubMenuBarVM> SubMenuList { get; set; }
    }
}
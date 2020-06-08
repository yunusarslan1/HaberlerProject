using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaberlerProject.Models.ViewModel
{
    public class JsTreeItemVM
    {
        public string id { get; set; }
        public string text { get; set; }
        public JsTreeState state { get; set; }
        public List<JsTreeItemVM> children { get; set; }
    }
    public class JsTreeState
    {
        public bool opened { get; set; }
    }
}
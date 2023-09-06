using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class CommonModel
    {
        public int Value { get; set; }
        public string Text { get; set; }

    }


    public class DropDown
    {
        public List<CommonModel> lstDropDownList { get; set; }
    }

    public class MultiDrodown
    {

        public List<CommonModel> lstDropDownList { get; set; }
        public List<CommonModel> IIDropDownList { get; set; }
        public List<CommonModel> IIIDropDownList { get; set; }
        public List<CommonModel> IVDropDownList { get; set; }
        public List<CommonModel> VDropDownList { get; set; }
        public List<CommonModel> VIDropDownList { get; set; }
        public List<CommonModel> VIIDropDownList { get; set; }
        public List<CommonModel> VIIIDropDownList { get; set; }
        public List<CommonModel> IXDropDownList { get; set; }
        public List<CommonModel> XDropDownList { get; set; }
        public List<CommonModel> XIDropDownList { get; set; }
        public List<CommonModel> XIIDropDownList { get; set; }
        public List<CommonModel> XIIIDropDownList { get; set; }
        public List<CommonModel> XIVDropDownList { get; set; }
        public List<CommonModel> XVDropDownList { get; set; }
        public List<CommonModel> XVIDropDownList { get; set; }
        public List<CommonModel> XVIIDropDownList { get; set; }
        public List<CommonModel> XVIIIDropDownList { get; set; }
        public List<CommonModel> XIXDropDownList { get; set; }
        public List<CommonModel> XXDropDownList { get; set; }
        public List<CommonModel> XXIDropDownList { get; set; }
        public List<CommonModel> XXIIDropDownList { get; set; }
        public List<CommonModel> XXIIIDropDownList { get; set; }

    }
}

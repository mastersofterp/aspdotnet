using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ACCOUNT_User_Control_Reports : System.Web.UI.UserControl
{
    
    protected void imdPdf_Click(object sender, ImageClickEventArgs e)
    {
        Session["ExportType"] = "pdf";
    }
    protected void imgWord_Click(object sender, ImageClickEventArgs e)
    {
        Session["ExportType"] = "doc";
    }
    protected void imgExcel_Click(object sender, ImageClickEventArgs e)
    {
        Session["ExportType"] = "xls";
    }
}

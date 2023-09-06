using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using IITMS.NITPRM;

public partial class ACCOUNT_ShowTransaction : System.Web.UI.Page
{
    Common objCommon = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        Export();
    }
    private void Export()
    {

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_TRANS", "*", "", "", "TRANSACTION_NO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView GVDayWiseAtt = new GridView();
            string ContentType = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                //ds.Tables[0].Columns.RemoveAt(3);
                GVDayWiseAtt.DataSource = ds;
                GVDayWiseAtt.DataBind();

                string attachment = "attachment; filename=Transaction.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GVDayWiseAtt.RenderControl(htw);
                //lvStudApplied.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
            }


        }

        Response.Redirect("~/ACCOUNT/AccountingVouchers.aspx");
    }
}
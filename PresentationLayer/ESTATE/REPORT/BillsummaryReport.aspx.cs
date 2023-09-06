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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Report_BillsummaryReport : System.Web.UI.Page
{
    Common objCommon = new Common();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null || Session["username"] == null ||
                     Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    CheckPageAuthorization();
                    //ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlquartertype, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "QNO>0", "QNO");
                }
                divMsg.InnerHtml = string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    //protected void Repeater_energymetercharges_ItemCommand(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DataRowView drv     = e.Item.DataItem as DataRowView;
    //        Image Image_Profile = e.Item.FindControl("Image_Profile") as Image;
    //        Label Label_QTRNO   = e.Item.FindControl("Label_AlumniBirthDate") as Label;
    //        Label Label_name    = e.Item.FindControl("Label_dtReg") as Label;
    //        Label Label_electric= e.Item.FindControl("Label_Father") as Label;
    //        Label Label_water   = e.Item.FindControl("Label_FName") as Label;
    //        Label Label_total   = e.Item.FindControl("Label_FO") as Label;
        
    //    }
    //}
    
    private void ShowReport(string reportTitle, string rptFileName)
      {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTATE")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTATE," + rptFileName;
            url += "&param=@p_college_code=" + Session["colcode"].ToString() + ",@P_DATE=" + Convert.ToDateTime(txtselectdate.Text).ToString("yyyy-MM-dd") + ",@P_QNO=" + Convert.ToInt32(ddlquartertype.SelectedValue) + ",@P_QRT_Name=" + ddlquartertype.SelectedItem.Text;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updReport, this.updReport.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }


    protected void btnBillsummary_Click(object sender, EventArgs e)
    {
        if (txtselectdate.Text != string.Empty)
        {
           // ShowReport("Bill Summary", "SummaryBillWithQrtType.rpt");
            ShowReport("Bill Summary", "BlockWiseSummaryBill.rpt");
        }
        else 
        {
           // objCommon.DisplayMessage("Please Select Quaerter Type and Month.", this.Page);
            objCommon.DisplayMessage(this.updReport, "Please Select Month.", this.Page);
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtselectdate.Text = string.Empty;
        ddlquartertype.ClearSelection();
    }
}

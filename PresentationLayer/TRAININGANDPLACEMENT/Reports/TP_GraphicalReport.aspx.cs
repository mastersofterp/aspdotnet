using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;


public partial class TRAININGANDPLACEMENT_Reports_TP_GraphicalReport : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,TPController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
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
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0","BATCHNO DESC");
               
            }
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void radlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radlSelect.SelectedValue.ToString().Equals("T"))
        {
           trAdm.Visible=false;
        }
        else
        {
            trAdm.Visible = true;
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedValue.ToString().Equals(0))
            {
                objCommon.DisplayMessage("Please Select Batch..!!", this.Page);
                 
            }    
            else
            {
                if (radlSelect.SelectedValue == "C")
                {
                    ShowReport("Company", "TP_Graph_Company.rpt");

                }
                else if (radlSelect.SelectedValue == "B")
                {
                    ShowReport("Company", "TP_Graph_Branch.rpt");

                }
                else if (radlSelect.SelectedValue == "R")
                {
                    ShowReport("Company", "TP_Graph_Branch.rpt");
                }
            }
                
            if (radlSelect.SelectedValue == "T")
            {
                ShowReport("Company","TP_Graph_Total.rpt");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_TP_GraphicalReport.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            if (radlSelect.SelectedValue == "C")
            {
                url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "," + "@P_TYPE=1";
            }
            else if (radlSelect.SelectedValue == "B")
            {
                url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "," + "@P_TYPE=2";
            }
            else if (radlSelect.SelectedValue == "R")
            {
                url += "&param=@P_ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "," + "@P_TYPE=2";
            }
            else if (radlSelect.SelectedValue == "T")
            {
                url += "&param=@P_ADMBATCH=0" + "," + "@P_TYPE=4";
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_TP_GraphicalReport.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}

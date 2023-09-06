//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     :                            
// CREATION DATE : 02-FEB-2015                                                        
// CREATED BY    : NITIN MESHRAM                                                 
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

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
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class STORES_Reports_str_Req_Status_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    Masters objMasters = new Masters();
    string UsrStatus = string.Empty;

    #region Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            //objCommon = new Common(Session["UserName"].ToString().Trim(), Session["Password"].ToString().Trim(), Session["DataBase"].ToString().Trim());

        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                ViewState["butAction"] = "add";
                //Session["dtitems"] = null;

            }

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
    #endregion

    #region Methods
    private void ShowReport(string reportTitle, string rptFileName)
    {

        string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFrmDt.Text)));
        Fdate = Fdate.Substring(0, 10);
        string Ldate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
        Ldate = Ldate.Substring(0, 10);
                
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UA_NO=" + Session["userno"].ToString() + ",@P_FROMDATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TODATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy");
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon = new Common();
            objCommon.DisplayMessage(updpanel, Common.Message.ExceptionOccured.ToString(), this);
        }

    }
    #endregion
    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("APPROVED REQUISITION STATUS", "strReqStatus.rpt");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtFrmDt.Text = string.Empty;
        txtTodt.Text = string.Empty;
    }
}

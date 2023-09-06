//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : GRAND_Master.aspx                                                  
// CREATION DATE : 02-March-2010                                                       
// CREATED BY    : Chaitanya C. Bhure                                                         
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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Stores_Masters_Grand_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                this.BindListViewGrand();
                ViewState["action"] = "add";
                //Set  Report Parameters
               // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Grand_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    void Clear()
    {
        txtGrndCode.Text = string.Empty;
        txtGrndName.Text = string.Empty;
        txtGrndDet.Text = string.Empty;
        ViewState["action"] = "add";
    }
    void BindListViewGrand()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllGRAND();
            ds = objCommon.FillDropDown("STORE_GRANDMASTER", "GRAND_CODE,GRAND_NAME,GRAND_DETAILS,COLLEGE_CODE", "GRANDNO", "", "");
            lvGrandMaster.DataSource = ds;
            lvGrandMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Grand_Master.BindListViewGrand-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["grandno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsGrand(Convert.ToInt32(ViewState["grandno"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Grand_Master_btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvGrandMaster_PreRender(object sender, EventArgs e)
    {
        BindListViewGrand();
    }
    protected void butSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("add"))
            {
                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_GRANDMASTER", " count(*)", "grand_code= '" + Convert.ToString(txtGrndCode.Text)+ "'"));

                if (duplicateCkeck == 0)
                {
                    CustomStatus cs = (CustomStatus)objStrMaster.AddGrand(txtGrndCode.Text, txtGrndName.Text, txtGrndDet.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        BindListViewGrand();
                        objCommon.DisplayMessage(updpnlMain, "Record Saved Successfully", this);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(updpnlMain,"Record Already Exist", this);
                }
            }
            else
            {
                int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_GRANDMASTER", " count(*)", "grand_code= '" + Convert.ToString(txtGrndCode.Text) + " ' and grandno <> " + Convert.ToInt32(ViewState["grandno"].ToString())));

                 if (duplicateCkeck == 0)
                 {
                     if (ViewState["grandno"] != null)
                     {

                         CustomStatus csupd = (CustomStatus)objStrMaster.UpdateGrand(Convert.ToInt32(ViewState["grandno"]), txtGrndCode.Text, txtGrndName.Text, txtGrndDet.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                         if (csupd.Equals(CustomStatus.RecordUpdated))
                         {
                             ViewState["action"] = "add";
                             this.Clear();
                             this.BindListViewGrand();
                             objCommon.DisplayMessage(updpnlMain,"Record Updated Succesfully", this);
                         }
                     }
                 }
                 else
                 {
                     objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                 }
            }
        }
    }

    private void ShowEditDetailsGrand(int grandno)
    {
        DataSet ds = null;

        try
        {
           // ds = objStrMaster.GetSingleRecordGrand(grandno);
            ds = objCommon.FillDropDown("STORE_GRANDMASTER", "GRAND_CODE,GRAND_NAME,GRAND_DETAILS,COLLEGE_CODE", "GRANDNO", "GRANDNO=" + grandno, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGrndCode.Text = ds.Tables[0].Rows[0]["GRAND_CODE"].ToString();
                txtGrndName.Text = ds.Tables[0].Rows[0]["GRAND_NAME"].ToString();
                txtGrndDet.Text = ds.Tables[0].Rows[0]["GRAND_DETAILS"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Grand_Master.ShowEditDetailsTAX-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
       
        //this.Clear();
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowReport("Grand Master REPORT", "Grand_Master_Report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"];
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


           // objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Grand_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //protected void btnshowrpt_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
    //        //url += "Reports/CommonReport.aspx?";
    //        //url += "pagetitle=" + reportTitle;
    //        //url += "&path=~,Reports,STORES," + rptFileName;
    //        //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"];
    //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        //divMsg.InnerHtml += " </script>";

    //        ////To open new window from Updatepanel
    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        //sb.Append(@"window.open('" + url + "','','" + features + "');");
    //        //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);


    //        objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Grand_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
    //    }
    //    catch (Exception ex)
    //    {
    //        //if (Convert.ToBoolean(Session["error"]) == true)
    //        //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        //else
    //        //    objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
}

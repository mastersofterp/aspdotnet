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
//using Google.API.Translate;
using System.Drawing;
using System.ComponentModel;
using System.Net;
using System.IO;


public partial class PAYROLL_MASTERS_PayMainDepartment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITMasController objITMas = new ITMasController();
    DeptDesigMaster objDeptDesig = new DeptDesigMaster();
    string UsrStatus = string.Empty;

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
        if (!Page.IsPostBack)
        {
            //Check Session
            Form.Attributes.Add("onLoad()", "msg()");
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                ViewState["action"] = "add";
                BindListView();
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "onLoad", "onLoad();", true);
    }
    #region Private Method
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayDepartment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayDepartment.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objDeptDesig.GetMainDepartment();

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvDepartment.DataSource = ds;
                lvDepartment.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PAYROLL")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_UserId=" + Session["userfullname"].ToString() + ",@P_ReportName=" + reportTitle;
            //",@P_Session=" + Session["Session"].ToString() + ",@P_Ip=" + Session["IPADDR"].ToString() ++ ",@P_WorkingDate=" + Session["WorkingDate"].ToString().Trim()

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpanel, this.updpanel.GetType(), "controlJSScript", sb.ToString(), true);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    private void Clear()
    {
        txtmaindeptname.Text = "";
        txtmaindeptcode.Text = "";
        // txtDeptKannad.Text = "";

        ViewState["lblFNO"] = null;
        ViewState["action"] = "add";
    }
    #endregion

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //Update();
        PayMaster objPay = new PayMaster();
        objPay.MAINDEPTNAME = txtmaindeptname.Text.Trim();
        objPay.MAINDEPTCODE = txtmaindeptcode.Text.Trim();

        if (ViewState["action"] != null)
        {
            bool result = CheckPurpose();
            if (ViewState["action"].ToString().Equals("add"))
            {
                //bool result = CheckPurpose();

                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    objPay.MAINDEPTNO = 0;
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddMainDepartment(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Saved Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);
                }
            }
            else
            {
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    ViewState["action"] = "add";
                    objPay.MAINDEPTNO = Convert.ToInt32(ViewState["lblFNO"].ToString().Trim());
                    CustomStatus cs = (CustomStatus)objDeptDesig.AddMainDepartment(objPay);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully!", this.Page);
                        ViewState["lblCNO"] = null;
                        Clear();
                    }
                    else
                        objCommon.DisplayMessage(this.updpanel, "Exception Occured", this.Page);





                }
            }
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        // dsPURPOSE = objCommon.FillDropDown("ACD_ILIBRARY", "*", "", "BOOK_NAME='" + txtBTitle.Text + "'", "");
        // AMOL SAWARKAR  CONDITION OR ORIGINAL CHNAGE (AND)
        dsPURPOSE = objCommon.FillDropDown("PAYROLL_MAINDEPT", "*", "", "MAINDEPT='" + txtmaindeptname.Text + "' AND MAIN_CODE='" + txtmaindeptcode.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
       // ShowReport("ITDEDUCTIONHEADS", "Pay_ITDeductionHead.rpt");
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["lblFNO"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblMainDeptName = lst.FindControl("lblmainDept") as Label;
            Label lblMainDeptCode= lst.FindControl("lblmaindeptcode") as Label;
            txtmaindeptname.Text = lblMainDeptName.Text.Trim();
            txtmaindeptcode.Text = lblMainDeptCode.Text.Trim();
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
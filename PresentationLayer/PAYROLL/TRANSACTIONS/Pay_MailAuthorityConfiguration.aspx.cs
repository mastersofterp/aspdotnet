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
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class PAYROLL_TRANSACTIONS_Pay_MailAuthorityConfiguration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();
    int collegeno;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
    }
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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //   ShowEmpDetails();   
                ViewState["action"] = "add";

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() != "1")
                {
                    FillCollege();
                    BindEmployeeMailAuthorityDetails();
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //pnlId.Visible = true;
                    FillCollege();
                    BindEmployeeMailAuthorityDetails();
                }
            }
        }
        else
        {

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_MailAuthorityConfiguration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_MailAuthorityConfiguration.aspx");
        }
    }
    private void FillCollege()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_MailAuthorityConfiguration.aspx.FillDropDownPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {

        int cs = 0;
        try
        {
            if (ddlCollege.SelectedIndex < 0)
            {
                string MSG = "Please  Select College!!";
                MessageBox(MSG);
                return;
            }
            
            if(txtemployeename.Text == "" || Convert.ToString(txtemployeename.Text) == null)
            {
                string MSG = "Please Enter Employee Name!!";
                MessageBox(MSG);
                return;
            }
           
            if (txtemployeeemail.Text == "" || Convert.ToString(txtemployeename.Text) == null)
            {
                string MSG = "Please Enter Employee Email Id!!";
                MessageBox(MSG);
                return;
            }
            else
            {
                if (ViewState["action"] != null)
                {
                    if (ddlCollege.SelectedIndex > 0) //yes
                    {

                        objEM.COLLEGEID = Convert.ToInt32(ddlCollege.SelectedValue);

                    }
                    else //no
                    {
                        objEM.COLLEGEID = 0;
                    }
                    if (txtemployeename.Text != "")
                    {
                        objEM.EMPNAME = txtemployeename.Text.ToString();
                    }
                    else
                    {
                        objEM.EMPNAME = "";
                    }
                    if (txtemployeeemail.Text != "")
                    {
                        objEM.EMAILID = txtemployeeemail.Text.Trim();
                    }
                    else
                    {
                        objEM.EMAILID = "";
                    }
                    if (txtnotificationdays.Text != "")
                    {
                        objEM.NOTIFICATIONDAYS = Convert.ToInt32(txtnotificationdays.Text.Trim());
                    }
                    else
                    {
                        objEM.NOTIFICATIONDAYS = 0;
                    }
                    if (chkisactive.Checked == true)
                    {
                        objEM.IsActive = true;
                    }
                    else
                    {
                        objEM.IsActive = false;
                    }
                    objEM.PASSWORD = "";
                    //HERE UPDATE THE EMPLOYEE ASSET DETAILE
                    if (ViewState["action"].ToString().Equals("edit"))
                    {
                        objEM.EMPAUTHMAILID = EMPMAILAUTHID;
                        cs = Convert.ToInt32(objECC.UpdateEmployeeMailAuthority(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Updated Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeMailAuthorityDetails();
                            txtnotificationdays.Text = txtemployeename.Text = txtemployeeemail.Text = "";
                            ddlCollege.SelectedIndex=0;
                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                    else
                    {
                        cs = Convert.ToInt32(objECC.SaveEmployeeMailAuthoriy(objEM));
                        if (cs == 1)
                        {
                            string MSG = "Records Saved Sucessfully";
                            MessageBox(MSG);
                            BindEmployeeMailAuthorityDetails();
                            txtnotificationdays.Text = txtemployeename.Text = txtemployeeemail.Text = "";
                            ddlCollege.SelectedIndex=0;

                        }
                        else
                        {
                            string MSG = "Records Saved Failed";
                            MessageBox(MSG);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_MailAuthorityConfiguration.aspx.btnsave_Click->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    
    public void BindEmployeeMailAuthorityDetails()
    {
        bool status;
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpMailAUthority();
            }
            else
            {
                ds = objECC.GetAllEmpMailAUthority();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvempmaillist.DataSource = ds.Tables[0];
                lvempmaillist.DataBind();
            }
            else
            {
                lvempmaillist.DataSource = null;
                lvempmaillist.DataBind();
            }
        }
        else
        {
            return;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    static int EMPMAILAUTHID = 0;
    protected void btneditmail_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            EMPMAILAUTHID = int.Parse(btnEdit.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetAllEmplMailAuthorityIDWISE(EMPMAILAUTHID);
            if (dsdues.Tables[0].Rows.Count > 0)
            {

                ddlCollege.SelectedValue = dsdues.Tables[0].Rows[0]["CollegeId"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["CollegeId"].ToString();
                txtemployeename.Text = dsdues.Tables[0].Rows[0]["Name"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["Name"].ToString();
                txtemployeeemail.Text = dsdues.Tables[0].Rows[0]["MailID"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["MailID"].ToString();
                txtnotificationdays.Text = dsdues.Tables[0].Rows[0]["Notification_Days"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["Notification_Days"].ToString();
                if (Convert.ToBoolean(dsdues.Tables[0].Rows[0]["Isactive"]) == true)
                 {
                     chkisactive.Checked = true;
                 }
                else
                {
                    chkisactive.Checked = false;

                 }
            }
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_MailAuthorityConfiguration.aspx.btneditmail_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {

    }
}
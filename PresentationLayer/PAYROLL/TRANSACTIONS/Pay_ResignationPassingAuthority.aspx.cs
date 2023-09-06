using System;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Net;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_TRANSACTIONS_Pay_ResignationPassingAuthority : System.Web.UI.Page
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
                // BindEmployeeDocumentsDetails();  
                ViewState["action"] = "add";

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() != "1")
                {
                    FillCollege();
                    BindEmployeeRegPassingPassDetails();

                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    FillCollege();
                    BindEmployeeRegPassingPassDetails();
                }
            }
        }
        else
        {

        }
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
        objCommon.FillDropDownList(ddlusername, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE in(3,5)", "");
    }
    public void BindEmployeeRegPassingPassDetails()
    {
        bool status;
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpResigntionPassingDetails();
            }
            else
            {
                ds = objECC.GetAllEmpResigntionPassingDetails();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvResignationPassAuthName.DataSource = ds.Tables[0];
                lvResignationPassAuthName.DataBind();
            }
            else
            {
                lvResignationPassAuthName.DataSource = null;
                lvResignationPassAuthName.DataBind();
            }
        }
        else
        {
            return;
        }
    }
    public void BindEmpRegPassingPassDetailsByCollege(int CollegeNo)
    {
        bool status;
        if (Session["usertype"] != null)
        {
            DataSet ds = objECC.GetAllEmpResigntionPassingDetailsByCollege(CollegeNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dpPager.Visible = true;
            }
            else
            {
                dpPager.Visible = false;
            }
            lvResignationPassAuthName.DataSource = ds.Tables[0];
            lvResignationPassAuthName.DataBind();
        }
        else
        {
            return;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int cs = 0;
        if (ddlCollege.SelectedIndex < 0 && txtpassingpassname.Text == "" && ddlpassingpasstype.SelectedIndex < 0 && ddlusername.SelectedIndex < 0)
        {
            string MSG = "Please  Enter Proper Details!!";
            MessageBox(MSG);
            return;
        }
        else
        {
          if (ViewState["action"] != null)
          {
            if (ddlCollege.SelectedIndex > 0)
            {
                objEM.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);

            }
            else
            {
                objEM.COLLEGE_NO = 0;
            }
            if (txtpassingpassname.Text != "")
            {
                objEM.PANAME = txtpassingpassname.Text.ToString();
            }
            else
            {
                objEM.PANAME = "";
            }
            if (ddlpassingpasstype.SelectedIndex > 0)
            {
                objEM.PASSTYPE = Convert.ToInt32(ddlpassingpasstype.SelectedValue);
            }
            else
            {
                objEM.PASSTYPE = 0;
            }
            if(ddlusername.SelectedIndex > 0)
            {
                objEM.UA_NO = Convert.ToInt32(ddlusername.SelectedValue);
            }
            else
            {
                objEM.UA_NO = 0;
            }
            if (ViewState["action"].ToString().Equals("edit"))
            {
                objEM.REGPASSID = RegPassId;
                cs = Convert.ToInt32(objECC.UpdateEmpResignationPassingPass(objEM));
                if (cs == 1)
                {
                    string MSG = "Records Updated Sucessfully";
                    MessageBox(MSG);
                    BindEmployeeRegPassingPassDetails();
                    ddlCollege.SelectedIndex = 0;
                    ddlpassingpasstype.SelectedIndex = 0;
                    ddlusername.SelectedIndex = 0;
                    txtpassingpassname.Text = "";
                    btnsave.Text = "Save";
                }
                else
                {
                    string MSG = "Records Saved Failed";
                    MessageBox(MSG);
                }
            }
            else
            {
                cs = Convert.ToInt32(objECC.SaveEmpResignationPassingPass(objEM));
                if (cs == 2)
                {
                    string MSG = "Records Aleady  Exists";
                    MessageBox(MSG);
                }
                if (cs == 1)
                {
                    string MSG = "Records Saved Sucessfully";
                    MessageBox(MSG);
                    BindEmployeeRegPassingPassDetails();
                    ddlCollege.SelectedIndex = 0;
                    ddlpassingpasstype.SelectedIndex = 0;
                    ddlusername.SelectedIndex = 0;
                    txtpassingpassname.Text = "";
                    btnsave.Text = "Save";
                   
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
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
        BindEmpRegPassingPassDetailsByCollege(CollegeNo);
    }
    
    static int RegPassId = 0;
    protected void btneditDOC_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;

            RegPassId = int.Parse(btnEdit.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetEmpRegPassingPassByID(RegPassId);
            if (dsdues.Tables[0].Rows.Count > 0)
            {

                ddlCollege.SelectedValue = dsdues.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlpassingpasstype.SelectedValue = dsdues.Tables[0].Rows[0]["PASSTYPE"].ToString();
                ddlusername.SelectedValue = dsdues.Tables[0].Rows[0]["UA_NO"].ToString();
                txtpassingpassname.Text = dsdues.Tables[0].Rows[0]["PANAME"] == DBNull.Value ? "" : dsdues.Tables[0].Rows[0]["PANAME"].ToString();
                        }
            ViewState["action"] = "edit";
            btnsave.Text = "Update";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_ResignationPassingAuthority.aspx.btneditDOC_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, System.EventArgs e)
    {
        if(ddlCollege.SelectedIndex < 0)
        {
            BindEmployeeRegPassingPassDetails();
        }
        else
        {
            int CollegeNo = Convert.ToInt32(ddlCollege.SelectedValue);
            BindEmpRegPassingPassDetailsByCollege(CollegeNo);
        }
        
    }
}
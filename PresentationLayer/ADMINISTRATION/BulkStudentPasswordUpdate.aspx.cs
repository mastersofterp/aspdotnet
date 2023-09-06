//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// PAGE NAME     : BULK USER CREATION OF STUDENTS                                  
// CREATION DATE : 19-Aug-2009                                                     
// CREATED BY    : NIRAJ D. PHALKE    
// ADDED BY      : ASHISH DHAKATE
// ADDED DATE    : 30-DEC-2011                                             
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

//using System;
//using System.Data;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Collections;
//using System.Configuration;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.IO;
//using IITMS;
//using IITMS.UAIMS;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogic;
//using mastersofterp_MAKAUAT;
//using mastersofterp;
//using mastersofterp;
using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using mastersofterp_MAKAUAT;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using ClosedXML.Excel;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class ADMINISTRATION_BulkStudentLogin : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Student objSC = new Student();
    StudentController objStud = new StudentController();
    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


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
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }
            //Populate DropDownLists
            PopulateDropDown();

            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Rishabh on 28/12/2021
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Rishabh on 28/12/2021
        }
        divMsg.InnerHtml = string.Empty;


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
        }
    }


    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.PopulateDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        if (rdGeneratepass.Checked == true)
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN USER_ACC U ON (S.IDNO = U.UA_IDNO) LEFT JOIN USER_PWD_GENERATION_LOG UL ON(U.UA_IDNO = UL.UA_IDNO)", "IDNO", "STUDNAME,REGNO,U.UA_NAME,ISNULL(UL.STATUS,0) STATUSS", "UA_TYPE = 2 AND S.ADMBATCH =" + Convert.ToInt32(ddlAdmBatch.SelectedValue), "REGNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds.Tables[0];
                lvStudents.DataBind();
                //pnllistview.Visible = true;
                lvStudents.Visible = true;
                //btnUpdate.Enabled = true;
            }
            else
            {
                objCommon.DisplayUserMessage(upduser, "No Record Found!", Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                //pnllistview.Visible = false;
                lvStudents.Visible = false;
                // btnUpdate.Enabled = false;
            }
        }
        if (rdoCopyPassword.Checked == true)
        {
            DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN USER_ACC U ON (S.IDNO = U.UA_IDNO) LEFT JOIN TEMP_PASS UL ON(U.UA_IDNO = UL.IDNO)", "S.IDNO", "STUDNAME,REGNO,U.UA_NAME,ISNULL(STATUSS,0) STATUSS", "UA_TYPE = 2 AND S.ADMBATCH =" + Convert.ToInt32(ddlAdmBatch.SelectedValue), "REGNO");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds.Tables[0];
                lvStudents.DataBind();
                //pnllistview.Visible = true;
                lvStudents.Visible = true;
                //btnUpdate.Enabled = true;
            }
            else
            {
                objCommon.DisplayUserMessage(upduser, "No Record Found!", Page);
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                //pnllistview.Visible = false;
                lvStudents.Visible = false;
                // btnUpdate.Enabled = false;
            }
        }
    }





    protected void btnUpdatePass_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            User_AccController objACC = new User_AccController();
            UserAcc objUA = new UserAcc();

            if (rdGeneratepass.Checked == true)
            {
                foreach (ListViewDataItem itm in lvStudents.Items)
                {
                    System.Web.UI.WebControls.CheckBox chk = itm.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
                    System.Web.UI.WebControls.Label lblreg = itm.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                    System.Web.UI.WebControls.HiddenField hdnf = itm.FindControl("hidStudentId") as System.Web.UI.WebControls.HiddenField;
                    System.Web.UI.WebControls.Label lblstud = itm.FindControl("lblstud") as System.Web.UI.WebControls.Label;



                    //if (chk.Checked == true && (chk.Enabled == true && chk.Text == ""))
                    //{
                    objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                    // id = objUA.UA_IDNo;
                    objUA.UA_Name = lblreg.Text;
                    string pwd = string.Empty;
                    string PasswordName = CommonComponent.GenerateRandomPassword.GenearteFourLengthPassword();
                    pwd = PasswordName;
                    objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                    objUA.UA_FullName = lblstud.Text;

                    ret = objACC.UpdateStudentUserPwdRandom(objUA);
                    if (ret == 1)
                    {
                        objCommon.DisplayMessage(this.upduser, "Students Password Update Successfully", this.Page);
                    }


                    //}
                }
            }
            if (rdoCopyPassword.Checked == true)
            {
                foreach (ListViewDataItem itm in lvStudents.Items)
                {
                    System.Web.UI.WebControls.CheckBox chk = itm.FindControl("chkRow") as System.Web.UI.WebControls.CheckBox;
                    System.Web.UI.WebControls.Label lblreg = itm.FindControl("lblreg") as System.Web.UI.WebControls.Label;
                    System.Web.UI.WebControls.HiddenField hdnf = itm.FindControl("hidStudentId") as System.Web.UI.WebControls.HiddenField;
                    System.Web.UI.WebControls.Label lblstud = itm.FindControl("lblstud") as System.Web.UI.WebControls.Label;



                    //if (chk.Checked == true && (chk.Enabled == true && chk.Text == ""))
                    //{
                    objUA.UA_IDNo = Convert.ToInt32(hdnf.Value);
                    // id = objUA.UA_IDNo;
                    objUA.UA_Name = lblreg.Text;
                    string pwd = string.Empty;
                    string getpwd = objCommon.LookUp("User_Acc", "UA_PWD", "UA_NAME='" + objUA.UA_Name + "'");
                    string strPwd = clsTripleLvlEncyrpt.ThreeLevelDecrypt(getpwd);
                    objUA.UA_Pwd = strPwd;
                    //string PasswordName = CommonComponent.GenerateRandomPassword.GenearteFourLengthPassword();
                    //pwd = PasswordName;
                    //objUA.UA_Pwd = clsTripleLvlEncyrpt.ThreeLevelEncrypt(pwd);
                    objUA.UA_FullName = lblstud.Text;

                    ret = objACC.CopyStudentUserPwdRandom(objUA);
                    if (ret == 1)
                    {
                        objCommon.DisplayMessage(this.upduser, "Students Password Update Successfully", this.Page);
                    }


                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Administration_BulkStudentLogin.btnModify_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlAdmBatch_SelectedIndexChanged1(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
    }
}



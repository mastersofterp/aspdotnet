//======================================================================================
// PROJECT NAME  : RFC-COMMON                                           
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Examiner Master                                                   
// CREATION DATE : 12th July,2022                                                   
// CREATED BY    : Nikhil L. (Intergrated Only)                                        
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using ClosedXML.Excel;

public partial class Academic_PhdExaminerMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    PhdController objSC = new PhdController();
    Student objs = new Student();

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
        try
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    ViewState["pstaffno"] = "0";
                    ViewState["IDNO"] = "0";
                    ViewState["action"] = "add";

                    //objCommon.FillDropDownList(ddlexaminer, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND UA_TYPE = 3 AND (DRCSTATUS='S' OR DRCSTATUS='SD')", "UA_FULLNAME");
                    objCommon.FillDropDownList(ddlBankname, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                    ddlexaminer.Visible = false;

                    string IPADDRESS = string.Empty;

                   // string clientMachineName;
                    //clientMachineName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);
                    ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];


                    //to hide or show examiner status
                    ExaminerSettings();


                }
            }

            this.BindExaminer();

        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_Masters_Staff.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public void ExaminerSettings()
    {
        try
        {
            if (Session["usertype"].ToString() == "1")
            {
                divStatus.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#ExaminerStatus').show();$('td:nth-child(10)').show();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#ExaminerStatus').show();$('td:nth-child(10)').show();});", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#ExaminerStatus').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#ExaminerStatus').hide();$('td:nth-child(10)').hide();});", true);
            }
            else
            {
                divStatus.Visible = false;
                chkActiveOrInactive.Checked = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "$('#ExaminerStatus').hide();$('td:nth-child(10)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#ExaminerStatus').hide();$('td:nth-child(10)').hide();});", true);
            }
        }
        catch { }

    }

    private string GetUserIPAddress()
    {
        string User_IPAddress = string.Empty;
        string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
        {
            User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

        }
        else////with Proxy detection
        {
            string[] splitter = { "," };
            string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

            int LatestItem = IP_Array.Length - 1;

        }
        return User_IPAddress;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //to hide or show examiner status
        ExaminerSettings();
        Response.Redirect(Request.Url.ToString());
    
    }

    #region User-Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PhdExaminerMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PhdExaminerMaster.aspx");
        }
    }

    //private void PopulateDropDown()
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Academic_Masters_Staff.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
             string Department=string.Empty;
             string Branch_Name=string.Empty;
            if (rbInternal.Checked == false && rbExternal.Checked == false)
            {
                objCommon.DisplayMessage("Please Select Internal/External!", this.Page);
                return;
            }

            objs.IdNo = Convert.ToInt32(ViewState["IDNO"].ToString());
            objs.StudName = txtexaminer.Text.Trim().ToUpper();
            objs.PAddress = txtAddress.Text.Trim().ToUpper();
            objs.StudentMobile = txtMobile.Text.Trim();
            objs.PMobile = txtContact.Text.Trim();
            objs.EmailID = txtEmail.Text.Trim();
            objs.FatherMobile = txtothermobile.Text.Trim();
            objs.MotherMobile = txtothercontact.Text.Trim();
            objs.IPADDRESS = ViewState["ipAddress"].ToString();
            objs.Uano = Convert.ToInt32(Session["userno"]);
            objs.BankNo=Convert.ToInt32(ddlBankname.SelectedValue);
            objs.AccNo = txtAccountNo.Text.Trim();
            objs.Accholdername = txtholdername.Text.Trim();
            objs.IFSCcode = txtIFSCCode.Text.Trim();
            Department = txtdepartment.Text.Trim();
            Branch_Name = txtbranch.Text.Trim();

            string spec = "";
            spec = txtSpecialization.Text.Trim();

            if (rbExternal.Checked == true)
            {
                objs.NADID = "E";
            }
            else
            {
                objs.NADID = "I";
            }
            int status = 0;
            status = chkActiveOrInactive.Checked == true ? 1 : 0;
            string affiliation=string.Empty;
            affiliation=txtAffiliation.Text.ToString().Trim();
          
            //int Examinerno = Convert.ToInt32(objSC.AddPhdExaminerDetails(objs, (chkActiveOrInactive.Checked == true ? 1 : 0), Department, Branch_Name, txtSpecialization.Text, txtAffiliation.Text));
            int Examinerno = Convert.ToInt32(objSC.AddPhdExaminerDetails(objs,status,Department,Branch_Name,spec,affiliation));
            if (Examinerno == 1)
            {
                objCommon.DisplayMessage(updPS, "Examiner Added Successfully!", this.Page);
                ViewState["IDNO"] = 0;
            }
            if (Examinerno == 2)
            {
                objCommon.DisplayMessage(updPS, "Examiner Updated Successfully!", this.Page);
                ViewState["IDNO"] = 0;
            }

            this.BindExaminer();
            this.Clear();

            //to hide or show examiner status
            ExaminerSettings();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdExaminerMaster.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEditRecord = sender as ImageButton;
            lblUserMsg.Text = "Note : Editing information!";
            DataSet ds = objSC.GETPhdExaminerDetailsIDNO(int.Parse(btnEditRecord.CommandArgument));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                ViewState["IDNO"] = dr["IDNO"].ToString();
                txtexaminer.Text = dr["NAME"].ToString();
                txtAddress.Text = dr["ADDRESS"].ToString();
                txtContact.Text = dr["CONTACT_NO"] == DBNull.Value ? string.Empty : dr["CONTACT_NO"].ToString();
                txtEmail.Text = dr["EMAILID"].ToString();
                txtMobile.Text = dr["MOBILE"] == DBNull.Value ? string.Empty : dr["MOBILE"].ToString();
                txtothermobile.Text = dr["OTHERMOBILE"] == DBNull.Value ? string.Empty : dr["OTHERMOBILE"].ToString();
                txtothercontact.Text = dr["OTHERCONTACT_NO"] == DBNull.Value ? string.Empty : dr["OTHERCONTACT_NO"].ToString();
                txtdepartment.Text = dr["DEPARTMENT"].ToString();
                objCommon.FillDropDownList(ddlBankname, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
                ddlBankname.SelectedValue = dr["BANKNO"] == DBNull.Value ? "0" : dr["BANKNO"].ToString();
                txtAccountNo.Text = dr["ACCNO"] == DBNull.Value ? string.Empty : dr["ACCNO"].ToString();
                txtholdername.Text = dr["ACC_HOLDERNAME"] == DBNull.Value ? string.Empty : dr["ACC_HOLDERNAME"].ToString();
                txtIFSCCode.Text = dr["IFSC_CODE"] == DBNull.Value ? string.Empty : dr["IFSC_CODE"].ToString();
                txtbranch.Text = dr["BRANCHNAME"] == DBNull.Value ? string.Empty : dr["BRANCHNAME"].ToString();


                txtSpecialization.Text = dr["SPEC"] == DBNull.Value ? string.Empty : dr["SPEC"].ToString();
                txtAffiliation.Text = dr["AFFILIATION"] == DBNull.Value ? string.Empty : dr["AFFILIATION"].ToString();

                if (dr["EXTERNAL"].ToString() == "I")
                {
                    rbInternal.Checked = true;
                    rbExternal.Checked = false;
                }
                else
                {
                    rbInternal.Checked = false;
                    rbExternal.Checked = true;
                }


                chkActiveOrInactive.Checked = Convert.ToBoolean(dr["EXAMINER_STATUS"]);

                //to hide or show examiner status
                ExaminerSettings();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdExaminerMaster.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {

        txtexaminer.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtContact.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtAddress.Text = string.Empty;
        txtothercontact.Text = string.Empty;
        txtothermobile.Text = string.Empty;
        // ddlexaminer.Visible = false;
        // rbExternal.Checked = true;

        //   rbInternal.Checked = false;
        chkActiveOrInactive.Checked = false;

        ddlBankname.SelectedIndex = 0;
        txtAccountNo.Text = string.Empty;
        txtholdername.Text = string.Empty;
        txtIFSCCode.Text = string.Empty;
        txtbranch.Text = string.Empty;
        txtdepartment.Text = string.Empty;


        txtSpecialization.Text = string.Empty;
        txtAffiliation.Text = string.Empty;
    }

    private void BindExaminer()
    {
        try
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")//admin
            {
                ds = objSC.GETPhdExaminerDetails(Convert.ToInt32(Session["usertype"].ToString()));
            }
            else//FACULTY
            {
                ds = objSC.GETPhdExaminerDetails(Convert.ToInt32(Session["usertype"].ToString()));
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                lvdetails.DataSource = ds;
                lvdetails.DataBind();
                lvdetails.Visible = true;
            }
            else
            {
                lvdetails.DataSource = null;
                lvdetails.DataBind();
                lvdetails.Visible = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdExaminerMaster.BindExaminer() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            //ShowReport("Report", "rptPhDExaminer.rpt");
           // DataSet ds = objSC.GETPhdExaminerDetails_Excel();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    using (XLWorkbook wb = new XLWorkbook())
            //    {
            //        foreach (System.Data.DataTable dt in ds.Tables)
            //        {
            //            //Add System.Data.DataTable as Worksheet.
            //            wb.Worksheets.Add(dt);
            //        }
            //        //Export the Excel file.
            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.Charset = "";
            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        Response.AddHeader("content-disposition", "attachment;filename=Examiner Details.xlsx");
            //        using (MemoryStream MyMemoryStream = new MemoryStream())
            //        {
            //            wb.SaveAs(MyMemoryStream);
            //            MyMemoryStream.WriteTo(Response.OutputStream);
            //            Response.Flush();
            //            Response.End();
            //        }
            //    }
            //}
            //to hide or show examiner status
            ExaminerSettings();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdExaminerMaster.btnReport_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlexaminer_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("User_Acc", "UA_FULLNAME", "UA_MOBILE ,UA_EMAIL", "UA_NO=" + ddlexaminer.SelectedValue, "");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtexaminer.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["UA_MOBILE"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["UA_EMAIL"].ToString();
        }
    }

    protected void rbInternal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInternal.Checked == true)
        {
            ddlexaminer.Visible = true;
            Clear();
        }
        else
        {
            ddlexaminer.Visible = false;
            Clear();
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERTYPE=" + Convert.ToInt32(Session["userno"])+",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.upd1, this.upd1.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdExaminerMaster.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

}

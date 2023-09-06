#region NAMESPACES
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls.Adapters;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Globalization;
using System.IO;
using MSXML2;



#endregion

public partial class Tally_Transactions_tallyCompanyConfigNew : System.Web.UI.Page
{
    Con_PayrollTallyConfig ObjCC = new Con_PayrollTallyConfig();
    Ent_CompanyConfig ObjCCM = new Ent_CompanyConfig();
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Message = string.Empty;
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
                    // Check User Authority 
                    // this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlstaffType, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO <> 0", "STAFFNO");
                    this.objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_NO", "COLLEGE_NAME", "", "COLLEGE_NAME");
                    objCommon.FillDropDownList(ddlServer, "Payroll_TallyConfig", "TallyConfigId", "ServerName+':'+Convert(varchar(10),PortNumber)ServerName", "TallyConfigId > 0 AND IsActive = 1 AND EndTime = '9999-12-31'", "TallyConfigId");
                    BindTallyCompany();
                }
            }

        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return;
        }
    }

    ////public void BindDropDowns()
    ////{
    ////    try
    ////    {

    ////        SqlParameter[] objPar = new SqlParameter[2];
    ////        objPar[0] = new SqlParameter("@CommandType", "BindDropDowns");
    ////        objPar[1] = new SqlParameter("@CollegeId", Session["COLL_ID"]);
    ////        //objPar[2] = new SqlParameter("@TallyCompanyConfigId",)

    ////        DropDownArray[] ddlArray = new DropDownArray[2];
    ////        ddlArray[0] = new DropDownArray(ddlReceiptBook, "CashBookId", "CashBookName", true);
    ////        ddlArray[1] = new DropDownArray(ddlServer, "TallyConfigId", "ServerName", true);






    ////        objCommon.FillDropDownListArray(ddlArray, "[Academic].[uspBindAllTallyCompanyConfigData]", objPar);
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
    ////        return;
    ////    }

    ////}




    public void BindTallyCompany()
    {
        try
        {
            ObjCCM.CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
            ObjCCM.CommandType = "BindPayTallyCompanyId";
            DataSet ds = ObjCC.GetAllDetails(ObjCCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DivCompany.Visible = true;
                    Rep_Company.DataSource = ds;
                    Rep_Company.DataBind();
                }
                else
                {
                    DivCompany.Visible = false;
                    Rep_Company.DataSource = null;
                    Rep_Company.DataBind();
                }
            }
            else
            {
                DivCompany.Visible = false;
                Rep_Company.DataSource = null;
                Rep_Company.DataBind();
            }
        }
        catch (Exception ex)
        {
            // objCommon.DisplayMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }
    }
    protected void btnEdit_click(object sender, EventArgs e)
    {
        try
        {
            btnSubmit.Text = "Update";
            btnSubmit.ToolTip = "Click To Update";
            ImageButton btnEdit = sender as ImageButton;
            int TallyCompanyConfigId = int.Parse(btnEdit.CommandArgument);
            ViewState["TallyCompanyConfigId"] = TallyCompanyConfigId;
            ObjCCM.TallyCompanyConfigId = TallyCompanyConfigId;
            ObjCCM.CommandType = "BindTallyCompanyId";
            ObjCCM.CollegeId = Convert.ToInt32(Session["colcode"]);
            DataSet ds = ObjCC.GetAllDetails(ObjCCM);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ddlstaffType.SelectedValue = ds.Tables[0].Rows[0]["CashBookid"].ToString();
                    ddlServer.SelectedValue = ds.Tables[0].Rows[0]["TallyConfigId"].ToString();
                    txtTallyCompany.Text = ds.Tables[0].Rows[0]["TallyCompanyName"].ToString();
                    chkIsActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActive"]);
                    ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["CollegeId"].ToString();


                }
                else
                {
                    //objCommon.ShowErrorMessage(Panel_Warning, Label_Warning, CLOUD_COMMON.Message.NoFound, CLOUD_COMMON.MessageType.Alert);
                    objCommon.DisplayMessage("Record Not Found", this);
                    return;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Error);
        }
    }

    private void Clear()
    {

        ddlstaffType.SelectedValue = "0";
        //txtBankAddress.Text = string.Empty;
        ddlServer.SelectedValue = "0";
        txtTallyCompany.Text = string.Empty;
        chkIsActive.Checked = true;
        ViewState["TallyCompanyConfigId"] = null;
        btnSubmit.Text = "Submit";
        btnSubmit.ToolTip = "Click To Submit";
        //DivCompany.Visible = false;
        ddlCollege.SelectedValue = "0";
        txtTallyCompany.Visible = true;
        ddlTallyCompany.Visible = false;
    }

    protected void ddltallCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlTallyCompany.SelectedIndex>0)
        {
            txtTallyCompany.Text = ddlTallyCompany.SelectedItem.Text;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            long res = 0;
            ObjCCM.CashBookId = Convert.ToInt32(ddlstaffType.SelectedValue);
            ObjCCM.TallyConfigId = Convert.ToInt32(ddlServer.SelectedValue);

            ObjCCM.TallyCompanyName = Convert.ToString(txtTallyCompany.Text.Trim());


            ObjCCM.IsActive = chkIsActive.Checked;


            ObjCCM.CreatedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjCCM.ModifiedBy = Convert.ToInt32(Session["userno"].ToString());
            ObjCCM.ModifiedDate = DateTime.UtcNow.AddHours(5.5);
            ObjCCM.IPAddress = Convert.ToString(Session["ipAddress"]);
            ObjCCM.MACAddress = Convert.ToString("1");
            ObjCCM.CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);

            if (ViewState["TallyCompanyConfigId"] == null)
            {
                res = ObjCC.AddUpdateTallyConfig(ObjCCM, ref Message);
            }
            else
            {
                ObjCCM.TallyCompanyConfigId = Convert.ToInt32(ViewState["TallyCompanyConfigId"].ToString());
                res = ObjCC.AddUpdateTallyConfig(ObjCCM, ref Message);
            }
            if (res == -99)
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
                objCommon.DisplayMessage(upDetails, "Exception Occured", this);
                return;


            }
            if (res == 0)
            {
                //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.DuplicateEntry, CLOUD_COMMON.MessageType.Alert);

                //return;/
                objCommon.DisplayMessage(upDetails, "Record Already Exists", this);
                return;
            }
            if (res <= 0)
            {
                // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.NotSaved, CLOUD_COMMON.MessageType.Alert);
                objCommon.DisplayMessage(upDetails, "Record Not Saved", this);
                return;

            }
            else
            {
                if (ViewState["TallyCompanyConfigId"] == null)
                {

                    // objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, CLOUD_COMMON.Message.Saved, CLOUD_COMMON.MessageType.Success);
                    objCommon.DisplayMessage(upDetails, "Record Saved Successfully", this);
                    BindTallyCompany();
                    Clear();
                }
                else
                {
                    //  objCommon.ShowErrorMessage(Panel_Confirm, Label_ConfirmMessage, CLOUD_COMMON.Message.Updated, CLOUD_COMMON.MessageType.Success);
                    objCommon.DisplayMessage(upDetails, "Record Updated Successfully", this);
                    BindTallyCompany();
                    Clear();
                }
            }
        }
        catch (System.Exception ex)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("bank.aspx")));
            url += "../Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&format=No";
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@CollegeId=" + Session["COLL_ID"] + "";
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return;
            throw;
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Bank Report", "BankMasterReport.rpt");
            Clear();
        }
        catch (Exception ex)
        {
            //objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            //return;
            throw;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ServerXMLHTTP30 serverHTTP = new ServerXMLHTTP30();
        string responseStr = "";
        string StrRequestXML = "";
        DataSet dsCompany = new DataSet();

        StrRequestXML = "<ENVELOPE>" +
                        "  <HEADER>" +
                        "    <TALLYREQUEST>Export Data</TALLYREQUEST>" +
                        "  </HEADER>" +
                        "  <BODY>" +
                        "    <EXPORTDATA>" +
                        "      <REQUESTDESC>" +
                        "        <REPORTNAME>List of Companies</REPORTNAME>" +
                        "        <STATICVARIABLES>" +
                        "          <SVEXPORTFORMAT>$$SysName:XML</SVEXPORTFORMAT>" +
                        "        </STATICVARIABLES>" +
                        "    </REQUESTDESC>" +
                        "    </EXPORTDATA>" +
                        "  </BODY>" +
                        "</ENVELOPE>";


        string selectedCompany = "";



        try
        {


            string Address = "http://" + ddlServer.SelectedItem.Text;
            serverHTTP.open("POST", Address, false, null, null);
            serverHTTP.send(StrRequestXML);
            responseStr = serverHTTP.responseText;
            dsCompany.ReadXml(new StringReader(responseStr));

            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables.Count == 1)
                {
                    txtTallyCompany.Visible = true;
                    ddlTallyCompany.Visible = false;

                    DataRow dr1 = dsCompany.Tables[0].NewRow();
                    dr1[0] = "Please Select";
                    dsCompany.Tables[0].Rows.InsertAt(dr1, 0);
                    foreach (DataRow drCompany in dsCompany.Tables[0].Rows)
                    {
                        selectedCompany = drCompany[0].ToString();
                    }

                    if (selectedCompany != "")
                    {
                        txtTallyCompany.Text = selectedCompany;
                    }
                }
                else
                {
                    DataRow dr1 = dsCompany.Tables[1].NewRow();
                    //dr1[1] = "Please Select";
                    //dsCompany.Tables[1].Rows.InsertAt(dr1, 0);
                    ddlTallyCompany.Visible = true;
                    txtTallyCompany.Visible = false;

                    ddlTallyCompany.Items.Clear();
                    //ddlTallyCompany.Items.Add("Please Select");
                    //ddlTallyCompany.SelectedItem.Value = "0";

                    if (dsCompany.Tables[1].Rows.Count > 0)
                    {
                        ddlTallyCompany.DataSource = dsCompany.Tables[1];
                        ddlTallyCompany.DataValueField = dsCompany.Tables[1].Columns[0].ToString();
                        ddlTallyCompany.DataTextField = dsCompany.Tables[1].Columns[0].ToString();
                        ddlTallyCompany.DataBind();
                        ddlTallyCompany.SelectedIndex = 0;
                    }

                }
            }

            

        }
        catch (Exception ex)
        {
            if (ex.Message == "Exception from HRESULT: 0x80072EFD")
            {
                Showmessage("Connection Not Possible! \\n Either Tally server is not running on remote machine or Machine is not in network.");
                return;
            }
            else
            {
                throw;
            }
        }




    }




    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }
}
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
using IITMS.SQLServer.SQLDAL;

public partial class HOSTEL_OnlineApply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RoomAllotmentController objRM = new RoomAllotmentController();
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
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"..\includes\prototype.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"..\includes\scriptaculous.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"..\includes\modalbox.js"));

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
                //Page Authorization
                //CheckPageAuthorization();

                if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "7" || Session["usertype"].ToString() == "1")
                {
                    CheckActivity();
                }
                //Session["apply"] = null;
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                PopulateDropdown();
            }
            else
            {
               
                if (Page.Request.Params["__EVENTTARGET"] != null)
                {
                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                    {
                        string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                        bindlist(arg[0], arg[1]);
                    }

                    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                    {
                        txtSearch.Text = string.Empty;
                        lvStudent.DataSource = null;
                        lvStudent.DataBind();
                        lblNoRecords.Text = string.Empty;
                    }
                }
            }
            if (Request.QueryString["id"] != null)
            {
                txtRegno.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(Request.QueryString["id"].ToString()));
                //if (Convert.ToInt32(Request.QueryString["id"].ToString()) == 0)
                FillInformation();
                this.DisplayInfo(Convert.ToInt32(Request.QueryString["id"].ToString()));
            }
            if (Session["usertype"].ToString() != "2")
                trRegno.Visible = true;
            else
            {
                trRegno.Visible = false;
                string idno = objCommon.LookUp("ACD_STUDENT S INNER JOIN USER_ACC U ON(U.UA_IDNO=S.IDNO)", "DISTINCT IDNO", "CAN=0 AND ADMCAN=0 AND UA_NO=" + Convert.ToInt32(Session["userno"].ToString()));
                if (idno == "")
                    objCommon.DisplayMessage("Record Not Found", this.Page);
                else
                {
                    FillInformation();
                    DisplayInfo(Convert.ToInt32(idno));
                }
                
            }   
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;
            if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "7" || Session["usertype"].ToString() == "1")
            {
                CheckActivity();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineApply.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void PopulateDropdown()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "FLOCK=1", "HOSTEL_SESSION_NO DESC");
        ddlSession.SelectedIndex = 1;
        objCommon.FillDropDownList(ddlPermCity, "ACD_CITY", "CITYNO", "CITY", "CITYNO>0", "CITY");
        objCommon.FillDropDownList(ddlPermState, "ACD_STATE", "STATENO", "STATENAME", "STATENO>0", "STATENAME");
        objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "BANKNO>0", "BANKNO");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
           if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OnlineApply.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OnlineApply.aspx");
        }
    }

    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetails(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        //txtRegno.Text = objCommon.LookUp("ACD_STUDENT", "ENROLLNO", "IDNO=" + Convert.ToInt32(lnk.CommandArgument));

        Response.Redirect(url + "&id=" + lnk.CommandArgument);

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "CAN=0 AND ADMCAN=0 AND ENROLLNO='" + txtRegno.Text.Trim() + "'");
            if (idno == "")
            {
                objCommon.DisplayMessage("Please Enter Valid Enrollment No.", this.Page);
                return;
            }
            else
            {
                this.DisplayInfo(Convert.ToInt32(idno));
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineApply.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void DisplayInfo(int idno)
    {
        DataSet ds = null;
        
        ds = objRM.GetStudentInfoByIdno(idno);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            /// show student information

            lblStudName.Text = dr["STUDNAME"].ToString();
            lblSex.Text = dr["SEX"].ToString();
            lblRegNo.Text = dr["REGNO"].ToString();
            lblDateOfAdm.Text = ((dr["ADMDATE"].ToString().Trim() != string.Empty) ? Convert.ToDateTime(dr["ADMDATE"].ToString()).ToShortDateString() : dr["ADMDATE"].ToString());
            lblPaymentType.Text = dr["PAYTYPENAME"].ToString();
            lblDegree.Text = dr["DEGREENAME"].ToString();
            lblBranch.Text = dr["BRANCH_NAME"].ToString();
            lblYear.Text = dr["YEARNAME"].ToString();
            lblSemester.Text = dr["SEMESTERNAME"].ToString();
            lblSemester.ToolTip = dr["SEMESTERNO"].ToString();
            lblBatch.Text = dr["BATCHNAME"].ToString();
            lblCategory.Text = dr["CATEGORY"].ToString();
            lblAppSem.Text = dr["APP_SEM"].ToString();
            lblAppSem.ToolTip = dr["APP_SEMNO"].ToString();

            if (dr["PCITY"].ToString() != "0" && dr["PCITY"].ToString() != "")
                ddlPermCity.SelectedValue = dr["PCITY"].ToString();

            if (dr["PSTATE"].ToString() != "0" && dr["PSTATE"].ToString() != "")
                ddlPermState.SelectedValue = dr["PSTATE"].ToString();
            if(txtPermAddress.Text =="")
                txtPermAddress.Text = dr["PADDRESS"].ToString();
            
            // BANK DETAILS
            if (dr["BANKNOS"].ToString()!="0")
                ddlBank.SelectedValue = dr["BANKNOS"].ToString();
            else
                ddlBank.SelectedValue = dr["BANKNO"].ToString();
            if(dr["ACC_NO"].ToString()!="")
                txtAccNo.Text = dr["ACC_NOS"].ToString();
            else
                txtAccNo.Text = dr["ACC_NO"].ToString();
            txtBBranch.Text = dr["BANK_BRANCH"].ToString();
            txtMobileNo.Text = dr["STUDENTMOBILE"].ToString();

            // 1sem AIEEE
            lblAIEEE.Text = dr["MERIT_NO"].ToString();
            if (lblAppSem.ToolTip == "1") trAIEEE.Visible = true; else trAIEEE.Visible = false;

            divStudInfo.Visible = true;
            divMessSlip.Visible = true;
            ViewState["Idno"] = idno;
            if (dr["PHOTO"].ToString()!="")
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";

            //Application form
            string check = objCommon.LookUp("ACD_HOSTEL_APPLY_STUDENT", "IDNO", " [STATUS]=1 AND IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (check == "")
                btnAppform.Enabled = false;
            else
                btnAppform.Enabled = true;
           

            //Demand Slip
            string id = string.Empty;
            id = objCommon.LookUp("ACD_DCR", "DISTINCT IDNO", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND IDNO=" + idno);
            
            if (id != "")
            {
                btnMess.Visible = true;
                btnHostel.Visible = true;
                ViewState["Idno"] = id;
                divMessSlip.Visible = true;
            }
            else
            {
                btnMess.Visible = false;
                btnHostel.Visible = false;
                divMessSlip.Visible = false;
            }

            //Show result for (n-2) semester
            //DataSet ds1 = objCommon.FillDropDown("ACD_TRRESULT TR INNER JOIN ACD_STUDENT S ON (S.IDNO=TR.IDNO)", "TR.IDNO", "TR.SEMESTERNO,DBO.FN_DESC('SEMESTER',TR.SEMESTERNO)SEMESTER,SGPA,CGPA", "TR.IDNO="+idno+" AND TR.SEMESTERNO=S.SEMESTERNO-2 AND SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=TR.IDNO AND SEMESTERNO=TR.SEMESTERNO AND SCHEMENO=TR.SCHEMENO)", "TR.IDNO");
            DataSet ds1 = objCommon.FillDropDown("ACD_TRRESULT TR INNER JOIN ACD_STUDENT S ON (S.IDNO=TR.IDNO) left outer join (select min(semesterno)sem,idno from acd_trresult a where idno=" + idno + " and result='F' and sessionno=(select MAX(sessionno) from acd_trresult where idno=a.idno and semesterno=a.semesterno and sessionno<" + Convert.ToInt32(Session["currentsession"].ToString()) + ") group by idno)C on(c.idno=tr.idno)", "TR.IDNO", "TR.SEMESTERNO,DBO.FN_DESC('SEMESTER',TR.SEMESTERNO)SEMESTER,(case tr.result when 'F' then 0 else CAST(ROUND(SGPA,2)AS NUMERIC(4,2))end)SGPA,(case when c.sem<tr.semesterno then 0 else (case tr.result when 'F' then 0 else CAST(ROUND(CGPA,2)AS NUMERIC(4,2))end)end)CGPA", "TR.IDNO=" + idno + " AND TR.SEMESTERNO=(" + Convert.ToInt32(lblSemester.ToolTip) + "-1) AND SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO=TR.IDNO AND SEMESTERNO=TR.SEMESTERNO AND SCHEMENO=TR.SCHEMENO and sessionno<" + Convert.ToInt32(Session["currentsession"].ToString()) + ")", "TR.IDNO");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lvResult.DataSource = ds1;
                lvResult.DataBind();
            }

            // Check Half Fee Paid
            string recon = objCommon.LookUp("ACD_DCR", "RECON", "CAN=0 AND IDNO=" + idno + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND RECIEPT_CODE='MF' AND SEMESTERNO IN (1,3,5,7,9) AND RECON=1");
            if (recon == "True")
            {
                rdlMess.Items.FindByValue("1").Text = "Half Payment ( 9000 )";
                rdlMess.Items.FindByValue("0").Enabled = false;
            }
        }
    }

    
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int output=0;
            string check = objCommon.LookUp("ACD_HOSTEL_APPLY_STUDENT", "COUNT(DISTINCT IDNO)", "IDNO=" + Convert.ToInt32(ViewState["Idno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (check == "0")
            {
                output = objRM.ApplyHostel(Convert.ToInt32(ViewState["Idno"].ToString()), Convert.ToInt32(ddlSession.SelectedValue), 1, Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ViewState["Pcity"].ToString()), Convert.ToInt32(ViewState["Pstate"].ToString()), ViewState["PAddress"].ToString(), Convert.ToInt32(ViewState["Bank_no"].ToString()), ViewState["Acc_no"].ToString(), ViewState["Bank_Branch"].ToString(), ViewState["MobileNo"].ToString());
                if (output != -99)
                {
                    objCommon.DisplayMessage("Record Saved Successfully", this.Page);
                    DisplayInfo(Convert.ToInt32(ViewState["Idno"].ToString()));
                    btnAppform.Enabled = true;
                }
                else
                    objCommon.DisplayMessage("Transcation Failed", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Your Entry Already Exist", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineApply.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString()=="2")
            Response.Redirect(Request.Url.ToString());
        else
            Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id=")));
    }

    protected void btnMess_Click(object sender, EventArgs e)
    {
        string recon = objCommon.LookUp("ACD_DCR", "RECON", "CAN=0 AND IDNO=" + Convert.ToInt32(ViewState["Idno"].ToString()) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND RECIEPT_CODE='MF' AND SEMESTERNO IN (1,3,5,7,9) AND RECON=1");
        if (recon == "True")
            ShowReport(ViewState["Idno"].ToString(), "Fee_Receipt_Hostel", "FeeCollectionReceipt_Mess_Half.rpt", "MF");
        else
            ShowReport(ViewState["Idno"].ToString(), "Fee_Receipt_Hostel", "FeeCollectionReceipt_Mess.rpt", "MF");
    }
    protected void btnHostel_Click(object sender, EventArgs e)
    {
        ShowReport(ViewState["Idno"].ToString(), "Fee_Receipt_Hostel", "FeeCollectionReceipt_Hostel.rpt", "HF");
    }

    private void ShowReport(string param, string reportTitle, string rptFileName, string Receipt_code)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            if (Receipt_code.ToString() == "MF")
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + param.ToString() + ",@P_DEGREENO=0,@P_BRANCHNO=0,@P_SEMESTERNO=0,@P_RECEIPT_CODE=" + Receipt_code.ToString() + ",@P_PAYMENT_TYPE=" + Convert.ToInt32(rdlMess.SelectedValue);
            else
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + param.ToString() + ",@P_DEGREENO=0,@P_BRANCHNO=0,@P_SEMESTERNO=0,@P_RECEIPT_CODE=" + Receipt_code.ToString() + ",@P_PAYMENT_TYPE=0";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_OnlineApply.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void btnAppform_Click(object sender, EventArgs e)
    {
        ShowReportApp("Application Form", "ApplicationForm.rpt");
    }

    private void ShowReportApp(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["Idno"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Hostel_OnlineApply.ShowReportApp() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void FillInformation()
    {
        ViewState["Acc_no"] = txtAccNo.Text.Trim();
        ViewState["Bank_Branch"] = txtBBranch.Text.Trim();
        ViewState["Bank_no"] = ddlBank.SelectedValue;
        ViewState["PAddress"]=txtPermAddress.Text.Trim();
        ViewState["Pcity"] = ddlPermCity.SelectedValue;
        ViewState["Pstate"] = ddlPermState.SelectedValue;
        ViewState["MobileNo"] = txtMobileNo.Text.Trim();
    }

    private void CheckActivity()
    {
        string sessionno = string.Empty;
        sessionno = objCommon.LookUp("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (AM.ACTIVITY_NO = SA.ACTIVITY_NO)", "SA.SESSION_NO", "AM.ACTIVITY_CODE = 'AP_Hostel'");

        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                btnSubmit.Visible = false;
                return;

            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                btnSubmit.Visible = false;
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage("Apply Hostel Activity has been Stopped, You are only allow to generate reports.", this.Page);
            btnSubmit.Visible = false;
            return;
        }
        dtr.Close();
    }
    
}

//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : ESATE
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 16-OCT-2015
// DESCRIPTION   : ONLINE APPLICATION FORM TO APPLY FOR QUARTER.
//=========================================================================
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
using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

public partial class ESTATE_Transaction_OnlineApplication : System.Web.UI.Page
{ 

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineApp objOApp = new OnlineApp();
    OnlineAppController objOACon = new OnlineAppController();

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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlquartertype, "EST_QRT_TYPE", "QNO", "QUARTER_TYPE", "QNO>0", "QNO");

                    objCommon.FillDropDownList(ddlApplicant, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO)", "E.IDNO", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "P.PSTATUS='Y'  AND E.IDNO NOT IN (SELECT EMPID FROM EST_ONLINE_APPLICATION WHERE STATUS =1)", "E.PFILENO");
                 
                    FillEmpInfo();
                   
                    BindlistView();                    
                    txtApplicationDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    if (Convert.ToInt32(Session["idno"]) != 0)
                    {
                        txtApplicationDate.Enabled = false;
                    }
                    else
                    {
                        txtApplicationDate.Enabled = true;
                    }
                }              
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to check the authorization of page.
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    //this method is used to get the employee details.
    private void FillEmpInfo()
    
    {
        DataSet ds = null;
        if (Convert.ToInt32(Session["idno"]) != 0)
        {
            // for faculty   
           // trAppList.Visible = false;
            tdApp1.Visible = false;
            tdApp2.Visible = false;
            tdApp3.Visible = false;

            tdApp4.Visible = true;
            tdApp5.Visible = true;
            tdApp6.Visible = true;

            imgApp.Visible = false;
            ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SCALE S ON (P.SCALENO = S.SCALENO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DIG ON (E.SUBDESIGNO = DIG.SUBDESIGNO)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.DOJ, E.TOWNADD1, E.PHONENO, E.EMAILID, DPT.SUBDEPT, DIG.SUBDESIG, S.PAY_BAND", "P.PSTATUS='Y' AND E.IDNO =" + Convert.ToInt32(Session["idno"]), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                txtDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                txtdatejoing.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                txtPayBand.Text = ds.Tables[0].Rows[0]["PAY_BAND"].ToString();
                txtPAdd.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();
                txtContNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                imgApp.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Information is not Found.');", true);
                Clear();
            }
        }
        else
        {
            // for Admin
         //trAppList.Visible = true;
            tdApp1.Visible = true;
            tdApp2.Visible = true;
            tdApp3.Visible = true;

            tdApp4.Visible = true;
            tdApp5.Visible = true;
            tdApp6.Visible = true;

            imgApp.Visible = true;
          // ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SCALE S ON (P.SCALENO = S.SCALENO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DIG ON (E.SUBDESIGNO = DIG.SUBDESIGNO)", "isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'')  AS NAME", "E.DOJ, E.TOWNADD1, E.PHONENO, E.EMAILID, DPT.SUBDEPT, DIG.SUBDESIG, S.PAY_BAND", "P.PSTATUS='Y' AND E.IDNO =" + Session["idno"], "");
         }
           
       
    }

    // This event is used to Submit the details of Hired vehicle.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(Session["idno"]) != 0)
            {
                objOApp.EMPID = Convert.ToInt32(Session["idno"]);
            }
            else
            {
                objOApp.EMPID = Convert.ToInt32(ddlApplicant.SelectedValue);
            }
            objOApp.TOTAL_MEMBERS = Convert.ToInt32(txtMembers.Text.Trim());
            objOApp.REMARK = txtRemark.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtRemark.Text.Trim());  
            objOApp.QRT_TYPE = Convert.ToInt32(ddlquartertype.SelectedValue);
            objOApp.APPLICATION_DATE = Convert.ToDateTime(txtApplicationDate.Text);

            if (ViewState["APP_ID"] == null)
            {
                CustomStatus cs = (CustomStatus)objOACon.AddUpdateOnlineApplication(objOApp);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updpnlge, "Record Already Exist.", this.Page);
                    return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Submitted Successfully.');", true);
                    Clear();
                }                
            }
            else
            {
                objOApp.APPID = Convert.ToInt32(ViewState["APP_ID"].ToString());
                CustomStatus cs = (CustomStatus)objOACon.AddUpdateOnlineApplication(objOApp);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    Clear();
                    objCommon.DisplayMessage(this.updpnlge, "Record Already Exist.", this.Page);
                    return;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                Clear();
            }
            BindlistView();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //This event is used to cancel the last selection.
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }

    //This event is used to modify the existing record.
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int app_id = int.Parse(btnEdit.CommandArgument);
            ViewState["APP_ID"] = int.Parse(btnEdit.CommandArgument);
            ShowDetails(app_id);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to bind the list of application of user.
    private void BindlistView()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["idno"]) != 0)
            {
                ds = objCommon.FillDropDown("EST_ONLINE_APPLICATION O INNER JOIN PAYROLL_EMPMAS E ON (O.EMPID = E.IDNO)  LEFT JOIN EST_QRT_TYPE Q ON (Q.QNO = O.QRT_TYPE)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "O.REMARK, (CASE WHEN O.TOTAL_MEMBERS = 0 THEN NULL ELSE O.TOTAL_MEMBERS END) as TOTAL_MEMBERS, Q.QUARTER_TYPE, O.APPID, O.STATUS", "O.EMPID=" + Convert.ToInt32(Session["idno"]), "O.APPID desc");
            }
            else
            {
                ds = objCommon.FillDropDown("EST_ONLINE_APPLICATION O INNER JOIN PAYROLL_EMPMAS E ON (O.EMPID = E.IDNO)  LEFT JOIN EST_QRT_TYPE Q ON (Q.QNO = O.QRT_TYPE)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "O.REMARK, (CASE WHEN O.TOTAL_MEMBERS = 0 THEN NULL ELSE O.TOTAL_MEMBERS END) as TOTAL_MEMBERS, Q.QUARTER_TYPE, O.APPID, O.STATUS", "", "O.APPID desc");
            }            
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvOnApp.DataSource = ds;
                lvOnApp.DataBind();
                lvOnApp.Visible = true;
            }
            else
            {
                lvOnApp.DataSource = null;
                lvOnApp.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to show the details of the selected record.
    private void ShowDetails(int app_id)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("EST_ONLINE_APPLICATION", "*", "", "APPID=" + app_id, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TOTAL_MEMBERS"].ToString() == "0")
                {
                    txtMembers.Text = string.Empty;
                }
                else
                {
                    txtMembers.Text = ds.Tables[0].Rows[0]["TOTAL_MEMBERS"].ToString();
                }
                txtRemark.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                ddlquartertype.SelectedValue = ds.Tables[0].Rows[0]["QRT_TYPE"].ToString();
                ddlApplicant.SelectedValue = ds.Tables[0].Rows[0]["EMPID"].ToString();
                if (Convert.ToInt32(Session["idno"]) == 0)
                {
                    ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SCALE S ON (P.SCALENO = S.SCALENO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DIG ON (E.SUBDESIGNO = DIG.SUBDESIGNO)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+' '+isnull(E.FNAME,'')+' '+isnull(E.MNAME,'')+' '+isnull(E.LNAME,'')  AS NAME", "E.DOJ, E.TOWNADD1, E.PHONENO, E.EMAILID, DPT.SUBDEPT, DIG.SUBDESIG, S.PAY_BAND", "P.PSTATUS='Y' AND E.IDNO =" + Convert.ToInt32(ddlApplicant.SelectedValue), "");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                        txtDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                        txtDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                        txtdatejoing.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                        txtPayBand.Text = ds.Tables[0].Rows[0]["PAY_BAND"].ToString();
                        txtPAdd.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();
                        txtContNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                        txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    }
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to clear the controles.
    private void Clear()
    {
        if (Convert.ToInt32(Session["idno"]) != 0)
        {
            txtMembers.Text = string.Empty;
            txtRemark.Text = string.Empty;
            ddlquartertype.SelectedIndex = 0;
            ViewState["APP_ID"] = null;
        }
        else
        {
            txtName.Text = string.Empty;
            txtDesig.Text = string.Empty;
            txtDept.Text = string.Empty;
            txtPayBand.Text = string.Empty;
            txtPAdd.Text = string.Empty;
            txtdatejoing.Text = string.Empty;
            txtContNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
           
            txtMembers.Text = string.Empty;
            txtRemark.Text = string.Empty;
            ddlquartertype.SelectedIndex = 0;
            ViewState["APP_ID"] = null;
            ddlApplicant.SelectedIndex = 0;
        }
    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Hire Vehicle Details", "rptHireDetails.rpt");
    }

    private void ShowReport(string exporttype, string rptFileName)
    {
        //try
        //{
        //    string Script = string.Empty;

        //    string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("vehicle_maintenance")));
        //    url += "Reports/CommonReport.aspx?";
        //    url += "exporttype=pdf";
        //    url += "&filename=HireVehicleDetailReport.pdf";
        //    url += "&path=~,Reports,VEHICLE_MAINTENANCE," + rptFileName;
        //    url += "&param=@P_HIRE_TYPE=" + ddlHireType.SelectedValue;

        //    // To open new window from Updatepanel
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        //    sb.Append(@"window.open('" + url + "','','" + features + "');");
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.ShowHireDetails() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objUCommon.ShowError(Page, "Server Unavailable.");
        //}

    }

    protected void ddlApplicant_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;

            //ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SCALE S ON (P.SCALENO = S.SCALENO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DIG ON (E.SUBDESIGNO = DIG.SUBDESIGNO)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'')  AS NAME", "E.DOJ, E.TOWNADD1, E.PHONENO, E.EMAILID, DPT.SUBDEPT, DIG.SUBDESIG, S.PAY_BAND", "P.PSTATUS='Y' AND E.IDNO =" + Convert.ToInt32(ddlApplicant.SelectedValue), "");
            ds = objCommon.FillDropDown("PAYROLL_EMPMAS E INNER JOIN PAYROLL_PAYMAS P ON (E.IDNO = P.IDNO) INNER JOIN PAYROLL_SCALE S ON (P.SCALENO = S.SCALENO) INNER JOIN PAYROLL_SUBDEPT DPT ON (E.SUBDEPTNO = DPT.SUBDEPTNO) INNER JOIN PAYROLL_SUBDESIG DIG ON (E.SUBDESIGNO = DIG.SUBDESIGNO)", "isnull(E.PFILENO,'')+' - '+isnull(E.Title,'')+''+isnull(E.FNAME,'')+''+isnull(E.MNAME,'')+''+isnull(E.LNAME,'')  AS NAME", "E.DOJ, E.TOWNADD1, E.PHONENO, E.EMAILID, DPT.SUBDEPT, DIG.SUBDESIG", "P.PSTATUS='Y' AND E.IDNO =" + Convert.ToInt32(ddlApplicant.SelectedValue), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtDesig.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                txtDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                txtdatejoing.Text = ds.Tables[0].Rows[0]["DOJ"].ToString();
                txtPayBand.Text = ds.Tables[0].Rows[0]["PAY_BAND"].ToString();
                txtPAdd.Text = ds.Tables[0].Rows[0]["TOWNADD1"].ToString();
                txtContNo.Text = ds.Tables[0].Rows[0]["PHONENO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Information is not Found.');", true);
                Clear();
            }            
        }           
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTATE_Transaction_OnlineApplication.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindlistView();
    }
}
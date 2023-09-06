// ============================================================================
// CREATED BY    : MRUNAL SINGH      
// CREATED DATE  : 20/06/2017
// MODIFIED DATE : 
// MODIFIED BY   : 
// DESCRIPTION   : USED TO INSERT UPDATE AGENDA DETAILS 
// =============================================================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Web.Caching;
using System.Drawing;
using System.Configuration;
using System.Linq;
using System.Web.Security;





public partial class MEETING_MANAGEMENT_TRANSACTION_AgendaContents : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingMaster objMM = new MeetingMaster();
    MeetingController objMCon = new MeetingController();

    public static int pk_agenda_id;
    public string Docpath = ConfigurationManager.AppSettings["DirPath"];
    public static string RETPATH = "";

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
                    ViewState["action"] = "add";
                }
                if (Convert.ToInt32(Session["usertype"]) == 1)
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0", "NAME");
                else
                    objCommon.FillDropDownList(ddlCommitee, "TBL_MM_COMITEE", "ID", "NAME", "[STATUS] = 0 AND  DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "", "NAME");
               
                objMM.LOCK = 'N';
                objMM.TABLE_ITEM = 'N';
                Session["RecTbl"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

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
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


   


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                objMM.COLLEGE_NO = 0; // Convert.ToInt32(Session["coll_name"]);
                objMM.COMMITEE_NO = Convert.ToInt32(ddlCommitee.SelectedValue);
                objMM.AGENDA_ID = Convert.ToInt32(ddlAgenda.SelectedValue);
                objMM.MEETING_CODE = ddlpremeeting.SelectedItem.Text;
                objMM.USERID = Convert.ToInt32(Session["userno"]);

                if (lvContent.Items.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Agenda Contents.');", true);
                    return;
                }

                DataTable dt;
                dt = (DataTable)Session["RecTbl"];
                objMM.CONTENT_TBL = dt;

                if (ViewState["action"].ToString().Equals("add"))
                {
                    objMM.ACID = 0;

                    if (objMCon.AddUpdateAgendaContents(objMM) != 0)
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Saved Successfully.');", true);
                        Clear();
                        ddlCommitee.SelectedIndex = 0;
                    }
                }
                else
                {
                    objMM.ACID = Convert.ToInt32(ViewState["ACID"]);
                    CustomStatus cs = (CustomStatus)objMCon.AddUpdateAgendaContents(objMM);
                    if (objMCon.AddUpdateAgendaContents(objMM) != 0)
                    {
                        BindlistView();
                        ViewState["action"] = "add";
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Updated Successfully.');", true);
                        Clear();
                        ddlCommitee.SelectedIndex = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    // This method is used to clear the controls.
    private void Clear()
    {
        ddlCommitee.SelectedIndex = 0;
        if (ddlpremeeting.Items.Count > 0)
        {
            ddlpremeeting.Items.Clear();
            ddlpremeeting.DataSource = null;
            ddlpremeeting.DataBind();
            ddlpremeeting.Items.Insert(0, "Please Select"); //Shaikh Juned 08-09-2022
        }
        else
        {

        }
        
        ddlAgenda.SelectedIndex = 0;
        lvContent.DataSource = null;
        lvContent.DataBind();

        pnlAgendaList.Visible = false;
        Session["RecTbl"] = null;
        ViewState["SRNO"] = null;

        lvAgendaDetails.DataSource = null;
        lvAgendaDetails.DataBind();
        pnlContent.Visible = false;
        txtAgendaDetails.Text = string.Empty;
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }


    protected void ddlCommitee_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Enabled = true;
        DataSet DS_MEETINCODE = objCommon.FillDropDown("TBL_MM_AGENDA", "distinct MEETING_CODE", "MEETING_CODE", "FK_MEETING='" + Convert.ToInt32(ddlCommitee.SelectedValue) + "'", "");
        if (DS_MEETINCODE.Tables.Count > 0)
        {
            if (DS_MEETINCODE.Tables[0].Rows.Count > 0)
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.Items.Add("Please Select");
                ddlpremeeting.SelectedItem.Value = "0";
                ddlpremeeting.DataTextField = "MEETING_CODE";
                ddlpremeeting.DataValueField = "MEETING_CODE";
                ddlpremeeting.DataSource = DS_MEETINCODE.Tables[0];
                ddlpremeeting.DataBind();
                ddlpremeeting.SelectedIndex = 0;
            }
            else
            {
                ddlpremeeting.Items.Clear();
                ddlpremeeting.DataSource = null;
                ddlpremeeting.DataBind();
                ddlAgenda.Items.Clear();
            }
        }

    }


    


    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlpremeeting.SelectedIndex > 0)
            {
                ShowAgendaContentReport("pdf", "AgendaContentDetailsReport.rpt");
            }
            else if (ddlCommitee.SelectedValue=="0")  //Shaikh Juned 08-09-2022
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Committee.');", true);
                return;
            }
            else if (ddlpremeeting.SelectedValue == "0") //Shaikh Juned 08-09-2022
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Meeting.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowAgendaContentReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=AgendaContentReport" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            url += "&param=@P_MEETING_CODE=" + ddlpremeeting.SelectedItem.Text + ",@P_COMMITTEEID=" + Convert.ToInt32(ddlCommitee.SelectedValue);

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.ShowAgendaContentReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    

    protected void ddlpremeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlAgenda, "TBL_MM_AGENDA", "PK_AGENDA", "AGENDATITAL", "LOCK = 'N' AND FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlpremeeting.SelectedItem.Text + "'", "PK_AGENDA");
        BindlistView();
    }


    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA A ON (AC.AGENDA_ID = A.PK_AGENDA)", "ACID, AC.AGENDA_ID, A.AGENDANO", "A.AGENDATITAL, AC.MEETING_CODE, A.LOCK", "A.MEETING_CODE='" + ddlpremeeting.SelectedItem.Text + "'", "AC.ACID");

            //  DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS AC INNER JOIN TBL_MM_AGENDA A ON (AC.AGENDA_ID = A.PK_AGENDA) INNER JOIN  TBL_MM_AGENDA_CONTENT_DETAILS ACD ON (AC.ACID = ACD.ACID)", "AC.ACID, AC.AGENDA_ID, A.AGENDANO", "A.AGENDATITAL, AC.MEETING_CODE, ACD.CONTENT_DETAILS", "AC.AGENDA_ID =" + Convert.ToInt32(ddlAgenda.SelectedValue), "ACD.ACD_ID");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lvAgendaDetails.DataSource = ds;
                    lvAgendaDetails.DataBind();
                    pnlContent.Visible = true;
                }
            }
            else
            {
                lvAgendaDetails.DataSource = null;
                lvAgendaDetails.DataBind();
                pnlContent.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ACID = int.Parse(btnEdit.CommandArgument);
            ViewState["ACID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(ACID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(int ACID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENTS", "*", "", "ACID=" + ACID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {               

              
                ddlCommitee.SelectedValue = ds.Tables[0].Rows[0]["COMMITTEEID"].ToString();
                objCommon.FillDropDownList(ddlpremeeting, "TBL_MM_AGENDA", "distinct MEETING_CODE", "MEETING_CODE", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue), "");
                ddlpremeeting.SelectedValue = ds.Tables[0].Rows[0]["MEETING_CODE"].ToString();
                objCommon.FillDropDownList(ddlAgenda, "TBL_MM_AGENDA", "PK_AGENDA", "AGENDATITAL", "FK_MEETING=" + Convert.ToInt32(ddlCommitee.SelectedValue) + " AND MEETING_CODE='" + ddlpremeeting.SelectedItem.Text + "'", "PK_AGENDA");
                ddlAgenda.SelectedValue = ds.Tables[0].Rows[0]["AGENDA_ID"].ToString();

                DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENT_DETAILS", "SRNO", "CONTENT_DETAILS", "ACID=" + ACID, "");
                if (dsContent.Tables[0].Rows.Count > 0)
                {
                    lvContent.DataSource = dsContent;
                    lvContent.DataBind();
                    pnlAgendaList.Visible = true;
                    DataTable dt = dsContent.Tables[0];
                    Session["RecTbl"] = dt;

                }
                else
                {
                    lvContent.DataSource = null;
                    lvContent.DataBind();
                    pnlAgendaList.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private DataTable CreateTable()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("CONTENT_DETAILS", typeof(string)));
        return dtRe;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAgendaList.Visible = true;

            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                int maxVal = 0;
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();

                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                }
                if (ViewState["EDIT_SRNO"] != null)
                {
                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO"]);
                }
                else
                {
                    dr["SRNO"] = maxVal + 1;
                }
                dr["CONTENT_DETAILS"] = txtAgendaDetails.Text.Trim() == null ? string.Empty : Convert.ToString(txtAgendaDetails.Text.Trim()).Replace(',', ' ');

                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateTable();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["CONTENT_DETAILS"] = txtAgendaDetails.Text.Trim() == null ? string.Empty : Convert.ToString(txtAgendaDetails.Text.Trim()).Replace(',', ' ');

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    protected void ClearRec()
    {
        txtAgendaDetails.Text = string.Empty;
        ViewState["actionContent"] = null;
        ViewState["EDIT_SRNO"] = null;
    }


    protected void btnEditRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = ((DataTable)Session["RecTbl"]);
                ViewState["EDIT_SRNO"] = btnEditRec.CommandArgument;
                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
                txtAgendaDetails.Text = dr["CONTENT_DETAILS"].ToString();
                dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
                ViewState["actionContent"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "MEETING_MANAGEMENT_TRANSACTION_AgendaContents.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }


    protected void ddlAgenda_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataSet dsContent = objCommon.FillDropDown("TBL_MM_AGENDA_CONTENT_DETAILS ACD INNER JOIN TBL_MM_AGENDA_CONTENTS AC ON (ACD.ACID = AC.ACID)", "SRNO", "CONTENT_DETAILS", "AGENDA_ID = " + Convert.ToInt32(ddlAgenda.SelectedValue), "");
            if (dsContent.Tables[0].Rows.Count > 0)
            {
                lvContent.DataSource = dsContent;
                lvContent.DataBind();
                pnlAgendaList.Visible = true;

                DataTable dt = dsContent.Tables[0];
                Session["RecTbl"] = dt;

            }
            else
            {
                lvContent.DataSource = null;
                lvContent.DataBind();
                pnlAgendaList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_Meeting_Agenda.ddlAgenda_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


}
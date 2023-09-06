
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data;
using System.Web.UI.HtmlControls;


using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text;
using SendGrid;

using mastersofterp_MAKAUAT;
using System.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public partial class ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarkEntryDashboardWireFrameEntity objMEDWFEntity = new MarkEntryDashboardWireFrameEntity();
    MarkEntryDashboardWireFrameController objMEDWFController = new MarkEntryDashboardWireFrameController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null || Session["coll_name"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        if (!Page.IsPostBack)
        {
            //Page Authorization
            CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            PopulateDropDownList();
        }
        //lblmsg.Text = "";
    }


    #region -----------User Define Function-----------------

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MarkEntryDashboardWireFrame.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MarkEntryDashboardWireFrame.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT A WITH (NOLOCK) INNER JOIN ACD_SESSION_MASTER B WITH (NOLOCK) ON (A.SESSIONNO=B.SESSIONNO)", "DISTINCT A.SESSIONNO", "B.SESSION_NAME", "", "A.SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion---------User Define Function-----------------

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (ddlSession.SelectedIndex > 0)
            {
                objMEDWFEntity.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objMEDWFEntity.REPORT_TYPE = 1;
                objMEDWFEntity.TEMP_EXAMNO = 0;
                objMEDWFEntity.COLLEGE_ID = 0;
                objMEDWFEntity.DEPTNO = 0;

                DataSet ds = objMEDWFController.GetCourseCollegeDepartmentWisePendingCount(objMEDWFEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divCourseMarksEntryDetail.Visible = true;
                    gvParent.DataSource = ds;
                    gvParent.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(updMarksEntryDetailReport, "No Record Found", this.Page);
                    gvParent.DataSource = null;
                    gvParent.DataBind();
                }
            }
            else
            {
                gvParent.DataSource = null;
                gvParent.DataBind();
            }
        }
        catch (Exception Ex)
        {
            throw new Exception(Ex.Message);
        }
    }

    protected void gvParent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objMEDWFEntity.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objMEDWFEntity.REPORT_TYPE = 2;
            objMEDWFEntity.COLLEGE_ID = 0;
            objMEDWFEntity.DEPTNO = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChildGrid = (GridView)e.Row.FindControl("gvChild");

                HiddenField hdfTempExam = e.Row.FindControl("hdfTempExam") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR") as HtmlGenericControl;

                objMEDWFEntity.TEMP_EXAMNO = hdfTempExam.Value == string.Empty ? 0 : Convert.ToInt32(hdfTempExam.Value);

                DataSet ds = objMEDWFController.GetCourseCollegeDepartmentWisePendingCount(objMEDWFEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChildGrid.DataSource = ds;
                    gvChildGrid.DataBind();
                }
                else
                {
                    gvChildGrid.DataSource = null;
                    gvChildGrid.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame__gvParent_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChild_1 = (GridView)e.Row.FindControl("gvChild_1");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfTempExam = e.Row.FindControl("hdfTempExam") as HiddenField;
                HtmlGenericControl div = e.Row.FindControl("divcR1") as HtmlGenericControl;

                objMEDWFEntity.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objMEDWFEntity.REPORT_TYPE = 3;
                objMEDWFEntity.TEMP_EXAMNO = hdfTempExam.Value == string.Empty ? 0 : Convert.ToInt32(hdfTempExam.Value);
                objMEDWFEntity.COLLEGE_ID = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objMEDWFEntity.DEPTNO = 0;

                DataSet ds = objMEDWFController.GetCourseCollegeDepartmentWisePendingCount(objMEDWFEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChild_1.DataSource = ds;
                    gvChild_1.DataBind();
                }
                else
                {
                    gvChild_1.DataSource = null;
                    gvChild_1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame_gvChild_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void gvChild_1_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChild_2 = (GridView)e.Row.FindControl("gvChild_2");

                HiddenField hdfCollegeId = e.Row.FindControl("hdfCollegeId") as HiddenField;
                HiddenField hdfTempExam = e.Row.FindControl("hdfTempExam") as HiddenField;
                HiddenField hdfDeptNo = e.Row.FindControl("hdfDeptNo") as HiddenField;
                
                HtmlGenericControl div = e.Row.FindControl("divcR1") as HtmlGenericControl;

                objMEDWFEntity.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objMEDWFEntity.REPORT_TYPE = 4;
                objMEDWFEntity.TEMP_EXAMNO = hdfTempExam.Value == string.Empty ? 0 : Convert.ToInt32(hdfTempExam.Value);
                objMEDWFEntity.COLLEGE_ID = hdfCollegeId.Value == string.Empty ? 0 : Convert.ToInt32(hdfCollegeId.Value);
                objMEDWFEntity.DEPTNO = hdfDeptNo.Value == string.Empty ? 0 : Convert.ToInt32(hdfDeptNo.Value);

                DataSet ds = objMEDWFController.GetCourseCollegeDepartmentWisePendingCount(objMEDWFEntity);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvChild_2.DataSource = ds;
                    gvChild_2.DataBind();
                }
                else
                {
                    gvChild_2.DataSource = null;
                    gvChild_2.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_MarkEntryDashboardWireFrame_gvChild_1_RowDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    
    }

    protected void btnSendDeptMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        int CheckStatus = 0;
        string CollegeId = string.Empty;
        string SrNo = string.Empty;

        Button btn = sender as Button;
        int Dept = Convert.ToInt32(btn.CommandArgument);


        foreach (GridViewRow row in gvParent.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChild = (GridView)row.FindControl("gvChild");
                if (gvChild != null)
                {
                    foreach (GridViewRow row1 in gvChild.Rows)
                    {
                        if (row1.RowType == DataControlRowType.DataRow)
                        {
                            GridView gvChild_1 = (GridView)row1.FindControl("gvChild_1");
                            if (gvChild_1 != null)
                            {
                                foreach (GridViewRow row2 in gvChild_1.Rows)
                                {
                                    if (row2.RowType == DataControlRowType.DataRow)
                                    {
                                        HiddenField hdfMailID = (HiddenField)row2.FindControl("hdfMailID");
                                        HiddenField hdfCollegeId = (HiddenField)row2.FindControl("hdfCollegeId");
                                        HiddenField hdfTempExam = (HiddenField)row2.FindControl("hdfTempExam");
                                       
                                        
                                        CollegeId = hdfCollegeId.Value;
                                        SrNo = hdfTempExam.Value;

                                        CheckBox chk = (CheckBox)row2.FindControl("chk");
                                        if (chk.Checked == true)
                                        {
                                            CheckStatus = 1;
                                            if (hdfMailID.Value != string.Empty)
                                            {
                                                if (ToEmail == string.Empty)
                                                {
                                                    ToEmail += hdfMailID.Value;
                                                }
                                                else
                                                {
                                                    ToEmail += "," + hdfMailID.Value;
                                                }
                                            }
                                            
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (CheckStatus == 0)
        {
            ToEmail = string.Empty;
            foreach (GridViewRow row in gvParent.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvChild = (GridView)row.FindControl("gvChild");
                    if (gvChild != null)
                    {
                        foreach (GridViewRow row1 in gvChild.Rows)
                        {
                            if (row1.RowType == DataControlRowType.DataRow)
                            {
                                GridView gvChild_1 = (GridView)row1.FindControl("gvChild_1");
                                if (gvChild_1 != null)
                                {
                                    foreach (GridViewRow row2 in gvChild_1.Rows)
                                    {
                                        if (row2.RowType == DataControlRowType.DataRow)
                                        {
                                            HiddenField hdfMailID = (HiddenField)row2.FindControl("hdfMailID");
                                            HiddenField hdfDeptNo = (HiddenField)row2.FindControl("hdfDeptNo");
                                            if (Dept == Convert.ToInt32(hdfDeptNo.Value))
                                            {
                                                if (hdfMailID.Value != string.Empty)
                                                {
                                                    if (ToEmail == string.Empty)
                                                    {
                                                        ToEmail += hdfMailID.Value;
                                                    }
                                                    else
                                                    {
                                                        ToEmail += "," + hdfMailID.Value;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        if (ToEmail != string.Empty)
        {
           
            string divname = btn.ToolTip;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
           

            //div4

            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div6" + divname + "');", true);
        }
    
    }

    protected void btnSendFacultyMail_OnClick(object sender, EventArgs e)
    {
        string ToEmail = string.Empty;
        int CheckStatus = 0;
        string CollegeId = string.Empty;
        string SrNo = string.Empty;

        Button btn = sender as Button;

        int College = Convert.ToInt32(btn.CommandArgument);
        int Dept = Convert.ToInt32(btn.ToolTip);
        int ExamNotemp = Convert.ToInt32(btn.CommandName);

        foreach (GridViewRow row in gvParent.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                GridView gvChild = (GridView)row.FindControl("gvChild");
                if (gvChild != null)
                {
                        foreach (GridViewRow row1 in gvChild.Rows)
                        {
                            if (row1.RowType == DataControlRowType.DataRow)
                            {
                                GridView gvChild_1 = (GridView)row1.FindControl("gvChild_1");
                                if (gvChild_1 != null)
                                {
                                    foreach (GridViewRow row2 in gvChild_1.Rows)
                                    {
                                        if (row2.RowType == DataControlRowType.DataRow)
                                        {
                                            GridView gvChild_2 = (GridView)row2.FindControl("gvChild_2");
                                            if (gvChild_2 != null)
                                            {
                                                foreach (GridViewRow row3 in gvChild_2.Rows)
                                                {
                                                    if (row3.RowType == DataControlRowType.DataRow)
                                                    {
                                                        HiddenField hdfMailID = (HiddenField)row3.FindControl("hdfMailID");
                                                        CheckBox chk = (CheckBox)row3.FindControl("chk");
                                                        HiddenField hdfCollegeId = (HiddenField)row2.FindControl("hdfCollegeId");
                                                        HiddenField hdfTempExam = (HiddenField)row2.FindControl("hdfTempExam");
                                                        
                                                        CollegeId = hdfCollegeId.Value;
                                                        SrNo = hdfTempExam.Value;
                                                      
                                                        if (chk.Checked == true)
                                                        {
                                                            CheckStatus = 1;
                                                            if (hdfMailID.Value != string.Empty)
                                                            {
                                                                if (ToEmail == string.Empty)
                                                                {
                                                                    ToEmail = hdfMailID.Value;
                                                                }
                                                                else
                                                                {
                                                                    ToEmail += "," + hdfMailID.Value;
                                                                }
                                                            }
                                                           
                                                        }
                                                       
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }

        if (CheckStatus == 0)
        {
            ToEmail = string.Empty;
            foreach (GridViewRow row in gvParent.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    GridView gvChild = (GridView)row.FindControl("gvChild");
                    if (gvChild != null)
                    {
                        foreach (GridViewRow row1 in gvChild.Rows)
                        {
                            if (row1.RowType == DataControlRowType.DataRow)
                            {
                                GridView gvChild_1 = (GridView)row1.FindControl("gvChild_1");
                                if (gvChild_1 != null)
                                {
                                    foreach (GridViewRow row2 in gvChild_1.Rows)
                                    {
                                        if (row2.RowType == DataControlRowType.DataRow)
                                        {
                                            GridView gvChild_2 = (GridView)row2.FindControl("gvChild_2");
                                            if (gvChild_2 != null)
                                            {
                                                foreach (GridViewRow row3 in gvChild_2.Rows)
                                                {
                                                    if (row3.RowType == DataControlRowType.DataRow)
                                                    {
                                                        HiddenField hdfMailID = (HiddenField)row3.FindControl("hdfMailID");
                                                        HiddenField hdfCollegeId = (HiddenField)row3.FindControl("hdfCollegeId");
                                                        HiddenField hdfDeptNo = (HiddenField)row3.FindControl("hdfDeptNo");
                                                        HiddenField hdfTempExam = (HiddenField)row3.FindControl("hdfTempExam");

                                                        if (Dept == Convert.ToInt32(hdfDeptNo.Value) && College == Convert.ToInt32(hdfCollegeId.Value) && ExamNotemp == Convert.ToInt32(hdfTempExam.Value))
                                                        {
                                                            if (hdfMailID.Value != string.Empty)
                                                            {
                                                                if (ToEmail == string.Empty)
                                                                {
                                                                    ToEmail += hdfMailID.Value;
                                                                }
                                                                else
                                                                {
                                                                    ToEmail += "," + hdfMailID.Value;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        if (ToEmail != string.Empty)
        {
            string divname = btn.ToolTip;
            Session["divname"] = divname;
            Session["ToEmail"] = ToEmail;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + ToEmail + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div5" + divname + "');", true);  
        }
    }

    protected void btnSent_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsconfig = null;
            string COMPANY_EMAILSVCID=string.Empty;
            string SENDGRID_APIKEY=string.Empty;
            string CollegeId = string.Empty;
            string SrNo = string.Empty;
          

            int SendingEmailStatus = 0;
            string EmailId = string.Empty;
            string IPADDRESS = Request.ServerVariables["REMOTE_ADDR"];

            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            Session["SENDGRID_APIKEY"] = string.Empty;
            if (dsconfig.Tables[0].Rows.Count > 0)
            {
               COMPANY_EMAILSVCID = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
               SENDGRID_APIKEY = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            }
            string[] mail = Convert.ToString(Session["ToEmail"]).Split(',');
            DataRow dr = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("TOEMAILID", typeof(string));
            dt.Columns.Add("FROMEMAILID", typeof(string));
            dt.Columns.Add("EMAIL_STATUS", typeof(string));
            dt.Columns.Add("EMAIL_TEXTMATTER", typeof(string));
            dt.Columns.Add("EMAIL_SUBJECT", typeof(string));
            dt.Columns.Add("EMAILFROM_UA_NO", typeof(int));
            dt.Columns.Add("IPADDRESS", typeof(string));
         
            int status = 0;
        
            for (int i = 0; i < mail.Length; i++)
            {
                if (mail[i] != string.Empty)
                {
                    //Gridview Mail Sending Process.
                    Task<int> task = Execute(txtBody.Text, mail[i], txtSubject.Text, COMPANY_EMAILSVCID, SENDGRID_APIKEY);
                    status = task.Result;

                    dr = dt.NewRow();
                    dr["TOEMAILID"] = mail[i];
                    dr["FROMEMAILID"] = COMPANY_EMAILSVCID;
                    dr["EMAIL_STATUS"] = status == 1 ? "Delivered" : "Not Delivered";
                    dr["EMAIL_TEXTMATTER"] = txtBody.Text;
                    dr["EMAIL_SUBJECT"] = txtSubject.Text;
                    dr["EMAILFROM_UA_NO"] = Convert.ToInt32(Session["userno"]);
                    dr["IPADDRESS"] = ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"]; ;
                    dt.Rows.Add(dr);
              
                    if (status == 0)
                    {
                        SendingEmailStatus = 1;
                        if (EmailId == string.Empty)
                        {
                            EmailId = mail[i];
                        }
                        else
                        {
                            EmailId +=","+ mail[i];
                        }
                    }
                }
            }

            objMEDWFController.Insert_MarksEntry_Dashboard_Wire_Frame_Email_Sending_Log(dt);
                   
            if (SendingEmailStatus == 0)
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Sent Successfully", this.Page);
                txt_emailid.Text = string.Empty;
                txtBody.Text = string.Empty;
                txtSubject.Text = string.Empty;
                

                foreach (GridViewRow row in gvParent.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        GridView gvChild = (GridView)row.FindControl("gvChild");
                        if (gvChild != null)
                        {
                            foreach (GridViewRow row1 in gvChild.Rows)
                            {
                                if (row1.RowType == DataControlRowType.DataRow)
                                {
                                    GridView gvChild_1 = (GridView)row1.FindControl("gvChild_1");
                                    if (gvChild_1 != null)
                                    {
                                        foreach (GridViewRow row2 in gvChild_1.Rows)
                                        {
                                            if (row2.RowType == DataControlRowType.DataRow)
                                            {
                                                CheckBox chk = (CheckBox)row2.FindControl("chk");
                                                if (chk.Checked == true)
                                                {
                                                    chk.Checked = false;
                                                }
                                                GridView gvChild_2 = (GridView)row2.FindControl("gvChild_2");
                                                if (gvChild_2 != null)
                                                {
                                                    foreach (GridViewRow row3 in gvChild_2.Rows)
                                                    {
                                                        if (row3.RowType == DataControlRowType.DataRow)
                                                        {
                                                            HiddenField hdfMailID = (HiddenField)row3.FindControl("hdfMailID");
                                                            CheckBox chk1 = (CheckBox)row3.FindControl("chk");
                                                            HiddenField hdfCollegeId = (HiddenField)row2.FindControl("hdfCollegeId");
                                                            HiddenField hdfTempExam = (HiddenField)row2.FindControl("hdfTempExam");
                                                          
                                                            CollegeId = hdfCollegeId.Value;
                                                            SrNo = hdfTempExam.Value;

                                                            if (chk1.Checked == true)
                                                            {
                                                                chk1.Checked = false;
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                string divname = Session["divname"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div4" + SrNo + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div5" + CollegeId + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divexpandcollapse", "divexpandcollapse('div6" + divname + "');", true);
               
                Session["divname"] = string.Empty;
            }
            else
            {
                objCommon.DisplayMessage(updMarksEntryDetailReport, "Email Not Send Some Faculty like " + EmailId+", Please Try Again !!!.", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "View('" + EmailId +"');", true);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AACADEMIC_REPORTS_MarksEntryDetailReport_btnSent_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub, string COMPANY_EMAILSVCID, string SENDGRID_APIKEY)
    {
        int ret = 0;
        try
        {
            var fromAddress = new MailAddress(COMPANY_EMAILSVCID, "MAKAUT");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = SENDGRID_APIKEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(COMPANY_EMAILSVCID, "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }
        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txt_emailid.Text = string.Empty;
        txtBody.Text = string.Empty;
        txtSubject.Text = string.Empty;
    }
}
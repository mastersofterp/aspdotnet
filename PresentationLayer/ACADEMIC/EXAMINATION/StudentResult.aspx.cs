using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_EXAMINATION_StudentResult : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //added on 24-04-2020 by Vaishali
                if (Session["usertype"].ToString().Equals("2"))     //Student 
                {
                    CheckPageAuthorization();
                    PopulateDropDownList();
                    ddlSession.Focus();
                    divDetails.Visible = true;
                    divRegistrationNo.Visible = false;
                    btnShow.Visible = false;
                   // btnShow.Attributes["style"] = "display:none";
                }
                else
                {
                    divDetails.Visible = true;
                    divRegistrationNo.Visible = true;
                    btnShow.Visible = true;
                   // btnShow.Attributes["style"] = "display:block";
                    ddlSession.Focus();
                    PopulateDropDownList();
                   // objCommon.DisplayUserMessage(this.Page, "You are not authorized to view this page !!!!", this.Page);
                }
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        try
        {
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                objCommon.FillDropDownList(ddlSession, "RESULT_PUBLISH_DATA A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "DISTINCT A.SESSIONNO", "SESSION_PNAME", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND B.SESSIONNO > 0 AND ISNULL(IS_MIDS_ENDS,0)=2", "A.SESSIONNO DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlSession, "RESULT_PUBLISH_DATA A INNER JOIN ACD_SESSION_MASTER B ON A.SESSIONNO = B.SESSIONNO", "DISTINCT A.SESSIONNO", "SESSION_PNAME", "B.SESSIONNO > 0 AND ISNULL(IS_MIDS_ENDS,0)=2", "A.SESSIONNO DESC");
            }

            int count = ddlSession.Items.Count;
            if(ddlSession.Items.Count == 1)
                lblNote.Visible = true;
            else
                lblNote.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResult.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

 
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex != 0)
        {
            if (Session["usertype"].ToString().Equals("2"))
            {
                objCommon.FillDropDownList(ddlSemester, "RESULT_PUBLISH_DATA A INNER JOIN ACD_SEMESTER B ON A.SEMESTERNO = B.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND B.SEMESTERNO > 0 AND ISNULL(IS_MIDS_ENDS,0)=2 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "A.SEMESTERNO DESC");
            }
            else
            {
                objCommon.FillDropDownList(ddlSemester, "RESULT_PUBLISH_DATA A INNER JOIN ACD_SEMESTER B ON A.SEMESTERNO = B.SEMESTERNO", "DISTINCT A.SEMESTERNO", "SEMESTERNAME", "B.SEMESTERNO > 0 AND ISNULL(IS_MIDS_ENDS,0)=2 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "A.SEMESTERNO DESC");
            }
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
    }

    protected void btnViewResult_Click(object sender, EventArgs e)
    {
        ViewStudentDetails();
    }

    private void ViewStudentDetails()
    {
        try
        {
            DataSet ds = null;
            if (Session["usertype"].ToString().Equals("2"))     //Student 
            {
                ds = objSC.GetStudresultDetails(Convert.ToInt32(Session["idno"]), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            }
            else
            {
                string idno = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "regno='" + txtRegistrationNo.Text + "'");
                ds = objSC.GetStudresultDetails(Convert.ToInt32(idno), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            }

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                divStudDetails.Visible = true;
                lblName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblSession.Text = ds.Tables[0].Rows[0]["SESSION_PNAME"].ToString();
                lblSGPA.Text = ds.Tables[0].Rows[0]["SGPA"].ToString();

                lvStudent.Visible = true;
                pnlStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                btnPrint.Visible = true;
                spanNote.Visible = true;
            }
            else
            {
                divStudDetails.Visible = false;
                btnPrint.Visible = false;
                spanNote.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            int idno = 0;
            if (Session["usertype"].ToString().Equals("2"))
            {
                idno = Convert.ToInt32(Session["idno"].ToString());
            }
            else
            {
                idno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtRegistrationNo.Text + "'"));
            }
            int degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "DEGREENO", "IDNO=" + Convert.ToInt32(idno)));
            int branchnno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "BRANCHNO", "IDNO=" + Convert.ToInt32(idno)));
            int collegeid = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(idno)));
            int duration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO =" + degreeno + " AND BRANCHNO =" + branchnno + " AND COLLEGE_ID=" + collegeid));
            int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

            if (duration == semesterno)
                ShowReport("StudentResult", "rptProvisionalFinalGradeCardReport.rpt");
            else
                ShowReport("StudentResult", "rptProvisionalGradeCardReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    public void ShowReport(string reportTitle, string rptFileName)
    {
        if (Session["usertype"].ToString() != "2")
            Session["idno"] = objCommon.LookUp("ACD_STUDENT WITH (NOLOCK)", "IDNO", "regno='" + txtRegistrationNo.Text + "'");

        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
       
        url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_IDNO=" + Convert.ToInt32(Session["idno"]) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;
        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
        //divMsg.InnerHtml += " </script>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex != 0)
        {
            if (Session["usertype"].ToString().Equals("2"))
            {
                int ExistDueClear = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS WITH (NOLOCK)", "COUNT(IDNO) AS CNT", "ISNULL(NODUES_STATUS,0)=1 AND IDNO = " + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + ddlSession.SelectedValue + ""));
                if (ExistDueClear > 0)
                {
                    btnViewResult.Visible = true;
                    //int idno = Convert.ToInt32(Session["idno"]);
                }
                else
                {
                    //objCommon.DisplayUserMessage(this.Page, "Your Dues not Clear Yet, Kindly Contact to your Account Section of MAKAUT.", this.Page);
                    objCommon.DisplayUserMessage(this.Page, "Please Contact Finance Office for fees reconciliation.", this.Page);
                   // ddlSession.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                spanNote.Visible = false;
                lblNote.Visible = false;
                divStudDetails.Visible = false;
            }

        }
        else
        {
            btnViewResult.Visible = false;
            btnPrint.Visible = false;
            divStudDetails.Visible = false;
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            spanNote.Visible = false;
            lblNote.Visible = false;
        }
        
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtRegistrationNo.Text + "' AND ISNULL(ADMCAN,0)=0 AND ISNULL(CAN,0)=0");
        idno = idno == string.Empty ? "0" : idno;
        if (Convert.ToInt32(idno) > 0)
        {
           string result= objCommon.LookUp("RESULT_PUBLISH_DATA ", "COUNT(IDNO)","IDNO = " + idno + " AND SEMESTERNO ="+ddlSemester.SelectedValue+"AND ISNULL(IS_MIDS_ENDS,0)=2 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
           if (Convert.ToInt32(result) > 0)
           {
               int ExistDueClear = Convert.ToInt32(objCommon.LookUp("ACD_NODUES_STATUS WITH (NOLOCK)", "COUNT(IDNO) AS CNT", "ISNULL(NODUES_STATUS,0)=1 AND IDNO = " + idno + " AND SESSIONNO=" + ddlSession.SelectedValue + ""));
               if (ExistDueClear > 0)
               {
                   btnViewResult.Visible = false;
                   ViewStudentDetails();
                   //int idno = Convert.ToInt32(Session["idno"]);
               }
               else
               {
                   //objCommon.DisplayUserMessage(this.Page, "Your Dues not Clear Yet, Kindly Contact to your Account Section of MAKAUT.", this.Page);
                   objCommon.DisplayUserMessage(this.Page, "Please Contact Finance Office for fees reconciliation.", this.Page);
                   btnViewResult.Visible = false;
                   btnShow.Visible = true;
                   //ddlSession.SelectedIndex = 0;
                   return;
               }
           }
           else
           {
               objCommon.DisplayUserMessage(this.Page, "Selected Student Result is not Published.", this.Page);
               btnViewResult.Visible = false;
               btnShow.Visible = true;
               //btnShow.Attributes["style"] = "display:block";
               return;
           }
        }
        else
        {
            objCommon.DisplayMessage(updUpdate, "Please Enter Valid Registration Number.", this.Page);

            //btnShow.Attributes["style"] = "display:block";
            return;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    
    public void showhide()
    {
        btnShow.Visible = true;
        btnViewResult.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        lvStudent.Visible = false;
        divStudDetails.Visible = false;
        btnPrint.Visible = false;
        spanNote.Visible = false;
        
    }
    protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
    {
        showhide();
    }
}
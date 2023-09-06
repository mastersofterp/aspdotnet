using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ACADEMIC_ApplyForNoDues : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    Student objstudent = new Student();

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                CheckPageAuthorization();
                int idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
                ViewState["IDNO"] = idno;
                showDetails();
                int flag = Convert.ToInt32(objCommon.LookUp("ACD_NODUES", "count(IDNO)", "IDNO=" + idno));
                if (flag > 0)
                {
                    divchk.Visible = false;
                    btnStatus.Visible = true;
                    int approve = Convert.ToInt32(objCommon.LookUp("ACD_NO_DUES_STATUS", "FINANCE_DEPT_APPROVED", "IDNO=" + idno));
                    if (approve == 1)
                    {
                        btnPrint.Visible = true;
                    }
                }
                else
                {
                    btnStatus.Visible = false;
                    divchk.Visible = true;
                }

            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ApplyForNoDues.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=ApplyForNoDues.aspx");
        }
    }
    private void showDetails()
    {
        int idno = 0;
        idno = Convert.ToInt32(Session["idno"]);
        try
        {
            if (idno > 0)
            {
                DataTableReader dtr = objSC.GetStudentDetailsForNoDues(idno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        //divdetails.Visible = true;
                        lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblStudName.ToolTip = Session["idno"].ToString();
                        lblRegno.Text = dtr["REGNO"].ToString();
                        lblStudClg.Text = dtr["COLLEGE_NAME"] == null ? string.Empty : dtr["COLLEGE_NAME"].ToString();
                        lblStudClg.ToolTip = dtr["COLLEGE_ID"] == null ? string.Empty : dtr["COLLEGE_ID"].ToString();
                        //ViewState["admbatch"] = dtr["ADMBATCH"];
                        lblStudDegree.Text = dtr["DEGREENAME"] == null ? string.Empty : dtr["DEGREENAME"].ToString();
                        lblStudDegree.ToolTip = dtr["DEGREENO"] == null ? string.Empty : dtr["DEGREENO"].ToString();
                        lblStudBranch.Text = dtr["LONGNAME"] == null ? string.Empty : dtr["LONGNAME"].ToString();
                        lblStudBranch.ToolTip = dtr["BRANCHNO"] == null ? string.Empty : dtr["BRANCHNO"].ToString();
                        lblMobileNo.Text = dtr["STUDENTMOBILE"] == null ? string.Empty : dtr["STUDENTMOBILE"].ToString();
                        lblMailId.Text = dtr["EMAILID"] == null ? string.Empty : dtr["EMAILID"].ToString();
                        lblstudSemester.Text = dtr["SEMESTERNAME"] == null ? string.Empty : dtr["SEMESTERNAME"].ToString();
                        lblstudSemester.ToolTip = dtr["SEMESTERNO"] == null ? string.Empty : dtr["SEMESTERNO"].ToString();
                        lblGender.Text = (dtr["SEX"].ToString() == "M" && dtr["SEX"] != null) ? "Male" : "Female";
                        lblYearPass.Text = dtr["YEAR_OF_PASS"] == null ? string.Empty : dtr["YEAR_OF_PASS"].ToString();
                    }
                    else
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ApplyForNoDues.aspx");
                        //divdetails.Visible = false;
                        return;
                    }
                }
                else
                {
                    Response.Redirect("~/notauthorized.aspx?page=ApplyForNoDues.aspx");
                    //divdetails.Visible = false;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForNoDues.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        if (chkselect.Checked == true)
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int flag = Convert.ToInt32(objCommon.LookUp("ACD_NO_DUES_STATUS", "COUNT(IDNO)", "IDNO=" + Convert.ToInt32(lblStudName.ToolTip)));
            if (flag > 0)
            {
                objCommon.DisplayMessage(updTeach, "You Already Apply for No Dues", this.Page);
            }
            else
            {
                objstudent.RegNo = lblRegno.Text;
                objstudent.IdNo = Convert.ToInt32(lblStudName.ToolTip);
                objstudent.StudName = lblStudName.Text;
                objstudent.College_ID = Convert.ToInt32(lblStudClg.ToolTip);
                objstudent.DegreeNo = Convert.ToInt32(lblStudDegree.ToolTip);
                objstudent.BranchNo = Convert.ToInt32(lblStudBranch.ToolTip);
                objstudent.StudentMobile = Convert.ToString(lblMobileNo.Text);
                objstudent.EmailID = Convert.ToString(lblMailId.Text);

                CustomStatus cs = (CustomStatus)objSC.AddNoDuesStud(objstudent, lblYearPass.Text.Trim());
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updTeach, "Record Saved Successfully.", this.Page);
                    divchk.Visible = false;
                    btnSubmit.Visible = false;
                    btnStatus.Visible = true;
                    
                }
            }
            btnSubmit.Visible = false;
            chkselect.Checked = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ApplyForNoDues.btnSubmit_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        BindListViewapprovedstud();
    }
    private void BindListViewapprovedstud()
    {
        try
        {
          
            int idno = Convert.ToInt32(Session["idno"]);
       
            
            DataSet ds = objSC.GetStudTracking(idno);

            if (ds.Tables[0].Rows.Count > 0)
            {

                lvTrackingDetails.DataSource = ds;
                lvTrackingDetails.DataBind();
                
                lvTrackingDetails.Visible = true;

            }
          
            Label lblappproval1Status = (Label)lvTrackingDetails.FindControl("lblappproval1Status");
            lblappproval1Status.Text = ds.Tables[0].Rows[0]["APPROVAL1"].ToString() +" "+ "Approved Status"+"";

            //lblappproval1Status.Text = ds.Tables[0].Rows[0]["APPROVAL1"].ToString();

            Label lblpendingreasonappproval1 = (Label)lvTrackingDetails.FindControl("lblpendingreasonappproval1");
            lblpendingreasonappproval1.Text = lblpendingreasonappproval1.Text + "Pending Reason By" + " " + ds.Tables[0].Rows[0]["APPROVAL1"].ToString() + "";

            Label lblappproval2status = (Label)lvTrackingDetails.FindControl("lblappproval2status");
            lblappproval2status.Text = ds.Tables[0].Rows[0]["APPROVAL2"].ToString() + " " + "Approved Status" + "";

            Label lblpendingappproval2 = (Label)lvTrackingDetails.FindControl("lblpendingappproval2");
            lblpendingappproval2.Text = lblpendingappproval2.Text + "Pending Reason By" + " " + ds.Tables[0].Rows[0]["APPROVAL2"].ToString() + "";

            Label lblappproval3status = (Label)lvTrackingDetails.FindControl("lblappproval3status");
            lblappproval3status.Text = ds.Tables[0].Rows[0]["APPROVAL3"].ToString() + " " + "Approved Status" + "";

            Label lblpendingappproval3 = (Label)lvTrackingDetails.FindControl("lblpendingappproval3");
            lblpendingappproval3.Text = lblpendingappproval3.Text + "Pending Reason By" + " " + ds.Tables[0].Rows[0]["APPROVAL3"].ToString() + "";

            Label lblappproval4status = (Label)lvTrackingDetails.FindControl("lblappproval4status");
            lblappproval4status.Text = ds.Tables[0].Rows[0]["APPROVAL4"].ToString() + " " + "Approved Status" + "";

            Label lblpendingresappproval4 = (Label)lvTrackingDetails.FindControl("lblpendingresappproval4");
            lblpendingresappproval4.Text = lblpendingresappproval4.Text + "Pending Reason By" + " " + ds.Tables[0].Rows[0]["APPROVAL4"].ToString() + "";

            Label lblappproval5status = (Label)lvTrackingDetails.FindControl("lblappproval5status");
            lblappproval5status.Text = ds.Tables[0].Rows[0]["APPROVAL5"].ToString() + " " + "Approved Status" + "";

            Label lblpendingappproval5 = (Label)lvTrackingDetails.FindControl("lblpendingappproval5");
            lblpendingappproval5.Text = lblpendingappproval5.Text + "Pending Reason By" + " " + ds.Tables[0].Rows[0]["APPROVAL5"].ToString() + "";

            //DataSet dsStudent = objCommon.FillDropDown("ACD_Add_APPROVAL_MASTER", "APPROVAL1,APPROVAL2", "APPROVAL3,APPROVAL4,APPROVAL5", "", "");

            //lblApprover1.Text = lblApprover1.Text + "Pending Reason By"+" (" + dsStudent.Tables[0].Rows[0]["APPROVAL1"].ToString() + ")";
            //lblApprover2.Text = lblApprover2.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL2"].ToString() + ")";
            //lblApprover3.Text = lblApprover3.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL3"].ToString() + ")";
            //lblApprover4.Text = lblApprover4.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL4"].ToString() + ")";
            //lblApprover5.Text = lblApprover5.Text + " (" + dsStudent.Tables[0].Rows[0]["APPROVAL5"].ToString() + ")";

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewapprovedstud -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (Session["OrgId"].ToString() == "3" || Session["OrgId"].ToString() == "4")
        {
            ShowReport("No Dues Certificate", "NoDuesCertificateReport_CPUK_CPUH.rpt");
        }
        else
        {
            ShowReport("No Dues Certificate", "NoDuesCertificateReport.rpt");
        }

        //ShowReport("No Dues Certificate", "NoDuesCertificateReport.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"]);

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updTeach, this.updTeach.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch
        {
            throw;
        }
    }
   
}
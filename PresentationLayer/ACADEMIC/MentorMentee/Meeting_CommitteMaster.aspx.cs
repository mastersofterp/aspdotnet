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
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;

public partial class ACADEMIC_MentorMentee_Meeting_CommitteMaster : System.Web.UI.Page
{
    Common objCommon = new Common(); 
    UAIMS_Common objUCommon = new UAIMS_Common();
    MeetingScheduleMaster objMM = new MeetingScheduleMaster();
    Schedule_MeetingController objMC = new Schedule_MeetingController();
     static int ptype;

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
                    objCommon.FillDropDownList(ddlDepartment, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "", "SUBDEPTNO");
                }
               
                BindlistView();
            }
            else
            {
                // msgcomp.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    // This method is used to Bind the Committee list.
    private void BindlistView()
    {
        DataSet ds = null;
        try
        {

            //  ds = objCommon.FillDropDown("TBL_MM_COMITEE CO INNER JOIN ACD_COLLEGE_MASTER CM ON (CO.COLLEGE_NO = CM.COLLEGE_ID)", "CO.ID, CO.CODE, CO.NAME, CO.DEPTNO, CO.STATUS, CM.COLLEGE_NAME", "", "CO.STATUS =0 AND (COLLEGE_NO = " + CollegeNo + " OR " + CollegeNo + "= 0)", "CO.ID");
            ds = objCommon.FillDropDown("ACD_MEETING_COMITEE CO LEFT JOIN ACD_COLLEGE_MASTER CM ON (CO.COLLEGE_NO = CM.COLLEGE_ID) INNER JOIN PAYROLL_SUBDEPT D ON (CO.DEPTNO=D.SUBDEPTNO)", "CO.ID, CO.CODE, CO.NAME, CO.DEPTNO,D.SUBDEPT, CO.STATUS, ISNULL(CM.COLLEGE_NAME,'') AS COLLEGE_NAME", "", "CO.[STATUS] =0 ", "CO.ID DESC");//AND CO.DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]) + "
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvComit.DataSource = ds;
                lvComit.DataBind();
                lvComit.Visible = true;
            }
            else
            {
                lvComit.DataSource = null;
                lvComit.DataBind();
                lvComit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

  
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        ViewState["action"] = Convert.ToString("add").Trim();
        ptype = 1;
        btnSubmit.Visible = true;
    }

    private bool checkCommitteeExist()
    {

        bool retVal = false;
        DataSet ds = objCommon.FillDropDown("ACD_MEETING_COMITEE", "ID", "NAME", "NAME='" + txtname.Text + "'", "ID");
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                retVal = true;
            }

        }

        return retVal;

    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objMM.CODE = Convert.ToString(txtcode.Text);
            objMM.NAME = Convert.ToString(txtname.Text);
            objMM.UA_Userno = Convert.ToInt32(Session["userno"].ToString());

           // objMM.DEPTNO = Convert.ToInt32(Session["UA_EmpDeptNo"]);
            objMM.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (txtcode.Text == string.Empty || txtname.Text == string.Empty)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Enter Data.", this.Page);
                return;
            }
            
            if (ViewState["action"] != null)
            {
                //if (checkCommitteeExist())
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Committee Already Exist.');", true);
                //    Clear();

                //}
                //else
                //{

                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        DataSet ds = objCommon.FillDropDown("ACD_MEETING_COMITEE", "*", "", "STATUS = 0 AND (NAME ='" + objMM.NAME + "' OR CODE='" + objMM.CODE + "')", "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            txtname.Text = string.Empty;
                            txtcode.Text = string.Empty;
                            ddlDepartment.SelectedValue = "0";
                        }
                        else
                        {
                            objMM.ID = 0;
                            ptype = 1;
                            CustomStatus cs = (CustomStatus)objMC.AddUpdate_Meeting_Comittee_Details(objMM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                                // pnlComitInfo.Visible = true;
                                Clear();
                                BindlistView();
                            }
                        }
                    }
                    else
                    {
                        objMM.ID = Convert.ToInt32(ViewState["CommitteeId"]);
                        ptype = 2;
                        DataSet ds = objCommon.FillDropDown("ACD_MEETING_COMITEE", "*", "", "(NAME ='" + objMM.NAME + "' OR CODE='" + objMM.CODE + "') AND ID!=" + Convert.ToInt32(ViewState["CommitteeId"]), "");
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            txtname.Text = string.Empty;
                            txtcode.Text = string.Empty;
                        }
                        else
                        {
                            CustomStatus cs = (CustomStatus)objMC.AddUpdate_Meeting_Comittee_Details(objMM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                                // pnlComitInfo.Visible = true;
                                Clear();
                                BindlistView();
                            }
                        }
                    }
                //}
                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CommitteeId = int.Parse(btnEdit.CommandArgument);
            ViewState["CommitteeId"] = int.Parse(btnEdit.CommandArgument);
            ptype = 2;
            ViewState["action"] = "edit";
            ShowDetails(CommitteeId);
            btnSubmit.Visible = true;

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int EDID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_MEETING_COMITEE", "*", "", "ID=" + Convert.ToInt32(ViewState["CommitteeId"]) + "", "ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                //  rdbCommitteeType.SelectedValue = ds.Tables[0].Rows[0]["COMMITTEE_TYPE"].ToString();

                //if (rdbCommitteeType.SelectedValue == "U")
                //  {
                //    rdbCommitteeType.SelectedValue = ds.Tables[0].Rows[0]["COMMITTEE_TYPE"].ToString(); ;
                //    trCollegeName.Visible = false;
                //  }
                //else
                //   {
                //     rdbCommitteeType.SelectedValue = ds.Tables[0].Rows[0]["COMMITTEE_TYPE"].ToString();
                //     trCollegeName.Visible = true;
                //   }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    // This method is used to clear the controls.
    private void Clear()
    {
        txtcode.Text = string.Empty;
        txtname.Text = string.Empty;
        ViewState["CommitteeId"] = null;
        ViewState["action"] = "add";
       // ddlCollege.SelectedIndex = 0;
        // rdbCommitteeType.SelectedValue = "U";
        ddlCollege.SelectedValue = "0";
        trCollegeName.Visible = false;
        ddlDepartment.SelectedValue = "0";
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int committeeID = int.Parse(btnDelete.CommandArgument);
            ViewState["COMMITTEE_ID"] = int.Parse(btnDelete.CommandArgument);

            DataSet ds = objCommon.FillDropDown("ACD_MEETING_RELETIONMASTER", "*", "", "FK_COMMITEE=" + committeeID, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Committee can not delete, it is in use.');", true);
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objMC.DeleteMeetingCommittee(committeeID);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    Clear();
                    BindlistView();
                    //Modified by Saahil Trivedi 27/01/2022
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Record Deleted Successfully');", true);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            ShowCommitteeListReport("pdf", "CommitteeList.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Meeting_Management_Master_CommitteMaster.btnCLReport_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowCommitteeListReport(string exporttype, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("MEETING_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=CommitteeList" + ".pdf";
            url += "&path=~,Reports,MEETING_MANAGEMENT," + rptFileName;
            // url += "&param=@p_college_code=" + Session["colcode"].ToString();
            url += "&param=@P_DEPTNO=" + Convert.ToInt32(Session["UA_EmpDeptNo"]);

            // To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.ShowCommitteeListReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void rdbCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (rdbCommitteeType.SelectedValue == "U")
            //{
            //    trCollegeName.Visible = false;
            //}
            //else
            //{
            //    trCollegeName.Visible = true;
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MEETING_MGT_Master_CommitteMaster.rdbCommitteeType_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}




   
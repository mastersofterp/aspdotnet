//=================================================================================
// PROJECT NAME  : YCCE                                                       
// MODULE NAME   : Academic
//Page Name      : Course Cancellation                                     
// CREATION DATE :                                                     
// CREATED BY    :                                         
// MODIFIED BY   :                                                     
// MODIFIED DESC :        
//=================================================================================


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

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;



public partial class ACADEMIC_CancelCourse : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Student_Acd objStudent = new Student_Acd();
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                PopulateDropDownList();
            }
          

        }
        
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=addExemCourse.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=addExemCourse.aspx");
        }
    }


    // Populate DropDown List  
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "Top(3)SESSIONNO", "SESSION_NAME", "SESSIONNO >0 ", "SESSIONNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCase.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // Show The Couse Registered Details in Listview
    private void BindListView()
    {
        try
        {
            DataSet ds = objSC.GetCancelCourseData(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlsemester.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvinfo.DataSource = ds;
                lvinfo.DataBind();
            }
            else
            {
                lvinfo.DataSource = null;
                lvinfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCase.BindListView()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
  
    protected void btnShow_Click(object sender, EventArgs e)
    {
        
        try
        {
            int idno;
            idno = objSC.GetStudentIDByRegNo(txtStudent.Text.Trim());
            if (idno == 0)
            {
                lblDegreeName.Text = string.Empty;
                lblMsg.Text = "Student Not Found Please Check Registration Number!!";
                lblSemester.Text = string.Empty;
                lblScheme.Text = string.Empty;
                lblsem.Visible = false;
                ddlsemester.Visible = false;
                lvinfo.DataSource = null;
                lvinfo.DataBind();
                Details.Visible = false;
                return;
            }
            else
            {
                lvinfo.DataSource = null;
                lvinfo.DataBind();
                ViewState["idno"] = idno;
                Details.Visible = true;
                lblsem.Visible = true;
                ddlsemester.Visible = true;
                DataSet dsStudent = objSC.GetStudentDetailsExam(idno);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        pnlSearch.Visible = true;
                        lblStudent.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblStudent.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblroll.Text = dsStudent.Tables[0].Rows[0]["ROLLNO"].ToString();
                        lblDegreeName.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() ;
                        lblDegreeName.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                        lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                        lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                        lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        objCommon.FillDropDownList(ddlsemester, "ACD_STUDENT_RESULT", "DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "SESSIONNO=" + ddlSession.SelectedValue + " AND IDNO=" + idno, "SEMESTERNO");
                     

                    }
                }
                else
                {
                    Details.Visible = false;
                    lvinfo.DataSource = null;
                    lvinfo.DataBind();
                    objCommon.DisplayMessage("No Record Found For session : " + ddlSession.SelectedItem.Text + " having Reg no. " + txtStudent.Text, this.Page);
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CopyCase.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int COUNT = 0;
        objStudent.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
        objStudent.IdNo = Convert.ToInt32(ViewState["idno"].ToString());
        objStudent.RollNo = lblroll.Text;
        objStudent.SemesterNo = Convert.ToInt32(ddlsemester.SelectedValue);
        objStudent.SchemeNo = Convert.ToInt32(lblScheme.ToolTip.Trim());
        objStudent.Reason = txtReason.Text.Trim();
        objStudent.UA_No = Convert.ToInt32(Session["userno"].ToString());
        objStudent.Ipaddress = Request.ServerVariables["REMOTE_HOST"];
        foreach (ListViewDataItem item in lvinfo.Items)
        {
            CheckBox check = item.FindControl("chkaccept") as CheckBox;
            if (check.Checked == true)
            {
                COUNT++;
            }
        }

        if (COUNT > 0)
        {

            foreach (ListViewDataItem item in lvinfo.Items)
            {
                CheckBox check = item.FindControl("chkaccept") as CheckBox;
                if (check.Checked == true)
                {
                    objStudent.Coursenos += ((item.FindControl("lblCCode")) as Label).ToolTip + "$";
                }
            }



            CustomStatus cs = (CustomStatus)objSC.UpdateCancelCourse(objStudent);
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Course Cancel Successfully", this.Page);
                BindListView();
                Clearall();
            }

        }
        else
        {
            objCommon.DisplayMessage("Please Select Atleast One Course From a List !!", this.Page);
        }
    }

    //Clear All Details
    private void Clearall()
    {
       
        //txtStudent.Text = string.Empty;
        //txtStudent.Enabled = true;
        //lblDegreeName.Text = string.Empty;
        //lblBranch.Text = string.Empty;
        //lblStudent.Text = string.Empty;
        //lblSemester.Text = string.Empty;
        //lblScheme.Text = string.Empty;
        txtReason.Text = string.Empty;
        //lvinfo.DataSource = null;
        //lvinfo.DataBind();
        //ddlsemester.SelectedValue = "0";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlsemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        BindListView();
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtStudent.Text =string.Empty;
        Details.Visible = false;
        lblsem.Visible = false;
        ddlsemester.Visible = false;
        lvinfo.DataSource = null;
        lvinfo.DataBind();
    }
}
    
//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : GET_PASS OUT STUDENT LIST. 
// CREATION DATE : 26-OCTOBER-2021
// CREATED BY    : DILEEP KARE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class ACADEMIC_Passed_Out_StudentList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon =new UAIMS_Common();
    StudentController objStuController = new StudentController();

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
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.CheckPageAuthorization();
                }
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_DOCUMENT_SUBMISSION.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Passed_Out_StudentList.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Passed_Out_StudentList.aspx");
        }
    }

    protected void btnGetPassStudents_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }

    private void BindListView()
    {
        DataSet ds = null;
        ds = objStuController.Get_Passout_Student_List();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lvStudentList.DataSource = ds;
            lvStudentList.DataBind();
            pnlStudentList.Visible = true;
            btnSubmit.Visible = true;
            hdnCount.Value = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            objCommon.DisplayMessage(this.updPassedOut, "No data found.", this.Page);
            lvStudentList.DataSource = null;
            lvStudentList.DataBind();
            pnlStudentList.Visible = false;
            btnSubmit.Visible = false;
        }
    }

    protected void btnDisplayStudExcel_Click(object sender, EventArgs e)
    {
        DataSet ds = null;
        GridView GV = new GridView();
        ds = objStuController.Get_Passout_Student_List_Excel();
         if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
         {
             GV.DataSource = ds;
             GV.DataBind();
             string Attachment = "Attachment;filename=Pass_Out_Student_List.xls";
             Response.ClearContent();
             Response.AddHeader("content-disposition", Attachment);
             Response.ContentType = "application/md-excel";
             StringWriter sw = new StringWriter();
             HtmlTextWriter htw = new HtmlTextWriter(sw);
             GV.HeaderStyle.Font.Bold = true;
             GV.RenderControl(htw);
             Response.Write(sw);
             Response.End();
         }
         else
         {
             objCommon.DisplayMessage(this.updPassedOut, "No data found.", this.Page);
         }
    }

    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("IDNO", typeof(int));
        dt.Columns.Add("COLLEGE_ID", typeof(int));
        dt.Columns.Add("DEGREENO", typeof(int));
        dt.Columns.Add("BRANCHNO", typeof(int));
        dt.Columns.Add("PASS_SESSION", typeof(int));
        return dt;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = GetDataTable();
        CheckBox ctrHeader = (CheckBox)lvStudentList.FindControl("chkHead");

        foreach (ListViewDataItem item in lvStudentList.Items)
        {
            Label lblIdno = item.FindControl("lblIDNO") as Label;
            Label lblDegree = item.FindControl("lblDegree") as Label;
            Label lblBranch = item.FindControl("lblBranch") as Label;
            Label lblSession = item.FindControl("lblSession") as Label;
            CheckBox chk = item.FindControl("chkstud") as CheckBox;
            if(chk.Checked)
            {
                dt.Rows.Add(Convert.ToInt32(lblIdno.Text), Convert.ToInt32(lblDegree.ToolTip), Convert.ToInt32(lblDegree.Text), Convert.ToInt32(lblBranch.Text), Convert.ToInt32(lblSession.Text));
            }
        }
        CustomStatus cs=(CustomStatus) objStuController.insert_pass_out_student_list(dt);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            ctrHeader.Checked = false;
            objCommon.DisplayMessage(this.updPassedOut, "Record saved successfully", this.Page);
            this.BindListView();
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
//=================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : Academic
// PAGE NAME     : FeedBack_Question.aspx
// CREATION DATE : 27-03-2015
// CREATED BY    : Mr.Manish Walde
// MODIFIED BY   : Neha Baranwal
// MODIFIED DESC : 
//=================================================================================
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
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Generic;

public partial class ACADEMIC_Exit_feedback_header_master : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSBC = new StudentFeedBackController();
    StudentFeedBack SFB = new StudentFeedBack();

    #region Page Load
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
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Fill DropDownList
                SQLHelper objsql = new IITMS.SQLServer.SQLDAL.SQLHelper(_nitprm_constr);
                DataSet ds = objsql.ExecuteDataSet("SELECT SEMESTERNO,SEMESTERNAME from ACD_SEMESTER where SEMESTERNO > 0 order by SEMESTERNO");

               // objCommon.FillDropDownList(ddlExitsem, "ACD_FEEDBACK_QUESTION S INNER JOIN ACD_SEMESTER SS ON(SS.SEMESTERNO = S.SEMESTERNO)", "DISTINCT S.SEMESTERNO", "SS.SEMESTERNAME", "", "S.SEMESTERNO"); //added by nehal on 22062023

                FillExitQuestionHeader();
                ViewState["action"] = "add";
            }
        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Feedback_Question.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Feedback_Question.aspx");
        }
    }
    #endregion

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


    protected void btnExitSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    //Add Batch
                    int headerid = Convert.ToInt32(ViewState["headerid"].ToString());
                    CustomStatus cs = (CustomStatus)objSBC.UpdateStudentFeedBackExitHeader(headerid, txtFeedbackHeader.Text); 
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        txtFeedbackHeader.Text = string.Empty;
                        ViewState["action"] = "add";
                        btnExitSubmit.Text = "Submit";
                        btnExitSubmit.CssClass = "btn btn-primary";
                        txtFeedbackHeader.Focus();
                        objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Error Adding Feedback Header!", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objSBC.InsertStudentFeedBackExitHeader(txtFeedbackHeader.Text);

                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.Page, "Record already exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        txtFeedbackHeader.Text = string.Empty;
                        objCommon.DisplayMessage(this.Page, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Error Adding Feedback Header!", this.Page);
                    }

                }
                FillExitQuestionHeader();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exit_feedback_header_master.btnExitSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        //CustomStatus cs = (CustomStatus)objSBC.UpdateStudentFeedBackExitHeader(txtFeedbackHeader.Text);
        //if (cs.Equals(CustomStatus.RecordSaved))
        //{
        //    FillExitQuestionHeader();
        //    txtFeedbackHeader.Text = string.Empty;
        //    objCommon.DisplayMessage(this.Page, "Feedback Header Added Successfully.", this.Page);
        //    return;
        //}
        //else
        //{
        //    //objCommon.DisplayMessage(this.Page, "Record Already Exist ", this.Page);
        //    //return;
        //}
    }

    private void FillExitQuestionHeader()
    {
        DataSet ds = objSBC.GetAllExitFeedBackQuestionForHeader();

        if (ds != null)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvExitFeedback.DataSource = null;
                lvExitFeedback.DataBind();
                lvExitFeedback.Items.Clear();
                lvExitFeedback.Visible = true;
                lvExitFeedback.DataSource = ds;
                lvExitFeedback.DataBind();
            }
            else
            {
                lvExitFeedback.Visible = false;
                lvExitFeedback.DataSource = null;
                lvExitFeedback.DataBind();
            }
            ds.Dispose();
        }
        else
        {
            lvExitFeedback.Visible = false;
            lvExitFeedback.DataSource = null;
            lvExitFeedback.DataBind();
        }
    }

    protected void btnExitCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    //protected void ddlExitsem_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    objCommon.FillListBox(ddlExitQues, "ACD_FEEDBACK_QUESTION FQ INNER JOIN  ACD_FEEDBACK_MASTER FM ON FM.FEEDBACK_NO=FQ.CTID", "DISTINCT QUESTIONID", "QUESTIONNAME", "MODE_ID = 3 AND SEMESTERNO =" + Convert.ToInt32(ddlExitsem.SelectedValue), "QUESTIONID"); //added by nehal on 21062023
    //}
    private void ShowDetail(int headerid)
    {
        SqlDataReader dr = objSBC.GetHeaderMasterNo(headerid);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["headerid"] = headerid.ToString();
                txtFeedbackHeader.Text = dr["QUESTION_HEADER"] == null ? string.Empty : dr["QUESTION_HEADER"].ToString();
            }
        }
        if (dr != null) dr.Close();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int headerid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(headerid);
            ViewState["action"] = "edit";

            btnExitSubmit.Text = "Update";
            btnExitSubmit.CssClass = "btn btn-primary";
            txtFeedbackHeader.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Exit_feedback_header_master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
}

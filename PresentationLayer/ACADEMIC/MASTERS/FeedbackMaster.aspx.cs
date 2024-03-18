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
using System.Collections.Generic;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLayer.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;

public partial class ACADEMIC_MASTERS_FeedbackMaster : System.Web.UI.Page
{
    #region Page Events
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentFeedBackController objSFC = new StudentFeedBackController();
    static int feedbackNo;

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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }

            }
            objCommon.FillDropDownList(ddlfeedbackmode, "ACD_FEEDBACK_MODE", "MODE_ID", "FEEDBACK_MODE_NAME", "", "");
            BindListView();
            ViewState["action"] = "add";
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  
            //string FilePath = @"D:\Abhinay Data\Currently Working\SVN_SVCE\PresentationLayer\DATA_TABLE\jss\DataTableButtonSetting.js";
            //string text = System.IO.File.ReadAllText(FilePath);
            //ScriptManager.RegisterStartupScript(this, GetType(), "script", text, true);
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=FeedbackMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FeedbackMaster.aspx");
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Coursetype = 0;
            int Choisefor = 0;
            if (rdoTheory.Checked == true || rdoPractical.Checked == true)
            {
                Coursetype = (rdoTheory.Checked ? 1 : 2);
            }
            else
            {
                Coursetype = 0;
            }
            if (rdoStudent.Checked == true || rdoFaculty.Checked == true)
            {
                Choisefor = (rdoStudent.Checked ? 1 : 2);
            }
            else
            {
                Choisefor = 0;
            }
            int Status = 0;
            if (chkActiveStatus.Checked == true)
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }
            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add Batch
                    CustomStatus cs = (CustomStatus)objSFC.AddFeedbackMaster(txtFeedbackName.Text, Session["colcode"].ToString(), Convert.ToInt32(ddlfeedbackmode.SelectedValue), Coursetype, Choisefor, Status, ftbDesc.Text.Trim());
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updGrade, "Record already exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(updGrade, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGrade, "Error Adding Grade Type!", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objSFC.UpdateFeedbackMaster(feedbackNo, txtFeedbackName.Text, Session["colcode"].ToString(), Convert.ToInt32(ddlfeedbackmode.SelectedValue), Coursetype, Choisefor, Status, ftbDesc.Text.Trim());

                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(updGrade, "Record already exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        Clear();
                        ViewState["action"] = null;
                        btnSave.Text = "Submit";
                        btnSave.CssClass = "btn btn-primary";
                        txtFeedbackName.Focus();
                        objCommon.DisplayMessage(this.updGrade, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updGrade, "Error Adding Grade Type!", this.Page);
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Clear()
    {
        btnNote.Text = "Add Note";
        ftbDesc.Text = string.Empty;
        txtFeedbackName.Text = string.Empty;
        Label1.Text = string.Empty;
        ddlfeedbackmode.SelectedIndex = 0;
        rdoFaculty.Checked = false;
        rdoStudent.Checked = false;
        rdoPractical.Checked = false;
        rdoTheory.Checked = false;
        rdoNone1.Checked = true;
        rdoNone2.Checked = true;
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objSFC.GetAllFeedback();
            lvFeedback.DataSource = ds;
            lvFeedback.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_FeedbackMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        ViewState["action"] = null;
        btnSave.Text = "Submit";
        btnSave.CssClass = "btn btn-primary";
        txtFeedbackName.Focus();
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowDetail(int feedbackNo)
    {
        SqlDataReader dr = objSFC.GetFeedbackNo(feedbackNo);

        if (dr != null)
        {
            if (dr.Read())
            {
                ViewState["feedbackno"] = feedbackNo.ToString();
                txtFeedbackName.Text = dr["FEEDBACK_NAME"] == null ? string.Empty : dr["FEEDBACK_NAME"].ToString();
                ftbDesc.Text = dr["FEEDBACK_NOTE"] == null ? string.Empty : dr["FEEDBACK_NOTE"].ToString();
                //objCommon.FillDropDownList(ddlfeedbackmode, "ACD_FEEDBACK_MODE", "MODE_ID", "FEEDBACK_MODE_NAME", "", "");
                ddlfeedbackmode.SelectedValue = dr["MODE_ID"].ToString() == string.Empty ? "0" : dr["MODE_ID"].ToString();
                if (dr["COURSE_TYPE"].ToString().Equals("1"))
                {
                    rdoTheory.Checked = true;
                    rdoPractical.Checked = false;
                    rdoNone1.Checked = false;
                }
                else if (dr["COURSE_TYPE"].ToString().Equals("2"))
                {
                    rdoTheory.Checked = false;
                    rdoPractical.Checked = true;
                    rdoNone1.Checked = false;
                }
                else
                {
                    rdoTheory.Checked = false;
                    rdoPractical.Checked = false;
                    rdoNone1.Checked = true;
                }
                if (dr["CHOISE_FOR"].ToString().Equals("1"))
                {
                    rdoStudent.Checked = true;
                    rdoFaculty.Checked = false;
                    rdoNone2.Checked = false;
                }
                else if (dr["CHOISE_FOR"].ToString().Equals("2"))
                {
                    rdoStudent.Checked = false;
                    rdoFaculty.Checked = true;
                    rdoNone2.Checked = false;
                }
                else
                {
                    rdoStudent.Checked = false;
                    rdoFaculty.Checked = false;
                    rdoNone2.Checked = true;
                }

                if (dr["IS_ACTIVE"].ToString().Equals("1"))
                {
                    chkActiveStatus.Checked = true;
                }
                else
                {
                    chkActiveStatus.Checked = false;
                }
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        //string MyScript = "$(document).ready(function () { var table = $('#example').DataTable();table.button(0).disable();});";
        //ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey",MyScript, true);
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            feedbackNo = int.Parse(btnEdit.CommandArgument);
            Label1.Text = string.Empty;

            ShowDetail(feedbackNo);
            ViewState["action"] = "edit";

            btnSave.Text = "Update";
            btnSave.CssClass = "btn btn-primary";
            txtFeedbackName.Focus();
            btnNote.Text = "View Note";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_FeedbackMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnNote_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        /// Note.Visible = true;
    }
}


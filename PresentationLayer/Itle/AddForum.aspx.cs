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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

public partial class Itle_AddForum : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

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

                if (Session["ICourseNo"] == null)
                    //Response.Redirect("~/Itle/selectCourse.aspx");
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");

                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();

                //Page Authorization
                CheckPageAuthorization();

                BindListView();
            }
        }
    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AddForum.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AddForum.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            IForumMasterController objAM = new IForumMasterController();
            DataSet ds = objAM.GetAllForumByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));

            lvAssignment.DataSource = ds;
            lvAssignment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AddForum.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int forum_no)
    {
        try
        {
            IForumMasterController objFM = new IForumMasterController();
            ViewState["forum_no"] = forum_no;
            DataTableReader dtr = objFM.GetSingleForum(Convert.ToInt32(ViewState["forum_no"]));
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtForum.Text = dtr["FORUM"] == null ? "" : dtr["FORUM"].ToString();
                    txtDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AddForum.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AddForum.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    protected void ClearControls()
    {
        txtForum.Text = string.Empty;
        txtDescription.Text = "&nbsp;";
        ViewState["action"] = null;
        ViewState["forum_no"] = null;
    }

    protected void ClearThreadControls()
    {

        ViewState["action"] = null;
        ViewState["thread_no"] = null;
    }

    #endregion

    #region Page events

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        try
        {
            IForumMasterController objAM = new IForumMasterController();
            IForumMaster objAssign = new IForumMaster();
            objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objAssign.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAssign.FORUM = txtForum.Text.Trim();
            objAssign.DESCRIPTION = txtDescription.Text.Trim();
            objAssign.CREATEDDATE = DateTime.Now;

            #region Commented by Saket Singh
            //if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            //{
            //    if (ViewState["forum_no"] != null)
            //    {
            //        objAssign.FORUM_NO = Convert.ToInt32(ViewState["forum_no"]);
            //        CustomStatus cs = (CustomStatus)objAM.UpdateForum(objAssign);
            //        if (cs.Equals(CustomStatus.RecordUpdated))
            //            objCommon.DisplayMessage(UpdForum, "Forum Updated successfully", this.Page);
            //        else if (cs.Equals(CustomStatus.DuplicateRecord))
            //            objCommon.DisplayMessage(UpdForum, "Forum Already Exist", this.Page);
            //            //lblStatus.Text = "Record Modified";
            //        else
            //        { 
            //        }
            //    }
            //    BindListView();
            //}
            //else
            //{  //Add Assignment
            //    CustomStatus cs = (CustomStatus)objAM.AddForum(objAssign);
            //    if (cs.Equals(CustomStatus.RecordSaved))
            //        objCommon.DisplayMessage(UpdForum, "Forum Created successfully", this.Page);
            //       else if(cs.Equals(CustomStatus.DuplicateRecord))
            //        objCommon.DisplayMessage(UpdForum, "Forum Already Exist", this.Page);
            //    else
            //        if (cs.Equals(CustomStatus.FileExists))
            //            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
            //    BindListView();
            //}
            //ClearControls();
            #endregion

            //Added by Saket Singh on 06-09-2017
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objAssign.FORUM_NO = Convert.ToInt32(ViewState["forum_no"]);

                CustomStatus cs = (CustomStatus)objAM.UpdateForum(objAssign);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(UpdForum, "Forum Updated successfully", this.Page);
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(UpdForum, "Forum Already Exist", this.Page);
                }
                else
                {
                    if (cs.Equals(CustomStatus.FileExists))
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                }
                ClearControls();
                BindListView();
            }
            else
            {

                bool result = CheckPurpose();
                if (result == true)
                {
                    //objCommon.DisplayMessage("Record Already Exist", this);
                    MessageBox("Record Already Exist");
                    return;
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objAM.AddForum(objAssign);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(UpdForum, "Forum Created successfully", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(UpdForum, "Forum Already Exist", this.Page);
                    }
                    else
                    {
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
                    ClearControls();
                    BindListView();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AddForum.btnAdd_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

        ClearControls();
        lblStatus.Text = string.Empty;
        ViewState["forum_no"] = null;
        pnlForumDetail.Visible = true;
        pnlForumGrid.Visible = true;
        //pnlThreadDetail.Visible = false;
        //pnlThreadGrid.Visible = false;  
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int forum_no = int.Parse(btnEdit.CommandArgument);
            ShowDetail(forum_no);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDel = sender as ImageButton;
            int forum_no = int.Parse(btnDel.CommandArgument);

            IForumMasterController objFMC = new IForumMasterController();

            if (Convert.ToInt16(objFMC.DeleteForum(forum_no)) == Convert.ToInt16(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(UpdForum, "Forum Deleted Sucessfully", this.Page);
                //lblStatus.Text = "Forum Deleted Successfully...";
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AddForum.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnlnkSelect_Click(object sender, EventArgs e)
    {
        LinkButton btnSelect = sender as LinkButton;
        //int.Parse(btnSelect.CommandArgument);
        ViewState["forum_no"] = int.Parse(btnSelect.CommandArgument); ;  /// if (ViewState["forum_no"] != null) 
        //lblThreadFor.Text = "Threads For Forum : " + btnSelect.Text;
        //lblAvailableForums.Visible = false;
        //BindThreadListView(Convert.ToInt32(ViewState["forum_no"]));
        pnlForumGrid.Visible = false;
        pnlForumDetail.Visible = false;
        //pnlThreadGrid.Visible = true;
        //pnlThreadDetail.Visible = true;
        lblStatus.Text = string.Empty;
        Response.Redirect("StudForum.aspx?pageno=1456");
    }

    protected void tbnBackForum_Click(object sender, EventArgs e)
    {
        ViewState["forum_no"] = null;
        lblStatus.Text = string.Empty;
        pnlForumDetail.Visible = true;
        pnlForumGrid.Visible = true;

    }
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("ACD_IFORUMMASTER", "*", "", "FORUM='" + txtForum.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnViewForum_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Forum_Report", "Itle_Forum_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AddForum.btnViewForum_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    #endregion
}

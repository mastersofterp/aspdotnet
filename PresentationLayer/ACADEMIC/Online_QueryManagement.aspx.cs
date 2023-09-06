using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class ACADEMIC_Online_QueryManagement : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FaqController objFAQ = new FaqController();
    FetchDataController objfech = new FetchDataController();
   
    #region Page Events
    
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
                //objCommon.FillDropDownList(ddlFormCategory, "ACD_QUERY_CATEGORY", "DISTINCT CATEGORYNO", "QUERY_CATEGORY_NAME", "CATEGORYNO>0 AND ACTIVE_STATUS=1", "CATEGORYNO");    //Commented by Nikhil Lambe on 20-03-2023 as there is no active status column on live link which is developed by Kajal(Yograj Sir Team).
                objCommon.FillDropDownList(ddlFormCategory, "ACD_QUERY_CATEGORY", "DISTINCT CATEGORYNO", "QUERY_CATEGORY_NAME", "CATEGORYNO>0", "CATEGORYNO");

                //BindListView();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
        }
    }

    #endregion

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Online_QueryManagement.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Online_QueryManagement.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            int categoryno = Convert.ToInt32(ddlFormCategory.SelectedValue);
            DataSet ds = objFAQ.GetAllStudent_Feadback(categoryno);
            if (ds != null)
            {
                lvStudentQuery.DataSource = ds;
                lvStudentQuery.DataBind();
            }
            else
            {
                lvStudentQuery.DataSource = null;
                lvStudentQuery.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_ProgramType_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lvStudentQuery_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;

            if (dr["FEEDBACK_STATUS"].Equals("CLOSED"))
            {

                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;

            }
            else if (dr["FEEDBACK_STATUS"].Equals("OPEN"))
            {
                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;
            }
        }
    }

    protected void lvStudentQuery_PagePropertiesChanged(object sender, EventArgs e)
    {
        DataPager dp = (DataPager)lvStudentQuery.FindControl("DataPager1");       
        BindListView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int status = 0;
        int Catno = 0;
        int userno = 0;
        //string FeedbackQuery = string.Empty;
        string query_reply = string.Empty;
        int replied_user = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
        try
        {

            Catno = Convert.ToInt32(ViewState["CategoryNo"]);
            userno = Convert.ToInt32(ViewState["userno"]);
            query_reply = txtFeedback.Text;
            int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "AND ACTIVE_STATUS=1 AND QUERY_CATEGORY=" + Catno));
            int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS ", "QUERY_CATEGORY", "USERNO=" + userno + "AND ACTIVE_STATUS=1  AND QUERYNO=" + Queryno));
            string UserID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            int QUERY_DET_ID = objFAQ.GET_QUERY_DET_ID(Queryno, userno);
            status = Convert.ToInt32(ddlStatus.SelectedValue);
            int result = objFAQ.Add_Feedback_Reply(userno,query_reply,categoryno, status, Queryno, replied_user,QUERY_DET_ID,UserID);
            if (result != -99)
            {
                lblStatus1.Visible = true;
                lblStatus1.ForeColor = System.Drawing.Color.Green;
                lblStatus1.Text = "Query Submitted Successfully!";
                txtFeedback.Text = "";
                //ddlFormCategory.SelectedIndex = 0;
                BindConversation();

            }
            BindListView();

            if (ddlStatus.SelectedValue == "2")
            {
                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Online_QueryManagement.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindConversation()
    {
        int userno = 0;
        userno = Convert.ToInt32(ViewState["userno"]);
        //Commented by Nikhil L. on 02/11/2022 due to inline query.
        //DataSet user_ds = objCommon.FillDropDown("ACD_USER_FEEDBACK", "(CASE WHEN FEEDBACK_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em>Applicant:</em> ',FEEDBACK_DETAILS), '<em><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_DETAILS", "(CASE WHEN FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em>CPUK Reply('+ REPLIED_USER +'):</em> ',FEEDBACK_REPLY), '<em><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_REPLY", "USERNO=" + userno + " AND QUERYNO=" + Convert.ToInt32(ViewState["QueryNo"]) + "", "SRNO");
       // DataSet user_ds = objfech.GetReplyForQueryManagement(userno,Convert.ToInt32(ViewState["QueryNo"]));
        DataSet user_ds = objfech.GetReplyForQueryManagement(userno, Convert.ToInt32(ViewState["CategoryNo"]));
        lvFeedbackReply.DataSource = user_ds;
        lvFeedbackReply.DataBind();
    }

    protected void ddlFormCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        //Button btnunloc = (Button)this.lvFeedbacklist.FindControl("btnUnlock");
        int userno = Convert.ToInt32(((System.Web.UI.WebControls.Button)(sender)).CommandArgument);
        //Label lblUsername = this.FindControl("lvFeedbacklist").FindControl("lblUser") as Label;
        string AppId = ((System.Web.UI.WebControls.Button)(sender)).ToolTip.ToString();

        string ipAddress = Request.ServerVariables["REMOTE_HOST"];
        int unlocked_by = Convert.ToInt32(Session["userno"]);

        CustomStatus cs = (CustomStatus)objfech.UnlockConfirmStatus(userno, unlocked_by, DateTime.Now, ipAddress);
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayUserMessage(updatepanel1, "Unlocked Successfully!", this.Page);
        }
        else
        {
        }
        BindListView();
    }

    protected void rdbfeedback_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ViewDetails1")
        {

            ViewState["CategoryNo"] = null;
            ViewState["userno"] = null;
            ImageButton btnImgB = sender as ImageButton;

            int CategoryNo = Int32.Parse(e.CommandArgument.ToString());
            int userno = Int32.Parse(btnImgB.AlternateText);
            int queryno = Convert.ToInt32(btnImgB.ToolTip);
            ViewState["QueryNo"] = Convert.ToInt32(queryno);
            ViewState["CategoryNo"] = Convert.ToInt32(CategoryNo);
            ViewState["userno"] = Convert.ToInt32(userno);
            int Active_Status = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "ACTIVE_STATUS", "USERNO=" + userno + " AND QUERYNO=" + queryno + " AND QUERY_CATEGORY=" + CategoryNo));
            if (Active_Status == 2)
            {
                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit.Visible = false;
            }
            else
            {
                ddlStatus.Visible = true;
                txtFeedback.Visible = true;
                btnSubmit.Visible = true;
            }
            ddlStatus.SelectedIndex = 0;
            BindConversation();

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal1", "showPopup();", true);

    }
   
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        //int userno = Convert.ToInt32(((System.Web.UI.WebControls.Button)(sender)).CommandArgument);

        //int PROGRAM_TYPE = Convert.ToInt32(objCommon.LookUp("ACD_USER_BRANCH_PREF", "ISNULL(PROGRAN_TYPE,0)PROGRAN_TYPE", "USERNO=" + userno));
        //if (PROGRAM_TYPE == 11)
        //{
        //    ShowReport("UserPreview", "rptJHRCA_StudentPreview.rpt", userno);
        //}
        //else
        //{
        //    ShowReport("UserPreview", "rptStudentPreview.rpt", userno);
        //}

        //if (e.CommandName == "ViewDetails1")
        //{

            ViewState["CategoryNo"] = null;
            ViewState["userno"] = null;
            ImageButton btnImgB = sender as ImageButton;
            Button btnPreview = sender as Button;

            int userno = Int32.Parse(btnPreview.CommandName);
            
            int Catno = Int32.Parse(btnPreview.ToolTip);
            int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "AND QUERY_CATEGORY=" + Catno));          
            int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + " AND QUERYNO=" + Queryno));
            ViewState["QueryNo"] = Convert.ToInt32(Queryno);
            ViewState["CategoryNo"] = Convert.ToInt32(categoryno);
            ViewState["userno"] = Convert.ToInt32(userno);
            int Active_Status = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "ACTIVE_STATUS", "USERNO=" + userno + " AND QUERYNO=" + Queryno + " AND QUERY_CATEGORY=" + categoryno));
            if (Active_Status == 2)
            {
                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit.Visible = false;
            }
            else
            {
                ddlStatus.Visible = true;
                txtFeedback.Visible = true;
                btnSubmit.Visible = true;
            }
            ddlStatus.SelectedIndex = 0;
            BindConversation();

        //}
        ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal1", "showPopup();", true);


    }
   
}
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
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;


public partial class ACADEMIC_EXAMINATION_ExamFeeDefination : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                    // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }

                    PopulateDropDownList();


                }
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamFeeDefination.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentIDCardReport.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {

            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");




        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamFeeDefination.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlColg.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");

        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));

            ddlSession.Items.Clear();
            ddlSession.Items.Add(new ListItem("Please Select", "0"));
            lvFeeDefination.Visible = false;
            btnSave.Visible = false;

        }
        lvFeeDefination.Visible = false;
        btnSave.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSection, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND UA_SECTION  IN(1,2,3) ", "UA_SECTION");
        }
        else
        {
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            lvFeeDefination.Visible = false;
            btnSave.Visible = false;
        }
        lvFeeDefination.Visible = false;
        btnSave.Visible = false;
    }

    private void LoadMark()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_GRADE G INNER JOIN ACD_UA_SECTION S ON (S.UA_SECTION=G.UGPGOT)", "GRADENO", "GRADE,GRADEPOINT,MAXMARK,MINMARK,UA_SECTIONNAME AS UA_SECTIONNAME,DESC_GRADE,case when isnull(G.ACTIVESTATUS,0)=0 then 'Inactive' else 'Active' end as ACTIVESTATUS,case when isnull(G.RESULT,0)=0 then 'Fail' else 'Pass' end as RESULT", "GRADENO>0", "GRADENO DESC");
            //DataSet ds = objCommon.FillDropDown("ACD_GRADE G INNER JOIN ACD_GRADE_NEW N ON (N.GRADENO=G.GRADENO_NEW)", "GRADENO", "GRADE,GRADEPOINT,MAXMARK,MINMARK,ACTIVESTATUS, RESULT  ", "GRADENO>0", "GRADENO DESC");
            DataSet ds = objCommon.FillDropDown("ACD_SUBJECT_TYPE_WISE_PASSING_RULE1 S FULL outer JOIN ACD_EXAM_FEE_DEFINATION R  ON (S.SUBID=R.SubId_New)  ", "S.SUBID", "S.SUBNAME , R.Regular, R.Backlog ", "SUBID>0", "SUBID DESC");
            if (ds != null && ds.Tables.Count > 0)
            {
                lvFeeDefination.DataSource = ds.Tables[0];
                lvFeeDefination.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamFeeDefination.LoadMark()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListView()
    {
        try
        {
            TextBox Regular = FindControl("txtRegular") as TextBox;
            TextBox BackLog = FindControl("txtBacklog") as TextBox;
            int ClgId = Convert.ToInt32(ddlColg.SelectedValue);
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);
            Label SubName = FindControl("lblSubName") as Label;
            ExamController EC = new ExamController();
            DataSet ds = EC.GetAllFeeDef(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue));

            lvFeeDefination.DataSource = ds;
            lvFeeDefination.DataBind();
            lvFeeDefination.Visible = true;
            btnSave.Visible = true;


            //foreach (ListViewDataItem lvItem in lvFeeDefination.Items)
            //{
            //    string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "Count(1)", "SESSIONNO ='" + Sessionno + "' AND UA_SECTION = '" + Sectionno + "' AND                COLLEGE_ID = '" + ClgId + "'");
            //    if ((Chk != null || Chk != string.Empty) && Chk != "0")
            //    { 
            //        lvFeeDefination.Visible = true;
            //        btnSave.Visible = false;


            //    }
            //    else
            //    {
            //        lvFeeDefination.Visible = true;
            //        btnSave.Visible = true;
            //    }
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamFeeDefination.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedIndex > 0) { BindListView(); } else { lvFeeDefination.Visible = false; btnSave.Visible = false; }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExamController EC = new ExamController();
        try
        {
            foreach (ListViewDataItem lvItem in lvFeeDefination.Items)
            {

                Label SubId = lvItem.FindControl("lblSubId") as Label;
                Label SubName = lvItem.FindControl("lblSubName") as Label;
                TextBox Regular = lvItem.FindControl("txtRegular") as TextBox;
                TextBox BackLog = lvItem.FindControl("txtBacklog") as TextBox;

                int Subid_new = Convert.ToInt32(SubId.Text.Trim());
                string Subname = SubName.Text.Trim();
                int regular = Convert.ToInt32(Regular.Text.Trim());
                int Backlog = Convert.ToInt32(BackLog.Text.Trim());
                int OrgId = Convert.ToInt32(Session["OrgId"]);
                int Subid = 0;
                int ClgId = Convert.ToInt32(ddlColg.SelectedValue);
                int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                int Sectionno = Convert.ToInt32(ddlSection.SelectedValue);

                CustomStatus cs = (CustomStatus)EC.AddFeeEntry(Subid, Subname, regular, Backlog, OrgId, ClgId, Sessionno, Sectionno, Subid_new);
                if (cs.Equals(CustomStatus.RecordSaved))
                {

                    objCommon.DisplayMessage(this.updFee, "Record Saved Successfully!", this.Page);
                    BindListView();
                    //this.LoadMark();

                }
                else
                {

                    objCommon.DisplayMessage(this.updFee, "Record Successfully saved!", this.Page);
                    BindListView();
                    //this.LoadMark();

                }


            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamFeeDefination.btnSave-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }



    }
}
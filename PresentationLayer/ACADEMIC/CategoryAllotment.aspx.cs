using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
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

public partial class ACADEMIC_CategoryAllotment : System.Web.UI.Page
{
    #region Page Events
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                PopulateDropDownList();
                btnSubmit.Enabled = false;
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
                Response.Redirect("~/notauthorized.aspx?page=CategoryAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CategoryAllotment.aspx");
        }
    }
    #endregion

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.Clear();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string categorys = string.Empty;
            string rollnos = string.Empty;

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                
                if (Convert.ToInt32((lvItem.FindControl("ddlcat") as DropDownList).SelectedValue) > 0)
                {
                    studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    categorys += (lvItem.FindControl("ddlcat") as DropDownList).SelectedValue + "$";
                }
            }
        

            if (studids.Length <= 0 && categorys.Length <= 0)
            {
                objCommon.DisplayMessage(this.updSection, "Please Select Category", this.Page);
                return;
            }

            if (objSC.UpdateStudentCategory(studids,categorys) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                this.BindListView();
                objCommon.DisplayMessage(this.updSection, "category Alloted Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.updSection, "Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CategoryAllotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Private Methods
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CategoryAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = null;

            
            //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_CATEGORY SC ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO, S.STUDNAME, S.CATEGORYNO,SC.CATEGORY,S.ROLLNO", "S.DEGREENO = " + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + "  AND ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue";
            ds = objCommon.FillDropDown(" ACD_STUDENT S INNER JOIN ACD_CATEGORY SC ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO, S.STUDNAME, S.CATEGORYNO,SC.CATEGORY,S.ROLLNO", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue,"S.ROLLNO");
           
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds;
                    lvStudents.DataBind();
                    btnSubmit.Enabled = true;
                }
                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                objCommon.DisplayMessage(this.updSection, "No Students found for selected criteria!", this.Page);
                btnSubmit.Enabled = false;
            }            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CategoryAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlSchemetype.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        btnSubmit.Enabled = false;
    }
    #endregion

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
        //    ddlBranch.Focus();
        //}
        int deptno = Convert.ToInt32(Session["userdeptno"].ToString());
        if (ddlDegree.SelectedIndex > 0)
        {
            if (deptno > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND DEPTNO = " + deptno, "BRANCHNO");
            }
            else
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            }
            ddlBranch.Focus();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
        }

        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            DropDownList ddlcat = e.Item.FindControl("ddlcat") as DropDownList;
            DataSet ds = objCommon.FillDropDown("ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORYNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTableReader dtr = ds.Tables[0].CreateDataReader();
                while (dtr.Read())
                {
                    ddlcat.Items.Add(new ListItem(dtr["CATEGORY"].ToString(), dtr["CATEGORYNO"].ToString()));
                }
            }
            ddlcat.SelectedValue = ddlcat.ToolTip;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CategoryAllotment.lvStudents_ItemDataBound-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
}

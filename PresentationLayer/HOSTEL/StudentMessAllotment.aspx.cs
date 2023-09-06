//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : MESS ALLOTMENT                                  
// CREATION DATE : 
// ADDED BY      : DIGESH PATEL                                            
// ADDED DATE    : 2-AUG-2018
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class HOSTEL_MessAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelController objhos = new HostelController();

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                PopulateDropDownList();
                trFilter.Visible = false;
                trRdo.Visible = false;
                ddlMess.Enabled = false;
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
                Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=batchallotment.aspx");
        }
    }

    #region Form Events
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            Student_Acd objStudent = new Student_Acd();
            int hostelSessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            int messno = Convert.ToInt32(ddlMess.SelectedValue);
            string StudId = "";

            foreach (ListViewDataItem lvItem in lvStudents.Items)
            {
                CheckBox chkBox = lvItem.FindControl("cbRow") as CheckBox;
                if (chkBox.Checked == true)
                    StudId += chkBox.ToolTip + ",";
            }
            if (objhos.UpdateMessofStudents(StudId, messno, hostelSessionNo) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {
                BindListView();
                objCommon.DisplayMessage(this.UpdatePanel1, "Mess Alloted Successfully!!!", this.Page);
            }
            else
                objCommon.DisplayMessage(this.UpdatePanel1, "Server Error...", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            ddlBranch.Focus();
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        }
        else
        {
            ddlDegree.SelectedIndex = 0;
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
    }
    #endregion

    #region Private Methods

    private void BindListView()
    {
        try
        {
            //Fill Student List
            hdfTot.Value = "0";
            txtTotStud.Text = "0";
            int branchNo = 0;
            string fromregno = txtFromRollNo.Text.Trim();
            string toregno = txtToRollNo.Text.Trim();
            if (fromregno.Length < 1)
            {
                fromregno = "";
            }
            if (toregno.Length < 1)
            {
                toregno = "";
            }
            if (ddlBranch.SelectedValue == "99")
                branchNo = 0;
            else
                branchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            DataSet ds = objhos.Show_allotmess(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlhostel.SelectedValue), Convert.ToInt32(ddlmessfilter.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), fromregno, toregno, 0);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                objCommon.FillDropDownList(ddlMess, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "", "MESS_NO");
                ddlMess.Enabled = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                ddlMess.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_messallotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlBranch.SelectedIndex = 0;
        ddlMess.SelectedIndex = 0;
        txtFromRollNo.Text = string.Empty;
        txtToRollNo.Text = string.Empty;
        ddlMess.SelectedIndex = 0;
        txtTotStud.Text = "0";
        hdfTot.Value = "0";
        lblStatus2.Text = string.Empty;
        ddlhostel.SelectedIndex = 0;
        ddlmessfilter.SelectedIndex = 0;
    }
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_HOSTEL_SESSION", "HOSTEL_SESSION_NO", "SESSION_NAME", "HOSTEL_SESSION_NO >0", "HOSTEL_SESSION_NO DESC");
            ddlSession.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
            objCommon.FillDropDownList(ddlhostel, "ACD_HOSTEL", "HBNO", "HOSTEL_NAME", "HBNO>0", "HBNO");
            objCommon.FillDropDownList(ddlmessfilter, "ACD_HOSTEL_MESS", "MESS_NO", "MESS_NAME", "MESS_NO>0", "MESS_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_batchallotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");

            }
        }
    }
    #endregion

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        hdfTot.Value = (Convert.ToInt16(hdfTot.Value) + 1).ToString();
    }
}

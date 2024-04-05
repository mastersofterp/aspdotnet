using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;
public partial class ACADEMIC_NEPCourseCreditDefination : System.Web.UI.Page
{
    Common objCommon = new Common();
    NEPController nepC = new NEPController();
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Action
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                PopulateDropdown();
            }
            //BindList();
        }
        catch (Exception ex)
        {
            throw;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SchemeWiseNEPMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SchemeWiseNEPMapping.aspx");
        }
    }
    #endregion

    protected void PopulateDropdown()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION WITH (NOLOCK)", "SESSIONID", "SESSION_NAME", "SESSIONID > 0", "SESSIONID DESC");
    }

    #region Dropdown Events
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        int sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        objCommon.FillDropDownList(ddlScheme, "ACD_SESSION S INNER JOIN ACD_SESSION_MASTER SM ON S.SESSIONID = SM.SESSIONID INNER JOIN ACD_NEP_SCHEME_MAPPING CSM ON CSM.COLLEGE_ID = SM.COLLEGE_ID INNER JOIN ACD_SCHEME SCH ON SCH.SCHEMENO = CSM.SCHEMENO", "DISTINCT SCH.SCHEMENO", "SCH.SCHEMENAME", "S.SESSIONID =" + sessionid, "SCHEMENO");
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lvNEPCategory.DataSource = null;
        lvNEPCategory.DataBind();
        Panel1.Visible = false;
        btnShow.Enabled = true;
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        
        lvNEPCategory.DataSource = null;
        lvNEPCategory.DataBind();
        Panel1.Visible = false;
        btnShow.Enabled = true;
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvNEPCategory.DataSource = null;
        lvNEPCategory.DataBind();
        Panel1.Visible = false;
        btnShow.Enabled = true;
    }
    #endregion

    private void BindNEPCategoryinList(int sessionid, int schemeid, int semid) 
    {
        try
        {
            DataSet ds = nepC.GetNEPCategorybyScheme(sessionid,schemeid,semid);
            if (ds != null && ds.Tables.Count > 0)
            {
                lvNEPCategory.DataSource = ds;
                lvNEPCategory.DataBind();
            }
        }
        catch 
        {
            throw;
        }
    }

    #region Button Events
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        btnShow.Enabled = false;
        this.BindNEPCategoryinList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CustomStatus cs = CustomStatus.Others;
        int sessionid = Convert.ToInt32(ddlSession.SelectedValue);
        int schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);
        string categoryno = string.Empty;
        string maxCredit = string.Empty;
        string mincredit = string.Empty;
        string noSubjcets = string.Empty;
        DateTime date = DateTime.Now;
        int Ua_no = Convert.ToInt32(Session["userno"]);
        string IpAddress = Request.ServerVariables["REMOTE_ADDR"];

        foreach (ListViewItem item in lvNEPCategory.Items) 
        {
            CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
            ListViewItem lvi = (ListViewItem)chkSelect.NamingContainer;
            HiddenField hfNEPCategory = (HiddenField)lvi.FindControl("hfNEPCategory");
            TextBox txtMaxCredit = (TextBox)lvi.FindControl("txtMaxCredit");
            TextBox txtMinCredit = (TextBox)lvi.FindControl("txtMinCredit");
            TextBox txtSubjects = (TextBox)lvi.FindControl("txtSubjects");
            if(chkSelect.Checked)
            {
                if (txtMaxCredit.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(this.updNEPMapping, "Please Enter Max Credit!", this.Page);
                    return;
                }
                if (txtMinCredit.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(this.updNEPMapping, "Please Enter Min Credit!", this.Page);
                    return;
                }
                if (txtSubjects.Text == string.Empty)
                {
                    objCommon.DisplayUserMessage(this.updNEPMapping, "Please Enter No. of Subjects!", this.Page);
                    return;
                }
                if (Convert.ToInt32(txtMaxCredit.Text) <= Convert.ToInt32(txtMinCredit.Text))
                {
                    objCommon.DisplayUserMessage(this.updNEPMapping, "Max Credit is Greater than Min Credit", this.Page);
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtMaxCredit.Text) && !string.IsNullOrEmpty(txtMinCredit.Text) && !string.IsNullOrEmpty(txtSubjects.Text))
            {
                categoryno += hfNEPCategory.Value + ",";
                maxCredit += txtMaxCredit.Text + ",";
                mincredit += txtMinCredit.Text + ",";
                noSubjcets += txtSubjects.Text + ",";
            }
        }
        
        if (categoryno.Length > 1)
        {
            categoryno = categoryno.Remove(categoryno.Length - 1);
        }
        if (maxCredit.Length > 1)
        {
            maxCredit = maxCredit.Remove(maxCredit.Length - 1);
        }
        if (mincredit.Length > 1)
        {
            mincredit = mincredit.Remove(mincredit.Length - 1);
        }
        if (noSubjcets.Length > 1)
        {
            noSubjcets = noSubjcets.Remove(noSubjcets.Length - 1);
        }

        if (categoryno != string.Empty || maxCredit != string.Empty || mincredit != string.Empty || noSubjcets != string.Empty)
        {
            cs = (CustomStatus)nepC.NEPCreditInsert(sessionid, schemeno, semesterno, categoryno, maxCredit, mincredit, noSubjcets, date, Ua_no, IpAddress);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Record Saved Successfully!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Record Updated Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Error!", this.Page);
            }
            this.BindNEPCategoryinList(sessionid,schemeno,semesterno);
        }
        else 
        {
            objCommon.DisplayUserMessage(this.updNEPMapping, "Please Enter Record to Submit!", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    #endregion

    private void Clear() 
    {
        ddlSession.SelectedIndex = 0;
        ddlScheme.Items.Clear();
        ddlSemester.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));
        lvNEPCategory.DataSource = null;
        lvNEPCategory.DataBind();
        Panel1.Visible = false;
        btnShow.Enabled = true;
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelect = (CheckBox)sender;
        ListViewItem item = (ListViewItem)chkSelect.NamingContainer;

        TextBox txtMaxCredit = (TextBox)item.FindControl("txtMaxCredit");
        TextBox txtMinCredit = (TextBox)item.FindControl("txtMinCredit");
        TextBox txtSubjects = (TextBox)item.FindControl("txtSubjects");

        if (chkSelect.Checked)
        {
            txtMaxCredit.Enabled = true;
            txtMinCredit.Enabled = true;
            txtSubjects.Enabled = true;
        }
        else
        {
            txtMaxCredit.Enabled = false;
            txtMinCredit.Enabled = false;
            txtSubjects.Enabled = false;
        }
    }

}
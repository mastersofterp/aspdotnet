using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;
using System.Linq;

public partial class exitLevel : System.Web.UI.Page
{
    Common objCommon = new Common();
    DataSet dss = new DataSet();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string sessionuid = string.Empty;
    static string msessionuid = string.Empty;
    static string IpAddress = string.Empty;
    static string OrgID = string.Empty;

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
                Response.Redirect("~/default_atlas.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //to load all dropdown list
                this.populateSemester();
                this.PopulateDropDownList();
                this.BindExitListView();
                this.BindListView();
                //this.BindListView();

                //assign session values to static variables

                OrgID = Session["OrgId"].ToString();
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));
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
                Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Minor_creation.aspx");
        }
    }

    #region First Tab "Exit Criteria"
    protected void populateSemester()
    {
        objCommon.FillDropDownList(ddlExitSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
    }

    protected void ClearExitField()
    {
        txtCriteriaName.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlLevels.SelectedIndex = 0;
        txtCredits.Text = string.Empty;
        ddlExitSem.SelectedIndex = 0;
        txtCGPA.Text = string.Empty;
        rdActive.Checked = true;
    }

    protected void BindExitListView()
    {
        dss = objCommon.FillDropDown("ACD_EXIT_CRITERIA_MASTER", "EXIT_CRITERIA_NO", "CRITERIA_NAME, STATUS, EFFECTIVE_DATE, LEVELS, CREDITS, EXIT_SEMESTER, CGPA", "", "EXIT_CRITERIA_NO");

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            pnlExitLevel.Visible = true;
            lvExitLevel.DataSource = dss;
            lvExitLevel.DataBind();
            lvExitLevel.Visible = true;
        }
        else
        {
            lvExitLevel.DataSource = null;
            lvExitLevel.DataBind();
            lvExitLevel.Visible = false;
        }
    }

    private DataTable CreateTable_ExitCriteria()
    {
        DataTable dtExit = new DataTable();
        dtExit.Columns.Add(new DataColumn("CRITERIA_NAME", typeof(string)));
        dtExit.Columns.Add(new DataColumn("EFFECTIVE_DATE", typeof(string)));
        dtExit.Columns.Add(new DataColumn("LEVELS", typeof(string)));
        dtExit.Columns.Add(new DataColumn("CREDITS", typeof(int)));
        dtExit.Columns.Add(new DataColumn("EXIT_SEMESTER", typeof(string)));
        dtExit.Columns.Add(new DataColumn("CGPA", typeof(string)));
        dtExit.Columns.Add(new DataColumn("STATUS", typeof(int)));
        return dtExit;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //string criteria = txtCriteriaName.Text;
        //int status = 0;
        //string effectFrom = txtToDate.Text;
        //string level = ddlLevels.SelectedValue;
        //string sem = ddlExitSem.SelectedValue;
        //int credits = Convert.ToInt32(txtCredits.Text);
        //string cgpa = txtCGPA.Text;

        try
        {
            DataTable dt;
            if (Session["ExitCrtr"] != null && ((DataTable)Session["ExitCrtr"]) != null)
            {
                dt = (DataTable)Session["ExitCrtr"];
                DataTable dt1 = (DataTable)Session["ExitCrtr"];
                foreach (DataRow drow in dt1.Rows)
                {
                    if (drow["CRITERIA_NAME"].ToString().Equals(txtCriteriaName.Text.ToString()))
                    {
                        objCommon.DisplayMessage(this.updExitCriteria, "Criteria Already Exists", this.Page);
                        txtCriteriaName.Focus();
                        return;
                    }
                }
            }
            else
            {
                dt = this.CreateTable_ExitCriteria();
            }
            DataRow dr = dt.NewRow();
            dr["CRITERIA_NAME"] = txtCriteriaName.Text;
            dr["EFFECTIVE_DATE"] = txtToDate.Text;
            dr["LEVELS"] = ddlLevels.SelectedValue;
            dr["CREDITS"] = Convert.ToInt32(txtCredits.Text);
            dr["EXIT_SEMESTER"] = ddlExitSem.SelectedValue;
            dr["CGPA"] = txtCGPA.Text;
            dr["STATUS"] = rdActive.Checked ? 1 : 0;

            dt.Rows.Add(dr);
            Session["ExitCrtr"] = dt;
            lvAdd.DataSource = dt;
            lvAdd.DataBind();
            pnlAdd.Visible = true;
            //lvAdd.Visible = true;
            ClearExitField();
            btnAdd.Text = "ADD";
            //objCommon.DisplayMessage(this.updExitCriteria, "Criteria Added Successfully!!!", this.Page);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string criteria = string.Empty;
        int status = 0;
        string effectFrom = string.Empty;
        string level = string.Empty;
        string sem = string.Empty;
        int credits = 0;
        string cgpa = string.Empty;
        string strViewRecord = string.Empty;
        string condition = "0"; // For Adding Criteria

        //if(lvAdd.Items.Count <= 0)
        //{
        //    objCommon.DisplayMessage(this.updExitCriteria, "No Records Found!!!", this.Page);
        //    return;
        //}

        try
        {
            //DataTable dt;
            //if (Session["ExitCrtr"] != null && ((DataTable)Session["ExitCrtr"]) != null)
            //{
            //    dt = (DataTable)Session["ExitCrtr"];
            //    DataTable dt1 = (DataTable)Session["ExitCrtr"];
            //    foreach (DataRow drow in dt1.Rows)
            //    {
            //        criteria = drow["CRITERIA_NAME"].ToString();
            //        status = Convert.ToInt32(drow["STATUS"]);
            //        effectFrom = drow["EFFECTIVE_DATE"].ToString();
            //        level = drow["LEVELS"].ToString();
            //        sem = drow["EXIT_SEMESTER"].ToString();
            //        credits = Convert.ToInt32(drow["CREDITS"]);
            //        cgpa = drow["CGPA"].ToString();

            //        objCommon.AddExit(criteria, status, effectFrom, level, credits, sem, cgpa, condition);
            //    }
            //    objCommon.DisplayMessage(this.updExitCriteria, "Exit Criteria Added Successfully!!!", this.Page);
            //    Session["ExitCrtr"] = null;
            //}

            if (lvAdd.Items.Count <= 0)
            {
                objCommon.DisplayMessage(this.updExitCrt, "No Records Found!!!", this.Page);
                return;
            }
            else if (lvAdd.Items.Count > 0)
            {
                foreach (ListViewDataItem item in lvAdd.Items)
                {
                    Label c = item.FindControl("lblCrtN") as Label;
                    criteria = c.Text;
                    Label s = item.FindControl("lblStat") as Label;
                    status = Convert.ToInt32(s.Text);
                    Label ef = item.FindControl("lblEffect") as Label;
                    effectFrom = ef.Text;
                    Label l = item.FindControl("lblLevel") as Label;
                    level = l.Text;
                    Label sm = item.FindControl("lblSem") as Label;
                    sem = sm.Text;
                    Label crd = item.FindControl("lblCredits") as Label;
                    credits = Convert.ToInt32(crd.Text);
                    Label cgp = item.FindControl("lblCgpa") as Label;
                    cgpa = cgp.Text;

                    if (BtnSubmit.Text == "Submit")
                    {
                        strViewRecord = objCommon.LookUp("ACD_EXIT_CRITERIA_MASTER", "EXIT_CRITERIA_NO", "CRITERIA_NAME ='" + c.Text + "'");
                        //strViewRecordCollege = objCommon.LookUp("ACD_MINOR_GROUP_MASTER", "MNR_GRP_NO", "CDB_NO =" + ddlclgid);
                        if (strViewRecord != string.Empty)
                        {
                            objCommon.DisplayMessage(this.updExitCrt, "Record is Already Exists...", this.Page);
                            return;
                        }

                        objCommon.AddExit(criteria, status, effectFrom, level, credits, sem, cgpa, condition);
                    }
                    else if (BtnSubmit.Text == "Update")
                    {
                        //if (hdnCrt.Value != string.Empty)
                        //{
                        //    condition = "1";
                        //}
                        objCommon.AddExit(criteria, status, effectFrom, level, credits, sem, cgpa, hdnCrt.Value);
                    }
                    
                }

                if (BtnSubmit.Text == "Submit")
                {
                    objCommon.DisplayMessage(this.updExitCrt, "Exit Criteria Added Successfully!!!", this.Page);
                }
                else if (BtnSubmit.Text == "Update")
                {
                    objCommon.DisplayMessage(this.updExitCrt, "Exit Criteria Updated Successfully!!!", this.Page);
                }
                BtnSubmit.Text = "Submit";
                Session["ExitCrtr"] = null;
                pnlAdd.Visible = false;
                lvAdd.Visible = false;
                BindExitListView();
            }
        }
        catch
        {
            throw;
        }

        pnlExitLevel.Visible = true;
        lvExitLevel.Visible = true;
    }

    private void GetExitDetailsByCriteriaId(int ExitCrtId)
    {
        try
        {
            DataSet ds = objCommon.GetExitCriteriaDetailsByCrtID(ExitCrtId);
            if (ViewState["action"].ToString() == "edit")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //txtEventTitle.Text = ds.Tables[0].Rows[0]["EVENT_TITLE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_TITLE"].ToString();
                    //ddlEventType.SelectedValue = ds.Tables[0].Rows[0]["EventID"] == DBNull.Value ? "0" : ds.Tables[0].Rows[0]["EventID"].ToString();
                    //txtEventStart.Text = ds.Tables[0].Rows[0]["EVENT_START_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_START_DATE"].ToString();
                    //txtEventEnd.Text = ds.Tables[0].Rows[0]["EVENT_END_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_END_DATE"].ToString();
                    //txtRegStart.Text = ds.Tables[0].Rows[0]["EVENT_REG_START_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_REG_START_DATE"].ToString();
                    //txtRegEnd.Text = ds.Tables[0].Rows[0]["EVENT_REG_END_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_REG_END_DATE"].ToString();
                    //txtVenue.Text = ds.Tables[0].Rows[0]["EVENT_VENUE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_VENUE"].ToString();
                    //txtDesc.Text = ds.Tables[0].Rows[0]["EVENT_DESC"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_DESC"].ToString();                        
                    //lblBrochure.Visible = true;
                    //lblBrochure.Text = ds.Tables[0].Rows[0]["EVENT_BROCHURE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["EVENT_BROCHURE"].ToString();
                    
                    DataTable dt1;
                    dt1 = this.CreateTable_ExitCriteria();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = dt1.NewRow();
                        //dr["EXIT_CRITERIA_NO"] = ds.Tables[0].Rows[i]["EXIT_CRITERIA_NO"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["EXIT_CRITERIA_NO"].ToString();
                        dr["CRITERIA_NAME"] = ds.Tables[0].Rows[i]["CRITERIA_NAME"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["CRITERIA_NAME"].ToString();
                        dr["EFFECTIVE_DATE"] = ds.Tables[0].Rows[i]["EFFECTIVE_DATE"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["EFFECTIVE_DATE"].ToString();
                        dr["LEVELS"] = ds.Tables[0].Rows[i]["LEVELS"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["LEVELS"].ToString();
                        dr["CREDITS"] = ds.Tables[0].Rows[i]["CREDITS"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["CREDITS"].ToString();
                        dr["EXIT_SEMESTER"] = ds.Tables[0].Rows[i]["EXIT_SEMESTER"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["EXIT_SEMESTER"].ToString();
                        dr["CGPA"] = ds.Tables[0].Rows[i]["CGPA"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["CGPA"].ToString();
                        dr["STATUS"] = ds.Tables[0].Rows[i]["STATUS"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[i]["STATUS"].ToString();

                        dt1.Rows.Add(dr);
                    }
                    Session["ExitCrtr"] = dt1;
                    lvAdd.DataSource = dt1;
                    lvAdd.DataBind();

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int CrtId = int.Parse(btnEdit.CommandArgument);
            hdnCrt.Value = btnEdit.CommandArgument;
            ViewState["TITLEID"] = CrtId;
            GetExitDetailsByCriteriaId(CrtId);
            BtnSubmit.Text = "Update";
            pnlAdd.Visible = true;
            lvAdd.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private DataRow GetEditableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CRITERIA_NAME"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dataRow;
    }

    protected void imagebtn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lvAdd.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;
            if (Session["ExitCrtr"] != null && ((DataTable)Session["ExitCrtr"]) != null)
            {
                dt = ((DataTable)Session["ExitCrtr"]);
                DataRow dr = this.GetEditableDataRow(dt, btnEdit.CommandArgument);
                //hdnCrt.Value = btnEdit.CommandArgument;
                txtCriteriaName.Text = dr["CRITERIA_NAME"].ToString();
                txtToDate.Text = dr["EFFECTIVE_DATE"].ToString();
                ddlLevels.SelectedIndex = Convert.ToInt32(dr["LEVELS"]);
                txtCredits.Text = dr["CREDITS"].ToString();
                ddlExitSem.SelectedIndex = Convert.ToInt32(dr["EXIT_SEMESTER"]);
                txtCGPA.Text = dr["CGPA"].ToString();
                if (Convert.ToInt32(dr["STATUS"]) == 1)
                {
                    rdActive.Checked = true;
                }
                else
                {
                    rdActive.Checked = false;
                }

                dt.Rows.Remove(dr);
                Session["ExitCrtr"] = dt;
                lvAdd.DataSource = dt;
                lvAdd.DataBind();
                btnAdd.Text = "Update";
                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion First Tab "Exit Criteria"

    #region Second Tab "Criteria Allotment"
    protected void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "");
    }

    protected void BindListView()
    {
        dss = objCommon.FillDropDown("ACD_EXIT_CRITERIA_ALLOTMENT ECA INNER JOIN ACD_COLLEGE_SCHEME_MAPPING CSM ON (CSM.COSCHNO = ECA.COLLEGE_SCHEME_NO) INNER JOIN ACD_EXIT_CRITERIA_MASTER ECM ON (ECM.EXIT_CRITERIA_NO = ECA.EXIT_CRITERIA_NO)", "ECA.EXIT_CRT_ALLOT_NO", "ECA.COLLEGE_SCHEME_NO, ECA.EXIT_CRITERIA_NO, CSM.COL_SCHEME_NAME, ECM.CRITERIA_NAME", "", "ECA.EXIT_CRT_ALLOT_NO");

        if (dss != null && dss.Tables[0].Rows.Count > 0)
        {
            pnlCrtAllot.Visible = true;
            lvCrtAllot.DataSource = dss;
            lvCrtAllot.DataBind();
            lvCrtAllot.Visible = true;
        }
        else
        {
            lvCrtAllot.DataSource = null;
            lvCrtAllot.DataBind();
            lvCrtAllot.Visible = false;
        }
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCriteriaName, "ACD_EXIT_CRITERIA_MASTER", "EXIT_CRITERIA_NO", "CRITERIA_NAME", "EXIT_CRITERIA_NO > 0", "EXIT_CRITERIA_NO");
    }

    protected void imagebtnAllot_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["action"] = "edit";
            int CrtId = int.Parse(btnEdit.CommandArgument);
            hdnCrtId.Value = CrtId.ToString();
            dss = objCommon.GetAllotedExitCriteria(CrtId);
            PopulateDropDownList();
            ddlCollege.SelectedItem.Text = dss.Tables[0].Rows[0]["COL_SCHEME_NAME"].ToString();
            ddlCollege.SelectedValue = dss.Tables[0].Rows[0]["COLLEGE_SCHEME_NO"].ToString();
            objCommon.FillDropDownList(ddlCriteriaName, "ACD_EXIT_CRITERIA_MASTER", "EXIT_CRITERIA_NO", "CRITERIA_NAME", "EXIT_CRITERIA_NO > 0", "EXIT_CRITERIA_NO");
            ddlCriteriaName.SelectedItem.Text = dss.Tables[0].Rows[0]["CRITERIA_NAME"].ToString();
            ddlCriteriaName.SelectedValue = dss.Tables[0].Rows[0]["EXIT_CRITERIA_NO"].ToString();
            btnSubmit1.Text = "Update";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        string clgSch = string.Empty;
        string crt = string.Empty;
        string strViewRecord = string.Empty;

        try
        {
            clgSch = ddlCollege.SelectedValue;
            crt = ddlCriteriaName.SelectedValue;
            if (btnSubmit1.Text == "Submit")
            {
                hdnCrtId.Value = "0";

                strViewRecord = objCommon.LookUp("ACD_EXIT_CRITERIA_ALLOTMENT", "EXIT_CRT_ALLOT_NO", "COLLEGE_SCHEME_NO =" + clgSch + "AND EXIT_CRITERIA_NO =" + crt);
                if (strViewRecord != string.Empty)
                {
                    objCommon.DisplayMessage(this.updCrtAllotment, "Record is Already Exists...", this.Page);
                    return;
                }

                objCommon.AllotExitCriteria(clgSch, crt, hdnCrtId.Value);
                objCommon.DisplayMessage(this.updCrtAllotment, "Criteria Alloted Successfully!!!", this.Page);
            }
            else if (btnSubmit1.Text == "Update")
            {
                objCommon.AllotExitCriteria(clgSch, crt, hdnCrtId.Value);
                objCommon.DisplayMessage(this.updCrtAllotment, "Criteria Updation Successfully!!!", this.Page);
            }

            //dss = objCommon.FillDropDown("", "", "", "", "");
            //lvCrtAllot.DataSource = dss;
            //lvCrtAllot.DataBind();
            ddlCollege.SelectedIndex = 0;
            ddlCriteriaName.SelectedIndex = 0;
            BindListView();
            pnlCrtAllot.Visible = true;
            lvCrtAllot.Visible = true;
        }
        catch
        {
            throw;
        }
        btnSubmit1.Text = "Submit";
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ddlCollege.Items.Clear();
        ddlCollege.Items.Add(new ListItem("Please Select", "0"));
        PopulateDropDownList();
        ddlCriteriaName.Items.Clear();
        ddlCriteriaName.Items.Add(new ListItem("Please Select", "0"));
        btnSubmit1.Text = "Submit";
    }
    #endregion Second Tab "Criteria Allotment"
}
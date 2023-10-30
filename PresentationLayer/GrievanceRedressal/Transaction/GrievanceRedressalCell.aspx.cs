using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Text;

public partial class GrievanceRedressal_Transaction_GrievanceRedressalCell : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    GrievanceEntity objGrivE = new GrievanceEntity();
    GrievanceController objGrivC = new GrievanceController();


    #region
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
                    this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    ViewState["action"] = "add";
                    Session["RecTblGrCell"] = null;
                    ViewState["SRNO"] = null;
                    Session["RecTblGrCellRecord"] = null;
                    BindlistView(0);
                    objCommon.FillDropDownList(ddlCommitteeType, "GRIV_GR_COMMITTEE_TYPE", "GRCT_ID", "GR_COMMITTEE", "", "GRCT_ID");
                    objCommon.FillDropDownList(ddlDesignation, "GRIV_GRREDRESSAL_DESIGNATION", "GR_DESIGID", "DESIGN_NAME", "", "GR_DESIGID");
                    // objCommon.FillDropDownList(ddlCommitteeMembers, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE in (3,8,4,5)", "UA_NO");
                    //objCommon.FillDropDownList(ddlCommitteeMembers, "USER_ACC", "UA_NO", "+ isnull(CONVERT(NVARCHAR(20),UA_IDNO),'') +'-'+ISNULL(UA_FULLNAME,'')", "UA_TYPE in (3,8,4,5)", "UA_NO");
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNO");
                    objCommon.FillDropDownList(ddlEmpDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNO");
                    Session["COMMITTEE_TYPE_ID"] = null;
                    Session["DEPARTMENT_ID"] = null;

                    //if (rdbGriv.SelectedValue == "1")
                    //{
                       // FillDropDownSubGriv();
                    //}
                    //else
                    //{

                    //}
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }
    #endregion

    #region

    private DataTable CreateTabel()
    {
        DataTable dtRCell = new DataTable();
        dtRCell.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRCell.Columns.Add(new DataColumn("COMMITTEE_MEMBERS", typeof(string)));
        dtRCell.Columns.Add(new DataColumn("UANO", typeof(int)));
        dtRCell.Columns.Add(new DataColumn("DESIGNATION", typeof(string)));
        dtRCell.Columns.Add(new DataColumn("GR_DESIGID", typeof(int)));
        dtRCell.Columns.Add(new DataColumn("EMP_DEPARTMENTID", typeof(int)));
        dtRCell.Columns.Add(new DataColumn("EMP_DEPARTMENT", typeof(string)));
        dtRCell.Columns.Add(new DataColumn("STATUS", typeof(string)));
        dtRCell.Columns.Add(new DataColumn("STATUS_VALUE", typeof(char)));
        return dtRCell;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Session["COMMITTEE_TYPE_ID"] = objGrivE.COMMITTEE_TYPE_ID = Convert.ToInt32(ddlCommitteeType.SelectedValue);
            Session["DEPARTMENT_ID"] = objGrivE.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);

            //DataSet ds =  objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL_TRAN" , "UA_NO" , "DESIGNATION_ID"

            lvshowRedressalCell.Visible = true;

            DataTable dtDup = (DataTable)Session["RecTblGrCell"];
            if (dtDup != null)
            {
                if (CheckDuplicateRow(dtDup, ddlEmpDepartment.SelectedValue, ddlCommitteeMembers.SelectedValue, ddlDesignation.SelectedValue, rdbStatus.SelectedValue == "A" ? "Active" : "DeActive"))
                {
                    lvshowRedressalCell.DataSource = dtDup;
                    lvshowRedressalCell.DataBind();
                    //ddlDesignation.SelectedIndex = 0;
                    objCommon.DisplayMessage(this.Page, "Record Already Exist.", this.Page);
                    return;
                }
            }
            else
            {

            }

            if (ViewState["actionAdd"] == null)
            {
                if (Session["RecTblGrCell"] != null && ((DataTable)Session["RecTblGrCell"]) != null)
                {
                    int maxVal = 0;
                    DataTable dtRCell = (DataTable)Session["RecTblGrCell"];
                    DataRow dr = dtRCell.NewRow();
                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dtRCell.AsEnumerable().Max(row => row["SRNO"]));
                    }

                    dr["SRNO"] = maxVal + 1;
                    dr["COMMITTEE_MEMBERS"] = ddlCommitteeMembers.SelectedItem.Text;
                    dr["UANO"] = Convert.ToInt32(ddlCommitteeMembers.SelectedValue);
                    dr["DESIGNATION"] = ddlDesignation.SelectedItem.Text;
                    dr["GR_DESIGID"] = Convert.ToInt32(ddlDesignation.SelectedValue);
                    dr["EMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmpDepartment.SelectedValue);
                    dr["EMP_DEPARTMENT"] = ddlEmpDepartment.SelectedItem.Text;
                    dr["STATUS"] = rdbStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["STATUS_VALUE"] = rdbStatus.SelectedValue;
                    dtRCell.Rows.Add(dr);

                    Session["RecTblGrCell"] = dtRCell;
                    lvshowRedressalCell.DataSource = dtRCell;
                    lvshowRedressalCell.DataBind();
                    lvshowRedressalCell.Visible = true;
                    ClearShow();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                }
                else
                {  //deptid
                    DataTable dtRCell = this.CreateTabel();
                    DataRow dr = dtRCell.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                    dr["COMMITTEE_MEMBERS"] = ddlCommitteeMembers.SelectedItem.Text;
                    dr["UANO"] = Convert.ToInt32(ddlCommitteeMembers.SelectedValue);
                    dr["DESIGNATION"] = ddlDesignation.SelectedItem.Text;
                    dr["GR_DESIGID"] = Convert.ToInt32(ddlDesignation.SelectedValue);
                    dr["EMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmpDepartment.SelectedValue);
                    dr["EMP_DEPARTMENT"] = ddlEmpDepartment.SelectedItem.Text;
                    dr["STATUS"] = rdbStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["STATUS_VALUE"] = rdbStatus.SelectedValue;
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                    dtRCell.Rows.Add(dr);
                    ClearShow();
                    Session["RecTblGrCell"] = dtRCell;
                    lvshowRedressalCell.DataSource = dtRCell;
                    lvshowRedressalCell.DataBind();
                    lvshowRedressalCell.Visible = true;

                }

            }
            else
            {
                if (Session["RecTblGrCell"] != null && ((DataTable)Session["RecTblGrCell"]) != null)
                {
                    DataTable dtRCell = (DataTable)Session["RecTblGrCell"];
                    DataRow dr = dtRCell.NewRow();
                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO"]);
                    dr["COMMITTEE_MEMBERS"] = ddlCommitteeMembers.SelectedItem.Text;
                    dr["UANO"] = Convert.ToInt32(ddlCommitteeMembers.SelectedValue);
                    dr["DESIGNATION"] = ddlDesignation.SelectedItem.Text;
                    dr["GR_DESIGID"] = Convert.ToInt32(ddlDesignation.SelectedValue);
                    dr["EMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmpDepartment.SelectedValue);
                    dr["EMP_DEPARTMENT"] = ddlEmpDepartment.SelectedItem.Text;
                    dr["STATUS"] = rdbStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["STATUS_VALUE"] = rdbStatus.SelectedValue;
                    dtRCell.Rows.Add(dr);
                    Session["RecTblGrCell"] = dtRCell;
                    lvshowRedressalCell.DataSource = dtRCell;
                    lvshowRedressalCell.DataBind();
                    lvshowRedressalCell.Visible = true;
                    ClearShow();
                    ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnAddShDesc_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private bool CheckDuplicateRow(DataTable dt, string EmpDept, string ComitteMem, string Desig, string Status)
    {

        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EMP_DEPARTMENTID"].ToString() == EmpDept && dr["GR_DESIGID"].ToString() == Desig && dr["UANO"].ToString() == ComitteMem && dr["STATUS"].ToString() == Status)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.CheckDuplicateRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }


    protected void ClearShow()
    {
        // ddlCommitteeType.SelectedIndex = 0;
        // ddlDepartment.SelectedIndex = 0;
        //    ddlEmpDepartment.SelectedIndex = 0;
        ddlCommitteeMembers.SelectedIndex = 0;
        ddlDesignation.SelectedIndex = 0;
        ViewState["actionAdd"] = null;
        ViewState["EDIT_SRNO"] = null;
        rdbStatus.SelectedValue = "A";
        ddlEmpDepartment.Enabled = true;
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //ViewState["EDIT_SRNO"] = string.Empty;
            //ImageButton btnEdit = sender as ImageButton;
            //DataTable dtRCell;
            //if (Session["RecTblGrCell"] != null && ((DataTable)Session["RecTblGrCell"]) != null)
            //{
            //    dtRCell = ((DataTable)Session["RecTblGrCell"]);
            //    ViewState["EDIT_SRNO"] = btnEdit.CommandArgument;

            //    DataRow dr = this.GetEditableDatarow(dtRCell, btnEdit.CommandArgument);
            //    ddlCommitteeMembers.SelectedValue = dr["UANO"].ToString();
            //    ddlDesignation.SelectedValue = dr["GR_DESIGID"].ToString();
            //    ddlEmpDepartment.SelectedValue = dr["EMP_DEPARTMENTID"].ToString();
            //    rdbStatus.SelectedValue = dr["STATUS_VALUE"].ToString(); // == "Active" ? "A" : "I";

            //    dtRCell.Rows.Remove(dr);
            //    Session["RecTblGrCell"] = dtRCell;
            //    lvshowRedressalCell.DataSource = dtRCell;
            //    lvshowRedressalCell.DataBind();
            //    lvshowRedressalCell.Visible = true;
            //    ViewState["actionAdd"] = "edit";
            //    ddlEmpDepartment.Enabled = false;

            // ViewState["EDIT_SRNO"] = string.Empty;
            ImageButton btnEditRecord = sender as ImageButton;
            string[] arg = new string[4];
            arg = btnEditRecord.CommandArgument.ToString().Split(';');
            string empdept = arg[0];
            string commem = arg[1];
            string des = arg[2];
            string status = arg[3];
            ddlEmpDepartment.SelectedValue = empdept;
            ddlCommitteeMembers.SelectedItem.Text = commem;
            ddlDesignation.SelectedValue = des;
            rdbStatus.SelectedValue = status;
            DataTable dtRCell;
            dtRCell = ((DataTable)Session["RecTblGrCell"]);
            for (int i = dtRCell.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtRCell.Rows[i];
                if (dr["EMP_DEPARTMENTID"].ToString() == ddlEmpDepartment.SelectedValue && dr["COMMITTEE_MEMBERS"].ToString() == ddlCommitteeMembers.SelectedValue && dr["GR_DESIGID"].ToString() == ddlDesignation.SelectedValue && dr["STATUS_VALUE"].ToString() == rdbStatus.SelectedValue)
                {
                    dr.Delete();
                }
            }
            dtRCell.AcceptChanges();
            if (dtRCell.Rows.Count > 0)
            {
                lvshowRedressalCell.DataSource = dtRCell;
                lvshowRedressalCell.DataBind();
            }
            else
            {
                lvshowRedressalCell.DataSource = null;
                lvshowRedressalCell.DataBind();
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    #endregion

    #region
    protected void Clear()
    {
        //ddlCommitteeMembers.SelectedIndex = 0;
        ddlCommitteeMembers.SelectedValue = "0";
        ddlDesignation.SelectedIndex = 0;
        ddlCommitteeType.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        lvshowRedressalCell.DataSource = null;
        lvshowRedressalCell.DataBind();
        lvshowRedressalCell.Visible = false;
        ViewState["GRC_ID"] = null;
        ViewState["action"] = "add";
        Session["RecTblGrCell"] = null;
        divDept.Visible = false;
        ddlEmpDepartment.SelectedIndex = 0;
        ddlEmpDepartment.Enabled = true;

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //objGrivE.COMMITTEE_TYPE_ID = Convert.ToInt32(ddlCommitteeType.SelectedValue);
            //objGrivE.DEPARTMENT_ID = Convert.ToInt32(ddlDepartment.SelectedValue);

            objGrivE.COMMITTEE_TYPE_ID = Convert.ToInt32(Session["COMMITTEE_TYPE_ID"]);
            objGrivE.DEPARTMENT_ID = Convert.ToInt32(Session["DEPARTMENT_ID"]);

            if (Session["RecTblGrCell"] != null)
            {
                DataTable dt = (DataTable)Session["RecTblGrCell"];
                objGrivE.GRCELL_TABLE = dt;
            }
            else
            {
                objCommon.DisplayMessage(this.upCommitteeMember, "Please Add Members To The Committee.", this.Page);
                return;
            }

            objGrivE.UANO = Convert.ToInt32(Session["userno"]);

            if (rdbGriv.SelectedValue == "0")
            {
                objGrivE.ISCOMMITEETYPE = '1';
            }
            else
            {
                objGrivE.ISCOMMITEETYPE = '0';
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    // if (ddlDepartment.SelectedIndex != 0)
                    if (objGrivE.DEPARTMENT_ID != 0)
                    {

                        //if (ddlDepartment.SelectedIndex > 0)
                        if (objGrivE.DEPARTMENT_ID > 0)
                        {
                            DataSet ds = objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL", "GRC_ID", "GRCT_ID", "GRCT_ID=" + Convert.ToInt32(ddlCommitteeType.SelectedValue) + " AND DEPT_ID = 0", "");
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                objCommon.DisplayMessage(this.upCommitteeMember, "Record Already Exist", this.Page);
                                return;
                            }
                            else
                            {
                                objGrivE.GRC_ID = 0;
                                CustomStatus cs = (CustomStatus)objGrivC.AddUpdateRedressalCell(objGrivE);
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    //BindlistView(Convert.ToInt32(ddlCommitteeType.SelectedValue));
                                    BindlistView(0);
                                    objCommon.DisplayMessage(this.upCommitteeMember, "Record Saved Successfully.", this.Page);
                                    Clear();
                                }
                            }
                        }

                    }
                    else
                    {
                        objGrivE.GRC_ID = 0;
                        CustomStatus cs = (CustomStatus)objGrivC.AddUpdateRedressalCell(objGrivE);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //BindlistView(Convert.ToInt32(ddlCommitteeType.SelectedValue));
                            BindlistView(0);
                            objCommon.DisplayMessage(this.upCommitteeMember, "Record Saved Successfully.", this.Page);
                            Clear();
                        }
                    }
                }
                else
                {
                    objGrivE.GRC_ID = Convert.ToInt32(ViewState["GRC_ID"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateRedressalCell(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindlistView(Convert.ToInt32(ddlCommitteeType.SelectedValue));
                        BindlistView(0);
                        objCommon.DisplayMessage(this.upCommitteeMember, "Record Updated Successfully.", this.Page);
                        Clear();
                    }

                }
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlCommitteeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvshowRedressalCell.DataSource = null;
        lvshowRedressalCell.DataBind();
        if (ddlCommitteeType.SelectedValue == "0")
        {
            return;
        }
        DataSet ds = objCommon.FillDropDown("GRIV_GR_COMMITTEE_TYPE", "GRCT_ID", "GR_COMMITTEE,DEPT_FLAG", "GRCT_ID=" + Convert.ToInt32(ddlCommitteeType.SelectedValue), "GRCT_ID");

        //if ( ds.Tables[0].Rows[0]["DEPT_FLAG"].ToString() == "1")
        if (ds != null && ds.Tables[0].Rows[0]["DEPT_FLAG"].ToString() == "1")//Added by vijay andoju on 14-03-2020
        {
            divDept.Visible = true;
        }
        else
        {
            divDept.Visible = false;
        }
        if (ddlCommitteeType.SelectedValue != "0")
        {
            BindlistView(Convert.ToInt32(ddlCommitteeType.SelectedValue));
        }
        else { }
    }

    private void BindlistView(int GRCT_ID)
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL C LEFT JOIN ACD_DEPARTMENT P ON C.DEPT_ID=P.DEPTNO INNER JOIN GRIV_GR_COMMITTEE_TYPE CT  ON C.GRCT_ID=CT.GRCT_ID ", "C.GRC_ID,C.GRCT_ID,C.DEPT_ID", "CT.GR_COMMITTEE,P.DEPTNAME ", "", "C.GRC_ID");
            DataSet ds = objGrivC.GetGrievanceRedressalCellList(GRCT_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCommittee.DataSource = ds;
                lvCommittee.DataBind();
                lvCommittee.Visible = true;
            }
            else
            {
                lvCommittee.DataSource = null;
                lvCommittee.DataBind();
                lvCommittee.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEditRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditR = sender as ImageButton;
            int GRC_ID = int.Parse(btnEditR.CommandArgument);
            ViewState["GRC_ID"] = int.Parse(btnEditR.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(GRC_ID);
            ddlCommitteeMembers.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }


    private void ShowDetails(int GRC_ID)
    {
        try
        {

            DataSet ds = null;
            ds = objGrivC.GetCellDetailsByID(GRC_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCommitteeType.SelectedValue = ds.Tables[0].Rows[0]["GRCT_ID"].ToString();
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();
                if (ddlDepartment.SelectedValue == "0")
                {
                    divDept.Visible = false;
                }
                else
                {
                    divDept.Visible = true;
                }
            }
            DataSet dsMem = null;
            dsMem = objGrivC.GetCommitteeMemberDetailsByID(GRC_ID);
            if (dsMem.Tables[0].Rows.Count > 0)
            {
                lvshowRedressalCell.DataSource = dsMem;
                lvshowRedressalCell.DataBind();
                lvshowRedressalCell.Visible = true;
                Session["RecTblGrCell"] = dsMem.Tables[0];
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private DataRow GetEditableDatarowRecord(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("File Details", "rptGRCommitteeMembers.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("GrievanceRedressal")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,GrievanceRedressal," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
            //  ScriptManager.RegisterClientScriptBlock(this.updActivity, this.updActivity.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //protected void ddlCommitteeMembers_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        DataSet ds = objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL_TRAN", "UA_NO", " ", "UA_NO=" + Convert.ToInt32(ddlCommitteeMembers.SelectedValue), "");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            objCommon.DisplayMessage(this.upCommitteeMember, "Record Already Exist", this.Page);
    //            ddlCommitteeMembers.SelectedIndex = 0;
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ddlCommitteeMembers_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}
    protected void ddlEmpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlCommitteeMembers, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_TYPE in (3,8,4,5) AND UA_DEPTNO =" + Convert.ToInt32(ddlEmpDepartment.SelectedValue), "UA_NO");
            objCommon.FillDropDownList(ddlCommitteeMembers, "USER_ACC U LEFT JOIN PAYROLL_EMPMAS E ON(isnull(U.UA_IDNO,0)=isnull(E.IDNO,0))", "U.UA_NO", "+ ISNULL(E.PFILENO,'') +'-'+ isnull(U.UA_FULLNAME,'')", "U.UA_TYPE in (3,8,4,5) AND U.UA_DEPTNO ='" + Convert.ToString(ddlEmpDepartment.SelectedValue) + "'", "U.UA_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ddlCommitteeMembers_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void ddlCommitteeMembers_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //    try
        //    {

        //        DataSet ds = objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL_TRAN", "UA_NO", " ", "UA_NO=" + Convert.ToInt32(ddlCommitteeMembers.SelectedValue), "");
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            objCommon.DisplayMessage(this.upCommitteeMember, "Record Already Exist", this.Page);
        //            ddlCommitteeMembers.SelectedIndex = 0;
        //            Clear();
        //            return;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToBoolean(Session["error"]) == true)
        //            objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ddlCommitteeMembers_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
        //        else
        //            objUCommon.ShowError(Page, "Server UnAvailable");
        //    }
    }
    protected void rdbGriv_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbGriv.SelectedValue == "0")
        {
            pnlCommitteTypeMember.Visible = true;
            pnlSubGType.Visible = false;
            //divButtons.Visible = false;
        }
        else
        {
            pnlSubGType.Visible = true;
            pnlCommitteTypeMember.Visible = false;
            //divButtons.Visible = false;
            //FillDropDownSubGriv();
            FillDropDownGriv();
            FillDropDowns();
            BindlistViewSub(0);
        }
    }

    #region SubGrievance

    private void FillDropDownGriv()
    {
        try
        {
            objCommon.FillDropDownList(ddlGrivType, "GRIV_GRIEVANCE_TYPE", "GRIV_ID", "GT_NAME", "GRIV_ID>0", "GRIV_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_SubGrievance.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlGrivType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlSubGriv, "GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "GRIV_ID=" + Convert.ToInt32(ddlGrivType.SelectedValue), "SUB_ID");
            BindlistViewSub(Convert.ToInt32(ddlGrivType.SelectedValue));
            //Session["TblSubGriv"] = null;
            //DataTable dtSubCell = (DataTable)Session["TblSubGriv"];
            //ddlEmp.SelectedIndex = 0; 
            GrivsubClear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_SubGrievance.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }        
    }

    private void FillDropDowns()
    {
        try
        {
            objCommon.FillDropDownList(ddlDes, "GRIV_GRREDRESSAL_DESIGNATION", "GR_DESIGID", "DESIGN_NAME", "", "GR_DESIGID");
            objCommon.FillDropDownList(ddlEmp, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "", "DEPTNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Master_SubGrievance.FillDropDown ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //objCommon.FillDropDownList(ddlSubMem, "USER_ACC U LEFT JOIN PAYROLL_EMPMAS E ON(isnull(U.UA_IDNO,0)=isnull(E.IDNO,0))", "U.UA_NO", "+ ISNULL(E.PFILENO,'') +'-'+ isnull(U.UA_FULLNAME,'')", "U.UA_TYPE in (3,8,4,5) AND U.UA_DEPTNO ='" + Convert.ToString(ddlEmp.SelectedValue) + "'", "U.UA_NO");
            FillEmployees();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ddlCommitteeMembers_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillEmployees()
    {
        objCommon.FillDropDownList(ddlSubMem, "USER_ACC U LEFT JOIN PAYROLL_EMPMAS E ON(isnull(U.UA_IDNO,0)=isnull(E.IDNO,0))", "U.UA_NO", "+ ISNULL(E.PFILENO,'') +'-'+ isnull(U.UA_FULLNAME,'')", "U.UA_TYPE in (3,8,4,5) AND U.UA_DEPTNO ='" + Convert.ToString(ddlEmp.SelectedValue) + "'", "U.UA_NO");
    }
   
    protected void btnAddSub_Click(object sender, EventArgs e)
    {
        try
        {
            Session["GRIV_TYPE"] = objGrivE.GRIEVANCE_TYPE_ID = Convert.ToInt32(ddlGrivType.SelectedValue);
            Session["SUB_GRIV_TYPE"] = objGrivE.GRIV_SUB_ID = Convert.ToInt32(ddlSubGriv.SelectedValue);
            Session["DEPARTMENT_ID"] = objGrivE.DEPARTMENT_ID = Convert.ToInt32(ddlEmp.SelectedValue);

            //lvAddSub.Visible = true;
            pnlSubList.Visible = true;

            DataTable dtDupSub = (DataTable)Session["TblSubGriv"];
            if (dtDupSub != null)
            {
                if (CheckSubDuplicateRow(dtDupSub, ddlEmp.SelectedValue, ddlSubMem.SelectedValue, ddlDes.SelectedValue, rdSubStatus.SelectedValue == "A" ? "Active" : "DeActive"))
                {
                    lvAddSub.DataSource = dtDupSub;
                    lvAddSub.DataBind();
                    lvAddSub.Visible = true;
                    MessageBox("Record Already Exist.");                    
                    return;
                }
            }
            else
            {

            }

            if (ViewState["actionSubAdd"] == null)
            {
                if (Session["TblSubGriv"] != null && ((DataTable)Session["TblSubGriv"]) != null)
                {
                    int maxVal = 0;
                    DataTable dtSubCell = (DataTable)Session["TblSubGriv"];
                    DataRow dr = dtSubCell.NewRow();
                    if (dr != null)
                    {
                        maxVal = Convert.ToInt32(dtSubCell.AsEnumerable().Max(row => row["SUBSRNO"]));
                    }

                    dr["SUBSRNO"] = maxVal + 1;
                    dr["SUB_GRIV_MEM"] = ddlSubMem.SelectedItem.Text;
                    dr["SUBUANO"] = Convert.ToInt32(ddlSubMem.SelectedValue);
                    dr["SUBDESIGNATION"] = ddlDes.SelectedItem.Text;
                    dr["SUBDESIGID"] = Convert.ToInt32(ddlDes.SelectedValue);
                    dr["SUBEMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmp.SelectedValue);
                    dr["SUBEMP_DEPARTMENT"] = ddlEmp.SelectedItem.Text;
                    dr["SUBSTATUS"] = rdSubStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["SUBSTATUS_VALUE"] = rdSubStatus.SelectedValue;
                    dtSubCell.Rows.Add(dr);

                    Session["TblSubGriv"] = dtSubCell;
                    lvAddSub.DataSource = dtSubCell;
                    lvAddSub.DataBind();
                    lvAddSub.Visible = true;
                    ClearSub();
                    ViewState["SUBSRNO"] = Convert.ToInt32(ViewState["SUBSRNO"]) + 1;
                    
                }
                else
                {  //deptid
                    DataTable dtSubCell = this.CreateSubTabel();
                    DataRow dr = dtSubCell.NewRow();
                    dr["SUBSRNO"] = Convert.ToInt32(ViewState["SUBSRNO"]) + 1;                   
                    dr["SUB_GRIV_MEM"] = ddlSubMem.SelectedItem.Text;
                    dr["SUBUANO"] = Convert.ToInt32(ddlSubMem.SelectedValue);
                    dr["SUBDESIGNATION"] = ddlDes.SelectedItem.Text;
                    dr["SUBDESIGID"] = Convert.ToInt32(ddlDes.SelectedValue);
                    dr["SUBEMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmp.SelectedValue);
                    dr["SUBEMP_DEPARTMENT"] = ddlEmp.SelectedItem.Text;
                    dr["SUBSTATUS"] = rdSubStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["SUBSTATUS_VALUE"] = rdSubStatus.SelectedValue;
                    ViewState["SUBSRNO"] = Convert.ToInt32(ViewState["SUBSRNO"]) + 1;

                    dtSubCell.Rows.Add(dr);
                    ClearSub();
                    Session["TblSubGriv"] = dtSubCell;
                    lvAddSub.DataSource = dtSubCell;
                    lvAddSub.DataBind();
                    lvAddSub.Visible = true;
                }
            }
            else
            {
                if (Session["TblSubGriv"] != null && ((DataTable)Session["TblSubGriv"]) != null)
                {
                    DataTable dtSubCell = (DataTable)Session["TblSubGriv"];
                    DataRow dr = dtSubCell.NewRow();
                    dr["SUBSRNO"] = Convert.ToInt32(ViewState["EDIT_SUBSRNO"]);
                    dr["SUB_GRIV_MEM"] = ddlSubMem.SelectedItem.Text;
                    dr["SUBUANO"] = Convert.ToInt32(ddlSubMem.SelectedValue);
                    dr["SUBDESIGNATION"] = ddlDes.SelectedItem.Text;
                    dr["SUBDESIGID"] = Convert.ToInt32(ddlDes.SelectedValue);
                    dr["SUBEMP_DEPARTMENTID"] = Convert.ToInt32(ddlEmp.SelectedValue);
                    dr["SUBEMP_DEPARTMENT"] = ddlEmp.SelectedItem.Text;
                    dr["SUBSTATUS"] = rdSubStatus.SelectedValue == "A" ? "Active" : "DeActive";
                    dr["SUBSTATUS_VALUE"] = rdSubStatus.SelectedValue;                   
                    dtSubCell.Rows.Add(dr);
                    Session["TblSubGriv"] = dtSubCell;
                    lvAddSub.DataSource = dtSubCell;
                    lvAddSub.DataBind();
                    lvAddSub.Visible = true;
                    ClearSub();
                    ViewState["SUBSRNO"] = Convert.ToInt32(ViewState["SUBSRNO"]) + 1;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnAddShDesc_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ClearSub()
    {        
        ddlSubMem.SelectedIndex = 0;
        ddlDes.SelectedIndex = 0;
        ViewState["actionSubAdd"] = null;
        ViewState["EDIT_SUBSRNO"] = null;
        rdSubStatus.SelectedValue = "A";
        //ddlEmp.SelectedIndex = 0;
    }

    protected void btnSubmitSub_Click(object sender, EventArgs e)
    {
        try
        {
           

            objGrivE.GRIEVANCE_TYPE_ID = Convert.ToInt32(Session["GRIV_TYPE"]);
            objGrivE.GRIV_SUB_ID = Convert.ToInt32(Session["SUB_GRIV_TYPE"]);
            objGrivE.DEPARTMENT_ID = Convert.ToInt32(Session["DEPARTMENT_ID"]);            
            if (rdbGriv.SelectedValue == "0")
            {
                objGrivE.ISCOMMITEETYPE = '1';
            }
            else
            {
                objGrivE.ISCOMMITEETYPE = '0';
            }
            if (Session["TblSubGriv"] != null)
            {
                DataTable dt = (DataTable)Session["TblSubGriv"];
                objGrivE.GRSUB_TABLE = dt;
            }
            else
            {
                MessageBox("Please Add Members To The Committee.");
                return;
            }

            objGrivE.SUBUANO = Convert.ToInt32(Session["userno"]);

            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    // if (ddlDepartment.SelectedIndex != 0)
                    if (objGrivE.DEPARTMENT_ID != 0)
                    {

                        //if (ddlDepartment.SelectedIndex > 0)
                        if (objGrivE.DEPARTMENT_ID > 0)
                        {
                           // DataSet ds = objCommon.FillDropDown("GRIV_GR_REDRESSAL_CELL", "GRC_ID", "GRCT_ID", "GRCT_ID=" + Convert.ToInt32(ddlCommitteeType.SelectedValue) + " AND DEPT_ID = 0", "");
                            //if //(ds.Tables[0].Rows.Count > 0)
                            //{
                            //   // MessageBox("Record Already Exist");
                            //   // return;
                            //}
                            //else
                            //{
                                objGrivE.SUB_GR_ID = 0;
                                CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSubRedressal(objGrivE);
                                if (cs.Equals(CustomStatus.RecordSaved))
                                {
                                    //BindlistViewSub(Convert.ToInt32(ddlGrivType.SelectedValue));
                                    BindlistViewSub(0);
                                    MessageBox("Record Saved Successfully.");
                                    GrivClear();
                                }
                           // }
                        }

                    }
                    else
                    {
                        objGrivE.SUB_GR_ID = 0;
                        CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSubRedressal(objGrivE);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //BindlistViewSub(Convert.ToInt32(ddlGrivType.SelectedValue));
                            BindlistViewSub(0);
                            MessageBox("Record Saved Successfully.");
                            GrivClear();
                        }
                    }
                }
                else
                {
                    objGrivE.SUB_GR_ID = Convert.ToInt32(ViewState["SUB_GR_ID"]);
                    CustomStatus cs = (CustomStatus)objGrivC.AddUpdateSubRedressal(objGrivE);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindlistViewSub(Convert.ToInt32(ddlGrivType.SelectedValue));
                        BindlistViewSub(0);
                        MessageBox("Record Updated Successfully.");
                        GrivClear();
                    }

                }
            }
        }


        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelSub_Click(object sender, EventArgs e)
    {
        GrivClear();
    }
    protected void btnEditSub_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //FillEmployees(); 
            ImageButton btnEditRecord = sender as ImageButton;
            string[] arg = new string[4];
            arg = btnEditRecord.CommandArgument.ToString().Split(';');
            string subempdept = arg[0];            
            string subcomme = arg[1];
            string subdes = arg[2];
            string substatus = arg[3];
            ddlEmp.SelectedValue = subempdept;
            //ddlSubMem.SelectedItem.Text = subcomme;
            ddlSubMem.SelectedValue = subcomme;
            ddlDes.SelectedValue = subdes;
            rdSubStatus.SelectedValue = substatus;            
            DataTable dtSubCell;
            dtSubCell = ((DataTable)Session["TblSubGriv"]);

            for (int i = dtSubCell.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtSubCell.Rows[i];
                if (dr["SUBEMP_DEPARTMENTID"].ToString() == ddlEmp.SelectedValue && dr["SUBUANO"].ToString() == ddlSubMem.SelectedValue && dr["SUBDESIGID"].ToString() == ddlDes.SelectedValue && dr["SUBSTATUS_VALUE"].ToString() == rdSubStatus.SelectedValue)
                {
                    dr.Delete();
                }
            }
            dtSubCell.AcceptChanges();
            if (dtSubCell.Rows.Count > 0)
            {
                lvAddSub.DataSource = dtSubCell;
                lvAddSub.DataBind();
            }
            else
            {
                lvAddSub.DataSource = null;
                lvAddSub.DataBind();
            }
            //FillEmployees();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable CreateSubTabel()
    {
        DataTable dtSubCell = new DataTable();
        dtSubCell.Columns.Add(new DataColumn("SUBSRNO", typeof(int)));
        dtSubCell.Columns.Add(new DataColumn("SUB_GRIV_MEM", typeof(string)));
        dtSubCell.Columns.Add(new DataColumn("SUBUANO", typeof(int)));
        dtSubCell.Columns.Add(new DataColumn("SUBDESIGNATION", typeof(string)));
        dtSubCell.Columns.Add(new DataColumn("SUBDESIGID", typeof(int)));
        dtSubCell.Columns.Add(new DataColumn("SUBEMP_DEPARTMENTID", typeof(int)));
        dtSubCell.Columns.Add(new DataColumn("SUBEMP_DEPARTMENT", typeof(string)));
        dtSubCell.Columns.Add(new DataColumn("SUBSTATUS", typeof(string)));
        dtSubCell.Columns.Add(new DataColumn("SUBSTATUS_VALUE", typeof(char)));
        return dtSubCell;
    }

    private bool CheckSubDuplicateRow(DataTable dtSubDup, string SubEmpDept, string SubComitteMem, string SubDesig, string SubStatus)
    {

        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtSubDup.Rows)
            {
                if (dr["SUBEMP_DEPARTMENTID"].ToString() == SubEmpDept && dr["SUBDESIGID"].ToString() == SubDesig && dr["SUBUANO"].ToString() == SubComitteMem && dr["SUBSTATUS"].ToString() == SubStatus)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.CheckDuplicateRow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

  

    private void BindlistViewSub(int GRIV_ID)
    {
        try
        {

            DataSet ds = objGrivC.GetSubGrievanceRedressalCellList(GRIV_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvSubMem.DataSource = ds;
                lvSubMem.DataBind();
                pnlSubTypeList.Visible = true;
            }
            else
            {
                lvSubMem.DataSource = null;
                lvSubMem.DataBind();
                pnlSubTypeList.Visible = false;
                lvAddSub.DataSource = null;
                lvAddSub.DataBind();
                pnlSubList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    protected void btnEditSubRec_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditR = sender as ImageButton;
            int SUB_GR_ID = int.Parse(btnEditR.CommandArgument);
            ViewState["SUB_GR_ID"] = int.Parse(btnEditR.CommandArgument);
            ViewState["action"] = "edit";
            ShowSubDetails(SUB_GR_ID);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.btnEdit_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowSubDetails(int SUB_GR_ID)
    {
        try
        {

            DataSet ds = null;
            ds = objGrivC.GetSubGrivDetailsByID(SUB_GR_ID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlGrivType.SelectedValue = ds.Tables[0].Rows[0]["GRIV_ID"].ToString();
                objCommon.FillDropDownList(ddlSubGriv, "GRIV_SUB_GRIEVANCE_TYPE", "SUB_ID", "SUBGTNAME", "GRIV_ID=" + Convert.ToInt32(ddlGrivType.SelectedValue), "SUB_ID");
                ddlSubGriv.SelectedValue = ds.Tables[0].Rows[0]["SUB_ID"].ToString();
                //FillEmployees();
            }
            DataSet dsSubMem = null;
            dsSubMem = objGrivC.GetSubCommitteeByID(SUB_GR_ID);
            if (dsSubMem.Tables[0].Rows.Count > 0)
            {
                lvAddSub.DataSource = dsSubMem;
                lvAddSub.DataBind();
                lvAddSub.Visible = true;
                Session["TblSubGriv"] = dsSubMem.Tables[0];
                ddlEmp.SelectedValue = dsSubMem.Tables[0].Rows[0]["SUBEMP_DEPARTMENTID"].ToString();
                objCommon.FillDropDownList(ddlSubMem, "USER_ACC U LEFT JOIN PAYROLL_EMPMAS E ON(isnull(U.UA_IDNO,0)=isnull(E.IDNO,0))", "U.UA_NO", "+ ISNULL(E.PFILENO,'') +'-'+ isnull(U.UA_FULLNAME,'')", "U.UA_TYPE in (3,8,4,5) AND U.UA_DEPTNO ='" + Convert.ToString(ddlEmp.SelectedValue) + "'", "U.UA_NO");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "GrievanceRedressal_Transaction_GrievanceRedressalCell.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void GrivClear()
    {        
        ddlGrivType.SelectedValue = "0";
        ddlSubGriv.SelectedIndex = 0;
        ddlEmp.SelectedIndex = 0;
        ddlSubMem.SelectedValue = "0";
        ddlDes.SelectedIndex = 0;
        rdSubStatus.SelectedValue = "A";
        lvAddSub.DataSource = null;
        lvAddSub.DataBind();
        lvAddSub.Visible = false;
        ViewState["SUB_GR_ID"] = null;
        ViewState["action"] = "add";
        Session["TblSubGriv"] = null;
        DataTable dtSubCell = (DataTable)Session["TblSubGriv"];
        //dtSubCell.Rows.Clear();      
    }

    protected void GrivsubClear()
    {        
        ddlSubGriv.SelectedIndex = 0;
        ddlEmp.SelectedIndex = 0;
        ddlSubMem.SelectedValue = "0";
        ddlDes.SelectedIndex = 0;
        rdSubStatus.SelectedValue = "A";
        lvAddSub.DataSource = null;
        lvAddSub.DataBind();
        lvAddSub.Visible = false;
        ViewState["SUB_GR_ID"] = null;
        ViewState["action"] = "add";
        Session["TblSubGriv"] = null;
        DataTable dtSubCell = (DataTable)Session["TblSubGriv"];
        //dtSubCell.Rows.Clear();      
    }
   
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class RECRUITMENT_Master_RequisitionApprovalLevels : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RequisitionController objReq = new RequisitionController();
    Requisition objRequisition = new Requisition();

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
    DataTable dtPR = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                CheckPageAuthorization();
                FillDepartment();
                BindReqApprLvl();
                dtPR = setPRGridViewDataset(dtPR, "dtPR").Clone();
                ViewState["dtPR"] = dtPR;
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
                Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
        }
    }
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_CODE =" + Convert.ToInt32(Session["colcode"]) + " ", "DEPT.SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillDepartment ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillPost()
    {
        try
        {
            int Deptno = Convert.ToInt32(ddlDepartment.SelectedValue);
            DataSet ds = objReq.GetPosts(Deptno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstPost.DataSource = ds;
                lstPost.DataTextField = "sPostCode";
                lstPost.DataValueField = "nId";
                lstPost.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillPost ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillUser()
    {
        try
        {
            objCommon.FillDropDownList(ddlApproval, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  ", "UA_FULLNAME");
            objCommon.FillDropDownList(ddlUser, "USER_ACC U INNER JOIN REC_REQUISITION_USER RU ON (RU.UA_NO = U.UA_NO)", "U.UA_NO", "U.UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND RU.ISACTIVE = 1 AND RU.DEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "U.UA_FULLNAME");
            //objCommon.FillDropDownList(ddlPA01, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
            //objCommon.FillDropDownList(ddlPA02, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
            //objCommon.FillDropDownList(ddlPA03, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
            //objCommon.FillDropDownList(ddlPA04, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
            //objCommon.FillDropDownList(ddlPA05, "USER_ACC", "UA_NO", "UA_FULLNAME AS UA_FULLNAME", "UA_Type in(1,3,4,5,8)  AND UA_EMPDEPTNO=" + Convert.ToInt32(ddlDepartment.SelectedValue), "UA_FULLNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillUser ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPost();
        FillUser();

    }
    //protected void btnAddAppr_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        dtPR = (DataTable)ViewState["dtPR"];
    //        DataRow dr = dtPR.NewRow();

    //        if (CheckDuplicatePR(dtPR, ddlApproval.SelectedItem.Text))
    //        {
    //            MessageBox("Record Already Exist!");
    //            return;
    //        }

    //        if (ddlApproval.SelectedIndex > 0)
    //        {
    //            dr["Authority"] = Convert.ToString(ddlApproval.SelectedItem.Text);
    //            dr["AuthorityNo"] = Convert.ToInt32(ddlApproval.SelectedValue);
    //        }
    //        else
    //        {
    //            //dr["Authority"] = string.Empty;
    //            //dr["AuthorityNo"] = string.Empty;
    //            MessageBox("Please Select Approval Authority");
    //            return;
    //        }

    //        dtPR.Columns["SRNO"].AutoIncrementStep = 1;

    //        if (dtPR.Rows.Count > 0)
    //        {

    //            //int maxSeqNo = dtPR.AsEnumerable().Max(row => row.Field<int>("SRNO"));
    //            int maxSeqNo = dtPR.AsEnumerable()
    //               .Where(row => row["SRNO"] != DBNull.Value && row["SRNO"] != null)
    //               .Max(row => Convert.ToInt32(row["SRNO"]));
    //            dr["SRNO"] = maxSeqNo + 1;
    //        }
    //        else
    //        {
    //            dr["SRNO"] = 1;
    //        }

    //        dtPR.Rows.Add(dr);
    //        dtPR.AcceptChanges();
    //        lvApplvl.DataSource = dtPR;
    //        lvApplvl.DataBind();
    //        pnlAl.Visible = true;
    //        lvApplvl.Visible = true;
    //        ddlApproval.SelectedIndex = 0;
    //    }
    //    catch (Exception ex)
    //    {
            
    //    }
    //}

protected void btnAddAppr_Click(object sender, EventArgs e)
{
    try
    {
        dtPR = (DataTable)ViewState["dtPR"];
        if (dtPR.Rows.Count >= 5)
        {
            MessageBox("You Cannot Add More Than 5 Approval Authorities");
            return;
        }

        DataRow dr = dtPR.NewRow();

        if (CheckDuplicatePR(dtPR, ddlApproval.SelectedItem.Text))
        {
            MessageBox("Record Already Exist!");
            return;
        }

        if (ddlApproval.SelectedIndex > 0)
        {
            dr["Authority"] = Convert.ToString(ddlApproval.SelectedItem.Text);
            dr["AuthorityNo"] = Convert.ToInt32(ddlApproval.SelectedValue);
        }
        else
        {
            MessageBox("Please Select Approval Authority");
            return;
        }

        dtPR.Columns["SRNO"].AutoIncrementStep = 1;

        if (dtPR.Rows.Count > 0)
        {
            int maxSeqNo = dtPR.AsEnumerable()
               .Where(row => row["SRNO"] != DBNull.Value && row["SRNO"] != null)
               .Max(row => Convert.ToInt32(row["SRNO"]));
            dr["SRNO"] = maxSeqNo + 1;
        }
        else
        {
            dr["SRNO"] = 1;
        }

        dtPR.Rows.Add(dr);
        dtPR.AcceptChanges();
        lvApplvl.DataSource = dtPR;
        lvApplvl.DataBind();
        pnlAl.Visible = true;
        lvApplvl.Visible = true;
        ddlApproval.SelectedIndex = 0;
    }
    catch (Exception ex)
    {
        // Handle the exception
        MessageBox(ex.Message);
    }
}

    private bool CheckDuplicatePR(DataTable dt, string value1)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Authority"].ToString() == value1)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {

        }
        return retVal;
    }

    protected DataTable setPRGridViewDataset(DataTable dtt, string TabNametwo)
    {
        dtt.TableName.Equals(TabNametwo);
        dtt.Columns.Add("SRNO");
        dtt.Columns.Add("AuthorityNo");
        dtt.Columns.Add("Authority");
        dtt.Columns["SRNO"].AutoIncrement = true; dtt.Columns["SRNO"].AutoIncrementSeed = 1; dtt.Columns["SRNO"].AutoIncrementStep = 1;
        return dtt;
    }
    protected void btnDeleteEN_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (ViewState["dtPR"] != null && ((DataTable)ViewState["dtPR"]) != null)
            {
                DataTable dtM = (DataTable)ViewState["dtPR"];
                dtM.Rows.Remove(this.GetEditableDatarow(dtM, btnDelete.CommandArgument));
                ViewState["dtPR"] = dtM;
                if (dtM.Rows.Count > 0)
                {
                    lvApplvl.DataSource = dtM;
                    lvApplvl.DataBind();
                }
                else
                {
                    lvApplvl.DataSource = null;
                    lvApplvl.DataBind();
                    pnlAl.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "IO_OutwardDispatch.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlDepartment.SelectedIndex = 0;
        lstPost.ClearSelection();
        lstPost.Items.Clear();
        ddlApproval.SelectedIndex = 0;
        ddlUser.SelectedIndex = 0;
        dtPR = (DataTable)ViewState["dtPR"];
        if (dtPR != null)
        {
            dtPR.Clear();
        }
        pnlAl.Visible = false;
        ddlDepartment.Enabled = true;
        ddlUser.Enabled = true;
        ViewState["REQ_PANO"] = null;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objRequisition.PANO1 = 0;
            objRequisition.PANO2 = 0;
            objRequisition.PANO3 = 0;
            objRequisition.PANO4 = 0;
            objRequisition.PANO5 = 0;
            int REQ_PANO = 0;
            string status1 = Request.Form["switch"];
            if (ViewState["REQ_PANO"] != null)
            {
                REQ_PANO = Convert.ToInt32(ViewState["REQ_PANO"].ToString());
            }
            objRequisition.REQ_PANO = REQ_PANO;
            objRequisition.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            objRequisition.USERNO = Convert.ToInt32(ddlUser.SelectedValue);
            objRequisition.COLLEGE_CODE = Session["colcode"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                objRequisition.COLLEGE_NO = 0;
            }
            else
            {
                objRequisition.COLLEGE_NO = Convert.ToInt32(values);
            }
            objRequisition.CREATEDBY = Convert.ToInt32(Session["userno"]);
            objRequisition.MODIFIEDBY = Convert.ToInt32(Session["userno"]);
            if (!string.IsNullOrEmpty(status1) && status1 == "on")
            {
                objRequisition.IsActive = true;
            }
            else
            {
                objRequisition.IsActive = false;
            }

            StringBuilder selectedValues = new StringBuilder();

            // Iterate through the selected items in the list box
            foreach (ListItem item in lstPost.Items)
            {
                // Append the selected item followed by a comma
                if (item.Selected)
                {
                    selectedValues.Append(item.Value);
                    selectedValues.Append(",");
                }
            }

            // Remove the trailing comma if there are selected items
            if (selectedValues.Length > 0)
            {
                selectedValues.Length--; // Remove the last comma
            }

            // Store the result in a variable
            objRequisition.POSTNO = selectedValues.ToString();

            //-----------------------//----------------------------//

            StringBuilder selectedItems = new StringBuilder();

            // Iterate through the selected items in the list box
            foreach (ListItem item in lstPost.Items)
            {
                // Append the selected item followed by a comma
                if (item.Selected)
                {
                    selectedItems.Append(item.ToString());
                    selectedItems.Append(",");
                }
            }

            // Remove the trailing comma if there are selected items
            if (selectedItems.Length > 0)
            {
                selectedItems.Length--; // Remove the last comma
            }

            // Store the result in a variable
            objRequisition.POST = selectedItems.ToString();

            dtPR = (DataTable)ViewState["dtPR"];

            if (dtPR.Rows.Count == 5)
            {
                objRequisition.PANO1 = Convert.ToInt32(dtPR.Rows[0]["AuthorityNo"]);
                objRequisition.PANO2 = Convert.ToInt32(dtPR.Rows[1]["AuthorityNo"]);
                objRequisition.PANO3 = Convert.ToInt32(dtPR.Rows[2]["AuthorityNo"]);
                objRequisition.PANO4 = Convert.ToInt32(dtPR.Rows[3]["AuthorityNo"]);
                objRequisition.PANO5 = Convert.ToInt32(dtPR.Rows[4]["AuthorityNo"]);
            }
            else if (dtPR.Rows.Count == 4)
            {
                objRequisition.PANO1 = Convert.ToInt32(dtPR.Rows[0]["AuthorityNo"]);
                objRequisition.PANO2 = Convert.ToInt32(dtPR.Rows[1]["AuthorityNo"]);
                objRequisition.PANO3 = Convert.ToInt32(dtPR.Rows[2]["AuthorityNo"]);
                objRequisition.PANO4 = Convert.ToInt32(dtPR.Rows[3]["AuthorityNo"]);
            }
            else if (dtPR.Rows.Count == 3)
            {
                objRequisition.PANO1 = Convert.ToInt32(dtPR.Rows[0]["AuthorityNo"]);
                objRequisition.PANO2 = Convert.ToInt32(dtPR.Rows[1]["AuthorityNo"]);
                objRequisition.PANO3 = Convert.ToInt32(dtPR.Rows[2]["AuthorityNo"]);
            }
            else if (dtPR.Rows.Count == 2)
            {
                objRequisition.PANO1 = Convert.ToInt32(dtPR.Rows[0]["AuthorityNo"]);
                objRequisition.PANO2 = Convert.ToInt32(dtPR.Rows[1]["AuthorityNo"]);
            }
            else if (dtPR.Rows.Count == 1)
            {
                objRequisition.PANO1 = Convert.ToInt32(dtPR.Rows[0]["AuthorityNo"]);
            }
            else
            {
                MessageBox("Please Add Atleast One Approval Level");
                return;
            }

            bool result = CheckReqAppLvl();

            if (result == true)
            {
                MessageBox("Record Already Exist!");
                //Clear();
                return;
            }

            CustomStatus cs = (CustomStatus)objReq.AddUpdateReqAppLevelData(objRequisition, 0);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                ViewState["action"] = null;
                Clear();
                MessageBox("Record Saved Successfully!");
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                ViewState["action"] = null;
                Clear();
                MessageBox("Record Updated Successfully!");
            }
            BindReqApprLvl();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void ddlPA01_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //this.EnableDisable(1);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA01_click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPA02_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //this.EnableDisable(2);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA02_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPA03_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //this.EnableDisable(3);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA03_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPA04_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // this.EnableDisable(4);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA04_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlPA05_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //this.EnableDisable(5);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACCOUNT_Master_AccountPassingAuthorityPath.ddlPA05_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindReqApprLvl()
    {
        objRequisition.REQ_PANO = 0;
        try
        {
            DataSet ds = objReq.GetAllRequisitionApprLevel(objRequisition);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRecApprLvl.DataSource = ds;
                lvRecApprLvl.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Scale.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowDetails(Int32 REQ_PANO)
    {
        DataSet ds = new DataSet();
        objRequisition.REQ_PANO = REQ_PANO;
        string status1 = Request.Form["switch"];
        try
        {
            ds = objReq.GetAllRequisitionApprLevel(objRequisition);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["REQ_PANO"] = REQ_PANO;
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString();
                FillUser();
                ddlUser.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                Boolean Isactive = Convert.ToBoolean(ds.Tables[0].Rows[0]["ISACTIVE"].ToString());
                if (Isactive == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatRecUser(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatRecUser(false);", true);
                }
                string postno = ds.Tables[0].Rows[0]["POST_NO"].ToString();
                string[] valuesArray = postno.Split(',');
                FillPost();
                // Bind the values to the ListBox
                foreach (string value in valuesArray)
                {
                    ListItem item = lstPost.Items.FindByValue(value);
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                }
                lvApplvl.DataSource = ds.Tables[1];
                ViewState["dtPR"] = ds.Tables[1];
                lvApplvl.DataBind();
                lvApplvl.Visible = true;
                pnlAl.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int REQ_PANO = int.Parse(btnEdit.CommandArgument);
        ShowDetails(REQ_PANO);
        ddlDepartment.Enabled = false;
        ddlUser.Enabled = false;
    }

    public bool CheckReqAppLvl()
    {
        bool result = false;
        DataSet ds = new DataSet();

        ds = objCommon.FillDropDown("REC_REQUISITION_APPROVAL_LEVELS", "*", "", "DEPTNO= '" + Convert.ToInt32(ddlDepartment.SelectedValue) + "' and UA_NO='" + Convert.ToInt32(ddlUser.SelectedValue) + "' AND ISACTIVE = 1 AND REQ_PANO != '" + Convert.ToInt32(ViewState["REQ_PANO"]) + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    private DataRow GetEditableDatarow(DataTable dtM, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dtM.Rows)
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
                objCommon.ShowError(Page, "Leave_Application.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }
}
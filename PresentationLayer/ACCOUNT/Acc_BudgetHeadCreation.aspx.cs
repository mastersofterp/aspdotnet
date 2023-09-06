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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.NITPRM;


public partial class ACCOUNT_Acc_BudgetHeadCreation : System.Web.UI.Page
{

    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();
    budgetHead objBudgetHead = new budgetHead();
    budgetHeadController objBudgetHeadController = new budgetHeadController();
    string _CCMS = ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");

                }
                else
                {
                    Session["comp_set"] = "";
                }


                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                GetDepartment();
                PopulateDropDown();
                DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
                populatetreeview(dtPRNO, 0, null);
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {


            //if (abc > 0)
            //{

            //    objCommon.DisplayUserMessage(UPDMainGroup, "Budget Code Already Exist...!", this);
            //    btnSubmit.Text = "Submit";
            //    // lblStatus.Visible = true;
            //    // lblStatus.Text = "Budget Code Already Exist...!";
            //    //Response.Write("Budget Code Already Exist...!");
            //    return;
            //}

            //else
            //{
            objBudgetHead.BUDG_CODE = txtBudCode.Text.ToUpper();
            objBudgetHead.BUDG_NAME = txtbudgetName.Text.ToUpper();
            objBudgetHead.BUDG_PRNO = Convert.ToInt32(ddlBudget.SelectedValue);
            objBudgetHead.COLLEGE_CODE = Session["colcode"].ToString();
            string code_year = Session["comp_code"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Chk Group name    
                    int abc = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "count(1)", "Budg_Code='" + txtBudCode.Text + "'"));
                    if (abc > 0)
                    {

                        objCommon.DisplayUserMessage(UPDMainGroup, "Budget Code Already Exist...!", this);
                        btnSubmit.Text = "Submit";
                        return;
                    }
                    else
                    {
                        string IsExist = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NO", "UPPER(BUDG_NAME)='" + txtbudgetName.Text.Trim().ToUpper() + "' and UPPER(BUDG_CODE)='" + txtBudCode.Text + "'");
                        if (IsExist != string.Empty)
                        {
                            objCommon.DisplayMessage(UPDMainGroup, "Group Already Exist.", this);
                            return;
                        }
                        objBudgetHead.BUDG_NO = 0;
                        CustomStatus cs = (CustomStatus)objBudgetHeadController.AddUpdateBudgetName(objBudgetHead, code_year, Convert.ToInt32(ddlDept.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            Clear();
                            PopulateDropDown();
                            tvHierarchy.Nodes.Clear();

                            DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
                            populatetreeview(dtPRNO, 0, null);
                            tvHierarchy.ExpandAll();
                            //lblStatus.Text = "Record Saved Successfully!!!";
                            objCommon.DisplayMessage(UPDMainGroup, "Record Saved Successfully!!!", this);
                        }
                        else
                            lblStatus.Text = "Server Error!!!";
                    }
                }
                else
                {
                    if (ViewState["id"] != null)
                    {
                        objBudgetHead.BUDG_NO = Convert.ToInt32(ViewState["id"].ToString());

                        CustomStatus cs = (CustomStatus)objBudgetHeadController.AddUpdateBudgetName(objBudgetHead, code_year, Convert.ToInt32(ddlDept.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            PopulateDropDown();
                            tvHierarchy.Nodes.Clear();

                            DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
                            populatetreeview(dtPRNO, 0, null);
                            tvHierarchy.ExpandAll();
                            //lblStatus.Text = "Record Updated Successfully!!!";
                            objCommon.DisplayMessage(UPDMainGroup, "Record Updated Successfully!!!", this);
                        }
                        else
                            lblStatus.Text = "Server Error!!!";

                    }
                }
            }
            txtBudCode.Focus();
        }
        //}
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Acc_BudgetHeadCreation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void ddlBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlBudget.SelectedValue) > 0)
        //{
        //    int Dept = Convert.ToInt32(objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "ISNULL(BUDG_DEPT,0)", "BUDG_NO=" + ddlBudget.SelectedValue));
        //    if (Dept == 0)
        //        GetDepartment();
        //    else
        //        ddlDept.SelectedValue = Dept.ToString();
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        PopulateDropDown();
        tvHierarchy.DataBind();
        //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=0", "").Tables[0];
        //populatetreeview(dtPRNO, 0, null);
    }

    protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        //    try
        //    {
        //        //very important 
        //        string id = Request.Form[lstGroup.UniqueID].ToString();

        //        if (id != "" | id != string.Empty)
        //        {
        //            Clear();
        //            ViewState["action"] = "edit";
        //            ViewState["id"] = id.ToString();

        //            //Show Details 
        //            MainGroupController objMGC = new MainGroupController();
        //            string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

        //            DataTableReader dtr = objMGC.GetMainGroup(Convert.ToInt32(id), code_year);
        //            if (dtr.Read())
        //            {
        //                txtGroupName.Text = dtr["MGRP_NAME"].ToString();
        //                ddlParentGroup.SelectedValue = dtr["PRNO"].ToString();
        //                ddlFAHead.SelectedValue = dtr["FA_NO"].ToString();
        //                txtAccCode.Text = dtr["ACC_CODE"] == DBNull.Value ? string.Empty : dtr["ACC_CODE"].ToString();
        //            }
        //            dtr.Close();

        //        }
        //        else
        //        {
        //            ViewState["action"] = "add";
        //            ViewState["id"] = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (Convert.ToBoolean(Session["error"]) == true)
        //            objUCommon.ShowError(Page, "Account_maingroup.lstGroup_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
        //        else
        //            objUCommon.ShowError(Page, "Server UnAvailable");
        //    }
    }

    #region User Defined Methods

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/maingroup.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
                }
            }
        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=Acc_BudgetHeadCreation.aspx");
            }
        }
    }

    private void GetDepartment()
    {
        DataSet ds = objBudgetHeadController.FetchDepartment(_CCMS);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDept.DataSource = ds;
            ddlDept.DataTextField = "SUBDEPT";
            ddlDept.DataValueField = "SUBDEPTNO";
            ddlDept.DataBind();
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            string[] database = _CCMS.ToString().Trim().Split(';');
            string DatabaseName = database[3].ToString().Split('=')[1].ToString();

            objCommon.FillDropDownList(ddlBudget, "ACC_" + Session["comp_code"].ToString().Trim() + "_BUDGET_HEAD", "BUDG_NO", "BUDG_NAME", string.Empty, "BUDG_NAME");
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_BUDGET_HEAD A", "A.BUDG_NO, A.BUDG_CODE", "A.BUDG_NAME", string.Empty, "BUDG_NAME");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvbudgethead.DataSource = ds.Tables[0];
                    lvbudgethead.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_maingroup.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtBudCode.Text = string.Empty;
        txtbudgetName.Text = string.Empty;
        ddlBudget.SelectedValue = "0";
        lblStatus.Text = "";
        ViewState["action"] = "add";
        PopulateDropDown();
        txtBudCode.Enabled = true;
    }
    #endregion

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NO", "BUDG_CODE,BUDG_NAME", "BUDG_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "BUDG_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvbudgethead.DataSource = ds;
                lvbudgethead.DataBind();

            }
        }
        txtSearch.Focus();
    }

    private void populatetreeview(DataTable dt, int ParentID, TreeNode TreeNode)
    {
        foreach (DataRow row in dt.Rows)
        {
            TreeNode treeChild = new TreeNode()
            {
                Text = row["BUDG_NAME"].ToString(),
                Value = row["BUDG_NO"].ToString()
            };
            if (ParentID == 0)
            {
                tvHierarchy.Nodes.Add(treeChild);
                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
            else
            {
                TreeNode.ChildNodes.Add(treeChild);

                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_NAME", "BUDG_NO", "BUDG_PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
        }
    }
    protected void lvbudgethead_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            DataSet ds = objCommon.FillDropDown("Acc_" + Session["comp_code"].ToString() + "_BUDGET_HEAD", "BUDG_CODE", "BUDG_NAME,BUDG_PRNO", "BUDG_NO=" + e.CommandArgument.ToString(), "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBudCode.Text = ds.Tables[0].Rows[0]["BUDG_CODE"].ToString();
                txtBudCode.Enabled = false;
                txtbudgetName.Text = ds.Tables[0].Rows[0]["BUDG_NAME"].ToString();
                ddlBudget.SelectedValue = ds.Tables[0].Rows[0]["BUDG_PRNO"].ToString();
                //ddlDept.SelectedValue = ds.Tables[0].Rows[0]["BUDG_DEPT"].ToString();
                ViewState["id"] = e.CommandArgument;
                ViewState["action"] = "edit";
            }
        }
    }

    public Control updBank { get; set; }

    public Control updBudgetCode { get; set; }
}

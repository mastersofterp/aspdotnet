//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : FINAL ACCOUNT GROUP                                             
// CREATION DATE : 02-SEPTEMBER-2009                                               
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

using System.Data.SqlClient;
using IITMS.NITPRM;


public partial class Account_maingroup : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");
            }
            else
            {

                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {

            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ViewState["action"] = "add";
            txtGroupName.Focus();
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
                //Load Page Help

                PopulateDropDown();
                DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=0", "").Tables[0];
                populatetreeview(dtPRNO, 0, null);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlParentGroup.SelectedIndex == 0 && ddlFAHead.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(UPDMainGroup, "Please Select Parent Group Or Final Account Head.", this);
                return;
            }

            MainGroupController objMGC = new MainGroupController();
            MainGroup objMainGroup = new MainGroup();
            objMainGroup.Mgrp_Name = txtGroupName.Text.Trim().ToUpper();
            objMainGroup.Pr_No = Convert.ToInt16(ddlParentGroup.SelectedValue);
            objMainGroup.Fa_No = Convert.ToInt16(ddlFAHead.SelectedValue);
            objMainGroup.Acc_Code = txtAccCode.Text.Trim().ToUpper();
            objMainGroup.College_Code = Session["colcode"].ToString();

            DataSet dsgrp = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "*", "", "MGRP_NAME='" + ddlParentGroup.SelectedItem.Text.ToString().Trim() + "'", "");
            if (dsgrp != null)
            {
                if (dsgrp.Tables[0].Rows.Count > 0)
                {
                    objMainGroup.Payment_type = dsgrp.Tables[0].Rows[0]["PAYMENT_TYPE_NO"].ToString().Trim();
                }
                else
                {
                    objMainGroup.Payment_type = "3";
                    //objCommon.DisplayMessage(UPDMainGroup, "Payment type no error occured.", this);
                    //return;
                }
            }
            else
            {
                objMainGroup.Payment_type = "0";
                //objCommon.DisplayMessage(UPDMainGroup, "Payment type no error occured.", this);
                //return;
            }


            string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Chk Group name
                    string IsExist = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "UPPER(MGRP_NAME)='" + txtGroupName.Text.Trim().ToUpper() + "'");
                    if (IsExist != string.Empty)
                    {
                        objCommon.DisplayMessage(UPDMainGroup, "Group Already Exist.", this);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objMGC.AddMainGroup(objMainGroup, code_year);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        PopulateDropDown();
                        tvHierarchy.Nodes.Clear();

                        DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=0", "").Tables[0];
                        populatetreeview(dtPRNO, 0, null);
                        tvHierarchy.ExpandAll();
                        //lblStatus.Text = "Record Saved Successfully!!!";
                        objCommon.DisplayMessage(UPDMainGroup, "Record Saved Successfully!!!", this);
                    }
                    else
                        lblStatus.Text = "Server Error!!!";
                }
                else
                {
                    if (ViewState["id"] != null)
                    {
                        objMainGroup.Mgrp_No = Convert.ToInt16(ViewState["id"]);
                        string MGRP_NOS = "1,30000,10001,10002,10003,10004,10005,10006,10007,10008,10009,10010,10011,10012,10013,10014,10015,10016,10017,10018,10019,10020,10021,10022,10023,10024,10025,10026,10027,10028";
                        if (MGRP_NOS.Contains(ViewState["id"].ToString()) == true)
                        {
                            objCommon.DisplayUserMessage(UPDMainGroup, "This Final account group can not update", this.Page);
                            return;
                        }

                        CustomStatus cs = (CustomStatus)objMGC.UpdateMainGroup(objMainGroup, code_year);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            PopulateDropDown();
                            tvHierarchy.Nodes.Clear();

                            DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=0", "").Tables[0];
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
            txtGroupName.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_maingroup.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        PopulateDropDown();

        //tvHierarchy.Nodes.Clear();
        //tvHierarchy.ExpandDepth = 0;

        //DataTable dtPRNO = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=0", "").Tables[0];
        //populatetreeview(dtPRNO, 0, null);
        tvHierarchy.DataBind();
        //Response.Redirect(Request.Url.ToString());
        //txtAccCode.Text="";
        //txtGroupName.Text = "";
        //ddlFAHead.SelectedIndex = 0;
        //ddlParentGroup.SelectedIndex = 0;
        //txtGroupName.Focus();
    }

    protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //very important 
            string id = Request.Form[lstGroup.UniqueID].ToString();

            if (id != "" | id != string.Empty)
            {
                Clear();
                ViewState["action"] = "edit";
                ViewState["id"] = id.ToString();

                //Show Details 
                MainGroupController objMGC = new MainGroupController();
                string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

                DataTableReader dtr = objMGC.GetMainGroup(Convert.ToInt32(id), code_year);
                if (dtr.Read())
                {
                    txtGroupName.Text = dtr["MGRP_NAME"].ToString();
                    ddlParentGroup.SelectedValue = dtr["PRNO"].ToString();
                    ddlFAHead.SelectedValue = dtr["FA_NO"].ToString();
                    txtAccCode.Text = dtr["ACC_CODE"] == DBNull.Value ? string.Empty : dtr["ACC_CODE"].ToString();
                }
                dtr.Close();

            }
            else
            {
                ViewState["action"] = "add";
                ViewState["id"] = null;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_maingroup.lstGroup_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region User Defined Methods
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
    //    }
    //}
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=maingroup.aspx");
        }
    }



    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlParentGroup, "ACC_" + Session["comp_code"].ToString().Trim() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", string.Empty, "MGRP_NAME");
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", string.Empty, "MGRP_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstGroup.DataTextField = "MGRP_NAME";
                    lstGroup.DataValueField = "MGRP_NO";
                    lstGroup.DataSource = ds.Tables[0];
                    lstGroup.DataBind();
                }
            }

            objCommon.FillDropDownList(ddlFAHead, "ACC_" + Session["comp_code"].ToString() + "_FINAL_ACCOUNT_HEADS", "FA_NO", "FA_NAME", string.Empty, "FA_NAME");

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
        txtGroupName.Text = string.Empty;
        txtAccCode.Text = string.Empty;
        ddlParentGroup.SelectedIndex = 0;
        ddlFAHead.SelectedIndex = 0;
        lstGroup.SelectedIndex = -1;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
        ViewState["id"] = null;
        txtGroupName.Focus();
        ddlParentGroup.Enabled = true;
        ddlFAHead.Enabled = true;
    }
    #endregion

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NO", "MGRP_NAME", "MGRP_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "MGRP_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstGroup.DataTextField = "MGRP_NAME";
                lstGroup.DataValueField = "MGRP_NO";
                lstGroup.DataSource = ds.Tables[0];
                lstGroup.DataBind();

            }
            else
            {
                objCommon.DisplayMessage(UPDMainGroup, "Record Not Found!", this);

            }
        }

        txtSearch.Focus();


    }

    protected void ddlParentGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParentGroup.SelectedIndex > 0)
        {
            ddlFAHead.SelectedIndex = 0;
            ddlFAHead.Enabled = false;

        }
        else if (ddlParentGroup.SelectedIndex == 0)
            ddlFAHead.Enabled = true;
    }
    protected void ddlFAHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFAHead.SelectedIndex > 0)
        {
            ddlParentGroup.SelectedIndex = 0;
            ddlParentGroup.Enabled = false;

        }
        else if (ddlFAHead.SelectedIndex == 0)
            ddlParentGroup.Enabled = true;
    }

    private void populatetreeview(DataTable dt, int ParentID, TreeNode TreeNode)
    {
        foreach (DataRow row in dt.Rows)
        {
            TreeNode treeChild = new TreeNode()
            {
                Text = row["MGRP_NAME"].ToString(),
                Value = row["MGRP_NO"].ToString()
            };
            if (ParentID == 0)
            {
                tvHierarchy.Nodes.Add(treeChild);
                //tvHierarchy.DataBind();
                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
            else
            {
                TreeNode.ChildNodes.Add(treeChild);

                DataTable dtChild = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString() + "_MAIN_GROUP", "MGRP_NAME", "MGRP_NO", "PRNO=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
        }
    }
}

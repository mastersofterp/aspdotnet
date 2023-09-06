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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class BudgetHead_new : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CustomStatus CS = new CustomStatus();
    //BudgetHeadEntity onjEnt = new BudgetHeadEntity();
    BudgetHeadEntity objEnt = new BudgetHeadEntity();
    Acc_BudgetHead_newController objCon = new Acc_BudgetHead_newController();


    //static int bankId = 0;
    #region PageLoad Event

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
            //Response.Redirect("Default.aspx");
            Response.Redirect("~/Default.aspx");

        }
        objCommon = new Common();
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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    // objCommon.DisplayUserMessage(updBank, "Select company/cash book.", this);

                    //Response.Redirect("~/Account/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";


                    //   divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                }
                BindTreenode();
                // DataTable dtPRNO = null;
                // dtPRNO = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "PARENT_ID=0", "").Tables[0];
                // populatetreeview(dtPRNO, 0, null);
            }

            ViewState["action"] = "add";
            FillDropdown();
            rdbBudgetProposal.Items[0].Selected = true;

        }

      //  rdbBudgetProposal.SelectedValue = "1";

    }

    #endregion


    #region Private Methods

    protected void BindListView()
    {
        //DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "*" + "," + "BUDGET_HEAD", "", "");

        //     if (ds.Tables.Count > 0)
        //{
        //     if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        lvbudgethead.DataSource = ds;
        //        lvbudgethead.DataTextField ="BUDGET_HEAD";
        //        lvbudgethead.DataValueField = "BUDGET_NO";
        //        lvbudgethead.DataBind();
        //    }
        //}
    }

    protected void FillDropdown()
    {
        // DataSet ds = objCon.BIND_DROPDOWN();
        // ddlParentBudget.DataSource = ds;
        // ddlParentBudget.DataTextField = "BUDGET_HEAD";
        // ddlParentBudget.DataBind();
        //lvbudgethead.DataSource = ds;
        //  lvbudgethead.DataTextField ="BUDGET_HEAD";
        //lvbudgethead.DataValueField = "BUDGET_NO";
        //lvbudgethead.DataBind();
        objCommon.FillDropDownList(ddlParentBudget, "ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "", "");
        DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "", "");
        lvbudgethead.DataSource = ds;
        lvbudgethead.DataTextField = "BUDGET_HEAD";
        lvbudgethead.DataValueField = "BUDGET_NO";
        lvbudgethead.DataBind();
        BindSerialNo();
    }

    private void BindSerialNo() {
        HdnSerial.Value = objCommon.LookUp("ACC_BUDGET_HEAD_NEW", "ISNULL(COUNT(SERIAL_NO),0)+1", "PARENT_ID=0");
        txtSerialno.Text = HdnSerial.Value;
    }
    private void BindTreenode()
    {
        DataTable dtPRNO = null;
        dtPRNO = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "PARENT_ID=0", "").Tables[0];
        populatetreeview(dtPRNO, 0, null);
    }

    private void populatetreeview(DataTable dt, int ParentID, TreeNode TreeNode)
    {
        foreach (DataRow row in dt.Rows)
        {
            TreeNode treeChild = new TreeNode()
            {
                Text = row["BUDGET_HEAD"].ToString(),
                Value = row["BUDGET_NO"].ToString()
            };
            if (ParentID == 0)
            {
                tvHierarchy.Nodes.Add(treeChild);
                DataTable dtChild = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "PARENT_ID=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
            else
            {
                TreeNode.ChildNodes.Add(treeChild);

                DataTable dtChild = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "PARENT_ID=" + treeChild.Value, "").Tables[0];
                populatetreeview(dtChild, Convert.ToInt32(treeChild.Value), treeChild);
            }
        }
    }

    public void Clear()
    {
        txtBudgetHead.Text = string.Empty;
        txtBudgetShortName.Text = string.Empty;
        ddlParentBudget.SelectedIndex = 0;
        rdbBudgetProposal.ClearSelection();
        rdbBudgetProposal.Items[0].Selected = true;
        txtSearch.Text = string.Empty;
        txtSerialno.Text = string.Empty;
        ViewState["action"] = "add";
        rdoRecruit.ClearSelection();
        hdnparty.Value = "0";
        txtAcc.Text = String.Empty;
        FillDropdown();
        txtBudgetShortName.Enabled = true;
        ViewState["ParentBudget"] = null;
    }

    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAccount(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            prefixText = prefixText.ToUpper();
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetAccountEntryCashBank(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["PARTY_NAME"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }
    #endregion

    #region Events

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString() == "add")
            {
                objEnt.BUDGET_NO = 0;
                objEnt.PARENT_ID = Convert.ToInt32(ddlParentBudget.SelectedValue);
                objEnt.BUDGET_CODE = txtBudgetShortName.Text;
                objEnt.BUDGET_HEAD = txtBudgetHead.Text;
                // objEnt.BUDGET_PRAPOSAL = rdbBudgetProposal.SelectedIndex + 1;
                objEnt.BUDGET_PRAPOSAL = Convert.ToInt32(rdbBudgetProposal.SelectedValue);
                objEnt.COLLEGE_CODE = Convert.ToInt32(Session["colcode"].ToString());
                objEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
                objEnt.SERIAL_NO = txtSerialno.Text;
                objEnt.RECURRING = rdoRecruit.SelectedValue;
                objEnt.PARTYNO = Convert.ToInt32(hdnparty.Value);

                CustomStatus cs = (CustomStatus)objCon.AddUpd_BudgetHead(objEnt);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayUserMessage(UPBudget,"This Budget Head is already Exists", this.Page);
                    // return;
                }
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayUserMessage(UPBudget,"Budget Head Been Saved Successfully", this.Page);
                    //  return;

                }
            }
            else
            {
                objEnt.BUDGET_NO = Convert.ToInt32(Session["Budget_no"]);
                //objEnt.PARENT_ID = ddlParentBudget.SelectedIndex; Session["Budget_no"] = Convert.ToInt32(lvbudgethead.SelectedIndex) + 1;
                objEnt.PARENT_ID = Convert.ToInt32(ddlParentBudget.SelectedValue);
                objEnt.BUDGET_CODE = txtBudgetShortName.Text;
                objEnt.BUDGET_HEAD = txtBudgetHead.Text;
                // objEnt.BUDGET_PRAPOSAL = rdbBudgetProposal.SelectedIndex + 1;
                objEnt.BUDGET_PRAPOSAL = Convert.ToInt32(rdbBudgetProposal.SelectedValue);
                objEnt.COLLEGE_CODE = Convert.ToInt32(Session["colcode"].ToString());
                objEnt.CREATED_BY = Convert.ToInt32(Session["userno"]);
                objEnt.SERIAL_NO = txtSerialno.Text;
                objEnt.RECURRING = rdoRecruit.SelectedValue;
                objEnt.PARTYNO = Convert.ToInt32(hdnparty.Value);

                CustomStatus cs = (CustomStatus)objCon.AddUpd_BudgetHead(objEnt);
                if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayUserMessage(UPBudget,"This Budget Head is already Exists.", this.Page);
                    //return;
                }
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayUserMessage(UPBudget,"Budget Head Been Updated Successfully.", this.Page);
                    //  return;
                }
                ViewState["action"] = "add";
            }
            BindTreenode();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BudgetHead_new.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        // FillDropdown();
        Clear();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        //DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_HEAD", "BUDGET_NO", "BUDGET_HEAD like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%'", "");
        //lvbudgethead.DataSource = ds;
        //lvbudgethead.DataTextField = "BUDGET_HEAD";
        //lvbudgethead.DataValueField = "BUDGET_NO";
        //lvbudgethead.DataBind();


    }

    protected void lvbudgethead_SelectedIndexChanged(object sender, EventArgs e)
    {
        // DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "PARENT_ID," + "BUDGET_CODE," + "BUDGET_HEAD," + "PARENT_ID," + "SERIAL_NO,"+ "BUDGET_PRAPOSAL,"+"BUDGET_NO=" + lvbudgethead.SelectedValue + "", "");
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //   if
        //}

        try
        {

            //objEnt.BUDGET_NO = Convert.ToInt32(lvbudgethead.SelectedIndex) + 1; // commented by gopal anthati on 02/11/2021
            objEnt.BUDGET_NO = Convert.ToInt32(lvbudgethead.SelectedValue);
            string Comp_Code = Session["comp_code"].ToString();   // Added by Akshay on 09-05-2022

            DataSet ds = objCon.GET_BUDGETHEAD_BY_BUDGET_NO(objEnt, Comp_Code);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtBudgetHead.Text = ds.Tables[0].Rows[0]["BUDGET_HEAD"].ToString();
                    txtBudgetShortName.Text = ds.Tables[0].Rows[0]["BUDGET_CODE"].ToString();
                    ViewState["BudgetCode"] = ds.Tables[0].Rows[0]["BUDGET_CODE"].ToString();

                

                    txtSerialno.Text = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();
                    ViewState["SerialNo"] = ds.Tables[0].Rows[0]["SERIAL_NO"].ToString();

                    ddlParentBudget.SelectedValue = ds.Tables[0].Rows[0]["PARENT_ID"].ToString();
                    ViewState["ParentBudget"] = ds.Tables[0].Rows[0]["PARENT_ID"].ToString();

                    //rdbBudgetProposal.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_PRAPOSAL"].ToString();
                    rdbBudgetProposal.SelectedValue = ds.Tables[0].Rows[0]["BUDGET_PRAPOSAL"].ToString();
                    hdnparty.Value = ds.Tables[0].Rows[0]["PARTY_NO"].ToString();
                    //if (ddlParentBudget.SelectedIndex > 0)
                    //{
                    //    txtSerialno.Enabled = false;
                    //}
                    //else
                        
                    //{
                    //    txtSerialno.Enabled = true;
                    //}
                    if (ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != string.Empty && ds.Tables[0].Rows[0]["PARTY_NAME"].ToString() != "*")
                    {
                        txtAcc.Text = ds.Tables[0].Rows[0]["PARTY_NAME"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["REC_NONREC"].ToString() != string.Empty)
                    {
                        rdoRecruit.SelectedValue = ds.Tables[0].Rows[0]["REC_NONREC"].ToString();
                    }
                    //Session["Budget_no"] = Convert.ToInt32(lvbudgethead.SelectedIndex) + 1; // commented by gopal anthati on 02/11/2021
                    Session["Budget_no"] = Convert.ToInt32(lvbudgethead.SelectedValue);
                    txtBudgetShortName.Enabled = false;
                }
                ViewState["action"] = "edit";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BudgetHead_new.btnsubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    //public void radiobtnchk(string s)
    //{
    //    if (s == "1")
    //    {
    //        rdbBudgetProposal.SelectedValue = "1";
    //    }
    //    else
    //        rdbBudgetProposal.SelectedValue = "2";
    //}
    //protected void txtSearch_TextChanged1(object sender, EventArgs e)
    //{

    //    DataSet ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW", "BUDGET_NO", "BUDGET_HEAD", "BUDGET_HEAD like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' ", "BUDGET_HEAD");
    //    if (ds.Tables.Count > 0)
    //    {
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            lvbudgethead.DataTextField = "MGRP_NAME";
    //            lvbudgethead.DataValueField = "MGRP_NO";
    //            lvbudgethead.DataSource = ds.Tables[0];
    //            lvbudgethead.DataBind();

    //        }
    //    }

    //    txtSearch.Focus();
    //}

    protected void txtAcc_TextChanged(object sender, EventArgs e)
    {
        string[] Party = txtAcc.Text.Trim().Split('*');
        if (Party.Length > 1)
        {
            hdnparty.Value = objCommon.LookUp("ACC_" + Session["comp_code"].ToString() + "_PARTY", "PARTY_NO", "ACC_CODE='" + Party[1] + "'");
        }
    }
    #endregion
    protected void ddlParentBudget_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        try
        {
            if (ddlParentBudget.SelectedIndex > 0)
            {
               
               if (ViewState["ParentBudget"].ToString() == ddlParentBudget.SelectedValue)
               {
                   txtBudgetShortName.Text = ViewState["BudgetCode"].ToString();
                   txtSerialno.Text = ViewState["SerialNo"].ToString();
               }
               else
               {
                   ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW BH LEFT JOIN ACC_BUDGET_HEAD_NEW BHN ON(BH.BUDGET_NO = BHN.PARENT_ID AND BHN.PARENT_ID=" + Convert.ToInt32(ddlParentBudget.SelectedValue) + ")", "BH.BUDGET_CODE", "count(BHN.PARENT_ID)SRNO", "BH.BUDGET_NO=" + Convert.ToInt32(ddlParentBudget.SelectedValue) + " GROUP BY BH.BUDGET_CODE", "");
                   if (ds.Tables[0].Rows.Count > 0)
                   {
                       txtBudgetShortName.Text = ds.Tables[0].Rows[0]["BUDGET_CODE"].ToString() + "." + (Convert.ToInt32(ds.Tables[0].Rows[0]["SRNO"].ToString()) + 1);
                       txtSerialno.Text = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["SRNO"].ToString()) + 1);
                       //txtSerialno.Enabled = false;
                   }
               }

            }
            else
            {
                txtBudgetShortName.Text = String.Empty;
                txtSerialno.Text = HdnSerial.Value;
                //txtSerialno.Enabled = true;
            }
        }
        catch
        {
            ds = objCommon.FillDropDown("ACC_BUDGET_HEAD_NEW BH LEFT JOIN ACC_BUDGET_HEAD_NEW BHN ON(BH.BUDGET_NO = BHN.PARENT_ID AND BHN.PARENT_ID=" + Convert.ToInt32(ddlParentBudget.SelectedValue) + ")", "BH.BUDGET_CODE", "count(BHN.PARENT_ID)SRNO", "BH.BUDGET_NO=" + Convert.ToInt32(ddlParentBudget.SelectedValue) + " GROUP BY BH.BUDGET_CODE", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtBudgetShortName.Text = ds.Tables[0].Rows[0]["BUDGET_CODE"].ToString() + "." + (Convert.ToInt32(ds.Tables[0].Rows[0]["SRNO"].ToString()) + 1);
                txtSerialno.Text = Convert.ToString(Convert.ToInt32(ds.Tables[0].Rows[0]["SRNO"].ToString()) + 1);
                //txtSerialno.Enabled = false;
            }
        }
    }
}



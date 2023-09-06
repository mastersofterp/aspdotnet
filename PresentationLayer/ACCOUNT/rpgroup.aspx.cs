//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : RECEIPT PAYMENT GROUP                                           
// CREATION DATE : 25-AUGUST-2009                                                  
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
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Account_rpgroup : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
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
                { Session["comp_set"] = ""; }


                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                PopulateDropDown();

                ViewState["action"] = "add";
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ReceiptPaymentController objRPC = new ReceiptPaymentController();
            ReceiptPayment objReceiptPayment = new ReceiptPayment();
            objReceiptPayment.Rp_Name = txtGroupName.Text.Trim().ToUpper();
            if (hdnid.Value.ToString().Trim() != "")
            {
                objReceiptPayment.Rp_No = Convert.ToInt16(hdnid.Value);

            }
            else
            {
                objReceiptPayment.Rp_No = 0;

            }
            objReceiptPayment.RpprNo = Convert.ToInt16(ddlParentGroup.SelectedValue);
            objReceiptPayment.Rph_No = Convert.ToInt16(ddlFAHead.SelectedValue);
            objReceiptPayment.Acc_Code = txtAccCode.Text.Trim();
            objReceiptPayment.College_Code = Session["colcode"].ToString();

            string IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_RECIEPT_PRINT_GROUP", "RP_NAME", "RP_NAME='" + txtGroupName.Text + "'");
            if (IsAvailable != string.Empty)
            {
                objCommon.DisplayUserMessage(UPDRPGROUP, "Group Already Exist", this.Page);
                return;
            }

            string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {

                    //string IsAvailable = objCommon.LookUp("Acc_" + Session["comp_code"] + "_RECIEPT_PRINT_GROUP", "RP_NO", "RP_NAME ='" + txtGroupName.Text + "'");
                    //if (IsAvailable != string.Empty)
                    //{
                    //    objCommon.DisplayUserMessage(UPDRPGROUP, "Category Already Defined", this.Page);
                    //    return;
                    //}


                    CustomStatus cs = (CustomStatus)objRPC.AddReceiptPayment(objReceiptPayment, code_year);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        Clear();
                        PopulateDropDown();
                        //lblStatus.Text = "Record Saved Successfully!!!";
                        objCommon.DisplayMessage(UPDRPGROUP, "Record Saved Successfully!!!", this);
                    }
                    else
                        lblStatus.Text = "Server Error!!!";
                }
                else
                {
                    if (ViewState["id"] != null)
                    {
                        objReceiptPayment.Rp_No = Convert.ToInt16(ViewState["id"]);

                        CustomStatus cs = (CustomStatus)objRPC.UpdateReceiptPayment(objReceiptPayment, code_year);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            Clear();
                            PopulateDropDown();
                            //lblStatus.Text = "Record Updated Successfully!!!";
                            objCommon.DisplayMessage(UPDRPGROUP, "Record Updated Successfully!!!", this);
                        }
                        else
                            lblStatus.Text = "Server Error!!!";

                    }
                }
                txtGroupName.Focus();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_rpgroup.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        Response.Redirect(Request.Url.ToString());
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
                ReceiptPaymentController objRPC = new ReceiptPaymentController();
                string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

                DataTableReader dtr = objRPC.GetReceiptPayment(Convert.ToInt32(id), code_year);
                if (dtr.Read())
                {
                    txtGroupName.Text = dtr["RP_NAME"].ToString();
                    ddlParentGroup.SelectedValue = dtr["RPPRNO"].ToString();
                    hdnid.Value = id;
                    ddlFAHead.SelectedValue = dtr["RPH_NO"].ToString();
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
                objUCommon.ShowError(Page, "Account_rpgroup.lstGroup_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {

                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/rpgroup.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), objCommon.LookUp("ACCESS_LINK", "AL_No", "AL_URL='Account/rpgroup.aspx'"), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }

        }
    }


    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlParentGroup, "ACC_" + Session["comp_code"].ToString().Trim() + "_RECIEPT_PRINT_GROUP", "RP_NO", "RP_NAME", "MODIFIED = 'TRUE' OR MODIFIED IS NULL", "RP_NAME");
            DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_RECIEPT_PRINT_GROUP", "RP_NO", "RP_NAME", "MODIFIED = 'TRUE' OR MODIFIED IS NULL", "RP_NAME");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lstGroup.DataTextField = "RP_NAME";
                    lstGroup.DataValueField = "RP_NO";
                    lstGroup.DataSource = ds.Tables[0];
                    lstGroup.DataBind();
                }
            }

            objCommon.FillDropDownList(ddlFAHead, "ACC_" + Session["comp_code"].ToString().Trim() + "_RECIEPT_PRINT_HEADS", "RPH_NO", "RPH_NAME", string.Empty, "RPH_NAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Account_rpgroup.PopulateCompanyList-> " + ex.Message + " " + ex.StackTrace);
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
    }
    #endregion




    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = objCommon.FillDropDown("ACC_" + Session["comp_code"].ToString().Trim() + "_RECIEPT_PRINT_GROUP", "RP_NO", "RP_NAME", "RP_NAME like '%" + Convert.ToString(txtSearch.Text).Trim().ToUpper() + "%' and (MODIFIED = 'TRUE' OR MODIFIED IS NULL)", "RP_NAME");
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstGroup.DataTextField = "RP_NAME";
                lstGroup.DataValueField = "RP_NO";
                lstGroup.DataSource = ds.Tables[0];
                lstGroup.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(UPDRPGROUP, "Record Not Found!", this);
                return;
            }
        }
      

        txtSearch.Focus();
    }
}

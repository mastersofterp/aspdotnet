//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Configuration.aspx                                                
// CREATION DATE : 03-10-2020                                                        
// CREATED BY    : GOPAL ANTHATI                                                    
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System.Web;
using System.IO;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.Linq;

public partial class STORES_Transactions_Str_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    Str_ConfigurationEnt ObjEnt = new Str_ConfigurationEnt();
    Str_ConfigurationCon ObjCon = new Str_ConfigurationCon();

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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {

                    }                   
                    ViewState["Action"] = "Add";
                    FillDropDown();
                    BindData();
                    ddlCollege.SelectedValue = "1";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_calibration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindData()
    {
        
        DataSet ds = null;       
        ds = objCommon.FillDropDown("STORE_REFERENCE", "*", "", "", "");       
        txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
        txtPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
        txtCodeStandard.Text = ds.Tables[0].Rows[0]["CODE_STANDARD"].ToString();
       // txtPreDsrYear.Text = ds.Tables[0].Rows[0]["PREVIOUS_YEAR_DSR"].ToString();
       // txtCurDsrYear.Text = ds.Tables[0].Rows[0]["CURRENT_YEAR_DSR"].ToString();
        ddlCompStmntAuth.SelectedValue = ds.Tables[0].Rows[0]["COMPARATIVE_STAT_AUTHORITY_UANO"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["COMPARATIVE_STAT_AUTHORITY_UANO"].ToString();
        ddlStoreUser.SelectedValue = ds.Tables[0].Rows[0]["DEPT_STORE_USER"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["DEPT_STORE_USER"].ToString();
        ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
       // ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["CNAME"].ToString();
        rdbSactionAuth.SelectedValue = ds.Tables[0].Rows[0]["SANCTIONING_AUTHORITY"].ToString();
        rdblAvailableQty.SelectedValue = ds.Tables[0].Rows[0]["IsAvailableQty"].ToString(); //08/02/2024
        rdbAuthoritynQuotation.SelectedValue = ds.Tables[0].Rows[0]["IsAuthorityShowOnQuot"].ToString(); //08/02/2024
        ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_SEC_GP_ENTRY"].ToString()==""?"0":ds.Tables[0].Rows[0]["IS_SEC_GP_ENTRY"].ToString()) == 1)
        {
            chkIsSecGPEntry.Checked = true;
        }
        else
        {
            chkIsSecGPEntry.Checked = false;
        }

        //-------Added by shabina 26/09/2022
        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_BUDGET_HEAD"].ToString() == "" ? "0" : ds.Tables[0].Rows[0]["IS_BUDGET_HEAD"].ToString()) == 1)
        {
            chkBudgetHeadReq.Checked = true;
        }
        else
        {
            chkBudgetHeadReq.Checked = false;
        }
        //-------------end------------------//

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_MAIL_SEND"].ToString()==""?"0":ds.Tables[0].Rows[0]["IS_MAIL_SEND"].ToString()) == 1)
        {
            chkMailSend.Checked = true;
        }
        else
        {
            chkMailSend.Checked = false;
        }

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["PO_APPROVAL"].ToString()==""?"0":ds.Tables[0].Rows[0]["PO_APPROVAL"].ToString()) == 1)
        {
            chkPoApproval.Checked = true;
        }
        else
        {
            chkPoApproval.Checked = false;
        }

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["DEPT_WISE_ITEM"].ToString()==""?"0":ds.Tables[0].Rows[0]["DEPT_WISE_ITEM"].ToString()) == 1)
        {
            chkDeptwiseitem.Checked = true;
        }
        else
        {
            chkDeptwiseitem.Checked = false;
        }

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["DSR_CREATION_YES_NO"].ToString()==""?"0":ds.Tables[0].Rows[0]["DSR_CREATION_YES_NO"].ToString()) == 1)
        {
            chkDsrCreation.Checked = true;
        }
        else
        {
            chkDsrCreation.Checked = false;
        }

        if (Convert.ToInt32(ds.Tables[0].Rows[0]["IS_COMPARATIVE_STAT_APPROVAL"].ToString()==""?"0":ds.Tables[0].Rows[0]["IS_COMPARATIVE_STAT_APPROVAL"].ToString()) == 1)
        {
            chkCompSApproval.Checked = true;
        }
        else
        {
            chkCompSApproval.Checked = false;
        }
       
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlCollege,"ACD_COLLEGE_MASTER","COLLEGE_ID","COLLEGE_NAME","","");
        objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "MDNO", "MDNAME", "", "");
        objCommon.FillDropDownList(ddlCompStmntAuth, "USER_ACC", "UA_NO", "UA_FULLNAME", "UA_STATUS=0 AND UA_TYPE IN (1,3,5,7,8)", "");      
        objCommon.FillDropDownList(ddlStoreUser, "STORE_APPROVAL_LEVEL", "APLNO", "APLT", "", "");
        objCommon.FillDropDownList(ddlState, "STORE_STATE", "STATENO", "STATENAME", "", "STATENAME");
    }

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "store_transaction_str_calibration.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ObjEnt.MDNO = Convert.ToInt32(ddlDepartment.SelectedValue);
        ObjEnt.DEPTUSER = Convert.ToInt32(ddlStoreUser.SelectedValue);
        ObjEnt.SANCTION_AUTH = Convert.ToChar(rdbSactionAuth.SelectedValue);        
        ObjEnt.COMP_STMNT_AUTH_UANO = Convert.ToInt32(ddlCompStmntAuth.SelectedValue);
        ObjEnt.MAIL_SEND = chkMailSend.Checked ? 1 : 0;
        ObjEnt.DSR_CREATION = chkDsrCreation.Checked ? 1 : 0;
        ObjEnt.PO_APPROVAL = chkPoApproval.Checked ? 1 : 0;
        ObjEnt.DEPT_WISE_ITEM = chkDeptwiseitem.Checked ? 1 : 0;
        ObjEnt.PHONE = txtPhone.Text;
        ObjEnt.EMAIL = txtEmail.Text;
        ObjEnt.COLLEGE_NAME = ddlCollege.SelectedItem.Text;
        ObjEnt.CODE_STANDARD = txtCodeStandard.Text;
        ObjEnt.IS_COMPARATIVE_STAT_APPROVAL = chkCompSApproval.Checked ? 1 : 0;
        ObjEnt.STATENO = Convert.ToInt32(ddlState.SelectedValue);
        ObjEnt.IS_SECGP = chkIsSecGPEntry.Checked ? 1 : 0;


        ObjEnt.IS_BUDGET_HEAD = chkBudgetHeadReq.Checked ? 1 : 0;  //---26/09/2022 Added by shabina For Making Budget Head optional
      
        //ObjEnt.PRE_DSR_YEAR = txtPreDsrYear.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPreDsrYear.Text.Trim());
        //ObjEnt.CUR_DSR_YEAR = txtCurDsrYear.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCurDsrYear.Text.Trim());
        ObjEnt.COLCODE = Convert.ToInt32(Session["colcode"]);
        ObjEnt.IsAvailableQty = Convert.ToChar(rdblAvailableQty.SelectedValue);
        ObjEnt.IsAuthorityShowOnQuot = Convert.ToChar(rdbAuthoritynQuotation.SelectedValue);   //08/02/2024

            CustomStatus cs = (CustomStatus)ObjCon.InsUpdConfigurationDetails(ObjEnt);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //objCommon.DisplayMessage(updpanel, "Record Saved Successfully", this);
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this);

            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
               // objCommon.DisplayMessage(updpanel, "Record Updated Successfully", this);
                objCommon.DisplayMessage(this.Page, "Record Updated Successfully", this);


               // objCommon.DisplayMessage("Record Updated Successfully.", this.Page);
               // objCommon.DisplayMessage(this.updpanel, "Record Updated Successfully", this.Page); 
               // MessageBox("Record Updated Successfully.");
               // DisplayMessage("Item Deleted successfully");


            }
            BindData();
          
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindData();      

    }
}
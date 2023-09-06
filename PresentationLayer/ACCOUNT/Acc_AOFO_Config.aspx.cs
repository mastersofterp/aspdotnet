using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;

public partial class ACCOUNT_Acc_AOFO_Config : System.Web.UI.Page
{
    Common objCommon = new Common();
    AccountConfigurationController objMGC = new AccountConfigurationController();
    AccountConfiguration objMainGroup = new AccountConfiguration();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CollegeId"] != null || Session["UserName"] != null || Session["Password"] != null || Session["DataBase"] != null)
        {
            objCommon = new Common();
        }
        else
        {
           
            Response.Redirect("~/Default.aspx");

        }

        objCommon = new Common();
        if (!Page.IsPostBack)
        {
           
            FillDropDown();
            populateData();
        }

      
            
    }

    private void FillDropDown()
    {
        objCommon.FillDropDownList(ddlAO, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (1,3)", "UA_FULLNAME");

        objCommon.FillDropDownList(ddlFO, "user_acc", "UA_NO", "UA_FULLNAME", "UA_TYPE IN (1,3)" , "UA_FULLNAME");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
      
    }

    private void Clear()
    {
        ddlAO.SelectedIndex = 0;
        ddlFO.SelectedIndex = 0;
        chkTempVoucher.Checked = false;
        btnSubmit.Visible = false;
    
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        if (ddlAO.SelectedValue == "0")
        {
            objCommon.ShowError(Page, "Please Select AO");
            return;
        }

        if (ddlFO.SelectedValue == "0")
        {
            objCommon.ShowError(Page, "Please Select FO");
            return;
        }

        char IsTemp;

        if(chkTempVoucher.Checked == true)
        {
         IsTemp ='Y';
        }
        else
        {
          IsTemp ='N';
        }
      

        CustomStatus cs = (CustomStatus)objMGC.AddUpdateAOFOConfiguration(Convert.ToInt32(ddlAO.SelectedValue), Convert.ToInt32(ddlFO.SelectedValue),Convert.ToChar(IsTemp));

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            Clear();
            populateData();
            objCommon.DisplayUserMessage(UPBudget, "Record is Updated Successfully", this.Page);
         
        }
        if (cs.Equals(CustomStatus.RecordSaved))
        {
            Clear();
            populateData();
            objCommon.DisplayUserMessage(UPBudget, "Record is Inserted Successfully", this.Page);
           
        }
    }

    private void populateData()
    {

        DataSet ds = objCommon.FillDropDown("ACC_MAIN_CONFIGURATION AM  INNER JOIN USER_ACC AC ON (AC.UA_NO=AM.FO) INNER JOIN USER_ACC SS ON (SS.UA_NO=AM.AO)", "AM.AO AOID,SS.UA_FULLNAME AO,AC.UA_FULLNAME FO,SS.UA_FULLNAME VERIFIER,AC.UA_FULLNAME APPROVER,CASE AM.IsTempVoucher WHEN 'Y' THEN 'YES' WHEN 'N' THEN 'NO' ELSE '-' END AS IsTempVoucher  ", "", "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = false;
            lvAOFO.DataSource = ds;
            lvAOFO.DataBind();
        }
        else
        {
            btnSubmit.Visible = true;
            lvAOFO.DataSource = ds;
            lvAOFO.DataBind();
        }
      
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
       try
        {
            btnSubmit.Visible = true;
            ImageButton btnEdit = sender as ImageButton;
            ViewState["AO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetails(Convert.ToInt32(ViewState["AO"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACCOUNT>>ACC_AOFO_Config.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void ShowEditDetails(int AO)
    {
        DataSet ds = null;

        try
        {
            ds = objCommon.FillDropDown("ACC_MAIN_CONFIGURATION", "AO,FO,VERIFIER,APPROVER,IsTempVoucher", "", "AO =" + AO, "");
         
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlAO.SelectedValue = ds.Tables[0].Rows[0]["AO"].ToString();
                ddlFO.SelectedValue = ds.Tables[0].Rows[0]["FO"].ToString();
                if (ds.Tables[0].Rows[0]["IsTempVoucher"].ToString() == "Y")
                {
                    chkTempVoucher.Checked = true;
                }
                else
                    if (ds.Tables[0].Rows[0]["IsTempVoucher"].ToString() == "N")
                    {
                        chkTempVoucher.Checked = false;
                    }
            }
            else {

                objCommon.ShowError(Page, "Data Not Found!");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACCOUNT>>ACC_AOFO_Config.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }
}
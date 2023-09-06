using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_OA_Offline_Fees_Collection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        //lableDisable(); 
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = feeController.GetStudentInfoForOfflineFeeCollect(txtApplicationID.Text);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            lblApplicationid.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
            lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            lableVisibleTrue();
            divfee.Visible = true;
            ViewState["FEES"] = ds.Tables[0].Rows[0]["FEES"].ToString();
            txtAmount.Text = ViewState["FEES"].ToString();
            txtAmounttobepaid.Text = ViewState["FEES"].ToString();
            ViewState["USERNO"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
            ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            lblFees.Text = ds.Tables[0].Rows[0]["PAY_TYPE"].ToString();
            if (lblFees.Text.ToString().Equals("NA"))
            {
                lblFees.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                rdoFees.Enabled = false;
            }
            lblMode.Text = ds.Tables[0].Rows[0]["PAY_MODE_CODE"].ToString();
            if (lblMode.Text.ToString().Equals("NA"))
            {
                lblMode.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                rdoFees.Enabled = false;
            }
        }
        else
        {
            divfee.Visible = false;
            objCommon.DisplayMessage(this.Page, " Please Enter Valid  Application ID", this.Page);
            lableDisable();
            AddClear();
            txtApplicationID.Text = string.Empty;
        }
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ShowData();            
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this.Page, " Please Enter Valid  Application ID", this.Page);
        //    lableDisable();           
        //    AddClear();
        //    txtApplicationID.Text = string.Empty;
        //}      
    }

    //public void ShowData()
    //{
    //    DataSet ds = new DataSet();
    //    ds = feeController.GetStudentInfoForOfflineFeeCollect(txtApplicationID.Text);
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {
    //        lblApplicationid.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
    //        lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
    //        lblDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
    //        lblBranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
    //        lableVisibleTrue();
    //        divfee.Visible = true;
    //        ViewState["FEES"] = ds.Tables[0].Rows[0]["FEES"].ToString();
    //        txtAmount.Text = ViewState["FEES"].ToString();
    //        txtAmounttobepaid.Text = ViewState["FEES"].ToString();
    //        ViewState["USERNO"] = ds.Tables[0].Rows[0]["USERNO"].ToString();
    //        ViewState["DEGREENO"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
    //        ViewState["BRANCHNO"] = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
    //        lblFees.Text = ds.Tables[0].Rows[0]["PAY_TYPE"].ToString();
    //        if (lblFees.Text.ToString().Equals("NA"))
    //        {
    //            lblFees.ForeColor = System.Drawing.Color.Red;
    //        }
    //        else
    //        {
    //            rdoFees.Enabled = false;
    //        }
    //        lblMode.Text = ds.Tables[0].Rows[0]["PAY_MODE_CODE"].ToString();
    //        if (lblMode.Text.ToString().Equals("NA"))
    //        {
    //            lblMode.ForeColor = System.Drawing.Color.Red;
    //        }
    //        else
    //        {
    //            rdoFees.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        divfee.Visible = false;
    //    }
    //}
    public void AddClear()
    {
        lblApplicationid.Text = string.Empty;
        lblName.Text = string.Empty;
        lblDegree.Text = string.Empty;
        lblBranch.Text = string.Empty;
    }
    public void lableDisable()
    {
        
        divStudentinfo.Visible =  false;
        //divfee.Visible = false;
        btnCollect.Visible = false;
        btnCancel.Visible = false;

 
    }
    public void lableVisibleTrue()
    {
        divStudentinfo.Visible = true;
        //divfee.Visible = true;
        btnCollect.Visible = true;
        btnCancel.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //AddClear();
        //lableDisable();
        //txtApplicationID.Text = string.Empty;
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnCollect_Click(object sender, EventArgs e)
    {
        try
        {
            string payMode=string.Empty;
            string payType = string.Empty;
            decimal dd_Amount; DateTime dd_Date=DateTime.Today; string dd_City = string.Empty;
            string fees = objCommon.LookUp("ACD_USER_BRANCH_PREF", "FEES", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]) + " AND DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + " AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"]));
            if (ddlPaymentMode.SelectedValue == "1")
            {
                if (Convert.ToDecimal(fees).Equals(Convert.ToDecimal(txtAmount.Text.ToString())))
                {
                    payMode="C";
                    payType = "ONLINEADMISSION-APP";
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Something Went Wrong.", this.Page);
                    txtAmount.Text = fees.ToString();
                    return;
                }
            }
            else if (ddlPaymentMode.SelectedValue == "2")
            {
                if (Convert.ToDecimal(fees).Equals(Convert.ToDecimal(txtAmounttobepaid.Text.ToString())))
                {
                    payMode="D";
                    payType = "ONLINEADMISSION-ADM";
                    dd_Date = Convert.ToDateTime(txtDate.Text.ToString());

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Something Went Wrong.", this.Page);
                    txtAmounttobepaid.Text = fees.ToString();
                    return;
                }
            }
            dd_City=txtCity.Text.ToString().Equals(string.Empty)?"NA" : txtCity.Text.ToString().Trim();
            dd_Amount=txtAmounttobepaid.Text.ToString().Equals(string.Empty)?0 :Convert.ToDecimal(txtAmounttobepaid.Text.ToString());
            //feeController
            CustomStatus cs = (CustomStatus)feeController.Update_Recon_Status_OA(Convert.ToInt32(ViewState["USERNO"].ToString()), payMode, payType, dd_Date, dd_City, dd_Amount,Convert.ToInt32(ddlBank.SelectedValue));
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Fees Collection Updated Successfully.", this.Page);
                ClearField();
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void ClearField()
    {

        divStudentinfo.Visible = false;
        divfee.Visible = false;
        divfeesMode.Visible = false;
        divButton.Visible = false;
        ViewState["FEES"] = null;
        ViewState["USERNO"] = null;
        ViewState["DEGREENO"] = null;
        ViewState["BRANCHNO"] = null;
        txtApplicationID.Text = string.Empty;
        ddlBank.SelectedIndex = 0;
        ddlPaymentMode.SelectedIndex = 0;
        rdoFees.SelectedIndex = -1;
        txtDate.Text = string.Empty;
        txtAmounttobepaid.Text = string.Empty;
        txtAmount.Text = string.Empty;
        txtCity.Text = string.Empty;
    }
    protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedIndex == 1)
        {
            divfeesMode.Visible = false;
            divAmount.Visible = true;
            lableVisibleTrue();
        }
        else if (ddlPaymentMode.SelectedIndex == 2)
        {
            objCommon.FillDropDownList(ddlBank, "ACD_BANK", "BANKNO", "BANKNAME", "ACTIVESTATUS>0", "BANKNAME");
            divfeesMode.Visible = true;
            divAmount.Visible = false;
            lableVisibleTrue();
        }
        else
        {
            divfeesMode.Visible = false;
            divAmount.Visible = false;
            divButton.Visible = false;
        }
        
    }
    protected void rdoFees_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdoFees.SelectedIndex > 0)
            {
               
                divStudentinfo.Visible = true;
                divButton.Visible = true;
                divPayMode.Visible = true;
            }
            else
            {
                divPayMode.Visible = true;
                divStudentinfo.Visible = true;
                divButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
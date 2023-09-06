//======================================================================================
//PAGE NAME     : Late Fee Cancel
// CREATION DATE : 09 APRIL 2020
// CREATED BY    : NIKHIL V. LAMBE
//======================================================================================
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
using System.IO;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_LateFeeCancel : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    ChalanReconciliationController crController = new ChalanReconciliationController();

    #region Page Events
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
                   // this.CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND FLOCK=1", "SESSIONNO DESC");
                    objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
                }
            }
            else
            {
                // Clear message div
                divMsg.InnerHtml = string.Empty;
                /// Check if postback is caused by reconcile chalan or delete chalan buttons
                /// if yes then call corresponding methods

                if (Request.Params["__EVENTTARGET"] != null && Request.Params["__EVENTTARGET"].ToString() != string.Empty)
                {
                    if (Request.Params["__EVENTTARGET"].ToString() == "DeleteLateFee")
                    {
                        this.DeleteLateFee();
                    }
                    else if (Request.Params["__EVENTTARGET"].ToString() == "Clear")
                        this.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_LateFeeCanDirect.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LateFeeCanDirect.aspx");
            }
            Common objCommon = new Common();
            // objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LateFeeCanDirect.aspx");
        }
    } 

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divRemark.Visible = true;
        //if (rdoCancelLateFee.Checked)
        //{           
        //    btnModify.Enabled = false;
        //    btnCanLateFee.Disabled = false;
        //}
        //if (rdoModifyLateFee.Checked)
        //{
        //    btnModify.Visible = true;
        //    btnModify.Enabled = true;           
        //    btnCanLateFee.Visible = false;
        //}
        try
        {
            String strIdno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtSearchText.Text.Trim() + "'");
            ViewState["idno"] = strIdno;
            DataSet dsStudInfo = objCommon.FillDropDown("ACD_DEMAND D INNER JOIN ACD_BRANCH B ON(D.BRANCHNO=B.BRANCHNO) INNER JOIN ACD_SEMESTER S ON(D.SEMESTERNO=S.SEMESTERNO) INNER JOIN ACD_DEGREE DEG ON(D.DEGREENO=DEG.DEGREENO)", "D.SEMESTERNO,B.SHORTNAME,S.SEMESTERNAME,DEG.DEGREENAME", "CAST(D.NAME AS NVARCHAR(50)) AS STUDNAME,D.ENROLLNMENTNO,D.BRANCHNO,D.DEGREENO", " D.IDNO=" + strIdno + " AND D.RECIEPT_CODE='" + ddlRecType.SelectedValue + "' AND S.SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "D.IDNO");
            if (dsStudInfo != null && dsStudInfo.Tables.Count > 0 && dsStudInfo.Tables[0].Rows.Count > 0)
            {
                lblName.Text = dsStudInfo.Tables[0].Rows[0]["STUDNAME"].ToString();
                lblRegNo.Text = dsStudInfo.Tables[0].Rows[0]["ENROLLNMENTNO"].ToString();
                lblBranch.Text = dsStudInfo.Tables[0].Rows[0]["DEGREENAME"].ToString() + "/" + dsStudInfo.Tables[0].Rows[0]["SHORTNAME"].ToString();
                lblSemester.Text = dsStudInfo.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dsStudInfo.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                //divRemark.Visible = true;
                divStud.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(pnlFeeTable, "No record found ", this.Page);
                divStud.Visible = false;
                divRemark.Visible = false;
                return;
            }
            DataSet ds = crController.FindChalan_OnExam("IDNO", strIdno, Convert.ToInt32(ddlSemester.SelectedValue), ddlRecType.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvSearchResults.DataSource = ds;
                lvSearchResults.DataBind();
                lvSearchResults.Visible = true;
              //  btnCanLateFee.Disabled = false;
                if (rdoCancelLateFee.Checked)
                {
                    btnModify.Enabled = false;
                    btnCanLateFee.Disabled = false;
                }
                if (rdoModifyLateFee.Checked)
                {
                    btnModify.Visible = true;
                    btnModify.Enabled = true;
                    btnCanLateFee.Visible = false;
                }

                if (rdoModifyLateFee.Checked)
                {
                    foreach (ListViewDataItem item in lvSearchResults.Items)
                    {
                        TextBox late_fee = item.FindControl("txtLateFee") as TextBox;
                        late_fee.Enabled = true;
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(pnlFeeTable, "Student's Late fine demand not found ", this.Page);
                lvSearchResults.Visible = false;
                btnCanLateFee.Disabled = true;
                divRemark.Visible = false;
            }
            txtRemark.Text = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_LateFeeCanDirect.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void DeleteLateFee()
    {
        try
        {
            bool IsCancelled = false;
            string Cancel_FeeHead = string.Empty;
            string Late_fee_Cancel = objCommon.LookUp("REFF", "LATE_FEE_CANCEL", "");
            if (Session["userno"].ToString().Equals(Late_fee_Cancel))
            {
                DailyCollectionRegister dcr = new DailyCollectionRegister();
                foreach (ListViewDataItem item in lvSearchResults.Items)
                {
                    CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                    TextBox late_fee = item.FindControl("txtLateFee") as TextBox;
                    Label Receipt_type = item.FindControl("lblReceipttype") as Label;

                    if (chkRegister.Checked)
                    {
                        string strDcrNo = (!string.IsNullOrEmpty(chkRegister.ToolTip)) ? chkRegister.ToolTip : string.Empty;
                        dcr.DcrNo = (!string.IsNullOrEmpty(strDcrNo)) ? int.Parse(strDcrNo) : 0;
                        dcr.StudentId = Convert.ToInt32(ViewState["idno"]);
                        dcr.ReceiptDate = DateTime.Today;
                        dcr.UserNo = int.Parse(Session["userno"].ToString());

                        if (Request.Params["__EVENTTARGET"].ToString() == "DeleteLateFee")
                            dcr.Remark = Receipt_type.Text + " has been canceled by " + Session["userfullname"].ToString() + ". ";

                        dcr.Remark += txtRemark.Text.Trim();
                        dcr.CashAmount = Convert.ToDouble(late_fee.Text);
                        dcr.ReceiptTypeCode = Receipt_type.Text.Trim().ToUpper().Contains("LATE") ? "L" : Receipt_type.ToolTip.Trim().ToUpper();
                        Cancel_FeeHead = Receipt_type.Text.Trim().ToUpper();
                        IsCancelled = crController.DeleteLateFee(dcr,0);
                    }
                }

                if (IsCancelled)
                {
                    objCommon.DisplayMessage(pnlFeeTable, Cancel_FeeHead + " Canceled Successfully.", this.Page);                   
                    lvSearchResults.Visible = false;
                    divRemark.Visible = false;
                    btnCanLateFee.Disabled = true;
                    ClearFields();
                }
                else
                    objCommon.DisplayMessage(pnlFeeTable, "Unable to complete the operation.", this.Page);
                   
            }
            else
            {
                objCommon.DisplayMessage(pnlFeeTable, "Only Controller of Examination can be Canceled the Late Fees.", this.Page);                
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_LateFeeCanDirect.DeleteLateFee() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        try
        {
            Response.Redirect(Request.Url.ToString());
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_LateFeeCanDirect --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }    

    private string GetViewStateItem(string itemName)
    {
        if (ViewState.Count > 0 &&
            ViewState[itemName] != null &&
            ViewState[itemName].ToString() != null &&
            ViewState[itemName].ToString() != string.Empty)
            return ViewState[itemName].ToString();
        else
            return string.Empty;
    }

    //private void ShowMessage(string msg)
    //{
    //    this.divMsg.InnerHtml = "<script type='text/javascript' language='javascript'> alert(\"" + msg + "\"); </script>";
    //}
    protected void rdoLateFee_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoCancelLateFee.Checked)
        {
            ClearFields();           
            btnModify.Visible = false;
            btnCanLateFee.Visible = true;
            btnCanLateFee.Disabled = false;
            btnCanLateFee.Disabled = true;
        }
    }
    protected void rdoModifyLateFee_CheckedChanged(object sender, EventArgs e)
    {       
        if (rdoModifyLateFee.Checked)
        {          
            ClearFields();
            btnModify.Visible = true;
            divRemark.Visible = false;
            btnCanLateFee.Visible = false;
        }
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            string strDcrNo = string.Empty;
            bool IsCancelled = false;
            string Cancel_FeeHead = string.Empty;
            string Late_fee_Cancel = objCommon.LookUp("REFF", "LATE_FEE_CANCEL", "");
            if (Session["userno"].ToString().Equals(Late_fee_Cancel))
            {
                if (rdoModifyLateFee.Checked)
                {
                    foreach (ListViewDataItem item in lvSearchResults.Items)
                    {
                        CheckBox chkRegister = item.FindControl("chkRegister") as CheckBox;
                        TextBox late_fee = item.FindControl("txtLateFee") as TextBox;
                        Label Receipt_type = item.FindControl("lblReceipttype") as Label;

                        if (chkRegister.Checked)
                        {
                            DailyCollectionRegister dcr = new DailyCollectionRegister();
                            strDcrNo = (!string.IsNullOrEmpty(chkRegister.ToolTip)) ? chkRegister.ToolTip : string.Empty;
                            dcr.DcrNo = (!string.IsNullOrEmpty(strDcrNo)) ? int.Parse(strDcrNo) : 0;
                            dcr.StudentId = Convert.ToInt32(ViewState["idno"]);
                            dcr.ReceiptDate = DateTime.Today;
                            dcr.UserNo = int.Parse(Session["userno"].ToString());
                            dcr.Remark = Receipt_type.Text + " has been Modified by " + Session["userfullname"].ToString() + ". ";
                            dcr.Remark += txtRemark.Text.Trim();
                            dcr.CashAmount = Convert.ToDouble(late_fee.Text);
                            dcr.ReceiptTypeCode = Receipt_type.Text.Trim().ToUpper().Contains("LATE") ? "L" : Receipt_type.ToolTip.Trim().ToUpper();
                            Cancel_FeeHead = Receipt_type.Text.Trim().ToUpper();
                            dcr.UserNo = Convert.ToInt32(Session["userno"].ToString());
                            IsCancelled = crController.DeleteLateFee(dcr, 1);
                        }
                    }

                    if (IsCancelled)
                    {
                        objCommon.DisplayMessage(pnlFeeTable, Cancel_FeeHead + " Modified Successfully", this.Page);
                        lvSearchResults.Visible = false;
                        btnModify.Enabled = false;
                        ClearFields();
                    }
                    else
                        objCommon.DisplayMessage(pnlFeeTable, "Unable to complete the operation.", this.Page);

                }
                else
                    objCommon.DisplayMessage(pnlFeeTable, "Please select a chalan to  Modify Late Fee.", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(pnlFeeTable, "Only Controller of Examination can be Canceled the Late Fees.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_LateFeeCancel.btnModify_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    public void ClearFields()
    {
        divStud.Visible = false;
        lvSearchResults.DataSource = null;
        lvSearchResults.DataBind();
        ddlSemester.SelectedIndex = 0;
        ddlRecType.SelectedIndex = 0;
        txtSearchText.Text = string.Empty;
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            String strIdno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtSearchText.Text.Trim() + "'");
            DataSet ds = objCommon.FillDropDown("ACD_DEMAND", "RECIEPT_CODE", "", " CAN=0 AND DELET=0 AND IDNO=" + Convert.ToInt32(strIdno) + " AND SEMESTERNO=" + Convert.ToInt32(ddlSemester.SelectedValue), "DM_NO");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string rec_Codes = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rec_Codes += "'" + ds.Tables[0].Rows[i][0].ToString() + "',";
                }
                if (!string.IsNullOrEmpty(rec_Codes))
                    rec_Codes = rec_Codes.TrimEnd(',');
                objCommon.FillDropDownList(ddlRecType, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RECIEPT_CODE IN (" + rec_Codes + ")", "RECIEPT_TITLE"); //SELECT VALUE FROM DBO.SPLIT(" + Rec_Codes + ",',')) OR " + Rec_Codes + "=''
            }
        }
        else
        {
            objCommon.DisplayMessage(pnlFeeTable, "Please select Semester", this.Page);
            return;
        }
    }
}
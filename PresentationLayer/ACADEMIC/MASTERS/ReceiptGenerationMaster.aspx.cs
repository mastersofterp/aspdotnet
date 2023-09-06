using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ACADEMIC_MASTERS_ReceiptGenerationMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFCC = new FeeCollectionController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                   // this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                 objCommon.FillDropDownList(ddlReceiptCode, "ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE", "RCPTTYPENO>0", "");
                    objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "", "");
                    this.BindListView();
                    // Set form action as add on first time form load.
                    ViewState["action"] = "add";
                   
                }
             
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "DefineCounter.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objFCC.GetReceiptGenerationList();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvReceiptGeneration.DataSource = ds;
                lvReceiptGeneration.DataBind();
            }
            else
            {
                lvReceiptGeneration.DataSource = null;
                lvReceiptGeneration.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        // Check user's authrity for Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
    //        }
    //    }
    //    else
    //    {
    //        // Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=DefineCounter.aspx");
    //    }
   // }
    protected void ddlReceiptCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReceiptCode.SelectedIndex > 0)
            {
                ddlCounter.Items.Clear();
              //  objCommon.FillDropDownList(ddlCounter, "ACD_COUNTER_REF", "PRINTNAME", "COUNTERNAME + '  ' + '('+PRINTNAME+')'", "RECEIPT_PERMISSION="+ ddlReceiptCode.SelectedValue, "COUNTERNO ASC");
                ViewState["rec_code"] = objCommon.LookUp("ACD_RECIEPT_TYPE", "RECIEPT_CODE", "RECIEPT_TITLE='" + ddlReceiptCode.SelectedItem.Text + "'");
                //this.objCommon.FillDropDownList(ddlCounterNo, "ACD_COUNTER_REF", "COUNTERNO", "PRINTNAME", "RECEIPT_PERMISSION = '" + rec_code + "'", "COUNTERNO");
           objCommon.FillDropDownList(ddlCounter, "ACD_COUNTER_REF", "PRINTNAME", "COUNTERNAME", "RECEIPT_PERMISSION = '" + ViewState["rec_code"] + "'", "COUNTERNO");
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    private void ClearControlContents()
    {
        Response.Redirect(Request.Url.ToString());
    }
    private void Clear()
    {
        ddlReceiptCode.SelectedIndex = 0;
       // ddlReceiptCode.Items.Clear();
        ddlCounter.SelectedIndex = 0;
        ddlCounter.Items.Clear();
        txtReceiptClass.Text = string.Empty;
        rblPaymentMode.SelectedIndex = -1;
        ddlDegree.SelectedIndex = 0;
        //ddlDegree.Items.Clear();
        degree.Visible = true;
        if (chkIsMiss.Checked == true)
        {
            chkIsMiss.Checked = false;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int degreeno;
          string recptCode = ViewState["rec_code"].ToString();
            string paymentMode = rblPaymentMode.SelectedItem.Text;
            if (paymentMode == "Online")
            {
                paymentMode = "O";
            }
            else 
            {
                paymentMode = "C";
            }
            //string counter = ddlCounter.SelectedValue;
       string counter = objCommon.LookUp("ACD_COUNTER_REF", "PRINTNAME", "COUNTERNAME='" + ddlCounter.SelectedItem.Text + "'");
            string className = txtReceiptClass.Text;
            if (chkIsMiss.Checked == true)
            {
              degreeno =  ddlDegree.SelectedIndex = 0;
            }
            else
            {
                degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            }
            CustomStatus cs = (CustomStatus)objFCC.InsertReceiptGenerationData(degreeno, recptCode, paymentMode, counter, className);
            if (cs.Equals(CustomStatus.RecordExist))
            {
                objCommon.DisplayMessage(this.updReceiptGen, "Record Already Exist", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updReceiptGen, "Record Saved Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.updReceiptGen, "Error Adding Slot Name!", this.Page);
            }
            this.Clear();
            this.BindListView();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void chkIsMiss_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsMiss.Checked == true)
        {
            degree.Visible = false;
        }
        else {
            degree.Visible = true;
        }
    }
}
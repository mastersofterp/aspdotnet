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

public partial class ACADEMIC_NewAdmReconciliation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();

    #region Page Event

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
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_StudentHorizontalReport.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=NewAdmReconciliation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NewAdmReconciliation.aspx");
        }
    }

    #endregion

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindStudentList();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Refresh Page url
        Response.Redirect(Request.Url.ToString());
    }

    private void BindStudentList()
    {
        try
        {
            StudentController objSC = new StudentController();

            DataSet ds = objSC.GetStudentForChallanReconciliation(Convert.ToInt32(ddlAdmbatch.SelectedValue));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                lvStudentDetail.DataSource = ds;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = true;
                btnSubmit.Visible = true;

            }
            else
            {
                lvStudentDetail.DataSource = null;
                lvStudentDetail.DataBind();
                divstudentdetail.Visible = false;
                btnSubmit.Visible = false;
                objCommon.DisplayMessage(updStudent, "No Record Found.", this);

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "User_Status_Report.DisplayAllCount() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
        {

            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(updStudent, "Please Select atleast One Student!!", this.Page);
            return;
        }
        try
        {
            foreach (ListViewDataItem dataitem in lvStudentDetail.Items)
            {

                CheckBox chkBox = (dataitem.FindControl("cbRow")) as CheckBox;
                if (chkBox.Checked == true)
                {
                    string Semesterno = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(chkBox.ToolTip));
                    int Idno = Convert.ToInt32(chkBox.ToolTip);
                    int semesterno = Convert.ToInt32(Semesterno);
                    string reciept_code = "TF";
                    string counterno = objCommon.LookUp("ACD_COUNTER_REF", "COUNTERNO", "UA_NO=" + Convert.ToInt32(Session["userno"]) + "AND RECEIPT_PERMISSION='" + reciept_code.ToString()+"'");
                    int ua_no = Convert.ToInt32(Session["userno"].ToString());
                 
                   
                    int output = objFees.InsertAdmissionChallanPayment_DCR(Idno, semesterno, Convert.ToInt32(counterno),ua_no);
                    if (output == -99)
                    {
                        objCommon.DisplayMessage(updStudent, "Challan Reconciliation Failed.", this.Page);
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(updStudent, "Challan Reconciled successfully.", this.Page);
                    }
                }
            }

            this.ClearControl();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic_StudentInfoEntry.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
            //this.ClearControl();
        }
    }

    protected void ClearControl()
    {
        ddlAdmbatch.SelectedIndex = 0;
        lvStudentDetail.DataSource = null;
        lvStudentDetail.DataBind();
        divstudentdetail.Visible = false;
        btnSubmit.Visible = false;
    }
}
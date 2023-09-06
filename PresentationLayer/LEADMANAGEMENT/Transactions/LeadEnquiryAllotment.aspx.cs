using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.IO;

public partial class LEADMANAGEMENT_Transactions_LeadEnquiryAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeadEnquiryAllotmentController objLEA = new LeadEnquiryAllotmentController();

    #region Form Event

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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    DropDown();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlAdmissionBatch_OnSelectedIndexChanged(object Sender, EventArgs e)
    {
        try
        {
            if (ddlAdmissionBatch.SelectedIndex > 0)
            {
                DataSet ds = objLEA.GetAdmissionBatchwiseStudentList(Convert.ToInt32(ddlAdmissionBatch.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rptStudentList.DataSource = ds;
                    rptStudentList.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ScrollRepeater();", true);
                    divButton.Visible = true;
                }
                else
                {
                    rptStudentList.DataSource = null;
                    rptStudentList.DataBind();
                    divButton.Visible = false;
                    objCommon.DisplayMessage(this.upLeadEnquiryAllotment, "Record not found", this.Page);
                }
            }
            else
            {
                rptStudentList.DataSource = null;
                rptStudentList.DataBind();
                divButton.Visible = false;
            }

            if (ddlAdmissionBatch.SelectedIndex > 0)
            {
                DataSet dsCnt = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_GENERATION S WITH (NOLOCK) LEFT JOIN ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT A WITH (NOLOCK) ON (S.ENQUIRYNO=A.ENQUIRYNO)", "COUNT(*) AS TOTAL_ENQUIRY,CAST(SUM(CASE WHEN ISNULL(A.ENQUIRYNO,0)=0 THEN 1 END) AS INT)AS PENDING_COUNT", "CAST(SUM(CASE WHEN ISNULL(A.ENQUIRYNO,0)>0 THEN 1 END) AS INT)AS ALLOTED_COUNT", "BATCHNO=" + ddlAdmissionBatch.SelectedValue + "", "");
                if (dsCnt.Tables[0].Rows.Count > 0)
                {
                    lblTotalCount.Text = dsCnt.Tables[0].Rows[0]["TOTAL_ENQUIRY"].ToString();
                    lblPending.Text = dsCnt.Tables[0].Rows[0]["PENDING_COUNT"].ToString();
                    lblAllotmentCount.Text = dsCnt.Tables[0].Rows[0]["ALLOTED_COUNT"].ToString();
                    divEnquiryCount.Visible = true;
                }
                else
                {
                    divEnquiryCount.Visible = false;
                }
            }
            else
            {
                divEnquiryCount.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.ddlAdmissionBatch_OnSelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnSave_Click(object Sender, EventArgs e)
    {
        try {
            int CreateModifyBy = Convert.ToInt32(Session["userno"].ToString());
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

            DataRow dr = null;
            DataTable dtEquiryAllotment = new DataTable("TBL_LEAD_STUDENT_ENQUIRY_ALLOTEMENT");
            dtEquiryAllotment.Columns.Add("ALLOTMENT_NO", typeof(int));
            dtEquiryAllotment.Columns.Add("ENQUIRYNO", typeof(int));
            dtEquiryAllotment.Columns.Add("LEAD_UA_NO", typeof(int));
            dtEquiryAllotment.Columns.Add("UA_NO", typeof(string));
            dtEquiryAllotment.Columns.Add("ALLOTMENT_DATE", typeof(DateTime));
            dtEquiryAllotment.Columns.Add("CREATED_MODIFY_BY", typeof(int));
            dtEquiryAllotment.Columns.Add("IPADDRESS", typeof(string));
            DataTable _dtEquiryAllotment = null;

            int AllotmentNo = Convert.ToInt32(objCommon.LookUp("ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT", "COUNT(*) AS CNT", ""));

            string UA_NO = objCommon.LookUp("ACD_LEAD_LEVELCONFIGURATION STD", "ISNULL(STUFF((SELECT ', ' + CAST(UA_NO AS NVARCHAR(256)) FROM ACD_LEAD_LEVELCONFIGURATION SSM WHERE SSM.LEAD_UA_NO = STD.LEAD_UA_NO FOR XML PATH('')), 1, 1, ''), 'NOT ASSIGNED YET') AS UA_NO", "STD.LEAD_UA_NO=" + ddlEquiryAllotment.SelectedValue + ""); ////AND ISNULL(LEVELNO,0)<>0

            if (ddlEquiryAllotment.SelectedIndex > 0)
            {
                foreach (RepeaterItem rpt in rptStudentList.Items)
                {
                    HiddenField hdfEnquiryNo = rpt.FindControl("hdfEnquiryNo") as HiddenField;
                    CheckBox chkSingle = rpt.FindControl("chkSingle") as CheckBox;
                    if (chkSingle.Enabled.Equals(true) && chkSingle.Checked==true)
                    {
                        dr = dtEquiryAllotment.NewRow();

                        AllotmentNo += 1;
                        dr["ALLOTMENT_NO"] = AllotmentNo;
                        dr["ENQUIRYNO"] = hdfEnquiryNo.Value;
                        dr["LEAD_UA_NO"] = ddlEquiryAllotment.SelectedValue;
                        dr["UA_NO"] = UA_NO;
                        dr["ALLOTMENT_DATE"] = DateTime.Today;
                        dr["CREATED_MODIFY_BY"] = CreateModifyBy;
                        dr["IPADDRESS"] = IPAddress;

                        dtEquiryAllotment.Rows.Add(dr);
                    }
                }

                _dtEquiryAllotment = dtEquiryAllotment;

                CustomStatus cs = (CustomStatus)objLEA.InsertUpdateEnquiryAllotment(_dtEquiryAllotment);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.upLeadEnquiryAllotment, "Record Save successfully", this.Page);
                    ddlEquiryAllotment.SelectedIndex = 0;
                    ddlAdmissionBatch_OnSelectedIndexChanged(Sender, e);
                }
                else if (cs.Equals(CustomStatus.Error))
                {
                    objCommon.DisplayMessage(this.upLeadEnquiryAllotment, "Error Occured", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try 
        {
            ControlClear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try 
        {
            GridView GV = new GridView();
            string ContentType = string.Empty;
            DataSet dsEquiryAllotedDetail = objLEA.GetAdmissionBatchwiseStudentListInExcel(Convert.ToInt32(ddlAdmissionBatch.SelectedValue), Convert.ToInt32(ddlEquiryAllotment.SelectedValue));
            if (dsEquiryAllotedDetail.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsEquiryAllotedDetail;
                GV.DataBind();
                string attachment = "attachment; filename=Enquiry_Alloted_Detail.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.MS-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this.upLeadEnquiryAllotment, "Record Not Found", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    #endregion Form Event

    #region User Define Function

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=LeadEnquiryAllotment.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=LeadEnquiryAllotment.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.CheckPageAuthorization-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void DropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlEquiryAllotment, "[DBO].[ACD_LEAD_LEVELCONFIGURATION] A WITH (NOLOCK) INNER JOIN USER_ACC B WITH (NOLOCK) ON (A.UA_NO=B.UA_NO)", "A.LEAD_UA_NO", "B.UA_FULLNAME", "ISNULL(LEVELNO,0)=0", "LC_NO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_LeadEnquiryAllotment.DropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void ControlClear()
    {
        ddlAdmissionBatch.SelectedIndex = ddlEquiryAllotment.SelectedIndex = 0;
        rptStudentList.DataSource = null;
        rptStudentList.DataBind();
        divEnquiryCount.Visible = false;
    }

    #endregion User Define Function
}
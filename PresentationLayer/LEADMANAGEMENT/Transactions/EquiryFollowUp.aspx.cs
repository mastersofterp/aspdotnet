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

public partial class LEADMANAGEMENT_Transactions_EquiryFollowUp : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    EquiryFollowUpController objEFC = new EquiryFollowUpController();

    //Form Event
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

                    //Bind Admission Batch Drop Down.
                    DropDown();

                    //Update Repeat Schedule Automatically When No done status found & Last user End Date is Expired.
                    objEFC.UpdateEnquiryFollowUpScheduleAutomatic();

                    //Enquiry Done Status
                    BindEquiryDoneStatusList();

                    //Bind EquiryFollowup Student List.
                    BindEquiryFollowupStudentList();
                   
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void ddlAdmissionBatch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindEquiryFollowupStudentList();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow dr = null;
            DataTable dtEnquiryFollowUp = new DataTable("ENQUIRY_FOLLOW_UPTBL");
            dtEnquiryFollowUp.Columns.Add("ENQUIRYNO", typeof(int));
            dtEnquiryFollowUp.Columns.Add("DEGREENO", typeof(int));
            dtEnquiryFollowUp.Columns.Add("BRANCHNO", typeof(int));
            dtEnquiryFollowUp.Columns.Add("LEAD_UA_NO", typeof(int));
            dtEnquiryFollowUp.Columns.Add("UA_NO", typeof(int));
            dtEnquiryFollowUp.Columns.Add("ENQUIRYSTATUS", typeof(int));
            dtEnquiryFollowUp.Columns.Add("REMARK", typeof(string));

            DataTable _dtEnquiryFollowUp = null;
            int SelectCount = 0;

            foreach(ListViewItem item in lvEquiryList.Items)
            {
                HiddenField hdfEnquiryNo = item.FindControl("hdfEnquiryNo") as HiddenField;
                HiddenField hdfLead_UA_No = item.FindControl("hdfLead_UA_No") as HiddenField;
                DropDownList ddlEnquiryStatus = item.FindControl("ddlEnquiryStatus") as DropDownList;
                DropDownList ddlDegree = item.FindControl("ddlDegree") as DropDownList;
                DropDownList ddlBranch = item.FindControl("ddlBranch") as DropDownList;
                TextBox txtRemark = item.FindControl("txtRemark") as TextBox;

                CheckBox chkEnquiryStatus = item.FindControl("chkEnquiryStatus") as CheckBox;
                if (chkEnquiryStatus.Checked == true)
                {
                    dr = dtEnquiryFollowUp.NewRow();
                    dr["ENQUIRYNO"] = hdfEnquiryNo.Value;
                    dr["DEGREENO"] = ddlDegree.SelectedValue;
                    dr["BRANCHNO"] = ddlBranch.SelectedValue;
                    dr["LEAD_UA_NO"] = hdfLead_UA_No.Value;
                    dr["UA_NO"] = Session["userno"].ToString();
                    dr["ENQUIRYSTATUS"] = ddlEnquiryStatus.SelectedValue;
                    dr["REMARK"] = txtRemark.Text;

                    dtEnquiryFollowUp.Rows.Add(dr);
                    SelectCount += 1;
                }
            }

            if (SelectCount > 0)
            {
                _dtEnquiryFollowUp = dtEnquiryFollowUp;
                CustomStatus cs = (CustomStatus)objEFC.UpdateEnquiryFollowUpStatus(_dtEnquiryFollowUp);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "Enquiry Follow UP Status Updated Successfully", this.Page);
                    BindEquiryFollowupStudentList();
                    BindEquiryDoneStatusList();
                }
                else if (cs.Equals(CustomStatus.Error))
                {
                    objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "Error Occured", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "Please Select Enquiry.", this.Page);
            }
        
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
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

            DataSet dsEquiryFollowupList = objEFC.GetStudentEnquiryFolloupListExcelReport(Convert.ToInt32(Session["userno"].ToString()),Convert.ToInt32(ddlAdmissionBatch.SelectedValue)); 
            if (dsEquiryFollowupList.Tables[0].Rows.Count > 0)
            {
                GV.DataSource = dsEquiryFollowupList;
                GV.DataBind();
                string attachment = "attachment; filename=Equiry_Follow_Up_List.xls";
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
                objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "Record Not Found", this.Page);
                lvEquiryList.DataSource = null;
                lvEquiryList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.btnShowReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void lvEquiryList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        DropDownList ddlEnquiryStatus = (DropDownList)e.Item.FindControl("ddlEnquiryStatus");
        DropDownList ddlDegree = (DropDownList)e.Item.FindControl("ddlDegree");
        BindEquiryStatusDropDown(ddlEnquiryStatus);
        BindDegreeDropDown(ddlDegree);
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlDegree = (DropDownList)sender;
        var id = ddlDegree.ID;
        var degreeno = ddlDegree.SelectedValue;

        ListViewItem item = (ListViewItem)ddlDegree.NamingContainer;
        DropDownList ddlBranch = (DropDownList)item.FindControl("ddlBranch");
        if (ddlDegree.SelectedIndex > 0)
        {
            BindBranchDropDown(ddlBranch,Convert.ToInt32(degreeno));
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ScrollRepeater();", true);
    }

    protected void ShowModal(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var EnquiryNo = button.CommandArgument;
        DataSet ds = objCommon.FillDropDown("ACD_LEAD_STUDENT_ENQUIRY_GENERATION", "(CASE WHEN ISNULL(STUDFIRSTNAME,'')<>'' AND ISNULL(STUDMIDDLENAME,'')<>'' AND ISNULL(STUDLASTNAME,'') <>'' THEN STUDFIRSTNAME +' '+ STUDMIDDLENAME +' '+ STUDLASTNAME WHEN ISNULL(STUDFIRSTNAME,'')<>'' AND ISNULL(STUDMIDDLENAME,'')<>'' AND ISNULL(STUDLASTNAME,'') ='' THEN STUDFIRSTNAME +' '+ STUDMIDDLENAME WHEN ISNULL(STUDFIRSTNAME,'')<>'' AND ISNULL(STUDMIDDLENAME,'')='' AND ISNULL(STUDLASTNAME,'') <>'' THEN STUDFIRSTNAME +' '+ STUDLASTNAME WHEN ISNULL(STUDFIRSTNAME,'')<>'' AND ISNULL(STUDMIDDLENAME,'')='' AND ISNULL(STUDLASTNAME,'') ='' THEN STUDFIRSTNAME END) AS STUDENTNAME", "ISNULL(STUDMOBILE,'') AS STUDMOBILE,ISNULL(STUDEMAIL,'') AS STUDEMAIL,ISNULL(PARENTMOBILE,'') AS PARENTMOBILE,ISNULL(PARENTEMAIL,'') AS PARENTEMAIL", "ENQUIRYNO=" + EnquiryNo + "", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblId.Text = EnquiryNo;
            lblName.Text = ds.Tables[0].Rows[0]["STUDENTNAME"].ToString();
            lblMobileno.Text = ds.Tables[0].Rows[0]["STUDMOBILE"].ToString() == string.Empty ? "" :ds.Tables[0].Rows[0]["STUDMOBILE"].ToString();
            lblEmailID.Text = ds.Tables[0].Rows[0]["STUDEMAIL"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["STUDEMAIL"].ToString();
            lblParentMobileNo.Text = ds.Tables[0].Rows[0]["PARENTMOBILE"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["PARENTMOBILE"].ToString();
            lblParentEmailID.Text = ds.Tables[0].Rows[0]["PARENTEMAIL"].ToString() == string.Empty ? "" : ds.Tables[0].Rows[0]["PARENTEMAIL"].ToString();
            mpe.Show();
        }
        else
        {
            objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "No Detail Found", this.Page);
        }
    }

    #endregion Form Event


    //User Define Function
    #region User Define Function

    private void CheckPageAuthorization()
    {
        try
        {
            if (Request.QueryString["pageno"] != null)
            {
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=EquiryFollowUp.aspx");
                }
            }
            else
            {
                Response.Redirect("~/notauthorized.aspx?page=EquiryFollowUp.aspx");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.CheckPageAuthorization-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void DropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmissionBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.DropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindEquiryStatusDropDown(DropDownList ddlEnquiryStatus)
    {
        try
        {
            objCommon.FillDropDownList(ddlEnquiryStatus, "ACD_ENQUIRYSTATUS", "ENQUIRYSTATUSNO", "ENQUIRYSTATUSNAME", "ISNULL(ENQUIRYSTATUS,0)=1", "ENQUIRYSTATUSNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.BindEquiryStatusDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindDegreeDropDown(DropDownList ddlDegree)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.BindDegreeDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindBranchDropDown(DropDownList ddlBranch,int DegreeNo)
    {
        try
        {
            if (DegreeNo > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON B.BRANCHNO=CB.BRANCHNO", "DISTINCT B.BRANCHNO", " B.LONGNAME", " CB.DEGREENO=" + Convert.ToInt32(DegreeNo), "LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                ddlBranch.SelectedIndex = 0;
                return;
            }
            //
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.BindBranchDropDown-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindEquiryFollowupStudentList()
    {
        try 
        { 
             DataSet ds=objEFC.GetStudentEnquiryFolloupList(Convert.ToInt32(Session["userno"].ToString()),Convert.ToInt32(ddlAdmissionBatch.SelectedValue));
             if (ds.Tables[0].Rows.Count > 0)
             {
                 lvEquiryList.DataSource = ds;
                 lvEquiryList.DataBind();

                 if (lvEquiryList.Items.Count > 0)
                 {
                     foreach (ListViewDataItem item in lvEquiryList.Items)
                     {
                         HiddenField hdfEnquiryStatuNo = item.FindControl("hdfEnquiryStatuNo") as HiddenField;
                         HiddenField hdfDegreeNo = item.FindControl("hdfDegreeNo") as HiddenField;
                         HiddenField hdfBranchNo = item.FindControl("hdfBranchNo") as HiddenField;
                         DropDownList ddlEddlEnquiryStatus = item.FindControl("ddlEnquiryStatus") as DropDownList;
                         DropDownList ddlDegree = item.FindControl("ddlDegree") as DropDownList;
                         DropDownList ddlBranch = item.FindControl("ddlBranch") as DropDownList;
                       
                         ddlEddlEnquiryStatus.SelectedValue = hdfEnquiryStatuNo.Value;
                         ddlDegree.SelectedValue = hdfDegreeNo.Value;

                         if (Convert.ToInt32(hdfDegreeNo.Value) > 0)
                         {
                             BindBranchDropDown(ddlBranch, Convert.ToInt32(hdfDegreeNo.Value));
                             ddlBranch.SelectedValue = hdfBranchNo.Value;
                         }
                     }
                 }
                 ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ScrollRepeater();", true);
                 //divButton.Visible = true;
                 btnSave.Visible = true;
             }
             else
             {
                 lvEquiryList.DataSource = null;
                 lvEquiryList.DataBind();
                 objCommon.DisplayMessage(this.upLeadEnquiryFollowUp, "No Enquiry Found for Follow UP", this.Page);
                 //divButton.Visible = false;
                 btnSave.Visible = false;
             }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.BindEquiryFollowupStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void BindEquiryDoneStatusList()
    {
        try
        {
            pnlEnquiryDoneStatus.Visible = false;
            DataSet ds = objEFC.GetStudentEnquiryFolloupDoneList(Convert.ToInt32(Session["userno"].ToString()), 0); //selection of Userno and batch
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlEnquiryDoneStatus.Visible = true;
                lstEnquiryDoneStatus.DataSource = ds;
                lstEnquiryDoneStatus.DataBind();
            }
            else
            {
                lstEnquiryDoneStatus.DataSource = null;
                lstEnquiryDoneStatus.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "LEADMANAGEMENT_Transactions_EquiryFollowUp.BindEquiryDoneStatusList-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }
    
    #endregion User Define Fucntion

    // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
}
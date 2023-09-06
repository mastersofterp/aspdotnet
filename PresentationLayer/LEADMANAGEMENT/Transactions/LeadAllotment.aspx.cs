using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using ClosedXML.Excel;
using System.IO;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class LEADMANAGEMENT_Transactions_LeadAllotment : System.Web.UI.Page
{
    LMController objLMC = new LMController();
    Common objCommon = new Common();
    FaqController objFAQ = new FaqController();
    protected void Page_Load(object sender, EventArgs e)
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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();


                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            PopulateDropDown();
        }
    }
    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=LeadAllotment.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=LeadAllotment.aspx");
    //    }
    //}
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet dsGetStudentsList = null;
            dsGetStudentsList = objLMC.GetStudentsDetails_LeadAllotment(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlProgrammeType.SelectedValue));
            if (dsGetStudentsList.Tables[0].Rows.Count > 0)
            {
                objCommon.FillDropDownList(ddlMainUser, "ACD_LEAD_COUNSELOR_ALLOTMENT CA INNER JOIN USER_ACC UA ON (CA.MAINUSER_UA_NO=UA.UA_NO)", "DISTINCT CA.MAINUSER_UA_NO", "UA_NAME", "ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND UGPGOT=" + Convert.ToInt32(ddlProgrammeType.SelectedValue), "CA.MAINUSER_UA_NO");
                lvLeadAllot.DataSource = dsGetStudentsList;
                lvLeadAllot.DataBind();
                lvLeadAllot.Visible = true;
                //hftot.Value = dsGetStudentsList.Tables[0].Rows.Count.ToString();
                //btnSend.Enabled = true;
                divUser.Visible = true;
                divSubCounsellor.Visible = true;
                btnSubmit.Enabled = true;
            }
            else
            {
                lvLeadAllot.Visible = false;
                //btnSend.Enabled = false;
                divUser.Visible = false;
                divSubCounsellor.Visible = false;
                btnSubmit.Enabled = false;
                objCommon.DisplayMessage(updAllot, "No Record Found.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearFields();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH AD INNER JOIN ACD_USER_REGISTRATION UR ON(AD.BATCHNO=UR.ADMBATCH)", "DISTINCT UR.ADMBATCH", "BATCHNAME", "UR.ADMBATCH>0", "UR.ADMBATCH");
        objCommon.FillDropDownList(ddlAdmBatch_Rpt, "ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT", "DISTINCT ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)BATCHNAME", "ADMBATCH>0", "ADMBATCH");
        objCommon.FillDropDownList(ddlFormCategory, "ACD_QUERY_CATEGORY", "DISTINCT CATEGORYNO", "QUERY_CATEGORY_NAME", "CATEGORYNO>0", "CATEGORYNO");
        objCommon.FillDropDownList(ddlAdmBatch_Remark, "ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT", "DISTINCT ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)BATCHNAME", "ADMBATCH>0", "ADMBATCH");


    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProgrammeType, "ACD_UA_SECTION UA INNER JOIN ACD_USER_REGISTRATION UR ON(UA.UA_SECTION=UR.UGPGOT)", "DISTINCT UR.UGPGOT", "UA_SECTIONNAME", " UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue), "UR.UGPGOT");
                ddlProgrammeType.Focus();
            }
            lvLeadAllot.DataSource = null;
            lvLeadAllot.DataBind();
            lvLeadAllot.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearFields()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgrammeType.Items.Clear();
        ddlProgrammeType.Items.Add(new ListItem("Please Select", "0"));
        //ddlLevel.SelectedIndex = 0;
        lvLeadAllot.Visible = false;
        lvLeadAllot.DataSource = null;
        lvLeadAllot.DataBind();
        divUser.Visible = false;
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
 
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string ipAddress = string.Empty; string usernos = string.Empty; string degreenos = string.Empty;
            if (ddlMainUser.SelectedIndex == 0)
            {
                objCommon.DisplayMessage(updAllot, "Please Select Main User.", this.Page);
                return;
            }
            foreach (ListViewDataItem lvData in lvLeadAllot.Items)
            {
                CheckBox chk = lvData.FindControl("cbRow") as CheckBox;
                HiddenField hdnDegreenos=lvData.FindControl("hdnDegree") as HiddenField;
                if (chk.Checked && chk.Enabled==true)
                {
                    usernos += chk.ToolTip.ToString()+",";
                    degreenos +=hdnDegreenos.Value.ToString() + ",";
                }
            }
            if (!usernos.ToString().Equals(string.Empty))
            {
                usernos = usernos.Substring(0, usernos.Length-1);
            }
            if (!degreenos.ToString().Equals(string.Empty))
            {
                degreenos = degreenos.Substring(0, degreenos.Length - 1);
            }
            ipAddress= Request.ServerVariables["REMOTE_ADDR"];      
            int main_counsallor=0;
            if (ddlMainUser.SelectedIndex>0)
            {
                main_counsallor = Convert.ToInt32(ddlMainUser.SelectedValue);
            }
            int sub_counsallor = 0;
            if (ddlsubuser.SelectedIndex > 0)
            {
                sub_counsallor = Convert.ToInt32(ddlsubuser.SelectedValue);
            }
            CustomStatus cs = (CustomStatus)objLMC.Add_Student_Record_For_Lead_Allot(usernos, degreenos, Convert.ToInt32(ddlMainUser.SelectedValue), Convert.ToInt32(Session["userno"]), ipAddress, Convert.ToInt32(Session["OrgId"]), Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlProgrammeType.SelectedValue), main_counsallor, sub_counsallor);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.Page, "Record Saved Successfully", this.Page);
                ClearField();
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.Page, "Record updated Successfully", this.Page);
                ClearField();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Something Went Wrong!", this.Page);
                ClearField();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearField()
    {
        ddlAdmBatch.SelectedIndex = 0;
       // ddlLevel.SelectedIndex = 0;
        ddlMainUser.SelectedIndex = 0;
        ddlsubuser.SelectedIndex = 0;

        ddlProgrammeType.SelectedIndex = 0;
        lvLeadAllot.DataSource = null;
        lvLeadAllot.DataBind();
        //btnSend.Enabled = false;
        btnSubmit.Enabled = false;
        divUser.Visible = false;
        divSubCounsellor.Visible = false;
    }
    protected void btnIN_Click(object sender, EventArgs e)
    {
        objCommon.DisplayMessage(this.Page, "IN", this.Page);
        return;
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch_Rpt.SelectedIndex > 0 && ddlProgrammeType_Rpt.SelectedIndex > 0)
            {
                DataSet dsExcel = objLMC.Get_Student_Record_For_Lead_Excel(Convert.ToInt32(ddlAdmBatch_Rpt.SelectedValue), Convert.ToInt32(ddlProgrammeType_Rpt.SelectedValue));
                if (dsExcel.Tables[0].Rows.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (System.Data.DataTable dt in dsExcel.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }
                        //Export the Excel file.
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=LeadAllotmentReport.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlAdmBatch_Rpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch_Rpt.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProgrammeType_Rpt, "ACD_LEAD_STUDENT_ENQUIRY_ALLOTEMENT AL INNER JOIN ACD_UA_SECTION UA ON(AL.PROGRAMME_TYPE=UA.UA_SECTION)", "DISTINCT PROGRAMME_TYPE", "UA_SECTIONNAME", "PROGRAMME_TYPE > 0", "PROGRAMME_TYPE");
                ddlProgrammeType_Rpt.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlProgrammeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvLeadAllot.DataSource = null;
            lvLeadAllot.DataBind();
            lvLeadAllot.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvLeadAllot.DataSource = null;
        lvLeadAllot.DataBind();
        lvLeadAllot.Visible = false;
    }
    //protected void ddlMainUser_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    objCommon.FillListBox(ddlsubuser, "user_acc", "UA_NO", "UA_NAME", "UA_NO>0 AND UA_TYPE<>2", "UA_NO>0  and MAINUSER_UA_NO=" + Convert.ToInt32(ddlMainUser.SelectedValue) + "" );

    //    // and MAINUSER_UA_NO=" + Convert.ToInt32(ddlMainUser.SelectedValue) + "", "MAINUSER_UA_NO");
    //    //lvLeadAllot.DataSource = null;
    //    //lvLeadAllot.DataBind();
    //    //lvLeadAllot.Visible = false;
    //}
    protected void ddlFormCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindListView()
    {
        try
        {
            int categoryno = Convert.ToInt32(ddlFormCategory.SelectedValue);
            DataSet ds = objFAQ.GetAllStudent_Feadback(categoryno);
            if (ds != null)
            {
                lvStudentQuery.DataSource = ds;
                lvStudentQuery.DataBind();
            }
            else
            {
                lvStudentQuery.DataSource = null;
                lvStudentQuery.DataBind();
                objCommon.DisplayMessage(this.Page, "No Record Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Masters_ProgramType_Master.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnPriview_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["CategoryNo"] = null;
            ViewState["userno"] = null;
            ImageButton btnImgB = sender as ImageButton;
            Button btnPreview = sender as Button;

            int userno = Int32.Parse(btnPreview.CommandName);

            int Catno = Int32.Parse(btnPreview.ToolTip);
            int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERYNO", "USERNO=" + userno + "AND QUERY_CATEGORY=" + Catno));
            int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "QUERY_CATEGORY", "USERNO=" + userno + " AND QUERYNO=" + Queryno));

            ViewState["QueryNo"] = Convert.ToInt32(Queryno);
            ViewState["CategoryNo"] = Convert.ToInt32(categoryno);
            ViewState["userno"] = Convert.ToInt32(userno);
            int Active_Status = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS", "ACTIVE_STATUS", "USERNO=" + userno + " AND QUERYNO=" + Queryno + " AND QUERY_CATEGORY=" + categoryno));
            if (Active_Status == 2)
            {
                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit.Visible = false;
            }
            else
            {
                ddlStatus.Visible = true;
                txtFeedback.Visible = true;
                btnSubmit.Visible = true;
            }
            ddlStatus.SelectedIndex = 0;
            BindConversation();

            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal1", "showPopup();", true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvStudentQuery_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if ((e.Item.ItemType == ListViewItemType.DataItem))
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;
            DataRow dr = ((DataRowView)dataItem.DataItem).Row;

            if (dr["FEEDBACK_STATUS"].Equals("CLOSED"))
            {

                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;

            }
            else if (dr["FEEDBACK_STATUS"].Equals("OPEN"))
            {
                ((Label)e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
                ((Label)e.Item.FindControl("lblStatus") as Label).Font.Bold = true;
            }
        }
    }
    protected void lvStudentQuery_PagePropertiesChanged(object sender, EventArgs e)
    {
        DataPager dp = (DataPager)lvStudentQuery.FindControl("DataPager1");
        BindListView();
    }
    private void BindConversation()
    {
        int userno = 0;
        userno = Convert.ToInt32(ViewState["userno"]);

        //DataSet user_ds = objCommon.FillDropDown("ACD_USER_FEEDBACK", "(CASE WHEN FEEDBACK_DETAILS IS NULL THEN '' ELSE CONCAT( CONCAT('<em>Applicant:</em> ',FEEDBACK_DETAILS), '<em><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_DETAILS", "(CASE WHEN FEEDBACK_REPLY IS NULL THEN '' ELSE CONCAT(CONCAT('<em>CPUK Reply('+ REPLIED_USER +'):</em> ',FEEDBACK_REPLY), '<em><small><br/><sd>'+ cast(FEEDBACK_DATE as varchar(50)) + '</sd></small></em>') END) AS FEEDBACK_REPLY", "USERNO=" + userno + " AND QUERYNO=" + Convert.ToInt32(ViewState["QueryNo"]) + "", "SRNO");
        DataSet user_ds = objFAQ.BindConversationForQueryManagement(userno, Convert.ToInt32(ViewState["QueryNo"]));
        lvFeedbackReply.DataSource = user_ds;
        lvFeedbackReply.DataBind();
    }
    protected void btnSubmit_Reply_Click(object sender, EventArgs e)
    {
        int status = 0;
        int Catno = 0;
        int userno = 0;
        string FeedbackQuery = string.Empty;
        string feedback_reply = string.Empty;
        string replied_user = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        try
        {

            Catno = Convert.ToInt32(ViewState["CategoryNo"]);
            userno = Convert.ToInt32(ViewState["userno"]);
            feedback_reply = txtFeedback.Text;

            int Queryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS Q inner join  ACD_USER_FEEDBACK F ON (Q.QUERYNO=F.QUERYNO)", "Q.QUERYNO", "Q.USERNO=" + userno + "AND F.FEEDBACK_STATUS=1 AND Q.QUERY_CATEGORY=" + Catno));
            int categoryno = Convert.ToInt32(objCommon.LookUp("ACD_USER_QUERY_DETAILS Q inner join  ACD_USER_FEEDBACK F ON (Q.QUERYNO=F.QUERYNO)", "Q.QUERY_CATEGORY", "Q.USERNO=" + userno + "AND F.FEEDBACK_STATUS=1 AND Q.QUERYNO=" + Queryno));

            string Ip_Address = Request.ServerVariables["REMOTE_HOST"];
            string UserID = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            status = Convert.ToInt32(ddlStatus.SelectedValue);
            int orgid = Convert.ToInt32(Session["OrgId"]);
            //int result = objFAQ.Add_Feedback_Reply(userno, UserID, feedback_reply, Ip_Address, categoryno, status, Queryno, replied_user, Convert.ToInt32(Session["OrgId"]));
            int result = objFAQ.Add_Feedback_Reply_Lead(userno, feedback_reply, Ip_Address, categoryno, status, Queryno, replied_user, UserID, orgid);

            if (result != -99)
            {
                lblStatus1.Visible = true;
                lblStatus1.ForeColor = System.Drawing.Color.Green;
                lblStatus1.Text = "Query Submitted Successfully!";
                txtFeedback.Text = "";
                ddlFormCategory.SelectedIndex = 0;
                BindConversation();

            }
            BindListView();

            if (ddlStatus.SelectedValue == "2")
            {
                ddlStatus.Visible = false;
                txtFeedback.Visible = false;
                btnSubmit_Reply.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    protected void btnCancel_Rpt_Click(object sender, EventArgs e)
    {
        try
        {
            ddlAdmBatch_Rpt.SelectedIndex = -1;
            ddlProgrammeType_Rpt.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lnkName_Click(object sender, EventArgs e)
    {
        int userno = 0;
        LinkButton btnName = sender as LinkButton;
        ListViewDataItem item = btnName.NamingContainer as ListViewDataItem;
        HiddenField hdnUserno = item.FindControl("hdnUserno") as HiddenField;
        userno =Convert.ToInt32(hdnUserno.Value);
        ViewState["userno"] = userno;
        DataSet dsStudent_ForModelPopUp=objLMC.Get_Student_Record_ForPopUp_LeadModule(userno);
        if (dsStudent_ForModelPopUp.Tables.Count > 0)
        {
            if (dsStudent_ForModelPopUp.Tables[0].Rows.Count > 0)
            {
               lblName.Text=dsStudent_ForModelPopUp.Tables[0].Rows[0]["FIRSTNAME"].ToString();
               lblEmailId.Text=dsStudent_ForModelPopUp.Tables[0].Rows[0]["EMAILID"].ToString();
               lblPhone.Text=dsStudent_ForModelPopUp.Tables[0].Rows[0]["MOBILENO"].ToString();
            }
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myApplicationModal').modal()", true);
        objCommon.FillDropDownList(ddlLeadStatus, "ACD_ENQUIRYSTATUS", "ENQUIRYSTATUSNO", "ENQUIRYSTATUSNAME", "ENQUIRYSTATUS=1", "ENQUIRYSTATUSNO");
        liRemark.Attributes.Add("style", "display:none");
        //liRemark.Visible = true;
        liLeadStatus.Attributes.Add("style", "display:none");
        liFollowDate.Attributes.Add("style", "display:none");
        divButtonModel.Attributes.Add("style", "display:none");
    }
    protected void btn_SubmitModal_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLeadStatus.SelectedValue == "0")
            {
                objCommon.DisplayMessage(this.updPopUp, "Please Select Lead Status.", this.Page);
                return;
            }
            if (txt_Remark.Text == "")
            {
                objCommon.DisplayMessage(this.updPopUp, "Please Enter Remark.", this.Page);
                return;
            }
            int enquiryNo=0;int action=0;
            if (ViewState["Enquiry"].ToString() != string.Empty)
            {
                enquiryNo =Convert.ToInt32(ViewState["Enquiry"].ToString());
            }
            action = Convert.ToInt32(ViewState["Action"].ToString());
            CustomStatus cs = (CustomStatus)objLMC.AddLeadStatus(Convert.ToInt32(ddlLeadStatus.SelectedValue), Convert.ToInt32(ViewState["userno"]), Convert.ToInt32(Session["userno"]), txt_Remark.Text.ToString().Trim(), enquiryNo, action, Convert.ToDateTime(txtEndDate.Text.Trim()), Convert.ToInt32(Session["OrgId"]));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(this.updPopUp, "Record Saved Successfully.", this.Page);
                ClearPopUp();
                return;
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(this.updPopUp, "Record Updated Successfully.", this.Page);
                ClearPopUp();
                return;
            }
            else
            {
                objCommon.DisplayMessage(this.updPopUp, "Something Went Wrong.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
 
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        ddlLeadStatus.SelectedIndex = 0;
        txt_Remark.Text = string.Empty;
        txtEndDate.Text = string.Empty;
    }
    protected void imgEditLead_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            liRemark.Attributes.Add("style", "display:block");
            //liRemark.Visible = true;
            liLeadStatus.Attributes.Add("style", "display:block");
            liFollowDate.Attributes.Add("style", "display:block");
            divButtonModel.Attributes.Add("style", "display:block");
            DataSet ds = objLMC.Check_Lead_Status(Convert.ToInt32(ViewState["userno"]));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["ENQUIRYNO"].ToString()) > 0)
                    {
                        ddlLeadStatus.SelectedValue = ds.Tables[1].Rows[0]["ENQUIRYSTATUS"].ToString().Equals(string.Empty) ? "0" : ds.Tables[1].Rows[0]["ENQUIRYSTATUS"].ToString();
                        txt_Remark.Text = ds.Tables[1].Rows[0]["REMARKS"].ToString().Equals(string.Empty) ? "" : ds.Tables[1].Rows[0]["REMARKS"].ToString();
                        txtEndDate.Text = ds.Tables[1].Rows[0]["NEXT_FOLLOWUP_DATE"].ToString().Equals(string.Empty) ? "" : ds.Tables[1].Rows[0]["NEXT_FOLLOWUP_DATE"].ToString();
                        ViewState["Enquiry"] = ds.Tables[1].Rows[0]["ENQUIRYNO"].ToString();
                        ViewState["Action"] = 2;
                    }
                    else
                    {
                        ViewState["Action"] = 1;
                        ViewState["Enquiry"] = 0;
                    }
                }
                else
                {
                    ViewState["Action"] = 1;
                    ViewState["Enquiry"] = 0;
                }
            }
            else
            {
                ViewState["Action"] = 1;
                ViewState["Enquiry"] = 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearPopUp()
    {
        ddlLeadStatus.SelectedIndex = 0;
        txt_Remark.Text = string.Empty;
        txtEndDate.Text = string.Empty;
    }
    protected void ddlAdmBatch_Remark_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch_Remark.SelectedIndex > 0)
            {
                DataSet ds = objLMC.Get_Lead_Remarks(Convert.ToInt32(ddlAdmBatch_Remark.SelectedValue));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lvRemark.DataSource = ds;
                        lvRemark.DataBind();
                        lvRemark.Visible = true;
                    }
                    else
                    {
                        lvRemark.DataSource = null;
                        lvRemark.DataBind();
                        lvRemark.Visible = false;
                    }
                }
            }
            else
            {
                lvRemark.DataSource = null;
                lvRemark.DataBind();
                lvRemark.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void ddlMainUser_SelectedIndexChanged1(object sender, EventArgs e)
    {
         try
        {
            ViewState["subuser"] = null;
            DataSet dsGetSubcouncellor = null;
            dsGetSubcouncellor = objLMC.GetSubcouncellorDetails(Convert.ToInt32(ddlMainUser.SelectedValue));
                       
            if (dsGetSubcouncellor.Tables[0].Rows.Count > 0)
            {
                //objCommon.FillDropDownList(ddlsubuser, "ACD_LEAD_COUNSELOR_ALLOTMENT CA INNER JOIN USER_ACC UA ON (CA.SUBUSER_UA_NO=UA.UA_NO)", "DISTINCT CA.SUBUSER_UA_NO", "UA_NAME", "MAINUSER_UA_NO=" + Convert.ToInt32(ddlMainUser.SelectedValue) , "CA.SUBUSER_UA_NO");               
                divSubCounsellor.Visible = true;          
                ddlsubuser.Items.Clear();
                ddlsubuser.Items.Add("Please Select");
                ddlsubuser.SelectedItem.Value = "0";

                if (dsGetSubcouncellor.Tables[0].Rows.Count > 0)
                {
                    ddlsubuser.DataSource = dsGetSubcouncellor;
                    ddlsubuser.DataValueField = dsGetSubcouncellor.Tables[0].Columns[0].ToString();
                    ddlsubuser.DataTextField = dsGetSubcouncellor.Tables[0].Columns[1].ToString();
                    ddlsubuser.DataBind();
                    ddlsubuser.SelectedIndex = 0;
                }
            }
            else
            {
                
                divSubCounsellor.Visible = false;
               
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}

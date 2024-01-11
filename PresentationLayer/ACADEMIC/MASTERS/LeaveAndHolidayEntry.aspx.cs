//======================================================================================
// PROJECT NAME  : UAIMS                                                        
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Event And Holidays Entry Form.                                      
// CREATION DATE :                                                        
// CREATED BY    :                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

public partial class ACADEMIC_MASTERS_LeaveAndHolidayEntry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
   

    #region Page Methods
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
                    this.CheckPageAuthorization();
                    Blob_Storage();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    this.FillDropdown();
                    this.BindListView();

                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }

            this.BindListView();
            divMsg.InnerHtml = string.Empty;
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  -  Added By Nikhil L. on 29/01/2022
            //objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  Added By Nikhil L. on 29/01/2022
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=LeaveAndHolidayEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=LeaveAndHolidayEntry.aspx");
        }
    }
    private void Blob_Storage()
    {

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
    }
    private void FillDropdown()
    {
        try
        {
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN SESSION_ACTIVITY A ON A.SESSION_NO=S.SESSIONNO ", " Distinct S.SESSIONNO", "S.SESSION_NAME", "S.SESSIONNO>0 AND STARTED=1", "S.SESSIONNO DESC");
            objCommon.FillDropDownList(ddlLeaveName, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "specialleavetypeno");

            //objCommon.FillDropDownList(ddlSessionBulk, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND EXAMTYPE=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
            //objCommon.FillDropDownList(ddlLeaveTypeBulk, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0", "specialleavetypeno");
            //Added by Nikhil L. on 29/01/2022
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_NAME");
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
            DataSet ds = objAttC.GetAllLeave(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(Session["userno"].ToString()));
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExamday.DataSource = ds;
                lvExamday.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvExamday);//Set label - 
            }
            else
            {
                lvExamday.DataSource = null;
                lvExamday.DataBind();
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvExamday.Items)
                {
                    ImageButton btnEdit = item.FindControl("btnEdit") as ImageButton;
                    Label lblStatus = item.FindControl("lblAStatus") as Label;

                    if (lblStatus.Text == "PENDING")
                    {
                        btnEdit.Enabled = true;
                        btnEdit.ImageUrl = "~/Images/edit.png";
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        if (lblStatus.Text == "APPROVED")
                        {
                            btnEdit.ImageUrl = "~/images/check1.jpg";
                            //btnEdit.ImageUrl.wi="50px";
                            btnEdit.Width = Unit.Pixel(15);
                        }
                        if (lblStatus.Text == "REJECTED")
                        {
                            btnEdit.ImageUrl = "~/Images/delete.png";
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
    #endregion

    #region Single Students

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlOdType, "ACD_ODTYPE", "ODID", "OD_NAME", "ODID>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "ODID");

        btnReport.Enabled = true;
        this.BindListViewApproval();
        this.BindListView();

        LoadSlots();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowDetails();
    }

    private void ShowDetails()
    {
        try
        {
            if (txtRollNo.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage(this, "Please Enter Student Registration No./PRN No.", this.Page);
                txtRollNo.Focus();
                return;
            }

            //string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'");
            string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "' OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'AND ISNULL(ADMCAN,0)=0");
            string sessionno = ddlSession.SelectedValue;

            if (string.IsNullOrEmpty(idno))
            {
                objCommon.DisplayMessage(updHoliday, "Student with Univ. Reg. No. OR Adm. No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                //divCourses.Visible = false;
                //divNote.Visible = true;
                return;
            }

            string facAdvisor = objCommon.LookUp("ACD_STUDENT", "ISNULL(FAC_ADVISOR,0)", "REGNO = '" + txtRollNo.Text.Trim() + "'OR ENROLLNO = '" + txtRollNo.Text.Trim() + "'");


            if ((string.IsNullOrEmpty(facAdvisor) || facAdvisor != Session["userno"].ToString()) && Session["usertype"].ToString() == "3")
            {
                objCommon.DisplayMessage(updHoliday, "You are not faculty Advisor of selected Univ. Reg  No. OR Adm. No." + txtRollNo.Text.Trim() + "!", this.Page);
                txtRollNo.Text = string.Empty;
                txtRollNo.Focus();
                //divNote.Visible = true;
                this.ClearControls();
                return;
            }

            string branchno = objCommon.LookUp("ACD_STUDENT", "BRANCHNO", "IDNO=" + idno);
            string degreeno = objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + idno);
            string deptno = objCommon.LookUp("ACD_SCHEME", "DEPTNO", "BRANCHNO=" + branchno + " AND DEGREENO=" + degreeno);
            //string hoddeptno = Session["userdeptno"].ToString();

            //if (Convert.ToInt32(deptno) == hoddeptno || hoddeptno == 0)
            //{
            string college_id = objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + idno);
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(college_id), "SESSIONNO DESC");
            //ddlSession.Focus();

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,(CASE WHEN S.FATHERNAME = '' THEN '--' WHEN S.FATHERNAME IS NULL THEN '--' ELSE FATHERNAME END)FATHERNAME,(CASE WHEN S.MOTHERNAME = '' THEN '--' WHEN S.MOTHERNAME IS NULL THEN '--' ELSE MOTHERNAME END)MOTHERNAME,S.REGNO,S.ROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,ISNULL(S.PHYSICALLY_HANDICAPPED,0) AS PH", "S.IDNO = " + idno, string.Empty);

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    SingleStudOD.Visible = true;
                    btnSubmit.Enabled = true;
                    btnReport.Enabled = true;
                    int count = Convert.ToInt32(objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(IDNO) ", "IDNO=" + idno + " AND PHOTO IS NOT NULL"));
                    if (count > 0)
                    {
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/IMAGES/nophoto.jpg";
                    }
                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    //ddlSemester.SelectedValue = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    //Payment Type..
                    //ddlPayType.SelectedValue = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();

                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString() == "False" ? "No" : "Yes";
                    //objCommon.FillDropDownList(ddlSession, "ACD_STUDENT_RESULT R INNER JOIN ACD_SESSION_MASTER S ON (R.SESSIONNO = S.SESSIONNO)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "IDNO =" + idno + " AND SEMESTERNO=" + lblSemester.ToolTip + "", "S.SESSIONNO");
                    tblInfo.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(updHoliday, "Student with Reg  No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                    divCourses.Visible = false;
                    //divNote.Visible = true;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvExamday_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "APPROVAL_STATUS");

            if (status == "PENDING")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.SandyBrown;
            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "REJECTED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void ddlOdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOdType.SelectedValue == "1")//Normal OD
        {
            txtToDate.Enabled = false;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }
        else //Special  OD
        {
            txtToDate.Enabled = true;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }
    }
    public void LoadSlots()
    {
        if (txtFromDate.Text != "" && ddlSession.SelectedValue != null)
        {
            DataSet ds = null;
            int idno = lblName.ToolTip != string.Empty ? Convert.ToInt32(lblName.ToolTip) : 0;
            ds = objAttC.GetSelectedDateSlots(Convert.ToInt32(ddlSession.SelectedValue), idno, Convert.ToDateTime(txtFromDate.Text));

            if (ddlOdType.SelectedValue == "1")//Normal OD
            {
                txtToDate.Text = txtFromDate.Text;
                txtToDate.Enabled = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkSlots.DataSource = ds;
                    chkSlots.DataTextField = "SLOTTIME";
                    chkSlots.DataValueField = "SLOTNO";
                    chkSlots.DataBind();
                    chkSlots.Visible = true;
                    chkCheckAll.Visible = true;
                    chkCheckAll.Enabled = true;
                    lblSlot.Visible = true;
                }
                else
                {
                    objCommon.DisplayMessage(updHoliday, "No Slots available for selected date.", this);
                    chkSlots.DataSource = null;
                    chkSlots.DataBind();
                    chkSlots.Visible = false;
                    chkCheckAll.Visible = false;
                    chkCheckAll.Enabled = false;
                    lblSlot.Visible = false;
                }
            }
            else //Special  OD
            {
                txtToDate.Enabled = true;
                // txtToDate.Text = string.Empty;
                chkCheckAll.Enabled = false;
            }
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            LoadSlots();
        }
        catch
        {
            throw;
        }
    }

    protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckAll.Checked == true)
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = false;
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Single student leave apply
        try
        {

            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
            string slotno = string.Empty, idno = string.Empty, regno = string.Empty;
            int odType = 1;

            if (txtFromDate.Text != string.Empty)// | txtToDate.Text != string.Empty)
            {
                hiddenfieldfromDt.Value = txtFromDate.Text;

                #region commented
                //hiddenFieldToDt.Value = txtToDate.Text;
                //if (ChkDate.Checked)
                //{
                //    if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value) | (Convert.ToDateTime(txtToDate.Text) > Convert.ToDateTime(hiddenFieldToDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value))))
                //    {
                //        objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
                //        return;
                //    }
                //    if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                //    {
                //        objCommon.DisplayMessage(updHoliday, "From Date should be Lesser than To Date", this.Page);
                //        return;
                //    }
                //}
                //else
                //{
                #endregion commented
                if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(hiddenfieldfromDt.Value)))
                {
                    objCommon.DisplayMessage(updHoliday, "Please Select Date in Proper Range", this.Page);
                    return;
                }

                if ((Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text)))
                {
                    objCommon.DisplayMessage(updHoliday, "Please Select Date in Proper Range", this.Page);
                    txtToDate.Focus();
                    //this.BindListViewWithOD();
                    return;
                }

                objAttModel.LeaveStartDate = Convert.ToDateTime(txtFromDate.Text);
                if (ddlOdType.SelectedValue == "2")
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtToDate.Text);
                else
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtFromDate.Text);
                objAttModel.College_code = Session["colcode"].ToString();
                objAttModel.Event_Detail = txtEventDetail.Text;
                objAttModel.LEAVENO = Convert.ToInt32(ddlLeaveName.SelectedValue);
                idno = lblName.ToolTip;
                regno = txtRollNo.Text.Trim();
                objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);


                for (int i = 0; i < chkSlots.Items.Count; i++)
                {
                    if (chkSlots.Items[i].Selected == true)
                        slotno += chkSlots.Items[i].Value + ',';
                }
                if (!string.IsNullOrEmpty(slotno))
                    slotno = slotno.Substring(0, slotno.Length - 1);

                if (ddlOdType.SelectedValue == "1")//if NORMAL OD then slots selection mandatory. 
                {
                    if (slotno == string.Empty)
                    {
                        objCommon.DisplayMessage(updHoliday, "Please select atleast one slot!", this.Page);
                        return;
                    }
                }
                odType = Convert.ToInt32(ddlOdType.SelectedValue);
                if (fuDoc.HasFile)
                {
                    string contentType = contentType = fuDoc.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(fuDoc.PostedFile.FileName);
                    string userno = Session["userno"].ToString();
                    // string Leavtype = ddlLeaveName.SelectedItem.Text;
                    string OTP = GenerateOTP();
                    if (ext == ".pdf")
                    {
                        HttpPostedFile file = fuDoc.PostedFile;
                        string filename = userno + "_ODLEAVE_" + OTP;
                        ViewState["filename"] = filename + ext;
                        int fileSize = fuDoc.PostedFile.ContentLength;
                        int KB = fileSize / 1024;
                        if (KB <= 150)
                        {
                            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                            {
                                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_ODLEAVE_" + OTP, fuDoc);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                            }
                            else
                            {
                                if (CheckDuplicateEntry() == true)
                                {
                                    objCommon.DisplayMessage(updHoliday, "Entry For This Date Already Done!", this.Page);
                                    return;
                                }
                                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_ODLEAVE_" + OTP, fuDoc);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                        return;
                    }
                }

                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //if (CheckDuplicateEntry() == true)
                    //{
                    //    objCommon.DisplayMessage(updHoliday, "Entry for this Date Already Done!", this.Page);
                    //    return;
                    //}
                    objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objAttModel.Holiday_No = Convert.ToInt32(ViewState["holidayno"]);//Holiday no.
                    string filename = string.Empty;
                    if (ViewState["filename"] == null)
                    {
                        if (lblFileName.Text == "")
                        {
                            filename = string.Empty;
                        }
                        else
                        {
                            filename = lblFileName.Text;
                        }
                    }
                    else
                    {
                        filename = ViewState["filename"].ToString();
                    }
                    CustomStatus cs = (CustomStatus)objAttC.UpdateLeaveDetails(objAttModel, idno, regno, slotno, odType, filename);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Updated Successfully!", this.Page);
                    }
                }
                else
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage(updHoliday, "Entry for this Date Already Done!", this.Page);
                        return;
                    }

                    objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    string filename = string.Empty;
                    if (ViewState["filename"] == null)
                    {
                        filename = string.Empty;
                    }
                    else
                    {
                        filename = ViewState["filename"].ToString();
                    }
                    //Add New
                    CustomStatus cs = (CustomStatus)objAttC.AddLeaveDetails(objAttModel, idno, regno, slotno, odType, filename);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(updHoliday, "Transaction Failed", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updHoliday, "Please Enter Date", this.Page);
                return;
            }

            this.BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ClearControls()
    {
        ddlSession.SelectedIndex = 0;
        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ViewState["action"] = null;
        chkSlots.Items.Clear();
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        lblSlot.Visible = false;
        ddlOdType.SelectedIndex = 0;
    }

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            int Academic_Session = 0;
            //if (ChkDate.Checked)
            //    Academic_Session = objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "HOLIDAY_NO", "CONVERT(nvarchar,ACADEMIC_HOLIDAY_STDATE,103)= dbo.DateOnly('" + Convert.ToDateTime(txtFromDate.Text.Trim()) + "')  AND CONVERT(nvarchar,ACADEMIC_HOLIDAY_ENDDATE,103) = dbo.DateOnly('" + Convert.ToDateTime(txtToDate.Text.Trim()) + "') ");
            //else
            //   
            Academic_Session = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "COUNT(*)", "isnull(cancel,0)=0 and idno=" + Convert.ToInt32(lblName.ToolTip) + " AND ISNULL(APPROVAL_STATUS,0)<>2 AND ( ( CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) between CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) and CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) )  )"));

            if (Academic_Session > 0)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return flag;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int holidayno = int.Parse(btnEdit.CommandArgument);
            ViewState["holidayno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(holidayno);

            ddlSession.SelectedIndex = 1;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SessionController objsessionn = new SessionController();
            ImageButton btnDelete = sender as ImageButton;
            int Sessionno = int.Parse(btnDelete.CommandArgument);

            //Delete 
            //CustomStatus cs = (CustomStatus)objsessionn.DeleteAcademicLeave(Convert.ToInt32(btnDelete.ToolTip));
            objCommon.DisplayMessage(updHoliday, "Holiday Entry Deleted Successfully !!", this.Page);
            this.BindListView();
            this.ClearControls();
            return;

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowDetails(int Holidayno)
    {
        try
        {
            string _slotNos = string.Empty;
            SqlDataReader dr = objAttC.GetSingleAcademicLeave(Holidayno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtRollNo.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();

                    txtEventDetail.Text = dr["ACADEMIC_HOLIDAY_DETAIL"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_DETAIL"].ToString();
                    ddlLeaveName.SelectedValue = dr["ACADEMIC_LEAVE_NO"] == null ? string.Empty : dr["ACADEMIC_LEAVE_NO"].ToString();
                    lblName.ToolTip = dr["IDNO"].ToString();
                    if (dr["ODTYPE"] == null | dr["ODTYPE"].ToString().Equals(""))
                        ddlOdType.SelectedIndex = 0;
                    else
                        ddlOdType.SelectedValue = dr["ODTYPE"].ToString();

                    txtFromDate.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    if (dr["FILENAME"] == null)
                    {
                        lblFileName.Visible = false;
                    }
                    else
                    {
                        lblFileName.Visible = true;
                        lblFileName.Text = dr["FILENAME"].ToString();
                    }
                    
                    if (!string.IsNullOrEmpty(txtFromDate.Text))
                    {
                        this.txtFromDate_TextChanged(new object(), new EventArgs());
                    }
                    if (dr["SESSIONNO"] == null | dr["SESSIONNO"].ToString().Equals(""))
                        ddlSession.SelectedIndex = 0;
                    else
                        ddlSession.SelectedValue = dr["SESSIONNO"].ToString();


                    if (chkSlots.Items.Count > 0)
                    {
                        _slotNos = dr["SLOTNO"].ToString();
                        string[] values = _slotNos.Split(',');
                        foreach (string a in values)
                        {
                            for (int i = 0; i < chkSlots.Items.Count; i++)
                            {
                                if (chkSlots.Items[i].Value == a)
                                    chkSlots.Items[i].Selected = true;
                            }
                        }
                    }


                    //if (dr["ACADEMIC_HOLIDAY_ENDDATE"] == null | dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString().Equals(""))
                    //{
                    //    tdToDate.Visible = false;
                    //    //lblFromDate.Text = "Event Date";
                    //    ChkDate.Checked = false;
                    //}
                    //else
                    //{
                    //    ChkDate.Checked = true;
                    //    tdToDate.Visible = true;

                    //    //lblFromDate.Text = "From Date";
                    //}
                }
            }
            ShowDetails();
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdoReportType.SelectedValue == "2")
            {
                GridView GVStudData = new GridView();

                DataSet ds = objAttC.GetODApplyLeave(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(Session["userno"].ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVStudData.DataSource = ds;
                    GVStudData.DataBind();

                    string attachment = "attachment;filename=ListOfOD_Applied_Students.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.Charset = "";
                    Response.ContentType = "application/ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    GVStudData.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                else
                {
                    objCommon.DisplayMessage("Record Not Found!!", this.Page);
                    return;
                }
            }
            else
            {
                this.ReportDetails("rptSpecialLeave", "rptSpecialLeave.rpt");
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    //showing the report in pdf formate as per as  selection of report name  or file name.
    private void ReportDetails(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UATYPE=" + Convert.ToInt32(Session["usertype"].ToString()) + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //Shows report
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            //url += "exporttype=" + exporttype;
            //url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + rdoReportType.SelectedValue;
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_UATYPE=" + Convert.ToInt32(Session["usertype"].ToString()) + ",@P_UANO=" + Convert.ToInt32(Session["userno"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updHoliday, this.updHoliday.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDeptwise_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("rptSpecialLeave", "rptSpecialLeaveDeptWise.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        this.BindListView();
    }

    #endregion Single Students

    #region Approval

    private void ShowLeaveDetails(int Holidayno, int collegeno, int sessionno)
    {
        try
        {
            string _slotNos = string.Empty;
            SqlDataReader dr = objAttC.GetSingleLeaveForApproval(Holidayno, collegeno, sessionno);
            if (dr != null)
            {
                if (dr.Read())
                {
                    lblRegno.Text = dr["REGNO"] == null ? string.Empty : dr["REGNO"].ToString();
                    lblStudname.Text = dr["STUDNAME"] == null ? string.Empty : dr["STUDNAME"].ToString();
                    lblStudname.ToolTip = dr["IDNO"] == null ? string.Empty : dr["IDNO"].ToString();
                    lblLeaveType.Text = dr["specialleavetype"] == null ? string.Empty : dr["specialleavetype"].ToString();
                    lblfromDate.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_STDATE"].ToString();
                    lblToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString();
                    lblLeaveDetail.Text = dr["ACADEMIC_HOLIDAY_DETAIL"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_DETAIL"].ToString();
                    leaveStatus.Text = dr["APPROVAL_STATUS"] == null ? string.Empty : dr["APPROVAL_STATUS"].ToString();
                    if (leaveStatus.Text == "PENDING")
                        leaveStatus.ForeColor = System.Drawing.Color.SandyBrown;
                    if (leaveStatus.Text == "APPROVED")
                        leaveStatus.ForeColor = System.Drawing.Color.Green;
                    if (leaveStatus.Text == "REJECTED")
                        leaveStatus.ForeColor = System.Drawing.Color.Red;

                    hdnLeaveNo.Value = dr["HOLIDAY_NO"].ToString();
                    hdnSessionNo.Value = dr["SESSIONNO"].ToString();
                    lblODType.Text = dr["OD_NAME"] == null ? string.Empty : dr["OD_NAME"].ToString();

                    //=========================================================//
                    if (dr["ODTYPE"] == null | dr["ODTYPE"].ToString().Equals(""))
                        hdnODType.Value = "0";
                    else
                        hdnODType.Value = dr["ODTYPE"].ToString();

                    txtFromDate.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(lblfromDate.Text))
                    {
                        this.getSelectDateSlotByStudent(Convert.ToInt32(dr["ODTYPE"].ToString()), Convert.ToInt32(dr["SESSIONNO"].ToString()));
                    }

                    if (chkSelectdSlots.Items.Count > 0)
                    {
                        _slotNos = dr["SLOTNO"].ToString();
                        string[] values = _slotNos.Split(',');
                        foreach (string a in values)
                        {
                            for (int i = 0; i < chkSelectdSlots.Items.Count; i++)
                            {
                                if (chkSelectdSlots.Items[i].Value == a)
                                    chkSelectdSlots.Items[i].Selected = true;
                            }
                        }
                    }
                    chkSelectdSlots.ToolTip = dr["OD_COUNT"] == null ? string.Empty : dr["OD_COUNT"].ToString();
                    if (Convert.ToInt32(Session["usertype"].ToString()) == 3)
                    {
                        if (!string.IsNullOrEmpty(dr["OD_COUNT"].ToString()))
                        {
                            int count = 0;
                            for (int i = 0; i < chkSelectdSlots.Items.Count; i++)
                            {
                                if (chkSelectdSlots.Items[i].Selected == true)
                                    count += 1;
                            }
                            if ((Convert.ToInt32(dr["OD_COUNT"]) + count) >= 63)
                            {
                                btnSubmitStatus.Enabled = false;
                                ODCountFlag.Visible = true;
                                ODCountFlag.Text = "Approval Limit Exceeded !";
                                ODCountFlag.ToolTip = dr["OD_COUNT"] == null ? string.Empty : dr["OD_COUNT"].ToString();
                                ODCountFlag.ForeColor = System.Drawing.Color.Red;
                                ODCountFlag.Font.Bold = true;
                            }
                            else
                            {
                                btnSubmitStatus.Enabled = true;
                                ODCountFlag.Visible = false;
                                ODCountFlag.Text = string.Empty;
                            }
                        }
                    }
                    else
                    {
                        btnSubmitStatus.Enabled = true;
                        ODCountFlag.Visible = false;
                        ODCountFlag.Text = string.Empty;
                    }

                    //=========================================================//
                    string Status = dr["A_STATUS"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, typeof(string), "uniqueKey", "SetCheckBox(" + Status + ");", true);
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void getSelectDateSlotByStudent(int odType, int sessionno)
    {
        chkSelectdSlots.Items.Clear();
        DataSet ds = objAttC.GetSelectedDateSlots(Convert.ToInt32(sessionno), Convert.ToInt32(lblStudname.ToolTip), Convert.ToDateTime(lblfromDate.Text));
        if (odType == 1)//Normal OD
        {
            //txtToDate.Text = txtFromDate.Text;
            //txtToDate.Enabled = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkSelectdSlots.DataSource = ds;
                chkSelectdSlots.DataTextField = "SLOTTIME";
                chkSelectdSlots.DataValueField = "SLOTNO";
                chkSelectdSlots.DataBind();
                chkSelectdSlots.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(updHoliday, "No Slots available for selected date.", this);
                chkSelectdSlots.DataSource = null;
                chkSelectdSlots.DataBind();
                chkSelectdSlots.Visible = false;
            }
        }
        else //Special  OD
        {
            //txtToDate.Enabled = true;
            //// txtToDate.Text = string.Empty;
            //chkCheckAll.Enabled = false;
        }
    }

    protected void btnShowLeave_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkButton = sender as LinkButton;
            int leaveNo = int.Parse(lnkButton.CommandArgument);
            int sessionno = int.Parse(lnkButton.ToolTip);
            this.ShowLeaveDetails(leaveNo, Convert.ToInt32(ddlCollege.SelectedValue), sessionno);
            //OPEN POPUP
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "OpenPreview();", true);
            ScriptManager.RegisterStartupScript(Page, GetType(), "OpenPreview", "<script>OpenPreview()</script>", false);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void btnSubmitStatus_Click(object sender, EventArgs e)
    {
        //Leave Approval panel for Status update
        int status = 0, attCount = 0;
        string slotnos = string.Empty;
        try
        {
            int leaveNo = Convert.ToInt32(hdnLeaveNo.Value);
            int sessionno = Convert.ToInt32(hdnSessionNo.Value);
            if (Request.Form["Status"] != null)
            {
                status = Convert.ToInt32(Request.Form["Status"].ToString());
            }

            //============================================================//
            for (int i = 0; i < chkSelectdSlots.Items.Count; i++)
            {
                if (chkSelectdSlots.Items[i].Selected == true)
                    slotnos += chkSelectdSlots.Items[i].Value + ',';
            }

            if (!string.IsNullOrEmpty(slotnos))
                slotnos = slotnos.Substring(0, slotnos.Length - 1);

            if (hdnODType.Value == "1")//if NORMAL OD then slots selection mandatory. 
            {
                if (slotnos == string.Empty)
                {
                    objCommon.DisplayMessage(updHoliday, "Please select atleast one slot!", this.Page);
                    return;
                }
            }
            //============================================================//

            if (status == 1)
            {
                //check for Attendance is available for the date and status is Present
                DataSet ds = null;
                string slotnames = string.Empty;
                string fdate = Convert.ToDateTime(lblfromDate.Text.ToString()).ToString("yyyy/MM/dd");
                string toDate = Convert.ToDateTime(lblToDate.Text.ToString()).ToString("yyyy/MM/dd");
                if (hdnODType.Value == "1")//if NORMAL OD then slots selection mandatory. 
                {
                    if (slotnos == string.Empty)
                    {
                        objCommon.DisplayMessage(updHoliday, "Please select atleast one slot!", this.Page);
                        return;
                    }
                    else
                    {
                        attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "SA.STUDIDS=" + lblStudname.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0 AND SLOTNO IN(" + slotnos + ")"));
                        if (attCount > 0)
                        {
                            ds = objCommon.FillDropDown("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO INNER JOIN ACD_TIME_SLOT S ON S.SLOTNO=A.SLOTNO", "DISTINCT A.SLOTNO,TIMEFROM+' - '+TIMETO AS SLOTTIME", "", "SA.STUDIDS=" + lblStudname.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE)AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0 AND A.SLOTNO IN(" + slotnos + ")", "");
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                slotnames = slotnames + dr["SLOTTIME"].ToString() + ',';
                            }
                        }
                    }
                }
                else // IF ODTYPE IS SPECIAL OD THEN CHECK ATT. MARKED OR NOT BETWEEN START DATE & END DATE..
                {
                    attCount = Convert.ToInt32(objCommon.LookUp("ACD_ATTENDANCE A INNER JOIN ACD_SUB_ATTENDANCE SA ON SA.ATT_NO=A.ATT_NO", "COUNT(*)", "SA.STUDIDS=" + lblStudname.ToolTip + " and CAST(ATT_DATE AS DATE) between CAST('" + fdate + "' AS DATE) and CAST('" + toDate + "' AS DATE) AND SA.ATT_STATUS = 1 AND ISNULL(A.CANCEL,0) = 0 "));
                }
                if (attCount > 0)
                {
                    if (hdnODType.Value == "1")//if NORMAL OD then slots selection mandatory. 
                    {
                        lblmessageShow.Text = "Attendance for the selected slots " + slotnames + " has marked as Present ! Please mark as Absent for Approval.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
                        objCommon.DisplayMessage(updHoliday, "Attendance for the selected slots <b>" + slotnames + "</b> has marked as Present ! Please mark as Absent for Approval.", this.Page);
                    }
                    else
                    {
                        lblmessageShow.Text = "Attendance for the selected OD date has marked as Present ! Please mark as Absent for Approval.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal2();", true);
                        objCommon.DisplayMessage(this, "Attendance for the selected OD date has marked as Present ! Please mark as Absent for Approval.", this);
                    }
                    return;
                }
            }
            int ua_no = Convert.ToInt32(Session["userno"]);
            CustomStatus cs = (CustomStatus)objAttC.UpdateLeaveStatus(leaveNo, status, slotnos, ua_no, ViewState["ipAddress"].ToString());  //added by nehal
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                this.ShowLeaveDetails(leaveNo, Convert.ToInt32(ddlCollege.SelectedValue), sessionno);
                this.BindListViewApproval();
                objCommon.DisplayMessage(updHoliday, "Leave Record Updated Successfully!", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvLeaveApproval_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton lnkbtn = e.Item.FindControl("btnShowLeave") as LinkButton;
        HiddenField hdnODTYPE = e.Item.FindControl("hdnODTYPE") as HiddenField;
        Label lblRegNo = e.Item.FindControl("lblRegNo") as Label;

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "APPROVAL_STATUS");
            int ODTYPE = (int)DataBinder.Eval(item.DataItem, "ODTYPE");
            int OD_COUNT = (int)DataBinder.Eval(item.DataItem, "OD_COUNT");

            if (status == "PENDING")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.SandyBrown;
                if (ODTYPE == 2 && Convert.ToInt32(Session["usertype"].ToString()) == 3)//faculty advisor can't approve special od as per req.13-03-2020
                {
                    lnkbtn.Enabled = false;
                }
                if (OD_COUNT >= 63 && Convert.ToInt32(Session["usertype"].ToString()) == 3)//faculty advisor can't approve OD after 63 slots as per req.13-03-2020
                {
                    lnkbtn.Enabled = false;
                    //lblRegNo.ForeColor = System.Drawing.Color.Red;
                    //lblRegNo.Font.Bold = true;
                }
                if (OD_COUNT >= 63)
                {
                    lblRegNo.ForeColor = System.Drawing.Color.Red;
                    lblRegNo.Font.Bold = true;
                }
            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Green;
                if (OD_COUNT >= 63)
                {
                    lblRegNo.ForeColor = System.Drawing.Color.Red;
                    lblRegNo.Font.Bold = true;
                }
            }
            else if (status == "REJECTED")
            {
                (e.Item.FindControl("lblStatus") as Label).ForeColor = System.Drawing.Color.Red;
                lnkbtn.Enabled = false;
                if (OD_COUNT >= 63)
                {
                    lblRegNo.ForeColor = System.Drawing.Color.Red;
                    lblRegNo.Font.Bold = true;
                }
            }
        }
    }

    private void BindListViewApproval()
    {
        try
        {
            DataSet ds = objAttC.GetAllLeaveForApproval(Convert.ToInt32(Session["usertype"].ToString()), Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlSession.SelectedValue));
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvLeaveApproval.DataSource = ds;
                lvLeaveApproval.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvLeaveApproval);//Set label - 
            }
            else
            {
                lvLeaveApproval.DataSource = null;
                lvLeaveApproval.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion Approval

    #region Bulk Apply

    protected void ddlSessionBulk_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;

        //lvStudents.DataSource = null;
        //lvStudents.DataBind();

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlDegree.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "B.BRANCHNO", "LONGNAME", "B.BRANCHNO<>99 AND DEGREENO = " + ddlDegree.SelectedValue, "B.BRANCHNO");
        //    ddlBranch.Focus();
        //}
        //else
        //{

        //}

        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        //lvStudents.DataSource = null;
        //lvStudents.DataBind();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBranch.SelectedIndex > 0)
        //{
        //    objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME", "SCHEMENO", "SCHEMENAME", "DEGREENO = " + ddlDegree.SelectedValue + " AND BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
        //    ddlScheme.Focus();
        //}
        //else
        //{
        //    ddlScheme.Items.Clear();
        //    ddlBranch.SelectedIndex = 0;
        //}

        //ddlSemester.SelectedIndex = 0;
        //lvStudents.DataSource = null;
        //lvStudents.DataBind();
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlScheme.SelectedIndex > 0)
            //{               
            //    ddlSemester.Items.Clear();
            //    string odd_even = objCommon.LookUp("ACD_SESSION_MASTER", "ODD_EVEN", "SESSIONNO=" + Convert.ToInt32(ddlSessionBulk.SelectedValue));
            //    string exam_type = objCommon.LookUp("ACD_SESSION_MASTER", "EXAMTYPE", "SESSIONNO=" + Convert.ToInt32(ddlSessionBulk.SelectedValue));
            //    string semCount =  objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH A INNER JOIN ACD_BRANCH B ON B.BRANCHNO=A.BRANCHNO", "CAST(DURATION AS INT)*2 AS DURATION", "B.BRANCHNO=" + ddlBranch.SelectedValue + "");

            //    if (exam_type == "1" && odd_even != "3")
            //    {
            //        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.ODD_EVEN=" + odd_even + "AND SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");
            //    }
            //    else
            //    {
            //        objCommon.FillDropDownList(ddlSemester, "ACD_STUDENT S INNER JOIN ACD_SEMESTER SM ON(S.SEMESTERNO=SM.SEMESTERNO)", "DISTINCT SM.SEMESTERNO", "SM.SEMESTERNAME ", "SM.SEMESTERNO<=" + semCount + "", "SM.SEMESTERNO");

            //    }
            //    ddlSemester.Focus();
            //}
            //else
            //{
            //    ddlSemester.Items.Clear();
            //    ddlScheme.SelectedIndex = 0;
            //}

            //lvStudents.DataSource = null;
            //lvStudents.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListViewBulk();
    }

    protected void ChkDate_CheckedChanged(object sender, EventArgs e)
    {
        //if (ChkDate.Checked)
        //{           
        //    tdToDate.Visible = true;            
        //}
        //else
        //{           
        //    tdToDate.Visible = false;            
        //}
        this.BindListView();
    }

    //Save leave in Bulk
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //    if (txtFromDateBulk.Text != string.Empty | txtToDateBulk.Text != string.Empty)
            //    {
            //        hiddenfieldfromDtBulk.Value = txtFromDateBulk.Text;
            //        hiddenFieldToDtBulk.Value = txtToDateBulk.Text;
            //        if (chkDateBulk.Checked)
            //        {
            //            if ((Convert.ToDateTime(txtFromDateBulk.Text) < Convert.ToDateTime(hiddenfieldfromDtBulk.Value)) | (Convert.ToDateTime(txtToDateBulk.Text) < Convert.ToDateTime(hiddenfieldfromDtBulk.Value) | (Convert.ToDateTime(txtToDateBulk.Text) > Convert.ToDateTime(hiddenFieldToDtBulk.Value)) | (Convert.ToDateTime(txtToDateBulk.Text) < Convert.ToDateTime(hiddenfieldfromDtBulk.Value))))
            //            {
            //                objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
            //                return;
            //            }
            //            if (Convert.ToDateTime(txtFromDateBulk.Text) > Convert.ToDateTime(txtToDateBulk.Text))
            //            {
            //                objCommon.DisplayMessage(updHoliday, "From Date should be Lesser than To Date", this.Page);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            if ((Convert.ToDateTime(txtFromDateBulk.Text) < Convert.ToDateTime(hiddenfieldfromDtBulk.Value)) | (Convert.ToDateTime(txtFromDateBulk.Text) > Convert.ToDateTime(hiddenfieldfromDtBulk.Value)))
            //            {
            //                objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
            //                return;
            //            }
            //        }                 
            //        objAttModel.Sessionno = Convert.ToInt32(ddlSessionBulk.SelectedValue);

            //        objAttModel.FromDate = Convert.ToDateTime(txtFromDateBulk.Text);//HOLIDAY START DT
            //        if (chkDateBulk.Checked)
            //            objAttModel.ToDate = Convert.ToDateTime(txtToDateBulk.Text);///HOLIDAY END DT
            //        else
            //            objAttModel.ToDate = Convert.ToDateTime(txtFromDateBulk.Text);
            //        objAttModel.CollegeCode = Convert.ToInt32(Session["colcode"].ToString());//

            //        objAttModel.LeaveDetail = txtLeaveDetailBulk.Text;
            //        objAttModel.LEAVENO = Convert.ToInt32(ddlLeaveTypeBulk.SelectedValue);

            //        foreach (ListViewDataItem lvItem in lvStudents.Items)
            //        {
            //            CheckBox chkBox = lvItem.FindControl("chkSelect") as CheckBox;
            //            if (chkBox.Checked == true)
            //                objAttModel.StudId += chkBox.ToolTip + ",";
            //        }

            //        if (objAttModel.StudId.Length <= 0)
            //        {
            //            objCommon.DisplayMessage(this.UpdatePanel1, "Please Select Student", this.Page);
            //            return;
            //        }

            //        //Add Leave Bulk
            //        if (objAttC.AddLeaveBulk_Student(objAttModel) == Convert.ToInt32(CustomStatus.RecordUpdated))
            //            objCommon.DisplayMessage(this.UpdatePanel1, "Leave Added Successfully..", this.Page);
            //        else
            //            objCommon.DisplayMessage(this.UpdatePanel1, "Server Error", this.Page);

            //    }
            //    else
            //    {
            //        objCommon.DisplayMessage(updHoliday, "Please Enter Date", this.Page);
            //        return;
            //    }

            //    ClearControlsBulk();             
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindListViewBulk()
    {
        try
        {
            ////StudentController objSC = new StudentController();
            //DataSet ds = objAttC.GetStudentsForLeaveApply(Convert.ToInt16(ddlSessionBulk.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    lvStudents.DataSource = ds;
            //    lvStudents.DataBind();
            //    hftot.Value = ds.Tables[0].Rows.Count.ToString();
            //}
            //else
            //{
            //    objCommon.DisplayMessage(this.UpdatePanel1, "No Student found !", this.Page);
            //    lvStudents.DataSource = null;
            //    lvStudents.DataBind();
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //if ((e.Item.FindControl("lblAdTeacher") as Label).ToolTip != string.Empty)
        //{
        //    DataSet ds = objCommon.FillDropDown("USER_ACC", "UA_NO", "UA_FULLNAME", "UA_NO IN(" + (e.Item.FindControl("lblAdTeacher") as Label).ToolTip + ")", "UA_NO");
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTableReader dtr = ds.Tables[0].CreateDataReader();
        //        while (dtr.Read())
        //        {
        //            (e.Item.FindControl("lblAdTeacher") as Label).Text += dtr["UA_FULLNAME"].ToString() + ",";
        //        }
        //        dtr.Close();
        //    }
        //}
    }

    private void ClearControlsBulk()
    {
        //ddlSessionBulk.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlScheme.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        //txtFromDateBulk.Text = string.Empty;
        //txtToDateBulk.Text = string.Empty;
        //ddlLeaveTypeBulk.SelectedIndex = 0;
        //txtLeaveDetailBulk.Text = string.Empty;
        //txtTotStud.Text = string.Empty;
        //lvStudents.DataSource = null;
        //lvStudents.DataBind();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void SelectDate(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
        }
    }

    #endregion Bulk Apply

    protected void chkSelectdSlots_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        if (Convert.ToInt32(Session["usertype"].ToString()) == 3)//faculty advisor can't approve OD after 63 slots as per req.13-03-2020
        {
            for (int i = 0; i < chkSelectdSlots.Items.Count; i++)
            {
                if (chkSelectdSlots.Items[i].Selected == true)
                    count += 1;
            }
            if ((Convert.ToInt32(chkSelectdSlots.ToolTip) + count) > 63)
            {
                btnSubmitStatus.Enabled = false;
                ODCountFlag.Visible = true;
                ODCountFlag.Text = "Approval Limit Exceeded !";
                ODCountFlag.ForeColor = System.Drawing.Color.Red;
                ODCountFlag.Font.Bold = true;
            }
            else
            {
                btnSubmitStatus.Enabled = true;
                ODCountFlag.Visible = false;
                ODCountFlag.Text = string.Empty;
            }
        }
        else
        {
            btnSubmitStatus.Enabled = true;
            ODCountFlag.Visible = false;
            ODCountFlag.Text = string.Empty;
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO DESC");
                ddlSession.Focus();
                
            }
            else
            {
                ddlCollege.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void imgbtnpfPrevDoc2_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;


        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc ;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
        }
        else
        {
            if (extension == ".pdf")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                //Response.Flush();
                Response.End();
            }
        }
    }
    protected void imgbtnpfPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;


        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
        }
        else{
            if (extension == ".pdf")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                //Response.Flush();
                Response.End();
            }      
        }
    }
    private string GenerateOTP()
    {
        string allowedChars = "";

        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string otpString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            otpString += temp;
        }
        return otpString;
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = System.IO.Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.UpdatePanel5, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = System.IO.Path.GetFileNameWithoutExtension(FileName);
        try
        {
            System.Threading.Tasks.Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

}

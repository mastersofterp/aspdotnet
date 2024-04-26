// 08-04-2024

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;
using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using System.Net;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_CancelScholershipAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    Student objS = new Student();
    QrCodeController objQrC = new QrCodeController();
    int prev_status;

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
                   // CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }

                    PopulateDropDownList();
                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    //ShowDetails();
                }


                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6") // For RCPIT and RCPIPER)
                {
                    ddlScholarShipsType.SelectedValue = "1";
                    divschmode.Visible = false;  
                    divSemester.Visible = false;
                    divddlSchlWiseBulk.Visible = false;
                    divddlSort.Visible = false;
                }
                else
                {
                    divschmode.Visible = true;
                    ddlSchMode.SelectedValue = "2";
                    divSemester.Visible = true;
                    divddlSchlWiseBulk.Visible = true;
                    lblYearMandatory.Visible = true;
                    divddlSort.Visible = true;
                }

            }
            //txtSemesterAmountEnabledHidden.Value = string.Empty;
            // lblSession.Text = Convert.ToString(Session["sessionname"]);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=CancelScholershipAllotment.aspx");
    //        }
    //    }
    //    else
    //    {
    //        Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=CancelScholershipAllotment.aspx");
    //    }
    //}

    protected void PopulateDropDownList()
    {
        try
        {
            // FILL DROPDOWN SCHEME TYPEbtnShow_Click
            //objCommon.FillDropDownList(ddlSchemetype, "ACD_SCHEMETYPE", "SCHEMETYPENO", "SCHEMETYPE", "SCHEMETYPENO > 0", "SCHEMETYPENO");
            // FILL DROPDOWN BATCH
            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");
           
            //objCommon.FillDropDownList(ddlColg, "ACD_college_master", "college_id", "college_name", "college_id>0", "college_id");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");
            //  objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 ", "SEMESTERNO");
            objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR>0", "YEAR");
            objCommon.FillDropDownList(ddlScholarShipsType, "ACD_SCHOLORSHIPTYPE", "SCHOLORSHIPTYPENO", "SCHOLORSHIPNAME", "SCHOLORSHIPTYPENO > 0  AND ACTIVESTATUS=1 ", "SCHOLORSHIPTYPENO");  // added on 2020 feb 11
            objCommon.FillDropDownList(ddlAcdYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ACTIVE_STATUS=1", "ACADEMIC_YEAR_ID DESC");
            // FILL DROPDOWN ADMISSION BATCH
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        try
        {
            if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
            {
                this.BindListView();
            }
            else
            {
                if (ddlYear.SelectedValue == "0")
                {
                    objCommon.DisplayMessage(this.Page, "Please Select Year", this.Page);
                    return;
                }
                else
                {
                    this.BindListView();
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void BindListView()
    {
        try
        {
            DataSet ds;
            int sessionno = Convert.ToInt32(Session["currentsession"]);

            if (rbRegEx.SelectedIndex == 0)
            {
                prev_status = 0;
            }
            else
            {
                prev_status = 1;
            }

            string dd = ddlScholarShipsType.SelectedValue;
            int ScholarshipId = Convert.ToInt32(ddlScholarShipsType.SelectedValue);


            ds = studCont.GetStudentCancelScholershipAllotment(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue),Convert.ToInt32(ddlAdmBatch.SelectedValue), prev_status, Convert.ToInt32(ddlAcdYear.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue), ScholarshipId);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divNote.Visible = true;
                divSchltype.Visible = true;
                //ddlScholarShipsType.SelectedIndex = 2;
                Panel1.Visible = true;
                lvStudentRecords.Visible = true;
                lvStudentRecords.DataSource = ds;
                lvStudentRecords.DataBind();
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudentRecords);//Set label 
                hftot.Value = lvStudentRecords.Items.Count.ToString();

                foreach (ListViewDataItem itm in lvStudentRecords.Items)
                {
                   // CheckBox chk = itm.FindControl("chkReport") as CheckBox;
                    TextBox txtSchAmt = itm.FindControl("txtSemesterAmount") as TextBox;
                    HiddenField hdidno = itm.FindControl("hidIdNo") as HiddenField;
                    HiddenField hdfBranchno = itm.FindControl("hdfBranchno") as HiddenField;
                    Label lblDegreeno = itm.FindControl("lblschamt") as Label;

                   

                    string count = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "COUNT(1)", "IDNO=" + hdidno.Value + "  AND BRANCHNO= " + hdfBranchno.Value + "  AND DEGREENO=" + lblDegreeno.ToolTip + "AND  YEAR=" + lblDegreeno.Text + " AND  SCHOLARSHIP_ID=" + Convert.ToInt32(ddlScholarShipsType.SelectedValue));

                    if (count != "0")
                    {
                        txtSchAmt.Enabled = false;
                       
                        
                    }
                    else
                    {
                        if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")// For RCPIT and RCPIPER)
                        {
                            divamount.Visible = false;
                        }
                        else
                        {
                            if (ddlSchMode.SelectedValue == "2")
                            {
                                divamount.Visible = true;
                                txtSchAmt.Text = txtAmountsch.Text;
                            }
                            else if (ddlSchMode.SelectedValue == "2")
                            {
                                divamount.Visible = false;
                            }
                        }
                        txtSchAmt.Enabled = true;
                       
                    }
                }
            }

            else
            {
                objCommon.DisplayMessage(updtime, "Record Not Found,Kindly Check for Demand is Creates or Not.", this.Page);
                lvStudentRecords.DataSource = null;
                lvStudentRecords.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlSemester.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
            Session["listIdCard"] = null;
            Response.Redirect(Request.Url.ToString());
            txtSemesterAmountEnabledHidden.Value = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.btnCancel_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                ddlBranch.Items.Clear();

                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " and B.College_id=" + ddlColg.SelectedValue, "A.LONGNAME");
                ddlBranch.Focus();
            }
            else
            {
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("Please Select", "0"));

                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            }
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            divNote.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.ddlDegree_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranch.SelectedIndex > 0)
            {

                ddlSemester.Items.Clear();
                objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 ", "SEMESTERNO");
                ddlSemester.Focus();

            }
            else
            {

                ddlSemester.Items.Clear();
                ddlSemester.Items.Add(new ListItem("Please Select", "0"));
            }
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            divNote.Visible = false;
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.ddlBranch_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

   


    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE A INNER JOIN ACD_DEGREE B ON(A.DEGREENO=B.DEGREENO) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "B.DEGREENAME", "A.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "a.DEGREENO");
            lvStudentRecords.DataSource = null;
            lvStudentRecords.DataBind();
            divNote.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.ddlColg_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int InsertCount = 0;
        int UpdateCount = 0;
    
        int SCHLMODE = 0;
        int Percentage = 0;
        try
        {
           
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholarShipsType.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            
            int totalRow = lvStudentRecords.Items.Count;
            int counter = 0;
            foreach (ListViewItem item in lvStudentRecords.Items)
            {
                string output = "";
                HiddenField hfidno = (HiddenField)item.FindControl("hidIdNo");
                HiddenField hfSchlshipno = (HiddenField)item.FindControl("hfSchlshipno");
                TextBox txtSemestersAmount = (TextBox)item.FindControl("txtSemesterAmount");
                objS.IdNo = Convert.ToInt32(hfidno.Value);
                int Schlshipno = Convert.ToInt32(hfSchlshipno.Value);
                string SemesterNo = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SEMESTERNO", "IDNO=" + objS.IdNo + " AND SCHLSHPNO=" + Schlshipno);
                objS.SemesterNo = Convert.ToInt32(SemesterNo);
               
                objS.AcademicYearNo = Convert.ToInt32(ddlAcdYear.SelectedValue);


                if (txtSemestersAmount.Text == "")
                {
                    objS.Amount = null;
                }
                else
                {
                    objS.Amount = txtSemestersAmount.Text;
                }



                if (Session["OrgId"].ToString() == "1" || Session["OrgId"].ToString() == "6")
                {
                    SCHLMODE = 0;
                    Percentage = 0;
                }
                else
                {
                    SCHLMODE = Convert.ToInt32(ddlSchMode.SelectedValue);

                    if (SCHLMODE == 1)
                    {
                        Percentage = Convert.ToInt32(txtschAmt.Text);
                    }
                    else
                    {
                        Percentage = 0;
                    }
                }

                

                if (objS.Amount != null)
                {
                    if (ddlSchlWiseBulk.SelectedValue == "1")
                    {
                        if (txtSemestersAmount.Enabled == true || txtSemesterAmountEnabledHidden.Value == "true")
                        {
                            output = studCont.InsertStudentCancelScholershipDetailsSemWise(objS, Convert.ToInt32(ddlAcdYear.SelectedValue), SCHLMODE, Percentage);
                        }
                    }
                    else
                    {
                        if (txtSemestersAmount.Enabled == true || txtSemesterAmountEnabledHidden.Value == "true")
                        {
                            output = studCont.InsertStudentCancelScholershipDetails(objS, Convert.ToInt32(ddlAcdYear.SelectedValue), SCHLMODE, Percentage);
                           
                        }
                    }

                }
                else if (objS.Amount == null)
                {
                    counter++;
                }
                if (output == "1")
                {
                    InsertCount++;
                }
                else if (output == "2")
                {
                    UpdateCount++;
                }
            }
            txtSemesterAmountEnabledHidden.Value = string.Empty;
         
            if (InsertCount > 0)
            {
                objCommon.DisplayMessage(updtime, "Records Saved Successfully", this.Page);
                BindListView();
            }
            else if (UpdateCount > 0)
            {
                objCommon.DisplayMessage(updtime, "Records Updated Successfully", this.Page);
                BindListView();
            }
            else if (totalRow == counter)
            {
                objCommon.DisplayMessage(updtime, "Please Enter amount", this.Page);
            }
            else
            {
                //objCommon.DisplayMessage(updtime, "Some Error Occured", this.Page);
                objCommon.DisplayMessage(updtime, "Please Select at least One Student Record", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    protected void ddlAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        //ddlScholarShipsType.SelectedIndex = 0;
        //divSchltype.Visible = false;
    }
    protected void rbRegEx_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudentRecords.DataSource = null;
        lvStudentRecords.DataBind();
        divNote.Visible = false;
        ddlScholarShipsType.SelectedIndex = 0;
        divSchltype.Visible = false;
    }
  
    

    protected void ddlSchMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchMode.SelectedValue == "1")
            {
                divAmt.Visible = true;
                divamount.Visible = false;
            }
            else
            {
                divAmt.Visible = false;
                divamount.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.ddlSchMode_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDeleteAllotment_Click(object sender, ImageClickEventArgs e)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            ImageButton btnDeleteAllotment = sender as ImageButton;
            ListViewItem lvItem = (ListViewItem)btnDeleteAllotment.NamingContainer;
            HiddenField hfidno = (HiddenField)lvItem.FindControl("hidIdNo");
         
            HiddenField hfSchlshipno = (HiddenField)lvItem.FindControl("hfSchlshipno");
            objS.IdNo = Convert.ToInt32(hfidno.Value);
            int Schlshipno = Convert.ToInt32(hfSchlshipno.Value);
            objS.DegreeNo = Convert.ToInt32(ddlDegree.SelectedValue);
            objS.BranchNo = Convert.ToInt32(ddlBranch.SelectedValue);
            objS.AcademicYearNo = Convert.ToInt32(ddlAcdYear.SelectedValue);
            objS.AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            objS.ScholershipTypeNo = Convert.ToInt32(ddlScholarShipsType.SelectedValue);
            objS.Uano = Convert.ToInt32(Session["userno"].ToString());
            objS.IPADDRESS = Session["ipAddress"].ToString().Trim();

            string SemesterNo = objCommon.LookUp("ACD_STUDENT_SCHOLERSHIP", "SEMESTERNO", "IDNO=" + objS.IdNo + " AND SCHLSHPNO=" + Schlshipno);
            objS.SemesterNo = Convert.ToInt32(SemesterNo);

            if (Schlshipno > 0)
            {
                retStatus = studCont.DeleteCancelScholarshipAllotment(objS, Schlshipno);
                if (retStatus == Convert.ToInt32(CustomStatus.RecordDeleted))
                {
                    objCommon.DisplayMessage(this.updtime, "Scholarship Allotment Deleted Successfully!", this.Page);
                    BindListView();
                }
                else
                {
                    objCommon.DisplayMessage(this.updtime, "Error occurred!", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.btnDeleteAllotment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEdit = sender as ImageButton;
    //        ListViewItem lvItem = (ListViewItem)btnEdit.NamingContainer;
    //        TextBox txtSemAmount = (TextBox)lvItem.FindControl("txtSemesterAmount");
    //        txtSemAmount.Enabled = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "ACADEMIC_CancelScholershipAllotment.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
       
    //}
}

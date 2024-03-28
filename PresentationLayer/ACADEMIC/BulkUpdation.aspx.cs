using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.OleDb;

public partial class ACADEMIC_BulkUpdation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

    int[] selectedItems;


    #region Page Action
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    {
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    }

                    if (Session["usertype"].ToString() != "2") //Added By Nikhil V.Lambe on 11/03/2021 for page should be access by Admin and HOD.
                        this.PopulateDropDown();
                    else
                        Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
                }
                objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
                objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  - 
            }
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkUpdation.aspx");
        }
    }
    #endregion

    private void PopulateDropDown()
    {
        try
        {
            if (Session["usertype"].ToString() != "1")
            {
                divDept1.Visible = true;
                divDept2.Visible = true;
                //objCommon.FillDropDownList(ddlDepartment, "USER_ACC U INNER JOIN ACD_DEPARTMENT D ON(U.UA_DEPTNO=D.DEPTNO)", "DISTINCT D.DEPTNO", "D.DEPTNAME", "D.DEPTNO IN(" + Convert.ToString(Session["userdeptno"]) + ")", "DEPTNO");
                DataSet ds = objCommon.FillDropDownDepartmentUserWise(Convert.ToInt32(Session["usertype"]), Convert.ToString(Session["userdeptno"]));
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlDepartment.DataSource = ds;
                    ddlDepartment.DataValueField = ds.Tables[0].Columns[0].ToString();
                    ddlDepartment.DataTextField = ds.Tables[0].Columns[1].ToString();
                    ddlDepartment.DataBind();
                    ddlDepartment.SelectedIndex = 0;
                }
            }

            objCommon.FillDropDownList(ddlAdm2, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");

            objCommon.FillDropDownList(ddlSemester1, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddldegree1, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");

            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO DESC");

            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE WITH (NOLOCK)", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.BindListView();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            StudentController objSC = new StudentController();
            string studids = string.Empty;
            string categorys = string.Empty;
            string rollnos = string.Empty;
            string FullName = string.Empty;
            string FirstName = string.Empty;
            string MiddleName = string.Empty;
            string LastName = string.Empty;
            string paddress = string.Empty;
            string femail = string.Empty;   //ADDED BY VINAY MISHRA ON 28082023 FOR 47935
            string memail = string.Empty;   //ADDED BY VINAY MISHRA ON 28082023 FOR 47935

            int Ua_no = Convert.ToInt32 (Session["userno"]);
            string Ip_Address = string.Empty;
            if (ddlCat.SelectedValue == "9" )
            {
                foreach (ListViewDataItem lvs in lvStudFather.Items)
                {
                    studids += (lvs.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    if (ddlCat.SelectedValue == "9" || ddlCat.SelectedValue == "10")
                    {
                        FullName += (lvs.FindControl("txtName") as TextBox).Text + "$";
                        FirstName += (lvs.FindControl("txtFirstName") as TextBox).Text + "$";
                        MiddleName += (lvs.FindControl("txtMiddle") as TextBox).Text + "$";
                        LastName += (lvs.FindControl("txtLastName") as TextBox).Text + "$";

                    }
                }
                int fieldID = Convert.ToInt32(ddlCat.SelectedValue);
                string IpAddress=Request.ServerVariables["REMOTE_ADDR"];
                if (objSC.UpdateStudentAndFatherInfo(studids, fieldID, FullName, FirstName, MiddleName, LastName, IpAddress,ddlCat.SelectedItem.Text, Convert.ToInt32(Ua_no)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updStudent, "Data Updated Successfully.", this.Page);
                    return;
                }
                else
                    objCommon.DisplayMessage(this.updStudent, "Server Error.", this.Page);

                //if()
            }

            if (ddlCat.SelectedValue == "15")       //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
            {
                foreach (ListViewDataItem lvs in lvStudParentEmail.Items)
                {
                    studids += (lvs.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    if (ddlCat.SelectedValue == "15")
                    {
                        femail += (lvs.FindControl("txtFatherEmail") as TextBox).Text + "$";
                        memail += (lvs.FindControl("txtMotherEmail") as TextBox).Text + "$";
                    }
                }
                int fieldID = Convert.ToInt32(ddlCat.SelectedValue);
                string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (objSC.UpdateFatherAndMotherEmail(studids, fieldID, femail, memail, IpAddress, ddlCat.SelectedItem.Text, Convert.ToInt32(Ua_no)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updStudent, "Data Updated Successfully.", this.Page);
                    return;
                }
                else
                    objCommon.DisplayMessage(this.updStudent, "Server Error.", this.Page);
            }

            if (ddlCat.SelectedValue != "9" && ddlCat.SelectedValue != "15")
            {

                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    // for email and mobile number radio button addedd by safal gupta on 05022021 

                    //Convert.ToInt32((lvItem.FindControl("ddlcat") as DropDownList).SelectedValue) > 0 || 
                    if ((lvItem.FindControl("txtAdmDate") as TextBox).Text != "" || (lvItem.FindControl("txtusn") as TextBox).Text != "" || (lvItem.FindControl("ddlcat1") as DropDownList).SelectedItem.Text != "" || (lvItem.FindControl("txtemail") as TextBox).Text != "")
                    {
                        studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";

                        if (ddlCat.SelectedValue == "2")
                        {
                            categorys += (lvItem.FindControl("txtAdmDate") as TextBox).Text + "$";
                        }
                        else if (ddlCat.SelectedValue == "5" || ddlCat.SelectedValue == "7" || ddlCat.SelectedValue == "16" || ddlCat.SelectedValue == "17" || ddlCat.SelectedValue == "18" || ddlCat.SelectedValue == "19") // Modified By Shrikant W. on 28-11-2023                       
                        {
                            categorys += (lvItem.FindControl("txtusn") as TextBox).Text + "$";
                        }
                        else if (ddlCat.SelectedValue == "8")
                        {
                            categorys += (lvItem.FindControl("txtemail") as TextBox).Text + "$";

                        }
                        else if (ddlCat.SelectedValue == "10" || ddlCat.SelectedValue == "11")
                        {
                            categorys += (lvItem.FindControl("txtusn") as TextBox).Text + "$";
                        }
                        else if (ddlCat.SelectedValue == "13")
                        {
                            categorys += (lvItem.FindControl("txtLAdd") as TextBox).Text + "$";
                            paddress += (lvItem.FindControl("txtpadd") as TextBox).Text + "$";
                        }
                        else
                        {
                            categorys += (lvItem.FindControl("ddlcat1") as DropDownList).SelectedValue + "$";
                        }
                    }

                }
                //if (studids.Length <= 0 && categorys.Length <= 0)
                //{
                //    objCommon.DisplayMessage(this.updStudent, "Please Select Values for Filter.", this.Page);
                //    return;
                //}
                int fieldID = Convert.ToInt32(ddlCat.SelectedValue);


                if (objSC.UpdateStudentCategory(studids, categorys, fieldID,paddress) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updStudent, "Data Updated Successfully.", this.Page);
                }
                else
                    objCommon.DisplayMessage(this.updStudent, "Server Error.", this.Page);
            }
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
            pnlStudFather.Visible = false;
            PnlStudParentEmail.Visible = false;
            lblAddressNote.Visible = false;
            DataSet ds = null;
            DataSet dsStudFath = null;
            DataSet dsParentEmail = null;
            if (ddlCat.SelectedValue == "1")  // for College Code
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.BLOODGRPNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_COLLEGECODE SC WITH (NOLOCK) ON (S.COLLEGECODE = SC.CODENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.COLLEGECODE AS COLUMNID,SC.CODENAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "2") // for Student Type
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_IDTYPE SC WITH (NOLOCK) ON (S.IDTYPE = SC.IDTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDTYPE AS COLUMNID ,SC.IDTYPEDESCRIPTION,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "3") // KEA status
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.STUDNAME,S.ADMDATE AS COLUMNNAME ,'' AS PCOLUMNNAME,S.CATEGORYNO AS COLUMNID,SC.CATEGORY", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_KEA_STATUS SC WITH (NOLOCK) ON (S.KEA_STATUS = SC.KEANO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.KEA_STATUS AS COLUMNID,SC.KEA_NAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "4") // Claim Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT JOIN ACD_CASTE C WITH (NOLOCK) ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.CATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "5") // Allotted Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.ADMCATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMCATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "6") // Admission Batch
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_GENDER SC WITH (NOLOCK) ON (S.SEX = SC.SEX)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SEX AS COLUMNID,SC.GENDERNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
               // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH SC WITH (NOLOCK) ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "7") // Blood Group
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //S.CSN_NO,
            }
            else if (ddlCat.SelectedValue == "8") // Admission Date
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.EMAILID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO AS COLUMNID", "REGNO, CSN_NO, STUDNAME, ADMDATE", "DEGREENO =" + ddlDegree.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "CSN_NO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "9")
            {
                dsStudFath = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME AS COLUMNNAME , S.STUDFIRSTNAME  AS COLUMNFIRSTNAME,S.STUDMIDDLENAME AS COLUMNMIDDLENAME,S.STUDLASTNAME AS COLUMNLASTNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "IDNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.REGNO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "10")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
              
                //S.CSN_NO,
            }
            else if (ddlCat.SelectedValue == "11")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MOTHERNAME AS COLUMNNAME ,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.CSN_NO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "12")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SHIFT AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
               // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_CASTE C WITH (NOLOCK) ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_ADMBATCH SC ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //S.CSN_NO,
            }
            else if (ddlCat.SelectedValue == "13")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
               // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENTTYPE P WITH (NOLOCK) ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //S.CSN_NO,
            }
            else if (ddlCat.SelectedValue == "14")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_MEDIUMOFINSTRUCTION_MASTER MIM WITH (NOLOCK) ON (MIM.MEDIUMID = S.MEDIUM_INSTRUCT_NO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.MEDIUM_INSTRUCT_NO AS COLUMNID,MIM.MEDIUMNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH A WITH (NOLOCK) ON (S.ACAD_YR = A.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,A.BATCHNAME AS COLUMNNAME, S.ACAD_YR AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

            }
            else if (ddlCat.SelectedValue == "15")
            {
                dsParentEmail = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHER_EMAIL AS COLUMNNAMEFATHEREMAIL,S.MOTHER_EMAIL AS COLUMNNAMEMOTHEREMAIL, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STATE_RANK, S.STUDNAME,S.STATE_RANK AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "16")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MERITNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.QEXMROLLNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "17")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO ", "S.REGNO,S.STUDNAME,S.FATHERMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID, '' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.ORDER_NO, S.STUDNAME,S.ORDER_NO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "18")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME,S.MOTHERMOBILE AS COLUMNNAME , S.IDNO AS COLUMNID , '' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENT_GROUP P WITH (NOLOCK) ON(P.GROUP_ID=S.AIDED) ", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYMENT_GROUP AS COLUMNNAME, S.AIDED AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (ddlCat.SelectedValue == "19")  // Added by Shrikant W. on 28-12-2023 for ABCC ID
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ABCC_ID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }


            #region Command
            //else if (ddlCat.SelectedValue == "19")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "20")
            //{
            //    //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_GENDER SC ON (S.SEX = SC.SEX) ", "S.IDNO", "S.REGNO, S.STUDNAME,S.SEX AS COLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //    //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.BLOODGRPNO AS COLUMNID,SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_GENDER SC WITH (NOLOCK) ON (S.SEX = SC.SEX)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SEX AS COLUMNID,SC.GENDERNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}

            //else if (ddlCat.SelectedValue == "21")// FOR MOBILE ADDED BY SAFAL
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "22")// FOR EMAIL ADDED BY SAFAL
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.EMAILID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "23") //  Category
            //{
             
            //}
            //else if (ddlCat.SelectedValue == "24")
            //{
            //    //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDFIRSTNAME AS COLUMNNAME,S.STUDMIDDLENAME,S.STUDLASTNAME, S.STUDNAME,S.IDNO AS COLUMNID", "S.DEGREENO=" + ddlDegree.SelectedValue + "AND S.SEMESTERNO=" + ddlSemester.SelectedValue + "AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + "AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //    dsStudFath = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME AS COLUMNNAME , S.STUDFIRSTNAME  AS COLUMNFIRSTNAME,S.STUDMIDDLENAME AS COLUMNMIDDLENAME,S.STUDLASTNAME AS COLUMNLASTNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "IDNO");
            //}
            //else if (ddlCat.SelectedValue == "25")
            //{
            //    //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDFIRSTNAME AS COLUMNNAME,S.STUDMIDDLENAME,S.STUDLASTNAME, S.STUDNAME,S.IDNO AS COLUMNID", "S.DEGREENO=" + ddlDegree.SelectedValue + "AND S.SEMESTERNO=" + ddlSemester.SelectedValue + "AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + "AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //    //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.FATHERNAME AS COLUMNNAME , S.FATHERFIRSTNAME  AS COLUMNFIRSTNAME,S.FATHERMIDDLENAME AS COLUMNMIDDLENAME,S.FATHERLASTNAME AS COLUMNLASTNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "IDNO");
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "26")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MOTHERNAME AS COLUMNNAME ,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "27")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SHIFT AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "28")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
               
            //}
            //else if (ddlCat.SelectedValue == "29")  //ADDED BY VINAY MISHRA ON 17/08/2023 TO ADD MEDIUM OF INSTRUCTION FIELD FOR UPDATION
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_MEDIUMOFINSTRUCTION_MASTER MIM WITH (NOLOCK) ON (MIM.MEDIUMID = S.MEDIUM_INSTRUCT_NO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.MEDIUM_INSTRUCT_NO AS COLUMNID,MIM.MEDIUMNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "30")  //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
            //{
            //    dsParentEmail = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHER_EMAIL AS COLUMNNAMEFATHEREMAIL,S.MOTHER_EMAIL AS COLUMNNAMEMOTHEREMAIL, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "31")  //ADDED BY VINAY MISHRA ON 14/09/2023 TO ADD MERIT NUMBER FIELD FOR UPDATION
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MERITNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "32")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO ", "S.REGNO,S.STUDNAME,S.FATHERMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID, '' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            //else if (ddlCat.SelectedValue == "33")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME,S.MOTHERMOBILE AS COLUMNNAME , S.IDNO AS COLUMNID , '' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            //}
            #endregion Command
            #region studfather
            if (ddlCat.SelectedValue == "9")
            {
                if (dsStudFath != null && dsStudFath.Tables.Count > 0)
                {
                    pnlStudent.Visible = false;
                    if (dsStudFath.Tables[0].Rows.Count > 0)
                    {
                     
                        pnlStudFather.Visible = true;
                        lvStudFather.DataSource = dsStudFath;
                        lvStudFather.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudFather);//Set label -
                        btnSubmit.Enabled = true;
                        //Label lblName = (Label)lvStudents.FindControl("lblFields");
                        //lblName.Text = rdbCat.SelectedItem.Text.ToUpper();
                    }
                    else
                    {
                        lvStudFather.DataSource = null;
                        lvStudFather.DataBind();
                        objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                        rdbCat.ClearSelection();
                        btnSubmit.Enabled = false;
                    }
                }

                else
                {
                    lvStudFather.DataSource = null;
                    lvStudFather.DataBind();

                    objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                    rdbCat.ClearSelection();
                    btnSubmit.Enabled = false;
                }
            }
            #endregion

            //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
            #region "Parents Email Updation"    
            if (ddlCat.SelectedValue == "15")
            {
                if (dsParentEmail != null && dsParentEmail.Tables.Count > 0)
                {
                    PnlStudParentEmail.Visible = false;
                    if (dsParentEmail.Tables[0].Rows.Count > 0)
                    {
                        //lvStudents.Visible = false;
                        PnlStudParentEmail.Visible = true;
                        lvStudParentEmail.Visible = true;
                        lvStudParentEmail.DataSource = dsParentEmail;
                        lvStudParentEmail.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudParentEmail);//Set label -
                        btnSubmit.Enabled = true;
                        //Label lblName = (Label)lvStudents.FindControl("lblFields");
                        //lblName.Text = rdbCat.SelectedItem.Text.ToUpper();
                    }
                    else
                    {
                        lvStudParentEmail.DataSource = null;
                        lvStudParentEmail.DataBind();
                        objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                        rdbCat.ClearSelection();
                        btnSubmit.Enabled = false;
                    }
                }

                else
                {
                    lvStudParentEmail.DataSource = null;
                    lvStudParentEmail.DataBind();

                    objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                    rdbCat.ClearSelection();
                    btnSubmit.Enabled = false;
                }
            }
            #endregion

            if (ddlCat.SelectedValue != "9" && ddlCat.SelectedValue != "15")
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {                        
                        pnlStudent.Visible = true;
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "$('#thReason1').hide();$('td:nth-child(9)').hide();var prm = Sys.WebForms.PageRequestManager.getInstance();prm.add_endRequest(function () { $('#thReason1').hide();$('td:nth-child(9)').hide();});", true);
                        lvStudents.DataSource = ds;
                        lvStudents.DataBind();
                        Control ctrHeader = lvStudents.FindControl("thDivPAddress");
                        ctrHeader.Visible = (ddlCat.SelectedValue != "13") ? false : true;

                        foreach (ListViewItem lvRow in lvStudents.Items)
                        {
                            Control BlockStat = (Control)lvRow.FindControl("tdDivPAddress");
                            BlockStat.Visible = (ddlCat.SelectedValue != "13") ? false : true;
                        }
                        
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -

                        //}
                        btnSubmit.Enabled = true;
                        Label lblName = (Label)lvStudents.FindControl("lblFields");
                        lblName.Text = ddlCat.SelectedItem.Text.ToUpper();
                    }
                    else
                    {
                        lvStudents.DataSource = null;
                        lvStudents.DataBind();
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
                        objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                        rdbCat.ClearSelection();
                        btnSubmit.Enabled = false;
                    }
                }

                else
                {
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
                    objCommon.DisplayMessage(this.updStudent, "No Students found for selected criteria.", this.Page);
                    rdbCat.ClearSelection();
                    btnSubmit.Enabled = false;
                }
            }

            ViewState["DataSet"] = ds;
            ViewState["DataSet1"] = dsParentEmail;
            ViewState["DataSet2"] = dsStudFath;
       
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void Clear()
    {
        //ddlAdmBatch.SelectedIndex = 0;
        //ddlDegree.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
        //lvStudents.DataSource = null;
        //lvStudents.DataBind();        
        //btnSubmit.Enabled = false;
        //rdbCat.SelectedIndex = 0;
        //trFilter.Visible = false;
        // Response.Redirect(Request.RawUrl);
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAdmBatch.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Admission Batch", this.Page);
            return;
        }

        if (ddlDegree.SelectedIndex > 0)
        {
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CD.BRANCHNO > 0 ", "B.LONGNAME"); //AND CD.COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue)          
            ddlSemester.SelectedValue = "0";
            trFilter.Visible = false;
            rdbCat.ClearSelection();
        }
        else
        {
            ddlBranch.Items.Clear();
            ddlDegree.SelectedIndex = 0;
            trFilter.Visible = false;
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -

    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            pnlStudFather.Visible = false;
            PnlStudParentEmail.Visible = false;
            pnlStudent.Visible = true;
            //lvStudFather.Visible = false;
            lvStudParentEmail.Visible = false;
            lvStudents.Visible = true;
            DataSet ds = null;
            DropDownList ddlcat1 = e.Item.FindControl("ddlcat1") as DropDownList;
            TextBox txtAdmissionDate = e.Item.FindControl("txtAdmDate") as TextBox;//Admission Date  
            Image imgCal = e.Item.FindControl("imgFrmDt") as Image; // calender image
            TextBox txtUSN = e.Item.FindControl("txtusn") as TextBox; // USN No
            TextBox txtemail = e.Item.FindControl("txtemail") as TextBox;// for email
            TextBox txtLAdd = e.Item.FindControl("txtLAdd") as TextBox;  // for local address
           //TextBox tdDivPAddress = e.Item.FindControl("txtPAdd") as TextBox; // for p address
            if (ddlCat.SelectedValue == "1")  // for College Code
            {
                //lblStudent.Visible = true;
                //ds = objCommon.FillDropDown("ACD_COLLEGECODE", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                //ds = objCommon.FillDropDown("ACD_COLLEGECODE WITH (NOLOCK)", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                txtemail.Visible = false;
                txtLAdd.Visible = false;
                ds = objCommon.FillDropDown("ACD_BLOODGRP WITH (NOLOCK)", "BLOODGRPNO AS COLUMNID", "BLOODGRPNAME AS COLUMNNAME", "BLOODGRPNO > 0", "BLOODGRPNO");
            }
            else if (ddlCat.SelectedValue == "2") // for Student Type
            {

                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ddlcat1.Visible = false;
                txtUSN.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

                //lblStudent.Visible = true;
                //txtemail.Visible = false;
               // txtLAdd.Visible = false;

                ds = objCommon.FillDropDown("ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO AS COLUMNID", "IDTYPEDESCRIPTION AS COLUMNNAME", "", "IDTYPENO");
            }
            else if (ddlCat.SelectedValue == "3") // KEA status
            {
                ddlcat1.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;
                ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");

               // txtLAdd.Visible = false;
             //   lblStudent.Visible = true;
                //txtemail.Visible = false;
                //ds = objCommon.FillDropDown("ACD_KEA_STATUS WITH (NOLOCK)", "KEANO AS COLUMNID", "KEA_NAME AS COLUMNNAME", "", "KEANO");
            }
            else if (ddlCat.SelectedValue == "4") // Claim Category
            {
                ds = objCommon.FillDropDown("ACD_CASTE WITH (NOLOCK)", "CASTENO AS COLUMNID", "CASTE AS COLUMNNAME", "CASTENO > 0", "CASTENO");
                ddlcat1.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;
            }
            else if (ddlCat.SelectedValue == "5") // Allotted Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

               //// lblStudent.Visible = true;
               // txtemail.Visible = false;
               // ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");
            }
            else if (ddlCat.SelectedValue == "6") // Admission Batch
            {
                ds = objCommon.FillDropDown("ACD_GENDER WITH (NOLOCK)", "SEX AS COLUMNID", "GENDERNAME AS COLUMNNAME", "SEX is not null", "SEX");
                ddlcat1.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                //    lblStudent.Visible = true;
                txtUSN.Visible = false;
                txtLAdd.Visible = false;

               // txtLAdd.Visible = false;

               //// lblStudent.Visible = true;
               // txtemail.Visible = false;
               // ds = objCommon.FillDropDown("ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO AS COLUMNID", "BATCHNAME AS COLUMNNAME", "BATCHNO > 0", "BATCHNO");
            }
            else if (ddlCat.SelectedValue == "7") // Blood Group
            {

                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                //  lblStudent.Visible = true;
                txtLAdd.Visible = false;

             //   txtLAdd.Visible = false;
             ////   lblStudent.Visible = true;
             //   txtemail.Visible = false;
             //   ds = objCommon.FillDropDown("ACD_BLOODGRP WITH (NOLOCK)", "BLOODGRPNO AS COLUMNID", "BLOODGRPNAME AS COLUMNNAME", "BLOODGRPNO > 0", "BLOODGRPNO");
            }
            else if (ddlCat.SelectedValue == "8") // Admission Date
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.EMAILID AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

                //txtLAdd.Visible = false;
                ////ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                //ddlcat1.Visible = false;
                //txtUSN.Visible = false;
                //txtemail.Visible = false;
              //  lblStudent.Visible = true;
            }
            else if (ddlCat.SelectedValue == "9")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.REGNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;
             //   lblStudent.Visible = true;

            }
            else if (ddlCat.SelectedValue == "10") // Admission Date
            {
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ddlcat1.Visible = false;
                //txtUSN.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
                //S.CSN_NO,
            }

            else if (ddlCat.SelectedValue == "11")
            {

                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtUSN.Visible = false;
                txtLAdd.Visible = true;
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.CSN_NO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                //ddlcat1.Visible = false;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

              //  lblStudent.Visible = true;
            }
            else if (ddlCat.SelectedValue == "12")
            {

                ddlcat1.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                //  lblStudent.Visible = true;
                txtLAdd.Visible = false;
                txtUSN.Visible = false;
                ddlcat1.Items.Clear();
                ddlcat1.Items.Add(new ListItem("Please Select", "0"));
                ddlcat1.Items.Add(new ListItem("Full Time", "1"));
                ddlcat1.Items.Add(new ListItem("Part Time", "2"));
                ddlcat1.SelectedValue = ddlcat1.ToolTip;
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_CASTE C ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //ds = objCommon.FillDropDown("ACD_CASTE WITH (NOLOCK)", "CASTENO AS COLUMNID", "CASTE AS COLUMNNAME", "CASTENO > 0", "CASTENO");
                //ddlcat1.Visible = true;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (ddlCat.SelectedValue == "13")
            {

                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtUSN.Visible = false;
                txtLAdd.Visible = true;

                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //ds = objCommon.FillDropDown("ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO AS COLUMNID", "PAYTYPENAME AS COLUMNNAME", "PAYTYPENO > 0", "PAYTYPENO");
                //ddlcat1.Visible = true;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;
               //blStudent.Visible = true;
            }

            else if (ddlCat.SelectedValue == "14")
            {
                txtLAdd.Visible = false;
                txtUSN.Visible = false;
                txtemail.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                ddlcat1.Visible = true;
                ds = objCommon.FillDropDown("ACD_MEDIUMOFINSTRUCTION_MASTER WITH (NOLOCK)", "MEDIUMID AS COLUMNID", "MEDIUMNAME AS COLUMNNAME", "MEDIUMID > 0", "MEDIUMID");
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //ds = objCommon.FillDropDown("ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO AS COLUMNID", "BATCHNAME AS COLUMNNAME", "", "BATCHNO");
                //ddlcat1.Visible = true;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            //else if (ddlCat.SelectedValue == "15")
            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.STATE_RANK, S.STUDNAME,S.STATE_RANK AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.STATE_RANK");
            //    ddlcat1.Visible = false;
            //    txtAdmissionDate.Visible = false;
            //    imgCal.Visible = false;
            //    txtemail.Visible = false;
            //    txtLAdd.Visible = false;

            //  //  lblStudent.Visible = true;
            //}
            else if (ddlCat.SelectedValue == "16")
            {
                txtLAdd.Visible = false;
                txtUSN.Visible = true;
                txtemail.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                ddlcat1.Visible = false;
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MERITNO AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.QEXMROLLNO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                //ddlcat1.Visible = false;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (ddlCat.SelectedValue == "17")
            {
                txtLAdd.Visible = false;
                txtUSN.Visible = true;
                txtemail.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                ddlcat1.Visible = false;
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.FATHERMOBILE AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

            //{
            //    ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.ORDER_NO, S.STUDNAME,S.ORDER_NO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.ORDER_NO");
            //    ddlcat1.Visible = false;
            //    txtAdmissionDate.Visible = false;
            //    imgCal.Visible = false;
            //    txtemail.Visible = false;
            //    txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (ddlCat.SelectedValue == "18")
            {
                txtLAdd.Visible = false;
                txtUSN.Visible = true;
                txtemail.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                ddlcat1.Visible = false;
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MOTHERMOBILE AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");


                //ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.AIDED AS COLUMNNAME, S.AIDED AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //ds = objCommon.FillDropDown("ACD_PAYMENT_GROUP WITH (NOLOCK)", "GROUP_ID AS COLUMNID", "PAYMENT_GROUP AS COLUMNNAME", "GROUP_ID > 0", "GROUP_ID");
                //ddlcat1.Visible = true;
                //txtAdmissionDate.Visible = false;
                //imgCal.Visible = false;
                //txtemail.Visible = false;
                //txtLAdd.Visible = false;

                //lblStudent.Visible = true;
               //DropDownList ddlcatt = (DropDownList)e.Item.FindControl("ddlcat");
                //ddlcatt.Items.Add(new ListItem("Aided", "1"));
                //ddlcatt.Items.Add(new ListItem("Un-aided", "2"));
            }
            else if (ddlCat.SelectedValue == "19")  // Added By Shrikant W. on 28-11-2023
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ABCC_ID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ddlcat1.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;
            }
            #region Comment
            // else if (ddlCat.SelectedValue == "19") // Adarcard
           // {
           //     //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //     txtLAdd.Visible = false;

           ////     lblStudent.Visible = true;

           // }
           // else if (ddlCat.SelectedValue == "20")
           // {
           //     //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_GENDER SC ON (S.SEX = SC.SEX)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,SC.GENDERNAME AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           //     //ds = objCommon.FillDropDown("ACD_BLOODGRP", "BLOODGRPNO AS COLUMNID", "BLOODGRPNAME AS COLUMNNAME", "BLOODGRPNO > 0", "BLOODGRPNO");
           //     ds = objCommon.FillDropDown("ACD_GENDER WITH (NOLOCK)", "SEX AS COLUMNID", "GENDERNAME AS COLUMNNAME", "SEX is not null", "SEX");
           //     ddlcat1.Visible = true;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           // //    lblStudent.Visible = true;
           //     txtUSN.Visible = false;
           //     txtLAdd.Visible = false;

           // }
           // else if (ddlCat.SelectedValue == "21") // MOBILE
           // {
           //     //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //   //  lblStudent.Visible = true;
           //     txtLAdd.Visible = false;

           // }
           // else if (ddlCat.SelectedValue == "22") // EMAIL
           // {

           //     //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.EMAILID AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //     txtLAdd.Visible = false;

           //   //  lblStudent.Visible = true;
           // }
           // else if (ddlCat.SelectedValue == "23") // Category
           // {
           //     ddlcat1.Visible = true;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //     txtLAdd.Visible = false;

           //    // lblStudent.Visible = true;
           //     ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");
           // }
           // else if (ddlCat.SelectedValue == "25")//Father Name
           // {
           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //     txtLAdd.Visible = false;

           //   //  lblStudent.Visible = false;
           //     txtUSN.Visible = true;
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           // }
           // else if (ddlCat.SelectedValue == "28")
           // {

           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //     txtUSN.Visible = false;
           //     txtLAdd.Visible = true;
           //     //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           //     //txtLAdd.Attributes.Add("onkeypress", "return checkAddress(txt)");
           //     // txtPAdd.Visible = true;
           //     //   lblStudent.Visible = true;
           // }
           // else if (ddlCat.SelectedValue == "26")
           // {
           //     ddlcat1.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtLAdd.Visible = false;

           //     txtemail.Visible = false;
           //    // lblStudent.Visible = false;
           //     txtUSN.Visible = true;
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MOTHERNAME AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "COLUMNID");
           // }
           // else if (ddlCat.SelectedValue == "27") // TC Part Time/Full Time
           // {
           //     ddlcat1.Visible = true;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     txtemail.Visible = false;
           //   //  lblStudent.Visible = true;
           //     txtLAdd.Visible = false;
           //     txtUSN.Visible = false;
           //     ddlcat1.Items.Clear();
           //     ddlcat1.Items.Add(new ListItem("Please Select", "0"));
           //     ddlcat1.Items.Add(new ListItem("Full Time", "1"));
           //     ddlcat1.Items.Add(new ListItem("Part Time", "2"));
           //     ddlcat1.SelectedValue = ddlcat1.ToolTip;
           // }
           // else if (ddlCat.SelectedValue == "29")  //ADDED BY VINAY MISHRA ON 17/08/2023 - ADD FIELD TO UPDATE MEDIUM OF INSTRUCTION FOR STUDENTS
           // {
           //     txtLAdd.Visible = false;
           //     txtUSN.Visible = false;
           //     txtemail.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     ddlcat1.Visible = true;
           //     ds = objCommon.FillDropDown("ACD_MEDIUMOFINSTRUCTION_MASTER WITH (NOLOCK)", "MEDIUMID AS COLUMNID", "MEDIUMNAME AS COLUMNNAME", "MEDIUMID > 0", "MEDIUMID");
           // }
           // else if (ddlCat.SelectedValue == "31")  //ADDED BY VINAY MISHRA ON 14/09/2023 - ADD FIELD TO UPDATE MERIT NUMBER FOR STUDENTS
           // {
           //     txtLAdd.Visible = false;
           //     txtUSN.Visible = true;
           //     txtemail.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     ddlcat1.Visible = false;
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MERITNO AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           // }

           // else if (ddlCat.SelectedValue == "32") 
           // {
           //     txtLAdd.Visible = false;
           //     txtUSN.Visible = true;
           //     txtemail.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     ddlcat1.Visible = false;
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.FATHERMOBILE AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
           // }

           // else if (ddlCat.SelectedValue == "33")
           // {
           //     txtLAdd.Visible = false;
           //     txtUSN.Visible = true;
           //     txtemail.Visible = false;
           //     txtAdmissionDate.Visible = false;
           //     imgCal.Visible = false;
           //     ddlcat1.Visible = false;
           //     ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MOTHERMOBILE AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            // }
            #endregion Comment

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ddlCat.SelectedValue == "2")
                {
                 //   lblStudent.Visible = true;
                    txtemail.Visible = false;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = true;
                    imgCal.Visible = true;
                    txtUSN.Visible = false;
                    txtLAdd.Visible = false;
                }
                //else if (ddlCat.SelectedValue == "9" || ddlCat.SelectedValue == "11" || ddlCat.SelectedValue == "15" || ddlCat.SelectedValue == "16" || ddlCat.SelectedValue == "17") //|| rdbCat.SelectedValue == "19") - Modified By Vinay Mishra to Apply Validation for Aadhar Card Number - Bug Id 166611
                //{
                //    txtemail.Visible = false;
                //    txtUSN.Visible = true;
                //    ddlcat1.Visible = false;
                //    txtAdmissionDate.Visible = false;
                //    imgCal.Visible = false;
                //    txtLAdd.Visible = false;
                // //   lblStudent.Visible = true;
                //}
                else if (ddlCat.SelectedValue == "5")  //Added By Vinay Mishra to Apply Validation for Aadhar Card Number - Bug Id 166611
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.MaxLength = 12;
                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");
                }
                else if (ddlCat.SelectedValue == "7")
                {
                    //TextBox t1=e.Item.FindControl("txtUSN") as TextBox;
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;

                    txtUSN.MaxLength = 10;
                 //   lblStudent.Visible = true;

                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");

                    //txtUSN.TextMode

                }
                else if (ddlCat.SelectedValue == "8")
                {
                    // if(txtUSN.)
                    txtUSN.Visible = false;
                    txtemail.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    txtLAdd.Visible = false;
                    imgCal.Visible = false;
                    //lblStudent.Visible = true;
                    // ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);

                    // txtUSN.Attributes.Add("onblur", "return checkEmail(event)");

                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "return checkEmail()", "return checkEmail()", true);
                }
                else if (ddlCat.SelectedValue == "10")
                {
                    txtLAdd.Visible = false;
                    txtUSN.Visible = true;
                    txtemail.Visible = false;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    //lblStudent.Visible = true;
                }
                else if (ddlCat.SelectedValue == "11")
                {
                    txtLAdd.Visible = false;
                    txtUSN.Visible = true;
                    txtemail.Visible = false;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                 //   lblStudent.Visible = true;
                }
                else if (ddlCat.SelectedValue == "16")  //Added By Vinay Mishra on 14092023 - To Update Merit Number for Students
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.MaxLength = 7;
                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");

                }
                else if (ddlCat.SelectedValue == "17")  
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.MaxLength = 13;
                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");

                }
                else if (ddlCat.SelectedValue == "18")  
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.MaxLength = 13;
                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");
                }

                else if (ddlCat.SelectedValue == "19")    // Added By Shrikant W. on 28-11-2023
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat1.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.MaxLength = 20;
                    txtUSN.Attributes.Add("onkeypress", "return allowAlphaNumericSpace(event, this)");
                }
                else
                {
                    ddlcat1.Visible = true;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.Visible = false;
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        ddlcat1.Items.Add(new ListItem(dtr["COLUMNNAME"].ToString(), dtr["COLUMNID"].ToString()));
                    }
                    ddlcat1.SelectedValue = ddlcat1.ToolTip;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void rdbCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListView();
        //if (rdbCat.SelectedValue == "28")
        //{
        //    lblAddressNote.Visible = true;
        //    objCommon.DisplayMessage(this.updStudent, "Note - Use only Comma(,) , Hyphen(-), Backslash(/) characters during entering the Address/Permanent Address. Other special character's are not acceptable.", this.Page);
        //    //objCommon.DisplayMessage(this.updStudent, "Note - Do Not Use Single Quotation(') Mark/Character During Entering the Address Permanent Address.", this.Page);
        //}
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Degree", this.Page);
            return;
        }
        if (ddlBranch.SelectedIndex > 0)
        {
            if (ddlDegree.SelectedIndex > 0 && ddlAdmBatch.SelectedIndex > 0)
            {
                //string dec = objCommon.LookUp("USER_ACC", "UA_DEC", "UA_NO=" + Session["userno"].ToString());
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEPARTMENT D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEPTNO = B.DEPTNO)", "DISTINCT (B.DEPTNO)", "D.DEPTNAME", "B.COLLEGE_ID=" + ddlClg.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "B.DEPTNO");
                trFilter.Visible = false;
                ddlSemester.SelectedValue = "0";
                rdbCat.ClearSelection();
            }
        }
        else
        {
            trFilter.Visible = false;
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e){}

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        trFilter.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
    }

    protected void lvStudFather_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            pnlStudent.Visible = false;
            PnlStudParentEmail.Visible = false;
            DataSet ds1 = null;
            TextBox txtUSN = e.Item.FindControl("txtusn") as TextBox;
            TextBox txtName = e.Item.FindControl("txtName") as TextBox;
            TextBox txtFirstName = e.Item.FindControl("txtFirstName") as TextBox;
            TextBox txtMiddle = e.Item.FindControl("txtMiddle") as TextBox;
            TextBox txtLastName = e.Item.FindControl("txtLastName") as TextBox;
            if (ddlCat.SelectedValue == "8")
            {
                //ds = objCommon.FillDropDown("ACD_COLLEGECODE", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                ds1 = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME AS COLUMNNAME , S.STUDFIRSTNAME  AS COLUMNFIRSTNAME,S.STUDMIDDLENAME AS COLUMNMIDDLENAME,S.STUDLASTNAME AS COLUMNLASTNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "COLUMNID");
                //txtemail.Visible = false;
            }
            //else if (rdbCat.SelectedValue == "25")
            //{
            //    ds1 = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.FATHERNAME AS COLUMNNAME , S.FATHERFIRSTNAME  AS COLUMNFIRSTNAME,S.FATHERMIDDLENAME AS COLUMNMIDDLENAME,S.FATHERLASTNAME AS COLUMNLASTNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "COLUMNID");
            //}
            //if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            //{
            //    if (rdbCat.SelectedValue == "24")// || rdbCat.SelectedValue == "10")
            //    {
            //        //txtName.Text = ds1.Tables[0].Rows[0]["COLUMNNAME"].ToString();
            //        //txtFirstName.Text = ds1.Tables[0].Rows[0]["COLUMNFIRSTNAME"].ToString();
            //        //txtMiddle.Text = ds1.Tables[0].Rows[0]["COLUMNMIDDLENAME"].ToString();
            //        //txtLastName.Text = ds1.Tables[0].Rows[0]["COLUMNLASTNAME"].ToString();
            //    }
            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString() != "1")
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO>0  AND B.DEPTNO =" + Convert.ToInt32(ddlDepartment.SelectedValue), "D.DEGREENO");
    }

    protected void lvStudParentEmail_ItemDataBound(object sender, ListViewItemEventArgs e)  //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
    {
        try
        {
            PnlStudParentEmail.Visible = true;
            pnlStudent.Visible = false;
            //pnlStudFather.Visible = false;
            DataSet ds1 = null;
            //TextBox txtUSN = e.Item.FindControl("txtusn") as TextBox;
            //TextBox txtName = e.Item.FindControl("txtName") as TextBox;
            //TextBox txtLastName = e.Item.FindControl("txtLastName") as TextBox;
            TextBox txtFatherEmail = e.Item.FindControl("txtFatherEmail") as TextBox;
            TextBox txtMotherEmail = e.Item.FindControl("txtMotherEmail") as TextBox;
            
            if (rdbCat.SelectedValue == "15")    //ADDED BY VINAY MISHRA ON 28/08/2023 - ADD FIELD TO UPDATE PARENTS EMAIL ID FOR STUDENTS
            {
                //ds = objCommon.FillDropDown("ACD_COLLEGECODE", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                ds1 = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.FATHER_EMAIL AS COLUMNNAMEFATHEREMAIL,S.MOTHER_EMAIL AS COLUMNNAMEMOTHEREMAIL", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "COLUMNID");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnClear.Visible = true;
        btnSubmit.Visible = true;
        //btnExport.Visible = true;
        //if (ddlCat.SelectedValue == "0")
        //{
        //    btnExport.Visible = false;
        //}
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudParentEmail.DataSource = null;
        lvStudParentEmail.DataBind();
        lvStudFather.DataSource = null;
        lvStudFather.DataBind();
        if (ddlAdmBatch.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Admission Batch", this.Page);
            return;
        }
        if (ddlDegree.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Degree", this.Page);
            return;
        }
        if (ddlBranch.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Branch", this.Page);
            return;
        }
        if (ddlSemester.SelectedValue == "0")
        {
            objCommon.DisplayMessage(this.updStudent, "Please Select Semester", this.Page);
            return;
        }
        this.BindListView();

        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
    }

    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    try
    //    {   DataSet ds;
    //        if (ddlfilter1.SelectedValue == "1")  // for College Code
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.BLOODGRPNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_COLLEGECODE SC WITH (NOLOCK) ON (S.COLLEGECODE = SC.CODENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.COLLEGECODE AS COLUMNID,SC.CODENAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "2") // for Student Type
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_IDTYPE SC WITH (NOLOCK) ON (S.IDTYPE = SC.IDTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDTYPE AS COLUMNID ,SC.IDTYPEDESCRIPTION,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "3") // KEA status
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.STUDNAME,S.ADMDATE AS COLUMNNAME ,'' AS PCOLUMNNAME,S.CATEGORYNO AS COLUMNID,SC.CATEGORY", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_KEA_STATUS SC WITH (NOLOCK) ON (S.KEA_STATUS = SC.KEANO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.KEA_STATUS AS COLUMNID,SC.KEA_NAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "4") // Claim Category
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT JOIN ACD_CASTE C WITH (NOLOCK) ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.CATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "5") // Allotted Category
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.ADMCATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMCATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "6") // Admission Batch
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_GENDER SC WITH (NOLOCK) ON (S.SEX = SC.SEX)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SEX AS COLUMNID,SC.GENDERNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH SC WITH (NOLOCK) ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "7") // Blood Group
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //S.CSN_NO,
    //        }
    //        else if (ddlfilter1.SelectedValue == "8") // Admission Date
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.EMAILID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO AS COLUMNID", "REGNO, CSN_NO, STUDNAME, ADMDATE", "DEGREENO =" + ddlDegree.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "CSN_NO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "9")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME AS COLUMNNAME , S.STUDFIRSTNAME  AS COLUMNFIRSTNAME,S.STUDMIDDLENAME AS COLUMNMIDDLENAME,S.STUDLASTNAME AS COLUMNLASTNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "IDNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.REGNO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "10")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");

    //            //S.CSN_NO,
    //        }
    //        else if (ddlfilter1.SelectedValue == "11")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MOTHERNAME AS COLUMNNAME ,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.CSN_NO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "12")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SHIFT AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_CASTE C WITH (NOLOCK) ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_ADMBATCH SC ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
    //            //S.CSN_NO,
    //        }
    //        else if (ddlfilter1.SelectedValue == "13")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENTTYPE P WITH (NOLOCK) ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //            //S.CSN_NO,
    //        }
    //        else if (ddlfilter1.SelectedValue == "14")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_MEDIUMOFINSTRUCTION_MASTER MIM WITH (NOLOCK) ON (MIM.MEDIUMID = S.MEDIUM_INSTRUCT_NO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.MEDIUM_INSTRUCT_NO AS COLUMNID,MIM.MEDIUMNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH A WITH (NOLOCK) ON (S.ACAD_YR = A.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,A.BATCHNAME AS COLUMNNAME, S.ACAD_YR AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

    //        }
    //        else if (ddlfilter1.SelectedValue == "15")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHER_EMAIL AS COLUMNNAMEFATHEREMAIL,S.MOTHER_EMAIL AS COLUMNNAMEMOTHEREMAIL, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STATE_RANK, S.STUDNAME,S.STATE_RANK AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "16")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MERITNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.QEXMROLLNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "17")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO ", "S.REGNO,S.STUDNAME,S.FATHERMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID, '' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.ORDER_NO, S.STUDNAME,S.ORDER_NO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "18")
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME,S.MOTHERMOBILE AS COLUMNNAME , S.IDNO AS COLUMNID , '' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //            //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENT_GROUP P WITH (NOLOCK) ON(P.GROUP_ID=S.AIDED) ", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYMENT_GROUP AS COLUMNNAME, S.AIDED AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
    //        }
    //        else if (ddlfilter1.SelectedValue == "19")  // Added by Shrikant W. on 28-12-2023 for ABCC ID
    //        {
    //            ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ABCC_ID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddldegree1.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester1.SelectedValue + " AND  ADMBATCH=" + ddlAdm2.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlbranch1.SelectedValue, "S.REGNO");
    //        }

    //        else
    //        {
    //            ds = null;
    //        }

    //        StudentController objSC = new StudentController();
    //        //DataSet ds = objCC.RetrieveBulkUpdateDataForExcel(Convert.ToInt32(ddlAdmBatch.SelectedValue),Convert.ToInt32(ddlDegreeSelectedValue),Convert.ToInt32(ddlBranchSelectedValue),Convert.ToInt32(ddlSemesterSelectedValue),);
    //        if (ddlfilter1.SelectedValue != "9" && ddlfilter1.SelectedValue != "15")
    //        {
               
    //            if (ddlfilter1.SelectedValue == "1" || ddlfilter1.SelectedValue == "3" || ddlfilter1.SelectedValue == "4" || ddlfilter1.SelectedValue == "6" || ddlfilter1.SelectedValue == "14")
    //            {
    //                DataSet ds1 = null;
    //                ds1 = objSC.AddMasters(Convert.ToInt32(ddlfilter1.SelectedValue));
    //                if (ds1 != null && ds1.Tables.Count > 0)
    //                {
    //                    DataTable dt = ds1.Tables[0];
    //                    ds1.Tables.Remove(dt);
    //                    dt.TableName = "Master";
    //                    ds.Tables.Add(dt);
    //                    ds.Tables[1].TableName = "Master";
    //                }
    //            }
    //        }
    //        //else if (ddlCat.SelectedValue == "9")
    //        //{
    //        //    ds = ViewState["DataSet2"] as DataSet;
    //        //}
    //        //else if (ddlCat.SelectedValue == "15")
    //        //{
    //        //    ds = ViewState["DataSet1"] as DataSet;
    //        //}
    //        //else
    //        //{
    //        //    ds = null;
    //        //}
    //        ds.Tables[0].TableName = "Data";
    //        //ds.Tables[2].TableName = "Semester Master";
    //        //ds.Tables[3].TableName = "Scheme Master";
    //        //ds.Tables[4].TableName = "BOS_Department Master";
    //        //ds.Tables[5].TableName = "Elective Group";

    //        //if (ds.Tables[0].Rows.Count > 0)//&& ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0 && ds.Tables[4].Rows.Count > 0 && ds.Tables[5].Rows.Count > 0)

    //        //{         
    //        using (XLWorkbook wb = new XLWorkbook())
    //        {
    //            foreach (System.Data.DataTable dt in ds.Tables)
    //            {
    //                //Add System.Data.DataTable as Worksheet.
    //                wb.Worksheets.Add(dt);
    //            }

    //            //Export the Excel file.
    //            Response.Clear();
    //            Response.Buffer = true;
    //            Response.Charset = "";
    //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    //            Response.AddHeader("content-disposition", "attachment;filename=Student_" + ddlfilter1.SelectedItem.Text + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
    //            using (MemoryStream MyMemoryStream = new MemoryStream())
    //            {
    //                wb.SaveAs(MyMemoryStream);
    //                MyMemoryStream.WriteTo(Response.OutputStream);
    //                Response.Flush();
    //                Response.End();
    //            }
    //        }
    //    }
    //    catch (ArgumentOutOfRangeException ex)
    //    {
    //        objCommon.DisplayMessage(updpnlImportData, "Something whent wrong", this.Page);
    //        return;
    //    }
    //  //  }
    //}

    //protected void btnUploadexcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Uploaddata();
    //    }
    //    catch (Exception ex)
    //    {
          
    //    }
    //}

    //private void Uploaddata()
    //{
    //    try
    //    {
    //        if (FUFile.HasFile)
    //        {
    //           string FileName = Path.GetFileName(FUFile.PostedFile.FileName);
    //            string Extension = Path.GetExtension(FUFile.PostedFile.FileName);
    //            if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
    //            {
    //                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
    //                string FilePath = Server.MapPath(FolderPath + FileName);
    //                FUFile.SaveAs(FilePath);
    //                ExcelToDatabase(FilePath, Extension, "yes");
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage(updpnlImportData, "Only .xls or .xlsx extention is allowed", this.Page);
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage(updpnlImportData, "Please select the Excel File to Upload", this.Page);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
            
    //    }
    //}

    //private void ExcelToDatabase(string FilePath, string Extension, string isHDR)
    //{
    //    string studids= string.Empty;
    //    string categaries = string.Empty;
    //    int res =0;

    //    int drawing = 0;
    //    CourseController objCC = new CourseController();
    //    Course objC = new Course();
    //    try
    //    {
    //        CustomStatus cs = new CustomStatus();
    //        string conStr = "";

    //        switch (Extension)
    //        {
    //            //case ".xls": //Excel 97-03
    //            //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //            //    break;
    //            //case ".xlsx": //Excel 07
    //            //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
    //            //    break;
    //            case ".xls": //Excel 97-03
    //                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
    //                break;
    //            case ".xlsx": //Excel 07
    //                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";

    //                break;
    //        }
    //        conStr = String.Format(conStr, FilePath, isHDR);

    //        OleDbConnection connExcel = new OleDbConnection(conStr);
    //        OleDbCommand cmdExcel = new OleDbCommand();
    //        OleDbDataAdapter oda = new OleDbDataAdapter();
    //        DataTable dt = new DataTable();
    //        cmdExcel.Connection = connExcel;
    //        //Get the name of First Sheet

    //        connExcel.Open();
    //        DataTable dtExcelSchema;
    //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //        connExcel.Close();

    //        //Read Data from First Sheet
    //        connExcel.Open();
    //        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
    //        oda.SelectCommand = cmdExcel;
    //        oda.Fill(dt);

    //        //Bind Excel to GridView
    //        DataSet ds = new DataSet();
    //        oda.Fill(ds);
    //        DataView dv1 = dt.DefaultView;
    //       // dv1.RowFilter = "isnull(,'')<>''";
    //        DataTable dtNew = dv1.ToTable();

    //        //lvStudData.DataSource = dtNew; // ds.Tables[0]; /// dSet.Tables[0].DefaultView.RowFilter = "Frequency like '%30%')"; ;
    //        //lvStudData.DataBind();
    //        int i = 0;

    //        int Ua_no = Convert.ToInt32(Session["userno"]);
    //        #region Filter1
    //        if (ddlfilter1.SelectedValue != "9" && ddlfilter1.SelectedValue != "15")
    //        {
    //        StudentController objSC = new StudentController();
    //        for (i = 0; i < dtNew.Rows.Count; i++)
    //        //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        //foreach (DataRow dr in dt.Rows)
    //        {
    //            Student ObjStud = new Student();
    //            DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
    //                if (!(dtNew.Rows[i]["IDNO"]).ToString().Equals(string.Empty))
    //                {
    //                    studids += Convert.ToInt32((dtNew.Rows[i]["IDNO"]).ToString()) + "$";
    //                    // ObjStud.IdNo =Convert.ToInt32 (dtNew.Rows[i]["IDNO"]).ToString());
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(updpnlImportData, "Please enter IDNO at Row no. " + (i + 1), this.Page);
    //                    return;
    //                }

    //                if (!(dtNew.Rows[i]["REGNO"]).ToString().Equals(string.Empty))
    //                {
    //                    //ObjStud.RegNo = (dtNew.Rows[i]["REGNO"]).ToString();
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(updpnlImportData, "Please enter REGNO at Row no. " + (i + 1), this.Page);
    //                    return;
    //                }

    //                if (!(dtNew.Rows[i]["STUDNAME"]).ToString().Equals(string.Empty))
    //                {
    //                    //ObjStud.StudName = (dtNew.Rows[i]["STUDNAME"]).ToString();
    //                }
    //                else
    //                {
    //                    objCommon.DisplayMessage(updpnlImportData, "Please enter STUDENT NAME at Row no. " + (i + 1), this.Page);
    //                    return;
    //                }

                  
    //                    if (Convert.ToInt32(ddlfilter1.SelectedValue) == 2 || Convert.ToInt32(ddlfilter1.SelectedValue) == 6 || Convert.ToInt32(ddlfilter1.SelectedValue) == 4)
    //                    {
    //                        categaries += dtNew.Rows[i]["COLUMNID"].ToString() + "$";
    //                    }
    //                    else if (Convert.ToInt32(ddlfilter1.SelectedValue) == 3 || Convert.ToInt32(ddlfilter1.SelectedValue) == 4 || Convert.ToInt32(ddlfilter1.SelectedValue) == 1 || Convert.ToInt32(ddlfilter1.SelectedValue) == 12 || Convert.ToInt32(ddlfilter1.SelectedValue) == 14)
    //                    {
    //                        categaries += Convert.ToInt32((dtNew.Rows[i]["COLUMNID"]).ToString()) + "$";
    //                    }
    //                    else if (Convert.ToInt32(ddlfilter1.SelectedValue) == 7 || Convert.ToInt32(ddlfilter1.SelectedValue) == 8 || Convert.ToInt32(ddlfilter1.SelectedValue) == 10 || Convert.ToInt32(ddlfilter1.SelectedValue) == 11 || Convert.ToInt32(ddlfilter1.SelectedValue) == 13 || Convert.ToInt32(ddlfilter1.SelectedValue) == 16 || Convert.ToInt32(ddlfilter1.SelectedValue) == 17 || Convert.ToInt32(ddlfilter1.SelectedValue) == 18)
    //                    {
    //                        categaries += Convert.ToInt32((dtNew.Rows[i]["COLUMNNAME"]).ToString()) + "$";
    //                    }
    //                    //ObjStud.BloodGroupNo = Convert.ToInt32((dtNew.Rows[i]["COLUMNID"]).ToString());
                   
    //            }
    //            string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
    //            int fieldID =  Convert.ToInt32(ddlfilter1.SelectedValue);
    //            res = objSC.UpdateStudentCategory(studids, categaries, fieldID, IpAddress);
                
    //                //objC.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
    //               // cs = (CustomStatus)objSC.UpdateExcelSheet(ObjStud);
    //                connExcel.Close();
                
    //        }
    //         #endregion Filter1 

    //          #region filter2

    //         else if (ddlfilter1.SelectedValue == "9")
    //         {
    //             StudentController objSC = new StudentController();
                
    //             string categorys = string.Empty;
    //             string rollnos = string.Empty;
    //             string FullName = string.Empty;
    //             string FirstName = string.Empty;
    //             string MiddleName = string.Empty;
    //             string LastName = string.Empty;
    //             string paddress = string.Empty;
                

                
    //             string Ip_Address = string.Empty;

    //            // StudentController objSC = new StudentController();
    //             for (i = 0; i < dtNew.Rows.Count; i++)
    //             //for (i = 0; i < ds.Tables[0].Rows.Count; i++)
    //             //foreach (DataRow dr in dt.Rows)
    //             {
    //                 Student ObjStud = new Student();
    //                 DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
    //                 object Name = row[0];
    //                 if (Name != null && !String.IsNullOrEmpty(Name.ToString().Trim()))
    //                 {
    //                     if (!(dtNew.Rows[i]["IDNO"]).ToString().Equals(string.Empty))
    //                     {
    //                         studids += Convert.ToInt32((dtNew.Rows[i]["IDNO"]).ToString()) + "$";
    //                         // ObjStud.IdNo =Convert.ToInt32 (dtNew.Rows[i]["IDNO"]).ToString());
    //                     }
    //                     else
    //                     {
    //                         objCommon.DisplayMessage(updpnlImportData, "Please enter IDNO at Row no. " + (i + 1), this.Page);
    //                         return;
    //                     }

    //                     if (!(dtNew.Rows[i]["REGNO"]).ToString().Equals(string.Empty))
    //                     {
    //                         //ObjStud.RegNo = (dtNew.Rows[i]["REGNO"]).ToString();
    //                     }
    //                     else
    //                     {
    //                         objCommon.DisplayMessage(updpnlImportData, "Please enter REGNO at Row no. " + (i + 1), this.Page);
    //                         return;
    //                     }


    //                     FullName += (dtNew.Rows[i]["COLUMNNAME"]).ToString() + "$";
    //                     FirstName += (dtNew.Rows[i]["COLUMNFIRSTNAME"]).ToString() + "$";
    //                     MiddleName += (dtNew.Rows[i]["COLUMNMIDDLENAME"]).ToString() + "$";
    //                     LastName += (dtNew.Rows[i]["COLUMNLASTNAME"]).ToString() + "$";

                        
    //                     //ObjStud.BloodGroupNo = Convert.ToInt32((dtNew.Rows[i]["COLUMNID"]).ToString());

    //                 }
    //                 string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
    //                 int fieldID = Convert.ToInt32(ddlfilter1.SelectedValue);
    //                 res =  objSC.UpdateStudentAndFatherInfo(studids, fieldID, FullName, FirstName, MiddleName, LastName, IpAddress,ddlfilter1.SelectedItem.Text, Convert.ToInt32(Ua_no));

    //                 //objC.BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
    //                 // cs = (CustomStatus)objSC.UpdateExcelSheet(ObjStud);
    //                 connExcel.Close();
    //             }


    //         }
    //        #endregion filter2


    //         else if (ddlfilter1.SelectedValue == "15")
    //         {
    //             StudentController objSC = new StudentController();
    //             int fieldID = Convert.ToInt32(ddlfilter1.SelectedValue);
    //              string femail = string.Empty;   //ADDED BY VINAY MISHRA ON 28082023 FOR 47935
    //             string memail = string.Empty;   //ADDED BY VINAY MISHRA ON 28082023 FOR 47935
    //             if (!(dtNew.Rows[i]["IDNO"]).ToString().Equals(string.Empty))
    //             {
    //                 studids += Convert.ToInt32((dtNew.Rows[i]["IDNO"]).ToString()) + "$";
    //                 // ObjStud.IdNo =Convert.ToInt32 (dtNew.Rows[i]["IDNO"]).ToString());
    //             }
    //             else
    //             {
    //                 objCommon.DisplayMessage(updpnlImportData, "Please enter IDNO at Row no. " + (i + 1), this.Page);
    //                 return;
    //             }
    //             string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
    //             femail += (dtNew.Rows[i]["COLUMNNAMEFATHEREMAIL"]).ToString() + "$";
    //             memail += (dtNew.Rows[i]["COLUMNNAMEMOTHEREMAIL"]).ToString() + "$";
    //             res = objSC.UpdateFatherAndMotherEmail(studids, fieldID, femail, memail, IpAddress, ddlfilter1.SelectedItem.Text, Convert.ToInt32(Ua_no));
    //         }
    //        if (res >0)
    //        {
    //            // BindListView();
    //            objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Updated Successfully!!", this.Page);
               
    //            ddlAdm2.SelectedValue = "0";
    //            ddldegree1.SelectedValue = "0";
    //            ddlbranch1.SelectedValue = "0";
    //            ddlSemester1.SelectedValue = "0";
    //            ddlfilter1.SelectedValue = "0";
    //            return;
    //        }
    //        else
    //        {
    //            //BindListView();
    //            objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Uploaded Successfully!!", this.Page);
    //            return;
    //        }
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);


    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //        {
    //            objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

    //            return;
    //        }
           
              
    //    }
    //}

    protected void ddldegree1_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlbranch1, "ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddldegree1.SelectedValue) + " AND CD.BRANCHNO > 0 ", "B.LONGNAME");
    }

    #region Old Code
    //protected void ddlfilter1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    btnUploadexcel.Visible = true;
    //    btnExport.Visible = true;
    //    if (ddlfilter1.SelectedValue == "0")
    //    {
    //        btnExport.Visible = false;
    //    }

    //    if (ddlAdm2.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updStudent, "Please Select Admission Batch", this.Page);
    //        return;
    //    }
    //    if (ddldegree1.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updStudent, "Please Select Degree", this.Page);
    //        return;
    //    }
    //    if (ddlbranch1.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updStudent, "Please Select Branch", this.Page);
    //        return;
    //    }
    //    if (ddlSemester1.SelectedValue == "0")
    //    {
    //        objCommon.DisplayMessage(this.updStudent, "Please Select Semester", this.Page);
    //        return;
    //    }
    //}
    #endregion

    #region Bulk Student Field Updation (Excel) Added by Gunesh Mohane
    //Added by Gunesh Mohane on 04/03/2024
    protected void btnExport2_Click(object sender, EventArgs e)
    {
        try
        {
            List<int> selectedItems = new List<int>();

            foreach (int i in ddlfilter1.GetSelectedIndices())
            {
                selectedItems.Add(Convert.ToInt32(ddlfilter1.Items[i].Value));
            }


            StudentController objSC = new StudentController();
            DataSet ds = objSC.RetrieveStudentMasterDataForExcel(Convert.ToInt32(ddlAdm2.SelectedValue), Convert.ToInt32(ddldegree1.SelectedValue),
                Convert.ToInt32(ddlbranch1.SelectedValue), Convert.ToInt32(ddlSemester1.SelectedValue), selectedItems);


            if (ds.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in ds.Tables)
                    {
                        //Add System.Data.DataTable as Worksheet.
                        wb.Worksheets.Add(dt);
                    }

                    //ClearControls();
                    //Export the Excel file.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=PreFormat_For_UploadStudentData_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                        
                    }
                }
                
            }
            else
            {
                objCommon.DisplayMessage(this.updStudent, "No Students Found for Selected Criteria.", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
            }
        }
        catch 
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

                return;
            }
        }
    }

    //Added by Gunesh Mohane on 04/03/2024
    protected void btnUploadexcel2_Click(object sender, EventArgs e)
    {
        try
        {
            Uploaddata2();
        }
        catch (Exception ex)
        {

        }
    }

    //Added by Gunesh Mohane on 04/03/2024
    private void Uploaddata2()
    {
        try
        {
            if (FUFile.HasFile)
            {
                string FileName = Path.GetFileName(FUFile.PostedFile.FileName);
                string Extension = Path.GetExtension(FUFile.PostedFile.FileName);
                if (Extension.Equals(".xls") || Extension.Equals(".xlsx"))
                {
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FUFile.SaveAs(FilePath);
                    ExcelToDatabase2(FilePath, Extension, "yes");
                }
                else
                {
                    objCommon.DisplayMessage(updpnlImportData, "Only .xls or .xlsx extention is allowed", this.Page);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(updpnlImportData, "Please select the Excel File to Upload", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                return;
            }
        }
        catch (Exception ex)
        {

        }
    }

    //Added by Gunesh Mohane on 04/03/2024
    private void ExcelToDatabase2(string FilePath, string Extension, string isHDR)
    {
        string studids = string.Empty;
        try
        {
            CustomStatus cs = new CustomStatus();
            string conStr = "";
            #region Excel Binding
            switch (Extension)
            {
                //case ".xls": //Excel 97-03
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                //case ".xlsx": //Excel 07
                //    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                //    break;
                case ".xls": //Excel 97-03
                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";
                    break;
                case ".xlsx": //Excel 07
                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0'";

                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet

            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = null;
            foreach (DataRow row in dtExcelSchema.Rows)
            {
                if (row["TABLE_NAME"].ToString() == "Data$")
                {
                    SheetName = row["TABLE_NAME"].ToString();
                    break;
                }
            }
            connExcel.Close();

            //Read Data from First Sheet
           connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);


            //Bind Excel to GridView
            DataSet ds = new DataSet();
            oda.Fill(ds);
            DataView dv1 = dt.DefaultView;
            DataTable dtNew = dv1.ToTable();
            #endregion
            int Ua_no = Convert.ToInt32(Session["userno"]);
            string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
            for (int i = 0; i < dtNew.Rows.Count; i++)
            {
                Student objStu = new Student();
                DataRow row = dtNew.Rows[i];//ds.Tables[0].Rows[i];
                StudentController objSC = new StudentController();

                #region Parameters
                if (!(dtNew.Rows[i]["IDNO"]).ToString().Equals(string.Empty))
                {
                    objStu.IdNo += Convert.ToInt32((dtNew.Rows[i]["IDNO"]).ToString());
                    // ObjStud.IdNo =Convert.ToInt32 (dtNew.Rows[i]["IDNO"]).ToString());
                }
                else
                {
                    objCommon.DisplayMessage(updpnlImportData, "Please enter IDNO at Row no. " + (i + 1), this.Page);
                    return;
                }

                string idno = objStu.IdNo.ToString();
                objStu.StudName = (dtNew.Columns.Contains("STUDNAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["STUDNAME"].ToString())) ? dtNew.Rows[i]["STUDNAME"].ToString() : null;
                objStu.firstName = (dtNew.Columns.Contains("STUDFIRSTNAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["STUDFIRSTNAME"].ToString())) ? dtNew.Rows[i]["STUDFIRSTNAME"].ToString() : null;
                objStu.MiddleName = (dtNew.Columns.Contains("STUDMIDDLENAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["STUDMIDDLENAME"].ToString())) ? dtNew.Rows[i]["STUDMIDDLENAME"].ToString() : null;
                objStu.LastName = (dtNew.Columns.Contains("STUDLASTNAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["STUDLASTNAME"].ToString())) ? dtNew.Rows[i]["STUDLASTNAME"].ToString() : null;
                objStu.FatherName = (dtNew.Columns.Contains("FATHERNAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["FATHERNAME"].ToString())) ? dtNew.Rows[i]["FATHERNAME"].ToString() : null;
                objStu.MotherName = (dtNew.Columns.Contains("MOTHERNAME") && !string.IsNullOrEmpty(dtNew.Rows[i]["MOTHERNAME"].ToString())) ? dtNew.Rows[i]["MOTHERNAME"].ToString() : null;
                objStu.StudentMobile = (dtNew.Columns.Contains("STUDENTMOBILE") && !string.IsNullOrEmpty(dtNew.Rows[i]["STUDENTMOBILE"].ToString())) ? dtNew.Rows[i]["STUDENTMOBILE"].ToString() : null;
                objStu.Sex = (dtNew.Columns.Contains("SEX") && !string.IsNullOrEmpty(dtNew.Rows[i]["SEX"].ToString())) ? Convert.ToChar(dtNew.Rows[i]["SEX"].ToString()) : (char)0;
                objStu.Dob = (dtNew.Columns.Contains("DOB") && dtNew.Rows[i]["DOB"] != DBNull.Value) ? Convert.ToDateTime(dtNew.Rows[i]["DOB"]) : default(DateTime);
                objStu.AadharCardNo = (dtNew.Columns.Contains("ADDHARCARDNO") && !string.IsNullOrEmpty(dtNew.Rows[i]["ADDHARCARDNO"].ToString())) ? dtNew.Rows[i]["ADDHARCARDNO"].ToString() : null;
                objStu.LAddress = (dtNew.Columns.Contains("LOCALADDRESS") && !string.IsNullOrEmpty(dtNew.Rows[i]["LOCALADDRESS"].ToString())) ? dtNew.Rows[i]["LOCALADDRESS"].ToString() : null;
                objStu.PAddress = (dtNew.Columns.Contains("PERMANENTADDRESS") && !string.IsNullOrEmpty(dtNew.Rows[i]["PERMANENTADDRESS"].ToString())) ? dtNew.Rows[i]["PERMANENTADDRESS"].ToString() : null;
                objStu.EmailID = (dtNew.Columns.Contains("STUDEMAIL")) ? dtNew.Rows[i]["STUDEMAIL"].ToString() : null;
                objStu.Fatheremail = (dtNew.Columns.Contains("FATHER_EMAIL") && !string.IsNullOrEmpty(dtNew.Rows[i]["FATHER_EMAIL"].ToString())) ? dtNew.Rows[i]["FATHER_EMAIL"].ToString() : null;
                objStu.Motheremail = (dtNew.Columns.Contains("MOTHER_EMAIL") && !string.IsNullOrEmpty(dtNew.Rows[i]["MOTHER_EMAIL"].ToString())) ? dtNew.Rows[i]["MOTHER_EMAIL"].ToString() : null;
                int shiftValue = 0;
                if (dtNew.Columns.Contains("SHIFT") && dtNew.Rows[i]["SHIFT"] != DBNull.Value)
                {
                    string shiftString = dtNew.Rows[i]["SHIFT"].ToString();
                    if (shiftString == "Full Time")
                    {
                        shiftValue = 1;
                    }
                    else if (shiftString == "Part Time")
                    {
                        shiftValue = 2;
                    }
                    else 
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Shift at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                }
                objStu.Shift = shiftValue;
                objStu.AbccId = (dtNew.Columns.Contains("ABCC_ID") && !string.IsNullOrEmpty(dtNew.Rows[i]["ABCC_ID"].ToString())) ? dtNew.Rows[i]["ABCC_ID"].ToString() : null;
                objStu.MeritNo = (dtNew.Columns.Contains("MERITNO") && !string.IsNullOrEmpty(dtNew.Rows[i]["MERITNO"].ToString())) ? dtNew.Rows[i]["MERITNO"].ToString() : null;
                objStu.FatherMobile = (dtNew.Columns.Contains("FATHERMOBILE") && !string.IsNullOrEmpty(dtNew.Rows[i]["FATHERMOBILE"].ToString())) ? dtNew.Rows[i]["FATHERMOBILE"].ToString() : null;
                objStu.MotherMobile = (dtNew.Columns.Contains("MOTHERMOBILE") && !string.IsNullOrEmpty(dtNew.Rows[i]["MOTHERMOBILE"].ToString())) ? dtNew.Rows[i]["MOTHERMOBILE"].ToString() : null;

                string aadharno = objStu.AadharCardNo;
                string mobileno = objStu.StudentMobile;
                string emailid = objStu.EmailID;
                string bloodgrpname = (dtNew.Columns.Contains("BLOODGRPNAME") && dtNew.Rows[i]["BLOODGRPNAME"] != DBNull.Value) ? dtNew.Rows[i]["BLOODGRPNAME"].ToString() : null;
                string catname = (dtNew.Columns.Contains("CATEGORY") && dtNew.Rows[i]["CATEGORY"] != DBNull.Value) ? dtNew.Rows[i]["CATEGORY"].ToString() : null;
                string castename = (dtNew.Columns.Contains("CASTE") && dtNew.Rows[i]["CASTE"] != DBNull.Value) ? dtNew.Rows[i]["CASTE"].ToString() : null;
                string mediumname = (dtNew.Columns.Contains("MEDIUMNAME") && dtNew.Rows[i]["MEDIUMNAME"] != DBNull.Value) ? dtNew.Rows[i]["MEDIUMNAME"].ToString() : null;

                DataTableReader dtr = objSC.GetMasterID(idno,bloodgrpname, catname, castename, mediumname,aadharno,mobileno,emailid);
                if (dtr.HasRows)
                {
                    if (dtr.Read())
                    {
                        objStu.BloodGroupNo = (dtr["BLOODGRPNO"] != DBNull.Value) ? Convert.ToInt32(dtr["BLOODGRPNO"].ToString()) : 0;
                        objStu.CategoryNo = (dtr["CATEGORYNO"] != DBNull.Value) ? Convert.ToInt32(dtr["CATEGORYNO"].ToString()) : 0;
                        objStu.Caste = (dtr["CASTENO"] != DBNull.Value) ? Convert.ToInt32(dtr["CASTENO"].ToString()) : 0;
                        objStu.MediumID = (dtr["MEDIUMID"] != DBNull.Value) ? dtr["MEDIUMID"].ToString() : null;

                        if(!string.IsNullOrEmpty(aadharno))
                            if (dtr["ISAADHARALREADYPRESENT"] != DBNull.Value)
                                if (dtr["AADHARCHARNO"] == DBNull.Value)
                                {
                                    objCommon.DisplayMessage(updpnlImportData, "Duplicate Aadhar Card Number is Present at IDNO. " + idno, this.Page);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                                    return;
                                }

                        if (!string.IsNullOrEmpty(mobileno))
                            if (dtr["ISMOBILEALREADYPRESENT"] != DBNull.Value)
                                if (dtr["MOBILENO"] == DBNull.Value)
                                {
                                    objCommon.DisplayMessage(updpnlImportData, "Duplicate Mobile Number is Present at IDNO. " + idno, this.Page);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                                    return;
                                }

                        if (!string.IsNullOrEmpty(emailid))
                            if (dtr["ISEMAILALREADYPRESENT"] != DBNull.Value)
                                if (dtr["EMAILID"] == DBNull.Value)
                                {
                                    objCommon.DisplayMessage(updpnlImportData, "Duplicate Email is Present at IDNO. " + idno, this.Page);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                                    return;
                                }

                        if(bloodgrpname != null && objStu.BloodGroupNo == 0 && bloodgrpname != "-")
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Blood Group not found in ERP Master at IDNO. " + idno, this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                        else if (catname != null && objStu.CategoryNo == 0 && catname != "-")
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Category not found in ERP Master at IDNO. " + idno, this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                        else if (castename != null && objStu.Caste == 0 && castename != "-")
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Caste not found in ERP Master at IDNO. " + idno, this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                        else if (mediumname != null && string.IsNullOrEmpty(objStu.MediumID) && mediumname != "-")
                        {
                            objCommon.DisplayMessage(updpnlImportData, "Medium of Instruction not found in ERP Master at IDNO. " + idno, this.Page);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                            return;
                        }
                    }                
                }
                
                dtr.Close();

                if (!string.IsNullOrEmpty(objStu.AadharCardNo))
                    if (objStu.AadharCardNo.Length != 12 || !objStu.AadharCardNo.ToString().All(char.IsDigit))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Aadhar Number at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }

                if (!string.IsNullOrEmpty(objStu.StudentMobile))
                    if (objStu.StudentMobile.Length != 10 || !objStu.StudentMobile.ToString().All(char.IsDigit))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Student Mobile Number at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }

                
                if (!string.IsNullOrEmpty(objStu.FatherMobile))
                    if (objStu.FatherMobile.Length != 10 || !objStu.FatherMobile.ToString().All(char.IsDigit))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Father Mobile Number at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }

                
                if (!string.IsNullOrEmpty(objStu.EmailID))
                    if (!IsValidEmail(objStu.EmailID))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Email Address at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }

                if (!string.IsNullOrEmpty(objStu.Fatheremail))
                    if (!IsValidEmail(objStu.Fatheremail))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Father Email Address at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }

                if (!string.IsNullOrEmpty(objStu.Motheremail))
                    if (!IsValidEmail(objStu.Motheremail))
                    {
                        objCommon.DisplayMessage(updpnlImportData, "Invalid Mother Email Address at IDNO. " + idno, this.Page);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);
                        return;
                    }
                #endregion

                cs = (CustomStatus)objSC.UpdateBulkField(objStu, Ua_no, IpAddress);
            }
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage(updpnlImportData, "Excel Sheet Updated Successfully!!", this.Page);
                ClearControls();
                
                return;
            }
        }
        catch
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                objCommon.DisplayMessage(updpnlImportData, "Data not available in ERP Master", this.Page);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "tab();", true);

                return;
            }


        }

    }

    //Added by Gunesh Mohane 11-03-2024
    protected bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    //Added by Gunesh Mohane 07-03-2024
    protected void ClearControls() 
    {
        ddlfilter1.ClearSelection();
        ddlAdm2.SelectedValue = "0";
        ddldegree1.SelectedValue = "0";
        ddlbranch1.SelectedValue = "0";
        ddlSemester1.SelectedValue = "0";
        //ddlfilter1.SelectedValue = "0";
    }

    //Added by Gunesh Mohane 07-03-2024
    protected void btnClear2_Click(object sender, EventArgs e)
    {
        ClearControls();
    }

    #endregion
}
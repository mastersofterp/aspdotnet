using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
using System.Web;

public partial class ACADEMIC_BulkUpdation : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();

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
            if (rdbCat.SelectedValue == "24" )
            {
                foreach (ListViewDataItem lvs in lvStudFather.Items)
                {
                    studids += (lvs.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    if (rdbCat.SelectedValue == "24" || rdbCat.SelectedValue == "25")
                    {
                        FullName += (lvs.FindControl("txtName") as TextBox).Text + "$";
                        FirstName += (lvs.FindControl("txtFirstName") as TextBox).Text + "$";
                        MiddleName += (lvs.FindControl("txtMiddle") as TextBox).Text + "$";
                        LastName += (lvs.FindControl("txtLastName") as TextBox).Text + "$";

                    }
                }
                int fieldID = Convert.ToInt32(rdbCat.SelectedValue);
                string IpAddress=Request.ServerVariables["REMOTE_ADDR"];
                if (objSC.UpdateStudentAndFatherInfo(studids, fieldID, FullName, FirstName, MiddleName, LastName, IpAddress,rdbCat.SelectedItem.Text, Convert.ToInt32(Ua_no)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updStudent, "Data Updated Successfully.", this.Page);
                    return;
                }
                else
                    objCommon.DisplayMessage(this.updStudent, "Server Error.", this.Page);

                //if()
            }

            if (rdbCat.SelectedValue == "30")       //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
            {
                foreach (ListViewDataItem lvs in lvStudParentEmail.Items)
                {
                    studids += (lvs.FindControl("cbRow") as CheckBox).ToolTip + "$";
                    if (rdbCat.SelectedValue == "30")
                    {
                        femail += (lvs.FindControl("txtFatherEmail") as TextBox).Text + "$";
                        memail += (lvs.FindControl("txtMotherEmail") as TextBox).Text + "$";
                    }
                }
                int fieldID = Convert.ToInt32(rdbCat.SelectedValue);
                string IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                if (objSC.UpdateFatherAndMotherEmail(studids, fieldID, femail, memail, IpAddress, rdbCat.SelectedItem.Text, Convert.ToInt32(Ua_no)) == Convert.ToInt32(CustomStatus.RecordUpdated))
                {
                    this.BindListView();
                    objCommon.DisplayMessage(this.updStudent, "Data Updated Successfully.", this.Page);
                    return;
                }
                else
                    objCommon.DisplayMessage(this.updStudent, "Server Error.", this.Page);
            }

            if (rdbCat.SelectedValue != "24" && rdbCat.SelectedValue != "30")
            {

                foreach (ListViewDataItem lvItem in lvStudents.Items)
                {
                    // for email and mobile number radio button addedd by safal gupta on 05022021 

                    //Convert.ToInt32((lvItem.FindControl("ddlcat") as DropDownList).SelectedValue) > 0 || 
                    if ((lvItem.FindControl("txtAdmDate") as TextBox).Text != "" || (lvItem.FindControl("txtusn") as TextBox).Text != "" || (lvItem.FindControl("ddlcat") as DropDownList).SelectedItem.Text != "" || (lvItem.FindControl("txtemail") as TextBox).Text != "")
                    {
                        studids += (lvItem.FindControl("cbRow") as CheckBox).ToolTip + "$";

                        if (rdbCat.SelectedValue == "8" || rdbCat.SelectedValue == "10")
                        {
                            categorys += (lvItem.FindControl("txtAdmDate") as TextBox).Text + "$";
                        }
                        else if (rdbCat.SelectedValue == "9" || rdbCat.SelectedValue == "11" || rdbCat.SelectedValue == "15" || rdbCat.SelectedValue == "16" || rdbCat.SelectedValue == "17" || rdbCat.SelectedValue == "19" || rdbCat.SelectedValue == "21")
                        {
                            categorys += (lvItem.FindControl("txtusn") as TextBox).Text + "$";
                        }
                        else if (rdbCat.SelectedValue == "22")
                        {
                            categorys += (lvItem.FindControl("txtemail") as TextBox).Text + "$";

                        }
                        else if (rdbCat.SelectedValue == "26" || rdbCat.SelectedValue == "25")
                        {
                            categorys += (lvItem.FindControl("txtusn") as TextBox).Text + "$";
                        }
                        else if (rdbCat.SelectedValue == "28")
                        {
                            categorys += (lvItem.FindControl("txtLAdd") as TextBox).Text + "$";
                            paddress += (lvItem.FindControl("txtpadd") as TextBox).Text + "$";
                        }
                        else
                        {
                            categorys += (lvItem.FindControl("ddlcat") as DropDownList).SelectedValue + "$";
                        }
                    }

                }
                if (studids.Length <= 0 && categorys.Length <= 0)
                {
                    objCommon.DisplayMessage(this.updStudent, "Please Select Values for Filter.", this.Page);
                    return;
                }
                int fieldID = Convert.ToInt32(rdbCat.SelectedValue);


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
            if (rdbCat.SelectedValue == "1")  // for College Code
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_COLLEGECODE SC WITH (NOLOCK) ON (S.COLLEGECODE = SC.CODENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.COLLEGECODE AS COLUMNID,SC.CODENAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "2") // for Student Type
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_IDTYPE SC WITH (NOLOCK) ON (S.IDTYPE = SC.IDTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDTYPE AS COLUMNID ,SC.IDTYPEDESCRIPTION,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "3") // KEA status
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_KEA_STATUS SC WITH (NOLOCK) ON (S.KEA_STATUS = SC.KEANO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.KEA_STATUS AS COLUMNID,SC.KEA_NAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "4") // Claim Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.CATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "5") // Allotted Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.ADMCATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMCATEGORYNO AS COLUMNID,SC.CATEGORY,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "6") // Admission Batch
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH SC WITH (NOLOCK) ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "7") // Blood Group
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.BLOODGRPNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //S.CSN_NO,
            }
            else if (rdbCat.SelectedValue == "8") // Admission Date
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO AS COLUMNID", "REGNO, CSN_NO, STUDNAME, ADMDATE", "DEGREENO =" + ddlDegree.SelectedValue + " AND SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "CSN_NO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "9")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.REGNO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "10")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //S.CSN_NO,
            }
            else if (rdbCat.SelectedValue == "11")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.CSN_NO AS COLUMNNAME, S.IDNO AS COLUMNID,SC.BLOODGRPNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "12")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_CASTE C WITH (NOLOCK) ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_ADMBATCH SC ON (S.ADMBATCH = SC.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.ADMBATCH AS COLUMNID,SC.BATCHNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                //S.CSN_NO,
            }
            else if (rdbCat.SelectedValue == "13")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENTTYPE P WITH (NOLOCK) ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //S.CSN_NO,
            }
            else if (rdbCat.SelectedValue == "14")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_ADMBATCH A WITH (NOLOCK) ON (S.ACAD_YR = A.BATCHNO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,A.BATCHNAME AS COLUMNNAME, S.ACAD_YR AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

            }
            else if (rdbCat.SelectedValue == "15")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STATE_RANK, S.STUDNAME,S.STATE_RANK AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "16")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.QEXMROLLNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "17")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.ORDER_NO, S.STUDNAME,S.ORDER_NO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "18")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_PAYMENT_GROUP P WITH (NOLOCK) ON(P.GROUP_ID=S.AIDED) ", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYMENT_GROUP AS COLUMNNAME, S.AIDED AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "19")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "20")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_GENDER SC ON (S.SEX = SC.SEX) ", "S.IDNO", "S.REGNO, S.STUDNAME,S.SEX AS COLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.BLOODGRPNO AS COLUMNID,SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_GENDER SC WITH (NOLOCK) ON (S.SEX = SC.SEX)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SEX AS COLUMNID,SC.GENDERNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

            }

            else if (rdbCat.SelectedValue == "21")// FOR MOBILE ADDED BY SAFAL
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "22")// FOR EMAIL ADDED BY SAFAL
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.EMAILID AS COLUMNNAME, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "23") //  Category
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_CATEGORY SC WITH (NOLOCK) ON (S.CATEGORYNO = SC.CATEGORYNO)", "S.IDNO", "S.REGNO,S.STUDNAME,S.ADMDATE AS COLUMNNAME ,'' AS PCOLUMNNAME,S.CATEGORYNO AS COLUMNID,SC.CATEGORY", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "24")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDFIRSTNAME AS COLUMNNAME,S.STUDMIDDLENAME,S.STUDLASTNAME, S.STUDNAME,S.IDNO AS COLUMNID", "S.DEGREENO=" + ddlDegree.SelectedValue + "AND S.SEMESTERNO=" + ddlSemester.SelectedValue + "AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + "AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                dsStudFath = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.STUDNAME AS COLUMNNAME , S.STUDFIRSTNAME  AS COLUMNFIRSTNAME,S.STUDMIDDLENAME AS COLUMNMIDDLENAME,S.STUDLASTNAME AS COLUMNLASTNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "IDNO");
            }
            else if (rdbCat.SelectedValue == "25")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDFIRSTNAME AS COLUMNNAME,S.STUDMIDDLENAME,S.STUDLASTNAME, S.STUDNAME,S.IDNO AS COLUMNID", "S.DEGREENO=" + ddlDegree.SelectedValue + "AND S.SEMESTERNO=" + ddlSemester.SelectedValue + "AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + "AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO,S.FATHERNAME AS COLUMNNAME , S.FATHERFIRSTNAME  AS COLUMNFIRSTNAME,S.FATHERMIDDLENAME AS COLUMNMIDDLENAME,S.FATHERLASTNAME AS COLUMNLASTNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "IDNO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "26")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.MOTHERNAME AS COLUMNNAME ,'' AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "27")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.SHIFT AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "28")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
               
            }
            else if (rdbCat.SelectedValue == "29")  //ADDED BY VINAY MISHRA ON 17/08/2023 TO ADD MEDIUM OF INSTRUCTION FIELD FOR UPDATION
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_MEDIUMOFINSTRUCTION_MASTER MIM WITH (NOLOCK) ON (MIM.MEDIUMID = S.MEDIUM_INSTRUCT_NO)", "S.IDNO", "S.REGNO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, S.MEDIUM_INSTRUCT_NO AS COLUMNID,MIM.MEDIUMNAME,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "30")  //ADDED BY VINAY MISHRA ON 28/08/2023 TO ADD PARENT EMAIL FIELD FOR UPDATION
            {
                dsParentEmail = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHER_EMAIL AS COLUMNNAMEFATHEREMAIL,S.MOTHER_EMAIL AS COLUMNNAMEMOTHEREMAIL, S.IDNO AS COLUMNID,'' AS PCOLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }

            #region studfather
            if (rdbCat.SelectedValue == "24" )
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
            if (rdbCat.SelectedValue == "30")
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

            if (rdbCat.SelectedValue != "24" && rdbCat.SelectedValue != "30")
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
                        ctrHeader.Visible = (rdbCat.SelectedValue != "28") ? false : true;

                        foreach (ListViewItem lvRow in lvStudents.Items)
                        {
                            Control BlockStat = (Control)lvRow.FindControl("tdDivPAddress");
                            BlockStat.Visible = (rdbCat.SelectedValue != "28") ? false : true;
                        }
                        
                        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -

                        //}
                        btnSubmit.Enabled = true;
                        Label lblName = (Label)lvStudents.FindControl("lblFields");
                        lblName.Text = rdbCat.SelectedItem.Text.ToUpper();
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
            //lvStudFather.Visible = false;
            lvStudParentEmail.Visible = false;
            lvStudents.Visible = true;
            DataSet ds = null;
            DropDownList ddlcat = e.Item.FindControl("ddlcat") as DropDownList;
            TextBox txtAdmissionDate = e.Item.FindControl("txtAdmDate") as TextBox;//Admission Date  
            Image imgCal = e.Item.FindControl("imgFrmDt") as Image; // calender image
            TextBox txtUSN = e.Item.FindControl("txtusn") as TextBox; // USN No
            TextBox txtemail = e.Item.FindControl("txtemail") as TextBox;// for email
            TextBox txtLAdd = e.Item.FindControl("txtLAdd") as TextBox;  // for local address
           // TextBox tdDivPAddress = e.Item.FindControl("txtPAdd") as TextBox; // for p address
            if (rdbCat.SelectedValue == "1")  // for College Code
            {

              //  lblStudent.Visible = true;
                //ds = objCommon.FillDropDown("ACD_COLLEGECODE", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                ds = objCommon.FillDropDown("ACD_COLLEGECODE WITH (NOLOCK)", "CODENO AS COLUMNID", "CODENAME AS COLUMNNAME", "CODENO > 0", "CODENO");
                txtemail.Visible = false;
                txtLAdd.Visible = false;
            }
            else if (rdbCat.SelectedValue == "2") // for Student Type
            {
              //  lblStudent.Visible = true;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

                ds = objCommon.FillDropDown("ACD_IDTYPE WITH (NOLOCK)", "IDTYPENO AS COLUMNID", "IDTYPEDESCRIPTION AS COLUMNNAME", "", "IDTYPENO");
            }
            else if (rdbCat.SelectedValue == "3") // KEA status
            {
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
                txtemail.Visible = false;
                ds = objCommon.FillDropDown("ACD_KEA_STATUS WITH (NOLOCK)", "KEANO AS COLUMNID", "KEA_NAME AS COLUMNNAME", "", "KEANO");
            }
            else if (rdbCat.SelectedValue == "4") // Claim Category
            {
                txtLAdd.Visible = false;

               // lblStudent.Visible = true;
                txtemail.Visible = false;
                ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");
            }
            else if (rdbCat.SelectedValue == "5") // Allotted Category
            {
                txtLAdd.Visible = false;

               // lblStudent.Visible = true;
                txtemail.Visible = false;
                ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");
            }
            else if (rdbCat.SelectedValue == "6") // Admission Batch
            {
                txtLAdd.Visible = false;

               // lblStudent.Visible = true;
                txtemail.Visible = false;
                ds = objCommon.FillDropDown("ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO AS COLUMNID", "BATCHNAME AS COLUMNNAME", "BATCHNO > 0", "BATCHNO");
            }
            else if (rdbCat.SelectedValue == "7") // Blood Group
            {
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
                txtemail.Visible = false;
                ds = objCommon.FillDropDown("ACD_BLOODGRP WITH (NOLOCK)", "BLOODGRPNO AS COLUMNID", "BLOODGRPNAME AS COLUMNNAME", "BLOODGRPNO > 0", "BLOODGRPNO");
            }
            else if (rdbCat.SelectedValue == "8") // Admission Date
            {
                txtLAdd.Visible = false;

                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.ADMDATE AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                ddlcat.Visible = false;
                txtUSN.Visible = false;
                txtemail.Visible = false;
              //  lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "9")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.REGNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;

            }
            else if (rdbCat.SelectedValue == "10") // Admission Date
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.DOB AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ddlcat.Visible = false;
                txtUSN.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
                //S.CSN_NO,
            }
            else if (rdbCat.SelectedValue == "11")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT OUTER JOIN ACD_BLOODGRP SC WITH (NOLOCK) ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO,S.CSN_NO, S.STUDNAME,S.CSN_NO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.CSN_NO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

              //  lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "12")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_CASTE C ON (S.CASTE = C.CASTENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,C.CASTE AS COLUMNNAME, S.CASTE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                ds = objCommon.FillDropDown("ACD_CASTE WITH (NOLOCK)", "CASTENO AS COLUMNID", "CASTE AS COLUMNNAME", "CASTENO > 0", "CASTENO");
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "13")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                ds = objCommon.FillDropDown("ACD_PAYMENTTYPE WITH (NOLOCK)", "PAYTYPENO AS COLUMNID", "PAYTYPENAME AS COLUMNNAME", "PAYTYPENO > 0", "PAYTYPENO");
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }

            else if (rdbCat.SelectedValue == "14")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_PAYMENTTYPE P ON (S.PTYPE = P.PAYTYPENO)", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,P.PAYTYPENAME AS COLUMNNAME, S.PTYPE AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                ds = objCommon.FillDropDown("ACD_ADMBATCH WITH (NOLOCK)", "BATCHNO AS COLUMNID", "BATCHNAME AS COLUMNNAME", "", "BATCHNO");
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "15")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.STATE_RANK, S.STUDNAME,S.STATE_RANK AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.STATE_RANK");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

              //  lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "16")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.QEXMROLLNO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "17")
            {
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.ORDER_NO, S.STUDNAME,S.ORDER_NO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.ORDER_NO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "18")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S ", "S.IDNO", "S.REGNO,S.CSN_NO, S.STUDNAME,S.AIDED AS COLUMNNAME, S.AIDED AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.IDNO");
                ds = objCommon.FillDropDown("ACD_PAYMENT_GROUP WITH (NOLOCK)", "GROUP_ID AS COLUMNID", "PAYMENT_GROUP AS COLUMNNAME", "GROUP_ID > 0", "GROUP_ID");
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

             //   lblStudent.Visible = true;
               //DropDownList ddlcatt = (DropDownList)e.Item.FindControl("ddlcat");
                //ddlcatt.Items.Add(new ListItem("Aided", "1"));
                //ddlcatt.Items.Add(new ListItem("Un-aided", "2"));
            }
            else if (rdbCat.SelectedValue == "19") // Adarcard
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

           //     lblStudent.Visible = true;

            }
            else if (rdbCat.SelectedValue == "20")
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_GENDER SC ON (S.SEX = SC.SEX)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,SC.GENDERNAME AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                //ds = objCommon.FillDropDown("ACD_BLOODGRP", "BLOODGRPNO AS COLUMNID", "BLOODGRPNAME AS COLUMNNAME", "BLOODGRPNO > 0", "BLOODGRPNO");
                ds = objCommon.FillDropDown("ACD_GENDER WITH (NOLOCK)", "SEX AS COLUMNID", "GENDERNAME AS COLUMNNAME", "SEX is not null", "SEX");
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
            //    lblStudent.Visible = true;
                txtUSN.Visible = false;
                txtLAdd.Visible = false;

            }
            else if (rdbCat.SelectedValue == "21") // MOBILE
            {
                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.STUDENTMOBILE AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
              //  lblStudent.Visible = true;
                txtLAdd.Visible = false;

            }
            else if (rdbCat.SelectedValue == "22") // EMAIL
            {

                //ds = objCommon.FillDropDown("ACD_STUDENT S LEFT OUTER JOIN ACD_BLOODGRP SC ON (S.BLOODGRPNO = SC.BLOODGRPNO)", "S.IDNO AS COLUMNID ", "S.REGNO, S.STUDNAME,S.ADDHARCARDNO AS COLUMNNAME, SC.BLOODGRPNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID ", "S.REGNO,S.QEXMROLLNO, S.STUDNAME,S.EMAILID AS COLUMNNAME ", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.QEXMROLLNO");
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

              //  lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "23") // Category
            {
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

               // lblStudent.Visible = true;
                ds = objCommon.FillDropDown("ACD_CATEGORY WITH (NOLOCK)", "CATEGORYNO AS COLUMNID", "CATEGORY AS COLUMNNAME", "CATEGORYNO > 0", "CATEGORYNO");
            }
            else if (rdbCat.SelectedValue == "25")//Father Name
            {
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtLAdd.Visible = false;

              //  lblStudent.Visible = false;
                txtUSN.Visible = true;
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO", "S.REGNO, S.STUDNAME,S.FATHERNAME AS COLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");
            }
            else if (rdbCat.SelectedValue == "28")
            {

                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
                txtUSN.Visible = false;
                txtLAdd.Visible = true;
               // ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) LEFT  JOIN ACD_STU_ADDRESS A ON (S.IDNO = A.IDNO)", "S.IDNO", "S.REGNO,S.STUDNAME,A.LADDRESS AS COLUMNNAME ,A.PADDRESS AS PCOLUMNNAME, S.IDNO AS COLUMNID", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "S.REGNO");

                // txtPAdd.Visible = true;
                //   lblStudent.Visible = true;
            }
            else if (rdbCat.SelectedValue == "26")
            {
                ddlcat.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtLAdd.Visible = false;

                txtemail.Visible = false;
               // lblStudent.Visible = false;
                txtUSN.Visible = true;
                ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK)", "S.IDNO AS COLUMNID", "S.REGNO,S.STUDNAME,S.MOTHERNAME AS COLUMNNAME", "S.DEGREENO =" + ddlDegree.SelectedValue + " AND S.SEMESTERNO=" + ddlSemester.SelectedValue + " AND  ADMBATCH=" + ddlAdmBatch.SelectedValue + " AND ADMCAN=0 AND CAN=0 AND BRANCHNO=" + ddlBranch.SelectedValue, "COLUMNID");
            }
            else if (rdbCat.SelectedValue == "27") // TC Part Time/Full Time
            {
                ddlcat.Visible = true;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                txtemail.Visible = false;
              //  lblStudent.Visible = true;
                txtLAdd.Visible = false;
                txtUSN.Visible = false;
                ddlcat.Items.Clear();
                ddlcat.Items.Add(new ListItem("Please Select", "0"));
                ddlcat.Items.Add(new ListItem("Full Time", "1"));
                ddlcat.Items.Add(new ListItem("Part Time", "2"));

                ddlcat.SelectedValue = ddlcat.ToolTip;
            }
            else if (rdbCat.SelectedValue == "29")  //ADDED BY VINAY MISHRA ON 17/08/2023 - ADD FIELD TO UPDATE MEDIUM OF INSTRUCTION FOR STUDENTS
            {
                txtLAdd.Visible = false;
                txtUSN.Visible = false;
                txtemail.Visible = false;
                txtAdmissionDate.Visible = false;
                imgCal.Visible = false;
                ddlcat.Visible = true;
                ds = objCommon.FillDropDown("ACD_MEDIUMOFINSTRUCTION_MASTER WITH (NOLOCK)", "MEDIUMID AS COLUMNID", "MEDIUMNAME AS COLUMNNAME", "MEDIUMID > 0", "MEDIUMID");
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (rdbCat.SelectedValue == "8" || rdbCat.SelectedValue == "10")
                {
                 //   lblStudent.Visible = true;
                    txtemail.Visible = false;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = true;
                    imgCal.Visible = true;
                    txtUSN.Visible = false;
                    txtLAdd.Visible = false;

                }
                else if (rdbCat.SelectedValue == "9" || rdbCat.SelectedValue == "11" || rdbCat.SelectedValue == "15" || rdbCat.SelectedValue == "16" || rdbCat.SelectedValue == "17" || rdbCat.SelectedValue == "19")
                {
                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;

                 //   lblStudent.Visible = true;
                }
                else if (rdbCat.SelectedValue == "21")
                {
                    //TextBox t1=e.Item.FindControl("txtUSN") as TextBox;

                    txtemail.Visible = false;
                    txtUSN.Visible = true;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;

                    txtUSN.MaxLength = 10;
                 //   lblStudent.Visible = true;

                    txtUSN.Attributes.Add("onkeypress", "return numeralsOnly(event)");

                    //txtUSN.TextMode

                }
                else if (rdbCat.SelectedValue == "22")
                {
                    // if(txtUSN.)
                    txtUSN.Visible = false;
                    txtemail.Visible = true;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = false;
                    txtLAdd.Visible = false;
                    imgCal.Visible = false;
                  //  lblStudent.Visible = true;
                    // ClientScript.RegisterStartupScript(this.GetType(), "UpdateTime", script, true);

                    // txtUSN.Attributes.Add("onblur", "return checkEmail(event)");

                    // Page.ClientScript.RegisterStartupScript(this.GetType(), "return checkEmail()", "return checkEmail()", true);
                }
                else if (rdbCat.SelectedValue == "25")
                {
                    txtLAdd.Visible = false;
                    txtUSN.Visible = true;
                    txtemail.Visible = false;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                 //   lblStudent.Visible = true;
                }
                else if (rdbCat.SelectedValue == "26")
                {
                    txtLAdd.Visible = false;
                    txtUSN.Visible = true;
                    txtemail.Visible = false;
                    ddlcat.Visible = false;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;

                 //   lblStudent.Visible = true;
                }
                else
                {

                    ddlcat.Visible = true;
                    txtAdmissionDate.Visible = false;
                    imgCal.Visible = false;
                    txtLAdd.Visible = false;
                    txtUSN.Visible = false;
                    DataTableReader dtr = ds.Tables[0].CreateDataReader();
                    while (dtr.Read())
                    {
                        ddlcat.Items.Add(new ListItem(dtr["COLUMNNAME"].ToString(), dtr["COLUMNID"].ToString()));
                    }
                    ddlcat.SelectedValue = ddlcat.ToolTip;
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
        if (rdbCat.SelectedValue == "28")
        {
            lblAddressNote.Visible = true;
            objCommon.DisplayMessage(this.updStudent, "Note - Do Not Use Single Quotation(') Mark Character During Entering the Address Permanent Address.", this.Page);
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedIndex > 0)
        {
            trFilter.Visible = true;
            rdbCat.ClearSelection();
        }
        else
        {
            trFilter.Visible = false;
        }
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudents);//Set label -
    }

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
            if (rdbCat.SelectedValue == "24")
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
            
            if (rdbCat.SelectedValue == "30")    //ADDED BY VINAY MISHRA ON 28/08/2023 - ADD FIELD TO UPDATE PARENTS EMAIL ID FOR STUDENTS
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
}
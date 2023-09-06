
//======================================================================================
// PROJECT NAME  : UAIMS [Sarala Birla University, Ranchi]                                                          
// MODULE NAME   : ACADEMIC/EXAMINATION                                                             
// PAGE NAME     : NO DUES FEES COLLECTION                                    
// CREATION DATE : 16-APRIL-2019
// CREATED BY    : MD. REHBAR SHEIKH                                     
// MODIFIED DATE :                                                              
// MODIFIED DESC :                                                                  
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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_NoDuesCollection : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();

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
        if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                this.CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc WITH (NOLOCK)", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    divCourses.Visible = false;
                    pnlSearch.Visible = false;
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    pnlSearch.Visible = true;
                }
                else
                {
                    //pnlStart.Enabled = false;
                }

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                PopulateDropDownList();
            }

            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_ID");  //  AND CD.UGPGOT IN (" + Session["ua_section"] + ") AND CD.UGPGOT = 1
            //objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "C.COLLEGE_ID IN(" + Session["college_nos"] + ") AND C.COLLEGE_ID > 0", "C.COLLEGE_ID");
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENO");         
            ddlSession.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_NoDuesCollection.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=NoDuesProforma.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=NoDuesProforma.aspx");
        }
    }

    private void ShowDetails()
    {
        divCourses.Visible = true;
        int idno = 0;
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue); //Convert.ToInt32(Session["currentsession"]);
        StudentController objSC = new StudentController();
        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollno.Text);
        }
        try
        {
            if (idno > 0)
            {
                ViewState["IDNO"] = idno;
                // DataSet dsStudent = objSC.GetStudentNoduesfee(idno, sessionno);
                DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A WITH (NOLOCK) INNER JOIN ACD_STUD_PHOTO B WITH (NOLOCK) ON(A.IDNO = B.IDNO)", "A.IDNO", "A.STUDNAME,A.FATHERNAME,B.PHOTO,DBO.FN_DESC('SEMESTER',A.SEMESTERNO)AS SEMESTER,DBO.FN_DESC('DEGREENAME',A.DEGREENO)AS DEGREE,DBO.FN_DESC('BRANCHLNAME',A.BRANCHNO) AS BRANCH,A.BRANCHNO,A.DEGREENO,A.SCHEMENO,A.SEMESTERNO,A.COLLEGE_ID,A.PTYPE", "A.REGNO = '" + txtEnrollno.Text + "' AND REGNO IS NOT NULL", "A.IDNO");
                DataSet dsDueFee = objCommon.FillDropDown("ACD_DCR WITH (NOLOCK)", "IDNO", "TOTAL_AMT", " IDNO = " + idno + " AND RECIEPT_CODE='DF'", string.Empty);
                if (dsStudent != null && dsStudent.Tables.Count > 0)
                {
                    if (dsStudent.Tables[0].Rows.Count > 0)
                    {
                        lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                        lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
                        lblIdno.Text = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                        lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREE"].ToString();
                        hdnDegreeNo.Value = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                        hdnBranchNo.Value = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                        hdnSemesterNo.Value = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                        hdnSchemeNo.Value = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                        hdnCollegeId.Value = dsStudent.Tables[0].Rows[0]["COLLEGE_ID"].ToString();
                        lblBranch.Text = dsStudent.Tables[0].Rows[0]["BRANCH"].ToString();
                        lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTER"].ToString();
                        hdnPType.Value = dsStudent.Tables[0].Rows[0]["PTYPE"].ToString();

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";
                    }
                    if (dsDueFee.Tables[0].Rows.Count > 0)
                    {
                        lblDueFee.Text = dsDueFee.Tables[0].Rows[0]["TOTAL_AMT"].ToString();

                    }
                    else
                    {
                        lblDueFee.Text = "0.00";
                    }
                }

                showFeeDetails();
            }
            else
            {
                divCourses.Visible = false;
                objCommon.DisplayMessage(this.UpdateProgress1, "Sorry, Record not exist !!!.", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_NoDuesProforma.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowDetails();
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    public void showFeeDetails()
    {
        if (ViewState["IDNO"] != null)
        {
            MarksEntryController objMark = new MarksEntryController();
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);//Convert.ToInt32(Session["currentsession"]);
            DataSet ds = objMark.GetNoDuesFeeDetails(sessionno, Convert.ToInt32(ViewState["IDNO"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlLv.Visible = true;
                lvStudentFee.DataSource = ds;
                lvStudentFee.DataBind();

                /// showing total amount in total amount textbox using javascript.
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Total", "<script type='text/javascript'>UpdateTotalAmount();</script>", false);

                btnSubmit.Enabled = true;
            }
            else
            {
                pnlLv.Visible = false;
                lvStudentFee.DataSource = null;
                lvStudentFee.DataBind();
                ShowMessage("No Student Found.");
                return;
            }
        }
        else
        {
            ShowMessage("Please Search Student.");
            return;
        }
    }

    protected void lvStudentFee_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        try
        {
            if ((e.Item.ItemType == ListViewItemType.DataItem))
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                DataRow dr = ((DataRowView)dataItem.DataItem).Row;

                if (dr["ACCT_INCHRG_AMOUNT"].Equals("PENDING"))
                {
                    ((Label)e.Item.FindControl("lblAcctAmount")).Visible = false;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Visible = true;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Enabled = true;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Text = string.Empty;
                    //((Label)e.Item.FindControl("lblAcctAmount")).Text = string.Empty;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).ForeColor = System.Drawing.Color.Black;
                    //((Label)e.Item.FindControl("lblStatus")).Text = "Dues";
                    //((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Red;
                    //((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;

                }
                else if (!dr["ACCT_INCHRG_AMOUNT"].Equals("PENDING"))
                {
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Visible = true;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Enabled = false;
                    //((Label)e.Item.FindControl("lblAcctAmount")).Visible = true;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).BorderColor = System.Drawing.Color.Green;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).ForeColor = System.Drawing.Color.Black;
                    //((Label)e.Item.FindControl("lblStatus")).Text = "No Dues";
                    //((Label)e.Item.FindControl("lblStatus")).ForeColor = System.Drawing.Color.Green;
                    //((Label)e.Item.FindControl("lblStatus")).Font.Bold = true;
                }

                if (dr["ACCT_INCHRG_REMARKS"].ToString() == string.Empty)
                {
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Visible = true;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Enabled = true;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Text = string.Empty;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).BorderColor = System.Drawing.Color.Red;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).ForeColor = System.Drawing.Color.Black;

                }
                else
                {
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).BorderColor = System.Drawing.Color.Green;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).ForeColor = System.Drawing.Color.Black;
                }

                if (dr["ACAD_DEPT_AMOUNT"].Equals("PENDING"))
                {
                    ((Label)e.Item.FindControl("lblAcadAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblAcadAmt")).Font.Bold = true;
                }
                else if (!dr["ACAD_DEPT_AMOUNT"].Equals("PENDING"))
                {
                    //((Label)e.Item.FindControl("lblAcadAmt")).BackColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblAcadAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblAcadAmt")).Font.Bold = true;
                }

                if (dr["LIB_INCHRG_AMOUNT"].Equals("PENDING"))
                {
                    ((Label)e.Item.FindControl("lblLibAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblLibAmt")).Font.Bold = true;
                }
                else if (!dr["LIB_INCHRG_AMOUNT"].Equals("PENDING"))
                {
                    //((Label)e.Item.FindControl("lblLibAmt")).BackColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblLibAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblLibAmt")).Font.Bold = true;
                }

                if (dr["CORDINATOR_AMOUNT"].Equals("PENDING"))
                {
                    ((Label)e.Item.FindControl("lblCordinatorAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblCordinatorAmt")).Font.Bold = true;
                }
                else if (!dr["CORDINATOR_AMOUNT"].Equals("PENDING"))
                {
                    //((Label)e.Item.FindControl("lblCordinatorAmt")).BackColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblCordinatorAmt")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lblCordinatorAmt")).Font.Bold = true;
                }

                if (Convert.ToInt32(dr["IS_FINE"]) == 0)
                {
                    pnlNoduesF.Visible = true;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Enabled = false;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Text = "0.00";
                    //((TextBox)e.Item.FindControl("txtTotalAmount")).Text = "0.00";

                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Text = "NO COMMENT";
                    btnSubmit.Visible = false;
                    //btnCancel.Visible = false;
                }
                else
                {
                    pnlNoduesF.Visible = false;
                    ((TextBox)e.Item.FindControl("txtAcctAmount")).Enabled = true;
                    ((TextBox)e.Item.FindControl("txtAcctRemarks")).Enabled = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentDuesNoDuesFine.lvStudent_ItemDataBound() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(updNoDues, "Please Select Session.", this.Page);
            return;
        }

        if (txtEnrollno.Text.Trim() == string.Empty)
        {
            objCommon.DisplayMessage(updNoDues, "Enrolment No. Required.", this.Page);
            return;
        }

        string ACCT_AMOUNT = string.Empty;
        string ACCT_REMARKS = string.Empty;
        decimal TOTTAL_AMOUNT = 0;
        int IDNO =0;
        int SESSIONNO=0; 
        int COLLEGE_ID=0;
        int DEGREENO=0;
        int BRANCHNO=0;
        int SCHEMENO=0;
        int SEMESTERNO=0;
        int ACCT_UA_NO=0;
        int UA_TYPE=0;
        FeeCollectionController ObjFee = new FeeCollectionController();
        foreach (ListViewDataItem item in lvStudentFee.Items)
        {
            if ((item.FindControl("hdnIsFine") as HiddenField).Value == "1")
            {
                IDNO = Convert.ToInt32(lblIdno.Text);
                SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                COLLEGE_ID = Convert.ToInt32(hdnCollegeId.Value);
                DEGREENO = Convert.ToInt32(hdnDegreeNo.Value);
                BRANCHNO = Convert.ToInt32(hdnBranchNo.Value);
                SCHEMENO = Convert.ToInt32(hdnSchemeNo.Value);
                SEMESTERNO = Convert.ToInt32(hdnSemesterNo.Value);
                //int STUD_TYPE = Convert.ToInt32(ddlStudentType.SelectedValue);
                ACCT_AMOUNT = (item.FindControl("txtAcctAmount") as TextBox).Text.Trim();
                TOTTAL_AMOUNT = Convert.ToDecimal((item.FindControl("hdTotAmount") as HiddenField).Value.Trim());
                ACCT_REMARKS = (item.FindControl("txtAcctRemarks") as TextBox).Text;
                ACCT_UA_NO = Convert.ToInt32(Session["userno"]);
                UA_TYPE = Convert.ToInt32(Session["usertype"]);
            }
            else if ((item.FindControl("hdnIsFine") as HiddenField).Value == "0")
            {
                objCommon.DisplayMessage(updNoDues, "CAN'T SUBMIT. THERE IS NO-DUES FOUND FOR FILTERED STUDENT.", this.Page);
                return;
            }

            if (ACCT_AMOUNT == string.Empty)
            {
                objCommon.DisplayMessage(updNoDues, "Amount Required for Account Incharge. Please enter.", this.Page);
                return;
            }

            if (ACCT_REMARKS == string.Empty)
            {
                objCommon.DisplayMessage(updNoDues, "Remarks Required for Account Incharge. Please enter.", this.Page);
                return;
            }

            CustomStatus cs = new CustomStatus();

            string recieptTypeCode = objCommon.LookUp("ACD_RECIEPT_TYPE WITH (NOLOCK)", "RECIEPT_CODE", "RCPTTYPENO=2");
            int Ptype = Convert.ToInt32(hdnPType.Value);
            string colgCode = Session["colcode"].ToString();
            string idno = lblIdno.Text + ",";

            cs = (CustomStatus)ObjFee.NoDuesCollectionInsert(IDNO,SESSIONNO,COLLEGE_ID,DEGREENO,BRANCHNO,SCHEMENO,SEMESTERNO,
                                                             Convert.ToDecimal(ACCT_AMOUNT), ACCT_REMARKS, ACCT_UA_NO, UA_TYPE, 
                                                             Convert.ToDecimal(TOTTAL_AMOUNT), recieptTypeCode, Ptype, colgCode);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(updNoDues, "Record Updated Successfully!", this.Page);
                return;
            }
        }
    }
}
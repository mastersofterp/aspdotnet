
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


public partial class PAYROLL_TRANSACTIONS_PayEmpNoDuesProforma : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegistration objSReg = new StudentRegistration();
    FeeCollectionController feeController = new FeeCollectionController();
    DemandModificationController dmController = new DemandModificationController();
    EmpCreateController objECC = new EmpCreateController();

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
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() != "1" )
                {
                    //CHECK ACTIVITY FOR EXAM REGISTRATION
                    divCourses.Visible = true;
                    //pnlId.Visible = false;
                    DivSerach.Visible = false;
                    ShowEmpDetails(Convert.ToInt32(Session["idno"]));
                }
                else if (ViewState["usertype"].ToString() == "1")
                {
                    //pnlId.Visible = true;
                    BindEmployeeList();
                }
                else
                {
                    pnlStart.Enabled = false;
                }

                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }

            //hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }
        divMsg.InnerHtml = string.Empty;
    }
    #region



    private void BindEmployeeList()
    {
        DataTable dt = objECC.RetrieveEmpDetailsNoDuesNew("AAA", "ALLEMPLOYEE");
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PayEmpNoDuesProforma.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PayEmpNoDuesProforma.aspx");
        }
    }
    //private void ShowDetails()
    //{

    //    int idno = 0;
    //    //int sessionno = Convert.ToInt32(Session["currentsession"]);
    //    StudentController objSC = new StudentController();
    //    if (ViewState["usertype"].ToString() == "2")
    //    {
    //        idno = Convert.ToInt32(Session["idno"]);
    //    }
    //    else if (ViewState["usertype"].ToString() == "1")
    //    {
    //        idno =  Convert.ToInt32(txtIdNo.Text);
    //    }
    //    try
    //    {
    //        if (idno > 0)
    //        {
    //            // DataSet dsStudent = objSC.GetStudentNoduesfee(idno, sessionno);
    //            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT A INNER JOIN ACD_STUD_PHOTO B ON(A.IDNO = B.IDNO) INNER JOIN ACD_BRANCH BR ON A.BRANCHNO=BR.BRANCHNO INNER JOIN ACD_DEGREE D ON A.DEGREENO=D.DEGREENO INNER JOIN ACD_SEMESTER S ON A.SEMESTERNO=S.SEMESTERNO", "A.IDNO", "A.STUDNAME,A.FATHERNAME,B.PHOTO,SEMESTERNAME AS SEMESTER,DEGREENAME AS DEGREE,BR.LONGNAME AS BRANCH ,isnull(BR.SPECIALIZATION,'')SPECIALIZATION,A.DEGREENO", "A.IDNO = " + idno + " AND REGNO IS NOT NULL", "A.IDNO");
    //            //DataSet dsDueFee = objCommon.FillDropDown("ACD_DCR", "IDNO","TOTAL_AMT"," IDNO = " + idno + " AND RECIEPT_CODE='DF'", string.Empty);
    //            if (dsStudent != null && dsStudent.Tables.Count > 0)
    //            {
    //                if (dsStudent.Tables[0].Rows.Count > 0)
    //                {

    //                    //lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
    //                    //lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
    //                    //lblFatherName.Text = " (<b>Fathers Name : </b>" + dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString() + ")";
    //                    //lblIdno.Text = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
    //                    //lblDegree.Text = dsStudent.Tables[0].Rows[0]["DEGREE"].ToString();
    //                    //lblBranch.Text = dsStudent.Tables[0].Rows[0]["BRANCH"].ToString();
    //                    //lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTER"].ToString();
    //                    //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=student";

    //                    ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
    //                    lblEmpcode.Text = dtr["seq_no"].ToString();
    //                    lbltitle.Text = dtr["title"].ToString();
    //                    lblFName.Text = dtr["fname"].ToString();
    //                    lblMname.Text = dtr["mname"].ToString();
    //                    lblLname.Text = dtr["lname"].ToString();
    //                    lblDepart.Text = dtr["SUBDEPT"].ToString();
    //                    lblDesignation.Text = dtr["SUBDESIG"].ToString();
    //                    lblMob.Text = dtr["PHONENO"].ToString();
    //                    lblEmail.Text = dtr["EMAILID"].ToString();
    //                    lblDOJ.Text = Convert.ToDateTime(dtr["DOJ"]).ToString("dd/MM/yyyy");
    //                    imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=emp";
    //                    imgPhoto.Visible = true;
                        

    //                    BindNoDuesDetails();

    //                    divCourses.Visible = true;
    //                }
    //                //if (dsDueFee.Tables[0].Rows.Count > 0)
    //                //{
    //                //    lblDueFee.Text = dsDueFee.Tables[0].Rows[0]["TOTAL_AMT"].ToString();

    //                //}
    //                //else
    //                //{
    //                //    lblDueFee.Text = "0.00";
    //                //}
    //            }
    //            else
    //            {
    //                objCommon.DisplayMessage("Employee Not Found !!", this.Page);
    //                txtIdNo.Text = "";
    //                txtIdNo.Focus();
    //                divCourses.Visible = false;
    //                //imgPhoto.Visible = false;
    //            }
    //        }
    //        else
    //        {
    //            objCommon.DisplayMessage("Idno is not Valid !!", this.Page);
    //            txtIdNo.Text = "";
    //            txtIdNo.Focus();
    //            divCourses.Visible = false;
    //            //imgPhoto.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "PAYROLL_PayEmpNoDuesProforma.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string searchtext = string.Empty;
        string category = string.Empty;

        if (rdoSelection.SelectedValue == "IDNO")
        {
            category = "IDNO";
        }
        else if (rdoSelection.SelectedValue == "NAME")
        {
            category = "NAME";
        }
        else if (rdoSelection.SelectedValue == "PFILENO")
        {
            category = "PFILENO";
        }

        searchtext = txtSearch.Text.ToString();

        DataTable dt = objECC.RetrieveEmpDetailsNoDuesNew(searchtext, category);
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        //ShowEmpDetails();
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();

        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));

    }
    protected void BtnCancelSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }


    private void ShowEmpDetails(int idno)
    {
        int idnos = 0;
        
        if (ViewState["usertype"].ToString() != "1")
        {
            idnos = Convert.ToInt32(Session["idno"]);
        }
        else if (ViewState["usertype"].ToString() == "1")
        {
            idnos = Convert.ToInt32(idno);
        }

        DataTableReader dtr = objECC.ShowEmpDetails(idnos);
        if (dtr != null)
        {
            if (dtr.Read())
            {

                divCourses.Visible = true;


                ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
                lblEmpcode.Text = dtr["EmployeeId"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();
                lblDOJ.Text = Convert.ToDateTime(dtr["DOJ"]).ToString("dd/MM/yyyy");
                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=emp";
                imgPhoto.Visible = true;


            }
            BindNoDuesDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            divCourses.Visible = false;
            imgPhoto.Visible = false;
        }
    }

   
    public void BindNoDuesDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;

            int idno = 0;
            if (ViewState["usertype"].ToString() != "1")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (ViewState["usertype"].ToString() == "1")
            {
                idno = Convert.ToInt32(ViewState["IDNO"]);
            }


            ds = objECC.GetIDNOWiseNoDuesDetails(Convert.ToInt32(idno));

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvNoDuesDetails.DataSource = ds.Tables[0];
                lvNoDuesDetails.DataBind();
            }
            else
            {
                lvNoDuesDetails.DataSource = null;
                lvNoDuesDetails.DataBind();

            }
        }
        else
        {
            // objCommon.DisplayMessage("Please Refresh the Page again!!", this.Page);
            return;
        }
    }



    private void ShowReport(string reportTitle, string rptFileName)
    {
        //if (ddlduepurpose.SelectedIndex > 0)
        //{
            int idno = 0;
            if (ViewState["usertype"].ToString() != "1")
            {
                idno = Convert.ToInt32(Session["idno"]);
            }
            else if (ViewState["usertype"].ToString() == "1")
            {
                idno = Convert.ToInt32(ViewState["IDNO"]);
            }
            //ShowReport("NoDuesFeeProformaForStudents", "rptNoDueFeeProformaStud.rpt");
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,PAYROLL," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(Session["currentsession"]) + ",@P_DUEPURPOSE=Resigning";// +ddlduepurpose.SelectedItem.Text;

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        //}
        //else
        //{
        //    objCommon.DisplayMessage(this, "Please Select Purpose of No Dues", this.Page);
        //    return;
        //}
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        int count = 0;
        int Overcount = 0;
        int SpcCount = 0;
        foreach (ListViewDataItem dataitem in lvNoDuesDetails.Items)
        {
            SpcCount = SpcCount + 1;
            Label lblNODUES_STATUS = dataitem.FindControl("lblNODUES_STATUS") as Label;
            Label lblOverNoDuesStatus = dataitem.FindControl("lblOverNoDuesStatus") as Label;
            if (lblNODUES_STATUS.Text == "1")
            {
                count = Convert.ToInt16(count) + Convert.ToInt16(1);
            }
            if (lblOverNoDuesStatus.Text == "1")
            {
                Overcount = Convert.ToInt16(Overcount) + Convert.ToInt16(1);
            }
            
        }
        if (SpcCount == 0)
        {
            objCommon.DisplayMessage("No Dues Details Found !!!", this.Page);
            return;
        }
        else if (Overcount > 0)
        {
            objCommon.DisplayMessage("Please clear all authority dues !!!", this.Page);
            return;
        }
        else if (count > 0)
        { 
            objCommon.DisplayMessage("You Are Not Eligible Because Dues are pending !!!", this.Page);
            return;
        }
        else
        {
            ShowReport("NoDuesFeeProformaForEmployee", "rptNoDueFeeProformaEmp.rpt");
        }


    }
    
}
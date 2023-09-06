//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : CONVOCATION CERTIFICATE ISSUE
// CREATION DATE : 23-JAN-2012                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;

public partial class ACADEMIC_ConvocationCertIssue : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();
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

                PopulateDropDownList();
                lblRegulationDate.Text = objCommon.LookUp("ACD_REFE WITH (NOLOCK)", "REGULATION_DATE", string.Empty);
               //txtConvocationDate.Text = Convert.ToDateTime(objCommon.LookUp("ACD_REFE", "CONVERT(datetime,CONVOCATION_DATE,101)", string.Empty)).ToShortDateString();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
             }
        }
        divMsg.InnerHtml = string.Empty;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ConvocationCertIssue.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ConvocationCertIssue.aspx");
        }
    }
   
    private void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlConvocation, "ACD_CONVOCATION_MASTER WITH (NOLOCK)", "CONV_NO", "CONVOCATION_NAME", "CONV_NO>0", "CONV_NO DESC");
            //ddlConvocation.SelectedIndex = 1;
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
            //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=8", "SEMESTERNO");
            objCommon.FillDropDownList(ddlCertificateNo, "ACD_CERTIFICATE_MASTER WITH (NOLOCK)", "CERT_NO", "CERT_NAME", "CERT_NO > 0 AND CERT_NO = 5", "CERT_NO");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region Fill DropDownList

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH WITH (NOLOCK)", "BRANCHNO", "LONGNAME", "DEGREENO = " + ddlDegree.SelectedValue, "BRANCHNO");
                objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D WITH (NOLOCK) INNER JOIN ACD_BRANCH B WITH (NOLOCK) ON(D.DEPTNO=B.DEPTNO)", "DISTINCT D.DEPTNO", "DEPTNAME", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "D.DEPTNO");
                ddlBranch.Focus();
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add("Please Select");
            }
            else
            {
                ClearControls();
            }
            if (ddlDegree.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=8", "SEMESTERNO");
            }
            if (ddlDegree.SelectedValue == "2")
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=4", "SEMESTERNO");
            }
            if (ddlDegree.SelectedValue == "3")
            {
                objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER WITH (NOLOCK)", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO=4", "SEMESTERNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
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
                ddlScheme.Items.Clear();
                objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "BRANCHNO = " + ddlBranch.SelectedValue, "SCHEMENO");
                ddlScheme.Focus();
            }
            else
            {
                ddlScheme.Items.Clear();
                ddlScheme.Items.Add("Please Select");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlScheme.SelectedIndex > 0)
            {
                string semno = objCommon.LookUp("ACD_SCHEME WITH (NOLOCK)", "SEMESTERNO", "SCHEMENO =" + ddlScheme.SelectedValue);
                ddlSem.SelectedValue = semno;
            }
            else
            {
                ddlSem.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)

                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    private void ClearControls()
    {
       // ddlDegree.SelectedIndex = 0;
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add("Please Select");
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add("Please Select");
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ddlDegree.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ClearControls();
    }
   
    
    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void pnlStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        //Label lbl = e.Item.FindControl("lblIdno") as Label;
        //Label lblcourse = e.Item.FindControl("lblCourse") as Label;
        //if (lbl.Text == hdfidno.Value)
        //    lbl.Visible = false;
        //else
        //    lbl.Visible = true;

        
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            StudentRegist objSR = new StudentRegist();

            string student_name = string.Empty;

            foreach (ListViewDataItem dataitem in lvConvocationCertificate.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                { 
                    GenerateQrCode((((dataitem.FindControl("lblIDNo")) as Label).Text), (((dataitem.FindControl("lblRegno")) as Label).Text), (((dataitem.FindControl("lblStudName")) as Label).Text));
                }
            }
            string ids = GetStudentIDs();
            if (!string.IsNullOrEmpty(ids))

                if(ddlCertificateNo.SelectedValue == "5")
                {
                    // show convocation certificate 
                    //if(ddlDegree.SelectedValue =="4" || ddlDegree.SelectedValue =="6")
                    //    ShowReport(rdoReportType.SelectedValue, ids, "Convocation_Certificate_Report", "rptConvocation_Certificate_MCA.rpt");
                    //else
                    ShowReport(rdoReportType.SelectedValue, ids, "Convocation_Certificate_Report", "rptConvocation_Certificate.rpt");
                }
                //else if (ddlCertificateNo.SelectedValue == "2")
                //{
                //    // show DOCTOR OF PHILOSOPHY IN ENGINEERING certificate 
                //    ShowReport(rdoReportType.SelectedValue, ids, "DOCTOR_OF_PHILOSOPHY_IN_ENGINEERING", "rptDoctorPhilosophyEngg.rpt");
                //}
                //else if (ddlCertificateNo.SelectedValue == "3")
                //{
                //    // show DEPARTMENTAL GOLD MEDAL certificate 
                //    ShowReport(rdoReportType.SelectedValue, ids, "DEPARTMENTAL_GOLD_MEDAL", "rptDepartmentalGoldMedal.rpt");
                //}
                //else if (ddlCertificateNo.SelectedValue == "4")
                //{
                //    // show INSTITUTE GOLD MEDAL certificate 
                //    ShowReport(rdoReportType.SelectedValue, ids, "INSTITUTE_GOLD_MEDAL", "rptInstituteGoldMedal.rpt");
                //}

            else
                objCommon.DisplayMessage("Please Select Students!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentIDCard.btnBackSide_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnStudReport_Click(object sender, EventArgs e)
    {
        string ids = "0";
        ShowReport(rdoReportType.SelectedValue,ids, "Convocation Student List", "rptConvocation_List.rpt");
    }

    private void ShowReport(string exporttype,string param, string reportTitle, string rptFileName)
    {
        try
        {
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,REPORTS,ACADEMIC," + rptFileName;

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + param + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_CERTNO=" + Convert.ToInt32(ddlCertificateNo.SelectedValue);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    private void BindListView()
    {
        //Get student list as per scheme & semester & secion
        DataSet ds = null;


        ds = objCommon.FillDropDown("ACD_STUDENT S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT_HIST SR WITH (NOLOCK) ON (S.IDNO = SR.IDNO) INNER JOIN ACD_TRRESULT TR WITH (NOLOCK) ON (TR.IDNO=SR.IDNO AND TR.SCHEMENO= SR.SCHEMENO AND TR.SEMESTERNO = SR.SEMESTERNO AND TR.SESSIONNO = SR.SESSIONNO)", "DISTINCT S.IDNO", "S.REGNO,DBO.FN_DESC('DEGREENAME',S.DEGREENO)AS DEGREE,DBO.FN_DESC('BRANCHSNAME',S.BRANCHNO)AS BRANCH_SHORT,DBO.FN_DESC('BRANCHLNAME',S.BRANCHNO) AS BRANCH_LONG,S.STUDNAME", "TR.SESSIONNO=" + ddlSession.SelectedValue + " AND S.DEGREENO = " + ddlDegree.SelectedValue + " AND TR.SEMESTERNO = " + ddlSem.SelectedValue + " AND S.BRANCHNO = " + ddlBranch.SelectedValue + " AND SR.SCHEMENO=" + ddlScheme.SelectedValue + " AND TR.PASSFAIL='PASS' AND S.CAN=0 AND S.ADMCAN=0 and s.idno not in (select distinct idno from acd_trresult t where idno=s.idno and sessionno=(select MAX(sessionno) from acd_trresult where idno=t.idno and semesterno=t.semesterno) and result='F')", "S.REGNO");
        

        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlConvocation.Visible = true;
                lvConvocationCertificate.DataSource = ds.Tables[0];
                lvConvocationCertificate.DataBind();
                lvConvocationCertificate.Visible = true;
            }
            else
            {
                pnlConvocation.Visible = false;
                lvConvocationCertificate.DataSource = null;
                lvConvocationCertificate.DataBind();
            }
        }
        else
        {
            pnlConvocation.Visible = false;
            lvConvocationCertificate.DataSource = null;
            lvConvocationCertificate.DataBind();
        }
        string id = string.Empty;
        foreach (ListViewDataItem item in lvConvocationCertificate.Items)
        {
            CheckBox cbRow = item.FindControl("cbRow") as CheckBox;
            Label lblIDNo = item.FindControl("lblIDNo") as Label;
            if (lblIDNo.Text == objCommon.LookUp("ACD_CONVOCATION", "IDNO", "IDNO=" + lblIDNo.Text + " AND CERTNO=" + ddlCertificateNo.SelectedValue))
                cbRow.Checked = true;
            else
                cbRow.Checked = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      
        StudentRegistration objSRegist = new StudentRegistration();
        StudentRegist objSR = new StudentRegist();

        string student_name = string.Empty;
        string degree = string.Empty;
        string branch = string.Empty;
        int deptNo = 0;
        string deptName = string.Empty;
        int certno = 0;
        try
        {
            foreach (ListViewDataItem dataitem in lvConvocationCertificate.Items)
            {
                //Get Student Details from lvStudent
                CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
                if (cbRow.Checked == true)
                {
                    objSR.IDNOS += Convert.ToInt32(((dataitem.FindControl("lblIDNo")) as Label).Text) + "$";
                    objSR.REGNO += ((dataitem.FindControl("lblRegno")) as Label).Text + "$";
                    student_name += ((dataitem.FindControl("lblStudName")) as Label).Text + "$";
                    degree = ((dataitem.FindControl("lblDegree")) as Label).Text;
                    branch = ((dataitem.FindControl("lblBranch")) as Label).ToolTip;
                    
                }
            }
                objSR.UA_NO = Convert.ToInt32(Session["userno"]);
                objSR.IPADDRESS = ViewState["ipAddress"].ToString();
                certno = Convert.ToInt32(ddlCertificateNo.SelectedValue);

                //objSR.IPADDRESS = "192.168.0.11";
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
            // get department number 
                //if (ddlCertificateNo.SelectedValue != "4" && ddlCertificateNo.SelectedValue != "3")
                //{
                deptNo = Convert.ToInt32(objCommon.LookUp("ACD_BRANCH WITH (NOLOCK)", "DISTINCT DEPTNO", "DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + "AND BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue)));
                deptName = objCommon.LookUp("ACD_DEPARTMENT WITH (NOLOCK)", "DEPTNAME", "DEPTNO=" + Convert.ToInt32(deptNo));
                //}
                //if (ddlCertificateNo.SelectedValue == "3")
                //    deptName = ddlDept.SelectedItem.Text;

                    CustomStatus cs = (CustomStatus)objSRegist.AddConvocation(objSR, student_name, degree, branch, lblRegulationDate.Text, txtConvocationDate.Text, Session["ipAddress"].ToString(), deptName, certno, Convert.ToInt32(ddlConvocation.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage("Student Certificate Generate Successfully!!", this.Page);
                    }
                    else
                        objCommon.DisplayMessage("Error in Certificate Number Generate!!", this.Page);
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void GenerateQrCode(string idno,string regno,string studname)
    {

        DataSet ds = objCommon.FillDropDown("ACD_STUDENT WITH (NOLOCK)", "*", "", "REGNO='" + regno + "'", "REGNO");

        //string BranchName = objCommon.LookUp("ACD_BRANCH","SHORTNAME","BRANCHNO="+ ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim()+"");

        DataSet ds1 = objQrC.GetStudentDataForConvocation(Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"].ToString().Trim()), Convert.ToInt16(ds.Tables[0].Rows[0]["DEGREENO"].ToString().Trim()));
      
        string Qrtext = "RegNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Serial_No=" + ds1.Tables[0].Rows[0]["SERIALNO"].ToString().Trim() + "; Branch=" +
                              ds1.Tables[0].Rows[0]["DEPTNAME"].ToString().Trim() + "; CGPA=" +
                              ds1.Tables[0].Rows[0]["CGPA"].ToString().Trim() + "; Convocation_Date=" +
                              ds1.Tables[0].Rows[0]["CONVOCATION_DATE"].ToString().Trim() + ";Process_Date=" +
                              ds1.Tables[0].Rows[0]["PROCESSDATE"].ToString().Trim() + ";";
        
        Session["qr"] = Qrtext.ToString();

        QRCodeEncoder encoder = new QRCodeEncoder();
        encoder.QRCodeVersion = 10;
        Bitmap img = encoder.Encode(Session["qr"].ToString());
        //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
        img.Save(Server.MapPath("~\\img.Jpeg"));
        ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

        byte[] QR_IMAGE = ViewState["File"] as byte[];
        long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"].ToString().Trim()), QR_IMAGE);
    }
    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }
    private string GetStudentIDs()
    {
        string studentIds = string.Empty;
        try
        {
            foreach (ListViewDataItem item in lvConvocationCertificate.Items)
            {
                if ((item.FindControl("cbRow") as CheckBox).Checked)
                {
                    if (studentIds.Length > 0)
                        studentIds += "$" + (item.FindControl("lblIDNo") as Label).Text;
                    else
                        studentIds = (item.FindControl("lblIDNo") as Label).Text;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ConvocationCertIssue.GetStudentIDs() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentIds;
    }

    protected void ddlCertificateNo_SelectedIndexChanged(object sender, EventArgs e)
    {

        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.UGPGOT IN (" + Session["ua_section"] + ")", "DEGREENAME");
        if (ddlCertificateNo.SelectedValue == "4" || ddlCertificateNo.SelectedValue == "3")
        {
            ddlBranch.SelectedIndex = 0;
            ddlScheme.SelectedIndex = 0;
            ddlSem.SelectedIndex = 0;
            trBranch.Visible = false;
            trScheme.Visible = false;
            trSem.Visible = false;
        }
        else 
        {
            trBranch.Visible = true;
            trScheme.Visible = true;
            trSem.Visible = true;
        }
        if (ddlCertificateNo.SelectedValue == "3")
            trDept.Visible = true;
        else
            trDept.Visible = false;

        lvConvocationCertificate.DataSource = null;
        lvConvocationCertificate.DataBind();
        lvConvocationCertificate.Visible = false;
    }

    protected void btnConvoReport_Click(object sender, EventArgs e)
    {
        string ids = "0";
        //string ids = GetStudentIDs();
        //if(ddlDegree.SelectedValue=="3")
        //    ShowReport(ids, "Convocation Student List", "rptConvocationStudList.rpt");
        //else
        //    ShowReport(ids, "Convocation Student List", "rptConvListOhter.rpt");

        //if(ddlDegree.SelectedValue == "3")
        //    ShowReport(rdoReportType.SelectedValue, ids, "Convocation Student List", "rptConvocationStudList.rpt");
        //else
            ShowReport(rdoReportType.SelectedValue, ids, "Convocation Student List", "rptConvListOhter.rpt");

    }

    protected void btnConvReport_Click(object sender, EventArgs e)
    {
        string ids = "0";

        //string ids = GetStudentIDs();
        ShowReport(rdoReportType.SelectedValue, ids, "Convocation Student List", "rptConvocationStudListNoPhoto.rpt");
    }


    protected void btnPassoutStudent_Click(object sender, EventArgs e)
    {
        if (ddlConvocation.SelectedIndex == 0 || ddlCertificateNo.SelectedIndex == 0 || ddlDegree.SelectedIndex == 0 || ddlBranch.SelectedIndex == 0)
        {
            objCommon.DisplayMessage("Please select mandatory selections given below in Note:",this.Page);
            return;
        }
        ShowReportPassout(rdoReportType.SelectedValue, "Passout Student List", "StudentPassoutListWithPhotos.rpt");
    }

    private void ShowReportPassout(string exporttype, string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "exporttype=" + exporttype;
            url += "&filename=" + reportTitle.Replace(" ", "-").ToString() + "." + rdoReportType.SelectedValue;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + ",@P_CERTNO=" + Convert.ToInt32(ddlCertificateNo.SelectedValue) + ",@P_CONVO_NO=" + Convert.ToInt32(ddlConvocation.SelectedValue) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",username=" + Session["userfullname"].ToString() + "";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    
}

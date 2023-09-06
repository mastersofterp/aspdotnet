using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using System.Diagnostics;

using System.Collections.Generic;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;


public partial class ACADEMIC_Condonation_Apply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CourseController objCC = new CourseController();
    ConfigController Confi = new ConfigController();
    StudentController objSC = new StudentController();
    ActivityController objActController = new ActivityController();
    string sessionno="";
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_Condonation"].ToString();
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
                if (CheckActivity() == true)
                {
                    divCourses.Visible = true;
                }
                else
                {
                    divCourses.Visible = false;
                }
                if (Convert.ToInt32(Session["usertype"]) == 1)
                {
                    CheckPageAuthorization();
                }
                else
                {
                }
                if (Session["usertype"].ToString() == "2")
                {

                    ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage(UpdApplyCondolance, "This Page Is For Student Login", this.Page);
                    return;
                }             
            }
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
    }
    private void ShowStudentDetails()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_STUDENT_DETAILS";
            string SP_Parameters2 = "@P_IDNO";
            string Call_Values2 = "" + Convert.ToInt32(Session["idno"]) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsStudList.Tables[0].Rows[0];
                lblStudentName1.Text = dr["STUDNAME"].ToString() == string.Empty ? string.Empty : dr["STUDNAME"].ToString();
                lblApplydate.Text = dr["APPLY_DATE"].ToString() == string.Empty ? string.Empty : dr["APPLY_DATE"].ToString();
                lblAdmissionNo.Text = dr["ENROLLNO"].ToString() == string.Empty ? string.Empty : dr["ENROLLNO"].ToString();
                lblSemester.Text = dr["SEMESTERNAME"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNAME"].ToString();
                ViewState["COLLEGE_ID"] = dr["COLLEGE_ID"].ToString() == string.Empty ? string.Empty : dr["COLLEGE_ID"].ToString();
                ViewState["SEMESTERNO"] = dr["SEMESTERNO"].ToString() == string.Empty ? string.Empty : dr["SEMESTERNO"].ToString();
                ViewState["SCHEMENO"] = dr["schemeno"].ToString() == string.Empty ? string.Empty : dr["schemeno"].ToString();
                ViewState["DEGREENO"] = dr["DEGREENO"].ToString() == string.Empty ? string.Empty : dr["DEGREENO"].ToString();
                ViewState["BRANCHNO"] = dr["BRANCHNO"].ToString() == string.Empty ? string.Empty : dr["BRANCHNO"].ToString();
                ViewState["YEAR"] = dr["YEAR"].ToString() == string.Empty ? string.Empty : dr["YEAR"].ToString();
                if (dr["PHOTO"].ToString() != "")
                {
                    //imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() + "&type=student";
                }
                objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER S INNER JOIN ACD_COLLEGE_MASTER CM ON CM.COLLEGE_ID=S.COLLEGE_ID", "DISTINCT SESSIONNO", "(SESSION_NAME +' - ' + COLLEGE_NAME) COLLATE DATABASE_DEFAULT AS SESSION_NAME", "S.COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]) + "AND SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)  and STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%'  and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
                //objCommon.FillDropDownList(ddlsession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]), "SESSIONNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.ShowStudentDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //private void CheckActivity()
    //{
    //    string sessionno = string.Empty;
    //    int COLLEGE_ID= Convert.ToInt32(objCommon.LookUp("acd_student","college_id","idno="+Convert.ToInt32(Session["idno"])));
    //    sessionno = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 AND college_id=" + COLLEGE_ID);

    //    ActivityController objActController = new ActivityController();
    //    DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

    //    if (dtr.Read())
    //    {
    //        if (dtr["STARTED"].ToString().ToLower().Equals("false"))
    //        {
    //            objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
    //            divCourses.Visible = false;
    //            return;
    //        }
    //        if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
    //        {
    //            objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
    //            divCourses.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
    //        divCourses.Visible = false;
    //        return;
    //    }
    //    divCourses.Visible = true;
    //    dtr.Close();
    //}
    private bool CheckActivity()
    {

        bool ret = true;
        string sessionno = string.Empty;
        string degreeno = string.Empty;
        string branchno = string.Empty;
        string semesterno = string.Empty;
        ActivityController objActController = new ActivityController();
        DataSet ds = objActController.GetSessionNoForActivity(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            sessionno = ds.Tables[0].Rows[0]["SESSIONNO"].ToString();
            degreeno = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            branchno = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
            semesterno = ds.Tables[0].Rows[0]["SEMESTERNO"].ToString();
        }

        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(sessionno), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), degreeno, branchno, semesterno);

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage("This Activity has been Stopped. Contact Admin.!!", this.Page);
                divCourses.Visible = false;
                ret = false;

            }
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                divCourses.Visible = true;
                ret = false;
            }

        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            divCourses.Visible = false;
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=StudentDocumentList.aspx");
        }
    }
    private void BindList()
    {
        int listcount =0;
        int enabcount =0;
        int reportcount = 0;
        int idno = Convert.ToInt32(Session["idno"]);
        string reason = objCommon.LookUp("ACD_STUDENT_APPLY_CONDOLANCE", "DISTINCT REASON", "idno=" + idno);
        txtRequestDescription.Text = reason;
        DataSet dsAttendanceDetails = Confi.GetDetailsOfAttendanceByIdnoForoCondolance(idno, Convert.ToInt32(ddlsession.SelectedValue), Convert.ToInt32(ViewState["COLLEGE_ID"]), Convert.ToInt32(ViewState["SEMESTERNO"]));
        if (dsAttendanceDetails != null && dsAttendanceDetails.Tables.Count > 0 && dsAttendanceDetails.Tables[0].Rows.Count > 0)
        {
            Panel1.Visible = true;
            Coursesdetails.Visible = true;
            lvAttendanceDetails.DataSource = dsAttendanceDetails;
            lvAttendanceDetails.DataBind();
        }
        else
        {
            Panel1.Visible = false;
            Coursesdetails.Visible = false;
            objCommon.DisplayMessage(this.UpdApplyCondolance, "Record Not Found!", this.Page);     
            lvAttendanceDetails.DataSource = null;
            lvAttendanceDetails.DataBind();
        }

        foreach (ListViewDataItem lvitem in lvAttendanceDetails.Items)
        {
            listcount++;
            CheckBox chkallotment = lvitem.FindControl("chkallotment") as CheckBox;
            Label lblstatus = lvitem.FindControl("lblstatus") as Label;
            Label lblshortage = lvitem.FindControl("lblshortage") as Label;
            Label lblattendace = lvitem.FindControl("lblattendace") as Label;
            Label lblrange = lvitem.FindControl("lblrange") as Label;
            HiddenField hdfrangfrom = lvitem.FindControl("hdfrangfrom") as HiddenField;
            HiddenField hdfoperatorfrom = lvitem.FindControl("hdfoperatorfrom") as HiddenField;
            HiddenField hdfopratoto = lvitem.FindControl("hdfopratoto") as HiddenField;
            
            if ( lblshortage.Text == "" || lblshortage.Text.ToString().Substring(0, 1) == "-"  )
            {
                enabcount++;
                chkallotment.Enabled = false;
            }
           
            else if (hdfoperatorfrom.Value == "<")
            {
                if (Convert.ToDecimal(hdfrangfrom.Value) < Convert.ToDecimal(lblattendace.Text) || lblshortage.Text != "0")
                {
                    enabcount++;

                    chkallotment.Enabled = false;
                }

            }
            else if (hdfoperatorfrom.Value == "=")
            {
                if (Convert.ToDecimal(hdfrangfrom.Value) != Convert.ToDecimal(lblattendace.Text))
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }

            }
            else if (hdfoperatorfrom.Value == "<=")
            {
                if (Convert.ToDecimal(lblattendace.Text) <= Convert.ToDecimal(hdfrangfrom.Value))
                {
                    chkallotment.Enabled = true;
                }
                else
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }

            }
            else if (hdfoperatorfrom.Value == ">" && hdfopratoto.Value == "<")
            {
                if (Convert.ToDecimal(lblattendace.Text) > Convert.ToDecimal(hdfrangfrom.Value) && Convert.ToDecimal(lblattendace.Text) < Convert.ToDecimal(lblrange.Text))
                {
                    chkallotment.Enabled = true;
                }
                else
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }

            }
            else if (hdfoperatorfrom.Value == ">")
            {
                if (Convert.ToDecimal(lblattendace.Text) > Convert.ToDecimal(hdfrangfrom.Value))
                {
                    chkallotment.Enabled = true;
                }
                else
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }

            }

            else if (hdfoperatorfrom.Value == ">=")
            {
                if (Convert.ToDecimal(lblattendace.Text) >= Convert.ToDecimal(hdfrangfrom.Value))
                {
                    chkallotment.Enabled = true;
                }
                else
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }

            }
            else if (hdfopratoto.Value == "<")
            {
                if (Convert.ToDecimal(lblrange.Text) < Convert.ToDecimal(lblattendace.Text) || lblshortage.Text == "0")
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }
            }
            else if (hdfopratoto.Value == "=")
            {
                if (lblshortage.Text != "0")
                {
                    enabcount++;
                    chkallotment.Enabled = false;
                }
            }
           
            if (lblstatus.Text == "Pending" || lblstatus.Text == "Approved" || lblstatus.Text == "Reject")
            {
                reportcount++;
            }
            if (lblstatus.Text == "Approved")
            {
                enabcount++;
                chkallotment.Enabled = false;
                chkallotment.BackColor = System.Drawing.Color.Green;
            }
        }
        if (reportcount > 0)
        {
            btnreport.Visible = true;
        }
        else
        {

            btnreport.Visible = false;
        }
        if (listcount == enabcount)
        {
            txtRequestDescription.Enabled = false;
            btnSubmit.Enabled = false;
            btnreport.Enabled = false;
        }
        else
        {
            txtRequestDescription.Enabled = true;
            btnSubmit.Enabled = true;
            btnreport.Enabled = true;
        }
    }
    protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();
    }
    private void clear()
    {
        Coursesdetails.Visible = false;
        ddlsession.SelectedIndex = 0;
        txtRequestDescription.Text = "";
        Panel1.Visible = false;
        lvAttendanceDetails.DataSource = null;
        lvAttendanceDetails.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            string couresno = "", coursename = "", attendance = "", range = "", shortage = "";
            int idno = Convert.ToInt32(Session["idno"]);
            CustomStatus cs = CustomStatus.Others;
            string filename = "";
            string docname = "";
            int countsempattern = 0;
            countsempattern = Convert.ToInt32(objCommon.LookUp("ACD_SCHEME", "ISNULL(STUDY_PATTERN_NO,0)STUDY_PATTERN_NO", "SCHEMENO=" + Convert.ToInt32(ViewState["SCHEMENO"])));
            if (countsempattern == 0)
            {
                objCommon.DisplayMessage(this.UpdApplyCondolance, "Semester Pattern Not Define please Contact Your Department", this.Page);
                clear();
                return;
            }
            int degreeduration = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "DURATION_LAST_SEM", "DEGREENO=" + Convert.ToInt32(ViewState["DEGREENO"]) + "AND BRANCHNO=" + Convert.ToInt32(ViewState["BRANCHNO"])));
            if (countsempattern == 1 || countsempattern == 2)
            {
                int countCondo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_APPLY_CONDOLANCE", "COUNT(*)", "IDNO=" + idno + "AND YEAR=" + Convert.ToInt32(ViewState["YEAR"])));
                if (countCondo == 1)
                {
                    objCommon.DisplayMessage(this.UpdApplyCondolance, "You Cannot Apply For Condonation More Than Once in that Perticular Year", this.Page);
                    clear();
                    return;
                }
                int countCondoDegree = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_APPLY_CONDOLANCE", "isnull(MAX(COUNT),0)", "IDNO=" + idno));
                if (countCondoDegree > 2)
                {
                    objCommon.DisplayMessage(this.UpdApplyCondolance, "You Cannot Apply For Condonation More Than Two Times in Complete degree duration", this.Page);
                    clear();
                    return;
                }               
            }
            else if ((countsempattern == 3))
            {
                int countCondo = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT_APPLY_CONDOLANCE", "COUNT(*)", "IDNO=" + idno + "AND SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"]) + "AND YEAR=" + Convert.ToInt32(ViewState["YEAR"])));
                if (countCondo == 1)
                {
                    objCommon.DisplayMessage(this.UpdApplyCondolance, "You Cannot Apply For Condonation More Than Once", this.Page);
                    clear();
                    return;
                }
                
            }

            if (fuDocument.HasFile)
            {
                string contentType = contentType = fuDocument.PostedFile.ContentType;
                string ext = System.IO.Path.GetExtension(fuDocument.PostedFile.FileName);
                HttpPostedFile file = fuDocument.PostedFile;
                filename = fuDocument.PostedFile.FileName;
                docname = idno + "_doc_" + "Condonation" + ext;

                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, idno + "_doc_" + "Condonation" + "", fuDocument);
                if (retval == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                    return;
                }
            }
            else
            {
                docname = "";
            }
            foreach (ListViewDataItem dataitem in lvAttendanceDetails.Items)
            {
                CheckBox chkBox = dataitem.FindControl("chkallotment") as CheckBox;
                Label lblcoursename = dataitem.FindControl("lblcoursename") as Label;
                Label lblattendace = dataitem.FindControl("lblattendace") as Label;
                Label lblrange = dataitem.FindControl("lblrange") as Label;
                Label lblshortage = dataitem.FindControl("lblshortage") as Label;
                Label lblstatus = dataitem.FindControl("lblstatus") as Label;
                if (chkBox.Checked == true && chkBox.Enabled == true)
                {
                    count++;
                    couresno   += chkBox.ToolTip + ',';
                    coursename += lblcoursename.Text + ',';
                    attendance += lblattendace.Text + ',';
                    range      += lblrange.Text + ',';
                    shortage   += lblshortage.Text + ',';
                }
            }
            couresno   = couresno.TrimEnd(',');
            coursename = coursename.TrimEnd(',');
            attendance = attendance.TrimEnd(',');
            range      = range.TrimEnd(',');
            shortage   = shortage.TrimEnd(',');

            if (count == 0)
            {
                objCommon.DisplayMessage(this.UpdApplyCondolance, "Please Select At least One checkbox", this.Page);
            }
            cs = (CustomStatus)Confi.InsertStudentCondoApply(Convert.ToInt32(idno), Convert.ToInt32(ddlsession.SelectedValue), Convert.ToInt32(ViewState["SEMESTERNO"]), couresno, coursename, attendance, range, shortage, docname, Convert.ToString(txtRequestDescription.Text), Convert.ToInt32(Session["userno"]), Convert.ToString(ViewState["ipAddress"]), Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToString(hdnSelectedCourseFee.Value));
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(UpdApplyCondolance, "Record Saved Successfully", this.Page);
                ShowStudentDetails();
                btnreport.Visible = true;
                clear();               
            }         
        }
        catch (Exception ex)
        {

        }
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
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Coursesdetails.Visible = false;
        ddlsession.SelectedIndex = 0;
        lvAttendanceDetails.DataSource = null;
        lvAttendanceDetails.DataBind();
    }
    protected void btnPy_Click(object sender, EventArgs e)
    {

    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowReport("Student Condonation Report", "StudentCondonation.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int idno = Convert.ToInt32(Session["idno"]);
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ""

                   + ",@P_SESSIONNO=" + Convert.ToInt32(ddlsession.SelectedValue)
                   + ",@P_IDNO=" + idno
                   + ",@P_SEMESTERNO=" + Convert.ToInt32(ViewState["SEMESTERNO"])
                   + ",@P_COLLEGE_ID=" + Convert.ToInt32(ViewState["COLLEGE_ID"]);
              
            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdApplyCondolance, this.UpdApplyCondolance.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
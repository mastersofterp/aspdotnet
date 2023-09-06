using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using DynamicAL_v2;
using System.IO;
using Microsoft.Win32;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

public partial class ACADEMIC_EXAMINATION_StudExamTrans_Reval_ReexamApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    ExamController objExamController = new ExamController();
    DynamicControllerAL AL = new DynamicControllerAL();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();

    int degreeno = 0;
    int branchno = 0;
    int semesterno = 0;
    int collegeid = 0;

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
                    //CheckPageAuthorization();
                    PopulerDropdown();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //CheckActivity();
                }
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void PopulerDropdown()
    {
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND EXAMTYPE=1 AND IS_ACTIVE=1 AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + "", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH  B INNER JOIN [DBO].[ACD_COLLEGE_DEGREE_BRANCH] DB ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO > 0 AND ISNULL(B.ACTIVESTATUS,0)=1", "B.BRANCHNO");
    }

    private bool CheckActivity()
    {
        
        DataSet dssession = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SESSION_NO", "", "STARTED = 1 AND  SHOW_STATUS =1 AND SA.USERTYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "SESSION_NO DESC");

        if (dssession.Tables[0].Rows.Count > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
            ddlSession.SelectedValue = dssession.Tables[0].Rows[0]["SESSION_NO"].ToString();
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "SESSIONNO DESC");
        }
        //ddlSession.SelectedValue = dssession.Tables[0].Rows[0]["SESSION_NO"].ToString();
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "SESSIONNO DESC");


        DataSet DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()), "");
        if (DS != null && DS.Tables[0].Rows.Count > 0)
        {
            degreeno = Convert.ToInt32(DS.Tables[0].Rows[0]["DEGREENO"].ToString());
            branchno = Convert.ToInt32(DS.Tables[0].Rows[0]["BRANCHNO"].ToString());
            semesterno = Convert.ToInt32(DS.Tables[0].Rows[0]["SEMESTERNO"].ToString());
            collegeid = Convert.ToInt32(DS.Tables[0].Rows[0]["COLLEGE_ID"].ToString());
        }
        bool ret = true;
        ActivityController objActController = new ActivityController();
        DataTableReader dtr = objActController.CheckActivity(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Request.QueryString["pageno"].ToString()), Convert.ToString(degreeno), Convert.ToString(branchno), Convert.ToString(semesterno));

        if (dtr.Read())
        {
            if (dtr["STARTED"].ToString().ToLower().Equals("false"))
            {
                objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage(this, "Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
                // Divmain.Visible = false;
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
            //  Divmain.Visible = false;

        }
        dtr.Close();
        return ret;
        //}
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudExamTransApproval.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudExamTransApproval.aspx");
        }
    }

    private void GetTransStudData()
    {
        DataSet dsCERT = objCommon.DynamicSPCall_Select("PKG_ACD_GET_TRANS_APPLY_LIST", "@P_SESSIONNO, @P_SEMESTERNO,@P_BRANCHNO,@P_STATUS", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlstatus.SelectedValue) + "");
        // DataSet dsCERT = objExamController.GetTransactionEntryList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSemester.SelectedValue));
        if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
        {
            lvltransapprov.DataSource = dsCERT;
            lvltransapprov.DataBind();
        }
        else
        {
            objCommon.DisplayMessage(this, "No Record Found", this.Page);
            lvltransapprov.DataSource = null;
            lvltransapprov.DataBind();

        }
    }

    private void GetReExamDetails()
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet dsCERT = objCommon.DynamicSPCall_Select("PKG_ACD_GET_REEXAM_STUD_LIST_NEW", "@P_SESSIONNO, @P_SEMESTERNO,@P_BRANCHNO,@P_STATUS", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlstatus.SelectedValue) + "");
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvltransapprov.DataSource = dsCERT;
                lvltransapprov.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                lvltransapprov.DataSource = null;
                lvltransapprov.DataBind();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select Session", this.Page);
            return;
        }
    }

    private void GetRevalExamDetails()
    {
        if (ddlSession.SelectedIndex > 0)
        {
            DataSet dsCERT = objCommon.DynamicSPCall_Select("PKG_ACD_GET_REVAL_STUD_LIST_NEW", "@P_SESSIONNO, @P_SEMESTERNO,@P_BRANCHNO,@P_STATUS", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlstatus.SelectedValue) + "");
            if (dsCERT.Tables[0].Rows.Count > 0 && dsCERT != null)
            {
                lvltransapprov.DataSource = dsCERT;
                lvltransapprov.DataBind();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                lvltransapprov.DataSource = null;
                lvltransapprov.DataBind();
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Please Select Session", this.Page);
            return;
        }
    }
    //private void GetPromotionStatus()
    //{
    //    DataSet dsCERT1 = objCommon.DynamicSPCall_Select("PKG_GET_PROMOTION_STATUS_BYID_EXAM_APPROVED", "@P_SESSIONNO, @P_SEMESTERNO,@P_BRANCHNO,@P_STATUS", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlstatus.SelectedValue) + "");
    //    if (dsCERT1 != null && dsCERT1.Tables.Count > 0 && dsCERT1.Tables[0].Rows.Count > 0)
    //    {
    //        lvltransapprov.DataSource = dsCERT1;
    //        lvltransapprov.DataBind();
    //    }
    //    else
    //    {
    //        objCommon.DisplayMessage(this, "No Record Found", this.Page);
    //        lvltransapprov.DataSource = null;
    //        lvltransapprov.DataBind();
    //    }

    //}

    protected void lnkrollno_Click(object sender, EventArgs e)
    {
        LinkButton lnkrollno = sender as LinkButton;
        int IDNO = Convert.ToInt32(lnkrollno.CommandName);
        int semesterno = Convert.ToInt32(ddlSemester.SelectedValue);

        Session["semesterre_exam"] = semesterno;
        Session["IDNONEW"] = IDNO;
        Session["Sessionno"] = Convert.ToInt32(ddlSession.SelectedValue);

        if (ddlexamtype.SelectedValue == "1")
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "location.href='InternshipApplication.aspx';", true);
            //test
           // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/EXAMINATION/StudExamTransDetails.aspx?pageno=2739');", true);

            //Live
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/EXAMINATION/StudExamTransDetails.aspx?pageno=2739');", true);
           
        }
        if (ddlexamtype.SelectedValue == "2")
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "location.href='InternshipApplication.aspx';", true);
            //test
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/EXAMINATION/Re_ExamRegistration.aspx?pageno=2833');", true);

            //Live
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/EXAMINATION/Re_ExamRegistration.aspx?pageno=2801');", true);
           
        }
        if (ddlexamtype.SelectedValue == "3")
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "location.href='InternshipApplication.aspx';", true);
            //test
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/RevaluationRegistrationByStudent.aspx?pageno=1086');", true);

            //Live
          ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/RevaluationRegistrationByStudent.aspx?pageno=2800');", true);
           
        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlexamtype.SelectedValue == "1")
            {
                GetTransStudData();
            }
            if (ddlexamtype.SelectedValue == "2")
            {
                GetReExamDetails();
            }
            if (ddlexamtype.SelectedValue == "3")
            {
                GetRevalExamDetails();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl.ToString());
    }

    protected void btnExReport_Click(object sender, EventArgs e)
    {
        try
        {
            StudentCampExcel();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Activity_type_reports.btnExReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void StudentCampExcel()
    {
        DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_TRANSACTION_STUDENT_COUNT", "@P_SESSIONNO,@P_BRANCHNO, @P_SEMESTERNO", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataGrid dg = new DataGrid();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string attachment = "attachment; filename=" + "ExamTransactionStudentCount.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/" + "ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();

            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                return;
            }

        }
        else
        {
            objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
            return;
        }
    }

    protected void btnExremainlist_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.DynamicSPCall_Select("PKG_ACD_GET_TRANS_NOT_SUBMIT_STUD_LIST", "@P_SESSIONNO,@P_BRANCHNO, @P_SEMESTERNO,@P_ORGID", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ddlBranch.SelectedValue) + "," + Convert.ToInt32(ddlSemester.SelectedValue) + "," + Convert.ToInt32(Session["OrgId"].ToString()) + "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataGrid dg = new DataGrid();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string attachment = "attachment; filename=" + "RemainingExamTransactionStudentCount.xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/" + "ms-excel";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    dg.DataSource = ds.Tables[0];
                    dg.DataBind();
                    dg.HeaderStyle.Font.Bold = true;
                    dg.RenderControl(htw);
                    Response.Write(sw.ToString());
                    Response.End();

                }
                else
                {
                    objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                    return;
                }

            }
            else
            {
                objCommon.DisplayMessage(this, "No Data Found for this Selection", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_Activity_type_reports.btnExremainlist_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //protected void lnkTransDoc_Click(object sender, EventArgs e)
    //{
    //    string Url = string.Empty;
    //    string directoryPath = string.Empty;
    //    try
    //    {
    //        string blob_ConStr = string.Empty;
    //        string blob_ContainerName = string.Empty;
    //        blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();

    //        if (ddlexamtype.SelectedValue == "1")
    //        {          
    //            blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEXAM"].ToString();
    //        }
    //        if (ddlexamtype.SelectedValue == "2")
    //        {
    //            blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReExam"].ToString();
    //        }
    //        if (ddlexamtype.SelectedValue == "3")
    //        {
    //            blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReVal"].ToString();
    //        }

    //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
    //        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
    //        string directoryName = "~/DownloadImg" + "/";
    //        directoryPath = Server.MapPath(directoryName);

    //        if (!Directory.Exists(directoryPath.ToString()))
    //        {

    //            Directory.CreateDirectory(directoryPath.ToString());
    //        }
    //        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
    //        string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
    //        var ImageName = img;
    //        if (img == null || img == "")
    //        {

    //            // objCommon.DisplayMessage(this, "Image not Found...", this);
    //            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
    //            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    //            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //            embed += "</object>";
    //            //ltEmbed.Text = "Image Not Found....!";

    //        }
    //        else
    //        {

    //            DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

    //            var blob = blobContainer.GetBlockBlobReference(ImageName);

    //            string filePath = directoryPath + "\\" + ImageName;


    //            if ((System.IO.File.Exists(filePath)))
    //            {
    //                System.IO.File.Delete(filePath);
    //            }

    //            blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


    //            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
    //            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
    //            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
    //            embed += "</object>";
    //            //ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

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

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //return;
        ObjE.file_name = lnkTransDoc.ToolTip;
        ObjE.file_path = "Blob Storage";
        int ret = 0;
        int count = 0;

        foreach (ListViewDataItem lvitem in lvltransapprov.Items)
        {


            if (ddlstatus.SelectedIndex > 0)
            {
                HiddenField hdfidno = lvitem.FindControl("hdfidno") as HiddenField;
                HiddenField Transno = lvitem.FindControl("Transno") as HiddenField;
                HiddenField Transamt = lvitem.FindControl("Transamt") as HiddenField;
                HiddenField TransDate = lvitem.FindControl("TransDate") as HiddenField;
                HiddenField SEMNO = lvitem.FindControl("SEMNO") as HiddenField;
                CheckBox chkBox = lvitem.FindControl("chkstatus") as CheckBox;

                int idno = 0;
                int sessiono = 0;
                int orgid = 0;
                int approvedby = 0;
                int Approvedstatus = 0;
                string Remark = "";
                if (chkBox.Checked == true)
                {
                    if (ddlexamtype.SelectedValue == "1")
                    {
                        idno = int.Parse(hdfidno.Value);
                        sessiono = Convert.ToInt32(ddlSession.SelectedValue);
                        orgid = Convert.ToInt32(Session["OrgId"]);
                        approvedby = Convert.ToInt32(Session["userno"].ToString());
                        Approvedstatus = Convert.ToInt32(ddlstatus.SelectedValue);

                        if (ddlstatus.SelectedIndex == 1)
                        {
                            Remark = "Pending";
                        }
                        else if (ddlstatus.SelectedIndex == 2)
                        {
                            Remark = "Approved";
                        }
                        else if (ddlstatus.SelectedIndex == 3)
                        {
                            Remark = "Rejected";
                        }
                    }
                    else
                    {
                        
                        ObjE.Idno = int.Parse(hdfidno.Value);
                        ObjE.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                        ObjE.Transaction_no = Transno.Value;
                        ObjE.trans_amt = (Transamt.Value.Trim() == "" || Transamt.Value.Trim() == string.Empty) ? 0 : Convert.ToDecimal(Transamt.Value.Trim());
                        //if (!TransDate.Value.Trim().Equals(string.Empty)) ObjE.Transaction_date = Convert.ToDateTime(TransDate.Value.Trim());
                        ObjE.Transaction_date = (TransDate.Value.Trim().Equals(string.Empty) || TransDate.Value.Trim() == string.Empty) ? Convert.ToDateTime(DateTime.Now) : Convert.ToDateTime(TransDate.Value.Trim());
                        ObjE.OrgId = Convert.ToInt32(Session["OrgId"]);
                        ObjE.Approvedby = Convert.ToInt32(Session["userno"].ToString());
                        ObjE.Approvedstatus = Convert.ToInt32(ddlstatus.SelectedValue);
                        ObjE.SemesterNo = Convert.ToInt32(SEMNO.Value);

                        if (ddlstatus.SelectedIndex == 1)
                        {
                            ObjE.Remark = "Pending";
                        }
                        else if (ddlstatus.SelectedIndex == 2)
                        {
                            ObjE.Remark = "Approved";
                        }
                        else if (ddlstatus.SelectedIndex == 3)
                        {
                            ObjE.Remark = "Rejected";
                        }
                    }

                    int ua_type = Convert.ToInt32(Session["usertype"]);

                    //HiddenField hdfsession = lvitem.FindControl("hdfsession") as HiddenField;
                    if (chkBox.Checked == true)
                    {
                        count++;
                        if (ddlexamtype.SelectedValue == "1")
                        {
                            ret = objExamController.AddExamTransactionDetails_Admin(idno, sessiono, Approvedstatus, approvedby, Remark.ToString(), orgid);
                        }
                        if (ddlexamtype.SelectedValue == "2")
                        {
                            ret = Convert.ToInt32(objSReg.AddReExamTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"]), ViewState["ipAddress"].ToString(),Convert.ToInt32(ddlSemester.SelectedValue)));
                        }
                        if (ddlexamtype.SelectedValue == "3")
                        {
                            ret = Convert.ToInt32(objSReg.AddRevalTransactionDetails(ObjE, Convert.ToInt32(Session["usertype"])));
                        }
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this, "Please select Status", this.Page);
                return;
            }

            //if (count == 0)
            //{
            //    objCommon.DisplayMessage(this, "Please Select atleast one student from list", this.Page);
            //    return;
            //}
        }
        if (ret > 0)
        {
            if (ddlexamtype.SelectedValue == "1")
            {
                objCommon.DisplayMessage(this, "Student Exam Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
                ddlstatus.ClearSelection();
                GetTransStudData();
                //return;
            }
            if (ddlexamtype.SelectedValue == "2")
            {
                objCommon.DisplayMessage(this, "Student Re Exam Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
                ddlstatus.ClearSelection();
                GetReExamDetails();
                //return;
            }
            if (ddlexamtype.SelectedValue == "3")
            {
                objCommon.DisplayMessage(this, "Student Revaluation Exam Transaction " + ddlstatus.SelectedItem.Text + " Sucessfully.", this);
                ddlstatus.ClearSelection();
                GetRevalExamDetails();
                // return;
            }
        }
        else
        {
            if (ddlexamtype.SelectedValue == "1")
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                GetTransStudData();
                return;
            }
            if (ddlexamtype.SelectedValue == "2")
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                GetReExamDetails();
                return;
            }
            if (ddlexamtype.SelectedValue == "3")
            {
                objCommon.DisplayMessage(this, "Something went wrong ..Please try again !", this);
                GetRevalExamDetails();
                return;
            }
        }
    }

    protected void ddlexamtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlexamtype.SelectedIndex > 0)
        {
            lvltransapprov.DataSource = null;
            lvltransapprov.DataBind();

            if (ddlexamtype.SelectedValue == "1")
            {
                objCommon.FillDropDownList(ddlSession, "ACD_EXAM_TRANSACTION_DETAILS T INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=T.SESSIONNO)", "DISTINCT T.SESSIONNO", "SESSION_NAME", "S.SESSIONNO > 0 AND S.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + "", "");
                ddlSession.Focus();
                btnExremainlist.Visible = true;
                btnExReport.Visible = true;
                btnreport.Visible = false;
            }
            else if (ddlexamtype.SelectedValue == "2")
            {
                objCommon.FillDropDownList(ddlSession, "ACD_REEXAM_REGISTERED_AND_TRANSACTION_DETAILS T INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=T.SESSIONNO)", "DISTINCT T.SESSIONNO", "SESSION_NAME", "S.SESSIONNO > 0 AND S.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + "", "");
                ddlSession.Focus();
                btnExremainlist.Visible = false;
                btnExReport.Visible = false;
                btnreport.Visible = true;
            }
            else if (ddlexamtype.SelectedValue == "3")
            {
                objCommon.FillDropDownList(ddlSession, "ACD_REVAL_RESULT T INNER JOIN ACD_SESSION_MASTER S ON(S.SESSIONNO=T.SESSIONNO)", "DISTINCT T.SESSIONNO", "SESSION_NAME", "S.SESSIONNO > 0 AND S.ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + "", "");
                ddlSession.Focus();
                btnExremainlist.Visible = false;
                btnExReport.Visible = false;
                btnreport.Visible = true;
            }
            else
            {
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND EXAMTYPE=1 AND IS_ACTIVE=1 AND ORGANIZATIONID=" + Convert.ToInt32(Session["OrgId"]) + "", "SESSIONNO DESC");
                ddlSession.Focus();
                btnExremainlist.Visible = false;
                btnExReport.Visible = false;
                btnreport.Visible = false;
            }
        }
        else
        {
            ddlSession.SelectedIndex = 0;
            ddlexamtype.Focus();
            btnExremainlist.Visible = false;
            btnExReport.Visible = false;
            btnreport.Visible = false;
            objCommon.DisplayMessage(this.Page, "Please Select Exam Type", this.Page);
            return;
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView GV = new GridView();
            DataSet ds = null;
            if (ddlexamtype.SelectedValue == "2")
            {
                string sp_procedure = "PKG_ACD_GET_COURSE_FOR_REEXAM_OVERALL_REPORT";
                //string sp_parameters = "@P_SESSIONNO";
                //string sp_callValues = "" + (ddlSession.SelectedValue) + "";
                string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO";     //   add semesmeter filter on dt 12-11-2022
                string sp_callValues = "" + (ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "";          
                ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            }
            if (ddlexamtype.SelectedValue == "3")
            {
                string sp_procedure = "PKG_ACD_GET_COURSE_FOR_REVALUATION_OVERALL_REPORT";
               // string sp_parameters = "@P_SESSIONNO";         
                //  string sp_callValues = "" + (ddlSession.SelectedValue) + "";
                string sp_parameters = "@P_SESSIONNO,@P_SEMESTERNO";               //  add semesmeter filter on dt 12-11-2022
                string sp_callValues = "" + (ddlSession.SelectedValue) + "," + ddlSemester.SelectedValue + "";
                ds = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            }

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables != null)
            {
                GV.DataSource = ds;
                GV.DataBind();
                string Attachment = string.Empty;
                if (ddlexamtype.SelectedValue == "2")
                {
                     Attachment = "Attachment ; filename=ReExamStatusReport.xls";
                }
                if (ddlexamtype.SelectedValue == "3")
                {
                    Attachment = "Attachment ; filename=RevalExamStatusReport.xls";
                }
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GV.HeaderStyle.Font.Bold = true;
                GV.HeaderStyle.Font.Name = "Times New Roman";
                GV.RowStyle.Font.Name = "Times New Roman";
                GV.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                objCommon.DisplayMessage(this, "No Record Found", this.Page);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "MarksEntryNotDone.aspx.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkTransDoc_Click(object sender, ImageClickEventArgs e)
    {
        
        if (ddlexamtype.SelectedValue == "1")
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;
            try
            {
                string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameEXAM"].ToString();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/DownloadImg" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
                var ImageName = img;
                if (img == null || img == "")
                {

                    // objCommon.DisplayMessage(this, "Image not Found...", this);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = "Image Not Found....!";

                }
                else
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var blob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                }
            }
            catch (Exception ex)
            {

            }
        }

        if (ddlexamtype.SelectedValue == "2")
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;
            try
            {
                string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReExam"].ToString();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/DownloadImg" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
                var ImageName = img;
                if (img == null || img == "")
                {

                    // objCommon.DisplayMessage(this, "Image not Found...", this);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = "Image Not Found....!";

                }
                else
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var blob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                }
            }
            catch (Exception ex)
            {

            }
        }

        if (ddlexamtype.SelectedValue == "3")
        {
            string Url = string.Empty;
            string directoryPath = string.Empty;
            try
            {
                string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerNameReVal"].ToString();

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                string directoryName = "~/DownloadImg" + "/";
                directoryPath = Server.MapPath(directoryName);

                if (!Directory.Exists(directoryPath.ToString()))
                {

                    Directory.CreateDirectory(directoryPath.ToString());
                }
                CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
                string img = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
                var ImageName = img;
                if (img == null || img == "")
                {

                    // objCommon.DisplayMessage(this, "Image not Found...", this);
                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"600px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = "Image Not Found....!";

                }
                else
                {

                    DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                    var blob = blobContainer.GetBlockBlobReference(ImageName);

                    string filePath = directoryPath + "\\" + ImageName;


                    if ((System.IO.File.Exists(filePath)))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    blob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"400px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/DownloadImg/" + ImageName));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
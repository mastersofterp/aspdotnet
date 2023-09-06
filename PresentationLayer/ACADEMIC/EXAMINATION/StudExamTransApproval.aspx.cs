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

public partial class ACADEMIC_EXAMINATION_StudExamTransApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    Exam ObjE = new Exam();
    ExamController objExamController = new ExamController();
    DynamicControllerAL AL = new DynamicControllerAL();
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
                    CheckPageAuthorization();
                    PopulerDropdown();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    CheckActivity();
                }
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
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0", "SESSIONNO DESC");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "DISTINCT SEMESTERNO", "SEMESTERNAME", "SEMESTERNO > 0", "SEMESTERNO");
        //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "DISTINCT BRANCHNO", "LONGNAME", "BRANCHNO > 0 AND ISNULL(ACTIVESTATUS,0)=1", "BRANCHNO");
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH  B INNER JOIN [DBO].[ACD_COLLEGE_DEGREE_BRANCH] DB ON(DB.BRANCHNO=B.BRANCHNO)", "DISTINCT B.BRANCHNO", "B.LONGNAME", "B.BRANCHNO > 0 AND ISNULL(B.ACTIVESTATUS,0)=1", "B.BRANCHNO");
    }

    private bool CheckActivity()
    {
        //DataSet dssession = objCommon.FillDropDown("SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO)", "SESSION_NO", "", "STARTED = 1 AND  SHOW_STATUS =1 AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%'", "SESSION_NO DESC");

        //ddlSession.SelectedValue = dssession.Tables[0].Rows[0]["SESSION_NO"].ToString();
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "SESSIONNO DESC");


        //DataSet DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()), "");
        //if (DS != null && DS.Tables.Count > 0)
        //{
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
            }
        }
        else
        {
            objCommon.DisplayMessage(this, "Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
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

    protected void lnkrollno_Click(object sender, EventArgs e)
    {
        LinkButton lnkrollno = sender as LinkButton;
        int IDNO = Convert.ToInt32(lnkrollno.CommandName);
        Session["IDNONEW"] = IDNO;


        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "redirect script", "location.href='InternshipApplication.aspx';", true);
        //live
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "ACADEMIC/EXAMINATION/StudExamTransDetails.aspx?pageno=2739');", true);
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('http://localhost:54190/PresentationLayer/ACADEMIC/EXAMINATION/StudExamTransDetails.aspx?pageno=2780');", true);


       // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('https://rcpittest.mastersofterp.in/ACADEMIC/EXAMINATION/StudExamTransDetails.aspx');", true);

        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('https://rcpit.mastersofterp.in/ACADEMIC/EXAMINATION/StudExamTransDetails.aspx');", true);
       
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            GetTransStudData();
        }
        catch(Exception ex)
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

    protected void lnkTransDoc_Click(object sender, EventArgs e)
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

}
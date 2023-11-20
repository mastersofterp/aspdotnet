using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
using IITMS.SQLServer.SQLDAL;
using System.IO;
public partial class HOSTEL_GATEPASS_HostelInOutRequests : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelInOutRequestsController ObjInOut = new HostelInOutRequestsController();
    HostelInOutReq ObjHReq = new HostelInOutReq();
    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
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
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                objCommon.FillDropDownList(ddlPurpose, "ACD_HOSTEL_PURPOSE_MASTER", "PURPOSE_NO", "PURPOSE_NAME", "ISACTIVE=1", "PURPOSE_NO");
                BindListView();
                //MoreDetails();
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion Page Events
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelInOutRequests.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelInOutRequests.aspx");
        }
    }
    private void BindListView()
    {
        try
        {
            DataSet ds = ObjInOut.GetAllRequestsBySearch(ObjHReq);
            lvRequests.DataSource = ds;
            lvRequests.DataBind();
            foreach (ListViewDataItem lv in lvRequests.Items)
            {
                HiddenField hdnStatus = lv.FindControl("HdnStatus") as HiddenField;
                HiddenField hdnFirstApproval = lv.FindControl("HdnFirstApproval") as HiddenField;
                DropDownList ddlFirstApproval = lv.FindControl("ddlparentapproval") as DropDownList;
                DropDownList ddlStatus = lv.FindControl("ddlStatus") as DropDownList;

                ddlFirstApproval.SelectedValue = Convert.ToString(hdnFirstApproval.Value);
                ddlStatus.SelectedValue = Convert.ToString(hdnStatus.Value);
                ddlFirstApproval.Enabled = false;
                ddlStatus.Enabled = false;

            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ObjHReq.Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtApplyDate.Text);
            ObjHReq.Purpose = ddlPurpose.SelectedValue;
            ObjHReq.Gatepassno = txtGatePassCode.Text;
            ObjHReq.Indate = string.IsNullOrEmpty(txtInDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtInDate.Text);
            ObjHReq.Outdate = string.IsNullOrEmpty(txtOutDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtOutDate.Text);
            ObjHReq.Status = ddlStatus.SelectedValue;
            DataSet ds = ObjInOut.GetAllRequestsBySearch(ObjHReq);
            lvRequests.DataSource = ds;
            lvRequests.DataBind();
            foreach (ListViewDataItem lv in lvRequests.Items)
            {
                HiddenField hdnStatus = lv.FindControl("HdnStatus") as HiddenField;
                HiddenField hdnFirstApproval = lv.FindControl("HdnFirstApproval") as HiddenField;
                DropDownList ddlFirstApproval = lv.FindControl("ddlparentapproval") as DropDownList;
                DropDownList DdlStatus = lv.FindControl("ddlStatus") as DropDownList;
                ddlFirstApproval.SelectedValue = Convert.ToString(hdnFirstApproval.Value);
                ddlStatus.SelectedValue = Convert.ToString(hdnStatus.Value);
                ddlFirstApproval.Enabled = false;
                DdlStatus.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.btnSearch_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    [WebMethod]
    public static List<HostelInOutReq> GetData(HostelInOutReq ObjHReq)
    {
        HostelInOutRequestsController objHR = new HostelInOutRequestsController();
        int idno = Convert.ToInt32(ObjHReq.Idno);
        int hgpid = Convert.ToInt32(ObjHReq.Hgpid);

        SqlDataReader sdr = objHR.GetMoreDetails(idno, hgpid);
        List<HostelInOutReq> ListData = new List<HostelInOutReq>();

        while (sdr.Read())
        {
            ListData.Add(new HostelInOutReq
            {
                Applydate = Convert.ToDateTime(sdr["APPLY_DATE"]),
                Remarks = sdr["REMARKS"].ToString(),
                Infromto = sdr["INFORM_TO"].ToString(),
                Firstapprovername = sdr["FIRST_APPROVAL_NAME"].ToString(),
                Firstapprovaldesignation = sdr["FIRST_APPROVAL_DESIGNATION"].ToString(),
                Firstapprovaldate =string.IsNullOrEmpty(sdr["FIRST_APPROVAL_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(sdr["FIRST_APPROVAL_DATE"]),
                Firstrapprovalstatus = sdr["FIRST_APPROVAL_STATUS"].ToString(),
                Secondapprovername = sdr["SECOND_APPROVAL_NAME"].ToString(),
                Secondapprovaldedignation = sdr["SECOND_APPROVAL_DESIGNATION"].ToString(),
                Secondapprovalstatus = sdr["SECOND_APPROVAL_STATUS"].ToString(),
                Secondapprovaldate =string.IsNullOrEmpty(sdr["SECOND_APPROVAL_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(sdr["SECOND_APPROVAL_DATE"]),
                Thirdapprovarname = sdr["THIRD_APPROVAL_NAME"].ToString(),
                Thirdapprovaldate = string.IsNullOrEmpty(sdr["THIRD_APPROVAL_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(sdr["THIRD_APPROVAL_DATE"]),
                Thirdapprovaldesignation = sdr["THIRD_APPROVAL_DESIGNATION"].ToString(),
                Thirdapprovalstatus = sdr["THIRD_APPROVAL_STATUS"].ToString(),
                Fourthapprovaldate = string.IsNullOrEmpty(sdr["FOURTH_APPROVAL_DATE"].ToString()) ? (DateTime?)null : Convert.ToDateTime(sdr["FOURTH_APPROVAL_DATE"]),
                FourthApprovalDesignation = sdr["FOURTH_APPROVAL_DESIGNATION"].ToString(),
                Fourthapprovalstatus = sdr["FOURTH_APPROVAL_STATUS"].ToString(),
                Fourthapprovarname = sdr["FOURTH_APPROVAL_NAME"].ToString(),

            });
        }
        return ListData;
    }

    [WebMethod]
    public static List<HostelInOutReq> GetInMate(int idno)
    {
        HostelInOutRequestsController objHR = new HostelInOutRequestsController();
        SqlDataReader sdr = objHR.GetInMateDetails(idno);
        List<HostelInOutReq> ListData = new List<HostelInOutReq>();

        while (sdr.Read())
        {
            ListData.Add(new HostelInOutReq
            {
                Studentname = sdr["STUDNAME"].ToString(),
                Studentemail = sdr["EMAILID"].ToString(),
                Studentmobile = sdr["STUDENTMOBILE"].ToString(),
                Fathername = sdr["FATHERNAME"].ToString(),
                Fatheremail = sdr["FATHER_EMAIL"].ToString(),
                Fathermobile = sdr["FATHERMOBILE"].ToString(),
                MotherName = sdr["MOTHERNAME"].ToString(),
                Motheremail = sdr["MOTHER_EMAIL"].ToString(),
                Mothermobile = sdr["MOTHERMOBILE"].ToString()

            });
        }
        return ListData;
    }

    [WebMethod]
    public string SaveImage(HostelInOutReq data)
    {
        HostelInOutRequestsController objHR = new HostelInOutRequestsController();

        string filePath = Server.MapPath("~/LeaveUploadDocuments/");
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        string name = Path.GetFileName(data.UploadedfileName);
        byte[] bytes = Convert.FromBase64String(data.UploadedFile);
        File.WriteAllBytes(filePath + name, bytes);

        objHR.InsertAttachedDocuments(data);

        return "Data Saved Successfully.";
    }
    //protected void btnupload_Click(object sender, EventArgs e)
    //{
    //    HostelInOutRequestsController objHR = new HostelInOutRequestsController();
    //    ObjHReq.UploadedfileName = txtFilename.Text;
    //    if (fileInput.HasFile)
    //    {
    //        // Check file format
    //        string fileExtension = Path.GetExtension(fileInput.FileName).ToLower();
    //        if (IsAllowedExtension(fileExtension))
    //        {
    //            // Specify the folder where you want to save the files
    //            string uploadFolder = Server.MapPath("~/LeaveUploadDocuments/");
    //            if (!Directory.Exists(uploadFolder))
    //            {
    //                Directory.CreateDirectory(uploadFolder);
    //            }

    //            // Generate a unique file name (you can use a timestamp or a GUID)
    //            string fileName = Guid.NewGuid().ToString() + fileExtension;
    //            ObjHReq.UploadedFile = fileName;
    //            // Save the file
    //            fileInput.SaveAs(Path.Combine(uploadFolder, fileName));

    //            // Store the file path in the database 
    //            objHR.InsertAttachedDocuments(ObjHReq);

    //            // Optionally, display a success message
    //            Response.Write("File uploaded successfully.");
    //        }
    //        else
    //        {
    //            // Invalid file format
    //            Response.Write("Invalid file format. Allowed formats: .jpg, .jpeg, .png, .gif");
    //        }
    //    }
    //}
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
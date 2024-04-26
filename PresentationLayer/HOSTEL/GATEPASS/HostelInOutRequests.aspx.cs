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

    //below code added by Himanshu Tamrakar 05042024
    DateTime Fromdate = DateTime.Now.AddDays(-1);
    DateTime Todate = DateTime.Now.AddDays(7);

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
                //commented and added by Himanshu Tamrakar 06042024
                //BindListView():
                BindListView(Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")));

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
    private void BindListView(string Todate, string Fromdate)
    {
        try
        {
            string applydate = null;
            //string indate = null;
            //string outdate = null;
            DataSet ds = ObjInOut.GetAllRequestsBySearch(ObjHReq,applydate, Todate, Fromdate);
            lvRequests.DataSource = ds;
            lvRequests.DataBind();
            foreach (ListViewDataItem lv in lvRequests.Items)
            {
                HiddenField hdnStatus = lv.FindControl("HdnStatus") as HiddenField;
                HiddenField hdnFirstApproval = lv.FindControl("HdnFirstApproval") as HiddenField;
                DropDownList ddlFirstApproval = lv.FindControl("ddlparentapproval") as DropDownList;
                DropDownList ddlStatus = lv.FindControl("ddlStatus") as DropDownList;
                CheckBox chkselect = lv.FindControl("chkApprove") as CheckBox;
                if (hdnStatus.Value == "A")
                {
                    chkselect.Enabled = false;
                }
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
            if (Convert.ToDateTime(txtOutDate.Text) > Convert.ToDateTime(txtInDate.Text))
            {
                objCommon.DisplayMessage("To Date is Greater Than From date.", this);
                txtInDate.Text = string.Empty;
                txtOutDate.Text = string.Empty;
                return;
            }
            //string applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? null : txtApplyDate.Text;
            //ObjHReq.Purpose = ddlPurpose.SelectedValue;
            //ObjHReq.Gatepassno = txtGatePassCode.Text;
            //string indate = string.IsNullOrEmpty(txtInDate.Text) ? null : txtInDate.Text;
            //string outdate = string.IsNullOrEmpty(txtOutDate.Text) ? null : txtOutDate.Text;
            //ObjHReq.Status = ddlStatus.SelectedValue;
            string Applydate = string.IsNullOrEmpty(txtApplyDate.Text) ? null : DateTime.Parse(txtApplyDate.Text).ToString("yyyy-MM-dd");
            ObjHReq.Purpose = ddlPurpose.SelectedValue;
            ObjHReq.Gatepassno = txtGatePassCode.Text;
            string Fromdate = string.IsNullOrEmpty(txtOutDate.Text) ? null : DateTime.Parse(txtOutDate.Text).ToString("yyyy-MM-dd");
            string Todate = string.IsNullOrEmpty(txtInDate.Text) ? null : DateTime.Parse(txtInDate.Text).ToString("yyyy-MM-dd");
            ObjHReq.Status = string.IsNullOrEmpty(ddlStatus.SelectedValue) ? null : ddlStatus.SelectedValue;
            DataSet ds = ObjInOut.GetAllRequestsBySearch(ObjHReq, Applydate, Todate, Fromdate);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)   //Added By Himanshu tamrakar 22/11/2023
            {
                lvRequests.DataSource = ds;
                lvRequests.DataBind();
                foreach (ListViewDataItem lv in lvRequests.Items)
                {
                    HiddenField hdnStatus = lv.FindControl("HdnStatus") as HiddenField;
                    HiddenField hdnFirstApproval = lv.FindControl("HdnFirstApproval") as HiddenField;
                    DropDownList ddlFirstApproval = lv.FindControl("ddlparentapproval") as DropDownList;
                    DropDownList DdlStatus = lv.FindControl("ddlStatus") as DropDownList;
                    CheckBox chkselect = lv.FindControl("chkApprove") as CheckBox;
                    if (hdnStatus.Value == "A")
                    {
                        chkselect.Enabled = false;
                    }
                    ddlFirstApproval.SelectedValue = Convert.ToString(hdnFirstApproval.Value);
                    DdlStatus.SelectedValue = Convert.ToString(hdnStatus.Value);
                    ddlFirstApproval.Enabled = false;
                    DdlStatus.Enabled = false;

                }
            }
            else
            {
                objCommon.DisplayMessage("Records not Found.", this.Page);
                return;
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

    //[WebMethod]
    //public string SaveImage(HostelInOutReq data)
    //{
    //    HostelInOutRequestsController objHR = new HostelInOutRequestsController();

    //    string filePath = Server.MapPath("~/LeaveUploadDocuments/");
    //    if (!Directory.Exists(filePath))
    //    {
    //        Directory.CreateDirectory(filePath);
    //    }
    //    string name = Path.GetFileName(data.UploadedfileName);
    //    byte[] bytes = Convert.FromBase64String(data.UploadedFile);
    //    File.WriteAllBytes(filePath + name, bytes);

    //    objHR.InsertAttachedDocuments(data);

    //    return "Data Saved Successfully.";
    //}
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
    protected void chkApprove_CheckedChanged(object sender, EventArgs e)   //Added By Himanshu tamrakar On date 21-11-2023
    {
        try
        {
            int i=0;
            foreach (ListViewDataItem lv in lvRequests.Items)
            {
                HiddenField hdnStatus = lv.FindControl("HdnStatus") as HiddenField;
                HiddenField hdnFirstApproval = lv.FindControl("HdnFirstApproval") as HiddenField;
                DropDownList ddlFirstApproval = lv.FindControl("ddlparentapproval") as DropDownList;
                DropDownList DdlStatus = lv.FindControl("ddlStatus") as DropDownList;
                CheckBox chkselect = lv.FindControl("chkApprove") as CheckBox;
                if (chkselect.Checked)
                {
                    ddlFirstApproval.Enabled = true;
                    i++;
                }
                else
                {
                    ddlFirstApproval.Enabled = false;
                }
            }
            if (i > 0)
            {
                btnParentSubmit.Visible = true;
            }
            else
            {
                btnParentSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.chkApprove_CheckedChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnParentSubmit_Click(object sender, EventArgs e) //Added By Himanshu tamrakar on date 21-11-2023
    {
        try
        {
            int i=0;
            foreach (ListViewDataItem lv in lvRequests.Items)
            {
                CheckBox chkselect = lv.FindControl("chkApprove") as CheckBox;
                if (chkselect.Checked)
                {
                    HiddenField hdnhgpid = lv.FindControl("hdnhgpid") as HiddenField;
                    DropDownList DdlPAStatus = lv.FindControl("ddlparentapproval") as DropDownList;
                    
                    i=ObjInOut.ChangeParentApprovalStatus(Convert.ToInt32(hdnhgpid.Value), Convert.ToChar(DdlPAStatus.SelectedValue));
                    i++;
                }
            }
            if (i >= 2)
            {
                objCommon.DisplayMessage("Records Updated Sucessfully.", this.Page);
                //commented and added by Himanshu Tamrakar 06042024
                //BindListView():
                BindListView(Convert.ToString(DateTime.Parse(Convert.ToString(Todate)).ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Parse(Convert.ToString(Fromdate)).ToString("yyyy-MM-dd")));


            }
            else
            {
                objCommon.DisplayMessage("Please Select Atleast One Record.", this.Page);
            }
        }
        catch(Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.btnParentSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
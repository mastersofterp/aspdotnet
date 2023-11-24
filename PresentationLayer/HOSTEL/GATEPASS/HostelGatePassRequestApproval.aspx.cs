using HostelBusinessLogicLayer.BusinessEntities.Hostel;
using HostelBusinessLogicLayer.BusinessLogic.Hostel;
using IITMS;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HOSTEL_GATEPASS_HostelGatePassRequestApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HostelGatePassRequestApprovalController Hgp = new HostelGatePassRequestApprovalController();
    HostelGatePassRequestApproval ObjHgp = new HostelGatePassRequestApproval();

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

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HostelGatePassRequestApproval.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HostelGatePassRequestApproval.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = null;
            if (Convert.ToInt32(Session["usertype"]) == 1)
            {
                ds = Hgp.GetAllRequests(0);
            }
            else
            {
                ds = Hgp.GetAllRequests(Convert.ToInt32(Session["userno"]));
            }
            lvReq.DataSource = ds;
            lvReq.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn = (Button)sender;
            int hgpid = Convert.ToInt32(btn.CommandArgument);
            DataSet ds = null;
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            ds = Hgp.ShowStudentRequestDetails(hgpid, Convert.ToInt32(Session["userno"]), OrganizationId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivShowRequestDetails.Visible = true;
                DivShowRequest.Visible = false;
                lblRegno.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                lblSessionName.Text = ds.Tables[0].Rows[0]["SESSION_NAME"].ToString();
                lblHostel.Text = ds.Tables[0].Rows[0]["HOSTELNAME"].ToString();
                lblRoomName.Text = ds.Tables[0].Rows[0]["ROOM_NO"].ToString();
                lblGender.Text = ds.Tables[0].Rows[0]["GENDERNAME"].ToString();
                lblSemester.Text = ds.Tables[0].Rows[0]["SEMESTER"].ToString();
                lblStudentType.Text = ds.Tables[0].Rows[0]["STUDENT_TYPE"].ToString();
                lblPassingpath.Text = ds.Tables[0].Rows[0]["APPROVAL_PASSING_PATH"].ToString();
                lblRemark.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                hdnidno.Value = ds.Tables[0].Rows[0]["IDNO"].ToString();
                hdnhgpid.Value = ds.Tables[0].Rows[0]["HGP_ID"].ToString();
                lblapplydate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["APPLY_DATE"].ToString()).ToString("dd-MMM-yyyy");
                hdnAttachmenturl.Value = ds.Tables[0].Rows[0]["UPLOAD_DOCUMENT"].ToString();
                lblApprover.Text = ds.Tables[0].Rows[0]["APPROVER"].ToString();
                lvShowApprovalStatus.DataSource = ds;
                lvShowApprovalStatus.DataBind();

                //if (Convert.ToInt32(Session["usertype"]) == 14 || Convert.ToInt32(Session["usertype"]) == 1) //Commented By Himanshu Tamrakar 23/11/2023
                //{
                //    liFileAttach.Visible = true;
                //}
                //else
                //{
                //    liFileAttach.Visible = false;
                //}
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.btnShow_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int Idno = Convert.ToInt32(hdnidno.Value);
            int Hgpid = Convert.ToInt32(hdnhgpid.Value);
            int OrganizationId = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
            string status = ddlStatus.SelectedValue;
            string fileName = Path.GetFileName(FileAttach.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLower(); // Convert extension to lowercase
            int len = 0;
            int getPos = 0;
            string fileUrl = string.Empty;
            string f_status = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "FINAL_STATUS", "DIRECT_APPROVAL_UANO IS NOT NULL AND HGP_ID=" + Hgpid);  //Condition Added By Himanshu tamrakar 22/11/2023
            string f_status1 = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "FINAL_STATUS", "HGP_ID=" + Hgpid);  //Condition Added By Himanshu tamrakar 22/11/2023

            if (f_status == "A")
            {
                objCommon.DisplayMessage(this, "Gate Pass Already Approved By Admin.", this);
                return;
            }
            else if (f_status1 == "A")
            {
                objCommon.DisplayMessage(this, "Gate Pass Generated.You Can Not Modify Status.", this);
                return;
            }
            //if (Convert.ToInt32(Session["usertype"]) == 14 || Convert.ToInt32(Session["usertype"]) == 1)    //commented  By Himanshu Tamrakar 23/11/2023
            //{
            if (FileAttach.HasFile)
            {
                // Check if the file extension is allowed
                if (fileExtension == ".jpg" || fileExtension == ".pdf" || fileExtension == ".png")
                {
                    long fileSize = FileAttach.PostedFile.ContentLength;

                    // Check if the file size is less than 500KB (500 * 1024 bytes)
                    if (fileSize <= 500 * 1024)
                    {
                        // Define the folder path where you want to save the uploaded files
                        string filePath = Server.MapPath("GATEPASSATTCHMENT/" + System.Guid.NewGuid() + fileName); // Change the path as needed
                        //string filePath = Path.Combine(uploadFolder, fileName);

                        // Save the file to the specified folder
                        FileAttach.SaveAs(filePath);
                        getPos = filePath.LastIndexOf("\\");
                        len = filePath.Length;
                        string getPath = filePath.Substring(getPos, len - getPos);
                        string pathToStore = getPath.Remove(0, 1);
                        // Store the file URL in your database (you should replace this with your actual database logic)
                        fileUrl = "GATEPASSATTCHMENT/" + pathToStore;
                    }
                    else
                    {
                        objCommon.ConfirmMessage(this, "File size exceeds the limit (500KB). Please choose a smaller file.", this);
                        return;
                    }
                }
                else
                {
                    objCommon.ConfirmMessage(this, "Only .jpg ,.png and .pdf files are allowed.", this);
                    return;
                }
            }
            //commented  By Himanshu Tamrakar 23/11/2023
            //    else
            //    {
            //        if (Convert.ToInt32(Session["usertype"]) == 14)
            //        {
            //            fileUrl = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT", "HGP_ID=" + Hgpid);
            //            if (fileUrl == string.Empty)
            //            {
            //                objCommon.ConfirmMessage(this, "Please select a file to upload.", this);
            //                return;
            //            }
            //        }
            //        else
            //        {

            //        }
            //    }
            //}
            //else
            //{
            //    fileUrl = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT", "HGP_ID=" + Hgpid);
            //    fileName = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT_NAME", "HGP_ID=" + Hgpid);
            //}
            if (string.IsNullOrEmpty(fileUrl)) //Added By Himanshu Tamrakar 23/11/2023
            {
                fileUrl = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT", "HGP_ID=" + Hgpid);
                fileName = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT_NAME", "HGP_ID=" + Hgpid);
            }
            int output = Hgp.ApproveGatepass(Idno, fileUrl, fileName, Hgpid, status, OrganizationId, Convert.ToInt32(Session["userno"]));
            if (output == 1)
            {
                objCommon.ConfirmMessage(this, "Record Saved SuccessFully.", this);
            }
            else if (output == 2)
            {
                objCommon.ConfirmMessage(this, "Please Wait for First Approvar forword", this);
            }
            else if (output == 3)
            {
                objCommon.ConfirmMessage(this, "Please Wait for First and Second Approvar forword", this);
            }
            else if (output == 4)
            {
                objCommon.ConfirmMessage(this, "Please Wait for First second and Third Approvar forword", this);
            }
            else if (output == 5)
            {
                objCommon.ConfirmMessage(this, "Record not found for this approval.", this);
            }

            DataSet ds = null;

            ds = Hgp.ShowStudentRequestDetails(Hgpid, Convert.ToInt32(Session["userno"]), OrganizationId);

            lvShowApprovalStatus.DataSource = ds;
            lvShowApprovalStatus.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_GATEPASS_HostelInOutRequests.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowAttachment_Click(object sender, EventArgs e)
    {
        // string imageUrl = hdnAttachmenturl.Value;
        string imageUrl = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS", "UPLOAD_DOCUMENT", "HGP_ID=" + hdnhgpid.Value);
        if (!string.IsNullOrEmpty(imageUrl))
        {
            // Use Process.Start to open the file in the default application
            System.Diagnostics.Process.Start(Server.MapPath(imageUrl));
        }
        else
        {
            objCommon.ConfirmMessage(this, "File not Found.", this);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.BindListView();   //Added By Himanshu tamrakar 22-11-2023
        DivShowRequestDetails.Visible = false;
        DivShowRequest.Visible = true;
    }
}
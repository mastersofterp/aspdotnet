using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using System.IO;
using System;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure.Storage;

using System.Net.NetworkInformation;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Cryptography;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using Newtonsoft.Json;
using EASendMail;
using System.Text;
using System.Net.Mime;



public partial class Manage_Tickets : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int RaiseTicketID;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

            }
           // pnlPassport.Visible = false;
           // ChkDepartment.Visible = false;
            //Panel1.Visible = false;
            BindListView();
            BindDropdown();

            ViewState["Mode"] = "add";
           
       //     binddash();
            divstatus.Visible = true;
        }
        divstatus.Visible = true;
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AchievementMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AchievementMaster.aspx");
        }
    }




    private void BindDropdown()
    {
        //Amol sawarkar 10-03-2022

        objCommon.FillDropDownList(ddlCategory, "QM_RequestCategory C inner join QM_UserAllocation A on(C.QMRequestCategoryID = A.QMRequestTypeID) ", "QMRequestCategoryID", "QMRequestCategoryName", "InchargeID =" + Session["userno"].ToString(), "QMRequestCategoryName Asc");
        //objCommon.FillDropDownList(ddlTeam, "user_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE <>2", "UA_FULLNAME Asc");
        objCommon.FillDropDownList(ddlTeam, "dbo.Split((select Stuff(( SELECT ', ' + A.memberid FROM QM_UserAllocation A where  InchargeID =" + Session["userno"].ToString() + " FOR XML PATH('')), 1, 2, '') AS memberid ),',') as member inner join User_Acc U on member.value =u.ua_no", "distinct value", "UA_FULLNAME", string.Empty, string.Empty);
    }

    // edit status fatch code 
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        ManageTickets objMT = new ManageTickets();
        ManageTicketsController objRTC = new ManageTicketsController();
        try
        {
            LinkButton btnView = sender as LinkButton;
            ViewState["Mode"] = "edit"; 
            ViewState["RaiseTicketID"] = int.Parse(btnView.CommandArgument);
            //Clear();
            DataSet ds = objRTC.GetDataBYID(Convert.ToInt32(ViewState["RaiseTicketID"]));

            LblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
            LblStudName.Text = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
            LbCurrYear.Text = ds.Tables[0].Rows[0]["YEARNAME"].ToString();
            LbSemester.Text = ds.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
            LbBranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
            LbDegName.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            lblrequesttype.Text = ds.Tables[0].Rows[0]["QMRequestTypeName"].ToString();
            lblcatgory.Text = ds.Tables[0].Rows[0]["QMRequestCategoryName"].ToString();
            lblsubcatgory.Text = ds.Tables[0].Rows[0]["QMRequestSubCategoryName"].ToString();
            Label3.Text = ds.Tables[0].Rows[0]["TicketDescription"].ToString();
            Labcredate.Text = ds.Tables[0].Rows[0]["CreationDate"].ToString();
            if (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "True")
            {
                Emergency.Visible = true;
                Label1.Text = ds.Tables[0].Rows[0]["EmergencyServiceAmount"].ToString();
            }
            else
            {
                Emergency.Visible = false;
                amount.Visible = true;
                Label1.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
            }
            if (ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "True")
            {
                amount.Visible = true;
                Label1.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
            }
            else
            {
                amount.Visible = true;
                Label1.Text = ds.Tables[0].Rows[0]["EmergencyServiceAmount"].ToString();

            }
            //10-01-2023
            if (ds.Tables[0].Rows[0]["IsStudentRemark"].ToString() == "True")
            {
                //pnlPassport.Visible = true;
                //chkStudRemark.Checked = true;
                txtReqInfo.Text = ds.Tables[0].Rows[0]["InfoReq"].ToString();
                txtReqInfo.Text = string.Empty;   //a\\
                
            }
            else
            {
                pnlPassport.Visible = false;
                chkStudRemark.Checked = false;
                txtReqInfo.Text = string.Empty;
            }
            objCommon.FillDropDownList(ddlDepartmentM, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
            objCommon.FillDropDownList(ddlCategoryM, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "", "QMRequestCategoryName Asc");
            objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "", "QMRequestSubCategoryName Asc");
            objCommon.FillDropDownList(ddlRequestTypeM, "QM_RequestType ", "QMRequestTypeID", "QMRequestTypeName", "", "QMRequestTypeName Asc");

            if (ds.Tables[0].Rows[0]["IsDeptChange"].ToString() == "True")
            {
               
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["NewDeptId"].ToString()) > 0)
                {
                    ddlDepartmentM.SelectedValue = ds.Tables[0].Rows[0]["NewDeptId"].ToString();
                }
                else
                {
                    ddlDepartmentM.SelectedIndex= 0;
                }

               
                //ChkDepartment.Checked = true;
                //Panel1.Visible = true;
                  ddlDepartmentM.SelectedIndex = 0;
               // ddlRequestTypeM.SelectedValue = ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString();
               // ddlCategoryM.SelectedValue = ds.Tables[0].Rows[0]["QMRequestCategoryID"].ToString();
                //ddlSubCategoryM.SelectedValue = ds.Tables[0].Rows[0]["QMRequestSubCategoryID"].ToString();
                   
            }
            else
            {
                ChkDepartment.Checked = false;
                Panel1.Visible = false;
                ddlDepartmentM.SelectedIndex = 0;
            }

          

          
           string inchare = objCommon.LookUp("QM_UserAllocation", "distinct(InchargeID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + "and  QMRequestTypeID=" + ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString() + "");
           //string inchare = objCommon.LookUp("QM_UserAllocation", "distinct(InchargeID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + "and  QMDepartmentID=" + ds.Tables[0].Rows[0]["QMDepartmentID"].ToString() + "");
            //int T = Convert.ToInt32();

            if (inchare == Session["userno"].ToString())
            {
               //objCommon.FillDropDownList(ddlReAssign, "dbo.Split((select memberid from QM_UserAllocation where QMRequestTypeID =" + ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString() + " and InchargeID= " + Session["userno"].ToString() + "),',') as member inner join User_Acc U on member.value =u.ua_no", "value", "UA_FULLNAME", string.Empty, string.Empty);
               objCommon.FillDropDownList(ddlReAssign, "dbo.Split((select memberid from QM_UserAllocation where QMRequestTypeID =" + ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString() + " ),',') as member inner join User_Acc U on member.value =u.ua_no", "value", "UA_FULLNAME", "u.ua_no not in("+Convert.ToInt32(Session["userno"])+")", string.Empty);
               //l// objCommon.FillDropDownList(ddlReAssign, "dbo.Split((select memberid from QM_UserAllocation where QMDepartmentID =" + ds.Tables[0].Rows[0]["QMDepartmentID"].ToString() + " ),',') as member inner join User_Acc U on member.value =u.ua_no", "value", "UA_FULLNAME", "u.ua_no not in(" + Convert.ToInt32(Session["userno"]) + ")", string.Empty);
            }
            else
            {

                DataSet dp = objCommon.FillDropDown("dbo.Split((select memberid from QM_UserAllocation where QMRequestTypeID =25 ),',') as member inner join User_Acc U on member.value =u.ua_no", "value", "UA_FULLNAME ", "", "");
                ddlReAssign.DataSource = dp;
                ddlReAssign.DataTextField = "UA_FULLNAME";
                ddlReAssign.DataValueField = "value";
                ddlReAssign.DataBind();  
               // objCommon.FillDropDownList(ddlReAssign, "(select value, UA_FULLNAME (dbo.Split((select memberid from QM_UserAllocation where QMRequestTypeID =" + ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString() ),',') as member inner join User_Acc U on member.value =u.ua_no))a", "a.value", "a.UA_FULLNAME", "a.value=" + Session["userno"].ToString() + "", string.Empty);
            }
           
            
            // add 3 dropdown bind 29-12-2022
            //objCommon.FillDropDownList(ddlDepartmentM, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");

           // objCommon.FillDropDownList(ddlCategoryM, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "", "QMRequestCategoryName Asc");
          //  objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "", "QMRequestSubCategoryName Asc");
           // objCommon.FillDropDownList(ddlRequestTypeM, "QM_RequestType ", "QMRequestTypeID", "QMRequestTypeName", "", "QMRequestTypeName Asc");
         //   objCommon.FillDropDownList(ddlRequestTypeM, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //veiw.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }


    private void BindListView()
    {
        int CntOpen = 0, CntInprocess = 0, CntSum = 0,CntClose=0;
        ManageTicketsController objRTC = new ManageTicketsController();
        try
        {

            string inchare = objCommon.LookUp("QM_UserAllocation", "distinct(InchargeID)", "InchargeID=" + Convert.ToInt32(Session["userno"]));

            //int T = Convert.ToInt32();

            if (inchare == Session["userno"].ToString())
            {
                DataSet ds = objRTC.GETDATA(Convert.ToInt32(Session["userno"]), 'I');
                //DataSet ds = objRTC.GETDATA(Convert.ToInt32(Session["userno"]));
                lvticket.DataSource = ds.Tables[0];
                lvticket.DataBind();
               
                for (int i = 0; i < lvticket.Items.Count; i++)
                {
                    Label lblStatus = lvticket.Items[i].FindControl("lablstatus") as Label;
                    if (ds.Tables[0].Rows[i]["Status"].ToString() == "P")
                    {
                        lblStatus.Text = "OPEN";
                        lblStatus.CssClass = "badge badge-danger";
                        CntOpen = CntOpen + 1;

                    }
                    else if (ds.Tables[0].Rows[i]["Status"].ToString() == "F")
                    {
                        lblStatus.Text = "PROCESSING";
                        lblStatus.CssClass = "badge badge-warning";
                        CntInprocess = CntInprocess + 1; 
                    }
                    else
                    {
                        lblStatus.Text = "RESOLVED";
                        lblStatus.CssClass = "badge badge-success";
                        CntClose = CntClose + 1;
                    }
                }
            }
            else
            {
                DataSet ds = objRTC.GETDATA(Convert.ToInt32(Session["userno"]), 'M');
                //DataSet ds = objRTC.GETDATA(Convert.ToInt32(Session["userno"]));
                lvticket.DataSource = ds.Tables[0];
                lvticket.DataBind();

                for (int i = 0; i < lvticket.Items.Count; i++)
                {
                    Label lblStatus = lvticket.Items[i].FindControl("lablstatus") as Label;
                    if (ds.Tables[0].Rows[i]["Status"].ToString() == "P")
                    {
                        lblStatus.Text = "OPEN";
                        lblStatus.CssClass = "badge badge-danger";
                        CntOpen = CntOpen + 1;

                    }
                    else if (ds.Tables[0].Rows[i]["Status"].ToString() == "F")
                    {
                        lblStatus.Text = "PROCESSING";
                        lblStatus.CssClass = "badge badge-warning";
                        CntInprocess = CntInprocess + 1; 
                    }
                    else
                    {
                        lblStatus.Text = "RESOLVED";
                        lblStatus.CssClass = "badge badge-success";
                        CntClose = CntClose + 1;
                    }
                }
            }

         //   CntSum = CntOpen + CntInprocess;
            CntSum = CntOpen +CntInprocess + CntClose;
            lblOpen.Text = CntOpen.ToString();
            lblInprocess.Text = CntInprocess.ToString();
            lblTotal.Text = CntSum.ToString();
            lblClose.Text = CntClose.ToString();

        }
        catch (Exception ex)
        {

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ManageTickets objMT = new ManageTickets();
        ManageTicketsController objMTC = new ManageTicketsController();
        try
        {

            string filetext = string.Empty;
            if (ViewState["QMRaiseTicketID"] == null)
            {

                int newid = Convert.ToInt32(objCommon.LookUp("QM_TICKETDETAILS", "ISNULL(MAX(QMRaiseTicketID),0)", ""));
                ViewState["QMTicketDetailID"] = newid + 1;
            }
            //string path = "", filenames = "", docnos = "", docnames = "", existsfile = ""; int Count = 0;
            //path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            if (fuManageTicket.HasFiles)
            {

                byte[] imgData;
                if (fuManageTicket.HasFiles)
                {
                    ////if (fuRaiseTicket.HasFile)
                    ////{
                    if (!fuManageTicket.PostedFile.ContentLength.Equals(string.Empty) || fuManageTicket.PostedFile.ContentLength != null)
                    {
                        int fileSize = fuManageTicket.PostedFile.ContentLength;

                        int KB = fileSize / 1024;
                        if (KB >= 500)
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);

                            return;
                        }

                        string ext = System.IO.Path.GetExtension(fuManageTicket.FileName).ToLower();
                        //if (ext == ".pdf" || ext == ".jpeg" || ext == ".jpg")
                        //{

                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage("Please Upload PDF file only", this.Page);
                        //    return;
                        //}

                        if (fuManageTicket.FileName.ToString().Length > 50)
                        {
                            objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                            return;
                        }


                        imgData = objCommon.GetImageData(fuManageTicket);
                        filetext = imgData.ToString();
                        string filename_Certificate = Path.GetFileName(fuManageTicket.PostedFile.FileName);
                        //string Ext = Path.GetExtension(fuFile.FileName);

                        filetext = (ViewState["QMRaiseTicketID"]) + "_QM_Manage_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["QMRaiseTicketID"]) + "_QM_Manage_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fuManageTicket, imgData);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }

                    }

                    else if (!fuManageTicket.HasFile && lblfile.Text != string.Empty)
                    {
                        filetext = lblfile.Text;
                    }


                    objMT.Filename = filetext;


                // objRT.Attachment = FileUpload1.FileName;
                //HttpPostedFile FileSize = FileUpload1.PostedFile;
                //string ext = System.IO.Path.GetExtension(FileUpload1.FileName);

                //if (FileSize.ContentLength <= 1000000)
                //{
                //    Count++;
                //    existsfile = path + Convert.ToInt32(Session["idno"]) + "_" + FileUpload1.FileName;
                //    FileInfo file = new FileInfo(existsfile);
                //    if (file.Exists)//check file exsit or not  
                //    {
                //        file.Delete();
                //    }
                //    int retval = Blob_Upload(blob_ConStr, blob_ContainerName, FileUpload1.FileName, FileUpload1);
                //    if (retval == 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                //        return;
                //    }
                //    //fuDocument.PostedFile.SaveAs(path + Path.GetFileName(Convert.ToInt32(Session["idno"]) + "_" + fuDocument.FileName));
                //    //filenames += Convert.ToInt32(Session["idno"]) + "_doc_" + docno.Text + ext + '$';
                //    //docnos += docno.Text + '$';
                //    //docnames += DocName.Text + '$';
                }

                objMT.QMRaiseTicketID = Convert.ToInt32(ViewState["RaiseTicketID"]);
                if (ddlReAssign.SelectedIndex > 0)
                {
                    objMT.PendingId = int.Parse(ddlReAssign.SelectedValue);
                }
                else
                {
                    objMT.PendingId =Convert.ToInt32(Session["userno"]);
                }
               
                objMT.Remark = txtResponse.Text;
                //
                objMT.IsStudentRemark = chkStudRemark.Checked ? true : false;
                objMT.RequiredInformation = txtReqInfo.Text;
                objMT.IsDeptChange=ChkDepartment.Checked ? true : false;

                if (ddlDepartmentM.SelectedIndex > 0)
                {
                    objMT.NewDeptId = Convert.ToInt32(ddlDepartmentM.SelectedValue);
                }
                else
                {
                    objMT.NewDeptId = 0;
                }

                //   objMT.Status = Convert.ToChar(ddlStatusView.SelectedValue);
                // added by amol 06/04/2022
                if (ddlStatusView.SelectedIndex > 0)
                {
                    objMT.Status = Convert.ToChar(ddlStatusView.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Status...');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#veiw').modal('show')", true);
                    return;
                }

                 objMT.Filepath = "";
                //objMT.Filename = fuManageTicket.FileName;
                if (ddlDepartmentM.SelectedIndex > 0)
                {
                    CustomStatus cs = (CustomStatus)objMTC.UpdateManageTicketChangeDept(Convert.ToInt32(ViewState["RaiseTicketID"]), Convert.ToInt32(ddlRequestTypeM.SelectedValue), Convert.ToInt32(ddlCategoryM.SelectedValue), Convert.ToInt32(ddlSubCategoryM.SelectedValue));
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Update Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Data !", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objMTC.AddManageTicket(objMT);
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Data !", this.Page);
                    }
                }
                Clear();
                BindListView();
                binddash();
                //string msg = "hi";
                //string email = "anmolsawarkar@gmail.com";
                //string sub = "hello";
                //mailsendonstudent(msg, email, sub);
                
            }
            else
            {
                objMT.QMRaiseTicketID = Convert.ToInt32(ViewState["RaiseTicketID"]);
                if (ddlReAssign.SelectedIndex > 0)
                {
                    objMT.PendingId = int.Parse(ddlReAssign.SelectedValue);
                }
                else
                {
                    objMT.PendingId = Convert.ToInt32(Session["userno"]);
                }
                objMT.Remark = txtResponse.Text; 
                //addd two line
                objMT.IsStudentRemark = chkStudRemark.Checked ? true : false;
                objMT.RequiredInformation = txtReqInfo.Text;
                objMT.IsDeptChange = ChkDepartment.Checked ? true : false;

                if (ddlDepartmentM.SelectedIndex > 0)
                {
                    objMT.NewDeptId = Convert.ToInt32(ddlDepartmentM.SelectedValue);
                }
                else
                {
                    objMT.NewDeptId = 0;
                }

                //   objMT.Status = Convert.ToChar(ddlStatusView.SelectedValue);
                // added by amol 06/04/2022
                if (ddlStatusView.SelectedIndex > 0)
                {
                    objMT.Status = Convert.ToChar(ddlStatusView.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Select Status...');", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#veiw').modal('show')", true);
                    return;
                }
                objMT.Filepath = "";
                objMT.Filename = fuManageTicket.FileName;
                objMT.UserId = Convert.ToInt32(Session["userno"].ToString());
                if (ddlDepartmentM.SelectedIndex > 0)
                {
                    CustomStatus cs = (CustomStatus)objMTC.UpdateManageTicketChangeDept(Convert.ToInt32(ViewState["RaiseTicketID"]), Convert.ToInt32(ddlRequestTypeM.SelectedValue), Convert.ToInt32(ddlCategoryM.SelectedValue), Convert.ToInt32(ddlSubCategoryM.SelectedValue));
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Update Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Data !", this.Page);
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objMTC.AddManageTicket(objMT);
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this.UpdatePanel1, "Data Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Data !", this.Page);
                    }
                }
                Clear();
                BindListView();
                //string msg="hi";
                //string email = "anmolsawarkar@gmail.com";
                //string sub = "hello";
                //mailsendonstudent(msg, email, sub);
                //binddash();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #region BlogStorage
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
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
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
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
    #endregion BlogStorage


    //# region blob

    //private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    //{
    //    CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
    //    CloudBlobClient client = account.CreateCloudBlobClient();
    //    CloudBlobContainer container = client.GetContainerReference(ContainerName);
    //    return container;
    //}

    //public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    //{
    //    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
    //    int retval = 1;
    //    string Ext = Path.GetExtension(FU.FileName);
    //    string FileName = DocName;
    //    try
    //    {
    //        DeleteIFExits(FileName);
    //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //        container.CreateIfNotExists();
    //        container.SetPermissions(new BlobContainerPermissions
    //        {
    //            PublicAccess = BlobContainerPublicAccessType.Blob
    //        });

    //        CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
    //        cblob.UploadFromStream(FU.PostedFile.InputStream);
    //    }
    //    catch
    //    {
    //        retval = 0;
    //        return retval;
    //    }
    //    return retval;
    //}


    //public void DeleteIFExits(string FileName)
    //{
    //    CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
    //    string FN = Path.GetFileNameWithoutExtension(FileName);
    //    try
    //    {
    //        Parallel.ForEach(container.ListBlobs(FN, true), y =>
    //        {
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            ((CloudBlockBlob)y).DeleteIfExists();
    //        });
    //    }
    //    catch (Exception) { }
    //}


    //public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    //{
    //    CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
    //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //    var permission = container.GetPermissions();
    //    permission.PublicAccess = BlobContainerPublicAccessType.Container;
    //    container.SetPermissions(permission);

    //    DataTable dt = new DataTable();
    //    dt.TableName = "FilteredBolb";
    //    dt.Columns.Add("Name");
    //    dt.Columns.Add("Uri");

    //    //var blobList = container.ListBlobs(useFlatBlobListing: true);
    //    var blobList = container.ListBlobs(Id, true);
    //    foreach (var blob in blobList)
    //    {
    //        string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
    //        string y = x.Split('_')[0];
    //        dt.Rows.Add(x, blob.Uri);
    //    }
    //    return dt;
    //}

    //#endregion

    // view attachment code 
    protected void lnkdescription_Click1(object sender, EventArgs e)
    {
        try
        {

            //string FileName = objCommon.LookUp("QM_TICKETDETAILS", "Filename", "QMRaiseTicketID=" + ViewState["RaiseTicketID"]);
            //string FileName = objCommon.LookUp("QM_RaiseTicket", "Filename", "QMRaiseTicketID=" + RaiseTicketID);
            string FileName = objCommon.LookUp("QM_RaiseTicket", "Filename", "QMRaiseTicketID=" + ViewState["RaiseTicketID"]);
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = FileName;
            var ImageName = img;
            if (img != null || img != "")
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                //string filePath = directoryPath  + ImageName;
                string filePath = dtBlobPic.Rows[0]["Uri"].ToString();

                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                //Response.Redirect(filePath);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "show window",
                                        "shwwindow('" + filePath + "');", true);
                
                //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);


                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"700px\" height=\"400px\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
            }


        }
        catch (Exception ex)
        {

        }
    }



    private void Clear()
    {
        ddlReAssign.SelectedValue = "0";
        ddlStatusView.SelectedValue = "0";
        txtResponse.Text = string.Empty;
        txtReqInfo.Text = string.Empty;
        chkStudRemark.Checked = false;
        pnlPassport.Visible = false;
        //txtReqInfo.Visible = false;
       // ChkDepartment.Visible = false;
        //Panel1.Visible = false;
        ddlCategoryM.SelectedIndex = 0;
        ddlRequestTypeM.SelectedIndex = 0;
        ddlSubCategoryM.SelectedIndex = 0;
        ddlDepartmentM.SelectedIndex = 0;
   
       
    }

    private void binddash()
    {



        int total_tickets = 0;
        int open_tickets = 0;
        int Inprocess_tickets = 0;
        int Close_tickets = 0;
        //AND QMRT.payment_stat=1
        string inchare = objCommon.LookUp("QM_UserAllocation", "distinct(InchargeID)", "InchargeID=" + Convert.ToInt32(Session["userno"]));
        if (inchare == Session["userno"].ToString())
        {
        //    total_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and QMR.IsActive<>0  and EndTime = '9999-12-31 00:00:00.000' and Status <> 'C' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 "));
            //open_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND QMRT.TicketStatus=QTD.Status AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'P' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000'  AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1"));
            //Inprocess_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND QMRT.TicketStatus=QTD.Status AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'F' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1"));
        
            total_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and QMR.IsActive<>0  and EndTime = '9999-12-31 00:00:00.000' and Status <> 'C' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 "));
            open_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND QMRT.TicketStatus=QTD.Status inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'P' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000'  AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1"));
            Inprocess_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND QMRT.TicketStatus=QTD.Status inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'F' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1"));
            Close_tickets = 0; // As not showing close tickets in status
      
            
            //   Close_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND QMRT.TicketStatus=QTD.Status AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'C'  and  QMR.IsActive<>0  AND EndTime = '9999-12-31 00:00:00.000'  AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1"));
            //total_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + " and QMR.IsActive<>0  and EndTime = '9999-12-31 00:00:00.000'  AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 AND isnull(QMRT.payment_stat,0) =1 "));
            //open_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'P' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 AND isnull(QMRT.payment_stat,0) =1"));
            //Inprocess_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'F' and QMR.IsActive<>0 AND EndTime = '9999-12-31 00:00:00.000' AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 AND isnull(QMRT.payment_stat,0) =1"));
            //Close_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket QMRT  inner join  QM_TicketDetails QTD on QTD.QMRaiseTicketID = QMRT.QMRaiseTicketID AND ISNULL(QTD.payment_stat,0)=isnull(QMRT.payment_stat,0) inner join [dbo].[QM_RequestCategory] QMC on QMRT.QMRequestCategoryID=QMC.QMRequestCategoryID inner join [dbo].[QM_RequestType] QMR on QMR.QMRequestTypeID=QMC.QMRequestTypeID inner join [dbo].[QM_RequestSubCategory] QMSC on QMRT.QMRequestSubCategoryID=QMSC.QMRequestSubCategoryID inner join User_Acc U on QMRT.CreatedBy = U.UA_NO inner join User_Acc U1 on QTD.PendingID  = U1.UA_NO ", "count(QMRT.QMRaiseTicketID)", "InchargeID=" + Convert.ToInt32(Session["userno"]) + " and Status = 'C'  and  QMR.IsActive<>0  AND EndTime = '9999-12-31 00:00:00.000'  AND (case when QMRT.IsPaidService = 0 then 1 else  QMRT.payment_stat end)=1 AND isnull(QMRT.payment_stat,0) =1"));
           
        }
        else
        {
            total_tickets = int.Parse(objCommon.LookUp("QM_TicketDetails ", "count(QMRaiseTicketID)", "PendingID=" + Convert.ToInt32(Session["userno"]) + "AND EndTime = '9999-12-31 00:00:00.000'"));
            open_tickets = int.Parse(objCommon.LookUp("QM_TicketDetails ", "count(QMRaiseTicketID)", "Status = 'P' AND PendingID=" + Convert.ToInt32(Session["userno"]) + "AND EndTime = '9999-12-31 00:00:00.000'"));
            Inprocess_tickets = int.Parse(objCommon.LookUp("QM_TicketDetails ", "count(QMRaiseTicketID)", "Status = 'F' AND PendingID=" + Convert.ToInt32(Session["userno"]) + "AND EndTime = '9999-12-31 00:00:00.000'"));
            Close_tickets = int.Parse(objCommon.LookUp("QM_TicketDetails ", "count(QMRaiseTicketID)", "Status = 'C' AND PendingID=" + Convert.ToInt32(Session["userno"]) + "AND EndTime = '9999-12-31 00:00:00.000'"));
        }
        lblTotal.Text = total_tickets.ToString();
        lblClose.Text = Close_tickets.ToString();
        lblInprocess.Text = Inprocess_tickets.ToString();
        lblOpen.Text = open_tickets.ToString();



    }




    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory(ddlCategory.SelectedValue);
    }
    private void BindSubCategory(string Category)
    {
        objCommon.FillDropDownList(ddlSubCategory, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "QMRequestCategoryID=" + Category, "QMRequestSubCategoryName Asc");
    }
   
    
   

    protected void ddlReAssign_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlReAssign.SelectedIndex > 0)
        //{
        //    divstatus.Visible = false;
        //}
        //else
        //{
        //    divstatus.Visible = true;
        //}
        ddlStatusView.SelectedIndex = 0; //add 10-05-2022
    }
    protected void ddlStatusView_SelectedIndexChanged(object sender, EventArgs e)
    {
        // add for 06-05-2022 close validation 
        if (ddlReAssign.SelectedIndex > 0)
        {
            if (ddlStatusView.SelectedValue == "C")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('As it is re-assign then this status should not closed...');", true);
                ddlStatusView.SelectedValue = "0";
                return;
            }
        }
           
    }


    protected void linkButton3_Click(object sender, EventArgs e)           // redirect page code 
    {
        //LinkButton btnView = sender as LinkButton;

        //string studentId = btnView.CommandArgument;
        //string url = string.Empty;
        //string host = Request.Url.Host;
        //string scheme = Request.Url.Scheme;
        //int portno = Request.Url.Port;

        //if (host == "localhost")
        //{
        //    //url = scheme + "://" + host + ":" + portno + "/PresentationLayer/ACADEMIC/Student_Information.aspx?pageno=2980" + "&userno=" + studentId + "";

        //}
        //else
        //{
        //    //url = scheme + "://" + host + "/ACADEMIC/Student_Information.aspx?pageno=2980" + "&userno=" + studentId + "";

        //}


        //Redirect_New_Tab(url);
    }
    private void Redirect_New_Tab(string url_To_Open)
    {
        string URL = "window.open('" + url_To_Open + "', '_blank');";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", URL, true);
    }
    protected void linkButton4_Click(object sender, EventArgs e)
    {
        ManageTickets objMT = new ManageTickets();
        ManageTicketsController objRTC = new ManageTicketsController();
        try
        {
            LinkButton btnView = sender as LinkButton;
           // ViewState["RaiseTicketID"] = int.Parse(btnView.CommandArgument);

            ViewState["StudentID"] = int.Parse(btnView.CommandArgument);
            //Clear();
            DataSet ds1 = objRTC.GetDetailsById(Convert.ToInt32(ViewState["StudentID"]));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                LblStudName1.Text = ds1.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                LbCurrYear1.Text = ds1.Tables[0].Rows[0]["YEARNAME"].ToString();
                LbSemester1.Text = ds1.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                LbBranch1.Text = ds1.Tables[0].Rows[0]["BRANCH"].ToString();
                LbDegName1.Text = ds1.Tables[0].Rows[0]["DEGREENAME"].ToString();
            }

            DataSet ds = objRTC.GetStudentTicketHistory(Convert.ToInt32(ViewState["StudentID"]));
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvTickerHistory.DataSource = ds.Tables[0];
                lvTickerHistory.DataBind();
            }
            else
            {

                lvTickerHistory.DataSource = ds.Tables[0];
                lvTickerHistory.DataBind();

            }

            //Clear();
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenDetailView();", true);
    }
    protected void chkPass_CheckedChanged(object sender, EventArgs e)
    {
        pnlPassport.Visible = chkStudRemark.Checked;
    }



    // send mail to student 
    private void mailsendonstudent(string Message, string toEmailId, string subjects)
        // private void mailsendonstudent()
    {
        try
        {
           // string Message = "";
           // string toEmailId = "";
            //string subjects = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);


            MailMessage msg = new MailMessage();
            msg.To.Add(new System.Net.Mail.MailAddress(toEmailId));
            msg.From = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString());
            msg.Subject = subjects;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Message);
            msg.BodyEncoding = Encoding.UTF8;

            msg.Body = Convert.ToString(sb);
            msg.IsBodyHtml = true;

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL"].ToString(), dsconfig.Tables[0].Rows[0]["SLIIT_EMAIL_PWD"].ToString());

            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp-mail.outlook.com"; // "smtp.live.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.TargetName = "STARTTLS/smtp.office365.com";

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            client.EnableSsl = true;
            try
            {
                client.Send(msg);
                //lblText.Text = "Message Sent Succesfully";
            }
            catch (Exception ex)
            {
                //lblText.Text = ex.ToString();
            }
            finally { }

            string maskEmailID = string.Empty;
            string mailUser = string.Empty;
            string mailDomain = string.Empty;
            string mask = string.Empty;

            System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(toEmailId);
            mailUser = addr.User;
            mailDomain = addr.Host;

            //int maillength = mailUser.Length;

            //if (maillength == 2)
            //{
            //    mask = MaskString(mailUser, 1);
            //}
            //if (maillength == 3)
            //{
            //    mask = MaskString(mailUser, 2);
            //}
            //if (maillength == 4)
            //{
            //    mask = MaskString(mailUser, 2);
            //}
            //if (maillength > 4)
            //{
            //    mask = MaskString(mailUser, 4); // show last 4 character of user part -- ***defg
            //}
            //maskEmailID = mask + "@" + mailDomain;


          //  objCommon.DisplayMessage(this, "email sent successfully!", this.Page);
           // Console.WriteLine("email sent successfully!");
           // objCommon.DisplayMessage(this, "Check your email " + maskEmailID + " for the OTP and use the same to reset your password.", this.Page);
           // btnGetOtp.Visible = false; btn_SubmitModal.Visible = true; DivPassword.Visible = true; DivOTP.Visible = true;
            //objCommon.DisplayMessage(this, "Sorry , failed to send email !!!", this.Page);
        }
        catch (Exception ep)
        {
            objCommon.DisplayMessage(this, "Sorry , failed to send email !!!", this.Page);
            //Console.WriteLine("failed to send email with the following error:");
            // Console.WriteLine(ep.Message);
        }
        finally { }

    }

    private string MaskString(string s, int nos)
    {
        int NUM_ASTERISKS = nos;

        if (s.Length < NUM_ASTERISKS) return s;

        int asterisks = s.Length - NUM_ASTERISKS;
        string result = new string('*', asterisks);
        result += s.Substring(s.Length - NUM_ASTERISKS);
        return result;
    }



    protected void ddlRequestTypeM_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCategoryM(ddlRequestTypeM.SelectedValue);
    }
    protected void ddlCategoryM_SelectedIndexChanged(object sender, EventArgs e)
    {
         BindSubCategoryM(ddlCategoryM.SelectedValue);
    }
    protected void ddlSubCategoryM_SelectedIndexChanged(object sender, EventArgs e)
    {
       // objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory", "*", "", "QMRequestSubCategoryID =" + Convert.ToInt32(ddlSubCategoryM.SelectedValue), "");
       // BindSubCategoryM1(ddlSubCategoryM.SelectedValue);
    }
    private void BindCategoryM(string RequestType)
    {
        objCommon.FillDropDownList(ddlCategoryM, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "QMRequestTypeID=" + RequestType, "QMRequestCategoryName Asc");
    }
     private void BindSubCategoryM(string Category)
    {
        objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "QMRequestCategoryID=" + Category, "QMRequestSubCategoryName Asc");
    }

     private void BindSubCategoryM1(string SubCategory)
     {
        
        // objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory","*", "QMRequestSubCategoryID", "QMRequestCategoryID=" + SubCategory, "QMRequestSubCategoryName Asc");
         objCommon.FillDropDownList(ddlSubCategoryM, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "QMRequestCategoryID=" + SubCategory, "QMRequestSubCategoryName Asc");
     }

     protected void ChkDepartment_CheckedChanged(object sender, EventArgs e)
     {
         Panel1.Visible = ChkDepartment.Checked;
     }
     protected void ddlDepartmentM_SelectedIndexChanged(object sender, EventArgs e)
     {
         ddlCategoryM.SelectedIndex = 0;

         objCommon.FillDropDownList(ddlRequestTypeM, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive=" + 1 + "AND QMDepartmentID=" + ddlDepartmentM.SelectedValue, "QMRequestTypeName Asc");
     }
     protected void btnclear_Click(object sender, EventArgs e)
     {
         Clear();    
     }
     protected void btnCancel_Click(object sender, EventArgs e)
     {
         ddlRequestTypeM.SelectedIndex = 0;
         ddlCategoryM.SelectedIndex = 0;
         pnlPassport.Visible = false;
         Clear();    
     }
}


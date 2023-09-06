using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using System.IO;
using System.Text;
//using _NVP;
using System.Collections.Specialized;
using System.Web.UI.HtmlControls;
using Microsoft.WindowsAzure.Storage.Blob;
//using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

public partial class Raise_Ticket : System.Web.UI.Page
{
    Common objCommon= new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int RaiseTicketID;
    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName"].ToString();
    



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
            pnlPassport.Visible = false;
            BindListView();
            BindDropdown();
            binddash();
            //16-12-2022 add
            objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");
            ViewState["Mode"] = "add";
        }
        //else
        //{
        //    objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");
        //    //ddlCategory.Items.Clear();
        //   // ddlCategory.Items.Add(new ListItem("Please Select"));
        //   // ddlSubCategory.Items.Clear();
        //   // ddlSubCategory.Items.Add(new ListItem("Please Select"));

        //}
        
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


    protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCategory(ddlRequestType.SelectedValue);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory(ddlCategory.SelectedValue);
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = null;

   
       ds = objCommon.FillDropDown("QM_RequestSubCategory","*", "","QMRequestSubCategoryID ="+Convert.ToInt32(ddlSubCategory.SelectedValue),"");

       if (ddlSubCategory.SelectedValue != "0")
       {
           lblrequesttype.Visible = true;
           lblrequesttype.Text = ds.Tables[0].Rows[0]["GeneralInstruction"].ToString();

       
           if ((ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "True") && (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "True"))
           {
               //show paid amount
               //display OPT check box as unchecked

               chkYess.Visible = true;
               lblamount.Visible = true;
               txtAmount.Text = ds.Tables[0].Rows[0]["EmergencyServiceAmount"].ToString();
               txtAmount.Visible = true;
               chkYess.Checked = false;
               txtAmount.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
               btnSubmit.Visible = false;
               btnPaySubmit.Visible = true;

           }
           else if ((ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "False") && (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "False"))
           {
               //hide OPT check box.
               //hide amount text box
               lblamount.Visible = false;
               txtAmount.Visible = false;
               chkYess.Visible = false;
               chkYess.Enabled = false;
               chkYess.Checked = false;
               btnSubmit.Visible = true;
               btnSubmit.Enabled = true;
               btnPaySubmit.Visible = false;
           }
           else if ((ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "True") && (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "False"))
           {
               //display amount text box with paid amount
               //hide OPT check box
               lblamount.Visible = true;
               txtAmount.Visible = true;
            //   txtAmount.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
               btnSubmit.Visible = false;
               btnPaySubmit.Visible = true;
               chkYess.Visible = false;  // 05-05-2022 true then false
               chkYess.Enabled = false;  //05-05-2022 true then false
               if ((ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "True"))
               {
                  chkYess.Checked = false;
               }
               else
               {
                   chkYess.Checked = true;
               }

               if (chkYess.Checked == true)
               {
                   txtAmount.Visible = false;
                   txtAmount.Text = string.Empty;

               }
               else
               {
                   txtAmount.Visible = true;
                   txtAmount.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();

               }


           }
           else if ((ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "False") && (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "True"))
           {
               //initially hide amount text box
               //display OPT check box as unchecked
               //on checked of OPT, display amount text box with emergency amount
               //on uncheck of OPT, hide amount text box
               txtAmount.Visible = false;
               chkYess.Visible = true;
               chkYess.Enabled = true;
               chkYess.Checked = false;
               if ((ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "True"))
               {
                   chkYess.Checked = true;
               }
               else
               {
                   chkYess.Checked = false;
               }

               if (chkYess.Checked == true)
               {
                   txtAmount.Visible = true;
                   txtAmount.Text = ds.Tables[0].Rows[0]["EmergencyServiceAmount"].ToString();

               }
               else
               {
                   txtAmount.Visible = false;
                   txtAmount.Text = string.Empty;
               }
               btnSubmit.Visible = false;
               btnPaySubmit.Visible = true;


           }

           //if (ds.Tables[0].Rows[0]["IsEmergencyService"].ToString() == "True")
           //{
           //    chkYess.Visible = true;
           //    chkYess.Checked = true;//29
           //    lblamount.Visible = true;
           //    txtAmount.Visible = true;
           //    btnSubmit.Visible = false;
           //    btnPaySubmit.Visible = true;

           //}
           //else 
           //{
           //    chkYess.Visible = false;
           //    chkYess.Checked = false;//29
           //    lblamount.Visible = false;
           //    txtAmount.Visible = false;
           //    btnSubmit.Visible = true;
           //    btnPaySubmit.Visible = false;
           //}

           //if (ds.Tables[0].Rows[0]["IsPaidService"].ToString() == "True")
           //{
           //    chkYess.Visible = true;//29
           //    lblamount.Visible = true;
           //    txtAmount.Visible = true;
           //    txtAmount.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
           //    btnSubmit.Visible = false;
           //    btnPaySubmit.Visible = true;

           //}
           //else
           //{
           //    chkYess.Visible = false;//29
           //    lblamount.Visible = false;
           //    txtAmount.Visible = false;
           //    btnSubmit.Visible = true;
           //    btnPaySubmit.Visible = false;
           //}
       }
       else  //  30/04/2022add  plz select issue 
       {

           lblrequesttype.Visible = false;
           txtAmount.Visible = false;
           lblamount.Visible = false;
           chkYess.Visible = false;      
           chkYess.Checked = false;
           btnSubmit.Visible = false;
       }
    }

    private void BindDropdown()
    {
        objCommon.FillDropDownList(ddlDepartment, "QM_ServiceDepartment", "QMDepartmentID", "QMDepartmentName", "QMDepartmentID>0 AND IsActive=" + 1, "QMDepartmentName Asc");
        
    }
    private void BindCategory( string RequestType)
    {
        objCommon.FillDropDownList(ddlCategory, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "QMRequestTypeID=" + RequestType, "QMRequestCategoryName Asc");
    }
    private void BindSubCategory( string Category)
    {
        objCommon.FillDropDownList(ddlSubCategory, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "QMRequestCategoryID=" + Category, "QMRequestSubCategoryName Asc");
    }

    protected void chkYess_CheckedChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("QM_RequestSubCategory", "*", "", "QMRequestSubCategoryID =" + Convert.ToInt32(ddlSubCategory.SelectedValue), "");
        if (chkYess.Checked == true)
        {
            txtAmount.Text = ds.Tables[0].Rows[0]["EmergencyServiceAmount"].ToString();
          
        }
        else
        {   
            txtAmount.Text = ds.Tables[0].Rows[0]["PaidServiceAmount"].ToString();
           
        }
        // 03-05-2022
        if (Convert.ToDecimal(txtAmount.Text) == 0)
        {
            btnSubmit.Visible = true;
            btnPaySubmit.Visible = false;
        }
        else
        {
            btnSubmit.Visible = false;
            btnPaySubmit.Visible = true;
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
        //add 
       // btnSubmit.Enabled = false;


        string filetext = string.Empty;
        if (ViewState["QMRaiseTicketID"] == null)
        {

            int newid = Convert.ToInt32(objCommon.LookUp("QM_RaiseTicket", "ISNULL(MAX(QMRaiseTicketID),0)", ""));
            ViewState["QMRaiseTicketID"] = newid + 1;
        }

        try
        {
            //string path = "", filenames = "", docnos = "", docnames = "", existsfile = ""; int Count = 0;
            //path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            byte[] imgData;
            if (fuRaiseTicket.HasFiles)
            {
                ////if (fuRaiseTicket.HasFile)
                ////{
                if (!fuRaiseTicket.PostedFile.ContentLength.Equals(string.Empty) || fuRaiseTicket.PostedFile.ContentLength != null)
                {
                    int fileSize = fuRaiseTicket.PostedFile.ContentLength;

                    int KB = fileSize / 1024;
                    if (KB >= 500)
                    {
                        objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 1 MB.", this.Page);

                        return;
                    }

                    string ext = System.IO.Path.GetExtension(fuRaiseTicket.FileName).ToLower();
                    //if (ext == ".pdf" || ext == ".jpeg" || ext == ".jpg")
                    //{

                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage("Please Upload PDF file only", this.Page);
                    //    return;
                    //}

                    if (fuRaiseTicket.FileName.ToString().Length > 50)
                    {
                        objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                        return;
                    }


                    imgData = objCommon.GetImageData(fuRaiseTicket);
                    filetext = imgData.ToString();
                    string filename_Certificate = Path.GetFileName(fuRaiseTicket.PostedFile.FileName);
                    //string Ext = Path.GetExtension(fuFile.FileName);

                    filetext = (ViewState["QMRaiseTicketID"]) + "_QM_Raise_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                    int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["QMRaiseTicketID"]) + "_QM_Raise_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fuRaiseTicket, imgData);
                    if (retval == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                        return;
                    }

                }

                else if (!fuRaiseTicket.HasFile && lblfile.Text != string.Empty)
                {
                    filetext = lblfile.Text;
                }


                objRT.Filename = filetext;




                //// objRT.Attachment = FileUpload1.FileName;
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
                //}



                objRT.QMRequestTypeID = Convert.ToInt32(ddlRequestType.SelectedValue);
                objRT.QMRequestCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                objRT.QMRequestSubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                objRT.TicketStatus = 'P';
                objRT.TicketDescription = txtTicketDescription.Text.Trim();
                objRT.FeedBack = string.Empty;
                //objRT.Filepath = path;
                //objRT.Filename = FileUpload1.FileName;
                objRT.CreatedBy = Convert.ToInt32(Session["userno"]);

                if (chkYess.Visible == false && txtAmount.Visible == true)
                {

                    objRT.IsPaidService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsPaidService = false;
                    objRT.PaidServiceAmount = 0;
                    //13-04-2022
                    // objRT.PaidServiceAmount = double.Parse(txtAmount.Text);  
                }

                if (chkYess.Visible == true && chkYess.Checked == true)
                {
                    objRT.IsEmergencyService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsEmergencyService = false;
                    objRT.PaidServiceAmount = 0;
                    //13-04-2022
                    // objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }

                //
                //if (objRT.IsStudentRemark == true && objRT.IsStudentRemark == false)

                objRT.IsStudentRemark = chkStudRemark.Checked ? true : false;
                objRT.RequiredInformation = txtReqInfo.Text;


                if (ViewState["Mode"].Equals("edit"))
                {
                    int raisetktid = Convert.ToInt32(ViewState["RaiseTicketID"]);
                    CustomStatus cs = (CustomStatus)objRTC.updateRaiseTicketResponse(raisetktid, objRT);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Update Successfully", this.Page);
                        ViewState["Mode"] = "";
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objRTC.AddRaiseTicket(objRT, 1);
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {


                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Achievement Details!", this.Page);
                    }
                }
                Clear();
                BindListView();
                binddash();


            }
            else
            {

                objRT.QMRequestTypeID = Convert.ToInt32(ddlRequestType.SelectedValue);
                objRT.QMRequestCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                objRT.QMRequestSubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                objRT.TicketStatus = 'P';
                objRT.TicketDescription = txtTicketDescription.Text.Trim();
                objRT.FeedBack = string.Empty;
                objRT.Filepath = string.Empty;
                objRT.Filename = string.Empty;
                objRT.CreatedBy = Convert.ToInt32(Session["userno"]);

                if (chkYess.Visible == false && txtAmount.Visible == true)
                {
                    objRT.IsPaidService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsPaidService = false;
                    objRT.PaidServiceAmount = 0;
                    //// objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }

                if (chkYess.Visible == true && chkYess.Checked == true)
                {
                    objRT.IsEmergencyService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsEmergencyService = false;
                    objRT.PaidServiceAmount = 0;
                    // objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                }

                //
                objRT.IsStudentRemark = chkStudRemark.Checked ? true : false;
                objRT.RequiredInformation = txtReqInfo.Text;
                if (ViewState["Mode"].Equals("edit"))
                {
                    int raisetktid = Convert.ToInt32(ViewState["RaiseTicketID"]);
                    CustomStatus cs = (CustomStatus)objRTC.updateRaiseTicketResponse(raisetktid, objRT);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Update Successfully", this.Page);
                        ViewState["Mode"] = "";
                    }
                }
                else
                {
                    CustomStatus cs = (CustomStatus)objRTC.AddRaiseTicket(objRT, 1);
                    if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.RecordSaved))
                    {


                        objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);

                    }
                    else
                    {
                        objCommon.DisplayMessage(this.UpdatePanel1, "Error!!!", this.Page);
                    }
                }
                Clear();
                BindListView();
                binddash();
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

    private void Clear()
    {
        ddlDepartment.SelectedValue = "0";  
        //ddlCategory.SelectedValue = "0";
        //ddlSubCategory.SelectedValue = "0";
        //ddlRequestType.SelectedValue = "0";
        lblamount.Visible = false;
        chkYess.Visible = false;
        txtAmount.Visible = false;
        txtAmount.Text = string.Empty;
        txtSuggestions.Text = string.Empty;
        txtTicketDescription.Text = string.Empty;
        btnSubmit.Enabled = true;
        lblrequesttype.Text = string.Empty;
        chkYess.Checked = false;
    }

    //private void btnclear()
    //{
    //    Clear();
    //}

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();

        try
        {
            objRT.QMRaiseTicketID = RaiseTicketID;
            objRT.FeedBack = txtSuggestions.Text;
            objRT.FeedBackPoints = double.Parse(hdfFeedback.Value);


            CustomStatus cs = (CustomStatus)objRTC.AddRaiseTicket1(objRT);
            if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "FeedBack Already Exist", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordSaved))
            {


                objCommon.DisplayMessage(this.UpdatePanel1, "FeedBack Saved Successfully!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding FeedBack !", this.Page);
            }
            Clear();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
           
        }

    }

    private void BindListView()
    {
      
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
        try
        {
            DataSet ds = objRTC.GETDATA(Convert.ToInt32(Session["userno"]));
            lvticket.DataSource = ds.Tables[0];
            lvticket.DataBind();

            for (int i = 0; i < lvticket.Items.Count; i++)
            {
                Label lblStatus = lvticket.Items[i].FindControl("lablstatus") as Label;
                if (ds.Tables[0].Rows[i]["TicketStatus"].ToString() == "P")
                {
                    lblStatus.Text = "OPEN";
                    lblStatus.CssClass = "badge badge-danger";
                   
                }
                else if (ds.Tables[0].Rows[i]["TicketStatus"].ToString() == "F")
                {
                    lblStatus.Text = "PROCESSING";
                    lblStatus.CssClass = "badge badge-warning";
                   
                }
                else
                {
                    lblStatus.Text = "RESOLVED";
                    lblStatus.CssClass = "badge badge-success";
                   
                }
            }

        }
        catch (Exception ex)
        {

        }
    }


    // response select code 
    protected void lnkResponse_Click(object sender, EventArgs e)
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
       
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            LinkButton btnView = sender as LinkButton;

            RaiseTicketID = int.Parse(btnView.CommandArgument);
            Clear();
            btnSubmit.Enabled = true;
            divstudreq.Visible = true;
            DataSet ds = objRTC.GetDataBYID(RaiseTicketID);
            objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID=" + ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString() + " AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");
            ddlRequestType.SelectedValue = ds.Tables[0].Rows[0]["QMRequestTypeID"].ToString();
            ddlRequestType.Enabled = false;
            
            
            objCommon.FillDropDownList(ddlCategory, "QM_RequestCategory ", "QMRequestCategoryID", "QMRequestCategoryName", "QMRequestTypeID=" + ddlRequestType.SelectedValue, "QMRequestCategoryName Asc");
            ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["QMRequestCategoryID"].ToString();
            ddlCategory.Enabled = false;
       

            objCommon.FillDropDownList(ddlSubCategory, "QM_RequestSubCategory", "QMRequestSubCategoryID", "QMRequestSubCategoryName", "QMRequestCategoryID=" + ddlCategory.SelectedValue, "QMRequestSubCategoryName Asc");
            ddlSubCategory.SelectedValue = ds.Tables[0].Rows[0]["QMRequestSubCategoryID"].ToString();
            ddlSubCategory.Enabled = false;
        
            //Label3.Text = (ds.Tables[0].Rows[0]["TicketDescription"].ToString());
            //Label1.Text = objCommon.LookUp("QM_TicketDetails", "Remark", "QMRaiseTicketID=" + RaiseTicketID + " AND ENDTIME = '9999-12-31 00:00:00.000'");
            txtTicketDescription.Text = (ds.Tables[0].Rows[0]["TicketDescription"].ToString());
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsStudentRemark"]) == true)
            {
                lblstudreq.Text = (ds.Tables[0].Rows[0]["InfoReq"].ToString());

                //txtTicketDescription.Text = (ds.Tables[0].Rows[0]["TicketDescription"].ToString());
                txtTicketDescription.Enabled = true;               
                btnSubmit.Enabled = true;
               // btnSubmit.Visible = true;
                divstudreq.Visible = true;

            }
            else
            {
                // txtTicketDescription.Text = string.Empty;
                txtTicketDescription.Enabled = false;
                btnSubmit.Enabled = false;
               // btnSubmit.Visible = false;
               lblstudreq.Text = string.Empty;
               divstudreq.Visible = false;
            }
            //lblrequesttype1.Text = (ds.Tables[0].Rows[0]["QMRequestTypeName"].ToString());
            //lblcategory1.Text = (ds.Tables[0].Rows[0]["QMRequestCategoryName"].ToString());
            //Lblsubcategory1.Text = (ds.Tables[0].Rows[0]["QMRequestSubCategoryName"].ToString());
            //lblticketDes.Text = (ds.Tables[0].Rows[0]["TicketDescription"].ToString());
            lblrequesttype.Text = ds.Tables[0].Rows[0]["GeneralInstruction"].ToString();
           // lblrequesttype.Text = ds.Tables[0].Rows[0]["InfoReq"].ToString();
           
            ViewState["Mode"] = "edit";
            ViewState["RaiseTicketID"] = RaiseTicketID;
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
    protected void lnkFeedBack_Click(object sender, EventArgs e)
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
        try
        {
            LinkButton btnView = sender as LinkButton;

            RaiseTicketID = int.Parse(btnView.CommandArgument);
            Clear();
            DataSet ds = objRTC.GetDataBYID(RaiseTicketID);

            txtSuggestions.Text = (ds.Tables[0].Rows[0]["FeedBack"].ToString());
            txtSuggestions.MaxLength = 100;
            double val ;
            
            if (ds.Tables[0].Rows[0]["FeedbackPoints"].ToString() == string.Empty)
            {
                 val = 0.00;
            }
            else 
            {
                 val = double.Parse(ds.Tables[0].Rows[0]["FeedbackPoints"].ToString());
            }
            //double val = double.Parse(ds.Tables[0].Rows[0]["FeedbackPoints"].ToString());

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Src", "startvalue1('"+val+"');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);

            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "starvalue();", true);
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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive=" + 1 + "AND QMDepartmentID=" + ddlDepartment.SelectedValue, "QMRequestTypeName Asc");
    }


    #region Online Payment
    //protected void SendTransaction()   //Commited by sachin 01-04-2023
    //{
    //    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)768 | (System.Net.SecurityProtocolType)3072;
    //    String result = null;
    //    String gatewayCode = null;
    //    String response = null;

    //    // get the request form and make sure to UrlDecode each value in case special characters used
    //    NameValueCollection formdata = new NameValueCollection();
    //    //foreach (String key in Request.Form)
    //    //{
    //    //    formdata.Add(key, HttpUtility.UrlDecode(Request.Form[key]));
    //    //}

    //    Merchant merchant = new Merchant();

    //    // [Snippet] howToConfigureURL - start
    //    StringBuilder url = new StringBuilder();
    //    if (!merchant.GatewayHost.StartsWith("http"))
    //        url.Append("https://");
    //    url.Append(merchant.GatewayHost);
    //    //url.Append("/api/nvp/version/");
    //    //url.Append(merchant.Version);

    //    merchant.GatewayUrl = url.ToString();
    //    // [Snippet] howToConfigureURL - end

    //    Connection connection = new Connection(merchant);

    //    // [Snippet] howToConvertFormData -- start
    //    StringBuilder data = new StringBuilder();
    //    data.Append("merchant=" + merchant.MerchantId);
    //    data.Append("&apiUsername=" + merchant.Username);
    //    data.Append("&apiPassword=" + merchant.Password);

    //    // add each key and value in the form data
    //    formdata.Add("apiOperation", "CREATE_CHECKOUT_SESSION");
    //    //formdata.Add("apiUsername", "merchant.700182200072");
    //    //formdata.Add("apiPassword", "004dc01e2adfd7ec414ec6255a16e505");
    //    //formdata.Add("merchant", "700182200072");

    //    //formdata.Add("interaction.returnUrl", "http://localhost:55158/PresentationLayer/OnlineResponse.aspx");

    //    //string returnurl = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
    //    //formdata.Add("interaction.returnUrl", returnurl);
        
    //    //Local Link
    //    formdata.Add("interaction.returnUrl", "http://localhost:59566/PresentationLayer/QUERYMGT/QM_OnlineResponse.aspx");

    //    //Local Virtual Links
    //    //formdata.Add("interaction.returnUrl", "http://IITMSLP/SLIIT/QUERYMGT/QM_OnlineResponse.aspx");
        
    //    //Live Link
    //    //formdata.Add("interaction.returnUrl", "https://sims.sliit.lk/QUERYMGT/QM_OnlineResponse.aspx");

    //    //Test Link
    //    //formdata.Add("interaction.returnUrl", "https://erptest.sliit.lk/QUERYMGT/QM_OnlineResponse.aspx");

    //    //formdata.Add("interaction.operation", "PURCHASE");
    //    //formdata.Add("order.id", lblOrderID.Text);
    //    //formdata.Add("order.currency", "LKR");
    //    //formdata.Add("order.amount", hdfAmount.Value);
        
    //    formdata.Add("interaction.operation", "PURCHASE");
    //    formdata.Add("order.id", lblOrderID.Text);
    //    formdata.Add("order.currency", "LKR");
    //    formdata.Add("order.amount", hdfTotalAmount.Value);
    //    formdata.Add("order.description", Convert.ToString(Session["userno"]));
    //    //url.Append("/apiOperation/");
    //    //url.Append("CREATE_CHECKOUT_SESSION");
    //    //url.Append("/apiUsername/");
    //    //url.Append("merchant.700182200072");
    //    //url.Append("/apiPassword/");
    //    //url.Append("004dc01e2adfd7ec414ec6255a16e505");
    //    //url.Append("/merchant/");
    //    //url.Append("700182200072");
    //    //url.Append("interaction.operation");
    //    //url.Append("/PURCHASE/");
    //    //url.Append("order.id");
    //    //url.Append("123456789");
    //    //url.Append("/order.currency/");
    //    //url.Append("LKR");
    //    //url.Append("/order.amount/");
    //    //url.Append("1000");
    //    //url.Append("/order.description/");
    //    //url.Append("\"d2564\"");
    //    foreach (string key in formdata)
    //    {
    //        data.Append("&" + key.ToString() + "=" + HttpUtility.UrlEncode(formdata[key], System.Text.Encoding.GetEncoding("ISO-8859-1")));
    //    }
    //    // [Snippet] howToConvertFormData -- end

    //    response = connection.SendTransaction(data.ToString());

    //    // [Snippet] howToParseResponse - start
    //    NameValueCollection respValues = new NameValueCollection();
    //    if (response != null && response.Length > 0)
    //    {
    //        String[] responses = response.Split('&');
    //        foreach (String responseField in responses)
    //        {
    //            String[] field = responseField.Split('=');
    //            respValues.Add(field[0], HttpUtility.UrlDecode(field[1]));
    //        }
    //    }
    //    // [Snippet] howToParseResponse - end

    //    result = respValues["result"];

    //    // Form error string if error is triggered
    //    if (result != null && result.Equals("ERROR"))
    //    {
    //        String errorMessage = null;
    //        String errorCode = null;

    //        String failureExplanations = respValues["explanation"];
    //        String supportCode = respValues["supportCode"];

    //        if (failureExplanations != null)
    //        {
    //            errorMessage = failureExplanations;
    //        }
    //        else if (supportCode != null)
    //        {
    //            errorMessage = supportCode;
    //        }
    //        else
    //        {
    //            errorMessage = "Reason unspecified.";
    //        }

    //        String failureCode = respValues["failureCode"];
    //        if (failureCode != null)
    //        {
    //            errorCode = "Error (" + failureCode + ")";
    //        }
    //        else
    //        {
    //            errorCode = "Error (UNSPECIFIED)";
    //        }

    //        // now add the values to result fields in panels
    //        //lblErrorCode.Text = errorCode;
    //        //lblErrorMessage.Text = errorMessage;
    //        //pnlError.Visible = true;
    //    }

    //    // error or not display what response values can
    //    gatewayCode = respValues["response.gatewayCode"];
    //    if (gatewayCode == null)
    //    {
    //        gatewayCode = "Response not received.";
    //    }
    //    //lblGateWayCode.Text = gatewayCode;
    //    //lblResult.Text = result;

    //    // build table of NVP results and add to panel for results

    //    int shade = 0;
    //    foreach (String key in respValues)
    //    {

    //        if (key == "session.id")
    //        {
    //            Session["ERPPaymentSession"] = respValues[key];
    //        }

    //    }
    //}

    #endregion


    protected void btnPaySubmit_Click(object sender, EventArgs e)
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
        try
        {
            //string path = "", filenames = "", docnos = "", docnames = "", existsfile = ""; int Count = 0;
            //path = System.Configuration.ConfigurationManager.AppSettings["UploadDocPath"];
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            string filetext = string.Empty;
            if (ViewState["QMRaiseTicketID"] == null)
            {

                int newid = Convert.ToInt32(objCommon.LookUp("QM_RaiseTicket", "ISNULL(MAX(QMRaiseTicketID),0)", ""));
                ViewState["QMRaiseTicketID"] = newid + 1;
            }

            if (fupayment.HasFiles)
            {

                byte[] imgData;
                if (fupayment.HasFile)
                {
                    if (!fupayment.PostedFile.ContentLength.Equals(string.Empty) || fupayment.PostedFile.ContentLength != null)
                    {
                        int fileSize = fupayment.PostedFile.ContentLength;

                        int KB = fileSize / 1024;
                        if (KB >= 500)
                        {
                            objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);

                            return;
                        }

                        string ext = System.IO.Path.GetExtension(fupayment.FileName).ToLower();
                        if (ext == ".pdf" || ext == ".jpeg" || ext == ".jpg")
                        {

                        }
                        else
                        {
                            objCommon.DisplayMessage("Please Upload PDF file only", this.Page);
                            return;
                        }

                        if (fuRaiseTicket.FileName.ToString().Length > 50)
                        {
                            objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                            return;
                        }


                        imgData = objCommon.GetImageData(fupayment);
                        filetext = imgData.ToString();
                        string filename_Certificate = Path.GetFileName(fupayment.PostedFile.FileName);
                        //string Ext = Path.GetExtension(fuFile.FileName);

                        filetext = (ViewState["QMRaiseTicketID"]) + "_QM_Payment_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                        int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["QMRaiseTicketID"]) + "_QM_Payment_Ticket_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fupayment, imgData);
                        if (retval == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                            return;
                        }

                    }
                }
                else if (!fupayment.HasFile && lblfile.Text != string.Empty)
                {
                    filetext = lblfile.Text;
                }


                objRT.Filename = filetext;




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
                //}

            

                objRT.QMRequestTypeID = Convert.ToInt32(ddlRequestType.SelectedValue);
                objRT.QMRequestCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                objRT.QMRequestSubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                objRT.TicketStatus = 'P';
                objRT.TicketDescription = txtTicketDescription.Text.Trim();
                objRT.FeedBack = string.Empty;
                //objRT.Filepath = path;
                //objRT.Filename = FileUpload1.FileName;
                objRT.CreatedBy = Convert.ToInt32(Session["userno"]);

                if (chkYess.Visible == false && txtAmount.Visible == true)
                {
                    objRT.IsPaidService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                   // objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsPaidService = false;
                    objRT.PaidServiceAmount = 0;
                   // objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                  //  objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                }

                if (chkYess.Visible == true && chkYess.Checked == true)
                {
                    objRT.IsEmergencyService = true;
                    objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                   // objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                }
                else
                {
                    objRT.IsEmergencyService = false;
                    objRT.PaidServiceAmount = 0;
                    //objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                   // objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                }

                SubmitData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                CustomStatus cs = (CustomStatus)objRTC.AddRaiseTicket(objRT, 0);


                //if (cs.Equals(CustomStatus.DuplicateRecord))
                //{
                //    objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                //}
                //else if (cs.Equals(CustomStatus.RecordSaved))
                //{


                //    objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);
                //}
                //else
                //{
                //    objCommon.DisplayMessage(this.UpdatePanel1, "Error Adding Achievement Details!", this.Page);
                //}
                BindListView();
                binddash();
            }
            else
            {

                objRT.QMRequestTypeID = Convert.ToInt32(ddlRequestType.SelectedValue);
                objRT.QMRequestCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                objRT.QMRequestSubCategoryID = Convert.ToInt32(ddlSubCategory.SelectedValue);
                objRT.TicketStatus = 'P';
                objRT.TicketDescription = txtTicketDescription.Text.Trim();
                objRT.FeedBack = string.Empty;
                objRT.Filepath = string.Empty;
                objRT.CreatedBy = Convert.ToInt32(Session["userno"]);
                objRT.Filename = string.Empty;
                txtOrderid.Text = lblOrderID.Text;
                if (chkYess.Checked == false && txtAmount.Visible == true)
                {
                    if (chkYess.Checked == true)
                    {
                        objRT.IsEmergencyService = true;
                        //objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                        objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                        txtAmountPaid.Text = txtAmount.Text;
                        btnSubmit.Visible = false;
                        btnPaySubmit.Visible = true;
                    }
                    else
                    {
                        objRT.IsPaidService = true;
                        objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                       // objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                        txtAmountPaid.Text = txtAmount.Text;
                        btnSubmit.Visible = false;
                        btnPaySubmit.Visible = true;
                    }
                    
                }
                else
                {
                    if (chkYess.Checked == true)
                    {
                        objRT.IsEmergencyService = true;
                        objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                       // objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                        txtAmountPaid.Text = txtAmount.Text;
                        btnSubmit.Visible = false;
                        btnPaySubmit.Visible = true;
                    }
                    else
                    {
                        objRT.IsPaidService = false;
                        objRT.PaidServiceAmount = 0;
                        //13-04-2022
                        //objRT.PaidServiceAmount = double.Parse(txtAmount.Text);
                        //objRT.EmergencyServiceAmount = double.Parse(txtAmount.Text);
                        btnSubmit.Visible = true;
                        btnPaySubmit.Visible = false;
                    }
                    
                }
                SubmitData();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#raise-ticket-modal').modal('show')", false);
                CustomStatus cs = (CustomStatus)objRTC.AddRaiseTicket(objRT, 0);
                Session["ReferenceNo"] = objCommon.LookUp("QM_RaiseTicket", "max(ReferenceNo)", "QMRaiseTicketID=(select max(QMRaiseTicketID)from QM_RaiseTicket)");
                //if (cs.Equals(CustomStatus.DuplicateRecord))
                //{
                //    objCommon.DisplayMessage(this.UpdatePanel1, "Record Already Exist", this.Page);
                //}
                //else if (cs.Equals(CustomStatus.RecordSaved))
                //{


                //    objCommon.DisplayMessage(this.UpdatePanel1, "Record Saved Successfully!", this.Page);
                //}
                //else
                //{
                //    objCommon.DisplayMessage(this.UpdatePanel1, "Error!!!", this.Page);
                //}

            }
        
            BindListView();
            binddash();
            Clear();
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_AchievementMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void CreateCustomerRef()
    {
        Random rnd = new Random();
        int ir = rnd.Next(01, 10000);
        Random rnd1 = new Random();
        int ir1 = rnd1.Next(01, 99999);
        lblOrderID.Text = Convert.ToString(Convert.ToInt32(Session["USERNO"]) + Convert.ToString(ir1) + Convert.ToString(ir));
        Session["ORDERIDRESPONSE"] = lblOrderID.Text;
    }


    private void SubmitData()
    {
        RaiseTicket objRT = new RaiseTicket();
        RaiseTicketController objRTC = new RaiseTicketController();
        try
        {

            Random random = new Random();
            CreateCustomerRef();


            objRT.UserNo = Convert.ToInt32(Session["user"]);
            
            objRT.OrderID = lblOrderID.Text;
            objRT.Amount = Convert.ToDouble(txtAmount.Text);
            hdfAmount.Value = txtAmount.Text;
            objRT.TotalAmount = 0.0;
            objRT.FeeType = "Online";
            int PAYMENTTYPE = 2;

            // Additional Parameters
            objRT.TransDate = System.DateTime.Today;
            

            int result = 0;

            result = objRTC.SubmitFees(objRT, PAYMENTTYPE, 2 , Convert.ToString(Session["ReferenceNo"]));   //PAYMENTTYPE  FOR ONLINE , GATEWAYID 2 FOR ONLINE

            if (result > 0)
            {

                DataSet ds = null;
                ds = objRTC.GetOnlineTrasactionOnlineOrderID(Convert.ToString(Session["user"]), Convert.ToString(lblOrderID.Text));


                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (Convert.ToString(ds.Tables[0].Rows[0]["ORDER_ID"]) == lblOrderID.Text)
                    {
                        hdfServiceCharge.Value = Convert.ToString(ds.Tables[0].Rows[0]["SERVICE_CHARGE"]);
                        txtOrderid.Text = lblOrderID.Text;
                        txtAmountPaid.Text = hdfAmount.Value;
                        txtServiceCharge.Text = hdfServiceCharge.Value;
                        decimal text = Convert.ToDecimal(hdfAmount.Value) + Convert.ToDecimal(hdfServiceCharge.Value);
                        txtTotalPayAmount.Text = Convert.ToString(text);
                        hdfTotalAmount.Value = Convert.ToString(text);
                        //SendTransaction();      //Commited by sachin 01-04-2023

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#myModal33').modal('show')", true);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Something went wrong , Please try again !", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Failed To Done Online Payment.", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_OnlinePayment_StudentLogin.SubmitData-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


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





    protected void lnkresponse_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = objCommon.LookUp("QM_TicketDetails", "Filename", "QMRaiseTicketID=" + RaiseTicketID + " And ENDTIME = '9999-12-31 00:00:00.000'");

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
                //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "show window",
                                        "shwwindow('" + filePath + "');", true);

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
    protected void lnkdescription_Click(object sender, EventArgs e)
    {
        try
        {
           
            string FileName = objCommon.LookUp("QM_RaiseTicket", "Filename", "QMRaiseTicketID=" + RaiseTicketID);

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
                //Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "show window",
                                        "shwwindow('" + filePath + "');", true);

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



    private void binddash()
    {
        int total_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "payment_stat=1 AND CreatedBy =" + Convert.ToInt32(Session["userno"])));
        int open_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'P' AND payment_stat=1 AND CreatedBy =" + Convert.ToInt32(Session["userno"])));
        int Inprocess_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'F' AND payment_stat=1 AND CreatedBy =" + Convert.ToInt32(Session["userno"])));
        int Close_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'C' AND payment_stat=1 AND CreatedBy =" + Convert.ToInt32(Session["userno"])));


        //int total_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", " CreatedBy =" + Convert.ToInt32(Session["userno"])));
        //int open_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'P'  AND CreatedBy =" + Convert.ToInt32(Session["userno"])));
        //int Inprocess_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'F' AND CreatedBy =" + Convert.ToInt32(Session["userno"])));
        //int Close_tickets = int.Parse(objCommon.LookUp("QM_RaiseTicket ", "count(QMRaiseTicketID)", "TicketStatus = 'C' AND  CreatedBy =" + Convert.ToInt32(Session["userno"])));
        



        lblTotal.Text = total_tickets.ToString();
        lblClose.Text = Close_tickets.ToString();
        lblInprocess.Text = Inprocess_tickets.ToString();
        lblOpen.Text = open_tickets.ToString();
        
    }


    //26-04-2022 add amol sawarkar
    protected void btnclear1_Click(object sender, EventArgs e)
    {

        ddlDepartment.SelectedValue = "0";
        //ddlCategory.SelectedValue = "0";
        //ddlSubCategory.SelectedValue = "0";
        //ddlRequestType.SelectedValue = "0";
        objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Please Select"));
        ddlSubCategory.Items.Clear();
        ddlSubCategory.Items.Add(new ListItem("Please Select"));
        lblamount.Visible = false;
        chkYess.Visible = false;
        txtAmount.Visible = false;
        txtAmount.Text = string.Empty;
        txtSuggestions.Text = string.Empty;
        txtTicketDescription.Text = string.Empty;
        btnSubmit.Enabled = false;
        lblrequesttype.Text = string.Empty;
        chkYess.Checked = false;
        txtReqInfo.Text = string.Empty;
        chkStudRemark.Checked = false;
        pnlPassport.Visible = false;
       

    }
    protected void chkStudRemark_CheckedChanged(object sender, EventArgs e)
    {
        pnlPassport.Visible = chkStudRemark.Checked;
    }

    protected void btnRaiseTicket_Click(object sender, EventArgs e)
    {
        //chkStudRemark.Checked = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        pnlPassport.Visible = chkStudRemark.Checked;
        ddlRequestType.Enabled = true;
        ddlCategory.Enabled = true;
        ddlSubCategory.Enabled = true;
        txtTicketDescription.Enabled = true;
        divstudreq.Visible = false;
        objCommon.FillDropDownList(ddlRequestType, "QM_RequestType", "QMRequestTypeID", "QMRequestTypeName", "QMRequestTypeID>0 AND IsActive='" + 1 + "'", "QMRequestTypeName Asc");
        ddlCategory.Items.Clear();
        ddlCategory.Items.Add(new ListItem("Please Select"));
        ddlSubCategory.Items.Clear();
        ddlSubCategory.Items.Add(new ListItem("Please Select"));
        
    }

    //protected void lnkstudentremark_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
            
    //        int RaiseTicketID = Convert.ToInt32(lnkstudentremark.CommandArgument);
    //        if(RaiseTicketID>0)
    //        {
    //        string FileName = objCommon.LookUp("QM_TicketDetails", "isnull(InfoReq,'')InfoReq", "QMRaiseTicketID=" + RaiseTicketID);
    //        Label2.Text = FileName.ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
            
    //        throw ex;
    //    }
    //}




}
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHD ANNEXURE-B ELIGIBILITY                                            
// CREATION DATE : 14/1/2019                                                 
// CREATED BY    : Dipali Nanore                         
// MODIFIED DATE :                 
// ADDED BY      :                                  
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;


public partial class Academic_PhdThesisSupervisior : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string Filepath = string.Empty; string Filename = string.Empty;
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME", "ID > 0 ", "SRNO");
                ddlSearch.SelectedIndex = 0;
                ViewState["usertype"] = ua_type;
                if (ViewState["usertype"].ToString() == "2")
                {
                    objCommon.DisplayMessage("Student Not Eligible For Thesis Submission:", this.Page);
                    return;
                    //pnlId.Visible = false;
                    //ShowStudentDetails();
                    //ViewState["action"] = "edit";
                    // Btnsubmit.Visible = false;


                }
                if (ViewState["usertype"].ToString() == "1")
                {
                    pnlsearch.Visible = true;
                    dvgeneral.Visible = false;
                    //dvdetails.Visible = true;
                    divGeneralInfo.Visible = false;
                    pnldetails.Visible = false;
                    //dvstudentdetails.Visible = false;
                    dvuploadthesis.Visible = false;
                    dvbutton.Visible = false;


                }
                else if (ViewState["usertype"].ToString() == "4" || ViewState["usertype"].ToString() == "3" || ViewState["usertype"].ToString() == "1")
                {
                    pnlId.Visible = true;
                    lblidno.Enabled = true;
                    ShowStudentDetails();
                    //if (Request.QueryString["id"] != null)
                    //{
                    //    ViewState["action"] = "edit";
                    //    ShowStudentDetails();
                    //}
                }

                //ViewState["ipAddress"] = GetUserIPAddress(); //Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            }
        }
        else
        {
            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    //txtSearch.Text = string.Empty;
                    //lvStudent.DataSource = null;
                    //lvStudent.DataBind();
                    //lblNoRecords.Text = string.Empty;
                }
            }
        }

        if (Page.Request.Params["__EVENTTARGET"] != null)
        {
            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearchstu"))
            {
                string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                bindliststudent(arg[0], arg[1]);
            }

            if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnClose"))
            {
                // lblMsg.Text = string.Empty;

            }
        }
    }

    // IPADRESS  DETAILS ..
    //private string GetUserIPAddress()
    //{
    //    string User_IPAddress = string.Empty;
    //    string User_IPAddressRange = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //    if (string.IsNullOrEmpty(User_IPAddressRange))//without Proxy detection
    //    {
    //        User_IPAddress = Request.ServerVariables["REMOTE_ADDR"];

    //    }
    //    else////with Proxy detection
    //    {
    //        string[] splitter = { "," };
    //        string[] IP_Array = User_IPAddressRange.Split(splitter, System.StringSplitOptions.None);

    //        int LatestItem = IP_Array.Length - 1;

    //    }
    //    return User_IPAddress;
    //}

    // Cancel De

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    //Bind Search List 
    private void bindlist(string category, string searchtext)
    {
        string branchno = "0";

        PhdController objSC = new PhdController();
        DataSet ds = objSC.RetrieveStudentDetailsPHD(searchtext, category, branchno);

        if (ds.Tables[0].Rows.Count > 0)
        {
            //lvStudent.DataSource = ds;
            //lvStudent.DataBind();
            //lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
       // else
            //lblNoRecords.Text = "Total Records : 0";
    }

    // SHOW STUDENT DETAILS 
    private void ShowStudentDetails()
    {
        PhdController objSC = new PhdController();
        DataTableReader dtr = null;
        DataSet ds = new DataSet();
        //  FOR ADMIN ..
        if (ViewState["usertype"].ToString() != "2")
        {
            string count = objCommon.LookUp("ACD_PHD_EXAMINER", "COUNT(IDNO)", "IDNO=" + (Convert.ToInt32(Session["stuinfoidno"])) + " AND ISNULL(SYNOPSIS_STATUS,0)=1 AND SYN_SUBDATE IS NOT NULL");
            string count1 = objCommon.LookUp("ACD_PHD_THESIS", "COUNT(IDNO)", "IDNO=" + (Convert.ToInt32(Session["stuinfoidno"]).ToString()) + " AND ISNULL(SUPERVISIOR_THS_STATUS,0)=1 AND SUPERVISIOR_THS_DATE IS NOT NULL");

            if (Convert.ToInt32(count) > 0)
            {
                dtr = objSC.GetPhdStudentSupervisiorThesisDetails(Convert.ToInt32(Session["stuinfoidno"]));
                //pnDisplay.Enabled = true;
                //Btnsubmit.Enabled = true;

            }
            else
            {
                objCommon.DisplayMessage("Student Not Eligible Because Synopsis Submission is Pending !!", this.Page);
                Btnsubmit.Enabled = false;
            }

            if (Convert.ToInt32(count1) > 0)
            {
                ds = objCommon.FillDropDown("ACD_PHD_THESIS", "SUPERVISIOR_THS_NAME FILENAME ,SUPERVISIOR_THS_PATH PATH", "IDNO", "IDNO=" + Convert.ToInt32(Session["stuinfoidno"]) + " AND ISNULL(SUPERVISIOR_THS_STATUS,0) = 1", "");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvUpload.DataSource = ds;
                    lvUpload.DataBind();
                    lvUpload.Visible = true;
                }

                objCommon.DisplayMessage("Your Already Submited Details", this.Page);
                lblmsg.Text = "Your Already Submited Details";
                Btnsubmit.Enabled = false;
            }
        }

        if (dtr != null)
        {
            if (dtr.Read())
            {
                //    --IDNO	ENROLLNO	ROLLNO	STUDNAME	FATHERNAME	BRANCHNAME	SUPERVISORNO	SUPERVISORNAME	SUPERROLE	RESEARCH	
                //--PHDSTATUS	CREDITS_GIVEN	CREDITS_COMPLETED	ADMDATE	SYN_SUBDATE	THESIS_UPLOAD_DATE	SUPERVISIOR_THS_STATUS	
                //--SUPERVISIOR_THS_DATE	THESIS_EXTENDDATE

               //txtIDNo.Text = dtr["IDNO"].ToString();
                lblidno.ToolTip = dtr["IDNO"].ToString();
                lblidno.Text = dtr["IDNO"].ToString();
                lblEnrollNo.ToolTip = dtr["ENROLLNO"].ToString();
                lblEnrollNo.Text = dtr["ENROLLNO"].ToString();
                //lblRegNo.Text = dtr["ROLLNO"].ToString();
                lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblFatherName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString().ToUpper();
                lblDateOfJoining.Text = dtr["ADMDATE"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["ADMDATE"]).ToString("dd/MM/yyyy");
                lblBranch.Text = dtr["BRANCHNAME"] == null ? string.Empty : dtr["BRANCHNAME"].ToString();
                lblStatus.Text = dtr["PHDSTATUS"] == null ? string.Empty : dtr["PHDSTATUS"].ToString();
                lblSupervisor.Text = dtr["SUPERVISORNAME"] == null ? string.Empty : dtr["SUPERVISORNAME"].ToString();
                lblSupervisor.ToolTip = dtr["SUPERVISORNO"] == null ? string.Empty : dtr["SUPERVISORNO"].ToString();
                lblCreditgiven.Text = dtr["CREDITS_GIVEN"] == null ? string.Empty : dtr["CREDITS_GIVEN"].ToString();
                lblCreditCmplt.Text = dtr["CREDITS_COMPLETED"] == null ? string.Empty : dtr["CREDITS_COMPLETED"].ToString();
                lblsyssdate.Text = dtr["SYN_SUBDATE"] == null ? string.Empty : dtr["SYN_SUBDATE"].ToString();
                lblthsdate.Text = dtr["THESIS_UPLOAD_DATE"] == null ? string.Empty : dtr["THESIS_UPLOAD_DATE"].ToString();
                lblthsextn.Text = dtr["THESIS_EXTENDDATE"] == null ? string.Empty : dtr["THESIS_EXTENDDATE"].ToString();


                if (ViewState["usertype"].ToString() == "3")
                {
                    if (lblSupervisor.ToolTip.ToString() == Session["userno"].ToString())
                    {
                        DateTime todate = DateTime.Today.Date;
                        DateTime thsmindate = Convert.ToDateTime(lblsyssdate.Text.ToString());
                        DateTime thsmaxdate = Convert.ToDateTime(lblthsdate.Text.ToString());

                        if (todate >= thsmindate && todate <= thsmaxdate)
                        {
                            Btnsubmit.Visible = true;
                        }
                        else
                        {
                            if (lblthsextn.Text == string.Empty)
                            {
                                objCommon.DisplayMessage("Your To Late To Submitted Thesis", this.Page);
                                lblmsg.Text = "Your To Late To Submitted Thesis";
                                Btnsubmit.Visible = false;
                            }
                            else
                            {
                                DateTime compexdate = Convert.ToDateTime(lblthsextn.Text.ToString());
                                if (todate <= compexdate)
                                {
                                    Btnsubmit.Visible = true;
                                }
                                else
                                {
                                    objCommon.DisplayMessage("Your To Late To Submitted Thesis", this.Page);
                                    lblmsg.Text = "Your To Late To Submitted Thesis";
                                    Btnsubmit.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Only This Student Supervisior Can Uploaded Thesis!! ", this.Page);
                    }
                }

                if (ViewState["usertype"].ToString() == "1")
                {
                    pnlsearch.Visible = false;
                    dvgeneral.Visible = true;
                    //dvdetails.Visible = true;
                    divGeneralInfo.Visible = false;
                    pnldetails.Visible = true;
                    //dvstudentdetails.Visible = false;
                    dvuploadthesis.Visible = true;
                    dvbutton.Visible = true;
                    Btnsubmit.Visible = true;
                    btncertificate.Visible = true;
                }

                if (ViewState["usertype"].ToString() == "4")
                {
                    Btnsubmit.Visible = false;
                    btncertificate.Visible = true;
                }


            }
        }
    }

    //private void CheckPageAuthorization()
    //{
    //    if (Request.QueryString["pageno"] != null)
    //    {
    //        //Check for Authorization of Page
    //        if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
    //        {
    //            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
    //        }
    //    }
    //    else
    //    {
    //        //Even if PageNo is Null then, don't show the page
    //        Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
    //    }
    //}

    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
        {
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        }
        else
        {
            url = Request.Url.ToString();
        }
        int idno = Convert.ToInt32(lnk.CommandArgument);
        Session["stuinfoidno"] = idno;
        ShowStudentDetails();
        pnldetails.Visible = true;
        //Response.Redirect(url + "&id=" + lnk.CommandArgument);
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(Session["userno"].ToString());
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updBatch, this.updBatch.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_PhdAnnexure.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void Btnsubmit_Click(object sender, EventArgs e)
    {
        if ((ViewState["usertype"].ToString() == "3" && lblSupervisor.ToolTip.ToString() == Session["userno"].ToString()) || ViewState["usertype"].ToString() == "1")
        {
            if (fuExtended.FileName != string.Empty)
            {
                SupervisiorSubmitThesis(fuExtended, fuExtended.FileName.ToString(), "Thesis");

                PhdController objSC = new PhdController();
                Student objS = new Student();
                //objS.IdNo = Convert.ToInt32(lblRegNo.ToolTip);
                objS.IdNo = Convert.ToInt32(Session["stuinfoidno"]);
                objS.ThesisTitle = Filename;
                objS.PhdExaminerFile1 = Filepath;
                objS.Uano = Convert.ToInt32(Session["userno"].ToString());
                objS.IPADDRESS = ViewState["ipAddress"].ToString();

                string output = objSC.UpdatePhdThesisFile(objS);
                if (output == "1")
                {
                    objCommon.DisplayMessage("Thesis Uploaded Successfully", this.Page);
                    this.ShowStudentDetails();
                }
                else
                {
                    objCommon.DisplayMessage("Thesis Not Uploaded", this.Page);
                    this.ShowStudentDetails();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select File To Upload", this.Page);
            }
        }
        else
        {

            objCommon.DisplayMessage("Your Not Eligible For Thesis Upload, Only Supervisior can Uploaded File", this.Page);
        }
    }

    //  synopsis ,Thesis File Upload CODE 
    private void SupervisiorSubmitThesis(FileUpload fupload, string file, string dcname)
    {
        //FileUpload fupload = new FileUpload();
        //fupload.ID = "fuEx" + id;          
        string rollno = string.Empty;
        string DOCNAME = string.Empty, FILENAME = string.Empty;
       //// string path = "d:\\temp\\PHD\\THESIS\\";
         string path = System.Configuration.ConfigurationManager.AppSettings["PHD_THESIS"];

      //string path = Server.MapPath("~/PHDFILES/");
        DOCNAME = dcname;
        FILENAME = fupload.FileName;
        rollno = lblEnrollNo.Text.ToString().Trim();

        string FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();

        try
        {
            if (!(Directory.Exists(path)))
                Directory.CreateDirectory(path);
            if (fupload.HasFile)
            {
                string[] array1 = Directory.GetFiles(path);

                foreach (string str in array1)
                {
                    if ((path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName.ToString()).Equals(str))
                    {
                      //  File.Delete(path + FileString);
                    }
                }
                fupload.PostedFile.SaveAs(path + rollno.ToString() + "_" + DOCNAME + "_" + fupload.FileName);
                FileString = rollno.ToString() + "_" + DOCNAME.ToString() + "_" + FILENAME.ToString();
                Filename = FileString;
                Filepath = path;
            }
        }
        catch (Exception ex) { }
    }


    protected void btncertificate_Click(object sender, EventArgs e)
    {
        ShowReport("PhdThesis", "Phd_Supervisior_Thesis_Uploaded.rpt");
    }


    protected void lnkDownloadDoc_Click(object sender, EventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)(sender as Control).NamingContainer;

        HiddenField hdfFilename = (HiddenField)item.FindControl("hdfFilename");
        LinkButton lnkbtndoc = (LinkButton)item.FindControl("lnkDownloadDoc");

        string FILENAME = string.Empty;

        FILENAME = lnkbtndoc.Text.ToString();

        string filePath = hdfFilename.Value.ToString().Trim();

        FileStream Writer = null;
        //-------
        //string[] array1 = Directory.GetFiles(filePath);
        //foreach (string str in array1)
        //{
        //    filePath = filePath;
        //}

        filePath = filePath + FILENAME;
        lnkbtndoc.ToolTip = filePath;
        DownloadFile(filePath);
        Writer = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.None);
        Writer.Close();

    }


    public void DownloadFile(string filePath1)
    {
        try
        {

            FileStream sourceFile = new FileStream((filePath1), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();

            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.AddHeader("content-disposition", "attachment; filename=" + filePath1);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = GetResponseType(filePath1.Substring(filePath1.IndexOf('.')));
            Response.Write("Unable to download the attachment.");
        }
    }

    private string GetResponseType(string fileExtension)
    {
        string ret = string.Empty;
        switch (fileExtension.ToLower())
        {
            case ".doc":
                ret = "application/vnd.ms-word";
                break;

            case ".docx":
                ret = "application/vnd.ms-word";
                break;

            case ".wps":
                ret = "application/ms-excel";
                break;

            case ".jpeg":
                ret = "image/jpeg";
                break;

            case ".gif":
                ret = "image/gif";
                break;

            case ".png":
                ret = "image/png";
                break;

            case ".bmp":
                ret = "image/bmp";
                break;

            case ".tiff":
                ret = "image/tiff";
                break;

            case ".ico":
                ret = "image/x-icon";
                break;
            case ".txt":
                ret = "text/plain";
                break;

            case ".pdf":
                ret = "application/pdf";
                break;

            case ".jpg":
                ret = "image/jpg";
                break;

            case "":
                ret = "";
                break;

            default:
                ret = "";
                break;
        }
        return ret;
    }


    private void bindliststudent(string category, string searchtext)
    {

        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetailsNewForPHDOnly(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            liststudent.Visible = true;
            liststudent.DataSource = ds;
            liststudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), liststudent);//Set label 
            //lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            //lblNoRecords.Text = "Total Records : 0";
            liststudent.Visible = false;
            liststudent.DataSource = null;
            liststudent.DataBind();
        }
    }
    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlLV.Visible = false;
            //lblNoRecords.Visible = false;
            liststudent.DataSource = null;
            liststudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtsearchstu.Visible = false;
                        pnlDropdown.Visible = true;

                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);

                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtsearchstu.Visible = true;
                        pnlDropdown.Visible = false;

                    }
                }
            }
            else
            {

                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;

            }
        }
        catch
        {
            throw;
        }

    }
    protected void btnsearchstu_Click(object sender, EventArgs e)
    {
        //lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
        {
            value = ddlDropdown.SelectedValue;
        }
        else
        {
            value = txtsearchstu.Text;
        }



        bindliststudent(ddlSearch.SelectedItem.Text, value);
        ddlDropdown.ClearSelection();
        txtsearchstu.Text = string.Empty;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}




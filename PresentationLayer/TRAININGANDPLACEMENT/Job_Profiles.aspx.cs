using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
public partial class EXAMINATION_Projects_Job_Profiles : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    StudentController objtS = new StudentController();
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    BindListViewJobProfile();
                   // string idno = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + userno);
                    int idno = Convert.ToInt32(Session["idno"]);
                   // Details_Veiw.Visible = true;
                    //int offersend = Convert.ToInt32(objCommon.LookUp("ACD_TP_REGISTER", "Count( OFFER_SEND)", "IDNO=" + Convert.ToInt32(Session["idno"])));
                    //if (offersend == 1)
                    //{
                    //    pnlrounds.Visible = false;
                    //}
                    //else
                    //{
                    //    pnlrounds.Visible = true;
                    //}
                    BindStudroundDetails();
                    BindLVOfferDetails();
                }                
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListViewJobProfile()
    {
        try
        {
            
            int idno = Convert.ToInt32(Session["idno"]);
            DataSet ds = objCompany.JobProfileBindLV(idno);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvJobProfile.DataSource = ds;
                lvJobProfile.DataBind();
                ViewState["TSno"] = Convert.ToInt32(ds.Tables[0].Rows.Count);
            }
            foreach (ListViewDataItem dataitem in lvJobProfile.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;
                HiddenField hdscheduleno = dataitem.FindControl("hdscheduleno") as HiddenField;
                int scheduleno = Convert.ToInt32(hdscheduleno.Value);
                string Statuss = (Status.Text.ToString());
                if (Statuss == "NOT-APPLIED")
                {
                    Status.CssClass = "badge badge-warning";
                }
                else if (Statuss == "SELECTED")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else if (Statuss == "OFFER SEND")
                {
                    Status.CssClass = "badge badge-info";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                    //pnljob.Visible = false;
                    // pnlhide.Visible = false;
                }
                string intvselect = objCommon.LookUp("ACD_TP_REGISTER", "ISNULL(INTVSELECT,0) as INTVSELECT", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO ='" + scheduleno + "'");
                if (intvselect == "True")
                {
                    pnlrounds.Visible = true;
                }


                //string studconfirm = objCommon.LookUp("ACD_TP_REGISTER", "STUDCONFIRM", "IDNO=" + (Session["idno"]).ToString());
                //if (studconfirm == "True")
                //{
                //    pnlrounds.Visible = true;
                //    btnSubmitOffer.Visible = false;
                //    btnCancelOffer.Visible = false;
                //    ddlStatus.SelectedValue = (1).ToString();
                //    ddlStatus.Enabled = false;
                //}

                //string studapply = objCommon.LookUp("ACD_TP_REGISTER", "STUDAPPLY", "IDNO=" + (Session["idno"]).ToString());
                //if (studconfirm == "True")
                //{

                //    pnljob.Visible = false;
                //    pnlhide.Visible = false;
                //}

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindCompanyDetails(int Compid, int Scheduleno)
    {         
        try
        {
            DataSet ds = objCompany.BindJobProfileCompDetails(Compid, Scheduleno);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Show();", true);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblCompanyName.Text = ds.Tables[0].Rows[0]["COMPNAME"].ToString();
                lblJobType.Text = ds.Tables[0].Rows[0]["JOBROLETYPE"].ToString();
                lblJobRole.Text = ds.Tables[0].Rows[0]["JOBTYPE"].ToString();
                lblLocation.Text = ds.Tables[0].Rows[0]["LOCATIONNAME"].ToString();
                lblPlacementMode.Text = ds.Tables[0].Rows[0]["PLACED_STATUS"].ToString();
                lblJobDescription.Text = ds.Tables[0].Rows[0]["JOBDISCRIPTION"].ToString();
                lblAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                lblCurrency.Text = ds.Tables[0].Rows[0]["CUR_NAME"].ToString();
                lblInterval.Text = ds.Tables[0].Rows[0]["INTERVALS"].ToString();
                lblDateFromTo.Text =  Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWFROM"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["INTERVIEWTO"].ToString()).ToString("MMM dd, yyyy");
                lblVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
                lblLastDateApply.Text = ds.Tables[0].Rows[0]["LASTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["LASTDATE"].ToString()).ToString("dd/MM/yyyy");
                lblEligibility.Text = ds.Tables[0].Rows[0]["CRITERIA"].ToString();
                lblRound1.Text = ds.Tables[0].Rows[0]["SELECTNAME"].ToString();
                lblDescription.Text = ds.Tables[0].Rows[0]["ROUNDS_DISCRIPTION"].ToString();
               // pnljob.Visible = true;
               // pnlhide.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkcmpdetails_Click(object sender, EventArgs e)
    {
       
        LinkButton compdetails = sender as LinkButton;
        int Scheduleno = int.Parse(compdetails.CommandArgument);
        

        string Compid = objCommon.LookUp("ACD_TP_COMPSCHEDULE", "COMPID", "SCHEDULENO ='" + Scheduleno + "'");
        ViewState["COMPID"] = int.Parse(Compid);
        ViewState["action"] = "edit";
        AllConditions( Scheduleno);
        //string studconfirm = objCommon.LookUp("ACD_TP_REGISTER", "STUDCONFIRM", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO ='" + Scheduleno + "'");
        //if (studconfirm != "")
        //{
        //    //pnlrounds.Visible = true;
        //    //btnSubmitOffer.Visible = false;
        //    //btnCancelOffer.Visible = false;
        //    //ddlStatus.SelectedValue = (1).ToString();
        //    //ddlStatus.Enabled = false;
        //    pnljob.Visible = false;
        //    pnlhide.Visible = false;
        //}
        //else
        //{
           
        //    btnSubmitOffer.Visible = true;
        //    btnCancelOffer.Visible = true;
            
        //   ViewState["hdshedno"] = Scheduleno.ToString();

        //}
        

        //-------start

        //foreach (ListViewDataItem dataitem in lvJobProfile.Items)
        //{
        //    Label Status = dataitem.FindControl("lblStatus") as Label;
        //    HiddenField hdscheduleno = dataitem.FindControl("hdscheduleno") as HiddenField;
        //    int scheduleno = Convert.ToInt32(hdscheduleno.Value);
        //    string Statuss = (Status.Text.ToString());
        //    string studconfirm = objCommon.LookUp("ACD_TP_REGISTER", "STUDCONFIRM", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO ='" + scheduleno + "'");
        //    if (studconfirm != "")
        //    {
        //        pnlrounds.Visible = true;
        //        btnSubmitOffer.Visible = false;
        //        btnCancelOffer.Visible = false;
        //        ddlStatus.SelectedValue = (1).ToString();
        //        ddlStatus.Enabled = false;
        //    }

        //}

        //------end----


        this.BindCompanyDetails(Convert.ToInt32(Compid), Scheduleno);
        //lblCompanyName.Text = "BB";
        
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {

        if (fuResume.HasFile)
        {
            string ext = System.IO.Path.GetExtension(fuResume.FileName).ToLower();
            if (ext == ".pdf")
            {
                fuResume.SaveAs(Server.MapPath("~/ACADEMIC/Resume/" + fuResume.FileName));
                string path = Server.MapPath("~/ACADEMIC/Resume/");
            }
            else
            {
                objCommon.DisplayMessage("Please Upload only Pdf files", this.Page);
                return;
            }
            //if (ext==string.Empty)
            //{
            //    objCommon.DisplayMessage("Please Upload Resume File ", this.Page);
            //    return;
            //}
            if (fuResume.FileName.ToString().Length > 50)
            {
                objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                return;
            }
            if (chkinfo.Checked == true)
            {
                int idno = Convert.ToInt32(Session["idno"]);
                string file = fuResume.FileName.Replace("idno", "_");
                //string filename = "_" + file;

                //foreach (ListViewItem item in lvJobProfile.Items)
                //{
                //    LinkButton sno = (item.FindControl("lnkrdetails") as LinkButton);
                //    ViewState["scheduleno"] = Convert.ToInt32(sno.ToolTip);   

                //}
                int scheduleno = Convert.ToInt32(ViewState["hdshedno"]);

                CustomStatus CS = (CustomStatus)objCompany.UpdateStudResume(objTP, idno, file, scheduleno);
                if (CS.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(upnlJobProfile, "Applied Successfully.", this.Page);
                    ViewState["action"] = null;
                    BindListViewJobProfile();
                    //clear();
                }
            }
            else if (chkinfo.Checked == false)
            {
                objCommon.DisplayMessage(upnlJobProfile, "Please accept the Declaration.", this.Page);            
            }            
        }
    }

    private void BindStudroundDetails()
    {

        string idno = Session["idno"].ToString();
        int scheduleno = Convert.ToInt32(ViewState["Sceduleno"]);
        DataSet ds = objCompany.getjob_profile_data(idno, scheduleno);
        if (ds.Tables[0].Rows.Count > 0)
        {

            lblAppDate.Text = ds.Tables[0].Rows[0]["OFFER_DATE"].ToString();
            txtOfferDescription.Text = ds.Tables[0].Rows[0]["OFFER_DISCRIPTION"].ToString();
            ViewState["RESUME"] = ds.Tables[0].Rows[0]["RESUME"].ToString();
        }
        else{
            //pnljob.Visible = false;
            //pnlhide.Visible = false;
        }

        //string studconfirm = objCommon.LookUp("ACD_TP_REGISTER", "STUDCONFIRM", "IDNO=" + (Session["idno"]).ToString());
        //if (studconfirm =="True")
        //{
        //    pnlrounds.Visible = true;
        //    btnSubmitOffer.Visible = false;
        //    btnCancelOffer.Visible = false;
        //    ddlStatus.SelectedValue = (1).ToString();
        //    ddlStatus.Enabled = false;
        //}
    }

    protected void lnkrdetails_Click(object sender, EventArgs e)
    {
        //foreach (ListViewItem item in lvJobProfile.Items)
        //{
        //    LinkButton ibschedule = item.FindControl("lnkrdetails") as LinkButton;
        //ViewState["Sceduleno"] = ibschedule.ToolTip;
        LinkButton btnSchedule = sender as LinkButton;
        ViewState["Sceduleno"] = btnSchedule.ToolTip;

            string idno = Session["idno"].ToString();

        int Scheduleno =Convert.ToInt32( ViewState["Sceduleno"]);
       // AllConditions(Scheduleno);
        string studconfirm1 = objCommon.LookUp("ACD_TP_REGISTER ", "INTVSELECT", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO= '" + Convert.ToInt32(ViewState["Sceduleno"]) + "' and INTVSELECT=1");
        if (studconfirm1 == "False")
        {
            objCommon.DisplayMessage(this.Page, "Before Selection It Will Be Not Applicable.", this.Page);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#View_Details').modal('hide');", true);

            return;
        }

        string studconfirm2 = objCommon.LookUp("ACD_TP_REGISTER ", "STUDCONFIRM", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO= '" + Convert.ToInt32(ViewState["Sceduleno"]) + "' and STUDCONFIRM=1");
        if (studconfirm1 == "True")
        {
            btnSubmitOffer.Enabled = false;
            btnCancelOffer.Enabled = false;
        }
           
            BindLVOfferDetails();
            BindListViewJobProfile();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Showround();", true);
            BindStudroundDetails();
            //return;
        //}
       
    }

    private void BindLVOfferDetails()
    {
        try
        {
            string idno = Session["idno"].ToString();
            int scheduleno = Convert.ToInt32(ViewState["Sceduleno"]);
            DataSet ds = objCompany.getjob_profile_data(idno, scheduleno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvappldetails.DataSource = ds;
                lvappldetails.DataBind();
                lvappldetails.Visible = true;
                pnlrounds.Visible = true;
                //ViewState["TSno"] = Convert.ToInt32(ds.Tables[0].Rows.Count);
            }
            else
            {
                lvappldetails.DataSource = null;
                lvappldetails.DataBind();
                lvappldetails.Visible = false;
              //  pnlrounds.Visible = false;
                
            }
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lnkdownload_Click(object sender, EventArgs e)
    {
        LinkButton DownLoadBtn = sender as LinkButton;
        //string filetemp = (sender as Button).CommandArgument;
        //string FILE_NAME = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        string FILE_NAME = ViewState["RESUME"].ToString();
        string FILENAME = DownLoadBtn.ToolTip;
        //string filePath = Server.MapPath("~/ACADEMIC/OfferLetter/" + FILE_NAME);
        string filePath = Server.MapPath("~/ACADEMIC/Resume/" + FILE_NAME);
        FileInfo file = new FileInfo(filePath);
        if (file.Exists)

        {
            DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), FILE_NAME);
        }
        else
        {
            objCommon.DisplayMessage("Requested file is not available to download", this.Page);

               return;
        }
        //if (file.Exists)
        //{
        //    Response.Clear();
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //    Response.AddHeader("Content-Length", file.Length.ToString());
        //    Response.ContentType = "application/octet-stream";
        //    Response.Flush();
        //    Response.TransmitFile(file.FullName);
        //    Response.End();
        //}
        //else
        //{
        //    objCommon.DisplayMessage("Requested file is not available to download", this.Page);
            
        //    return;
        //}

    }

    private void AllConditions(int Scheduleno)
    {
        try
        {
            //-----start------Validation for student is In Disciplinary Action ----
            DateTime AnnounToDate = Convert.ToDateTime(objCommon.LookUp("ACD_TP_COMPSCHEDULE", "INTERVIEWTO", "SCHEDULENO='" + Scheduleno + "'"));

            int disciplinaryAction = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_TP_DISCIPLINARY_ACTION B ON (A.REGNO=B.REGNO and A.ENROLLNO=B.ENROLLNO)", "COUNT(*)", "IDNO='" + (Session["idno"]).ToString() + "' and '"+Convert.ToDateTime(AnnounToDate).ToString("yyyy-MM-dd")+"' between B.DISCIPLINARY_START_DATE and B.DISCIPLINARY_END_DATE"));
            if (disciplinaryAction > 0)
            {
                objCommon.DisplayMessage(this.Page, "Sorry You Are Not Eligible For Job Application Due To Disciplinary Action Found Agains You .", this.Page);
                // Details_Veiw.Visible = false;
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ClosePopup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#Details_Veiw').modal('hide');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#Details_Veiw').hide();", true);
                return;
            }
            //else
            //{
            //   // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowPopup();", true);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#Details_Veiw').modal('show');", true);
            //   // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#Details_Veiw').show();", true);
            //}

           


            //--------end--------------------------

            //---------start----Job Details-----
            string studconfirm = objCommon.LookUp("ACD_TP_REGISTER", "STUDCONFIRM", "IDNO='" + (Session["idno"]).ToString() + "' and SCHEDULENO ='" + Scheduleno + "'");
            if (studconfirm != "")
            {
                //pnlrounds.Visible = true;
                //btnSubmitOffer.Visible = false;
                //btnCancelOffer.Visible = false;
                //ddlStatus.SelectedValue = (1).ToString();
                //ddlStatus.Enabled = false;
                pnljob.Visible = false;
                pnlhide.Visible = false;
               // pnlrounds.Visible = false;
            }
            else
            {
                pnlrounds.Visible = false;
                btnSubmitOffer.Visible = true;
                btnCancelOffer.Visible = true;
                pnljob.Visible = true;
                pnlhide.Visible = true;
               // Details_Veiw.Visible = true;
                ViewState["hdshedno"] = Scheduleno.ToString();

            }

            //---------end----Job Details-----



            //---------start----Rounds Details-----
            string studconfirm1 = objCommon.LookUp("ACD_TP_REGISTER A inner join ACD_TP_STUDENT_SELECTION_PROCESS B ON (A.SCHEDULENO=B.SCHEDULENO )", "STUDCONFIRM", "IDNO='" + (Session["idno"]).ToString() + "' and A.SCHEDULENO= '" + Convert.ToInt32(ViewState["Sceduleno"]) + "' and STUDCONFIRM=1 and INTVSELECT=1");
            if (studconfirm1 == "True")
            {
                lnkdownload.Visible = false;
                pnlrounds.Visible = false;
                divRounds.Visible = false;



                //pnlrounds.Visible = true;
                //divbutton.Visible = true;
                //lvappldetails.Visible = true;


                //pnlrounds.Visible = true;
                //btnSubmitOffer.Visible = true;
                //btnCancelOffer.Visible = true;
                //divbutton.Visible = true;
                //btnSubmitOffer.Visible = true;
                //btnCancelOffer.Visible = true;
                //ddlStatus.SelectedValue = (1).ToString();
                //ddlStatus.Enabled = true;

            }
            else
            {

                //lnkdownload.Visible = false;
                //pnlrounds.Visible = false;
                //divRounds.Visible = false;

                pnlrounds.Visible = true;
                divbutton.Visible = true;
                lvappldetails.Visible = true;


                //pnlrounds.Visible = false;
                //btnSubmitOffer.Visible = false;
                //btnCancelOffer.Visible = false;
             //   divbutton.Visible = false;
               // btnSubmitOffer.Visible = false;
               // btnCancelOffer.Visible = false;
                //ddlStatus.SelectedValue = (1).ToString();
                //ddlStatus.Enabled = false;
            }
            //---------end----Rounds Details-----
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void fileDownload(string FILE_NAME, string filepath)
    {
        ResponseFile(Page.Request, Page.Response, FILE_NAME, filepath, 1024000);
    }

     public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _FILE_NAME, string _fullPath, long _speed)
                  {
                        try
                         {
                               FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                               BinaryReader br = new BinaryReader(myFile);
                        try
                         {
                              _Response.AddHeader("Accept-Ranges", "bytes");
                              _Response.Buffer = false;
                               long fileLength = myFile.Length;
                               long startBytes = 0;

                               int pack = 10240; //10K bytes
                               int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                 if (_Request.Headers["Range"] != null)
                    {
                              _Response.StatusCode = 206;
                               string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                               startBytes = Convert.ToInt64(range[1]);
                                }
                               _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                                if (startBytes != 0)
                                {
                                _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                                 }
                                _Response.AddHeader("Connection", "Keep-Alive");
                                _Response.ContentType = "application/octet-stream";
                                _Response.AddHeader("Content-Disposition", "attachment;filename=" + _FILE_NAME);

                                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                                for (int i = 0; i < maxCount; i++)
                                 {
                                if (_Response.IsClientConnected)
                                 {
                                  _Response.BinaryWrite(br.ReadBytes(pack));
                                 }
                                else
                                 {
                                   i = maxCount;
                                 }
                                 }
                                 }
                                 catch
                                 {
                                    return false;
                                  }
                                     finally
                                  {
                                     br.Close();
                                     myFile.Close();
                                   }
                                   }
                                  catch
                                   {
                                      return false;
                                   }
                                      return true;
                                    }


     protected void btnSubmitOffer_Click(object sender, EventArgs e)
     {
         int idno = Convert.ToInt32(Session["idno"]);
         int scheduleno =Convert.ToInt32 (ViewState["Sceduleno"]);
         if (ddlStatus.SelectedValue == "1")
         {
             int ConfStatus = 1;
             CustomStatus CS = (CustomStatus)objCompany.UpdateStudConfirmStatus(objTP, idno, ConfStatus, scheduleno);
             if (CS.Equals(CustomStatus.RecordUpdated))
             {
                 objCommon.DisplayMessage(upnlJobProfile, "Response Saved Successfully.", this.Page);
                 ViewState["action"] = null;
                 //clear();
             }
             
         }
         else if (ddlStatus.SelectedValue == "2")
         {
             int ConfStatus = 2;
             CustomStatus CS = (CustomStatus)objCompany.UpdateStudConfirmStatus(objTP, idno, ConfStatus, scheduleno);
             if (CS.Equals(CustomStatus.RecordUpdated))
             {
                 objCommon.DisplayMessage(upnlJobProfile, "Response Saved Successfully.", this.Page);
                 ViewState["action"] = null;
                 //clear();
             }

         }
     }


           public void DownloadFile(string path, string fileName)
    {
        try
        {
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }
    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
  
     
}
//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : Manish 
// MODIFIED BY   : 
// MODIFIED DATE : 13/04/2016
// MODIFIED DESC : 
//======================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using IITMS.SQLServer.SQLDAL;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Ecc;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Codec.Util;
using System.Drawing;

using System.Transactions;
using CrystalDecisions.Shared;
using System.IO;

public partial class ACADEMIC_EXAMINATION_TranscriptReportUG : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    QrCodeController objQrC = new QrCodeController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //CHECK THE STUDENT LOGIN
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_IDNO=" + Convert.ToInt32(Session["idno"]));
                ViewState["usertype"] = ua_type;

               
            }
            imgPhoto.ImageUrl = "~/images/nophoto.jpg";

        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtEnrollmentSearch.Text = string.Empty;
                   
                }
            }
        }
        divMsg.InnerHtml = string.Empty;

    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ShowDetails();
        GenerateQrCode();
    }

    public byte[] imageToByteArray(string MyString)
    {
        FileStream ff = new FileStream(MyString, FileMode.Open);
        int ImageSize = (int)ff.Length;
        byte[] ImageContent = new byte[ff.Length];
        ff.Read(ImageContent, 0, ImageSize);
        ff.Close();
        ff.Dispose();
        return ImageContent;
    }

    private void GenerateQrCode()
    {
        string RegNo = txtEnrollmentSearch.Text.ToString();
        string idno = objCommon.LookUp("ACD_STUDENT", "idno", "REGNO='" + RegNo + "'");
        if (!string.IsNullOrEmpty(idno))
        {
            int Idno = Convert.ToInt32(idno);

            DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "*", "", "REGNO='" + RegNo + "'", "REGNO");

            //string BranchName = objCommon.LookUp("ACD_BRANCH","SHORTNAME","BRANCHNO="+ ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Trim()+"");

            DataSet ds1 = objQrC.GetStudentResultData(Idno);
            //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
            string Qrtext = "RegNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Session=" + ds1.Tables[0].Rows[0]["SESSION"].ToString().Trim() + "; Sem1(Total Obt. Marks)=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS1"].ToString().Trim() + "; S2=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS2"].ToString().Trim() + "; S3=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS3"].ToString().Trim() + "; S4=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS4"].ToString().Trim() + "; S5=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS5"].ToString().Trim() + "; S6=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS6"].ToString().Trim() + "; S7=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS7"].ToString().Trim() + "; S8=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS8"].ToString().Trim() + "; S9=" +
                                  ds1.Tables[0].Rows[0]["TOTAL_OBTAINED_MARKS9"].ToString().Trim() + ";";


            Session["qr"] = Qrtext.ToString();

            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeVersion = 10;
            Bitmap img = encoder.Encode(Session["qr"].ToString());
            //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
            //img.Save(Server.MapPath("~\\img.Jpeg"));
            img.Save(Server.MapPath("~\\img.Jpeg"));
            ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

            byte[] QR_IMAGE = ViewState["File"] as byte[];
            long ret = objQrC.AddUpdateQrCode(Idno, QR_IMAGE);
        }
        else
        {
            objCommon.DisplayMessage(this.updpnlExam, "No student found having Reg no.: " + txtEnrollmentSearch.Text.Trim(), this);
        }
    }
    private void ShowDetails()
    {
        Clear();
        int idno;
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();
       
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());

            ViewState["idno"] = idno;
        try
        {
            if (idno > 0)
            {
                DataTableReader dtr = objSC.GetStudentDetails(idno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                        lblBranch.Text = branchname;
                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                        lblDOB.Text = dtr["DOB"] == DBNull.Value ? "" : Convert.ToDateTime(dtr["DOB"]).ToString("dd/MM/yyyy");
                        string caste = objCommon.LookUp("ACD_CASTE", "CASTE", "CASTENO=" + dtr["CASTE"].ToString());
                        lblCaste.Text = caste;
                        string category = objCommon.LookUp("ACD_CATEGORY", "CATEGORY", "CATEGORYNO=" + dtr["CATEGORYNO"].ToString());
                        lblCategory.Text = category;
                        string religion = objCommon.LookUp("ACD_RELIGION", "RELIGION", "RELIGIONNO=" + dtr["RELIGIONNO"].ToString());
                        lblReligion.Text = religion;
                        string nation = objCommon.LookUp("ACD_NATIONALITY", "NATIONALITY", "NATIONALITYNO=" + dtr["NATIONALITYNO"].ToString());
                        lblNationality.Text = nation;
                        lblLAdd.Text = dtr["LADDRESS"] == null ? string.Empty : dtr["LADDRESS"].ToString();
                        //string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                        //lblCity.Text = city;
                        lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                        lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                        lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                        //lblEnrollNo.Text = dtr["ROLLNO"] == null ? string.Empty : dtr["ROLLNO"].ToString();
                        string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        lblSemester.Text = semester;
                        //city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                        //lblPCity.Text = city;
                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";

                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updpnlExam, "No student found having Reg no.: " + txtEnrollmentSearch.Text.Trim(), this);
                lblSemester.Text = string.Empty;
                
                //ShowMessage("No student found having Reg no.: " + txtEnrollmentSearch.Text.Trim());
                //lblEnrollNo.Text = string.Empty;
                
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   
    private void Clear()
    {
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblName.Text = string.Empty;
        lblMName.Text = string.Empty;
        lblDOB.Text = string.Empty;
        lblCaste.Text = string.Empty;
        lblCategory.Text = string.Empty;
        lblReligion.Text = string.Empty;
        lblNationality.Text = string.Empty;
        lblLAdd.Text = string.Empty;
        lblLLNo.Text = string.Empty;
        lblMobNo.Text = string.Empty;
        lblPAdd.Text = string.Empty;
        imgPhoto.ImageUrl = null;     
        lblSemester.Text = string.Empty;

    }
    private void ShowTranscript(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString() + ",@P_SEMESTERNO=" + 0 + "";
            //,@P_REPORT_STATUS=" + txtReportStatus.Text + "";

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    //protected void btnTranscript_Click(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32 ((ViewState["idno"])) >0)
    //    {
    //        //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
    //        this.ShowTranscript("Transcript", "rptTranscripReport.rpt");
    //    }
    //}
    protected void btnReport_Click(object sender, EventArgs e)
    {
           ShowAllResult("AllResult", "rpt_StudentAllResultUGGreater2013-2014.rpt");
    }
    
  private void ShowAllResult(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() +  ",@P_IDNO=" + ViewState["idno"].ToString()+",@P_SEMESTERNO="+0+"";
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + ViewState["idno"].ToString();

            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

          ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_EXAMINATION_TranscriptReport.btnTranscript_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
  //protected void btnTranscriptBackReport_Click(object sender, EventArgs e)
  //{
  //    int Admbatch = Convert.ToInt16( objCommon.LookUp("ACD_STUDENT", "ADMBATCH", "IDNO="+ Convert.ToInt16( ViewState["idno"].ToString()) +""));

  //    if (Admbatch < 5)
  //    {
  //        this.ShowTranscriptBack("Transcript_Back_Report", "rptTranscriptBackReportLessADMFive.rpt");
  //    }
  //    else
  //    {
  //        this.ShowTranscriptBack("Transcript_Back_Report", "rptTranscriptBackReportGreaterADMFive.rpt");
  //    }
  //}


  //private void ShowTranscriptBack(string reportTitle, string rptFileName)
  //{
  //    try
  //    {
  //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
  //        url += "Reports/CommonReport.aspx?";
  //        url += "pagetitle=" + reportTitle;
  //        url += "&path=~,Reports,Academic," + rptFileName;
  //        // url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_SCHEMENO=" + 0 + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_IDNO=" + GetIDNO() + ",@P_ORDER_BY=1,@P_COLLEGE_CODE=" + Session["colcode"].ToString();
  //        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString()+",@P_IDNO=" + ViewState["idno"].ToString()+"";
  //        //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
  //        //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
  //        //divMsg.InnerHtml += " </script>";
  //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
  //        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
  //        sb.Append(@"window.open('" + url + "','','" + features + "');");

  //        ScriptManager.RegisterClientScriptBlock(this.updpnlExam, this.updpnlExam.GetType(), "controlJSScript", sb.ToString(), true);
  //    }
  //    catch (Exception ex)
  //    {
  //        if (Convert.ToBoolean(Session["error"]) == true)
  //            objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
  //        else
  //            objCommon.ShowError(Page, "Server Unavailable.");
  //    }
  //}

  protected void btnTranscriptWithHeader_Click(object sender, EventArgs e)
  {

      if (Convert.ToInt32((ViewState["idno"])) > 0)
      {
          //this.ShowTranscript("Transcript", "rptTranscript2.rpt");
          this.ShowTranscript("Transcript Report UG", "rptTranscripReportWithHeaderUG.rpt");
      }
  }
  protected void btnCancel_Click(object sender, EventArgs e)
  {
      Response.Redirect(Request.Url.ToString());
  }
}


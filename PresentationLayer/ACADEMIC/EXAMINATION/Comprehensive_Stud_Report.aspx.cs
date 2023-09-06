//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Comprehensive Student Report
// CREATION DATE : 
// CREATED BY    : AMIT YADAV
// MODIFIED BY   : ASHISH DHAKATE
// MODIFIED DATE : 14/02/2012
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

public partial class ACADEMIC_Comprehensive_Stud_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
   
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
        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
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
                string ua_type = objCommon.LookUp("User_Acc", "UA_TYPE", "UA_NO=" + Convert.ToInt32(Session["userno"]));
                ViewState["usertype"] = ua_type;

                if (ViewState["usertype"].ToString() == "2")
                {
                    pnlSearch.Visible = false;
                    ShowDetails();
                }
                else
                {
                    pnlSearch.Visible = true;
                }
            }
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
                    lvRegStatus.DataSource = null;
                    lvRegStatus.DataBind();
                    lvFees.DataSource = null;
                    lvFees.DataBind();
                    lvCertificate.DataSource = null;
                    lvCertificate.DataBind();
                    lblMsg.Text = string.Empty;
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
    }

    private void ShowDetails()
    {
        Clear();
        int idno;
        StudentController objSC = new StudentController();
        DataSet dsregistration, dsResult, dsFees, dsCertificate, dsRemark, dsRefunds, dsTestMarks, dsAttendance;
        FeeCollectionController feeController = new FeeCollectionController();
        if (ViewState["usertype"].ToString() == "2")
        {

            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            idno = feeController.GetStudentIdByEnrollmentNo(txtEnrollmentSearch.Text.Trim());
        }

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
                        string city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["LCITY"].ToString());
                        lblCity.Text = city;
                        lblLLNo.Text = dtr["LTELEPHONE"] == null ? string.Empty : dtr["LTELEPHONE"].ToString();
                        lblMobNo.Text = dtr["LMOBILE"] == null ? string.Empty : dtr["LMOBILE"].ToString();
                        lblPAdd.Text = dtr["PADDRESS"] == null ? string.Empty : dtr["PADDRESS"].ToString();
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                        lblSemester.Text = semester;
                        city = objCommon.LookUp("ACD_CITY", "CITY", "CITYNO=" + dtr["PCITY"].ToString());
                        lblPCity.Text = city;

                        if (dtr["TYPE_OF_PHYSICALLY_HANDICAP"].ToString() != "0")
                            lblHandicap.Text = dtr["PHYSICAL_HANDICAP"] == null ? string.Empty : "Yes ("+ dtr["PHYSICAL_HANDICAP"].ToString() + " )";
                        else
                            lblHandicap.Text = "NO";

                        imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dtr["IDNO"].ToString() + "&type=student";


                        //Students Current Registration Details
                        try
                        {
                            dsregistration = objSC.RetrieveStudentCurrentRegDetails(idno);
                            if (dsregistration.Tables[0].Rows.Count > 0)
                            {
                                lvRegStatus.DataSource = dsregistration;
                                lvRegStatus.DataBind();
                            }
                            else
                            {
                                lvRegStatus.DataSource = null;
                                lvRegStatus.DataBind();
                            }
                        }
                        catch
                        {
                        }

                        //End of Students Current Registration Details

                        //Students Attendance Details
                        try
                        {
                            string semesterno = dtr["SEMESTERNO"].ToString();
                            string schemeno = dtr["SCHEMENO"].ToString();
                            string sessionno = Session["currentsession"].ToString();

                            dsAttendance = objSC.RetrieveStudentAttendanceDetails(Convert.ToInt32(sessionno), Convert.ToInt32(schemeno), Convert.ToInt32(semesterno), idno);


                            if (dsAttendance.Tables[0].Rows.Count > 0)
                            {
                                lvAttendance.DataSource = dsAttendance;
                                lvAttendance.DataBind();
                            }
                            else
                            {
                                lvAttendance.DataSource = null;
                                lvAttendance.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        // End of Students Attendance Details


                        //Student Result Details
                        try
                        {
                            dsResult = objSC.RetrieveStudentCurrentResult(idno);
                            if (dsResult.Tables[0].Rows.Count > 0)
                            {
                                lvResult.DataSource = dsResult;
                                lvResult.DataBind();
                            }
                            else
                            {
                                lvResult.DataSource = null;
                                lvResult.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        //End of Student Result Details


                        //Students Fees Details
                        try
                        {
                            dsFees = objSC.RetrieveStudentFeesDetails(idno);
                            if (dsFees.Tables[0].Rows.Count > 0)
                            {
                                lvFees.DataSource = dsFees;
                                lvFees.DataBind();
                            }
                            else
                            {
                                lvFees.DataSource = null;
                                lvFees.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        //End of Students Fees Details

                        //Students Certificate issued Details
                        try
                        {
                            dsCertificate = objSC.RetrieveStudentCertificateDetails(idno);
                            if (dsCertificate.Tables[0].Rows.Count > 0)
                            {
                                lvCertificate.DataSource = dsCertificate;
                                lvCertificate.DataBind();
                            }
                            else
                            {
                                lvCertificate.DataSource = null;
                                lvCertificate.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        //End of Students Certificate issued Details

                        //Students class Test Details
                        try
                        {
                            dsTestMarks = objSC.RetrieveStudentClassTestMarks(idno);
                            if (dsTestMarks.Tables[0].Rows.Count > 0)
                            {

                                lvTestMark.DataSource = dsTestMarks;
                                lvTestMark.DataBind();
                            }
                            else
                            {
                                lvTestMark.DataSource = null;
                                lvTestMark.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        //End of Students class Test Details

                        //Remark
                        try
                        {
                            dsRemark = objSC.GetAllRemarkDetails(idno);
                            if (dsRemark != null && dsRemark.Tables.Count > 0 && dsRemark.Tables[0].Rows.Count > 0)
                            {
                                lvRemark.DataSource = dsRemark;
                                lvRemark.DataBind();
                            }
                            else
                            {
                                lvRemark.DataSource = null;
                                lvRemark.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        // End of Remark


                        //Refund details
                        try
                        {
                            dsRefunds = objSC.GetStudentRefunds(idno);
                            if (dsRefunds != null && dsRefunds.Tables.Count > 0 && dsRefunds.Tables[0].Rows.Count > 0)
                            {
                                lvRefund.DataSource = dsRefunds;
                                lvRefund.DataBind();
                            }
                            else
                            {
                                lvRefund.DataSource = null;
                                lvRefund.DataBind();
                            }
                        }
                        catch
                        {
                        }
                        // End of Refund Details
                    }
                }
            }
            else
            {
                ShowMessage("No student found having enrollment no.: " + txtEnrollmentSearch.Text.Trim());
                lblEnrollNo.Text = string.Empty;
                lblSemester.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = ex.ToString();
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Comprehensive_Stud_Report.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void lbReport_Click(object sender, EventArgs e)
    {
        //////Show Tabulation Sheet
        LinkButton btn = sender as LinkButton;
        string sessionNo = (btn.Parent.FindControl("hdfSession") as HiddenField).Value;
        string semesterNo = (btn.Parent.FindControl("hdfSemester") as HiddenField).Value;
        string schemeNo = (btn.Parent.FindControl("hdfScheme") as HiddenField).Value;

        int branchNo =Convert.ToInt16( objCommon.LookUp("ACD_SCHEME", "BRANCHNO", "SCHEMENO=" + Convert.ToInt16(schemeNo) + ""));
        int degreeNo = Convert.ToInt16(objCommon.LookUp("ACD_SCHEME", "DEGREENO", "SCHEMENO=" + Convert.ToInt16(schemeNo) + ""));
        string IdNo = (btn.Parent.FindControl("hdfIDNo") as HiddenField).Value;

            string RegNo = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt16(IdNo) + "");
            //GenerateQrCode(IdNo, RegNo, (lblName.Text),Convert.ToInt16(sessionNo),Convert.ToInt16(semesterNo),branchNo,degreeNo);
 
        //this.ShowTRReport("Tabulation_Sheet", "rptTabulationRegistarStud.rpt", sessionNo, schemeNo, semesterNo, IdNo);
        this.ShowTRReport("Grade_card", "rptGradeCardReport.rpt", sessionNo, branchNo, semesterNo, IdNo);
    }
    //private void GenerateQrCode(string idno, string regno, string studname,int sessionno,int semesterno,int branchno,int degreeno)
    //{
    //    QrCodeController objQrC = new QrCodeController();
    //    DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "*", "", "REGNO='" + regno + "'", "REGNO");


    //    DataSet ds1 = objQrC.GetStudentDataForGradeCard(Convert.ToInt16(sessionno), Convert.ToInt16(degreeno), Convert.ToInt16(branchno), Convert.ToInt16(semesterno), Convert.ToInt16(idno),);
        
    //    //StudName:=" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + ";
    //    string Qrtext = "RegNo=" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + "; StudName:" + ds.Tables[0].Rows[0]["STUDNAME"].ToString().Trim() + "; Session=" +
    //                          ds1.Tables[0].Rows[0]["SESSION"].ToString().Trim() + "; Earn_Cr(SEM)=" +
    //                          ds1.Tables[0].Rows[0]["EARN_CREDITS"].ToString().Trim() + "; EGP(SEM)=" +
    //                          ds1.Tables[0].Rows[0]["EGP"].ToString().Trim() + "; SGPA=" +
    //                          ds1.Tables[0].Rows[0]["SGPA"].ToString().Trim() + "; Earn_Cr(CUM)=" +
    //                          ds1.Tables[0].Rows[0]["CUMMULATIVE_CREDITS"].ToString().Trim() + "; EGP(CUM)=" +
    //                          ds1.Tables[0].Rows[0]["CUMMULATIVE_EGP"].ToString().Trim() + "; CGPA=" +
    //                          ds1.Tables[0].Rows[0]["CGPA"].ToString().Trim() + ";";

    //    Session["qr"] = Qrtext.ToString();

    //    QRCodeEncoder encoder = new QRCodeEncoder();
    //    encoder.QRCodeVersion = 10;
    //    Bitmap img = encoder.Encode(Session["qr"].ToString());
    //    //img.Save(Server.MapPath("~\\QrCode Files\\" + ds.Tables[0].Rows[0]["REGNO"].ToString().Trim() + ".Jpeg"));
    //    img.Save(Server.MapPath("~\\img.Jpeg"));
    //    ViewState["File"] = imageToByteArray(Request.PhysicalApplicationPath + "\\img.Jpeg");

    //    //img.Save(Server.MapPath("~\\img.Jpeg"));
    //    byte[] QR_IMAGE = ViewState["File"] as byte[];
    //    long ret = objQrC.AddUpdateQrCode(Convert.ToInt16(ds.Tables[0].Rows[0]["IDNO"].ToString().Trim()), QR_IMAGE);
    //}
    //This Method convert image to byte array.
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
    private void ShowTRReport(string reportTitle, string rptFileName, string sessionNo, int branchNo, string semesterNo, string idNo)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt16(sessionNo) + ",@P_DEGREENO="+1+",@P_BRANCHNO=" + branchNo + ",@P_SEMESTERNO=" + Convert.ToInt16(semesterNo) + ",@P_IDNO=" + Convert.ToInt16(idNo) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void Clear()
    {
        lblRegNo.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblName.Text = string.Empty;
        lblMName.Text = string.Empty;
        lblDOB.Text = string.Empty;
        lblCaste.Text = string.Empty ;
        lblCategory.Text = string.Empty;        
        lblReligion.Text = string.Empty;
        lblNationality.Text = string.Empty;
        lblLAdd.Text = string.Empty;
        lblLLNo.Text = string.Empty;
        lblMobNo.Text = string.Empty;
        lblPAdd.Text = string.Empty;
        imgPhoto.ImageUrl = null;
        lvRegStatus.DataSource = null;
        lvRegStatus.DataBind();
        lvAttendance.DataSource = null;
        lvAttendance.DataBind();
        lvResult.DataSource = null;
        lvResult.DataBind();
        lvFees.DataSource = null;
        lvFees.DataBind();
        lvCertificate.DataSource = null;
        lvCertificate.DataBind();
        lvTestMark.DataSource = null;
        lvTestMark.DataBind();
        lvRemark.DataSource = null;
        lvRemark.DataBind();
        lvRefund.DataSource = null;
        lvRefund.DataBind();
        lblSemester.Text = string.Empty;
    
    }
}

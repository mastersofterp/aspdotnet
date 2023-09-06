//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT_EXAM REGISTRATION                                      
// CREATION DATE : 10_SEP_2017
// ADDED BY      : AMIT BHUMBUR
// ADDED DATE    : 10_SEP_2017                                                
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class ACADEMIC_Student_ExamRegistration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FeeCollectionController feeController = new FeeCollectionController();
    StudentRegistration objSReg = new StudentRegistration();
    StudentRegist objSR = new StudentRegist();
    StudentController objSC = new StudentController();
    int cnt_registered = 0;
    int cnt_pending = 0;
    int cnt_total = 0;
    int flag = 0;
    int degreeno = 0;
    int branchno = 0;
    int semesterno = 0;
    int collegeid = 0;
    int activitystatus = 0;

    #region Page Load
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
        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

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
                ////Page Authorization
                //this.CheckPageAuthorization();
                if (Session["payment"].ToString().Equals("payment"))
                {
                    Page.Title = Session["coll_name"].ToString();
                }
                else
                { // Check User Authority 
                    this.CheckPageAuthorization();
                }

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                this.PopulateDropDownList();
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;


                IPADDRESS = ip.AddressList[0].ToString();
                //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["ipAddress"] = IPADDRESS;

                //Check for Activity On/Off for course registration.
                if (CheckActivity())
                {
                    ViewState["action"] = "add";
                    ViewState["idno"] = "0";
                    if (Session["usertype"].ToString().Equals("2"))     //Student 
                    {
                        int Activityflag = Convert.ToInt32(objCommon.LookUp("REFF", "ISNULL(FINAL_SEM_REGULAR_ACTIVITY,0)", ""));

                        if (Activityflag == 1)
                        {
                            DataSet DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
                            if (DS != null && DS.Tables.Count > 0)
                            {
                                degreeno = Convert.ToInt32(DS.Tables[0].Rows[0]["DEGREENO"].ToString());
                                branchno = Convert.ToInt32(DS.Tables[0].Rows[0]["BRANCHNO"].ToString());
                                semesterno = Convert.ToInt32(DS.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                                collegeid = Convert.ToInt32(DS.Tables[0].Rows[0]["COLLEGE_ID"].ToString());

                                int durationLastSem = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(DURATION_LAST_SEM,0)", "COLLEGE_ID=" + collegeid + " AND DEGREENO=" + degreeno + " AND BRANCHNO=" + branchno));
                                if (durationLastSem == semesterno)
                                {
                                    divOptions.Visible = false;
                                    ViewState["idno"] = Session["idno"].ToString();

                                    //this.ShowDetails();
                                    //BindStudentDetails();
                                    LoadStudentPanel();
                                }
                                else
                                {
                                    objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                                    return;
                                }
                            }
                        }
                        else
                        {

                            DataSet DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
                            if (DS != null && DS.Tables.Count > 0)
                            {
                                degreeno = Convert.ToInt32(DS.Tables[0].Rows[0]["DEGREENO"].ToString());
                                branchno = Convert.ToInt32(DS.Tables[0].Rows[0]["BRANCHNO"].ToString());
                                semesterno = Convert.ToInt32(DS.Tables[0].Rows[0]["SEMESTERNO"].ToString());
                                collegeid = Convert.ToInt32(DS.Tables[0].Rows[0]["COLLEGE_ID"].ToString());

                                int durationLastSem = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_DEGREE_BRANCH", "ISNULL(DURATION_LAST_SEM,0)", "COLLEGE_ID=" + collegeid + " AND DEGREENO=" + degreeno + " AND BRANCHNO=" + branchno));
                                if (durationLastSem != semesterno)
                                {
                                    divOptions.Visible = false;
                                    ViewState["idno"] = Session["idno"].ToString();

                                    //this.ShowDetails();
                                    //BindStudentDetails();
                                    LoadStudentPanel();
                                }
                                else
                                {
                                    objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                                    return;
                                }
                            }
                        }
                    }
                    //else if (Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("7"))     //Admin OR Operator 
                    else if (Session["usertype"].ToString().Equals("1"))
                    {
                        //divOptions.Visible = true;
                        LoadAdminPanel();
                    }
                    //else
                    //{
                    //    divOptions.Visible = true;
                    //    LoadFacultyPanel();
                    //}
                }
                else
                {
                    divCourses.Visible = false;

                    divOptions.Visible = false;
                }
                
            }
        }
        divMsg.InnerHtml = string.Empty;
        if (Session["usertype"].ToString().Equals("2"))
        {
            showstudentphoto();
            showstudentsignature();
            divphotosign.Visible = true;
            lblNote.Visible = true;
            btnSubmit.Text = "Submit";
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_ExamRegistration.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_ExamRegistration.aspx");
        }
    }

    private bool CheckActivity()
    {
        DataSet DS = objCommon.FillDropDown("ACD_STUDENT ", "SEMESTERNO,DEGREENO", "BRANCHNO,COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
        if (DS != null && DS.Tables.Count > 0)
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
                objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
                ret = false;
            }

            //if (dtr["PRE_REQ_ACT"] == DBNull.Value || dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            if (dtr["PRE_REQ_ACT"].ToString().ToLower().Equals("true"))
            {
                objCommon.DisplayMessage("Pre-Requisite Activity for this Page is Not Stopped!!", this.Page);
                ret = false;
            }
        }
        else
        {
            objCommon.DisplayMessage("Either this Activity has been Stopped Or You are Not Authorized to View this Page. Contact Admin.", this.Page);
            ret = false;
        }
        dtr.Close();
        return ret;
    }
    #endregion

    #region From Student Login
    protected void btnProceed_Click(object sender, EventArgs e)
    {
        if (Session["usertype"].ToString().Equals("2"))     //Student 
        {
            LoadStudentPanel();
        }
        else
        {
            LoadFacultyPanel();
        }
    }
    private void showstudentphoto()
    {
        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
        if (idno != "")
        {
            string imgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "photo", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));

            if (imgphoto == string.Empty)
            {
                imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            }
            else
            {
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + Session["idno"].ToString() + "&type=STUDENT";
            }

        }
        else
        {
            imgPhoto.ImageUrl = null;

        }
    }

    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }
    private void showstudentsignature()
    {
        string idno = objCommon.LookUp("ACD_STUD_PHOTO", "ISNULL(IDNO,0)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
        if (idno != "")
        {
            string signphoto = objCommon.LookUp("ACD_STUD_PHOTO", "stud_sign", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));

            if (signphoto == string.Empty)
            {

                ImgSign.ImageUrl = "~/images/sign11.jpg"; ;
            }
            else
            {
                ImgSign.ImageUrl = "~/showimage.aspx?id=" + Session["idno"].ToString() + "&type=STUDENTSIGN";
            }
        }
        else
        {
            ImgSign.ImageUrl = null;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int count_Backlog_idno = 0;
        int count_Regular_idno = 0;
        string backlogFine = "0.00";

        //**********************Photo and SIgn Uploade Start********************************************//   
        StudentController objEc = new StudentController();
        Student objstud = new Student();


        try
        {
            if (Session["usertype"].ToString().Equals("2"))
            {
                if (fuPhotoUpload.HasFile)
                {
                    string ext = System.IO.Path.GetExtension(fuPhotoUpload.PostedFile.FileName);
                    //if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".PNG" || ext.ToUpper().Trim() == ".JPEG" || ext.ToUpper().Trim() == ".GIF")
                    if (ext.ToUpper().Trim() == ".JPG" || ext.ToUpper().Trim() == ".JPEG")
                    {

                        //if (fuPhotoUpload.PostedFile.ContentLength < 25600)
                        if (fuPhotoUpload.PostedFile.ContentLength < 256000)
                        {

                            byte[] resizephoto = ResizePhoto(fuPhotoUpload);
                            if (resizephoto.LongLength >= 256000)
                            {
                                objCommon.DisplayMessage(this.updScheme, "Photo size must be less or equal to 250kb", this.Page);
                                return;
                            }
                            else
                            {
                                objstud.StudPhoto = this.ResizePhoto(fuPhotoUpload);
                                objstud.IdNo = Convert.ToInt32(Session["idno"].ToString());
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updScheme, "Image size must be less or equal to 250kb", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updScheme, "Only .JPG or .JPEG Images are allowed!", this.Page);
                        return;
                    }
                }
                else
                {
                  
                    string count = objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(IDNO)", "IDNO=" + Session["idno"].ToString() + " AND PHOTO IS NULL");
                    if (count != "0")
                    {
                        objCommon.DisplayMessage(this.updScheme, "Please Upload  Photo!", this.Page);
                        return;
                    }
                }

                if (fuSignUpload.HasFile)
                {
                    string ext1 = System.IO.Path.GetExtension(this.fuSignUpload.PostedFile.FileName);
                    if (ext1.ToUpper().Trim() == ".JPG" || ext1.ToUpper().Trim() == ".JPEG")
                    {
                        if (fuSignUpload.PostedFile.ContentLength < 256000)
                        {

                            byte[] resizephoto = ResizePhoto(fuSignUpload);

                            //if (resizephoto.LongLength >= 25600)
                            //if (resizephoto.LongLength >= 40960)
                            if (resizephoto.LongLength >= 256000)
                            {
                                objCommon.DisplayMessage(this.updScheme, "Signature size must be less or equal to 250kb", this.Page);
                                return;
                            }
                            else
                            {
                                objstud.StudSign = this.ResizePhoto(fuSignUpload);
                                objstud.IdNo = Convert.ToInt32(Session["idno"].ToString());
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.updScheme, "Signature size must be less or equal to 250kb", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updScheme, "Only .JPG or .JPEG Images are allowed!", this.Page);
                        return;
                    }

                    CustomStatus cs = (CustomStatus)objEc.UpdateStudPhoto(objstud);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        // objCommon.DisplayMessage(this.updScheme, "Student Photo upload Successfully!!", this.Page);
                        // showstudentphoto();

                    }
                    CustomStatus cs1 = (CustomStatus)objEc.UpdateStudSign(objstud);
                    if (cs1.Equals(CustomStatus.RecordUpdated))
                    {
                        //objCommon.DisplayMessage(this.updScheme, "Student Sign upload Successfully!!", this.Page);
                        // showstudentsignature();
                    }

                }
                else
                {
                   
                    string count = objCommon.LookUp("ACD_STUD_PHOTO", "COUNT(IDNO)", "IDNO=" + Session["idno"].ToString() + " AND STUD_SIGN IS NULL");
                    if (count != "0")
                    {
                        objCommon.DisplayMessage(this.updScheme, "Please Upload Signature!", this.Page);
                        return;
                    }

                }

            }
            //**********************Photo and SIgn Uploade End********************************************//       
            int sem = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + lblEnrollNo.Text + "'"));
            string exmcount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (exmcount != "0" && Convert.ToInt32(Session["usertype"]) == 2)
            {
                objCommon.DisplayMessage(this.updScheme, "Exam Form  is already Fillup", this.Page);
                //btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                return;
            }
            else
            {
                string exmcount1 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND IDNO=" + Convert.ToInt32(lblName.ToolTip) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (exmcount1 != "0")
                {
                    objCommon.DisplayMessage(this.updScheme, "Exam Form is already Confirmed.", this.Page);
                    //btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                    return;
                }
            }

            //StudentRegist objSR = new StudentRegist();

            foreach (ListViewDataItem dataitem in lvCurrentSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.COURSENOS = objSR.COURSENOS + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    string amt = (dataitem.FindControl("hdnCourseRegister") as HiddenField).Value.Trim();
                    objSR.CourseFee = objSR.CourseFee + (amt != string.Empty ? Convert.ToDecimal(amt) : 0);

                }
            }
            foreach (ListViewDataItem dataitem in lvBacklogSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.Backlog_course = objSR.Backlog_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
               
                    backlogFine = lblBacklogFine.Text;
                }
                //else
                //{
                //    lblBacklogFine.Text = "0";
                //}

            }
            //backlogFine = lblBacklogFine.Text;
            backlogFine = hdnFinalBacklogFine.Value;
            foreach (ListViewDataItem dataitem in lvReAppearedCourse.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.Re_Appeared = objSR.Re_Appeared + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    string amt = (dataitem.FindControl("hdnReAppearedCourse") as HiddenField).Value.Trim();
                    objSR.CourseFee = objSR.CourseFee + (amt != string.Empty ? Convert.ToDecimal(amt) : 0);
                }
            }
            foreach (ListViewDataItem dataitem in lvAuditSubjects.Items)
            {
                if ((dataitem.FindControl("chkAccept") as CheckBox).Checked == true)
                {
                    objSR.Audit_course = objSR.Audit_course + (dataitem.FindControl("lblCCode") as Label).ToolTip + "$";
                    string amt = (dataitem.FindControl("hdnAuditCourse") as HiddenField).Value.Trim();
                    objSR.CourseFee = objSR.CourseFee + (amt != string.Empty ? Convert.ToDecimal(amt) : 0);
                }
            }

            if (objSR.COURSENOS != null)
            {
                objSR.COURSENOS = objSR.COURSENOS.TrimEnd('$');
            }
            else
            {
                objSR.COURSENOS = "";
            }
            objSR.Backlog_course = objSR.Backlog_course.TrimEnd('$');
            objSR.Re_Appeared = objSR.Re_Appeared.TrimEnd('$');
            objSR.Audit_course = objSR.Audit_course.TrimEnd('$');

            if (ViewState["action"].ToString() == "add")
            {
                objSR.EXAM_REGISTERED = 0;
            }
            else
            {
                objSR.EXAM_REGISTERED = 1;
            }
            if (objSR.COURSENOS.Length > 0)
            {
                DataSet ds_status = objSReg.Get_Exam_Mark_Status(Convert.ToInt32(lblName.ToolTip), Convert.ToString(objSR.COURSENOS));
                if (ds_status != null && ds_status.Tables.Count > 0 && ds_status.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds_status.Tables[0].Rows.Count; i++)
                    {
                        if (ds_status.Tables[0].Rows[i]["MSTATUS"].ToString() == "0")
                        {
                            objCommon.DisplayMessage(this.updScheme, "Internal Mark Entry Not Found For Selected Courses. Kindly Contact to Your Department.", this.Page);
                            return;
                        }
                    }
                }
            }

            if (objSR.COURSENOS.Length > 0 || objSR.Backlog_course.Length > 0)
            {
                string studentIDs = lblName.ToolTip;

                //Add registered 
                objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
                objSR.IDNO = Convert.ToInt32(lblName.ToolTip);
                objSR.SEMESTERNO = Convert.ToInt32(lblSemester.ToolTip);
                objSR.SCHEMENO = Convert.ToInt32(lblScheme.ToolTip);
                objSR.IPADDRESS = Session["ipAddress"].ToString();
                objSR.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objSR.COLLEGE_CODE = Session["colcode"].ToString();
                objSR.REGNO = lblEnrollNo.Text.Trim();
                objSR.ROLLNO = txtRollNo.Text.Trim();
                //objSR.CommanFee = hdnCommanFee.Value.Trim() != string.Empty ? Convert.ToDecimal(hdnCommanFee.Value) : 0;
                objSR.LateFine = hdnLateFine.Value.Trim() != string.Empty ? Convert.ToDecimal(hdnLateFine.Value) : 0;
                objSR.CommanFee = lblCommanFee.Text.Trim() != string.Empty ? Convert.ToDecimal(lblCommanFee.Text) : 0;
              
                objSR.Backlogfees = Convert.ToDecimal(backlogFine);
                //objSR.TotalFee = objSR.CourseFee + objSR.CommanFee + objSR.LateFine + objSR.Backlogfees;
                objSR.TotalFee = Convert.ToDecimal(hdnRegFee.Value);
                // objSR.TotalFee = Convert.ToInt32(lblTotalFee.Text);
                objSR.ReceiptFlag = "EXM";

                int paymenttypenoOld = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
                FeeDemand demandCriteria = this.GetDemandCriteria();
                feeController.CreateDemandForExamination(demandCriteria, paymenttypenoOld, lblEnrollNo.Text.Trim(), Convert.ToDecimal(hdnRegFee.Value)); //Commented By Dileep on 10.03.2021 Not Required to create Demand.
                int ret = objSReg.AddExamRegiSubjects1(objSR);

                if (ret == 1)
                {
                    //objCommon.DisplayMessage(this.updScheme, "Exam registration done successfully. Kindly click on Online Payment button to paid Exam Fees.", this.Page);

                    if (Convert.ToInt32(Session["usertype"]) == 2)
                    {
                        //btnSubmit.Visible = false;
                        btnSubmit.Enabled = false;
                        objCommon.DisplayMessage(this.updScheme, "Your Provisional Exam Form Fill Up Done Successfully. Kindly Do the Online Payment for Confirmation of Exam Form.", this.Page);
                        //objCommon.DisplayMessage(this.updScheme, "Your Exam Form Fillup Application is Submitted to Your Department, Once They Will Approve it, You Can Make Online Payment For The Confirmation of Exam Registration.", this.Page);
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updScheme, "Exam Form Confirmation done Successfully.", this.Page);
                    }

                    ShowDetails();
               
                    btnSubmit.Enabled = false;
                                
                    txtRollNo.Enabled = true;
                    showstudentphoto();
                    showstudentsignature();
                    btnChallan.Visible = false;
                    //btnOnlinePayment.Visible = false;
                    //btnOnlinePayment.Visible = true;
                    btnOnlinePayment.Enabled = true;

                }
                else
                {
                    objCommon.DisplayMessage(this.updScheme, "Error! in saving record.", this.Page);
                }
                btnSubmit.Enabled = false;

            }
            else
            {
                objCommon.DisplayMessage(this.updScheme, "Please Select atleast One Course in course list for Exam Registration.", this.Page);
            }

            
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Private Methods
    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
        ddlSession.SelectedIndex = 1;
        //mrqSession.InnerHtml = "Registration Started for Session : " + (Convert.ToInt32(ddlSession.SelectedValue) > 0 ? ddlSession.SelectedItem.Text : "---");
        ddlSession.Focus();
    }

    private void LoadStudentPanel()
    {
        tblSession.Visible = false;
        string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(REGISTERED,0) = 1 AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(INCH_EXAM_REG,0)=1 AND IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        //string count = objCommon.LookUp("ACD_DCR", "COUNT(*)", "RECIEPT_CODE = 'EF' AND IDNO=" + ViewState["idno"].ToString() + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
        if (count != "0")
        {
            //objCommon.DisplayMessage("Your exam registration already done.", this.Page);
            this.ShowDetails();
            BindStudentDetails();
          
        }
        else
        {
            pnlFeeDetails.Visible = true;
            this.ShowDetails();
            BindStudentDetails();
        }


    }

    private void LoadFacultyPanel()
    {
        if (Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("7"))   ///|| Session["usertype"].ToString().Equals("1")
        {
            //objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1 and PAGE_LINK = " + Request.QueryString["pageno"].ToString() + ")", "SESSIONNO DESC");
            // objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND SESSIONNO IN ( SELECT SESSION_NO FROM SESSION_ACTIVITY SA INNER JOIN ACTIVITY_MASTER AM ON (SA.ACTIVITY_NO = AM.ACTIVITY_NO) WHERE STARTED = 1 AND SHOW_STATUS =1  AND UA_TYPE LIKE '%" + Session["usertype"].ToString() + "%' and PAGE_LINK LIKE '%" + Request.QueryString["pageno"].ToString() + "%')", "SESSIONNO DESC");
            objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 ", "SESSIONNO DESC");
            if (ddlSessionReg.Items.Count > 1)
            {
                ddlSessionReg.SelectedIndex = 1;
            }
            BindStudentList();
        }
        rblOptions.SelectedValue = "M";

        divCourses.Visible = false;
        pnlDept.Visible = true;
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = string.Empty;
        PopulateDropDownList();
    }
    private void LoadAdminPanel()
    {
        //if (Session["usertype"].ToString().Equals("7") || Session["usertype"].ToString().Equals("1"))
        if (Session["usertype"].ToString().Equals("1"))
        {
            objCommon.FillDropDownList(ddlSessionReg, "ACD_SESSION_MASTER", "DISTINCT SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0", "SESSIONNO DESC");
            if (ddlSessionReg.Items.Count > 1)
            {
                ddlSessionReg.SelectedIndex = 1;
            }
        }
        rblOptions.SelectedValue = "S";
        divOptions.Visible = false;
        txtRollNo.Text = string.Empty;

        divCourses.Visible = true;
        pnlDept.Visible = false;
        //btnBackHOD.Visible = false;
        tblInfo.Visible = false;
        tblSession.Visible = true;
        //btnSubmit.Visible = true;
        //btnSubmit.Enabled = true;
        divphotosign.Visible = false;
        lblNote.Visible = false;
        btnSubmit.Text = "Exam Fees Submission Confirmation";

    }
    #endregion

    #region From Faculty Advisor Login Single Student Search

    protected void rblOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";

        if (rblOptions.SelectedValue == "S")///For Single
        {

            divCourses.Visible = true;
            pnlDept.Visible = false;
            ddlSession.Enabled = false;
            txtRollNo.Text = string.Empty;
            tblInfo.Visible = false;
            tblSession.Visible = true;
            btnShow.Visible = true;
            btnCancel.Visible = true;
            txtRollNo.Enabled = true;
            // btnBackHOD.Visible = false;
        }
        else///For Multiple
        {

            divCourses.Visible = false;
            pnlDept.Visible = true;
            txtRollNo.Text = string.Empty;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        string idno = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + txtRollNo.Text.Trim() + "'" + "and admcan=0");
        string semesterno = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "REGNO = '" + txtRollNo.Text.Trim() + "'" + "and admcan=0");
        string stud_exam_count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "IDNO=" + idno + " AND SESSIONNO=" + ddlSession.SelectedValue + " AND ISNULL(STUD_EXAM_REGISTERED,0)=0 AND ISNULL(PREV_STATUS,0)=0 AND ISNULL(REGISTERED,0)=1 AND ISNULL(CANCEL,0)=0 AND SEMESTERNO=" + semesterno);
        if (Convert.ToInt32(stud_exam_count) > 0)
        {
            objCommon.DisplayUserMessage(this.Page, "Exam Form Fillup not done yet by Student, So no need to Confirm for this Student.", this.Page);
            //Response.Redirect(Request.Url.ToString());
            //Response.Redirect("~/Academic/Student_ExamRegistration.aspx");
            txtRollNo.Text = string.Empty;
            return;

        }

        if (idno == "")
        {
            objCommon.DisplayMessage("Student Not Found for Entered Registration No.[" + txtRollNo.Text.Trim() + "]", this.Page);
        }
        else
        {
            ViewState["idno"] = idno;

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }
            if ((Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("7")) ? true : ValidateFacultyAdvisor())
            {
                //Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    objCommon.DisplayMessage(this.updScheme, "Student with registration No. [" + txtRollNo.Text.Trim() + "] has not applied for selected session exam. \\nBut you can directly register him.", this.Page);
                    //return;
                }

                ViewState["action"] = "edit";
                this.ShowDetails();

                string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (count != "0")
                {
                    //objCommon.DisplayMessage(this.updScheme, "Exam registration is already confirmed. You can generate only registration slip.", this.Page);
                    //pnlFeeDetails.Visible = false;
                    // btnOnlinePayment.Visible = true;
                    //btnOnlinePayment.Enabled = true;
                    //btnSubmit.Enabled = true;
                    //btnSubmit.Visible = true;
                    //btnPrintRegSlip.Visible = true;
                    BindStudentDetails();
                }
                else
                {
                    pnlFeeDetails.Visible = true;
                    BindStudentDetails();
                }

                // btnBackHOD.Visible = false;
                txtRollNo.Enabled = false;
                ddlSession.Enabled = false;
                rblOptions.Enabled = false;
            }
        }
    }

    private bool ValidateFacultyAdvisor()
    {
        bool ret = true;
        //Validate Faculty Advisor
        int facAdvisor = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(FAC_ADVISOR,0)FAC_ADVISOR", "IDNO=" + ViewState["idno"].ToString()));

        if (facAdvisor != Convert.ToInt32(Session["userno"]))
        {
            objCommon.DisplayMessage("You did not have the permission to change selected student registration.\\nOnly alloted faculty advisor can do this.", this.Page);
            ret = false;
        }
        return ret;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            txtRollNo.Text = btnEdit.CommandArgument;
            txtRollNo.Enabled = false;

            ViewState["idno"] = objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO = '" + btnEdit.CommandArgument + "'");

            if (string.IsNullOrEmpty(ViewState["idno"].ToString()) || ViewState["idno"].ToString() == "0")
            {
                objCommon.DisplayMessage("Student with Registration No." + txtRollNo.Text.Trim() + " Not Exists!", this.Page);
                return;
            }

            if (ValidateFacultyAdvisor())
            {
                ViewState["action"] = "edit";
                this.ShowDetails();

                //Check current semester applied or not
                string applyCount = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(REGISTERED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
                if (applyCount == "0")
                {
                    objCommon.DisplayMessage("Student with registration No. [" + txtRollNo.Text.Trim() + "] has not applied for selected session exam. \\nBut you can directly register him.", this.Page);
                    //return;
                }

                BindStudentDetails();
                btnShow.Visible = false;
                btnCancel.Visible = false;
                pnlDept.Visible = false;
                tblSession.Visible = false;
                rblOptions.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";

        divCourses.Visible = true;
        pnlDept.Visible = false;
        ddlSession.Enabled = false;
        txtRollNo.Text = string.Empty;
        txtRollNo.Enabled = true;
        rblOptions.Enabled = true;

        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        lvReAppearedCourse.DataSource = null;
        lvReAppearedCourse.DataBind();
        lvAuditSubjects.DataSource = null;
        lvAuditSubjects.DataBind();

        tblInfo.Visible = false;
    }
    #endregion

    #region Private Methods
    private void ShowDetails()
    {
        try
        {
            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) INNER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO) LEFT OUTER JOIN ACD_EXAM_REG_FEES EF ON (S.IDNO = EF.IDNO AND EF.SESSIONNO = " + ddlSession.SelectedValue + " AND EF.RECEIPT_FLAG = 'EXM') ", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ENROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,(CASE S.PHYSICALLY_HANDICAPPED WHEN '0' THEN 'NO' WHEN '1' THEN 'YES' END) AS PH, ISNULL(COMMAN_FEE,0)COMMAN_FEE, ISNULL(COURSE_FEE,0)COURSE_FEE, ISNULL(LATE_FINE,0)LATE_FINE,ISNULL(BACKLOG_FEES,0)BACKLOG_FEES, ISNULL(TOTAL_AMT,0)TOTAL_AMT", "S.IDNO = " + ViewState["idno"].ToString(), string.Empty);
            int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
            Session["SemesternoRFpcf"] = semesterno;
            int schemetype = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO=SC.SCHEMENO)", "SCHEMETYPE", "regno='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
            int degreeno = Convert.ToInt32(objCommon.LookUp("acd_student", "degreeno", "regno='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
            int yearno = Convert.ToInt32(objCommon.LookUp("acd_student", "year", "regno='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
            int idno = Convert.ToInt32(dsStudent.Tables[0].Rows[0]["IDNO"].ToString());
            DataSet DsFees = objCommon.FillDropDown("ACD_DEMAND ", "IDNO,SEMESTERNO", "ISNULL(F1,0)REGULAR_FEES,ISNULL(F2,0)BACKLOG_FEES,ISNULL(TOTAL_AMT,0)TOTAL_FEES", "IDNO=" + idno + "AND SEMESTERNO=" + semesterno + " AND RECIEPT_CODE='EF'", "");
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();
                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();
                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();
                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString();

                    lblCommanFee.Text = dsStudent.Tables[0].Rows[0]["COMMAN_FEE"].ToString();
                    hdnCommanFee.Value = dsStudent.Tables[0].Rows[0]["COMMAN_FEE"].ToString();
                    hdnDefaultCommanFee.Value = dsStudent.Tables[0].Rows[0]["COMMAN_FEE"].ToString();

                    lblSelectedCourseFee.Text = dsStudent.Tables[0].Rows[0]["COURSE_FEE"].ToString();
                    hdnSelectedCourseFee.Value = dsStudent.Tables[0].Rows[0]["COURSE_FEE"].ToString();

                    lblLateFine.Text = dsStudent.Tables[0].Rows[0]["LATE_FINE"].ToString();
                    hdnLateFine.Value = dsStudent.Tables[0].Rows[0]["LATE_FINE"].ToString();

                    lblTotalFee.Text = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["TOTAL_FEES"].ToString() : "0";
                    hdnTotalFee.Value = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["TOTAL_FEES"].ToString() : "0";

                    lblBacklogFine.Text = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["BACKLOG_FEES"].ToString() : "0";
                    hdnBacklogFine.Value = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["BACKLOG_FEES"].ToString() : "0";

                    lblBacklogFee.Text = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["BACKLOG_FEES"].ToString() : "0";
                    // hdnBacklogFee.Value = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["BACKLOG_FEES"].ToString() : "0";

                    lblRegFee.Text = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["REGULAR_FEES"].ToString() : "0";
                    //hdnRegFee.Value = (DsFees.Tables[0].Rows.Count > 0) ? DsFees.Tables[0].Rows[0]["REGULAR_FEES"].ToString() : "0";

                    tblInfo.Visible = true;
                    divCourses.Visible = true;
                    txtnonCBCSSem.Text = lblSemester.ToolTip;


                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindStudentDetails()
    {
        //btnSubmit.Visible = true;
        //btnSubmit.Enabled = true;
        //btnPrintRegSlip.Visible = true;

        int sessionno = (Session["usertype"] != "2") ? Convert.ToInt32(ddlSession.SelectedValue) : Convert.ToInt32(Session["currentsession"].ToString());
        string Detained_Count = objCommon.LookUp("ACD_STUDENT_RESULT", "ISNULL(COUNT(IDNO),0)", "IDNO=" + ViewState["idno"] + " AND SESSIONNO=" + sessionno + " AND ISNULL(DETAIND,0)=1");
        if (Convert.ToInt32(Detained_Count) > 0)
        {
            objCommon.DisplayMessage(updScheme, "You are not eligible for Exam Form Fill Up ,May be you are Detained.Kindly contact with your Department.", this.Page);
            //btnSubmit.Visible = false;
            btnSubmit.Enabled = false;
            pnlFeeDetails.Visible = false;
            return;
       
        }
        BindAvailableCourseList();
        BindStudentFailedCourseList(); // Remove comment dated on 23032021 by swapnil

        DateTime PenaltyDate = Convert.ToDateTime(objCommon.LookUp("REFF", "CAST(UPTODATE AS DATE)UPTODATE", ""));
        // objRef.PenaltyDate = Convert.ToDateTime(txtpenaltyDate.Text.Trim()); 
        DataSet ds_LateFine = objSReg.Get_Courses_LateFine(PenaltyDate);

        lblLateFine.Text = ds_LateFine.Tables[0].Rows[0]["LATE_FINE"].ToString();
        hdnLateFine.Value = ds_LateFine.Tables[0].Rows[0]["LATE_FINE"].ToString();
        if (lvCurrentSubjects.Visible == true || lvBacklogSubjects.Visible == true || lvAuditSubjects.Visible == true)
        {
            //btnSubmit.Visible = true;
            btnSubmit.Enabled = true;
        }
        else
        {
            //btnSubmit.Visible = false;
            btnSubmit.Enabled = false;
        }

      
        if (flag == 0)
        {
            //if (activitystatus == 1)
            //{
            //    btnSubmit.Enabled = false;
            //}
            //string count1 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //if (count1 != "0")
            //{
            //    objCommon.DisplayMessage(this.Page, "Exam Form is already confirmed. You can generate only registration slip.", this.Page);
              
            //    btnSubmit.Enabled = false;
              
            //    btnPrintRegSlip.Enabled = true;
               
            //    btnOnlinePayment.Enabled = false;
               
            //    btnPaymentSlip.Visible = true;
            //    return;
            //}

            //Check current semester registered or not  //PREV_STATUS = 0 and 
            //string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(INCH_EXAM_REG,0)=1 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //if (count != "0")
            //{
            //    if (activitystatus == 0)
            //    {
            //        if (Convert.ToInt32(Session["usertype"]) == 2)
            //        {
                        
            //            btnSubmit.Enabled = false;
            //            objCommon.DisplayMessage(this.Page, "Your Exam Form Fillup Application is Approved by The Department, You Can Now Make Online Payment for The Confirmation of Exam Registration And Can Download The Admit Card.", this.Page);

            //        }
            //        else
            //        {
                        
            //            btnSubmit.Enabled = true;
            //            objCommon.DisplayMessage(this.Page, "Exam Form already Fill Up by Student.", this.Page);

            //        }
                   
            //        btnPrintRegSlip.Enabled = false;
                  
            //        if (Convert.ToInt32(ViewState["PaymentEndDate"]) > 0)
            //        {
            //            objCommon.DisplayMessage(this.Page, "Payment Activity has been stopped. You can generate only registration slip if Online Payment is done.", this.Page);
            //            return;

            //        }
            //        else
            //        {
            //            btnOnlinePayment.Enabled = true;
            //        }
            //    }
            //    else
            //    {
            //        btnSubmit.Enabled = false;
            //        btnPrintRegSlip.Enabled = false;
                  
            //        if (Convert.ToInt32(ViewState["PaymentEndDate"]) > 0)
            //        {
            //            objCommon.DisplayMessage(this.Page, "Payment Activity has been stopped. You can generate only registration slip if Online Payment is done.", this.Page);
            //            return;
                        
            //        }
            //        else
            //        {
            //            btnOnlinePayment.Enabled = true;
            //        }
            //    }

            //}


            //Check current semester registered or not  //PREV_STATUS = 0 and 
            //string count2 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(INCH_EXAM_REG,0)=0 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            //if (count2 != "0")
            //{
            //    if (activitystatus == 0)
            //    {
            //        if (Convert.ToInt32(Session["usertype"]) == 2)
            //        {
                    
            //            btnSubmit.Enabled = false;
            //            objCommon.DisplayMessage(this.Page, "Your Exam Form Fillup Application Already Submitted to Your Department, Once They Will Approve it, You Can Make Online Payment For The Confirmation of Exam Registration.", this.Page);

            //        }
            //        else
            //        {
            //            btnSubmit.Enabled = true;
            //            objCommon.DisplayMessage(this.Page, "Exam Form already Fill Up by Student.", this.Page);

            //        }
                  
            //        btnPrintRegSlip.Enabled = false;
                  
            //        btnOnlinePayment.Enabled = false;
            //    }
            //    else
            //    {
            //        btnPrintRegSlip.Enabled = false;
            //        btnOnlinePayment.Enabled = false;
            //    }

            //}

            string count1 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (count1 != "0")
            {
                objCommon.DisplayMessage("Exam Form is already confirmed. You can generate only registration slip.", this.Page);
                //btnSubmit.Visible = false;
                btnSubmit.Enabled = false;
                //btnPrintRegSlip.Visible = true;
                btnPrintRegSlip.Enabled = true;
                //btnOnlinePayment.Visible = false;
                //btnOnlinePayment.Visible = false; // Added by Swapnil 23022021 for Online Payment
                btnOnlinePayment.Enabled = false;
                //btnPaymentSlip.Visible = true;
                btnPaymentSlip.Visible = true;
                return;
            }

            //Check current semester registered or not  //PREV_STATUS = 0 and 
            string count = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(INCH_EXAM_REG,0)=1 AND ISNULL(STUD_EXAM_REGISTERED,0) = 1 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (count != "0")
            {
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    //btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage(this.Page, "Your Provisional Exam Form Already Fill Up. Kindly Do the Online Payment for Confirmation of Exam Form.", this.Page);

                }
                else
                {
                    //btnSubmit.Visible = true;

                    btnSubmit.Enabled = true;
                    objCommon.DisplayMessage(this.Page, "Exam Form already Fill Up by Student.", this.Page);

                }
                //  btnSubmit.Visible = false;
                // btnPrintRegSlip.Visible = false;
                btnPrintRegSlip.Enabled = false;
                //btnOnlinePayment.Visible = true;
                btnOnlinePayment.Enabled = true;

            }

            string count2 = objCommon.LookUp("ACD_STUDENT_RESULT", "COUNT(CCODE)", "ISNULL(CANCEL,0)=0 AND ISNULL(ACCEPTED,0) = 1 AND IDNO=" + ViewState["idno"] + " AND ISNULL(INCH_EXAM_REG,0)=0 AND ISNULL(PREV_STATUS,0)=0 AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue));
            if (count2 != "0")
            {
                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    //btnSubmit.Visible = false;
                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage(this.Page, "Your registration Approval not done by HOD, Kindly contact to your department.", this.Page);

                }
                else
                {
                    //btnSubmit.Visible = true;

                    btnSubmit.Enabled = false;
                    objCommon.DisplayMessage(this.Page, "Student Exam Approval not done by HOD, Kindly contact to department.", this.Page);

                }
                //  btnSubmit.Visible = false;
                // btnPrintRegSlip.Visible = false;
                btnPrintRegSlip.Enabled = false;
                //btnOnlinePayment.Visible = true;
                btnOnlinePayment.Enabled = false;

            }

           

        }
        
    }

    private void BindAvailableCourseList()
    {
        DataSet dsCurrCourses = null;
        //Show Current Semester Courses ..
        //dsCurrCourses = objCommon.FillDropDown("ACD_STUDENT_RESULT C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSENAME,C.SUBID,C.ELECT,CAST(C.CREDITS AS INT) CREDITS,S.SUBNAME, ISNULL(REGISTERED,0)REGISTERED, ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED, DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER ", "C.SCHEMENO = " + lblScheme.ToolTip + " AND C.SEMESTERNO = " + lblSemester.ToolTip + "AND ACCEPTED = 1 AND ISNULL(AUDIT_COURSE,0)=0 AND SESSIONNO = " + ddlSession.SelectedValue + "AND IDNO="+lblName.ToolTip, "C.CCODE"); /// + " AND C.OFFERED = 1"
        int schemeno = Convert.ToInt32(objCommon.LookUp("acd_student", "schemeno", "idno=" + Convert.ToInt32(ViewState["idno"])));
        int semester = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", " ADMCAN=0 AND idno=" + Convert.ToInt32(ViewState["idno"])));
        dsCurrCourses = objSReg.GetStudentCoursesForRegularRegistration1(Convert.ToInt32(ViewState["idno"]), Convert.ToInt16(ddlSession.SelectedValue), schemeno, semester, 2);
        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvCurrentSubjects.DataSource = dsCurrCourses.Tables[0];
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = true;

            //if (Session["usertype"].ToString().Equals("2"))     //Student 
            //if (lblTotalFee.Text.Equals("0.00") || lblTotalFee.Text.Equals("0"))
            //{
            //    lblCommanFee.Text = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
            //    hdnCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();

            //    lblLateFine.Text = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();
            //    hdnLateFine.Value = dsCurrCourses.Tables[0].Rows[0]["TOTAL_LATE_FEES"].ToString();

            //}
            //hdnDefaultCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
        }
        else
        {
            lvCurrentSubjects.DataSource = null;
            lvCurrentSubjects.DataBind();
            lvCurrentSubjects.Visible = false;
            //objCommon.DisplayMessage("No Course found in Allotted Scheme and Semester.", this.Page);
        }
    }

    private void BindStudentFailedCourseList()
    {
        DataSet dsCurrCourses = null;
        int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", " ADMCAN=0 AND REGNO='" + txtRollNo.Text + "'" + "or idno=" + ViewState["idno"] + ""));
        //Show Backlog Semester Courses ..
        dsCurrCourses = objSReg.GetStudentCoursesForBacklogRegistration1(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), semesterno, 2);
        DataSet dsFee = objCommon.FillDropDown("ACD_EXAM_FEE_CONFIG", "REG_FAMT", "BACKLOG_FAMTSEM1,BACKLOG_FAMTSEM2,BACKLOG_FAMTSEM3,BACKLOG_FAMTSEM4,BACKLOG_FAMTSEM5,BACKLOG_FAMTSEM6,BACKLOG_FAMTSEM7,BACKLOG_FAMTSEM8", "", "");

        if (dsCurrCourses != null && dsCurrCourses.Tables.Count > 0 && dsCurrCourses.Tables[0].Rows.Count > 0)
        {
            lvBacklogSubjects.DataSource = dsCurrCourses.Tables[0];
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = true;
            hdnBacklogCount.Value = dsCurrCourses.Tables[0].Rows.Count.ToString();
            //trBacklogFee.Visible = false;
            //pnlBacklogFee.Visible = true;


            hdnDefaultCommanFee.Value = dsCurrCourses.Tables[0].Rows[0]["EXAM_COMMAN_FEE"].ToString();
            hdnBacklogFine.Value = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();
            txtnew.Text = dsCurrCourses.Tables[0].Rows[0]["BACKLOG_FEES"].ToString();

            flag = 1;
        }

        else
        {
            lvBacklogSubjects.DataSource = null;
            lvBacklogSubjects.DataBind();
            lvBacklogSubjects.Visible = false;
            // trBacklogFee.Visible = true;
            //pnlBacklogFee.Visible = false;
            if (dsFee != null && dsFee.Tables.Count > 0 && dsFee.Tables[0].Rows.Count > 0)
            {
                //lblRegFee.Text = dsFee.Tables[0].Rows[0]["REG_FAMT"].ToString();
                lblBacklogFee.Text = "0";
            }
        }
        if (dsFee != null && dsFee.Tables.Count > 0 && dsFee.Tables[0].Rows.Count > 0)
        {
            //lblBacklogSem1Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM1"].ToString();
            //lblBacklogSem2Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM2"].ToString();
            //lblBacklogSem3Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM3"].ToString();
            //lblBacklogSem4Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM4"].ToString();
            //lblBacklogSem5Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM5"].ToString();
            //lblBacklogSem6Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM6"].ToString();
            //lblBacklogSem7Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM7"].ToString();
            //lblBacklogSem8Fee.Text = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM8"].ToString();

            //lblRegFee.Text = dsFee.Tables[0].Rows[0]["REG_FAMT"].ToString();
            hdnRegFee.Value = dsFee.Tables[0].Rows[0]["REG_FAMT"].ToString();
            hdnBacklogFeeSem1.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM1"].ToString();
            hdnBacklogFeeSem2.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM2"].ToString();
            hdnBacklogFeeSem3.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM3"].ToString();
            hdnBacklogFeeSem4.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM4"].ToString();
            hdnBacklogFeeSem5.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM5"].ToString();
            hdnBacklogFeeSem6.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM6"].ToString();
            hdnBacklogFeeSem7.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM7"].ToString();
            hdnBacklogFeeSem8.Value = dsFee.Tables[0].Rows[0]["BACKLOG_FAMTSEM8"].ToString();
        }
    }

    private void BindReAppearedCourseList()
    {
        DataSet dsReappearCourse = null;
        //Show ReAppeared Course List
        dsReappearCourse = objSReg.GetStudentCoursesForReAppearedCourseRegistration1(Convert.ToInt16(ddlSession.SelectedValue), Convert.ToInt32(ViewState["idno"]), 0, 2);
        if (dsReappearCourse != null && dsReappearCourse.Tables.Count > 0 && dsReappearCourse.Tables[0].Rows.Count > 0)
        {
            lvReAppearedCourse.DataSource = dsReappearCourse.Tables[0];
            lvReAppearedCourse.DataBind();
            lvReAppearedCourse.Visible = true;
        }
        else
        {
            lvReAppearedCourse.DataSource = null;
            lvReAppearedCourse.DataBind();
            lvReAppearedCourse.Visible = false;
        }
    }

    private void BindAuditCourseList()
    {
        DataSet dsAuditCourse = null;
        //Show Audit Courses ..
        //dsAuditCourse = objCommon.FillDropDown("ACD_STUDENT_RESULT C INNER JOIN ACD_SUBJECTTYPE S ON (C.SUBID = S.SUBID)", "DISTINCT C.COURSENO", "C.CCODE,C.COURSENAME,C.SUBID,C.ELECT,0 as CREDITS,S.SUBNAME, ISNULL(REGISTERED,0)REGISTERED, ISNULL(EXAM_REGISTERED,0)EXAM_REGISTERED,DBO.FN_DESC('SEMESTER',C.SEMESTERNO)SEMESTER", "IDNO = " + ViewState["idno"] + " AND SESSIONNO = " + ddlSession.SelectedValue + " AND ISNULL(ACCEPTED,0)=1 AND ISNULL(AUDIT_COURSE,0)=1 AND ISNULL(PREV_STATUS,0)=0", "C.CCODE");
        dsAuditCourse = objSReg.GetStudentCoursesForAuditRegistration1(Convert.ToInt32(ViewState["idno"]), Convert.ToInt16(ddlSession.SelectedValue), 2);
        if (dsAuditCourse != null && dsAuditCourse.Tables.Count > 0 && dsAuditCourse.Tables[0].Rows.Count > 0)
        {
            lvAuditSubjects.DataSource = dsAuditCourse.Tables[0];
            lvAuditSubjects.DataBind();
            lvAuditSubjects.Visible = true;
        }
        else
        {
            lvAuditSubjects.DataSource = null;
            lvAuditSubjects.DataBind();
            lvAuditSubjects.Visible = false;
        }
    }

    #endregion

    protected void btnPrintRegSlip_Click(object sender, EventArgs e)
    {
        ShowReport("ExamRegistrationSlip", "rptExamRegForm.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
        int idno = Convert.ToInt32(lblName.ToolTip);
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + sessionno + ",@P_OPT_FLAG=2";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updScheme, this.updScheme.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region Faculty Advisor Accepting Student Registration

    private void BindStudentList()
    {
        try
        {
            if (ddlSessionReg.SelectedValue == "0")
            {
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                return;
            }

            StudentRegistration objSRegist = new StudentRegistration();
            DataSet dsStudent = objSRegist.GetCourseRegStudentList1(Convert.ToInt32(ViewState["idno"]), Convert.ToInt32(Session["usertype"]), Convert.ToInt32(Session["dec"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(ddlSessionReg.SelectedValue), 2);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lvStudent.DataSource = dsStudent.Tables[0];
                    lvStudent.DataBind();
                    lblRegistered.Text = cnt_registered.ToString();
                    lblPending.Text = cnt_pending.ToString();
                    lblTotal.Text = cnt_total.ToString();
                }
                else
                {
                    objCommon.DisplayMessage("Students Not Registered for selected Session.", this.Page);
                    lvStudent.DataSource = null;
                    lvStudent.DataBind();
                    lblRegistered.Text = string.Empty;
                    lblPending.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                }
            }
            else
            {
                objCommon.DisplayMessage("Students Not Registered for selected Session.", this.Page);
                lvStudent.DataSource = null;
                lvStudent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_courseRegistration.BindStudentList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnBackHOD_Click(object sender, EventArgs e)
    {
        ViewState["idno"] = "0";
        btnShow.Visible = false;
        pnlDept.Visible = true;
        divCourses.Visible = false;
        lvCurrentSubjects.DataSource = null;
        lvCurrentSubjects.DataBind();
        lvBacklogSubjects.DataSource = null;
        lvBacklogSubjects.DataBind();
        lvAuditSubjects.DataSource = null;
        lvAuditSubjects.DataBind();
        this.BindStudentList();

        txtRollNo.Text = string.Empty;
        lblAdmBatch.Text = string.Empty;
        lblBranch.Text = string.Empty;
        lblEnrollNo.Text = string.Empty;
        lblFatherName.Text = string.Empty;
        lblMotherName.Text = string.Empty;
        lblName.Text = string.Empty;
        lblPH.Text = string.Empty;
        lblScheme.Text = string.Empty;
        lblSemester.Text = string.Empty;
        rblOptions.Enabled = true;
    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lbl = e.Item.FindControl("lblStatus") as Label;
        if (lbl.Text.ToUpper() == "REGISTERED")
        {
            lbl.Style.Add("color", "Green");
            cnt_registered++;
        }
        else
        {
            lbl.Style.Add("color", "Red");
            cnt_pending++;
        }
        cnt_total++;
    }

    private FeeDemand GetDemandCriteria()
    {
        FeeDemand demandCriteria = new FeeDemand();
        // StudentRegist objSR = new StudentRegist();
        Student objS = new Student();
        int branchno = Convert.ToInt32(objCommon.LookUp("acd_student", "branchno", "regno='" + lblEnrollNo.Text + "'"));
        int degreeno = Convert.ToInt32(objCommon.LookUp("acd_student", "degreeno", "regno='" + lblEnrollNo.Text + "'"));
        int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + lblEnrollNo.Text + "'"));
        int admbatch = Convert.ToInt32(objCommon.LookUp("acd_student", "ADMBATCH", "regno='" + lblEnrollNo.Text + "'"));
        int paymenttypeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "PTYPE", "regno='" + lblEnrollNo.Text + "'"));
        //lblEnrollNo
        Session["SemesternoRFpcf"] = semesterno;
        try
        {
            demandCriteria.StudentId = Convert.ToInt32(ViewState["idno"]);
            demandCriteria.StudentName = lblName.Text;
            demandCriteria.SessionNo = Convert.ToInt32(ddlSession.SelectedValue);
            demandCriteria.ReceiptTypeCode = "EF";
            demandCriteria.BranchNo = branchno;
            demandCriteria.DegreeNo = degreeno;
            demandCriteria.SemesterNo = semesterno;
            demandCriteria.AdmBatchNo = admbatch;
            demandCriteria.PaymentTypeNo = paymenttypeno;
            demandCriteria.UserNo = int.Parse(Session["userno"].ToString());
            demandCriteria.CollegeCode = Session["colcode"].ToString();
            demandCriteria.BacklogFees = objSR.Backlog_course.Length > 0 ? Convert.ToDouble(hdnBacklogFee.Value) : 0;
            demandCriteria.RegularFees = objSR.COURSENOS.Length > 0 ? Convert.ToDouble(hdnRegFee.Value) : 0;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_EaxmRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return demandCriteria;
    }

    #endregion

    protected void btnChallan_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    StudentController objSC = new StudentController();
        //    Student objS = new Student();
        //    string rec_code = "EF";
        //    objS.SessionNo = Convert.ToInt32(Session["currentsession"]);
        //    objS.IdNo = Convert.ToInt32(ViewState["idno"]);
        //    int semesterno = Convert.ToInt32(objCommon.LookUp("acd_student", "semesterno", "regno='" + lblEnrollNo.Text + "'"));
        //    string recptno = GetReceiptNo();
        //    objSC.InsertExamIntoDcr(objS, recptno, semesterno, rec_code);
        //    string dcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(ViewState["idno"]) + " AND Sessionno = " + Convert.ToInt32(ddlSession.SelectedValue) + " and reciept_code='" + rec_code + "'");
        //    if (dcrNo != string.Empty)
        //    {
        //        this.ShowReportStudChalan("ExamFeesChallanReport", "Exam_Fee_Collection_Receipt.rpt");
        //    }
        //    //btnChallan.Enabled = false;
        //}
        //catch (Exception ex)
        //{
        //    if (Convert.ToBoolean(Session["error"]) == true)
        //        objCommon.ShowError(Page, "ACADEMIC_Student_EaxmRegistration.GetDemandCriteria() --> " + ex.Message + " " + ex.StackTrace);
        //    else
        //        objCommon.ShowError(Page, "Server Unavailable.");
        //}
    }

    private string GetReceiptNo()
    {
        string receiptNo = string.Empty;
        //StudentController objSC = new StudentController();
        Student objS = new Student();
        try
        {

            string REC_NO = string.Empty;
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            objS.IdNo = IDNO;
            // String reciptCode = objCommon.LookUp("ACD_DEMAND", "RECIEPT_CODE", "idno=" + IDNO + "and sessionno=" + Convert.ToInt32(Session["currentsession"]));
            String reciptCode = "EF";
            DataSet ds = objSC.GetNewReceiptData("B", 1, reciptCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                String FeesSessionStartDate;
                FeesSessionStartDate = objCommon.LookUp("REFF", "RIGHT(year(Start_Year),2)", "");
                DataRow dr = ds.Tables[0].Rows[0];
                string increment_id = objCommon.LookUp("ACD_DCR", "ISNULL(MAX(DCR_NO),0)DCR_NO", "");
                if (increment_id != string.Empty || increment_id != "0")
                {
                    int id = Convert.ToInt32(increment_id) + 1;

                    receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + reciptCode + "/" + FeesSessionStartDate + "/" + id;
                }
                else
                {
                    int id = 0;
                    receiptNo = dr["PRINTNAME"].ToString() + "/" + "B" + "/" + reciptCode + "/" + FeesSessionStartDate + "/" + id;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetNewReceiptNo() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return receiptNo;
    }

    private void ShowReportStudChalan(string reportTitle, string rptFileName)
    {

        try
        {
            int sessionno = Convert.ToInt32(Session["currentsession"]);
            string receipt_code = "EF";
            int IDNO = Convert.ToInt32(ViewState["idno"]);
            int collg_code = Convert.ToInt32(objCommon.LookUp("reff", "College_code", ""));
            int DCRNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + IDNO + "and SESSIONNO = " + Convert.ToInt32(Session["currentsession"]) + " AND reciept_code='" + receipt_code + "'"));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + collg_code + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + DCRNO;
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //        //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.upDegree, this.upDegree.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_StudentRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }




    // Online Payment Button
    protected void btnOnlinePayment_Click(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ViewState["PaymentEndDate"]) > 0)
        {
            objCommon.DisplayMessage(this.Page, "Payment Activity has been stopped. You can generate only registration slip if Online Payment is done.", this.Page);
            return;

        }
      


        int IDNO = 0;
        Session["rectype"] = "EF";
        Session["rectypebackexm"] = "";
        Session["onlinepaysession"] = ddlSession.SelectedValue;

        if (Session["usertype"].ToString().Equals("2"))     //Student 
        {
            IDNO = Convert.ToInt32(Session["idno"].ToString());
        }
        else
        {
            IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "IDNO", "REGNO='" + txtRollNo.Text + "'or enrollno='" + txtRollNo.Text + "'"));
        }

        Session["idno"] = IDNO;
        int demand = Convert.ToInt32(objCommon.LookUp("ACD_DEMAND D INNER JOIN ACD_SESSION_MASTER S ON D.SESSIONNO=S.SESSIONNO", "COUNT(1)", "idno='" + IDNO + "'AND D.SESSIONNO='" + Convert.ToInt32(Session["onlinepaysession"].ToString()) + "' AND D.RECIEPT_CODE='" + Session["rectype"].ToString() + "'"));
        int rcon = Convert.ToInt32(objCommon.LookUp("ACD_DCR D INNER JOIN ACD_SESSION_MASTER S ON D.SESSIONNO=S.SESSIONNO", "COUNT(1)", "idno='" + IDNO + "'AND FLOCK=0 AND RECON=1 AND D.SESSIONNO='" + Convert.ToInt32(Session["onlinepaysession"].ToString()) + "' AND RECIEPT_CODE='" + Session["rectype"].ToString() + "'"));

        if (demand <= 0)
        {
            objCommon.DisplayMessage(" Exam Fees Demand is not Created for Current Year", this.Page);
            return;
        }
        else if (rcon > 0)
        {
            objCommon.DisplayMessage("Your Payment already done for current year", this.Page);
            //btnOnlinePayment.Visible = false;
            //btnOnlinePayment.Visible = true; // Added by Swapnil 23022021 for Online Payment
            btnOnlinePayment.Enabled = false;
            // btnPrintRegSlip.Visible = true;
            btnPrintRegSlip.Enabled = true;
            btnPaymentSlip.Visible = true;

            return;
        }
        else
        {
            // Response.Redirect("~/StudentInfoSearch.aspx");
            //Response.Redirect("~/ONLINEPAYMENT/StudentInfoSearch.aspx");
            DataSet ds = objSC.Get_StudentDataOnlinePayment(IDNO, Convert.ToInt32(ddlSession.SelectedValue), Session["rectype"].ToString(), Convert.ToInt32(Session["SemesternoRFpcf"]));
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    int status1 = 0;
                    int Currency = 1;
                    double amount = 0;
                    amount = Convert.ToDouble(ds.Tables[0].Rows[0]["TOTAL_AMT"].ToString());

                    string UserId = Convert.ToString(Session["userno"]);
                    if (Session["userno"] == null)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                Reprocess:
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    Random ram = new Random();
                    int i = ram.Next(1, 9);
                    int j = ram.Next(21, 51);
                    int k = ram.Next(471, 999);
                    int l = System.DateTime.Today.Day;
                    int m = System.DateTime.Today.Month;
                    string refno = (i + "" + j + "" + k + "" + l + "" + m).ToString() + '-' + UserId;

                    string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + refno + "'");

                    if (str1 != "" || str1 != string.Empty)
                    {
                        goto Reprocess;
                    }

                    //string str1 = "select * from feerefT where refno='" + refno + "'";
                    //DataTable dtg = Class1.GetData(str1);
                    //if (dtg.Rows.Count > 0)
                    //{
                    //    goto Reprocess;
                    //}

                    string PaymentMode = "EXAM FEES COLLECTION";
                    Session["PaymentMode"] = PaymentMode;
                    Session["studAmt"] = amount;
                    ViewState["studAmt"] = amount;//hdnTotalCashAmt.Value;
                    Session["studName"] = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    Session["studPhone"] = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    Session["studEmail"] = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    Session["studrefno"] = refno;
                    Session["ReceiptType"] = "EF";
                    Session["idno"] = IDNO;
                    Session["homelink"] = "Student_ExamRegistration.aspx";
                    string datetm = indianTime.ToString("dd-MMM-yyyy");
                    string status = "Not Continued";

                    feeController.InsertOnlinePaymentlog(Convert.ToString(IDNO), Session["ReceiptType"].ToString(), PaymentMode, Convert.ToString(amount), status, refno);

                    int result = feeController.InsertOnlinePayment_TempDCR(Convert.ToInt32(IDNO), Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(Session["SemesternoRFpcf"]), refno, 1, "EF", "");

                    if (result > 0)
                    {
                        Response.Write(datetm);

                        //Response.Redirect("https://makauttest.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavExamRequest.aspx", false);
                        Response.Redirect("https://makaut.mastersofterp.in/ACADEMIC/ONLINEFEECOLLECTION/ccavExamRequest.aspx", false);
                        //Response.Redirect("http://localhost:62778/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavExamRequest.aspx");

                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        //Response.Redirect("http://localhost:60903/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/ccavExamRequest.aspx");

                    }
                    else
                    {
                        objCommon.DisplayMessage("Online Payment Not Done, Please Try Again..!!", this.Page);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
    protected void btnPaymentSlip_Click(object sender, EventArgs e)
    {
        ShowOnlineReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
    }
    private void ShowOnlineReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = 0;
            if (tblSession.Visible == false)
            {
                IDNO = Convert.ToInt32(Session["idno"] == string.Empty ? "0" : Session["idno"].ToString());
            }
            else
            {
                IDNO = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtRollNo.Text + "'") == "" ? "0" : objCommon.LookUp("ACD_STUDENT", "ISNULL(IDNO,0)", "ENROLLNO='" + txtRollNo.Text + "'"));

            }
            int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO=" + Convert.ToInt32(IDNO) + " AND SEMESTERNO=" + Convert.ToInt32(Session["SemesternoRFpcf"]) + " AND PAY_TYPE = 'O' and PAY_SERVICE_TYPE = 1 AND RECIEPT_CODE = 'EF'"));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            //divMSG.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMSG.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMSG.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updScheme, this.updScheme.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
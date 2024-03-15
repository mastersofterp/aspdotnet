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
using System.Net;
using System.Security;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
//using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using SendGrid;
using System.IO;
public partial class User : System.Web.UI.Page
{
    IITMS.UAIMS.Common objCommon = new IITMS.UAIMS.Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpCreateController objECC = new EmpCreateController();
    private static string isServer = System.Configuration.ConfigurationManager.AppSettings["isServer"].ToString();
    string defaultPage = "";
    public int intEditflag = 0;
    private byte[] _Photo = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (isServer == "true")
        {
            defaultPage = "~/default.aspx";
        }
        else
        {
            defaultPage = "~/default_crescent.aspx";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        setSessionEmpPhoto();



        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect(defaultPage);
            }


            int userno = Convert.ToInt32(Session["userno"]);
            string username = Convert.ToString(Session["username"]);
            int usertype = Convert.ToInt32(Session["usertype"]);
            string userfullname = Convert.ToString(Session["userfullname"]);
            lblId.Text = Session["idno"].ToString();
            if (usertype == 2)
            {
                string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Session["idno"].ToString());

                lblstudid.Text = REGNO.ToString();
                txtstudregno.Text = REGNO.ToString();
                pnlstudprof.Visible = true;
                pnlempprof.Visible = false;
                GetStudentInfo();
            }
            else
            {
                lblId.Text = Session["idno"].ToString();
                pnlstudprof.Visible = false;
                pnlempprof.Visible = true;
                GetEmpInfo();
                //BindProjectList();

            }

            BindImage();

        }

    }

    private void getBase64String()
    {
        DataSet ds = objCommon.FillDropDown("PAYROLL_EMP_PHOTO", "IDNO", "PHOTO", "IDNO=49", "IDNO");
        //string empimgphoto = objCommon.LookUp("PAYROLL_EMP_PHOTO", "PHOTO", "IDNO=49");
        ////byte[] imgData =(byte[]) ds.Tables[0].Columns[0]["PHOTO"];
        byte[] imgData = (byte[])ds.Tables[0].Rows[0]["PHOTO"];
        //imgData = Encoding.ASCII.GetBytes(empimgphoto); 
        if (imgData != null)
        {
            ////imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(Session["idno"]) + "&type=emp";
            imgEmpPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
            ViewState["PHOTO"] = imgData;
        }
    }

    private void setSessionEmpPhoto()
    {
        try
        {
            byte[] imgData = null;

            // store the FileUpload object in Session. 
            // "FileUpload1" is the ID of your FileUpload control
            // This condition occurs for first time you upload a file
            if (Session["fuimgEmpPhoto"] == null && fuimgEmpPhoto.HasFile)
            {
                Session["fuimgEmpPhoto"] = fuimgEmpPhoto;

            }
            // This condition will occur on next postbacks        
            else if (Session["fuimgEmpPhoto"] != null && (!fuimgEmpPhoto.HasFile))
            {
                fuimgEmpPhoto = (FileUpload)Session["fuimgEmpPhoto"];

            }
            //  when Session will have File but user want to change the file 
            // i.e. wants to upload a new file using same FileUpload control
            // so update the session to have the newly uploaded file
            else if (fuimgEmpPhoto.HasFile)
            {
                Session["fuimgEmpPhoto"] = fuimgEmpPhoto;

            }

            if (fuimgEmpPhoto.HasFile)
            {
                imgData = ResizePhoto(fuimgEmpPhoto);
                if (imgData != null)
                {
                    //imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(Session["IDNO"]) + "&type=emp";
                    imgEmpPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    ViewState["PHOTO"] = imgData;
                }
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "lnkUserProfile.setSessionEmpPhoto-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void GetEmpInfo()
    {
        int idnoacc = Convert.ToInt32(Session["IDNO"]);
        int usernoacc = Convert.ToInt32(Session["userno"]);
        DataTableReader dtr = objECC.GetEmpInfo(usernoacc);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                lblId.Text = dtr["PFILENO"].ToString();
                //txtIdNo.Text = dtr["idno"].ToString();
                lblName.Text = dtr["UA_FULLNAME"].ToString();
                lblDesignation.Text = dtr["DESIGNATION"].ToString();
                lblDept.Text = dtr["DEPARTMENT"].ToString();
                //if (dtr["PHONENO"].ToString() == "" || dtr["PHONENO"].ToString() == string.Empty)
                //{
                string PhoneNo = objCommon.LookUp("USER_ACC", "ISNULL(UA_MOBILE,'')UA_MOBILE", "UA_NO=" + Convert.ToInt32(Session["userno"]) + "");
                if (string.IsNullOrEmpty(PhoneNo) || PhoneNo.ToString() == "" || PhoneNo.ToString() == string.Empty)
                {
                    txtPhoneNo.Text = "0";
                }
                else
                {
                    txtPhoneNo.Text = PhoneNo.Trim();

                }
                //}
                //else
                //{
                //    txtPhoneNo.Text = dtr["PHONENO"].ToString();
                //}
                if (dtr["EMAILID"].ToString() == "" || dtr["EMAILID"].ToString() == string.Empty)
                {
                    string EmailId = objCommon.LookUp("USER_ACC", "UA_EMAIL", "UA_NO=" + Convert.ToInt32(Session["userno"]) + "");
                    txtEmail.Text = EmailId;
                }
                else
                {
                    txtEmail.Text = dtr["EMAILID"].ToString();
                }

                txtBlock1.Text = dtr["BLOCKNO1"].ToString();
                txtBlock2.Text = dtr["BLOCKNO2"].ToString();
                txtRoomNo1.Text = dtr["ROOMNO1"].ToString();
                txtRoomNo2.Text = dtr["ROOMNO2"].ToString();
                txtCabinNo1.Text = dtr["CABBINNO1"].ToString();
                txtCabinNo2.Text = dtr["CABBINNO2"].ToString();
                txtIntercomNo1.Text = dtr["INTERCOMNO1"].ToString();
                txtIntercomNo2.Text = dtr["INTERCOMNO2"].ToString();
                txtSP1.Text = dtr["SPEC_1"].ToString();
                txtSP2.Text = dtr["SPEC_2"].ToString();
                txtSP3.Text = dtr["SPEC_3"].ToString();
                txtSP4.Text = dtr["SPEC_4"].ToString();
                txtSP5.Text = dtr["SPEC_5"].ToString();

                //  string studimgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO=" + Convert.ToInt32(Session["idno"]));

                ViewState["PHOTO"] = null;
                string empimgphoto = objCommon.LookUp("PAYROLL_EMP_PHOTO", "PHOTO", "IDNO=" + Convert.ToInt32(Session["IDNO"]));
                ////byte[] imgData = null;
                if (empimgphoto == string.Empty)
                {
                    imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
                }
                else
                {
                    ////imgEmpPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    imgEmpPhoto.ImageUrl = "~/showimage.aspx?id=" + Convert.ToInt32(Session["IDNO"]) + "&type=emp";

                    ViewState["PHOTO"] = empimgphoto;
                }
                lblUserLoginId.Text = dtr["UA_NAME"].ToString();
            }
            divnotemsg.Style.Add("display", "block");
            //fuimgEmpPhoto.Enabled = false;

        }

    }

    public void GetStudentInfo()
    {
        int usernoacc = Convert.ToInt32(Session["userno"]);
        DataTableReader dtr = objECC.GetStudInfo(usernoacc);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //txtIdNo.Text = dtr["idno"].ToString();
                lblstudsem.Text = dtr["SEMESTERNAME"].ToString();
                txtstudbranch.Text = dtr["LONGNAME"].ToString();
                txtstudphone.Text = dtr["PHONENO"].ToString();
                txtstudemail.Text = dtr["UA_EMAIL"].ToString();
                txtstudname.Text = dtr["UA_FULLNAME"].ToString();
                Session["studemail"] = txtstudemail.Text;

                string studimgphoto = objCommon.LookUp("ACD_STUD_PHOTO", "PHOTO", "IDNO=" + Convert.ToInt32(Session["idno"]));

                if (studimgphoto == string.Empty)
                {
                    imgStudPhoto.ImageUrl = "~/images/nophoto.jpg";
                }
                else
                {

                    imgStudPhoto.ImageUrl = "~/showimage.aspx?id=" + lblId.Text + "&type=STUDENT";
                }
            }
        }

    }

    protected void BindImage()
    {

        try
        {
          

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

              

                DataSet ds = objCommon.FillDropDown("PAYROLL_EMP_PHOTO", "IDNO", "PHOTO", "IDNO=" + Convert.ToInt32(Session["idno"]), "");
                byte[] imgData = (byte[])ds.Tables[0].Rows[0]["PHOTO"];

                if (imgData != null)
                {
                    imgEmpPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                    ViewState["PHOTO"] = imgData;
                }
                else
                {
                    imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
                }
            }
            else
            {
               
                DataTableReader dtr = null;
                string type = "";
                if (Convert.ToInt32(Session["usertype"]) == 2)
                    type = "STUDENT";
                else if (Convert.ToInt32(Session["usertype"]) == 3)
                    type = "EMP";
                else if (Convert.ToInt32(Session["usertype"]) != 1 || Convert.ToInt32(Session["usertype"]) != 2 || Convert.ToInt32(Session["usertype"]) != 3)
                    type = "EMP";
                dtr = objECC.getUserProfileImage(Session["idno"].ToString(), type);

                if (dtr.Read())
                {
                    if (dtr["PHOTO"].ToString() == "")
                    {

                        imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
                    }
                    else
                    {
                        byte[] imgData = null;
                        imgData = dtr["PHOTO"] as byte[];

                        if (imgData != null)
                        {
                            imgEmpPhoto.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                            ViewState["PHOTO"] = imgData;
                        }
                        else
                        {
                            imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
                        }
                    }
                }
                else
                {
                    imgEmpPhoto.ImageUrl = "~/images/nophoto.jpg";
                }
            }
        }
        catch (Exception ex)
        {
        }

    }


    protected void btnEdit_Click(object sender, EventArgs e)
    {
        divFileUpload.Style.Add("display", "block");
        txtPhoneNo.Enabled = false;
        txtEmail.Enabled = false;
        txtBlock1.Enabled = true;
        txtBlock2.Enabled = true;
        txtCabinNo1.Enabled = true;
        txtCabinNo2.Enabled = true;
        txtDetailProject.Enabled = true;
        txtIntercomNo1.Enabled = true;
        txtIntercomNo2.Enabled = true;
        txtProFromDate.Enabled = true;
        txtProToDate.Enabled = true;
        txtRoomNo1.Enabled = true;
        txtRoomNo2.Enabled = true;
        txtProjectName.Enabled = true;
        txtSP1.Enabled = true;
        txtSP2.Enabled = true;
        txtSP3.Enabled = true;
        txtSP4.Enabled = true;
        txtSP5.Enabled = true;
        txtSponsoredBy.Enabled = true;
        lblMsg.Visible = false;
        btnEdit.Visible = false;
        btnOtpF.Visible = true;
        btnUpdate.Visible = false;
        trOtpF.Visible = false;
        divnotemsg.Style.Add("display", "none");
        divnote.Style.Add("display", "block");
    }
    public void Default()
    {

    }
    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);

            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream as Stream);
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

            //Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["otp"].ToString() == txtOtpF.Text.Trim())
            {
                ViewState["otp"] = null;
                int usernoacc = Convert.ToInt32(Session["userno"]);
                int idnoacc = Convert.ToInt32(Session["IDNO"]);

                bool isPhotoUploaded = false;
                string message = string.Empty;

                if (fuimgEmpPhoto.HasFile || ViewState["PHOTO"] != null)
                {
                    isPhotoUploaded = true;
                }
                setSessionEmpPhoto();
                string photo = "";
                if (fuimgEmpPhoto.HasFile || ViewState["PHOTO"] != null)
                {
                    byte[] resizephoto = (byte[])ViewState["PHOTO"];
                    if (resizephoto.LongLength >= 500000)
                    {
                        objCommon.DisplayMessage("File size should be less than or equal to 500 KB.", this.Page);
                        return;
                    }
                    else
                    {
                        _Photo = resizephoto;
                    }

                    photo = "Photo";
                }
                else
                {
                    if (ViewState["PHOTO"] == null)
                    {
                        objCommon.DisplayMessage("Please select Photo.", this.Page);
                        return;
                    }
                    else
                    {
                        _Photo = null;
                        _Photo = ViewState["PHOTO"] as byte[];
                    }
                }

                CustomStatus cs = (CustomStatus)objECC.UpdateEmpInfo(usernoacc, txtPhoneNo.Text.Trim(), txtEmail.Text.Trim(), txtBlock1.Text.Trim(), txtBlock2.Text.Trim(), txtRoomNo1.Text.Trim(), txtRoomNo2.Text.Trim(), txtCabinNo1.Text.Trim(), txtCabinNo2.Text.Trim(), txtIntercomNo1.Text.Trim(), txtIntercomNo2.Text.Trim(), txtSP1.Text.Trim(), txtSP2.Text.Trim(), txtSP3.Text.Trim(), txtSP4.Text.Trim(), txtSP5.Text.Trim(), _Photo, idnoacc);//txtPAdress.Text, txtLAddress.Text,
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Record Updated...";
                    MessageBox("Record Updated Successully...");
                    //Response.Redirect(Request.Url.ToString());
                    divnoteOTP.Style.Add("display", "none");
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                }
                else
                {
                    lblMsg.Text = "Transaction Failed!!";

                }

                GetEmpInfo();
                BindImage();
                txtPhoneNo.Enabled = false;
                txtEmail.Enabled = false;
                txtBlock1.Enabled = false;
                txtBlock2.Enabled = false;
                txtCabinNo1.Enabled = false;
                txtCabinNo2.Enabled = false;
                txtDetailProject.Enabled = false;
                txtIntercomNo1.Enabled = false;
                txtIntercomNo2.Enabled = false;
                txtProFromDate.Enabled = false;
                txtProToDate.Enabled = false;
                txtRoomNo1.Enabled = false;
                txtRoomNo2.Enabled = false;
                txtProjectName.Enabled = false;
                txtSP1.Enabled = false;
                txtSP2.Enabled = false;
                txtSP3.Enabled = false;
                txtSP4.Enabled = false;
                txtSP5.Enabled = false;


                txtSponsoredBy.Enabled = false;
                trOtpF.Visible = false;
                //txtPAdress.Enabled = false;
                //txtLAddress.Enabled = false;
                //txtUName.Enabled = false;
                intEditflag = 0;

                divFileUpload.Style.Add("display", "none");
            }
            else
            {
                //lblMessage.Text = "Pleae enter correct OTP.";
                MessageBox("Please enter correct OTP.");
                return;
            }

            ViewState["PHOTO"] = null;
            fuimgEmpPhoto.Dispose();
            txtOtpF.Text = string.Empty;
        }
        catch (Exception ex)
        {

        }
    }
    void DisplayMessage(string Message)
    {
        string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
        string message = string.Format(prompt, Message);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Confirmation", message, false);
    }
    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void btneditStud_Click(object sender, EventArgs e)
    {
        idOtp.Visible = true;
        btneditStud.Visible = false;
        txtstudemail.Enabled = false;
        txtstudphone.Enabled = false;
        //txtstudname.Enabled = true;
        //txtstudbranch.Enabled = false;
        //lblstudsem.Enabled = false;
        //txtstudname.Enabled = true;
        btnverify.Visible = true;
        btnUpdStud.Visible = false;
        idOtp.Visible = false;
        studivotp.Style.Add("display", "block");

    }
    protected void btnUpdStud_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["otp"].ToString() == txtotp.Text.Trim())
            {
                ViewState["otp"] = null;
                int usernoacc = Convert.ToInt32(Session["userno"]);
                int idnoacc = Convert.ToInt32(Session["IDNO"]);
                CustomStatus cs = (CustomStatus)objECC.UpdateStuInfo(usernoacc, txtstudphone.Text.Trim(), txtstudemail.Text.Trim());
                //, txtstudname.Text.Trim());
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Record Updated...";
                    MessageBox("Record Updated Successfully...");
                    btnUpdStud.Visible = false;
                    btneditStud.Visible = true;
                    idOtp.Visible = false;
                    studivmsg.Style.Add("display", "none");
                }
                else
                {
                    lblMsg.Text = "Transaction Failed";

                }
                GetStudentInfo();
                txtstudphone.Enabled = false;
                txtstudemail.Enabled = false;
                txtstudname.Enabled = false;
            }
            else
            {
                //lblMessage.Text = "Pleae enter correct OTP.";
                MessageBox("Please enter correct OTP.");
                return;
            }
            txtotp.Text = string.Empty;
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkchangepwd_Click(object sender, EventArgs e)
    {
        pnlchangepwd.Visible = true;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        txtOldPassword.Text = string.Empty;
        txtNewPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        pnlchangepwd.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNewPassword.Text.Trim() == string.Empty || txtOldPassword.Text.Trim() == string.Empty || txtConfirmPassword.Text.Trim() == string.Empty)
            {
                //lblMessage.Text = "Blank password is not allowed";
                MessageBox("Blank password is not allowed");
            }
            else
            {
                User_AccController objUC = new User_AccController();
                UserAcc objUA = new UserAcc();
                objUA.UA_Name = Session["username"].ToString();
                objUA.UA_No = Convert.ToInt32(Session["userno"].ToString());
                objUA.UA_Pwd = txtNewPassword.Text.Trim();
                objUA.UA_OldPwd = txtOldPassword.Text.Trim();

                //objUA.UA_Pwd = Common.EncryptPassword(txtNewPassword.Text.Trim());
                //objUA.UA_OldPwd = Common.EncryptPassword(txtOldPassword.Text.Trim());

                CustomStatus cs = (CustomStatus)objUC.ChangePasswordByadmin(objUA);

                if (cs.Equals(CustomStatus.InvalidUserNamePassword))
                {
                    //lblMessage.Text = "Invalid Old Password";
                    MessageBox("Invalid Old Password");
                }
                else
                {
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        if (Convert.ToInt32(Session["usertype"]) == 2 && Convert.ToString(Session["firstlog"]) == "False")
                        {
                            MessageBox("Password Modified Successfully");
                            txtConfirmPassword.Text = string.Empty;
                            txtNewPassword.Text = string.Empty;
                            txtOldPassword.Text = string.Empty;
                            Response.Redirect(defaultPage);
                        }
                        else
                        {
                            MessageBox("Password Modified Successfully");
                            txtConfirmPassword.Text = string.Empty;
                            txtNewPassword.Text = string.Empty;
                            txtOldPassword.Text = string.Empty;
                        }



                        //lblMessage.Text = "Password Modified Successfully";
                        //objCommon.DisplayMessage(UpdatePanel1, "Password Modified Successfully", this);
                        //txtConfirmPassword.Text = string.Empty;
                        //txtNewPassword.Text = string.Empty;
                        //txtOldPassword.Text = string.Empty;

                        //if (Request.QueryString["status"] != null)
                        //{
                        //    if (Request.QueryString["status"].ToString().Equals("firstlog"))
                        //    {
                        //        //update the firstlog status
                        //        objUC.UpdateFirstLogin(Session["username"].ToString());
                        //    }
                        //}
                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                lblMessage.Text = "Invalid Old Password";
            else
                lblMessage.Text = "Server UnAvailable";
        }
    }
    //Function For Bind Listview of Projects .
    public void BindProjectList()
    {
        DataSet ds = objCommon.FillDropDown("FACULTY_PROJECTS", "FPNO", "PROJECT_NAME,SPONSORED_BY,CONVERT(NVARCHAR(30), FROM_DATE, 103)AS FROM_DATE,CONVERT(NVARCHAR(30), TO_DATE, 103)AS TO_DATE,PROJECT_DETAIL ", "ua_no=" + Convert.ToInt32(Session["userno"]) + "", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            pnlProjectList.Visible = true;
            lvProjects.DataSource = ds;
            lvProjects.DataBind();
        }
        else
        {
            pnlProjectList.Visible = false;
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            //if (btnAdd.Text == "Add")
            //{
            //    int usernoacc = Convert.ToInt32(Session["userno"]);
            //    CustomStatus cs = (CustomStatus)objECC.AddProjectDetails(usernoacc, txtProjectName.Text, txtSponsoredBy.Text, Convert.ToDateTime(txtProFromDate.Text), Convert.ToDateTime(txtProToDate.Text), txtDetailProject.Text);//txtPAdress.Text, txtLAddress.Text,
            //    if (cs.Equals(CustomStatus.RecordUpdated))
            //    {

            //        MessageBox("Project Inserted Successully...");
            //        BindProjectList();
            //        txtProjectName.Text = string.Empty;
            //        txtSponsoredBy.Text = string.Empty;
            //        txtProFromDate.Text = string.Empty;
            //        txtProToDate.Text = string.Empty;
            //        txtDetailProject.Text = string.Empty;

            //    }


            //    else
            //    {
            //        lblMsg.Text = "Transaction Failed";

            //    }
            //}
            //else
            //{
            //    if (btnAdd.Text == "Update")
            //    {
            //        CustomStatus cs = (CustomStatus)objECC.UpdateProjectDetails(Convert.ToInt16( ViewState["exdtno"].ToString()) , txtProjectName.Text, txtSponsoredBy.Text, Convert.ToDateTime(txtProFromDate.Text), Convert.ToDateTime(txtProToDate.Text), txtDetailProject.Text);//txtPAdress.Text, txtLAddress.Text,
            //        if (cs.Equals(CustomStatus.RecordUpdated))
            //        {   

            //            MessageBox("Project Update Successully...");
            //            BindProjectList();
            //            txtProjectName.Text = string.Empty;
            //            txtSponsoredBy.Text = string.Empty;
            //            txtProFromDate.Text = string.Empty;
            //            txtProToDate.Text = string.Empty;
            //            txtDetailProject.Text = string.Empty;
            //            btnAdd.Text = "Add";

            //        }
            //        else
            //        {
            //            lblMsg.Text = "Transaction Failed";

            //        }
            //    }

            //}

        }
        catch (Exception ex)
        {

        }
    }
    //Function For edit the Selected Project Form List.
    protected void btnEditProject_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;

            DataSet ds = objCommon.FillDropDown("FACULTY_PROJECTS", "FPNO", "PROJECT_NAME,SPONSORED_BY,CONVERT(NVARCHAR(30), FROM_DATE, 103)AS FROM_DATE,CONVERT(NVARCHAR(30), TO_DATE, 103)AS TO_DATE,PROJECT_DETAIL ", "FPNO=" + int.Parse(btnEditRecord.CommandArgument) + "", "");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["exdtno"] = btnEditRecord.CommandArgument;
                    txtProjectName.Text = ds.Tables[0].Rows[0]["PROJECT_NAME"].ToString();
                    txtSponsoredBy.Text = ds.Tables[0].Rows[0]["SPONSORED_BY"].ToString();
                    txtProFromDate.Text = ds.Tables[0].Rows[0]["FROM_DATE"].ToString();
                    txtProToDate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                    txtDetailProject.Text = ds.Tables[0].Rows[0]["PROJECT_DETAIL"].ToString();

                }
            }
            btnAdd.Text = "Update";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Emp_Profile.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    //Function For Deleting the Selected Project Form List.
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            EmpCreateController objEXM = new EmpCreateController();
            ImageButton btnDelete = sender as ImageButton;
            int FPNO = int.Parse(btnDelete.CommandArgument);


            //CustomStatus cs = (CustomStatus)objEXM.DeleteProject(FPNO);
            objCommon.DisplayMessage("Project Deleted Succesfully !!", this.Page);
            this.BindProjectList();
            return;



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_EMP_PROFILE.btnDelete_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private string GeneartePassword()
    {
        string allowedChars = "";
        //   allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
        //   allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string passwordString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            passwordString += temp;
        }
        return passwordString;
    }
    public void TransferToEmail(string mailid)
    {
        try
        {
            MailMessage msg = new MailMessage();
            string message = string.Empty;
            string emailid = string.Empty;
            string subject = string.Empty;
            string password = string.Empty;
            string studname = string.Empty;
            studname = txtstudname.Text.Trim();
            emailid = mailid;
            DataSet dsconfig = null;

            string pwd = GeneartePassword();
            password = pwd;
            ViewState["otp"] = password;
            //dsconfig = objCommon.FillDropDown("Email_Configuration", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName,SUBJECT_OTP", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            string decrFromPwd = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

            string collegename = dsconfig.Tables[0].Rows[0]["CollegeName"].ToString();
            msg.Subject = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() + " || OTP for reset password";
            subject = msg.Subject;
            message = "<html><body>" +
                                     "<div align=\"center\">" +
                                     "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                      "<tr>" +
                                      "<td>" + "</tr>" +
                                      "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\"><h1>Greetings !!</h1><br/>Dear <b>" + studname + "</b>,<br/><p>THANK YOU FOR EDITING INFORMATION</p><br /><p>WELCOME TO " + collegename.ToString().ToUpper() + " </p><br/><b>YOUR OTP FOR USER PROFILE IS:" + password + "</b></td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>With Best Wishes<br/>" + dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() + "</td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</div>" +
                                     "</body></html>";




            //added by tejas jaiswal
            int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
            int status = 0;

            if (SENDGRID_Status == 1)
            {
                //status = SendMailBYSendgrid(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
                //status = sendEmail(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
                //TransferToEmail(txtstudemail.Text.Trim());
                Task<int> task = Execute(message, emailid, subject);
                status = task.Result;

            }
            else
            {
                status = sendEmail(message, emailid, subject);

            }
            if (status == 1)
            {
                MessageBox("Your OTP has been successfully sent on your registerd e-mail id.");
            }

            else
            {
                MessageBox("Failed to Send OTP");
                //Showmessage("Failed to send email");
            }

            MessageBox("Your OTP has been successfully sent on your registerd e-mail id.");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "EmpPrpofile.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void btnverify_Click(object sender, EventArgs e)
    {

        MailMessage msg = new MailMessage();
        string message = string.Empty;
        string emailid = string.Empty;
        string subject = string.Empty;
        string password = string.Empty;
        string studname = string.Empty;
        studname = txtstudname.Text.Trim();
        emailid = txtstudemail.Text.Trim().Replace("'", "");
        DataSet dsconfig = null;

        string pwd = GeneartePassword();
        password = pwd;
        ViewState["otp"] = password;
        //dsconfig = objCommon.FillDropDown("Email_Configuration", "EMAILSVCID", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName,SUBJECT_OTP", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
        string fromAddress = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
        string decrFromPwd = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

        string collegename = dsconfig.Tables[0].Rows[0]["CollegeName"].ToString();
        msg.Subject = dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() + " || OTP for reset password";
        subject = msg.Subject;
        message = "<html><body>" +
                         "<div align=\"center\">" +
                         "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                          "<tr>" +
                          "<td>" + "</tr>" +
                          "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\"><h1>Greetings !!</h1><br/>Dear <b>" + studname + "</b>,<br/><p>THANK YOU FOR EDITING INFORMATION</p><br /><p>WELCOME TO " + collegename.ToString().ToUpper() + " </p><br/><b>YOUR OTP FOR USER PROFILE IS:" + password + "</b></td>" +
                         "</tr>" +
                         "<tr>" +
                         "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>With Best Wishes<br/><b>" + dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() + "</b></td>" +  //added by tejas jaiswal
                         "</tr>" +
                         "</table>" +
                         "</div>" +
                         "</body></html>";
        //System.Text.StringBuilder mailBody = new StringBuilder();
        //message+="<h1>Greetings !!</h1>";
        // message+="Dear <b>{0}</b>,"+ studname;
        // message+="<br />";
        // message+="<br />";
        // message+="<p>THANK YOU FOR EDITING INFORMATION</p>";
        // message+="<br />";
        // message+="<p>WELCOME TO " + collegename + " </p>";
        // message+="<br />";
        // message+="<b>YOUR OTP FOR USER PROFILE IS:" + password + "</b>";
        // message+="<br />";
        //mailBody.AppendFormat("APPLICATION ID : <b>" + username + "</b>");
        //mailBody.AppendFormat("<br />");
        //mailBody.AppendFormat("OTP : <b>" + password + "</b>");
        //string Mailbody = mailBody.ToString();
        //string nMailbody = message.Replace("#content", Mailbody);
        //msg.IsBodyHtml = true;
        //msg.Body = nMailbody;
        //message = msg.Body;



        //added by tejas jaiswal
        int SENDGRID_Status = int.Parse(objCommon.LookUp("reff", "SENDGRID_Status", string.Empty));
        int status = 0;

        if (SENDGRID_Status == 1)
        {
            //status = SendMailBYSendgrid(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
            //status = sendEmail(Message, txtEmailId.Text, "SBU ERP || OTP for reset password");
            //TransferToEmail(txtstudemail.Text.Trim());
            Task<int> task = Execute(message, emailid, subject);
            status = task.Result;

        }
        else
        {
            status = sendEmail(message, emailid, subject);

        }
        if (status == 1)
        {
            objCommon.DisplayMessage("OTP has been sent to Your Email Id, Enter To Continue Reset Password Process.", this);
        }

        else
        {
            objCommon.DisplayMessage("Failed to send email", this);
            //Showmessage("Failed to send email");
        }


        //status = task.Result;
        idOtp.Visible = true;
        txtstudphone.Enabled = false;
        txtstudemail.Enabled = false;
        txtstudname.Enabled = false;
        btnverify.Visible = false;
        btnUpdStud.Visible = true;
        studivotp.Style.Add("display", "none");
        studivmsg.Style.Add("display", "block");

    }
    protected void btnOtpF_Click(object sender, EventArgs e)
    {
        TransferToEmail(txtEmail.Text.Trim());
        divnote.Style.Add("display", "none");
        divnoteOTP.Style.Add("display", "block");
        trOtpF.Visible = true;
        txtPhoneNo.Enabled = false;
        txtEmail.Enabled = false;
        txtBlock1.Enabled = false;
        txtBlock2.Enabled = false;
        txtCabinNo1.Enabled = false;
        txtCabinNo2.Enabled = false;
        txtDetailProject.Enabled = false;
        txtIntercomNo1.Enabled = false;
        txtIntercomNo2.Enabled = false;
        txtProFromDate.Enabled = false;
        txtProToDate.Enabled = false;
        txtRoomNo1.Enabled = false;
        txtRoomNo2.Enabled = false;
        txtProjectName.Enabled = false;
        txtSP1.Enabled = false;
        txtSP2.Enabled = false;
        txtSP3.Enabled = false;
        txtSP4.Enabled = false;
        txtSP5.Enabled = false;

        txtSponsoredBy.Enabled = false;
        trOtpF.Visible = true;
        btnOtpF.Visible = false;
        btnUpdate.Visible = true;


        setSessionEmpPhoto();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> Preview()();</script>", false);
    }

    protected void btnempbtncancel_Click(object sender, EventArgs e)
    {
        // Response.Redirect(Request.Url.ToString());
        ////Response.Redirect("~/home.aspx");
        if (Session["usertype"].ToString() == "3")
        {
            Response.Redirect("~/homeFaculty.aspx", false);
        }
        else if (Session["usertype"].ToString() == "2")
        {
            Response.Redirect("~/studeHome.aspx", false);
        }
        else
        {
            Response.Redirect("~/principalHome.aspx");
        }
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub)
    {
        int ret = 0;

        try
        {
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new MailAddress(toEmailId, "");
            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = client.SendEmailAsync(msg);
            //var response = await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            ret = 0;
        }
        return ret;
    }


    public int sendEmail(string Message, string toEmailId, string sub)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName,SUBJECT_OTP", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
            var toAddress = new MailAddress(toEmailId, "");
            // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
            string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = sub,
                Body = Message,
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default,
                IsBodyHtml = true
            })
            {
                //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(message);
                return ret = 1;
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            ret = 0;
        }
        return ret;
    }

}
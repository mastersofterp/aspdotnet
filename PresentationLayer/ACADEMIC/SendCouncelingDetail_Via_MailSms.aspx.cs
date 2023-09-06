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
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using SendGrid;
public partial class ACADEMIC_SendCouncelingDetail_Via_MailSms : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SmsController objSmsC = new SmsController();
    StudentController objSC = new StudentController();

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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
              // empPanel.Visible = true;
               // objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID > 0", "USERTYPEID");
                objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
            }

            cancel();

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SendCouncelingDetail_Via_MailSms.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SendCouncelingDetail_Via_MailSms.aspx");
        }
    }

    protected void btnSndSms_Click(object sender, EventArgs e)
    {
        if (rdbMessage.SelectedValue == "Mail")
            if (txtSubject.Text.Trim() == string.Empty)
            {
                objCommon.DisplayMessage("Please Enter Text Subject.", this.Page);
                return;
            }

        string ua_no = string.Empty;
        string Mobile = null;
        // ----------------------------Add By Maithili [23-07-2019]-------------------------//
        //if (ddlUserType.SelectedValue == "2")
        //{
            if(Convert.ToInt32(ddlBatch.SelectedValue) > 0)
            {
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                Label lblMobile = item.FindControl("lblMobile") as Label;
                Label lblEmail = item.FindControl("lblEmail") as Label;
                Label lblFeeStatus = item.FindControl("lblfee_status") as Label;
                Label lblusername  = item.FindControl("lblusername") as Label;
                Label lblFname = item.FindControl("lblFname") as Label;
                Session["SSName"] = lblFname.Text;
                string i = lblFeeStatus.Text;
                if (chek.Checked == true)
                {
                    if (rdbMessage.SelectedValue == "SMS")
                        {
                            SMS(txtMessage.Text, lblMobile.Text);
                            ua_no += chek.ToolTip + ",";
                            Mobile += lblMobile.Text + ",";
                        }
                        else if (rdbMessage.SelectedValue == "Mail")
                        {
                            if (lblEmail.Text != string.Empty)
                            {
                                bool status = SendMailBYSendgridForgetPassword(lblEmail.Text, txtMessage.Text, txtSubject.Text);
                                if (status == true)
                                {
                                    ua_no += chek.ToolTip + ",";
                                    Mobile += lblMobile.Text + ",";
                                }
                            }

                        }
                    
                }
            }
            }
       
        if (ua_no == string.Empty)
        {
            objCommon.DisplayMessage("Either you not select student or Applicant not paid Fees.", this.Page);
            return;
        }

        string message = txtMessage.Text;
        if (txtMessage.Text != string.Empty)
        {
            objCommon.DisplayUserMessage(updCollege, "Message Sent Succesfully", this.Page);
        }
        else
            if (ua_no != string.Empty)
            {
                objCommon.DisplayMessage("Sorry..! Don't find Mobile No. for Some Employee", this.Page);
            }
            else
            {
                objCommon.DisplayMessage("Please select user", this.Page);
            }

        cancel();
    }
    private void SMS(string Message, string Mobileno)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");

            string Url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");//"http://smsnmms.co.in/sms.aspx";
            Session["url"] = Url;
            string UserId = ds.Tables[0].Rows[0]["SMSSVCID"].ToString(); //"hr@iitms.co.in";
            Session["userid"] = UserId;

            string Password = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();//"iitmsTEST@5448";
            Session["pwd"] = Password;
            string MobileNo = "91" + Mobileno.ToString();



            if (Mobileno.ToString() != string.Empty)
            {
                SendSMS(Url, UserId, Password, MobileNo, Message);
                //SendSMS(MobileNo, Message); //Added Mahesh
            }

        }
        catch (Exception)
        {
            // objCommon.ShowErrorMessage(Panel_Error, Label_ErrorMessage, CLOUD_COMMON.Message.ExceptionOccured, CLOUD_COMMON.MessageType.Alert);
            return;
        }

    }

    public void SendSMS(string url, string uid, string pass, string mobno, string message)
    {
        try
        {
            WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "");
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need 
            //return urlText;
        }
        catch (Exception)
        {

        }
    }

    // public bool SendMailBYSendgrid(string emailid, string CCemail)
    public bool SendMailBYSendgridForgetPassword(string emailid,string message ,string subject)
    {
        bool ret = false;
        try
        {

            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
            string fromAddress = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
            string user = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();
            string pwd = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
            string decrFromPwd = Common.DecryptPassword(pwd);
            //string decrFromPwd = pwd;
            var myMessage = new SendGridMessage();


            myMessage.From = new MailAddress(fromAddress);
            myMessage.AddTo(emailid);
            myMessage.Subject = subject;


            using (StreamReader reader = new StreamReader(Server.MapPath("~/email_template_forgot_password.html")))
            {
                myMessage.Html = reader.ReadToEnd();
            }

            myMessage.Html = myMessage.Html.Replace("{Name}", Session["SSName"].ToString());
            myMessage.Html = myMessage.Html.Replace("{Message}", message);
           // myMessage.Html = myMessage.Html.Replace("{Password}", Session["password"].ToString());

            var credentials = new NetworkCredential(user, decrFromPwd);
            var transportWeb = new Web(credentials);
            transportWeb.Deliver(myMessage);
            return ret = true;
        }
        catch (Exception)
        {
            ret = false;
        }
        //return transportWeb.DeliverAsync(myMessage);
        return ret;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cancel();
    }
    public void cancel()
    {
        ////trStudent_search.Visible = false;
        ////ddlUserType.SelectedIndex = 0;
        ////empPanel.Visible = true;
        ////lvEmployee.Visible = false;
        lvStudent.Visible = false;
        txtMessage.Text = string.Empty.Trim();
        trSubject.Visible = false;
        ddlBatch.SelectedIndex = 0;
        rdbMessage.SelectedValue = "SMS";
        rdbshow.SelectedIndex = -1;
    }

    //protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlUserType.SelectedValue != "0")
    //    {
    //        //---------------------Add by Maithili [23-09-2019]-------------------------//
    //        if (ddlUserType.SelectedValue == "2")
    //        {
    //            trEmployee.Visible = false;
    //            lvEmployee.Visible = false;
    //            trStudent.Visible = true;
    //            lvStudent.Visible = true;
    //            //trStudent_search.Visible = true;
    //            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO desc");
    //            //objCommon.FillDropDownList(ddlYear, "ACD_YEAR", "YEAR", "YEARNAME", "YEAR > 0", "YEAR");
    //            //objCommon.FillDropDownList(ddlCategory, "ACD_CATEGORY", "CATEGORYNO", "CATEGORY", "CATEGORYNO > 0", "CATEGORYNO");

    //            DataSet ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            lvStudent.DataSource = ds;
    //            lvStudent.DataBind();


    //        }//----------------------------------------------------------------------//
    //        else
    //        {

    //            trEmployee.Visible = true;
    //            lvEmployee.Visible = true;
    //           // trStudent_search.Visible = false;
    //            trStudent.Visible = false;
    //            lvStudent.Visible = false;
    //            DataSet ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID", "A.UA_NO", "A.UA_FULLNAME,UA_MOBILE,B.USERDESC,UA_EMAIL", "A.UA_TYPE=" + ddlUserType.SelectedValue, "A.UA_FULLNAME");
    //            lvEmployee.DataSource = ds;
    //            lvEmployee.DataBind();

    //            lvStudent.DataSource = null;
    //            lvStudent.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        lvEmployee.DataSource = null;
    //        lvEmployee.DataBind();
    //    }
    //}

    //---------------------Add by Maithili [24-09-2019]-------------------------//
    //protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
        //if (ddlUserType.SelectedValue != "0")
        //{

        //    if (ddlUserType.SelectedValue == "2")
        //    {
                //trStudent.Visible = true;
                //lvStudent.Visible = true;
                //DataSet ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "ADMBATCH=" + ddlBatch.SelectedValue + " and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
                //lvStudent.DataSource = ds;
                //lvStudent.DataBind();
        //    }

        //}

       // trStudent.Visible = true;
        //lvStudent.Visible = true;
        //DataSet ds = objCommon.FillDropDown("ACD_USER_REGISTRATION", "USERNO", "EMAILID,USERNAME,MOBILENO,FIRSTNAME,ISNULL(FEE_STATUS,0)FEE_STATUS", "ADMBATCH=" + ddlBatch.SelectedValue, "USERNO");
        //lvStudent.DataSource = ds;
        //lvStudent.DataBind();

    //}
    //protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds;
    //    if (ddlUserType.SelectedValue != "0")
    //    {

    //        if (ddlUserType.SelectedValue == "2")
    //        {
    //            trStudent.Visible = true;
    //            lvStudent.Visible = true;
    //            if (ddlBatch.SelectedIndex != 0 && ddlYear.SelectedIndex != 0)
    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "ADMBATCH=" + ddlBatch.SelectedValue + " and YEAR=" + ddlYear.SelectedValue + "and CATEGORYNO=" + ddlCategory.SelectedValue + " and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            else

    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "CATEGORYNO=" + ddlCategory.SelectedValue + " and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            lvStudent.DataSource = ds;
    //            lvStudent.DataBind();
    //        }

    //    }
    //}
    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds;
    //    if (ddlUserType.SelectedValue != "0")
    //    {

    //        if (ddlUserType.SelectedValue == "2")
    //        {
    //            trStudent.Visible = true;
    //            lvStudent.Visible = true;
    //            if (ddlBatch.SelectedIndex != 0)
    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "ADMBATCH=" + ddlBatch.SelectedValue + " and YEAR=" + ddlYear.SelectedValue + " and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            else
    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "YEAR=" + ddlYear.SelectedValue + " and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            lvStudent.DataSource = ds;
    //            lvStudent.DataBind();
    //        }

    //    }
    //}

    //protected void ddlSex_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds;
    //    if (ddlUserType.SelectedValue != "0")
    //    {

    //        if (ddlUserType.SelectedValue == "2")
    //        {
    //            trStudent.Visible = true;
    //            lvStudent.Visible = true;
    //            if (ddlBatch.SelectedIndex != 0 && ddlYear.SelectedIndex != 0 && ddlCategory.SelectedIndex != 0)
    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "ADMBATCH=" + ddlBatch.SelectedValue + " and YEAR=" + ddlYear.SelectedValue + "and CATEGORYNO=" + ddlCategory.SelectedValue + "and SEX='" + ddlSex.SelectedValue.ToString() + "' and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            else
    //                ds = objCommon.FillDropDown("USER_ACC A INNER JOIN USER_RIGHTS B ON A.UA_TYPE=B.USERTYPEID INNER JOIN ACD_STUDENT S ON (S.IDNO=A.UA_IDNO)", "A.UA_NO", "A.UA_FULLNAME,STUDENTMOBILE,B.USERDESC,REGNO,DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCH,DBO.FN_DESC('YEARNAME',YEAR)YEAR,DBO.FN_DESC('CATEGORY',CATEGORYNO)CATEGORY,SEX,ADMCAN,UA_EMAIL", "SEX='" + ddlSex.SelectedValue.ToString() + "' and A.UA_TYPE=" + ddlUserType.SelectedValue, "REGNO");
    //            lvStudent.DataSource = ds;
    //            lvStudent.DataBind();
    //        }

    //    }
    //}
    protected void rdbMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMessage.SelectedValue == "Mail")
        { trSubject.Visible = true; }
        else if (rdbMessage.SelectedValue == "SMS")
        { trSubject.Visible = false; }
    }
    protected void rdbshow_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowStudents();
    }
    private void ShowStudents()
    {
        DataSet ds = objSC.SearchOnlineCouncelingData(Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(rdbshow.SelectedValue));

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();

        }
        else
        {
            objCommon.DisplayMessage("Student Not Avaliable", this.Page);
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            
        }
    }

}
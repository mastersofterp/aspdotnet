using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Collections.Specialized;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class AadharVerification : System.Web.UI.Page
{
    Common objCommon = new Common();
    string ClientSecret = string.Empty;
    string ClientId = string.Empty;
    string RedirectUrl = string.Empty;
    FeeCollectionController feeController = new FeeCollectionController();
    StudentController objSC = new StudentController();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region digilocker cred
        ClientSecret = "44dfea5f3c4e3c472c9f";
        ClientId = "8EE9083B";
        RedirectUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/AadharVerification.aspx?pageno=2701"; //2722
        #endregion digilocker cred

        string Error = Request.QueryString["error"];
        //authorization code after successful authorization   

        string Code = Request.QueryString["code"];
        string State = Request.QueryString["state"];
        if (Code != null)
        {
            
            //btnShowInfo_Click(sender, e);
            IsTokenExpires(Code, State, Error);
            divStudentInfo.Visible = true;
            divFooter.Visible = true;
            img1.Visible = false;
            img2.Visible = true;
            lblError.Visible = false;
        }
        else
        {
            divFooter.Visible = false;
            ////imgVerified.Visible = false;
            lblError.Visible = false;

        }
    }
    protected void rdbSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbSelect.SelectedValue == "1")
        {
            string response_type = "code";
            string state = Session["userno"].ToString();

            string userAuthenticationURI = "https://api.digitallocker.gov.in/public/oauth2/1/authorize?response_type=" + response_type + "&client_id=" + ClientId + "&redirect_uri=" + RedirectUrl + "&state=" + state;

            if (!string.IsNullOrEmpty(userAuthenticationURI))
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = (snder, cert, chain, error) => true;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
                request.Method = "GET";
                request.ContentType = "application/json"; //new MediaTypeHeaderValue("text/html");
                WebResponse response = request.GetResponse();
                string responseUri = response.ResponseUri.ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ajaxCall('" + responseUri + "')", true);
            }
        }
        else
        {
            rdbSelect.ClearSelection();
            objCommon.DisplayMessage(this.Page, "Please visit this page after linked your mobile number with Aadhaar.", this.Page);
            return;
        }
    }
    protected void IsTokenExpires(string Code, string State, string Error)
    {
        if (Error != null)
        {
        }
        else if (Code != null)
        {
            AfterSuccessfullAuthorization();
        }
    }
    protected void AfterSuccessfullAuthorization()
    {
        string Error = Request.QueryString["error"];
        //authorization code after successful authorization   

        string Code = string.Empty;
        string State = string.Empty;
        Code = Request.QueryString["code"];
        State = Request.QueryString["state"];
        ViewState["ResponseCode"] = Code;
        ViewState["ResponseState"] = State;

        Code = ViewState["ResponseCode"].ToString();
        State = ViewState["ResponseState"].ToString();

        if (Error != null)
        {
        }
        else if (Code != null)
        {
            PostbackToDigiLockerServer(Code, State);
        }
        else if (Code == null)
        {
        }
    }
    protected void PostbackToDigiLockerServer(string Code, string State)
    {
        try
        {
            string gettoken = string.Empty;
            ExchangeAuthorizationCode(Convert.ToInt32(Session["userno"]), Code, State);
        }
        catch (Exception ex)
        {
        }
    }
    protected void ExchangeAuthorizationCode(int userId, string code, string state)
    {
        string accessToken = "";
        string[] splitString;
        var Content = "code=" + code + "&client_id=" + ClientId + "&client_secret=" + ClientSecret + "&redirect_uri=" + RedirectUrl + "&grant_type=authorization_code";

        var request = WebRequest.Create("https://api.digitallocker.gov.in/public/oauth2/1/token");
        request.Method = "POST";
        byte[] byteArray = Encoding.UTF8.GetBytes(Content);
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = byteArray.Length;
        using (Stream dataStream = request.GetRequestStream())
        {
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }
        var Response = (HttpWebResponse)request.GetResponse();
        Stream responseDataStream = Response.GetResponseStream();
        StreamReader reader = new StreamReader(responseDataStream);
        string ResponseData = reader.ReadToEnd();
        reader.Close();
        responseDataStream.Close();
        Response.Close();
        if (Response.StatusCode == HttpStatusCode.OK)
        {
            var ReturnedToken = JsonConvert.DeserializeObject<Token>(ResponseData);
            if (ReturnedToken.refresh_token != null)
            {
                accessToken = ReturnedToken.access_token;
                Session["token"] = accessToken;
                GetUserDetails(accessToken);
            }
            else
            {

            }
        }
        else
        {

        }
    }

    private void GetUserDetails(string accToken)
    {
        try
        {
            if (accToken == null || accToken == "" || accToken == string.Empty)
            {
                //string ref_token = "";
                //string refreshToken = RefreshAccessToken(AccessToken, out ref_token);
            }
            else
            {
                string fileUri = "";
                var DocListReq = "https://api.digitallocker.gov.in/public/oauth2/1/user";
                // Create a request for the URL.    
                var Request = WebRequest.Create(DocListReq);
                Request.Headers.Add("Authorization", "Bearer " + accToken);

                var Response = (HttpWebResponse)Request.GetResponse();
                // Get the stream containing content returned by the server.    
                var DataStream = Response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.    
                var Reader = new StreamReader(DataStream);
                // Read the content.    
                var JsonString = Reader.ReadToEnd();
                // Cleanup the streams and the response.    
                Reader.Close();
                DataStream.Close();
                Response.Close();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var ReturnedToken = JsonConvert.DeserializeObject<UserDetails>(JsonString);
                    if (ReturnedToken.name != null)
                    {
                        divStudentInfo.Visible = true;
                        divFooter.Visible = true;
                        img1.Visible = false;
                        img2.Visible = true;
                        lblName.Text = ReturnedToken.name;
                        lblDOB.Text = ReturnedToken.dob;
                        lblGender.Text = ReturnedToken.gender;
                    }
                    divFooter.Visible = true;
                    //divFooter.Visible = true;
                    //imgVerified.Visible = true;
                }
                else
                {
                    divFooter.Visible = false;
                    //imgVerified.Visible = false;
                    lblError.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }
    private void Get_e_AadharData(string accToken, string[] stateString)
    {
        try
        {
            if (accToken == null || accToken == "" || accToken == string.Empty)
            {
                //string ref_token = "";
                //string refreshToken = RefreshAccessToken(AccessToken, out ref_token);
            }
            else
            {
                string fileUri = "";
                var DocListReq = "https://api.digitallocker.gov.in/public/oauth2/3/xml/eaadhaar";
                // Create a request for the URL.    
                var Request = WebRequest.Create(DocListReq);
                Request.Headers.Add("Authorization", "Bearer " + accToken);

                var Response = (HttpWebResponse)Request.GetResponse();
                // Get the stream containing content returned by the server.    
                var DataStream = Response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.    
                var Reader = new StreamReader(DataStream);
                // Read the content.    
                var JsonString = Reader.ReadToEnd();
                // Cleanup the streams and the response.    
                Reader.Close();
                DataStream.Close();
                Response.Close();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    //divFooter.Visible = true;
                    //imgVerified.Visible = true;
                }
                else
                {
                    //divFooter.Visible = false;
                    //imgVerified.Visible = false;
                    lblError.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void btnShowInfo_Click(object sender, EventArgs e)
    {
        if (Session["regno"] != null)
        {
            txtEnrollNo.Text = Session["regno"].ToString();
        }
        int studentId = feeController.GetStudentIdByEnrollmentNoforLedgerreport(txtEnrollNo.Text.Trim());
        DataTableReader dtr = objSC.GetStudentDetails(studentId);
        Session["regno"] = txtEnrollNo.Text.Trim();
        if (studentId > 0)
        {
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    lblRegNo.Text = dtr["REGNO"].ToString();
                    ViewState["admbatch"] = dtr["ADMBATCH"];
                    string branchname = objCommon.LookUp("ACD_BRANCH", "LONGNAME", "BRANCHNO=" + dtr["branchno"].ToString());
                    lblBranch.Text = branchname;
                    lblStudName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                    //lblAdmBatch.Text = dtr["BATCH"] == null ? string.Empty : dtr["BATCH"].ToString();
                    string semester = objCommon.LookUp("ACD_SEMESTER", "SEMESTERNAME", "SEMESTERNO=" + dtr["SEMESTERNO"].ToString());
                    lblSemester.Text = semester;
                    lblAadhar.Text = dtr["ADDHARCARDNO"] == null ? "0" : dtr["ADDHARCARDNO"].ToString();
                    divStudentInfo.Visible = true;
                    divOption.Visible = true;
                    img1.Visible = true;
                    img2.Visible = false;
                }
            }
        }
        else
        {
            divStudentInfo.Visible = false;
            divOption.Visible = false;
            objCommon.DisplayUserMessage(updTimeTable, "No student found with given enrollment number.", this.Page);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
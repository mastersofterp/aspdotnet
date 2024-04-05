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
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public partial class ACADEMIC_PublishResult : System.Web.UI.Page
{
    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ResultProcessing objResult = new ResultProcessing();
    StudentController objSC = new StudentController();
    int IS_PublishCount = 0;
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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                PopulateDropDownList();
                //*ddlSession.Focus();
                //  trinternal.Visible = true;
                //*ddlSession.SelectedIndex = 0;
                ddlExamName.SelectedIndex = 1;
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PublishResult.aspx");
        }
    }
    #region
    private void PopulateDropDownList()
    {
        try
        {

           // objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER SM", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID > 0", "SM.COLLEGE_ID DESC");
            objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_REPORTS_StudentResultList.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Fill DropDownList

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divStudentRecord.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            // objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (D.DEGREENO=B.DEGREENO)", "DISTINCT (D.DEGREENO)", "DEGREENAME", "D.DEGREENO > 0 AND B.COLLEGE_ID=" + ddlColg.SelectedValue, "D.DEGREENO");
            ddlBranch.Focus();
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            //ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtDateOfPublish.Text = string.Empty;
            //ddlDegree.Focus();
            //if (ddlColg.SelectedIndex == 0)
            //{
            //    ddlDegree.Items.Clear();
            //    ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            //}

            //Added Mahesh on Dated 03-05-2021

            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT R WITH (NOLOCK) ON(S.SCHEMENO=R.SCHEMENO) INNER JOIN ACD_STUDENT ST WITH (NOLOCK) ON (R.IDNO=ST.IDNO)", "DISTINCT S.SCHEMENO", "S.SCHEMENAME", "ST.COLLEGE_ID=" + ddlColg.SelectedValue + " AND R.SESSIONNO=" + ddlSession.SelectedValue, "S.SCHEMENO");
            ddlScheme.Focus();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MarkEntryComparision.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
            {
                objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }
    #region degree
    //protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    divStudentRecord.Visible = false;
    //    lvStudent.DataSource = null;
    //    lvStudent.DataBind();
    //    //ddlExamName.SelectedIndex = 0;
    //    if (ddlDegree.SelectedIndex > 0)
    //    {
    //        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B WITH (NOLOCK) ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");

    //        ddlBranch.Focus();
    //        //ddlScheme.Items.Clear();
    //        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSection.Items.Clear();
    //        ddlSection.Items.Add(new ListItem("Please Select", "0"));
    //        //ddlExam.SelectedIndex = 0;
    //        ddlStatus.SelectedIndex = 0;
    //        lvStudent.DataSource = null;
    //        lvStudent.DataBind();
    //        txtDateOfPublish.Text = string.Empty;
    //    }
    //    else
    //    {
    //        ddlBranch.Items.Clear();
    //        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
    //        //ddlScheme.Items.Clear();
    //        //ddlScheme.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSem.Items.Clear();
    //        ddlSem.Items.Add(new ListItem("Please Select", "0"));
    //        ddlSection.Items.Clear();
    //        ddlSection.Items.Add(new ListItem("Please Select", "0"));
    //        //ddlExam.SelectedIndex = 0;
    //        ddlStatus.SelectedIndex = 0;
    //        txtDateOfPublish.Text = string.Empty;
    //    }
    //}
    #endregion
    #region branch
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        //  ddlExamName.SelectedIndex = 0;
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlScheme.Items.Clear();
            objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", " BRANCHNO = " + ddlBranch.SelectedValue + " AND DEGREENO=" + ViewState["degreeno"], "SCHEMENO");
            ddlScheme.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            //ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtDateOfPublish.Text = string.Empty;
        }
        else
        {
            ddlScheme.Items.Clear();
            ddlScheme.Items.Add(new ListItem("Please Select", "0"));
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            //ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtDateOfPublish.Text = string.Empty;
        }
    }

    #endregion
    #region scheme
    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        // ddlExamName.SelectedIndex = 0;
        if (ddlScheme.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ViewState["schemeno"], "A.SEMESTERNO");
            ddlSem.Focus();
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            // ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtDateOfPublish.Text = string.Empty;
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("Please Select", "0"));
            // ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtDateOfPublish.Text = string.Empty;
        }
    }
    #endregion scheme

    protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION S WITH (NOLOCK),ACD_STUDENT_RESULT SRWITH (NOLOCK)", "DISTINCT SR.SECTIONNO", "S.SECTIONNAME", "SR.SECTIONNO = S.SECTIONNO AND S.SECTIONNO > 0 AND SR.EXAM_REGISTERED=1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue) + "AND SR.PREV_STATUS=" + Convert.ToInt32(ddlExam.SelectedValue), "S.SECTIONNAME");
    }

    #endregion

    protected void btnPublish_Click(object sender, EventArgs e)
    {
        try
        {
            // DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            ////ViewState["degreeno"]


            // if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            // {
            //     ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //     ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //     ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //     ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            // }
            int result = 0;
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);
            int branchno = 0;
            int schemeno = Convert.ToInt32(ViewState["schemeno"]); ;
             //int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            DateTime publishDate = Convert.ToDateTime(txtDateOfPublish.Text);
            string examdate = "";  //Examination date added by Injamam Ansari 16-10-2023

            if (!string.IsNullOrEmpty(txtExamDate.Text))
            {
                if (Checkdate(txtExamDate.Text) == false)
                {
                    objCommon.DisplayMessage(updUpdate, "Examination Date is not in Valid Format", this.Page);
                    SetFocus(txtExamDate);
                    return;
                }
                else
                {
                    examdate = (Convert.ToDateTime(txtExamDate.Text.ToString())).ToString();
                }
            }
            string ids = string.Empty;
            string stuids = string.Empty;
            
            ids = GetStudentID();
            if (ids != "0")
            {
                //result = objResult.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 0, Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue));
                //result = objResult.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 0, Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["schemeno"]));

                // result = objResult.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 0,  Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["schemeno"]));

                //result = objResult.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 0, Convert.ToInt32(ddlExamType.SelectedValue), schemeno); //Commented for new method  with parameter of exam date
                result = AddPublishResults(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 0, Convert.ToInt32(ddlExamType.SelectedValue), schemeno, examdate);     //Examination date added by Injamam Ansari 16-10-2023


                //if (rdoselect.SelectedValue == "1")
                //{
                //    result = objResult.AddPublishResult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), GetStudentID(), 1, Session["IPADDRESS"].ToString(), txtRemark.Text, ddlInternal.SelectedValue, Convert.ToInt32(Session["userno"].ToString()));
                //}
                //elseddlExamName
                //{
                //    result = objResult.AddPublishResult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), GetStudentID(), 1, Session["IPADDRESS"].ToString(), txtRemark.Text, "EXTERMARK", Convert.ToInt32(Session["userno"].ToString()));

                //}
                foreach (ListViewDataItem item in lvStudent.Items)
                {
                    CheckBox chk = item.FindControl("chkStudent") as CheckBox;
                    HiddenField hdfuserno = item.FindControl("hidIdNo") as HiddenField;
                    if (chk.Checked == true)
                    {
                       // ids += ((item.FindControl("lblStudname")) as HiddenField).Value + ",";
                        stuids += hdfuserno.Value + ",";
                    }
                }

                stuids = stuids.TrimEnd(',');

                if (result == 1)
                {
                    objCommon.DisplayMessage(updUpdate, "Result Publish Successfully!!!", this.Page);
                    BindListView();
                   string title=("Result Publish");
                   string Sessionname = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_PNAME", "SESSIONNO="+Convert.ToInt32(ddlSession.SelectedValue)+"");
                   string msg = ("Dear Student , your result of examination " + Sessionname + " has been published.");

                   //string ua_nos = ids;
                   string ua_nos = stuids;
                   Task<string> task = DecryptPass(ua_nos, title, msg);
                   var status = task;

                   //string task = DecryptPass(ua_nos, title, msg);
                

                }

                else
                    objCommon.DisplayMessage(updUpdate, "Transaction Fail!!", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(updUpdate, "Please Select Atleast One Student.", this.Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PublishResult.btnPublish_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    public static async Task<string> DecryptPass(string Uanos, string Title, string msg)
    {
        try
        {
            Common objcommon = new Common();
            string sitename = objcommon.LookUp("APP_DETAILS", "APP_API_URL", "ID >0 ");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            string URL = "" + sitename + "/api/v2/Notification/SendNotification";
           // string URL = "sitename/api/v2/Notification/SendNotification";
            var formData = new Dictionary<string, string>
           {
              { "uaNos", Uanos },
              { "Message", msg}  ,    
              { "Title", Title}  
           };

            using (var client = new HttpClient())
            {

                var content = new FormUrlEncodedContent(formData);

                //var jsonContent = new StringContent(JsonConvert.SerializeObject(new
                //{
                //    uaNos = ,
                //    Message = msg,
                //    Title = Title

                //}), Encoding.UTF8, "application/json");


                var result = await client.PostAsync(URL, content);


                if (!result.IsSuccessStatusCode)
                {
                    throw new ArgumentException("something bad happended");
                }
               
                string res = await result.Content.ReadAsStringAsync();

                JavaScriptSerializer serializer1 = new JavaScriptSerializer();
                MyObj obje = serializer1.Deserialize<MyObj>(res);

                return obje.DecryptedString;
            }
        }
        catch(Exception ex)
        {
            return null;
        }

    }
    class MyObj
    {
        public string DecryptedString { get; set; }

    }
    //public static string DecryptPass(string Uanos,string Title,string msg)
    //{

    //    string URL = "https://androidapipcentest.mastersofterp.in/api/v2//Notification/SendNotification";
    //   //var token = "671326ff-a69c-4b5a-88f2-cfe076301c01";
    //    using (var client = new HttpClient())
    //    {
    //        var jsonContent = new StringContent(JsonConvert.SerializeObject(new
    //        {
     
    //            uaNos =Uanos,
    //            Message=msg,
    //            Title = Title


    //        }), Encoding.UTF8, "application/json");


    //        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);


    //        var result = client.PostAsync(URL, jsonContent);


    //        //if (!result.IsSuccessStatusCode)
    //        //{
    //        //    throw new ArgumentException("something bad happended");
    //        //}
    //        //string json = client.result();

    //            //var res = result;
         

    //        JavaScriptSerializer serializer1 = new JavaScriptSerializer();
    //        MyObj obje = serializer1.Deserialize<MyObj>(result.ToString());
    //       // var object1 = serializer1.Deserialize<MyObj>(res);



    //        return result.ToString();
    //    }

    //}
   //public class MyObj
   // {
   //     public string Decrypted { get; set; }

   // }

    private void ClearControl()
    {
        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlScheme.SelectedIndex = 0;
        ddlExam.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        hftot.Value = "0";
        txtRemark.Text = string.Empty;
        txtDateOfPublish.Text = string.Empty;
    }

    protected void btnUnpublish_Click(object sender, EventArgs e)
    {
        try
        {
            ResultProcessing objResult1 = new ResultProcessing();
            int result1 = 0;
            int sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            int degreeno = Convert.ToInt32(ViewState["degreeno"]);
            // int branchno = Convert.ToInt32(ddlBranch.SelectedValue);
            // int branchno = Convert.ToInt32(ViewState["branchno"]);
            int semesterno = Convert.ToInt32(ddlSem.SelectedValue);
            DateTime publishDate = Convert.ToDateTime(txtDateOfPublish.Text);
            int branchno = 0;
            int schemeno = Convert.ToInt32(ViewState["schemeno"]); ;


            string ids = string.Empty;
            ids = GetStudentID();
            string examdate = "";  //Examination date added by Injamam Ansari 16-10-2023

            if (!string.IsNullOrEmpty(txtExamDate.Text))
            {
                examdate = (Convert.ToDateTime(txtExamDate.Text)).ToString();
            }
            if (ids != "0")
            {
                //result1 = objResult1.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 1, Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["schemeno"]));
                //result1 = objResult1.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 1, Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["schemeno"]));
                //result1 = objResult1.AddPublishResult(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 1, Convert.ToInt32(ddlExamType.SelectedValue), schemeno);

                result1 = AddPublishResults(sessionno, degreeno, branchno, semesterno, GetStudentID(), publishDate, Request.ServerVariables["REMOTE_HOST"].ToString(), 1, Convert.ToInt32(ddlExamType.SelectedValue), schemeno, examdate);     //Examination date added by Injamam Ansari 16-10-2023

                //if (rdoselect.SelectedValue == "1")
                //{
                //    result = objResult.AddPublishResult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), GetStudentID(), 2, Session["IPADDRESS"].ToString(), txtRemark.Text, ddlInternal.SelectedValue, Convert.ToInt32(Session["userno"].ToString()));
                //}
                //else
                //{
                //    result = objResult.AddPublishResult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlStatus.SelectedValue), GetStudentID(), 2, Session["IPADDRESS"].ToString(), txtRemark.Text, "EXTERMARK", Convert.ToInt32(Session["userno"].ToString()));

                //}

                if (result1 == 1)
                {
                    objCommon.DisplayMessage(updUpdate, "Result Unpublish Successfully", this.Page);
                    BindListView();
                }
                else
                    objCommon.DisplayMessage(updUpdate, "Transaction Fail!!", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updUpdate, "Please Select Atleast One Student.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_PublishResult.btnUnpublish_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        txtDateOfPublish.Text = string.Empty;
        txtExamDate.Text = string.Empty;
        txtRemark.Text = string.Empty;
        txtTotStud.Text = string.Empty;
        ddlClgname.SelectedIndex = 0;
        ddlExamType.SelectedIndex = -1;
        ddlExam.SelectedIndex = -1;
        ddlStatus.SelectedIndex = -1;
        ddlExamName.SelectedIndex = 0;
        ddlColg.Items.Clear();
        ddlColg.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlInternal.Items.Clear();
        ddlInternal.Items.Add(new ListItem("Please Select", "0"));
        ddlScheme.Items.Clear();
        ddlScheme.Items.Add(new ListItem("Please Select", "0"));
        ddlSection.Items.Clear();
        ddlSection.Items.Add(new ListItem("Please Select", "0"));
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        ViewState["degreeno"] = null;
        SetFocus(ddlClgname);
    }

    protected void BindListView()
    {
        try
        {
            //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            ////ViewState["degreeno"]


            //if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            //{
            //    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
            //    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
            //    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
            //    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
            //}
            divStudentRecord.Visible = false;
            DataSet dsShowData = null;
            //int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            //ViewState["degreeno"] = degreeno;

            //dsShowData = objSC.GetStudentDetailsForPublishresult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlScheme.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlColg.SelectedValue));
            //dsShowData = objSC.GetStudentDetailsForPublishresult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlExamName.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));
            //dsShowData = objSC.GetStudentDetailsForPublishresult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSem.SelectedValue),  Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ViewState["college_id"]));

            string sp_procedure = "PKG_ACAD_GET_STUDENT_FOR_PUBLISHRESULT_SCHEMEWISE";
            string sp_parameters = "@P_SCHEMENO,@P_SESSIONNO,@P_DEGREENO,@P_SEMESTERNO,@P_PREV_STATUS,@P_COLLEGE_ID,@P_OUT";
            //string sp_callValues = "" + (Convert.ToInt32(ddlSession.SelectedValue))+ "," + Convert.ToInt32(ddlDegree.SelectedValue) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + Convert.ToInt32(ddlExamType.SelectedValue) + ","+Convert.ToInt32(ddlClgname.SelectedValue)+","+0+"";
            string sp_callValues = "" + Convert.ToInt32(ViewState["schemeno"]) + "," + (Convert.ToInt32(ddlSession.SelectedValue)) + "," + Convert.ToInt32(ViewState["degreeno"]) + "," + Convert.ToInt32(ddlSem.SelectedValue) + "," + Convert.ToInt32(ddlExamType.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "," + 0 + "";

               dsShowData = objCommon.DynamicSPCall_Select(sp_procedure, sp_parameters, sp_callValues);
            
            
           // dsShowData = objSC.GetStudentDetailsForPublishresult(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlExamType.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));


            if (dsShowData.Tables[0].Rows.Count > 0)
            {
                divStudentRecord.Visible = true;
                lvStudent.DataSource = dsShowData;
                lvStudent.DataBind();
                btnPublish.Enabled = true;
                btnUnpublish.Enabled = true;
                CheckBox chk = lvStudent.FindControl("cbRows") as CheckBox;
                chk.Checked = false;
                hftot.Value = dsShowData.Tables[0].Rows.Count.ToString();
                int tot_stud = Convert.ToInt32(hftot.Value);
                //if ((tot_stud == IS_PublishCount) && (IS_PublishCount > 0))
                //{
                //    btnPublish.Enabled = false;
                //    btnUnpublish.Enabled = true;
                //}
                //else if ((tot_stud != IS_PublishCount) && (IS_PublishCount > 0))
                //{
                //    btnUnpublish.Enabled = true;
                //}
                //else
                //{
                //    btnUnpublish.Enabled = false;
                //}
            }
            else
            {
                divStudentRecord.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                btnPublish.Enabled = false;
                btnUnpublish.Enabled = false;
                hftot.Value = "0";
                objCommon.DisplayMessage(updUpdate, "Record Not Found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PublishResult.btnShow_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindListView();
    }

    private string GetStudentID()
    {
        string studentId = "0";
        try
        {
            foreach (ListViewDataItem item in lvStudent.Items)
            {
                CheckBox chk = item.FindControl("chkStudent") as CheckBox;

                if (chk.Checked)
                {
                    if (studentId.Length > 0)
                        studentId += ",";
                    studentId += chk.ToolTip;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_PublishResult.GetStudentID() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
        return studentId;
    }

    protected void rdoselect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(rdoselect.SelectedValue) == 1)
        {
            trinternal.Visible = true;
            trexmtype.Visible = false;
        }
        else
        {
            trinternal.Visible = false;
            trexmtype.Visible = true;
        }
    }

    protected void ddlInternal_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        objCommon.FillDropDownList(ddlSection, "ACD_SECTION S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (S.SECTIONNO = SR.SECTIONNO)", "DISTINCT SR.SECTIONNO", "S.SECTIONNAME", "SR.SECTIONNO = S.SECTIONNO AND S.SECTIONNO > 0 AND SR.EXAM_REGISTERED=1 AND SR.SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + "AND SR.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "AND SR.SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue), "S.SECTIONNAME");
    }

    #region
    //protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    foreach (ListViewDataItem lvdata in lvStudent.Items)
    //    {
    //      System.Web.UI.WebControls.Label  lblroll = lvdata.FindControl("lblRollNo") as Label;

    //        if (Convert.ToInt32(rdoselect.SelectedValue) > 2)
    //        {
    //            lblroll.Text = "Roll No";        
    //        }
    //        else
    //        {
    //            lblroll.Text = "Seat No";                
    //        }
    //    }
    //}
    #endregion

    #region report
    private void ShowReportPublish(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        int chkcount = 0;

        if (ddlSession.SelectedIndex > 0)
        {
            chkcount = Convert.ToInt32(objCommon.LookUp("RESULT_PROCESS_LOG WITH (NOLOCK)", "COUNT(FLOCK)", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue)));

            if (chkcount > 0)
            {

                ShowReportPublish("PublishResultReport", "rptPublishReport.rpt");
            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Result Not Published !!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updUpdate, "Please Select Session!!", this.Page);
        }
    }

    public void ShowReportSemester(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + Convert.ToInt32(ddlSem.SelectedValue);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);
    }

    public void ShowReportDepartment(string reportTitle, string rptFileName)
    {
        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
        url += "Reports/CommonReport.aspx?";
        url += "pagetitle=" + reportTitle;
        url += "&path=~,Reports,Academic," + rptFileName;
        url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
        sb.Append(@"window.open('" + url + "','','" + features + "');");

        ScriptManager.RegisterClientScriptBlock(this.updUpdate, this.updUpdate.GetType(), "controlJSScript", sb.ToString(), true);
    }

    protected void btnReportsemester_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (ddlDegree.SelectedIndex > 0)
            {
                if (ddlBranch.SelectedIndex > 0)
                {
                    ShowReportSemester("SemesterwiseReport", "rptExam_Result_Percent_Semesterwise_Graph.rpt");

                }
                else
                {
                    objCommon.DisplayMessage(this.updUpdate, "Please Select Branch !!", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Please Select Degree !!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updUpdate, "Please Select Session !!", this.Page);
        }
    }

    protected void btnReportDepart_Click(object sender, EventArgs e)
    {
        if (ddlSession.SelectedIndex > 0)
        {
            if (ddlDegree.SelectedIndex > 0)
            {

                ShowReportDepartment("DepartmentwiseReport", "rptExam_Result_Percent_Departmentwise.rpt");

            }
            else
            {
                objCommon.DisplayMessage(this.updUpdate, "Please Select Degree !!", this.Page);
            }
        }
        else
        {
            objCommon.DisplayMessage(this.updUpdate, "Please Select Session !!", this.Page);
        }
    }

    #endregion report

    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        ddlExamType.SelectedIndex = -1;
        txtExamDate.Text = string.Empty;
        txtDateOfPublish.Text = string.Empty;
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        SetFocus(ddlExamType);
        //ddlExamName.SelectedIndex = 0;

    }

    protected void lvStudent_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label lblstatus = e.Item.FindControl("lblstatus") as Label;
        if (lblstatus.ToolTip == "Published")
        {
            IS_PublishCount += 1;
        }
    }

    protected void ddlExamName_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {

        Common objCommon = new Common();
        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlExamType.SelectedIndex = -1;
        txtExamDate.Text = string.Empty;
        txtDateOfPublish.Text = string.Empty;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        divStudentRecord.Visible = false;
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        if (ddlClgname.SelectedIndex > 0)
        {
            //   string totalstud = objCommon.LookUp("ACD_EXAM_DATE AED INNER JOIN ACD_STUDENT_RESULT ASR ON ASR.COURSENO=AED.COURSENO AND ASR.SEMESTERNO=AED.SEMESTERNO AND ASR.SESSIONNO=AED.SESSIONNO AND ASR.SCHEMENO=AED.SCHEMENO INNER JOIN ACD_COURSE C ON ( ASR.COURSENO = C.COURSENO ) ", "COUNT(DISTINCT IDNO)", "ISNULL(CANCEL,0)=0 AND ISNULL(EXAM_REGISTERED,0) = 1 AND ISNULL(DETAIND,0)=0 AND ISNULL(EXT_IND,0)=(case when  PREV_STATUS = 1 then 'T' else '0' end) AND EXAMDATE='" + EXAMDATE + "' AND slotno=" + ddlslot.SelectedValue + " AND ASR.sessionno=" + ddlSession.SelectedValue + " AND (ISNULL(ASR.PREV_STATUS,0)=" + Convert.ToInt32(ddlExamType.SelectedValue) + " OR " + Convert.ToInt32(ddlExamType.SelectedValue) + "=2)" + " AND ISNULL(AED.STATUS,0)=" + Convert.ToInt32(ddlExamType.SelectedValue) + "AND C.SUBID IN(1,3) ");  //AND AED.TH_PR=1

            //  int collegeid = Convert.ToInt32(objCommon.LookUp("ACD_COLLEGE_MASTER S INNER JOIN ACD_COLLEGE_SCHEME_MAPPING M ON (M.COLLEGE_ID=S.COLLEGE_ID)", "DISTINCT S.COLLEGE_ID", "S.COLLEGE_ID=" + Convert.ToInt32(ddlClgname.SelectedValue)));
            int college_id = Convert.ToInt32(ddlClgname.SelectedValue);
            college_id = Convert.ToInt32(ViewState["college_id"]);

            //Common objCommon = new Common();
            // DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            //DataSet ds = objCommon.GetCollegeSchemeMappingDetails(collegeid);
            //ViewState["degreeno"]

           DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));
            //ViewState["degreeno"]


           if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
           {
               ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
               ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
               ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
               ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

           }
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "COLLEGE_CODE = " + Convert.ToInt32(Session["colcode"]), "SESSIONNO DESC");                
            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D WITH (NOLOCK) INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD WITH (NOLOCK) ON (CD.DEGREENO=D.DEGREENO)", "DISTINCT(D.DEGREENO)", "D.DEGREENAME", "D.DEGREENO > 0 AND CD.COLLEGE_ID =" + ViewState["college_id"].ToString(), "DEGREENAME");


           
            
            SetFocus(ddlSession);

        }

    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        ddlDegree.SelectedIndex = 0;
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        ddlExamType.SelectedIndex = -1;
        txtDateOfPublish.Text = string.Empty;
        txtExamDate.Text = string.Empty;
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        // ddlExamName.SelectedIndex = 0;
        if (ddlSession.SelectedIndex > 0)
        {
            //ddlSem.Items.Clear();
            // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT B WITH (NOLOCK) ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO > 0 AND SESSIONNO=" + ddlSession.SelectedValue + " AND SCHEMENO=" + ddlScheme.SelectedValue, "A.SEMESTERNO");
            // objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue, "S.SEMESTERNO");

            //ddlSem.Focus();
            //*  ddlSection.Items.Clear();
            //*  ddlSection.Items.Add(new ListItem("Please Select", "0"));
            // ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            txtDateOfPublish.Text = string.Empty;
            SetFocus(ddlSem);
        }
        else
        {
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            //* ddlSection.Items.Clear();
            //* ddlSection.Items.Add(new ListItem("Please Select", "0"));
            // ddlExam.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtDateOfPublish.Text = string.Empty;
        }

    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.SelectedIndex = 0;
        ddlExamType.SelectedIndex = -1;
        txtExamDate.Text = string.Empty;
        txtDateOfPublish.Text = string.Empty;
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        SetFocus(ddlSem);
    }

    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtExamDate.Text = string.Empty;
        txtDateOfPublish.Text = string.Empty;
        divStudentRecord.Visible = false;
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        btnPublish.Enabled = false;
        btnUnpublish.Enabled = false;
        SetFocus(txtDateOfPublish);
    }

    protected bool Checkdate(string examdate)
    {
        bool status = false;
        DateTime d;
        if (DateTime.TryParseExact(examdate, new string[] { "dd/MM/yyyy", "MM/dd/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
        {
            status = true;
        }
        else
        {
            status = false;
        }

        return status;
    }


    //---------------------------------------------------------------------Controller ----------------------------------------------------

    public int AddPublishResults(int sessionno, int degreeno, int branchno, int semesterno, string idnos, DateTime pdate, string ipAdd, int status, int Prev_status, int schemeno, string examdate)
    {
        int retStatus = Convert.ToInt32(CustomStatus.Others);
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(connectionString);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[12];
            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
            objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
            objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
            objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
            objParams[4] = new SqlParameter("@P_IDNO", idnos);
            objParams[5] = new SqlParameter("@P_IPADDRESS", ipAdd);
            objParams[6] = new SqlParameter("@P_PUB_UNPUB_DATE", pdate);
            objParams[7] = new SqlParameter("@P_PUB_UNPUB", status);
            objParams[8] = new SqlParameter("@P_PREV_STATUS", Prev_status);
            objParams[9] = new SqlParameter("@P_SCHEMENO", schemeno);
            objParams[10] = new SqlParameter("@P_EXAM_DATE", examdate);
            objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[11].Direction = ParameterDirection.Output;

            object ret = objSQLHelper.ExecuteNonQuerySP("PROC_EXAM_PUBLISH_RESULT_SCHEMEWISE", objParams, true);
            if (Convert.ToInt32(ret) == -99)
                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            else
                retStatus = Convert.ToInt32(ret);

        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
        }
        return retStatus;

    }
    
}

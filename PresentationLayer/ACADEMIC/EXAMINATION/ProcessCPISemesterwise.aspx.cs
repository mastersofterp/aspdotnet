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
using System.Data.OleDb;
using System.Data.Common;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
public partial class ACADEMIC_EXAMINATION_ProcessCPISemesterwise : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExam = new ExamController();
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                string host = Dns.GetHostName();
                IPHostEntry ip = Dns.GetHostEntry(host);
                string IPADDRESS = string.Empty;

                IPADDRESS = ip.AddressList[0].ToString();
                ViewState["ipAddress"] = IPADDRESS;
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
                Response.Redirect("~/notauthorized.aspx?page=ProcessCPISemesterwise.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ProcessCPISemesterwise.aspx");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {  
        int IDNO =Convert.ToInt32( objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollno.Text + "' AND ROLLNO IS NOT NULL"));

        int a = objExam.ProcessSemesterwise(IDNO, Convert.ToInt32(ddlSem.SelectedValue));
        if (a > 0)
        {
            objCommon.DisplayMessage("Result Process Successfully!", this.Page);
        }
        //int log = objExam.ProcessLog(IDNO, Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(Session["userno"].ToString()), ViewState["ipAddress"].ToString());
     
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlSem, "ACD_STUDENT_RESULT_HIST", " DISTINCT SEMESTERNO", "DBO.FN_DESC('SEMESTER',SEMESTERNO)SEMESTER", "ROLL_NO='" + txtRollno.Text + "' AND ROLL_NO IS NOT NULL","");
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        int IDNO =Convert.ToInt32( objCommon.LookUp("ACD_STUDENT", "IDNO", "ROLLNO='" + txtRollno.Text + "' AND ROLLNO IS NOT NULL"));
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT_RESULT_HIST A INNER JOIN ACD_TRRESULT C ON(A.IDNO = C.IDNO)", "DISTINCT A.IDNO,C.REGNO", "C.STUDNAME,C.SEMESTERNO,SUBSTRING(CAST(C.SGPA AS NVARCHAR(10)),0,5)SGPA,SUBSTRING(CAST(C.CGPA AS NVARCHAR(10)),0,5)CGPA,DBO.FN_DESC('SESSION',C.SESSIONNO)SESSION", " A.IDNO = " + IDNO + " AND C.SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + " AND C.SESSIONNO =(SELECT MAX(SESSIONNO) FROM ACD_TRRESULT WHERE IDNO = " + IDNO + " AND SEMESTERNO = " + Convert.ToInt32(ddlSem.SelectedValue) + ")", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
        }
    }
}

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
using System.Net;
using System.Data.OleDb;
using System.Data.Common;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class PAYROLL_REPORTS_Pay_Employee_Reports : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, string.Empty);
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
                if (Session["usertype"].ToString() == "1")
                {
                    BindEmployeeList();
                }
                else
                {
                    DivSerach.Visible = false;
                    btnBack.Visible = false;
                    int idno = 0;
                    idno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"].ToString())));
                    ShowEmpDetails(idno);

                }




            }
        }
        else
        {
            divMsg.InnerHtml = "";
        }

    }



    private void ShowEmpDetails(int idno)
    {
        EmpCreateController objECC = new EmpCreateController();

        DataTableReader dtr = objECC.ShowEmpDetails(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                //ShowStauts();
                pnlId.Visible = true;

                // objCommon.FillDropDownList(ddlAuthorityType, "PAYROLL_NODUES_AUTHORITY_TYPE", "AUTHO_TYP_ID", "AUTHORITY_TYP_NAME", "", "AUTHO_TYP_ID");
                // ddlAuthorityType.SelectedIndex = 1;
                //if (ViewState["AuthoTypeId"].ToString() != "")
                //{


                ViewState["IDNO"] = lblIDNo.Text = dtr["idno"].ToString();
                lblEmpcode.Text = dtr["EmployeeId"].ToString();
                lbltitle.Text = dtr["title"].ToString();
                lblFName.Text = dtr["fname"].ToString();
                lblMname.Text = dtr["mname"].ToString();
                lblLname.Text = dtr["lname"].ToString();
                lblDepart.Text = dtr["SUBDEPT"].ToString();
                lblDesignation.Text = dtr["SUBDESIG"].ToString();
                lblMob.Text = dtr["PHONENO"].ToString();
                lblEmail.Text = dtr["EMAILID"].ToString();

                lblDOJ.Text = dtr["DateofJoining"] == null ? string.Empty : dtr["DateofJoining"].ToString();

                imgPhoto.ImageUrl = "../../showimage.aspx?id=" + dtr["idno"].ToString() + "&type=EMP";
                imgPhoto.Visible = true;
                //}


            }
            // BindNoDuesDetails();
        }
        else
        {
            objCommon.DisplayMessage("Employee Not Found !!", this.Page);
            pnlId.Visible = false;
            imgPhoto.Visible = false;
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Emp_No_Dues.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Emp_No_Dues.aspx");
        }
    }


    private void BindEmployeeList()
    {
        DataTable dt = objECC.RetrieveEmpDetailJoining("AAA", "ALLEMPLOYEE");
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
    }



    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;

        ViewState["IDNO"] = lblIDNo.Text = lnk.CommandArgument.ToString();

        DivSerach.Visible = false;
        ShowEmpDetails(Convert.ToInt32(ViewState["IDNO"].ToString()));

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlId.Visible = false;
        DivSerach.Visible = true;
    }


    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Abstract Salary Report", "PayEmployee_Joining_Report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string IP = Request.ServerVariables["REMOTE_HOST"];
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("PayRoll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
            //@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",
            url += "&param=@P_IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString()) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "payroll_empinfo.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
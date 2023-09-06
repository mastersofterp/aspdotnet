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

public partial class PAYROLL_TRANSACTIONS_Pay_ApprovalOfAssetAllotment : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMaster objEM = new EmpMaster();
    EmpCreateController objECC = new EmpCreateController();
    int collegeno;
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
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //Making panels visible
                pnlSelection.Visible = true;
                BindAssetAllotmentDetails();
                
            }
        }
    }
    public void BindAssetAllotmentDetails()
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")
            {
                ds = objECC.GetAllEmpAssetDetailALL();
            }
            else
            {
                ds = objECC.GetAllEmpAssetDetailALL();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpAssetallotment.DataSource = ds.Tables[0];
                lvEmpAssetallotment.DataBind();
            }
            else
            {
                lvEmpAssetallotment.DataSource = null;
                lvEmpAssetallotment.DataBind();
            }
        }
        else
        {

            return;
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        if (Session["usertype"] != null)
        {
            DataSet ds = null;
            if (Session["usertype"].ToString() == "1")    
            {
                //string   fromdate = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy/MM/dd");
                //string todate = Convert.ToDateTime(txttodate.Text).ToString("yyyy/MM/dd");
                DateTime fromdate = Convert.ToDateTime(txtfromdate.Text);
                DateTime todate = Convert.ToDateTime(txttodate.Text);
                ds = objECC.GetAllEmpAssetDetailDatewise(fromdate, todate);
            }
            else
            {
                //string fromdate = Convert.ToDateTime(txtfromdate.Text).ToString("yyyy/MM/dd");
                //string todate = Convert.ToDateTime(txttodate.Text).ToString("yyyy/MM/dd");
                DateTime fromdate = Convert.ToDateTime(txtfromdate.Text);
                DateTime todate = Convert.ToDateTime(txttodate.Text);
                ds = objECC.GetAllEmpAssetDetailDatewise(fromdate, todate);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpAssetallotment.DataSource = ds.Tables[0];
                lvEmpAssetallotment.DataBind();
            }
            else
            {
                lvEmpAssetallotment.DataSource = null;
                lvEmpAssetallotment.DataBind();
            }
        }
        else
        {

            return;
        }
    }
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("AssetAllotment", "EmployeeAssetReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_EmployeeAssetReport.rpt.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
       
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("payroll")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Payroll," + rptFileName;
           // url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtfromdate.Text) + ",@P_TODATE=" + Convert.ToDateTime(txttodate.Text) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            url += "&param=@P_FROMDATE=" + Convert.ToDateTime(txtfromdate.Text).ToString("yyyy-MM-dd") + ",@P_TODATE=" + Convert.ToDateTime(txttodate.Text).ToString("yyyy-MM-dd") + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_EmployeeAssetReport.rpt.ShowReport()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    static int Assetallotid = 0;
    bool status;
    string strstatus;
    //int EMPID;

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnapproved_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
            Button  btnapprove = sender as Button;

            Assetallotid = int.Parse(btnapprove.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetAllEmpAssetDetailIDWISE(Assetallotid);
            if (dsdues.Tables[0].Rows.Count > 0)
            {
               objEM.IdNo = Convert.ToInt32(dsdues.Tables[0].Rows[0]["IDNO"]);
               objEM.COLLEGE_NO = Convert.ToInt32(dsdues.Tables[0].Rows[0]["COLLEGE_NO"]);
               objEM.ASSETID = Convert.ToInt32(dsdues.Tables[0].Rows[0]["ASSETID"]);
               objEM.ASSETREMARK = dsdues.Tables[0].Rows[0]["ASSETREMARK"].ToString();
               objEM.ASSETNAME = dsdues.Tables[0].Rows[0]["ASSETNAME"].ToString();
               objEM.ISAPPROVED = true;
               objEM.ASSETALLOTID = Assetallotid;
               cs = Convert.ToInt32(objECC.ApprovedEmpAssetAllotment(objEM));
               if (cs == 1)
               {
                   string MSG = "Asset Request Accepted Successfully";
                   MessageBox(MSG);
                   BindAssetAllotmentDetails();
               }
               else
               {

                   string MSG = "Request Failed";
                   MessageBox(MSG);
               }   
            } 
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_ApprovalOfAssetAllotment.aspx.btnapproved_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnnotapproved_Click(object sender, EventArgs e)
    {
        int cs = 0;
        try
        {
            Button btnapprove = sender as Button;

            Assetallotid = int.Parse(btnapprove.CommandArgument);

            // ShowDetails(NoDuesNo);
            DataSet dsdues = objECC.GetAllEmpAssetDetailIDWISE(Assetallotid);
            if (dsdues.Tables[0].Rows.Count > 0)
            {
                objEM.IdNo = Convert.ToInt32(dsdues.Tables[0].Rows[0]["IDNO"]);
                objEM.COLLEGE_NO = Convert.ToInt32(dsdues.Tables[0].Rows[0]["COLLEGE_NO"]);
                objEM.ASSETID = Convert.ToInt32(dsdues.Tables[0].Rows[0]["ASSETID"]);
                objEM.ASSETREMARK = dsdues.Tables[0].Rows[0]["ASSETREMARK"].ToString();
                objEM.ASSETNAME = dsdues.Tables[0].Rows[0]["ASSETNAME"].ToString();
                objEM.ISAPPROVED = false;
                objEM.ASSETALLOTID = Assetallotid;
                cs = Convert.ToInt32(objECC.ApprovedEmpAssetAllotment(objEM));
                if (cs == 1)
                {
                    string MSG = "Asset Request Not Accepted";
                    MessageBox(MSG);
                    BindAssetAllotmentDetails();
                }
                else
                {

                    string MSG = "Request  Failed";
                    MessageBox(MSG);
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_TRANSACTIONS_Pay_ApprovalOfAssetAllotment.aspx.btnnotapproved_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindAssetAllotmentDetails();
    }
}
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;

public partial class TRAININGANDPLACEMENT_Reports_Inplant_student_list : System.Web.UI.Page
{
    private static int i = 0;
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    private static string nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
    SqlConnection conn = new SqlConnection(nitprm_constr);
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            ddlload(ddlcompany);
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


                //ddlSceduleAll.Visible = false;

            }
        }
    }
    
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int COMPANYID = 0;
        COMPANYID = Convert.ToInt32(ddlcompany.SelectedItem.Value);
        Label1.Text = COMPANYID.ToString();
        string qry = "EXEC PKG_ACAD_TP_INPLANT_STUD_LIST_REPORT " + COMPANYID;
        SqlCommand cmd = new SqlCommand(qry, conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        try
        {
            conn.Open();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            


            //
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
       ShowReport("Forwarding_Letter", "tp_forwarding_letter.rpt");
    }
        

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            
           
                //string RptHeader = "SELECTED STUDENT LIST FOR INTERVIEW";
            url += "&param=@COMPID=" + Convert.ToInt32(ddlcompany.SelectedValue);
                    //+ "," + "username=" + Session["userfullname"].ToString();


            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        ShowReport("inplant_studentlist", "inplant_studt.rpt");
    }
    private void ddlload(DropDownList ddlobjectname)
    {

        string qry = "SELECT COMPNAME,COMPANYID FROM ACAD_TP_INPLANT_COMPANYS";
        SqlConnection conn = new SqlConnection(nitprm_constr);
        SqlCommand CMD = new SqlCommand(qry, conn);
        SqlDataAdapter DA = new SqlDataAdapter(CMD);
        DataSet ds1 = new DataSet();
        try
        {
            conn.Open();
            DA.Fill(ds1, "COMPANYINFO");

            //
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message.ToString();
        }
        finally
        {
            conn.Close();
        }
        //ListItem LITEM = new ListItem();
        //LITEM.Text = "PLEASE SELECT";
        //LITEM.Value = "0";
        //DropDownList4.Items.Add(LITEM.Text);
        //DropDownList4.Items.Add(LITEM.Value);

        ddlobjectname.DataSource = ds1;
        ddlobjectname.DataValueField = ds1.Tables["COMPANYINFO"].Columns["COMPANYID"].ToString();
        ddlobjectname.DataTextField = ds1.Tables["COMPANYINFO"].Columns["COMPNAME"].ToString();
        ddlobjectname.DataBind();
        ddlobjectname.SelectedIndex = 0;
    }
}

using System;
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
using IITMS.NITPRM;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.IO;


public partial class Itle_Assignment_report_of_all_student : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TestResult objResult = new TestResult();
    AssignmentController objAM = new AssignmentController();


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
               
                FillDropDownList();
                Page.Title = Session["coll_name"].ToString();

                // FillDropdown();

            }
            FillDropDownList();
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Student_Result_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Student_Result_Report.aspx");
        }
    }


    protected void FillDropDownList()
    {

        DataSet ds = objCommon.FillDropDown("ACD_SESSION_MASTER A INNER JOIN  ACD_COLLEGE_MASTER B ON (A.COLLEGE_ID=B.COLLEGE_ID)", "A.SESSIONNO", "CONCAT( A.SESSION_NAME ,' - ', B.COLLEGE_NAME)", "", "A.SESSIONNO desc");

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ddlsession.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
        }
      
        
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlsession.SelectedValue; //hdn1.Value.Trim(); 
             Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try  
        {
            if (ddlsession.SelectedValue != "" )
            {
                
                    ShowReport("Itle_Student_Result", "Assignment_report_of_all_student.rpt");
               
            }
            else
            {
                objCommon.DisplayUserMessage(UpdatePanel1, "Please Select Session !", this.Page);
                return;
            }


        }

        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "ITLE_StudentResultReport.btnSubmit_Click->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

   

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnexport_Click(object sender, System.EventArgs e)
    {
        string sessionnos = string.Empty;
        foreach (ListItem items in ddlsession.Items)  
        {
            if (items.Selected == true)
            {
                sessionnos += (items.Value).Split('-')[0] + ','; //Add by maithili [08-09-2022]

                //collegenos += items.Value + ','; 
                //collegenames += items.Text + ',';
            }
        }
        DataSet ds = objAM.GetAllStudentAssignmentResult_Of_All_Student(sessionnos);
        if ((ds == null) || (ds.Tables[0].Rows.Count == 0))
        {
            objCommon.DisplayUserMessage(this, " Details Are Not Found...!", this);

            return;
        }

        GridView gvData = new GridView();

        gvData.DataSource = ds;
        gvData.DataBind();


        if (ds.Tables[0].Rows.Count > 0)
        {
            //To add heading in excel
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            
           
            string attachment = "attachment; filename=" + "Assignment Report Of All Student" + ".xls";
           
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();



        }

    }
    }
    
    

//======================================================================================
// PROJECT NAME  : RF-CAMPUS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     :DETENTION REPORT                                 
// CREATION DATE : 28/01/2021
// ADDED BY      : SAFAL GUPTA                                               
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_Detention_Report : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objStudentController = new StudentController();

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_SCHEME_MAPPING CM INNER JOIN ACD_DETENTION_INFO D ON (CM.SCHEMENO=D.SCHEMENO AND CM.ORGANIZATIONID=D.ORGANIZATIONID)", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND CM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_DETENTION_INFO D ON(S.SESSIONNO=D.SESSIONNO AND S.ORGANIZATIONID=D.ORGANIZATIONID)", "DISTINCT S.SESSIONNO", "S.SESSION_PNAME", "S.SESSIONNO > 0 AND ISNULL(PROV_DETAIN,0)=0 AND ISNULL(FINAL_DETAIN,0)=0 AND ISNULL(CANCEL_DETAIN,0)=1 AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "S.SESSIONNO DESC");
            }

        }

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }

        string strHostName = System.Net.Dns.GetHostName();
        string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Detention_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Detention_Report.aspx");
        }
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSemester.Items.Clear();
        ddlSemester.Items.Add(new ListItem("Please Select", "0"));

        if (ddlSession.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER S INNER JOIN ACD_DETENTION_INFO CT ON S.SEMESTERNO=CT.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + " AND CT.SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + "", "S.SEMESTERNO");
        }
        else
        {
            ddlSemester.SelectedIndex = 0;
        }
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlClgname.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlClgname.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                
            }
        }
        else
        {
            objCommon.DisplayMessage("Please Select College & Regulation", this.Page);
            ddlClgname.Focus();
        }

    }
   
    protected void btnReport_Click(object sender, EventArgs e)
    {
        //code for Provisional  PDF Report 
        DataSet ds = objStudentController.Get_Detent_StudentData(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));

       if (ds.Tables[0].Rows.Count > 0)
       {
           try
           {
               string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
               url += "Reports/CommonReport.aspx?";
               url += "pagetitle=REPORT";
               url += "&path=~,Reports,Academic," + "rptDetention_Report.rpt";
               url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);

               divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
               divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
               divMsg.InnerHtml += " </script>";
           }
           catch (Exception ex)
           {
               throw;
           }
       }
       else
       {
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Data Not Found in Mark Table,So that Record Cannot be generate')", true);
           objCommon.DisplayMessage(upd1, "Data Not Found", this.Page);
           return;
       }
      // Clear();
  }

    private void Clear()
    {
        ddlSession.SelectedValue = "0";
        ddlClgname.SelectedValue = "0";
        ddlSemester.SelectedValue = "0";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnCoursewisereport_Click(object sender, EventArgs e)
    {

        // Code added Safal Gupta on 29012021
        GridView GvStudent = new GridView();
        DataSet dsfee = objStudentController.GetAllStudentDetentionFinalEXCEL(Convert.ToInt32(ddlSession.SelectedValue));
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=Final_Detention.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            objCommon.DisplayMessage(upd1, "Data Not Found", this.Page);
        }
       }

    protected void btnNillReport_Click(object sender, EventArgs e)
    {
        // Code added Safal Gupta on 29012021
        GridView GvStudent = new GridView();
        //DataSet dsfee = objStudentController.GetAllStudentDetentionProEXCEL(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));
        DataSet dsfee = objStudentController.GetAllStudentDetentionProEXCEL(Convert.ToInt32(ddlSession.SelectedValue));
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=Provisional_Detention "+"_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(upd1,"Data Not Found",this.Page);
        }
    }
  
    protected void btnProvisionalReport_Click(object sender, EventArgs e)
    {
        //code for Provisional PDF Report 

        DataSet ds = objStudentController.Get_Provisional_Detent_StudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["degreeno"]), Convert.ToInt32(ViewState["branchno"]), Convert.ToInt32(ViewState["schemeno"]), Convert.ToInt32(ddlSemester.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {
            try
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=REPORT";
                url += "&path=~,Reports,Academic," + "rptProDetention_Report.rpt";
               // url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + ddlDegree.SelectedValue + ",@P_BRANCHNO=" + ddlBranch.SelectedValue + ",@P_SCHEMENO=" + ddlScheme.SelectedValue + ",@P_SESSIONNAME=" + ddlSession.SelectedItem.Text + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue;//+ ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_DEGREENO=" + Convert.ToInt32(ViewState["degreeno"]) + ",@P_BRANCHNO=" + Convert.ToInt32(ViewState["branchno"]) + ",@P_SCHEMENO=" + Convert.ToInt32(ViewState["schemeno"]) + ",@P_SEMESTERNO=" + ddlSemester.SelectedValue + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','REPORT','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.upd1, this.upd1.GetType(), "controlJSScript", sb.ToString(), true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Data Not Found in Mark Table,So that Record Cannot be generate')", true);
            objCommon.DisplayMessage(upd1, "Data Not Found", this.Page);
            return;
        }
    }
    protected void btnCancelDetention_Click(object sender, EventArgs e)
    {
        
        GridView GvStudent = new GridView();
        DataSet dsfee = objStudentController.GetAllStudentDetentionCancelExcel(Convert.ToInt32(ddlSession.SelectedValue));
        if (dsfee.Tables[0].Rows.Count > 0)
        {
            GvStudent.DataSource = dsfee;
            GvStudent.DataBind();
            string attachment = "attachment; filename=Cancel_Detention.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GvStudent.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage(upd1, "Data Not Found", this.Page);
        }
    }
}

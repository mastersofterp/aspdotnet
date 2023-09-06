using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Reports_IP_StudRegisterList : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objStud = new TPController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

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
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                objCommon.FillDropDownList(ddlCompany, "ACD_TP_COMPANY", "COMPID", "COMPNAME", "INPLANT='Y'", "COMPNAME");
                ddlCompany.Items.Add("Other");
                pnllist.Visible = false;
                pnlchoice.Visible = false;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (radlTransfer.SelectedValue == "R")
            {
                ShowReport("IPStudRegList", "IPStudRegList.rpt");
            }
            else if (radlTransfer.SelectedValue == "C")
            {
                ShowReport("IPFinalList", "IPFinalList.rpt");
            }
            else if (radlTransfer.SelectedValue == "S")
            {
                if (radlSelect.SelectedValue == "R")
                {
                    ShowReport("IPFinalList", "IP_SortedList.rpt");  
                }
                else if (radlSelect.SelectedValue == "E")
                {
                    int SESSIONNO = Convert.ToInt32(objCommon.LookUp("ACD_REFE", "ISNULL(TP_CURRENT_SESSIONNO,0)AS TP_CURRENT_SESSIONNO", ""));
                    DataSet ds = objStud.GetSortedStudlist(Convert.ToInt32(ddlCompany.SelectedValue), SESSIONNO);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pnllist.Visible = true;
                        lvPinfo.DataSource = ds.Tables[0];
                        lvPinfo.DataBind();
                    }
                    else
                    {
                        pnllist.Visible = false;
                        lvPinfo.DataSource = null;
                        lvPinfo.DataBind();
                    }
                }
            }
            else if (radlTransfer.SelectedValue == "P")
            {
                ShowReport("IPPreferenceList", "IP_PreferenceList.rpt");                
            }
            else if (radlTransfer.SelectedValue == "A")
            {
                ShowReport("IPCompanywiseSummary","IP_CompSummary.rpt");
            }
            

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudentList.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string SESSIONNO = objCommon.LookUp("ACD_REFE", "ISNULL(TP_CURRENT_SESSIONNO,0)AS TP_CURRENT_SESSIONNO", "");
            string SessionName =objCommon.LookUp("ACD_TP_SESSION","SESSIONNAME","SESSIONNO="+ SESSIONNO);
            int compid = 0;
            if (ddlCompany.SelectedValue.Equals("Other"))
            {
                compid = -1;
            }
            else
            {
                compid = Convert.ToInt32(ddlCompany.SelectedValue);
            }
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;
            
            if (radlTransfer.SelectedValue == "R")
            {

                url += "&param=@P_SESSIONNO=" + SESSIONNO + ",username=" + Session["userfullname"].ToString() + ",SessionName=" + SessionName.ToString();
            }
            else if (radlTransfer.SelectedValue == "C")
            {
                url += "&param=@P_SESSIONNO=" + SESSIONNO + ",@P_COMPID=" + compid + ",SessionName=" + SessionName.ToString() + ",username=" + Session["userfullname"].ToString(); 
            }
            else if (radlTransfer.SelectedValue == "S")
            {
                url += "&param=@P_SESSIONNO=" + SESSIONNO + ",@P_COMPID=" + Convert.ToInt32(ddlCompany.SelectedValue) + ",SessionName=" + SessionName.ToString() + ",username=" + Session["userfullname"].ToString();  
            }
            else if (radlTransfer.SelectedValue == "P")
            {
                url += "&param=@P_SESSIONNO=" + SESSIONNO + ",@P_COMPID=" + compid + ",SessionName=" + SessionName.ToString() + ",username=" + Session["userfullname"].ToString();  
            }
            else if (radlTransfer.SelectedValue == "A")
            {
                url += "&param=@P_SESSIONNO=" + SESSIONNO + ",@P_COMPID=" + compid + ",SessionName=" + SessionName.ToString() + ",username=" + Session["userfullname"].ToString();
            }

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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //if (rdoStudinfo.Checked)
            //    lvStudinfo.RenderControl(htmlWrite);
            //else if (rdoPersinfo.Checked)
            //    lvPinfo.RenderControl(htmlWrite);
            //else if (rdoAcadinfo.Checked)
            //    lvAcademic.RenderControl(htmlWrite);
            //else if (rdoOthinfo.Checked)
            //    lvOther.RenderControl(htmlWrite);
            lvPinfo.RenderControl(htmlWrite);

           

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_IP_StudRegisterList.btnExport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void radlTransfer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radlTransfer.SelectedValue == "S")
        {
            pnlchoice.Visible = true;
        }
        else
        {
            pnlchoice.Visible = false;
            pnllist.Visible = false;
        }
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.IO;
using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class HostelStudentMessApply : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    HostelController objhos = new HostelController();

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
        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
        Page.ClientScript.RegisterClientScriptInclude("selective", ResolveUrl(@"~\includes\prototype.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective1", ResolveUrl(@"~\includes\scriptaculous.js"));
        Page.ClientScript.RegisterClientScriptInclude("selective2", ResolveUrl(@"~\includes\modalbox.js"));

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
                if (Session["username"].ToString() != string.Empty)
                {
                    ViewState["usertype"] = Session["usertype"];
                    ShowStudentDetails();
                    if (ViewState["usertype"].ToString() == "2")
                    {
                        imgSearch.Visible = false;
                    }
                }
            }
        }
        else
        {

            if (Page.Request.Params["__EVENTTARGET"] != null)
            {
                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btnsearch"))
                {
                    string[] arg = Page.Request.Params["__EVENTARGUMENT"].ToString().Split(',');
                    bindlist(arg[0], arg[1]);
                }

                if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("btncancelmodal"))
                {
                    txtSearch.Text = string.Empty;
                    lblNoRecords.Text = string.Empty;
                }
                int idno = 0;
                if (ViewState["usertype"].ToString() == "2")
                {
                    idno = Convert.ToInt32(Session["idno"]);

                }
                else
                {
                    if (Request.QueryString["id"] != null)
                    {
                        idno = Convert.ToInt32(Request.QueryString["id"].ToString());
                    }
                }
                
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + idno + "&type=student";
            }
        }
    }
    private void bindlist(string category, string searchtext)
    {
        StudentController objSC = new StudentController();
        DataSet ds = objSC.RetrieveStudentDetails(searchtext, category);

        if (ds.Tables[0].Rows.Count > 0)
        {
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
            lblNoRecords.Text = "Total Records : 0";
    }
    protected void lnkId_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Response.Redirect(url + "&id=" + lnk.CommandArgument);
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    private void ShowStudentDetails()
    {
        int idno = 0;
         SqlDataReader dtr = null;

        if (ViewState["usertype"].ToString() == "2")
        {
            idno = Convert.ToInt32(Session["idno"]);
        }
        else
        {
            if (Request.QueryString["id"] != null)
            {
                idno = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
        }
        dtr = objhos.Get_Hostel_Student_Detail(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                bool hosteler = Convert.ToBoolean(dtr["HOSTELER"]);
                lblregno.Text = dtr["REGNO"].ToString();
                lblregno.ToolTip = Convert.ToString(idno);
                lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                lblName.ToolTip = dtr["HOSTELER"] == null ? string.Empty : dtr["HOSTELER"].ToString();
                lblBranch.Text = dtr["BRANCHNAME"].ToString();
                lblSemester.Text = dtr["SEMESTERNAME"] == null ? string.Empty : dtr["SEMESTERNAME"].ToString();
                lblSemester.ToolTip = dtr["SEMESTERNO"].ToString();
                lblhostelname.Text = dtr["HOSTELNAME"] == null ? string.Empty : dtr["HOSTELNAME"].ToString();
                lblmessname.Text = dtr["MESSNAME"] == null ? string.Empty : dtr["MESSNAME"].ToString();

                lblhostelname.ToolTip = dtr["HOSTEL_NO"] == null ? string.Empty : dtr["HOSTEL_NO"].ToString();
                lblmessname.ToolTip = dtr["MESS_NO"] == null ? string.Empty : dtr["MESS_NO"].ToString();

                lblsession.Text = objCommon.LookUp("ACD_HOSTEL_SESSION", "SESSION_NAME", "HOSTEL_SESSION_NO=" + dtr["HOSTEL_SESSION_NO"]);
                lblsession.ToolTip = dtr["HOSTEL_SESSION_NO"].ToString();
                lblschmonth.Text = dtr["SCH_MONTH"].ToString();

                lblallotedmessbname.ToolTip = dtr["ALLOTED_MESSNO"] == null ? string.Empty : dtr["ALLOTED_MESSNO"].ToString();
                lblallotedmessbname.Text = dtr["ALLOTED_MESSNAME"] == null ? string.Empty : dtr["ALLOTED_MESSNAME"].ToString();


                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + idno + "&type=student";
                show_vacantmess_summery();

                if (hosteler && dtr["ELIGIABLE"].ToString().Trim() == "Yes")
                {
                    tr2.Visible = true;
                    btnSubmit.Visible = true;
                    btnCancel.Visible = true;
                }
                else
                {
                    lblMsg.Text = dtr["MSG"].ToString();
                    tr2.Visible = false;
                    btnSubmit.Visible = false;
                    btnCancel.Visible = false;
                    string MONTH_MESS_NO = dtr["MONTH_MESS_NO"] == null ? string.Empty : dtr["MONTH_MESS_NO"].ToString();
                    if (MONTH_MESS_NO.ToString().Length > 0)
                    {
                        if (Convert.ToInt16(MONTH_MESS_NO) > 0)
                        {
                           btnPrint.Visible = true;
                        }
                    }
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int updateby = 0;
        updateby = Convert.ToUInt16(Session["userno"]);

        if (Isvalid())
        {
            int output = objhos.InsertMessRequest(Convert.ToInt16(lblregno.ToolTip), Convert.ToInt32(rblMess.SelectedValue), Convert.ToInt32(lblsession.ToolTip), Convert.ToInt32(lblhostelname.ToolTip), updateby, txtpurpose.Text,lblschmonth.Text);
            if (output != -99)
            {
                objCommon.DisplayMessage("Record Save Successfully!!!", this.Page);
                ShowStudentDetails();
                ClearAll();
            }
        }
    }
    private void ClearAll()
    {
        ddlMess.SelectedIndex = 0;
        txtpurpose.Text = string.Empty;
    }
    private bool Isvalid()
    {
        if (rblMess.SelectedValue=="") 
        {
            objCommon.DisplayMessage("Please Select Mess", this.Page);
            return false;
        }
        else 
        if (txtpurpose.Text == string.Empty)
        {
            objCommon.DisplayMessage("Please Enter Remark", this.Page);
            return false;
        }
        
        return true;
    }

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        ClearAll();
    }
    private void show_vacantmess_summery()
    {
        DataSet dsMess = objhos.Get_Mess_Seat_Detail(Convert.ToInt16(lblregno.ToolTip), Convert.ToInt32(ddlMess.SelectedValue), Convert.ToInt32(lblsession.ToolTip), Convert.ToInt32(lblhostelname.ToolTip), 0, 0, lblschmonth.Text);
         if (dsMess.Tables[0].Rows.Count > 0)
         {
             ddlMess.Items.Clear();
             ddlMess.Items.Add("Please Select");
             ddlMess.SelectedItem.Value = "0";

            
             lvMess.DataSource = dsMess;
             lvMess.DataBind();
             
             //DataTable dtmess = dsMess.Tables[0].Select("VACANT >0 AND MESS_NO <> '" + lblmessname.ToolTip +"'" ).CopyToDataTable();
             //DataTable dtmess = dsMess.Tables[0];
             DataTable dtmess = dsMess.Tables[0].Select("VACANT > 0 AND MESS_NO <> '" + lblmessname.ToolTip +"'" ).CopyToDataTable();
             

             rblMess.DataSource = dtmess;
             rblMess.DataTextField = dtmess.Columns[1].ToString();
             rblMess.DataValueField = dtmess.Columns[0].ToString();
             rblMess.DataBind();
            
        }
    }
    protected void ddlMess_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMess.SelectedValue == lblmessname.ToolTip)
        {
            objCommon.DisplayMessage("Mess Already Alloted", this.Page);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("Mess Allotment Receipt", "MessAllotmentReceipt.rpt");
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(lblregno.ToolTip) + ",@P_HOSTEL_SESSION_NO=" + Convert.ToInt32(lblsession.ToolTip)
                + ",@P_UPDATE_MONTH=" + Convert.ToString(lblschmonth.Text);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_REPORTS_ElectionVotingResultReport.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
}

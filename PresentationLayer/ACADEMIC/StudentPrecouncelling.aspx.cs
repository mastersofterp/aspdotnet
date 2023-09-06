using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;


public partial class ACADEMIC_StudentPrecouncelling : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();

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
        try
        {
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
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
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //Page Authorization
                    this.CheckPageAuthorization();

                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
      

                    if (rdbSelection.SelectedValue.ToString() == "0")
                    {
                        rApplicationID.Visible = true;
                        btnShow.Visible = true;
                        divdetails.Visible = true;
                        rAdmbatch.Visible = false;
                        btnPdfreport.Visible = false;
                    }
                    
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentPrecouncelling.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");

        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudentPrecouncelling.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudentPrecouncelling.aspx");
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = null;
            string userno = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtAppID.Text.ToString() + "'");

            

            if (userno != string.Empty)
            {
                int status = Convert.ToInt32(objCommon.LookUp("ACD_USER_PROFILE_STATUS", "ISNULL(CONFIRM_STATUS,0) AS CONFIRM_STATUS", "USERNO='" + userno.ToString() + "'"));
                if (status == 1)
                {
                    ViewState["newuser"] = userno;

                    objCommon.FillDropDownList(ddlentranceExam, "ACD_QUALEXM", "QUALIFYNO", "QUALIEXMNAME", "QUALIFYNO>0 AND QEXAMSTATUS='E'", "QUALIFYNO");

                    ds = objCommon.FillDropDown("ACD_USER_REGISTRATION UR LEFT JOIN ACD_PRECOUNCEL_ONLINE_ADM P ON(P.USERNO=UR.USERNO) LEFT JOIN ACD_QUALEXM Q ON(Q.QUALIFYNO=P.ENTRANCE_EXAMNO)", "UR.USERNO,USERNAME", "UR.FIRSTNAME,LASTNAME,Q.QUALIEXMNAME,ISNULL(ENTRANCE_EXAMNO,0)ENTRANCE_EXAMNO,SCHOLARSHIP_STATUS,EMAILID,MOBILENO,ADMBATCH", "  UR.USERNAME='" + txtAppID.Text + "'", "");

                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        fldstudent.Visible = true;

                        lblAppID.Text = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                        lblName.Text = ds.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                        lblEmail_id.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        lblMobileNo.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();

                        if (ds.Tables[0].Rows[0]["ENTRANCE_EXAMNO"].ToString() != string.Empty)
                        {
                          
                            ddlentranceExam.SelectedValue = ds.Tables[0].Rows[0]["ENTRANCE_EXAMNO"].ToString();
                            if (ddlentranceExam.SelectedValue != "0")
                            {
                                ddlentranceExam.Enabled = false;
                                btnSubmit.Visible = false;
                            }
                        }

                        if (ds.Tables[0].Rows[0]["SCHOLARSHIP_STATUS"].ToString() != string.Empty)
                        {
                            
                            rdbScholarship.SelectedValue = ds.Tables[0].Rows[0]["SCHOLARSHIP_STATUS"].ToString();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Either Application ID Not found or Application Fees not paid. ", this.Page);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Either Application ID Not found or Application not confirm. ", this.Page);
                    txtAppID.Text = string.Empty;
                    txtAppID.Focus();
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Enter Correct Application ID.", Page);
                txtAppID.Text=string.Empty;
                txtAppID.Focus();
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentPrecouncelling.btnShow_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status;
            int Scholarship_status = 0;
         
          
            if (rdbScholarship.SelectedValue != "0" && rdbScholarship.SelectedValue != "1")
            {
                objCommon.DisplayMessage(updLists, "Please Select Scholarship Status.", this.Page);
                return;
            }
            else
                Scholarship_status = Convert.ToInt32(rdbScholarship.SelectedValue);

            status = objSC.InsertPreCouncelingDetali(Convert.ToInt32(ViewState["newuser"]), Convert.ToInt32(ddlentranceExam.SelectedValue), Scholarship_status, ViewState["ipAddress"].ToString(), Convert.ToInt32(Session["userno"]));
              
                if (status == 1)
                {
                    objCommon.DisplayMessage("Pre Counceling done successfully", this.Page);
                    btnShow_Click(sender, e);
                }
                else if (status == 2)
                {
                    objCommon.DisplayMessage("Pre Counceling update successfully", this.Page);
                    btnShow_Click(sender, e);
                }
                else
                {
                    objCommon.DisplayMessage("Error in submitting the data", this.Page);
                }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentPrecouncelling.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnPdfreport_Click(object sender, EventArgs e)
    {
        this.ShowReport("Pre_Counceling_Report", "rptStudentPreCounceling.rpt", 1);
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowReport(string reportTitle, string rptFileName, int report_type)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ACADEMIC")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            if (report_type == 1)
                //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
                url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ADMBATCH=" + ddlAdmbatch.SelectedValue;
            //else
            //  //  url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(ViewState["newuser"]) + ",@P_ADMBATCH=" + Convert.ToInt32(ddlSession.SelectedValue);
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USERNO=" + Convert.ToInt32(ViewState["newuser"]) + ",@P_ADMBATCH=0";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updLists, this.updLists.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_StudentPrecouncelling.btnSubmit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnSingleReport_Click(object sender, EventArgs e)
    {
        //this.ShowReport("Document_Verified_Report", "rptStudentdocReport.rpt", 2);
    }
    protected void rdbSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbSelection.SelectedValue.ToString() == "0")
        {
            btncancel_Click(sender, e);
        }
        else if (rdbSelection.SelectedValue.ToString() == "1")
        {
            rApplicationID.Visible = false;
            btnShow.Visible = false;
            divdetails.Visible = false;
            btnPdfreport.Visible = true;
            rAdmbatch.Visible = true;
            objCommon.FillDropDownList(ddlAdmbatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNO");
        }

    }

    

}


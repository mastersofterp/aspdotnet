using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class HOSTEL_GATEPASS_AddHostelGatePass : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    AddHostelGatePass objGatePass = new AddHostelGatePass();
    AddHostelGatePassController objHGP = new AddHostelGatePassController();

    #region Page Events
    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Set MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            imgPhoto.ImageUrl = "~/images/nophoto.jpg";
            if (!Page.IsPostBack)
            {
                // Check User Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    // Check User Authority 
                    this.CheckPageAuthorization();

                    // Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    // Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_Master_GatePassMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #endregion Page Events

    #region Action
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        objGatePass.PassNo = Convert.ToInt32(txtPass.Text.Trim());

        string gatepassno = objCommon.LookUp("ACD_HOSTEL_GATEPASS_DETAILS HGD INNER JOIN ACD_HOSTEL_ROOM_ALLOTMENT HRA ON(HGD.IDNO = HRA.RESIDENT_NO AND HGD.HOSTEL_SESSION_NO = HRA.HOSTEL_SESSION_NO)", "HGD.HOSTEL_GATE_PASS_NO", "HRA.CAN=0 AND HGD.HOSTEL_GATE_PASS_NO = '" + txtPass.Text + "'");
        if (gatepassno == "")
        {
            objCommon.DisplayMessage("Record Not Found", this.Page);
            pnlList.Visible = true;
            pnlinfo.Visible = false;
            btnReport.Visible = false;
        }
        else
        {
            // FillInformation();
            DisplayInfo(Convert.ToInt32(gatepassno));
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Hostel Gate Pass Report", "HostelGatePassReport.rpt");
    }

    private void DisplayInfo(int gatepassno)
    {
        DataSet ds = null;
        try
        {
            ds = objHGP.GetHostelGatePassInfo(gatepassno);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                pnlList.Visible = false;
                DataRow dr = ds.Tables[0].Rows[0];
                /// show student information

                lblName.Text = dr["STUDNAME"].ToString();
                lbladno.Text = dr["REGNO"].ToString();
                lblhostel.Text = dr["HOSTEL_NAME"].ToString();
                lblRoom.Text = dr["ROOM_NO"].ToString();
                lblvalFrom.Text = dr["OUTDATE"].ToString();
                lblvalTo.Text = dr["INDATE"].ToString();
                lblapproval1.Text = dr["FIRST_APPROVER"].ToString();
                lblapproval2.Text = dr["SECOND_APPROVER"].ToString();
                lblapproval3.Text = dr["THIRD_APPROVER"].ToString();
                lblapproval4.Text = dr["FOURTH_APPROVER"].ToString();

                //if (dr["PHOTO"].ToString() != "")
                imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dr["IDNO"].ToString() +"&type=STUDENT";

                pnlinfo.Visible = true;
                btnReport.Visible = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "HOSTEL_AddHostelGatePass.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable");
        }

    }
    #endregion Action

    #region Private Methods
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("hostel")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Hostel," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_GATEPASSNO=" + Convert.ToInt32(txtPass.Text);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "HOSTEL_AddHostelGatePass.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AddHostelGatePass.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AddHostelGatePass.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            DataSet ds = objHGP.GetAllGatePass();
            lvPurpose.DataSource = ds;
            lvPurpose.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Hostel_GATEPASS_AddHostelGatePass.BindListView --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Private Methods

    protected void btnInTimeEntry_Click(object sender, EventArgs e)
    {
        string GatePassNo = txtPass.Text.Trim();

        CustomStatus cs = (CustomStatus)objHGP.UpdateColumnData("ACD_HOSTEL_GATEPASS_DETAILS", "IN_TIME_ENTRY=GETDATE()", "HOSTEL_GATE_PASS_NO='" + GatePassNo + "'");

        if (cs.Equals(CustomStatus.RecordUpdated))
        {
            objCommon.DisplayMessage("In Time updated successfully.", this.Page);
        }
    }
}
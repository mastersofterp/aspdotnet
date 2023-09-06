using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_Advance_Process_Approval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AdvancePassingAuthorityController objAdvPassAuthCon = new AdvancePassingAuthorityController();
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
                CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                AdvancePassingAuthority objAPAuth = new AdvancePassingAuthority();
                pnlAdd.Visible = false;
                pnlvedit.Visible = false;
                pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"].ToString());
                ViewState["USERNO"] = usernock;               
                string PANO =(objCommon.LookUp("PAYROLL_ADVANCE_PASSING_AUTHORITY", "PANO", "UA_NO=" + Convert.ToInt32(ViewState["USERNO"])));               
                ViewState["PANO"] = PANO;
                BindAdvanceApplPendingList();
                btnHidePanel.Visible = false;
                trfrmto.Visible = false;
                trbutshow.Visible = false;
                txtFromdt.Text = System.DateTime.Now.ToString();
                txtTodt.Text = System.DateTime.Now.ToString();
                ViewState["ModifyLeave"] = "add";


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
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }
    protected void BindAdvanceApplPendingList()
    {
        try
        {
            DataSet ds = objAdvPassAuthCon.GetPendListforAdvanceApproval(Convert.ToInt32(ViewState["USERNO"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                dpPager.Visible = false;
            }
            else
            {
                dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.BindLVAdvanceApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    private void clear()
    {
        txtAdvanceAmount.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        ViewState["EMPNO"] = ViewState["COLLEGE_NO"] = ViewState["STNO"] = null;
       
        divAuthorityList.Visible = false;
        
        pnlButton.Visible = false;
        clear_lblvalue();
    }
    private void clear_lblvalue()
    {
        lblEmpName.Text = string.Empty;      
        lblReason.Text = string.Empty;
        lblApplyDate.Text = string.Empty;       
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int ANO = int.Parse(btnApproval.CommandArgument);
            Session["ANO"] = ANO;
            ShowDetails(ANO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            pnlButton.Visible = true;

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
 
    private void ShowDetails(Int32 ANO)
    {
        DataSet ds = new DataSet();
        try
        {
            ds = objAdvPassAuthCon.GetAdvanceApplDetail(ANO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ANO"] = ANO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();            
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                txtAdvanceAmount.Text = ds.Tables[0].Rows[0]["ADVANCEAMOUNT"].ToString();
                lblApplyDate.Text = ds.Tables[0].Rows[0]["ADVCANEDATE"].ToString();
               // ViewState["PANO"] = ds.Tables[0].Rows[0]["PANO"].ToString();
            }
            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();
            divAuthorityList.Visible = true;         
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            AdvancePassingAuthority objAPAuth = new AdvancePassingAuthority();
            objAPAuth.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
            int ANO = Convert.ToInt32(ViewState["ANO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
             if (Status == "Approve".ToString().Trim())
            {
                Status = "A".ToString().Trim();
            }
            else if (Status == "Reject".ToString().Trim())
            {
                Status = "R".ToString().Trim();
            }

           objAPAuth.ADVCANSTATUS = Status;         
           DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
           objAPAuth.ANO = Convert.ToInt32(Session["ANO"]);
           objAPAuth.APPROVEREJECTDATE = Aprdate;
           objAPAuth.ADVANCEAMOUNT = Convert.ToDouble(txtAdvanceAmount.Text);
           objAPAuth.APPROVEREJECTREMARK = txtRemarks.Text.ToString();
           objAPAuth.PANO = Convert.ToInt32(ViewState["PANO"].ToString());
        
          //TO UPDATE ADVANCE APPLICATION ENTRY

         
           CustomStatus lcs = (CustomStatus)objAdvPassAuthCon.UpdateAppPassEntry(objAPAuth);
          if (lcs.Equals(CustomStatus.RecordUpdated))
          {
              MessageBox("Record Updated Successfully");
              pnlAdd.Visible = false;
              pnlvedit.Visible = false;
              pnllist.Visible = true;
              ViewState["action"] = null;
              clear_lblvalue();
              clear();
              BindAdvanceApplPendingList();
          }
         
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        trfrmto.Visible = true;
        trbutshow.Visible = true;
        this.BindLVLeaveApprStatusAll();
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DataSet ds = objAdvPassAuthCon.GetPendListforAdvanceApprovalStatusALL(Convert.ToInt32(Session["USERNO"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                dpPager.Visible = false;
            }
            else
            {
                dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVAdvanceApprStatusParticular();
    }

    protected void BindLVAdvanceApprStatusParticular()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);

            DataSet ds = objAdvPassAuthCon.GetPendListforLVApprovalStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                dpPager.Visible = false;
            }
            else
            {
                dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_PROCESS_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = false;

        pnlAdd.Visible = false;
        pnllist.Visible = true;
        trfrmto.Visible = false;
        trbutshow.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnlvedit.Visible = false;
        pnllist.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
    }
}
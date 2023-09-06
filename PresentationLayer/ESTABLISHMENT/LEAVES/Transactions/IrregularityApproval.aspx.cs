using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

public partial class ESTABLISHMENT_LEAVES_Transactions_IrregularityApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();
    Leaves objLM = new Leaves();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;

                pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"]);
                BindLVLeaveApplPendingList();
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

    protected void BindLVLeaveApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforIrregularityApproval(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvPendingList.DataSource = ds;
            lvPendingList.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
         try
        {
            Button btnApproval = sender as Button;
            int IRRTRNO = int.Parse(btnApproval.CommandArgument);
            Session["IRRTRNO"] = IRRTRNO;

            ShowDetails(IRRTRNO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            pnlButton.Visible = true;
            lnkbut.Visible = false;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnApproval_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void ShowDetails(Int32 IRRTRNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetIrregularLeaveApproval(IRRTRNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["IRRTRNO"] = IRRTRNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblDept.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
                lblDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
                lblDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                lblInTime.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
                lblOutTime.Text = ds.Tables[0].Rows[0]["OUTTIME"].ToString();
                lblWorking.Text = ds.Tables[0].Rows[0]["WORKHOURS"].ToString();
                lblIrregularstatus.Text = ds.Tables[0].Rows[0]["IRREGULARITYSTATUS"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                lblShiftIn.Text = ds.Tables[0].Rows[0]["SHIFTINTIME"].ToString();
                lblShiftOut.Text = ds.Tables[0].Rows[0]["SHIFTOUTTIME"].ToString();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                string FORWARD_APPROVAL_STATUS = ds.Tables[2].Rows[0]["FORWARD_APPROVAL_STATUS"].ToString();
                if (FORWARD_APPROVAL_STATUS != "F".ToString().Trim())
                {
                    ListItem removeItem = ddlSelect.Items.FindByValue("F");
                    ddlSelect.Items.Remove(removeItem);
                    removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Remove(removeItem);
                }
                else
                {
                    ddlSelect.Items.Clear();
                    ddlSelect.Items.Add("Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Add("Approve & Final Submit");
                    ddlSelect.Items.Add("Reject");
                }

            }
            int UA_No = 0;
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                {
                    UA_No = Convert.ToInt32(ds.Tables[1].Rows[i]["UA_No"].ToString());
                }
            }
            if (Convert.ToInt32(Session["userno"]) == UA_No)     //// if (Convert.ToInt32(Session["userno"]) == UA_No)
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("F");
                ddlSelect.Items.Remove(removeItem);
                removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                ddlSelect.Items.Remove(removeItem);
            }
            else
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("A");
                ddlSelect.Items.Remove(removeItem);
                removeItem = ddlSelect.Items.FindByValue("Approve & Final Submit");
                ddlSelect.Items.Remove(removeItem);
            }
            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();
            divAuthorityList.Visible = true;          
          
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int IRRTRNO = Convert.ToInt32(ViewState["IRRTRNO"].ToString());
            int UA_NO = Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            if (Status == "Forward To Next Authority(Recommended)".ToString().Trim())
            {
                Status = "F".ToString().Trim();
            }
            else if (Status == "Approve & Final Submit".ToString().Trim())
            {
                Status = "A".ToString().Trim();
            }
            else if (Status == "Reject".ToString().Trim())
            {
                Status = "R".ToString().Trim();
            }
            objLM.STATUS = Status;

            string Remarks;
            if (txtRemarks.Text != string.Empty)
            {
                Remarks = txtRemarks.Text.ToString();
            }
            else
            {
                Remarks = string.Empty;
            }
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (ViewState["ModifyLeave"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objApp.UpdateIrregularityAppPassEntry(IRRTRNO, UA_NO, Status, Remarks, Aprdate, 0);
                        string statusnew = objCommon.LookUp("PAYROLL_IRREGULARITY_APP_ENTRY", "status", "IRRTRNO=" + IRRTRNO);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                MessageBox("Record Saved Successfully");
                                divAuthorityList.Visible = false;
                                pnlButton.Visible = false;
                                pnllist.Visible = true;
                                pnlAdd.Visible = false;
                                ViewState["action"] = null;
                                clear_lblvalue();
                              
                                BindLVLeaveApplPendingList();
                                clear();
                            }
                        }                    
                    else
                    {
                        CustomStatus cs = (CustomStatus)objApp.UpdateIrregularityAppPassEntry(IRRTRNO, UA_NO, Status, Remarks, Aprdate, 0);
                        string statusnew = objCommon.LookUp("PAYROLL_IRREGULARITY_APP_ENTRY", "status", "IRRTRNO=" + IRRTRNO);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            pnllist.Visible = true;
                            pnlAdd.Visible = false;
                            divAuthorityList.Visible = false;
                            pnlButton.Visible = false;
                            ViewState["action"] = null;
                            clear_lblvalue();
                            
                            clear();
                        }
                    }
                }
                lnkbut.Visible = true;
                BindLVLeaveApplPendingList();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        ViewState["EMPNO"] = ViewState["COLLEGE_NO"] = ViewState["STNO"] = null;

    }

    private void clear_lblvalue()
    {
        lblEmpName.Text = string.Empty;
        lblDept.Text = string.Empty;
        lblDesignation.Text = string.Empty;
        lblDate.Text = string.Empty;
        lblInTime.Text = string.Empty;
        lblOutTime.Text = string.Empty;
        lblShiftIn.Text = string.Empty;
        lblShiftOut.Text = string.Empty;
        lblWorking.Text = string.Empty;
        lblIrregularstatus.Text = string.Empty;
        lblReason.Text = string.Empty;
    }
    protected void lnkbut_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        trfrmto.Visible = true;
        trbutshow.Visible = true;
        this.BindLVLeaveApprStatusAll();
        pnlButton.Visible = false;
        lnkbut.Visible = false;
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DataSet ds = objApp.GetALLIrregularityApproval(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
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
        pnlButton.Visible = false;

        lnkbut.Visible = true;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVLeaveApprStatusParticular();
    }
    protected void txtTodt_TextChanged(object sender, EventArgs e)
    {
        DateTime DtFrom, DtTo, Test;
        if (DateTime.TryParseExact(txtTodt.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out Test) == true)
        {
            if (txtTodt.Text != string.Empty && txtTodt.Text != "__/__/____" && txtFromdt.Text != string.Empty && txtFromdt.Text != "__/__/____")
            {
                DtFrom = Convert.ToDateTime(txtFromdt.Text.ToString());

                DtTo = Convert.ToDateTime(txtTodt.Text.ToString());

                if (DtFrom > DtTo)
                {
                    MessageBox("To Date Should Be Larger Than Or Equals To From Date");
                    //txtTodt.Text = string.Empty;
                    txtTodt.Text = string.Empty;
                    return;
                }
            }
        }
        else
        {
            txtTodt.Text = string.Empty;
        }
    }

    protected void BindLVLeaveApprStatusParticular()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);


            DataSet ds = objApp.GetPendListforIrregularittyStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprStatus.DataSource = ds;
            lvApprStatus.DataBind();
            btnHidePanel.Visible = true;
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        pnllist.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
        btnSave.Visible = true;
        btnSave.Enabled = true;
        pnlButton.Visible = false;
        divAuthorityList.Visible = false;
        lnkbut.Visible = true;
    }
}
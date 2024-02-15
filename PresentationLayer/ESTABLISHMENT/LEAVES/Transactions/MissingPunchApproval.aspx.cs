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
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_MissingPunchApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objApp = new LeavesController();

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

                pnlAdd.Visible = false;
                // pnlvedit.Visible = false;
                // pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"]);
                BindMissPunchReqPendingList();
                // btnHidePanel.Visible = false;
                // trfrmto.Visible = false;
                //  trbutshow.Visible = false;
                //  txtFromdt.Text = System.DateTime.Now.ToString();
                //  txtTodt.Text = System.DateTime.Now.ToString();
                ViewState["ModifyLeave"] = "add";
                CheckPageAuthorization();

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
                Response.Redirect("~/notauthorized.aspx?page=CL_45_Appointment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CL_45_Appointment.aspx");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        //pnlvedit.Visible = false;
        pnllist.Visible = true;
        ViewState["action"] = null;
        clear_lblvalue();
        clear();
    }

    private void clear_lblvalue()
    {
        lblEmpName.Text = string.Empty;
        lblWDate.Text = string.Empty;
        lblInTime.Text = string.Empty;
        lblOutTime.Text = string.Empty;
        lblReason.Text = string.Empty;
    }

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
        ViewState["RFIDNO"] = null;
    }
    protected void BindMissPunchReqPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforMissPunchApproval(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                DivNote.Visible = true;
                lvPendingList.DataSource = null;
                lvPendingList.DataBind();
            }
            else
            {
                DivNote.Visible = false;
                lvPendingList.DataSource = ds;
                lvPendingList.DataBind();
            }

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_MissingPunchApproval.BindMissPunchReqPendingList() ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int RFIDNO = Convert.ToInt32(ViewState["RFIDNO"].ToString());
        string dt = Convert.ToDateTime(lblWDate.Text).ToString("yyyy-MM-dd").Trim();
        DateTime WorkDate = Convert.ToDateTime(lblWDate.Text);
        DateTime InTime = Convert.ToDateTime(dt + (lblInTime.Text.Trim() == "" ? " 00:00:00" : " " + lblInTime.Text.Trim()));
        DateTime OutTime = Convert.ToDateTime(dt + (lblOutTime.Text.Trim() == "" ? " 00:00:00" : " " + lblOutTime.Text.Trim()));
        string Status = ddlSelect.SelectedValue.ToString();
        string Remarks = txtRemarks.Text.ToString();
        int Userid = Convert.ToInt32(Session["userno"].ToString()); 
        DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString().Equals("edit"))
            {
                CustomStatus cs = (CustomStatus)objApp.UpdateMissPunchApp(RFIDNO, WorkDate, InTime, OutTime, Status, Remarks, Aprdate, Userid);

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    MessageBox("Record save successfully!");
                    BindMissPunchReqPendingList();
                    pnllist.Visible = true;
                    pnlAdd.Visible = false;
                    ViewState["action"] = null;
                    clear_lblvalue();
                    clear();
                }
            }
        }
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int MPNO = int.Parse(btnApproval.CommandArgument);
            Session["MPNO"] = MPNO;

            ShowDetails(MPNO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            //pnlvedit.Visible = true;
            //chklwp.Checked = false;

            //chklwp.Visible = false;
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
    private void ShowDetails(Int32 MPNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetMissPunchDetail(MPNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MPNO"] = MPNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblWDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WRKDATE"].ToString()).ToString("dd/MM/yyyy");
                lblInTime.Text = ds.Tables[0].Rows[0]["IN_TIME"].ToString();
                lblOutTime.Text = ds.Tables[0].Rows[0]["OUT_TIME"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
                int RFIDNO = Convert.ToInt32(ds.Tables[0].Rows[0]["RFIDNO"]);
                ViewState["RFIDNO"] = RFIDNO;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Comp_off_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
}
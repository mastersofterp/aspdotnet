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

public partial class ESTABLISHMENT_LEAVES_Transactions_Comp_Off_Approval : System.Web.UI.Page
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
                BindLVLeaveApplPendingList();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int RNO = Convert.ToInt32(ViewState["RNO"].ToString());
            int UA_NO=Convert.ToInt32(Session["userno"]);
            string Status = ddlSelect.SelectedValue.ToString();
            string Remarks= txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);


            int Year = Convert.ToInt32(DateTime.Now.Year.ToString());

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {
                    CustomStatus cs = (CustomStatus)objApp.UpdateComp_offRequestEntry(RNO, UA_NO, Status, Remarks, Aprdate, 0);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        MessageBox("Record save successfully!");
                        BindLVLeaveApplPendingList();
                        pnllist.Visible = true;
                        pnlAdd.Visible = false;
                        ViewState["action"] = null;
                        clear_lblvalue();
                        clear();
                    }
                }
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
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int RNO = int.Parse(btnApproval.CommandArgument);
            Session["RNO"] = RNO;

            ShowDetails(RNO);
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

    private void ShowDetails(Int32 RNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetCompOffLeaveDetail(RNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["RNO"] = RNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblLeaveName.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                lblWDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["WORKING_DATE"].ToString()).ToString("dd/MM/yyyy");
                lblInTime.Text = ds.Tables[0].Rows[0]["IN_TIME"].ToString();
                lblOutTime.Text = ds.Tables[0].Rows[0]["OUT_TIME"].ToString();
                lblHour.Text = ds.Tables[0].Rows[0]["WORKING_HOUR"].ToString();
                lblReason.Text = ds.Tables[0].Rows[0]["REASON"].ToString();
               // int lno = Convert.ToInt32(ds.Tables[0].Rows[0]["LNO"]);
                int idno = Convert.ToInt32(ds.Tables[0].Rows[0]["IDNO"]);

                //Used To show details in Leave Modify Panel
 
                //txtLeavename.Text = ds.Tables[0].Rows[0]["LName"].ToString();
                //txtfrmdt.Text = ds.Tables[0].Rows[0]["From_date"].ToString();
                //txttodate.Text = ds.Tables[0].Rows[0]["TO_DATE"].ToString();
                //txtNodays.Text = ds.Tables[0].Rows[0]["NO_OF_DAYS"].ToString();
                //txtJoindt.Text = ds.Tables[0].Rows[0]["JOINDT"].ToString();


                //int YR = DateTime.Now.Year;
                //DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]
                //if (ds1.Tables[0].Rows.Count > 0)
                //{

                //    double total = Convert.ToDouble(ds1.Tables[0].Rows[0]["TOTAL"]);
                //    // DataSet dsbal = objCommon.FillDropDown("PAYROLL_LEAVE_APP_ENTRY", "SUM(NO_OF_DAYS) as LEAVES", "", "STATUS IN('A','T') and EMPNO=" + idno + "AND LNO=" + lno, "");
                //    double leaves = Convert.ToDouble(objCommon.LookUp("PAYROLL_LEAVE_APP_ENTRY", "ISNULL(SUM(NO_OF_DAYS),0) as LEAVES", "STATUS IN('A','T') and EMPNO=" + idno + "AND LNO=" + lno));
                //    // double leaves = Convert.ToDouble(dsbal.Tables [0].Rows [0]["LEAVES"]);
                //    lbltot.Text = total.ToString();
                //    lblbal.Text = (total - leaves).ToString();
                //    txtLeavebal.Text = (total - leaves).ToString();


                //}


            }

            //lvStatus.DataSource = ds.Tables[1];
            //lvStatus.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Comp_off_Approval.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }
  
    protected void BindLVLeaveApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforCompOffApproval(Convert.ToInt32(Session["userno"]));
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
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.BindLVLeaveApplPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

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
        lblLeaveName.Text = string.Empty;
        lblWDate.Text = string.Empty;
        lblInTime.Text = string.Empty;
        lblOutTime.Text = string.Empty;
        lblHour.Text = string.Empty;
        lblReason.Text = string.Empty;
    }

    private void clear()
    {
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedIndex = 0;
    }
}

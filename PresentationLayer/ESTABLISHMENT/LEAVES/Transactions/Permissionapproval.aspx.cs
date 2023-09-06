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

public partial class ESTABLISHMENT_LEAVES_Transactions_Permissionapproval : System.Web.UI.Page
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
                // CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                //pnlvedit.Visible = false;
                pnllist.Visible = true;
                int usernock = Convert.ToInt32(Session["userno"]);
                BindLVPerApplPendingList();
                btnHidePanel.Visible = false;
                trfrmto.Visible = false;
                trbutshow.Visible = false;
                txtFromdt.Text = System.DateTime.Now.ToString();
                txtTodt.Text = System.DateTime.Now.ToString();
                ViewState["ModifyLeave"] = "add";


            }
        }
    }

    protected void BindLVPerApplPendingList()
    {
        try
        {
            DataSet ds = objApp.GetPendListforPermissionApproval(Convert.ToInt32(Session["userno"]));
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
    //protected void dpPager_PreRender(object sender, EventArgs e)
    //{

    //    BindLVPerApplPendingList();
    //}

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // clear();
        // Newly Added

        divAuthorityList.Visible = true;
        pnlButton.Visible = true;
        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedValue = "0";
        // Newly Added.

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        pnlAdd.Visible = false;
        //pnlvedit.Visible = false;
        pnllist.Visible = true;
        lnkbut.Visible = true;
        pnlSelectList.Visible = false;
        ViewState["action"] = null;
        pnlButton.Visible = false;

        txtRemarks.Text = string.Empty;
        ddlSelect.SelectedValue = "0";
        //  clear_lblvalue();
        // clear();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Leaves objLM = new Leaves();
            int PERTNO = Convert.ToInt32(ViewState["PERTNO"].ToString());
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
            string Remarks = txtRemarks.Text.ToString();
            DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
            // bool isSMS = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isSMS,0)as isSMS", ""));
            //  bool isEmail = Convert.ToBoolean(objCommon.LookUp("payroll_leave_ref", "isnull(isEmail,0)as isEmail", ""));

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("edit"))
                {                   
                    CustomStatus cs = (CustomStatus)objApp.UpdateAppperPassEntry(PERTNO, UA_NO, Status, Remarks, Aprdate, 0);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        pnllist.Visible = true;
                        pnlAdd.Visible = false;
                        ViewState["action"] = null;
                        // clear_lblvalue();
                        MessageBox("Record Updated Successfully");
                        clear();
                    }
                   


                }
            }
            BindLVPerApplPendingList();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Transactions_Leave_Approval.btnSave_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    protected void btnApproval_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnApproval = sender as Button;
            int PERTNO = int.Parse(btnApproval.CommandArgument);
            Session["PERTNO"] = PERTNO;
            lnkbut.Visible = false;

            ShowDetails(PERTNO);
            pnllist.Visible = false;
            pnlAdd.Visible = true;
            pnlButton.Visible = true;
            pnlSelectList.Visible = true;

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


    private void ShowDetails(Int32 PERTNO)
    {
        DataSet ds = new DataSet();

        try
        {
            ds = objApp.GetPerApplDetail(PERTNO);

            //int YR = DateTime.Now.Year;
            //DataSet ds1 = objApp.GetLeavesStatus(Convert.ToInt32(Session["idno"].ToString()), YR, 0);//Session["idno"]
            //ds.Tables[1] = objApp.GetLeaveApplStatus(LETRNO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["PERTNO"] = PERTNO;
                lblEmpName.Text = ds.Tables[0].Rows[0]["EmpName"].ToString();
                lblFromdt.Text = ds.Tables[0].Rows[0]["Date"].ToString();

                lblReason.Text = ds.Tables[0].Rows[0]["REMARK"].ToString();
                lblcat.Text = ds.Tables[0].Rows[0]["cate"].ToString();

                int idno = Convert.ToInt32(ds.Tables[0].Rows[0]["EMPNO"]);



                //Used To show details in Leave Modify Panel 

                ViewState["EMPNO"] = ds.Tables[0].Rows[0]["EMPNO"].ToString();

                // int YR = DateTime.Now.Year;
                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, leaveno);//Session["idno"]


                // DataSet ds1 = objApp.GetLeavesStatus(idno, YR, lno);//Session["idno"]   
            }
            lvStatus.DataSource = ds.Tables[1];
            lvStatus.DataBind();
            divAuthorityList.Visible = true;
            if (ds.Tables[2].Rows.Count > 0)
            {
                //FORWARD_APPROVAL_STATUS
                string FORWARD_APPROVAL_STATUS = ds.Tables[2].Rows[0]["FORWARD_APPROVAL_STATUS"].ToString();

                //string status = Convert.ToString(objCommon.LookUp("PAYROLL_LEAVE_APP_PASS_ENTRY", "status", "LETRNO=" + LETRNO + " and status='P'"));
                //if (status == "P")
                //{
                //    //ddlSelect.Items.Remove("Forward To Next Authority(Recommended)");
                //    // ddlSelect.SelectedValue.Remove="F";
                //    ListItem removeItem = ddlSelect.Items.FindByValue("F");
                //    ddlSelect.Items.Remove(removeItem);
                //}
                if (FORWARD_APPROVAL_STATUS != "F".ToString().Trim())
                {
                    ListItem removeItem = ddlSelect.Items.FindByValue("F");
                    //ddlSelect.Items.Remove(removeItem);
                    removeItem = ddlSelect.Items.FindByValue("Approve & Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Remove(removeItem);

                   

                }
                else
                {
                    ddlSelect.Items.Clear();
                    ddlSelect.Items.Add("Approve & Forward To Next Authority(Recommended)");
                    ddlSelect.Items.Add("Approve & Final Submit");
                    ddlSelect.Items.Add("Reject");



                    //ddlSelectModify.Items.Clear();
                    //ddlSelectModify.Items.Add("Forward To Next Authority(Recommended)");
                    //ddlSelectModify.Items.Add("Approve & Final Submit");
                    //ddlSelectModify.Items.Add("Reject");

                }

            }

            //// Added By SHrikant Bharne.
            int UA_No = 0;
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                {
                    UA_No = Convert.ToInt32(ds.Tables[1].Rows[i]["UA_No"].ToString());
                }
            }
            //int stno = Convert.ToInt32(ViewState["STNO"]);


            if (Convert.ToInt32(Session["userno"]) == UA_No)     //// if (Convert.ToInt32(Session["userno"]) == UA_No)
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("F");
                ddlSelect.Items.Remove(removeItem);
                //removeItem = ddlSelect.Items.FindByValue("Forward To Next Authority(Recommended)");
                removeItem = ddlSelect.Items.FindByValue("Approve & Forward To Next Authority(Recommended)");
                ddlSelect.Items.Remove(removeItem);
            }
            else
            {
                ListItem removeItem = ddlSelect.Items.FindByValue("A");
                ddlSelect.Items.Remove(removeItem);
                removeItem = ddlSelect.Items.FindByValue("Approve & Final Submit");
                ddlSelect.Items.Remove(removeItem);
            }

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

    protected void lnkbut_Click(object sender, EventArgs e)
    {
        lnkbut.Visible = false;
        pnlODStatus.Visible = true;
        pnlAdd.Visible = false;
        pnllist.Visible = false;
        trfrmto.Visible = true;
        trbutshow.Visible = true;
        btnHidePanel.Visible = true;
        this.BindLVLeaveApprStatusAll();
    }

    protected void BindLVLeaveApprStatusAll()
    {
        try
        {
            DataSet ds = objApp.GetPendListforperApprovalStatusALL(Convert.ToInt32(Session["userno"]));
            if (ds.Tables[0].Rows.Count <= 0)
            {
               // dpPager.Visible = false;
            }
            else
            {
               // dpPager.Visible = true;
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindLVPerApprStatusParticular();
    }

    protected void BindLVPerApprStatusParticular()
    {
        try
        {
            //DateTime DT = Convert.ToDateTime (txtFromdt.Text);
            string Fdate = (String.Format("{0:u}", Convert.ToDateTime(txtFromdt.Text)));
            Fdate = Fdate.Substring(0, 10);
            string Tdate = (String.Format("{0:u}", Convert.ToDateTime(txtTodt.Text)));
            Tdate = Tdate.Substring(0, 10);


            DataSet ds = objApp.GetPendListforPERMISSApprovalStatusParticular(Convert.ToInt32(Session["userno"]), Fdate, Tdate);
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

    private void clear()
    {
        txtRemarks.Text = string.Empty;

        ddlSelect.SelectedIndex = 0;
        ViewState["EMPNO"] = ViewState["COLLEGE_NO"] = ViewState["STNO"] = null;

        //ViewState["action"] = null;
        //ViewState["ModifyLeave"] = null;
        divAuthorityList.Visible = false;
        lnkbut.Visible = true;



        ViewState["EMPNO"] = null;
        pnlButton.Visible = false;
    }

    protected void btnHidePanel_Click(object sender, EventArgs e)
    {
        pnlODStatus.Visible = false;

        lnkbut.Visible = true;

        pnlAdd.Visible = false;
        pnllist.Visible = true;
        trfrmto.Visible = false;
        trbutshow.Visible = false;
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class RECRUITMENT_Transactions_RequisitionApproval : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    RequisitionController objReq = new RequisitionController();
    Requisition objRequisition = new Requisition();

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
                //CheckPageAuthorization();

                pnlAdd.Visible = true;
                pnlbtn.Visible = true;
                FillDepartment();
                //FillPost();
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
                Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RequisitionUser.aspx");
        }
    }
    private void FillDepartment()
    {
        try
        {
            objCommon.FillDropDownList(ddlDepartment, "PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT DEPT ON(DEPT.SUBDEPTNO=E.SUBDEPTNO)", "DISTINCT E.SUBDEPTNO", "DEPT.SUBDEPT", "E.SUBDEPTNO <> 0 AND E.COLLEGE_CODE =" + Convert.ToInt32(Session["colcode"]) + " ", "DEPT.SUBDEPT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillDepartment ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void FillPost()
    {
        try
        {
            int deptNo = Convert.ToInt32(ddlDepartment.SelectedValue);
            DataSet ds = objReq.GetPosts(deptNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPost.DataSource = ds;
                ddlPost.DataTextField = "sPostCode";
                ddlPost.DataValueField = "nId";
                ddlPost.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Master_RequisitionUser.FillPost ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlDepartment.SelectedIndex = 0;
        ddlPost.SelectedIndex = 0;
        ddlpostType.SelectedIndex = 0;
        ddlFilter.SelectedIndex = 0;
        pnlPndRequiAppList.Visible = false;
        pnlApprlist.Visible = false;
    }
    protected void BindRequiReqPendingList()
    {
        try
        {
            objRequisition.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlpostType.SelectedIndex > 0)
            {
                objRequisition.POSTCATNO = Convert.ToInt32(ddlpostType.SelectedValue);
            }
            else
            {
                objRequisition.POSTCATNO = 0;
            }
            if (ddlPost.SelectedIndex > 0)
            {
                objRequisition.POST_NO = Convert.ToInt32(ddlPost.SelectedValue);
            }
            else
            {
                objRequisition.POST_NO = 0;
            }
            DataSet ds = objReq.GetPendListforRequisitionApproval(Convert.ToInt32(Session["userno"]), objRequisition);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                if (ddlFilter.SelectedValue == "1")
                {
                    MessageBox("Pending Requests Data Not Found !");
                    pnlPndRequiAppList.Visible = false;
                    return;
                }
                else
                {
                    MessageBox("Approved Requests Data Not Found !");
                    pnlApprlist.Visible = false;
                    return;
                }
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvPendRequi.DataSource = ds;
            lvPendRequi.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionApproval.BindRequiReqPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void BindRequiReqApprList()
    {
        try
        {
            objRequisition.DEPTNO = Convert.ToInt32(ddlDepartment.SelectedValue);
            if (ddlpostType.SelectedIndex > 0)
            {
                objRequisition.POSTCATNO = Convert.ToInt32(ddlpostType.SelectedValue);
            }
            else
            {
                objRequisition.POSTCATNO = 0;
            }
            if (ddlPost.SelectedIndex > 0)
            {
                objRequisition.POST_NO = Convert.ToInt32(ddlPost.SelectedValue);
            }
            else
            {
                objRequisition.POST_NO = 0;
            }
            DataSet ds = objReq.GetApprListforRequisitionApproval(Convert.ToInt32(Session["userno"]), objRequisition);
            if (ds.Tables[0].Rows.Count <= 0)
            {
                if (ddlFilter.SelectedValue == "1")
                {
                    MessageBox("Pending Requests Data Not Found !");
                    pnlPndRequiAppList.Visible = false;
                    return;
                }
                else
                {
                    MessageBox("Approved Requests Data Not Found !");
                    pnlApprlist.Visible = false;
                    return;
                }
                //dpPager.Visible = false;
            }
            else
            {
                //dpPager.Visible = true;
            }
            lvApprList.DataSource = ds;
            lvApprList.DataBind();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionApproval.BindRequiReqPendingList ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }

    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (ddlFilter.SelectedValue == "1")
        {
            pnlPndRequiAppList.Visible = true;
            pnlApprlist.Visible = false;
            BindRequiReqPendingList();
        }
        else
        {
            pnlApprlist.Visible = true;
            pnlPndRequiAppList.Visible = false;
            BindRequiReqApprList();
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            int checkcount = 0;
            int instCount = 0;
            string selectedIDs = string.Empty;
            foreach (RepeaterItem lvItem in lvPendRequi.Items)
            {
                CheckBox chk = lvItem.FindControl("chkID") as CheckBox;
                TextBox txtRemk = lvItem.FindControl("txtRemarks") as TextBox;
                string chkid = "'" + chk.ToolTip.ToString().Trim() + "'";
                if (chk.Checked == true)
                {
                    checkcount += 1;
                    selectedIDs = selectedIDs + chk.ToolTip.ToString().Trim() + "$";
                    objRequisition.REQ_ID = Convert.ToInt32(chk.ToolTip);
                    objRequisition.USERNO = Convert.ToInt32(Session["userno"]);
                    string resno = objCommon.LookUp("REC_REQUISITION_REQUEST_PASS", "1", "REQ_ID = " + Convert.ToInt32(chk.ToolTip) + " AND STATUS IS NULL");

                    if (resno == "1")
                    {
                        objRequisition.APPROVAL_STATUS = "F".ToString().Trim();
                    }
                    else
                    {
                        objRequisition.APPROVAL_STATUS = "A".ToString().Trim();
                    }
                    DateTime Aprdate = Convert.ToDateTime(DateTime.Now.Date);
                    objRequisition.APPROVED_DATE = Aprdate;
                    objRequisition.DESCRIPTION = txtRemk.Text.Trim();
                    objRequisition.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

                    CustomStatus cs = (CustomStatus)objReq.UpdateRequisitionApprPass(objRequisition);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        instCount = 1;
                    }
                }
                chk.Checked = false;
            }
            if (checkcount == 0)
            {
                MessageBox("Please Select Atleast One Request");
                return;
            }
            if (instCount == 1)
            {
                MessageBox("Record Saved Successfully");
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "RECRUITMENT_Transactions_RequisitionApproval.btnApprove_Click ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillPost();
    }
}
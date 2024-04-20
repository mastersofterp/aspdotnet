using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_SERVICEBOOK_Pay_Sb_Research : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();
    public int _idnoEmp;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string empno = ViewState["idno"].ToString();

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
                //CheckPageAuthorization();
            }

            ViewState["action"] = "add";


        }

        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
            string DEPTNAME = objCommon.LookUp("PAYROLL_EMPMAS E INNER JOIN PAYROLL_SUBDEPT S ON(E.SUBDEPTNO=S.SUBDEPTNO)", "S.SUBDEPT", "E.IDNO=" + Convert.ToInt32(_idnoEmp));
            txtDepartment.Text = DEPTNAME.ToString();
        }
        BindListViewAllResearch();
        GetConfigForEditAndApprove();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceBook objSevBook = new ServiceBook();
            objSevBook.IDNO = _idnoEmp;
            objSevBook.PROJECT_TITLE = txtTitle.Text;
            objSevBook.DEPARTMENT = txtDepartment.Text;
            objSevBook.NATURE_OF_PROJECT_ID = Convert.ToInt32(ddlProject.SelectedValue);
            objSevBook.NAME_OF_PRINCIPAL = txtPrincipal.Text;
            objSevBook.SPONSERED_BY_ID = Convert.ToInt32(ddlSponsered.SelectedValue);
            objSevBook.FUNDING_AJENCY_NAME = txtAjency.Text;
            objSevBook.TOTAL_PROJECT_FUND = Convert.ToDecimal(txtTotalProjectfund.Text);
            objSevBook.PERIOD_FROM_DATE = Convert.ToDateTime(txtFromdate.Text);


            if (Convert.ToDateTime(txtPeriodToDate.Text) < Convert.ToDateTime(txtFromdate.Text))
            {
                MessageBox("Period to date should be greater than or equal  period from date");
                txtPeriodToDate.Text = "";
                return;
            }
            else
            {

                objSevBook.PERIOD_TO_DATE = Convert.ToDateTime(txtPeriodToDate.Text);
            }


            var Yr = txtYear.Text;
            objSevBook.YEAR = Convert.ToInt32(Yr);
            objSevBook.PROJECT_STATUS_ID = Convert.ToInt32(ddlStatus.SelectedValue);
            objSevBook.TOTAL_FUND_UTILISED = Convert.ToDecimal(txtUtilized.Text);
            objSevBook.OWNERSHIP_ID = Convert.ToInt32(ddlOwnerType.SelectedValue);
            objSevBook.JOINT_WITH_ID = Convert.ToInt32(ddlJoinWith.SelectedValue);
            objSevBook.JOINT_WITH = ddlJoinWith.SelectedItem.Text;
            objSevBook.RESULT_OF_INNOVATION = txtoutput.Text;
            objSevBook.IMPACT_FACTOR = txtImpact.Text;
            objSevBook.JOINT_BELONG_TO_ID = Convert.ToInt32(ddlBelong.SelectedValue);
            objSevBook.AMOUNT = Convert.ToDecimal(txtAmount.Text);
            objSevBook.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objServiceBook.AddResearch(objSevBook);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        this.Clear();
                        this.BindListViewAllResearch();
                        MessageBox("Record Saved Successfully");
                    }

                }
                else
                {
                    //Edit
                    if (ViewState["RESEARNO"] != null)
                    {
                        objSevBook.RESEARNO = Convert.ToInt32(ViewState["RESEARNO"].ToString());
                        CustomStatus cs = (CustomStatus)objServiceBook.UpdateResearch(objSevBook);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = "add";
                            this.Clear();
                            this.BindListViewAllResearch();
                            MessageBox("Record Updated Successfully");
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Sb_Innovation.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Clear();
        GetConfigForEditAndApprove();
    }
    private void Clear()
    {
        txtTitle.Text = string.Empty;
        ddlProject.SelectedIndex = 0;
        txtPrincipal.Text = string.Empty;
        txtAjency.Text = string.Empty;
        ddlJoinWith.SelectedIndex = 0;
        txtTotalProjectfund.Text = string.Empty;
        txtYear.Text = string.Empty;
        txtFromdate.Text = string.Empty;
        txtAmount.Text = string.Empty;
        ddlSponsered.SelectedIndex = 0;
        txtPeriodToDate.Text = string.Empty;
        txtUtilized.Text = string.Empty;
        txtAmount.Text = string.Empty;
        ddlStatus.SelectedIndex = 0;
        ddlOwnerType.SelectedIndex = 0;
        txtoutput.Text = string.Empty;
        txtImpact.Text = string.Empty;
        ddlBelong.SelectedIndex = 0;
        ViewState["action"] = "add";
        ViewState["IsEditable"] = null;
        ViewState["IsApprovalRequire"] = null;
        btnSubmit.Enabled = true;
    }

    private void BindListViewAllResearch()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllResearch(_idnoEmp);
            lvResearch.DataSource = ds;
            lvResearch.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Sb_Innovation.BindListViewAllInnovation-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CONFID = int.Parse(btnEdit.CommandArgument);
            ShowDetails(CONFID);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Sb_Innovation.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int RESEARNO)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleResearchDetails(RESEARNO);
            //To show created user details 
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["RESEARNO"] = RESEARNO.ToString();
                txtTitle.Text = ds.Tables[0].Rows[0]["PROJECT_TITLE"].ToString();
                txtPrincipal.Text = ds.Tables[0].Rows[0]["NAME_OF_PRINCIPAL"].ToString();
                ddlProject.SelectedValue = ds.Tables[0].Rows[0]["NATURE_OF_PROJECT_ID"].ToString();
                ddlSponsered.SelectedValue = ds.Tables[0].Rows[0]["SPONSERED_BY_ID"].ToString();
                txtYear.Text = ds.Tables[0].Rows[0]["YEAR"].ToString();
                txtAjency.Text = ds.Tables[0].Rows[0]["FUNDING_AJENCY_NAME"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                txtTotalProjectfund.Text = ds.Tables[0].Rows[0]["TOTAL_PROJECT_FUND"].ToString();
                ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["PROJECT_STATUS_ID"].ToString();
                txtUtilized.Text = ds.Tables[0].Rows[0]["TOTAL_FUND_UTILISED"].ToString();
                txtFromdate.Text = ds.Tables[0].Rows[0]["PERIOD_FROM_DATE"].ToString();
                txtPeriodToDate.Text = ds.Tables[0].Rows[0]["PERIOD_TO_DATE"].ToString();
                ddlOwnerType.SelectedValue = ds.Tables[0].Rows[0]["OWNERSHIP_ID"].ToString();
                ddlJoinWith.SelectedValue = ds.Tables[0].Rows[0]["JOINT_WITH_ID"].ToString();
                txtoutput.Text = ds.Tables[0].Rows[0]["RESULT"].ToString();
                txtImpact.Text = ds.Tables[0].Rows[0]["IMPACT_FACTOR"].ToString();
                ddlBelong.SelectedValue = ds.Tables[0].Rows[0]["JOINT_BELONG_TO_ID"].ToString();

                if (Convert.ToBoolean(ViewState["IsApprovalRequire"]) == true)
                {
                    string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
                    if (STATUS == "A")
                    {
                        MessageBox("Your Details Are Approved You Cannot Edit.");
                        btnSubmit.Enabled = false;
                        return;
                    }
                    else
                    {
                        btnSubmit.Enabled = true;
                    }
                    GetConfigForEditAndApprove();
                }
                else
                {
                    btnSubmit.Enabled = true;
                    GetConfigForEditAndApprove();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Admin_Responsibilities.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        //finally
        //{
        //    ds.Clear();
        //    ds.Dispose();
        //}

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnDel = sender as ImageButton;
            int RESEARNO = int.Parse(btnDel.CommandArgument);
            int IDNO = _idnoEmp;
            DataSet ds = new DataSet();
            ds = objCommon.FillDropDown("PAY_SB_RESEARCH", "LTRIM(RTRIM(ISNULL(APPROVE_STATUS,''))) AS APPROVE_STATUS", "", "RESEARNO=" + RESEARNO, "");
            string STATUS = ds.Tables[0].Rows[0]["APPROVE_STATUS"].ToString();
            if (STATUS == "A")
            {
                MessageBox("Your Details are Approved You Cannot Delete.");
                return;
            }
            else if (STATUS == "R")
            {
                MessageBox("Your Details are Rejected You Cannot Delete.");
                return;
            }
            else
            {
                CustomStatus cs = (CustomStatus)objServiceBook.DeleteResearch(RESEARNO);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    ViewState["action"] = "add";
                    MessageBox("Record Deleted Successfully");
                    Clear();
                    BindListViewAllResearch();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_Sb_Innovation.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);

    }

    #region ServiceBook Config

    private void GetConfigForEditAndApprove()
    {
        DataSet ds = null;
        try
        {
            Boolean IsEditable = false;
            Boolean IsApprovalRequire = false;
            string Command = "Research";
            ds = objServiceBook.GetServiceBookConfigurationForRestrict(Convert.ToInt32(Session["usertype"]), Command);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEditable = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsEditable"]);
                IsApprovalRequire = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsApprovalRequire"]);
                ViewState["IsEditable"] = IsEditable;
                ViewState["IsApprovalRequire"] = IsApprovalRequire;

                if (Convert.ToBoolean(ViewState["IsEditable"]) == true)
                {
                    btnSubmit.Enabled = false;
                }
                else
                {
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ViewState["IsEditable"] = false;
                ViewState["IsApprovalRequire"] = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.GetConfigForEditAndApprove-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }
    }

    #endregion
}
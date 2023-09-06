//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PF_Intrest_Calculation.ASPX                                                    
// CREATION DATE : 16-Feb-2010                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_TRANSACTIONS_PF_PF_Intrest_Calculation : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,GPF_CONTROLLER,GPF
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PFCONTROLLER objPfcontroller = new PFCONTROLLER();

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
                this.FillDropDown();
                ViewState["getpfno"] = "0";
                tblEmployee.Visible = false;
                this.GetFinYearSdateEdate();

            }
        }

    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=PF_Intrest_Calculation.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_Intrest_Calculation.aspx");
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int staffNo;
            int idNo;
            string carry;

            if (trEmployee.Visible)
                idNo = Convert.ToInt32(ddlemployee.SelectedValue);
            else
                idNo = 0;

            if (trStaff.Visible)
                staffNo = Convert.ToInt32(ddlStaff.SelectedValue);
            else
                staffNo = 0;

            if (chkCarrayOB.Checked)
                carry = "Y";
            else
                carry = "N";

            DataTable DatessTbl = new DataTable("DatesTbl");


            DatessTbl.Columns.Add("FROMDATE", typeof(DateTime));
            DatessTbl.Columns.Add("TODATE", typeof(DateTime));
            DatessTbl.Columns.Add("PERCENTAGE", typeof(decimal));


            DataRow dr = null;

            //For insert

            foreach (ListViewItem i in lvIntrest.Items)
            {
                HiddenField hdnToDate = (HiddenField)i.FindControl("hdnToDate");
                HiddenField hdnFDate = (HiddenField)i.FindControl("hdnFDate");
                TextBox txtPer = (TextBox)i.FindControl("txtPer");

                if (txtPer.Text == string.Empty || txtPer.Text == "") { txtPer.Text = "0"; }

                dr = DatessTbl.NewRow();

                dr["FROMDATE"] = Convert.ToDateTime(hdnFDate.Value);
                dr["TODATE"] = Convert.ToDateTime(hdnToDate.Value);
                dr["PERCENTAGE"] = Convert.ToDecimal(txtPer.Text);

                DatessTbl.Rows.Add(dr);
            }


            CustomStatus cs = (CustomStatus)objPfcontroller.PFIntrestCalculation(idNo, staffNo, Convert.ToInt32(txtYearlyPercentage.Text), txtFromDate.Text, txtToDate.Text, carry, DatessTbl);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayMessage(UpdatePanelMain, "Intrest calculation successfully completed", this);
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PF_Intrest_Calculation.butSubmit_Click()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");
            //objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') AND isnull(EM.PFNO,0)=1 ", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')");
            // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND  EM.IDNO > 0 and PM.PSTATUS = 'Y' AND (EM.STATUS IS NULL OR EM.STATUS <>'') ", "EM.IDNO");
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            throw new IITMSException("IITMS.UAIMS.PF_Intrest_Calculation.FillDropDown-> " + ex.ToString());
        }

    }

    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlemployee.SelectedIndex > 0)
        {
            ViewState["getpfno"] = objCommon.LookUp("payroll_empmas", "isnull(pfno,0)", "(status is null or status='') and idno=" + Convert.ToInt32(ddlemployee.SelectedValue));
            string shortname = objCommon.LookUp("payroll_pf_mast", "shortname", "pfno=" + Convert.ToInt32(ViewState["getpfno"].ToString()));
            lbleligibleFor.Text = shortname;
        }
    }

    private void GetFinYearSdateEdate()
    {
        DataSet DS;
        DS = objPfcontroller.GETPFFINANCEYAER();
        string Fsdate = string.Empty;
        string Fedate = string.Empty;
        Fsdate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFSDATE"]).ToString("dd/MM/yyyy");
        Fedate = Convert.ToDateTime(DS.Tables[0].Rows[0]["PFFEDATE"]).ToString("dd/MM/yyyy");
        txtFromDate.Text = Fsdate;
        txtToDate.Text = Fedate;

        if (Fsdate != null)
        {

            DataSet ds = objPfcontroller.GetIntrestgrid(Convert.ToDateTime(txtFromDate.Text));
            if (ds.Tables[0].Rows.Count <= 0)
            {
                tblIntrest.Visible = false;
            }
            else
            {
                tblIntrest.Visible = true;
                lvIntrest.DataSource = ds;
                lvIntrest.DataBind();
                ds.Dispose();
            }
        }
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = Convert.ToString(Convert.ToDateTime(txtFromDate.Text).AddMonths(12).AddDays(-1));
    }

    protected void radStaff_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = false;
        trEmployee.Visible = false;
        trStaff.Visible = true;
        tblEmployee.Visible = true;
        tblIntrest.Visible = true;
    }

    protected void radEmployee_CheckedChanged(object sender, EventArgs e)
    {
        trEligibleFor.Visible = true;
        trEmployee.Visible = true;
        trStaff.Visible = false;
        tblEmployee.Visible = true;
        tblIntrest.Visible = true;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                FillEmployee(Convert.ToInt32(ddlCollege.SelectedValue));
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void FillEmployee(int CollegeId)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'')+'['+ CONVERT(NVARCHAR(20),EM.IDNO) +']' as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS = 'Y' and EM.IDNO > 0 AND EM.STATUS IS NULL OR EM.STATUS <>'' AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue,"EM.IDNO");
                objCommon.FillDropDownList(ddlemployee, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", "ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND isnull(EM.PFNO,0)=1 AND  isnull(EM.PFNO,0)=1 AND EM.COLLEGE_NO=" + ddlCollege.SelectedValue, "EM.IDNO");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Monthly_Installment_Entry.FillEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

}

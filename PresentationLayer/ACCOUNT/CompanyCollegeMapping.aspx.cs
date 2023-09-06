//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : COLLEGE ASSIGN
// CREATION DATE : 10-AUGUST-2017                                               
// CREATED BY    : NAKUL CHAWRE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Web.UI.HtmlControls;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.NITPRM;

public partial class ACCOUNT_CompanyCollegeMapping : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    string back = string.Empty;
    FeesTransferStudentwiseController objFTS = new FeesTransferStudentwiseController();
    AccountTransaction objAccountTrans = new AccountTransaction();
    private string _CCMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ToString();
    private string _UAIMS = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ToString();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        { back = Request.QueryString["obj"].ToString().Trim(); }

        if (!Page.IsPostBack)
        {
            ViewState["action"] = "update";
            CheckPageAuthorization();

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                PopulateDropDownList();
                BindCashBook();
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
                Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
            }
            Common objCommon = new Common();
            objCommon.RecordActivity(int.Parse(Session["loginid"].ToString()), int.Parse(Request.QueryString["pageno"].ToString()), 0);
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createCompany.aspx");
        }
    }

    private void PopulateDropDownList()
    {
        objCommon.FillDropDownList(ddlCompany, "ACC_COMPANY", "COMPANY_CODE", "CAST(COMPANY_NAME AS VARCHAR(MAX)) +' '+ CAST(YEAR(COMPANY_FINDATE_FROM) AS VARCHAR(12)) +' - '+ CAST(YEAR(COMPANY_FINDATE_TO) AS VARCHAR(12)) AS COMPANY_NAME", "DROP_FLAG='N'", "COMPANY_NAME");
    }

    private void BindCashBook()
    {
        DataSet ds = objFTS.BindCollege(_CCMS);
        if (ds.Tables[0].Rows.Count > 0)
        {
            chkCollege.DataSource = ds;
            chkCollege.DataValueField = ds.Tables[0].Columns[0].ToString();
            chkCollege.DataTextField = ds.Tables[0].Columns[1].ToString();
            chkCollege.ToolTip = ds.Tables[0].Columns[0].ToString();
            chkCollege.DataBind();
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        string uanos = string.Empty;
        pnlListMain.Visible = true;
        //pnlListMain.Visible = false;
        trSubmit.Visible = true;
        ddlCompany.Enabled = false;

        if (Session["dtCollege"] != null)
            dt = (DataTable)Session["dtCollege"];

        foreach (ListItem chkClg in chkCollege.Items)
        {
            if (chkClg.Selected == true)
            {
                if (uanos == string.Empty)
                    uanos += chkClg.Value;
                else
                    uanos += "," + chkClg.Value;
            }
        }

        if (uanos == string.Empty)
        {
            objCommon.DisplayUserMessage(updCashBookAssign, "Please Select At least one cash book", this.Page);
            pnlListMain.Visible = false;
            return;
        }

        if (!(dt.Columns.Contains("COMPANY_NAME")))
            dt.Columns.Add("COMPANY_NAME");
        if (!(dt.Columns.Contains("COMPANY_NO")))
            dt.Columns.Add("COMPANY_NO");
        if (!(dt.Columns.Contains("COLLEGE_NAME")))
            dt.Columns.Add("COLLEGE_NAME");
        if (!(dt.Columns.Contains("COLLEGE_ID")))
            dt.Columns.Add("COLLEGE_ID");

        foreach (ListItem chkClg in chkCollege.Items)
        {
            if (chkClg.Selected == true)
            {
                DataRow dr = dt.NewRow();
                dr["COMPANY_NAME"] = ddlCompany.SelectedItem.Text;
                dr["COMPANY_NO"] = ddlCompany.SelectedValue;
                dr["COLLEGE_NAME"] = chkClg.Text;
                dr["COLLEGE_ID"] = chkClg.Value;

                dt.Rows.Add(dr);
            }
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (i != j)
                {
                    if (dt.Rows[j]["COLLEGE_ID"].ToString() == dt.Rows[i]["COLLEGE_ID"].ToString())
                        dt.Rows.RemoveAt(i);
                }
            }
        }
        Session["dtCollege"] = dt;
        lstCollege.DataSource = dt;
        lstCollege.DataBind();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        listdata.DataSource = null;
        listdata.DataBind();
        pnldata.Visible=false;
        PopulateDropDownList();
        BindCashBook();
        Session["dtCollege"] = null;
        lstCollege.DataSource = null;
        lstCollege.DataBind();
        pnlListMain.Visible = false;
        trSubmit.Visible = false;
        ddlCompany.Enabled = true;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ret = 0;
            DataTable dt = new DataTable();

            FinCashBookController objCBC = new FinCashBookController();
            string uanos = string.Empty;
            string links = string.Empty;
            string IPAddress = string.Empty;
            string Company_no = ddlCompany.SelectedValue;
            string date = DateTime.Now.ToString("dd-MMM-yyyy");

            CustomStatus cs = (CustomStatus)objCBC.AssignCollege(Company_no, (DataTable)Session["dtCollege"], date, Convert.ToInt32(Session["userno"].ToString()), IPAddress);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(updCashBookAssign, "Cash Book Assigned Successfully!!!", this.Page);
                PopulateDropDownList();
                BindCashBook();
                Session["dtCollege"] = null;
                lstCollege.DataSource = null;
                lstCollege.DataBind();
                pnlListMain.Visible = false;
                trSubmit.Visible = false;
                ddlCompany.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CompanyCollegeMapping_btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnldata.Visible = false;
        PopulateDropDownList();
        BindCashBook();
        Session["dtCollege"] = null;
        lstCollege.DataSource = null;
        lstCollege.DataBind();
        pnlListMain.Visible = false;
        trSubmit.Visible = false;
        ddlCompany.Enabled = true;
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstCollege.DataSource = null;
            lstCollege.DataBind();
            Session["dtCollege"] = null;
            string Company_no = ddlCompany.SelectedValue;
            DataSet ds = objFTS.GetCollegeData(_UAIMS, Company_no);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlListMain.Visible = true;
                trSubmit.Visible = true;
                lstCollege.DataSource = ds;
                lstCollege.DataBind();
                Session["dtCollege"] = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CompanyCollegeMapping_ddlCompany_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            ImageButton btnDelete = sender as ImageButton;
            int srno = Convert.ToInt32(btnDelete.CommandArgument);
            if (Session["dtCollege"] != null)
            {
                dt = (DataTable)Session["dtCollege"];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (srno - 1 == i)
                {
                    dt.Rows.RemoveAt(i);
                  
                    break;
                }
            }

            lstCollege.DataSource = dt;
            lstCollege.DataBind();
            Session["dtCollege"] = dt;
               
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "CompanyCollegeMapping_btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        string Company_no = ddlCompany.SelectedValue;
        DataSet ds = objCommon.FillDropDown("Acc_CompanyCollegeMapping A INNER JOIN ACD_COLLEGE_MASTER B ON (A.College_Id=B.COLLEGE_ID) INNER JOIN acc_company C ON (A.Company_No=C.COMPANY_CODE)", "A.Company_No,A.College_Id", "COLLEGE_NAME,COMPANY_NAME", "A.IsActive=1 AND A.Company_No='" + Company_no.ToString() + "'", "");
        if (ds.Tables[0].Rows.Count > 0)
        {
         
            pnldata.Visible = true;
            listdata.DataSource = ds;
            listdata.DataBind();
        }
        else
        {
            objCommon.DisplayUserMessage(updCashBookAssign, "Record Not Found!!!", this.Page);
            pnldata.Visible = false;
            listdata.DataSource = null;
            listdata.DataBind();
            return;
        }
    }
   
}

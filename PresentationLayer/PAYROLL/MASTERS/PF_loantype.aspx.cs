//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : PF_loantype.aspx                           
// CREATION DATE : 09-DEC-2009                                                     
// CREATED BY    : kiran g.v.s                                               
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_MASTERS_PF_loantype : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PFCONTROLLER objpfcontroller = new PFCONTROLLER();

    PF objpf = new PF();

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // Populate DropDownList
                this.FillPFMAST();
                //Bind the ListView with Qualification
                BindListViewPFLoanType();

                ViewState["action"] = "add";


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
                Response.Redirect("~/notauthorized.aspx?page=PF_loantype.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_loantype.aspx");
        }
    }

    private void BindListViewPFLoanType()
    {
        try
        {

            DataSet ds = objpfcontroller.GetAllPFLoanType();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPFLoanType.DataSource = ds;
                lvPFLoanType.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_loantype.BindListViewPFLoanType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
        ClearText();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {
                objpf.NAME = txtFullName.Text;
                objpf.SHORTNAME = txtShortName.Text;
                objpf.AMT = txtamt.Text==string.Empty?0:Convert.ToDecimal(txtamt.Text);
                objpf.PFNO = Convert.ToInt32(ddlPF.SelectedValue);
                objpf.APP_FOR = Convert.ToChar(1);
                if (chkDeducted.Checked)
                    objpf.DEDUCTED = true;
                else
                    objpf.DEDUCTED = false; 

                objpf.COLLEGE_CODE = Session["colcode"].ToString();

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objpfcontroller.AddPFLoanType(objpf);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewPFLoanType();
                        ViewState["action"] = "add";
                        ClearText();
                    }
                }
                else
                {
                    //Edit Qualification
                    if (ViewState["PFLOANTYPENO"] != null)
                    {
                        objpf.PFLOANTYPENO = Convert.ToInt32(ViewState["PFLOANTYPENO"].ToString());

                        CustomStatus cs = (CustomStatus)objpfcontroller.UpdatePFLoanType(objpf);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewPFLoanType();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(updMain, "Record updated successfully", this);
                            ClearText();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_loantype.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ClearText()
    {
        ddlPF.SelectedIndex = 0;
        txtamt.Text = "";
        txtFullName.Text = "";
        txtShortName.Text = "";
        chkDeducted.Checked = false;


    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PFLOANTYPENO = int.Parse(btnEdit.CommandArgument);
            ViewState["PFLOANTYPENO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.showdetails(PFLOANTYPENO);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_loantype.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void showdetails(int PFLOANTYPENO)
    {
        try
        {

            DataSet ds = objpfcontroller.GetPFLoanTypeByPfLaonTypeNo(PFLOANTYPENO);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFullName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
                ddlPF.SelectedValue = ds.Tables[0].Rows[0]["PFNO"].ToString();
                txtamt.Text = ds.Tables[0].Rows[0]["AMT"].ToString();
                chkDeducted.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["DEDUCTED"].ToString());
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_loantype.showdetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewPFLoanType();
    }
       
    private void FillPFMAST()
    {

        try
        {
            objCommon.FillDropDownList(ddlPF, "PAYROLL_PF_MAST", "PFNO", "'['+SHORTNAME+']'+' '+FULLNAME", "", "SHORTNAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_loantype.FillPFMAST-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
}

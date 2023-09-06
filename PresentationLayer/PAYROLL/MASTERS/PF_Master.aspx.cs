//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : PF_Master.aspx                                
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

public partial class PAYROLL_MASTERS_PF_Master : System.Web.UI.Page
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
                this.FillPayhead1();
                //Bind the ListView with Qualification
                BindListViewPFMaster();

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
                Response.Redirect("~/notauthorized.aspx?page=PF_Master.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=PF_Master.aspx");
        }
    }

    private void BindListViewPFMaster()
    {
        try
        {

            DataSet ds = objpfcontroller.GetAllPFMast();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPFmaster.DataSource = ds;
                lvPFmaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.BindListViewPFMaster-> " + ex.Message + " " + ex.StackTrace);
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
                objpf.FULLNAME = txtFullName.Text;
                objpf.SHORTNAME = txtShortName.Text;
                objpf.H1 = ddlSubscriptionHead.SelectedValue;
                objpf.H2 = ddlAddSubscriptionHead.SelectedValue;
                objpf.H3 = ddlRepaymentHead.SelectedValue;
                objpf.COLLEGE_CODE = Session["colcode"].ToString();

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objpfcontroller.AddPFMaster(objpf);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewPFMaster();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(updMain, "Record saved successfully", this);
                        ClearText();
                    }                    
                }
                else
                {
                    //Edit Qualification
                    if (ViewState["pfNo"] != null)
                    {
                        objpf.PFNO = Convert.ToInt32(ViewState["pfNo"].ToString());

                        CustomStatus cs = (CustomStatus)objpfcontroller.UpdatePFMast(objpf);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewPFMaster();
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
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int pfNo = int.Parse(btnEdit.CommandArgument);
            ViewState["pfNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.showdetails(pfNo);
            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ClearText()
    {
        txtFullName.Text = "";
        txtShortName.Text = "";
        ddlAddSubscriptionHead.SelectedIndex = 0;
        ddlRepaymentHead.SelectedIndex = 0;
        ddlSubscriptionHead.SelectedIndex = 0;
       

    }
    protected void showdetails(int pfNo)
    {
        try
        {

            DataSet ds = objpfcontroller.GetPFMastByPFNO(pfNo);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtFullName.Text = ds.Tables[0].Rows[0]["FULLNAME"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SHORTNAME"].ToString();
                ddlSubscriptionHead.SelectedValue = ds.Tables[0].Rows[0]["H1"].ToString();
                FillAddSubscriptionHeads(ds.Tables[0].Rows[0]["H1"].ToString());
                ddlAddSubscriptionHead.SelectedValue = ds.Tables[0].Rows[0]["H2"].ToString();
                FillRepaymentHeads(ds.Tables[0].Rows[0]["H1"].ToString(), ds.Tables[0].Rows[0]["H2"].ToString());
                ddlRepaymentHead.SelectedValue = ds.Tables[0].Rows[0]["H3"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.showdetails()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewPFMaster();
    }

    private void FillPayhead1()
    {
        try
        {
            objCommon.FillDropDownList(ddlSubscriptionHead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "SRNO>15 AND PAYSHORT IS NOT NULL AND PAYSHORT<>''", "PAYHEAD");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.FillPayhead1-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    protected void ddlSubscriptionHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubscriptionHead.SelectedIndex > 0)
        {
            this.FillAddSubscriptionHeads(ddlSubscriptionHead.SelectedValue);
           // this.FillRepaymentHeads(ddlSubscriptionHead.SelectedValue);
        }
    }

    protected void ddlAddSubscriptionHead_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSubscriptionHead.SelectedIndex > 0 && ddlAddSubscriptionHead.SelectedIndex > 0)
        {
            this.FillRepaymentHeads(ddlSubscriptionHead.SelectedValue, ddlAddSubscriptionHead.SelectedValue);
        }
    }

    private void FillAddSubscriptionHeads(string payhead)
    {
        try
        {
            objCommon.FillDropDownList(ddlAddSubscriptionHead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "SRNO>15 AND PAYHEAD NOT IN('" + payhead + "') AND PAYSHORT IS NOT NULL AND PAYSHORT<>''", "PAYHEAD");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.ddlSubscriptionHead_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void FillRepaymentHeads(string payhead1,string payhead2)
    {

        try
        {
            objCommon.FillDropDownList(ddlRepaymentHead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "SRNO>15 AND PAYHEAD NOT IN('" + payhead1 + "','" + payhead2 + "') AND PAYSHORT IS NOT NULL AND PAYSHORT<>''", "PAYHEAD");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_MASTERS_PF_Master.ddlAddSubscriptionHead_SelectedIndexChanged-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}

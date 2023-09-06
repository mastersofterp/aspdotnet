//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : Pay_Subpayhead_Mast.ascx                                  
// CREATION DATE : 21-05-2009                                                     
// CREATED BY    : kiran G.V.S
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class Masters_Pay_Subpayhead_Mast : System.Web.UI.UserControl
{
    Common objCommon = new Common();

    PayController objPay = new PayController();
    
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "Server UnAvailable");
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // Populate DropDownList
                PopulateDropdown();

                BindListViewSubPayHeads();

                ViewState["action"] = "add";

                objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Payroll" + "," + "CrystalReport.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_Subpayhead_Mast.aspx");
            }
        }
        else
        {
            Response.Redirect("~/notauthorized.aspx?page=Pay_Subpayhead_Mast.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
            objCommon.FillDropDownList(ddlMainPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYFULL", "PAYSHORT IS NOT NULL and PAYSHORT<>'' and PAYSHORT <> '-'", "SRNO");
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.PopulateDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewSubPayHeads()
    {
        try
        {   
            DataSet ds = objPay.GetAllSubPayHead();
            lvSubPayhead.DataSource = ds;
            lvSubPayhead.DataBind();           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.BindListViewSubPayHeads-> " + ex.Message + " " + ex.StackTrace);
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
        ddlMainPayhead.SelectedIndex = 0;
        txtshortpayhead.Text = string.Empty;
        txtFullName.Text = string.Empty;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
        chkBookAbj.Checked = false;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {               
               
                if (ViewState["action"].ToString().Equals("add"))
                {
                        lblStatus.Text = "Error";
                }
                else
                {
                    //Edit Quarter
                    if (ViewState["ibno"] != null)
                    {
                            lblStatus.Text = "Error";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;            
            ViewState["ibno"] = int.Parse(btnEdit.CommandArgument);
            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblPayhead = lst.FindControl("lblPayhead") as Label;
            Label lblSname = lst.FindControl("lblSname") as Label;
            Label lblFname = lst.FindControl("lblFname") as Label;
            Label lblBookAdj = lst.FindControl("lblBookAdj") as Label;
            if (lblBookAdj.Text=="Yes")
            {
                chkBookAbj.Checked = true;
            }
            else
            {
                chkBookAbj.Checked = false;
            }
            ddlMainPayhead.SelectedValue = lblPayhead.Text;
            txtshortpayhead.Text= lblSname.Text;
            txtFullName.Text=lblFname.Text;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_Pay_Subpayhead_Mast.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewSubPayHeads();
    }
    
    private  int ChkBookAdj()
    {
        int value = 0;
     
        if (chkBookAbj.Checked == true)
        {   
            value = 1;
        }
        else
        {   
            value = 0;
        }
        
        return value; 
    }   

}

//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : EMPLOYEE ACTVE OR INACTIVE                        
// CREATION DATE : 26-JULY-2011                                                          
// CREATED BY    : MRUNAL BANSOD                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
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
using IITMS.SQLServer.SQLDAL;

public partial class PAYROLL_TRANSACTIONS_Pay_EmpActiveInactive : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EmpMasController objemp = new EmpMasController();

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
        try
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
                }
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";
                //Populate DropdownList
                PopulateDropDownList();
               // int emp = Convert.ToInt32(ddlEmp.SelectedValue);

            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_EmpActiveInactive.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_EmpActiveInactive.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_EmpActiveInactive.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlRfid, "PAYROLL_EMPMAS", "RFIDNO", "RFIDNO AS RFID", "RFIDNO>0", "RFIDNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_EmpActiveInactive.PopulateDropDownList()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void ddlRfid_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.RFIDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.RFIDNO =" + ddlRfid.SelectedValue, "");
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int idno = Convert.ToInt32 (ddlEmp.SelectedValue);
        string activeStatus = string.Empty;
        try
        {
            if (chkActive.Checked == true)
            {
                activeStatus = "Y";
            }
            else
            {
                activeStatus = "N";
            }

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //Add New Help
                    CustomStatus cs = (CustomStatus)objemp.UpdateStatus(idno ,activeStatus);

                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {

                        objCommon.DisplayMessage("Record Updated Successfully", this);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Pay_EmpActiveInactive.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public void ShowStatus()
    {
        int idno = Convert.ToInt32(ddlEmp.SelectedValue);
        DataTableReader dtr = objemp.ShowEmpStatus(idno);
        if (dtr != null)
        {
            if (dtr.Read())
            {
                if (dtr["ACTIVE"].ToString() == "Y")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
            }
            dtr.Close();
        }
    }

    //To popup the messagebox.
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ShowStatus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ddlEmp.SelectedIndex = 0;
        ddlRfid.SelectedIndex = 0;
        txtrfid.Text = string.Empty;
    }
    protected void txtrfid_TextChanged(object sender, EventArgs e)
    {
        string rfidno = string.Empty;
        rfidno = txtrfid.Text;
        objCommon.FillDropDownList(ddlEmp, "PAYROLL_EMPMAS EM,PAYROLL_PAYMAS PM", "EM.IDNO AS IDNO", " '['+ CONVERT(NVARCHAR(20),EM.RFIDNO) +']'+ISNULL(TITLE,'')+' '+ISNULL(EM.FNAME,'')+' '+ISNULL(EM.MNAME,'')+' '+ISNULL(EM.LNAME,'') as ENAME", "EM.IDNO = PM.IDNO AND PM.PSTATUS='Y' and EM.IDNO > 0 AND EM.STATUS IS NULL AND EM.RFIDNO =" + txtrfid.Text, "");
        
       
    }
}

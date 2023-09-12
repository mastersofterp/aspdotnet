//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRoll_BulkITDeclaration.ASPX                                                    
// CREATION DATE : 11-JAN-2018                                                
// CREATED BY    : SACHIN GHAGRE                                                   
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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using BusinessLogicLayer.BusinessLogic;

public partial class PayRoll_BulkITDeclaration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ITDeclarationController ObjCon = new ITDeclarationController();
    ITDeclaration objITDE = new ITDeclaration();
    //ConnectionStrings
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        // CheckRef();

        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlSelect.Visible = true;
              
                           
                FillCollege();
                FillStaff();
                FillFinacialYear();
                FillITRules();

            }
        }
        else
        {

           
        }

    }

    protected void FillITRules()
    {
        objCommon.FillDropDownList(ddlITRule, "PAYROLL_ITRULE", "IT_RULE_ID", "IT_RULE_NAME", "IT_RULE_ID > 0  and IsActive =1", "IT_RULE_ID");
    }

    private void FillFinacialYear()
    {
        try
        {
            DateTime fdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITFDATE", ""));
            txtFromDate.Text = fdate.ToString();
            DateTime tdate = Convert.ToDateTime(objCommon.LookUp("PAYROLL_REFIT", "ITTDATE", ""));
            txtToDate.Text = tdate.ToString();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

   

    protected void btnSub_Click(object sender, EventArgs e)
    {
        try
        {
            BindEmployee();            
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem lvitem in lvITDeclaration.Items)
        {
            TextBox txt = lvitem.FindControl("txtDays") as TextBox;
            txt.Text = string.Empty;
        }
        ddlStaff.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
        lblerror.Text = string.Empty;
        lblmsg.Text = string.Empty;       

    }



    protected void FillStaff()
    {
        try
        {
            objCommon.FillDropDownList(ddlStaff, "PAYROLL_STAFF", "STAFFNO", "STAFF", "STAFFNO>0", "STAFFNO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void FillCollege()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID>0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_pay_modify_salary.FillDropdown-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


  
    private void BindEmployee()
    {
        try
        {
            if (!(Convert.ToInt32(ddlStaff.SelectedIndex) == 0))
            {
                int status = 0;
                DataSet ds = ObjCon.GetEmployeesByStaff(Convert.ToInt32(ddlStaff.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string fDate = txtFromDate.Text;
                    string tDate = txtToDate.Text;
                    foreach (DataRow drRow in ds.Tables[0].Rows)
                    {
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            int rowValue;
                            if (int.TryParse(drRow[i].ToString(), out rowValue))
                            {
                                //status = ObjCon.ITBulkDeclaration(fDate, tDate,Convert.ToInt32(drRow["IDNO"]),Convert.ToInt32(ddlITRule.SelectedValue));
                                status = ITBulkDeclaration(fDate, tDate,Convert.ToInt32(drRow["IDNO"]),Convert.ToInt32(ddlITRule.SelectedValue));
                              
                            }
                        }
                    }
                    if (status != 0)
                    {
                        string msg = string.Empty;
                        objCommon.DisplayMessage(UpdatePanel1, "IT DECLARATION DONE SUCCESSFULLY!!!", this);
                        return;
                        //msg = status == 1 ? "IT DECLARATION DONE SUCCESSFULLY!!!" : "IT DECLARATION FAILED!!!";
                        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
                    }
                }                      
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_Attendance.BindEmployee-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public int ITBulkDeclaration(string fromDate, string toDate, int idNo,int ITRuleId)
    {
        int retStatus = 0;
        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[5];
            objParams[0] = new SqlParameter("@P_FROMDATE", fromDate);
            objParams[1] = new SqlParameter("@P_TODATE", toDate);
            objParams[2] = new SqlParameter("@P_IDNO", idNo);
            objParams[3] = new SqlParameter("@P_ITRULEID", ITRuleId);
            objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
            objParams[4].Direction = ParameterDirection.Output;
            retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_IT_BULK_DECLARATION", objParams, true));
        }
        catch (Exception ex)
        {
            retStatus = Convert.ToInt32(CustomStatus.Error);
            throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.ITDeclarationController.ITBulkDeclaration-> " + ex.ToString());
        }
        return retStatus;
    }

  

}

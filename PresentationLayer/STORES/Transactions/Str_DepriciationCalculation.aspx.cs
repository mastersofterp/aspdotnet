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
using IITMS.UAIMS;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


public partial class STORES_Transactions_Str_DepriciationCalculation : System.Web.UI.Page
{

    Common ObjComman = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();


    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            ObjComman.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            ObjComman.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null || Session["strdeptname"] == null)
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

                    }
                }
                //if (rblAssestType.SelectedValue == "1")
                //{
                //    divFromDate.Visible = true;
                //    divToDate.Visible = true;
                //    divToFields.Visible = false;
                //}

                ObjComman.FillDropDownList(ddlCategory, "STORE_MAIN_ITEM_GROUP", "MIGNO", "MIGNAME", "MIGNO = 1", "MIGNAME");
                ObjComman.FillDropDownList(ddlSubCategory, "STORE_MAIN_ITEM_SUBGROUP", "MISGNO", "MISGNAME", "MIGNO=1", "MISGNAME");
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                ObjComman.ShowError(Page, "STORES_Transactions_Str_DepriciationCalculation.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                ObjComman.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?Payroll_LIC_Report.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Payroll_LIC_Report.aspx");
        }
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }


    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjComman.FillDropDownList(ddlItem, "STORE_ITEM", "ITEM_NO", "ITEM_NAME", "MISGNO=" + Convert.ToInt32(ddlSubCategory.SelectedValue) + "", "ITEM_NAME");

    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        ObjComman.FillDropDownList(ddlSerialNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue)+" AND DEPR_CAL_START_DATE IS NOT NULL" , "");
        //ObjComman.FillDropDownList(ddlSerialNo, "STORE_INVOICE_DSR_ITEM", "INVDINO", "DSR_NUMBER", "ITEM_NO=" + Convert.ToInt32(ddlItem.SelectedValue) + "","DEPR_CAL_START_DATE is not null","");
    
    }



    private void BindListViewDepreciation(string txtToDate)
    {
        try
        {
            DataSet ds = objStrMaster.CalculateDepriciation(Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlItem.SelectedValue), ddlSerialNo.SelectedItem.Text, Convert.ToDateTime(txtToDate));
            //DataSet ds = objStrMaster.GetDepreciation(Convert.ToInt32(ddlSubCategory.SelectedValue), Convert.ToInt32(ddlItem.SelectedValue), ddlSerialNo.SelectedItem.Text);
            lvDepreciation.DataSource = ds;
            lvDepreciation.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Str_DepriciationCalculation.BindListViewDepreciation-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    private void ShowEditDetailsDeptUser(int dpuNo)
    {
        DataSet ds = null;

        try
        {
            //ds = objStrMaster.GetSingleRecordDeptUser(dpuNo);
            ds = ObjComman.FillDropDown("STORE_DEPARTMENTUSER A INNER JOIN STORE_DEPARTMENT B ON (A.MDNO= B.MDNO )", "A.MDNO,MDNAME,UA_NO,APLNO,A.COLLEGE_CODE,A.ISAPPROVAL", "DUNO", "DUNO=" + dpuNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                ddlSubCategory.SelectedValue = ds.Tables[0].Rows[0]["APLNO"].ToString();
                ddlItem.SelectedValue = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                ddlSerialNo.SelectedValue = ds.Tables[0].Rows[0]["ISAPPROVAL"].ToString();

                
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.ShowEditDetailsDeptRegister-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

   

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //vishwas

       // ddlCategory.SelectedIndex="0";
        ddlSubCategory.SelectedIndex = 0;
        ddlItem.SelectedIndex = 0;
        ddlSerialNo.SelectedIndex = 0;
        txtToDate.Text = string.Empty;
        lvDepreciation.DataSource = null;
        lvDepreciation.DataBind();


        //            txtPaidAmount.Text = string.Empty;
        //txtDateOfPayment.Text = string.Empty;
        //ddlPayment.SelectedIndex = 0;
        //txtFromDate.Text = Convert.ToString(DateTime.Now.AddDays(DateTime.Now.Day + 1 - (DateTime.Now.Day * 2)).ToString("dd/MM/yyyy"));
        //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

    }


    protected void btnCalDepreciation_Click(object sender, EventArgs e)
    {
        //============================================10/05/2022======================================
        int ret = Convert.ToInt32(ObjComman.LookUp("STORE_DEPRECIATION_CALCULATION", "count(*)", "DSR_NUMBER ='" +ddlSerialNo.SelectedItem.Text+"'" ));
        if (ret > 0)
        {

            //select top  1 DEPR_FROM_DATE from STORE_DEPRECIATION_CALCULATION  WHERE DSR_NUMBER = 'RCPIT/ALMIRAH/1' order by dep_cal_id desc
            DataSet ds = ObjComman.FillDropDown("STORE_DEPRECIATION_CALCULATION", "top  1 DEP_CAL_ID", "DEPR_FROM_DATE" , "DSR_NUMBER ='" + ddlSerialNo.SelectedItem.Text + "'", "dep_cal_id desc");
            DateTime fromdate=Convert.ToDateTime(ds.Tables[0].Rows[0]["DEPR_FROM_DATE"]);
            if (Convert.ToDateTime(txtToDate.Text) < fromdate)
            {
                MessageBox("Up To Date Should Be Greater Than This Date " + fromdate.ToString("dd/MM/yyyy"));
                return;
            
            }
        }
        else
        { 
        //select DEPR_CAL_START_DATE  from STORE_INVOICE_DSR_ITEM where DSR_NUMBER = 'RCPIT/ALMIRAH/1'
            DateTime calstartdate = Convert.ToDateTime(ObjComman.LookUp("STORE_INVOICE_DSR_ITEM", "DEPR_CAL_START_DATE", "DSR_NUMBER ='" + ddlSerialNo.SelectedItem.Text + "'"));
            if (Convert.ToDateTime(txtToDate.Text) < calstartdate)
            {
                MessageBox("Up To Date Should Be Greater Than This Date " + calstartdate.ToString("dd/MM/yyyy"));
                return;
            
            }
        }
        //============================================10/05/2022======================================


        BindListViewDepreciation(txtToDate.Text);
    }
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["DEP_CAL_ID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsDeptUser(Convert.ToInt32(ViewState["DEP_CAL_ID"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "STORE_DEPRECIATION_CALCULATION.btnEdit_Click1-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
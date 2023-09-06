//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : Str_Dept_Master.aspx                                                  
// CREATION DATE : 01-Sept-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using IITMS;
//using IITMS.NITPRM;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS;
public partial class Stores_Masters_Str_Dept_Master : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StoreMasterController objStrMaster = new StoreMasterController();
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
        radYes.Checked = true;
        selected_tab.Value = Request.Form[selected_tab.UniqueID];
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
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["action"] = "add";
                AddStoreDept();
                FillDept();
                BindListViewSubDepartMent();
                BindListViewDepartMent();
                objCommon.FillDropDownList(ddlSubDept, "PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT", "SUBDEPTNO<>0", "SUBDEPTNO");
                //Set dept Report Parameters
               // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Departmeny_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
                //Set sub dept Report Parameters
                //objCommon.ReportPopUp(btnShowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Sub_Dept_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
                // Tabs.ActiveTabIndex = 1;
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
                Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=create_user.aspx");
        }
    }

    #region DEPARTMENT

    private void BindListViewDepartMent()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllDepartMent();
            ds = objCommon.FillDropDown("STORE_DEPARTMENT", "MDNAME,MDSNAME,COLLEGE_CODE", "MDNO", "", "");
            lvDepartment.DataSource = ds;
            lvDepartment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void AddStoreDept()
    {
        int CheckDuplicate = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", "count(*)", "MDNO=1"));
        if (CheckDuplicate == 0)
        {
            CustomStatus cs = (CustomStatus)objStrMaster.AddDepartment("MAIN STORE", "MAIN STORE", Session["colcode"].ToString(), Session["userfullname"].ToString());

        }
        int dublicateReff = Convert.ToInt32(objCommon.LookUp("STORE_REFERENCE", "Count(*)", ""));
        if (dublicateReff == 0)
        {
            CustomStatus cs1 = (CustomStatus)objStrMaster.AddStoreRefference(1);
        }
    }

    protected void butDepartMent_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", " count(*)", "mdname='" + Convert.ToString(txtDepartmentName.Text) + "'"));

                    if (duplicateCkeck == 0)
                    {
                        CustomStatus cs = (CustomStatus)objStrMaster.AddDepartment(txtDepartmentName.Text, txtDepartmentShortName.Text, Session["colcode"].ToString(), Session["userfullname"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            txtDepartmentName.Text = string.Empty;
                            txtDepartmentShortName.Text = string.Empty;
                            objCommon.DisplayMessage(updatePanel1, "Record Saved Successfully", this);
                            this.BindListViewDepartMent();
                            this.FillDept();
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel3, "Record Already Exist", this);
                    }
                }

                else
                {
                    if (ViewState["mdNo"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_DEPARTMENT", " count(*)", "mdname='" + Convert.ToString(txtDepartmentName.Text) + "' and mdno <> " + Convert.ToInt32(ViewState["mdNo"].ToString())));

                        if (duplicateCkeck == 0)
                        {
                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateDepartment(txtDepartmentName.Text, txtDepartmentShortName.Text, Session["colcode"].ToString(), Convert.ToInt32(ViewState["mdNo"].ToString()), Session["userfullname"].ToString());
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                txtDepartmentName.Text = string.Empty;
                                txtDepartmentShortName.Text = string.Empty;
                                objCommon.DisplayMessage(updatePanel3, "Record Updated Successfully", this);
                                this.BindListViewDepartMent();
                                this.FillDept();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel3, "Record Already Exist", this);
                        }

                    }

                }

            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation"))
            {
                objCommon.DisplayMessage("Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.butDepartMent_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }

        }
    }

    protected void btnEditDepartment_Click(object sender, EventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            ViewState["mdNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsDepartment(Convert.ToInt32(ViewState["mdNo"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.btnEditParty_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowEditDetailsDepartment(int mdNo)
    {
        DataSet ds = null;

        try
        {
            // ds = objStrMaster.GetSingleRecordDepartMent(mdNo);
            ds = objCommon.FillDropDown(" STORE_DEPARTMENT", "MDNAME,MDSNAME", "MDNO", "MDNO=" + mdNo, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtDepartmentName.Text = ds.Tables[0].Rows[0]["MDNAME"].ToString();
                txtDepartmentShortName.Text = ds.Tables[0].Rows[0]["MDSNAME"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.ShowEditDetailsDepartment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void butbutDepartMentCancel_Click(object sender, EventArgs e)
    {
        txtDepartmentName.Text = string.Empty;
        txtDepartmentShortName.Text = string.Empty;
        ViewState["action"] = "add";
    }

    protected void dpPagerDepartment_PreRender(object sender, EventArgs e)
    {
        BindListViewDepartMent();
    }

    #endregion

    #region SUBDEPARTMENT

    private void BindListViewSubDepartMent()
    {
        try
        {
            DataSet ds = null;// objStrMaster.GetAllSubDepartMent();
            ds = objCommon.FillDropDown("STORE_SUBDEPARTMENT A INNER JOIN STORE_DEPARTMENT B ON (A.MDNO=B.MDNO)", "SDNAME ,SDSNAME ,SHOW ,A.MDNO,B.MDNAME,A.COLLEGE_CODE", "SDNO", "", "");
            lvSubDepartment.DataSource = ds;
            lvSubDepartment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            int show = 0;

            if (radNo.Checked)
                show = 0;
            else
                show = 1;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SUBDEPARTMENT", " count(*)", "mdno=" + Convert.ToInt32(ddlMainDepartment.SelectedValue) + "and sdname= '" + Convert.ToString(ddlSubDept.SelectedItem.Text.ToString()) + "'"));
                    if (duplicateCkeck == 0)
                    {

                        CustomStatus cs = (CustomStatus)objStrMaster.AddSubDepartMent(Convert.ToInt32(ddlSubDept.SelectedValue) == 0 ? "-" : ddlSubDept.SelectedItem.Text, txtSubDepartmentShortName.Text, show, Convert.ToInt32(ddlMainDepartment.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString(), Convert.ToInt32(ddlSubDept.SelectedValue) == 0 ? -99 : Convert.ToInt32(ddlSubDept.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            this.BindListViewSubDepartMent();
                            objCommon.DisplayMessage(updatePanel1, "Record Saved Successfully", this);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updatePanel1, "Record Already Exist", this);
                    }
                }
                else
                {
                    if (ViewState["SDNO"] != null)
                    {
                        int duplicateCkeck = Convert.ToInt32(objCommon.LookUp("STORE_SUBDEPARTMENT", " count(*)", " mdno=" + Convert.ToInt32(ddlMainDepartment.SelectedValue) + " and sdname='" + Convert.ToString(ddlSubDept.SelectedItem.Text) + "' and sdno <> " + Convert.ToInt32(ViewState["SDNO"].ToString())));
                        if (duplicateCkeck == 0)
                        {

                            CustomStatus csupd = (CustomStatus)objStrMaster.UpdateSubDepartMent(Convert.ToInt32(ViewState["SDNO"].ToString()), Convert.ToInt32(ddlSubDept.SelectedValue) == 0 ? "-" : ddlSubDept.SelectedItem.Text, txtSubDepartmentShortName.Text, show, Convert.ToInt32(ddlMainDepartment.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString(), Convert.ToInt32(ddlSubDept.SelectedValue) == 0 ? -99 : Convert.ToInt32(ddlSubDept.SelectedValue));
                            if (csupd.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                objCommon.DisplayMessage(updatePanel3, "Record Updated Successfully", this);
                                this.BindListViewSubDepartMent();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(updatePanel1, "Record Already Exist", this);
                        }
                    }

                }
            }
            radYes.Checked = true;
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Unique Key Violation"))
            {
                objCommon.DisplayMessage("Record Already Exist", Page);
            }
            else
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.butSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objUCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void butSubDepartmentCancel_Click(object sender, EventArgs e)
    {

        this.Clear();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void Clear()
    {
        ddlSubDept.SelectedValue = "0";
        txtSubDepartmentShortName.Text = string.Empty;
        ddlMainDepartment.SelectedValue = "0";
        radNo.Checked = false;
        radYes.Checked = false;
        ViewState["action"] = "add";

    }

    protected void btnEditSubDepartment_Click(object sender, EventArgs e)
    {
        try
        {
            radNo.Checked = false;
            radYes.Checked = false;
            ImageButton btnEdit = sender as ImageButton;
            ViewState["SDNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsSubDepartment(Convert.ToInt32(ViewState["SDNO"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.btnEditSubDepartment_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowEditDetailsSubDepartment(int sdNo)
    {
        DataSet ds = null;

        try
        {
            ds = objCommon.FillDropDown("STORE_SUBDEPARTMENT A INNER JOIN STORE_DEPARTMENT B ON (A.MDNO=B.MDNO)", "SDNAME,SDSNAME,SHOW,A.MDNO,B.MDNAME,A.COLLEGE_CODE,isnull(PAYROLL_SUBDEPTNO,0) PAYROLL_SUBDEPTNO", "SDNO", "A.SDNO=" + sdNo, "");
            // ds = objStrMaster.GetSingleRecordSubDepartMent(sdNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMainDepartment.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                //txtSubDepartmentName.Text = ds.Tables[0].Rows[0]["SDNAME"].ToString();
                ddlSubDept.SelectedValue = ds.Tables[0].Rows[0]["PAYROLL_SUBDEPTNO"].ToString() == "-99" ? "0" : ds.Tables[0].Rows[0]["PAYROLL_SUBDEPTNO"].ToString();
                txtSubDepartmentShortName.Text = ds.Tables[0].Rows[0]["SDSNAME"].ToString();

                if (ds.Tables[0].Rows[0]["SHOW"].ToString() == "False")
                    radNo.Checked = true;

                if (ds.Tables[0].Rows[0]["SHOW"].ToString() == "True")
                    radYes.Checked = true;
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.ShowEditDetailsSubDepartment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void FillDept()
    {
        try
        {
            objCommon.FillDropDownList(ddlMainDepartment, "STORE_DEPARTMENT", "mdNo", "mdname", "mdNo>0", "mdname");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Dept_Master.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPagerSubDepartment_PreRender(object sender, EventArgs e)
    {
        BindListViewSubDepartMent();
    }

    #endregion

   

    protected void tab_activetabindexchanged(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
    }
    protected void btnShowrpt1_Click(object sender, EventArgs e)
    {
        ShowReport1("DepartmentMasterReport", "Sub_Dept_Report.rpt");
        //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Departmeny_List_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
        //Set sub dept Report Parameters
        //objCommon.ReportPopUp(btnShowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Sub_Dept_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
                
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        ShowReport1("DepartmentMasterReport", "Departmeny_List_Report.rpt");
    }

    private void ShowReport1(string reportTitle, string rptFileName)
    {


        try
        {
            //objCommon.ReportPopUp(btnshowrpt1, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "Vendor_Category_report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");      
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@UserName=" + Session["userfullname"].ToString();
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_ReportName=" + reportTitle + ",@P_VENDORNO=" + vendorno + ",@P_VENDORWISE=" + rblSelectAllVendor.SelectedValue + ",@P_FDATE=" + Convert.ToDateTime(Fdate).ToString("dd-MMM-yyyy") + ",@P_TDATE=" + Convert.ToDateTime(Ldate).ToString("dd-MMM-yyyy") + ",@P_MDNO=" + Session["MDNO"].ToString();
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Str_Tax_Master.ShowTaxMasterReport-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

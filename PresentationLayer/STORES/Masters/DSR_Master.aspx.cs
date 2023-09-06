//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : DSR_MASTER.aspx                                                  
// CREATION DATE : 02-March-2010                                                       
// CREATED BY    : Chaitanya c Bhure                                                       
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
//using IITMS.NITPRM;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
public partial class Stores_Masters_DSR_Master : System.Web.UI.Page
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
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                ViewState["StoreUser"] = null;
                this.CheckMainStoreUser();
                this.FillDept();
                this.FillGrand();
                BindListViewDSR();
                bindYear();
                ViewState["action"] = "add";
                //Set Report Parameters
                objCommon.ReportPopUp(btnshowrpt, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Stores" + "," + "DSR_Master_Report.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
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
                Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Not Authorized");
        }
    }

    //Check for Main Store User.
    private bool CheckMainStoreUser()
    {
        string test = Application["strrefmaindept"].ToString();
        string test1 = Session["strdeptcode"].ToString();

        if (Session["strdeptcode"].ToString() == Application["strrefmaindept"].ToString())
        {
            ViewState["StoreUser"] = "MainStoreUser";
            return true;
        }
        else
        {
            this.CheckDeptStoreUser();
            return false;
        }
    }

    //Check for Department Store User.
    private bool CheckDeptStoreUser()
    {
        string test = objCommon.LookUp("STORE_DEPARTMENTUSER", "APLNO", "UA_NO=" + Convert.ToInt32(Session["userno"]));

        // When department user is having approval level as Department Store means "4". It is fixed in Store Reference table.
        string deptStoreUser = objCommon.LookUp("STORE_REFERENCE", "DEPT_STORE_USER", "");

        if (test == deptStoreUser)
        {
            ViewState["StoreUser"] = "DeptStoreUser";
            return true;
        }
        else
        {
            ViewState["StoreUser"] = "NormalUser";
            return false;

        }
    }


    protected void FillDept()
    {
        try
        {
            //main store user
            if (ViewState["StoreUser"].ToString() == "MainStoreUser")
            {
                objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "mdno>0", "mdname");
            }
            else if (ViewState["StoreUser"].ToString() == "DeptStoreUser")
            {
                objCommon.FillDropDownList(ddlDepartment, "STORE_DEPARTMENT", "mdno", "mdname", "MDNO=" + Convert.ToInt32(Session["strdeptcode"]), "mdname");
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillDept-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void FillGrand()
    {
        try
        {
            objCommon.FillDropDownList(ddlGrand, "STORE_GRANDMASTER", "GRANDNO", "GRAND_NAME", "GRANDNO>0", "GRAND_NAME");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_Dept_User.FillGrand-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewDSR()
    {
        try
        {
            DataSet ds = objStrMaster.GetAllDSR();
            lvDSR.DataSource = ds;
            lvDSR.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_DSR_Master.BindListViewDepartMent-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void butSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    int Status = Convert.ToInt32(objCommon.LookUp("STORE_DEADSTOCKMASTER", "count(*)", "DSTK_NAME = '" + txtDSRName.Text + "' AND DSTK_SHORT_NAME = '" + txtDsrShortName.Text + "' AND MDNO='" + ddlDepartment.SelectedValue + "' AND DRNO='" + txtDRNO.Text + "'"));

                    if (Status == 0)
                    {
                        CustomStatus res = (CustomStatus)objStrMaster.AddDSR(txtDSRName.Text, txtDsrShortName.Text, Convert.ToInt32(ddlyear.SelectedValue), txtDRNO.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlGrand.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());

                        if (res.Equals(CustomStatus.RecordSaved))
                        {
                            this.Clear();
                            this.BindListViewDSR();
                            objCommon.DisplayMessage(updpnlMain, "Record Saved Successfully", this);
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                    }
                }

                else
                {
                    int Status = Convert.ToInt32(objCommon.LookUp("STORE_DEADSTOCKMASTER", "count(*)", "DSTK_NAME = '" + txtDSRName.Text + "' AND DSTK_SHORT_NAME = '" + txtDsrShortName.Text + "' AND DRNO='" + txtDRNO.Text + "' AND DSTKNO <> " + Convert.ToInt32(ViewState["dskno"].ToString())+" AND MDNO=" + ddlDepartment.SelectedValue));

                    if (Status == 0)
                    {

                        if (ViewState["dskno"] != null)
                        {

                            int dskno = Convert.ToInt32(ViewState["dskno"].ToString());
                            CustomStatus res = (CustomStatus)objStrMaster.UpdateDSR(Convert.ToInt32(ViewState["dskno"].ToString()), txtDSRName.Text, txtDsrShortName.Text, Convert.ToInt32(ddlyear.SelectedValue), txtDRNO.Text, Convert.ToInt32(ddlDepartment.SelectedValue), Convert.ToInt32(ddlGrand.SelectedValue), Session["colcode"].ToString(), Session["userfullname"].ToString());

                            if (res.Equals(CustomStatus.RecordUpdated))
                            {
                                ViewState["action"] = "add";
                                this.Clear();
                                this.BindListViewDSR();
                                objCommon.DisplayMessage(updpnlMain, "Record Updated Successfully", this);

                            }
                        }
                    }

                    else
                    {
                        objCommon.DisplayMessage(updpnlMain, "Record Already Exist", this);
                    }
                }
            }





        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_DSR_MASTER.butSubDSR_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void butCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["dskno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowEditDetailsDSR(Convert.ToInt32(ViewState["dskno"].ToString()));


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_DSR_MASTER.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void lvDSR_PreRender(object sender, EventArgs e)
    {

    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewDSR();
    }
    void Clear()
    {
        ddlGrand.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtDSRName.Text = string.Empty;
        txtDsrShortName.Text = string.Empty;
        txtDRNO.Text = string.Empty;
        ddlyear.SelectedIndex = 0;
    }

    private void ShowEditDetailsDSR(int DSKNO)
    {
        DataSet ds = null;

        try
        {
            // ds = objStrMaster.GetSingleRecordDSR (DSKNO );
            ds = objCommon.FillDropDown("STORE_DEADSTOCKMASTER", "DSTK_NAME,DSTK_SHORT_NAME,DSTK_YEAR,DRNO,MDNO,GRANDNO", "DSTKNO", "DSTKNO=" + DSKNO, "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["MDNO"].ToString();
                ddlGrand.SelectedValue = ds.Tables[0].Rows[0]["GRANDNO"].ToString();
                txtDSRName.Text = ds.Tables[0].Rows[0]["DSTK_NAME"].ToString();
                txtDsrShortName.Text = ds.Tables[0].Rows[0]["DSTK_SHORT_NAME"].ToString();
                txtDRNO.Text = ds.Tables[0].Rows[0]["DRNO"].ToString();
                ddlyear.SelectedValue = ds.Tables[0].Rows[0]["DSTK_YEAR"].ToString();
            }
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Stores_Masters_DSR_MASTER.ShowEditDetailsDSR-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    void bindYear()
    {
        DataSet ds = objStrMaster.GetAllFinancial_Year();
        ddlyear.DataSource = ds;
        ddlyear.DataValueField = "fno";
        ddlyear.DataTextField = "fname";
        ddlyear.DataBind();


    }
    protected void btnshowrpt_Click(object sender, EventArgs e)
    {
        ShowReport("DSR REPORT", "DSR_Master_Report.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("stores")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,STORES," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"] + "," + "@UserName=" + Session["username"];
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            //To open new window from Updatepanel
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            //if (Convert.ToBoolean(Session["error"]) == true)
            //    objUCommon.ShowError(Page, "Academic_StudentRoolist.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            //else
            //    objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowReport("DSR REPORT", "DSR_Master_Report.rpt");
    }
}

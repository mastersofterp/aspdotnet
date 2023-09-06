//=========================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH  (Laboratory Test)     
// CREATION DATE : 13-APR-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//========================================================================== 
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

using System.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Web.Caching;
using System.IO;
using System.Drawing;
using System.Configuration;

public partial class Health_LaboratoryTest_TestContent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LabMaster objLab = new LabMaster();
    LabController objLabController = new LabController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    objCommon.FillDropDownList(ddlTitle, "HEALTH_TEST_TITLE", "TITLENO", "TITLE", "", "TITLENO");
                    ViewState["action"] = "add";

                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = 0;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.Page_Load -> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (lvContent.Items.Count < 0)
            {
                //objCommon.DisplayMessage(this.updActivity, "Please Add Test Content.", this.Page);
                MessageBox("Please Add Test Content.");
                return;
            }
            //if(txtContent.Text == string.Empty)
            //{
                //objCommon.DisplayMessage(this.updActivity, "Please Add Test Content.", this.Page);
            //    MessageBox("Please Add Test Content.");
            //    return;
            //}

            string grpName = string.Empty;
            string contentName = string.Empty;
            string unit = string.Empty;
            string norRange = string.Empty;

            DataTable dt;
            dt = (DataTable)Session["RecTbl"];
            if (dt.Rows.Count <= 0)
            {
                MessageBox("Please Add Test Content.");
                return;
            }
            else
            {

            }
            objLab.TESTCONTENT = dt;
            objLab.TITLENO = Convert.ToInt32(ddlTitle.SelectedValue);

            objLab.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objLabController.AddUpdateTestContent(objLab);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        Clear();
                        objCommon.DisplayMessage(this.updActivity, "Record Save Successfully.", this.Page);
                    }
                }
                else
                {
                    if (ViewState["CONTENTNO"] != null)
                    {
                        objLab.CONTENTNO = Convert.ToInt32(ViewState["CONTENTNO"].ToString());
                        CustomStatus cs = (CustomStatus)objLabController.AddUpdateTestContent(objLab);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            Clear();
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {

                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.updActivity, "Record Update Successfully.", this.Page);
                            Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CONTENTNO = int.Parse(btnEdit.CommandArgument);
            ViewState["CONTENTNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(CONTENTNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void ShowDetails(int CONTENTNO)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_TEST_CONTENT", "*", "", "CONTENTNO=" + CONTENTNO, "CONTENTNO");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGrpName.Text = ds.Tables[0].Rows[0]["TITLE"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtGrpName.Text = string.Empty;
        txtContent.Text = string.Empty;
        txtNormalValue.Text = string.Empty;
        txtUnit.Text = string.Empty;
        ViewState["CONTENTNO"] = null;
        ViewState["action"] = "add";
        lvContent.DataSource = null;
        lvContent.DataBind();
        lvContent.Visible = false;
        ddlTitle.SelectedIndex = 0;
        DataTable dt;
        dt = (DataTable)Session["RecTbl"];
        dt.Clear();
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_TITLENO=0";

            ScriptManager.RegisterClientScriptBlock(updActivity, updActivity.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("TestContents", "rptTestContentReport.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTitle.SelectedIndex > 0)
            {
                DataSet dsRec = objLabController.GetContentByTitle(Convert.ToInt32(ddlTitle.SelectedValue));
                if (Convert.ToInt32(dsRec.Tables[0].Rows.Count) > 0)
                {
                    lvContent.DataSource = dsRec.Tables[0];
                    lvContent.DataBind();
                    lvContent.Visible = true;
                    Session["RecTbl"] = dsRec.Tables[0];
                    ViewState["SRNO"] = Convert.ToInt32(dsRec.Tables[0].Rows.Count);
                }
                else
                {
                    lvContent.DataSource = null;
                    lvContent.DataBind();
                    lvContent.Visible = false;
                    Session["RecTbl"] = null;
                    ViewState["SRNO"] = null;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select Title.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                DataTable dt = (DataTable)Session["RecTbl"];
                dt.Rows.Remove(this.GetEditableDatarow(dt, btnDelete.CommandArgument));
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["errror"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private DataTable CreateTable()
    {
        DataTable dtRe = new DataTable();
        dtRe.Columns.Add(new DataColumn("SRNO", typeof(int)));
        dtRe.Columns.Add(new DataColumn("GROUP_NAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("CONTENT_NAME", typeof(string)));
        dtRe.Columns.Add(new DataColumn("UNIT", typeof(string)));
        dtRe.Columns.Add(new DataColumn("NORMAL_RANGE", typeof(string)));
        return dtRe;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lvContent.Visible = true;

            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                int maxVal = 0;
                DataTable dt = (DataTable)Session["RecTbl"];
                DataRow dr = dt.NewRow();

                if (CheckDuplicate(dt, txtContent.Text))
                {
                    txtContent.Focus();
                    objCommon.DisplayMessage(this.updActivity, "This Content Name Already Exist.", this.Page);
                    return;
                }

                if (dr != null)
                {
                    maxVal = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SRNO"]));
                }
                if (ViewState["EDIT_SRNO"] != null)
                {
                    dr["SRNO"] = Convert.ToInt32(ViewState["EDIT_SRNO"]);
                }
                else
                {
                    dr["SRNO"] = maxVal + 1;
                }
                dr["GROUP_NAME"] = txtGrpName.Text.Trim() == null ? string.Empty : Convert.ToString(txtGrpName.Text.Trim()).Replace(',', ' ');
                dr["CONTENT_NAME"] = txtContent.Text.Trim() == null ? string.Empty : Convert.ToString(txtContent.Text.Trim()).Replace(',', ' ');
                dr["UNIT"] = txtUnit.Text.Trim() == null ? string.Empty : Convert.ToString(txtUnit.Text.Trim()).Replace(',', ' ');
                dr["NORMAL_RANGE"] = txtNormalValue.Text.Trim() == null ? string.Empty : Convert.ToString(txtNormalValue.Text.Trim()).Replace(',', ' ');

                dt.Rows.Add(dr);
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
                ClearRec();
                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
            }
            else
            {
                DataTable dt = this.CreateTable();
                DataRow dr = dt.NewRow();
                dr["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dr["GROUP_NAME"] = txtGrpName.Text.Trim() == null ? string.Empty : Convert.ToString(txtGrpName.Text.Trim()).Replace(',', ' ');
                dr["CONTENT_NAME"] = txtContent.Text.Trim() == null ? string.Empty : Convert.ToString(txtContent.Text.Trim()).Replace(',', ' ');
                dr["UNIT"] = txtUnit.Text.Trim() == null ? string.Empty : Convert.ToString(txtUnit.Text.Trim()).Replace(',', ' ');
                dr["NORMAL_RANGE"] = txtNormalValue.Text.Trim() == null ? string.Empty : Convert.ToString(txtNormalValue.Text.Trim()).Replace(',', ' ');

                ViewState["SRNO"] = Convert.ToInt32(ViewState["SRNO"]) + 1;
                dt.Rows.Add(dr);
                ClearRec();
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnAdd_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void ClearRec()
    {
        txtGrpName.Text = string.Empty;
        txtContent.Text = string.Empty;
        txtUnit.Text = string.Empty;
        txtNormalValue.Text = string.Empty;
        ViewState["actionContent"] = null;
        ViewState["EDIT_SRNO"] = null;
    }

    protected void btnEditRec_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEditRec = sender as ImageButton;
            DataTable dt;
            if (Session["RecTbl"] != null && ((DataTable)Session["RecTbl"]) != null)
            {
                dt = ((DataTable)Session["RecTbl"]);
                ViewState["EDIT_SRNO"] = btnEditRec.CommandArgument;
                DataRow dr = this.GetEditableDatarow(dt, btnEditRec.CommandArgument);
                txtGrpName.Text = dr["GROUP_NAME"].ToString();
                txtContent.Text = dr["CONTENT_NAME"].ToString();
                txtUnit.Text = dr["UNIT"].ToString();
                txtNormalValue.Text = dr["NORMAL_RANGE"].ToString();
                dt.Rows.Remove(dr);
                Session["RecTbl"] = dt;
                lvContent.DataSource = dt;
                lvContent.DataBind();
                ViewState["actionContent"] = "edit";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["erreor"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnEditRec_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private DataRow GetEditableDatarow(DataTable dt, string value)
    {
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["SRNO"].ToString() == value)
                {
                    datRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.GetEditableDatarow() -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return datRow;
    }

    private bool CheckDuplicate(DataTable dt, string value)
    {
        bool retVal = false;
        DataRow datRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["CONTENT_NAME"].ToString() == value)
                {
                    datRow = dr;
                    retVal = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_LaboratoryTest_TestContent.btnDelete_Click -->" + ex.Message + "" + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return retVal;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}
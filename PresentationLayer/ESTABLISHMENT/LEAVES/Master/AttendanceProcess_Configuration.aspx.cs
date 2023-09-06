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

public partial class ESTABLISHMENT_LEAVES_Master_AttendanceProcess_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    EMP_ATTANDANCE objAtt = new EMP_ATTANDANCE();

    #region PageEvents
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
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
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            FillCollege();
            FillStaffType();
            BindListViewAttendanceProcess();
            ViewState["action"] = "add";

        }


    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OD_Purpose.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OD_Purpose.aspx");
        }
    }



    #endregion
    //protected void ddlstaff_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindListViewAttendanceProcess();
    //}
    //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindListViewAttendanceProcess();
    //}
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_ID ASC");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);
        }
    }
    private void FillStaffType()
    {
        objCommon.FillDropDownList(ddlstaff, "PAYROLL_STAFFTYPE", "STNO", "STAFFTYPE", "stno > 0 AND ACTIVESTATUS =" + 1, "stafftype");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string action = ViewState["action"].ToString();

            bool result = CheckConfig();
            

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        Clear();
                        return;
                    }
                    else
                    {

                        objAtt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                        objAtt.STNO = Convert.ToInt32(ddlstaff.SelectedValue);
                        objAtt.PROCESS_FROM = Convert.ToInt32(txtProcessFrom.Text);
                        objAtt.PROCESS_TO = Convert.ToInt32(txtProcessTo.Text);
                        CustomStatus cs = (CustomStatus)objAttandance.AddAttandanceProcess(objAtt);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            BindListViewAttendanceProcess();
                            Clear();
                        }
                    }

                }
                else
                {
                    if (ViewState["ConfigNo"] != null)
                    {
                        int ConfigNo = Convert.ToInt32(ViewState["ConfigNo"]);
                        objAtt.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
                        objAtt.STNO = Convert.ToInt32(ddlstaff.SelectedValue);
                        objAtt.PROCESS_FROM = Convert.ToInt32(txtProcessFrom.Text);
                        objAtt.PROCESS_TO = Convert.ToInt32(txtProcessTo.Text);
                        CustomStatus cs = (CustomStatus)objAttandance.UpdAttandanceProcess(objAtt, ConfigNo);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Updated Successfully");
                            BindListViewAttendanceProcess();
                            Clear();
                        }
                    }
                }


            }
        }
        catch (Exception ex)
        {

        }
    }


    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    public bool CheckConfig()
    {
        bool result = false;
        DataSet dsConfig = new DataSet();

        int ProcessFrom = Convert.ToInt32(txtProcessFrom.Text);
        int ProcessTo = Convert.ToInt32(txtProcessTo.Text);

        dsConfig = objCommon.FillDropDown("PAYROLL_LEAVE_CONFIGURATION", "*", "", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND STNO=" + Convert.ToInt32(ddlstaff.SelectedValue)+ " ", "");

        if (dsConfig.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    protected void BindListViewAttendanceProcess()
    {
        try
        {
            DataSet ds = objAttandance.GetAllAttendanceProcess();
            //if (ds.Tables[0].Rows.Count <= 0)
            //{
            //   // btnShowReport.Visible = false;
            //    //dpPager.Visible = false;
            //}
            //else
            //{
            //    //btnShowReport.Visible = true;
            //    // dpPager.Visible = true;
            //}
            lvAttProcess.DataSource = ds.Tables[0];
            lvAttProcess.DataBind();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.BindListViewHolidays -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        ddlCollege.SelectedIndex = 0;
        ddlstaff.SelectedIndex = 0;
        txtProcessFrom.Text = string.Empty;
        txtProcessTo.Text = string.Empty;
        ViewState["action"] = "add";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        Int32 ConfigNo = int.Parse(btnEdit.CommandArgument);
        ShowDetails(ConfigNo);
        ViewState["action"] = "edit";
    }

    private void ShowDetails(int ConfigNo)
    {
        DataSet ds = null;
        try
        {
            ds = objAttandance.GetSingleAttendanceProcess(ConfigNo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["ConfigNo"] = ConfigNo;
                ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
                ddlstaff.SelectedValue = ds.Tables[0].Rows[0]["STNO"].ToString();
                txtProcessFrom.Text = ds.Tables[0].Rows[0]["ProcessFromDay"].ToString();
                txtProcessTo.Text = ds.Tables[0].Rows[0]["ProcessToDay"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_AttendanceProcess_Configuration.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
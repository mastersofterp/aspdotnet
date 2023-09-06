//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH       
// CREATION DATE : 21-MAY-2016
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 

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

public partial class Health_Master_BloodGroupMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    HelMasterController objHelMaster = new HelMasterController();
    Health objHel = new Health();

    #region Page Events
    /// <summary>
    /// This Page_Load event checks whether the user has login or not by checking Session["userno"],Session["username"]   
    /// </summary>
    /// 
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
                        lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));

                }
                BindListView();
                ViewState["action"] = "add";
            }

            //divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    /// <summary>
    /// Page_PreInit event calls SetMasterPage() method.
    /// This method sets the theme to the master page.
    /// </summary>
    /// 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    #endregion

    #region Actions

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        try
        {
            objHel.BLOODGP_NAME = txtBGroup.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtBGroup.Text.Trim());
            objHel.COLLEGE_CODE = Session["colcode"].ToString();

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    objHel.BLOODGRNO = 0;
                    CustomStatus cs = (CustomStatus)objHelMaster.AddUpdateBloodGp(objHel);
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                        return;
                    }
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(this.updActivity, "Record Saved Successfully.", this.Page);
                        this.ClearControls();
                    }
                }
                else
                {
                    if (ViewState["BGID"] != null)
                    {
                        objHel.BLOODGRNO = Convert.ToInt32(ViewState["BGID"].ToString());
                        CustomStatus cs = (CustomStatus)objHelMaster.AddUpdateBloodGp(objHel);
                        if (cs.Equals(CustomStatus.RecordExist))
                        {
                            objCommon.DisplayMessage(this.updActivity, "Record Already Exist.", this.Page);
                            return;
                        }
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListView();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(this.updActivity, "Record Updated Successfully.", this.Page);
                            this.ClearControls();
                        }
                    }
                }
            }


        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_OnClick(object sender, EventArgs e)
    {
        ViewState["action"] = "add";
        this.ClearControls();
    }



    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int BGID = int.Parse(btnEdit.CommandArgument);
            ViewState["BGID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetail(BGID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    protected void Report_Click(object sender, EventArgs e)
    {
        ShowReport("Blood Group", "rptBloodGroup.rpt");
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListView();
    }

    #endregion

    #region Private Methods
    /// <summary>
    /// CheckPageAuthorization() method checks whether the user is authorised to access this Page
    /// If he is not authorised, user will be redirected to "notauthorized.aspx" page and message is displayed that "You Are Not Authorized To Use this page".
    /// </summary>
    /// 
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=CreateOperator.aspx");
        }
    }

    /// <summary>
    /// BindListView() method fetches all the records from HEL_DOCTORMASTER table with the help of GetDoctorByDRID() method.
    /// These records are bind to the listview "lvDoctor".
    /// </summary>
    /// 
    private void BindListView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_BLOODGROUP", "BLOODGRNO", "BLOODGR", "", "");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvBGroup.DataSource = ds.Tables[0];
                lvBGroup.DataBind();
            }
            else
            {
                lvBGroup.DataSource = null;
                lvBGroup.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }



    private void ClearControls()
    {
        txtBGroup.Text = string.Empty;
        ViewState["BGID"] = null;
        ViewState["action"] = "add";

    }

    private void ShowDetail(int BGID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("HEALTH_BLOODGROUP", "BLOODGRNO", "BLOODGR", "BLOODGRNO=" + BGID, "");
            if (ds.Tables[0].Rows.Count != null)
            {
                txtBGroup.Text = ds.Tables[0].Rows[0]["BLOODGR"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnRport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("BloodGroup", "rptBloodGroup.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Health_Master_BloodGroupMaster.btnReport_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Health")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,HEALTH," + rptFileName;
            url += "&param=@P_BGID=0";
            ScriptManager.RegisterClientScriptBlock(updActivity, updActivity.GetType(), "Window", "window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');", true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Health_Master_BloodGroupMaster.ShowReport ->" + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


}
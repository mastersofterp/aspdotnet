//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE 
// PAGE NAME     : Pay_HolidayType.aspx                                                   
// CREATION DATE : 4 july 2012
// CREATED BY    : Mrunal Bansod                                       
// MODIFIED DATE : 
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Master_HolidayType : System.Web.UI.Page
{
    //Creating objects of Class Files Common,UAIMS_COMMON,LeaveController
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objHoliType = new LeavesController();


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
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                CheckPageAuthorization();
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;

                BindListViewHoliType();
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnBack.Visible = false;
                //Set Report Parameters 
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_Holidays.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
                // objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "ESTABLISHMENT" + "," + "LEAVES" + "," + "ESTB_PassAuthority.rpt&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=HolidayType.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=HolidayType.aspx");
        }
    }

    protected void BindListViewHoliType()
    {
        try
        {
            DataSet ds = objHoliType.GetAllHolidayType();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                btnShowReport.Visible = false;
              //  dpPager.Visible = false;
            }
            else
            {
                btnShowReport.Visible = true;
               // dpPager.Visible = true;
            }
            lvHolyType.DataSource = ds;
            lvHolyType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListViewHoliType();
    }

    private void Clear()
    {
        txtHolyType.Text = string.Empty;
        ViewState["HTNO"] = null;
        ViewState["action"] = "add";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;

        ViewState["action"] = "add";
        btnSave.Visible = true;
        btnCancel.Visible = true;
        btnBack.Visible = true;

        btnAdd.Visible = false;
        btnShowReport.Visible = false;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = CheckPurpose();

            Leaves objLeaves = new Leaves();
            objLeaves.HOLIDAYTYPE = Convert.ToString(txtHolyType.Text);

            objLeaves.COLLEGE_CODE = Convert.ToString(Session["colcode"]);

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objHoliType.AddHolidayType(objLeaves);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            MessageBox("Record Saved Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewHoliType();
                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                        }
                    }
                }
                else
                {
                    if (ViewState["HTNO"] != null)
                    {
                        objLeaves.HTNO = Convert.ToInt32(ViewState["HTNO"].ToString());
                        CustomStatus cs = (CustomStatus)objHoliType.UpdateHolidayType(objLeaves);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            MessageBox("Record Updated Successfully");
                            pnlAdd.Visible = false;
                            pnlList.Visible = true;
                            BindListViewHoliType();
                            ViewState["action"] = null;
                            Clear();
                            btnSave.Visible = false;
                            btnCancel.Visible = false;
                            btnBack.Visible = false;

                            btnAdd.Visible = true;
                            btnShowReport.Visible = true;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnSave_Click ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Clear();
        txtHolyType.Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false;
        pnlList.Visible = true;
        btnSave.Visible = false;
        btnCancel.Visible = false;
        btnBack.Visible = false;

        btnAdd.Visible = true;
        btnShowReport.Visible = true;       
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int HTNO = int.Parse(btnEdit.CommandArgument);
            ShowDetails(HTNO);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlList.Visible = false;

            btnSave.Visible = true;
            btnCancel.Visible = true;
            btnBack.Visible = true;

            btnAdd.Visible = false;
            btnShowReport.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int HTNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objHoliType.DeleteHolidaytype(HTNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                ViewState["action"] = null;
                BindListViewHoliType();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void ShowDetails(Int32 HTNO)
    {
        DataSet ds = null;
        try
        {
            ds = objHoliType.GetSingHolidayType(HTNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["HTNO"] = HTNO;
                txtHolyType.Text = ds.Tables[0].Rows[0]["HOLIDAYTYPE"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Passing_Authority.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Holidays_Entry", "ESTB_Holiday_Type.rpt");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.btnShowReport_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ESTABLISHMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ESTABLISHMENT,LEAVES," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            string collegeno = Session["college_nos"].ToString();
            string[] values = collegeno.Split(',');
            if (values.Length > 1)
            {
                url += "&param=@P_COLLEGE_CODE= 0," + "@username=" + Session["userfullname"].ToString();
            }
            else
            {
                url += "&param=@P_COLLEGE_CODE=" + Session["college_nos"].ToString() + "," + "@username=" + Session["userfullname"].ToString();
            }
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Holidays.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_HOLIDAYTYPE", "*", "", "HOLIDAYTYPE='" + txtHolyType.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
        //  MessageBox("Record Deleted Successfully");
    }
}

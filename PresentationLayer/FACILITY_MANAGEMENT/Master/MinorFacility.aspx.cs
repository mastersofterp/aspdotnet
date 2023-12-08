//==============================================
//CREATED BY: Swati Ghate
//CREATED DATE:14-04-2018
//PURPOSE: TO CREATE MINOR FACILITY
//==============================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class MinorFacility : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
   
    FacilityEntity objFM = new FacilityEntity();
    FacilityController objFC = new FacilityController();

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
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnlAdd.Visible = false;
                pnlList.Visible = true;
                pnlbutton.Visible = false;
               // CheckPageAuthorization();
               
            }

        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=MinorFacility.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=MinorFacility.aspx");
        }
    }
    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("FACILITY_MANAGEMENT")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,FACILITY_MANAGEMENT," + rptFileName;
            url += "&param=@username=" + Session["username"].ToString() + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comparative.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iresult = 0;
        objFM.MinFacilityName = txtFacilityName.Text;
        objFM.MinFacilityDetail = txtDetail.Text;
        objFM.IsActive = true;
        objFM.CreatedBy =Convert.ToInt32(Session["userno"]);
        objFM.IPAddress = Session["ipAddress"].ToString();
        objFM.MacAddress = Convert.ToString(Session["macAddress"]);
        objFM.CollegeCode = Convert.ToInt32(Session["colcode"]);
        if (ViewState["action"] != null)
        {
            if (ViewState["action"].ToString() == "add")
            {
                objFM.MinFacilityNo = 0;              
                iresult =Convert.ToInt32( objFC.AddUpdateMinorFacility(objFM));
                if (iresult <= 0)
                {
                    MessageBox("Record Already Exists");
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                    ViewState["action"] = null;
                    Clear();
                    BindMinFacility();
                }
                if (iresult == 1)
                {
                    MessageBox("Record Saved Successfully");
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                    ViewState["action"] = null;
                    Clear();
                    BindMinFacility();
                }
            }
            else
            {
                objFM.MinFacilityNo = Convert.ToInt32(ViewState["MinFacilityNo"]);

             
                iresult = Convert.ToInt32(objFC.AddUpdateMinorFacility(objFM));
                if (iresult <= 0)
                {
                    MessageBox("Record Already Exists");
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                    ViewState["action"] = null;
                    Clear();
                    BindMinFacility();
                }
                if (iresult == 1)
                {
                    MessageBox("Record Updated Successfully");
                    pnlAdd.Visible = false;
                    pnlList.Visible = true;
                    ViewState["action"] = null;
                    Clear();
                    BindMinFacility();
                }
            }
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
       
    }
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int MinFacilityNo = int.Parse(btnDelete.CommandArgument);
            DataSet ds = objCommon.FillDropDown("Facility_Detail", "*", "", "MinFacilityNo=" + MinFacilityNo + " and ISNULL(IsActive,0)=1 ", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox("Record Already In Used. Not Allow To Delete");
            }
            else
            {
                CustomStatus cs = (CustomStatus)objFC.DeleteMinorFacility(objFM);
                if (cs.Equals(CustomStatus.RecordDeleted))
                {
                    MessageBox("Record Deleted Successfully");
                    ViewState["action"] = null;
                    BindMinFacility();
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int MinFacilityNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(MinFacilityNo);
            ViewState["action"] = "edit";
            pnlAdd.Visible = true;
            pnlbutton.Visible = true;
            pnlList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void ShowDetails(Int32 MinFacilityNo)
    {
        DataSet ds = null;
        try
        {
            objFM.MinFacilityNo = MinFacilityNo;
            ds = objFC.GetMinorFacilityByNo(objFM);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MinFacilityNo"] = MinFacilityNo;
                txtFacilityName.Text = ds.Tables[0].Rows[0]["MinFacilityName"].ToString();
                txtDetail.Text = ds.Tables[0].Rows[0]["MinFacilityDetail"].ToString();                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.ShowDetails->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void Clear()
    {
        txtFacilityName.Text=txtDetail.Text = string.Empty;
        pnlbutton.Visible = false;
        pnlList.Visible = true;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = false; pnlbutton.Visible = false;
        pnlList.Visible = true;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Clear();
        pnlAdd.Visible = true;
        pnlList.Visible = false;
        pnlbutton.Visible = true;
        ViewState["action"] = "add";
    }
    protected void BindMinFacility()
    {
        try
        {
            DataSet ds = objFC.GetMinorFacility();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                //btnShowReport.Visible = false;
                dpPager.Visible = false;
            }
            else
            {
                //btnShowReport.Visible = true;
                dpPager.Visible = true;
            }
            lvFacility.DataSource = ds;
            lvFacility.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Facility_Management_MinorFacility.BindListViewPAuthority ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindMinFacility();
    }

}

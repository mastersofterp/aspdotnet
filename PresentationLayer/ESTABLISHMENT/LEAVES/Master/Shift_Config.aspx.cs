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

public partial class ESTABLISHMENT_LEAVES_Master_Shift_Config : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    Shifts objShift = new Shifts();

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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


            }
            BindListView();
            FillCollege();
            //FillDropDown();
            pnlSft.Visible = false;
            pnlShift.Visible = false;
            pnlLvShift.Visible = true;
            ViewState["action"] = null;
        }
    }
    #endregion

    #region Actions

    //To Add New Shift
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Clear();
        pnlSft.Visible = true;
        pnlShift.Visible = true;
        pnlLvShift.Visible = false;
        btnUpdatesft.Visible = false;

        ViewState["action"] = "add";
        ddlShift.Visible = false;
        txtShift.Visible = true;
        btnSaveSft.Visible = true;

        btnAdd.Visible = false;
        butBack.Visible = false;
        btnModifySft.Visible = false;
    }

    //To Display Report
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
    }

    //To save shift
    protected void btnSaveSft_Click(object sender, EventArgs e)
    {
        //if (btnSaveSft.Text == "Save")
        if (Convert.ToString(ViewState["action"]) == "add")
        {
            DataSet ds;
            ds = objCommon.FillDropDown("PAYroll_LEAVE_SHIFTMAS", "*", "", "SHIFTNAME='" + txtShift.Text.Trim().ToUpper() + "' and COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue), "SHIFTNO");
            if (ds.Tables[0].Rows.Count == 0)
            {
                AddNewShift();
                //Clear();
                UpdateClear();
                //objCommon.DisplayMessage("Record Saved Successfully", this);
            }
            else
            {
                //objUCommon.ShowError(Page, "SHIFT ALREADY EXISTS!!!");
                objCommon.DisplayMessage("Record already exists", this);
            }
        }
        else
        {
           // UpdateShift();
            //BindListView();
            Clear();
        }
        BindListView();

    }



    //To Refresh the list
    protected void btnCancelSft_Click(object sender, EventArgs e)
    {
        //Clear();
        pnlSft.Visible = true; ;
        pnlShift.Visible = true;
        pnlLvShift.Visible = false;
        //Clear();

        txtShift.Text = "";
        txtSunIn.Text = "";
        txtSunOut.Text = "";
        txtMonIn.Text = "";
        txtMonOut.Text = "";
        txtTueIn.Text = "";
        txtTueOut.Text = "";
        txtWedIn.Text = "";
        txtWedOut.Text = "";
        txtThuIn.Text = "";
        txtThuOut.Text = "";
        txtFriIn.Text = "";
        txtFriOut.Text = "";
        txtSatIn.Text = "";
        txtSatOut.Text = "";

        txtSunHours.Text = txtMonHours.Text = txtTueHours.Text = txtWedHours.Text= txtthuHours.Text = txtFriHours.Text = txtSatHours.Text = "";

        chkSun.Checked = chkMon.Checked = chkTue.Checked = chkWed.Checked = chkThu.Checked = chkFri.Checked = chkSat.Checked = false;
        ddlCollege.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        txtShift.Text = "";
        

    }

    //To Get Back to the previous page
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Clear();
        pnlSft.Visible = false;
        pnlShift.Visible = false;
        pnlLvShift.Visible = true;
        btnAdd.Visible = true;
        btnModifySft.Visible = true;
        butBack.Visible = true;
        UpdateClear();
    }

    #endregion

    #region Methods

   /* protected void AddNewShift()
    {
        //int shiftNo = 0;
        int shiftNo = Convert.ToInt32(ViewState["SHIFTNO"]);
        objShift.SHIFTNAME = txtShift.Text.Trim().ToUpper();
        objShift.STATUS[0] = chkSun.Checked == true ? 1 : 0;
        objShift.STATUS[1] = chkMon.Checked == true ? 1 : 0;
        objShift.STATUS[2] = chkTue.Checked == true ? 1 : 0;
        objShift.STATUS[3] = chkWed.Checked == true ? 1 : 0;
        objShift.STATUS[4] = chkThu.Checked == true ? 1 : 0;
        objShift.STATUS[5] = chkFri.Checked == true ? 1 : 0;
        objShift.STATUS[6] = chkSat.Checked == true ? 1 : 0;
        objShift.INTIME[0] = txtSunIn.Text.Trim();
        objShift.INTIME[1] = txtMonIn.Text.Trim();
        objShift.INTIME[2] = txtTueIn.Text.Trim();
        objShift.INTIME[3] = txtWedIn.Text.Trim();
        objShift.INTIME[4] = txtThuIn.Text.Trim();
        objShift.INTIME[5] = txtFriIn.Text.Trim();
        objShift.INTIME[6] = txtSatIn.Text.Trim();
        objShift.OUTTIME[0] = txtSunOut.Text.Trim();
        objShift.OUTTIME[1] = txtMonOut.Text.Trim();
        objShift.OUTTIME[2] = txtTueOut.Text.Trim();
        objShift.OUTTIME[3] = txtWedOut.Text.Trim();
        objShift.OUTTIME[4] = txtThuOut.Text.Trim();
        objShift.OUTTIME[5] = txtFriOut.Text.Trim();
        objShift.OUTTIME[6] = txtSatOut.Text.Trim();
        shiftNo = Convert.ToInt32(objCommon.LookUp("PAYroll_LEAVE_SHIFTMAS", "ISNULL(MAX(SHIFTNO),0)", ""));
        objAttandance.ShiftInsUpdate(objShift, shiftNo, 1);//3RD is Passed as 1 For Insert
        objAttandance.UpdateShiftRef();


    }*/

    protected void AddNewShift()
    {
        int shiftNo = 0;
        DataTable dtShift = new DataTable();
        dtShift.Columns.Add("SHIFTNAME");
        dtShift.Columns.Add("STATUS");
        dtShift.Columns.Add("DAYNO");
        dtShift.Columns.Add("INTIME");
        dtShift.Columns.Add("OUTTIME");
        dtShift.Columns.Add("HOURS");

        DataRow dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkSun.Checked == true ? 1 : 0;
        dr["DAYNO"] = 1;
        if (chkSun.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            
            dr["INTIME"] = txtSunIn.Text.Trim();
            dr["OUTTIME"] = txtSunOut.Text.Trim();
            dr["HOURS"] = txtSunHours.Text.Trim();
        }

        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkMon.Checked == true ? 1 : 0;
        dr["DAYNO"] = 2;
        if (chkMon.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtMonIn.Text.Trim();
            dr["OUTTIME"] = txtMonOut.Text.Trim();
            dr["HOURS"] = txtMonHours.Text.Trim();
        }

        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkTue.Checked == true ? 1 : 0;
        dr["DAYNO"] = 3;
        if (chkTue.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtTueIn.Text.Trim();
            dr["OUTTIME"] = txtTueOut.Text.Trim();
            dr["HOURS"] = txtTueHours.Text.Trim();
        }


        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkWed.Checked == true ? 1 : 0;
        dr["DAYNO"] = 4;
        if (chkWed.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtWedIn.Text.Trim();
            dr["OUTTIME"] = txtWedOut.Text.Trim();
            dr["HOURS"] = txtWedHours.Text.Trim();
        }


        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkThu.Checked == true ? 1 : 0;
        dr["DAYNO"] = 5;

        if (chkThu.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtThuIn.Text.Trim();
            dr["OUTTIME"] = txtThuOut.Text.Trim();
            dr["HOURS"] = txtthuHours.Text.Trim();
        }


        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkFri.Checked == true ? 1 : 0;
        dr["DAYNO"] = 6;


        if (chkFri.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtFriIn.Text.Trim();
            dr["OUTTIME"] = txtFriOut.Text.Trim();
            dr["HOURS"] = txtFriHours.Text.Trim();
        }

        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkSat.Checked == true ? 1 : 0;
        dr["DAYNO"] = 7;
        if (chkSat.Checked == false)
        {
            dr["INTIME"] = "00:00:00".Trim();
            dr["OUTTIME"] = "00:00:00".Trim();
            dr["HOURS"] = "00:00:00".Trim();
        }
        else
        {
            dr["INTIME"] = txtSatIn.Text.Trim();
            dr["OUTTIME"] = txtSatOut.Text.Trim();
            dr["HOURS"] = txtSatHours.Text.Trim();
        }

        dtShift.Rows.Add(dr);


        objShift.UA_NO = Convert.ToInt32(Session["userno"]);
        objShift.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        objAttandance.ShiftInsUpdate(objShift, dtShift);

        //objShift.SHIFTNAME = txtShift.Text.Trim().ToUpper();
        //objShift.STATUS[0] = chkSun.Checked == true ? 1 : 0;
        //objShift.STATUS[1] = chkMon.Checked == true ? 1 : 0;
        //objShift.STATUS[2] = chkTue.Checked == true ? 1 : 0;
        //objShift.STATUS[3] = chkWed.Checked == true ? 1 : 0;
        //objShift.STATUS[4] = chkThu.Checked == true ? 1 : 0;
        //objShift.STATUS[5] = chkFri.Checked == true ? 1 : 0;
        //objShift.STATUS[6] = chkSat.Checked == true ? 1 : 0;
        //objShift.INTIME[0] = txtSunIn.Text.Trim();
        //objShift.INTIME[1] = txtMonIn.Text.Trim();
        //objShift.INTIME[2] = txtTueIn.Text.Trim();
        //objShift.INTIME[3] = txtWedIn.Text.Trim();
        //objShift.INTIME[4] = txtThuIn.Text.Trim();
        //objShift.INTIME[5] = txtFriIn.Text.Trim();
        //objShift.INTIME[6] = txtSatIn.Text.Trim();
        //objShift.OUTTIME[0] = txtSunOut.Text.Trim();
        //objShift.OUTTIME[1] = txtMonOut.Text.Trim();
        //objShift.OUTTIME[2] = txtTueOut.Text.Trim();
        //objShift.OUTTIME[3] = txtWedOut.Text.Trim();
        //objShift.OUTTIME[4] = txtThuOut.Text.Trim();
        //objShift.OUTTIME[5] = txtFriOut.Text.Trim();
        //objShift.OUTTIME[6] = txtSatOut.Text.Trim();
        //shiftNo=Convert.ToInt32(objCommon.LookUp("PAYROLL_LEAVE_SHIFTMAS","ISNULL(MAX(SHIFTNO),0)",""));
        // objAttandance.ShiftInsUpdate(objShift, shiftNo, 1);//3RD is Passed as 1 For Insert

        //objAttandance.UpdateShiftRef();
        //MessageBox("Record Saved Successfully");
        objCommon.DisplayMessage("Record Saved Successfully", this);

    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

  /*  protected void UpdateShift()
    {
        //int shiftNo = Convert.ToInt32(ddlShift.SelectedValue);
        int shiftNo = Convert.ToInt32(ViewState["SHIFTNO"]);
        //objShift.SHIFTNAME = ddlShift.SelectedItem.Text.ToUpper().Trim();
        objShift.SHIFTNAME = txtShift.Text;

        objShift.STATUS[0] = chkSun.Checked == true ? 1 : 0;
        objShift.STATUS[1] = chkMon.Checked == true ? 1 : 0;
        objShift.STATUS[2] = chkTue.Checked == true ? 1 : 0;
        objShift.STATUS[3] = chkWed.Checked == true ? 1 : 0;
        objShift.STATUS[4] = chkThu.Checked == true ? 1 : 0;
        objShift.STATUS[5] = chkFri.Checked == true ? 1 : 0;
        objShift.STATUS[6] = chkSat.Checked == true ? 1 : 0;

        objShift.INTIME[0] = txtSunIn.Text.Trim();
        objShift.INTIME[1] = txtMonIn.Text.Trim();
        objShift.INTIME[2] = txtTueIn.Text.Trim();
        objShift.INTIME[3] = txtWedIn.Text.Trim();
        objShift.INTIME[4] = txtThuIn.Text.Trim();
        objShift.INTIME[5] = txtFriIn.Text.Trim();
        objShift.INTIME[6] = txtSatIn.Text.Trim();

        objShift.OUTTIME[0] = txtSunOut.Text.Trim();
        objShift.OUTTIME[1] = txtMonOut.Text.Trim();
        objShift.OUTTIME[2] = txtTueOut.Text.Trim();
        objShift.OUTTIME[3] = txtWedOut.Text.Trim();
        objShift.OUTTIME[4] = txtThuOut.Text.Trim();
        objShift.OUTTIME[5] = txtFriOut.Text.Trim();
        objShift.OUTTIME[6] = txtSatOut.Text.Trim();

        objAttandance.ShiftInsUpdate(objShift, shiftNo, 0);//3RD is Passed as 0 For Update
        //BindListView();

    }*/

    protected void Clear()
    {
        ViewState["action"] = "add";
        ViewState["SHIFTNO"] = null;
        //Response.Redirect("ESTABLISHMENT/LEAVES/Master/Shift_Config.aspx");
        //Response.Redirect("Shift_Config.aspx");
        //string url = string.Empty;
        //url += HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        //// url += "/PresentationLayer/Images/courseselection.png";//local
        //url += "\\PresentationLayer\\ESTABLISHMENT\\LEAVES\\Master\\Shift_Config.aspx";//
        //Response.Redirect(url);        
        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Shift_Config.aspx');", true);
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        //Bind the ListView with Domain            
        BindListView();
    }

    protected void BindListView()
    {
        DataSet ds;
        ds = objAttandance.GetShiftList();
        lvShift.DataSource = ds;
        lvShift.DataBind();
    }



    #endregion

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SHIFTNO = int.Parse(btnEdit.CommandArgument);
            ViewState["SHIFTNO"] = SHIFTNO.ToString();
            ShowDetails(SHIFTNO);
            ViewState["action"] = "edit";
            pnlShift.Visible = true;
            pnlLvShift.Visible = false;

            pnlSft.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                //objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Shift_Config.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Shift_Config.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnDelete = sender as ImageButton;
            int SHIFTNO = int.Parse(btnDelete.CommandArgument);
            CustomStatus cs = (CustomStatus)objAttandance.DeleteShifttype(SHIFTNO);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage("Deleted Sucessfully", this);
                ViewState["action"] = null;
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Shift_Config.btnDelete_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(Int32 SHIFTNO)
    {
        DataSet ds = null;
        try
        {
            ds = objAttandance.GetShiftDetails(SHIFTNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SHIFTNO"] = SHIFTNO;
                txtShift.Text = ds.Tables[0].Rows[0]["SHIFTNAME"].ToString();
                txtSunIn.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
                txtSunOut.Text = ds.Tables[0].Rows[0]["OUTTIME"].ToString();
                txtMonIn.Text = ds.Tables[0].Rows[1]["INTIME"].ToString();
                txtMonOut.Text = ds.Tables[0].Rows[1]["OUTTIME"].ToString();
                txtTueIn.Text = ds.Tables[0].Rows[2]["INTIME"].ToString();
                txtTueOut.Text = ds.Tables[0].Rows[2]["OUTTIME"].ToString();
                txtWedIn.Text = ds.Tables[0].Rows[3]["INTIME"].ToString();
                txtWedOut.Text = ds.Tables[0].Rows[3]["OUTTIME"].ToString();
                txtThuIn.Text = ds.Tables[0].Rows[4]["INTIME"].ToString();
                txtThuOut.Text = ds.Tables[0].Rows[4]["OUTTIME"].ToString();
                txtFriIn.Text = ds.Tables[0].Rows[5]["INTIME"].ToString();
                txtFriOut.Text = ds.Tables[0].Rows[5]["OUTTIME"].ToString();
                txtSatIn.Text = ds.Tables[0].Rows[6]["INTIME"].ToString();
                txtSatOut.Text = ds.Tables[0].Rows[6]["OUTTIME"].ToString();

                //To get the check box status
                chkSun.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].ToString());
                chkMon.Checked = Convert.ToBoolean(ds.Tables[0].Rows[1]["STATUS"].ToString());
                chkTue.Checked = Convert.ToBoolean(ds.Tables[0].Rows[2]["STATUS"].ToString());
                chkWed.Checked = Convert.ToBoolean(ds.Tables[0].Rows[3]["STATUS"].ToString());
                chkThu.Checked = Convert.ToBoolean(ds.Tables[0].Rows[4]["STATUS"].ToString());
                chkFri.Checked = Convert.ToBoolean(ds.Tables[0].Rows[5]["STATUS"].ToString());
                chkSat.Checked = Convert.ToBoolean(ds.Tables[0].Rows[6]["STATUS"].ToString());

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                //objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Shift_Config.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
                objUCommon.ShowError(Page, "ESTABLISHMENT_LEAVES_Master_Shift_Config.btnEdit_Click->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    private void FillCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ")", "COLLEGE_NAME");

        if (Session["username"].ToString() != "admin")
        {
            ListItem removeItem = ddlCollege.Items.FindByValue("0");
            ddlCollege.Items.Remove(removeItem);

        }
    }

    protected void butBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ESTABLISHMENT/Configuration/Attendance_Config.aspx");
    }
    protected void btnModifySft_Click(object sender, EventArgs e)
    {
        //Clear();
        pnlSft.Visible = true;
        pnlShift.Visible = true;
        pnlLvShift.Visible = false;
        txtShift.Visible = false;
        ddlShift.Visible = true;
        btnSaveSft.Visible = false;
        FillShift();


        ViewState["action"] = "add";

        btnAdd.Visible = false;
        btnUpdatesft.Visible = true;
        butBack.Visible = false;
        btnModifySft.Visible = false;
    }

    protected void FillShift()
    {
        //objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + " or " + Convert.ToInt32(ddlCollege.SelectedValue) + "=0", "SHIFTNAME");
        objCommon.FillDropDownList(ddlShift, "PAYROLL_LEAVE_SHIFTMAS", "DISTINCT(SHIFTNO)", "SHIFTNAME", "COLLEGE_NO=" + Convert.ToInt32(ddlCollege.SelectedValue) + "", "SHIFTNAME");

    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlShift.SelectedIndex == 0)
            {
                //btnAddSft.Enabled = false;
                Clear();
            }
            else
            {
                //btnAddSft.Enabled = true;
                DataSet ds;
                ds = objAttandance.GetShiftDetails(Convert.ToInt32(ddlShift.SelectedValue.Trim()), Convert.ToInt32(ddlCollege.SelectedValue));
                //Getting Chech Status
                chkSun.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].ToString());
                chkMon.Checked = Convert.ToBoolean(ds.Tables[0].Rows[1]["STATUS"].ToString());
                chkTue.Checked = Convert.ToBoolean(ds.Tables[0].Rows[2]["STATUS"].ToString());
                chkWed.Checked = Convert.ToBoolean(ds.Tables[0].Rows[3]["STATUS"].ToString());
                chkThu.Checked = Convert.ToBoolean(ds.Tables[0].Rows[4]["STATUS"].ToString());
                chkFri.Checked = Convert.ToBoolean(ds.Tables[0].Rows[5]["STATUS"].ToString());
                chkSat.Checked = Convert.ToBoolean(ds.Tables[0].Rows[6]["STATUS"].ToString());

                //Getting In Times
                txtSunIn.Text = ds.Tables[0].Rows[0]["INTIME"].ToString();
                txtMonIn.Text = ds.Tables[0].Rows[1]["INTIME"].ToString();
                txtTueIn.Text = ds.Tables[0].Rows[2]["INTIME"].ToString();
                txtWedIn.Text = ds.Tables[0].Rows[3]["INTIME"].ToString();
                txtThuIn.Text = ds.Tables[0].Rows[4]["INTIME"].ToString();
                txtFriIn.Text = ds.Tables[0].Rows[5]["INTIME"].ToString();
                txtSatIn.Text = ds.Tables[0].Rows[6]["INTIME"].ToString();

                //Getting Out Times
                txtSunOut.Text = ds.Tables[0].Rows[0]["OUTTIME"].ToString();
                txtMonOut.Text = ds.Tables[0].Rows[1]["OUTTIME"].ToString();
                txtTueOut.Text = ds.Tables[0].Rows[2]["OUTTIME"].ToString();
                txtWedOut.Text = ds.Tables[0].Rows[3]["OUTTIME"].ToString();
                txtThuOut.Text = ds.Tables[0].Rows[4]["OUTTIME"].ToString();
                txtFriOut.Text = ds.Tables[0].Rows[5]["OUTTIME"].ToString();
                txtSatOut.Text = ds.Tables[0].Rows[6]["OUTTIME"].ToString();



                txtSunHours.Text = ds.Tables[0].Rows[0]["WORKHOURS"].ToString();
                txtMonHours.Text = ds.Tables[0].Rows[1]["WORKHOURS"].ToString();
                txtTueHours.Text = ds.Tables[0].Rows[2]["WORKHOURS"].ToString();
                txtWedHours.Text = ds.Tables[0].Rows[3]["WORKHOURS"].ToString();
                txtthuHours.Text = ds.Tables[0].Rows[4]["WORKHOURS"].ToString();
                txtFriHours.Text = ds.Tables[0].Rows[5]["WORKHOURS"].ToString();
                txtSatHours.Text = ds.Tables[0].Rows[6]["WORKHOURS"].ToString();
            
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillShift();
        }
        catch (Exception ex)
        {
        }
    }
    
    protected void btnUpdatesft_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateShift();
            //Clear();
            UpdateClear();
            BindListView();
        }
        catch (Exception ex)
        {
        }
    }

    protected void UpdateShift()
    {
        int shiftNo = Convert.ToInt32(ddlShift.SelectedValue);
        DataTable dtShift = new DataTable();
        dtShift.Columns.Add("SHIFTNAME");
        dtShift.Columns.Add("STATUS");
        dtShift.Columns.Add("DAYNO");
        dtShift.Columns.Add("INTIME");
        dtShift.Columns.Add("OUTTIME");
        dtShift.Columns.Add("HOURS");

        DataRow dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkSun.Checked == true ? 1 : 0;
        dr["DAYNO"] = 1;
        dr["INTIME"] = txtSunIn.Text.Trim();
        dr["OUTTIME"] = txtSunOut.Text.Trim();
        dr["HOURS"] = txtSunHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkMon.Checked == true ? 1 : 0;
        dr["DAYNO"] = 2;
        dr["INTIME"] = txtMonIn.Text.Trim();
        dr["OUTTIME"] = txtMonOut.Text.Trim();
        dr["HOURS"] = txtMonHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkTue.Checked == true ? 1 : 0;
        dr["DAYNO"] = 3;
        dr["INTIME"] = txtTueIn.Text.Trim();
        dr["OUTTIME"] = txtTueOut.Text.Trim();
        dr["HOURS"] = txtTueHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkWed.Checked == true ? 1 : 0;
        dr["DAYNO"] = 4;
        dr["INTIME"] = txtWedIn.Text.Trim();
        dr["OUTTIME"] = txtWedOut.Text.Trim();
        dr["HOURS"] = txtWedHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkThu.Checked == true ? 1 : 0;
        dr["DAYNO"] = 5;
        dr["INTIME"] = txtThuIn.Text.Trim();
        dr["OUTTIME"] = txtThuOut.Text.Trim();
        dr["HOURS"] = txtthuHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkFri.Checked == true ? 1 : 0;
        dr["DAYNO"] = 6;
        dr["INTIME"] = txtFriIn.Text.Trim();
        dr["OUTTIME"] = txtFriOut.Text.Trim();
        dr["HOURS"] = txtFriHours.Text.Trim();
        dtShift.Rows.Add(dr);

        dr = dtShift.NewRow();
        dr["SHIFTNAME"] = ddlShift.SelectedItem.Text.ToString().Trim().ToUpper();//txtShift.Text.Trim().ToUpper();
        dr["STATUS"] = chkSat.Checked == true ? 1 : 0;
        dr["DAYNO"] = 7;
        dr["INTIME"] = txtSatIn.Text.Trim();
        dr["OUTTIME"] = txtSatOut.Text.Trim();
        dr["HOURS"] = txtSatHours.Text.Trim();
        dtShift.Rows.Add(dr);

        objShift.SHIFTNO = Convert.ToInt32(ddlShift.SelectedValue);
        objShift.UA_NO = Convert.ToInt32(Session["userno"]);
        objShift.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        objAttandance.ShiftInsUpdate(objShift, dtShift);
        //MessageBox("Record Updated Successfully");
        objCommon.DisplayMessage("Record Updated Successfully", this);


        /*
        objShift.SHIFTNAME = ddlShift.SelectedItem.Text.ToUpper().Trim();
        objShift.COLLEGE_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        objShift.UA_NO = Convert.ToInt32(ddlCollege.SelectedValue);
        objShift.STATUS[0] = chkSun.Checked == true ? 1 : 0;
        objShift.STATUS[1] = chkMon.Checked == true ? 1 : 0;
        objShift.STATUS[2] = chkTue.Checked == true ? 1 : 0;
        objShift.STATUS[3] = chkWed.Checked == true ? 1 : 0;
        objShift.STATUS[4] = chkThu.Checked == true ? 1 : 0;
        objShift.STATUS[5] = chkFri.Checked == true ? 1 : 0;
        objShift.STATUS[6] = chkSat.Checked == true ? 1 : 0;
        objShift.INTIME[0] = txtSunIn.Text.Trim();
        objShift.INTIME[1] = txtMonIn.Text.Trim();
        objShift.INTIME[2] = txtTueIn.Text.Trim();
        objShift.INTIME[3] = txtWedIn.Text.Trim();
        objShift.INTIME[4] = txtThuIn.Text.Trim();
        objShift.INTIME[5] = txtFriIn.Text.Trim();
        objShift.INTIME[6] = txtSatIn.Text.Trim();
        objShift.OUTTIME[0] = txtSunOut.Text.Trim();
        objShift.OUTTIME[1] = txtMonOut.Text.Trim();
        objShift.OUTTIME[2] = txtTueOut.Text.Trim();
        objShift.OUTTIME[3] = txtWedOut.Text.Trim();
        objShift.OUTTIME[4] = txtThuOut.Text.Trim();
        objShift.OUTTIME[5] = txtFriOut.Text.Trim();
        objShift.OUTTIME[6] = txtSatOut.Text.Trim();
        
       // objAttandance.ShiftInsUpdate(objShift, shiftNo, 0);//3RD is Passed as 0 For Update
        */

    }

    private void UpdateClear()
    {
        ViewState["action"] = "add";
        ViewState["SHIFTNO"] = null;

        //pnlSft.Visible = true; ;
        //pnlShift.Visible = true;
        //pnlLvShift.Visible = false;
        //Clear();

        chkSun.Checked = chkMon.Checked = chkTue.Checked = chkWed.Checked = chkThu.Checked = chkFri.Checked = chkSat.Checked = false;
        ddlCollege.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        txtShift.Text = "";

        txtShift.Text = "";
        txtSunIn.Text = "";
        txtSunOut.Text = "";
        txtMonIn.Text = "";
        txtMonOut.Text = "";
        txtTueIn.Text = "";
        txtTueOut.Text = "";
        txtWedIn.Text = "";
        txtWedOut.Text = "";
        txtThuIn.Text = "";
        txtThuOut.Text = "";
        txtFriIn.Text = "";
        txtFriOut.Text = "";
        txtSatIn.Text = "";
        txtSatOut.Text = "";

        txtSunHours.Text = txtMonHours.Text = txtTueHours.Text = txtWedHours.Text = txtthuHours.Text = txtFriHours.Text = txtSatHours.Text = "";
        btnAdd.Visible = true;
        btnModifySft.Visible = true;
        butBack.Visible = true;

        pnlLvShift.Visible = true;
        pnlSft.Visible = false;
        pnlShift.Visible = false;

    }

    protected void txtSunOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtMonIn.Text.ToString() != string.Empty && txtMonIn.Text.ToString() != "__:__:__ PM" && txtMonIn.Text.ToString() != "__:__:__ AM" && txtMonOut.Text.ToString() != string.Empty && txtMonOut.Text.ToString() != "__:__:__ PM" && txtMonOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtMonIn.Text.ToString();
                out_time = txtMonOut.Text.ToString();                
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtMonHours.Text = workinghours;
                txtTueIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }

    }


    protected void txtMonOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtMonIn.Text.ToString() != string.Empty && txtMonIn.Text.ToString() != "__:__:__ PM" && txtMonIn.Text.ToString() != "__:__:__ AM" && txtMonOut.Text.ToString() != string.Empty && txtMonOut.Text.ToString() != "__:__:__ PM" && txtMonOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtMonIn.Text.ToString();
                out_time = txtMonOut.Text.ToString();
                //workinghours = objCommon.LookUp("payroll_leave_ref", "FORMAT(CAST(DATEADD(MINUTE, datediff(MINUTE,cast('" + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME),'hh:mm:ss tt')", "");

                //workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S, ' + in_time + "' as datetime) , cast('" + out_time + "' as datetime)) /2.00 , cast('" + in_time + "'  as datetime)) AS dateTIME),'hh:mm:ss tt')", "");
                //workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S, '09:00:00', '16:00:00'), '1900-1-1'), 8)", "");
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'"+",'"+  out_time+"'), '1900-1-1'), 8)", "");
                txtMonHours.Text = workinghours;
                txtTueIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void txtTueOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtTueIn.Text.ToString() != string.Empty && txtTueIn.Text.ToString() != "__:__:__ PM" && txtTueIn.Text.ToString() != "__:__:__ AM" && txtTueOut.Text.ToString() != string.Empty && txtTueOut.Text.ToString() != "__:__:__ PM" && txtTueOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtTueIn.Text.ToString();
                out_time = txtTueOut.Text.ToString();                
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtTueHours.Text = workinghours;
                txtWedIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }


    protected void txtWedOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtWedIn.Text.ToString() != string.Empty && txtWedIn.Text.ToString() != "__:__:__ PM" && txtWedIn.Text.ToString() != "__:__:__ AM" && txtWedOut.Text.ToString() != string.Empty && txtWedOut.Text.ToString() != "__:__:__ PM" && txtWedOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtWedIn.Text.ToString();
                out_time = txtWedOut.Text.ToString();
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtWedHours.Text = workinghours;
                txtThuIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void txtThuOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtThuIn.Text.ToString() != string.Empty && txtThuIn.Text.ToString() != "__:__:__ PM" && txtThuIn.Text.ToString() != "__:__:__ AM" && txtThuOut.Text.ToString() != string.Empty && txtThuOut.Text.ToString() != "__:__:__ PM" && txtThuOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtThuIn.Text.ToString();
                out_time = txtThuOut.Text.ToString();
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtthuHours.Text = workinghours;
                txtFriIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void txtFriOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtFriIn.Text.ToString() != string.Empty && txtFriIn.Text.ToString() != "__:__:__ PM" && txtFriIn.Text.ToString() != "__:__:__ AM" && txtFriOut.Text.ToString() != string.Empty && txtFriOut.Text.ToString() != "__:__:__ PM" && txtFriOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtFriIn.Text.ToString();
                out_time = txtFriOut.Text.ToString();
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtFriHours.Text = workinghours;
                txtSatIn.Focus();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void txtSatOut_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string in_time; string out_time; string workinghours;
            if (txtSatIn.Text.ToString() != string.Empty && txtSatIn.Text.ToString() != "__:__:__ PM" && txtSatIn.Text.ToString() != "__:__:__ AM" && txtSatOut.Text.ToString() != string.Empty && txtSatOut.Text.ToString() != "__:__:__ PM" && txtSatOut.Text.ToString() != "__:__:__ AM")
            {
                in_time = txtSatIn.Text.ToString();
                out_time = txtSatOut.Text.ToString();
                workinghours = objCommon.LookUp("payroll_leave_ref", "CONVERT(VARCHAR(8), DATEADD(S, DATEDIFF(S,'" + in_time + "'" + ",'" + out_time + "'), '1900-1-1'), 8)", "");
                txtSatHours.Text = workinghours;
            }

        }
        catch (Exception ex)
        {

        }
    }
   
}
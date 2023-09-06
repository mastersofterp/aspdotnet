using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_Academic_FloorRoom_Master : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();

    // StudentController objSC = new StudentController();
    // ClubController OBJCLUB = new ClubController();
  #region Page Events
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

           // Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //CheckPageAuthorization();
                //Set the Page Title
                //Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
                //objCommon.FillDropDownList(ddlQualification, "ACD_QUALILEVEL", "QUALILEVELNO", "QUALILEVELNAME", "QUALILEVELNO>0", "QUALILEVELNO");
            }
            //populateDropDown();
            BindListView();
            BindListviewRoomIntake();
            populateDropDown();
            ViewState["action"] = "add";
            ViewState["actionRoom"] = "add";
        }
        divMsg.InnerHtml = string.Empty;
    }
    #endregion Page Events
    #region Floor Master
    #region Methods
    private void BindListView()
    {
        try
        {
            DataSet ds = objAttC.GetAllfloordata();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvfloor.DataSource = ds;
                lvfloor.DataBind();
                lvfloor.Visible = true;
                Pnlfloor.Visible = true;
            }
            else
            {
                lvfloor.DataSource = null;
                lvfloor.DataBind();
                Pnlfloor.Visible = false;
                //objCommon.DisplayMessage(this.updLoan, "No Record found for selected criteria.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

   
    protected void ClearField()
    {
        txtFloorName.Text = string.Empty;

        chkActive.Checked = true;
        ViewState["action"] = "add";
        // ViewState["ownid"] = null;


    }
    #endregion Methods
    #region Button Click Events
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int Floor_no = 0;
            int organizationid = 0;
            int Active = 0;
            //int collegecode = 0;
            string floorname = string.Empty;


            floorname = txtFloorName.Text;

            int CREATED_BY = 1;
            string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            organizationid = Convert.ToInt32(Session["OrgId"]);
            int collegecode = Convert.ToInt32(Session["colcode"]);
            if (chkActive.Checked)
            {
                Active = 1;
            }
            else
            {
                Active = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)objAttC.Addfloor(floorname, collegecode, Active, CREATED_BY, ipAddress, organizationid);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["action"] = "add";
                        ClearField();
                       // BindListView();
                        objCommon.DisplayMessage(this.upfloor, "Record Saved Successfully!", this.Page);
                        //BindListView();
                    }
                    else
                    {
                        ViewState["action"] = "add";
                        ClearField();
                       // BindListView();
                        objCommon.DisplayMessage(this.upfloor, "Record Already Exist !", this.Page);
                    }

                }
                else
                {
                    if (ViewState["Floor_no"] != null)
                    {
                        Floor_no = Convert.ToInt32(ViewState["Floor_no"].ToString());
                        CustomStatus cs = (CustomStatus)objAttC.Updatefloor(Floor_no, floorname, collegecode, Active, CREATED_BY, ipAddress, organizationid);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            ClearField();
                           // BindListView();
                            objCommon.DisplayMessage(this.upfloor, "Record Updated Successfully!", this.Page);
                             btnSave.Text="Submit";
                        }
                        else
                        {
                            ViewState["action"] = null;
                            ClearField();
                          // BindListView();
                            objCommon.DisplayMessage(this.upfloor, "Record Already Exist !", this.Page);
                        }

                    }
                }

            }

            BindListView();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AffiliatedFeesCategory.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearField();
        //Response.Redirect(Request.Url.ToString());
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Edit = sender as ImageButton;
        ViewState["Floor_no"] = Convert.ToInt32(Edit.CommandArgument);
        DataSet ds;
        //ds = objAttC.GetfloorbyNo(FLOORNO);
        ds = objCommon.FillDropDown("ACD_FLOOR_MASTER", "FLOORNO", "FLOORNAME,ACTIVESTATUS", "FLOORNO= " + Convert.ToInt32(ViewState["Floor_no"].ToString()), "FLOORNO");
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtFloorName.Text = ds.Tables[0].Rows[0]["FLOORNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["FLOORNAME"].ToString();

            int Active = Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVESTATUS"]);
            if (Active == 1)
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }
            ViewState["action"] = "edit";
            btnSave.Text = "Update";
        }
        else
        {
            ViewState["action"] = "add";
        }
    }
    #endregion Button Click Events
   #endregion Floor Master
    #region Room Master
    #region Button Click Events
    protected void btnSaveRoom_Click(object sender, EventArgs e)
{
        try
        {
            int room_no = 0;
            int organizationid = 0;
            int Activest = 0;
            int intake = 0;
            //int collegecode = 0;
            string roomname = string.Empty;

            int floor = Convert.ToInt32(ddlfloor.SelectedValue);

            roomname = txtRoomName.Text;
            //ddlfloor.SelectedValue=
            intake = Convert.ToInt32(txtintake.Text);
            //int CREATED_BY = 1;
            //string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            //organizationid = Convert.ToInt32(Session["OrgId"]);
            int collegecode = Convert.ToInt32(Session["colcode"]);
            if (chkroom.Checked)
            {
                Activest = 1;
            }
            else
            {
                Activest = 0;
            }
            if (ViewState["actionRoom"] != null)
            {

                if (ViewState["actionRoom"].ToString().Equals("add"))
                {

                    CustomStatus cs = (CustomStatus)objAttC.AddRoomintake(roomname, floor, intake, collegecode, Activest);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        ViewState["actionRoom"] = "add";
                        ClearFieldRoomInatke();
                        //BindListviewRoomIntake();
                        objCommon.DisplayMessage(this.updROOM, "Record Saved Successfully!", this.Page);
                       // BindListviewRoomIntake();
                    }
                    else
                    {
                        ViewState["actionRoom"] = "add";
                        ClearFieldRoomInatke();
                        //BindListviewRoomIntake();
                        objCommon.DisplayMessage(this.updROOM, "Record Already Exist !", this.Page);
                        //BindListviewRoomIntake();
                    }

                }
                else
                {
                    if (ViewState["room_no"] != null)
                    {
                        room_no = Convert.ToInt32(ViewState["room_no"].ToString());
                        CustomStatus cs = (CustomStatus)objAttC.UpdateRoomintake(room_no, roomname, floor, intake, collegecode, Activest);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["actionRoom"] = null;
                            ClearFieldRoomInatke();
                          // BindListviewRoomIntake();
                            objCommon.DisplayMessage(this.updROOM, "Record Updated Successfully!", this.Page);
                            btnSaveRoom.Text = "Submit";
                        }
                        else
                        {
                            ViewState["actionRoom"] = null;
                            ClearFieldRoomInatke();
                           // BindListviewRoomIntake();
                            objCommon.DisplayMessage(this.updROOM, "Record Already Exist !", this.Page);
                        }

                    }
                }

            }

        BindListviewRoomIntake();
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "AffiliatedFeesCategory.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btneditRoomIntake_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            ImageButton btnEdit = sender as ImageButton;
            int ROOMNO = int.Parse(btnEdit.CommandArgument);
            ViewState["room_no"] = Convert.ToInt32(btnEdit.CommandArgument);
            this.ShowDetails(ROOMNO);
            ViewState["edit"] = "edit";
            //Session["action"] = "edit";
            Pnlroom.Visible = true;
            //pnlList.Visible = false;
            //pnlAAPaList.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }
    protected void btnCancelRoom_Click(object sender, EventArgs e)
    {
        ClearFieldRoomInatke();
        //Response.Redirect(Request.Url.ToString());
    }
    #endregion Button Click Events
    #region Methods
    protected void ClearFieldRoomInatke()
    {
        txtRoomName.Text = string.Empty;
        ddlfloor.SelectedIndex = 0;
        txtintake.Text = string.Empty;
        chkActive.Checked = true;
        ViewState["actionRoom"] = "add";
       ViewState["room_no"] = null;


    }
    private void populateDropDown()
    {
        objCommon.FillDropDownList(ddlfloor, "ACD_FLOOR_MASTER", "FLOORNO", "FLOORNAME", "FLOORNO>0 AND ACTIVESTATUS=1", "FLOORNO");

    }
    private void BindListviewRoomIntake()
    {
        try
        {
            DataSet ds = objAttC.GetAllRoomIntakedata();
            if (ds.Tables[0].Rows.Count > 0)
            {

                lvroomintake.DataSource = ds;
                lvroomintake.DataBind();
                lvroomintake.Visible = true;
                Pnlroom.Visible = true;
            }
            else
            {
                lvroomintake.DataSource = null;
                lvroomintake.DataBind();
                Pnlroom.Visible = false;
                //objCommon.DisplayMessage(this.updLoan, "No Record found for selected criteria.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_LoanApplicableStudentList() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
   
    private void ShowDetails(Int32 room_no)
    {
        DataSet ds = null;
        try
        {
            ds = objAttC.GetRoomInatkebyNo(room_no);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["room_no"] = room_no;
                txtRoomName.Text = ds.Tables[0].Rows[0]["ROOMNAME"] == null ? string.Empty : ds.Tables[0].Rows[0]["ROOMNAME"].ToString();
                ddlfloor.SelectedValue = ds.Tables[0].Rows[0]["FLOORNO"].ToString();
                txtintake.Text = ds.Tables[0].Rows[0]["INTAKE"] == null ? string.Empty : ds.Tables[0].Rows[0]["INTAKE"].ToString();
                int Activest = Convert.ToInt32(ds.Tables[0].Rows[0]["ACTIVE_STATUS"]);
                if (Activest == 1)
                {
                    chkroom.Checked = true;
                }
                else
                {
                    chkroom.Checked = false;
                }
                ViewState["actionRoom"] = "edit";
                btnSaveRoom.Text = "Update";
            }
            else
            {
                ViewState["actionRoom"] = "add";
            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_AuthorityApprovalMaster.ShowDetails ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion Methods
    #endregion Room Master
}


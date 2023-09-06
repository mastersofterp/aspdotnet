using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
public partial class ACADEMIC_StudentAchievement_Create_Eventaspx : System.Web.UI.Page
{

    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    CreateEventController objCEC = new CreateEventController();
    CreateEventEntity objCEE = new CreateEventEntity();

    string blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
    string blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_CLUB"].ToString();

    int createeventid = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //createeventid = Convert.ToInt32(Request.QueryString["CREATE_EVENT_ID"]);
            //hdnCreateevent_id.Value = Request.QueryString["CREATE_EVENT_ID"];
            FillDropDown();
            BindListView();
            Prizes.Visible = false;

            //txtWinner.Visible = false;
            //txtRunnerUp.Visible = false;
            //txtThirdPlace.Visible = false; 
        }
    }

    public void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACHIEVEMENT_ACADMIC_YEAR", "DISTINCT ACADMIC_YEAR_ID", "ACADMIC_YEAR_NAME", "ACADMIC_YEAR_ID > 0 AND ACTIVE_STATUS=1", "ACADMIC_YEAR_ID DESC");
            objCommon.FillDropDownList(ddlEventCategory, "ACD_ACHIEVEMENT_EVENT_CATEGORY", "EVENT_CATEGORY_ID", "EVENT_CATEGORY_NAME", "EVENT_CATEGORY_ID > 0 AND ACTIVE_STATUS=1", "EVENT_CATEGORY_ID");
            objCommon.FillDropDownList(ddlEventLevel, "ACD_ACHIEVEMENT_EVENT_LEVEL", "EVENT_LEVEL_ID", "EVENT_LEVEL", "EVENT_LEVEL_ID >0 AND ACTIVE_STATUS=1", "EVENT_LEVEL_ID");
            objCommon.FillListBox(lstbxFacultyCoordinator, "User_Acc", "UA_NO", "UA_FULLNAME", "UA_TYPE=3", "UA_NO");//UA_STATUS
            objCommon.FillDropDownList(ddlDuration, "ACD_ACHIEVEMENT_DURATION", "DURATION_ID", "DURATION", "DURATION_ID > 0 AND ACTIVE_STATUS=1", "DURATION_ID");
            objCommon.FillDropDownList(ddlActivityType, "ACD_CLUB_ACTIVITY_MASTER", "ACTIVITYID", "ACTIVITY_NAME", "ACTIVITYID>0", "ACTIVITYID");
            objCommon.FillListBox(lstSelectClub, "ACD_CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO > 0 AND ACTIVESTATUS=1", "CLUB_ACTIVITY_NO ASC"); //Added by Yogesh K Dt:-10/11/2022
            //objCommon.FillListBox(lstSelectClub, "CLUB_MASTER", "CLUB_ACTIVITY_NO", "CLUB_ACTIVITY_TYPE", "CLUB_ACTIVITY_NO> 0", "CLUB_ACTIVITY_NO ASC");
            ////objCommon.FillListBox(lstHouses, "ACD_HOUSE_ALLOTMENT", "HOUSE_ID", "HOUSE_NAME", "HOUSE_ID > 0 AND ACTIVE_STATUS=1", "HOUSE_ID");

            objCommon.FillDropDownList(ddlCollege, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID<>0", "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Selection College, Degree, Branch
    private void calljavascriptFunctions()
    {
        string StartEndDate = hdnDate.Value;
        string[] dates = new string[] { };
        if (StartEndDate == "")
        {
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }

        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];
        DateTime dtStartDate = DateTime.Parse(StartDate);
        DateTime dtEndDate = DateTime.Parse(EndDate);

        hdnDate.Value = dtStartDate.ToString("MMM dd, yyyy") + " - " + dtEndDate.ToString("MMM dd, yyyy");


        if (hfdActive.Value == "")
        {
            hfdActive.Value = "true";
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');SetRegistrationDate('" + hdnRegDate.Value + "');SetCreateEvent(" + hfdActive.Value + ");", true);
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstDegree.Items.Clear();
            lstBranch.Items.Clear();
            lstHouses.Items.Clear();
            if (ddlCollege.SelectedIndex > 0)
            {
                BindDegree();
            }

            calljavascriptFunctions();

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.ddlCollege_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindDegree()
    {
        try
        {
            objCommon.FillListBox(lstDegree, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "COLLEGE_ID=" + ddlCollege.SelectedValue, "DEGREENO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.BindDegree() --> " + ex.Message + " " + ex.StackTrace);
        }

    }
    protected void lstDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstBranch.Items.Clear();
            lstHouses.Items.Clear();

            if (lstDegree.Items.Count > 0)
            {
                BindBranch();
            }
            calljavascriptFunctions();
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.lstDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindBranch()
    {
        try
        {
            string degree = "";
            foreach (ListItem lstItm in lstDegree.Items)
            {
                if (lstItm.Selected)
                {
                    degree += lstItm.Value + ",";
                }
            }
            degree = degree.Substring(0, (degree.Length - 1));

            objCommon.FillListBox(lstBranch, "VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "LONGNAME", "DEGREENO IN(" + degree + ")", "BRANCHNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.BindBranch() --> " + ex.Message + " " + ex.StackTrace);
        }

    }
    protected void lstBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lstHouses.Items.Clear();
            if (lstBranch.Items.Count > 0)
            {
                BindHouses();
            }

            calljavascriptFunctions();

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.lstBranch_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindHouses()
    {
        try
        {
            string branch = "";
            foreach (ListItem lstItm in lstBranch.Items)
            {
                if (lstItm.Selected)
                {
                    branch += lstItm.Value + ",";
                }
            }
            branch = branch.Substring(0, (branch.Length - 1));

            objCommon.FillListBox(lstHouses, "ACD_HOUSE_ALLOTMENT", "HOUSE_ID", "HOUSE_NAME", "HOUSE_ID IN (SELECT DISTINCT HOUSE_ID FROM  ACD_STUDENT WHERE BRANCHNO IN(" + branch + ") AND HOUSE_ID >0) AND HOUSE_ID > 0 AND ACTIVE_STATUS=1", "HOUSE_ID");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Create_Event.BindHouses() --> " + ex.Message + " " + ex.StackTrace);
        }

    }
    #endregion


    public byte[] ResizePhoto(FileUpload fu)
    {
        byte[] image = null;
        if (fu.PostedFile != null && fu.PostedFile.FileName != "")
        {
            string strExtension = System.IO.Path.GetExtension(fu.FileName);
            // Resize Image Before Uploading to DataBase
            System.Drawing.Image imageToBeResized = System.Drawing.Image.FromStream(fu.PostedFile.InputStream);
            int imageHeight = imageToBeResized.Height;
            int imageWidth = imageToBeResized.Width;
            int maxHeight = 240;
            int maxWidth = 320;
            imageHeight = (imageHeight * maxWidth) / imageWidth;
            imageWidth = maxWidth;

            if (imageHeight > maxHeight)
            {
                imageWidth = (imageWidth * maxHeight) / imageHeight;
                imageHeight = maxHeight;
            }

            // Saving image to smaller size and converting in byte[]
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            image = new byte[stream.Length + 1];
            stream.Read(image, 0, image.Length);
        }
        return image;
    }


    protected void BindListView()
    {
        try
        {
            DataSet ds = objCEC.CreateEventListView();

            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlEvent.Visible = true;
                lvCreateEvent.DataSource = ds.Tables[0];
                lvCreateEvent.DataBind();
            }
            else
            {
                pnlEvent.Visible = true;
                lvCreateEvent.DataSource = null;
                lvCreateEvent.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    private void ShowDetail(int CREATE_EVENT_ID)
    {
        DataSet ds = objCEC.EditCreateEventData(CREATE_EVENT_ID);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["CREATE_EVENT_ID"] = int.Parse(ds.Tables[0].Rows[0]["CREATE_EVENT_ID"].ToString());
            int NewsID = Convert.ToInt32(ViewState["CREATE_EVENT_ID"].ToString());

            lblfile.Text = ds.Tables[0].Rows[0]["FILE_NAME"].ToString();

            txtEventTitle.Text = ds.Tables[0].Rows[0]["EVENT_TITLE"].ToString();
            ddlAcademicYear.SelectedValue = ds.Tables[0].Rows[0]["ACADMIC_YEAR_ID"].ToString();
            ddlEventCategory.SelectedValue = ds.Tables[0].Rows[0]["EVENT_CATEGORY_ID"].ToString();
            txtEventTitle.Text = ds.Tables[0].Rows[0]["EVENT_TITLE"].ToString();
            txtOrganizeBy.Text = ds.Tables[0].Rows[0]["ORGANIZE_BY"].ToString();
            txtConductBy.Text = ds.Tables[0].Rows[0]["CONDUCT_BY"].ToString();
            ddlEventLevel.SelectedValue = ds.Tables[0].Rows[0]["EVENT_LEVEL_ID"].ToString();
            txtVenue.Text = ds.Tables[0].Rows[0]["VENUE"].ToString();
            ddlMode.SelectedValue = ds.Tables[0].Rows[0]["EVENT_MODE"].ToString();
            ddlDuration.SelectedValue = ds.Tables[0].Rows[0]["DURATION_ID"].ToString();

            txtWinner.Text = ds.Tables[0].Rows[0]["WINNER"] == DBNull.Value ? "" : Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0]["WINNER"].ToString())).ToString();
            txtRunnerUp.Text = ds.Tables[0].Rows[0]["RUNNER_UP"].ToString();
            txtThirdPlace.Text = ds.Tables[0].Rows[0]["THIRD_PLACE"].ToString();

            if (ds.Tables[0].Rows[0]["PRIZES"] != DBNull.Value)
            {
                chkPrizes.Checked = (bool)ds.Tables[0].Rows[0]["PRIZES"];

                if (chkPrizes.Checked)
                {
                    Prizes.Visible = true;
                    txtWinner.Enabled = true;
                    txtRunnerUp.Enabled = true;
                    txtThirdPlace.Enabled = true;

                }
                else
                {
                    Prizes.Visible = false;
                }
            }

            txtFundedBy.Text = ds.Tables[0].Rows[0]["FUNDED_BY"].ToString();

            //txtActivitytime.Text = ds.Tables[0].Rows[0]["ACTIVITY_TIME"].ToString(); // Added by Nikhil S Dt:-07/09/2022

            txtActivitytime.Text = ds.Tables[0].Rows[0]["ACTIVITY_TIME"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ACTIVITY_TIME"].ToString()).ToString("hh:mm tt");

            ddlActivityType.SelectedValue = ds.Tables[0].Rows[0]["ACTIVITY_TYPE"].ToString(); // Added by Nikhil S Dt:-07/09/2022

            txtRegistrationCapacity.Text = ds.Tables[0].Rows[0]["REGISTRATION_CAPACITY"] == DBNull.Value ? string.Empty : ds.Tables[0].Rows[0]["REGISTRATION_CAPACITY"].ToString();

            string[] fac = ds.Tables[0].Rows[0]["FACULTY_CORDINATOR"].ToString().Split(',');
            string converner = string.Empty;
            foreach (ListItem items in lstbxFacultyCoordinator.Items)
            {
                foreach (string n in fac)
                {
                    if (items.Value == n)
                    {
                        items.Selected = true;
                    }
                }
            }
            txtStartDate.Text = ds.Tables[0].Rows[0]["STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("dd/MM/yyyy");

            txtEndDate.Text = ds.Tables[0].Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("dd/MM/yyyy");

            txtRegistrationDate.Text = ds.Tables[0].Rows[0]["Registration_Date"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["Registration_Date"].ToString()).ToString("dd/MM/yyyy");

            hdnDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["STDATE"].ToString()).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("MMM dd, yyyy");
            ////ScriptManager.RegisterStartupScript(this, GetType(), "function", "Setdate('" + hdnDate.Value + "');", true);

            hdnRegDate.Value = Convert.ToDateTime(ds.Tables[0].Rows[0]["Registration_Date"].ToString()).ToString("MMM dd, yyyy");

            ////ScriptManager.RegisterStartupScript(this, GetType(), "function", "SetRegistrationDate('" + hdnRegDate.Value + "');", true);

            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "True")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');SetRegistrationDate('" + hdnRegDate.Value + "');SetCreateEvent(true);", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Setdate('" + hdnDate.Value + "');SetRegistrationDate('" + hdnRegDate.Value + "');SetCreateEvent(false);", true);
            }


            string[] ClubType = ds.Tables[0].Rows[0]["CLUB_TYPE"].ToString().Split(',');   // Added By Nikhil S Dt:-07/09/2022
            foreach (ListItem items in lstSelectClub.Items)
            {
                foreach (string s in ClubType)
                {
                    if (items.Value.Equals(s))
                    {
                        items.Selected = true;
                    }
                }
            }

            //COLLEGE_NO	DEGREENO	BRANCHNO
            ddlCollege.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString();
            BindDegree();
            if (ds.Tables[0].Rows[0]["DEGREENO"] != null)
            {
                string[] DEGREENO = ds.Tables[0].Rows[0]["DEGREENO"].ToString().Split(',');   // Added By Nikhil S Dt:-07/09/2022
                foreach (ListItem items in lstDegree.Items)
                {
                    foreach (string m in DEGREENO)
                    {
                        if (items.Value.Equals(m))
                        {
                            items.Selected = true;
                        }
                    }
                }
            }

            BindBranch();
            if (ds.Tables[0].Rows[0]["BRANCHNO"] != null)
            {
                string[] BRANCHNO = ds.Tables[0].Rows[0]["BRANCHNO"].ToString().Split(',');   // Added By Nikhil S Dt:-07/09/2022
                foreach (ListItem items in lstBranch.Items)
                {
                    foreach (string m in BRANCHNO)
                    {
                        if (items.Value.Equals(m))
                        {
                            items.Selected = true;
                        }
                    }
                }
            }

            BindHouses();
            if (ds.Tables[0].Rows[0]["HOUSES"] != null)
            {
                string[] Houses = ds.Tables[0].Rows[0]["HOUSES"].ToString().Split(',');   // Added By Nikhil S Dt:-07/09/2022
                foreach (ListItem items in lstHouses.Items)
                {
                    foreach (string m in Houses)
                    {
                        if (items.Value.Equals(m))
                        {
                            items.Selected = true;
                        }
                    }
                }
            }
        }
    }


    protected void btnEditCreateEvent_Click(object sender, System.EventArgs e)
    {
        try
        {
            //ImageButton btnEditCreateEvent = sender as ImageButton;
            LinkButton btnEditCreateEvent = sender as LinkButton;
            int CREATE_EVENT_ID = Convert.ToInt32(btnEditCreateEvent.CommandArgument);
            ViewState["ceid"] = Convert.ToInt32(btnEditCreateEvent.CommandArgument);
            ShowDetail(CREATE_EVENT_ID);
            ViewState["action"] = "edit";

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void lvCreateEvent_ItemEditing(object sender, ListViewEditEventArgs e)
    {

    }
    protected void btnCancelAcademicYear_Click(object sender, System.EventArgs e)
    {
        ClearCreateEventData();
    }
    public void ClearCreateEventData()
    {
        ViewState["action"] = null;
        ddlAcademicYear.SelectedIndex = 0;
        ddlEventCategory.SelectedIndex = 0;
        txtEventTitle.Text = "";
        txtOrganizeBy.Text = "";
        txtConductBy.Text = "";
        ddlEventLevel.SelectedIndex = 0;
        txtVenue.Text = "";
        ddlMode.SelectedIndex = 0;
        ddlDuration.SelectedIndex = 0;
        txtWinner.Text = "";
        txtRunnerUp.Text = "";
        txtThirdPlace.Text = "";
        txtFundedBy.Text = "";
        txtActivitytime.Text = "";
        chkPrizes.Checked = false;
        Prizes.Visible = false;
        txtRegistrationDate.Text = "";
        txtRegistrationCapacity.Text = "";
        ddlActivityType.SelectedIndex = 0; //Added By Nikhil S dt:-07/09/2022
        ViewState["CREATE_EVENT_ID"] = null;
        lblfile.Text = string.Empty;

        foreach (ListItem items in lstbxFacultyCoordinator.Items)
        {
            if (items.Selected == true)
            {
                items.Selected = false;

            }
        }

        foreach (ListItem items in lstSelectClub.Items)  // Added by Nikhil S dt:-07/09/2022
        {
            if (items.Selected == true)
            {
                items.Selected = false;

            }
        }

        ddlCollege.SelectedIndex = 0;
        lstDegree.Items.Clear();
        lstBranch.Items.Clear();
        lstHouses.Items.Clear();

        //foreach (ListItem items in lstHouses.Items)  // Added by Nikhil S dt:-07/09/2022
        //{
        //    if (items.Selected == true)
        //    {
        //        items.Selected = false;

        //    }
        //}


    }


    protected void chkPrizes_CheckedChanged(object sender, System.EventArgs e)
    {
        objCEE.winner = 0;
        if (chkPrizes.Checked)
        {
            Prizes.Visible = true;
            //txtWinner.Visible = true;
            //txtRunnerUp.Visible = true;
            //txtThirdPlace.Visible = true;


            txtWinner.Enabled = true;
            txtRunnerUp.Enabled = true;
            txtThirdPlace.Enabled = true;

            if (!String.IsNullOrEmpty(txtWinner.Text))
            {
                objCEE.winner = Convert.ToDecimal(txtWinner.Text);
                objCEE.runner_up = Convert.ToDecimal(txtRunnerUp.Text);
                objCEE.third_place = Convert.ToDecimal(txtThirdPlace.Text);
            }
        }
        else
        {
            Prizes.Visible = false;

            //txtWinner.Visible = false;
            //txtRunnerUp.Visible = false;
            //txtThirdPlace.Visible = false;

            //txtWinner.Enabled = false;
            //txtRunnerUp.Enabled = false;
            //txtThirdPlace.Enabled = false;

            //if (!String.IsNullOrEmpty(txtWinner.Text))
            //{
            //    objCEE.winner = Convert.ToInt32(txtWinner.Text);
            //    objCEE.runner_up = Convert.ToInt32(txtRunnerUp.Text);
            //    objCEE.third_place = Convert.ToInt32(txtThirdPlace.Text);
            //}
        }

    }
    protected void btnSubmitCreateEvent_Click(object sender, System.EventArgs e)
    {
        //int _ua_no = 0;
        //int ck = 0;
        objCEE.acadamic_year_id = Convert.ToInt32(ddlAcademicYear.SelectedValue);
        objCEE.event_category_id = Convert.ToInt32(ddlEventCategory.SelectedValue);
        objCEE.event_titlte = txtEventTitle.Text.Trim();
        objCEE.organize_by = txtOrganizeBy.Text.Trim();
        objCEE.conduct_by = txtConductBy.Text.Trim();
        objCEE.event_level_id = Convert.ToInt32(ddlEventLevel.SelectedValue);
        objCEE.Activity_id = Convert.ToInt32(ddlActivityType.SelectedValue);  // Added By Nikhil S dt:-07/09/2022

        string StartEndDate = hdnDate.Value;
        string StrDate1 = txtStartDate.Text;

        string RegistarionDate = txtRegistrationDate.Text;

        string[] dates = new string[] { };
        string[] Rdates = new string[] { };


        if ((RegistarionDate) == "")
        {
            objCommon.DisplayMessage(this, "Please select Registration Date !", this.Page);
            return;
        }
        else
        {
            RegistarionDate = RegistarionDate.Substring(0, RegistarionDate.Length - 0);
            //Rdates = RegistarionDate.Split('-');
        }

        if ((StartEndDate) == "")//GetDocs()
        {
            objCommon.DisplayMessage(this, "Please select Start Date End Date !", this.Page);
            return;
        }
        else
        {
            StartEndDate = StartEndDate.Substring(0, StartEndDate.Length - 0);
            //string[]
            dates = StartEndDate.Split('-');
        }

      



        string StartDate = dates[0];//Jul 15, 2021
        string EndDate = dates[1];

        DateTime dtStartDate = DateTime.Parse(StartDate);
        objCEE.SDate = DateTime.Parse(StartDate);

        DateTime dtEndDate = DateTime.Parse(EndDate);
        objCEE.EDate = DateTime.Parse(EndDate);

        DateTime dtRegDate = DateTime.Parse(RegistarionDate);
        objCEE.RegDate = DateTime.Parse(txtRegistrationDate.Text);

        if (dtRegDate > dtStartDate)
        {
            objCommon.DisplayMessage(this, "Registration Date Should not Greater Than Start date !", this.Page);
            return;
        }

        objCEE.venue = txtVenue.Text.Trim();
        objCEE.event_mode = ddlMode.SelectedValue;
        objCEE.duration_id = Convert.ToInt32(ddlDuration.SelectedValue);

        objCEE.winner = string.IsNullOrEmpty(txtWinner.Text.Trim()) ? 0 : Convert.ToDecimal(txtWinner.Text);
        objCEE.runner_up = string.IsNullOrEmpty(txtRunnerUp.Text.Trim()) ? 0 : Convert.ToDecimal(txtRunnerUp.Text);
        objCEE.third_place = string.IsNullOrEmpty(txtThirdPlace.Text.Trim()) ? 0 : Convert.ToDecimal(txtThirdPlace.Text);

        objCEE.funded_by = txtFundedBy.Text.Trim();
        objCEE.prizes = chkPrizes.Checked;
        objCEE.Time = txtActivitytime.Text;

        objCEE.RegCapacity = Convert.ToInt32(txtRegistrationCapacity.Text);



        if (chkPrizes.Checked)
        {
            objCEE.prizes = true;
        }
        else
        {
            objCEE.prizes = false;
        }

        if (hfdActive.Value == "true")
        {
            objCEE.IsActive = true;
        }
        else
        {
            objCEE.IsActive = false;
        }
        objCEE.OrganizationId = Convert.ToInt32(Session["OrgId"]);

        string _ua_no = string.Empty;
        foreach (ListItem items in lstbxFacultyCoordinator.Items)
        {
            if (items.Selected == true)
            {

                _ua_no += items.Value + ",";

            }
        }
        _ua_no = _ua_no.Remove(_ua_no.Length - 1);

        string _ActivityClub = string.Empty;           // Added By Nikhil S dt:-07/09/2022
        foreach (ListItem items in lstSelectClub.Items)
        {
            if (items.Selected == true)
            {

                _ActivityClub += items.Value + ",";

            }
        }
        _ActivityClub = _ActivityClub.Remove(_ActivityClub.Length - 1);

        #region
        //code added by Arjun P dt:01-12-2022 for College,Degree,Branch,House
        int _ActivityCollege = Convert.ToInt32(ddlCollege.SelectedValue);

        string _ActivityDegree = string.Empty;
        foreach (ListItem items in lstDegree.Items)
        {
            if (items.Selected == true)
            {

                _ActivityDegree += items.Value + ",";

            }
        }
        _ActivityDegree = _ActivityDegree.Remove(_ActivityDegree.Length - 1);

        string _ActivityBranch = string.Empty;
        foreach (ListItem items in lstBranch.Items)
        {
            if (items.Selected == true)
            {

                _ActivityBranch += items.Value + ",";

            }
        }
        _ActivityBranch = _ActivityBranch.Remove(_ActivityBranch.Length - 1);

        string _ActivityHouses = string.Empty;
        foreach (ListItem items in lstHouses.Items)
        {
            if (items.Selected == true)
            {

                _ActivityHouses += items.Value + ",";

            }
        }
        if (_ActivityHouses.Length > 1)
        {
            _ActivityHouses = _ActivityHouses.Remove(_ActivityHouses.Length - 1);
        }
        #endregion




        //if (!fuEventfile.HasFile)
        //{         
        //    ChallanCopy = objCommon.GetImageData(fuEventfile);
        //    string Ext = Path.GetExtension(fuEventfile.FileName);
        //    int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["userno"]) + "_ERP_" + "PDP" + "_0_" + "EventFilename", fuEventfile, ChallanCopy);
        //    if (retval == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
        //        return;
        //    }
        //    objCEE.file_name = Convert.ToInt32(Session["userno"]) + "_ERP_" + "PDP" + "_0_" + "EventFilename" + Ext;
        //}
        // else
        //{
        //    objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
        //    return;
        //}
        //objCEE.create_event_id = Convert.ToInt32(hdnCreateevent_id.Value);
        //DataSet ds = objCommon.FillDropDown("ACD_ACHIEVEMENT_CREATE_EVENT", "Count(*) as count", "CREATE_EVENT_ID=" + hdnCreateevent_id.Value + "", "", "");
        //int count = Convert.ToInt32(ds.Tables[0].Rows[0]["count"]);
        string filetext = string.Empty;
        if (ViewState["CREATE_EVENT_ID"] == null)
        {

            int newid = Convert.ToInt32(objCommon.LookUp("ACD_ACHIEVEMENT_CREATE_EVENT", "ISNULL(MAX(CREATE_EVENT_ID),0)", ""));
            ViewState["CREATE_EVENT_ID"] = newid + 1;
        }
        byte[] imgData;
        if (fuEventfile.HasFile)
        {
            if (!fuEventfile.PostedFile.ContentLength.Equals(string.Empty) || fuEventfile.PostedFile.ContentLength != null)
            {
                int fileSize = fuEventfile.PostedFile.ContentLength;

                int KB = fileSize / 1024;
                if (KB >= 500)
                {
                    objCommon.DisplayMessage(this.Page, "Uploaded File size should be less than or equal to 500 kb.", this.Page);

                    return;
                }

                string ext = System.IO.Path.GetExtension(fuEventfile.FileName).ToLower();
                if (ext == ".pdf" || ext == ".jpeg" || ext == ".jpg")
                {

                }
                else
                {
                    objCommon.DisplayMessage("Please Upload PDF file only", this.Page);
                    return;
                }

                if (fuEventfile.FileName.ToString().Length > 50)
                {
                    objCommon.DisplayMessage("Upload File Name is too long", this.Page);
                    return;
                }


                imgData = objCommon.GetImageData(fuEventfile);
                filetext = imgData.ToString();
                string filename_Certificate = Path.GetFileName(fuEventfile.PostedFile.FileName);
                //string Ext = Path.GetExtension(fuFile.FileName);

                filetext = (ViewState["CREATE_EVENT_ID"]) + "_Club_Event_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ext;
                int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, (ViewState["CREATE_EVENT_ID"]) + "_Club_Event_" + DateTime.Now.ToString("yyyyMMddHHmmss"), fuEventfile, imgData);
                if (retval == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                    return;
                }

            }
        }
        else if (!fuEventfile.HasFile && lblfile.Text != string.Empty)
        {
            filetext = lblfile.Text;
        }


        objCEE.file_name = filetext;

        //int count = 0;


        //   string filename_EventFile = Path.GetFileName(fuEventfile.PostedFile.FileName);
        //   byte[] ChallanCopy = null;

        //   if (!fuEventfile.HasFile)
        //   {

        //       ChallanCopy = objCommon.GetImageData(fuEventfile);

        //       string fileDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        //       string filename = idno + "_doc_" + docno + "_" + fileDateTime + ext;

        //       //string Ext = Path.GetExtension(fuEventfile.FileName);
        //       int retval = Blob_UploadDepositSlip(blob_ConStr, blob_ContainerName, Convert.ToInt32(Session["userno"]) + "_" + count + "_" + "EventBrochure_File", fuEventfile, ChallanCopy);

        //       if (retval == 0)
        //       {
        //           ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
        //           Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DisplayTab('8')", true);
        //           return;
        //       }
        //       //objET.CLAIM_FORM_PATH = Convert.ToInt32(Session["userno"]) + "_ERP_" + "PDP" + "_" + count + "_" + "TeacherPayment_Slip" + Ext;
        //       objCEE.file_name = Convert.ToInt32(Session["userno"]) + "_" + count + "_" + "EventBrochure_File" + Ext;

        //   }
        //else
        //{
        //    objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
        //    return;
        //}

        //objCEE.file_name = FileUpload1.FileName.ToString();
        //string filename = FileUpload1.FileName.ToString();
        //objCEE.file_name = FileUpload1.FileName.ToString();

        //string fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
        //if (FileUpload1.HasFile)
        //{
        //    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ACADEMIC/FileUpload/" + FileUpload1.FileName));
        //    string path = Server.MapPath("~/ACADEMIC/FileUpload/");
        //    decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
        //    Double FileSize = FileUpload1.PostedFile.ContentLength;
        //    string ext = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
        //    if (ext.ToUpper().Trim() == ".PDF")
        //    {
        //        byte[] ImagePhotoByte;
        //        double PhotoFileSize = FileUpload1.PostedFile.ContentLength;
        //        if (PhotoFileSize > 1000000)
        //        {
        //            byte[] resizephoto = ResizePhoto(FileUpload1);
        //            Response.Write("<script>alert('File Size Must Not Exceed 1 MB')</script>");
        //            return;
        //        }
        //        else
        //        {
        //            using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
        //            {
        //                ImagePhotoByte = FileUpload1.FileBytes;
        //                //lblFilename.Text = filename;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        objCommon.DisplayMessage(this, "Only PDF files are allowed!", this.Page);
        //        return;
        //    }

        if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
        {

            objCEE.create_event_id = Convert.ToInt32(ViewState["ceid"]);
            CustomStatus cs = (CustomStatus)objCEC.UpdateCreateEventData(objCEE, _ActivityClub, _ActivityCollege, _ActivityDegree, _ActivityBranch, _ActivityHouses, _ua_no);
            //Check for add or edit
            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                BindListView();
                objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                ClearCreateEventData();
            }
        }

        else
        {
            CustomStatus cs = (CustomStatus)objCEC.InsertCreateEventData(objCEE, _ActivityClub, _ActivityCollege, _ActivityDegree, _ActivityBranch, _ActivityHouses, _ua_no);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                BindListView();
                objCommon.DisplayMessage(this, "Record Saved Successfully..", this.Page);
                ClearCreateEventData();

            }
        }

    }
    protected void btnDownload_Click(object sender, System.EventArgs e)
    {

        try
        {
            int newid = Convert.ToInt32(objCommon.LookUp("ACD_ACHIEVEMENT_CREATE_EVENT", "ISNULL(MAX(CREATE_EVENT_ID),0)", ""));
            ViewState["CREATE_EVENT_ID"] = newid + 1;
            Button btnDownload = sender as Button;
            //LinkButton lnkDownload = sender as LinkButton;
            string FileName = btnDownload.CommandArgument.ToString();
            string Url = string.Empty;
            string directoryPath = string.Empty;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            string directoryName = "~/DownloadImg" + "/";
            directoryPath = Server.MapPath(directoryName);

            if (!Directory.Exists(directoryPath.ToString()))
            {

                Directory.CreateDirectory(directoryPath.ToString());
            }
            CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
            string img = btnDownload.CommandArgument.ToString();
            var ImageName = img;
            if (img != null || img != "")
            {

                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, img);

                var Newblob = blobContainer.GetBlockBlobReference(ImageName);

                string filePath = directoryPath + "\\" + ImageName;


                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);

                Response.Clear();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
           else
           {
            objCommon.DisplayUserMessage(this, "Requested file is not available to download", this.Page);
            return;
            } 
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(this, "Requested file is not available to download", this.Page);
            return;
        }

    }



        //Button btnDownload = sender as Button;
        //string filetemp = (sender as Button).CommandArgument;
        ////string FILE_NAME = ((System.Web.UI.WebControls.Button)(sender)).CommandArgument.ToString();
        //string FILE_NAME = btnDownload.ToolTip;
        //string filePath = Server.MapPath("~/ACADEMIC/FileUpload/" + FILE_NAME);
        //FileInfo file = new FileInfo(filePath);

        //if (file.Exists)
        //{
        //    Response.Clear();
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
        //    Response.AddHeader("Content-Length", file.Length.ToString());
        //    Response.ContentType = "application/octet-stream";
        //    Response.Flush();
        //    Response.TransmitFile(file.FullName);
        //    Response.End();
        //}
        //else
        //{
        //    objCommon.DisplayUserMessage(this, "Requested file is not available to download", this.Page);
        //    return;
        //}
    

    public void fileDownload(string FILE_NAME, string filepath)
    {
        ResponseFile(Page.Request, Page.Response, FILE_NAME, filepath, 1024000);
    }

    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _FILE_NAME, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + _FILE_NAME);

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }


    protected void btnReport_Click(object sender, System.EventArgs e)
    {
        DataGrid Gr = new DataGrid();
        DataSet ds = new DataSet();
        ds = objCEC.CreateEventReport();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Gr.DataSource = ds;
                Gr.DataBind();
                string Attachment = "Attachment; FileName=CreateEventReport.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", Attachment);
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Gr.HeaderStyle.Font.Bold = true;
                Gr.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }

        }
    }


    #region BlogStorage
    public int Blob_UploadDepositSlip(string ConStr, string ContainerName, string DocName, FileUpload FU, byte[] ChallanCopy)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;

        string Ext = Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.Properties.ContentType = System.Net.Mime.MediaTypeNames.Application.Pdf;
            if (!cblob.Exists())
            {
                using (Stream stream = new MemoryStream(ChallanCopy))
                {
                    cblob.UploadFromStream(stream);
                }
            }
            //cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }


    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }

    public void DeleteIFExits(string FileName)
    {
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = Path.GetFileNameWithoutExtension(FileName);
        try
        {
            Parallel.ForEach(container.ListBlobs(FN, true), y =>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }
    #endregion BlogStorage
  
}



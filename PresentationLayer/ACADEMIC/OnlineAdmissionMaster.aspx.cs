using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS;

public partial class ACADEMIC_OnlineAdmissionMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    StudentOnlineAdmissionMasterController objAdmMast = new StudentOnlineAdmissionMasterController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!Page.IsPostBack)
            {
                if (Session["userno"] == null && Session["username"] == null &&
                        Session["usertype"] == null && Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }

                ViewState["action"] = "add";
                Session["BoardNo"] = "0";

                //TAB-1 ExamBoard
                PopulateDropDownList();
                BindListView_ExamBoardDetails();

                //TAB-2 Subject
                BindListView_SubjectDetails();

                //TAB-3 Group
                BindListView_GroupDetails();

                //TAB-4 Subject Type
                BindALLDDLSubType();
                BindListView_SubjectType();

                //TAB-5 Board Subject Configuration
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count <= 0)
                    {
                        SetInitialRowBoSubConfig();
                    }
                }
                else
                {
                    SetInitialRowBoSubConfig();
                }
                BindBoardSubjConfig();

                //TAB-6 Subject Max Marks
                PopulateAddSubjectDropDownList();
                PopulateAddSubject_SubjectTypeDropDownList();
                BindListView_AddSubDetails();

                //TAB-7 Board Grade
                PopulateBoardGradeDropDownList();

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count <= 0)
                    {
                        SetInitialRow();
                    }
                }
                else
                {
                    SetInitialRow();
                }
                BindListView_BoardGradeSchemeDetails();

                //TAB-8 Reservation Config
                PopulateDropDownList_ReserConfig();

                //TAB-9 Qualifying Degree
                BindListView_QualDegreeDetails();

                //TAB-10 Program
                FillDropDownList();
                BindListView_ProgramDetails();

                //TAB-11 TestScore
                FillDropDownList_TestScore();
                BindListView_TestScore();

                //TAB-12 TestScore
                FillDropDownList_NonGate();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    #region Common Methods For All Tabs
    public void BindALLDDL(ref DropDownList ddl, DataSet ds, string textField, string valueField)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = ds;
            ddl.DataValueField = ds.Tables[0].Columns[valueField].ToString();
            ddl.DataTextField = ds.Tables[0].Columns[textField].ToString();
            ddl.DataBind();
            //  ddl.Items.Insert(0, "Please Select");
            ddl.Items.Insert(0, new ListItem("Please Select", "0"));
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    public void BindALLListBox(ListBox lstBox, DataSet ds, string textField, string valueField)
    {
        try
        {
            lstBox.Items.Clear();
            lstBox.DataSource = ds;
            lstBox.DataValueField = valueField;
            lstBox.DataTextField = textField;
            lstBox.DataBind();
            lstBox.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindALLDDL() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Tab-1 ExamBoard
    #region Common Dropdown List methods

    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
            BindALLDDL(ref ddlCountry, ds, "COUNTRYNAME", "COUNTRYNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "masters.PopulateDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCountry.SelectedIndex > 0)
            {
                BindState();
            }
            else
            {
                ddlState.Items.Clear();
                //ddlState.Items.Insert(0, "Please Select");
                ddlState.Items.Insert(0, new ListItem("Please Select", "0"));
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlCountry_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    private void BindState()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STATE", "STATENO", "STATENAME", "ACTIVESTATUS=1 AND COUNTRYNO=" + ddlCountry.SelectedValue, "STATENAME");
            BindALLDDL(ref ddlState, ds, "STATENAME", "STATENO");
            ddlState.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindState() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    #endregion

    #region Button Submit and Cancel
    protected void btnCancelBoard_Click(object sender, EventArgs e)
    {
        Session["BoardNo"] = "";
        ViewState["action"] = "add";
        //Response.Redirect(Request.Url.ToString());
        ClearExamBoard();
    }
    protected void btnSubmitBoard_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int BoardNo = 0;
            string BoardName = txtBoardName.Text.Trim();
            int CountryNo = Convert.ToInt32(ddlCountry.SelectedValue);
            int StateNo = Convert.ToInt32(ddlState.SelectedValue);


            string qualifyno = string.Empty;
            foreach (ListItem item in lstbxQualificationLevel.Items)
            {
                if (item.Selected == true)
                {
                    qualifyno += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(qualifyno))
            {
                qualifyno = qualifyno.Substring(0, qualifyno.Length - 1);
            }
            else
            {
                qualifyno = "0";
            }
            string QualifyNo = qualifyno;
            int ret = 0;
            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["BoardNo"].ToString()))
                {
                    BoardNo = Convert.ToInt32(Session["BoardNo"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objAdmMast.InsertUpdateExamBoard(BoardNo, CountryNo, StateNo, BoardName, QualifyNo));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearExamBoard();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
        }
        else
            Response.Redirect("~/default.aspx");
    }
    #endregion

    #region ListView Edit Delete Button
    private void BindListView_ExamBoardDetails()
    {
        try
        {
            //DataTable dt = objAdmMast.GetAllExamBoardList("", "").Tables[0];            
            //lvStudentDetails.DataSource = dt;
            //lvStudentDetails.DataBind();

            DataSet ds = objAdmMast.GetAllExamBoardList("", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlExamBoard.Visible = true;
                lvExamBoard.DataSource = ds;
                lvExamBoard.DataBind();
                ////objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvSession);//Set label - 

            }
            else
            {

                pnlExamBoard.Visible = false;
                lvExamBoard.DataSource = null;
                lvExamBoard.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_StudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEditExamBoard_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetSingleExamBoardInformation(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                //[BOARDNO],[BOARDNAME],[COUNTRYNO],[STATENO]
                Session["BoardNo"] = id;
                txtBoardName.Text = dt.Rows[0]["BOARDNAME"].ToString();
                ddlCountry.SelectedValue = dt.Rows[0]["COUNTRYNO"].ToString();
                BindState();
                ddlState.SelectedValue = dt.Rows[0]["STATENO"].ToString();
                //   ddlState.SelectedValue = dt.Rows[0]["STATENO"].ToString().Equals("0") ? "Please Select" : dt.Rows[0]["STATENO"].ToString();       
                lstbxQualificationLevel.ClearSelection();
                char delimiterChars = ',';
                string qualifyno = dt.Rows[0]["QUALIFYNO"].ToString();
                string[] qualify = qualifyno.Split(delimiterChars);
                for (int j = 0; j < qualify.Length; j++)
                {
                    for (int i = 0; i < lstbxQualificationLevel.Items.Count; i++)
                    {
                        if (qualify[j].Trim() == lstbxQualificationLevel.Items[i].Value.Trim())
                        {
                            lstbxQualificationLevel.Items[i].Selected = true;
                        }
                    }
                }


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditExamBoard_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnDeleteExamBoard_Click(object sender, EventArgs e)
    {
        try
        {
            //ImageButton btnDelete = sender as ImageButton;

            //string id = btnDelete.CommandArgument.ToString();

            //int del = 0;
            //del = objAdmMast.DeleteExamBoard(Convert.ToInt32(id));
            //if (del > 0)
            //{
            //    objCommon.DisplayMessage("Record deleted successfully.", this.Page);
            //    Clear();
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Error!Please try again", this.Page);
            //}

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnDeleteExamBoard_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ClearExamBoard()
    {
        ViewState["action"] = "add";
        Session["BoardNo"] = "0";

        txtBoardName.Text = string.Empty;

        ddlCountry.SelectedIndex = 0;
        ddlState.Items.Clear();
        //  ddlState.Items.Insert(0, "Please Select");
        ddlState.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlState.SelectedIndex = 0;
        lstbxQualificationLevel.ClearSelection();
        BindListView_ExamBoardDetails();

    }
    #endregion

    #region Tab-2-Subject
    #region Button Submit and Cancel
    protected void btnSubmitSub_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int SubjectNo = 0;
            string SubjectName = txtSubject.Text.Trim();

            int ret = 0;

            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["SubjectNo"].ToString()))
                {
                    SubjectNo = Convert.ToInt32(Session["SubjectNo"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objAdmMast.InsertUpdateSubject(SubjectNo, SubjectName));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearSubject();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);//Tab Postback issue
        }
        else
            Response.Redirect("~/default.aspx");

    }
    protected void btnCancelSub_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/default.aspx");
        Session["SubjectNo"] = "";
        ViewState["action"] = "add";
        //Response.Redirect(Request.Url.ToString());
        ClearSubject();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    #endregion

    #region ListView Edit Button
    protected void btnEditSubject_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetRetAllSubjectList(Convert.ToInt32(id), "", "").Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {

                //[STUDENTNO]
                //Session.Add("StudentNo",id);
                Session["SubjectNo"] = id;

                txtSubject.Text = dt.Rows[0]["SUBJECTNAME"].ToString();


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditSubject_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_2');</script>", false);
    }
    #endregion

    private void BindListView_SubjectDetails()
    {
        try
        {


            DataSet ds = objAdmMast.GetRetAllSubjectList(0, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlSubject.Visible = true;
                lvSubject.DataSource = ds;
                lvSubject.DataBind();


            }
            else
            {

                pnlSubject.Visible = false;
                lvSubject.DataSource = null;
                lvSubject.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_SubjectDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearSubject()
    {
        ViewState["action"] = "add";
        Session["SubjectNo"] = "";

        txtSubject.Text = string.Empty;

        BindListView_SubjectDetails();

    }
    #endregion

    #region Tab-3-Group
    #region Button Submit and Cancel
    protected void btnSubmitGroup_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int GroupId = 0;
            string GroupName = txtGroup.Text.Trim();

            int ret = 0;

            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["GroupId"].ToString()))
                {
                    GroupId = Convert.ToInt32(Session["GroupId"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objAdmMast.InsertUpdateGroup(GroupId, GroupName));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearGroup();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);//Tab Postback issue
        }
        else
            Response.Redirect("~/default.aspx");

    }
    protected void btnCancelGroup_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/default.aspx");
        Session["GroupId"] = "";
        ViewState["action"] = "add";
        //Response.Redirect(Request.Url.ToString());
        ClearGroup();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    #endregion

    #region ListView Edit Button
    protected void btnEditGroup_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetRetAllGroupList(Convert.ToInt32(id), "", "").Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {

                //[STUDENTNO]
                //Session.Add("StudentNo",id);
                Session["GroupId"] = id;

                txtGroup.Text = dt.Rows[0]["GROUPNAME"].ToString();


            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditGroup_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_3');</script>", false);
    }
    #endregion

    private void BindListView_GroupDetails()
    {
        try
        {


            DataSet ds = objAdmMast.GetRetAllGroupList(0, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                PnlGroup.Visible = true;
                lvGroup.DataSource = ds;
                lvGroup.DataBind();


            }
            else
            {

                PnlGroup.Visible = false;
                lvGroup.DataSource = null;
                lvGroup.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_GroupDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ClearGroup()
    {
        ViewState["action"] = "add";
        Session["GroupId"] = "";

        txtGroup.Text = string.Empty;

        BindListView_GroupDetails();

    }
    #endregion

    #region Tab-4-Subject Type
    public void BindALLDDLSubType()
    {
        try
        {

            DataSet dsSubjectType = objCommon.FillDropDown("ACD_SUBJECT", "SUBJECTNO", "SUBJECTNAME", "SUBJECTNO > 0 ", "SUBJECTNAME");

            lstSubjectName.Items.Clear();
            lstSubjectName.DataSource = dsSubjectType;
            lstSubjectName.DataValueField = "SUBJECTNO";
            lstSubjectName.DataTextField = "SUBJECTNAME";
            lstSubjectName.DataBind();
            lstSubjectName.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindALLDDLSubType() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    private void BindListView_SubjectType()
    {
        try
        {
            DataSet ds = objAdmMast.GetAllSubjectTypeList("", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlSubjectType.Visible = true;
                lvSubjectType.DataSource = ds;
                lvSubjectType.DataBind();

            }
            else
            {

                pnlSubjectType.Visible = false;
                lvSubjectType.DataSource = null;
                lvSubjectType.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_SubjectType() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    #region ListView Edit
    protected void btnEditSubjectType_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetSingleSubjectTypeInformation(Convert.ToInt32(id)).Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {

                Session["SubjectTypeNo"] = id;

                txtSubjectType.Text = dt.Rows[0]["SUBJECTTYPE"].ToString();
                BindALLDDLSubType();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (ListItem item in lstSubjectName.Items)
                    {
                        if (dt.Rows[i]["SUBJECTNO"].ToString().Equals(item.Value))
                        {
                            item.Selected = true;
                        }
                    }
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditSubjectType_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);//Tab Postback issue
    }
    #endregion

    #region SubjectType Submit & Cancel
    protected void btnSubmitSubjectType_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {

            string SubjectType = txtSubjectType.Text.Trim();
            string SubjectNo = string.Empty;
            foreach (ListItem item in lstSubjectName.Items)
            {
                if (item.Selected == true)
                {
                    SubjectNo += item.Value + ",";
                }
            }
            if (SubjectNo.Contains(','))
            {
                SubjectNo = SubjectNo.Remove(SubjectNo.Length - 1);
            }

            //int SubjectNo = Convert.ToInt32(lstSubjectName.SelectedValue); 

            int ret = 0;
            string subNames = "";
            string displaymsg = "";
            if (ViewState["action"].ToString().Equals("add"))
            {
                ret = Convert.ToInt32(objAdmMast.InsertSubjectType(SubjectType, SubjectNo));
                displaymsg = "Record added successfully.";
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["SubjectTypeNo"].ToString()))
                {
                    int SubjectTypeNo = Convert.ToInt32(Session["SubjectTypeNo"]);
                    //ret = Convert.ToInt32(objAdmMast.UpdateSubjectType(SubjectType, SubjectTypeNo, SubjectNo));
                    subNames = objAdmMast.UpdateSubjectType(SubjectType, SubjectTypeNo, SubjectNo);
                    if (IsAllDigits(subNames))
                    {
                        ret = Convert.ToInt32(subNames);
                    }
                    else
                    {
                        ret = 3;//for already subject marks entered.
                    }

                    displaymsg = "Record updated successfully.";
                }
            }

            if (ret == 3)
            {
                displaymsg = "Already filled marks these subjects:" + subNames + " against " + SubjectType + ".";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret == 1)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearSubjectType();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);//Tab Postback issue
        }
        else
            Response.Redirect("~/default.aspx");

    }
    public static bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!Char.IsDigit(c))
                return false;
        }
        return true;
    }
    protected void btnCancelSubjectType_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/default.aspx");
        Session["SubjectTypeNo"] = "";
        ViewState["action"] = "add";
        //Response.Redirect(Request.Url.ToString());
        BindALLDDLSubType();
        ClearSubjectType();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_4');</script>", false);//Tab Postback issue
    }
    #endregion

    private void ClearSubjectType()
    {
        ViewState["action"] = "add";
        Session["SubjectTypeNo"] = "";

        txtSubjectType.Text = string.Empty;
        lstSubjectName.SelectedIndex = 0;
        lstSubjectName.Items.Clear();
        lstSubjectName.Items.Insert(0, "Please Select");
        lstSubjectName.SelectedIndex = 0;
        BindListView_SubjectType();
        BindALLDDLSubType();

    }
    #endregion

    #region Tab-5 Board Subject Configuration

    #region Dropdown List & ListView
    private void BindBoardSubjConfig()
    {
        try
        {

            DataSet ds = objCommon.FillDropDown("ACD_EXAMBOARD", "BOARDNO", "BOARDNAME", "BOARDNO > 0", "BOARDNAME");
            BindALLDDL(ref ddlBoardSubjConfig, ds, "BOARDNAME", "BOARDNO");
            ddlBoardSubjConfig.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindBoardSubjConfig() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlBoardSubjConfig_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBoardSubjConfig.SelectedIndex > 0)
            {
                DataSet ds = objCommon.FillDropDown("ACD_ADMP_BOARD_SUBJECTTYPE_MAPPING", "ROW_NUMBER() OVER(ORDER BY BOARDNO ASC) AS ID,BOARDNO,SUBJECTTYPENO", "NO_OF_SUBJECTS", "BOARDNO =" + ddlBoardSubjConfig.SelectedValue, "BOARDNO");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["action"] = "edit";
                    ViewState["CurrentTable"] = ds.Tables[0];
                    BindListView_AddSubjectTypeSubjectDetails(ds.Tables[0]);
                }
                else
                {

                    pnlBoardSubType.Visible = true;
                    lvBoardSubType.DataSource = null;
                    lvBoardSubType.DataBind();
                    ViewState["action"] = "add";
                    ViewState["CurrentTable"] = null;
                    SetInitialRowBoSubConfig();
                }
            }

            else
            {

                pnlBoardSubType.Visible = true;
                lvBoardSubType.DataSource = null;
                lvBoardSubType.DataBind();
                ViewState["action"] = "add";
                ViewState["CurrentTable"] = null;
            }


        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlBoardSubjConfig_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue
    }
    private void BindListView_AddSubjectTypeSubjectDetails(DataTable dt)
    {
        try
        {

            if (dt != null && dt.Rows.Count > 0)
            {

                pnlBoardSubType.Visible = true;
                lvBoardSubType.DataSource = dt;// ds;
                lvBoardSubType.DataBind();

                ////ViewState["action"] = "edit";
                int nextRow, currentRow = 0;
                foreach (ListViewDataItem lv in lvBoardSubType.Items)
                {

                    DropDownList ddlBoConfSubType = lv.FindControl("ddlBoConfSubType") as DropDownList;
                    TextBox txtSubjectCount = lv.FindControl("txtSubjectCount") as TextBox;

                    objCommon.FillDropDownList(ddlBoConfSubType, "ACD_SUBJECTTYPE_MASTER", "SUBJECTTYPENO", "SUBJECTTYPE", "", "");


                    nextRow = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (currentRow == nextRow)
                        {

                            ddlBoConfSubType.SelectedValue = dt.Rows[i]["SUBJECTTYPENO"].ToString();
                            txtSubjectCount.Text = dt.Rows[i]["NO_OF_SUBJECTS"].ToString();
                        }
                        nextRow++;
                    }
                    currentRow++;
                }

            }
            else
            {
                SetInitialRowBoSubConfig();
                pnlBoardSubType.Visible = true;
                lvBoardSubType.DataSource = null;
                lvBoardSubType.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_AddSubjectTypeSubjectDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region DataSet To XML String Methods
    DataTable CreateDatatable_BoardSubjTypeConfig()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "ACD_ADMP_BOARD_SUBJECTTYPE_MAPPING";

            dt.Columns.Add("ID");

            dt.Columns.Add("SUBJECTTYPENO");
            dt.Columns.Add("NO_OF_SUBJECTS");


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.CreateDatatable_BoardSubjTypeConfig() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_BoardSubjTypeConfig()
    {
        DataTable dt = CreateDatatable_BoardSubjTypeConfig();
        try
        {
            int rowIndex = 0;
            foreach (var item in lvBoardSubType.Items)
            {
                DataRow dRow = dt.NewRow();
                HiddenField hidden1 = (HiddenField)lvBoardSubType.Items[rowIndex].FindControl("hfdBoConfSubType");
                DropDownList ddlBoConfSubType = (DropDownList)lvBoardSubType.Items[rowIndex].FindControl("ddlBoConfSubType");
                TextBox box1 = (TextBox)lvBoardSubType.Items[rowIndex].FindControl("txtSubjectCount");


                dRow["ID"] = hidden1.Value;
                dRow["SUBJECTTYPENO"] = ddlBoConfSubType.SelectedValue;
                dRow["NO_OF_SUBJECTS"] = box1.Text.Trim();


                rowIndex += 1;
                dt.Rows.Add(dRow);

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.Add_Datatable_BoardSubjTypeConfig() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }

    #region Add Board Subject Configuration ListBox
    private void SetInitialRowBoSubConfig()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("SUBJECTTYPENO", typeof(string)));
            dt.Columns.Add(new DataColumn("NO_OF_SUBJECTS", typeof(string)));


            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dr = dt.NewRow();
            dr["SUBJECTTYPENO"] = string.Empty;
            dr["NO_OF_SUBJECTS"] = string.Empty;


            dr["ID"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            lvBoardSubType.DataSource = dt;
            lvBoardSubType.DataBind();

            foreach (ListViewDataItem lv in lvBoardSubType.Items)
            {

                DropDownList ddlBoConfSubType = lv.FindControl("ddlBoConfSubType") as DropDownList;
                TextBox txtSubjectCount = lv.FindControl("txtSubjectCount") as TextBox;

                objCommon.FillDropDownList(ddlBoConfSubType, "ACD_SUBJECTTYPE_MASTER", "SUBJECTTYPENO", "SUBJECTTYPE", "", "");
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.SetInitialRowBoSubConfig() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SetPreviousBoardSubjTypeConfigData()
    {
        try
        {
            int rowIndex = 0;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            dtCurrentTable.Rows.Clear();
            dtCurrentTable.AcceptChanges();
            foreach (var item in lvBoardSubType.Items)
            {

                HiddenField hidden1 = (HiddenField)lvBoardSubType.Items[rowIndex].FindControl("hfdBoConfSubType");
                DropDownList ddlBoConfSubType = (DropDownList)lvBoardSubType.Items[rowIndex].FindControl("ddlBoConfSubType");
                TextBox box3 = (TextBox)lvBoardSubType.Items[rowIndex].FindControl("txtSubjectCount");



                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["SUBJECTTYPENO"] = ddlBoConfSubType.SelectedValue;
                drCurrentRow["NO_OF_SUBJECTS"] = box3.Text.Trim();
                drCurrentRow["ID"] = hidden1.Value;
                dtCurrentTable.Rows.Add(drCurrentRow);
                rowIndex += 1;

                //Do stuff
            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.SetPreviousBoardSubjTypeConfigData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {

                string _validateBoardSubjTypeConfig = validateBoardSubjTypeConfig();
                if (_validateBoardSubjTypeConfig != string.Empty)
                {
                    objCommon.DisplayMessage(Page, _validateBoardSubjTypeConfig, this.Page);
                    return;
                }

                SetPreviousBoardSubjTypeConfigData();
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    rowIndex = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["SUBJECTTYPENO"] = "0";
                    drCurrentRow["NO_OF_SUBJECTS"] = "0";
                    drCurrentRow["ID"] = rowIndex;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvBoardSubType.DataSource = dtCurrentTable;
                    lvBoardSubType.DataBind();


                    if (dtCurrentTable != null && dtCurrentTable.Rows.Count > 0)
                    {


                        ////ViewState["action"] = "edit";
                        int nextRow, currentRow = 0;
                        foreach (ListViewDataItem lv in lvBoardSubType.Items)
                        {

                            DropDownList ddlBoConfSubType = lv.FindControl("ddlBoConfSubType") as DropDownList;
                            TextBox txtSubjectCount = lv.FindControl("txtSubjectCount") as TextBox;

                            objCommon.FillDropDownList(ddlBoConfSubType, "ACD_SUBJECTTYPE_MASTER", "SUBJECTTYPENO", "SUBJECTTYPE", "", "");


                            nextRow = 0;
                            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                            {
                                if (currentRow == nextRow)
                                {

                                    ddlBoConfSubType.SelectedValue = dtCurrentTable.Rows[i]["SUBJECTTYPENO"].ToString();
                                    txtSubjectCount.Text = dtCurrentTable.Rows[i]["NO_OF_SUBJECTS"].ToString();
                                }
                                nextRow++;
                            }
                            currentRow++;
                        }

                    }
                }
                else
                {
                    objCommon.DisplayMessage(Page, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(Page, "Error!!!", this.Page);
            }
            //SetPreviousData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.btnAddDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue
        }
    }
    private string validateBoardSubjTypeConfig()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvBoardSubType.Items)
            {

                DropDownList ddlBoConfSubType = (DropDownList)lvBoardSubType.Items[rowIndex].FindControl("ddlBoConfSubType");
                TextBox box3 = (TextBox)lvBoardSubType.Items[rowIndex].FindControl("txtSubjectCount");

                if (ddlBoConfSubType.SelectedValue == null || ddlBoConfSubType.SelectedValue == "0" || ddlBoConfSubType.SelectedValue == "Please select")
                {
                    _validate = "Please Select SubjectType";
                    return _validate;
                }
                else if (box3.Text.Trim() == string.Empty || box3.Text.Trim() == "0")
                {
                    _validate = "Please Enter Subject Count";
                    return _validate;
                }

                int rowcheckDuplicateEntryIndex = 0;
                foreach (var checkGName in lvBoardSubType.Items)
                {

                    if (rowcheckDuplicateEntryIndex == rowIndex)
                    {
                        continue;
                    }
                    if (ddlBoConfSubType.SelectedValue.Equals(((DropDownList)checkGName.FindControl("ddlBoConfSubType")).SelectedValue))
                    {
                        _validate = "Already exist SubjectType.";

                        return _validate;
                    }
                    rowcheckDuplicateEntryIndex += 1;
                }

                rowIndex += 1;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "Masters.validateBoardSubjTypeConfig() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }
    #endregion

    #endregion

    #region Button Submit and Cancel
    protected void btnCancelSubConfig_Click(object sender, EventArgs e)
    {

        //Session["BoardNo"] = "";
        ViewState["action"] = "add";

        ClearBoardSubjTypeConfig();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue
    }
    protected void btnSubmitSubConfig_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue

            string _validateBoardSubjTypeConfig = validateBoardSubjTypeConfig();
            if (_validateBoardSubjTypeConfig != string.Empty)
            {
                objCommon.DisplayMessage(_validateBoardSubjTypeConfig, this.Page);
                return;
            }

            DataTable dt = Add_Datatable_BoardSubjTypeConfig();
            DataSet ds = new DataSet();
            ds.DataSetName = "ACD_ADMP_BOARD_SUBJECTTYPE_MAPPINGS";
            ds.Tables.Add(dt);
            if (dt.Rows.Count > 0)
            {

                int BoardNo = Convert.ToInt32(ddlBoardSubjConfig.SelectedValue);

                int ret = 0;

                string displaymsg = "";
                ret = Convert.ToInt32(objAdmMast.InsertUpdateBoardSubjTypeConfig(BoardNo, ds.GetXml()));
                if (ViewState["action"].ToString().Equals("add"))
                {

                    displaymsg = "Record added successfully.";
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {

                    displaymsg = "Record updated successfully.";
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }
                ClearBoardSubjTypeConfig();

                objCommon.DisplayMessage(displaymsg, this.Page);

            }

        }
        else
            Response.Redirect("~/default.aspx");


    }
    protected void btnBoSubConfigDeleteDetail_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue
        try
        {
            LinkButton btnDelete = sender as LinkButton;


            string commandArgs = btnDelete.CommandArgument.ToString();

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];



            for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtCurrentTable.Rows[i];
                ////if (dr["name"] == "Joe")
                if (dr["ID"].ToString().Equals(commandArgs))
                    dr.Delete();
            }
            dtCurrentTable.AcceptChanges();

            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                dtCurrentTable.Rows[i]["ID"] = i + 1;

            }

            if (dtCurrentTable != null && dtCurrentTable.Rows.Count > 0)
            {
                ViewState["CurrentTable"] = dtCurrentTable;
                lvBoardSubType.DataSource = dtCurrentTable;
                lvBoardSubType.DataBind();

                int nextRow, currentRow = 0;
                foreach (ListViewDataItem lv in lvBoardSubType.Items)
                {

                    DropDownList ddlBoConfSubType = lv.FindControl("ddlBoConfSubType") as DropDownList;
                    TextBox txtSubjectCount = lv.FindControl("txtSubjectCount") as TextBox;

                    objCommon.FillDropDownList(ddlBoConfSubType, "ACD_SUBJECTTYPE_MASTER", "SUBJECTTYPENO", "SUBJECTTYPE", "", "");


                    nextRow = 0;
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        if (currentRow == nextRow)
                        {

                            ddlBoConfSubType.SelectedValue = dtCurrentTable.Rows[i]["SUBJECTTYPENO"].ToString();
                            txtSubjectCount.Text = dtCurrentTable.Rows[i]["NO_OF_SUBJECTS"].ToString();
                        }
                        nextRow++;
                    }
                    currentRow++;
                }

            }
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_5');</script>", false);//Tab Postback issue

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "lnkReservationDetails.btnDeleteCulturalEventDetail_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ClearBoardSubjTypeConfig()
    {
        ViewState["action"] = "add";

        ddlBoardSubjConfig.SelectedIndex = 0;

        pnlBoardSubType.Visible = true;
        lvBoardSubType.DataSource = null;
        lvBoardSubType.DataBind();
        ViewState["action"] = "add";

        SetInitialRowBoSubConfig();
    }

    #endregion

    #region Tab-6 SUBJECT Max Marks
    #region Common Dropdown List methods
    private void PopulateAddSubjectDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
            BindALLDDL(ref ddlCountrySub, ds, "COUNTRYNAME", "COUNTRYNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "masters.PopulateAddSubjectDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlCountrySub_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStateSub.Items.Clear();
            ddlBoard.Items.Clear();
            //  ddlStateSub.Items.Insert(0, "Please Select");
            ddlStateSub.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlBoard.Items.Insert(0, "Please Select");
            ddlQualification.SelectedIndex = 0;
            ddlSubType.SelectedIndex = 0;
            pnlAddSubjectMaxMarks.Visible = false;
            lvAddSubjectMaxMarks.DataSource = null;
            lvAddSubjectMaxMarks.DataBind();
            if (ddlCountrySub.SelectedIndex > 0)
            {
                BindStateSub();
            }
            else
            {
                ddlStateSub.Items.Clear();
                ddlBoard.Items.Clear();
                //  ddlStateSub.Items.Insert(0, "Please Select");
                ddlStateSub.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlBoard.Items.Insert(0, "Please Select");
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlCountrySub_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }
    private void BindStateSub()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STATE", "STATENO", "STATENAME", "ACTIVESTATUS=1 AND COUNTRYNO=" + ddlCountrySub.SelectedValue, "STATENAME");
            BindALLDDL(ref ddlStateSub, ds, "STATENAME", "STATENO");
            ddlStateSub.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindStateSub() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlStateSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlStateSub.SelectedIndex > 0)
            //{
            //   // BindBoardSub();
            //}
            //else
            //{
            //ddlBoard.Items.Clear();
            //ddlBoard.Items.Insert(0, "Please Select");
            // }

            ddlBoard.Items.Clear();
            ddlBoard.Items.Insert(0, "Please Select");
            ddlQualification.SelectedIndex = 0;
            ddlSubType.SelectedIndex = 0;
            pnlAddSubjectMaxMarks.Visible = false;
            lvAddSubjectMaxMarks.DataSource = null;
            lvAddSubjectMaxMarks.DataBind();
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlStateSub_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }
    private void BindBoardSub()
    {
        try
        {
            //BOARDNO,BOARDNAME,STATENO
            int qualifyno = Convert.ToInt32(ddlQualification.SelectedValue);
            int countryid = Convert.ToInt32(ddlCountrySub.SelectedValue);
            int stateno = Convert.ToInt32(ddlStateSub.SelectedValue);

            DataSet ds = objAdmMast.GetBoard(countryid, stateno, qualifyno);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBoard.Items.Clear();
                ddlBoard.DataValueField = ds.Tables[0].Columns["BOARDNO"].ToString();
                ddlBoard.DataTextField = ds.Tables[0].Columns["BOARDNAME"].ToString();
                ddlBoard.DataSource = ds;
                ddlBoard.DataBind();
                ddlBoard.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlBoard.Items.Clear();
                ddlBoard.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            //DataSet ds = objCommon.FillDropDown("ACD_EXAMBOARD", "BOARDNO", "BOARDNAME", "STATENO=" + ddlStateSub.SelectedValue, "BOARDNAME");
            //BindALLDDL(ref ddlBoard, ds, "BOARDNAME", "BOARDNO");
            //ddlBoard.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindBoardSub() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    //ACD_SUBJECTTYPE_MASTER,SUBJECTTYPENO,SUBJECTTYPE    
    private void PopulateAddSubject_SubjectTypeDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_SUBJECTTYPE_MASTER", "SUBJECTTYPENO", "SUBJECTTYPE", "SUBJECTTYPENO > 0", "SUBJECTTYPE");
            BindALLDDL(ref ddlSubType, ds, "SUBJECTTYPE", "SUBJECTTYPENO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "masters.PopulateAddSubject_SubjectTypeDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlBoard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubType.SelectedIndex = 0;
            pnlAddSubjectMaxMarks.Visible = false;
            lvAddSubjectMaxMarks.DataSource = null;
            lvAddSubjectMaxMarks.DataBind();
            if (ddlBoard.SelectedIndex > 0)
            {
                commonBindExistAndEditData(Convert.ToInt32(ddlBoard.SelectedValue), Convert.ToInt32(ddlQualification.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ddlCountrySub.SelectedValue), Convert.ToInt32(ddlStateSub.SelectedValue));
            }
            else
            {
                pnlAddSubjectMaxMarks.Visible = false;
                lvAddSubjectMaxMarks.DataSource = null;
                lvAddSubjectMaxMarks.DataBind();
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlStateSub_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }
    protected void ddlQualification_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlSubType.SelectedIndex = 0;
            pnlAddSubjectMaxMarks.Visible = false;
            lvAddSubjectMaxMarks.DataSource = null;
            lvAddSubjectMaxMarks.DataBind();
            if (ddlQualification.SelectedIndex > 0)
            {
                BindBoardSub();
                commonBindExistAndEditData(Convert.ToInt32(ddlBoard.SelectedValue), Convert.ToInt32(ddlQualification.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ddlCountrySub.SelectedValue), Convert.ToInt32(ddlStateSub.SelectedValue));
            }
            else
            {
                pnlAddSubjectMaxMarks.Visible = false;
                lvAddSubjectMaxMarks.DataSource = null;
                lvAddSubjectMaxMarks.DataBind();
            }


        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlStateSub_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }
    protected void ddlSubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubType.SelectedIndex > 0)
            {
                //DataSet ds = objAdmMast.GetAllMaxMarksSubjectsList(Convert.ToInt32(ddlSubType.SelectedValue));
                //BindListView_AddSubMaxMarksDetails(ds.Tables[0]);
                commonBindExistAndEditData(Convert.ToInt32(ddlBoard.SelectedValue), Convert.ToInt32(ddlQualification.SelectedValue), Convert.ToInt32(ddlSubType.SelectedValue), Convert.ToInt32(ddlCountrySub.SelectedValue), Convert.ToInt32(ddlStateSub.SelectedValue));
            }
            else
            {

                pnlAddSubjectMaxMarks.Visible = false;
                lvAddSubjectMaxMarks.DataSource = null;
                lvAddSubjectMaxMarks.DataBind();

            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlStateSub_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }

    //Subject Name List based on SUBJECT TYPE DDL 
    private void BindListView_AddSubMaxMarksDetails(DataTable dt)
    {
        try
        {

            //DataSet ds = objAdmMast.GetAllMaxMarksSubjectsList(Convert.ToInt32(ddlSubType.SelectedValue));
            //DataTable dt
            if (dt != null && dt.Rows.Count > 0)
            {

                pnlAddSubjectMaxMarks.Visible = true;
                lvAddSubjectMaxMarks.DataSource = dt;// ds;
                lvAddSubjectMaxMarks.DataBind();

                int nextRow, currentRow = 0;
                foreach (ListViewDataItem lv in lvAddSubjectMaxMarks.Items)
                {

                    /* DropDownList ddlGroup = lv.FindControl("ddlGroup") as DropDownList;
                    objCommon.FillDropDownList(ddlGroup, "ACD_ADMP_GROUP", "GROUPID", "GROUPNAME", "", "");

                    nextRow = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (currentRow == nextRow)
                        {

                            ddlGroup.SelectedValue = dt.Rows[i]["GROUPID"].ToString();

                        }
                        nextRow++;
                    } */

                    CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                    ListBox lstGroup = lv.FindControl("lstGroup") as ListBox;
                    TextBox txtMaxMarks = lv.FindControl("txtMaxMarks") as TextBox;   // Added by Yogesh Kumbhar Date:15-11-2022

                    DataSet dsGroup = objCommon.FillDropDown("ACD_ADMP_GROUP", "GROUPID", "GROUPNAME", "", "");
                    BindALLListBox(lstGroup, dsGroup, "GROUPNAME", "GROUPID");

                    nextRow = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (currentRow == nextRow)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[i]["ISSUBACTIVE"].ToString()))
                            {
                                if ((bool)dt.Rows[i]["ISSUBACTIVE"])      // Added by Yogesh Kumbhar Date:15-11-2022
                                {
                                    chkIsActive.Checked = true;
                                    txtMaxMarks.Attributes.Remove("disabled");
                                    txtMaxMarks.Attributes.Add("enabled", "enabled");

                                    lstGroup.Attributes.Remove("disabled");
                                    lstGroup.Attributes.Add("enabled", "enabled");
                                }
                                else
                                {
                                    txtMaxMarks.Attributes.Remove("enabled");
                                    txtMaxMarks.Attributes.Add("disabled", "disabled");
                                    txtMaxMarks.Text = "";

                                    lstGroup.Attributes.Remove("enabled");
                                    lstGroup.Attributes.Add("disabled", "disabled");
                                    lstGroup.ClearSelection();
                                }
                            }

                            if (!string.IsNullOrEmpty(dt.Rows[i]["GROUPID"].ToString()))
                            {
                                string commaSeparatedIDs = dt.Rows[i]["GROUPID"].ToString();
                                int[] nums = Array.ConvertAll(commaSeparatedIDs.Split(','), int.Parse);

                                for (int j = 0; j < nums.Length; j++)
                                {
                                    foreach (ListItem item in lstGroup.Items)
                                    {
                                        if (nums[j].ToString().Equals(item.Value))
                                        {
                                            item.Selected = true;
                                        }

                                    }
                                }
                            }

                        }
                        nextRow++;
                    }
                    currentRow++;
                }

            }
            else
            {

                pnlAddSubjectMaxMarks.Visible = false;
                lvAddSubjectMaxMarks.DataSource = null;
                lvAddSubjectMaxMarks.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_AddSubMaxMarksDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region DataSet To XML String Methods
    DataTable CreateDatatable_SubMaxMarks()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "ACD_ADMISSION_SUBJECT_MARK";
            dt.Columns.Add("BOARDNO");
            dt.Columns.Add("QUALIFICATION_LEVEL");
            dt.Columns.Add("SUBJECTTYPENO");
            dt.Columns.Add("SUBJECTNO");
            dt.Columns.Add("MAXMARKS");
            dt.Columns.Add("GROUPID");
            dt.Columns.Add("ISSUBACTIVE");
            /*
            dt.Columns.Add("ModifiedBy");
            dt.Columns.Add("GradeName");
            dt.Columns.Add("MinMarks");
            dt.Columns.Add("MaxMarks");
            dt.Columns.Add("FailGrade");
            */
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.CreateDatatable_SubMaxMarks() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_SubMaxMarks()
    {
        DataTable dt = CreateDatatable_SubMaxMarks();
        try
        {
            int rowIndex = 0;
            foreach (var item in lvAddSubjectMaxMarks.Items)
            {
                DataRow dRow = dt.NewRow();
                HiddenField hidden1 = (HiddenField)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("hfdMaxMarksSubNo");
                CheckBox chkIsActive = (CheckBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("chkIsActive");
                TextBox box1 = (TextBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("txtMaxMarks");
                ////DropDownList ddlGroup = (DropDownList)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("ddlGroup");
                ListBox lstGroup = (ListBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("lstGroup");

                dRow["BOARDNO"] = ddlBoard.SelectedValue;
                dRow["QUALIFICATION_LEVEL"] = ddlQualification.SelectedValue;
                dRow["ISSUBACTIVE"] = chkIsActive.Checked ? 1 : 0;
                dRow["SUBJECTTYPENO"] = ddlSubType.SelectedValue; ;
                dRow["SUBJECTNO"] = hidden1.Value; ;
                dRow["MAXMARKS"] = box1.Text.Trim();
                ////dRow["GROUPID"] = Convert.ToInt32(ddlGroup.SelectedValue) > 0 ? ddlGroup.SelectedValue : null;

                string GroupId = string.Empty;
                foreach (ListItem lstitem in lstGroup.Items)
                {
                    if (lstitem.Selected == true)
                    {
                        GroupId += lstitem.Value + ",";
                    }
                }
                if (GroupId.Contains(','))
                {
                    GroupId = GroupId.Remove(GroupId.Length - 1);
                }
                dRow["GROUPID"] = GroupId; // Convert.ToInt32(lstGroup.SelectedValue) > 0 ? lstGroup.SelectedValue : null;

                rowIndex += 1;
                dt.Rows.Add(dRow);


                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.Add_Datatable_SubMaxMarks() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private string validateSubMaxMarksData()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvAddSubjectMaxMarks.Items)
            {
                CheckBox chkIsActive = (CheckBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("chkIsActive"); // Added by Yogesh Kumbhar Date:15-11-2022
                Label lbl1 = (Label)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("lblMaxMarksSubName");
                TextBox box1 = (TextBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("txtMaxMarks");
                ////DropDownList ddlGroup = (DropDownList)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("ddlGroup");
                ListBox lstGroup = (ListBox)lvAddSubjectMaxMarks.Items[rowIndex].FindControl("lstGroup");

                if (chkIsActive.Checked)     // Added by Yogesh Kumbhar Date:15-11-2022
                {
                    if (box1.Text.Trim() == string.Empty || box1.Text.Trim() == "0")
                    {
                        _validate = "Please Enter Max Marks for Subject: " + lbl1.Text.Trim();


                        box1.Attributes.Remove("disabled");
                        box1.Attributes.Add("enabled", "enabled");

                        lstGroup.Attributes.Remove("disabled");
                        lstGroup.Attributes.Add("enabled", "enabled");
                        return _validate;
                    }
                }
                else
                {
                    box1.Attributes.Remove("enabled");
                    box1.Attributes.Add("disabled", "disabled");
                    box1.Text = "";

                    lstGroup.Attributes.Remove("enabled");
                    lstGroup.Attributes.Add("disabled", "disabled");
                    lstGroup.ClearSelection();
                }
                //if (Convert.ToInt32(ddlGroup.SelectedValue) <= 0)
                //{

                //    _validate = "Please Select Group for Subject: " + lbl1.Text.Trim();
                //    return _validate;
                //}
                rowIndex += 1;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "masters.validateSubMaxMarksData() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }
    #endregion

    #region Button Submit and Cancel
    protected void btnCancelAddSub_Click(object sender, EventArgs e)
    {
        Session["BoardNo"] = "";
        ViewState["action"] = "add";
        ClearAddSubMaxMarks();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue
    }
    protected void btnSubmitAddSub_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue

            string _validateSubMaxMarksData = validateSubMaxMarksData();
            if (_validateSubMaxMarksData != string.Empty)
            {
                objCommon.DisplayMessage(_validateSubMaxMarksData, this.Page);
                return;
            }

            DataTable dt = Add_Datatable_SubMaxMarks();
            DataSet ds = new DataSet();
            ds.DataSetName = "ACD_ADMISSION_SUBJECT_MARKS";
            ds.Tables.Add(dt);
            if (dt.Rows.Count > 0)
            {

                int BoardNo = Convert.ToInt32(ddlBoard.SelectedValue);
                int QualificationLevel = Convert.ToInt32(ddlQualification.SelectedValue);
                int SubjectTypeNo = Convert.ToInt32(ddlSubType.SelectedValue);
                int CountryNo = Convert.ToInt32(ddlCountrySub.SelectedValue);
                int StateNo = Convert.ToInt32(ddlStateSub.SelectedValue);
                int ret = 0;

                string displaymsg = "";

                if (ViewState["action"].ToString().Equals("add"))
                {
                    ret = Convert.ToInt32(objAdmMast.InsertSubjectwiseMaxMarks(BoardNo, QualificationLevel, SubjectTypeNo, ds.GetXml(), CountryNo, StateNo));
                    displaymsg = "Record added successfully.";
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    ret = Convert.ToInt32(objAdmMast.UpdateSubjectwiseMaxMarksSubjects(BoardNo, QualificationLevel, SubjectTypeNo, ds.GetXml(), CountryNo, StateNo));
                    displaymsg = "Record updated successfully.";
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }

                if (ret == 2)
                {
                    displaymsg = "Record already exist.";
                }
                else if (ret > 0)
                {
                    ClearAddSubMaxMarks();
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }
                objCommon.DisplayMessage(displaymsg, this.Page);

            }
            else
            {
                //objCommon.DisplayMessage("Error!Please Fill Subject Max Marks Data again", this.Page);
                objCommon.DisplayMessage("Please Fill Subject Max Marks Data.", this.Page);
                return;
            }
        }
        else
            Response.Redirect("~/default.aspx");


    }
    #endregion

    #region ListView Edit Button
    private void BindListView_AddSubDetails()
    {
        try
        {

            DataSet ds = objAdmMast.GetAllSubjectwiseMaxMarksList("", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlAddSubjectList.Visible = true;
                lvAddSubjectList.DataSource = ds;
                lvAddSubjectList.DataBind();

            }
            else
            {

                pnlAddSubjectList.Visible = false;
                lvAddSubjectList.DataSource = null;
                lvAddSubjectList.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_AddSubDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEditAddSub_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_6');</script>", false);//Tab Postback issue

            ImageButton btnEdit = sender as ImageButton;
            DataSet ds;
            string[] commandArgs = btnEdit.CommandArgument.ToString().Split(new char[] { ',' });
            string BoardNo = commandArgs[0];
            string QualificationLevel = commandArgs[1];
            string SubjectTypeNo = commandArgs[2];
            string CountryNo = commandArgs[3];
            string StateNo = commandArgs[4];

            ViewState["action"] = "edit";
            ddlCountrySub.Enabled = false;
            ddlStateSub.Enabled = false;
            ddlBoard.Enabled = false;
            ddlQualification.Enabled = false;
            ddlSubType.Enabled = false;

            commonBindExistAndEditData(Convert.ToInt32(BoardNo), Convert.ToInt32(QualificationLevel), Convert.ToInt32(SubjectTypeNo), Convert.ToInt32(CountryNo), Convert.ToInt32(StateNo));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditAddSub_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void commonBindExistAndEditData(int BoardNo, int QualificationLevel, int SubjectTypeNo, int CountryNo, int StateNo)
    {
        try
        {
            if (BoardNo > 0 && QualificationLevel > 0 && SubjectTypeNo > 0)
            {
                DataSet ds;

                ds = objAdmMast.GetSingleSubjectwiseMaxMarksInformation(BoardNo, QualificationLevel, SubjectTypeNo, CountryNo, StateNo);
                if (ds.Tables.Count == 2)
                {
                    if (ViewState["action"].ToString().Equals("edit"))
                    {

                        ddlCountrySub.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                        if (ddlCountrySub.SelectedIndex > 0)
                        {
                            BindStateSub();
                        }
                        ddlStateSub.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                        ddlQualification.SelectedValue = ds.Tables[0].Rows[0]["QUALIFICATION_LEVEL"].ToString();

                        if (ddlQualification.SelectedIndex > 0)
                        {
                            BindBoardSub();
                        }
                        ddlBoard.SelectedValue = ds.Tables[0].Rows[0]["BOARDNO"].ToString();

                        ddlSubType.SelectedValue = ds.Tables[0].Rows[0]["SUBJECTTYPENO"].ToString();

                        BindListView_AddSubMaxMarksDetails(ds.Tables[1]);

                    }
                    else if (ViewState["action"].ToString().Equals("add"))
                    {

                        BindListView_AddSubMaxMarksDetails(ds.Tables[1]);
                    }
                }
                else
                {
                    //DataSet ds = objAdmMast.GetAllMaxMarksSubjectsList(Convert.ToInt32(ddlSubType.SelectedValue));
                    //BindListView_AddSubMaxMarksDetails(ds.Tables[0]);
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.commonBindExistAndEditData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ClearAddSubMaxMarks()
    {
        ViewState["action"] = "add";

        ddlCountrySub.SelectedIndex = 0;

        ddlStateSub.Items.Clear();
        //  ddlStateSub.Items.Insert(0, "Please Select");
        ddlStateSub.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlStateSub.SelectedIndex = 0;

        ddlBoard.Items.Clear();
        ddlBoard.Items.Insert(0, "Please Select");
        ddlBoard.SelectedIndex = 0;

        ddlQualification.SelectedIndex = 0;
        ddlSubType.SelectedIndex = 0;

        pnlAddSubjectMaxMarks.Visible = false;
        lvAddSubjectMaxMarks.DataSource = null;
        lvAddSubjectMaxMarks.DataBind();

        BindListView_AddSubDetails();

        ddlCountrySub.Enabled = true;
        ddlStateSub.Enabled = true;
        ddlBoard.Enabled = true;
        ddlQualification.Enabled = true;
        ddlSubType.Enabled = true;
    }
    #endregion

    #region Tab-7 Board Grade Scheme

    #region Common Dropdown List methods
    private void PopulateBoardGradeDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNO > 0", "COUNTRYNAME");
            BindALLDDL(ref ddlCountryGrade, ds, "COUNTRYNAME", "COUNTRYNO");
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "masters.PopulateBoardGradeDropDownList() --> " + ex.Message + " " + ex.StackTrace);
        }
    }
    protected void ddlCountryGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlQualificationGrade.SelectedIndex = 0;
            ddlStateGrade.Items.Clear();
            ddlBoardGrade.Items.Clear();
            // ddlStateGrade.Items.Insert(0, "Please Select");
            ddlStateGrade.Items.Insert(0, new ListItem("Please Select", "0"));
            ddlBoardGrade.Items.Insert(0, "Please Select");

            //  txtMaxGradePoint.Text = "";
            if (ddlCountryGrade.SelectedIndex > 0)
            {
                BindStateGrade();
            }
            else
            {
                ddlStateGrade.Items.Clear();
                ddlBoardGrade.Items.Clear();
                // ddlStateGrade.Items.Insert(0, "Please Select");
                ddlStateGrade.Items.Insert(0, new ListItem("Please Select", "0"));
                ddlBoardGrade.Items.Insert(0, "Please Select");
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlCountryGrade_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
    }
    private void BindStateGrade()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_STATE", "STATENO", "STATENAME", "ACTIVESTATUS=1 AND COUNTRYNO=" + ddlCountryGrade.SelectedValue, "STATENAME");
            BindALLDDL(ref ddlStateGrade, ds, "STATENAME", "STATENO");
            ddlStateGrade.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindStateSub() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlStateGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlStateGrade.SelectedIndex > 0)
            //{
            //    BindBoardGrade();
            //}
            //else
            //{
            //    ddlBoardGrade.Items.Clear();
            //    ddlBoardGrade.Items.Insert(0, "Please Select");
            //}
            ddlBoardGrade.Items.Clear();
            ddlBoardGrade.Items.Insert(0, "Please Select");
            ddlQualificationGrade.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlStateGrade_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
    }
    private void BindBoardGrade()
    {
        try
        {
            //BOARDNO,BOARDNAME,STATENO
            //DataSet ds = objCommon.FillDropDown("ACD_EXAMBOARD", "BOARDNO", "BOARDNAME", "STATENO=" + ddlStateGrade.SelectedValue, "BOARDNAME");
            //BindALLDDL(ref ddlBoardGrade, ds, "BOARDNAME", "BOARDNO");
            //ddlBoardGrade.SelectedIndex = 0;

            int stateno = 0;
            int qualifyno = Convert.ToInt32(ddlQualificationGrade.SelectedValue);
            int countryid = Convert.ToInt32(ddlCountryGrade.SelectedValue);
            if (ddlStateGrade.SelectedIndex > 0)
            {
                stateno = Convert.ToInt32(ddlStateGrade.SelectedValue);
            }
            else
            {
                stateno = 0;
            }
            DataSet ds = objAdmMast.GetBoard(countryid, stateno, qualifyno);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlBoardGrade.Items.Clear();
                ddlBoardGrade.DataValueField = ds.Tables[0].Columns["BOARDNO"].ToString();
                ddlBoardGrade.DataTextField = ds.Tables[0].Columns["BOARDNAME"].ToString();
                ddlBoardGrade.DataSource = ds;
                ddlBoardGrade.DataBind();
                ddlBoardGrade.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            else
            {
                ddlBoardGrade.Items.Clear();
                ddlBoardGrade.Items.Insert(0, new ListItem("Please Select", "0"));
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindBoardGrade() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlBoardGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBoardGrade.SelectedIndex > 0)
            {

                commonBindExistAndEditBoardGradeSchemeData(Convert.ToInt32(ddlBoardGrade.SelectedValue), Convert.ToInt32(ddlQualificationGrade.SelectedValue), Convert.ToInt32(ddlCountryGrade.SelectedValue), Convert.ToInt32(ddlStateGrade.SelectedValue));
            }
            else
            {
                txtMaxGradePoint.Text = "";
                SetInitialRow();
                //pnlGradeSchemeList.Visible = false;
                //lvGradeSchemeList.DataSource = null;
                //lvGradeSchemeList.DataBind();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlBoardGrade_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
    }

    protected void ddlQualificationGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlQualificationGrade.SelectedIndex > 0)
            {
                BindBoardGrade();
                commonBindExistAndEditBoardGradeSchemeData(Convert.ToInt32(ddlBoardGrade.SelectedValue), Convert.ToInt32(ddlQualificationGrade.SelectedValue), Convert.ToInt32(ddlCountryGrade.SelectedValue), Convert.ToInt32(ddlStateGrade.SelectedValue));
            }
            else
            {
                //pnlGradeSchemeList.Visible = false;
                //lvGradeSchemeList.DataSource = null;
                //lvGradeSchemeList.DataBind();
                txtMaxGradePoint.Text = "";
                ddlBoardGrade.Items.Clear();
                ddlBoardGrade.Items.Insert(0, "Please Select");
                SetInitialRow();
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlQualificationGrade_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
    }
    #endregion

    #region Add Grade ListBox
    private void SetInitialRow()
    {
        try
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("GRADENAME", typeof(string)));
            dt.Columns.Add(new DataColumn("GRADEPOINT", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column3", typeof(string)));

            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dr = dt.NewRow();
            dr["GRADENAME"] = string.Empty;
            dr["GRADEPOINT"] = string.Empty;
            //dr["Column3"] = string.Empty;

            dr["ID"] = 0;
            dt.Rows.Add(dr);
            ViewState["CurrentTable"] = dt;

            lvGradeSchemeList.DataSource = dt;
            lvGradeSchemeList.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.SetInitialRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void SetPreviousData()
    {
        try
        {
            int rowIndex = 0;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            dtCurrentTable.Rows.Clear();
            dtCurrentTable.AcceptChanges();
            foreach (var item in lvGradeSchemeList.Items)
            {

                HiddenField hidden1 = (HiddenField)lvGradeSchemeList.Items[rowIndex].FindControl("hfdvalue");
                TextBox box1 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradePoint");
                //TextBox box3 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtMaxGradePoint");


                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["GRADENAME"] = box1.Text.Trim();
                drCurrentRow["GRADEPOINT"] = box2.Text.Trim();
                //drCurrentRow["Column3"] = box3.Text.Trim();                
                drCurrentRow["ID"] = hidden1.Value;
                dtCurrentTable.Rows.Add(drCurrentRow);
                rowIndex += 1;

                //Do stuff
            }
            ViewState["CurrentTable"] = dtCurrentTable;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.SetPreviousData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnAddGradeSlab_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                //string _validateGradeSchemeData = validateGradeSchemeData();
                string _validateGradeSchemeData = validateGradeSchemeData();
                if (_validateGradeSchemeData != string.Empty)
                {
                    objCommon.DisplayMessage(Page, _validateGradeSchemeData, this.Page);
                    return;
                }
                SetPreviousData();
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    rowIndex = dtCurrentTable.Rows.Count + 1;
                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["GRADENAME"] = "0";
                    drCurrentRow["GRADEPOINT"] = "0";
                    //drCurrentRow["Column3"] = "0";
                    drCurrentRow["ID"] = rowIndex;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["CurrentTable"] = dtCurrentTable;
                    lvGradeSchemeList.DataSource = dtCurrentTable;
                    lvGradeSchemeList.DataBind();
                }
                else
                {
                    objCommon.DisplayMessage(Page, "Maximum Options Limit Reached", this.Page);
                }
            }
            else
            {
                objCommon.DisplayMessage(Page, "Error!!!", this.Page);
            }
            //SetPreviousData();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.btnAddGradeSlab_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
        }
    }
    private string validateGradeSchemeData_old()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvGradeSchemeList.Items)
            {

                TextBox box1 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradePoint");
                //TextBox box3 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtMaxGradePoint");

                if (box1.Text.Trim() == string.Empty)
                {
                    _validate = "Please Enter Grade Name";
                    return _validate;
                }
                else if (box2.Text.Trim() == string.Empty || box2.Text.Trim() == "0")
                {
                    _validate = "Please Enter Grade Point";
                    return _validate;
                }
                //else if (box3.Text.Trim() == string.Empty || box3.Text.Trim() == "0")
                //{
                //    _validate = "Please Enter Max Grade Point";
                //    return _validate;
                //}
                rowIndex += 1;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "Masters.validateGradeSchemeData_old() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }

    private string validateGradeSchemeData()
    {
        string _validate = string.Empty;
        try
        {
            int rowIndex = 0;
            foreach (var item in lvGradeSchemeList.Items)
            {

                TextBox box1 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradePoint");
                //TextBox box3 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtMaxGradePoint");

                if (txtMaxGradePoint.Text.Trim() == string.Empty || txtMaxGradePoint.Text.Trim() == "0")
                {
                    _validate = "Please Enter Max Grade Point";
                    return _validate;
                }
                else if (box1.Text.Trim() == string.Empty)
                {
                    _validate = "Please Enter Grade Name";
                    return _validate;
                }
                else if (box2.Text.Trim() == string.Empty || box2.Text.Trim() == "0")
                {
                    _validate = "Please Enter Grade Point";
                    return _validate;
                }
                else if (!(Convert.ToInt32(box2.Text.Trim()) <= Convert.ToInt32(txtMaxGradePoint.Text.Trim())))
                {
                    _validate = "Please Enter \"Grade Point\" must be less than or equals to \"Max Grade Point\"";
                    box2.Text = "0";
                    return _validate;
                }

                //else if (box3.Text.Trim() == string.Empty || box3.Text.Trim() == "0")
                //{
                //    _validate = "Please Enter Max Grade Point";
                //    return _validate;
                //}

                //if (rowIndex > 0)
                //{
                int rowcheckDuplicateEntryIndex = 0;
                foreach (var checkGName in lvGradeSchemeList.Items)
                {
                    //string ss=((TextBox)checkGName.FindControl("txtGradeName")).Text.Trim();
                    if (rowcheckDuplicateEntryIndex == rowIndex)
                    {
                        continue;
                    }
                    if (box1.Text.Trim().Equals(((TextBox)checkGName.FindControl("txtGradeName")).Text.Trim()))
                    {
                        _validate = "Already exist Grade Name.";
                        box1.Text = "0";
                        //box2.Text = "0";
                        //box3.Text = "0";
                        return _validate;
                    }
                    rowcheckDuplicateEntryIndex += 1;
                }
                rowcheckDuplicateEntryIndex = 0;
                foreach (var checkGPoint in lvGradeSchemeList.Items)
                {
                    //string ss=((TextBox)checkGName.FindControl("txtGradeName")).Text.Trim();
                    if (rowcheckDuplicateEntryIndex == rowIndex)
                    {
                        continue;
                    }
                    if (box2.Text.Trim().Equals(((TextBox)checkGPoint.FindControl("txtGradePoint")).Text.Trim()))
                    {
                        _validate = "Already exist Grade Point.";
                        //box1.Text = "0";
                        box2.Text = "0";
                        //box3.Text = "0";
                        return _validate;
                    }
                    rowcheckDuplicateEntryIndex += 1;
                }
                //rowcheckDuplicateEntryIndex = 0;
                //foreach (var checkGMaxPoint in lvGradeSchemeList.Items)
                //{                    
                //    if (rowcheckDuplicateEntryIndex == rowIndex)
                //    {
                //        continue;
                //    }
                //    if (box3.Text.Trim().Equals(((TextBox)checkGMaxPoint.FindControl("txtMaxGradePoint")).Text.Trim()))
                //    {
                //        _validate = "Already exist Max Grade Point.";                        
                //        box3.Text = "0";
                //        return _validate;
                //    }
                //    rowcheckDuplicateEntryIndex += 1;
                //}

                rowIndex += 1;

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
            {
                _validate = ex.Message;
                objCommon.ShowError(Page, "Masters.validateGradeSchemeData() --> " + ex.Message + " " + ex.StackTrace);
            }
            else
            {
                objCommon.ShowError(Page, "Server Unavailable.");
            }
        }
        return _validate;
    }
    #endregion

    #region Button Submit and Cancel
    private DataTable CreateDatatable_GradeScheme()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "ACD_ADMISSION_GRADEPOINT";
            //[BOARDNO],[QUALIFICATION_LEVEL],[GRADENAME],[GRADEPOINT],[MAXGRADEPOINT]
            dt.Columns.Add("BOARDNO");
            dt.Columns.Add("QUALIFICATION_LEVEL");
            dt.Columns.Add("GRADENAME");
            dt.Columns.Add("GRADEPOINT");
            dt.Columns.Add("MAXGRADEPOINT");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.CreateDatatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }
    private DataTable Add_Datatable_GradeScheme()
    {
        //bool chkGradeFail = false;
        //int RefundPolicyId = Convert.ToInt32(ViewState["GradeSchemeId"]);
        DataTable dt = CreateDatatable_GradeScheme();
        try
        {
            int rowIndex = 0;
            foreach (var item in lvGradeSchemeList.Items)
            {
                DataRow dRow = dt.NewRow();
                TextBox box1 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradeName");
                TextBox box2 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtGradePoint");
                //TextBox box3 = (TextBox)lvGradeSchemeList.Items[rowIndex].FindControl("txtMaxGradePoint");

                //[BOARDNO],[QUALIFICATION_LEVEL],[GRADENAME],[GRADEPOINT],[MAXGRADEPOINT]
                dRow["BOARDNO"] = ddlBoardGrade.SelectedValue;
                dRow["QUALIFICATION_LEVEL"] = ddlQualificationGrade.SelectedValue;

                dRow["GRADENAME"] = box1.Text.Trim();
                dRow["GRADEPOINT"] = Convert.ToDecimal(box2.Text.Trim());
                dRow["MAXGRADEPOINT"] = Convert.ToDecimal(txtMaxGradePoint.Text.Trim());//Convert.ToDecimal(box3.Text.Trim());                

                rowIndex += 1;
                dt.Rows.Add(dRow);

                //Do stuff
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Masters.Add_Datatable_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        return dt;
    }

    protected void btnCancelGrade_Click(object sender, EventArgs e)
    {
        //Session["BoardNo"] = "";
        ViewState["action"] = "add";
        ClearBoardGradeScheme();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue
    }
    protected void btnSubmitGrade_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue

            string _validateGradeSchemeData = validateGradeSchemeData();
            if (_validateGradeSchemeData != string.Empty)
            {
                objCommon.DisplayMessage(_validateGradeSchemeData, this.Page);
                //objCommon.DisplayMessage(Page, _validateGradeSchemeData, this.Page);
                return;
            }

            DataTable dt = Add_Datatable_GradeScheme();
            DataSet ds = new DataSet();
            ds.DataSetName = "ACD_ADMISSION_GRADEPOINTS";
            ds.Tables.Add(dt);
            if (dt.Rows.Count > 0)
            {

                int BoardNo = Convert.ToInt32(ddlBoardGrade.SelectedValue);
                int QualificationLevel = Convert.ToInt32(ddlQualificationGrade.SelectedValue);
                int CountryNo = Convert.ToInt32(ddlCountryGrade.SelectedValue);
                int StateNo = Convert.ToInt32(ddlStateGrade.SelectedValue);

                int ret = 0;

                string displaymsg = "";

                if (ViewState["action"].ToString().Equals("add"))
                {
                    ret = Convert.ToInt32(objAdmMast.InsertBoardGradeScheme(BoardNo, QualificationLevel, ds.GetXml(), CountryNo, StateNo));
                    displaymsg = "Record added successfully.";
                }
                else if (ViewState["action"].ToString().Equals("edit"))
                {
                    ret = Convert.ToInt32(objAdmMast.UpdateBoardGradeScheme(BoardNo, QualificationLevel, ds.GetXml(), CountryNo, StateNo));
                    displaymsg = "Record updated successfully.";
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }

                if (ret == 2)
                {
                    displaymsg = "Record already exist.";
                }
                else if (ret > 0)
                {
                    ClearBoardGradeScheme();
                }
                else
                {
                    displaymsg = "Error!Please Fill Data again";
                }
                objCommon.DisplayMessage(displaymsg, this.Page);

            }
            else
            {
                objCommon.DisplayMessage("Please Fill Board Grade Data.", this.Page);
                return;
            }
        }
        else
            Response.Redirect("~/default.aspx");


    }
    #endregion

    #region ListView Edit Button
    private void BindListView_BoardGradeSchemeDetails()
    {
        try
        {

            DataSet ds = objAdmMast.GetAllBoardGradeSchemeList("", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlBoardGradeList.Visible = true;
                lvBoardGradeList.DataSource = ds;
                lvBoardGradeList.DataBind();

            }
            else
            {

                pnlBoardGradeList.Visible = false;
                lvBoardGradeList.DataSource = null;
                lvBoardGradeList.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_BoardGradeSchemeDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnEditBoardGradeScheme_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_7');</script>", false);//Tab Postback issue

            ImageButton btnEdit = sender as ImageButton;
            string[] commandArgs = btnEdit.CommandArgument.ToString().Split(new char[] { ',' });
            string BoardNo = commandArgs[0];
            string QualificationLevel = commandArgs[1];
            string CountryNo = commandArgs[2];
            string StateNo = commandArgs[3];

            ViewState["action"] = "edit";
            ddlCountryGrade.Enabled = false;
            ddlStateGrade.Enabled = false;
            ddlBoardGrade.Enabled = false;
            ddlQualificationGrade.Enabled = false;

            commonBindExistAndEditBoardGradeSchemeData(Convert.ToInt32(BoardNo), Convert.ToInt32(QualificationLevel), Convert.ToInt32(CountryNo), Convert.ToInt32(StateNo));

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditBoardGradeScheme_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void commonBindExistAndEditBoardGradeSchemeData(int BoardNo, int QualificationLevel, int CountryNo, int StateNo)
    {
        try
        {
            if (BoardNo > 0 && QualificationLevel > 0)
            {
                DataSet ds;

                ds = objAdmMast.GetSingleBoardGradeSchemeInformation(BoardNo, QualificationLevel, CountryNo, StateNo);
                if (ds.Tables.Count == 2 && ds.Tables[0].Rows.Count > 0)
                {
                    txtMaxGradePoint.Text = ds.Tables[0].Rows[0]["MAXGRADEPOINT"].ToString();
                    if (ViewState["action"].ToString().Equals("edit"))
                    {

                        ddlCountryGrade.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                        if (ddlCountryGrade.SelectedIndex > 0)
                        {
                            BindStateGrade();
                        }
                        ddlStateGrade.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                        //if (ddlStateGrade.SelectedIndex > 0)
                        //{
                        //    BindBoardGrade();
                        //}                     
                        ddlQualificationGrade.SelectedValue = ds.Tables[0].Rows[0]["QUALIFICATION_LEVEL"].ToString();
                        if (ddlQualificationGrade.SelectedIndex > 0)
                        {
                            BindBoardGrade();
                        }
                        ddlBoardGrade.SelectedValue = ds.Tables[0].Rows[0]["BOARDNO"].ToString();
                        BindListView_GradeScheme(ds.Tables[1]);

                    }
                    else if (ViewState["action"].ToString().Equals("add"))
                    {

                        BindListView_GradeScheme(ds.Tables[1]);
                    }
                }
                else
                {
                    txtMaxGradePoint.Text = "";
                    SetInitialRow();
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.commonBindExistAndEditBoardGradeSchemeData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    //Grade Scheme List based on Board and Qualification_level DDL 
    private void BindListView_GradeScheme(DataTable dt)
    {
        try
        {

            if (dt != null && dt.Rows.Count > 0)
            {

                pnlGradeSchemeList.Visible = true;
                lvGradeSchemeList.DataSource = dt;// ds;
                lvGradeSchemeList.DataBind();

            }
            else
            {
                txtMaxGradePoint.Text = "";
                SetInitialRow();
                //pnlGradeSchemeList.Visible = false;
                //lvGradeSchemeList.DataSource = null;
                //lvGradeSchemeList.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_GradeScheme() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    private void ClearBoardGradeScheme()
    {
        ViewState["action"] = "add";

        ddlCountryGrade.SelectedIndex = 0;

        ddlStateGrade.Items.Clear();
        //  ddlStateGrade.Items.Insert(0, "Please Select");
        ddlStateGrade.Items.Insert(0, new ListItem("Please Select", "0"));
        ddlStateGrade.SelectedIndex = 0;

        ddlBoardGrade.Items.Clear();
        ddlBoardGrade.Items.Insert(0, "Please Select");
        ddlBoardGrade.SelectedIndex = 0;

        ddlQualificationGrade.SelectedIndex = 0;

        txtMaxGradePoint.Text = "";
        //pnlAddSubjectMaxMarks.Visible = false;
        //lvAddSubjectMaxMarks.DataSource = null;
        //lvAddSubjectMaxMarks.DataBind();
        SetInitialRow();
        BindListView_BoardGradeSchemeDetails();

        ddlCountryGrade.Enabled = true;
        ddlStateGrade.Enabled = true;
        ddlBoardGrade.Enabled = true;
        ddlQualificationGrade.Enabled = true;

    }
    #endregion

    #region Tab-8 Reservation Configuration
    #region Common Dropdown List methods
    private void PopulateDropDownList_ReserConfig()
    {
        try
        {
            DataSet dsDegree = objCommon.FillDropDown("VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT DEGREENO", "DEGREENAME", "ORGANIZATIONID = (SELECT ORGANIZATIONID from REFF) AND ACTIVESTATUS=1", " DEGREENAME");
            BindALLDDL(ref ddlReservationDegree, dsDegree, "DEGREENAME", "DEGREENO");

            DataSet dsMstReservation = objCommon.FillDropDown("ACD_ADMP_RESERVATION", "RESERVATION_ID", "RESERVATION_NAME", "RESERVATION_ID > 0 ", "RESERVATION_ID");

            lstReservation.Items.Clear();
            lstReservation.DataSource = dsMstReservation;
            lstReservation.DataValueField = "RESERVATION_ID";
            lstReservation.DataTextField = "RESERVATION_NAME";
            lstReservation.DataBind();
            lstReservation.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "masters.PopulateDropDownList_ReserConfig() --> " + ex.Message + " " + ex.StackTrace);
        }
    }

    protected void ddlReservationDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["action"] = "add";
            lstReservation.ClearSelection();
            pnlReservation.Visible = false;
            lvReservation.DataSource = null;
            lvReservation.DataBind();

            if (ddlReservationDegree.SelectedIndex > 0)
            {
                BindProgramCode();
            }
            else
            {
                ddlReservationProgCode.Items.Clear();
                ddlReservationProgCode.Items.Insert(0, "Please Select");
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlReservationDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
    }

    protected void ddlReservationProgCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["action"] = "add";
            lstReservation.ClearSelection();
            pnlReservation.Visible = false;
            lvReservation.DataSource = null;
            lvReservation.DataBind();

            if (ddlReservationProgCode.SelectedIndex > 0)
            {
                commonBindExistAndEditReservationData(Convert.ToInt32(ddlReservationDegree.SelectedValue), Convert.ToInt32(ddlReservationProgCode.SelectedValue));
            }

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlReservationDegree_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
    }


    private void BindProgramCode()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VW_ACD_COLLEGE_DEGREE_BRANCH", "DISTINCT BRANCHNO", "LONGNAME AS BRANCHNAME", "DEGREENO=" + ddlReservationDegree.SelectedValue + " AND ORGANIZATIONID =(SELECT ORGANIZATIONID from REFF) AND ACTIVESTATUS=1", " LONGNAME");
            BindALLDDL(ref ddlReservationProgCode, ds, "BRANCHNAME", "BRANCHNO");
            ddlReservationProgCode.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.BindProgramCode() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
    }

    #endregion

    #region BindListView
    private void BindListView_Reservation(int DegreeNo, int BranchNo)
    {
        try
        {
            DataSet ds = objAdmMast.GetAllReservationConfigList(DegreeNo, BranchNo);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ViewState["action"] = "edit";
                pnlReservation.Visible = true;
                lvReservation.DataSource = ds;
                lvReservation.DataBind();

            }
            else
            {
                ViewState["action"] = "add";
                pnlReservation.Visible = false;
                lvReservation.DataSource = null;
                lvReservation.DataBind();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_Reservation() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Reservation ListView Edit
    protected void btnEditReservation_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue

            ImageButton btnEdit = sender as ImageButton;
            DataSet ds;

            string[] commandArgs = btnEdit.CommandArgument.ToString().Split(new char[] { ',' });
            string DegreeNo = commandArgs[0];
            string BranchNo = commandArgs[1];


            ViewState["action"] = "edit";
            ddlReservationDegree.Enabled = false;
            ddlReservationProgCode.Enabled = false;

            commonBindExistAndEditReservationData(Convert.ToInt32(DegreeNo), Convert.ToInt32(BranchNo));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditReservation_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }


    }

    private void commonBindExistAndEditReservationData(int DegreeNo, int BranchNo)
    {
        try
        {
            if (DegreeNo > 0 && BranchNo > 0)
            {
                DataSet ds;

                ds = objAdmMast.GetSingleReservationConfigInformation(DegreeNo, BranchNo);
                if (ds.Tables.Count > 0)
                {
                    /*if (ViewState["action"].ToString().Equals("edit"))
                    {
                        ddlReservationDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREE_NO"].ToString();
                        if (ddlReservationDegree.SelectedIndex > 0)
                        {
                            BindProgramCode();
                        }

                        ddlReservationProgCode.SelectedValue = ds.Tables[0].Rows[0]["BRANCH_NO"].ToString();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (ListItem item in lstReservation.Items)
                            {
                                if (ds.Tables[0].Rows[i]["RESERVATION_ID"].ToString().Equals(item.Value))
                                {
                                    item.Selected = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            foreach (ListItem item in lstReservation.Items)
                            {
                                if (ds.Tables[0].Rows[i]["RESERVATION_ID"].ToString().Equals(item.Value))
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }*/
                    int isUpdate = 0;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        foreach (ListItem item in lstReservation.Items)
                        {
                            if (ds.Tables[0].Rows[i]["RESERVATION_ID"].ToString().Equals(item.Value))
                            {
                                item.Selected = true;
                                isUpdate++;
                            }
                        }
                    }

                    if (isUpdate > 0)
                    {
                        ViewState["action"] = "edit";
                    }
                    else
                    {
                        ViewState["action"] = "add";
                    }

                    txtReservationStartDate.Text = ds.Tables[0].Rows[0]["STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["STARTDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                    txtReservationEndDate.Text = ds.Tables[0].Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(ds.Tables[0].Rows[0]["ENDDATE"].ToString()).ToString("yyyy-MM-dd");//.ToString("dd/MM/yyyy");
                }
                else
                {
                    //DataSet ds = objAdmMast.GetAllMaxMarksSubjectsList(Convert.ToInt32(ddlSubType.SelectedValue));
                    //BindListView_AddSubMaxMarksDetails(ds.Tables[0]);
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.commonBindExistAndEditData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Show, Submit & Cancel Buttons
    protected void btnShowReservation_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReservationProgCode.SelectedIndex > 0)
            {
                BindListView_Reservation(Convert.ToInt32(ddlReservationDegree.SelectedValue), Convert.ToInt32(ddlReservationProgCode.SelectedValue));
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.commonBindExistAndEditData() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
    }

    protected void btnSubmitReservation_Click(object sender, EventArgs e)
    {

        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {

            int DegreeNo = Convert.ToInt32(ddlReservationDegree.SelectedValue);
            int BranchNo = Convert.ToInt32(ddlReservationProgCode.SelectedValue);
            string ReservationIds = string.Empty;
            foreach (ListItem item in lstReservation.Items)
            {
                if (item.Selected == true)
                {
                    ReservationIds += item.Value + ",";
                }
            }
            if (ReservationIds.Contains(','))
            {
                ReservationIds = ReservationIds.Remove(ReservationIds.Length - 1);
            }


            DateTime Startdate = Convert.ToDateTime(txtReservationStartDate.Text.ToString().Trim());
            DateTime Enddate = Convert.ToDateTime(txtReservationEndDate.Text.ToString().Trim());

            //int SubjectNo = Convert.ToInt32(lstSubjectName.SelectedValue); 

            int ret = 0;
            string ReservationDetails = "";
            string displaymsg = "";
            if (ViewState["action"].ToString().Equals("add"))
            {
                ret = Convert.ToInt32(objAdmMast.InsertReservationConfig(DegreeNo, BranchNo, ReservationIds, Startdate, Enddate));
                displaymsg = "Record added successfully.";
            }
            else if (ViewState["action"].ToString().Equals("edit"))
            {
                //if (!string.IsNullOrEmpty(Session["SubjectTypeNo"].ToString()))
                //{
                //int SubjectTypeNo = Convert.ToInt32(Session["SubjectTypeNo"]);
                //ret = Convert.ToInt32(objAdmMast.UpdateSubjectType(SubjectType, SubjectTypeNo, SubjectNo));
                ReservationDetails = objAdmMast.UpdateReservationConfig(DegreeNo, BranchNo, ReservationIds, Startdate, Enddate);
                if (IsAllDigits(ReservationDetails))
                {
                    ret = Convert.ToInt32(ReservationDetails);
                }
                else
                {
                    ret = 3;//for already subject marks entered.
                }

                displaymsg = "Record updated successfully.";
                //}
            }

            if (ret == 3)
            {
                displaymsg = "Already filled Reservation Details against Degree:" + ddlReservationDegree.SelectedItem.Text + " and " + ddlReservationProgCode.SelectedItem.Text + ".";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret == 1)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);

            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }

            ClearReservationConfig();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
        }
        else
            Response.Redirect("~/default.aspx");

    }

    protected void btnCancelReservation_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/default.aspx");
        //Session["SubjectTypeNo"] = "";
        //ViewState["action"] = "add";
        //Response.Redirect(Request.Url.ToString());

        ClearReservationConfig();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_8');</script>", false);//Tab Postback issue
    }
    #endregion

    private void ClearReservationConfig()
    {
        ViewState["action"] = "add";

        ddlReservationDegree.SelectedIndex = 0;
        //BindListView_Reservation();
        pnlReservation.Visible = false;
        lvReservation.DataSource = null;
        lvReservation.DataBind();

        ddlReservationProgCode.Items.Clear();
        ddlReservationProgCode.Items.Insert(0, "Please Select");
        ddlReservationProgCode.SelectedIndex = 0;

        //lstReservation.Items.Clear();
        //lstReservation.Items.Insert(0, "Please Select");
        //lstReservation.SelectedIndex = 0;

        lstReservation.ClearSelection();

        txtReservationStartDate.Text = string.Empty;
        txtReservationEndDate.Text = string.Empty;

        ddlReservationDegree.Enabled = true;
        ddlReservationProgCode.Enabled = true;
        lstReservation.Enabled = true;

    }

    #endregion


    #region Tab-9 Qualifying Degree

    #region ListView
    private void BindListView_QualDegreeDetails()
    {
        try
        {
            DataSet ds = objAdmMast.GetRetAllQualDegreeList(0, "", "");
            ////DataSet ds = GetRetAllQualDegreeList(0, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlQualDegree.Visible = true;
                lvQualDegree.DataSource = ds;
                lvQualDegree.DataBind();


            }
            else
            {

                pnlQualDegree.Visible = false;
                lvQualDegree.DataSource = null;
                lvQualDegree.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvQualDegree.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("InActive"))
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_QualDegreeDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Button Submit and Cancel
    protected void btnCancelQualDegree_Click(object sender, EventArgs e)
    {
        Session["QualDegreeId"] = "";
        ViewState["action"] = "add";
        ClearQualDegree();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
    }
    protected void btnSubmitQualDegree_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
            {
                int QualDegreeId = 0;

                string QualDegreeName = txtQualDegreeName.Text.Trim();
                bool Active = hfdActive.Value == "true" ? true : false;

                int ret = 0;


                string displaymsg = "Record added successfully.";

                if (ViewState["action"].ToString().Equals("edit"))
                {
                    if (!string.IsNullOrEmpty(Session["QualDegreeId"].ToString()))
                    {
                        QualDegreeId = Convert.ToInt32(Session["QualDegreeId"]);
                    }
                    displaymsg = "Record updated successfully.";
                }

                ret = Convert.ToInt32(objAdmMast.InsertUpdateQualDegree(QualDegreeId, QualDegreeName, Active, Convert.ToInt32(Session["userno"])));
                ////ret = Convert.ToInt32(InsertUpdateQualDegree(QualDegreeId, QualDegreeName, Active, Convert.ToInt32(Session["userno"])));

                if (ret == -111)
                {
                    objCommon.DisplayMessage(this, "Can not inactive this record as it is already used in transaction.", this.Page);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
                    return;
                }

                if (ret == 2)
                {
                    displaymsg = "Record already exist.";
                    objCommon.DisplayMessage(displaymsg, this.Page);
                }
                else if (ret > 0)
                {
                    objCommon.DisplayMessage(displaymsg, this.Page);
                    ClearQualDegree();
                }
                else
                {
                    objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
            }
            else
                Response.Redirect("~/default.aspx");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnSubmitQualDegree_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    #endregion

    #region ListView Edit Button
    protected void btnEditQualDegree_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetRetAllQualDegreeList(Convert.ToInt32(id), "", "").Tables[0];
            ////dt = GetRetAllQualDegreeList(Convert.ToInt32(id), "", "").Tables[0];

            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                Session["QualDegreeId"] = id;

                if ((bool)dt.Rows[0]["STATUS"])
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetActive(false);", true);
                }
                ////[QUALI_DEGREE_ID],[QUALI_DEGREE_NAME]
                txtQualDegreeName.Text = dt.Rows[0]["QUALI_DEGREE_NAME"].ToString();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditQualDegree_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_9');</script>", false);
    }
    #endregion

    private void ClearQualDegree()
    {
        ViewState["action"] = "add";
        Session["QualDegreeId"] = "";

        ////ddlProgramType.SelectedIndex = 0;
        ////txtProgramCode.Text = string.Empty;
        txtQualDegreeName.Text = string.Empty;

        BindListView_QualDegreeDetails();
        FillDropDownList();//Tab-10 Program DropDownList Changes
    }

    #endregion

    #region Tab-10 Program
    protected void FillDropDownList()
    {
        ////string COLLEGE_CODE = Session["CollegeId"].ToString();
        //objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "COLLEGE_CODE=51 AND ACTIVESTATUS=1", "UA_SECTION");
        objCommon.FillDropDownList(ddlProgramType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "ACTIVESTATUS=1", "UA_SECTION");//COLLEGE_CODE=72 AND 
        objCommon.FillDropDownList(ddlQualDegree, "ACD_ADMP_QUALIDEGREE", "QUALI_DEGREE_ID", "QUALI_DEGREE_NAME", "STATUS=1", "QUALI_DEGREE_NAME");
    }

    #region ListView
    private void BindListView_ProgramDetails()
    {
        try
        {
            DataSet ds = objAdmMast.GetRetAllProgramList(0, "", "");
            ////DataSet ds = GetRetAllProgramList(0, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlProgram.Visible = true;
                lvProgram.DataSource = ds;
                lvProgram.DataBind();


            }
            else
            {

                pnlProgram.Visible = false;
                lvProgram.DataSource = null;
                lvProgram.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvProgram.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("InActive"))
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_ProgramDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Button Submit and Cancel
    protected void btnCancelProgrm_Click(object sender, EventArgs e)
    {
        Session["ProgId"] = "";
        ViewState["action"] = "add";
        ClearProgram();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
    }
    protected void btnSubmitProgrm_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int ProgId = 0;
            string ProgType = (ddlProgramType.SelectedItem.Text);
            string QualDegree = (ddlQualDegree.SelectedValue);
            string ProgCode = txtProgramCode.Text.Trim();
            string ProgName = txtProgramName.Text.Trim();
            bool Active = hfdPRG_Active.Value == "true" ? true : false;

            int ret = 0;


            string displaymsg = "Record added successfully.";

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["ProgId"].ToString()))
                {
                    ProgId = Convert.ToInt32(Session["ProgId"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objAdmMast.InsertUpdateProgram(ProgId, ProgType, QualDegree, ProgCode, ProgName, Active, Convert.ToInt32(Session["userno"])));
            ////ret = Convert.ToInt32(InsertUpdateProgram(ProgId, ProgType, QualDegree, ProgCode, ProgName, Active, Convert.ToInt32(Session["userno"])));

            if (ret == 2)
            {
                displaymsg = "Record already exist.";
                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearProgram();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
        }
        else
            Response.Redirect("~/default.aspx");

    }
    #endregion

    #region ListView Edit Button
    protected void btnEditProgram_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetRetAllProgramList(Convert.ToInt32(id), "", "").Tables[0];
            ////dt = GetRetAllProgramList(Convert.ToInt32(id), "", "").Tables[0];

            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                Session["ProgId"] = id;

                foreach (ListItem item in ddlProgramType.Items)
                {
                    if (item.Text.Equals(dt.Rows[0]["PROG_TYPE"].ToString()))
                    {
                        ddlProgramType.SelectedValue = item.Value;
                    }
                }

                foreach (ListItem item in ddlQualDegree.Items)
                {
                    if (item.Value.Equals(dt.Rows[0]["QUALI_DEGREE_ID"].ToString()))
                    {
                        ddlQualDegree.SelectedValue = item.Value;
                    }
                }

                txtProgramCode.Text = dt.Rows[0]["PROG_CODE"].ToString();
                txtProgramName.Text = dt.Rows[0]["PROG_NAME"].ToString();
                //ddlProgramType.SelectedItem.Text = dt.Rows[0]["PROG_TYPE"].ToString();

                if ((bool)dt.Rows[0]["STATUS"])
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPRG_Active(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPRG_Active(false);", true);
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditProgram_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_10');</script>", false);
    }
    #endregion

    private void ClearProgram()
    {
        ViewState["action"] = "add";
        Session["ProgId"] = "";

        ddlProgramType.SelectedIndex = 0;
        ddlQualDegree.SelectedIndex = 0;

        txtProgramCode.Text = string.Empty;
        txtProgramName.Text = string.Empty;

        BindListView_ProgramDetails();

    }

    #endregion

     #region Tab-11 Test Master

    protected void FillDropDownList_TestScore()
    {
        DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNAME");
        BindALLDDL(ref ddlAdmBatch, ds, "BATCHNAME", "BATCHNO");
        //objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "UA_SECTION", "UA_SECTIONNAME", "COLLEGE_CODE=51 AND ACTIVESTATUS=1", "UA_SECTION");
        objCommon.FillListBox(lstbxdegreetype, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "ACTIVESTATUS=1", "UA_SECTION");
    }

    #region ListView
    private void BindListView_TestScore()
    {
        try
        {
            ////GetRetAllTestScoreList
            DataSet ds = objAdmMast.GetRetAllTestSccoreList(0, "", "");
            ////DataSet ds = GetRetAllTestSccoreList(0, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                pnlTestScore.Visible = true;
                lvTestScore.DataSource = ds;
                lvTestScore.DataBind();

            }
            else
            {

                pnlTestScore.Visible = false;
                lvTestScore.DataSource = null;
                lvTestScore.DataBind();

            }

            foreach (ListViewDataItem dataitem in lvTestScore.Items)
            {
                Label Status = dataitem.FindControl("lblStatus") as Label;

                string Statuss = (Status.Text);

                if (Statuss.Equals("No"))
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }

            }

            foreach (ListViewDataItem dataitem in lvTestScore.Items)
            {
                Label ActiveStatus = dataitem.FindControl("lblActiveStatus") as Label;

                if (ActiveStatus.Text == "1")
                {
                    ActiveStatus.Text = "Active";
                    ActiveStatus.Style.Add("color", "Green");
                }
                else
                {
                    ActiveStatus.Text = "Inactive";
                    ActiveStatus.Style.Add("color", "Red");
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_TestScore() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Button Submit and Cancel
    protected void btnCancelTestScore_Click(object sender, EventArgs e)
    {
        Session["TestScoreId"] = "";
        ViewState["action"] = "add";
        ClearTestScore();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
    }

    protected void btnSubmitTestScore_Click(object sender, EventArgs e)
    {
        if (ViewState["action"].ToString().Equals("add") || ViewState["action"].ToString().Equals("edit"))
        {
            int userno = Convert.ToInt32(Session["userno"].ToString());
            int TestScoreId = 0;
            string ProgType = (ddlProgramType.SelectedItem.Text);
            string ProgCode = txtProgramCode.Text.Trim();
            string ProgName = txtProgramName.Text.Trim();

            string BatchNo = ddlAdmBatch.SelectedValue;
            string TestName = txtTestName.Text.Trim();
            string MaxScore = txtMaxScore.Text.Trim();
            string Average = txtAverage.Text.Trim();
            string StdDeviation = txtStdDev.Text.Trim();

            DateTime Startdate = Convert.ToDateTime(txtTestScoreStartDate.Text.ToString().Trim());
            DateTime Enddate = Convert.ToDateTime(txtTestScoreEndDate.Text.ToString().Trim());
            
            bool isGateQualify = hfdTST_Active.Value == "true" ? true : false;
            int years = Convert.ToInt32(ddlyears.SelectedValue);
            bool allowTestScoreSubject = chkAllowScoreSubject.Checked;
            int activeStatus;
            if (hfdActiveStatus.Value == "true")
            {
                activeStatus = 1;
            }
            else
            {
                activeStatus = 0;
            }

            string subject1 = txtAddSub1.Text.Trim();
            string subject2 = txtAddSub2.Text.Trim();
            string subject3 = txtAddSub3.Text.Trim();
            string subject4 = txtAddSub4.Text.Trim();
            string subject5 = txtAddSub5.Text.Trim();
            string maxmarks1 = txtAddMaxMarks1.Text.Trim();
            string maxmarks2 = txtAddMaxMarks2.Text.Trim();
            string maxmarks3 = txtAddMaxMarks3.Text.Trim();
            string maxmarks4 = txtAddMaxMarks4.Text.Trim();
            string maxmarks5 = txtAddMaxMarks5.Text.Trim();                         
            int scoreIdSubject1 = Convert.ToInt32(hdnAddSub1.Value);
            int scoreIdSubject2 = Convert.ToInt32(hdnAddSub2.Value);
            int scoreIdSubject3 = Convert.ToInt32(hdnAddSub3.Value);
            int scoreIdSubject4 = Convert.ToInt32(hdnAddSub4.Value);
            int scoreIdSubject5 = Convert.ToInt32(hdnAddSub5.Value);

            int ret = 0;            
            string degreetype = "";
            foreach (ListItem item in lstbxdegreetype.Items)
            {
                if (item.Selected == true)
                {
                    degreetype += item.Value + ',';
                }
            }
            if (!string.IsNullOrEmpty(degreetype))
            {
                degreetype = degreetype.Substring(0, degreetype.Length - 1);
            }
            else
            {
                degreetype = "0";
            }

            #region Table
            //if ((chkAllowScoreSubject.Checked == false) && (txtAddSub1.Text.Trim() != string.Empty || txtAddMaxMarks1.Text.Trim() != string.Empty || txtAddSub2.Text.Trim() != string.Empty || txtAddMaxMarks2.Text.Trim() != string.Empty || txtAddSub3.Text.Trim() != string.Empty || txtAddMaxMarks3.Text.Trim() != string.Empty || txtAddSub4.Text.Trim() != string.Empty || txtAddMaxMarks4.Text.Trim() != string.Empty || txtAddSub5.Text.Trim() != string.Empty || txtAddMaxMarks5.Text.Trim() != string.Empty))
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Check the 'ALLOW SCORESUBJECT' Checkbox To Submit Records of the Table!", this.Page);
            //    return;
            //}

            ////CHECKBOX
            //if (chkAllowScoreSubject.Checked == true && txtAddSub1.Text.Trim() == string.Empty && txtAddMaxMarks1.Text.Trim() == string.Empty && txtAddSub2.Text.Trim() == string.Empty && txtAddMaxMarks2.Text.Trim() == string.Empty && txtAddSub3.Text.Trim() == string.Empty && txtAddMaxMarks3.Text.Trim() == string.Empty && txtAddSub4.Text.Trim() == string.Empty && txtAddMaxMarks4.Text.Trim() == string.Empty && txtAddSub5.Text.Trim() == string.Empty && txtAddMaxMarks5.Text.Trim() == string.Empty)
            //{
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Atleast ONE Record should be Add in the Table to Proceed!');", true);
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
            //    return;
            //}
            //else if (chkAllowScoreSubject.Checked == true && txtAddSub1.Text == string.Empty && txtAddSub2.Text.Trim() == string.Empty && txtAddSub3.Text.Trim() == string.Empty && txtAddSub4.Text.Trim() == string.Empty && txtAddSub5.Text.Trim() == string.Empty)
            //{
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Atleast ONE Record should be Add in the Table to Proceed!');", true);
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
            //    return;
            //}
            //else if (chkAllowScoreSubject.Checked == true && txtAddMaxMarks1.Text.Trim() == string.Empty && txtAddMaxMarks2.Text.Trim() == string.Empty && txtAddMaxMarks3.Text.Trim() == string.Empty && txtAddMaxMarks4.Text.Trim() == string.Empty && txtAddMaxMarks5.Text.Trim() == string.Empty)
            //{
            //    System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Atleast ONE Record should be Add in the Table to Proceed!');", true);
            //    return;
            //}


            ////DUPLICATE RECORDS
            //if ((txtAddSub1.Text!=string.Empty) && (txtAddSub1.Text.ToLower() == txtAddSub2.Text.ToLower() || txtAddSub1.Text.ToLower() == txtAddSub3.Text.ToLower() || txtAddSub1.Text.ToLower() == txtAddSub4.Text.ToLower() || txtAddSub1.Text == txtAddSub5.Text))
            //{

            //    objCommon.DisplayMessage(this.Page, "Record Already Exist For Subject Name " + txtAddSub1.Text + "  !", this.Page);
            //    return;
            //}
            //else if ((txtAddSub2.Text != string.Empty) && (txtAddSub2.Text.ToLower() == txtAddSub3.Text.ToLower() || txtAddSub2.Text.ToLower() == txtAddSub4.Text.ToLower() || txtAddSub2.Text.ToLower() == txtAddSub5.Text.ToLower()))
            //{
            //    objCommon.DisplayMessage(this.Page, "Record Already Exist For Subject Name " + txtAddSub2.Text + "  !", this.Page);
            //    return;
            //}
            //else if ((txtAddSub3.Text != string.Empty) && (txtAddSub3.Text.ToLower() == txtAddSub4.Text.ToLower() || txtAddSub3.Text.ToLower() == txtAddSub5.Text.ToLower()))
            //{
            //    objCommon.DisplayMessage(this.Page, "Record Already Exist For Subject Name " + txtAddSub3.Text + "  !", this.Page);
            //    return;
            //}
            //else if ((txtAddSub4.Text != string.Empty) && (txtAddSub4.Text.ToLower() == txtAddSub5.Text.ToLower()))
            //{
            //    objCommon.DisplayMessage(this.Page, "Record Already Exist For Subject Name " + txtAddSub4.Text + "  !", this.Page);
            //    return;
            //}

            ////NON ZERO
            //if (txtAddMaxMarks1.Text != string.Empty && Convert.ToDecimal(txtAddMaxMarks1.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks Greater Than Zero For Subject " + txtAddSub1.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks2.Text != string.Empty && Convert.ToDecimal(txtAddMaxMarks2.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks Greater Than Zero For Subject " + txtAddSub2.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks3.Text!=string.Empty && Convert.ToDecimal(txtAddMaxMarks3.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks Greater Than Zero For Subject " + txtAddSub3.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks4.Text != string.Empty && Convert.ToDecimal(txtAddMaxMarks4.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks Greater Than Zero For Subject " + txtAddSub4.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks5.Text != string.Empty && Convert.ToDecimal(txtAddMaxMarks5.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks Greater Than Zero For Subject " + txtAddSub5.Text + "  !", this.Page);
            //    return;
            //}


            ////IF DATA CONTAIN IN ONLY ONE COLUMN 
            //else if (txtAddSub1.Text != string.Empty && txtAddMaxMarks1.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks For  " + txtAddSub1.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddSub2.Text != string.Empty && txtAddMaxMarks2.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks For  " + txtAddSub2.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddSub3.Text != string.Empty && txtAddMaxMarks3.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks For  " + txtAddSub3.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddSub4.Text != string.Empty && txtAddMaxMarks4.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks for  " + txtAddSub4.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddSub5.Text != string.Empty && txtAddMaxMarks5.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Marks For  " + txtAddSub5.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks1.Text != string.Empty && txtAddSub1.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Subject Name for  " + txtAddMaxMarks1.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks2.Text != string.Empty && txtAddSub2.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Subject Name For  " + txtAddMaxMarks2.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks3.Text != string.Empty && txtAddSub3.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Subject Name For  " + txtAddMaxMarks3.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks4.Text != string.Empty && txtAddSub4.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Subject Name for  " + txtAddMaxMarks4.Text + "  !", this.Page);
            //    return;
            //}
            //else if (txtAddMaxMarks5.Text != string.Empty && txtAddSub5.Text == string.Empty)
            //{
            //    objCommon.DisplayMessage(this.Page, "Please Enter Subject Name For  " + txtAddMaxMarks5.Text + "  !", this.Page);
            //    return;
            //}
            #endregion 

            string Degreetype = degreetype;

            string displaymsg = "Record added successfully.";
            //BindListView_TestScore();

            if (ViewState["action"].ToString().Equals("edit"))
            {
                if (!string.IsNullOrEmpty(Session["TestScoreId"].ToString()))
                {
                    TestScoreId = Convert.ToInt32(Session["TestScoreId"]);
                }
                displaymsg = "Record updated successfully.";
            }

            ret = Convert.ToInt32(objAdmMast.InsertUpdateTestSccore(TestScoreId, BatchNo, TestName, MaxScore, Average, StdDeviation, Startdate, Enddate, isGateQualify, years, Degreetype, allowTestScoreSubject, subject1, maxmarks1, subject2, maxmarks2, subject3, maxmarks3, subject4, maxmarks4, subject5, maxmarks5, scoreIdSubject1, scoreIdSubject2, scoreIdSubject3, scoreIdSubject4, scoreIdSubject5, activeStatus, userno));
            //ret = Convert.ToInt32(objAdmMast.InsertUpdateTestSccore(TestScoreId, BatchNo, TestName, MaxScore, Average, StdDeviation, Startdate, Enddate, isGateQualify, years, Degreetype, allowTestScoreSubject, ScoreIdSubject, createdBy, createdOn, modifiedBy, modifiedOn));
            ////ret = Convert.ToInt32(InsertUpdateTestSccore(TestScoreId, BatchNo, TestName, MaxScore, Average, StdDeviation, Startdate, Enddate, isGateQualify));
            if (ret == 2)
            {
                displaymsg = "Record already exist.";

                objCommon.DisplayMessage(displaymsg, this.Page);
            }
            else if (ret > 0)
            {
                objCommon.DisplayMessage(displaymsg, this.Page);
                ClearTestScore();
            }
            else
            {
                objCommon.DisplayMessage("Error!Please Fill Data again", this.Page);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
        }
        else
            Response.Redirect("~/default.aspx");   
    }
    #endregion

    #region ListView Edit Button
    protected void btnEditTestScore_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            DataTable dt;

            string id = btnEdit.CommandArgument.ToString();
            dt = objAdmMast.GetRetAllTestSccoreList(Convert.ToInt32(id), "", "").Tables[0];
            ////dt = GetRetAllTestSccoreList(Convert.ToInt32(id), "", "").Tables[0];
            ViewState["action"] = "edit";
            if (dt.Rows.Count > 0)
            {
                //SCOREID ,BATCHNO,TESTNAME,MAXSCORE,AVERAGE,STDDEVIATION
                Session["TestScoreId"] = id;
                ddlAdmBatch.SelectedValue = dt.Rows[0]["BATCHNO"].ToString();
                txtTestName.Text = dt.Rows[0]["TESTNAME"].ToString();
                txtMaxScore.Text = dt.Rows[0]["MAXSCORE"].ToString();
                txtAverage.Text = dt.Rows[0]["AVERAGE"].ToString();
                txtStdDev.Text = dt.Rows[0]["STDDEVIATION"].ToString();

                txtTestScoreStartDate.Text = dt.Rows[0]["STARTDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dt.Rows[0]["STARTDATE"].ToString()).ToString("yyyy-MM-dd");
                txtTestScoreEndDate.Text = dt.Rows[0]["ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dt.Rows[0]["ENDDATE"].ToString()).ToString("yyyy-MM-dd");

                char delimiterChars = ',';
                string degreetype = dt.Rows[0]["DEGREE_TYPE"].ToString();
                string[] stu = degreetype.Split(delimiterChars);
                for (int j = 0; j < stu.Length; j++)
                {
                    for (int i = 0; i < lstbxdegreetype.Items.Count; i++)
                    {
                        if (stu[j].Trim() == lstbxdegreetype.Items[i].Value.Trim())
                        {
                            lstbxdegreetype.Items[i].Selected = true;
                        }
                    }
                }
                if ((bool)dt.Rows[0]["IS_GATE_QUALIFY"])
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTST_Active(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetTST_Active(false);", true);
                }

                if (dt.Rows[0]["ACTIVE_STATUS"].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "Set_ActiveStatus(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src1", "Set_ActiveStatus(false);", true);
                }

                ddlyears.SelectedValue = dt.Rows[0]["NO_OF_YEARS"].ToString();
                chkAllowScoreSubject.Checked = (bool)dt.Rows[0]["ALLOW_TESTSCORE_SUBJECT"];
                
                //txtAddSub1.Text = dt.Rows[0]["SUBJECT_NAME"].ToString();
                //hdnAddSub1.Value = dt.Rows[0]["SCOREIDSUBJECT"].ToString();
                //txtAddMaxMarks1.Text = dt.Rows[0]["MAXMARKS"].ToString();

                //txtAddSub2.Text = dt.Rows[1]["SUBJECT_NAME"].ToString();
                //hdnAddSub2.Value = dt.Rows[1]["SCOREIDSUBJECT"].ToString();
                //txtAddMaxMarks2.Text = dt.Rows[1]["MAXMARKS"].ToString();

                //txtAddSub3.Text = dt.Rows[2]["SUBJECT_NAME"].ToString();
                //hdnAddSub3.Value = dt.Rows[2]["SCOREIDSUBJECT"].ToString();
                //txtAddMaxMarks3.Text = dt.Rows[2]["MAXMARKS"].ToString();

                //txtAddSub4.Text = dt.Rows[3]["SUBJECT_NAME"].ToString();
                //hdnAddSub4.Value = dt.Rows[3]["SCOREIDSUBJECT"].ToString();
                //txtAddMaxMarks4.Text = dt.Rows[3]["MAXMARKS"].ToString();

                //txtAddSub5.Text = dt.Rows[4]["SUBJECT_NAME"].ToString();
                //hdnAddSub5.Value = dt.Rows[4]["SCOREIDSUBJECT"].ToString();
                //txtAddMaxMarks5.Text = dt.Rows[4]["MAXMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.btnEditTestScore_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_11');</script>", false);
    }
    #endregion

    private void ClearTestScore()
    {
        ViewState["action"] = "add";
        Session["TestScoreId"] = "";

        ddlAdmBatch.SelectedIndex = 0;
        txtTestName.Text = string.Empty;
        txtMaxScore.Text = string.Empty;
        txtAverage.Text = string.Empty;
        txtStdDev.Text = string.Empty;
        lstbxdegreetype.ClearSelection();
        txtTestScoreStartDate.Text = string.Empty;
        txtTestScoreEndDate.Text = string.Empty;
        ddlyears.SelectedIndex = 0;
        BindListView_TestScore();
        txtAddSub1.Text = string.Empty;
        txtAddMaxMarks1.Text = string.Empty;
        txtAddSub2.Text = string.Empty;
        txtAddMaxMarks2.Text = string.Empty;
        txtAddSub3.Text = string.Empty;
        txtAddMaxMarks3.Text = string.Empty;
        txtAddSub4.Text = string.Empty;
        txtAddMaxMarks4.Text = string.Empty;
        txtAddSub5.Text = string.Empty;
        txtAddMaxMarks5.Text = string.Empty;

    }

    #endregion

    #region Tab-12 GATE - NON GATE

    #region DropDownList Bind and SelectedIndex
    protected void FillDropDownList_NonGate()
    {
        //DataSet ds = objCommon.FillDropDown("ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0 AND ACTIVESTATUS = 1", "BATCHNAME");
        //BindALLDDL(ref ddlAdmBatch, ds, "BATCHNAME", "BATCHNO");

        ////objCommon.FillDropDownList(ddlNonGateDegreMapping, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON (D.DEGREENO= DB.DEGREENO) INNER JOIN ACD_BRANCH B ON(DB.BRANCHNO=B.BRANCHNO)", "CAST(DB.DEGREENO AS NVARCHAR(10))+'#'+ CAST(DB.BRANCHNO AS NVARCHAR(10)) AS DEGREENO_BRANCHNO", "D.DEGREENAME+' - '+B.LONGNAME AS DEGREENAME_BRANCHNAME", "DB.ACTIVESTATUS=1", "");
        objCommon.FillDropDownList(ddlNonGateDegreMapping, "VW_ACD_COLLEGE_DEGREE_BRANCH", "CAST(DEGREENO AS NVARCHAR(10))+'#'+ CAST(BRANCHNO AS NVARCHAR(10)) AS DEGREENO_BRANCHNO", "DEGREENAME+' - '+LONGNAME AS DEGREENAME_BRANCHNAME", "ACTIVESTATUS=1", "");
    }

    protected void ddlGateSubjectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlGateDegreeMapping.Visible = false;
            lvGateDegreeMapping.DataSource = null;
            lvGateDegreeMapping.DataBind();

            if (ddlGateSubjectCode.SelectedIndex > 0)
            {
                BindListView_Gate(ddlGateSubjectCode.SelectedItem.Text);
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlGateSubjectCode_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);//Tab Postback issue
    }
    protected void ddlNonGateDegreMapping_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            pnlNonGateDegreeMapping.Visible = false;
            lvNonGateDegreeMapping.DataSource = null;
            lvNonGateDegreeMapping.DataBind();
            if (ddlNonGateDegreMapping.SelectedIndex > 0)
            {
                BindListView_NonGate();
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Masters.ddlNonGateDegreMapping_SelectedIndexChanged() --> " + ex.Message + " " + ex.StackTrace);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);//Tab Postback issue
    }
    #endregion

    #region ListView
    private void BindListView_Gate(string GateSubjectCode)
    {
        try
        {
            DataSet ds = objAdmMast.GetAll_GATE_DataList(GateSubjectCode, "", "");
            ////DataSet ds = GetAll_GATE_DataList(GateSubjectCode, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlGateDegreeMapping.Visible = true;
                lvGateDegreeMapping.DataSource = ds;
                lvGateDegreeMapping.DataBind();
            }
            else
            {
                pnlGateDegreeMapping.Visible = false;
                lvGateDegreeMapping.DataSource = null;
                lvGateDegreeMapping.DataBind();
            }

            if (lvGateDegreeMapping.Items.Count > 0)
            {
                ViewState["OldCheckedGateNonGateList"] = null;

                int i = 0;
                foreach (ListViewDataItem lv in lvGateDegreeMapping.Items)
                {
                    CheckBox chkGateIsActive = lv.FindControl("chkGateIsActive") as CheckBox;
                    HiddenField hfdGateDegreeBranchId = lv.FindControl("hfdGateDegreeBranchId") as HiddenField;

                    if (chkGateIsActive.Checked)
                    {
                        CompaireGate_NonDegreeMappingEntity objCSDMEE = new CompaireGate_NonDegreeMappingEntity();
                        objCSDMEE.Degreeno_Programno = hfdGateDegreeBranchId.Value;

                        lstCSDME.Insert(i, objCSDMEE);

                    }
                }
                if (lstCSDME.Count > 0)
                {
                    ViewState["OldCheckedGateNonGateList"] = lstCSDME;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_Gate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void BindListView_NonGate()
    {
        try
        {
            string[] commandArgs = ddlNonGateDegreMapping.SelectedValue.ToString().Split(new char[] { '#' });
            string DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
            string BRANCHNO = commandArgs[1];

            DataSet ds = objAdmMast.GetAll_Non_GATE_DataList(DEGREENO, BRANCHNO, "", "");
            ////DataSet ds = GetAll_Non_GATE_DataList(DEGREENO, BRANCHNO, "", "");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlNonGateDegreeMapping.Visible = true;
                lvNonGateDegreeMapping.DataSource = ds;
                lvNonGateDegreeMapping.DataBind();
            }
            else
            {
                pnlNonGateDegreeMapping.Visible = false;
                lvNonGateDegreeMapping.DataSource = null;
                lvNonGateDegreeMapping.DataBind();
            }


            if (lvNonGateDegreeMapping.Items.Count > 0)
            {
                ViewState["OldCheckedGateNonGateList"] = null;

                int i = 0;
                foreach (ListViewDataItem lv in lvNonGateDegreeMapping.Items)
                {
                    CheckBox chkNonGateIsActive = lv.FindControl("chkNonGateIsActive") as CheckBox;
                    HiddenField hfdNonGateDegreeProgramId = lv.FindControl("hfdNonGateDegreeProgramId") as HiddenField;
                    ////TextBox txtSequance = lv.FindControl("txtSequance") as TextBox;

                    //if (hfdStageId.Value != null && txtSequance.Text.Trim().Length > 0)
                    //{
                    //    if (Convert.ToInt32(hfdStageId.Value) > 0 && Convert.ToInt32(txtSequance.Text.Trim()) > 0)
                    //    {
                    if (chkNonGateIsActive.Checked)
                    {
                        CompaireGate_NonDegreeMappingEntity objCSDMEE = new CompaireGate_NonDegreeMappingEntity();
                        objCSDMEE.Degreeno_Programno = hfdNonGateDegreeProgramId.Value;

                        lstCSDME.Insert(i, objCSDMEE);

                        ////chkIsActive.Checked = true;
                        ////i++;
                    }
                    //}
                }
                if (lstCSDME.Count > 0)
                {
                    ViewState["OldCheckedGateNonGateList"] = lstCSDME;
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "masters.BindListView_NonGate() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Save and cancel Button
    protected void btnSubmitGate_NonGateMapping_Click(object sender, System.EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);

            ////if (chk_Gate_NonGate.Checked == true)
            if (chk_Gate_NonGate.SelectedValue.Equals("1"))
            {
                //ddlGateSubjectCode.SelectedIndex = 0;
                opr_For_Gate();
            }
            else
            {
                opr_For_Non_Gate();
            }


            clearGate_NonGateMapping();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void opr_For_Gate()
    {
        try
        {
            ////int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            ////int DegreeNo = Convert.ToInt32(ddlDegremapping.SelectedValue);

            string displaymsg = "";
            int rowIndex = 0;
            int countForCheked = 0;
            string _validate = string.Empty;
            // validation methods
            foreach (ListViewDataItem lv in lvGateDegreeMapping.Items)
            {
                ////HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField; ;
                ////CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;

                CheckBox chkGateIsActive = lv.FindControl("chkGateIsActive") as CheckBox;
                HiddenField hfdGateDegreeBranchId = lv.FindControl("hfdGateDegreeBranchId") as HiddenField;

                if (chkGateIsActive.Checked)
                {
                    countForCheked++;
                }

                rowIndex += 1;
            }

            if (countForCheked <= 0)
            {
                objCommon.DisplayMessage(this, "Please Select Degree Mapping..!", this.Page);
                return;
            }

            // save operation methods
            foreach (ListViewDataItem lv in lvGateDegreeMapping.Items)
            {
                ////HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField; ;
                ////CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;
                CheckBox chkGateIsActive = lv.FindControl("chkGateIsActive") as CheckBox;
                HiddenField hfdGateDegreeBranchId = lv.FindControl("hfdGateDegreeBranchId") as HiddenField;

                if (ViewState["OldCheckedGateNonGateList"] != null)
                {
                    lstCSDME = (List<CompaireGate_NonDegreeMappingEntity>)ViewState["OldCheckedGateNonGateList"];
                    if (lstCSDME != null && lstCSDME.Count > 0)
                    {
                        foreach (CompaireGate_NonDegreeMappingEntity objCSDME in lstCSDME)
                        {
                            if (objCSDME.Degreeno_Programno.Equals(hfdGateDegreeBranchId.Value) && !chkGateIsActive.Checked)
                            {
                                //delete from table unselected(Old) Mapping for Branch and Degree
                                string[] commandArgs = hfdGateDegreeBranchId.Value.ToString().Split(new char[] { '#' });
                                string DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                                string BRANCHNO = commandArgs[1];

                                DeleteStgeDegreeMap(ddlGateSubjectCode.SelectedItem.Text, true, DEGREENO, BRANCHNO, null, null);
                            }

                        }
                    }
                }


                if (chkGateIsActive.Checked && Convert.ToInt32(hfdGateDegreeBranchId.Value.ToString().Length) > 0)
                {
                    if (ViewState["OldCheckedGateNonGateList"] != null)
                    {
                        lstCSDME = (List<CompaireGate_NonDegreeMappingEntity>)ViewState["OldCheckedGateNonGateList"];
                        if (lstCSDME != null && lstCSDME.Count > 0)
                        {
                            bool isFindExistTableRecord = false;
                            foreach (CompaireGate_NonDegreeMappingEntity objCSDME in lstCSDME)
                            {
                                if (objCSDME.Degreeno_Programno.Equals(hfdGateDegreeBranchId.Value))
                                {
                                    isFindExistTableRecord = true;
                                    continue;
                                }
                            }
                            if (!isFindExistTableRecord)
                            {
                                //skp again insertion 
                                string[] commandArgs = hfdGateDegreeBranchId.Value.ToString().Split(new char[] { '#' });
                                string DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                                string BRANCHNO = commandArgs[1];

                                SaveStageDegreeMap(ddlGateSubjectCode.SelectedItem.Text, true, DEGREENO, BRANCHNO, null, null);
                            }
                        }
                    }
                    else
                    {
                        //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;
                        string[] commandArgs = hfdGateDegreeBranchId.Value.ToString().Split(new char[] { '#' });
                        string DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                        string BRANCHNO = commandArgs[1];

                        SaveStageDegreeMap(ddlGateSubjectCode.SelectedItem.Text, true, DEGREENO, BRANCHNO, null, null);

                    }
                }

            }

            displaymsg = "Record added successfully.";
            objCommon.DisplayMessage(this, displaymsg, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    private void opr_For_Non_Gate()
    {
        try
        {
            ////int BatchNo = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            ////int DegreeNo = Convert.ToInt32(ddlDegremapping.SelectedValue);

            string displaymsg = "";
            int rowIndex = 0;
            int countForCheked = 0;
            string _validate = string.Empty;
            // validation methods
            foreach (ListViewDataItem lv in lvNonGateDegreeMapping.Items)
            {
                ////HiddenField hfdStageId = lv.FindControl("hfdStageId") as HiddenField; ;
                ////CheckBox chkIsActive = lv.FindControl("chkIsActive") as CheckBox;

                CheckBox chkNonGateIsActive = lv.FindControl("chkNonGateIsActive") as CheckBox;
                HiddenField hfdNonGateDegreeProgramId = lv.FindControl("hfdNonGateDegreeProgramId") as HiddenField;

                if (chkNonGateIsActive.Checked)
                {
                    countForCheked++;
                }

                rowIndex += 1;
            }

            if (countForCheked <= 0)
            {
                objCommon.DisplayMessage(this, "Please Select Degree Mapping..!", this.Page);
                return;
            }

            // save operation methods
            foreach (ListViewDataItem lv in lvNonGateDegreeMapping.Items)
            {

                CheckBox chkNonGateIsActive = lv.FindControl("chkNonGateIsActive") as CheckBox;
                HiddenField hfdNonGateDegreeProgramId = lv.FindControl("hfdNonGateDegreeProgramId") as HiddenField;

                if (ViewState["OldCheckedGateNonGateList"] != null)
                {
                    lstCSDME = (List<CompaireGate_NonDegreeMappingEntity>)ViewState["OldCheckedGateNonGateList"];
                    if (lstCSDME != null && lstCSDME.Count > 0)
                    {
                        foreach (CompaireGate_NonDegreeMappingEntity objCSDME in lstCSDME)
                        {
                            if (objCSDME.Degreeno_Programno.Equals(hfdNonGateDegreeProgramId.Value) && !chkNonGateIsActive.Checked)
                            {
                                string[] commandArgss = ddlNonGateDegreMapping.SelectedValue.ToString().Split(new char[] { '#' });
                                string DEGREENO = commandArgss[0];////DEGREENO, int BRANCHNO
                                string BRANCHNO = commandArgss[1];

                                //delete from table unselected(Old) Mapping for Branch and Degree
                                string[] commandArgs = hfdNonGateDegreeProgramId.Value.ToString().Split(new char[] { '#' });
                                string QUALI_DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                                string QUALI_PROG_NO = commandArgs[1];

                                DeleteStgeDegreeMap(null, false, DEGREENO, BRANCHNO, QUALI_DEGREENO, QUALI_PROG_NO);
                            }

                        }
                    }

                }


                if (chkNonGateIsActive.Checked && Convert.ToInt32(hfdNonGateDegreeProgramId.Value.ToString().Length) > 0)
                {
                    if (ViewState["OldCheckedGateNonGateList"] != null)
                    {
                        lstCSDME = (List<CompaireGate_NonDegreeMappingEntity>)ViewState["OldCheckedGateNonGateList"];
                        if (lstCSDME != null && lstCSDME.Count > 0)
                        {
                            bool isFindExistTableRecord = false;
                            foreach (CompaireGate_NonDegreeMappingEntity objCSDME in lstCSDME)
                            {
                                if (objCSDME.Degreeno_Programno.Equals(hfdNonGateDegreeProgramId.Value))
                                {
                                    isFindExistTableRecord = true;
                                    continue;
                                }
                            }
                            if (!isFindExistTableRecord)
                            {
                                //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;                    
                                string[] commandArgss = ddlNonGateDegreMapping.SelectedValue.ToString().Split(new char[] { '#' });
                                string DEGREENO = commandArgss[0];////DEGREENO, int BRANCHNO
                                string BRANCHNO = commandArgss[1];

                                string[] commandArgs = hfdNonGateDegreeProgramId.Value.ToString().Split(new char[] { '#' });
                                string QUALI_DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                                string QUALI_PROG_NO = commandArgs[1];

                                SaveStageDegreeMap(null, false, DEGREENO, BRANCHNO, QUALI_DEGREENO, QUALI_PROG_NO);
                            }
                        }
                    }
                    else
                    {
                        //HiddenField hfdIdNo = lv.FindControl("hfdIdNo") as HiddenField;                    
                        string[] commandArgss = ddlNonGateDegreMapping.SelectedValue.ToString().Split(new char[] { '#' });
                        string DEGREENO = commandArgss[0];////DEGREENO, int BRANCHNO
                        string BRANCHNO = commandArgss[1];

                        string[] commandArgs = hfdNonGateDegreeProgramId.Value.ToString().Split(new char[] { '#' });
                        string QUALI_DEGREENO = commandArgs[0];////DEGREENO, int BRANCHNO
                        string QUALI_PROG_NO = commandArgs[1];

                        SaveStageDegreeMap(null, false, DEGREENO, BRANCHNO, QUALI_DEGREENO, QUALI_PROG_NO);
                    }
                }

            }

            displaymsg = "Record added successfully.";
            objCommon.DisplayMessage(this, displaymsg, this.Page);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ClubStudentMapping.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    private void SaveStageDegreeMap(string GATE_SUB_CODE, bool IS_GATE_QUALIFY,
            string DEGREENO, string BRANCHNO, string QUALI_DEGREE_ID, string PROG_ID)
    {
        try
        {
            string ret = string.Empty;
            //displaymsg = "Record added successfully.";
            ret = objAdmMast.InsertUpdateGate_NonGate(GATE_SUB_CODE, IS_GATE_QUALIFY, DEGREENO, BRANCHNO, QUALI_DEGREE_ID, PROG_ID, Convert.ToInt32(Session["userno"]));
            ////ret = InsertUpdateGate_NonGate(GATE_SUB_CODE, IS_GATE_QUALIFY, DEGREENO, BRANCHNO, QUALI_DEGREE_ID, PROG_ID, Convert.ToInt32(Session["userno"]));

            //objCommon.DisplayMessage(displaymsg, this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StagesDegreeMapping.SaveStageDegreeMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    private void DeleteStgeDegreeMap(string GATE_SUB_CODE, bool IS_GATE_QUALIFY,
            string DEGREENO, string BRANCHNO, string QUALI_DEGREE_ID, string PROG_ID)
    {
        try
        {
            int ret = 0;
            ret = objAdmMast.DeleteGate_NonGate(GATE_SUB_CODE, IS_GATE_QUALIFY, DEGREENO, BRANCHNO, QUALI_DEGREE_ID, PROG_ID);
            ////ret = DeleteGate_NonGate(GATE_SUB_CODE, IS_GATE_QUALIFY, DEGREENO, BRANCHNO, QUALI_DEGREE_ID, PROG_ID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "StageDegreeMapping.DeleteStgeDegreeMap --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }
    protected void bntCancelGate_NonGateMapping_Click(object sender, EventArgs e)
    {
        clearGate_NonGateMapping();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
    }
    public void clearGate_NonGateMapping()
    {
        ViewState["action"] = "add";

        ////chk_Gate_NonGate.Checked = true;
        chk_Gate_NonGate.SelectedValue = "1";

        ddlGateSubjectCode.SelectedIndex = 0;
        pnlGateDegreeMapping.Visible = false;
        lvGateDegreeMapping.DataSource = null;
        lvGateDegreeMapping.DataBind();

        ddlNonGateDegreMapping.SelectedIndex = 0;
        pnlNonGateDegreeMapping.Visible = false;
        lvNonGateDegreeMapping.DataSource = null;
        lvNonGateDegreeMapping.DataBind();

        ////span_Gate_NonGate.InnerText = "GATE";
        visibleGateControls(true);
        visibleNonGateControls(false);

        ViewState["OldCheckedGateNonGateList"] = null;
    }
    #endregion


    protected void chk_Gate_NonGate_Changed(object sender, EventArgs e)
    {
        //if (chk_Gate_NonGate.Checked == true)
        //{
        if (chk_Gate_NonGate.SelectedValue.Equals("1"))
        {
            ddlNonGateDegreMapping.SelectedIndex = 0;
            pnlNonGateDegreeMapping.Visible = false;
            lvNonGateDegreeMapping.DataSource = null;
            lvNonGateDegreeMapping.DataBind();

            ////span_Gate_NonGate.InnerText = "GATE";
            visibleGateControls(true);
            visibleNonGateControls(false);
        }
        else
        {
            ddlGateSubjectCode.SelectedIndex = 0;
            pnlGateDegreeMapping.Visible = false;
            lvGateDegreeMapping.DataSource = null;
            lvGateDegreeMapping.DataBind();

            ////span_Gate_NonGate.InnerText = "NON GATE";
            visibleGateControls(false);
            visibleNonGateControls(true);
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> TabShow('tab_12');</script>", false);
    }

    protected void visibleGateControls(bool isGateVisible)
    {
        div_Gate_SubjectCode.Visible = isGateVisible;

    }

    protected void visibleNonGateControls(bool isNonGateVisible)
    {
        div_NonGate_DegreeBranch.Visible = isNonGateVisible;
    }

    //Gate
    //pnlGateDegreeMapping,lvGateDegreeMapping,hfdGateDegreeBranchId(DEGREE_BRANCH_ID),chkGateIsActive,(DEGREE_BRANCH)

    //Non Gate
    //pnlNonGateDegreeMapping,lvNonGateDegreeMapping,hfdNonGateDegreeProgramId(QUALI_DEGREE_PROGRAM_ID),chkNonGateIsActive,(QUALI_DEGREE_PROGRAM)


    List<CompaireGate_NonDegreeMappingEntity> lstCSDME = new List<CompaireGate_NonDegreeMappingEntity>();
    CompaireGate_NonDegreeMappingEntity objCSDME = new CompaireGate_NonDegreeMappingEntity();

    [Serializable]
    public class CompaireGate_NonDegreeMappingEntity
    {
        private int batchno = 0;
        private string degreeno_programno;
        private int gateID = 0;
        private int sequanceno = 0;

        public int Batchno
        {
            get { return batchno; }
            set { batchno = value; }
        }
        public string Degreeno_Programno
        {
            get { return degreeno_programno; }
            set { degreeno_programno = value; }
        }
        public int GateID
        {
            get { return gateID; }
            set { gateID = value; }
        }

    }

    #endregion


}
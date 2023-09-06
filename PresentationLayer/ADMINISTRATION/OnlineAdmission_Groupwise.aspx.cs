using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

public partial class ADMINISTRATION_OnlineAdmission_Groupwise : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    OnlineAdmissionGroupwiseController objOAC = new OnlineAdmissionGroupwiseController();
    string SP_name = string.Empty; string SP_parameters = string.Empty; string SP_value = string.Empty; int admType = 0;

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
            if (Session["userno"] == null || Session["username"] == null ||
              Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            else
            {
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)

                    if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4") || Session["OrgId"].ToString().Equals("5") || Session["OrgId"].ToString().Equals("7"))
                    {
                        divSchool.Visible = true;
                    }
                    else
                    {
                        divSchool.Visible = false;
                    }

                PopulatedropDown();

                this.BindListView();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["action"] = "add";
                BindList_NRI();
            }

        }

    }

    #region tab1
    private void BindListView()
    {

        try
        {
            DataSet ds = objOAC.GetAllConfig();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lvConfiguration.DataSource = ds;
                lvConfiguration.DataBind();
                lvConfiguration.Visible = true;
            }
            else
            {
                lvConfiguration.DataSource = null;
                lvConfiguration.DataBind();
                lvConfiguration.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "OnlineAdmission.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    private void PopulatedropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlgroup, "ACD_ADMISSION_CONFIG_GROUP", "GROUPNO", "GROUP_NAME", "GROUPNO>0", "GROUPNO");
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");
            objCommon.FillDropDownList(ddlAdmType, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO != 3", "IDTYPENO");
            DataSet dsDropDown_NRI = null;
            SP_name = "PKG_ACD_GET_DROPDOWN_ADM_CONFIG";
            SP_parameters = "@P_ADMTYPE";
            SP_value = "" + admType + "";
            dsDropDown_NRI = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_value);
            if (dsDropDown_NRI.Tables.Count > 0)
            {
                if (dsDropDown_NRI.Tables[0].Rows.Count > 0)
                {
                    ddlAdmBatch_NRI.Items.Clear();
                    ddlAdmBatch_NRI.Items.Add(new ListItem("Please Select", "0"));
                    ddlAdmBatch_NRI.DataSource = dsDropDown_NRI.Tables[0];
                    ddlAdmBatch_NRI.DataTextField = "BATCHNAME";
                    ddlAdmBatch_NRI.DataValueField = "BATCHNO";
                    ddlAdmBatch_NRI.DataBind();
                }
            }
            ViewState["ddlAdmType"] = dsDropDown_NRI.Tables[1];
            ViewState["ddlProgrammeType"] = dsDropDown_NRI.Tables[2];
        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        int check = rdobtnnri.SelectedIndex;
        txtApplicationFee.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        objCommon.FillListBox(lstDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "UGPGOT = " + ddlProgramme.SelectedValue + " AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "DEGREENO");
        rdobtnnri.SelectedIndex = check;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int form_category = 0;
        //  int collegeid = 0;
        int ugpg = 0;
        DateTime dt = DateTime.Now;
        DateTime dtEnd = Convert.ToDateTime(txtEndDate.Text);
        dtEnd = dtEnd + DateTime.Now.TimeOfDay;

        //seconds = dt.Second;
        string t1 = dt.ToString("H:mm");

        string STime = string.Empty;
        string ETime = string.Empty;
        double nrifees = 0;
        if ((txtStartDate.Text != string.Empty) && (txtEndDate.Text != string.Empty))
        {

            int rest = DateTime.Compare(dt, dtEnd);

            if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                if (dtEnd.ToString("yyyyMMdd") == dt.ToString("yyyyMMdd"))
                {
                    if (Convert.ToDateTime(txtEndTime.Text) < Convert.ToDateTime(t1))
                    {
                        objCommon.DisplayMessage(this.updNotify, "End Time Should be Greater than Current time", this.Page);
                        return;
                    }
                }
                else
                {
                    objCommon.DisplayMessage(this.updNotify, "End Date should be greater than Start Date", this.Page);
                    return;
                }
            }
            if (Convert.ToDateTime(txtStartTime.Text) < dt)
            {
                if (Convert.ToDateTime(txtStartTime.Text) < Convert.ToDateTime(t1))
                {
                    objCommon.DisplayMessage(this.updNotify, "Start Time Should be Greater than Current time", this.Page);
                    return;
                }
            }

            IITMS.UAIMS.BusinessLayer.BusinessEntities.Config objConfig = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Config();
            objConfig.Admbatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            string DegreeBranchno = string.Empty;
            foreach (ListItem item in lstbranch.Items)
            {
                if (item.Selected == true)
                {
                    DegreeBranchno += item.Value + ",";
                }
            }
            if (DegreeBranchno.Contains(","))
            {
                DegreeBranchno = DegreeBranchno.Remove(DegreeBranchno.Length - 1);
            }

            objConfig.College_Id = Convert.ToInt32(ddlSchool.SelectedValue);
            objConfig.Config_SDate = Convert.ToDateTime(txtStartDate.Text);
            objConfig.Config_EDate = Convert.ToDateTime(txtEndDate.Text);
            objConfig.AdmType = Convert.ToInt32(ddlAdmType.SelectedValue);

            STime = txtStartTime.Text;
            ETime = txtEndTime.Text;
            objConfig.Details = txtDetails.Text.Trim();
            objConfig.Fees = Convert.ToDouble(txtApplicationFee.Text.Trim());
            ugpg = Convert.ToInt32(ddlProgramme.SelectedValue);
            form_category = Convert.ToInt32(ddlCategory.SelectedValue);

            objConfig.Age = Convert.ToInt32(txtAge.Text.Trim());
            int Active = chkStatus.Checked ? Active = 1 : Active = 0;
            int groupno = 0;
            groupno = Convert.ToInt32(ddlgroup.SelectedValue);
            int is_nri = Convert.ToInt32(rdobtnnri.SelectedValue);
            if (rdobtnnri.SelectedValue == "1")
            {
                nrifees = Convert.ToDouble(txteqvinr.Text);
            }

            //check Edit or Insert
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string ConfigID = Convert.ToString(ViewState["ConfigID"]);
                CustomStatus cs = (CustomStatus)objOAC.UpdateOnline(objConfig, form_category, STime, ETime, ugpg, Active, ConfigID, DegreeBranchno, groupno, Convert.ToInt32(Session["OrgId"]), is_nri, nrifees);

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updNotify, "Notification Updated Successfully.", this.Page);
                    ViewState["action"] = "add";
                    ClearControls();

                }
                else if (cs.Equals(CustomStatus.TransactionFailed))
                {
                    objCommon.DisplayMessage(this.updNotify, "Failed To Update Notification.", this.Page);

                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(this.updNotify, "Can not Update Selected record is already in process.", this.Page);
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updNotify, "Record already exist for selected criteria", this.Page);
                }
            }
            else
            {
                CustomStatus cs = (CustomStatus)objOAC.AddOnline(objConfig, form_category, STime, ETime, ugpg, Convert.ToInt32(Session["OrgId"]), Active, DegreeBranchno, groupno, is_nri, nrifees);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    ViewState["test"] = "save";
                    objCommon.DisplayMessage(this.updNotify, "Notification Saved Successfully.", this.Page);
                    ClearControls();
                    ViewState["test"] = null;
                }
                else if (cs.Equals(CustomStatus.Error))
                {
                    objCommon.DisplayMessage(this.updNotify, "Failed To Save Record ", this.Page);
                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayMessage(this.updNotify, "Record already exist for selected criteria", this.Page);
                }
            }
        }
        this.BindListView();
        rdobtnnri.Enabled = false;
    }

    protected void lstbxDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int check = rdobtnnri.SelectedIndex;
        lstbranch.Items.Clear();
        if (lstDegree.Items.Count > 0)
        {
            txtApplicationFee.Text = string.Empty;
            txtDetails.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtStartTime.Text = string.Empty;
            Bindbranch();

        }
        rdobtnnri.SelectedIndex = check;
    }

    private void Bindbranch()
    {

        string Degree = string.Empty;
        foreach (ListItem item in lstDegree.Items)
        {
            if (item.Selected)
            {
                Degree += item.Value + ",";
            }
        }
        if (Degree.Length > 0)
        {
            Degree = Degree.Substring(0, (Degree.Length - 1));
            lstbranch.Items.Clear();

            if (ddlAdmType.SelectedIndex == 2)
            {
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE AD ON AD.DEGREENO=CB.DEGREENO", "DISTINCT CONCAT_WS( '-',AD.DEGREENO, B.BRANCHNO) BRANCHNO", "AD.CODE + ' - '+ LONGNAME as LONGNAME", "DEGREENO IN(" + Degree + ") AND ISNULL(CB.ADM_TYPE,0) = 1", "BRANCHNO");
            }
            else
            {
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.BRANCHNO=B.BRANCHNO INNER JOIN ACD_DEGREE AD ON AD.DEGREENO=CB.DEGREENO", "DISTINCT CONCAT_WS( '-',AD.DEGREENO, B.BRANCHNO) BRANCHNO", "AD.CODE + ' - '+ LONGNAME as LONGNAME", "AD.DEGREENO IN(" + Degree + ")", "BRANCHNO");
                lstbranch.DataSource = ds;
            }

            lstbranch.DataValueField = "BRANCHNO";
            lstbranch.DataTextField = "LONGNAME";
        }
        lstbranch.DataBind();
        lstbranch.SelectedIndex = -1;

    }

    private void ShowDetails(string ConfigID)
    {
        try
        {
            SqlDataReader dr = objOAC.GetSingleConfig(ConfigID);

            if (dr != null)
            {
                if (dr.Read())
                {
                    objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0", "BATCHNO DESC");

                    ddlAdmBatch.SelectedValue = dr["ADMBATCH"] == null ? "0" : dr["ADMBATCH"].ToString();
                    objCommon.FillDropDownList(ddlProgramme, "ACD_UA_SECTION C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.UGPGOT=C.UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");
                    ddlProgramme.SelectedValue = dr["UGPGOT"] == null ? "0" : dr["UGPGOT"].ToString();
                    objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.COLLEGE_ID=C.COLLEGE_ID ", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID>0  AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "C.COLLEGE_NAME");
                    ddlSchool.SelectedValue = dr["COLLEGE_ID"] == null ? "0" : dr["COLLEGE_ID"].ToString();
                    if (ddlSchool.SelectedValue != "0")
                    {
                        objCommon.FillListBox(lstDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "UGPGOT = " + ddlProgramme.SelectedValue + " AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "DEGREENO");
                    }
                    else
                    {
                        objCommon.FillListBox(lstDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "UGPGOT = " + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENO");
                    }
                    string degreeno = dr["DEGREENO"] == null ? "0" : dr["DEGREENO"].ToString();
                    char delimiterChars = ',';

                    string[] stu = degreeno.Split(delimiterChars);
                    for (int j = 0; j < stu.Length; j++)
                    {
                        for (int i = 0; i < lstDegree.Items.Count; i++)
                        {
                            if (stu[j].Trim() == lstDegree.Items[i].Value.Trim())
                            {
                                lstDegree.Items[i].Selected = true;
                            }
                        }
                    }
                    Bindbranch();
                    string branchno = dr["BRANCHNO"] == null ? "0" : dr["BRANCHNO"].ToString();
                    string[] branch = branchno.Split(delimiterChars);
                    for (int j = 0; j < branch.Length; j++)
                    {
                        for (int i = 0; i < lstbranch.Items.Count; i++)
                        {
                            if (branch[j].Trim() == lstbranch.Items[i].Value.Trim())
                            {
                                lstbranch.Items[i].Selected = true;
                            }
                        }
                    }
                    objCommon.FillDropDownList(ddlgroup, "ACD_ADMISSION_CONFIG_GROUP", "GROUPNO", "GROUP_NAME", "GROUPNO>0", "GROUPNO");
                    ddlgroup.SelectedValue = dr["GROUPNO"] == null ? "0" : dr["GROUPNO"].ToString();
                    txtStartDate.Text = dr["ADMSTRDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ADMSTRDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate.Text = dr["ADMENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ADMENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtStartTime.Text = dr["STARTTIME"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["STARTTIME"].ToString()).ToString("hh:mm tt");
                    txtEndTime.Text = dr["ENDTIME"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ENDTIME"].ToString()).ToString("hh:mm tt");
                    txtDetails.Text = dr["DETAILS"] == null ? string.Empty : dr["DETAILS"].ToString();
                    txtApplicationFee.Text = dr["FEES"] == null ? "0" : dr["FEES"].ToString();
                    txtAge.Text = dr["AGE"] == null ? "0" : dr["AGE"].ToString();
                    if (dr["ACTIVE_STATUS"].ToString().Equals("1"))
                    {
                        chkStatus.Checked = true;
                    }
                    else
                    {
                        chkStatus.Checked = false;

                    }
                    ddlAdmType.SelectedValue = dr["ADM_TYPE"] == null ? "0" : dr["ADM_TYPE"].ToString();
                    rdobtnnri.SelectedValue = dr["IS_NRI"] == null ? "0" : dr["IS_NRI"].ToString();
                    txteqvinr.Text = dr["NRI_FEES"] == null ? "0" : dr["NRI_FEES"].ToString();
                }

            }
            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ClearControls()
    {
        ddlProgramme.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        ddlCategory.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        txtStartDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtApplicationFee.Text = string.Empty;
        txtAge.Text = string.Empty;
        ViewState["action"] = "add";
        chkStatus.Checked = false;
        ddlAdmType.SelectedIndex = 0;
        lstDegree.Items.Clear();
        lstbranch.Items.Clear();
        txteqvinr.Text = string.Empty;
        ddlgroup.SelectedIndex = 0;
        rdobtnnri.Enabled = true;
        lblapp.Text = "Application Fee(INR)";
        divinr.Visible = false;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            string ConfigID = (btnEdit.CommandArgument);
            ViewState["ConfigID"] = (btnEdit.CommandArgument);
            ViewState["edit"] = "edit";
            this.ShowDetails(ConfigID);
            rdobtnnri.Enabled = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_SessionCreate.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        rdobtnnri.SelectedIndex = 0;
        rdobtnnri.Enabled = true;
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int check = rdobtnnri.SelectedIndex;
        ddlSchool.SelectedIndex = -1;
        lstDegree.Items.Clear();
        lstbranch.Items.Clear();
        rdobtnnri.SelectedIndex = 0;
        txteqvinr.Text = string.Empty;
        ddlgroup.SelectedIndex = 0;
        txtApplicationFee.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        ddlProgramme.SelectedIndex = 0;
        ddlAdmType.SelectedIndex = 0;
        if (ddlAdmBatch.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlProgramme, "ACD_UA_SECTION C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.UGPGOT=C.UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");

        }
        rdobtnnri.SelectedIndex = check;
    }

    protected void ddlAdmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int check = rdobtnnri.SelectedIndex;
        if (ddlAdmType.SelectedIndex == 2)
        {
            objCommon.FillDropDownList(ddlProgramme, "ACD_UA_SECTION C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.UGPGOT=C.UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND CB.ADM_TYPE= 1", "UA_SECTION");

        }
        else
        {
            objCommon.FillDropDownList(ddlProgramme, "ACD_UA_SECTION C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.UGPGOT=C.UA_SECTION", "DISTINCT UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0", "UA_SECTION");

        }
        rdobtnnri.SelectedIndex = check;
    }

    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        int check = rdobtnnri.SelectedIndex;
        txtApplicationFee.Text = string.Empty;
        txtDetails.Text = string.Empty;
        txtEndDate.Text = string.Empty;
        txtEndTime.Text = string.Empty;
        txtStartDate.Text = string.Empty;
        txtStartTime.Text = string.Empty;
        lstDegree.Items.Clear();
        lstbranch.Items.Clear();
        rdobtnnri.SelectedIndex = 0;
        txteqvinr.Text = string.Empty;
        ddlgroup.SelectedIndex = 0;
        if (Session["OrgId"].ToString().Equals("3") || Session["OrgId"].ToString().Equals("4") || Session["OrgId"].ToString().Equals("5") || Session["OrgId"].ToString().Equals("7"))
        {
            objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.COLLEGE_ID=C.COLLEGE_ID ", "DISTINCT C.COLLEGE_ID", "C.COLLEGE_NAME", "C.COLLEGE_ID>0  AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "C.COLLEGE_NAME");
            ddlSchool.Focus();
        }
        else
        {
            objCommon.FillListBox(lstDegree, "ACD_DEGREE D INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CB ON CB.DEGREENO=D.DEGREENO", "DISTINCT D.DEGREENO", "D.DEGREENAME", "UGPGOT = " + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENO");

        }
        rdobtnnri.SelectedIndex = check;
    }

    protected void rdobtnnri_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearControls();
        if (rdobtnnri.SelectedIndex == 1)
        {
            lblapp.Text = "Application Fee($)";
            divinr.Visible = true;
        }
        else
        {
            lblapp.Text = "Application Fee(INR)";
            divinr.Visible = false;
        }
    }

    #endregion

    #region NRI

    protected void ddlAdmBatch_NRI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch_NRI.SelectedIndex > 0)
            {
                ddlAdmType_NRI.Items.Clear();
                ddlAdmType_NRI.Items.Add(new ListItem("Please Select", "0"));
                ddlAdmType_NRI.DataSource = ViewState["ddlAdmType"];
                ddlAdmType_NRI.DataTextField = "IDTYPEDESCRIPTION";
                ddlAdmType_NRI.DataValueField = "IDTYPENO";
                ddlAdmType_NRI.DataBind();
                ddlAdmType_NRI.Focus();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlDegree_NRI_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlProgrammeType_NRI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsDropDownDegree_NRI = null;
            admType = Convert.ToInt16(ddlProgrammeType_NRI.SelectedValue);
            SP_name = "PKG_ACD_GET_DROPDOWN_ADM_CONFIG";
            SP_parameters = "@P_ADMTYPE";
            SP_value = "" + admType + "";
            dsDropDownDegree_NRI = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_value);
            if (dsDropDownDegree_NRI.Tables.Count > 0)
            {
                if (dsDropDownDegree_NRI.Tables[3].Rows.Count > 0)
                {
                    ddlDegree_NRI.Items.Clear();
                    ddlDegree_NRI.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegree_NRI.DataSource = dsDropDownDegree_NRI.Tables[3];
                    ddlDegree_NRI.DataTextField = "DEGREE";
                    ddlDegree_NRI.DataValueField = "DEGREENO";
                    ddlDegree_NRI.DataBind();
                    ddlDegree_NRI.Focus();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlSchool_NRI_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlAdmType_NRI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmType_NRI.SelectedIndex > 0)
            {
                ddlProgrammeType_NRI.Items.Clear();
                ddlProgrammeType_NRI.Items.Add(new ListItem("Please Select", "0"));
                ddlProgrammeType_NRI.DataSource = ViewState["ddlProgrammeType"];
                ddlProgrammeType_NRI.DataTextField = "UA_SECTIONNAME";
                ddlProgrammeType_NRI.DataValueField = "UA_SECTION";
                ddlProgrammeType_NRI.DataBind();
                ddlProgrammeType_NRI.Focus();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void btnSubmit_NRI_Click(object sender, EventArgs e)
    {
        try
        {
            int ugpg = 0;
            DateTime dt = DateTime.Now;
            DateTime dtEnd = Convert.ToDateTime(txtEndDate_NRI.Text);
            dtEnd = dtEnd + DateTime.Now.TimeOfDay;

            string t1 = dt.ToString("H:mm");

            string STime = string.Empty;
            string ETime = string.Empty;
            if ((txtStartDate_NRI.Text != string.Empty) && (txtEndDate_NRI.Text != string.Empty))
            {

                int rest = DateTime.Compare(dt, dtEnd);
                if (Convert.ToDateTime(txtStartDate_NRI.Text) > Convert.ToDateTime(txtEndDate_NRI.Text))
                {
                    if (dtEnd.ToString("yyyyMMdd") == dt.ToString("yyyyMMdd"))
                    {
                        if (Convert.ToDateTime(txtEndTime_NRI.Text) < Convert.ToDateTime(t1))
                        {
                            objCommon.DisplayMessage(this.Page, "End Time Should be Greater than Current time.", this.Page);

                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "End Date should be greater than Start Date.", this.Page);

                        return;
                    }
                }
                if (Convert.ToDateTime(txtStartTime_NRI.Text) < dt)
                {
                    if (Convert.ToDateTime(txtStartTime_NRI.Text) < Convert.ToDateTime(t1))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Start Time Should be Greater than Current time`)", true);
                        return;
                    }
                }
                IITMS.UAIMS.BusinessLayer.BusinessEntities.Config objConfig = new IITMS.UAIMS.BusinessLayer.BusinessEntities.Config();
                objConfig.Admbatch = Convert.ToInt32(ddlAdmBatch_NRI.SelectedValue);
                objConfig.Degree_No = Convert.ToInt32(ddlDegree_NRI.SelectedValue);
                objConfig.College_Id = Convert.ToInt32(ddlSchool_NRI.SelectedValue);
                objConfig.Config_SDate = Convert.ToDateTime(txtStartDate_NRI.Text);
                objConfig.Config_EDate = Convert.ToDateTime(txtEndDate_NRI.Text);
                objConfig.AdmType = Convert.ToInt32(ddlAdmType_NRI.SelectedValue);
                STime = txtStartTime_NRI.Text;
                ETime = txtEndTime_NRI.Text;
                objConfig.Details = txtRemark_NRI.Text.Trim();
                objConfig.Fees = Convert.ToDouble(txtAppFee_NRI.Text.Trim());
                ugpg = Convert.ToInt32(ddlProgrammeType_NRI.SelectedValue);
                string mode = "";
                int configId = 0;
                int Active = chkActive_NRI.Checked ? Active = 1 : Active = 0;
                if (ViewState["action_NRI"] == null || ViewState["action_NRI"].ToString().Equals(string.Empty))
                {
                    mode = "INSERT";
                    //CustomStatus cs = (CustomStatus)objOAC.AddOnlineAdm_NRI(objConfig, STime, ETime, ugpg, Convert.ToInt32(Session["OrgId"]), Active, mode, configId);
                    //if (cs.Equals(CustomStatus.RecordSaved))
                    //{
                    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Notification Saved Successfully.`)", true);
                    //    objCommon.DisplayMessage(this.Page, "Notification Saved Successfully.", this.Page);
                    //    BindList_NRI();
                    //    //tab2.Attributes.Add("class", "tab-pane active");
                    //    //tab1.Attributes.Add("class", "tab-pane");
                    //    ClearControls_NRI();
                    //}
                    //else
                    //{
                    //    objCommon.DisplayMessage(this.Page, "Failed To Save Record.", this.Page);
                    //    return;
                    //}
                }
                else if (ViewState["action_NRI"].ToString().Equals("edit"))
                {
                    mode = "UPDATE";
                    configId = Convert.ToInt32(ViewState["ConfigID"].ToString());
                    objConfig.ConfigID = configId;
                    //CustomStatus cs = (CustomStatus)objOAC.UpdateOnlineAdm_NRI(objConfig, STime, ETime, ugpg, Active);
                    // if (cs.Equals(CustomStatus.RecordUpdated))
                    // {
                    //     objCommon.DisplayMessage(this.Page, "Notification Updated Successfully.", this.Page);
                    //     BindList_NRI();
                    //     ClearControls_NRI();
                    // }
                    // else
                    // {
                    //     objCommon.DisplayMessage(this.Page, "Failed To Save Record.", this.Page);
                    //     return;
                    // }
                }

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnCancel_NRI_Click(object sender, EventArgs e)
    {

    }
    private void ClearControls_NRI()
    {
        ddlProgrammeType_NRI.SelectedIndex = 0;
        ddlSchool_NRI.SelectedIndex = 0;
        ddlAdmBatch_NRI.SelectedIndex = 0;
        ddlDegree_NRI.SelectedIndex = 0;
        //ddlBranch.SelectedIndex = 0;
        txtStartDate_NRI.Text = string.Empty;
        txtStartTime_NRI.Text = string.Empty;
        txtEndDate_NRI.Text = string.Empty;
        txtEndTime_NRI.Text = string.Empty;
        //txtIntake.Text = string.Empty;
        txtRemark_NRI.Text = string.Empty;
        txtAppFee_NRI.Text = string.Empty;
        chkActive_NRI.Checked = false;
        ddlAdmType_NRI.SelectedIndex = 0;
    }
    protected void BindList_NRI()
    {
        try
        {
            DataSet dsBind_NRI = null;
            int config = 0;
            SP_name = "PKG_GET_ALL_CONFIG_FOR_NRI";
            SP_parameters = "@P_CONFIGID";
            SP_value = "" + config + "";
            dsBind_NRI = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_value);
            if (dsBind_NRI.Tables.Count > 0)
            {
                lvNRI.DataSource = dsBind_NRI.Tables[1];
                lvNRI.DataBind();
                lvNRI.Visible = true;
            }
            else
            {
                lvNRI.DataSource = null;
                lvNRI.DataBind();
                lvNRI.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void btnEdit_NRI_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit_NRI = sender as ImageButton;
            int ConfigID = int.Parse(btnEdit_NRI.CommandArgument);
            ViewState["ConfigID"] = int.Parse(btnEdit_NRI.CommandArgument);
            ViewState["action_NRI"] = "edit";
            SP_name = "PKG_GET_ALL_CONFIG_FOR_NRI";
            SP_parameters = "@P_CONFIGID";
            SP_value = "" + ConfigID + "";
            DataSet dsEdit = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_value);
            if (dsEdit.Tables.Count > 0)
            {
                if (dsEdit.Tables[0].Rows.Count > 0)
                {
                    ddlAdmBatch_NRI.SelectedValue = dsEdit.Tables[0].Rows[0]["ADMBATCH"].ToString();
                    ddlProgrammeType_NRI.Items.Clear();
                    ddlProgrammeType_NRI.Items.Add(new ListItem("Please Select", "0"));
                    ddlProgrammeType_NRI.DataSource = ViewState["ddlProgrammeType"];
                    ddlProgrammeType_NRI.DataTextField = "UA_SECTIONNAME";
                    ddlProgrammeType_NRI.DataValueField = "UA_SECTION";
                    ddlProgrammeType_NRI.DataBind();
                    ddlProgrammeType_NRI.SelectedValue = dsEdit.Tables[0].Rows[0]["UGPGOT"].ToString();

                    DataSet dsDropDownDegree_NRI = null;
                    admType = Convert.ToInt16(ddlProgrammeType_NRI.SelectedValue);
                    SP_name = "PKG_ACD_GET_DROPDOWN_ADM_CONFIG";
                    SP_parameters = "@P_ADMTYPE";
                    SP_value = "" + admType + "";
                    dsDropDownDegree_NRI = objCommon.DynamicSPCall_Select(SP_name, SP_parameters, SP_value);
                    if (dsDropDownDegree_NRI.Tables.Count > 0)
                    {
                        if (dsDropDownDegree_NRI.Tables[3].Rows.Count > 0)
                        {
                            ddlDegree_NRI.Items.Clear();
                            ddlDegree_NRI.Items.Add(new ListItem("Please Select", "0"));
                            ddlDegree_NRI.DataSource = dsDropDownDegree_NRI.Tables[3];
                            ddlDegree_NRI.DataTextField = "DEGREE";
                            ddlDegree_NRI.DataValueField = "DEGREENO";
                            ddlDegree_NRI.DataBind();
                        }
                    }
                    ddlDegree_NRI.SelectedValue = dsEdit.Tables[0].Rows[0]["DEGREENO"].ToString();
                    txtStartDate_NRI.Text = dsEdit.Tables[0].Rows[0]["ADMSTRDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dsEdit.Tables[0].Rows[0]["ADMSTRDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtEndDate_NRI.Text = dsEdit.Tables[0].Rows[0]["ADMENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dsEdit.Tables[0].Rows[0]["ADMENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtStartTime_NRI.Text = dsEdit.Tables[0].Rows[0]["STARTTIME"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dsEdit.Tables[0].Rows[0]["STARTTIME"].ToString()).ToString("hh:mm tt");
                    txtEndTime_NRI.Text = dsEdit.Tables[0].Rows[0]["ENDTIME"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dsEdit.Tables[0].Rows[0]["ENDTIME"].ToString()).ToString("hh:mm tt");
                    txtRemark_NRI.Text = dsEdit.Tables[0].Rows[0]["DETAILS"] == null ? string.Empty : dsEdit.Tables[0].Rows[0]["DETAILS"].ToString();
                    txtAppFee_NRI.Text = dsEdit.Tables[0].Rows[0]["FEES"] == null ? "0" : dsEdit.Tables[0].Rows[0]["FEES"].ToString();
                    if (dsEdit.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().Equals("1"))
                    {
                        chkActive_NRI.Checked = true;
                    }
                    else
                    {
                        chkActive_NRI.Checked = false;
                    }
                    ddlAdmType_NRI.Items.Clear();
                    ddlAdmType_NRI.Items.Add(new ListItem("Please Select", "0"));
                    ddlAdmType_NRI.DataSource = ViewState["ddlAdmType"];
                    ddlAdmType_NRI.DataTextField = "IDTYPEDESCRIPTION";
                    ddlAdmType_NRI.DataValueField = "IDTYPENO";
                    ddlAdmType_NRI.DataBind();
                    ddlAdmType_NRI.SelectedValue = dsEdit.Tables[0].Rows[0]["ADM_TYPE"] == null ? "0" : dsEdit.Tables[0].Rows[0]["ADM_TYPE"].ToString();
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    #endregion

}


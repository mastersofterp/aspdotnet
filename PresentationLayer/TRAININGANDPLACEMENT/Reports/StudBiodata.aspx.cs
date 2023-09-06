using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class TRAININGANDPLACEMENT_Reports_StudBiodata : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
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
                // Check User Authority 
                this.CheckPageAuthorization(); 
                
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                //Fill_Branch();
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO NOT IN(0)", "DEGREENO");
                objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO NOT IN(0)", "BATCHNO");
                ddlStudName.Items.Add("Please Select");
                ddlStudName.SelectedIndex = 0;
                if (Session["usertype"].ToString().Equals("2"))
                {
                    ViewState["IDNO"] = Session["idno"];
                    string stype = objCommon.LookUp("acd_tp_student_registration", "STUDENT_TYPE", "IDNO=" + Convert.ToInt32(ViewState["IDNO"])).ToString();
                    if (stype.Equals("R"))
                        radlStudentType.SelectedValue = "R";
                    else
                        radlStudentType.SelectedValue = "P";

                    int branchno = Convert.ToInt32(objCommon.LookUp("acd_student", "Branchno", "IDNO=" + Convert.ToInt32(ViewState["IDNO"])));
                    ddlBranch.SelectedValue = branchno.ToString();
                    string StudName = objCommon.LookUp("acd_student", "studname", "IDNO=" + Convert.ToInt32(ViewState["IDNO"])).ToString();

                    ddlStudName.Items.Add(StudName);
                    radlStudentType.Enabled = false;
                    ddlBranch.Enabled = false;
                    ddlStudName.Enabled = false;


                    //ddlStudName.SelectedItem.Value = "0";

                    //pnlSelect.Enabled = false;
                }
                else
                {
                    //Fill_Branch();
                    ViewState["IDNO"] = string.Empty;
                }
                //Fill_Branch();
                //ViewState["IDNO"] = string.Empty;
            }
        }
        divMsg.InnerHtml = string.Empty;
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            // Check user's authrity for Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=StudBiodata.aspx");
            }
        }
        else
        {
            // Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=StudBiodata.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("Biodata_R", "TPStudRegBiodata.rpt");
            //if (radlStudentType.SelectedValue == "R")
            //{
            //    ShowReport("Biodata_R", "TPStudRegBiodata.rpt");
            //}
            //else if (radlStudentType.SelectedValue == "P")
            //{
            //    ShowReport("Biodata_P", "TPPassStudRegBiodata.rpt");
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.btnShow_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void Fill_Branch()
    {
        try
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO NOT IN(0) AND DEGREENO=" + Convert.ToInt16(ddlDegree.SelectedValue), "LONGNAME");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.Fill_Branch ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        ddlStudName.SelectedIndex = 0;
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.SelectedIndex = 0;
        ddlStudName.SelectedIndex = 0; 
        Fill_Branch();
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlStudName.SelectedIndex = 0;
            if (ddlBranch.SelectedIndex > 0)
            {
                FillStudent();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.ddlBranch_SelectedIndexChanged ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    protected void FillStudent()
    {
        try
        {
            string IDNOs = string.Empty;
            ddlStudName.Items.Clear();
            ddlStudName.Items.Add("Please Select");
            ddlStudName.SelectedItem.Value = "0";
            DataSet ds = objTP.GetStudentNo_Biodata(Convert.ToChar(radlStudentType.SelectedValue),Convert.ToInt32(ddlBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue),Convert.ToInt32(ddlBranch.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStudName.DataSource = ds.Tables[0];
                ddlStudName.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlStudName.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlStudName.DataBind();
                ddlStudName.SelectedIndex = 0;
                DataTable dt = ds.Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    if (IDNOs.Equals(string.Empty))
                        IDNOs = dr["IDNO"].ToString();

                    else
                        IDNOs += "." + dr["IDNO"].ToString();
                }

                ViewState["IDNO"] = IDNOs;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.FillStudent->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void radlStudentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (radlStudentType.SelectedValue == "P")
                trBatch.Visible = true;
            else
                trBatch.Visible = false;
                //trBatch.Attributes.Add("style", "display:none");

            ddlBatch.SelectedIndex = 0;
            ddlDegree.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            ddlStudName.SelectedIndex = 0;
            
            //FillStudent();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.radlStudentType_SelectedIndexChanged->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,TRAININGANDPLACEMENT," + rptFileName;

            if (radlStudentType.SelectedValue == "R")
            {
                if (ddlStudName.SelectedIndex == 0)
                {
                    url += "&param=@P_idno=" + Convert.ToString(ViewState["IDNO"]);
                }
                else
                {
                    url += "&param=@P_idno=" + Convert.ToString(ddlStudName.SelectedValue);
                }
            }
            else if (radlStudentType.SelectedValue == "P")
            {
                if (ddlStudName.SelectedIndex == 0)
                {
                    url += "&param=@P_idno=" + Convert.ToString(ViewState["IDNO"]);
                }
                else
                {
                    url += "&param=@P_idno=" + Convert.ToString(ddlStudName.SelectedValue);
                }
            }

            //string Script = string.Empty;
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this.updBiodata, updBiodata.GetType(), "Report", Script, true);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Reports_StudBiodata.ShowReport -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    

    public void Clear()
    {
        if (ddlDegree.Items.Count > 0)ddlDegree.SelectedIndex = 0;
        if (ddlBranch.Items.Count > 0)ddlBranch.SelectedIndex = 0;
        if (ddlStudName.Items.Count > 0)ddlStudName.SelectedIndex = 0;
    }

    protected void btnCan_Click(object sender, EventArgs e)
    {
        Clear();
    }
}

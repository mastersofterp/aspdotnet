using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;

using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
public partial class ACADEMIC_MASTERS_CollegeMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    College objCollege = new College();
    CollegeController objController = new CollegeController();
    UAIMS_Common objUCommon = new UAIMS_Common();


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


                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    this.CheckPageAuthorization();
                    HiddenField1.Value = "0";
                    BindlistView();
                    //ViewState["action"] = "edit";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACDEMIC_COLLEGE_MASTER.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=TPJobLoc.aspx");
        }
    }

    private void BindlistView()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK)", "COLLEGE_ID", "COLLEGE_NAME,(CASE WHEN COLLEGE_TYPE = 'S' THEN 'School' WHEN COLLEGE_TYPE = 'P' THEN 'Partner Institutes' ELSE 'College' END) AS COLLEGETYPE,LOCATION,SHORT_NAME,CODE", string.Empty, "COLLEGE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCollegeDetails.DataSource = ds;
                lvCollegeDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_COLLEGE_MASTER.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Clear()
    {
        txtName.Text = string.Empty;
        ddlCollegeType.SelectedIndex = 0;
        txtShortName.Text = string.Empty;
        txtcode.Text = string.Empty;
        txtLocName.Text = string.Empty;
    }

    protected Boolean funDuplicate()
    {
        DataSet ds = null;
        ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK)", "*", " ", " COLLEGE_NAME ='" + txtName.Text + "'", "");

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (HiddenField1.Value != null)
            {
                //objCollege.CollegeCode = txtCollegeCode.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtCollegeCode.Text.ToUpper().Trim());
                objCollege.Name = txtName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtName.Text.Trim());
                objCollege.Collegetype = ddlCollegeType.SelectedValue;//ddlCollegeType.SelectedItem.Text.Trim().Equals("School") ? "S" : "C";
                objCollege.Short_Name = txtShortName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtShortName.Text.Trim());
                objCollege.CollegeCode = txtcode.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtcode.Text.Trim());
                objCollege.Location = txtLocName.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtLocName.Text.Trim());

                //Check whether to add or update
                if (HiddenField1.Value != null)
                {
                    if (HiddenField1.Value.ToString().Equals("0"))
                    {
                        if (funDuplicate() == true)
                        {
                            objCommon.DisplayMessage(updCollege, "Records Already Exist!", this.Page);
                            Clear();
                            return;
                        }
                        CustomStatus cs = (CustomStatus)objController.AddCollege(objCollege);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            HiddenField1.Value = "0";
                            objCommon.DisplayMessage(this.updCollege, "Record Saved Successfully!", this.Page);
                            Clear();
                            BindlistView();
                        }
                        else
                        {
                            objCommon.DisplayMessage(updCollege, "Sorry!Try Again!", this.Page);
                        }
                    }
                    else
                    {
                        //Edit
                        if (HiddenField1.Value != null)
                        {
                            objCollege.COLLEGE_ID = Convert.ToInt32(HiddenField1.Value);

                            CustomStatus cs = (CustomStatus)objController.UpdateCollege(objCollege);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                                HiddenField1.Value = "0";

                                objCommon.DisplayMessage(this.updCollege, "Record Updated Successfully!", this.Page);
                                Clear();
                                BindlistView();
                            }
                            else
                            {
                                objCommon.DisplayMessage(updCollege, "Sorry! Try Again!", this.Page);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnSave_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int COLLEGE_ID = int.Parse(btnEdit.CommandArgument);
            Label1.Text = string.Empty;
            //ViewState["action"] = Convert.ToInt32(btnEdit);
            HiddenField1.Value = int.Parse(btnEdit.CommandArgument).ToString();
            ShowDetails(COLLEGE_ID);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_CollegeMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int COLLEGE_ID)
    {
        string sDegree = string.Empty;
        try
        {
            string degree = string.Empty;
            string branch = string.Empty;
            string department = string.Empty;
            DataSet ds = objCommon.FillDropDown("ACD_COLLEGE_MASTER WITH (NOLOCK)", "*", "", "COLLEGE_ID=" + COLLEGE_ID, "COLLEGE_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
                ddlCollegeType.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_TYPE"].ToString();
                txtShortName.Text = ds.Tables[0].Rows[0]["SHORT_NAME"].ToString();
                txtcode.Text = ds.Tables[0].Rows[0]["CODE"].ToString();
                txtLocName.Text = ds.Tables[0].Rows[0]["LOCATION"].ToString();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Master_CollegeMaster.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ShowReport("College_Report", "CollegeDetails.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_USER_NAME=" + Session["username"].ToString();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updCollege, this.updCollege.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_CollegeDetails.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

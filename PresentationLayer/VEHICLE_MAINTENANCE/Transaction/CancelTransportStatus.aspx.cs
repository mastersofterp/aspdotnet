//========================================================
// CREATED BY   : NIKHIL MANDAOKAR
// CREATED DATE : 12-08-2021
// DESCRIPTION  : IT IS USED TO CANCEL TRANSPORT STATUS.
//========================================================
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

public partial class VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();


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
                    this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    BindCollege();
                    ViewState["action"] = "add";


                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    // This method is used to check the authorization of page.
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

    private void BindCollege()
    {
        objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "", "");
    }

    private void BindlistView()
    {
        try
        {
            //DataSet ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "STUDNAME,ENROLLNO", "TRANSPORT=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "");

            DataSet ds = null;

            if (rdotrasnsporttyepe.SelectedValue == "C")
            {
                ds = objCommon.FillDropDown("VEHICLE_TRANSPORT_REQUISITION_APPLICATION A INNER JOIN ACD_STUDENT S ON (A.STUD_IDNO = S.IDNO)", "A.STUD_IDNO AS IDNO", "S.STUDNAME, S.ENROLLNO", "A.SECOND_APPROVE_STATUS = 'A' AND S.COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "");
            }
            else
            {
                ds = objCommon.FillDropDown("ACD_STUDENT", "IDNO", "STUDNAME, ENROLLNO", "TRANSPORT != 1 AND HOSTELER !=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvStudentList.DataSource = ds;
                lvStudentList.DataBind();
                lvStudentList.Visible = true;
            }
            else
            {

                lvStudentList.DataSource = null;
                lvStudentList.DataBind();
                lvStudentList.Visible = false;
                MessageBox("Data Not Found");
                Clear();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindlistView();
    }

    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlCollege.SelectedValue = "0";
        lvStudentList.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedValue == "0")
            {
                MessageBox("Please Select College");
                return;
            }


            DataTable StudentField = new DataTable("StudentField");

            StudentField.Columns.Add("IDNO", typeof(int));

            int count = 0;
            DataRow dr = null;
            foreach (ListViewItem i in lvStudentList.Items)
            {

                CheckBox chkIdNo = (CheckBox)i.FindControl("chkIdNo");
                HiddenField HDIdNo = (HiddenField)i.FindControl("hidIdNo");

                if (chkIdNo.Checked == true)
                {
                    count = 1;
                    dr = StudentField.NewRow();
                    dr["IDNO"] = HDIdNo.Value;
                    StudentField.Rows.Add(dr);
                }
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.updActivity, "Please Select At Least One Student.", this.Page);
                return;
            }

            DataTable StudentId = StudentField;

            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objVMC.UpdateCancelTransportStatus(StudentId, rdotrasnsporttyepe.SelectedValue, Convert.ToInt32(ddlCollege.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        ViewState["action"] = "add";
                        MessageBox("Record Saved Sucessfully ");
                        Clear();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    // It is used to get student list to update transport status.
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindlistView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_CancelTransportStatus.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
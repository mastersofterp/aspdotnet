using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_Advance_Apply_Process : System.Web.UI.Page
{
    string date = DateTime.Now.ToString("dd/MM/yyyy");
    int counter = 0;
    
    //Creating objects of Class Files Common,UAIMS_COMMON,Advance Amount Controller

    Common objCommon = new Common();
    static double Pending_count = 0;
    UAIMS_Common objUCommon = new UAIMS_Common();
    AdvancePassingAuthorityController objAdvPassAuthCon = new AdvancePassingAuthorityController();
    AdvancePassingAuthority objAPAuth = new AdvancePassingAuthority();

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
            if (Session["userno"] == null || Session["username"] == null ||Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                 CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                pnllist.Visible = true;
                pnlLeaveStatus.Visible = false;
              
                objAPAuth.UA_NO= Convert.ToInt32(Session["userno"].ToString());

                objAPAuth.EMPNO = objAPAuth.UA_NO;
                 
                int IDNO = Convert.ToInt32(objCommon.LookUp("User_ACC", "UA_IDNO", "UA_NO=" + objAPAuth.UA_NO));
                //get advance apply Process
                txtPath.Text = Convert.ToString(objCommon.LookUp("PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH", "PAPATH", "idno=" + IDNO));
                //tableName,fieldname1,fieldname2,fieldname3(condition),whereCondition
                DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "*", "", "IDNO=" + IDNO, "");
                if(ds!=null)
                {
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        objAPAuth.DEPTNO = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString());
                        objAPAuth.STAFFNO = Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString());
                        objAPAuth.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString());
                    }
                }
                BindListViewAdvanceApplyProcess();
            }
        }
        divMsg.InnerHtml = string.Empty;

    }

    //function use to Bind PAYROLL_ADVANCE_APP_ENTRY 
    protected void BindListViewAdvanceApplyProcess()
    {
        try
        {

           int ua_no = Convert.ToInt32(Session["userno"].ToString());
           objAPAuth.IDNO = Convert.ToInt32(objCommon.LookUp("User_ACC", "UA_IDNO", "UA_NO=" + ua_no));
           
            DataSet ds = objAdvPassAuthCon.GetAdvanceAppProcess(objAPAuth.STAFFNO,objAPAuth.DEPTNO,objAPAuth.COLLEGE_NO,objAPAuth.IDNO);
            if (ds.Tables[0].Rows.Count >= 0)
            {
                lvAdvAppProcess.DataSource = ds.Tables[0];
                lvAdvAppProcess.DataBind();
                lvAdvAppProcess.Visible = true;
                dpPager.Visible = true;
            }          

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_APP_ENTRY_.BindListViewAdvanceApplyProcess ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //function use to Check Authorization
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=createdomain.aspx");
        }
    }

    //function to popup the message box
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objAPAuth.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAPAuth.EMPNO = objAPAuth.UA_NO;
            int IDNO = Convert.ToInt32(objCommon.LookUp("User_ACC", "UA_IDNO", "UA_NO=" + objAPAuth.UA_NO));
            objAPAuth.PAPNO = Convert.ToInt32(objCommon.LookUp("PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH", "PAPNO", "idno=" + IDNO));
            //get Details from Payroll_empmas table
            DataSet ds = objCommon.FillDropDown("PAYROLL_EMPMAS", "*", "", "IDNO=" + IDNO, "");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objAPAuth.DEPTNO = Convert.ToInt32(ds.Tables[0].Rows[0]["SUBDEPTNO"].ToString());
                    objAPAuth.STAFFNO = Convert.ToInt32(ds.Tables[0].Rows[0]["STAFFNO"].ToString());
                    objAPAuth.COLLEGE_NO = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_NO"].ToString());
                    objAPAuth.COLLEGE_CODE = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_CODE"].ToString());
                }
            }

            objAPAuth.ADVANCEAMOUNT =Convert.ToInt32( txtAdvanceAmt.Text);
            objAPAuth.REASON = txtReason.Text;
            if (!txtAdvdt.Text.Trim().Equals(string.Empty)) 
                objAPAuth.ADVCANEDATE = Convert.ToDateTime(txtAdvdt.Text);
            objAPAuth.APPLYDATE =Convert.ToDateTime( DateTime.Now.ToString());
            //AddDetails Into PAYROLL_ADVANCE_APP_ENTRY table

            CustomStatus cs = (CustomStatus)objAdvPassAuthCon.AdvAppPro(objAPAuth, IDNO);

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                MessageBox("Record Saved Successfully");
            }
            else if(cs.Equals(CustomStatus.DuplicateRecord))
            {
                MessageBox("Select Diffrent Advance Date");
            }
            BindListViewAdvanceApplyProcess();
            clear();
          //  txtPath.Text = Convert.ToString(objCommon.LookUp("PAYROLL_ADVANCE_PASSING_AUTHORITY_PATH", "PAPATH", "idno=" + IDNO));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PAYROLL_ADVANCE_APP_ENTRY.AdvAppPro ->" + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server.UnAvailable");
        }
    }

    //Clear All From Controls
    public void clear()
    {
        txtAdvanceAmt.Text = string.Empty;
        txtAdvdt.Text = string.Empty;
      
        txtReason.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;


public partial class ACADEMIC_EXAMINATION_Mark_Entry_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    MarksEntryController objMEC = new MarksEntryController();
    string action = string.Empty;
    int QueryType = 0;
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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                //this.CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }

            BindListView();
            ViewState["action"] = btnSubmit.Text.ToString();

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        if (btnSubmit.Text != ViewState["action"].ToString())
        {
            btnSubmit.Text = "Submit";
        }

        txtCodeDesc.Focus();
        txtCodeValue.ReadOnly = false;
    }

    protected void btnEdit1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            QueryType = 3;
            ImageButton btnEditRecord = sender as ImageButton;
            DataSet ds = objMEC.GetMarkEntryStatusBYID(int.Parse(btnEditRecord.CommandArgument), QueryType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                txtCodeDesc.Text = dr["CODE_DESC"].ToString();
                txtCodeValue.Text = dr["CODE_VALUE"].ToString();
                txtCodeValue.ReadOnly = true;
                txtShortname.Text = dr["SHORT_NAME"].ToString();
                txtFinalGrade.Text = dr["Final_Grade"].ToString();
                txtGradePoint.Text = dr["Grade_Point"].ToString();
                btnSubmit.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_TimeTableSlot.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int returnvalue = 0;
        string CodeDesc = Convert.ToString(txtCodeDesc.Text);
        int CodeValue = Convert.ToInt32(txtCodeValue.Text);
        string ShortName = Convert.ToString(txtShortname.Text);
        int OrgID = Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]);
        string FinalGrade = Convert.ToString(txtFinalGrade.Text);
        double GradePoint = Convert.ToDouble(txtGradePoint.Text);


        if (btnSubmit.Text != ViewState["action"].ToString())
        {
            QueryType = 2;
            returnvalue = objMEC.UPDMarkEntryStatus(CodeDesc, CodeValue, ShortName, OrgID, FinalGrade, GradePoint, QueryType);
            if (returnvalue == 2)
            {

                objCommon.DisplayMessage(this.updMarkEntryStatus, "Record Updated Successfully", this.Page);
            }

            BindListView();
            clear();
            txtCodeDesc.Focus();
            txtCodeValue.ReadOnly = false;

        }
        else
        {
            DataSet codeval = objMEC.CheckDuplicateCodeValue(CodeValue);
            int Count = Convert.ToInt32(codeval.Tables[0].Rows[0][0].ToString());
            if (Count == 0)
            {
                QueryType = 1;
                returnvalue = objMEC.AddMarkEntryStatus(CodeDesc, CodeValue, ShortName, OrgID, FinalGrade, GradePoint, QueryType);
                if (returnvalue == 1)
                {
                    objCommon.DisplayMessage(this.updMarkEntryStatus, "Record Saved Successfully", this.Page);
                }

                BindListView();
            }
            else
            {

                objCommon.DisplayMessage(this.updMarkEntryStatus, "Record Already Exists", this.Page);
            }


        }
        clear();
        btnSubmit.Text = "Submit";


    }


    protected void BindListView()
    {
        try
        {
            QueryType = 4;
            MarksEntryController objMEC = new MarksEntryController();
            DataSet ds = objMEC.GetAllMarkEntryStatus(QueryType);

            if (ds != null && ds.Tables.Count > 0)
            {
                lvMarkEntryStatus.DataSource = ds.Tables[0];
                lvMarkEntryStatus.DataBind();
                lvMarkEntryStatus.Visible = true;

            }


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Masters_GradeMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void clear()
    {
        txtCodeDesc.Text = "";
        txtCodeValue.Text = "";
        txtFinalGrade.Text = "";
        txtGradePoint.Text = "";
        txtShortname.Text = "";
    }



    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int returnvalue = 0;
        QueryType = 5;
        MarksEntryController objMEC = new MarksEntryController();
        ImageButton btnDeleteRecord = sender as ImageButton;
       
        
        returnvalue = objMEC.DeleteMarkEntryStatus(Convert.ToInt32(btnDeleteRecord.CommandArgument), QueryType);
        if (returnvalue == 5)
        {
            objCommon.DisplayMessage(this.updMarkEntryStatus, "Record Deleted SuccessFully", this.Page);
            BindListView();
        }
        else
        {
            objCommon.DisplayMessage(this.updMarkEntryStatus, "Something Went Wrong", this.Page);
        }

    }


}






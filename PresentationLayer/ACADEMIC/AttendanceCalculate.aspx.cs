//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ATTENDANCE CALCULATE MASTER PAGE                                                     
// CREATION DATE : 11-Oct-2023                                                          
// CREATED BY    : SAKSHI K. MAKWANA                                 
// MODIFIED DATE :                                                         
// MODIFIED DESC :                                                               
//======================================================================================

using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_AttendanceCalculate : System.Web.UI.Page
{

    Attendance objAttendanceEntity = new Attendance();
    AcdAttendanceController AttendanceCalLogic = new AcdAttendanceController();
    UAIMS_Common objCommon = new UAIMS_Common();

    #region PageLoad
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
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }

            ListBind();
            Session["edit"] = "Submit";
        }
    }
    #endregion PageLoad

    #region List Bind
    protected void ListBind()
    {
        DataSet ds = AttendanceCalLogic.ShowAtendanceStatus();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lview.DataSource = ds.Tables[0];
            lview.DataBind();
        }
        else
        {
            lview.DataSource = null;
            lview.DataBind();
            lview.Visible = false;
            objCommon.DisplayMessage(this, "Record not found ", this.Page);
        }
    }

    #endregion List Bind

    #region CheckAuthentication
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchWiseIntake.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchWiseIntake.aspx");
        }
    }

    #endregion CheckAuthentication

    #region Submit Calculate Attendance

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Update Attendance Type

        if (Session["edit"] == "Update")
        {
            if (txtStatusName.Text != null)
            {
                objAttendanceEntity.Status = txtStatusName.Text;
                if (chkcal.Checked)
                {
                    objAttendanceEntity.Flag = 1;
                }
                else
                {
                    objAttendanceEntity.Flag = 0;
                }

                objAttendanceEntity.StatusNo = Convert.ToInt32(Session["id"].ToString());
                int results = AttendanceCalLogic.CalculateAttendance(objAttendanceEntity);
                if (results == 2)
                {
                    objCommon.DisplayMessage(this, "Record Updated  Successfully ", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, "Something went wrong record  not saved ", this.Page);
                }

                ListBind();
                txtStatusName.Text = string.Empty;
                chkcal.Checked = false;
            }
            btnSubmit.Text = "Submit";
            Session["edit"] = "null";

        }
        #endregion Update Attendance Type

        #region Insert Attendance Type

        else if (Session["edit"] == "Submit")
        {
            if (txtStatusName.Text != null)
            {
                objAttendanceEntity.Status = txtStatusName.Text;
                if (chkcal.Checked)
                {
                    objAttendanceEntity.Flag = 1;
                }
                else
                {
                    objAttendanceEntity.Flag = 0;
                }
                int result = AttendanceCalLogic.CalculateAttendanceSubmit(objAttendanceEntity);
                if (result == 1)
                {
                    objCommon.DisplayMessage(this, "Attendace Added Successfully ", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this, "Something Went Wrong", this.Page);
                }

                btnSubmit.Text = "Submit";
                Session["edit"] = "Submit";

                ListBind();
                txtStatusName.Text = string.Empty;
                chkcal.Checked = false;
            }

            btnSubmit.Text = "Submit";
            Session["edit"] = "null";
        }

        #endregion Insert Attendance Type
    }

    #region Clear Control
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtStatusName.Text = string.Empty;
        chkcal.Checked = false;

        btnSubmit.Text = "Submit";
        Session["edit"] = "Submit";
    }

    #endregion Clear Control

    #endregion Submit Calculate Attendance

    #region Edit
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int Status_no = int.Parse(btnEdit.CommandArgument);
        Session["id"] = Status_no;
        btnSubmit.Text = "Update";
        Session["edit"] = "Update";
        this.ShowDetails();

    }

    private void ShowDetails()
    {
        objAttendanceEntity.StatusNo = Convert.ToInt32(Session["id"].ToString());
        SqlDataReader dr = AttendanceCalLogic.GetStatusDetail(objAttendanceEntity);
        if (dr.Read())
        {
            txtStatusName.Text = dr["STATUS_NAME"].ToString();
            if (dr["CALCULATE"].ToString() == "1")
            {
                chkcal.Checked = true;
            }
            else
            {
                chkcal.Checked = false;
            }
        }



    }

    #endregion Edit
}
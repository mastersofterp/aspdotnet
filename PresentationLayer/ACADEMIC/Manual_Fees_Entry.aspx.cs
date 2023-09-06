using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

public partial class ACADEMIC_Manual_Fees_Entry : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController studCont = new StudentController();
    FeeCollectionController fcc = new FeeCollectionController();

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
                    //CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_Manual_Fees_Entry.Page_Load-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=FineAllotment.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=FineAllotment.aspx");
        }
    }

    private void ShowDetails()
    {
        StudentController objSC = new StudentController();
        //DataSet dsStudent = new DataSet();
        DataSet dsStudent = null;

        try
        {
            //if (ddlDegree.SelectedValue == "6")
            //{
            //    dsStudent = objSC.GetStudentDetailsWithRegistrationFees(txtApplicationId.Text, Convert.ToInt32(ddlDegree.SelectedValue));
            //}
            //else
            //{
            //    dsStudent = objSC.GetStudentDetailsWithRegistrationFees(txtApplicationId.Text, Convert.ToInt32(ddlDegree.SelectedValue));
            //}
            dsStudent = objSC.GetStudentDetailsWithRegistrationFees(txtApplicationId.Text, Convert.ToInt32(ddlDegree.SelectedValue));

            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["USERNO"].ToString();
                    lblEmail.Text = dsStudent.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblGender.Text = dsStudent.Tables[0].Rows[0]["GENDER"].ToString();
                    lblAppliedFees.Text = dsStudent.Tables[0].Rows[0]["FEES"].ToString();
                    if (ddlDegree.SelectedValue == "6")
                    {
                        lblDegreeName.Text = dsStudent.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                        lblDegreeName.ToolTip = dsStudent.Tables[0].Rows[0]["DEPARTMENT_NO"].ToString();
                    }
                    else
                    {
                        lblDegreeName.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString();
                        lblDegreeName.ToolTip = dsStudent.Tables[0].Rows[0]["DEGREENO"].ToString();
                    }
                    lblMobNo.Text = dsStudent.Tables[0].Rows[0]["MOBILENO"].ToString();
                    lblDOB.Text = Convert.ToDateTime(dsStudent.Tables[0].Rows[0]["DATEOFBIRTH"].ToString()).ToShortDateString();
                    lblPayStatus.Text = dsStudent.Tables[0].Rows[0]["PAY_STATUS"].ToString();
                    
                    divCourses.Visible = true;
                    pnlSingleStud.Visible = true;
                }
                else
                {
                    divCourses.Visible = false;
                    objCommon.DisplayMessage(updFine, "Registration Details Not Found!", this.Page);
                    txtApplicationId.Text = string.Empty;
                }
            }
            else
            {
                divCourses.Visible = false;
                objCommon.DisplayMessage(updFine, "Registration Details Not Found!", this.Page);
                txtApplicationId.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divCourses.Visible = true;
        divStudInfo.Visible = true;
        ShowDetails();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int status = 0;
        DataSet dss = new DataSet();

        try
        {
            if (ddlDegree.SelectedValue == "6")
            {
                string idno = objCommon.LookUp("ACD_PHD_DCR_ONLINE", "IDNO", "IDNO =" + lblName.ToolTip + "");

                if (idno != string.Empty && idno != null && idno != "")
                {
                    objCommon.DisplayMessage(updFine, "Fees Has Been Paid Already!!!", this.Page);
                    divCourses.Visible = false;
                    return;
                }
                else
                {
                    status = fcc.InsertManualRegistrationFeeEntry(Convert.ToInt32(lblName.ToolTip), txtApplicationId.Text, lblName.Text, Convert.ToInt32(lblDegreeName.ToolTip), lblAppliedFees.Text, Convert.ToInt32(ddlDegree.SelectedValue));
                }
            }
            else
            {
                string idno = objCommon.LookUp("ACD_DCR_ONLINE", "IDNO", "IDNO =" + lblName.ToolTip + "");

                if (idno != string.Empty || idno != null)
                {
                    objCommon.DisplayMessage(updFine, "Fees Has Been Paid Already!!!", this.Page);
                    divCourses.Visible = false;
                    return;
                }
                else
                {
                    status = fcc.InsertManualRegistrationFeeEntry(Convert.ToInt32(lblName.ToolTip), txtApplicationId.Text, lblName.Text, Convert.ToInt32(lblDegreeName.ToolTip), lblAppliedFees.Text, Convert.ToInt32(ddlDegree.SelectedValue));
                }
            }

            if (status == 1)
            {
                objCommon.DisplayMessage(updFine, "Manual Registration Fees Collection Has Been Done!!!", this.Page);
                divCourses.Visible = false;
                ddlDegree.SelectedIndex = 0;
                txtApplicationId.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtApplicationId.Text = string.Empty;
        divCourses.Visible = false;
    }
}
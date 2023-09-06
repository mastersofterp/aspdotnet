//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT TRANSPORT REQUISITION APPLICATION
// MODIFY DATE   : 23-JUL-2021
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DESC : CREATE TRANSPORT DEMAND FOR FEES.
//======================================================================================


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;

using System.Net.Mail;
using System.Net;
using System.Text;

public partial class VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    VMController objVMC = new VMController();
    VM objVM = new VM();


    #region   Page Load Event

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
                    this.GetTransportStatus();
                    txtDate.Text = Convert.ToString(System.DateTime.Now);
                   
                    ViewState["action"] = "add";
                    BindlistView();
                    FillDropDownList();
                   
                    ViewState["VTRAID"] = null;

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.Page_Load -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    #endregion
    #region UserDefined Methods

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

    protected void Clear()
    {
        ddlCategory.SelectedIndex = 0;
        ddlVehicleStop.SelectedIndex = 0;
        ViewState["VTRAID"] = null;
        ViewState["action"] = "add";
        txtPeriodfrom.Text = string.Empty;
        txtPeriodTo.Text = string.Empty;
        txtFeesAmount.Text = string.Empty;
    }

    private void FillDropDownList()
    {
        objCommon.FillDropDownList(ddlVehicleStop, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "", "STOPNAME");    

        int CollegeId = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"])));
        int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "FLOCK=1 AND COLLEGE_ID=" + CollegeId));

        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND FLOCK=1 AND COLLEGE_ID=" + CollegeId + "", "");
        ddlSession.SelectedValue = objCommon.LookUp("ACD_SESSION_MASTER", "SESSIONNO", "SESSIONNO=" + SessionNo);
        
        // objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", "IsActive=1 And EndTime='9999-12-31' AND SESSIONNO=" + SessionNo + " AND COLLEGE_ID=" + CollegeId, "CATEGORYNAME");
       

       // string Yearwise = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO)", "ISNULL(D.YEARWISE,0)", "IDNO=" + Convert.ToInt32(Session["idno"]));
        //if (Yearwise.ToString() == "1")
        //{
        //    this.objCommon.FillDropDownList(ddlSemester, "ACD_YEAR", "SEM", "YEARNAME", "YEARNAME <> '-' AND SEM > 0", "SEM");
        //     ddlSemester.SelectedValue = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
        //}
        //else
        //{
        //    this.objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNAME <> '-' AND SEMESTERNO > 0", "SEMESTERNO");
        //    ddlSemester.SelectedValue = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + Convert.ToInt32(Session["idno"]));
        //}
        
         
        
        DataSet ds = null;
        ds = objVMC.GetStudentPersonalDetails(Convert.ToInt32(Session["idno"]));
        if (ds.Tables[0].Rows.Count > 0)
        { 
            txtstudname.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
            txtDegree.Text = ds.Tables[0].Rows[0]["DEGREENAME"].ToString();
            txtbranch.Text = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
            txtsemester.Text = ds.Tables[0].Rows[0]["SEMFULLNAME"].ToString();
            txtMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
            txtAdmissionNo.Text = ds.Tables[0].Rows[0]["ENROLLNO"].ToString();
            txtInstitution.Text = ds.Tables[0].Rows[0]["COLLEGE_NAME"].ToString();
            txtBatch.Text = ds.Tables[0].Rows[0]["BATCHNAME"].ToString();
            string str = txtBatch.Text;
            string[] tokens = str.Split('-');
            string periodfrom = tokens[0].ToString();
            string periodto = tokens[1].ToString();
            txtPeriodfrom.Text = periodfrom;
            if (periodto != "")
            {
                txtPeriodTo.Text = "20" + periodto;
            }
            else
            {
                txtPeriodTo.Text = periodto;
            }
        }
    }

    private void BindlistView()
    {
        try
        {
            DataSet dss = objVMC.GetTransportRequisitionApplicationList(Convert.ToInt32(Session["idno"]));
            if (dss.Tables[0].Rows.Count > 0)
            {
                //ViewState["GRIV_ID"] = dss.Tables[0].Rows[0]["GRIV_ID"].ToString();
                lvTRApplication.DataSource = dss;
                lvTRApplication.DataBind();
            }
            else
            {
                lvTRApplication.DataSource = null;
                lvTRApplication.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.BindlistView -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    private void GetTransportStatus()
    {
        try
        {           
            string TransportStatus = objCommon.LookUp("ACD_STUDENT", "TRANSPORT", "IDNO=" + Convert.ToInt32(Session["idno"]));
            if (TransportStatus == "0")
            {
                btnSave.Enabled = false;
                objCommon.DisplayMessage(this.updApplication, "Transport status is not updated", this.Page);
            }
            else
            {
                btnSave.Enabled = true;
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.ShowDetails() -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


    #region Page Event

    private bool checkApplicationExist()
    {

        bool retVal = false;
        //DataSet ds = objCommon.FillDropDown("VEHICLE_TRANSPORT_REQUISITION_APPLICATION", "VTRAID", "STUD_IDNO", "STATUS NOT IN ('R', 'C') AND STUD_IDNO=" + Convert.ToInt32(Session["idno"]) + " AND SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "VTRAID");
        DataSet ds = objCommon.FillDropDown("VEHICLE_TRANSPORT_REQUISITION_APPLICATION", "VTRAID", "STUD_IDNO", "STATUS NOT IN ('R', 'C') AND STUD_IDNO=" + Convert.ToInt32(Session["idno"]) , "VTRAID");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                retVal = true;
            }

        }

        return retVal;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            // btnSave.Enabled = false;
            txtDate.Text = Convert.ToString(System.DateTime.Now);
            objVM.VCID = Convert.ToInt32(ddlCategory.SelectedValue);
            objVM.UANO = Convert.ToInt32(Session["idno"]);
            objVM.STOPNO = Convert.ToInt32(ddlVehicleStop.SelectedValue);
            objVM.APP_DATE = System.DateTime.Now;
            objVM.PERIOD_FROM = txtPeriodfrom.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPeriodfrom.Text.Trim());   
            objVM.PERIOD_TO = txtPeriodTo.Text.Trim().Equals(string.Empty) ? string.Empty : Convert.ToString(txtPeriodTo.Text.Trim());  
            objVM.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objVM.S_SEMESTER = Convert.ToInt32(ddlSemester.SelectedValue);

            if (Session["idno"] != null)
            {
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        if (checkApplicationExist())
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Requisition for this session is already exist.');", true);
                            Clear();

                        }
                        else
                        {
                            objVM.VTRAID = 0;
                            CustomStatus cs = (CustomStatus)objVMC.AddUpdVehicleTransportApplication(objVM);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                ViewState["VTRAID"] = objVM.VTRAID;
                                BindlistView();
                                objCommon.DisplayMessage(this.updApplication, "Record Saved Successfully.", this.Page);
                                Clear();
                            }
                        }
                    }
                    else
                    {
                        objVM.VTRAID = Convert.ToInt32(ViewState["VTRAID"]);
                        CustomStatus cs = (CustomStatus)objVMC.AddUpdVehicleTransportApplication(objVM);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindlistView();
                            objCommon.DisplayMessage(this.updApplication, "Record Updated Successfully.", this.Page);
                            Clear();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.btnSave_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }





    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }



    #endregion

    #region
    protected void btnEditRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int VTRAID = int.Parse(btnEdit.CommandArgument);
            ViewState["VTRAID"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            ShowDetails(VTRAID);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.btnEditRecord_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetails(int VTRAID)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("VEHICLE_TRANSPORT_REQUISITION_APPLICATION", "*", "", "STATUS='P' AND VTRAID=" + VTRAID, "VTRAID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["VCID"].ToString();
                //objCommon.FillDropDownList(ddlVehicleStop, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "VCID=" + ddlCategory.SelectedValue, "STOPNAME");
                //ddlVehicleStop.SelectedValue = ds.Tables[0].Rows[0]["STOPID"].ToString();

                ddlVehicleStop.SelectedValue = ds.Tables[0].Rows[0]["STOPID"].ToString();
                objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", "IsActive=1 And EndTime='9999-12-31'", "CATEGORYNAME");
                ddlCategory.SelectedValue = ds.Tables[0].Rows[0]["VCID"].ToString();
                txtFeesAmount.Text = objCommon.LookUp("VEHICLE_CATEGORYMASTER", "AMOUNT", "ISACTIVE=1 AND ENDTIME='9999-12-31' AND VCID=" + ddlCategory.SelectedValue);

                txtPeriodfrom.Text = ds.Tables[0].Rows[0]["PERIOD_FROM"].ToString();
                txtPeriodTo.Text = ds.Tables[0].Rows[0]["PERIOD_TO"].ToString();
            }
            else
            {
                ViewState["VTRAID"] = null;
                ViewState["action"] = "add";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Application Can Not Be Modified.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "VEHICLE_MAINTENANCE_Transaction_TransportRequitionApplication.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  objCommon.FillDropDownList(ddlVehicleStop, "VEHICLE_STOPMASTER", "STOPID", "STOPNAME", "VCID=" + ddlCategory.SelectedValue, "STOPNAME");     

      // txtFeesAmount.Text = objCommon.LookUp("VEHICLE_CATEGORYMASTER", "AMOUNT", "ISACTIVE=1 AND ENDTIME='9999-12-31' AND VCID=" + ddlCategory.SelectedValue);// add endtime condition

    }

    #endregion



    protected void ddlVehicleStop_SelectedIndexChanged(object sender, EventArgs e)
    {
        string vcId = objCommon.LookUp("VEHICLE_STOPMASTER", "VCID", "STOPID=" + Convert.ToInt32(ddlVehicleStop.SelectedValue));
      //  objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", "IsActive=1 And EndTime='9999-12-31' AND VCID=" + vcId , "CATEGORYNAME");
        objCommon.FillDropDownList(ddlCategory, "VEHICLE_CATEGORYMASTER", "VCID", "CATEGORYNAME", " VCID=" + vcId, "CATEGORYNAME");

        ddlCategory.SelectedValue = vcId;
       // txtFeesAmount.Text = objCommon.LookUp("VEHICLE_CATEGORYMASTER", "AMOUNT", "ISACTIVE=1 AND ENDTIME='9999-12-31' AND VCID=" + ddlCategory.SelectedValue);
        txtFeesAmount.Text = objCommon.LookUp("VEHICLE_CATEGORYMASTER", "AMOUNT", " VCID=" + ddlCategory.SelectedValue);

    
    }
}
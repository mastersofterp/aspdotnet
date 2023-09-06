using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class ACADEMIC_AdmissionStatusMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    static int StatusID;
    StudentController objSC = new StudentController();
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
                //CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                BindListView();
               
            }
            ViewState["action"] = "add";

        }
    }
    private void BindListView()
    {
        try
        {
            StudentController objSC = new StudentController();
            DataSet ds = objSC.GetAllAdmStatus();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                lvAdmissionstatus.DataSource = ds;
                lvAdmissionstatus.DataBind();
            }
            else
            {

                lvAdmissionstatus.DataSource = null;
                lvAdmissionstatus.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }



    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=DefineAcademicYear.aspx");
        }
    }


    private void ClearControls()
    {
        txtstatusdescp.Text = string.Empty;      
        btnSubmit.Text = "Submit";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int admstatusId = 0;
        int IsActive ;
        int IsAdmcancel = 0;
        
        try
        {
            
                StudentController objSC = new StudentController();
                string admstatus = txtstatusdescp.Text.ToString();
                
                if (hfdActive.Value == "true")
                {
                    IsActive = 1;
                }
                else
                {
                    IsActive = 0;

                }
                if (chkIsAdmcancel.Checked)
                {
                    IsAdmcancel = 1;
                }
                else
                {
                    IsAdmcancel = 0;
                }
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        
                        CustomStatus cs = (CustomStatus)objSC.AddAdmissionStatus(admstatus, IsActive, IsAdmcancel);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            ViewState["action"] = "add";
                            ClearControls();
                            objCommon.DisplayMessage(this.updstatus, "Record Saved Successfully!", this.Page);
                        }
                        else
                        {
                            ViewState["action"] = "add";
                            ClearControls();
                            objCommon.DisplayMessage(this.updstatus, "Record Already Exist !", this.Page);
                        }
                        //if (cs.Equals(CustomStatus.DuplicateRecord))
                        //{
                        //    objCommon.DisplayMessage(this.updstatus, "Record Already Exist", this.Page);
                        //}
                        //else if (cs.Equals(CustomStatus.RecordSaved))
                        //{
                        //    ViewState["action"] = "add";
                        //    ClearControls();
                        //    objCommon.DisplayMessage(this.updstatus, "Record Saved Successfully!", this.Page);
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage(this.updstatus, "Error !", this.Page);
                        //}
                    }
                    else
                    {
                       

                        int admstatusId = Convert.ToInt32(ViewState["statusid"]);
                        CustomStatus cs = (CustomStatus)objSC.UpdateAdmissionStatus(admstatus, IsActive, admstatusId, IsAdmcancel);

                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            ViewState["action"] = null;
                            
                            objCommon.DisplayMessage(this.updstatus, "Record Updated Successfully!", this.Page);
                            
                            ClearControls();
                            btnSubmit.Text = "Submit";
                            txtstatusdescp.Focus();
                        }
                        else
                        {
                            ViewState["action"] = null;
                            // ClearField();
                            objCommon.DisplayMessage(this.updstatus, "Record Already Exist !", this.Page);
                            ClearControls();
                        }
                        //if (cs.Equals(CustomStatus.RecordExist))
                        //{
                        //    objCommon.DisplayMessage(this.updstatus, "Record Already Exist", this.Page);
                        //}
                        //else if (cs.Equals(CustomStatus.RecordUpdated))
                        //{
                        //    objCommon.DisplayMessage(this.updstatus, "Record Updated Successfully!", this.Page);
                        //    ClearControls();
                        //    btnSubmit.Text = "Submit";
                        //    txtstatusdescp.Focus();
                        //}
                        //else
                        //{
                        //    objCommon.DisplayMessage(this.updstatus, "Error !", this.Page);
                        //}

                    }

                    BindListView();
                }
                btnSubmit.Text = "Submit";
            }

        
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();
        Response.Redirect(Request.Url.ToString());
    }

    private void ShowDetail(int statusid)
    {
        
        StudentController objSC = new StudentController();
        SqlDataReader dr = objSC.GetAdmStatus(statusid);

        if (dr != null)
        {
            if (dr.Read())
            {
                txtstatusdescp.Text = dr["STUDENT_ADMISSION_STATUS_DESCRIPTION"] == null ? string.Empty : dr["STUDENT_ADMISSION_STATUS_DESCRIPTION"].ToString();


                if (dr["ACTIVE_STATUS"].ToString() == "Active")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStatActive(false);", true);
                }
                

                if (Convert.ToInt32(dr["IS_ADM_CANCEL"]) == 1)
                {
                    chkIsAdmcancel.Checked = true;
                }
                else
                {
                    chkIsAdmcancel.Checked = false;
                }


            }
        }
        if (dr != null) dr.Close();
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
          
            ImageButton btnEdit = sender as ImageButton;
            StatusID = int.Parse(btnEdit.CommandArgument);
            ViewState["statusid"] = StatusID;
            ShowDetail(StatusID);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            txtstatusdescp.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
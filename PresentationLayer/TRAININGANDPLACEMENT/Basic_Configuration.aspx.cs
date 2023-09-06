//=================================================================================
// PROJECT NAME  : RF Campus.                                                          
// MODULE NAME   : T and P Master Pages New (SLIT).                                    
// CREATION DATE : 19-01-2022.
// CREATED BY    : Ro-hit More.                            
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;


public partial class Basic_Configuration : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objTP = new TPController();
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    
                   
                    
                    BindListViewCarrerArea();

                    lvWorkArea.DataSource = null;
                    lvWorkArea.DataBind();
                    
                    
                   
                   
                   
                    
                   
                    objCommon.FillDropDownList(ddlJobTypes, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS>0", "JOBTYPE");

                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }


    #region Currency Page Code Starts From Here


    private void BindListViewCurrency()
    {
        try
        {
            DataSet ds = objTP.GetCurrency();
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCurrency.DataSource = ds;
                lvCurrency.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvCurrency.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_ClickCurrency(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect(Request.Url.ToString());
            ClearCurrency();

            lvWorkArea.Visible = false;
            LvProficiency.Visible = false;
            lvlanguage.Visible = false;
            lsLevel.Visible = false;
            lvskill.Visible = false;
            lvIntervals.Visible = false;
            lvRound.Visible = false;
            lvAssocationfor.Visible = false;
            lvPlacementStatus.Visible = false;
            lvJobRole.Visible = false;
            lvJobType.Visible = false;
            lvJobSector.Visible = false;
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearCurrency()
    {
        txtCurrency.Text = string.Empty;
        ViewState["action"] = "add";
        lvCurrency.DataSource = null;
        lvCurrency.DataBind();
    }

    protected void btnSubmit_ClickCurrency(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            DataSet ds1 = objCommon.FillDropDown("ACD_TP_COMPSCHEDULE A inner join ACD_CURRENCY B On (A.CURRENCY = B.CUR_NO)", "CUR_NO", "CUR_NAME", "CUR_NO='" +Convert.ToInt32( ViewState["JOBNO"]) + "'", "CUR_NO");
            if (ds1.Tables[0].Rows.Count == 0)
            {
                if (hfCurrency.Value == "true")
                {
                    status = 1;
                }
                else
                {
                    status = 0;
                }
            }
            else
            {
                status = 1;
            }
            if (ViewState["action"] != null)
            {
               
                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}
                    
                    CustomStatus cs = (CustomStatus)objTP.AddCurrency(Convert.ToString(txtCurrency.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindListViewCurrency();
                        lvCurrency.Visible = false;
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        txtCurrency.Text = string.Empty;
                        
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                    ClearCurrency();
                }
                else
                {
                    if (ViewState["JOBNO"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["JOBNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_CURRENCY", "CUR_NO", "CUR_NAME", "CUR_NAME='" + Convert.ToString(txtCurrency.Text.Trim()) + "'and CUR_NO!='" + JLNO + "'", "CUR_NO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {

                             CustomStatus cs = (CustomStatus)objTP.UpdateCurrency(JLNO, Convert.ToString(txtCurrency.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                               //  BindListViewCurrency();
                                 lvCurrency.Visible = false;
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 txtCurrency.Text = string.Empty;
                                 //ClearCurrency();
                             }
                         }
                         else
                         {
                             // objCommon.DisplayMessage(upnlJobType, "Error occured while updating.", this.Page);
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exist.", this.Page);
                             //Clear();
                         }
                    }
                }
            }
           // BindListViewCurrency();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_ClickCurrency(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int Curno = int.Parse(btnEdit.CommandArgument);
            ViewState["JOBNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailscurrency(Curno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailscurrency(int Curno)
    {
        try
        {
            

                DataSet ds = objTP.GetCurrency(Curno);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtCurrency.Text = ds.Tables[0].Rows[0]["CUR_NAME"].ToString();
                    //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                    //{
                    //    chkStatusCurrency.Checked = true;
                    //}
                    //else
                    //{
                    //    chkStatusCurrency.Checked = false;

                    //}    

                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(true);", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetStat(false);", true);
                    }
                }
            
        
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


    #region JobType Page Code starts from Here

    private void BindListViewJobType()
    {
        try
        {
            DataSet ds = objTP.GetJobType(0);
                //objTP.GetJobType(0);

            if (ds.Tables[0].Rows.Count > 0)
            {
                lvJobType.DataSource = ds;
                lvJobType.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvJobType.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_ClickJobType(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfJobType.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objTP.AddJobType(Convert.ToString(txtJobType.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        lvJobType.Visible = false;
                        ClearJobType();
                       // BindListViewJobType();
                        objCommon.FillDropDownList(ddlJobTypes, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS>0", "JOBTYPE");
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearJobType();
                        
                    }
                }
                else
                {
                    if (ViewState["JOBNO"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["JOBNO"].ToString());

                        DataSet ds = objCommon.FillDropDown("ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "JOBTYPE='" + Convert.ToString(txtJobType.Text.Trim()) + "'and JOBNO!='" + JLNO + "'", "JOBNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateJobType(JLNO, Convert.ToString(txtJobType.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 lvJobType.Visible = false;
                                 ClearJobType();
                                // BindListViewJobType();
                                 objCommon.FillDropDownList(ddlJobTypes, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS>0", "JOBTYPE");
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exist.", this.Page);
                             ClearJobType();
                         }
                    }

                }
            }
         //   BindListViewJobType();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int JOBNO = int.Parse(btnEdit.CommandArgument);
            ViewState["JOBNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsJobType(JOBNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsJobType(int JOBNO)
    {
        try
        {
            DataSet ds = objTP.GetJobType(JOBNO);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtJobType.Text = ds.Tables[0].Rows[0]["JOBTYPE"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals("ACTIVE")))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobType(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobType(false);", true);
                }
               
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ClearJobType()
    {
        txtJobType.Text = string.Empty;
    }

    protected void btnCancelJobType_Click(object sender, EventArgs e)
    {
        ClearJobType();
        ViewState["action"] = "add";
        lvJobType.DataSource = null;
        lvJobType.DataBind();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    #endregion


    #region JobSector Page Code Starts from Here

    protected void btnSubmitJobSector_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfJobSector.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objTP.AddJobSector(Convert.ToString(txtJobSector.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       // BindListViewJobSector();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        ClearSector();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearSector();
                    }
                }
                else
                {
                    if (ViewState["JOBSECNO"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["JOBSECNO"].ToString());
                        DataSet ds = objCommon.FillDropDown("ACD_TP_JOBSECTOR", "JOBSECNO", "JOBSECTOR", "JOBSECTOR='" + Convert.ToString(txtJobSector.Text.Trim()) + "'and JOBSECNO!='" + JLNO + "'", "JOBSECNO");
                        if (ds.Tables[0].Rows.Count==0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.UpdateJobSector(JLNO, Convert.ToString(txtJobSector.Text.Trim()), status);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {
                             //   BindListViewJobType();
                                lvJobType.Visible = false;
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                ClearSector();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Record Already Exist.", this.Page);
                            ClearSector();
                        }
                    }

                }
            }
           // BindListViewJobSector();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditJobSector_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int JOBNO = int.Parse(btnEdit.CommandArgument);
            ViewState["JOBSECNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsJobSector(JOBNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsJobSector(int jobsecno)
    {
        try
        {
            DataSet ds = objTP.GetJobSector(jobsecno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtJobSector.Text = ds.Tables[0].Rows[0]["JOBSECTOR"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobSector(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobSector(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelJobSector_Click(object sender, EventArgs e)
    {
        ClearSector();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvCurrency.Visible = false;
        
    }

    protected void ClearSector()
    {
        txtJobSector.Text = string.Empty;
        ViewState["action"] = "add";
        lvJobSector.DataSource = null;
        lvJobSector.DataBind();
    }
    private void BindListViewJobSector()
    {
        try
        {
            DataSet ds = objTP.GetJobSectorLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvJobSector.DataSource = ds;
                lvJobSector.DataBind();
            }

            foreach (ListViewDataItem dataitem in lvJobSector.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion


    #region Carrer Area Page Code starts from Here

    private void ShowDetailsCarrerArea(int carareano)
    {
        try
        {
            DataSet ds = objTP.GetCarrerArea(carareano);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtCareerAreas.Text = ds.Tables[0].Rows[0]["WORKAREANAME"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCarrerArea(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCarrerArea(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEditCarrerArea_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnEdit = sender as LinkButton;
            int carareano = int.Parse(btnEdit.CommandArgument);
            ViewState["CarrerAreaNo"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCarrerArea(carareano);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitCarrerArea_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfCarrerArea.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objTP.AddCarrerArea(Convert.ToString(txtCareerAreas.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                     //   BindListViewJobSector();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        ClearcarrerArea();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearcarrerArea();
                    }
                }
                else
                {
                    if (ViewState["CarrerAreaNo"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["CarrerAreaNo"].ToString());

                        CustomStatus cs = (CustomStatus)objTP.UpdateCarrerArea(JLNO, Convert.ToString(txtCareerAreas.Text.Trim()), status);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                         //   BindListViewCarrerArea();
                            ViewState["action"] = "add";
                            objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                            ClearcarrerArea();
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Error occured while updating.", this.Page);
                            ClearcarrerArea();
                        }
                    }

                }
            }
           // BindListViewCarrerArea();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    
    private void BindListViewCarrerArea()
    {
        try
        {
            DataSet ds = objTP.GetCarrerAreaLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCarrerArea.DataSource = ds;
                lvCarrerArea.DataBind();
            }


            foreach (ListViewDataItem dataitem in lvCarrerArea.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ClearcarrerArea()
    {
        txtCareerAreas.Text = string.Empty;
    }
    protected void btnCancelCarrerArea_Click(object sender, EventArgs e)
    {
        ClearcarrerArea();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    #endregion


    #region Association for page code starts from Here


  
    protected void btnEditAssociationfor_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int associationno = int.Parse(btnEdit.CommandArgument);
            ViewState["Associationno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsAssociationfor(associationno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsAssociationfor(int associationno)
    {
        try
        {
            DataSet ds = objTP.GetAssociationFor(associationno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAssociation.Text = ds.Tables[0].Rows[0]["ASSOCIATION_FOR"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetAssociationfor(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetAssociationfor(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitAssociationfor_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfAssociationfor.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                      DataSet ds = objCommon.FillDropDown("ACD_TP_ASSOCIATION_FOR", "ASSOCIATIONNO", "ASSOCIATION_FOR", "ASSOCIATION_FOR='" + Convert.ToString(txtAssociation.Text.Trim()) + "'", "ASSOCIATIONNO");
                      if (ds.Tables[0].Rows.Count == 0)
                      {
                          CustomStatus cs = (CustomStatus)objTP.AddAssociationfor(Convert.ToString(txtAssociation.Text.Trim()), colcode, status);
                          if (cs.Equals(CustomStatus.RecordSaved))
                          {
                             // BindListViewAssociationFor();
                              ViewState["action"] = "add";
                              objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                              ClearAssociationFor();
                          }
                      }
                  //  if (cs.Equals(CustomStatus.RecordExist))
                      else
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearAssociationFor();
                    }
                }
                else
                {
                    if (ViewState["Associationno"] != null)
                    {
                        int assno = Convert.ToInt32(ViewState["Associationno"].ToString());
                        DataSet ds = objCommon.FillDropDown("ACD_TP_ASSOCIATION_FOR", "ASSOCIATIONNO", "ASSOCIATION_FOR", "ASSOCIATION_FOR='" + Convert.ToString(txtAssociation.Text.Trim()) + "' and ASSOCIATIONNO!='" + assno + "'", "ASSOCIATIONNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateAssociationFor(assno, Convert.ToString(txtAssociation.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {
                                // BindListViewAssociationFor();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 ClearAssociationFor();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             ClearAssociationFor();
                         }
                    }

                }
            }
           // BindListViewAssociationFor();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewAssociationFor()
    {
        try
        {
            DataSet ds = objTP.GetAssociationforLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvAssocationfor.DataSource = ds;
                lvAssocationfor.DataBind();
            }


            foreach (ListViewDataItem dataitem in lvAssocationfor.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearAssociationFor()
    {
        txtAssociation.Text = string.Empty;
        ViewState["action"] = "add";
        lvAssocationfor.DataSource = null;
        lvAssocationfor.DataBind();
    }
    protected void btnCancelAssociationFor_Click(object sender, EventArgs e)
    {
        ClearAssociationFor();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    #endregion


    #region Placement Status Page Code starts from Here
    protected void btnSubmitPlacementStatus_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfPlacementStatus.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                     //int JLNO = Convert.ToInt32(ViewState["JOBSECNO"].ToString());
                     DataSet ds = objCommon.FillDropDown("ACD_TP_PLACEMENT_STATUS", "STATUS_NO", "PLACED_STATUS", "PLACED_STATUS='" + Convert.ToString(txtStatus.Text.Trim()) + "'", "STATUS_NO");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.AddPlacementStatus(Convert.ToString(txtStatus.Text.Trim()), colcode, status);
                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                               // BindListPlacemntStatus();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                                ClearPlacementStatus();
                            }
                        }
                   // if (cs.Equals(CustomStatus.RecordExist))
                        else
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearPlacementStatus();
                    }
                }
                else
                {
                    if (ViewState["statusno"] != null)
                    {
                        int statusno = Convert.ToInt32(ViewState["statusno"].ToString());
                        DataSet ds = objCommon.FillDropDown("ACD_TP_PLACEMENT_STATUS", "STATUS_NO", "PLACED_STATUS", "PLACED_STATUS='" + Convert.ToString(txtStatus.Text.Trim()) + "' and STATUS_NO!='"+statusno+"'", "STATUS_NO");
                          if (ds.Tables[0].Rows.Count == 0)
                          {
                              CustomStatus cs = (CustomStatus)objTP.UpdatePlacementStatus(statusno, Convert.ToString(txtStatus.Text.Trim()), status);
                              if (cs.Equals(CustomStatus.RecordUpdated))
                              {
                                //  BindListPlacemntStatus();
                                  ViewState["action"] = "add";
                                  objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                  ClearPlacementStatus();
                              }
                          }
                          else
                          {
                              objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                              ClearPlacementStatus();
                          }
                    }

                }
            }
            //BindListPlacemntStatus();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditPlacementStatus_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int statusno = int.Parse(btnEdit.CommandArgument);
            ViewState["statusno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsPlacementStatus(statusno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListPlacemntStatus()
    {
        try
        {
            DataSet ds = objTP.GetPlacementStatusLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvPlacementStatus.DataSource = ds;
                lvPlacementStatus.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvPlacementStatus.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsPlacementStatus(int statusno)
    {
        try
        {
            DataSet ds = objTP.GetPlacementStatus(statusno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtStatus.Text = ds.Tables[0].Rows[0]["PLACED_STATUS"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPlacementStatus(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetPlacementStatus(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancelPlacementStatus_Click(object sender, EventArgs e)
    {
        ClearPlacementStatus();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    protected void ClearPlacementStatus()
    {
        txtStatus.Text = string.Empty;
        ViewState["action"] = "add";
        lvPlacementStatus.DataSource = null;
        lvPlacementStatus.DataBind();
    }

    #endregion


   #region Job Role Page code starts from Here

    protected void btnEditJobRole_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int jobroleno = int.Parse(btnEdit.CommandArgument);
            ViewState["jobroleno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsJobRole(jobroleno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsJobRole(int jobroleno)
    {
        try
        {
            DataSet ds = objTP.GetJobRole(jobroleno);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtJobRoleName.Text = ds.Tables[0].Rows[0]["JOBROLETYPE"].ToString();
                objCommon.FillDropDownList(ddlJobTypes, "ACD_TP_JOBTYPE", "JOBNO", "JOBTYPE", "STATUS>0", "JOBTYPE");

                ddlJobTypes.SelectedValue = ds.Tables[0].Rows[0]["JOBNO"].ToString();

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobRole(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetJobRole(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnSubmitJobRole_Click(object sender, EventArgs e)
    {

        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            
            if (hfJobRole.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    DataSet ds = objCommon.FillDropDown("ACD_TP_JOB_ROLE", "ROLENO", "JOBROLETYPE", "JOBROLETYPE='"+Convert.ToString(txtJobRoleName.Text.Trim())+"'and JOBNO='"+ Convert.ToInt32(ddlJobTypes.SelectedValue)+"'", "ROLENO");
                    if(ds.Tables[0].Rows.Count==0)
                    {
                    CustomStatus cs = (CustomStatus)objTP.AddJobRole(Convert.ToString(txtJobRoleName.Text.Trim()), colcode, status, Convert.ToInt32(ddlJobTypes.SelectedValue));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       // BindListJobRole();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        ClearJobRole();
                    }
                    }
                    //if (cs.Equals(CustomStatus.RecordExist))
                    else
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        ClearJobRole();
                        return;
                    }
                }
                else
                {
                    if (ViewState["jobroleno"] != null)
                    {
                        int roleno = Convert.ToInt32(ViewState["jobroleno"].ToString());
                        DataSet ds = objCommon.FillDropDown("ACD_TP_JOB_ROLE", "ROLENO", "JOBROLETYPE", "JOBROLETYPE='" + Convert.ToString(txtJobRoleName.Text.Trim()) + "'and JOBNO='" + Convert.ToInt32(ddlJobTypes.SelectedValue) + "' and ROLENO!='"+roleno+"'", "ROLENO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateJobRole(roleno, Convert.ToString(txtJobRoleName.Text.Trim()), status, Convert.ToInt32(ddlJobTypes.SelectedValue));
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {
                                 //BindListJobRole();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 ClearJobRole();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             ClearJobRole();
                         }
                    }

                }
            }
          //  BindListJobRole();
            ClearJobRole();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListJobRole()
    {
        try
        {
            DataSet ds = objTP.GetJobRoleLV();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvJobRole.DataSource = ds;
                lvJobRole.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvJobRole.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearJobRole()
    {
        txtJobRoleName.Text = string.Empty;
        ddlJobTypes.SelectedIndex=0;
        ViewState["action"] = "add";
        lvJobRole.DataSource = null;
        lvJobRole.DataBind();
    }

    protected void btnCancelJobRole_Click(object sender, EventArgs e)
    {
        ClearJobRole();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;

    }
    #endregion


   #region Round Page Code Starts From Here


    private void BindListViewRounds()
    {
        try
        {
            DataSet ds = objTP.BindLVRounds();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvRound.DataSource = ds;
                lvRound.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvRound.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_ClickRound(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect(Request.Url.ToString());
            ClearRound();
            lvWorkArea.Visible = false;
            LvProficiency.Visible = false;
            lvlanguage.Visible = false;
            lsLevel.Visible = false;
            lvskill.Visible = false;
            lvIntervals.Visible = false;
            lvAssocationfor.Visible = false;
            lvPlacementStatus.Visible = false;
            lvJobRole.Visible = false;
            lvJobType.Visible = false;
            lvJobSector.Visible = false;
            lvCurrency.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearRound()
    {
        txtRound.Text = string.Empty;
        ViewState["action"] = "add";
        lvRound.DataSource = null;
        lvRound.DataBind();
    }

    protected void btnSubmit_ClickRound(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfRounds.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddRounds(Convert.ToString(txtRound.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        //BindListViewRounds();
                        ClearRound();
                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        
                    }
                   // ClearCurrency();
                }
                else
                {
                    if (ViewState["JOBNO"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["JOBNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_SELECTIONTYPE", "SELECTNO", "SELECTNAME", "SELECTNAME='" + Convert.ToString(txtRound.Text.Trim()) + "' and SELECTNO != '" + JLNO + "'", "SELECTNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateRounds(JLNO, Convert.ToString(txtRound.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {


                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                              //   BindListViewRounds();
                                 ClearRound();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             //Clear();
                         }
                    }

                }
            }
            //BindListViewRounds();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_ClickRounds(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int roundno = int.Parse(btnEdit.CommandArgument);
            ViewState["JOBNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsRounds(roundno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsRounds(int roundno)
    {
        try
        {
            DataSet ds = objTP.EditRoundsByID(roundno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtRound.Text = ds.Tables[0].Rows[0]["SELECTNAME"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                //{
                //    chkStatusCurrency.Checked = true;
                //}
                //else
                //{
                //    chkStatusCurrency.Checked = false;

                //}    

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetRound(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetRound(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion


   #region Intervals Page Code Starts From Here


    private void BindListViewIntervals()
    {
        try
        {
            DataSet ds = objTP.BindLVIntervals();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvIntervals.DataSource = ds;
                lvIntervals.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvIntervals.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_ClickIntervals(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect(Request.Url.ToString());
            ClearIntervals();
            lvWorkArea.Visible = false;
            LvProficiency.Visible = false;
            lvlanguage.Visible = false;
            lsLevel.Visible = false;
            lvskill.Visible = false;
            lvRound.Visible = false;
            lvAssocationfor.Visible = false;
            lvPlacementStatus.Visible = false;
            lvJobRole.Visible = false;
            lvJobType.Visible = false;
            lvJobSector.Visible = false;
            lvCurrency.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnCancel_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void ClearIntervals()
    {
        txtIntervals.Text = string.Empty;
        ViewState["action"] = "add";
        lvIntervals.DataSource = null;
        lvIntervals.DataBind();
    }

    protected void btnSubmit_ClickIntervals(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfIntervals.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddIntervals(Convert.ToString(txtIntervals.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                     //   BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        txtIntervals.Text = string.Empty;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                    ClearIntervals();
                }
                else
                {
                    if (ViewState["JOBNO"] != null)
                    {
                        int JLNO = Convert.ToInt32(ViewState["JOBNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_INTERVALS", "INTNO", "INTERVALS", "INTERVALS='" + Convert.ToString(txtIntervals.Text.Trim()) + "' and INTNO!='" + JLNO + "'", "INTNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateIntervals(JLNO, Convert.ToString(txtIntervals.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                               //  BindListViewIntervals();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 //ClearCurrency();
                                 ClearIntervals();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             //Clear();
                         }
                         ClearIntervals();
                    }

                }
            }
          //  BindListViewIntervals();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_ClickIntervals(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int intno = int.Parse(btnEdit.CommandArgument);
            ViewState["JOBNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsIntervals(intno);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void ShowDetailsIntervals(int intno)
    {
        try
        {
            DataSet ds = objTP.EditIntervalsByID(intno);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtIntervals.Text = ds.Tables[0].Rows[0]["INTERVALS"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                //{
                //    chkStatusCurrency.Checked = true;
                //}
                //else
                //{
                //    chkStatusCurrency.Checked = false;

                //}    

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetIntervals(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetIntervals(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    #endregion

    //-----Start---18-11-2022---

    protected void btnEditSkillfor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int SKILNO = int.Parse(btnEdit.CommandArgument);
            ViewState["SKINO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsSkills(SKILNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsSkills(int SKILNO)
    {
        try
        {
            DataSet ds = objTP.EditSkillsByID(SKILNO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtSkills.Text = ds.Tables[0].Rows[0]["SKILLS"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                //{
                //    chkStatusCurrency.Checked = true;
                //}
                //else
                //{
                //    chkStatusCurrency.Checked = false;

                //}    

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSkills(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetSkills(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSkill_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfSkills.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddSkillS(Convert.ToString(txtSkills.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                      //  BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                   ClearSkills();
                }
                else
                {
                    if (ViewState["SKINO"] != null)
                    {
                        int SKNO = Convert.ToInt32(ViewState["SKINO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_SKILLS", "SKILNO", "SKILLS", "SKILLS='" + Convert.ToString(txtSkills.Text.Trim()) + "' and SKILNO!='" + SKNO + "'", "SKILNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateSkills(SKNO, Convert.ToString(txtSkills.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                                // BindListViewIntervals();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 ClearSkills();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             //Clear();
                         }
                    }

                }
            }
          //  BindListViewSkills();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void BtnSkillCacel_Click(object sender, EventArgs e)
    {
        ClearSkills();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }

    protected void ClearSkills()
    {
        txtSkills.Text = string.Empty;
        ViewState["action"] = "add";
        lvskill.DataSource = null;
        lvskill.DataBind();
    }

    private void BindListViewSkills()
    {
        try
        {
            DataSet ds = objTP.BindLVSkills();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvskill.DataSource = ds;
                lvskill.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvskill.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    //-----end---18-11-2022---
    protected void btnlevelSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfLevel.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.Addlevels(Convert.ToString(txtlevel.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                    Clearlevel();
                }
                else
                {
                    if (ViewState["LEVELNO"] != null)
                    {
                        int LEVELNO = Convert.ToInt32(ViewState["LEVELNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_LEVEL", "LEVELNO", "LEVELS", "LEVELS='" + Convert.ToString(txtlevel.Text.Trim()) + "' and LEVELNO!='" + LEVELNO + "'", "LEVELNO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateLevels(LEVELNO, Convert.ToString(txtlevel.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                               //  BindListViewIntervals();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 Clearlevel();
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             //Clear();
                         }
                    }

                }
            }
         //   BindListViewLevel();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }
    private void BindListViewLevel()
    {
        try
        {
            DataSet ds = objTP.BindLVLEVELS();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lsLevel.DataSource = ds;
                lsLevel.DataBind();
            }
            foreach (ListViewDataItem dataitem in lsLevel.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void Clearlevel()
    {
        txtlevel.Text = string.Empty;
        ViewState["action"] = "add";
        lsLevel.DataSource = null;
        lsLevel.DataBind();
    }
    protected void btnlevelCancel_Click(object sender, EventArgs e)
    {
        Clearlevel();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    protected void btnEditlevelfor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int LEVELNO = int.Parse(btnEdit.CommandArgument);
            ViewState["LEVELNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsLevel(LEVELNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsLevel(int LEVELNO)
    {
        try
        {
            DataSet ds = objTP.EditlevelsByID(LEVELNO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtlevel.Text = ds.Tables[0].Rows[0]["LEVELS"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                //{
                //    chkStatusCurrency.Checked = true;
                //}
                //else
                //{
                //    chkStatusCurrency.Checked = false;

                //}    

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetLevels(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetLevels(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnlanguage_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hflanguage.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.Addlanguage(Convert.ToString(txtlanguage.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                      //  BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        lvlanguage.Visible = false;
                        txtlanguage.Text = string.Empty;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                }
                else
                {
                    if (ViewState["LANGUAGENO"] != null)
                    {
                        int LANGUAGENO = Convert.ToInt32(ViewState["LANGUAGENO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_Language", "LANGUAGENO", "LANGUAGES", "LANGUAGES='" + Convert.ToString(txtlanguage.Text.Trim()) + "' and LANGUAGENO!='" + LANGUAGENO + "'", "LANGUAGENO");
                         if (ds.Tables[0].Rows.Count == 0)
                         {
                             CustomStatus cs = (CustomStatus)objTP.UpdateLanguas(LANGUAGENO, Convert.ToString(txtlanguage.Text.Trim()), status);
                             if (cs.Equals(CustomStatus.RecordUpdated))
                             {

                                 //BindListViewIntervals();
                                 ViewState["action"] = "add";
                                 objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                 lvlanguage.Visible = false;
                                 txtlanguage.Text = string.Empty;
                             }
                         }
                         else
                         {
                             objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                             //Clear();
                         }
                    }

                }
            }
           // BindListViewLanguage();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

     }



    private void BindListViewLanguage()
    {
        try
        {
            DataSet ds = objTP.BindLanguage();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvlanguage.DataSource = ds;
                lvlanguage.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvlanguage.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btncanlang_Click(object sender, EventArgs e)
    {
        txtlanguage.Text = string.Empty;
        ViewState["action"] = "add";
        lvlanguage.DataSource = null;
        lvlanguage.DataBind();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;

    }
    protected void btnEditlanguagefor_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int LANGUAGENO = int.Parse(btnEdit.CommandArgument);
            ViewState["LANGUAGENO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailslanguage(LANGUAGENO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailslanguage(int LANGUAGENO)
    {
        try
        {
            DataSet ds = objTP.EditlanguageByID(LANGUAGENO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtlanguage.Text = ds.Tables[0].Rows[0]["LANGUAGES"].ToString();
                //if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                //{
                //    chkStatusCurrency.Checked = true;
                //}
                //else
                //{
                //    chkStatusCurrency.Checked = false;

                //}    

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetLanguage(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetLanguage(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnProficiencySubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfProficiency.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddProficiency(Convert.ToString(txtProficiency.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        clear();

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                    }
                }
                else
                {
                    if (ViewState["PROFNO"] != null)
                    {
                        int PROFNO = Convert.ToInt32(ViewState["PROFNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_PROFICIENCY", "PROFNO", "PROFICIENCY", "PROFICIENCY='" + Convert.ToString(txtProficiency.Text.Trim()) + "' and PROFNO!='" + PROFNO + "'", "PROFNO");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.UpdateProficiency(PROFNO, Convert.ToString(txtProficiency.Text.Trim()), status);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                               // BindListViewIntervals();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                clear();
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                            //Clear();
                        }
                      }

                    
                }
                //BindListViewProficiency();

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void BindListViewProficiency()
    {
        try
        {
            DataSet ds = objTP.BindProficiency();


            if (ds.Tables[0].Rows.Count > 0)
            {
                LvProficiency.DataSource = ds;
                LvProficiency.DataBind();
            }
            foreach (ListViewDataItem dataitem in LvProficiency.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
        lvWorkArea.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }

    public void clear()
    {
        txtProficiency.Text = string.Empty;
        LvProficiency.DataSource = null;
        LvProficiency.DataBind();
    }
    protected void btnEditProficiency_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int PROFNO = int.Parse(btnEdit.CommandArgument);
            ViewState["PROFNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsProficiency(PROFNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsProficiency(int PROFNO)
    {
        try
        {
            DataSet ds = objTP.EditProficiencyByID(PROFNO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtProficiency.Text = ds.Tables[0].Rows[0]["PROFICIENCY"].ToString();
                  

                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetProficiency(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetProficiency(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnShowCurrency_Click(object sender, EventArgs e)
    {
       
        BindListViewCurrency();
        lvCurrency.Visible = true;
       
    }
    protected void btnShowJobSector_Click(object sender, EventArgs e)
    {
        BindListViewJobSector();
        lvJobSector.Visible = true;
    }
    protected void btnShowJobType_Click(object sender, EventArgs e)
    {
        BindListViewJobType();
        lvJobType.Visible = true;
    }
    protected void btnShowJobRole_Click(object sender, EventArgs e)
    {
        BindListJobRole();
        lvJobRole.Visible = true;
    }
    protected void btnShowPlacementStatus_Click(object sender, EventArgs e)
    {
        BindListPlacemntStatus();
        lvPlacementStatus.Visible = true;
    }
    protected void btnShowAssociationFor_Click(object sender, EventArgs e)
    {
        BindListViewAssociationFor();
        lvAssocationfor.Visible = true;
    }
    protected void btnShowRound_Click(object sender, EventArgs e)
    {
        BindListViewRounds();
        lvRound.Visible = true;
    }
    protected void btnShowIntervals_Click(object sender, EventArgs e)
    {
        BindListViewIntervals();
        lvIntervals.Visible = true;
    }
    protected void btnShowSkills_Click(object sender, EventArgs e)
    {
        BindListViewSkills();
        lvskill.Visible = true;
    }
    protected void btnShowlevel_Click(object sender, EventArgs e)
    {
        BindListViewLevel();
        lsLevel.Visible = true;
    }
    protected void btnShowlanguage_Click(object sender, EventArgs e)
    {
        BindListViewLanguage();
        lvlanguage.Visible = true;
    }
    protected void btnShowProficiency_Click(object sender, EventArgs e)
    {
        BindListViewProficiency();
        LvProficiency.Visible = true;
    }
    protected void btnWorkAresSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfWorkArea.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddWorkArea(Convert.ToString(txtWorkArea.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        //clear();
                        txtWorkArea.Text = string.Empty;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();
                        
                    }
                }
                else
                {
                    if (ViewState["WORKAREANO"] != null)
                    {
                        int WORKAREANO = Convert.ToInt32(ViewState["WORKAREANO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}
                        DataSet ds = objCommon.FillDropDown("ACD_TP_WORK_AREA", "WORKAREANO", "WORKAREANAME", "WORKAREANAME='" + Convert.ToString(txtWorkArea.Text.Trim()) + "' and WORKAREANO!='" + WORKAREANO + "'", "WORKAREANO");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.UpdateWorkArea(WORKAREANO, Convert.ToString(txtWorkArea.Text.Trim()), status);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                             //   BindListViewIntervals();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                txtWorkArea.Text = string.Empty;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                            //Clear();
                        }
                    }


                }

                lvWorkArea.DataSource = null;
                lvWorkArea.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewWorkArea()
    {
        try
        {
            DataSet ds = objTP.BindWorkArea();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvWorkArea.DataSource = ds;
                lvWorkArea.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvWorkArea.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnworkareacancel_Click(object sender, EventArgs e)
    {
        txtWorkArea.Text = string.Empty;
        lvWorkArea.DataSource = null;
        lvWorkArea.DataBind();

        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    protected void BtnWorkAreaShow_Click(object sender, EventArgs e)
    {
        BindListViewWorkArea();
        lvWorkArea.Visible = true;
    }
    protected void btnEditWorkArea_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int WORKAREANO = int.Parse(btnEdit.CommandArgument);
            ViewState["WORKAREANO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsWorArea(WORKAREANO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsWorArea(int WORKAREANO)
    {
        try
        {
            DataSet ds = objTP.EditWorkAreaByID(WORKAREANO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtWorkArea.Text = ds.Tables[0].Rows[0]["WORKAREANAME"].ToString();


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWorkArea(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetWorkArea(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnSubmitCategory_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfCategory.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddCategory(Convert.ToString(txtCategory.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        //clear();
                        txtCategory.Text = string.Empty;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();

                    }
                }
                else
                {
                    if (ViewState["CATEGORYNO"] != null)
                    {
                        int CATEGORYNO = Convert.ToInt32(ViewState["CATEGORYNO"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}


                        DataSet ds = objCommon.FillDropDown("ACD_TP_CATEGORY", "CATEGORYNO", "CATEGORYS", "CATEGORYS='" + Convert.ToString(txtCategory.Text.Trim()) + "' and CATEGORYNO!='" + CATEGORYNO + "'", "CATEGORYNO");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.UpdateCategory(CATEGORYNO, Convert.ToString(txtCategory.Text.Trim()), status);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                                //   BindListViewIntervals();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                txtCategory.Text = string.Empty;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                            //Clear();
                        }
                    }


                }

                lvCategory.DataSource = null;
                lvCategory.DataBind();
                lvCategory.Visible = false;
                lvExam.DataSource = null;
                lvExam.DataBind();
                lvExam.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditCategory_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int CATEGORYNO = int.Parse(btnEdit.CommandArgument);
            ViewState["CATEGORYNO"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsCtegory(CATEGORYNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsCtegory(int CATEGORYNO)
    {
        try
        {
            DataSet ds = objTP.EditCategoryByID(CATEGORYNO);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtCategory.Text = ds.Tables[0].Rows[0]["CATEGORYS"].ToString();


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetCategory(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        txtCategory.Text = string.Empty;
        lvCategory.DataSource = null;
        lvCategory.DataBind();
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
   
    protected void btnShowCategory_Click(object sender, EventArgs e)
    {
        lvCategory.Visible = true;
        BindListViewCategory();
    }
    private void BindListViewCategory()
    {
        try
        {
            DataSet ds = objTP.BindCategory();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvCategory.DataSource = ds;
                lvCategory.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvCategory.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExamSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string colcode = Convert.ToString(Session["colcode"]);
            if (hfExam.Value == "true")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            if (ViewState["action"] != null)
            {

                if (ViewState["action"].ToString().Equals("add"))
                {
                    //if (chkStatusCurrency.Checked == true)
                    //{
                    //    status = 1;
                    //}
                    //else {
                    //     status = 0;
                    //}

                    CustomStatus cs = (CustomStatus)objTP.AddExam(Convert.ToString(txtExam.Text.Trim()), colcode, status);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewIntervals();
                        ViewState["action"] = "add";
                        objCommon.DisplayMessage(upnlJobType, "Record Saved Successfully.", this.Page);
                        //clear();
                        txtExam.Text = string.Empty;

                    }
                    if (cs.Equals(CustomStatus.RecordExist))
                    {
                        objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                        //Clear();

                    }
                }
                else
                {
                    if (ViewState["EXAM"] != null)
                    {
                        int EXAM = Convert.ToInt32(ViewState["EXAM"].ToString());
                        //if (chkStatusCurrency.Checked == true)
                        //{
                        //    status = 1;
                        //}
                        //else
                        //{
                        //    status = 0;
                        //}


                        DataSet ds = objCommon.FillDropDown("ACD_TP_Exam", "EXAMNO", "EXAMS", "EXAMS='" + Convert.ToString(txtExam.Text.Trim()) + "' and EXAMNO!='" + EXAM + "'", "EXAMNO");
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            CustomStatus cs = (CustomStatus)objTP.UpdateExam(EXAM, Convert.ToString(txtExam.Text.Trim()), status);
                            if (cs.Equals(CustomStatus.RecordUpdated))
                            {

                                //   BindListViewIntervals();
                                ViewState["action"] = "add";
                                objCommon.DisplayMessage(upnlJobType, "Record Updated Successfully.", this.Page);
                                txtExam.Text = string.Empty;
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(upnlJobType, "Record Already Exists.", this.Page);
                            //Clear();
                        }
                    }


                }

                lvExam.DataSource = null;
                lvExam.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnExamCancel_Click(object sender, EventArgs e)
    {
        txtExam.Text = string.Empty;
        lvExam.DataSource = null;
        lvExam.DataBind();
        lvCategory.Visible = false;
        lvWorkArea.Visible = false;
        LvProficiency.Visible = false;
        lvlanguage.Visible = false;
        lsLevel.Visible = false;
        lvskill.Visible = false;
        lvIntervals.Visible = false;
        lvRound.Visible = false;
        lvAssocationfor.Visible = false;
        lvPlacementStatus.Visible = false;
        lvJobRole.Visible = false;
        lvJobType.Visible = false;
        lvJobSector.Visible = false;
        lvCurrency.Visible = false;
    }
    protected void btnExamShow_Click(object sender, EventArgs e)
    {
        lvExam.Visible = true;
        BindListViewExam();
    }
    private void BindListViewExam()
    {
        try
        {
            DataSet ds = objTP.BindExam();


            if (ds.Tables[0].Rows.Count > 0)
            {
                lvExam.DataSource = ds;
                lvExam.DataBind();
            }
            foreach (ListViewDataItem dataitem in lvExam.Items)
            {
                Label Status = dataitem.FindControl("lblstatus") as Label;
                string Statuss = (Status.Text.ToString());
                if (Statuss == "INACTIVE")
                {
                    Status.CssClass = "badge badge-danger";
                }
                else
                {
                    Status.CssClass = "badge badge-success";
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnEditExam_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int EXAM = int.Parse(btnEdit.CommandArgument);
            ViewState["EXAM"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetailsExam(EXAM);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.btnEdit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetailsExam(int EXAM)
    {
        try
        {
            DataSet ds = objTP.EditExamByID(EXAM);
            if (ds.Tables[0].Rows.Count > 0)
            {

                txtExam.Text = ds.Tables[0].Rows[0]["EXAMS"].ToString();


                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["STATUS"].Equals(1)))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetExam(true);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "SetExam(false);", true);
                }

            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.ShowDetails -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
}
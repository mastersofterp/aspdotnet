using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.IO;

public partial class ESTABLISHMENT_ServiceBook_Pay_SB_Current_Appointment_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ServiceBookController objServiceBook = new ServiceBookController();

    public int _idnoEmp;
    public string path = string.Empty;
    public string Docpath = HttpContext.Current.Server.MapPath("~/ESTABLISHMENT/upload_files/");
    public static string RETPATH = "";

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
                // CheckPageAuthorization();
            }
            //By default setting ViewState["action"] to add
            ViewState["action"] = "add";

            //if (rdbStatus.Checked==true)
            //{
            //    // appstatus.Visible = true;
            //    PanelUni.Visible = true;
            //}
            //else
            //{
            //    //appstatus.Visible = false;
            //    //divapp.Visible = false;
            //    //divdate.Visible = false;
            //    //divdoc.Visible = false;
            //    PanelPG.Visible = false;
            //}

            //if (rdbTeacher.Checked==true)
            //{
            //    //divpgteacher.Visible = true;
            //    divpg.Visible = true;
            //    divpgdt.Visible = true;
            //    divpgdoc.Visible = true;
            //}
            //else
            //{
            //    // divpgteacher.Visible = false;
            //    //divpg.Visible = false;
            //    //divpgdt.Visible = false;
            //    //divpgdoc.Visible = false;
            //}
        }

        //DropDownList ddlempidno = (DropDownList)this.Parent.FindControl("ddlEmployee");
        //_idnoEmp = Convert.ToInt32(ddlempidno.SelectedValue);
        if (Session["serviceIdNo"] != null)
        {
            _idnoEmp = Convert.ToInt32(Session["serviceIdNo"].ToString().Trim());
        }
        BindListCurrentAppointment();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Pay_SB_Current_Appointment_Status.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_SB_Current_Appointment_Status.aspx");
        }
    }

    private void BindListCurrentAppointment()
    {
        try
        {
            DataSet ds = objServiceBook.GetAllCurrentAppointmentDetailsOfEmployee(_idnoEmp);
            lvPrvService.DataSource = ds;
            lvPrvService.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.BindListCurrentAppointment-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }


    protected void rdbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbStatus.SelectedValue == "0")
        {
            // appstatus.Visible = true;
           // PanelUni.Visible = true;
            divapp.Visible = true;
            divdate.Visible = true;
          //  divdoc.Visible = true;



        }
        else
        {
            //appstatus.Visible = false;
            divapp.Visible = false;
            divdate.Visible = false;
            //PanelUni.Visible = false;
            //divdoc.Visible = false;
        }
    }
    protected void rdbTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbTeacher.SelectedValue == "0")
        {
            // divpgteacher.Visible = true;
            //PanelPG.Visible = true;
            divpg.Visible = true;
            divpgdt.Visible = true;
           // divpgdoc.Visible = true;
        }
        else
        {
            // divpgteacher.Visible = false;
            divpg.Visible = false;
            divpgdt.Visible = false;
           // PanelPG.Visible = false;
           // divpgdoc.Visible = false;
        }
    }
    //protected void txtToDate_TextChanged(object sender, EventArgs e)
    //{
    //    DateTime DtFrom, DtTo;
    //    DtFrom = Convert.ToDateTime(txtFromDate.Text);
    //    DtTo = Convert.ToDateTime(txtToDate.Text);
    //    if (DtTo < DtFrom)
    //    {
    //        MessageBox("To Date Should be Greater than  or equal to From Date");
    //        txtToDate.Text = string.Empty;
    //        return;
    //    }
    //    else
    //    {
    //        if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
    //        {
    //            DtFrom = Convert.ToDateTime(txtFromDate.Text);
    //            DtTo = Convert.ToDateTime(txtToDate.Text);
    //            DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();

    //            }
    //        }
    //    }
    //}
    //protected void txtFromDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtToDate.Text != string.Empty && txtToDate.Text != "__/__/____" && txtFromDate.Text != string.Empty && txtFromDate.Text != "__/__/____")
    //    {
    //        DateTime DtFrom = Convert.ToDateTime(txtFromDate.Text);
    //        DateTime DtTo = Convert.ToDateTime(txtToDate.Text);
    //        DataSet ds = objServiceBook.GetTotExperience(DtFrom, DtTo);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            txtExperience.Text = ds.Tables[0].Rows[0]["Total_experience"].ToString();


    //        }
    //    }
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            DataTable dt = null;
            if (ViewState["FILE1"] != null)
            {
                dt = (DataTable)ViewState["FILE1"];
            }

            DateTime DtFrom, DtTo;
            DtFrom = Convert.ToDateTime(txtFromDate.Text);
            DtTo = Convert.ToDateTime(txtToDate.Text);
            if (DtTo < DtFrom)
            {
                MessageBox("To Date Should be Greater than  or equal to From Date");
                return;
            }
            else
            {
                ServiceBook objSevBook = new ServiceBook();
                objSevBook.IDNO = _idnoEmp;
                objSevBook.FDT = Convert.ToDateTime(txtFromDate.Text);
                objSevBook.TDT = Convert.ToDateTime(txtToDate.Text);

              //  objSevBook.APPOINTMENT = txtAppointment.Text;
                objSevBook.APPOINTMENTMODE = ddlAppMode.SelectedValue;
               // objSevBook.COMMITTEEDETAILS = txtSelectionDetail.Text;
                objSevBook.COMMITTEEMEMBER = txtCommitteeMembers.Text;
               // objSevBook.ADVERTISEMENT = txtAdvt.Text;
                objSevBook.NEWSPAPER = txtNewspaper.Text;
                //objSevBook.DATE = txtNewsDt.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtNewsDt.Text.Trim());
                if (txtNewsDt.Text == String.Empty)
                {
                    objSevBook.AVDATE = null;
                }
                else
                {
                    objSevBook.AVDATE = Convert.ToDateTime(txtNewsDt.Text);
                }





                objSevBook.REFERENCE = txtReference.Text;
               // objSevBook.AUTHORITYNAME = txtNameAuthority.Text;
               // objSevBook.APPOINTMENTDDATE = txtApppDate.Text.Trim().Equals(string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtApppDate.Text.Trim());
                if (txtApppDate.Text == String.Empty)
                {
                    objSevBook.APPOINTMENTDDATE = null;
                }
                else
                {
                    objSevBook.APPOINTMENTDDATE = Convert.ToDateTime(txtApppDate.Text);
                }

                
                objSevBook.APPNO = txtAppNo.Text;
                objSevBook.POSTNAME = txtPostName.Text;
                objSevBook.APPSTATUS = txtAppStatus.Text;
                objSevBook.PAYSCALE = txtpayscale.Text;
                objSevBook.DEPARTMENT = txtDepartment.Text;
                objSevBook.EXPERIENCE = txtExperience.Text;

                objSevBook.EXPERIENCETYPE = ddlExpTyp.SelectedValue;


                if (rdbStatus.SelectedValue == "0")
                {
                    objSevBook.UNIVERSITYAPPNO = txtApprovalno.Text;
                    objSevBook.UNIAPPDT = Convert.ToDateTime(txtDate.Text);
                    objSevBook.UNIAPPSTATUS = "YES";

                    //if (flupuniv.HasFile)
                    //{
                    //    objSevBook.UNIVERSITYATACHMENT = Convert.ToString(flupuniv.PostedFile.FileName.ToString());
                    //   // objSevBook.UNIVERSITYATACHMENT = ViewState["fileOne"].ToString();
                // }
                    //else
                    //{
                    //    //if (ViewState["universityattachment"] != null)
                    //    //{
                    //    //    objSevBook.UNIVERSITYATACHMENT = ViewState["universityattachment"].ToString();
                    //    //}
                    //    //else
                    //    {
                    //        objSevBook.UNIVERSITYATACHMENT = string.Empty;
                    //    }
                    //}
                }
                else
                {
                    objSevBook.UNIVERSITYAPPNO = string.Empty;
                    objSevBook.UNIAPPDT = null;
                    objSevBook.UNIVERSITYATACHMENT = string.Empty;
                    objSevBook.UNIAPPSTATUS = "NO";
                }

                if (rdbTeacher.SelectedValue == "0")
                {
                    objSevBook.PGAPPNO = txtteachno.Text;
                    objSevBook.PGTAPPDT = Convert.ToDateTime(txtappdt.Text);
                    objSevBook.PGTAPPSTATUS = "YES";
                    //if (flupteach.HasFile)
                    //{
                    //   objSevBook.PGTATTACHMENT = Convert.ToString(flupteach.PostedFile.FileName.ToString());
                    //    //objSevBook.PGTATTACHMENT = ViewState["fileTwo"].ToString();
                    //}
                    //else
                    //{
                    //    //if (ViewState["pgtattachment"] != null)
                    //    //{
                    //    //    objSevBook.PGTATTACHMENT = ViewState["pgtattachment"].ToString();
                    //    //}
                    //    //else
                    //    {
                    //        objSevBook.PGTATTACHMENT = string.Empty;
                    //    }
                    //}

                }

                else
                {
                    objSevBook.PGAPPNO = string.Empty;
                    objSevBook.PGTAPPDT = null;
                    objSevBook.PGTATTACHMENT = string.Empty;
                    objSevBook.PGTAPPSTATUS = "NO";
                }

                //Check whether to add or update
                if (ViewState["action"] != null)
                {
                    if (ViewState["action"].ToString().Equals("add"))
                    {

                       // CustomStatus cs = (CustomStatus)objServiceBook.AddCurrentAppointment(objSevBook, dt);
                        CustomStatus cs = (CustomStatus)objServiceBook.AddCurrentAppointment(objSevBook);
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            //if (ViewState["DESTINATION_PATH"] != null)
                            //{
                            //    string SFNO = objCommon.LookUp("PAYROLL_SB_CURRENT_APPOINTMENT", "MAX(CANO)", "");
                            //    //AddDocuments(Convert.ToInt32(SFNO));
                            //}
                          //  objServiceBook.upload_new_files("CURRENT_APPOINTMENT", _idnoEmp, "CANO", "PAYROLL_SB_CURRENT_APPOINTMENT", "CAS_", flupuniv);
                            //objServiceBook.upload_new_files("CURRENT_APPOINTMENT", _idnoEmp, "CANO", "PAYROLL_SB_CURRENT_APPOINTMENT", "CAS_", flupteach);

                            this.Clear();
                            this.BindListCurrentAppointment();

                            MessageBox("Record Saved Successfully");
                        }
                        else if (cs.Equals(CustomStatus.RecordFound))
                        {
                            MessageBox("Record Already Exist");
                            this.Clear();
                        }
                    }
                    else
                    {
                        //Edit
                        if (ViewState["caNo"] != null)
                        {
                            objSevBook.CANO = Convert.ToInt32(ViewState["caNo"].ToString());
                            CustomStatus cs = (CustomStatus)objServiceBook.UpdateCurrentAppointment(objSevBook);
                            if (cs.Equals(CustomStatus.RecordUpdated)) 
                            {
                                //if (ViewState["DESTINATION_PATH"] != null)
                                //{
                                //    string SCNO = ViewState["CANO"].ToString();
                                //   // AddDocuments(Convert.ToInt32(SCNO));
                                //}
                                ViewState["action"] = "add";
                                this.Clear();
                                this.BindListCurrentAppointment();
                                
                                MessageBox("Record Updated Successfully");
                            }
                            else
                            {
                                ViewState["action"] = "add";
                                MessageBox("Record Already Exist");
                                this.Clear();
                                this.BindListCurrentAppointment();
                            }
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
         Clear();
    }

    private void Clear()
    {

        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;

        ddlAppMode.SelectedIndex = 0;
        txtExperience.Text = string.Empty;
       // txtAppointment.Text = string.Empty;
        //txtAppMode.Text = string.Empty;
       // txtSelectionDetail.Text = string.Empty;
        txtCommitteeMembers.Text = string.Empty;
        //txtAdvt.Text = string.Empty;
        txtNewspaper.Text = string.Empty;
        txtNewsDt.Text = string.Empty;
        txtReference.Text = string.Empty;
       // txtNameAuthority.Text = string.Empty;
        txtApppDate.Text = string.Empty;
        txtAppNo.Text = string.Empty;
        txtPostName.Text = string.Empty;
        txtAppStatus.Text = string.Empty;
        txtpayscale.Text = string.Empty;
        txtDepartment.Text = string.Empty;
        ddlExpTyp.SelectedIndex = 0;
        rdbStatus.SelectedIndex = 1;
        rdbTeacher.SelectedIndex = 1;
        ViewState["action"] = "add";
        txtApprovalno.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtteachno.Text = string.Empty;
        txtappdt.Text = string.Empty;
        divapp.Visible = false;
        divdate.Visible = false;
       // divdoc.Visible = false;
        divpg.Visible = false;
        divpgdt.Visible = false;
       // divpgdoc.Visible = false;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
            ImageButton btnEdit = sender as ImageButton;
            int caNo = int.Parse(btnEdit.CommandArgument);
            ShowDetails(caNo);
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

     private void ShowDetails(int caNo)
    {
        DataSet ds = null;
        try
        {
            ds = objServiceBook.GetSingleCurrentAppoinmentDetailsOfEmployee(caNo);
            
            if (ds.Tables[0].Rows.Count > 0)
            {

                ViewState["caNo"] = caNo.ToString();
                txtFromDate.Text = ds.Tables[0].Rows[0]["fdt"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["tdt"].ToString();
               
                txtExperience.Text = ds.Tables[0].Rows[0]["EXPERIENCE"].ToString();           
               // txtAppointment.Text = ds.Tables[0].Rows[0]["APPOINTMENT"].ToString();
               ddlAppMode.SelectedValue = ds.Tables[0].Rows[0]["APPOINTMENTMODE"].ToString();
              //  txtSelectionDetail.Text = ds.Tables[0].Rows[0]["COMMITTEEDETAILS"].ToString();
                txtCommitteeMembers.Text = ds.Tables[0].Rows[0]["COMMITTEEMEMBER"].ToString();
               // txtAdvt.Text = ds.Tables[0].Rows[0]["ADVERTISEMENT"].ToString();
                txtNewspaper.Text = ds.Tables[0].Rows[0]["NEWSPAPER"].ToString();
                txtNewsDt.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                txtReference.Text = ds.Tables[0].Rows[0]["REFERENCE"].ToString();
               // txtNameAuthority.Text = ds.Tables[0].Rows[0]["AUTHORITYNAME"].ToString();
                txtApppDate.Text = ds.Tables[0].Rows[0]["APPOINTMENTDDATE"].ToString();
                txtAppNo.Text = ds.Tables[0].Rows[0]["APPNO"].ToString();
                txtPostName.Text = ds.Tables[0].Rows[0]["POST"].ToString();
                txtAppStatus.Text = ds.Tables[0].Rows[0]["APPSTATUS"].ToString();
                txtpayscale.Text = ds.Tables[0].Rows[0]["PAYSCALE"].ToString();
                txtDepartment.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                ddlExpTyp.SelectedValue = ds.Tables[0].Rows[0]["EXPERIENCETYPE"].ToString();


                ViewState["universityattachment"] = ds.Tables[0].Rows[0]["UNIVERSITYATACHMENT"].ToString();
                ViewState["pgtattachment"] = ds.Tables[0].Rows[0]["PGTATTACHMENT"].ToString();

                string uniappstatus = ds.Tables[0].Rows[0]["UNIAPPSTATUS"].ToString();
                if (uniappstatus == "YES")
                {
                    rdbStatus.SelectedValue = "0";
                   // appstatus.Visible = true;
                    divapp.Visible = true;
                    divdate.Visible = true;
                   // divdoc.Visible = true;
                    txtApprovalno.Text = ds.Tables[0].Rows[0]["UNIVERSITYAPPNO"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["UNIAPPDT"].ToString();
                }
                else
                {
                     rdbStatus.SelectedValue = "1";
                   // appstatus.Visible = false;
                    divapp.Visible = false;
                    divdate.Visible = false;
                   // divdoc.Visible = false;
                }

                string pgappstatus = ds.Tables[0].Rows[0]["PGTAPPSTATUS"].ToString();
                if (pgappstatus == "YES")
                {
                    rdbTeacher.SelectedValue = "0";
                   // divpgteacher.Visible = true;
                    divpg.Visible = true;
                    divpgdt.Visible = true;
                   // divpgdoc.Visible = true;
                    txtteachno.Text = ds.Tables[0].Rows[0]["PGAPPNO"].ToString();
                    txtappdt.Text = ds.Tables[0].Rows[0]["PGTAPPDT"].ToString();
                }
                else
                {
                    rdbTeacher.SelectedValue = "1";
                    // divpgteacher.Visible = false;
                    divpg.Visible = false;
                    divpgdt.Visible = false;
                    //divpgdoc.Visible = false;
                }

             
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.ShowDetails-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Clear();
            ds.Dispose();
        }

    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
            ImageButton btnDel = sender as ImageButton;
            int caNo = int.Parse(btnDel.CommandArgument);
            CustomStatus cs = (CustomStatus)objServiceBook.DeleteCurrentAppointment(caNo);
            if (cs.Equals(CustomStatus.RecordDeleted))
            {
                MessageBox("Record Deleted Successfully");
                Clear();
                BindListCurrentAppointment();
                ViewState["action"] = "add";
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "PayRoll_Pay_PreviousService.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
 
  
}
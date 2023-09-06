//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADD EXAMINER
// CREATION DATE : 21-Mar-2023                                                          
// CREATED BY    : NEHAL                                                                    
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACADEMIC_PHD_Add_PhD_Examiner : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhd = new PhdController();

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
              //  CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                if (Session["usertype"].ToString() == "1" || Session["usertype"].ToString() == "2")
                {
                    Response.Redirect("~/notauthorized.aspx?page=Add_PhD_Examiner.aspx");
                }
                else
                {

                }
                BindListView();
                FillDropDown();
            }
        }
    }

    private void FillDropDown()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO");
            this.objCommon.FillDropDownList(ddlExaminerMap, "ACD_PHD_ADD_EXAMINER", "EXAMINER_ID", "EXAMINER_NAME", "EXAMINER_ID >0", "EXAMINER_ID");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_Submit_Presynopsis.FillDropDown-> " + ex.Message + " " + ex.StackTrace);
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
                Response.Redirect("~/notauthorized.aspx?page=Add_PhD_Examiner.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Add_PhD_Examiner.aspx");
        }
    }
    #region add Examiner
    private void BindListView()
    {
        try
        {
            DataSet ds = objPhd.GetAllExaminerList(0);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnlExaminer.Visible = true;
                lvExaminer.DataSource = ds;
                lvExaminer.DataBind();
            }
            else
            {
                pnlExaminer.Visible = false;
                lvExaminer.DataSource = null;
                lvExaminer.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string Examiner_Name = txtName.Text.Trim();
            string institute_Name = txtInstituteName.Text.Trim();
            
            string mobileno = txtMobile.Text;
            string Email = txtEmail.Text.Trim();
            int examiner_type = Convert.ToInt32(ddlExaminerType.SelectedValue);
            //int stateno = Convert.ToInt32(ddlState.SelectedValue);
            int stateno = 0;
            int countryno = 0;
            if (Convert.ToInt32(ddlExaminerType.SelectedValue) == 1)
            {
                countryno = Convert.ToInt32(objCommon.LookUp("ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME='INDIA'"));
                 stateno = Convert.ToInt32(ddlState.SelectedValue);
            }
            else
            {
                 countryno = Convert.ToInt32(ddlCountry.SelectedValue);
                  stateno = 0;
            }
            
            //Check for add or edit
            if (Session["action"] != null && Session["action"].ToString().Equals("edit"))
            {
                //Edit 
                int id = Convert.ToInt32(Session["id"]);
                CustomStatus cs = (CustomStatus)objPhd.InsertExaminer(id, Examiner_Name, institute_Name, mobileno, countryno, stateno, Email, examiner_type);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearControls();
                    BindListView();
                    Session["action"] = null;
                    objCommon.DisplayMessage(this.updExaminer, "Record Updated sucessfully", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updExaminer, "Record Already Exist", this.Page);
                }
            }

            else
            {
                //Add New
                CustomStatus cs = (CustomStatus)objPhd.InsertExaminer(0, Examiner_Name, institute_Name, mobileno, countryno, stateno, Email, examiner_type);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updExaminer, "Record Added sucessfully", this.Page);
                    ClearControls();
                    BindListView();
                }
                else
                {
                    objCommon.DisplayMessage(this.updExaminer, "Record Already Exist", this.Page);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {
        txtName.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtMobile.Text = string.Empty; 
        txtEmail.Text = string.Empty;
        ddlExaminerType.SelectedValue = "0";
        ddlState.SelectedValue = "0";
        ddlCountry.SelectedValue = "0";
        divCountry.Visible = false;
        divState.Visible = false;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnEdit = sender as ImageButton;
        int id = int.Parse(btnEdit.CommandArgument);
        Session["id"] = int.Parse(btnEdit.CommandArgument);
        ViewState["edit"] = "edit";

        this.ShowDetails(id);
        txtName.Focus();
    }
    private void ShowDetails(int id)
    {
        try
        {
            DataSet ds = objPhd.GetAllExaminerList(id);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtName.Text = ds.Tables[0].Rows[0]["EXAMINER_NAME"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["EXAMINER_NAME"].ToString();
                txtInstituteName.Text = ds.Tables[0].Rows[0]["INSTITUTE_NAME"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["INSTITUTE_NAME"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["MOBILE_NO"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString() == null ? string.Empty : ds.Tables[0].Rows[0]["EMAILID"].ToString();
                ddlExaminerType.SelectedValue = ds.Tables[0].Rows[0]["EXAMINER_TYPE_NO"].ToString();
                if (ddlExaminerType.SelectedValue == "1")
                {
                    divState.Visible = true;
                    divCountry.Visible = false;
                    objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0 AND ACTIVESTATUS=1", "STATENAME");
                    ddlState.SelectedValue = ds.Tables[0].Rows[0]["STATENO"].ToString();
                }
                else
                {
                    divCountry.Visible = true;                
                    divState.Visible = false;
                    objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNAME<>'INDIA'", "COUNTRYNAME");
                    ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["COUNTRYNO"].ToString();
                }
               
                
            }
            if (ds != null) ;

            Session["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlExaminerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlExaminerType.SelectedValue == "1")
            {
                divState.Visible = true;
                divCountry.Visible = false;
                objCommon.FillDropDownList(ddlState, "ACD_STATE", "STATENO", "STATENAME", "STATENO >0 AND ACTIVESTATUS=1", "STATENAME");
            }
            else
            {
                divCountry.Visible = true;
                divState.Visible = false;
                objCommon.FillDropDownList(ddlCountry, "ACD_COUNTRY", "COUNTRYNO", "COUNTRYNAME", "COUNTRYNAME<>'INDIA'", "COUNTRYNAME");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #endregion

    #region mapping 
   
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            int uano = Convert.ToInt32(Session["userno"].ToString());

            DataSet ds = objPhd.RetrieveStudentDetailsPHDforFaculty(uano, Convert.ToInt32(ddlAdmBatch.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                Panellistview.Visible = true;
                lvStudent.Visible = true;
                lvStudent.DataSource = ds;
                lvStudent.DataBind();
                btnSubmitMap.Visible = true;
                divExaminer.Visible = true;
                objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
                //lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
            }
            else
            {
                objCommon.DisplayMessage(this.updMapping, "Record not found", this.Page);
                //lblNoRecords.Text = "Total Records : 0";
                lvStudent.Visible = false;
                lvStudent.DataSource = null;
                lvStudent.DataBind();
                divExaminer.Visible = false;
                btnSubmitMap.Visible = false;
            }
        }
        catch
        {
            throw;
        }
        
    }
    protected void btnSubmitMap_Click(object sender, EventArgs e)
    {
         try 
         {
             int count = 0;
             int count1 = 0;
             foreach (ListViewDataItem dataitem in lvStudent.Items)
             {
                 CheckBox cbRow = dataitem.FindControl("chkloan") as CheckBox;
                 if (cbRow.Checked == true)
                     count++;
             }
             if (count <= 0)
             {
                 objCommon.DisplayMessage(this.updExaminer, "Please Select only one Student", this);
                 return;
             }
             else
             {
                 foreach (ListViewDataItem item in lvStudent.Items)
                 {
                     CheckBox chkAccept = item.FindControl("chkloan") as CheckBox;
                     Label lblstuenrollno = item.FindControl("lblstuenrollno") as Label;
                     int idno = Convert.ToInt32(lblstuenrollno.ToolTip);
                     int examinaer = Convert.ToInt32(ddlExaminerMap.SelectedValue);
                     int UA_NO = Convert.ToInt32(Session["userno"]);
                     CustomStatus cs = (CustomStatus)objPhd.InsertExaminerMapping(idno, examinaer, UA_NO);
                     if (cs.Equals(CustomStatus.RecordSaved))
                     {  
                          count1++;
                     }

                 }
                 if (count1 > 0)
                 {
                     objCommon.DisplayMessage(this.updExaminer, "Examiner alloted sucessfully", this.Page);
                     return;
                 }
                 else
                 {
                     objCommon.DisplayMessage(this.updExaminer, "Examiner already alloted ", this.Page);
                     return;
                 }
             }
         }
        catch
        {
            throw;
        }
    }
    protected void btnCancelMap_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion
}
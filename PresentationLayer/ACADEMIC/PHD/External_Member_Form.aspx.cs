/*
  Added by :-  Nikhil L.
  Created Date : 12/12/2022 
  Feature : Use to add or update the external member for PHD.
  Modify By :- Jay takalkhede
  Modify  Date : 25/08/2023
  Feature : Use to add college ddl  for PHD.
 * Version :- 1)RFC.PHD.REQUIRMENT.MAJOR.1 (12/12/2022) 2)RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;

public partial class ACADEMIC_PHD_External_Member_Form : System.Web.UI.Page
{
    #region Page Load
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PhdController objPhDController = new PhdController();
    string sp_Name = string.Empty; string sp_Parameters = string.Empty; string call = string.Empty;
    string name = string.Empty; string inst_Name = string.Empty; string mobile = string.Empty; string emailId = string.Empty;
    string designation = string.Empty; int createdBy = 0; string ipAddress = string.Empty; int desig = 0; int modified = 0;
    string mode = string.Empty; int outPut = 0;

  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                 CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                }
            }
            GetExtMembers();
            BindListViewMapping();
            PopulateDropDown();
            ViewState["mode"] = string.Empty;
            ViewState["id"] ="0";
        }

    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=External_Member_Form.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=External_Member_Form.aspx");
        }
    }
    #endregion Page Load

    #region external
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            DataSet dsMember = null;
            name = txtName.Text.ToString();
            inst_Name = txtInstituteName.Text.ToString();
            mobile = txtMobile.Text.ToString();
            emailId = txtEmail.Text.ToString();
            designation = txtDesig.Text.ToString();
            createdBy=Convert.ToInt32(Session["userno"].ToString());
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            desig = 0;
            if (mobile.Length==10)
            {
            
            if (ViewState["mode"].ToString().Equals(string.Empty))
            {
                mode = "INSERT";
                desig = 0;
            }
            else
            {
                mode = ViewState["mode"].ToString();
                desig =Convert.ToInt32(ViewState["desigNo"].ToString());
            }
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsMember = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsMember.Tables[0].Rows.Count > 0)
            {
                if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("0"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member already exists.`)", true);
                }
                else if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member added successfully.`)", true);
                    clearField();
                    GetExtMembers();
                }
                else if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("2"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`External member updated successfully.`)", true);
                    clearField();
                    GetExtMembers();
                }
            }
            }
            else
            {
                objCommon.DisplayMessage(this, "Mobile Number is Invalid", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgEdit = sender as ImageButton;
            desig = Convert.ToInt32(imgEdit.CommandArgument);
            DataSet dsEdit = null;
            mode = "EDIT";
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsEdit = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsEdit.Tables[0].Rows.Count > 0)
            {
                txtName.Text = dsEdit.Tables[0].Rows[0]["NAME"].ToString();
                txtInstituteName.Text = dsEdit.Tables[0].Rows[0]["INSTITIUTE_NAME"].ToString();
                txtMobile.Text = dsEdit.Tables[0].Rows[0]["MOBILE_NO"].ToString();
                txtEmail.Text = dsEdit.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                txtDesig.Text = dsEdit.Tables[0].Rows[0]["DESIGNATION"].ToString();
                ViewState["mode"] = "Update";
                ViewState["desigNo"] = desig;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Record does not exists.`)", true);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void GetExtMembers()
    {
        try
        {
            DataSet dsGet = null;
            desig = 0;
            mode = "GET";
            sp_Name = "PKG_ACD_INS_UPD_GET_PHD_EXT_MEMBER";
            sp_Parameters = "@P_NAME,@P_INST_NAME,@P_MOBILE_NO,@P_EMAILID,@P_DESIGN,@P_UA_NO,@P_IP_ADDRESS,@P_DESIG_NO,@P_MODE,@P_OUTPUT";
            call = "" + name + "," + inst_Name + "," + mobile + "," + emailId + "," + designation + "," + createdBy + "," + ipAddress + "," + desig + "," + mode + "," + outPut + "";
            dsGet = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsGet.Tables[0].Rows.Count > 0)
            {
                lvMember.DataSource = dsGet;
                lvMember.DataBind();
                pnlMember.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void clearField()
    {
        txtName.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtMobile.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDesig.Text = string.Empty;
        ViewState["mode"] = string.Empty;
        ViewState["desigNo"] = string.Empty;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearField();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Internal
    private void PopulateDropDown()
    {
        objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(C.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT  CDB.COLLEGE_ID", "C.COLLEGE_NAME", "isnull(C.ActiveStatus,0)=1 ", "CDB.COLLEGE_ID DESC");
        //objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(C.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT  CDB.COLLEGE_ID", "C.COLLEGE_NAME", "isnull(C.ActiveStatus,0)=1 AND CDB.DEPTNO=" + ddlDepartment.SelectedValue, "CDB.COLLEGE_ID DESC");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int Designationno = Convert.ToInt32(ddlDesignation.SelectedValue);
            int departmentno = Convert.ToInt32(ddlDepartment.SelectedValue);
            int collegeid = Convert.ToInt32(ddlInsName.SelectedValue);
            int id = int.Parse(ViewState["id"].ToString());
            int count = 0;
            string Facuilty = string.Empty;
            foreach (ListItem Item in lboFacuilty.Items)
            {
                if (Item.Selected)
                {
                    Facuilty += Item.Value + ",";
                    count++;
                }
            }
            Facuilty = Facuilty.Substring(0, Facuilty.Length - 1);
            if (ViewState["id"].ToString() != string.Empty && ViewState["id"].ToString() == "0")
            {
                CustomStatus cs = (CustomStatus)objPhDController.InsertInternalStudent(Facuilty, Designationno, departmentno, collegeid);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updInternalMember, "Record Saved successfully", this.Page);
                    BindListViewMapping();
                    lboFacuilty.DataSource = null;
                    lboFacuilty.Items.Clear();                   
                    ddlDesignation.SelectedIndex = 0;
                    ddlDepartment.SelectedIndex = 0;                 
                    //Added By Vipul T on dated 22-12-2023
                    ddlInsName.SelectedIndex = 0;
                    ddlDepartment.Items.Clear();
                    ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
                    //ddlInsName.Items.Clear();
                   // ddlInsName.Items.Add(new ListItem("Please Select", "0"));
                    ViewState["id"] = "0";
                    //clear();
                  


                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updInternalMember, "Record Already Exist", this.Page);
                    BindListViewMapping();
                    lboFacuilty.DataSource = null;
                    lboFacuilty.Items.Clear();
                    ddlDesignation.SelectedIndex = 0;
                    ddlDepartment.SelectedIndex = 0;                  
                    //Added By Vipul T on dated 22-12-2023
                    ddlInsName.SelectedIndex = 0;
                    ddlDepartment.Items.Clear();
                    ddlDepartment.Items.Add(new ListItem("Please Select", "0"));                   
                    ViewState["id"] = "0";
                    
                }
            }
            else
            {//RFC.PHD.ENHANCEMENT.MAJOR.1 (25-08-2023)(TKNO.46978) (25-08-2023)(TKNO.46978)
                CustomStatus cs = (CustomStatus)objPhDController.UpdateInternalStudent(Facuilty, Designationno, departmentno, collegeid,id);
                if (cs.Equals(CustomStatus.RecordUpdated))
                {

                    objCommon.DisplayMessage(this.updInternalMember, "Record Update successfully", this.Page);
                    BindListViewMapping();
                    lboFacuilty.DataSource = null;
                    lboFacuilty.Items.Clear();
                    ddlInsName.SelectedIndex = 0;
                    ddlDesignation.SelectedIndex = 0;
                    ddlDepartment.SelectedIndex = 0;
                    //Added By Vipul T on dated 22-12-2023
                    ddlInsName.SelectedIndex = 0;
                    ddlDepartment.Items.Clear();
                    ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
                    ViewState["id"] = "0";

                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updInternalMember, "Record Already Exist", this.Page);
                    BindListViewMapping();
                    lboFacuilty.DataSource = null;
                    lboFacuilty.Items.Clear();
                    ddlDesignation.SelectedIndex = 0;
                    ddlDepartment.SelectedIndex = 0;
                    //Added By Vipul T on dated 22-12-2023
                    ddlInsName.SelectedIndex = 0;
                    ddlDepartment.Items.Clear();
                    ddlDepartment.Items.Add(new ListItem("Please Select", "0"));

                   
                    ViewState["id"] = "0";
                   
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void clear()
    {
        ViewState["id"] = "0";
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        ViewState["id"] = "0";
        Response.Redirect(Request.Url.ToString());

    }

    //RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
    protected void BindListViewMapping()
    {
        try
        {

            DataSet ds = objPhDController.GetInternalMemberMappingData();

            if (ds.Tables[0].Rows.Count > 0)
            {
                PnMapping.Visible = true;
                lvMapping.DataSource = ds;
                lvMapping.DataBind();
            }
            else
            {
                PnMapping.Visible = false;
                lvMapping.DataSource = null;
                lvMapping.DataBind();

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.BindListViewMapping-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    //RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
    protected void ddlInsName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlInsName.SelectedIndex > 0)
        {
            //added by vipul Tichakule on dated 29-12-2023 
            objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT d inner join acd_college_degree_branch c on (d.deptno=c.deptno) inner join acd_college_master cc on (c.college_id=cc.college_id)", "distinct d.DEPTNO", "d.DEPTNAME", "isnull(d.ActiveStatus,0)=1 and cc.college_id=" + ddlInsName.SelectedValue + "", "d.DEPTNO");
             // objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT d inner  join acd_college_master c  on (d.organizationid=c.organizationid)", "DEPTNO", "DEPTNAME", "isnull(d.ActiveStatus,0)=1 and c.college_id=" + ddlInsName.SelectedValue + "", "DEPTNO DESC");
            //objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "isnull(ActiveStatus,0)=1", "DEPTNO DESC");
        }
        else
        {
            lboFacuilty.DataSource = null;
            lboFacuilty.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlDepartment.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    //RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            objCommon.FillListBox(lboFacuilty, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO) INNER JOIN PAYROLL_EMPMAS P ON UA.UA_IDNO = P.IDNO", "distinct ua_no", "UA_FULLNAME + ' - '+EmployeeId", "UA.ua_type IN (3,5) and isnull(ua_status,0)=0  and " + ddlInsName.SelectedValue + " in (select value from dbo.Split(UA_COLLEGE_NOS,',')) AND " + ddlDepartment.SelectedValue + " in (select value from dbo.Split(UA_DEPTNO,',')) ", "ua_no");
            //objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(C.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT  CDB.COLLEGE_ID", "C.COLLEGE_NAME", "isnull(C.ActiveStatus,0)=1 AND CDB.DEPTNO=" + ddlDepartment.SelectedValue, "CDB.COLLEGE_ID DESC");
        }
        else
        {
            lboFacuilty.DataSource = null;
            lboFacuilty.Items.Clear();
            ddlInsName.Items.Clear();
            ddlInsName.Items.Add(new ListItem("Please Select", "0"));
        }
    }

    //RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
    protected void btnEdit_Click1(object sender, ImageClickEventArgs e)
    {

        try
        {

            String FacuiltyNO = string.Empty;
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            DataSet ds = objPhDController.EditInternalMemberMappingData(ID);       
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //Commented By Vipul T on Date 06-11-2023 for Removing the Condition
                    //if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    //{
                    //    //RFC.PHD.ENHANCEMENT.MAJOR.2 (25-08-2023)(TKNO.46978)
                    //    objCommon.DisplayMessage(this.updInternalMember, "The supervisor has already been allotted to a student So it can not be edit", this.Page); // Commented By Vipul T 
                    //    BindListViewMapping();
                    //    lboFacuilty.DataSource = null;
                    //    lboFacuilty.Items.Clear();
                    //    ddlDesignation.SelectedIndex = 0;
                    //    ddlDepartment.SelectedIndex = 0;
                    //    ddlInsName.Items.Clear();
                    //    ddlInsName.Items.Add(new ListItem("Please Select", "0"));
                    //    ViewState["id"] = "0";
                    //    return;
                    //}
                    //else
                    //{

                    //}

                       //Added By Vipul On Dated 22-12-2023 as per Tno:- 
                    objCommon.FillDropDownList(ddlInsName, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON(C.COLLEGE_ID=CDB.COLLEGE_ID)", "DISTINCT  CDB.COLLEGE_ID", "C.COLLEGE_NAME", "isnull(C.ActiveStatus,0)=1 ", "CDB.COLLEGE_ID DESC");                   
                    ddlInsName.SelectedValue = ds.Tables[0].Rows[0]["COLLEGE_ID"].ToString();                  
                    objCommon.FillDropDownList(ddlDepartment, "ACD_DEPARTMENT", "DEPTNO", "DEPTNAME", "isnull(ActiveStatus,0)=1", "DEPTNO DESC");
                    ddlDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPTNO"].ToString();//end                    
                    objCommon.FillListBox(lboFacuilty, "user_acc", "ua_no", "UA_FULLNAME", "ua_type IN (3,5) and isnull(ua_status,0)=0 and " + ddlInsName.SelectedValue + " in (select value from dbo.Split(UA_COLLEGE_NOS,',')) AND " + ddlDepartment.SelectedValue + " in (select value from dbo.Split(UA_DEPTNO,','))", "ua_no");
                    lboFacuilty.SelectedValue = ds.Tables[0].Rows[0]["UANO"].ToString();
                    ddlDesignation.SelectedValue = ds.Tables[0].Rows[0]["DESIGNATIONNO"].ToString();
                
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PHDCommitteeDesignation.btnCommitteeEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

}
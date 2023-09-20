/*
  Added by     :- Jay Takalkhede
  Created Date : 28/08/2022 
  Feature      : Use to add Bulk Ann-A.
  Modify By    :- 
  Modify  Date : 
 *Version      :- 1)RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023)
 */
using System;
using System.Collections;
using System.Configuration;
using System.IO;
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
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using EASendMail;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogic;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ACADEMIC_PHD_BulkAnnexure : System.Web.UI.Page
{
    #region Page Load (RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023))
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    PhdController objPhd = new PhdController();

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
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            if (!Page.IsPostBack)
            {
                //Page Authorization
                //this.CheckPageAuthorization();
                Page.Title = Session["coll_name"].ToString();

                //  HiddenItem();
                //LvOffer.DataSource = GetData();
                //LvOffer.DataBind();
                PopulateDropDownList();
            }
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            divMsg.InnerHtml = string.Empty;

        }
        catch (Exception ex)
        {
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=phd_BulkAnnexure.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page  
            Response.Redirect("~/notauthorized.aspx?page=phd_BulkAnnexure.aspx");
        }
    }

    protected void PopulateDropDownList()
    {
        //objCommon.FillDropDownList(ddlAdmBatch, "ACD_PHD_REGISTRATION", "distinct ADMBATCH", "DBO.FN_DESC('ADMBATCH',ADMBATCH)ADMBATCHNAME", "ADMBATCH>0", "ADMBATCH");
        this.objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "ACTIVESTATUS = 1", "BATCHNO"); //added on 27/03/23
        objCommon.FillDropDownList(ddlSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND DESIGNATIONNO IN (1,3)", "ua_fullname");  
        objCommon.FillDropDownList(ddlSchool, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_COLLEGE_MASTER M ON(DB.COLLEGE_ID=M.COLLEGE_ID)", "DISTINCT DB. COLLEGE_ID", "M.COLLEGE_NAME", "UGPGOT=3", "COLLEGE_NAME");  //UGPG=3 added by Nikhil L. on 08/11/2022 hard coded for PhD colleges only.
    }

    #endregion Page Load 

    #region DDL (RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023))
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        try
        {
            btnSubmit.Visible = false;
            Panel3.Visible = false;
            LvPhdBulk.DataSource = null;
            LvPhdBulk.DataBind();
        }
        catch (Exception ex)
        {
        }
       
    }
    protected void ddlSchool_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            btnSubmit.Visible = false;
            Panel3.Visible = false;
            LvPhdBulk.DataSource = null;
            LvPhdBulk.DataBind();
            if (ddlSchool.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlprogram, "ACD_COLLEGE_DEGREE_BRANCH CDB INNER JOIN ACD_BRANCH B ON(B.BRANCHNO=CDB.BRANCHNO) ", "CDB.BRANCHNO", "B.SHORTNAME", "CDB.UGPGOT=3 AND COLLEGE_ID=" + Convert.ToInt32(ddlSchool.SelectedValue), "CDB.BRANCHNO");

            }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(`Something went wrong.`)", true);
            return;
        }
    }
    protected void ddlprogram_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        btnSubmit.Visible = false;
        Panel3.Visible = false;
        LvPhdBulk.DataSource = null;
        LvPhdBulk.DataBind();
       
    }
    protected void CheckBox1_CheckedChanged(object sender, System.EventArgs e)
    {
        try
        {
            if (CheckBox1.Checked)
            {
                objCommon.FillDropDownList(ddlSupervisor, "ACD_PHD_OUTSIDE_MEMBER_MASTER", "DESIG_NO", "NAME", "DESIG_NO>0", "NAME");

            }

            else
            {
                objCommon.FillDropDownList(ddlSupervisor, "USER_ACC UA INNER JOIN ACD_PHD_INTERNAL_MEMBER I ON(UA.UA_NO=I.UANO)", "UA_NO", "UA_FULLNAME", "UA_NO>0 AND DESIGNATIONNO IN (1,3)", "ua_fullname");

            }
        }
        catch (Exception ex)
        {
        }

    }
    #endregion DDL

    #region SHOW (RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023))
    protected void btnshow_Click(object sender, System.EventArgs e)
    {
        SHOW();
    }
    private void SHOW()
    {
        try
        {
            string SP_Name2 = "PKG_ACD_GET_PHD_BULK_ANNEXURE_STUDENTS_SELECTION_LIST";
            string SP_Parameters2 = "@P_ADMBATCH,@P_BRANCHNO,@P_COLLEGE_ID";
            string Call_Values2 = "" + Convert.ToInt32(ddlAdmBatch.SelectedValue.ToString()) + "," +
                                 Convert.ToInt32(ddlprogram.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlSchool.SelectedValue.ToString()) + "";
            DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                Panel3.Visible = true;
                LvPhdBulk.DataSource = dsStudList;
                LvPhdBulk.DataBind();
               
            }
            else
            {
                btnSubmit.Visible = false;
                objCommon.DisplayMessage(this.updbulkanne, "No Record Found", this.Page);
                Panel3.Visible = false;
                LvPhdBulk.DataSource = null;
                LvPhdBulk.DataBind();               
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion SHOW

    #region Cancel (RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023))
    private void Clear()
    {
        Panel3.Visible = false;
        btnSubmit.Visible = false;       
        ddlprogram.SelectedIndex = 0;
        ddlAdmBatch.SelectedIndex = 0;
        ddlSchool.SelectedIndex = 0;
        ddlSupervisor.SelectedIndex = 0;
        ddlSupervisorrole.SelectedIndex = 0;
        LvPhdBulk.DataSource = null;
        LvPhdBulk.DataBind();
        
    }
    protected void btncancel_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion Cancel

    #region Submit (RFC.PHD.REQUIRMENT.MAJOR.1 (28/08/2023))
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            int count=0;
            int Count1 = 0;
            DataSet dsMember = null;
             PhdController objPhdC = new PhdController();
             Phd objS = new Phd();
             string RESEARCH = string.Empty;
             string studentIds = string.Empty;
             foreach (ListViewDataItem item in LvPhdBulk.Items)
             {
                 CheckBox chkBox = item.FindControl("chkallotment") as CheckBox;
                 if (chkBox.Checked == true)
                     Count1++;
             }
             if (Count1 <= 0)
             {
                 objCommon.DisplayMessage(this.updbulkanne, "Please Select only one Student ", this);
                 return;
             }

            foreach (ListViewDataItem item in LvPhdBulk.Items)
            {
                CheckBox chkBox = item.FindControl("chkallotment") as CheckBox;
                TextBox txtarea = item.FindControl("txtarea") as TextBox;
                
                if (chkBox.Checked)
                {
                    if (txtarea.Text == "")
                    {
                        count++;
                    }
                    else
                    {

                        if (RESEARCH.Length > 0)
                            RESEARCH += "$";
                        RESEARCH += txtarea.Text.Trim();
                        
                        if (studentIds.Length > 0)
                            studentIds += "$";
                        studentIds += (item.FindControl("hidIdNo") as HiddenField).Value.Trim();
                    }
                }
            }
         
            if (count > 0)
            {
                objCommon.DisplayMessage(this.updbulkanne, "Please Enter Area of Research", this.Page);
                return;
            }
            string SuperRole = ddlSupervisorrole.SelectedValue; ///Supervisor Role
            string Research = RESEARCH; 
            int NOFSP = 4;
            int SupervisorStatus = 0;
            int SupervisorNo = 0;
            if (CheckBox1.Checked)
            {
                SupervisorStatus = Convert.ToInt32(ddlSupervisor.SelectedValue);  /// External Supervisor 
                SupervisorNo = 0;                                                 /// Internal Supervisor 
            }
            else
            {

                 SupervisorNo = Convert.ToInt32(ddlSupervisor.SelectedValue);
                 SupervisorStatus = 0;
            }
            string sp_Name = "PKG_PHD_BULK_INSERT_ALLOTED_SUPERVISOR";
            string sp_Parameters = "@P_IDNO,@P_NDGS,@P_SUPERROLE,@P_SUPERVISOR_UANO,@P_SUPERVISOR_EXT_UANO,@P_RESEARCH";
            string call = "" + studentIds + "," + NOFSP + "," + SuperRole + "," + SupervisorNo + "," + SupervisorStatus + "," + Research +  "";
            dsMember = objCommon.DynamicSPCall_Select(sp_Name, sp_Parameters, call);
            if (dsMember.Tables[0].Rows.Count > 0)
            {
                if (dsMember.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    objCommon.DisplayMessage(this.updbulkanne, "Supervisor Allotment Done Successfully", this.Page);
                    SHOW();
                    ddlSupervisor.SelectedIndex = 0;
                    ddlSupervisorrole.SelectedIndex = 0;
                    foreach (ListViewDataItem item in LvPhdBulk.Items)
                    {
                        CheckBox chkBoxAll = item.FindControl("chkAll") as CheckBox;
                        if (chkBoxAll.Checked == true)
                        {
                            chkBoxAll.Checked = false;
                        }
                    }
                    return;
                }
                else
                {
                    objCommon.DisplayMessage(this.updbulkanne, "Oops Something went wrong", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayMessage(this.updbulkanne, "Oops Something went wrong", this.Page);

                return;
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion Submit
}
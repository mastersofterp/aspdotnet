//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : CANCEL DETENTION
// CREATION DATE : 21/01/2021                                              
// CREATED BY    : SAFAL GUPTA
//MODIFIED BY    :                                          
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.IO;
public partial class ACADEMIC_CancelDetaintion : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentRegist objSR = new StudentRegist();
    StudentRegistration objSReg = new StudentRegistration();
    string finaldet = string.Empty;

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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)

                //Student
                if (Session["usertype"].ToString().Equals("2"))
                {
                    btnShow.Visible = false;  
                }
                else if (Session["usertype"].ToString().Equals("1") || Session["usertype"].ToString().Equals("3") || Session["usertype"].ToString().Equals("4"))
                {
                    btnShow.Visible = true;           
                }
                
                PopulateDropDownList();
              //  btnReport.Enabled = false;

            }
        
            ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            hdfTotNoCourses.Value = System.Configuration.ConfigurationManager.AppSettings["totExamCourses"].ToString();
        }
         

        if (Session["userno"] == null || Session["username"] == null ||
               Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {

        }
        divMsg.InnerHtml = string.Empty; 
    }

    protected void ddlClgname_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        divInfo.Visible = false;
        divNote.Visible = false;

        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

        if (ddlClgname.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER S INNER JOIN ACD_DETENTION_INFO D ON(S.SESSIONNO=D.SESSIONNO AND S.ORGANIZATIONID=D.ORGANIZATIONID)", "DISTINCT S.SESSIONNO", "SESSION_PNAME", "S.SESSIONNO > 0 AND COLLEGE_ID=" + Convert.ToInt32(ddlClgname.SelectedValue) + "AND ISNULL(PROV_DETAIN,0)=1 AND ISNULL(FINAL_DETAIN,0)=1 AND ISNULL(CANCEL_DETAIN,0)=0 AND S.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "S.SESSIONNO DESC");
                
        }
        else
        {
            objCommon.DisplayMessage("Please Select College.", this.Page);
            ddlClgname.Focus();
        }

    }

    private void PopulateDropDownList()
    {
        try
        {
            this.objCommon.FillDropDownList(ddlClgname, "ACD_COLLEGE_MASTER CM INNER JOIN ACD_COLLEGE_SCHEME_MAPPING SM ON (CM.COLLEGE_ID=SM.COLLEGE_ID) INNER JOIN ACD_DETENTION_INFO D ON(SM.SCHEMENO=D.SCHEMENO AND CM.ORGANIZATIONID=D.ORGANIZATIONID)", "DISTINCT CM.COLLEGE_ID", "ISNULL(CM.COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "CM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND ISNULL(PROV_DETAIN,0)=1 AND ISNULL(FINAL_DETAIN,0)=1 AND ISNULL(CANCEL_DETAIN,0)=0 AND CM.COLLEGE_ID > 0 AND CM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "CM.COLLEGE_ID");
           
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void lvDetend_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkRow = e.Item.FindControl("chkAccept") as CheckBox;
        Label prov = e.Item.FindControl("lblProv") as Label;
        if (ViewState["DetainType"].ToString() == "1")
        {
            chkRow.Checked = false;
            chkRow.Enabled = true;
            divNote.Visible = false;
        }
        else
        {
            chkRow.Checked = true;
            chkRow.Enabled = false;
            divNote.Visible = true;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        ShowDetendInfo();
    }
    private void ShowDetendInfo()
    {
        // CODE FOR GET FINAL DETEND INFO WHICH ARE ALREADY DETEN IN PROVISIONAL 
        try
        {
           //int i= objCommon.LookUp("ACD_STUDENT_RESULT", "SEATNO", "IDNO =" + idno + " AND SESSIONNO=" + ddlSession.SelectedValue);
            string idno = objCommon.LookUp("ACD_STUDENT_RESULT SR INNER JOIN ACD_DETENTION_INFO DI ON (SR.IDNO=DI.IDNO)", "DISTINCT(SR.IDNO)","REGNO='" + txtStudent.Text + "'");
            if (idno == "")
            {
                divCheck.Visible = false;
                lvDetend.Visible = false;
                lvDetend.DataSource = null;
                lvDetend.DataBind();
                divInfo.Visible = false;
                divNote.Visible = false;
                objCommon.DisplayMessage(updPnl, "Student Not Found", this.Page);
                return;
            }
            ViewState["idno"] = idno.ToString();
            DataSet ds = objSReg.GetFinalDetendInfo(Convert.ToInt32(ddlSession.SelectedValue),Convert.ToInt32(ddlSem.SelectedValue) ,Convert.ToInt32(idno));
           
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblStudName.Text = ds.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblRegNo.Text = ds.Tables[0].Rows[0]["REGNO"].ToString();
                    lblDegree.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["BRANCH_LONG"].ToString();
                    lblScheme.Text = ds.Tables[0].Rows[0]["SCHEME"].ToString();
                    lblStudMobile.Text = ds.Tables[0].Rows[0]["STUDENTMOBILE"].ToString();
                    lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    ViewState["DetainType"] = ds.Tables[0].Rows[0]["DETAIN_TYPE"].ToString();

                    lvDetend.Visible = true;
                    lvDetend.DataSource = ds;
                    lvDetend.DataBind();
                    objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvDetend);//Set label - 
                    btnSubmit.Visible = true;
                    divInfo.Visible = true;
                }
                else
                {
                    divCheck.Visible = false;
                    lvDetend.Visible = false;
                    lvDetend.DataSource = null;
                    lvDetend.DataBind();
                    divInfo.Visible = false;
                    divNote.Visible = false;
                    objCommon.DisplayMessage(updPnl, "Student Not Found for detention.", this.Page);
                    return;
                   
                }
            }
            else
            {
                 lvDetend.DataSource = null;
                lvDetend.DataBind();
                divInfo.Visible = false;
                divNote.Visible = false;
            }
        }

        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // CODE FOR SUBMIT FINAL DETENTION DATA 

        int count = 0;

        foreach (ListViewDataItem dataitem in lvDetend.Items)
        {

            CheckBox cbRow = dataitem.FindControl("chkAccept") as CheckBox;
            if (cbRow.Checked == true)
            {
                count++;
            }
        }

        if (count == 0)
        {
            objCommon.DisplayMessage(this.Page, "Please Select atleast One Course!!", this.Page);
            return;
        }
        try
        {
            objSR.SESSIONNO = Convert.ToInt32(ddlSession.SelectedValue);
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            objSR.UA_NO = Convert.ToInt32(Session["userno"]);
            objSR.IPADDRESS = Session["ipAddress"].ToString();
            objSR.SEMESTERNO = Convert.ToInt32(ddlSem.SelectedValue);
            objSR.COLLEGE_CODE = Session["colcode"].ToString();
            objSR.IDNO = Convert.ToInt32(ViewState["idno"]);
            foreach (ListViewDataItem item in lvDetend.Items)
            {
                CheckBox chk = item.FindControl("chkAccept") as CheckBox;
                Label lblCourseNo = item.FindControl("lblCourseNo") as Label;
                if (chk.Checked)
                {
                     objSR.COURSENOS += lblCourseNo.ToolTip + ",";
                   
                }
            }

           
            if (objSReg.UpdateCancelDetend(objSR) == Convert.ToInt32(CustomStatus.RecordUpdated))
            {

                objCommon.DisplayMessage(this.Page, "Final Detention Cancelled Successfully...", this.Page);
                lvDetend.DataSource = null;
                lvDetend.DataBind();
               btnSubmit.Visible = false;
               divInfo.Visible = false;
               divNote.Visible = false;
               Clear();
               //btnCancel.Visible = false;
            }
            else
            {
                objCommon.DisplayMessage("Server Error", this.Page);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void Clear()
    {
        ddlClgname.SelectedIndex = 0;
        ddlSem.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        txtStudent.Text = "";
        lvDetend.DataSource = null;
        lvDetend.DataBind();
        btnSubmit.Visible = false;
        //btnCancel.Visible = false;
        divCheck.Visible = false;
        divInfo.Visible = false;
        divNote.Visible = false;
    }

  
    protected void btnCan_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());

    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));
        if (ddlSession.SelectedIndex > 0)
        {
            ddlSession.Focus();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S INNER JOIN acd_detention_info CT ON S.SEMESTERNO=CT.SEMESTERNO", "DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue), "S.SEMESTERNO");

            divCheck.Visible = false;
            btnSubmit.Visible = false;
            //btnCancel.Visible = false;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            ddlSem.SelectedIndex = 0;
            divInfo.Visible = false;
            divNote.Visible = false;
        }
        else
        {
            divCheck.Visible = false;
            btnSubmit.Visible = false;
           // btnCancel.Visible = false;
            ddlSem.SelectedIndex = 0;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            txtStudent.Text = "";
            divInfo.Visible = false;
            divNote.Visible = false;
        
        }

    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            divCheck.Visible = false;
            btnSubmit.Visible = false;
            //btnCancel.Visible = false;
            ddlSem.Focus();
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            txtStudent.Text = "";
            divInfo.Visible = false;
            divNote.Visible = false;
        }
        else {

            divCheck.Visible = false;
            btnSubmit.Visible = false;
           // btnCancel.Visible = false;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            txtStudent.Text = "";
            divInfo.Visible = false;
            divNote.Visible = false;
        
        }
    }
    
    protected void btnReport_Click(object sender, EventArgs e)
    {
        // CODE FOR GENRATE REPORT FOR CANCEL DETENTION STUDENTS 
        DataSet ds = objSReg.Get_Cancel_Detent_StudentList(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue), Convert.ToInt32(ddlClgname.SelectedValue));

        if (ds.Tables[0].Rows.Count > 0)
        {

            GridView GV = new GridView();
            string ContentType = string.Empty;

            GV.DataSource = ds;
            GV.DataBind();
            string attachment = "attachment; filename=" + "Detaintion Cancel Report" + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.MS-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GV.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
            
        }
        else
        {
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "alert('Data Not Found in Mark Table,So that Record Cannot be generate')", true);
            objCommon.DisplayMessage(updPnl, "Data Not Found", this.Page);
            btnSubmit.Visible = false;
            //btnCancel.Visible = false;
            lvDetend.DataSource = null;
            lvDetend.DataBind();
            divInfo.Visible = false;
            divNote.Visible = false;

            return;
        }
    }

   
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using BusinessLogicLayer.BusinessLogic;

/*
---------------------------------------------------------------------------------------------------------------------------
Created By : 
Created On : 
Purpose : For sending Email for online admission portal student.
Version : 1.0.0
---------------------------------------------------------------------------------------------------------------------------
Version              Modified On             Modified By             Purpose
---------------------------------------------------------------------------------------------------------------------------
1.0.1                Kajal Jaiswal          09-04-2024               Added changes for email added student name dynamically.
                                                                     and binding page title dynamically.
------------------------------------------- -------------------------------------------------------------------------------
*/


public partial class ACADEMIC_SendEmailToStudents : System.Web.UI.Page
{
    Common objCommon = new Common();
    OnlineAdmissionController objOA = new OnlineAdmissionController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    this.CheckPageAuthorization();
                    PoplulateDropDown();
                }
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
                Response.Redirect("~/notauthorized.aspx?page=SendEmailToStudents.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SendEmailToStudents.aspx");
        }
    }

    protected void rdoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            BindStudentList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string userno = string.Empty;string Email = string.Empty;string message = string.Empty;string Subject = string.Empty;string appIds = string.Empty;
        try
        {
            int count = 0;
            int status = 0;
            foreach (ListViewDataItem lv in lvStudList.Items)
            {
                CheckBox cbRow = lv.FindControl("chkSelect1") as CheckBox;
                //CheckBox chek = item.FindControl("chkSelect1") as CheckBox;
                Label lblEmail = lv.FindControl("lblEmail") as Label;
                Label lblStudmobile = lv.FindControl("lblStudmobile") as Label;
                Label lblstudname = lv.FindControl("lblName") as Label;
                if (cbRow.Checked==true)
                {
                    count++;
                    Email = lblEmail.Text.ToString().Trim();
                    Subject = txtSubject.Text.ToString().Trim();
                    string msg = "Dear " + lblstudname.Text.ToString().Trim() + "<br/>";
                    string s = txtMessage.Text.ToString().Trim();
                    message = msg + s.Replace("\r\n", "<br/>");

                    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation 
                    objSendEmail.SendEmail(Email, message, Subject); //Calling Method
                   
                }
              
            }
            if (count == 0)
            {
                objCommon.DisplayMessage(this.Page, "Please Select Atleast One Student.", this.Page);
                return;
            }
            else if (count > 0)
            {
                objCommon.DisplayMessage(this.Page, "Email Sent Successfully.", this.Page);
                Clear();
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void PoplulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_USER_REGISTRATION UR INNER JOIN ACD_ADMBATCH AD ON(UR.ADMBATCH=AD.BATCHNO)", "DISTINCT (ADMBATCH)", "AD.BATCHNAME", "BATCHNO > 0", "ADMBATCH DESC");
            if (ddlAdmBatch.Items.Count == 2)
            {
                ddlAdmBatch.SelectedIndex = 1;
            }
            objCommon.FillDropDownList(ddlProgramme, "ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "(CASE WHEN UGPGOT=1 THEN 'UG' WHEN UGPGOT=2 THEN 'PG' END) PROGRAMME_TYPE", "UGPGOT > 0", "UGPGOT");
            if (ddlProgramme.Items.Count == 2)
            {
                ddlProgramme.SelectedIndex = 1;
            }
            objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=BP.DEGREENO)", "DISTINCT BP.DEGREENO", "DBO.FN_DESC('DEGREENAME',BP.DEGREENO) DEGREENAME", "BP.DEGREENO > 0 AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENAME");
            if (ddlDegree.Items.Count < 2)
            {
                ddlDegree.SelectedIndex = 1;
            }
            ddlDegree.Focus();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
            if (ddlProgramme.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlDegree, "ACD_USER_BRANCH_PREF BP INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON(DB.DEGREENO=BP.DEGREENO)", "DISTINCT BP.DEGREENO", "DBO.FN_DESC('DEGREENAME',BP.DEGREENO) DEGREENAME", "BP.DEGREENO > 0 AND UGPGOT=" + Convert.ToInt32(ddlProgramme.SelectedValue), "DEGREENAME");

                if (ddlDegree.Items.Count < 2)
                {
                    ddlDegree.SelectedIndex = 1;
                }
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void BindStudentList()
    {
        try
        {
            DataSet dsStudList = objOA.GetStudentsForEmailSMS(Convert.ToInt32(ddlAdmBatch.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(rdoList.SelectedValue), Convert.ToInt32(ddlProgramme.SelectedValue));
            if (dsStudList.Tables[0].Rows.Count > 0)
            {
                lvStudList.DataSource = dsStudList;
                lvStudList.DataBind();
                lvStudList.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "No Record Found.", this.Page);
                lvStudList.DataSource = null;
                lvStudList.DataBind();
                lvStudList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //ddlProgramme.SelectedIndex= -1;
            ddlProgramme.Items.Clear();
            ddlProgramme.Items.Add(new ListItem("Please Select", "0"));

            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select","0"));

            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;

            if (ddlAdmBatch.SelectedIndex > 0)
            {
                objCommon.FillDropDownList(ddlProgramme, "ACD_USER_REGISTRATION", "DISTINCT UGPGOT", "(CASE WHEN UGPGOT=1 THEN 'UG' WHEN UGPGOT=2 THEN 'PG' END) PROGRAMME_TYPE", "UGPGOT > 0", "UGPGOT");                
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Clear()
    {
        ddlAdmBatch.SelectedIndex = 0;
        ddlProgramme.Items.Clear();
        ddlProgramme.Items.Add(new ListItem("Please Select", "0"));

        ddlDegree.Items.Clear();
        ddlDegree.Items.Add(new ListItem("Please Select", "0"));
        rdoList.SelectedIndex = 1;
        txtMessage.Text = string.Empty;
        txtSubject.Text = string.Empty;
        lvStudList.DataSource = null;
        lvStudList.DataBind();
        lvStudList.Visible = false;
        rdoList.SelectedValue = "1";
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lvStudList.DataSource = null;
            lvStudList.DataBind();
            lvStudList.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
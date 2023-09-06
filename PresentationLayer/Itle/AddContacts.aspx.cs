using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class ITLE_AddContacts : System.Web.UI.Page
{

    Common objCommon = new Common();

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "SHORTNAME", "SHORTNAME <> ''", "SHORTNAME");
            //Added on 23-03-2017 by Saket Singh
             //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "(LONGNAME+' ( '+UGPGOT+' )')AS LONGNAME", "LONGNAME <> ''", "LONGNAME");
            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "(LONGNAME+' ( '+ ISNULL(UGPGOT,'') +' )')AS LONGNAME", "LONGNAME <> ''", "LONGNAME");

            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (CDB.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_UA_SECTION US ON (US.UA_SECTION = CDB.UGPGOT)", "B.BRANCHNO", "(B.LONGNAME+' ( '+ ISNULL(US.UA_SECTIONNAME,'') +' )')AS LONGNAME", "B.LONGNAME <> ''", "B.LONGNAME ASC");

            ShowContacts();

            if (Convert.ToInt32(Session["usertype"]) == 3)
            {
                rdoGroupMail.Visible = true;
            }
        }
    }

    #endregion

    #region Private Method

    private void ShowContacts()
    {
        try
        {
            IMailController myController = new IMailController();
            DataSet ds = myController.GetContacts(
                Convert.ToInt32(Session["userno"]),
                (rdoFaculty.Checked ? 'F' : 'S'),
                Convert.ToInt32(Session["SessionNo"]),
                Convert.ToInt32(ddlBranch.SelectedValue));

            if (rdoStudent.Checked)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
                lvFaculty.Visible = false;
            }
            else if (rdoFaculty.Checked)
            {
                lvFaculty.DataSource = ds;
                lvFaculty.DataBind();
                lvFaculty.Visible = true;
                lvStudents.Visible = false;
            }
            else
            {
                lvStudents.DataSource = ds;  //for binding group of students
                lvStudents.DataBind();
                lvStudents.Visible = true;
                lvFaculty.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    #endregion

    #region Selected Index Changed

    protected void rdoStudent_CheckedChanged(object sender, EventArgs e)
    {
        ShowContacts();
        divBranch.Style["display"] = "block";
        divGroupMail.Style["display"] = "none";
        
    }

    protected void rdoFaculty_CheckedChanged(object sender, EventArgs e)
    {
        ShowContacts();
        divBranch.Style["display"] = "none";
        divGroupMail.Style["display"] = "none";
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowContacts();
    }
    
    protected void rdoGroupMail_CheckedChanged(object sender, EventArgs e)
    {
        
        divBranch.Style["display"] = "none";
        divGroupMail.Style["display"] = "block";
        objCommon.FillDropDownList(ddlGroupMail, "ITLE_EMAIL_GROUP", "DISTINCT EGROUPNO", "GROUPNAME", "FACULTY_UANO="+Session["userno"], "EGROUPNO DESC");
        lvFaculty.Visible = false;
        lvStudents.Visible = false;
       
        

    }

    protected void ddlGroupMail_SelectedIndexChanged(object sender, EventArgs e)
    {
        IMailController myController = new IMailController();
        DataSet ds = myController.GetGroupStudent(Convert.ToInt32(Session["userno"]),Convert.ToInt32(Session["SessionNo"]),Convert.ToInt32(ddlGroupMail.SelectedValue));
        lvStudents.Visible = true;
        lvStudents.DataSource = ds;
        lvStudents.DataBind();

    }

    #endregion
}

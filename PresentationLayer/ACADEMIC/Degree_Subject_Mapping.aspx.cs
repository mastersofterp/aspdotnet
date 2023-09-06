using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

public partial class ACADEMIC_Degree_Subject_Mapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                    //  lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    //Fill DropDownLists
                    PopulatedropDown();
                    BindSubjectsList();
                //this.BindListView();
                ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                ViewState["action"] = "add";
            }
        }
    }
    protected void PopulatedropDown()
    {
        objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH DB INNER JOIN ACD_DEGREE D ON(DB.DEGREENO=D.DEGREENO)", "DISTINCT DB.DEGREENO", "D.DEGREENAME", "DB.DEGREENO > 0", "DB.DEGREENO");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int isCutOff = 0;
            int isCompulsory = 0;
            int isOthers = 0;
            isCutOff = chkCutOff.Checked ? 1 : 0;
            isCompulsory = chkComp.Checked ? 1 : 0;
            isOthers = chkOthers.Checked ? 1 : 0;
            if (ViewState["action"].ToString().Equals("add"))
            {
                CustomStatus cs = (CustomStatus)objCommon.Add_Degree_Sub_Mapping(Convert.ToInt32(ddlDegree.SelectedValue), txtSubName.Text.Trim(), isCompulsory,
                isCutOff, isOthers);

                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this.updSubject, "Record Saved Successfully.", this.Page);
                    //BindSubjectsList();
                    Clear();
                }
                else if (cs.Equals(CustomStatus.RecordExist))
                {
                    objCommon.DisplayMessage(this.updSubject, "Record Already Exists.", this.Page);
                }
                else
                {
                    objCommon.DisplayMessage(this.updSubject, "Something Went Wrong.", this.Page);
                }
            }
            else if(ViewState["action"].ToString().Equals("edit"))
            {
                CustomStatus cs = (CustomStatus) objCommon.Update_Degree_Sub_Mapping(Convert.ToInt32(ddlDegree.SelectedValue), txtSubName.Text.Trim(), isCompulsory,
                isCutOff, isOthers,Convert.ToInt32(ViewState["subID"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(this.updSubject, "Record Updated Successfully.", this.Page);
                    Clear();
                }
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
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int subID = int.Parse(btnEdit.CommandArgument);
            ViewState["subID"] = subID;
            ViewState["action"] = "edit";
            Show_Single_DegreeSub_Map();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindSubjectsList()
    {
        DataSet dsSub = new DataSet();
        dsSub = objCommon.GetDegreeSubjectMapping();
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            lvSubjectList.DataSource = dsSub;
            lvSubjectList.DataBind();
            lvSubjectList.Visible = true;
        }
        else
        {
            lvSubjectList.DataSource = null;
            lvSubjectList.DataBind();
            lvSubjectList.Visible = false;
        }
    }
    protected void Clear()
    {
        ddlDegree.SelectedIndex = 0;
        txtSubName.Text = string.Empty;
        chkComp.Checked = false;
        chkCutOff.Checked = false;
        chkOthers.Checked = false;
        BindSubjectsList();
        ViewState["action"] = "add";
    }
    protected void Show_Single_DegreeSub_Map()
    {
        DataSet dsSingle = objCommon.Get_Single_DegreeSubjectMapping(Convert.ToInt32(ViewState["subID"]));
        if (dsSingle.Tables[0].Rows.Count > 0)
        {
            ddlDegree.SelectedValue=dsSingle.Tables[0].Rows[0]["DEGREENO"].ToString();
            txtSubName.Text = dsSingle.Tables[0].Rows[0]["SUB_NAME"].ToString();
            chkCutOff.Checked = dsSingle.Tables[0].Rows[0]["IS_CUTOFF"].ToString() == "1" ? chkCutOff.Checked = true : chkCutOff.Checked = false;
            chkComp.Checked = dsSingle.Tables[0].Rows[0]["IS_COMPULSORY"].ToString() == "1" ? chkComp.Checked = true : chkComp.Checked = false;
            chkOthers.Checked = dsSingle.Tables[0].Rows[0]["IS_OTHERS"].ToString()     == "1" ? chkOthers.Checked=true : chkOthers.Checked = false;
        }
    }
}
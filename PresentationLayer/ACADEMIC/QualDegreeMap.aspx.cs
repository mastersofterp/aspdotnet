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
public partial class ACADEMIC_QualDegreeMap : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    //OnlineAdmission objAd = new OnlineAdmission();
    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();
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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                        //ViewState["Action"] = "add";
                    PopulateDropDown();
                    BindQualDegreeList();
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
                Response.Redirect("~/notauthorized.aspx?page=QualDegreeMap.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=QualDegreeMap.aspx");
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_COLLEGE_DEGREE_BRANCH DB", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "UGPGOT=2", "DEGREENAME");
            objCommon.FillDropDownList(ddlQualDegree, "ACD_COLLEGE_DEGREE_BRANCH DB", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "UGPGOT>0", "DEGREENAME");
            ddlQualDegree.Items.Add(new ListItem("BE", "901"));
            ddlQualDegree.Items.Add(new ListItem("Others", "902"));
            ddlQualDegree.Items.Add(new ListItem("BCA.LLB(Hons.)", "903"));
            ddlQualDegree.Items.Add(new ListItem("BTech.LLB", "904"));
            ddlQualDegree.Items.Add(new ListItem("LLB", "905"));
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            string ipAddress=string.Empty;
            if (chkStatus.Checked)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            ipAddress=Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (btnSubmit.Text.Equals("Submit"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Add_Qualify_Degree(Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlQualDegree.SelectedValue), status, Convert.ToInt32(Session["OrgId"]),
                ipAddress, Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updQual, "Record Saved Successfully.", this.Page);
                    ClearFields();
                    BindQualDegreeList();
                    return;
                }
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Update_Qualify_Degree(Convert.ToInt32(ViewState["qualDegree"]), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlQualDegree.SelectedValue), status, ipAddress, Convert.ToInt32(Session["userno"]));
                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage(updQual, "Record Updated Successfully.", this.Page);
                    ClearFields();
                    BindQualDegreeList();
                    return;
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
            ddlDegree.SelectedIndex = 0;
            ddlQualDegree.SelectedIndex = 0;
            chkStatus.Checked = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ClearFields()
    {
        ddlDegree.SelectedIndex = 0;
        ddlQualDegree.SelectedIndex = 0;
        chkStatus.Checked = false;
        //ViewState["action"] = null;
        btnSubmit.Text = "Submit";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton imgButton = sender as ImageButton;
            int qual_Degree = Convert.ToInt32(imgButton.CommandArgument.ToString());
            //ViewState["Action"] = "edit";
            ViewState["qualDegree"] = qual_Degree;
            btnSubmit.Text = "Update";
            BindQualDegreeList();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void BindQualDegreeList()
    {
        DataSet ds = null;
        if (btnSubmit.Text.Equals("Update"))
        {
             ds = Admcontroller.GetQualDegreeDetails(Convert.ToInt32(ViewState["qualDegree"]));
             ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
             ddlQualDegree.SelectedValue = ds.Tables[0].Rows[0]["QUALIFY_DEGREENO"].ToString();
             if (ds.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString().Equals("1"))
             {
                 chkStatus.Checked = true;
             }
             else
             {
                 chkStatus.Checked = false;
             }
        }
        else
        {
            ds = Admcontroller.GetQualDegreeDetails(0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvQualDegree.DataSource = ds;
                lvQualDegree.DataBind();
                lvQualDegree.Visible = true;
            }
            else
            {
                lvQualDegree.DataSource = null;
                lvQualDegree.DataBind();
                lvQualDegree.Visible = false;
            }
        }
       
    }
}
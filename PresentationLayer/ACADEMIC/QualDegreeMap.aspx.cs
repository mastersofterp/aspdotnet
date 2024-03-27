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
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

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
            objCommon.FillDropDownList(ddlQualDegree, "ACD_ADMP_QUALIDEGREE", "QUALI_DEGREE_ID", "QUALI_DEGREE_NAME", "STATUS=1", "QUALI_DEGREE_NAME");
            //ddlQualDegree.Items.Add(new ListItem("BE", "901"));
            //ddlQualDegree.Items.Add(new ListItem("Others", "902"));
            //ddlQualDegree.Items.Add(new ListItem("BCA.LLB(Hons.)", "903"));
            //ddlQualDegree.Items.Add(new ListItem("BTech.LLB", "904"));
            //ddlQualDegree.Items.Add(new ListItem("LLB", "905"));
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
            string ipAddress = string.Empty;
            if (chkStatus.Checked)
            {
                status = 1;
            }
            else
            {
                status = 0;
            }
            ipAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
            if (btnSubmit.Text.Equals("Submit"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Add_Qualify_Degree(
                Convert.ToInt32(ddlDegree.SelectedValue),
                Convert.ToInt32(ddlBranch.SelectedValue),
                Convert.ToInt32(ddlQualDegree.SelectedValue),
                Convert.ToInt32(ddlQualBranch.SelectedValue),


                status,
                Convert.ToInt32(Session["OrgId"]),
                ipAddress,
                Convert.ToInt32(Session["userno"])
            );
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(updQual, "Record Saved Successfully.", this.Page);
                    ClearFields();
                    BindQualDegreeList();
                    return;
                }
                else if (cs.Equals(CustomStatus.Error))
                {
                    objCommon.DisplayMessage(updQual, "Record Already exists", this.Page);
                }
            }
            else if (btnSubmit.Text.Equals("Update"))
            {
                CustomStatus cs = (CustomStatus)Admcontroller.Update_Qualify_Degree(
                Convert.ToInt32(ViewState["qualDegree"]),
                Convert.ToInt32(ddlDegree.SelectedValue),
                Convert.ToInt32(ddlBranch.SelectedValue),
                Convert.ToInt32(ddlQualDegree.SelectedValue),
                Convert.ToInt32(ddlQualBranch.SelectedValue),
                status,
                ipAddress,
                Convert.ToInt32(Session["userno"])
            );


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
            ddlBranch.SelectedIndex = 0;
            ddlQualBranch.SelectedIndex = 0;
            chkStatus.Checked = false;
            btnSubmit.Text = "Submit";
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
        ddlBranch.SelectedIndex = 0;
        ddlQualBranch.SelectedIndex = 0;
        chkStatus.Checked = false;
        //ViewState["action"] = null;
        btnSubmit.Text = "Submit";
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            PopulateDropDown();
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
            if (ds.Tables[0].Rows[0]["DEGREENO"] != DBNull.Value && ds.Tables[0].Rows[0]["DEGREENO"].ToString() != "")
            {
                ddlDegree.SelectedValue = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            }
            else
            {
                ddlDegree.SelectedValue = "0";
            }
            if (ddlDegree.SelectedValue == "19" || ddlDegree.SelectedValue == "20")
            {
                divBranch.Visible = true;
                if (ds.Tables[0].Rows[0]["BRANCHNO"] != DBNull.Value && ds.Tables[0].Rows[0]["BRANCHNO"].ToString() != "")
                {
                    if (ddlDegree.SelectedValue == "19")
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "CODE", "ACTIVESTATUS=1 AND DEGREENO=19", "CODE");
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    }
                    else 
                    {
                        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "CODE", "ACTIVESTATUS=1 AND DEGREENO=20", "CODE");
                        ddlBranch.SelectedValue = ds.Tables[0].Rows[0]["BRANCHNO"].ToString();

                    }
                }
                else
                {
                    ddlBranch.SelectedValue = "0";
                }
            }

            if (ds.Tables[0].Rows[0]["QUALIFY_DEGREENO"] != DBNull.Value && ds.Tables[0].Rows[0]["QUALIFY_DEGREENO"].ToString() != "")
            {
                //int qualidegree = Convert.ToInt32(ddlQualDegree.SelectedValue);
                //objCommon.FillDropDownList(ddlQualBranch, "ACD_ADMP_QUALIPROGRAM", "PROG_ID", "PROG_NAME", "STATUS=1 AND QUALI_DEGREE_ID= " + qualidegree, "PROG_ID");
                ddlQualDegree.SelectedValue = ds.Tables[0].Rows[0]["QUALIFY_DEGREENO"].ToString();
            }
            else
            {
                ddlQualDegree.SelectedValue = "0";
            }
            if (ds.Tables[0].Rows[0]["QUAL_BRANCHNO"] != DBNull.Value && ds.Tables[0].Rows[0]["QUAL_BRANCHNO"].ToString() != "")
            {
                int qualidegree = Convert.ToInt32(ddlQualDegree.SelectedValue);
                objCommon.FillDropDownList(ddlQualBranch, "ACD_ADMP_QUALIPROGRAM", "PROG_ID", "PROG_NAME", "STATUS=1 AND QUALI_DEGREE_ID= " + qualidegree, "PROG_ID");
                ddlQualBranch.SelectedValue = ds.Tables[0].Rows[0]["QUAL_BRANCHNO"].ToString();
            }
            else
            {
                ddlQualBranch.SelectedValue = "0";
            }

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

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlQualDegree, "ACD_ADMP_QUALIDEGREE", "QUALI_DEGREE_ID", "QUALI_DEGREE_NAME", "STATUS=1", "QUALI_DEGREE_NAME");

       // objCommon.FillDropDownList(ddlQualDegree, "ACD_COLLEGE_DEGREE_BRANCH DB", "DISTINCT DB.DEGREENO", "DBO.FN_DESC('DEGREENAME',DB.DEGREENO) DEGREENAME", "UGPGOT>0", "DEGREENAME");
        //ddlQualDegree.Items.Add(new ListItem("BE", "901"));
        //ddlQualDegree.Items.Add(new ListItem("Others", "902"));
        //ddlQualDegree.Items.Add(new ListItem("BCA.LLB(Hons.)", "903"));
        //ddlQualDegree.Items.Add(new ListItem("BTech.LLB", "904"));
        //ddlQualDegree.Items.Add(new ListItem("LLB", "905"));


        if (ddlDegree.SelectedValue == "19" || ddlDegree.SelectedValue == "20")
        {
            if (ddlDegree.SelectedValue == "19")
            {
                divBranch.Visible = true;
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "CODE", "ACTIVESTATUS=1 AND DEGREENO=19", "CODE");
            }
            else 
            {
                divBranch.Visible = true;
                objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH", "BRANCHNO", "CODE", "ACTIVESTATUS=1 AND DEGREENO=20", "CODE");

            }
        }
        else if (ddlDegree.SelectedValue != "19" || ddlDegree.SelectedValue != "20")
        {
            divBranch.Visible = false;
        }
    }
    protected void ddlQualDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        int qualidegree = Convert.ToInt32(ddlQualDegree.SelectedValue);
        objCommon.FillDropDownList(ddlQualBranch, "ACD_ADMP_QUALIPROGRAM", "PROG_ID", "PROG_NAME", "STATUS=1 AND QUALI_DEGREE_ID= " + qualidegree, "PROG_ID");
  
    }

}
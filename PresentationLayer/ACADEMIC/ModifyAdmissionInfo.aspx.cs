using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Linq;
using System.Data;

public partial class ACADEMIC_ModifyAdmissionInfo : System.Web.UI.Page
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
                //Page Authorization
                CheckPageAuthorization();
                ViewState["usertype"] = Session["usertype"];
                AdmDetails.Visible = false;
                FillDropDown();
                if (Session["usertype"].ToString() == "1")
                {
                    this.objCommon.FillDropDownList(ddlSearch, "ACD_SEARCH_CRITERIA", "ID", "CRITERIANAME, ISNULL(IS_FEE_RELATED,0) IS_FEE_RELATED", "ID > 0 AND ISNULL(IS_FEE_RELATED,0)=0", "SRNO");
                    ddlSearch.SelectedIndex = 1;
                    ddlSearch_SelectedIndexChanged(sender, e);
                }
            }
        }
    }

    private void FillDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "SEMESTERNAME");
            objCommon.FillDropDownList(ddlclaim, "ACD_IDTYPE", "IDTYPENO", "IDTYPEDESCRIPTION", "IDTYPENO>0 AND ISNULL(ACTIVESTATUS,0) = 1", "IDTYPEDESCRIPTION");
            objCommon.FillDropDownList(ddlAcademicYear, "ACD_ACADEMIC_YEAR", "ACADEMIC_YEAR_ID", "ACADEMIC_YEAR_NAME", "ACADEMIC_YEAR_ID>0 AND ISNULL(ACTIVE_STATUS,0) = 1", "ACADEMIC_YEAR_NAME");
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
                Response.Redirect("~/notauthorized.aspx?page=ModifyAdmissionInfo.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ModifyAdmissionInfo.aspx");
        }

        string IsAuthorised = objCommon.LookUp("ACD_MODULE_CONFIG", "ISNULL(AUTHORISED_USERS_FOR_MODIFY_ADMISSION_INFO,0)", "");
        if (!IsAuthorised.Contains(Session["userno"].ToString()))
            Response.Redirect("~/notauthorized.aspx?page=ModifyAdmissionInfo.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int uano = Convert.ToInt32(Session["userno"].ToString());
        lblNoRecords.Visible = true;
        string value = string.Empty;
        if (ddlDropdown.SelectedIndex > 0)
            value = ddlDropdown.SelectedValue;
        else
            value = txtSearch.Text;
        if (string.IsNullOrEmpty(value))
        {
            objCommon.DisplayMessage("Please Select / Enter " + ddlSearch.SelectedItem, this.Page);
            return;
        }
        bindlist(ddlSearch.SelectedItem.Text, value, uano);
        txtSearch.Text = string.Empty;
        AdmDetails.Visible = false;
    }

    private void bindlist(string category, string searchtext, int uano)
    {
        StudentController objSC = new StudentController();
        objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));
        DataSet ds = objSC.RetrieveStudentDetailsToModifyAdmissionInfo(searchtext, category, uano);

        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlLV.Visible = true;
            lvStudent.Visible = true;
            lvStudent.DataSource = ds;
            lvStudent.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvStudent);//Set label - 
            lblNoRecords.Text = "Total Records : " + ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblNoRecords.Text = "Total Records : 0";
            lvStudent.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Clear();
            pnlLV.Visible = false;
            lblNoRecords.Visible = false;
            lvStudent.DataSource = null;
            lvStudent.DataBind();
            if (ddlSearch.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetSearchDropdownDetails(ddlSearch.SelectedItem.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string ddltype = ds.Tables[0].Rows[0]["CRITERIATYPE"].ToString();
                    string tablename = ds.Tables[0].Rows[0]["TABLENAME"].ToString();
                    string column1 = ds.Tables[0].Rows[0]["COLUMN1"].ToString();
                    string column2 = ds.Tables[0].Rows[0]["COLUMN2"].ToString();
                    if (ddltype == "ddl")
                    {
                        pnltextbox.Visible = false;
                        txtSearch.Visible = false;
                        pnlDropdown.Visible = true;
                        divtxt.Visible = false;
                        lblDropdown.Text = ddlSearch.SelectedItem.Text;
                        objCommon.FillDropDownList(ddlDropdown, tablename, column1, column2, column1 + ">0", column1);
                    }
                    else
                    {
                        pnltextbox.Visible = true;
                        divtxt.Visible = true;
                        txtSearch.Visible = true;
                        pnlDropdown.Visible = false;
                    }
                }
            }
            else
            {
                pnltextbox.Visible = false;
                pnlDropdown.Visible = false;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lnkId_Click(object sender, EventArgs e)
    {
        Clear();
        LinkButton lnk = sender as LinkButton;
        string url = string.Empty;
        if (Request.Url.ToString().IndexOf("&id=") > 0)
            url = Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&id="));
        else
            url = Request.Url.ToString();

        Label lblenrollno = lnk.Parent.FindControl("lblstuenrollno") as Label;

        Session["stuinfoenrollno"] = lblenrollno.Text.Trim();
        Session["stuinfofullname"] = lnk.Text.Trim();
        Session["stuinfoidno"] = Convert.ToInt32(lnk.CommandArgument);
        ViewState["idno"] = Session["stuinfoidno"].ToString();
        int idno = 0;
        StudentController objSC = new StudentController();

        if (ViewState["usertype"].ToString() == "2" || (ViewState["usertype"].ToString() == "14"))
            idno = Convert.ToInt32(Session["idno"]);
        else
            idno = Convert.ToInt32(ViewState["idno"]);

        AdmDetails.Visible = true;
        lvStudent.Visible = false;
        lvStudent.DataSource = null;
        lblNoRecords.Visible = false;
        ShowDetails();
    }

    private void ShowDetails()
    {
        int idno = Convert.ToInt32(ViewState["idno"]);
        StudentController objSC = new StudentController();
        FeeCollectionController feeController = new FeeCollectionController();
        try
        {
            if (idno > 0)
            {
                AdmDetails.Visible = true;
                DataTableReader dtr = objSC.GetStudentCompleteDetails(idno);

                if (dtr != null)
                {
                    if (dtr.Read())
                    {
                        lblRegNo.Text = dtr["REGNO"].ToString();
                        ViewState["admbatch"] = dtr["ADMBATCH"];
                        lblName.Text = dtr["STUDNAME"] == null ? string.Empty : dtr["STUDNAME"].ToString();
                        lblName.ToolTip = idno.ToString();
                        lblGender.Text = (dtr["SEX"].ToString() == "M" && dtr["SEX"] != null) ? "Male" : "Female";
                        lblMName.Text = dtr["FATHERNAME"] == null ? string.Empty : dtr["FATHERNAME"].ToString();
                        lblEnrollNo.Text = dtr["ENROLLNO"] == null ? string.Empty : dtr["ENROLLNO"].ToString();
                        lblApplicationId.Text = dtr["APPLICATIONID"] == null ? string.Empty : dtr["APPLICATIONID"].ToString();

                        ddlSemester.SelectedValue = dtr["SEMESTERNO"].ToString();
                        ddlAcademicYear.SelectedValue = dtr["ACADEMIC_YEAR_ID"] == null ? string.Empty : dtr["ACADEMIC_YEAR_ID"].ToString();
                        ddlclaim.SelectedValue = dtr["IDTYPE"] == null ? string.Empty : dtr["IDTYPE"].ToString();
                        ddlBatch.SelectedValue = dtr["ADMBATCH"] == null ? string.Empty : dtr["ADMBATCH"].ToString();
                    }
                }
            }
            else
            {
                lblEnrollNo.Text = string.Empty;
            }
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
            int academicYear = Convert.ToInt16(ddlAcademicYear.SelectedValue);
            int semesterNo = Convert.ToInt16(ddlSemester.SelectedValue);
            int IDTYPE = Convert.ToInt16(ddlclaim.SelectedValue);
            int admBatch = Convert.ToInt16(ddlBatch.SelectedValue);

            StudentController objSC = new StudentController();
            int ret = objSC.ModifyStudAdmissionInfo(Convert.ToInt16(lblName.ToolTip), IDTYPE, academicYear, semesterNo, admBatch);
            if (ret != -99)
            {
                objCommon.DisplayMessage("Data Updated Successfully.", this.Page);
                ShowDetails();
            }
            else
                objCommon.DisplayMessage("Error.", this.Page);


        }
        catch { throw; }
    }

    private void Clear()
    {
        AdmDetails.Visible = false;
        ddlAcademicYear.SelectedIndex = ddlclaim.SelectedIndex = ddlBatch.SelectedIndex = ddlSemester.SelectedIndex = 0;
    }
}
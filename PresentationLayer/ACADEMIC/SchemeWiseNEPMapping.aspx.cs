using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;


public partial class ACADEMIC_SchemeWiseNEPMapping : System.Web.UI.Page
{
    Common objCommon = new Common();
    NEPController nepC = new NEPController(); 
    UAIMS_Common objUCommon = new UAIMS_Common();

    #region Page Action
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                    }
                    ViewState["edit"] = "add";
                    btnSubmit.Enabled = true;
                }
                PopulateDropdown();
                PopulateNEPCategory();
            }
            BindList();
        }
        catch (Exception ex)
        {
            throw;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Src", "test5();", true);
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=SchemeWiseNEPMapping.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SchemeWiseNEPMapping.aspx");
        }
    }
    #endregion

    protected void PopulateDropdown() 
    {
        objCommon.FillDropDownList(ddlScheme, "ACD_SCHEME WITH (NOLOCK)", "SCHEMENO", "SCHEMENAME", "SCHEMENO > 0", "SCHEMENO DESC");
    }

    private void PopulateNEPCategory()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_NEP_COURSE_CATEGORY WITH (NOLOCK)", "CATEGORYNO", "CATEGORYNAME", "ACTIVESTATUS<>0", "CATEGORYNO");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    chkNEPCategoryList.DataTextField = "CATEGORYNAME";
                    chkNEPCategoryList.DataValueField = "CATEGORYNO";
                    chkNEPCategoryList.ToolTip = "CATEGORYNAME";
                    chkNEPCategoryList.DataSource = ds.Tables[0];
                    chkNEPCategoryList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void BindList() 
    {
        try
        {
            DataSet ds = nepC.GetSchemeWiseNEPMapping();
            if (ds != null && ds.Tables.Count > 0)
            {
                lvSchemeNEPMapping.DataSource = ds;
                lvSchemeNEPMapping.DataBind();
            }
        }
        catch 
        {
            throw;
        }
    }

    protected void ddlScheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        MultipleCollegeBind();
    }

    private void MultipleCollegeBind()
    {
        try
        {
            DataSet ds = null;
            ds = nepC.Get_CollegeID_ByScheme(Convert.ToInt32(ddlScheme.SelectedValue));

            ddlCollege.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Enabled = true;
                // Assuming ds.Tables[0] contains the data for the dropdown
                DataTable dataTable = ds.Tables[0];

                ddlCollege.DataSource = dataTable;
                ddlCollege.DataTextField = dataTable.Columns[3].ColumnName;
                ddlCollege.DataValueField = "CombinedValue";
                dataTable.Columns.Add("CombinedValue", typeof(string), "COLLEGE_ID + '-' + COLLEGE_CODE");

                ddlCollege.DataBind();
            }
            else 
            {
                btnSubmit.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CustomStatus cs = CustomStatus.Others;
            int activity = 0;
            int groupid = 0;
            int Schemeno = Convert.ToInt32(ddlScheme.SelectedValue);
            string CollegeIds = string.Empty;
            string CollegeCodes = string.Empty;
            string NEPCategorys = string.Empty;
            string NEPSchemeNames = string.Empty;
            CollegeIds = GetSelectedCollegeIds();
            CollegeCodes = GetSelectedCollegeCodes();
            NEPCategorys = GetSelectedNEPCategorys();
            NEPSchemeNames = GetNEPSchemeNames();
            int Ua_no = Convert.ToInt32(Session["userno"]);
            DateTime date = DateTime.Now;
            string IpAddress = Request.ServerVariables["REMOTE_ADDR"];

            if (CollegeIds == string.Empty || CollegeIds == "" || CollegeIds == null)
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Please Select Atleast one College!", this.Page);
                return;
            }
            if (NEPCategorys == string.Empty || NEPCategorys == "" || NEPCategorys == null)
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Please Select Atleast one NEP Category!", this.Page);
                return;
            }

            if (ViewState["edit"].ToString() == "add")
            {
                activity = 1;
                cs = (CustomStatus)nepC.SchemewiseNEPInsert(activity, Schemeno, CollegeIds, CollegeCodes, NEPCategorys, NEPSchemeNames, date, Ua_no, IpAddress, groupid);
            }
            else if (ViewState["edit"].ToString() == "edit")
            {
                activity = 2;
                cs = (CustomStatus)nepC.SchemewiseNEPInsert(activity, Schemeno, CollegeIds, CollegeCodes, NEPCategorys, NEPSchemeNames, date, Ua_no, IpAddress, Convert.ToInt32(ViewState["GROUPID"].ToString()));
            }

            if (cs.Equals(CustomStatus.RecordSaved))
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Record Saved Successfully!", this.Page);
                Clear();
            }
            else if (cs.Equals(CustomStatus.DuplicateRecord))
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Record Already Exists!", this.Page);
            }
            else if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Record Updated Successfully!", this.Page);
                Clear();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updNEPMapping, "Error!", this.Page);
            }
            BindList();
        }
        catch 
        {
            throw;
        }
    }

    private string GetSelectedCollegeIds()
    {
        string CollegeIds = string.Empty;
        foreach (ListItem item in ddlCollege.Items)
        {
            if (item.Selected)
            {
                CollegeIds += item.Value.Split('-')[0] + ','; // Extract the ID part
            }
        }
        if (CollegeIds.Length > 1)
        {
            CollegeIds = CollegeIds.Remove(CollegeIds.Length - 1);
        }
        return CollegeIds;
    }

    private string GetSelectedCollegeCodes()
    {
        string CollegeCodes = string.Empty;
        foreach (ListItem item in ddlCollege.Items)
        {
            if (item.Selected)
            {
                CollegeCodes += item.Value.Split('-')[1] + ','; // Extract the code part
            }
        }
        if (CollegeCodes.Length > 1)
        {
            CollegeCodes = CollegeCodes.Remove(CollegeCodes.Length - 1);
        }
        return CollegeCodes;
    }

    private string GetSelectedNEPCategorys()
    {
        string NEPCategorys = string.Empty;
        foreach (ListItem items in chkNEPCategoryList.Items)
        {
            if (items.Selected == true)
            {
                NEPCategorys += (items.Value).Split('-')[0] + ','; 
            }
        }
        if (NEPCategorys.Length > 1)
        {
            NEPCategorys = NEPCategorys.Remove(NEPCategorys.Length - 1);
        }
        return NEPCategorys;
    }

    private string GetNEPSchemeNames()
    {
        string NEPSchemeNames = string.Empty;

        foreach (ListItem item1 in ddlCollege.Items)
        {
            foreach (ListItem item2 in chkNEPCategoryList.Items)
            {
                if (item1.Selected && item2.Selected)
                {
                    NEPSchemeNames += ddlScheme.SelectedItem.Text + "-" + item1.Text + "-" + item2.Text + ',';
                }
            }
        }
        if (NEPSchemeNames.Length > 1)
        {
            NEPSchemeNames = NEPSchemeNames.Remove(NEPSchemeNames.Length - 1);
        }

        

        return NEPSchemeNames;
    }

    private void Clear() 
    {
        ddlScheme.SelectedIndex = 0;
        ddlCollege.Items.Clear();
        chkNEPCategory.Checked = false;
        chkNEPCategoryList.ClearSelection();
        ViewState["GROUPID"] = null;
        ViewState["edit"] = "add";
        btnSubmit.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void lvSchemeNEPMapping_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem dataitem = (ListViewDataItem)e.Item;
        ImageButton btnEdit = dataitem.FindControl("btnEdit") as ImageButton;
        int gropuid = Convert.ToInt32(btnEdit.CommandArgument);
        ListView lv = dataitem.FindControl("lvDetails") as ListView;
        try
        {
            DataSet ds = nepC.GetSchemeWiseNEPMappingbyGroupID(gropuid);
            lv.DataSource = ds;
            lv.DataBind();

        }
        catch { throw; }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEditRecord = sender as ImageButton;
            int recordId = int.Parse(btnEditRecord.CommandArgument);
            ViewState["edit"] = "edit";
            DataSet ds = nepC.GetSchemeWiseNEPMappingEdit(recordId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PopulateDropdown();
                ViewState["GROUPID"] = recordId.ToString();
                ddlScheme.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["SCHEMENO"]);

                MultipleCollegeBind();
                string Collegeid = Convert.ToString(ds.Tables[0].Rows[0]["COLLEGE_ID"]);
                string[] subs = Collegeid.Split(',');

                foreach (ListItem collegeitems in ddlCollege.Items)
                {
                    string[] collegeValues = collegeitems.Value.Split('-'); 
                    foreach (string sub in subs)
                    {
                        if (collegeValues[0].Trim() == sub.Trim()) 
                        {
                            collegeitems.Selected = true;
                            break;
                        }
                    }
                }


                PopulateNEPCategory();

                string NEPCategory = Convert.ToString(ds.Tables[0].Rows[0]["CATEGORYNO"]);
                string[] subs2 = NEPCategory.Split(',');
                int count = 0;
                foreach (ListItem NEPCategoryitems in chkNEPCategoryList.Items)
                {
                    for (int i = 0; i < subs2.Count(); i++)
                    {
                        if (subs2[i].ToString().Trim() == NEPCategoryitems.Value)
                        {
                            NEPCategoryitems.Selected = true;
                            count++;
                        }
                    }
                    
                    if (chkNEPCategoryList.Items.Count == count)
                        chkNEPCategory.Checked = true;
                    else
                        chkNEPCategory.Checked = false;
                }
                
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
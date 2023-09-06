//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : REPAIR AND MAINTANANCE                                               
// PAGE NAME     : COMPLAINT ITEMMASTER                                                 
// CREATION DATE : 15-April-2009                                                        
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;
using System.Configuration;
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

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


public partial class Estate_complaint_item : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    //To Set the MasterPage
    //    if (Session["masterpage"] != null)
    //        objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
    //    else
    //        objCommon.SetMasterPage(Page, "");
    //}
    
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
                //   //Page Authorization
                CheckPageAuthorization();

                //   //Set the Page Title
                //// Page.Title = Session["coll_name"].ToString();

                //   //Load Page Help
                //   if (Request.QueryString["pageno"] != null)
                //   {
                //       lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //   }

                // Fill DropDown
                PopulateDropDownList();

                // show departmentname according username
                ComplaintController objCTC = new ComplaintController();
                ddlDepartmentName.Text = objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())).ToString();

                //Bind the ListView Complaint Item
                BindListViewComplaintItem(objCTC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));


                //Fill dropdown according departmentname
               // FillComplaintType(Convert.ToInt32(ddlDepartmentName.SelectedValue));
                ViewState["action"] = "add";
            }
        }

         divMsg.InnerHtml = string.Empty;
          //show report in new window       
       //  objCommon.ReportPopUp(btnReport, "pagetitle=PRM(Item Master Report)&path=~" + "," + "Reports" + "," + "REPAIR AND MAINTENANCE" + "," + "rptitemmaster_stock.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString(), "PRM");
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=RecieptTypeDefinition.aspx");
        }
    }
        
    private void ShowReportITEM(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
       // pnlAdd.Visible = true;        
        Clear();
       // pnlList.Visible = false;
        txtCurrentStock.Enabled = true;
        ViewState["action"] = "add";        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //pnlAdd.Visible = false;
        //pnlList.Visible = true;
        ViewState["action"] = null;
        Clear();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
       // pnlAdd.Visible = false;
      //  pnlList.Visible = true;
        ViewState["action"] = null;
        Clear();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int itemid = int.Parse(btnEdit.CommandArgument);

            ShowDetail(itemid);

            ViewState["action"] = "edit";
          //  pnlAdd.Visible = true;
          //  pnlList.Visible = false;
            txtCurrentStock.Enabled = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int itemid)
    {
        try
        {
            ComplaintController objCIC = new ComplaintController();
            SqlDataReader dr = objCIC.GetSingleComplaintItem(itemid);
            
            if (dr != null)
            {
                if (dr.Read())
                {
                    ViewState["itemid"] = itemid.ToString();
                    ddlDepartmentName.Text = dr["DEPTID"] == null ? string.Empty : dr["DEPTID"].ToString();
                    txtItemCode.Text = dr["ITEMCODE"] == null ? string.Empty : dr["ITEMCODE"].ToString();
                    txtItemName.Text = dr["ITEMNAME"] == null ? string.Empty : dr["ITEMNAME"].ToString();
                    ddlItemUnit.Text = dr["ITEMUNIT"] == null ? string.Empty : dr["ITEMUNIT"].ToString();
                    ////txtMaxStock.Text = dr["MAXSTOCK"] == null ? string.Empty : dr["MAXSTOCK"].ToString();
                    ////txtMinStock.Text = dr["MINSTOCK"] == null ? string.Empty : dr["MINSTOCK"].ToString();
                    ////txtCurrentStock.Text = dr["CURRSTOCK"] == null ? string.Empty : dr["CURRSTOCK"].ToString();
                    ddlItemType.Text = dr["ITEMTYPEID"] == null ? string.Empty : dr["ITEMTYPEID"].ToString();
                }
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void PopulateDropDownList()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("COMPLAINT_DEPARTMENT", "DEPTID", "DEPTNAME","FLAG_SP=1", "DEPTID");
            ddlDepartmentName.DataSource = ds;
            ddlDepartmentName.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlDepartmentName.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlDepartmentName.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewComplaintItem(int deptid)
    {
        try
        {
            ComplaintController objCIC = new ComplaintController();
            DataSet dsCI = objCIC.GetAllComplaintItem(deptid);
            
            if (dsCI.Tables[0].Rows.Count > 0)
            {
                lvComplaintItem.DataSource = dsCI;
                lvComplaintItem.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.BindListViewComplaintType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void Clear()
    {
        ddlItemType.SelectedIndex = 0;
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        ddlItemUnit.SelectedIndex = -1;
        txtMaxStock.Text = string.Empty;
        txtMinStock.Text = string.Empty;
        txtCurrentStock.Text = string.Empty;
        ViewState["action"] = "add";
        ddlDepartmentName.SelectedIndex = 0;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = CheckPurpose();
            ComplaintController objCIC = new ComplaintController();
            Complaint objComplaintItem = new Complaint();
            
            objComplaintItem.Deptid = Convert.ToInt32(ddlDepartmentName.SelectedValue);
            objComplaintItem.TypeId = Convert.ToInt32(ddlItemType.SelectedValue);
            objComplaintItem.ItemCode = txtItemCode.Text.Trim() == string.Empty ? string.Empty : txtItemCode.Text.Trim();
            objComplaintItem.ItemName = txtItemName.Text.Trim() == string.Empty ? string.Empty : txtItemName.Text.Trim();          
            objComplaintItem.ItemUnit = ddlItemUnit.SelectedValue;
            ////if (!txtMaxStock.Text.Trim().Equals(string.Empty)) objComplaintItem.MaxStock = Convert.ToInt32(txtMaxStock.Text.Trim());
            ////if (!txtMinStock.Text.Trim().Equals(string.Empty)) objComplaintItem.MinStock = Convert.ToInt32(txtMinStock.Text.Trim());
            ////if (!txtCurrentStock.Text.Trim().Equals(string.Empty)) objComplaintItem.CurrStock = Convert.ToInt32(txtCurrentStock.Text.Trim());

            //Check whether to add or update
            if (ViewState["action"] != null)
            {
                if (ViewState["action"].ToString().Equals("add"))
                {
                    ////Added on 27-09-2022 by Sonal Banode to check duplicate records
                    if (result == true)
                    {                        
                        MessageBox("Record Already Exist");
                        Clear();
                        return;
                    }
                    //Add item master
                    CustomStatus cs = (CustomStatus)objCIC.AddItemMaster(objComplaintItem);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       // pnlAdd.Visible = false;
                       // pnlList.Visible = true;
                        ViewState["action"] = null;
                        // added  by Sonal Banode on 27-09-2022 to show data of saved records
                        BindListViewComplaintItem(Convert.ToInt32(ddlDepartmentName.Text));
                        Clear();
                        MessageBox("Record Saved Successfully");
                    }
                }
                else
                {
                    //Edit
                    if (ViewState["itemid"] != null)
                    {
                        objComplaintItem.ItemId = Convert.ToInt32(ViewState["itemid"].ToString());

                        CustomStatus cs = (CustomStatus)objCIC.UpdateItemMaster(objComplaintItem);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                          // pnlAdd.Visible = false;
                         //  pnlList.Visible = true;
                           ViewState["action"] = null;
                           // added  by Sonal Banode on 27-09-2022 to show data of saved records
                           BindListViewComplaintItem(Convert.ToInt32(ddlDepartmentName.Text));
                           Clear();
                           MessageBox("Record Updated Successfully");
                       }
                    }
                }
            }
            // Commented by Sonal Banode on 27-09-2022 as department value was getting -1 after clear
            //BindListViewComplaintItem(Convert.ToInt32(ddlDepartmentName.Text));
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    
    private void FillComplaintType(int deptid)
    {
        try
        {
            ComplaintController objCTC = new ComplaintController();
            DataSet ds = objCTC.GetComplaintType(deptid);

            ddlItemType.DataSource = ds;
            ddlItemType.DataValueField = ds.Tables[0].Columns["TYPEID"].ToString();
            ddlItemType.DataTextField = ds.Tables[0].Columns["TYPENAME"].ToString();
            ddlItemType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "complaint_item.FillComplaintType-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpComplaintType_PreRender(object sender, EventArgs e)
    {
        ComplaintController objCC = new ComplaintController();
        BindListViewComplaintItem(objCC.GetDeptName(Convert.ToInt32(Session["userno"].ToString())));
    }

   

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("ItemMasterReport","rptitemmaster_stock.rpt");

    }


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Complaints")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Complaints," + rptFileName;
            url += "&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@P_USERNAME=" + Session["userfullname"].ToString() + "," + "@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel1, this.UpdatePanel1.GetType(), "controlJSScript", sb.ToString(), true);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }



    protected void ddlDepartmentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Added by Sonal Banode on 272-09-2022 to clear dropdown before filling to avoid duplicate entries
            ddlItemType.Items.Clear();
            ddlItemType.Items.Add("Please Select");
            ddlItemType.SelectedItem.Value = "-1";
            //
        FillComplaintType(Convert.ToInt32(ddlDepartmentName.SelectedValue));
        BindListViewComplaintItem(Convert.ToInt32(ddlDepartmentName.Text));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    //Added on 27-09-2022 by Sonal Banode to check duplicate entries
    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("COMPLAINT_ITEMMASTER", "*", "", "ITEMCODE='" + txtItemCode.Text.Trim() + "' AND ITEMNAME='" + txtItemName.Text.Trim() +
           "' AND ITEMTYPEID='" + ddlItemType.SelectedValue + "' AND  ITEMUNIT='" + ddlItemUnit.SelectedValue + "' AND  DEPTID='" + ddlDepartmentName.SelectedValue + "'", "");

        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }

    //Added on 27-09-2022 by Sonal Banode for message popup
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}

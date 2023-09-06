//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYROLL                                                         
// PAGE NAME     : TO CREATE QUARTER MASTERS                                       
// CREATION DATE : 03-MAY-2009                                                     
// CREATED BY    : NIRAJ D. PHALKE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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

public partial class Masters_quarterMas : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "Server UnAvailable");
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
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                // Populate DropDownList
                PopulateDropdown();

                //Bind the ListView with Quarter
                BindListViewQuarter();

                ViewState["action"] = "add";

                //Set Report Parameters
                objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Masters" + "," + "rptQuarter.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=quarterMas.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=quarterMas.aspx");
        }
    }

    private void PopulateDropdown()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("PAYROLL_QUARTER_TYPE", "QRTTYPENO", "QRTTYPE", string.Empty, "QRTTYPENO");
            ddlQrtType.DataSource = ds;
            ddlQrtType.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlQrtType.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlQrtType.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindListViewQuarter()
    {
        try
        {
            Masters objMasters = new Masters();
            DataSet dsQuarter = objMasters.AllQuarters();

            if (dsQuarter.Tables[0].Rows.Count > 0)
            {
                lvQuarter.DataSource = dsQuarter;
                lvQuarter.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.BindListViewQuarter-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlQrtType.SelectedIndex = 0;
        txtQuarter.Text = string.Empty;
        ViewState["action"] = "add";
        lblStatus.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //Add/Update
            if (ViewState["action"] != null)
            {
                Masters objMasters = new Masters();
                bool result = CheckPurpose();


                if (ViewState["action"].ToString().Equals("add"))
                {
                    if (result == true)
                    {
                        //objCommon.DisplayMessage("Record Already Exist", this);
                        MessageBox("Record Already Exist");
                        return;
                    }
                    else
                    {
                        CustomStatus cs = (CustomStatus)objMasters.AddQuarter(txtQuarter.Text, Convert.ToInt32(ddlQrtType.SelectedValue), Session["colcode"].ToString());
                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            BindListViewQuarter();
                            Clear();
                            //lblStatus.Text = "Record Successfully Added.";
                            MessageBox("Record Saved Successfully ");
                        }
                    }
                }

                else
                {
                    //Edit Quarter
                    if (ViewState["qtrno"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objMasters.UpdateQuarter(Convert.ToInt32(ViewState["qtrno"]), txtQuarter.Text, Convert.ToInt32(ddlQrtType.SelectedValue));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewQuarter();
                            Clear();
                            MessageBox("Record Updated Successfully ");
                            //lblStatus.Text = "Record Successfully Updated.";
                        }
                        //else
                        //    lblStatus.Text = "Record already existed.";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            ViewState["qtrno"] = int.Parse(btnEdit.CommandArgument);

            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            Label lblQtrTypeNo = lst.FindControl("lblQtrTypeNo") as Label;
            Label lblQtrName = lst.FindControl("lblQtrName") as Label;

            ddlQrtType.SelectedValue = lblQtrTypeNo.Text;
            txtQuarter.Text = lblQtrName.Text;
            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        BindListViewQuarter();
    }

    public bool CheckPurpose()
    {
        bool result = false;
        DataSet dsPURPOSE = new DataSet();

        dsPURPOSE = objCommon.FillDropDown("PAYROLL_QUARTERMAS", "*", "QRTTYPENO=" + ddlQrtType.SelectedValue + "", "QTRNAME='" + txtQuarter.Text + "'", "");
        if (dsPURPOSE.Tables[0].Rows.Count > 0)
        {
            result = true;

        }
        return result;
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }

}

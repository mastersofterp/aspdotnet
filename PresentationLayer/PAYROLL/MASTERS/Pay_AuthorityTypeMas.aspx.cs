

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

using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;

public partial class PAYROLL_MASTERS_Pay_AuthorityTypeMas : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    PayController objPay = new PayController();
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
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


                //Bind the ListView with Quarter
                BindListViewAuhtoType();

                ViewState["action"] = "add";

                //Set Report Parameters
                //objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&path=~" + "," + "Reports" + "," + "Masters" + "," + "rptQuarter.rpt&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString(), "UAIMS");
            }
        }
    }



    private void BindListViewAuhtoType()
    {
        try
        {
            Masters objMasters = new Masters();
            DataSet dsQuarter = objPay.AllAuthorityType();

            if (dsQuarter.Tables[0].Rows.Count > 0)
            {
                lvAutho.DataSource = dsQuarter;
                lvAutho.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_quarterMas.BindListViewAuhtoType-> " + ex.Message + " " + ex.StackTrace);
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

        txtAuthoryName.Text = string.Empty;
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

                if (ViewState["action"].ToString().Equals("add"))
                {
                    CustomStatus cs = (CustomStatus)objPay.AddAuthorityType(txtAuthoryName.Text);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListViewAuhtoType();
                        Clear();
                        lblStatus.Text = "Record Successfully Added";
                    }
                    else
                        lblStatus.Text = "Error";
                }
                else
                {
                    //Edit Quarter
                    if (ViewState["Authno"] != null)
                    {
                        CustomStatus cs = (CustomStatus)objPay.UpdateAuthorityType(Convert.ToInt32(ViewState["Authno"]), txtAuthoryName.Text);
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            BindListViewAuhtoType();
                            Clear();
                            lblStatus.Text = "Record Successfully Updated";
                        }
                        else
                            lblStatus.Text = "Error";
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
            //int qtrno = int.Parse(btnEdit.CommandArgument);
            ViewState["Authno"] = int.Parse(btnEdit.CommandArgument);

            ListViewDataItem lst = btnEdit.NamingContainer as ListViewDataItem;
            //Label lblQtrTypeNo = lst.FindControl("lblQtrTypeNo") as Label;
            Label lblQtrName = lst.FindControl("lblQtrName") as Label;


            txtAuthoryName.Text = lblQtrName.Text;
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
        BindListViewAuhtoType();
    }
}
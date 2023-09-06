using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class ESTABLISHMENT_LEAVES_Transactions_Comp_Off_Leave_List : System.Web.UI.Page
{
        Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LeavesController objLeave = new LeavesController();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To set Master Page
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");

    }

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
                Page.Title = Session["coll_name"].ToString();
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
                
                ShowDetails();
                CheckPageAuthorization();

            }
            //
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave_List.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Comp_Off_Leave_List.aspx");
        }
    }
    private void ShowDetails()
    {
        try
        {
            DataSet  ds = objLeave.GetCompoffEmployeeList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lvEmpList.Visible = true;
                lvEmpList.DataSource = ds;
                lvEmpList.DataBind();
                btnSave.Visible = true;
            }
            else
            {
                objCommon.DisplayMessage("No Employee for Comp-Off Leave...", this.Page);
                lvEmpList.DataSource = null;
                lvEmpList.DataBind();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Comp_Off_Leave_List.ShowDetails ->" + ex.Message + "  " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
   
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cs = 0;
        string r_no = string.Empty;

        foreach (ListViewDataItem item in lvEmpList.Items)
        {
            CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
            if (chkSelect.Checked)
            {
                //if (id_no.Length > 0)
                //    id_no += "$";
                r_no += chkSelect.ToolTip+"$";
            }
        }
        //id_no = id_no.Remove(id_no.Length - 1);
        if (r_no == "" || r_no == string.Empty)
        {
            objCommon.DisplayMessage("Please Select Atleast One Employee...", this);
            return;
        }
        int Year =Convert.ToInt32(DateTime.Now.Year.ToString());
        cs = Convert.ToInt32(objLeave.UpdateCompOffLeaves(r_no, Year));
        //if (cs.Equals(CustomStatus.RecordUpdated))
        //{
        //    objCommon.DisplayMessage("Compansatory Leave Credited Successfully...", this.Page);
        //    lvEmpList.DataSource = null;
        //    lvEmpList.DataBind();
        //    ViewState["action"] = null;
        //}
        if (cs == 2)
        {
            objCommon.DisplayMessage("Compansatory Leave Credited Successfully...", this.Page);
            ShowDetails();
           //ddldept.SelectedIndex = 0;
            ViewState["action"] = null;
            //btnShow.Visible = true;
           // btnSave.Visible = false;
            
        }
          
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        foreach (ListViewDataItem items in lvEmpList.Items)
        {
            //cbAl
            CheckBox chkSelect = items.FindControl("chkSelect") as CheckBox;
          
            if (chkSelect.Checked)
            {
                chkSelect.Checked = false;
               
            }

        }
    }   
    protected void lvEmpList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
      // btnShow.Visible = true;
        btnSave.Visible = false;
        lvEmpList.DataSource = null;
        lvEmpList.DataBind();
    }
}

using System;
using System.Collections;
using System.Configuration;
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

public partial class ESTABLISHMENT_Configuration_ServiceBookLink : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    EMP_Attandance_Controller objAttandance = new EMP_Attandance_Controller();
    Shifts objShift = new Shifts();

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

            //Check Session
            if (Session["userno"] == null || Session["username"] == null || Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                Page.Title = Session["coll_name"].ToString();
                objCommon.FillDropDownList(ddlUserType, "USER_RIGHTS", "USERTYPEID", "USERDESC", "USERTYPEID in(1,3,4,5,6,8)", "USERTYPEID");
                //ddlUserType_SelectedIndexChanged(sender, e);
            }

        }
       
    }


    protected void BindListViewStatus()
    {
        DataSet ds = null;
        ds = objAttandance.BindMenulist(Convert.ToInt32(ddlUserType.SelectedValue));
        // ds = objAttandance.GetStatus(Convert.ToInt32(ddlstaff.SelectedValue));
        lvStatus.DataSource = ds;
        lvStatus.DataBind();

    }
    protected void lvStatus_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            CheckBox chkActive = (CheckBox)e.Item.FindControl("chkActive");
            CheckBox chkPer = (CheckBox)e.Item.FindControl("chkPer");

            System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

            if (rowView["ACTIVE"].ToString() == "Y")
            {
                chkActive.Checked = true;
               
            }
            else
            {
                chkActive.Checked = false;
               
            }
            if (rowView["CALFORPER"].ToString() == "Y")
            {
                chkPer.Checked = true;

            }
            else
            {
                chkPer.Checked = false;

            }
           
        }
    }
    protected void ddlUserType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlUserType.SelectedIndex > 0)
        {
            pnlStatus.Visible = true;

            lvStatus.DataSource = null;
            lvStatus.DataBind();
            if (Convert.ToInt32(ddlUserType.SelectedValue) > 0)
            {
                BindListViewStatus();
            }
        }
        else
        {
            pnlStatus.Visible = false;

            lvStatus.DataSource = null;
        }

    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {

            if (Convert.ToInt32(lvStatus.Items.Count) > 0)
            {
                
                DataTable dt = new DataTable();
                dt.Columns.Add("MenuId");
                dt.Columns.Add("isActive");
                dt.Columns.Add("IsCalforPer");

                foreach (ListViewDataItem lvitem in lvStatus.Items)
                {
                    Label lblMenuid = lvitem.FindControl("lblMenuid") as Label;
                    CheckBox chkActive = lvitem.FindControl("chkActive") as CheckBox;
                    CheckBox chkPer = lvitem.FindControl("chkPer") as CheckBox;

                    DataRow dr = dt.NewRow();

                    dr["MenuId"] = lblMenuid.ToolTip;
                    if (chkActive.Checked == true)
                    {
                        dr["isActive"] = true;
                    }
                    else
                    {
                        dr["isActive"] = false;
                    }
                    if (chkPer.Checked == true)
                    {
                        dr["IsCalforPer"] = true;
                    }
                    else
                    {
                        dr["IsCalforPer"] = false;
                    }
                   

                    dt.Rows.Add(dr);
                }

                CustomStatus cs1 = (CustomStatus)objAttandance.insertMenuData(Convert.ToInt32(ddlUserType.SelectedValue),dt);
                lvStatus.DataSource = null;
                lvStatus.DataBind();
                MessageBox("Record Update Successfully !");
                BindListViewStatus();

                return;


            }
            else
            {
                MessageBox("Select user type !");
                return;
            }
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        ddlUserType.SelectedValue = "0";
        lvStatus.DataSource = null;
        //lvStatus.DataBind();
        pnlStatus.Visible = false;
    }
}
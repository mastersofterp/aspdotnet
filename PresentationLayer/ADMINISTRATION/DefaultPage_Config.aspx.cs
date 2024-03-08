using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using IITMS;
using IITMS.UAIMS;
using System.Data.Linq.Mapping;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using BusinessLogicLayer.BusinessEntities;
using BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using mastersofterp_MAKAUAT;
using BusinessLogicLayer.BusinessLogic.Administration;
using System.Data;

/*                            
---------------------------------------------------------------------------------------------------------------------------                            
Created By  : Isha Kanojiya                     
Created On  :  02-03-2024                  
Purpose     : To update default congfiguration param value                          
Version     : 1.0.0                        
---------------------------------------------------------------------------------------------------------------------------                            
Version  Modified On   Modified By      Purpose                            
---------------------------------------------------------------------------------------------------------------------------     
--------------------------------------------------------------------------------------------------------------------------                                               
*/

public partial class ADMINISTRATION_DefaultPage_Config : System.Web.UI.Page
{
    Common objCommon = new Common();
    ParameterListController objPLC = new ParameterListController();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["userno"] == null && Session["username"] == null &&
              Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            GetParameterList();
        }
    }

    protected void GetParameterList()
    {
        try
        {
            ds = objCommon.FillDropDown("ACD_DEFAULT_PAGE_CONFIGURATION", "CONFIGID, PARAM_NAME", "PARAM_VALUE, PARAM_DESCRIPTION", "", "");
            lvConfig.DataSource = ds;
            lvConfig.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.GetParameterList --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = CreateTable_ParamList();
            int count = 0;

            foreach (ListViewItem lvItems in lvConfig.Items)
            {
                DataRow dRow = dt.NewRow();

                HiddenField hdnparamid = lvItems.FindControl("hdnconfigid") as HiddenField;

                TextBox txtparamval = lvItems.FindControl("txtparamvalue") as TextBox;
                CheckBox chkparam = lvItems.FindControl("chkparam") as CheckBox;

                if (chkparam.Checked)
                {
                    if (string.IsNullOrEmpty(txtparamval.Text))
                    {
                        Label lblparamname = lvItems.FindControl("lblparamname") as Label;
                        objCommon.DisplayMessage(this.Page, "Please enter param value for " + lblparamname.Text, Page);
                        return;
                    }

                    dRow["CONFIGID"] = Convert.ToInt32(hdnparamid.Value);
                    dRow["PARAM_VALUE"] = txtparamval.Text;
                    dt.Rows.Add(dRow);
                }
                else
                {
                    count++;
                }
            }

            if (count == lvConfig.Items.Count)
            {
                objCommon.DisplayMessage(this.Page, "Please select at least one record!", Page);
                return;
            }

            string ParamList;
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(dt.Copy());
                ParamList = ds.GetXml();
            }

            int userno = Convert.ToInt32(Session["userno"]);
            int status = objPLC.AddParameter(userno, ParamList);

            if (status == 1)
            {
                objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", Page);
                GetParameterList();
            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Error", Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.btnsubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    public DataTable CreateTable_ParamList()
    {
        DataTable dtParamList = new DataTable();
        dtParamList.Columns.Add("CONFIGID", typeof(int));
        dtParamList.Columns.Add("PARAM_VALUE", typeof(string));
        dtParamList.Columns.Add("UA_NO", typeof(int));
        return dtParamList;
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (var lvItems in lvConfig.Items)
            {
                HiddenField hdnparamid = lvItems.FindControl("hdnconfigid") as HiddenField;
                TextBox txtparamval = lvItems.FindControl("txtparamvalue") as TextBox;
                CheckBox chkparam = lvItems.FindControl("chkparam") as CheckBox;

                if (chkparam.Checked)
                {
                    hdnparamid.Value = null;
                    txtparamval.Text = "";
                    GetParameterList();
                    txtparamval.Enabled = false;
                    chkparam.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.btnclear_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void chkparam_CheckedChanged(object sender, System.EventArgs e)
    {
        try
        {
            foreach (ListViewItem item in lvConfig.Items)
            {
                var chk = (CheckBox)item.FindControl("chkparam");
                var txtvalue = (TextBox)item.FindControl("txtparamvalue");

                if (chk.Checked)
                {
                    txtvalue.Enabled = true;
                }
                else
                {
                    txtvalue.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.chkparam_CheckedChanged --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnConnect_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("reff", "DEV_PASS", "", "", "");
            string pass = ds.Tables[0].Rows[0]["DEV_PASS"].ToString();
            string db_pwd = clsTripleLvlEncyrpt.DecryptPassword(pass);

            if (txtPass.Text.Trim() == db_pwd)
            {
                popup.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
            }
            else
            {
                objCommon.DisplayMessage("Password does not match!", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.btnConnect_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "1")
            {
                Response.Redirect("~/principalHome.aspx", false);
            }
            else if (Session["usertype"].ToString() == "2" || Session["usertype"].ToString() == "14")
            {
                Response.Redirect("~/studeHome.aspx", false);
            }
            else if (Session["usertype"].ToString() == "3")
            {
                Response.Redirect("~/homeFaculty.aspx", false);
            }
            else if (Session["usertype"].ToString() == "5")
            {
                Response.Redirect("~/homeNonFaculty.aspx", false);
            }
            else
            {
                Response.Redirect("~/home.aspx", false);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DefaultPage_Config.btnCancel_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}

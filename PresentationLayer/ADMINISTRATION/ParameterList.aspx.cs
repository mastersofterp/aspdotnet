﻿using System;
using IITMS;
using IITMS.UAIMS;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using BusinessLogicLayer.BusinessEntities;
using BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using mastersofterp_MAKAUAT;
using BusinessLogicLayer.BusinessLogic.Administration;
using BusinessLogicLayer.BusinessLogic.Administration;

/*                            
---------------------------------------------------------------------------------------------------------------------------                            
Created By : RUTUAJA DAWLE                     
Created On :  25-10-2023                   
Purpose :                         
Version :                         
---------------------------------------------------------------------------------------------------------------------------                            
Version  Modified On   Modified By      Purpose                            
---------------------------------------------------------------------------------------------------------------------------     
--------------------------------------------------------------------------------------------------------------------------                                               
*/       
public partial class ADMINISTRATION_ParameterList : System.Web.UI.Page
{
    Common objCommon = new Common();
    ParameterListController objPLC = new ParameterListController();
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetParameterList();
        }
    }

    private DataSet GetParameterList()
    {
        DataSet ds = objCommon.FillDropDown("ACD_PARAMETER", "PARAMID, PARAM_NAME", "PARAM_VALUE, PARAM_DESCRIPTION", "", "");
        lvGetParam.DataSource = ds;
        lvGetParam.DataBind();
        return ds;  
    }

    public DataTable CreateTable_ParamList()
    {
        DataTable dtParamList = new DataTable();
        dtParamList.Columns.Add("PARAMID", typeof(int));
        dtParamList.Columns.Add("PARAM_VALUE", typeof(string));
        dtParamList.Columns.Add("UA_NO", typeof(int));
        return dtParamList;
    }

    protected void chkparam_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {
            foreach (ListViewItem item in lvGetParam.Items)
            {
                var chk = (CheckBox)item.FindControl("chkparam");
                var txtval = (TextBox)item.FindControl("txtparamval");
                if (chk.Checked)
                {
                    txtval.Enabled = true;
                }
                else
                {
                    txtval.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ParameterList.chkparam_CheckedChanged1 --> " + ex.Message + " " + ex.StackTrace);
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

            foreach (ListViewItem lvItems in lvGetParam.Items)
            {
                DataRow dRow = dt.NewRow();

                HiddenField hdnparamid = lvItems.FindControl("hdnparamid") as HiddenField;

                TextBox txtparamval = lvItems.FindControl("txtparamval") as TextBox;
                CheckBox chkparam = lvItems.FindControl("chkparam") as CheckBox;

                if (chkparam.Checked)
                {
                    if (string.IsNullOrEmpty(txtparamval.Text))
                    {

                        Label lblparamname = lvItems.FindControl("lblparamname") as Label;
                        objCommon.DisplayMessage(this.Page, "Please enter param value for " + lblparamname.Text, Page);
                        return;
                    }

                    dRow["PARAMID"] = Convert.ToInt32(hdnparamid.Value);
                    dRow["PARAM_VALUE"] = txtparamval.Text;
                    dRow["UA_NO"] = Convert.ToInt32(Session["userno"]);
                    dt.Rows.Add(dRow);
                }
                else
                {
                    count++;
                }
            }

            if (count == lvGetParam.Items.Count)
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
            int status = objPLC.AddParam(userno, ParamList);

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
                objCommon.ShowError(Page, "ParameterList.btnsubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (var lvItems in lvGetParam.Items)
            {
                HiddenField hdnparamid = lvItems.FindControl("hdnparamid") as HiddenField;
                TextBox txtparamval = lvItems.FindControl("txtparamval") as TextBox;
                CheckBox chkparam = lvItems.FindControl("chkparam") as CheckBox;
                var txtparamlist = txtparamval;
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
                objCommon.ShowError(Page, "ParameterList.btnclear_Click --> " + ex.Message + " " + ex.StackTrace);
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
                objCommon.DisplayMessage("Password does not match!", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ParameterList.btnConnect_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
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
                objCommon.ShowError(Page, "ParameterList.btnCancel_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
      

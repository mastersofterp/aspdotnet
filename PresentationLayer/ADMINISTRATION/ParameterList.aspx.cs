using System;
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
        dtParamList.Columns.Add("PARAM_VALUE", typeof(int));
        dtParamList.Columns.Add("UA_NO", typeof(int));
        return dtParamList;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = CreateTable_ParamList();
        int rowIndex = 0;
        int count = 0;
        foreach (var lvItems in lvGetParam.Items)
        {
            DataRow dRow = dt.NewRow();

            HiddenField hdnparamid = lvItems.FindControl("hdnparamid") as HiddenField;
            TextBox txtparamval = lvItems.FindControl("txtparamval") as TextBox;
            CheckBox chkparam = lvItems.FindControl("chkparam") as CheckBox;
            if (txtparamval.Text == "" )
            {
                objCommon.DisplayMessage(this.Page, "Please enter the number in textbox  ", Page);
            }
                 
            else
            {
                if (chkparam.Checked == true)
                {
                    dRow["PARAMID"] = hdnparamid.Value;

                    dRow["PARAM_VALUE"] = txtparamval.Text;
                    dRow["UA_NO"] = Session["userno"].ToString();
                    dt.Rows.Add(dRow);
                    rowIndex = rowIndex + 1;
                }
                else if (chkparam.Checked == false)
                {
                    count++;
                }
               
            }
        }
        if (count == lvGetParam.Items.Count)
        {
            objCommon.DisplayMessage(this.Page, "Please Select atleast One Record!", Page);
            GetParameterList();
            return;
        }
        ds.Tables.Add(dt);
        string ParamList = ds.GetXml();
        int userno = Convert.ToInt32(Session["userno"].ToString());
        int status = Convert.ToInt32(AddParams(userno, ParamList));
        if (status == 1)
        {
            objCommon.DisplayMessage(this.Page, "Record Updated Successfully!", Page);
            GetParameterList();
        }
        else
        {
            objCommon.DisplayMessage(this.Page, "Error", Page);
            return;
        }
    }

    protected void chkparam_CheckedChanged1(object sender, EventArgs e)
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

    public string AddParams(int UserNo, string XmlData)
    {
        string retStatus = string.Empty;

        try
        {
            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            SqlParameter[] objParams = null;
            objParams = new SqlParameter[3];

            objParams[0] = new SqlParameter("@P_UANO", UserNo);
            objParams[1] = new SqlParameter("@P_XML", XmlData);
            objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
            objParams[2].Direction = ParameterDirection.Output;

            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMI_PARAMETERLIST_UPDATE", objParams, true);
            retStatus = ret.ToString();

            return retStatus;
        }
        catch (Exception ex)
        {
            throw new IITMSException("ADMINISTRATION_ParameterList->" + ex.ToString());
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
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

    protected void btnConnect_Click(object sender, EventArgs e)
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


    protected void btnCancel_Click(object sender, EventArgs e)
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
}
      

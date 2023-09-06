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
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using DynamicAL_v2;

public partial class CreateTemplate : System.Web.UI.Page
{
    Common objCommon = new Common();
    //UAIMS_Common objUCommon = new UAIMS_Common();
    ExamController objExamController = new ExamController();
    DynamicControllerAL AL = new DynamicControllerAL();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
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
                //This checks the authorization of the user.
            //    CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                //if (Request.QueryString["pageno"] != null)
                //{
                //    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                //}

                // Set form mode equals to -1(New Mode).
                ViewState["exdtno"] = "0";

                
                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
            }

            LoadDropDowns();
            GetTemplate();
        }
    }

    void LoadDropDowns()
    {
        objCommon.FillDropDownList(ddlCategoty, "ACD_MAIL_CATEGORIES", "ID", "CATEGORY_NAME", "", "CATEGORY_NAME");
    }

    [WebMethod]
    public static string UserType()
    {
        CreateTemplate ct = new CreateTemplate();
        DataSet ds = ct.objCommon.FillDropDown("User_Rights", "USERTYPEID", "USERDESC", "", "");
        DynamicControllerAL AL = new DynamicControllerAL();
        return AL.Dt2Json(ds.Tables[0]);
    }

    [WebMethod]
    public static string FillModules()
    {
        CreateTemplate ct = new CreateTemplate();
        DataSet ds = ct.objCommon.FillDropDown("dbo.ACCESS_LINK al INNER JOIN dbo.Acc_Section sc ON (sc.AS_No = al.AL_ASNo)", "al.AL_Link", "sc.AS_Title, al.AL_No", "", "sc.AS_No,al.AL_Link");
        DynamicControllerAL AL = new DynamicControllerAL();
        return AL.Dt2Json(ds.Tables[0]);
    }

    [WebMethod]
    public static string DataList(string val)
    {
        CreateTemplate ct = new CreateTemplate();
        DataSet ds = ct.objCommon.FillDropDown("ACD_MAIL_DATA_CATEGORY", "ID", "NAME, SP_NAME", "CATEGORY_ID=" + Convert.ToInt32(val) + " AND ISNULL(isDELETED,0)=0", "NAME");
        DynamicControllerAL AL = new DynamicControllerAL();
        return AL.Dt2Json(ds.Tables[0]);
    }

    [WebMethod]
    public static string CategoryType(string val)
    {
        DynamicControllerAL AL = new DynamicControllerAL();
        string SP = "", PR = "", VL = "";
        SP = "GET_SP_PARAMETERS_IF_EXISTS";
        PR = "@P_SP_NAME";
        VL = "" + val + "";
        DataSet ds = AL.DynamicSPCall_Select(SP, PR, VL);

        if (ds.Tables[0].Columns.Count == 1)
        {
            //"Stored Procedure not found", "Dashboard", AnimtedMsgBox.Buttons.OK, AnimtedMsgBox.Icon.Shield, AnimtedMsgBox.AnimateStyle.FadeIn);
            //return;
        }
        else if (ds.Tables[0].Rows.Count == 0)
        {
            PR = VL = "-1";
            SP = val;
        }
        else if (ds.Tables[0].Rows.Count != 0)
        {
            SP = PR = VL = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                PR += ds.Tables[0].Rows[i][0] + ",";
                VL += "0,";
            }
            PR = PR.Remove(PR.Length - 1, 1);
            VL = VL.Remove(VL.Length - 1, 1);
            SP = val;
        }

        DataSet dsX = AL.DynamicSPCall_Select(SP, PR, VL);
        DataTable dt = new DataTable();
        dt.Columns.Add("NAME");
        for (int i = 0; i < dsX.Tables[0].Columns.Count; i++)
        {
            dt.Rows.Add(dsX.Tables[0].Columns[i].ColumnName);
        }
       
        return AL.Dt2Json(dt);
    }

    private void GetTemplate()
    {
        string SP = "PKG_CRUD_MAIL_TEMPLATE";
        string PR = "@P_NAME, @P_SUBJECT, @P_USERS, @P_PAGES, @P_TEMPLATE, @P_CATEGORY, @P_STATUS, @P_DATA_LIST, @P_MAIL_TYPE, @P_OPERATION";
        string VL = "0,0,0,0,0,0,0,0,0,2";

        DataSet ds = AL.DynamicSPCall_Select(SP, PR, VL);

        rptTemplate.DataSource = ds;
        rptTemplate.DataBind();
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string SP = "PKG_CRUD_MAIL_TEMPLATE";
            string PR = "@P_NAME, @P_SUBJECT, @P_USERS, @P_PAGES, @P_TEMPLATE, @P_CATEGORY, @P_STATUS, @P_DATA_LIST, @P_MAIL_TYPE, @P_OPERATION";
            string VL = "" + Convert.ToString(txtTempleteName.Text).Replace(",", "^") + "," + Convert.ToString(txtSubject.Text).Replace(",", "^") + "," + Convert.ToString(hfdUserType.Value).Replace(",", "^") + "," + Convert.ToString(hfdPage.Value).Replace(",", "^") + "," + Convert.ToString(hfdTemplate.Value).Replace(",", "^") + "," + Convert.ToInt32(hfdCategoryType.Value) + "," + Convert.ToInt32(hfdStatus.Value) + "," + Convert.ToInt32(hfdDataList.Value) + "," + Convert.ToInt32(hfdMailType.Value) + "," + Convert.ToInt32(hfdOperation.Value) + "";

            string retVal = AL.DynamicSPCall_IUD(SP, PR, VL, true, 2);

            if (retVal == "-1")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template with same name is already exists. Try another Template Name.');", true);
            }
            else if (retVal == "1")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template created successfully.');", true);
                ClearControls();
                GetTemplate();
            }
            else if (retVal == "3")
            {
                ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template Updated successfully.');", true);
                hfdOperation.Value = "1";
                ClearControls();
                GetTemplate();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage("Server UnAvailable", this.Page);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        btnSubmit.Text = "Submit";
    }

    private void ClearControls()
    {
        txtTempleteName.Text = txtSubject.Text = string.Empty;
        ddlCategoty.SelectedIndex = 0;
        hfdOperation.Value = "1";
    }
    
    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Ibtn = sender as ImageButton;
        string TemplateName = Convert.ToString(Ibtn.CommandArgument).Split('$')[0];

        string SP = "PKG_CRUD_MAIL_TEMPLATE";
        string PR = "@P_NAME, @P_SUBJECT, @P_USERS, @P_PAGES, @P_TEMPLATE, @P_CATEGORY, @P_STATUS, @P_DATA_LIST, @P_MAIL_TYPE, @P_OPERATION";
        string VL = "" + TemplateName + ",0,0,0,0,0,0,0,0,4";

        string retVal = AL.DynamicSPCall_IUD(SP, PR, VL, true, 2);

        if (retVal == "4")
        {
            ScriptManager.RegisterStartupScript(updTemplate, updTemplate.GetType(), "Script", "alert('Template Deleted Successfully.');", true);
        }

        ClearControls();
        GetTemplate();
    }
}

using DynamicAL_v2;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mastersofterp_MAKAUAT;

public partial class OBE_OBE_Configue : System.Web.UI.Page
{
    ExamQuestionPatternController ObjQP = new ExamQuestionPatternController();
    CommonModel ObjComModel = new CommonModel();
    Common objCommon = new Common();
    DynamicControllerAL AL = new DynamicControllerAL();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=OBE_Configue.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=OBE_Configue.aspx");
        }
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
                //Page Authorization
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

            }
            //ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
            //string IpAddress = ViewState["ipAddress"].ToString();
            //int userno = 0;
            //userno = Convert.ToInt32(Session["userno"]);
        }


    }
    protected void btnCourse_Click(object sender, EventArgs e)
    {
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        string IpAddress = ViewState["ipAddress"].ToString();
        int userno = 0;
        userno = Convert.ToInt32(Session["userno"]);
        if (txtconformmessageValue.Value == "Yes")
        {
            string SP = "PKG_RF_OBE_INTEGRATION_COURSE_MIGRATION_UTILITY";//PKG_RF_OBE //PKG_RF_OBE_INTEGRATION_COURSE_MIGRATION_UTILITY
            string PR = "@P_UA_NO,@P_IPADDRESS,@P_OUT";
            string VL = "" + userno + "," + IpAddress + ",0";
            int result = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

            if (result == 1)
            {
                objCommon.DisplayMessage(updConfig, " Course Migration Successfully done.", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updConfig, " Something went wrong..Error occurred ", this.Page);

            }
        }
        else 
        {
        }

    }
    protected void btnExam_Click(object sender, EventArgs e)
    {
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        string IpAddress = ViewState["ipAddress"].ToString();
        int userno = 0;
        userno = Convert.ToInt32(Session["userno"]);
        if (txtconformmessageValue.Value == "Yes")
        {
            string SP = "PKG_RF_OBE_INTEGRATION_EXAM_MIGRATION_UTILITY";//PKG_RF_OBE_INTEGRATION_EXAM_MIGRATION_UTILITY
            string PR = "@P_UA_NO,@P_IPADDRESS,@P_OUT";
            string VL = "" + userno + "," + IpAddress + ",0";
            int result = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

            if (result == 1)
            {
                objCommon.DisplayMessage(updConfig, " Exam Migration Successfully done.", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updConfig, " Something went wrong..Error occurred ", this.Page);

            }
        }
        else
        {
        }

    }
    protected void btnUser_Click(object sender, EventArgs e)
    {
        ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
        string IpAddress = ViewState["ipAddress"].ToString();
        int userno = 0;
        userno = Convert.ToInt32(Session["userno"]);
        if (txtconformmessageValue.Value == "Yes")
        {
            string SP = "PKG_RF_OBE_INTEGRATION_USER_MIGRATION_UTILITY";
            string PR = "@P_UA_NO,@P_IPADDRESS,@P_OUT";
            string VL = "" + userno + "," + IpAddress + ",0";
            //VL = "" + Convert.ToInt32(hdExamPatternSubId.Value) + ", " + Convert.ToString(txtQuestionNo.Text) + ", " + Convert.ToInt32(ddlQueLevel.SelectedValue) + "," + Convert.ToInt32(txtOutOfQuestion.Text) + ", " + Convert.ToInt32(txtAttemptMinimum.Text) + ", " + Convert.ToInt32(hdPatternId.Value) + ", " + Convert.ToString(txtQuestionMarks.Text) + ", " + Convert.ToInt32(ddlQueOrWith.SelectedValue) + ", " + Convert.ToString(descript) + ", " + Convert.ToInt32(ddlParentQuestion.SelectedValue) + "," + Convert.ToString(MarkEntry) + ",0,0";
            int result = Convert.ToInt32(AL.DynamicSPCall_IUD(SP, PR, VL, true, 1));

            if (result == 1)
            {
                objCommon.DisplayMessage(updConfig, " User Migration Successfully done.", this.Page);

            }
            else
            {
                objCommon.DisplayMessage(updConfig, " Something went wrong..Error occurred ", this.Page);

            }
        }
        else
        {
        }

    }
    #region Need Password before access the Page.
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
    protected void btnCancel1_Click(object sender, EventArgs e)
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
    #endregion
}
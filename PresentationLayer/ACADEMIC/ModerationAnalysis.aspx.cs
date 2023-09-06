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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic.BusinessLogicLayer.BusinessLogic.Academic;
public partial class ModerationAnalysis : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
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

                CheckPageAuthorization();
                BindDropDown();
                //BindProvisinal();
                //BindFinal();
                Page.Title = Session["coll_name"].ToString();              
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
                Response.Redirect("~/notauthorized.aspx?page=ModerationAnalysis.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ModerationAnalysis.aspx");
        }
    }
    private void BindDropDown()
    {
        string deptnos = (Session["userdeptno"].ToString() == "" || Session["userdeptno"].ToString() == string.Empty) ? "0" : Session["userdeptno"].ToString();

        if (Session["usertype"].ToString() != "1")
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND (DB.DEPTNO IN (" + deptnos + ") OR ('" + deptnos + "')='0')", "");
        }
        else
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING", "COSCHNO", "COL_SCHEME_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), "COLLEGE_ID");
        }
        //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME", "COLLEGE_ID>0", "COLLEGE_ID");

        objCommon.FillDropDownList(ddladmbatch, "ACD_ACADEMICBATCH", "ACADEMICBATCHNO", "ACADEMICBATCH", "ACADEMICBATCHNO>0 AND ISNULL(ACTIVESTATUS,0)=1", "ACADEMICBATCHNO");
        objCommon.FillDropDownList(ddlSemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
        //objCommon.FillDropDownList(ddlDegreeType, "ACD_UA_SECTION", "UA_SECTION", "UA_SECTIONNAME", "UA_SECTION>0 AND ISNULL(ACTIVESTATUS,0)=1", "UA_SECTION");
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ddlCollege.SelectedValue, "SESSIONNO");
        if (ddlCollege.SelectedIndex > 0)
        {
            DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
            {
                ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                ViewState["college_idOVER"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID = " + Convert.ToInt32(ViewState["college_idOVER"]) + " AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "SESSIONNO DESC");
            }
        }
        else
        {
            ddlSession.Items.Clear();
        }
    }
    private void BindProvisinal()
    {
        if (ViewState["college_idOVER"] == null)
        {
            ViewState["college_idOVER"] = 0;
        }
        string SP_Name2 = "PKG_ACD_GET_STUDENT_PROVISIONAL_RESULT";
        string SP_Parameters2 = "@P_ACADEMICBATCH,@P_COLLEGE_ID,@P_SESSIONNO,@P_STUDENT_TYPE,@P_SEMESTERNO";
        string Call_Values2 = "" + Convert.ToInt32(ddladmbatch.SelectedValue.ToString()) + "," + Convert.ToInt32(ViewState["college_idOVER"].ToString()) + "," +
                Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudentType.SelectedValue.ToString()) + ","  +
                Convert.ToInt32(ddlSemester.SelectedValue.ToString());
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            pnlProvosional.Visible = true;
            LvProvisional.DataSource = dsStudList;
            LvProvisional.DataBind();
        }
        else
        {

            objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            pnlProvosional.Visible = false;
            LvProvisional.DataSource = null;
            LvProvisional.DataBind();
            //Clear();
        }
        foreach (ListViewDataItem lvitem in LvProvisional.Items)
        {
            Label lblpassstudent = lvitem.FindControl("lblpassstudent") as Label;
            Label lbltotal = lvitem.FindControl("lbltotal") as Label;
            Label lblpassper = lvitem.FindControl("lblpassper") as Label;

            decimal per = 0.00m;
            per = (Convert.ToDecimal(lblpassstudent.Text) / Convert.ToDecimal(lbltotal.Text)) * 100;
            lblpassper.Text = Convert.ToString(per).Substring(0, 2) + "%";
        }
    }
    private void BindFinal()
    {
        if (ViewState["college_idOVER"] == null)
        {
            ViewState["college_idOVER"] = 0;
        }
        string SP_Name2 = "PKG_ACD_GET_STUDENT_FINAL_RESULT";
        string SP_Parameters2 = "@P_ACADEMICBATCH,@P_COLLEGE_ID,@P_SESSIONNO,@P_STUDENT_TYPE,@P_SEMESTERNO";
        string Call_Values2 = "" + Convert.ToInt32(ddladmbatch.SelectedValue.ToString()) + "," + Convert.ToInt32(ViewState["college_idOVER"].ToString()) + "," +
                Convert.ToInt32(ddlSession.SelectedValue.ToString()) + "," + Convert.ToInt32(ddlStudentType.SelectedValue.ToString())  + "," +
                Convert.ToInt32(ddlSemester.SelectedValue.ToString());
        DataSet dsStudList = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);
        if (dsStudList.Tables[0].Rows.Count > 0)
        {
            LvFinal.DataSource = dsStudList;
            LvFinal.DataBind();
        }
        else
        {
            pnlfinal.Visible = true;
            objCommon.DisplayMessage(this.Page, "Record Not Found", this.Page);
            pnlfinal.Visible = false;
            LvFinal.DataSource = null;
            LvFinal.DataBind();
           //// Clear();
        }
        foreach (ListViewDataItem lvitem in LvFinal.Items)
        {
            Label lblpassstudent = lvitem.FindControl("lblpassstudent") as Label;
            Label lbltotal = lvitem.FindControl("lbltotal") as Label;
            Label lblpassper = lvitem.FindControl("lblpassper") as Label;

            decimal per = 0.00m;
            per = (Convert.ToDecimal(lblpassstudent.Text) / Convert.ToDecimal(lbltotal.Text)) * 100;
            lblpassper.Text = Convert.ToString(per).Substring(0, 2) + "%";
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindProvisinal();
        BindFinal();
    }
    private void Clear()
    {
        ddlSemester.SelectedIndex = 0;
        ddlCollege.SelectedIndex = 0;
       // ddlDegreeType.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        ddlStudentType.SelectedIndex = 0;
        ddladmbatch.SelectedIndex = -1;
        pnlfinal.Visible=false;
        LvFinal.DataSource = null;
        LvFinal.DataBind();
        pnlProvosional.Visible = false;
        LvProvisional.DataSource = null;
        LvProvisional.DataBind();

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
}
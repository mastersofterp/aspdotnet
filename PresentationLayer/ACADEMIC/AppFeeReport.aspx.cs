using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using System.IO;
public partial class ACADEMIC_AppFeeReport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string SP_Name = string.Empty; string SP_Call = string.Empty; string SP_Value = string.Empty;
    int admBatch = 0; int degreeType = 0;
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
        try
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
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //CheckPageAuthorization();
                }
                PopulateDropDown();
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AppFeeReport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AppFeeReport.aspx");
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fromDate;
            DateTime toDate;
            string modeofPay = string.Empty;
            int degreeType = 0;
            int degreeNo = 0;
            DataSet dsShow = null;
            admBatch = ddlAdmBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatch.SelectedValue) : 0;
            fromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
            toDate = Convert.ToDateTime(txtToDate.Text.ToString());
            modeofPay = ddlMode.SelectedIndex > 0 ? ddlMode.SelectedValue : "NA";
            degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
            degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            SP_Name = "PKG_ACD_OA_GET_FEE_PAYMENT_REPORT";
            SP_Call = "@P_ADMBATCH,@P_FROM_DATE,@P_TO_DATE,@P_PAY_MODE,@P_DEGREE_TYPE,@P_DEGREE";
            SP_Value = "" + admBatch+","+fromDate+","+toDate+","+modeofPay+","+degreeType+","+degreeNo+ "";
            dsShow = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsShow.Tables[0].Rows.Count > 0)
            {
                pnlList.Visible = true;
                lvPayList.DataSource = dsShow;
                lvPayList.DataBind();
            }
            else
            {
                pnlList.Visible = false;
                lvPayList.DataSource = null;
                lvPayList.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                return;
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void PopulateDropDown()
    {
        try
        {
            admBatch = 0; degreeType = 0;
            SP_Name = "PKG_ACD_OA_FILL_DROPDOWN_PAYMENT_FEE_REPORT";
            SP_Call = "@P_ADMBATCH,@P_DEGREE_TYPE";
            SP_Value = "" + admBatch+","+degreeType+"";
            DataSet dsBind = null;            
            dsBind=objCommon.DynamicSPCall_Select(SP_Name,SP_Call,SP_Value);
            if (dsBind.Tables[0].Rows.Count > 0)
            {
                ddlAdmBatch.DataSource = dsBind.Tables[0];
                ddlAdmBatch.DataValueField = "ADMBATCH";
                ddlAdmBatch.DataTextField = "BATCHNAME";
                ddlAdmBatch.DataBind();
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlAdmBatch.SelectedIndex > 0)
            //{
                DataSet dsBindMode = null;
                SP_Name = "PKG_ACD_OA_FILL_DROPDOWN_PAYMENT_FEE_REPORT";
                SP_Call = "@P_ADMBATCH,@P_DEGREE_TYPE";
                SP_Value = "" + admBatch + "," + degreeType + "";
                dsBindMode = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
                if (dsBindMode.Tables[1].Rows.Count > 0)
                {
                    ddlMode.Items.Clear();
                    ddlMode.Items.Add(new ListItem("Please Select", "0"));
                    ddlMode.DataSource = dsBindMode.Tables[1];
                    ddlMode.DataValueField = "PAY_MODE_CODE";
                    ddlMode.DataTextField = "MODE";
                    ddlMode.DataBind();
                }
                DataSet dsBindDegreeType = null;
                SP_Name = "PKG_ACD_OA_FILL_DROPDOWN_PAYMENT_FEE_REPORT";
                SP_Call = "@P_ADMBATCH,@P_DEGREE_TYPE";
                SP_Value = "" + admBatch + "," + degreeType + "";
                dsBindDegreeType = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
                if (dsBindDegreeType.Tables[2].Rows.Count > 0)
                {
                    ddlDegreeType.Items.Clear();
                    ddlDegreeType.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegreeType.DataSource = dsBindDegreeType.Tables[2];
                    ddlDegreeType.DataValueField = "UA_SECTION";
                    ddlDegreeType.DataTextField = "UA_SECTIONNAME";
                    ddlDegreeType.DataBind();
                    ddlDegreeType.Items.Add(new ListItem("Lateral", "4"));
                    ddlDegreeType.Items.Add(new ListItem("NRI", "5"));
                }
            //}
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdmBatch.SelectedIndex > 0)
            {
                DataSet dsBindDegreeType = null;
                SP_Name = "PKG_ACD_OA_FILL_DROPDOWN_PAYMENT_FEE_REPORT";
                SP_Call = "@P_ADMBATCH,@P_DEGREE_TYPE";
                SP_Value = "" + admBatch + "," + degreeType + "";
                dsBindDegreeType = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
                if (dsBindDegreeType.Tables[2].Rows.Count > 0)
                {
                    ddlDegreeType.Items.Clear();
                    ddlDegreeType.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegreeType.DataSource = dsBindDegreeType.Tables[2];
                    ddlDegreeType.DataValueField = "UA_SECTION";
                    ddlDegreeType.DataTextField = "UA_SECTIONNAME";
                    ddlDegreeType.DataBind();
                    ddlDegreeType.Items.Add(new ListItem("Lateral", "4"));
                    ddlDegreeType.Items.Add(new ListItem("NRI", "5"));
                }
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void ddlDegreeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlDegreeType.SelectedIndex > 0)
            //{
                DataSet dsBindDegree = null;
                admBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
                degreeType = Convert.ToInt32(ddlDegreeType.SelectedValue);
                SP_Name = "PKG_ACD_OA_FILL_DROPDOWN_PAYMENT_FEE_REPORT";
                SP_Call = "@P_ADMBATCH,@P_DEGREE_TYPE";
                SP_Value = "" + admBatch + "," + degreeType + "";
                dsBindDegree = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
                if (dsBindDegree.Tables[3].Rows.Count > 0)
                {
                    ddlDegree.Items.Clear();
                    ddlDegree.Items.Add(new ListItem("Please Select", "0"));
                    ddlDegree.DataSource = dsBindDegree.Tables[3];
                    ddlDegree.DataValueField = "DEGREENO";
                    ddlDegree.DataTextField = "DEGREENAME";
                    ddlDegree.DataBind();
                }
            //}
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void Clear()
    {
        try
        {
            ddlAdmBatch.Items.Clear();
            ddlAdmBatch.Items.Add(new ListItem ("Please Select","0"));

            ddlMode.Items.Clear();
            ddlMode.Items.Add(new ListItem("Please Select", "0"));

            ddlDegreeType.Items.Clear();
            ddlDegreeType.Items.Add(new ListItem("Please Select", "0"));

            ddlDegree.Items.Clear();
            ddlDegree.Items.Add(new ListItem("Please Select", "0"));

            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            lvPayList.DataSource = null;
            lvPayList.DataBind();
            pnlList.Visible = false;
            PopulateDropDown();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fromDate;
            DateTime toDate;
            string modeofPay = string.Empty;
            int degreeType = 0;
            int degreeNo = 0;
            DataSet dsExcel = null;
            admBatch = ddlAdmBatch.SelectedIndex > 0 ? Convert.ToInt32(ddlAdmBatch.SelectedValue) : 0;
            fromDate = Convert.ToDateTime(txtFromDate.Text.ToString());
            toDate = Convert.ToDateTime(txtToDate.Text.ToString());
            modeofPay = ddlMode.SelectedIndex > 0 ? ddlMode.SelectedValue : "";
            degreeType = ddlDegreeType.SelectedIndex > 0 ? Convert.ToInt32(ddlDegreeType.SelectedValue) : 0;
            degreeNo = ddlDegree.SelectedIndex > 0 ? Convert.ToInt32(ddlDegree.SelectedValue) : 0;
            SP_Name = "PKG_ACD_OA_GET_FEE_PAYMENT_EXCEL";
            SP_Call = "@P_ADMBATCH,@P_FROM_DATE,@P_TO_DATE,@P_PAY_MODE,@P_DEGREE_TYPE,@P_DEGREE";
            SP_Value = "" + admBatch + "," + fromDate + "," + toDate + "," + modeofPay + "," + degreeType + "," + degreeNo + "";
            dsExcel = objCommon.DynamicSPCall_Select(SP_Name, SP_Call, SP_Value);
            if (dsExcel.Tables[0].Rows.Count > 0)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    foreach (System.Data.DataTable dt in dsExcel.Tables)
                    {
                        wb.Worksheets.Add(dt);
                    }
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=ApplicationFeePayment.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                return;
            }
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
}
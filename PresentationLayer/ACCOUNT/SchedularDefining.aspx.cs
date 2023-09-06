//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : SCHEDULER DEFINING
// CREATION DATE : 08-OCTOBER-2009                                                  
// CREATED BY    : JITENDRA CHILATE
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Drawing;
using IITMS.NITPRM;


public partial class SchedularDefining : System.Web.UI.Page
{
    UAIMS_Common objUCommon = new UAIMS_Common();
    Common objCommon = new Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
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
                if (Session["comp_code"] == null || Session["fin_yr"] == null)
                {
                    Session["comp_set"] = "NotSelected";
                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else { Session["comp_set"] = ""; }
                   
                //Page Authorization
                CheckPageAuthorization();

                divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                BindScheduleDefination("Y");
                                              
            }
          
        }

////for testing the image logo

//        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;



//DataTable dtr=new DataTable();
//System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
//string type="string";
//dtr.Columns.Add("CollegeName", type.GetType());
//dtr.Columns.Add("college_logo",img.GetType());


//      SQLHelper objsql = new SQLHelper(_connectionString);
//      SqlDataReader dr=  objsql.ExecuteReader("select CollegeName,college_logo from reff");
//      if (dr.HasRows == true)
//      {
//          while (dr.Read())
//          {
              
//          byte[] buffer = (byte[])dr["college_logo"]; ;

//          MemoryStream ms = new MemoryStream(buffer);
//          System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms, true);

//          returnImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

              

//               //dtr.Load(dr);
//               //type =   dr["college_logo"].ToString();
//          }
          
          
          
          
          
      
//      }
        

////============



      
    }

    protected void BindScheduleDefination(string rep_type)
    {
        string code_year = Session["comp_code"].ToString();// +"_" + Session["fin_yr"].ToString();

        rptSchDef.DataSource = null;
        rptSchDef.DataBind();

        
        ScheduleDefineController objSD = new ScheduleDefineController();
        DataSet dtr = objSD.GetScheduleDefinations(code_year, rep_type);
        DataTable dt=new DataTable();

        if (rep_type == "Y")
        {
            DataColumn dc = new DataColumn();
            dc.ColumnName = "FA_NAME";
            dt.Columns.Add(dc);
            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "MGRP_NAME1";
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "MGRP_NAME";
            dt.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "SCH";
            dt.Columns.Add(dc3);

            DataRow row;
            int i;
            for (i = 0; i < dtr.Tables[0].Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    row = dt.NewRow();
                    row["FA_NAME"] = dtr.Tables[0].Rows[i]["FA_NAME"];
                    row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME1"];
                    row["MGRP_NAME"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                    row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                    dt.Rows.Add(row);
                }
                else if (dtr.Tables[0].Rows[i - 1]["FA_NAME"].ToString().Trim() == dtr.Tables[0].Rows[i]["FA_NAME"].ToString().Trim())
                {
                    if (dtr.Tables[0].Rows[i - 1]["MGRP_NAME1"].ToString().Trim() == dtr.Tables[0].Rows[i]["MGRP_NAME1"].ToString().Trim())
                    {
                        row = dt.NewRow();
                        row["FA_NAME"] = " ";
                        row["MGRP_NAME1"] = " ";
                        row["MGRP_NAME"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);

                    }
                    else
                    {
                        row = dt.NewRow();
                        row["FA_NAME"] = " ";
                        row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME1"];
                        row["MGRP_NAME"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);
                    }

                }
                else if (dtr.Tables[0].Rows[i - 1]["FA_NAME"].ToString().Trim() != dtr.Tables[0].Rows[i]["FA_NAME"].ToString().Trim())
                {

                    if (dtr.Tables[0].Rows[i - 1]["MGRP_NAME1"].ToString().Trim() == dtr.Tables[0].Rows[i]["MGRP_NAME1"].ToString().Trim())
                    {

                        row = dt.NewRow();
                        row["FA_NAME"] = dtr.Tables[0].Rows[i]["FA_NAME"];
                        row["MGRP_NAME1"] = " ";
                        row["MGRP_NAME"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        row = dt.NewRow();
                        row["FA_NAME"] = dtr.Tables[0].Rows[i]["FA_NAME"];
                        row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME1"];
                        row["MGRP_NAME"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);
                    }
                }
            }
        }
        else
        {
            // Summary
            DataColumn dc = new DataColumn();
            dc.ColumnName = "FA_NAME";
            dt.Columns.Add(dc);
            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "MGRP_NAME";
            dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "MGRP_NAME1";
            dt.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "SCH";
            dt.Columns.Add(dc3);

            DataRow row;
            int i;
            for (i = 0; i < dtr.Tables[0].Rows.Count - 1; i++)
            {
                if (i == 0)
                {
                    row = dt.NewRow();
                    row["FA_NAME"] = dtr.Tables[0].Rows[i]["FA_NAME"];
                    row["MGRP_NAME"] = "";
                    row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                    row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                    dt.Rows.Add(row);
                }
                else if (dtr.Tables[0].Rows[i - 1]["FA_NAME"].ToString().Trim() == dtr.Tables[0].Rows[i]["FA_NAME"].ToString().Trim())
                {
                    
                        row = dt.NewRow();
                        row["FA_NAME"] = " ";
                        row["MGRP_NAME"] = "";
                        row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);
                }
                else if (dtr.Tables[0].Rows[i - 1]["FA_NAME"].ToString().Trim() != dtr.Tables[0].Rows[i]["FA_NAME"].ToString().Trim())
                {
                        row = dt.NewRow();
                        row["FA_NAME"] = dtr.Tables[0].Rows[i]["FA_NAME"];
                        row["MGRP_NAME"] = "";
                        row["MGRP_NAME1"] = dtr.Tables[0].Rows[i]["MGRP_NAME"];
                        row["SCH"] = row["SCH"] = dtr.Tables[0].Rows[i]["SCH"] == DBNull.Value ? 0 : dtr.Tables[0].Rows[i]["SCH"];
                        dt.Rows.Add(row);
                }
            }
        }

        if (dt != null)
        {
            rptSchDef.DataSource = dt;
            rptSchDef.DataBind();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void rptSchDef_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptSchDef.PageIndex = e.NewPageIndex;
        rptSchDef.DataBind();
    }

    protected void rbDetail_CheckedChanged(object sender, EventArgs e)
    {
         rbSummary.Checked = false;
        if (rbDetail.Checked == true)
        {
            rptSchDef.Columns[2].Visible = true;
            BindScheduleDefination("Y");
           
        }
    }

    protected void rbSummary_CheckedChanged(object sender, EventArgs e)
    {
        rbDetail.Checked = false;
        if (rbSummary.Checked == true)
        {
            BindScheduleDefination("N");
            rptSchDef.Columns[2].Visible = false;
        }
    }

    #region User Defined Methods
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() != "config")
            {
                if (Request.QueryString["pageno"] != null)
                {
                    //Check for Authorization of Page
                    if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                    {
                        Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                    }
                }
                else
                {
                    //Even if PageNo is Null then, don't show the page
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }

            }

        }
        else
        {
            if (Request.QueryString["pageno"] != null)
            {
                //Check for Authorization of Page
                if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
                {
                    Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
                }
            }
            else
            {
                //Even if PageNo is Null then, don't show the page
                Response.Redirect("~/notauthorized.aspx?page=ledgerhead.aspx");
            }
        }
    }
    
    #endregion
    
}

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
using System.Data.OleDb;
using System.Collections.Generic;
using System.IO;

public partial class PAYROLL_TRANSACTIONS_ExelPayHeadImport : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    ExcelPayHeadImportController objEPIC = new ExcelPayHeadImportController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["mast erpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }


    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null || Session["college_nos"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                FillPayHead();

            }
        }
        else
        {


        }


    }


    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ExelPayHeadImport.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ExelPayHeadImport.aspx");
        }
    }



    #region Private Methods


    protected void FillPayHead()
    {
        try
        {
           // ddlPayhead.Items.Insert(0, "Please Select");
            // ddlPayhead.SelectedIndex = 0;
            // objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE='E'", "PAYHEAD");
          //  objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE='E'", "");
            // ddlPayhead.Items.Insert(0, "Please Select");

            // ddlPayhead.Items.Insert(0, "BASIC");
            // ddlPayhead.SelectedIndex = 0;
            // objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE='E'", "PAYHEAD");
            objCommon.FillDropDownList(ddlPayhead, "PAYROLL_PAYHEAD", "PAYHEAD", "PAYSHORT", "TYPE='E'", "");
            ddlPayhead.Items.Insert(0, "BASIC");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExelPayHeadImport.FillPayHead-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }




    #endregion



    #region Page Events


    protected void btnUploadExcel_Click(object sender, EventArgs e)
    {
        string folderPath=string.Empty;
        string Path = string.Empty;
        DataSet ds = new DataSet();
        try
        {
            if (flUplaodPayheadExcel.HasFile)
            {
                string FileName = flUplaodPayheadExcel.FileName;
                string ext = System.IO.Path.GetExtension(flUplaodPayheadExcel.FileName);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    CustomStatus cs = new CustomStatus();
                    ///// ADDED ON 27-01-2023///////////////////////////
                    folderPath = Server.MapPath("~/Other Rem/");
                    Path = "~/Other Rem/";
                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }
                   // string path = string.Concat(Server.MapPath(folderPath + flUplaodPayheadExcel.FileName));
                    ///////////////////////////////// END 27-01-2023
                    string path = string.Concat(Server.MapPath(Path + flUplaodPayheadExcel.FileName));     // New code 27-01-2023
                    //string path = string.Concat(Server.MapPath("~/Other Rem/" + flUplaodPayheadExcel.FileName));   //  OLD CODE

                    flUplaodPayheadExcel.PostedFile.SaveAs(path);
                    string fileName = flUplaodPayheadExcel.FileName;

                  //  OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");

                    OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR={1}'");

                    //OleDbCommand cmd = new OleDbCommand("SELECT * FROM ["+ddlSubPayhead.SelectedItem.Text+"$]", OleDbcon);

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [PayrollSheet$]", OleDbcon);//Sheet1

                    OleDbDataAdapter objAdapter1 = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    objAdapter1.Fill(ds);

                    dt = ds.Tables[0];
                    int i = 0;


                    Payroll objPayroll = new Payroll();

                    objPayroll.PAYHEAD = ddlPayhead.SelectedValue;

                    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                      //  objPayroll.PFILENO = ds.Tables[0].Rows[i]["PFILENO"].ToString() == "" || ds.Tables[0].Rows[i]["PFILENO"].ToString() == null ? "0" : ds.Tables[0].Rows[i]["PFILENO"].ToString();
                        objPayroll.PFILENO = ds.Tables[0].Rows[i]["PFILENO"].ToString();
                        objPayroll.TOTAMT = ds.Tables[0].Rows[i]["Amount"] == "0.0" || ds.Tables[0].Rows[i]["Amount"] == DBNull.Value ? Convert.ToDecimal("0") : Convert.ToDecimal(ds.Tables[0].Rows[i]["Amount"]);
                        cs = (CustomStatus)objEPIC.UpdatePayHeadsByExcel(objPayroll);
                    }
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        objCommon.DisplayMessage("Data Updated Successfully!!", this);
                        ddlPayhead.SelectedIndex = 0;
                    }
                }
                else
                {
                    objCommon.DisplayMessage("Upload Only Excel Format!!", this);
                    ddlPayhead.SelectedIndex = 0;
                }
            }
            else
            {
                objCommon.DisplayMessage("Please Select Excel File!!", this);
                ddlPayhead.SelectedIndex = 0;
            }
        }

        catch (Exception ex)
        {
            //objCommon.DisplayMessage("Exception Occured,Contact To Administrator!!", this);
            objCommon.DisplayMessage("Data is not in correct format!!", this);
        }
    }


    #endregion

    protected void btnDownlaod_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string path = Server.MapPath("~/IMAGES/Payroll.xls");
            Response.AddHeader("Content-Disposition", "attachment;filename=\"Payroll.xls\"");
            Response.TransmitFile(path);
            Response.End();
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage("Download Fail!", this);
        }

    }


}
//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : Pay_Monthly_Installment_Entry.aspx                                                  
// CREATION DATE : 19-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using AjaxControlToolkit;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.IO;
public partial class PAYROLL_TRANSACTIONS_Pay_Transfer_Data : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    PayController objpay = new PayController();

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
                //Page Authorization
                CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }


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
                Response.Redirect("~/notauthorized.aspx?page=Pay_Transfer_Data.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Pay_Transfer_Data.aspx");
        }
    }

    protected void butShowData_Click(object sender, EventArgs e)
    {
        string OpenPath, contents;
        int tabSize = 4;
        string[] arInfo;
        string line;

        // Create new DataTable.

        DataTable table = CreateTable();
        DataRow row;
        try
        {

            //OpenPath = Server.MapPath(".") + @"\010208.txt";
            OpenPath = @"\\jitendra\D$\" + this.GettextFileName(txtSelectDate.Text);
            string FILENAME = OpenPath;
            //Get a StreamReader class that can be used to read the file
            StreamReader objStreamReader;
            objStreamReader = File.OpenText(FILENAME);


            while ((line = objStreamReader.ReadLine()) != null)
            {
                contents = line.Replace(("").PadRight(tabSize, ' '), "\t");

                // define which character is seperating fields
                char[] textdelimiter = { '\t' };

                arInfo = contents.Split(textdelimiter);
                for (int i = 0; i <= arInfo.Length; i++)
                {
                    row = table.NewRow();
                    row["EMPLOYEEIDNO"] = arInfo[0].ToString();
                    string[] TimeDate = arInfo[1].Split(' ');
                    row["DATE"] = Convert.ToDateTime(arInfo[1]);
                    row["TIME"] = TimeDate[1].ToString().Replace(':','.');
                    row["MACHINENO"] = arInfo[2].ToString();
                    table.Rows.Add(row);
                }
            }
            objStreamReader.Close();

            // Set to DataGrid.DataSource property to the table.

            if (table.Rows.Count > 0)
            {
                GridView1.Visible = true;
                GridView1.DataSource = table;
                GridView1.DataBind();                
                lblError.Visible = false;
            }
            else
            {
                GridView1.Visible = false;
                lblError.Visible = true;

            }
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(UpdatePanel1, ex.Message, this.Page);
            GridView1.Visible = false;
            lblError.Visible = true;           
            lblError.Text = ex.Message;
        }
    }

    private DataTable CreateTable()
    {
        try
        {
            DataTable table = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column;

            // Create new DataColumn, set DataType, ColumnName
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "EMPLOYEEIDNO";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.DateTime");
            column.ColumnName = "DATE";
            table.Columns.Add(column);

            // Create Third column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "TIME";
            table.Columns.Add(column);

            // Create Fourth column. 
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "MACHINENO";
            table.Columns.Add(column);


            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private string GettextFileName(string SelectedDate)
    {
        string[] selectDate = SelectedDate.Split('/');

        string TextFileName = selectDate[0] + selectDate[1] + selectDate[2].Substring(2, 2) + ".txt";

        return TextFileName;
    }

    protected void TransferData_Click(object sender, EventArgs e)
    {
        try
        {
            Int32    idNo;
            DateTime attendedData;
            decimal loginTime;
            Int32 machineNo;
            DataTable table=null;

            if (GridView1.Rows.Count > 0)
            {

                objpay.DeletetTransferData(Convert.ToDateTime(GridView1.Rows[0].Cells[1].Text));


                for (Int32 i = 0; i < GridView1.Rows.Count; i++)
                {
                    idNo = Convert.ToInt32(GridView1.Rows[i].Cells[0].Text);
                    attendedData = Convert.ToDateTime(GridView1.Rows[i].Cells[1].Text);
                    loginTime = Convert.ToDecimal(GridView1.Rows[i].Cells[2].Text);
                    machineNo = Convert.ToInt32(GridView1.Rows[i].Cells[3].Text);
                    objpay.TransferData(idNo, attendedData, loginTime, machineNo, Session["username"].ToString(), Session["colcode"].ToString());
                }

                GridView1.DataSource = table;
                GridView1.DataBind();
                System.Threading.Thread.Sleep(1000);
                objCommon.DisplayMessage(UpdatePanel1, "Data Transfer Successfully", this.Page);
            }
            else
            {
                objCommon.DisplayMessage(UpdatePanel1, "Show Data to Transfer", this.Page);
            
            }

        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            objCommon.DisplayMessage(UpdatePanel1,ex.Message, this.Page);
            lblError.Text = ex.Message;        
        }
    }
}

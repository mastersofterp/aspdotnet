//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : BANK ENTRY                                                     
// CREATION DATE : 28-APR-2010                                               
// CREATED BY    : JITENDRA CHILATE                                                 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================

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
using System.Text.RegularExpressions;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Collections.Generic;
using IITMS.NITPRM;

public partial class BankEntry : System.Web.UI.Page
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
    string isSingleMode = string.Empty;
    public static string isAllreadySet = string.Empty;
    string isPerNarration = string.Empty;
    string isVoucherAuto = string.Empty;
    string isMessagingEnabled = string.Empty;
    string back = string.Empty;
    DataTable dt = new DataTable();
    public string[] para;
    public static string isEdit;
    public static int RowIndex = -1;
    GsmCommMain comm;
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
                if (Session["comp_code"] == null)
                {
                    Session["comp_set"] = "NotSelected";

                    objCommon.DisplayMessage("Select company/cash book.", this);

                    Response.Redirect("~/ACCOUNT/selectCompany.aspx");
                }
                else
                {

                    Session["comp_set"] = "";
                    //Page Authorization
                    CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();

                }



            }





        }
        rowgrid.Visible = false;
        divMsg.InnerHtml = string.Empty;
    }


    //===============

    private void SetDataColumn()
    {


        DataColumn dc = new DataColumn();
        dc.ColumnName = "Particulars";
        dt.Columns.Add(dc);
        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "Top";
        dt.Columns.Add(dc1);

        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "Left";
        dt.Columns.Add(dc2);


        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "Width";
        dt.Columns.Add(dc3);

        Session["Datatable"] = dt;



    }
    private void CheckPageAuthorization()
    {

        //Check for Authorization of Page
        //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
        //{
        //    Response.Redirect("~/notauthorized.aspx?page=BankEntry.aspx");
        //}

    }


    //Fill AutoComplete Against Account Textbox
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetBankName(string prefixText)
    {
        List<string> Ledger = new List<string>();
        DataSet ds = new DataSet();
        try
        {
            AutoCompleteController objAutocomplete = new AutoCompleteController();
            ds = objAutocomplete.GetBankMasterData(prefixText);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Ledger.Add(ds.Tables[0].Rows[i]["bankname"].ToString());
            }
        }
        catch (Exception ex)
        {
            ds.Dispose();
        }
        return Ledger;
    }



    protected void btnSave0_Click(object sender, EventArgs e)
    {
        rowgrid.Visible = true;
        if (txtBank.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Please Enter bank Name", this);
            txtBank.Focus();
            return;
        }

        if (txtBank.Text.ToString().IndexOf("*") == -1)
        {
            //insert new bank with cheque details
            SetDataColumn();
            dt = Session["Datatable"] as DataTable;
            DataRow row;
            string[] particular = { "Reciever", "Amount", "Amt. In Words", "Cheque Date", "Stamp1", "Stamp2" };
            int i = 0;
            for (i = 0; i < particular.Length; i++)
            {
                row = dt.NewRow();
                row["Particulars"] = particular[i].ToString();
                row["Top"] = "0";
                row["Left"] = "0";
                row["Width"] = "0";
                dt.Rows.Add(row);

            }
            GridData.DataSource = dt;
            GridData.DataBind();
            if (CheckNumeric() == false)
            {
                return;
            }

            ViewState["Insert"] = "Y";


        }
        else
        {
            string bankno = txtBank.Text.ToString().Trim().Split('*')[0].ToString().Trim();

            DataSet Datasetbnk = objCommon.FillDropDown("ACC_BANK_DETAIL", "*", "", "BANKNO=" + bankno.ToString(), "");
            if (Datasetbnk != null)
            {
                if (Datasetbnk.Tables[0].Rows.Count > 0)
                {
                    txtChqWidth.Text = Datasetbnk.Tables[0].Rows[0]["CHECKWIDTH"].ToString().Trim();
                    txtChqHeight.Text = Datasetbnk.Tables[0].Rows[0]["CHECKHEIGHT"].ToString().Trim();
                    SetDataColumn();
                    dt = Session["Datatable"] as DataTable;
                    DataRow row;
                    string[] particular = { "Reciever", "Amount", "Amt. In Words", "Cheque Date", "Stamp1", "Stamp2" };
                    int i = 0;
                    for (i = 0; i < particular.Length; i++)
                    {
                        row = dt.NewRow();
                        if (particular[i].ToString().Trim() == "Reciever")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["PARTYTOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["PARTYLEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["PARTYWIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }

                        if (particular[i].ToString().Trim() == "Amount")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["AMTTOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["AMTLEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["AMTWIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }

                        if (particular[i].ToString().Trim() == "Amt. In Words")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["WAMTTOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["WAMTLEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["WAMTWIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }

                        if (particular[i].ToString().Trim() == "Cheque Date")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["CKDTTOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["CKDTLEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["CKDTWIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }

                        if (particular[i].ToString().Trim() == "Stamp1")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["STAMP1TOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["STAMP1LEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["STAMP1WIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }
                        if (particular[i].ToString().Trim() == "Stamp2")
                        {
                            row["Particulars"] = particular[i].ToString();
                            row["Top"] = Datasetbnk.Tables[0].Rows[0]["STAMP2TOP"].ToString().Trim();
                            row["Left"] = Datasetbnk.Tables[0].Rows[0]["STAMP2LEFT"].ToString().Trim();
                            row["Width"] = Datasetbnk.Tables[0].Rows[0]["STAMP2WIDTH"].ToString().Trim();
                            dt.Rows.Add(row);
                        }




                    }


                    GridData.DataSource = dt;
                    GridData.DataBind();


                }


            }



            ViewState["Insert"] = "N";


        }





    }

    protected void SaveRecord()
    {
        AccountTransactionController obank = new AccountTransactionController();
        BankMaster objbank = new BankMaster();
        objbank.BANKNO = 0;
        objbank.BANKNAME = txtBank.Text.ToString().Trim();
        objbank.CHECKHEIGHT = Convert.ToDouble(txtChqHeight.Text.ToString().Trim());
        objbank.CHECKWIDTH = Convert.ToDouble(txtChqWidth.Text.ToString().Trim());

        if (GridData.Rows.Count > 0)
        {
            int y = 0;
            for (y = 0; y < GridData.Rows.Count; y++)
            {
                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Reciever")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.PARTYTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.PARTYLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.PARTYWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Amount")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.AMTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.AMTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.AMTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Amt. In Words")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.WAMTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.WAMTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.WAMTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }




                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Cheque Date")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.CKDTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.CKDTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.CKDTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }



                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Stamp1")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.STAMP1TOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.STAMP1LEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.STAMP1WIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }


                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Stamp2")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.STAMP2TOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.STAMP2LEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.STAMP2WIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

            }

            int i = obank.AddBankDetail(objbank);
            if (i == 1)
            {
                objCommon.DisplayMessage(UPDLedger, "Cheque Parameter For Bank " + txtBank.Text.ToString().Trim() + " Has Configured Successfully.", this);
                txtBank.Focus();
            }



        }



    }

    protected void UpdateRecord()
    {
        AccountTransactionController obank = new AccountTransactionController();
        BankMaster objbank = new BankMaster();
        objbank.BANKNO = Convert.ToInt16(txtBank.Text.ToString().Trim().Split('*')[0]);
        objbank.BANKNAME = Convert.ToString(txtBank.Text.ToString().Trim().Split('*')[1]);
        objbank.CHECKHEIGHT = Convert.ToDouble(txtChqHeight.Text.ToString().Trim());
        objbank.CHECKWIDTH = Convert.ToDouble(txtChqWidth.Text.ToString().Trim());

        if (GridData.Rows.Count > 0)
        {
            int y = 0;
            for (y = 0; y < GridData.Rows.Count; y++)
            {
                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Reciever")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.PARTYTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.PARTYLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.PARTYWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Amount")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.AMTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.AMTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.AMTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Amt. In Words")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.WAMTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.WAMTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.WAMTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }




                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Cheque Date")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.CKDTTOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.CKDTLEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.CKDTWIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }



                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Stamp1")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.STAMP1TOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.STAMP1LEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.STAMP1WIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }


                if (GridData.Rows[y].Cells[0].Text.ToString().Trim() == "Stamp2")
                {
                    TextBox txtop = GridData.Rows[y].FindControl("txttop") as TextBox;
                    objbank.STAMP2TOP = Convert.ToDouble(txtop.Text.ToString().Trim());

                    TextBox txtleft = GridData.Rows[y].FindControl("txtleft") as TextBox;
                    objbank.STAMP2LEFT = Convert.ToDouble(txtleft.Text.ToString().Trim());

                    TextBox txtwidth = GridData.Rows[y].FindControl("txtwidth") as TextBox;
                    objbank.STAMP2WIDTH = Convert.ToDouble(txtwidth.Text.ToString().Trim());


                }

            }

            int i = obank.UpdateBankDetail(objbank);
            if (i == 2)
            {
                objCommon.DisplayMessage(UPDLedger, "Cheque Parameter For Bank " + txtBank.Text.ToString().Trim() + " Has Configured Successfully.", this);
                txtBank.Focus();
            }



        }



    }



    private Boolean CheckNumeric()
    {

        if (GridData.Rows.Count > 0)
        {
            int k = 0;
            for (k = 0; k < GridData.Rows.Count; k++)
            {
                TextBox txttop = GridData.Rows[k].FindControl("txttop") as TextBox;
                TextBox txtleft = GridData.Rows[k].FindControl("txtleft") as TextBox;
                TextBox txtwidth = GridData.Rows[k].FindControl("txtwidth") as TextBox;
                txttop.Attributes.Add("onkeydown", "return CheckNumeric(this);");
                txtleft.Attributes.Add("onkeydown", "return CheckNumeric(this);");
                txtwidth.Attributes.Add("onkeydown", "return CheckNumeric(this);");

                if (txttop.Text.ToString().Trim() == "")
                {
                    objCommon.DisplayMessage(UPDLedger, "Field Can not Be Blank", this);
                    txttop.Focus();
                    return false;

                }
                if (txtleft.Text.ToString().Trim() == "")
                {
                    objCommon.DisplayMessage(UPDLedger, "Field Can not Be Blank", this);
                    txtleft.Focus();
                    return false;

                }
                if (txtwidth.Text.ToString().Trim() == "")
                {
                    objCommon.DisplayMessage(UPDLedger, "Field Can not Be Blank", this);
                    txtwidth.Focus();
                    return false;

                }
                double test = 0;
                try
                {
                    test = Convert.ToDouble(txttop.Text.ToString().Trim());
                    test = Convert.ToDouble(txtwidth.Text.ToString().Trim());
                    test = Convert.ToDouble(txtleft.Text.ToString().Trim());

                }
                catch
                {
                    objCommon.DisplayMessage(UPDLedger, "Numeric Value Should Be Entered.", this);
                    txtwidth.Focus();
                    return false;


                }





            }


        }


        return true;




    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtBank.Text.ToString() == "")
        {

            objCommon.DisplayMessage(UPDLedger, "Enter The Bank Name And Click On Go Button.", this);
            txtBank.Focus();
            return;

        }
        if (GridData.Rows.Count == 0)
        {
            objCommon.DisplayMessage(UPDLedger, "Generate The Cheque Parameters Configuration.", this);
            btnSave0.Focus();
            return;

        }
        if (txtChqHeight.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter Cheque Height.", this);
            txtChqHeight.Focus();
            return;

        }
        if (txtChqWidth.Text.ToString().Trim() == "")
        {
            objCommon.DisplayMessage(UPDLedger, "Enter Cheque Width.", this);
            txtChqWidth.Focus();
            return;

        }

        if (ViewState["Insert"].ToString().Trim() == "Y")
        {
            SaveRecord();
            ClearAll();
        }
        else
        {
            UpdateRecord();
            ClearAll();
            //put code for bank updatetions



        }

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearAll();
    }
    private void ClearAll()
    {
        txtBank.Text = "";
        txtChqHeight.Text = "";
        txtChqWidth.Text = "";
        GridData.DataSource = null;
        GridData.DataBind();
        txtBank.Focus();
        rowgrid.Visible = false;

    }
}

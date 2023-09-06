using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;


public partial class ACCOUNT_XMLupload : System.Web.UI.Page
{
    TallyDataTransfer objTally = new TallyDataTransfer();
    Common objCommon = new Common();
    static string _Account_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    static string compcode = string.Empty;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.QueryString["obj"] != null)
        {
            if (Request.QueryString["obj"].ToString().Trim() == "AccountingVouchers")
            {
                objCommon.SetMasterPage(Page, "ACCOUNT/LedgerMasterPage.master");

            }
            else
            {
                if (Session["masterpage"] != null)
                    objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
                else
                    objCommon.SetMasterPage(Page, "");
            }
        }
        else
        {
            if (Session["masterpage"] != null)
                objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
            else
                objCommon.SetMasterPage(Page, "");
        }

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
                    //CheckPageAuthorization();

                    divCompName.InnerHtml = Session["comp_name"].ToString().ToUpper();
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help

                    //PopulateDropDown();
                    //PopulateListBox();


                    compcode = Session["comp_code"].ToString();
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (fulUpload.HasFile)
        {
            if (File.Exists("~/FileUpload/" + fulUpload.FileName) == false)
                fulUpload.SaveAs(Server.MapPath("~/FileUpload/" + fulUpload.FileName));

            XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("~/FileUpload/" + fulUpload.FileName));

            DataSet ds = new DataSet();

            ds.ReadXml(xmlreader);

            xmlreader.Close();

            int a = objTally.AddTallyVocher(ds.Tables["VOUCHER"], ds.Tables["BANKALLOCATIONS.LIST"], ds.Tables["ALLLEDGERENTRIES.LIST"], Session["comp_code"].ToString());

            if (a == 1)
            {
                DataSet dsLedger = new DataSet();
                dsLedger = objCommon.FillDropDown("ACC_TALLY_ALLLEDGERENTRIES a left join ACC_" + Session["comp_code"].ToString() + "_PARTY b on (a.LEDGERNAME=b.PARTY_NAME)", "distinct LEDGERNAME", "cast(b.PARTY_NO as nvarchar(50))+'*'+ b.PARTY_NAME PARTY_NAME", "", "");
                if (dsLedger.Tables[0].Rows.Count > 0)
                    btnMap.Visible = true;
                else
                    btnMap.Visible = false;
                GridData.DataSource = dsLedger;
                GridData.DataBind();
            }
        }
    }

    protected void btnMap_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (!dt.Columns.Contains("LEDGERNAME"))
            dt.Columns.Add("LEDGERNAME");

        if (!dt.Columns.Contains("PARTY_NAME"))
            dt.Columns.Add("PARTY_NAME");

        if (!dt.Columns.Contains("PARTYNO"))
            dt.Columns.Add("PARTYNO");


        for (int i = 0; i < GridData.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            Label lblEmpName = GridData.Rows[i].FindControl("lblPayHeadsNo") as Label;
            dr["LEDGERNAME"] = lblEmpName.Text;

            TextBox txtLedgerHead = GridData.Rows[i].FindControl("txtLedgerHead") as TextBox;
            if (txtLedgerHead.Text == string.Empty)
            {
                objCommon.DisplayMessage("Please map all ledger", this.Page);
                return;
            }

            dr["PARTY_NAME"] = txtLedgerHead.Text.Split('*')[1];
            dr["PARTYNO"] = txtLedgerHead.Text.Split('*')[0];
            dt.Rows.Add(dr);

        }

        int a = objTally.AddTallyLedger(dt, Session["comp_code"].ToString());

        if (a != -99)
        {
            objCommon.DisplayUserMessage(updPnl,"Data uploaded successfully", this.Page);
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetLedger(string prefixText)
    {
        // Common objCommon = new Common();
        SQLHelper objSQLHelper = new SQLHelper(_Account_constr);
        DataSet ds = objSQLHelper.ExecuteDataSet("select cast(PARTY_NO as varchar(10))+'*'+PARTY_NAME as name from ACC_" + compcode + "_" + "PARTY WHERE PARTY_NAME like '%" + prefixText + "%'  ORDER BY PARTY_NAME");
        List<string> ledgerList = new List<string>();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            ledgerList.Add(ds.Tables[0].Rows[i]["name"].ToString());
        }
        return ledgerList;
    }

    protected void GridData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPayHeadsNo = e.Row.FindControl("lblPayHeadsNo") as Label;
            TextBox txtLedgerHead = e.Row.FindControl("txtLedgerHead") as TextBox;
            if (txtLedgerHead.Text == string.Empty)
                txtLedgerHead.Text = objCommon.LookUp("ACC_TALLY_LEDGER_MAPPING a inner join ACC_" + Session["comp_code"].ToString() + "_PARTY b on (a.PARTYNO=b.PARTY_NO)", "cast(b.PARTY_NO as nvarchar(50)) +'*'+b.PARTY_NAME", "COMP_CODE='" + Session["comp_code"].ToString() + "' AND A.LEDGERNAME='" + lblPayHeadsNo.Text.Replace("'","") + "'");
        }
    }
}

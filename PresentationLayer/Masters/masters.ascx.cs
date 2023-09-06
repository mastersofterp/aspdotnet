//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC & PAYROLL                                              
// PAGE NAME     : TO CREATE DYNAMIC MASTER PAGES                                  
// CREATION DATE : 27-APRIL-2009                                                   
// CREATED BY    : NIRAJ D. PHALKE, G.V.S. KIRAN, ASHWINI BARBATE                  
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

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;

public partial class Masters_masters : System.Web.UI.UserControl
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "Server UnAvailable");

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Create table for adding data
        if (Request.QueryString.Count != 0)
        {
            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }

            if (Request.QueryString["page"] != null && Request.QueryString["pagetitle"] != null)
            {
                Masters objMasters = new Masters(Request.QueryString["page"].ToString());
                gvTitle.InnerHtml = Request.QueryString["pagetitle"].ToString() + " List";
                lblPageTitle.Text = Request.QueryString["pagetitle"].ToString().ToUpper() + " MANAGEMENT";

                //Add Dynamic Gridveiw
                GridView gv = new GridView(); ;
                if (Request.QueryString["head"] != null)
                {
                    if (Request.QueryString["head"].Equals("gpf"))
                        gv = objMasters.CreateGridView("app_for='G'");
                    else if (Request.QueryString["head"].Equals("cpf"))
                        gv = objMasters.CreateGridView("app_for='C'");
                }
                else
                    gv = objMasters.CreateGridView(string.Empty);

                phList.Controls.Add(gv);
                gv.RowCommand += new GridViewCommandEventHandler(gvmaster_RowCommand);
                gv.CssClass = "table table-striped table-bordered nowrap display";
                gv.HeaderStyle.CssClass = "bg-light-blue";
                //Add Dynamic Table
                HtmlTable tbl = objMasters.CreateTableHTML();
                phAdd.Controls.Add(tbl);

                //Set Report Parameters
                string title = "@Title=" + objMasters.captions[0, 8];
                if (Request.QueryString["head"] != null)
                {
                    if (Request.QueryString["head"].Equals("gpf"))
                        title = "@Title=List of GPF Fund Head";
                    else if (Request.QueryString["head"].Equals("cpf"))
                        title = "@Title=List of CPF Fund Head";
                }

                //Add onclientclick method
                objCommon.ReportPopUp(btnShowReport, "pagetitle=UAIMS&page=" + Request.QueryString["page"].ToString() + "&param=@CollegeName=" + Session["coll_name"].ToString() + "," + "@UserName=" + Session["userfullname"].ToString() + "," + title, "UAIMS");
            }
        }

        if (ViewState["action"] == null)
            ViewState["action"] = "add";

        //if (Page.Request.Params["__EVENTTARGET"] != null)
        //{
        //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("gvmaster"))
        //        gvmaster_RowCommand(sender,null);
        //    if (Page.Request.Params["__EVENTTARGET"].ToString().ToLower().Contains("tabmaster"))
        //        gvmaster_RowCommand(sender,null);

        //}
        ReloadTableData();
    }

    //USED FOR SAVE AND UPDATE DATA FOR MULTIPLE PAGES
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["page"].ToString() != null)
            {
                TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
                CheckBox chk1 = phAdd.FindControl("chkColumn1") as CheckBox;
                CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                CheckBox chk4 = phAdd.FindControl("chkColumn3") as CheckBox;
                CheckBox chk2 = phAdd.FindControl("chkColumn4") as CheckBox;
                Masters objMaster = new Masters(Request.QueryString["page"].ToString());

                //Add/Update
                if (ViewState["action"] != null)
                {
                    string columnnames = string.Empty;
                    string columnvalues = string.Empty;
                    string columnid = string.Empty;

                    columnid = objMaster.captions[0, 3];
                    columnnames = objMaster.captions[0, 5];
                    string[] calnames = columnnames.Split(',');

                    int collen = int.Parse(objMaster.captions[0, 4]);

                    if (collen == 2)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = calnames[1] + "=" + txt1.Text.Trim() + "";

                            //added by sneha G.
                            if (objMaster.captions[1, 1].Equals("checkbox"))
                            {
                                if (chk1.Checked == true)
                                    columnvalues += "," + calnames[2] + "='" + "1" + "'";
                                else
                                    columnvalues += "," + calnames[2] + "='" + "0" + "'";
                            }

                            columnid = columnid + "=" + ViewState["id"];
                        }
                        else
                        {
                            //insert
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = "'" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = txt1.Text.Trim() + "," + "'";

                            //added by sneha G.
                            if (objMaster.captions[1, 1].Equals("checkbox"))
                            {
                                if (chk1.Checked == true)
                                    columnvalues += ",'" + "1" + "'";
                                else
                                    columnvalues += ",'" + "0" + "'";
                            }

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 3)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = calnames[1] + "=" + txt1.Text.Trim();

                            if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                columnvalues += "," + calnames[2] + "='" + txt2.Text.Trim() + "'";
                            else
                                columnvalues += "," + calnames[2] + "=" + txt2.Text.Trim();

                            #region Comment
                            //columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'" + "," + calnames[2] + "='" + txt2.Text.Trim() + "'";

                            //if (objMaster.captions[2, 1].Equals("checkbox"))
                            //{
                            //    if (chk3.Checked == true)
                            //        columnvalues += "," + "1";
                            //    else
                            //        columnvalues += "," + "0";
                            //}  
                            #endregion

                            //added by sneha G.
                            if (objMaster.captions[2, 1].Equals("checkbox"))
                            {
                                if (chk3.Checked == true)
                                    columnvalues += "," + calnames[3] + "='" + "1" + "'";
                                else
                                    columnvalues += "," + calnames[3] + "='" + "0" + "'";
                            }
                            columnid = columnid + "=" + ViewState["id"];
                        }
                        else
                        {
                            //insert
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = "'" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = txt1.Text.ToUpper().Trim();

                            if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                columnvalues += ",'" + txt2.Text.Trim() + "'";
                            else
                                columnvalues += "," + txt2.Text.Trim();

                            #region Comment
                            //if (objMaster.captions[2, 1].Equals("checkbox"))
                            //{
                            //    if (chk3.Checked == true)
                            //        columnvalues += "," + "1";
                            //    else
                            //        columnvalues += "," + "0";
                            //} 
                            #endregion

                            //added by sneha G.
                            if (objMaster.captions[2, 1].Equals("checkbox"))
                            {
                                if (chk3.Checked == true)
                                    columnvalues += ",'" + "1" + "'";
                                else
                                    columnvalues += ",'" + "0" + "'";
                            }

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";

                            //columnvalues = "'" + txt1.Text.Trim() + "'" + "," + "'" + txt2.Text.Trim() + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 4)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = calnames[1] + "=" + txt1.Text.Trim();

                            if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                columnvalues += "," + calnames[2] + "='" + txt2.Text.Trim() + "'";
                            else
                                columnvalues += "," + calnames[2] + "=" + txt2.Text.Trim();

                            if (objMaster.captions[2, 6].Equals("string") || objMaster.captions[2, 6].Equals("string2"))
                                columnvalues += "," + calnames[3] + "='" + txt3.Text.Trim() + "'";
                            else
                                columnvalues += "," + calnames[3] + "=" + txt3.Text.Trim();

                            //added by sneha G.
                            if (objMaster.captions[3, 1].Equals("checkbox"))
                            {
                                if (chk4.Checked == true)
                                    columnvalues += "," + calnames[4] + "='" + "1" + "'";
                                else
                                    columnvalues += "," + calnames[4] + "='" + "0" + "'";
                            }

                            //columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'" + "," + calnames[2] + "='" + txt2.Text.Trim() + "'" + "," + calnames[3] + "='" + txt3.Text.Trim() + "'";
                            columnid = columnid + "=" + ViewState["id"];
                        }
                        else
                        {
                            //insert
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = "'" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = txt1.Text.Trim();

                            if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                columnvalues += ",'" + txt2.Text.Trim() + "'";
                            else
                                columnvalues += "," + txt2.Text.Trim();

                            if (objMaster.captions[2, 6].Equals("string") || objMaster.captions[2, 6].Equals("string2"))
                                columnvalues += ",'" + txt3.Text.Trim() + "'";
                            else
                                columnvalues += "," + txt3.Text.Trim();

                            //added by sneha G.
                            if (objMaster.captions[3, 1].Equals("checkbox"))
                            {
                                if (chk4.Checked == true)
                                    columnvalues += ",'" + "1" + "'";
                                else
                                    columnvalues += ",'" + "0" + "'";
                            }

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";

                            //columnvalues = "'" + txt1.Text.Trim() + "'" + "," + "'" + txt2.Text.Trim() + "'" + "," + "'" + txt3.Text.Trim() + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 5)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            //columnnames = calnames[1] + "='" + txt1.Text + "'" + "," + calnames[2] + "='" + txt2.Text + "'" + "," + calnames[3] + "='" + txt3.Text + "'" + "," + calnames[4] + "='" + txt4.Text + "'";
                            columnid = columnid + "=" + ViewState["id"];

                            if (objMaster.captions[0, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                    columnvalues += calnames[1] + "='" + txt1.Text.Trim() + "'";
                                else
                                    columnvalues += calnames[1] + "=" + txt1.Text.Trim();
                            }

                            if (objMaster.captions[1, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                    columnvalues += "," + calnames[2] + "='" + txt2.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[2] + "=" + txt2.Text.Trim();
                            }

                            if (objMaster.captions[2, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[2, 6].Equals("string") || objMaster.captions[2, 6].Equals("string2"))
                                    columnvalues += "," + calnames[3] + "='" + txt3.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[3] + "=" + txt3.Text.Trim();
                            }

                            #region Comment
                            //else if (objMaster.captions[2, 1].Equals("checkbox"))
                            //{
                            //    if (chk3.Checked == true)
                            //        columnvalues += "," + calnames[3] + "=1";
                            //    else
                            //        columnvalues += "," + calnames[3] + "=0";
                            //}
                            #endregion

                            //added by sneha G.
                            else if (objMaster.captions[3, 1].Equals("checkbox"))
                            {
                                if (chk3.Checked == true)
                                    columnvalues += "," + calnames[4] + "='" + "1" + "'";
                                else
                                    columnvalues += "," + calnames[4] + "='" + "0" + "'";
                            }

                            if (objMaster.captions[3, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[3, 6].Equals("string") || objMaster.captions[3, 6].Equals("string2"))
                                    columnvalues += "," + calnames[4] + "='" + txt4.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[4] + "=" + txt4.Text.Trim();
                            }
                        }
                        else
                        {
                            //To insert
                            //columnvalues = "'" + txt1.Text + "'" + "," + "'" + txt2.Text + "'" + "," + "'" + txt3.Text + "'" + "," + "'" + txt4.Text + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                            if (objMaster.captions[0, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                    columnvalues += "'" + txt1.Text.Trim() + "'";
                                else
                                    columnvalues += "," + txt1.Text.Trim();
                            }

                            if (objMaster.captions[1, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                    columnvalues += "," + "'" + txt2.Text.Trim() + "'";
                                else
                                    columnvalues += "," + txt2.Text.Trim();
                            }

                            if (objMaster.captions[2, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[2, 6].Equals("string") || objMaster.captions[2, 6].Equals("string2"))
                                    columnvalues += "," + "'" + txt3.Text.Trim() + "'";
                                else
                                    columnvalues += "," + txt3.Text.Trim();

                            }

                            //else if (objMaster.captions[2, 1].Equals("checkbox"))
                            //{
                            //    if (chk3.Checked == true)
                            //        columnvalues += "," + "1";
                            //    else
                            //        columnvalues += "," + "0";
                            //}


                            //added by sneha G.
                            if (objMaster.captions[3, 1].Equals("checkbox"))
                            {
                                if (chk3.Checked == true)
                                    columnvalues += ",'" + "1" + "'";
                                else
                                    columnvalues += ",'" + "0" + "'";
                            }

                            if (objMaster.captions[3, 1].Equals("textbox"))
                            {
                                if (objMaster.captions[3, 6].Equals("string") || objMaster.captions[3, 6].Equals("string2"))
                                    columnvalues += "," + "'" + txt4.Text.Trim() + "'";
                                else
                                    columnvalues += "," + txt4.Text.Trim();
                            }

                            if (Request.QueryString["head"] != null)
                            {
                                if (Request.QueryString["head"].Equals("gpf"))
                                    columnvalues += "," + "'G'";
                                else if (Request.QueryString["head"].Equals("cpf"))
                                    columnvalues += "," + "'C'";
                            }

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }

                    if (ViewState["action"].ToString().Equals("add"))
                    {

                        //Modified By Zubair Ahmad
                        //Date: 13-DEC-2014
                        //Purpose: To apply validations on textBoxes of Payroll Master forms
                        string strpagename, strViewRecord = string.Empty;
                        strpagename = Request.QueryString["page"].ToString();

                        switch (strpagename)
                        {
                            case "acd_paymenttype":  //academic//Added by Jay T. On dated 14072023

                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\-]+$") && Regex.IsMatch(txt2.Text, @"^[a-zA-Z\s.\?\,\'\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Only Alphabets.", this.Page);
                                    return;
                                }

                            case "acd_semester":  //academic
                                if (Regex.IsMatch(txt3.Text, @"^[0-9\s.\?\,\'\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Only Numeric Value.", this.Page);
                                    return;
                                }

                            // For Caste
                            case "payroll_caste":
                                strViewRecord = objCommon.LookUp("payroll_caste", "CASTENO", "CASTE='" + txt1.Text + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                    return;
                                }

                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Only Alphabets.", this.Page);
                                    return;
                                }

                            // For Pay Rule
                            case "payroll_rule":
                                strViewRecord = objCommon.LookUp("payroll_rule", "RULENO", "PAYRULE='" + txt1.Text + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                    return;
                                }
                                else if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Image Type
                            case "payroll_photo_type":
                                if (Regex.IsMatch(txt1.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }

                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Qualification Level
                            case "payroll_qualilevel":

                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Alphabets Only.", this.Page);
                                    return;
                                }

                            // For Bank
                            case "payroll_bank":

                                strViewRecord = objCommon.LookUp("PAYROLL_BANK", "BANKNO", "BANKCODE='" + txt1.Text + "' AND BANKNAME='" + txt2.Text + "' AND BANKADDR='" + txt3.Text + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                    return;
                                }

                                if (Regex.IsMatch(txt2.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$") || Regex.IsMatch(txt3.Text, @"^[Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }

                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$") && Regex.IsMatch(txt2.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Supplimentry Heads
                            case "PAYROLL_SUPPLIMENTARY_HEAD":
                                strViewRecord = objCommon.LookUp("PAYROLL_SUPPLIMENTARY_HEAD", "SUPLHNO", "SUPLHEAD='" + txt1.Text + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                    return;
                                }

                                if (Regex.IsMatch(txt1.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }
                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Staff Type
                            case "payroll_stafftype":
                                if (Regex.IsMatch(txt1.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }
                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Designature
                            case "payroll_designature":
                                if (Regex.IsMatch(txt1.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }
                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }


                            //Added by : Prity Khandait
                            //Date : 23/12/2014 
                            //Purpose : To avoid duplicate entries.
                            // For City
                            case "payroll_city":

                                strViewRecord = objCommon.LookUp("payroll_city", "CITYNO", "CITY='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Category
                            case "payroll_category":
                                strViewRecord = objCommon.LookUp("payroll_category", "CATEGORYNO", "CATEGORY='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Religion
                            case "payroll_religion":
                                strViewRecord = objCommon.LookUp("payroll_religion", "religionNO", "religion='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Staff
                            case "payroll_staff":
                                strViewRecord = objCommon.LookUp("payroll_staff", "staffNO", "staff='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Title
                            case "payroll_title":
                                strViewRecord = objCommon.LookUp("PAYROLL_TITLE", "TITLENO", "TITLE='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                else if (Regex.IsMatch(txt1.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Only numeric value is not accepted.", this.Page);
                                    return;
                                }
                                else if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z0-9\s.\?\,\'\;\:\!\-]+$") || Regex.IsMatch(txt2.Text, @"^[0-9\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Special Symbols are not allowed.", this.Page);
                                    return;
                                }

                            // For Transaction Type
                            case "payroll_typetran":
                                strViewRecord = objCommon.LookUp("PAYROLL_TYPETRAN", "TYPETRANNO", "TYPETRAN='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Department
                            case "payroll_subdept":
                                strViewRecord = objCommon.LookUp("PAYROLL_SUBDEPT", "SUBDEPTNO", "SUBDEPT='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Alphabets Only.", this.Page);
                                    return;
                                }

                            // For Quarter Type
                            case "payroll_quarter_type":
                                strViewRecord = objCommon.LookUp("PAYROLL_QUARTER_TYPE", "QRTTYPENO", "QRTTYPE='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\;\:\!\-]+$"))
                                {
                                    break;
                                }
                                else
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Accept Alphabets Only.", this.Page);
                                    return;
                                }

                            // For LoanType
                            case "payroll_loantype":
                                strViewRecord = objCommon.LookUp("PAYROLL_LOANTYPE", "LOANNO", "LOANNAME='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Nationality
                            case "payroll_nationality":
                                strViewRecord = objCommon.LookUp("PAYROLL_NATIONALITY", "NATIONALITYNO", "NATIONALITYNM='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For Designation
                            case "payroll_subdesig":
                                strViewRecord = objCommon.LookUp("PAYROLL_SUBDESIG", "SUBDESIGNO", "SUBDESIG='" + txt1.Text.Trim() + "' and SUBSDESIG='" + txt2.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For NominiType
                            case "payroll_nominiType":
                                strViewRecord = objCommon.LookUp("PAYROLL_NOMINITYPE", "NTNO", "NOMINITYPE='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                            // For State
                            case "payroll_state":
                                strViewRecord = objCommon.LookUp("PAYROLL_STATE", "STATENO", "STATE='" + txt1.Text.Trim() + "'");
                                if (strViewRecord != string.Empty)
                                {
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exists", this.Page);
                                    return;
                                }
                                break;

                        }


                        /// SWATI- 14-3-14     
                        /// updated by pankaj nakhale 31/08/2019
                        if (objMaster.captions[0, 4] == "4" || objMaster.captions[0, 4] == "3" || objMaster.captions[0, 4] == "2")
                        {
                            if (objMaster.captions[0, 4] == "3")
                            {
                                DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    // Display message for Duplication
                                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //lblmsg.Text = "Record already exist";
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                }
                                else
                                {
                                    //Add New Masters Information
                                    int output = objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                                    if (output.Equals(Convert.ToInt32(CustomStatus.RecordSaved)))
                                    {
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }

                                        //lblmsg.Text = "Record Saved Successfully";
                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Saved Successfully", this.Page);
                                        ReloadTableData();
                                        //System.Threading.Thread.Sleep(5000);
                                        //bhus Response.Redirect(Request.Url.ToString());

                                    }
                                    //else
                                    //    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //lblmsg.Text = "Record already exist";
                                }


                            }
                            else if (objMaster.captions[0, 4] == "2")
                            {
                                DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[0, 2] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    // Display message for Duplication
                                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //lblmsg.Text = "Record already exist";
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                }
                                else
                                {
                                    //Add New Masters Information
                                    int output = objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                                    if (output.Equals(Convert.ToInt32(CustomStatus.RecordSaved)))
                                    {
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }

                                        //  lblmsg.Text = "Record Saved Successfully";
                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Saved Successfully", this.Page);
                                        ReloadTableData();
                                        //System.Threading.Thread.Sleep(5000);
                                        //  Response.Redirect(Request.Url.ToString());
                                        //lblmsg.Text = "Record Updated Succesfully";

                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        //  ReloadTableData();

                                    }
                                    //else
                                    //    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //    lblmsg.Text = "Record already exist";
                                }
                            }
                            else if (objMaster.captions[0, 4] == "4")/////THIS ADDED FOR COLUMN 3 CONDITION FOR ADD DATA BY PANKAJ NAKHALE 27112019
                            {
                                DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[2, 2] + "= '" + Convert.ToString(txt3.Text.Trim()) + "' and " + objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);
                                // DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    // Display message for Duplication
                                    //lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //lblmsg.Text = "Record already exist";
                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                }
                                else
                                {
                                    //Add New Masters Information
                                    int output = objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                                    if (output.Equals(Convert.ToInt32(CustomStatus.RecordSaved)))
                                    {
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }

                                        //lblmsg.Text = "Record Saved Successfully";
                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Saved Successfully", this.Page);
                                        ReloadTableData();
                                        //System.Threading.Thread.Sleep(5000);
                                        //bhus Response.Redirect(Request.Url.ToString());

                                    }
                                    //else
                                    //    lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //lblmsg.Text = "Record already exist";
                                }


                            }
                            ///////////////////////ADDED FOR 3 COLOMN ADD DATA BY PANKAJ NAKHALE 27112019///////////////
                        }//end swati
                        else
                        {

                            //Add New Masters Information
                            int output1 = objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                            if (output1.Equals(Convert.ToInt32(CustomStatus.RecordSaved)))
                            {
                                if (collen == 2)
                                {
                                    txt1.Text = string.Empty;
                                    chk1.Checked = false;               //added by sneha G. on 23/05/2020

                                }
                                if (collen == 3)
                                {
                                    txt1.Text = string.Empty;
                                    txt2.Text = string.Empty;
                                    chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                }
                                if (collen == 4)
                                {
                                    txt1.Text = string.Empty;
                                    txt2.Text = string.Empty;
                                    txt3.Text = string.Empty;
                                    chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                }
                                if (collen == 5)
                                {
                                    txt1.Text = string.Empty;
                                    txt2.Text = string.Empty;
                                    txt3.Text = string.Empty;
                                    txt4.Text = string.Empty;
                                    chk3.Checked = false;
                                }

                                // lblmsg.Text = "Record Saved Successfully";
                                //   lblmsg.ForeColor = System.Drawing.Color.Green;
                                objCommon.DisplayMessage(this.pnlAdd, "Record Saved Successfully", this.Page);
                                ReloadTableData();
                                //System.Threading.Thread.Sleep(5000);
                                //   Response.Redirect(Request.Url.ToString());
                            }
                            else
                            {
                                //lblmsg.ForeColor = System.Drawing.Color.Red;
                                //lblmsg.Text = "Record already exist";
                                objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                            }
                        }
                    }
                    else
                    {
                        #region comment

                        //Update Masters Information

                        //CustomStatus cs = (CustomStatus)objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                        //if (cs.Equals(CustomStatus.RecordSaved))
                        //{
                        //    ViewState["action"] = null;
                        //    lblmsg.Text = "Record Updated Successfully";
                        //    Response.Redirect(Request.Url.ToString());
                        //}
                        //else
                        //    lblmsg.Text = "Server Error";
                        #endregion
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            //Added by Jay T. On dated 14072023
                            string strpagename, strViewRecord = string.Empty,  strStatus = String.Empty, columnStatus=string.Empty;
                            strpagename = Request.QueryString["page"].ToString();

                            switch (strpagename)
                            {

                                case "acd_semester":  //academic  //Added by Jay T. On dated 14072023
                                    if (Regex.IsMatch(txt3.Text, @"^[0-9\s.\?\,\'\-]+$"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.pnlAdd, "Accept Only Numeric Value.", this.Page);
                                        return;
                                    }


                                case "acd_paymenttype":  //academic  //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_paymenttype", "PAYTYPENO", "PAYTYPENAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_paymenttype", "ACTIVESTATUS", "PAYTYPENO='" + ViewState["id"] + "'");
                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[2, 1].Equals("checkbox"))
                                        {
                                            if (chk3.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    if (Regex.IsMatch(txt1.Text, @"^[a-zA-Z\s.\?\,\'\-]+$") && Regex.IsMatch(txt2.Text, @"^[a-zA-Z\s.\?\,\'\-]+$"))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        objCommon.DisplayMessage(this.pnlAdd, "Accept Only Alphabets.", this.Page);
                                        return;
                                    }

                                case "ACD_UA_SECTION":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_ua_section", "UA_SECTION", "UA_SECTIONNAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_ua_section", "ACTIVESTATUS", "UA_SECTION='" + ViewState["id"] + "'");
                                    
                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[1, 1].Equals("checkbox"))
                                        {
                                            if (chk1.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;

                                case "acd_Course_Category":////Added by Jay T. On dated 14072023 
                                    strViewRecord = objCommon.LookUp("acd_course_category", "CATEGORYNO", "CATEGORYNAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_course_category", "ACTIVESTATUS", "CATEGORYNO='" + ViewState["id"] + "'");
                                    
                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[2, 1].Equals("checkbox"))
                                        {
                                            if (chk3.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;
                                case "acd_currency":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_currency", "CUR_NO", "CUR_NAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_currency", "ACTIVESTATUS", "CUR_NO='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[2, 1].Equals("checkbox"))
                                        {
                                            if (chk3.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;

                                case "acd_Fees_Master":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_Fees_Master", "FeeItemId", "FeeItemName='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_Fees_Master", "ACTIVESTATUS", "FeeItemId='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[1, 1].Equals("checkbox"))
                                        {
                                            if (chk1.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;

                                case "acd_specialisation":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_specialisation", "specialisationno", "specialisation_name='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_specialisation", "activestatus", "specialisationno='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[1, 1].Equals("checkbox"))
                                        {
                                            if (chk1.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;
                                case "ATT_WIEGHTAGE":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("att_wieghtage", "ATT_WT_NO", "ATTENDANCE='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("att_wieghtage", "ACTIVESTATUS", "ATT_WT_NO='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[2, 1].Equals("checkbox"))
                                        {
                                            if (chk3.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;
                                case "acd_payment_activity_master":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("acd_payment_activity_master", "ACTIVITYNO", "ACTIVITYNAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("acd_payment_activity_master", "ACTIVESTATUS", "ACTIVITYNO='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[1, 1].Equals("checkbox"))
                                        {
                                            if (chk1.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;
                                case "ACD_RECIEPT_CODE":// //Added by Jay T. On dated 14072023
                                    strViewRecord = objCommon.LookUp("ACD_RECIEPT_CODE", "RCNO", "RC_NAME='" + txt1.Text + "'");
                                    strStatus = objCommon.LookUp("ACD_RECIEPT_CODE", "ACTIVESTATUS", "RCNO='" + ViewState["id"] + "'");

                                    if (strViewRecord != string.Empty)
                                    {
                                        if (objMaster.captions[2, 1].Equals("checkbox"))
                                        {
                                            if (chk3.Checked == true)
                                                columnStatus = "1";
                                            else
                                                columnStatus = "0";
                                        }
                                        if (columnStatus == strStatus)
                                        {
                                            objCommon.DisplayMessage(this.pnlAdd, "Record is Already Exists...", this.Page);
                                            return;
                                        }
                                    }
                                    break;
                            }
                            if (objMaster.captions[0, 4] == "4" || objMaster.captions[0, 4] == "3" || objMaster.captions[0, 4] == "2")
                            {

                                if (objMaster.captions[0, 4] == "2")
                                {
                                    #region comment
                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[0, 2] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);
                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[1, 2] + "= '" + Convert.ToString(chk1.Checked) + "' and " + objMaster.captions[0, 2] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);               //Sneha G.

                                    //if (ds.Tables[0].Rows.Count > 0)
                                    //{
                                    //    // Display message for Duplication
                                    //    //lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //    //lblmsg.Text = "Record already exist";
                                    //    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    //}
                                    //}



                                    //=================17-08-2022==================start======
                                    //dispatch Post Type master
                                    //Added by : Shaikh Juned
                                    //Purpose : To avoid duplicate entries for Update Record.



                                    //  DataSet ds1 = objMaster.AllMasters(objMaster._tablename, calnames[1] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' AND " + calnames[2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "'", objMaster.captions[0, 2]);
                                    //DataSet ds1 = objMaster.AllMasters(objMaster._tablename, objMaster.captions[0, 2] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);

                                    //if (ds1.Tables[0].Rows.Count > 0)
                                    //{
                                    //    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    //    return;
                                    //}
                                    //=================17-08-2022==================end======
                                    //else
                                    //{
                                    #endregion

                                    int output1 = objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                                    if (output1.Equals(Convert.ToInt32(CustomStatus.RecordUpdated)))
                                    {
                                        ViewState["action"] = null;
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }
                                        // lblmsg.Text = "Record Updated Succesfully";

                                        // lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Updated Successfully", this.Page);
                                        ReloadTableData();
                                        //bhus   Response.Redirect(Request.Url.ToString());
                                    }
                                    else /// added by jay on 13/07/2023
                                    {

                                        objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                        ReloadTableData();
                                    }
                                    //}
                                }
                                else if (objMaster.captions[0, 4] == "3")
                                {
                                    #region comment

                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);

                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[2, 2] + "= '" + Convert.ToString(chk3.Checked) + "' and " +
                                    //    objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);


                                    //if (ds.Tables[0].Rows.Count > 0)
                                    //{
                                    //    // Display message for Duplication
                                    //    //  lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //    //lblmsg.Text = "Record already exist";
                                    //    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    //}
                                    //}




                                    //=================04-07-2022==================start======
                                    //dispatch letter type
                                    //Added by : Shaikh Juned
                                    //Purpose : To avoid duplicate entries for Update Record.



                                    //DataSet ds1 = objMaster.AllMasters(objMaster._tablename, calnames[1] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' AND " + calnames[2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "'", objMaster.captions[0, 2]);
                                    //if (ds1.Tables[0].Rows.Count > 0)
                                    //{
                                    //    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    //    return;
                                    //}



                                    //=================04-07-2022==================end======
                                    //else
                                    //{
                                    #endregion

                                    int output1 = objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                                    if (output1.Equals(Convert.ToInt32(CustomStatus.RecordUpdated)))
                                    {
                                        ViewState["action"] = null;
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }
                                        // lblmsg.Text = "Record Updated Succesfully";

                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Updated Successfully", this.Page);
                                        ReloadTableData();
                                        //bhus   Response.Redirect(Request.Url.ToString());
                                    }
                                    else /// added by jay on 13/07/2023
                                    {

                                        objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                        ReloadTableData();
                                    }
                                    //}
                                }
                                /////////////////////////////////////////////////////
                                else if (objMaster.captions[0, 4] == "4")
                                {
                                    #region Comment
                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[2, 2] + "= '" + Convert.ToString(txt3.Text.Trim()) + "' and " + objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);

                                    //DataSet ds = objMaster.AllMasters(objMaster._tablename, objMaster.captions[3, 2] + "= '" + Convert.ToString(chk4.Checked) + "' and " + objMaster.captions[2, 2] + "= '" + Convert.ToString(txt3.Text.Trim()) + "' and " + objMaster.captions[1, 2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' and " + objMaster.captions[0, 2] + " = '" + Convert.ToString(txt1.Text.Trim()) + "' ", objMaster.captions[0, 3]);         //Add checkbox by sneha
                                    //if (ds.Tables[0].Rows.Count > 0)
                                    //{
                                    //    // Display message for Duplication
                                    //    //lblmsg.ForeColor = System.Drawing.Color.Red;
                                    //    //lblmsg.Text = "Record already exist";
                                    //    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    //}
                                    ////}
                                    //else
                                    //{
                                    #endregion

                                    int output1 = objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                                    if (output1.Equals(Convert.ToInt32(CustomStatus.RecordUpdated)))
                                    {
                                        ViewState["action"] = null;
                                        if (collen == 2)
                                        {
                                            txt1.Text = string.Empty;
                                            chk1.Checked = false;               //added by sneha G. on 23/05/2020
                                        }
                                        if (collen == 3)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            chk3.Checked = false;               //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 4)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            chk4.Checked = false;                //added by sneha G. on 21/05/2020
                                        }
                                        if (collen == 5)
                                        {
                                            txt1.Text = string.Empty;
                                            txt2.Text = string.Empty;
                                            txt3.Text = string.Empty;
                                            txt4.Text = string.Empty;
                                            chk3.Checked = false;
                                        }
                                        // lblmsg.Text = "Record Updated Succesfully";

                                        //  lblmsg.ForeColor = System.Drawing.Color.Green;
                                        objCommon.DisplayMessage(this.pnlAdd, "Record Updated Successfully", this.Page);
                                        ReloadTableData();
                                        //bhus   Response.Redirect(Request.Url.ToString());
                                    }
                                    else /// added by jay on 13/07/2023
                                    {

                                        objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                        ReloadTableData();
                                    }
                                    //}
                                }
                                //////////////////////////////////////////////////////////
                            }
                            else
                            {
                                int output1 = objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                                if (output1.Equals(Convert.ToInt32(CustomStatus.RecordUpdated)))
                                {
                                    ViewState["action"] = null;
                                    if (collen == 2)
                                    {
                                        txt1.Text = string.Empty;
                                        chk3.Checked = false;               //added by sneha G. on 23/05/2020
                                    }
                                    if (collen == 3)
                                    {
                                        txt1.Text = string.Empty;
                                        txt2.Text = string.Empty;
                                        chk1.Checked = false;               //added by sneha G. on 21/05/2020
                                    }
                                    if (collen == 4)
                                    {
                                        txt1.Text = string.Empty;
                                        txt2.Text = string.Empty;
                                        txt3.Text = string.Empty;
                                        chk3.Checked = false;                //added by sneha G. on 21/05/2020
                                    }
                                    if (collen == 5)
                                    {
                                        txt1.Text = string.Empty;
                                        txt2.Text = string.Empty;
                                        txt3.Text = string.Empty;
                                        txt4.Text = string.Empty;
                                        chk4.Checked = false;
                                    }
                                    // lblmsg.Text = "Record Updated Succesfully";

                                    // lblmsg.ForeColor = System.Drawing.Color.Green;
                                    objCommon.DisplayMessage(this.pnlAdd, "Record Updated Successfully", this.Page);
                                    ReloadTableData();
                                    //bhus   Response.Redirect(Request.Url.ToString());
                                }
                                else /// added by jay on 13/07/2023
                                {

                                    objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                    ReloadTableData();
                                }
                            }
                            //else
                            //{
                            //    lblmsg.ForeColor = System.Drawing.Color.Red;
                            //    lblmsg.Text = "Record already exist";
                            //}
                        }
                    }

                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Masters_masters.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblmsg.Text = string.Empty;



        Masters objMaster = new Masters(Request.QueryString["page"].ToString());
        int collen = int.Parse(objMaster.captions[0, 4]);
        string colvalue = string.Empty;



        if (collen == 1)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            txt1.Text = string.Empty;
        }
        else if (collen == 2)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            // TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            txt1.Text = string.Empty;
            //txt2.Text = string.Empty;
            CheckBox cb1 = phAdd.FindControl("chkColumn1") as CheckBox;  //02/03/2022
            cb1.Checked = true;

        }
        else if (collen == 3)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            //TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;  10/03/2022
            txt1.Text = string.Empty;
            txt2.Text = string.Empty;
            //txt3.Text = string.Empty; 10/03/2022



            if (objMaster.captions[2, 1].Equals("textbox"))    //10/03/2022
            {
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                txt3.Text = string.Empty;
            }
            else
            {
                CheckBox cb3 = phAdd.FindControl("chkColumn2") as CheckBox;
                cb3.Checked = true;
            }
        }
        else if (collen == 4)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            txt1.Text = string.Empty;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            txt2.Text = string.Empty;



            if (objMaster.captions[2, 1].Equals("textbox"))
            {
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                txt3.Text = string.Empty;
            }
            else
            {
                CheckBox cb3 = phAdd.FindControl("chkColumn2") as CheckBox;
                cb3.Checked = true;
            }



           // TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
           // txt4.Text = string.Empty;
        }
        else if (collen == 5)//Added by Jay T. On dated 14072023
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            txt1.Text = string.Empty;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            txt2.Text = string.Empty;



            if (objMaster.captions[2, 1].Equals("textbox"))
            {
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                txt3.Text = string.Empty;
            }
            else
            {
                CheckBox cb3 = phAdd.FindControl("chkColumn2") as CheckBox;
                cb3.Checked = true;
            }

            if (objMaster.captions[3, 1].Equals("textbox"))
            {
                TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
                txt4.Text = string.Empty;
            }
            //else
            //{
            //    CheckBox cb3 = phAdd.FindControl("chkColumn2") as CheckBox;
            //    cb3.Checked = true;
            //}

            // TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
            // txt4.Text = string.Empty;
        }



        ViewState["action"] = "add";
        ViewState["id"] = null;
    }

    //USED FOR FIND COLUMN IN GRID VIEW
    protected void gvmaster_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        ViewState["action"] = "edit";
        lblmsg.Text = "";
        GridView gv1 = phList.FindControl("gvmaster") as GridView;

        string index = Page.Request.Params["__EVENTARGUMENT"].ToString().Substring(Page.Request.Params["__EVENTARGUMENT"].ToString().IndexOf("$") + 1);
        ViewState["id"] = gv1.Rows[Convert.ToInt32(index)].Cells[1].Text.ToString();

        Masters objMaster = new Masters(Request.QueryString["page"].ToString());
        int collen = int.Parse(objMaster.captions[0, 4]);

        if (collen == 2)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            txt1.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[2].Text.ToString());

            //Added by Sneha G.
            if (objMaster.captions[1, 1].Equals("checkbox"))
            {
                CheckBox chk1 = phAdd.FindControl("chkColumn1") as CheckBox;
                if (gv1.Rows[Convert.ToInt32(index)].Cells[3].Text.ToString() == "Active")
                {
                    chk1.Checked = true;
                }
                else
                {
                    chk1.Checked = false;
                }
            }
        }

        else if (collen == 3)
        {
            //    TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            //    TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            //    txt1.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[2].Text.ToString());
            //    txt2.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[3].Text.ToString());

            //added by Sneha G.
            if (objMaster.captions[0, 1].Equals("textbox"))
            {
                TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                txt1.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[2].Text.ToString());
            }

            if (objMaster.captions[1, 1].Equals("textbox"))
            {
                TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                txt2.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[3].Text.ToString());
            }

            //Added by Sneha G.
            if (objMaster.captions[2, 1].Equals("checkbox"))
            {
                CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                if (gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.ToString() == "Active")
                {
                    chk3.Checked = true;
                }
                else
                {
                    chk3.Checked = false;
                }

                //chk3.Checked = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("1") ? true : false;
                //chk3.Text = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("1") ? "Yes" : "No";
            }
        }

        else if (collen == 4)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
            txt1.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[2].Text.ToString());
            txt2.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[3].Text.ToString());
            txt3.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.ToString());
            //Added by Sneha G.
            if (objMaster.captions[3, 1].Equals("checkbox"))
            {
                CheckBox chk4 = phAdd.FindControl("chkColumn3") as CheckBox;
                if (gv1.Rows[Convert.ToInt32(index)].Cells[5].Text.ToString() == "Active")
                {
                    chk4.Checked = true;
                }
                else
                {
                    chk4.Checked = false;
                }
            }
        }

        else if (collen == 5)
        {
            if (objMaster.captions[0, 1].Equals("textbox"))
            {
                TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                txt1.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[2].Text.ToString());
            }

            if (objMaster.captions[1, 1].Equals("textbox"))
            {
                TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                txt2.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[3].Text.ToString());
            }

            if (objMaster.captions[2, 1].Equals("textbox"))
            {
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                txt3.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.ToString());
            }
            else if (objMaster.captions[2, 1].Equals("checkbox"))
            {
                CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                chk3.Checked = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("Active") ? true : false;
                //chk3.Text = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("1") ? "Yes" : "No";
            }

            if (objMaster.captions[3, 1].Equals("textbox"))
            {
                TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
                txt4.Text = HttpUtility.HtmlDecode(gv1.Rows[Convert.ToInt32(index)].Cells[5].Text.ToString());
            }
        }
    }

    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = sender as CheckBox;
        if (chk.Checked == true)
            chk.Text = "Yes";
        else
            chk.Text = "No";
    }

    private void ReloadTableData()
    {
        Masters objMasters = new Masters(Request.QueryString["page"].ToString());
        //gvTitle.InnerHtml = Request.QueryString["pagetitle"].ToString () + " List";
        //lblPageTitle.Text = Request.QueryString["pagetitle"].ToString ().ToUpper () + " MANAGEMENT";



        //Add Dynamic Gridveiw
        GridView gv = new GridView(); ;
        if (Request.QueryString["head"] != null)
        {
            if (Request.QueryString["head"].Equals("gpf"))
                gv = objMasters.CreateGridView("app_for='G'");
            else if (Request.QueryString["head"].Equals("cpf"))
                gv = objMasters.CreateGridView("app_for='C'");
        }
        else
            gv = objMasters.CreateGridView(string.Empty);



        phList.Controls.Clear();
        phList.Controls.Add(gv);
        gv.RowCommand += new GridViewCommandEventHandler(gvmaster_RowCommand);
        gv.CssClass = "table table-striped table-bordered nowrap display";
        gv.HeaderStyle.CssClass = "bg-light-blue";



        // CHANGES BY SUMIT 10102019
        //gv.HeaderStyle.CssClass = "bg-light-blue";   //"bg-pista text-darkgray";



        //Add Dynamic Table
        phAdd.Controls.Clear();
        HtmlTable tbl = objMasters.CreateTableHTML();
        phAdd.Controls.Add(tbl);



        //by default checkbox true added logic below 13-09-2022
        GridView gv12 = phList.FindControl("gvmaster") as GridView; //



        Masters objMaster1 = new Masters(Request.QueryString["page"].ToString());//
        int collen = int.Parse(objMaster1.captions[0, 4]); //



        if (collen == 2)
        {
            CheckBox cb1 = phAdd.FindControl("chkColumn1") as CheckBox;
            cb1.Checked = true;
        }



        else if (collen == 3)
        {
            CheckBox cb2 = phAdd.FindControl("chkColumn2") as CheckBox;
            cb2.Checked = true;
        }



        else if (collen == 4)
        {
            CheckBox cb3 = phAdd.FindControl("chkColumn3") as CheckBox;
            cb3.Checked = true;
        }



        else if (collen == 5)
        {
            CheckBox cb2 = phAdd.FindControl("chkColumn2") as CheckBox;
            cb2.Checked = true;

        }



    }


}

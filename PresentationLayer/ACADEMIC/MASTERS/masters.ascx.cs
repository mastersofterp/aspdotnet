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

using System.Data.SqlClient;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

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
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["page"].ToString() != null)
            {
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

                    if (collen == 1)
                    {
                        TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;

                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'";
                            else
                                columnvalues = calnames[1] + "=" + txt1.Text.Trim() + "";

                            columnid = columnid + "=" + ViewState["id"];
                        }
                        else
                        {
                            //insert
                            

                            if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                columnvalues = "'" + txt1.Text + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                            else
                                columnvalues = txt1.Text + "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 2)
                    {
                        TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                        TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
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

                            //columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'" + "," + calnames[2] + "='" + txt2.Text.Trim() + "'";
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

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";

                            //columnvalues = "'" + txt1.Text.Trim() + "'" + "," + "'" + txt2.Text.Trim() + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 3)
                    {
                        TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                        TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                        TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;

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

                            //columnvalues = calnames[1] + "='" + txt1.Text.Trim() + "'" + "," + calnames[2] + "='" + txt2.Text.Trim() + "'" + "," + calnames[3] + "='" + txt3.Text.Trim() + "'";
                            columnid = columnid + "=" + ViewState["id"];
                        }
                        else
                        {
                            //insert
                            DataSet ds = objMaster.AllMasters(objMaster._tablename, calnames[1] + "= '" + Convert.ToString(txt1.Text.Trim()) + "' AND " + calnames[2] + "= '" + Convert.ToString(txt2.Text.Trim()) + "' AND " + calnames[3] + "= '" + Convert.ToString(txt3.Text.Trim()) + "'", objMaster.captions[0, 3]);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                objCommon.DisplayMessage(this.pnlAdd, "Record already exist", this.Page);
                                return;
                            }

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

                            columnvalues += "," + "'" + Session["colcode"].ToString() + "'";

                            //columnvalues = "'" + txt1.Text.Trim() + "'" + "," + "'" + txt2.Text.Trim() + "'" + "," + "'" + txt3.Text.Trim() + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                        }
                    }
                    else if (collen == 4)
                    {
                        if (ViewState["action"].ToString().Equals("edit"))
                        {
                            //columnnames = calnames[1] + "='" + txt1.Text + "'" + "," + calnames[2] + "='" + txt2.Text + "'" + "," + calnames[3] + "='" + txt3.Text + "'" + "," + calnames[4] + "='" + txt4.Text + "'";
                            columnid = columnid + "=" + ViewState["id"];

                            if (objMaster.captions[0, 1].Equals("textbox"))
                            {
                                TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                                if (objMaster.captions[0, 6].Equals("string") || objMaster.captions[0, 6].Equals("string2"))
                                    columnvalues += calnames[1] + "='" + txt1.Text.Trim() + "'";
                                else
                                    columnvalues += calnames[1] + "=" + txt1.Text.Trim();
                            }

                            if (objMaster.captions[1, 1].Equals("textbox"))
                            {
                                TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                                if (objMaster.captions[1, 6].Equals("string") || objMaster.captions[1, 6].Equals("string2"))
                                    columnvalues += "," + calnames[2] + "='" + txt2.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[2] + "=" + txt2.Text.Trim();
                            }

                            if (objMaster.captions[2, 1].Equals("textbox"))
                            {
                                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                                if (objMaster.captions[2, 6].Equals("string") || objMaster.captions[2, 6].Equals("string2"))
                                    columnvalues += "," + calnames[3] + "='" + txt3.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[3] + "=" + txt3.Text.Trim();
                            }
                            else if (objMaster.captions[2, 1].Equals("checkbox"))
                            {
                                CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                                if (chk3.Checked == true)
                                    columnvalues += "," + calnames[3] + "=1";
                                else
                                    columnvalues += "," + calnames[3] + "=0";
                            }

                            if (objMaster.captions[3, 1].Equals("textbox"))
                            {
                                TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
                                if (objMaster.captions[3, 6].Equals("string") || objMaster.captions[3, 6].Equals("string2"))
                                    columnvalues += "," + calnames[4] + "='" + txt4.Text.Trim() + "'";
                                else
                                    columnvalues += "," + calnames[4] + "=" + txt4.Text.Trim();
                            }
                        }
                        else
                        {
                            //To insertAllMasters

                            //columnvalues = "'" + txt1.Text + "'" + "," + "'" + txt2.Text + "'" + "," + "'" + txt3.Text + "'" + "," + "'" + txt4.Text + "'" + "," + "'" + Session["colcode"].ToString() + "'";
                            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                            TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                            TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;

                            CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                          
                            
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
                            else if (objMaster.captions[2, 1].Equals("checkbox"))
                            {
                               
                                if (chk3.Checked == true)
                                    columnvalues += "," + "1";
                                else
                                    columnvalues += "," + "0";
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

                    //if (ViewState["action"].ToString().Equals("add"))
                    //{
                    //    //Add New Masters Information
                    //    CustomStatus cs = (CustomStatus)objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                    //    if (cs.Equals(CustomStatus.RecordUpdated))
                    //    {
                    //        lblmsg.Text = "Record Saved Successfully";
                    //        Response.Redirect(Request.Url.ToString());
                    //    }
                    //    else
                    //        lblmsg.Text = "Server Error";
                            
                    //}
                    if (ViewState["action"].ToString().Equals("add"))
                    {
                        //Add New Masters Information
                        int output = objMaster.AddMaster(Request.QueryString["page"].ToString(), columnnames, columnid, columnvalues);
                        if (output.Equals(Convert.ToInt32(CustomStatus.RecordSaved)))
                        {

                            Response.Redirect(Request.Url.ToString());
                            lblmsg.Text = "Record Saved Successfully";
                        }
                        else
                            lblmsg.Text = "Record already exist";
                    }
                    else
                    {
                       // Update Masters Information
                        int output1 = objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                        if (output1.Equals(Convert.ToInt32(CustomStatus.RecordUpdated)))
                        {
                            ViewState["action"] = null;
                            lblmsg.Text = "Record Updated Successfully";
                            Response.Redirect(Request.Url.ToString());
                         
                        }
                        else
                        {
                            lblmsg.Text = "Record already exist";
                        }
                        
                    //    //Update Masters Information

                    //    CustomStatus cs = (CustomStatus)objMaster.UpdateMaster(Request.QueryString["page"].ToString(), columnid, columnvalues);
                    //    if (cs.Equals(CustomStatus.RecordSaved))
                    //    {
                    //        ViewState["action"] = null;
                    //        lblmsg.Text = "Record Updated Successfully";
                    //        Response.Redirect(Request.Url.ToString());
                    //    }
                    //    else
                    //        lblmsg.Text = "Server Error";
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
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            txt1.Text = string.Empty;
            txt2.Text = string.Empty;
        }
        else if (collen == 3)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
            txt1.Text = string.Empty;
            txt2.Text = string.Empty;
            txt3.Text = string.Empty;
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
                cb3.Checked = false;
            }

            TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;                       
            txt4.Text = string.Empty;
        }

        ViewState["action"] = "add";
        ViewState["id"] = null;
    }

    protected void gvmaster_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        ViewState["action"] = "edit";
        GridView gv1 = phList.FindControl("gvmaster") as GridView;

        string index = Page.Request.Params["__EVENTARGUMENT"].ToString().Substring(Page.Request.Params["__EVENTARGUMENT"].ToString().IndexOf("$") + 1);
        ViewState["id"] = gv1.Rows[Convert.ToInt32(index)].Cells[1].Text;

        Masters objMaster = new Masters(Request.QueryString["page"].ToString());
        int collen = int.Parse(objMaster.captions[0, 4]);

        if (collen == 1)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            txt1.Text = gv1.Rows[Convert.ToInt32(index)].Cells[2].Text;
        }
        else if (collen == 2)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            txt1.Text = gv1.Rows[Convert.ToInt32(index)].Cells[2].Text;
            txt2.Text = gv1.Rows[Convert.ToInt32(index)].Cells[3].Text;
        }
        else if (collen == 3)
        {
            TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
            TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
            TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
            txt1.Text = gv1.Rows[Convert.ToInt32(index)].Cells[2].Text;
            txt2.Text = gv1.Rows[Convert.ToInt32(index)].Cells[3].Text;
            txt3.Text = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text;
        }
        else if (collen == 4)
        {
            if (objMaster.captions[0, 1].Equals("textbox"))
            {
                TextBox txt1 = phAdd.FindControl("txtColumn0") as TextBox;
                txt1.Text = gv1.Rows[Convert.ToInt32(index)].Cells[2].Text;
            }

            if (objMaster.captions[1, 1].Equals("textbox"))
            {
                TextBox txt2 = phAdd.FindControl("txtColumn1") as TextBox;
                txt2.Text = gv1.Rows[Convert.ToInt32(index)].Cells[3].Text;
            }

            if (objMaster.captions[2, 1].Equals("textbox"))
            {
                TextBox txt3 = phAdd.FindControl("txtColumn2") as TextBox;
                txt3.Text = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text;
            }
            else if (objMaster.captions[2, 1].Equals("checkbox"))
            {
                CheckBox chk3 = phAdd.FindControl("chkColumn2") as CheckBox;
                chk3.Checked = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("1") ? true : false;
                //chk3.Text = gv1.Rows[Convert.ToInt32(index)].Cells[4].Text.Equals("1") ? "Yes" : "No";
            }

            if (objMaster.captions[3, 1].Equals("textbox"))
            {
                TextBox txt4 = phAdd.FindControl("txtColumn3") as TextBox;
                txt4.Text = gv1.Rows[Convert.ToInt32(index)].Cells[5].Text;
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

    

}

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class ACADEMIC_BranchWiseIntake : System.Web.UI.Page
{


    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    StudentController objSC = new StudentController();
    StudentRegist objSR = new StudentRegist();
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
                this.CheckPageAuthorization();
                //Load Page Help
                if (Request.QueryString["pageno"] != null)
                {
                    // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }
            }
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "", "BATCHNO DESC");
            ddlAdmBatch.SelectedIndex = 1;
            PopulateDropDownList();
            BindListView();
            //AdvanceConfigBind();
            pnlAdvanceConfig.Visible = false;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BranchWiseIntake.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BranchWiseIntake.aspx");
        }
    }
    protected void BindListView()
    {
        try
        {
            int AdmBatch = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            DataSet ds = objSC.GetBranchWiseIntakeData(AdmBatch);

            if (ds.Tables[0].Rows.Count > 0)
            {
                PanelIntake.Visible = true;
                lvBranchWiseIntake.DataSource = ds.Tables[0];
                lvBranchWiseIntake.DataBind();
            }
            else
            {
                PanelIntake.Visible = false;
                lvBranchWiseIntake.DataSource = null;
                lvBranchWiseIntake.DataBind();
                objCommon.DisplayMessage(this.Page, "Record Not Found", Page);
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BranchWiseIntake.Bind-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENO");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "BranchWiseIntake.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
        txtIntake.Text = string.Empty; 
        AdvanceConfigBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    public void ClearData()
    {
        // ddlAdmBatch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlBranch.SelectedIndex = 0;
        txtIntake.Text = string.Empty;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int BATCHNO = Convert.ToInt32(ddlAdmBatch.SelectedValue);
            int DEGREENO = Convert.ToInt32(ddlDegree.SelectedValue);
            int BRANCHNO = Convert.ToInt32(ddlBranch.SelectedValue);
            string INTAKE = txtIntake.Text;
            int CREATED_BY = Convert.ToInt32(Session["userno"]);
            int MODIFY_BY = Convert.ToInt32(Session["userno"]);
            string IP_ADDRESS = Session["ipAddress"].ToString();
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                //Edit 
                int id = Convert.ToInt32(ViewState["id"]);

                CustomStatus cs = (CustomStatus)objSC.UpdateBranchWiseIntakeData(id, BATCHNO, DEGREENO, BRANCHNO, INTAKE, MODIFY_BY);

                if (ViewState["check"] != null && ViewState["check"].ToString().Equals("checktrue"))
                {
                    string categorys = string.Empty;
                    string paymentypes = string.Empty;
                    string totals = string.Empty;
                    foreach (ListViewDataItem lvitem in lvAdvanceConfig.Items)
                    {

                        CheckBox chk = ((CheckBox)lvitem.FindControl("chkAccept"));
                        if (chk.Checked)
                        {
                            string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                            string[] arr_Intake = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10", "hdf11", "hdf12", "hdf13", "hdf14", "hdf15", "hdf16", "hdf17", "hdf18", "hdf19", "hdf20" };

                            int colcount = Convert.ToInt32(ViewState["colcount"].ToString());
                            int newcolcount = colcount + 2;
                            for (int i = 0; i < newcolcount; i++)
                            {
                                if (i == newcolcount - 1)
                                {
                                    i = i + 3;
                                }
                                TextBox txtRule_1 = (TextBox)lvitem.FindControl(arr[i]);

                                if (txtRule_1.Text != string.Empty)
                                {
                                    categorys += Convert.ToString(chk.ToolTip) + ",";
                                    paymentypes += ((HiddenField)lvitem.FindControl(arr_Intake[i])).Value + ",";
                                    totals += Convert.ToString(Convert.ToDecimal(txtRule_1.Text) + ",");
                                }
                            }
                        }
                    }
                    string CATEGORY = categorys.TrimEnd(',');
                    string PAYMENTTYPE = paymentypes.TrimEnd(',');
                    string TOTAL = totals.TrimEnd(',');

                    string intakeid = ViewState["intakeid"].ToString();
                    string AdmBatch = ViewState["AdmBatch"].ToString();
                    string Degreeno = ViewState["Degreeno"].ToString();
                    string Branchno = ViewState["Branchno"].ToString();

                    CustomStatus csconfig = (CustomStatus)objSC.InsertBranchwiseintakeforadvnceconfig(Convert.ToInt32(AdmBatch), Convert.ToInt32(Degreeno), Convert.ToInt32(Branchno), CATEGORY, PAYMENTTYPE, TOTAL, Convert.ToInt32(intakeid));

                }

                if (cs.Equals(CustomStatus.RecordUpdated))
                {
                    ClearData();
                    BindListView();
                    pnlAdvanceConfig.Visible = false;
                    objCommon.DisplayMessage(this, "Record Updated sucessfully", this.Page);
                    ViewState["action"] = null;
                    ViewState["check"] = null;
                }
                ddlAdmBatch.Enabled = true;
                ddlDegree.Enabled = true;
                ddlBranch.Enabled = true;
            }
            else
            {

                CustomStatus cs = (CustomStatus)objSC.InsertBranchWiseIntakeData(BATCHNO, DEGREENO, BRANCHNO, INTAKE, CREATED_BY, IP_ADDRESS);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(this, "Record Saved sucessfully", this.Page);
                    BindListView();
                    ClearData();
                }

                else
                {
                    objCommon.DisplayMessage(this, "Record Already Exist", this.Page);
                    ClearData();
                }


                BindListView();
                pnlAdvanceConfig.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BranchWiseIntake.btnSave_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ID = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["id"] = Convert.ToInt32(btnEdit.CommandArgument);
            ViewState["intakeid"] = Convert.ToInt32(btnEdit.CommandArgument);
            ShowDetail(ID);
            ViewState["action"] = "edit";
            ddlAdmBatch.Enabled = false;
            ddlDegree.Enabled = false;
            ddlBranch.Enabled = false;
            AdvanceConfigBind();
            pnlAdvanceConfig.Visible = true;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "BranchWiseIntake.btnEdit_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowDetail(int ID)
    {

        DataSet ds = objSC.EditBranchWiseIntakeData(ID);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {

            if (ds.Tables[0].Rows[0]["BATCHNO"] == null | ds.Tables[0].Rows[0]["BATCHNO"].ToString().Equals(""))
            {
                ddlAdmBatch.SelectedIndex = 0;
                ViewState["AdmBatch"] = "0";
            }
            else
            {
                ddlAdmBatch.Text = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
                ViewState["AdmBatch"] = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
            }
            int AdmBatch = int.Parse(ds.Tables[0].Rows[0]["BATCHNO"].ToString());

            if (ds.Tables[0].Rows[0]["DEGREENO"] == null | ds.Tables[0].Rows[0]["DEGREENO"].ToString().Equals(""))
            {
                ddlDegree.SelectedIndex = 0;
                ViewState["Degreeno"] = "0";
            }
            else
            {
                ddlDegree.Text = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
                ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
            }
            int Degree = int.Parse(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
            int branch = int.Parse(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
            objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
            ddlBranch.SelectedValue = branch.ToString();
            ViewState["Branchno"] = branch.ToString();
            txtIntake.Text = ds.Tables[0].Rows[0]["INTAKE"].ToString();
        }
    }

    protected void ddlAdmBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListView();
        //AdvanceConfigBind();
        pnlAdvanceConfig.Visible = false;
        ClearData();
    }

    #region
    public void AdvanceConfigBind()
    {
        try
        {
            int index;
            DataTable dt = new DataTable();

            dt.Columns.Add("Category");
            dt.Columns.Add("CATEGORYNO");
            string SP_Name2 = "PKG_GET_ACD_CATEGORY_BRANCHWISE";
            string SP_Parameters2 = "@P_CATEGORYNO";
            string Call_Values2 = "0";
            DataSet ds = objCommon.DynamicSPCall_Select(SP_Name2, SP_Parameters2, Call_Values2);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["CATEGORY"] = ds.Tables[0].Rows[i]["CATEGORY"].ToString();
                    dr["CATEGORYNO"] = ds.Tables[0].Rows[i]["CATEGORYNO"].ToString();
                    dt.Rows.Add(dr);
                }
            }



            string SP_Name3 = "PKG_GET_ACD_PAYMENTTYPE_BRANCHWISE";
            string SP_Parameters3 = "@P_PAYTYPENO";
            string Call_Values3 = "0";
            DataSet dshead = objCommon.DynamicSPCall_Select(SP_Name3, SP_Parameters3, Call_Values3);

            ViewState["dshead"] = dshead;
            DataTable dthead = new DataTable();
            if (dshead != null && dshead.Tables.Count > 0 && dshead.Tables[0].Rows.Count > 0)
            {
                dthead = dshead.Tables[0];
                DataRow[] dr = dthead.Select("");
                string str = string.Empty;
                string str1 = string.Empty;
                int td = 0;
                int colcont = dshead.Tables[0].Columns.Count;
                ViewState["colcount"] = colcont.ToString();
                int rule1 = colcont + 1;

                for (int i = 0; i < colcont; i++)
                {
                    str += "$('td:nth-child(1)').show();$('td:nth-child(2)').show();$('#tbl_Rule1').attr('colspan'," + colcont + ");$('#th" + i + "').text('" + Convert.ToString(dr[0][i]).ToString() + "');$('#th" + i + "').text.length==0?$('#th" + i + "').hide():$('#th" + i + "').show();";
                }
                int z = 4;
                for (int j = 0; j < colcont; j++)
                {

                    str1 += "$('#th" + j + "').text('" + Convert.ToString(dr[0][j]).ToString() + "');$('#th" + j + "').text.length==0?$('td:nth-child(" + z + ")').hide():$('td:nth-child(" + z + ")').show();";
                    z++;
                }

                string str3 = str + str1;
                ViewState["headerscript"] = str3.ToString();//str+str1.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + str3 + "", true);
            }
            else
            {
                objCommon.DisplayMessage(this.updBatch, "No Payment types are Created,Kindly Create and Define Payment types", this.Page);
                lvAdvanceConfig.DataSource = null;
                lvAdvanceConfig.DataBind();
                lvAdvanceConfig.Visible = false;
                return;
            }

            string intakeid = ViewState["intakeid"].ToString();
            string AdmBatch = ViewState["AdmBatch"].ToString();
            string Degreeno = ViewState["Degreeno"].ToString();
            string Branchno = ViewState["Branchno"].ToString();

            DataSet dsbind = null;
            dsbind = objSC.GetBranchwiseintakeforadvnceconfig(Convert.ToInt32(AdmBatch), Convert.ToInt32(Degreeno), Convert.ToInt32(Branchno));



            if (dsbind != null && dsbind.Tables.Count > 0)
            {
                if (dsbind.Tables[0].Rows.Count > 0)
                {
                    lvAdvanceConfig.DataSource = dsbind.Tables[0];
                    lvAdvanceConfig.DataBind();
                    lvAdvanceConfig.Visible = true;
                    pnlAdvanceConfig.Visible = true;
                    dvAdvanceConfig.Visible = true;
                }
                else
                {
                    lvAdvanceConfig.DataSource = null;
                    lvAdvanceConfig.DataBind();
                    lvAdvanceConfig.Visible = false;
                }


                int arrVal = 0;
                string[] arr_TextBox = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                string[] arr_HiddenField = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10", "hdf11", "hdf12", "hdf13", "hdf14", "hdf15", "hdf16", "hdf17", "hdf18", "hdf19", "hdf20" };

                int i = 0;
                foreach (ListViewDataItem lvitem in lvAdvanceConfig.Items)
                {
                    for (; i < dsbind.Tables[0].Rows.Count; )
                    {
                        for (int j = 1; j < dsbind.Tables[0].Columns.Count; j++)
                        {
                            ((HiddenField)lvitem.FindControl(arr_HiddenField[arrVal])).Value = Convert.ToString(dsbind.Tables[0].Columns[j].ColumnName);
                            if (Convert.ToString(dsbind.Tables[0].Rows[i][j]) != "")
                            {
                                //int CL = Convert.ToInt32(dsbind.Tables[0].Columns[j].ColumnName);
                                ((TextBox)lvitem.FindControl(arr_TextBox[arrVal])).Text = Convert.ToString(dsbind.Tables[0].Rows[i][j]) != "0" ? Convert.ToString(dsbind.Tables[0].Rows[i][j]) : "0";
                                //((HiddenField)lvitem.FindControl(arr_HiddenField[arrVal])).Value = Convert.ToString(dsbind.Tables[0].Columns[j].ColumnName);
                                arrVal++;

                                if (j + 2 > Convert.ToInt32(dsbind.Tables[0].Columns.Count))
                                {
                                    arrVal = 0;
                                    break;
                                }
                            }
                        }
                        i++;
                        break;
                    }
                    arrVal = 0;

                    CheckBox chk = (CheckBox)lvitem.FindControl("chkAccept");
                    if (chk.Enabled == false)
                    {
                        chk.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void chkAccept_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["check"] = "checktrue";

            ScriptManager.RegisterStartupScript(this, GetType(), "YourUniqueScriptKey", "" + ViewState["headerscript"].ToString() + "", true);
            CheckBox chk = sender as CheckBox;

            foreach (ListViewDataItem lvitem in lvAdvanceConfig.Items)
            {

                CheckBox chk1 = ((CheckBox)lvitem.FindControl("chkAccept"));

                //TextBox txtrule2 = ((TextBox)lvitem.FindControl("txtTotal"));

                if (chk1.Checked)
                {
                    //  txtrule2.Enabled = true;
                    // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                    string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                    for (int i = 0; i <= Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                    {
                        if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                        {
                            //   i = i + 3;
                        }
                        TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                        txt.Enabled = true;

                    }

                }
                else
                {
                    //  txtrule2.Enabled = false;
                    // string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txtrule2" };

                    string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
                    for (int i = 0; i <= Convert.ToInt32(ViewState["colcount"].ToString()); i++)
                    {
                        if (i == Convert.ToInt32(ViewState["colcount"].ToString()) - 1)
                        {
                            //  i = i + 3;
                        }
                        TextBox txt = (TextBox)lvitem.FindControl(arr[i]);
                        txt.Enabled = false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    //protected void btnSaveAdvance_Click(object sender, EventArgs e)
    //{
    //    string categorys = string.Empty;
    //    string paymentypes = string.Empty;
    //    string totals = string.Empty;
    //    foreach (ListViewDataItem lvitem in lvAdvanceConfig.Items)
    //    {

    //        CheckBox chk = ((CheckBox)lvitem.FindControl("chkAccept"));
    //        if (chk.Checked)
    //        {
    //            string[] arr = new string[] { "txtCat1", "txtCat1asn", "txtCat2", "txtCat2asn", "txtCat3", "txtCat3asn", "txt7", "txt8", "txt9", "txt10", "txt11", "txt12", "txt13", "txt14", "txt15", "txt16", "txt17", "txt18", "txt19", "txt20" };
    //            string[] arr_Intake = new string[] { "hdfCat1", "hdfCat1_Asign", "hdfCat2", "hdfCat2_Asign", "hdfCat3", "hdfCat3_Asign", "hdf7", "hdf8", "hdf9", "hdf10", "hdf11", "hdf12", "hdf13", "hdf14", "hdf15", "hdf16", "hdf17", "hdf18", "hdf19", "hdf20" };

    //            int colcount = Convert.ToInt32(ViewState["colcount"].ToString());
    //            int newcolcount = colcount + 2;
    //            for (int i = 0; i < newcolcount; i++)
    //            {
    //                if (i == newcolcount - 1)
    //                {
    //                    i = i + 3;
    //                }
    //                TextBox txtRule_1 = (TextBox)lvitem.FindControl(arr[i]);

    //                if (txtRule_1.Text != string.Empty)
    //                {
    //                    categorys += Convert.ToString(chk.ToolTip) + ",";
    //                    paymentypes += ((HiddenField)lvitem.FindControl(arr_Intake[i])).Value + ",";
    //                    totals += Convert.ToString(Convert.ToDecimal(txtRule_1.Text) + ",");
    //                }
    //            }
    //        }
    //    }
    //    string CATEGORY = categorys.TrimEnd(',');
    //    string PAYMENTTYPE = paymentypes.TrimEnd(',');
    //    string TOTAL = totals.TrimEnd(',');

    //    string intakeid = ViewState["intakeid"].ToString();
    //    string AdmBatch = ViewState["AdmBatch"].ToString();
    //    string Degreeno = ViewState["Degreeno"].ToString();
    //    string Branchno = ViewState["Branchno"].ToString();

    //    CustomStatus cs = (CustomStatus)objSC.InsertBranchwiseintakeforadvnceconfig(Convert.ToInt32(AdmBatch), Convert.ToInt32(Degreeno), Convert.ToInt32(Branchno), CATEGORY, PAYMENTTYPE, TOTAL, Convert.ToInt32(intakeid));

    //    if (cs.Equals(CustomStatus.RecordSaved))
    //    {
    //        objCommon.DisplayMessage(this.Page, "Record Saved Successfully", Page);
    //    }
    //    AdvanceConfigBind();
    //}
    #endregion
    //protected void btnEditAdvance_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        ImageButton btnEditAdvance_Click = sender as ImageButton;
    //        int ID = Convert.ToInt32(btnEditAdvance_Click.CommandArgument);
    //        ViewState["intakeid"] = Convert.ToInt32(btnEditAdvance_Click.CommandArgument);
    //        ShowDetailAdvance(ID);
    //        ViewState["action"] = "editadvance";
    //        AdvanceConfigBind();
    //        pnlAdvanceConfig.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "BranchWiseIntake.btnEditAdvance_Click() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}
    //private void ShowDetailAdvance(int ID)
    //{

    //    DataSet ds = objSC.EditBranchWiseIntakeData(ID);
    //    if (ds != null && ds.Tables[0].Rows.Count > 0)
    //    {

    //        if (ds.Tables[0].Rows[0]["BATCHNO"] == null | ds.Tables[0].Rows[0]["BATCHNO"].ToString().Equals(""))
    //            ViewState["AdmBatch"] = "0";
    //        else
    //            ViewState["AdmBatch"] = ds.Tables[0].Rows[0]["BATCHNO"].ToString();
    //        int AdmBatch = int.Parse(ds.Tables[0].Rows[0]["BATCHNO"].ToString());

    //        if (ds.Tables[0].Rows[0]["DEGREENO"] == null | ds.Tables[0].Rows[0]["DEGREENO"].ToString().Equals(""))
    //            ViewState["Degreeno"] = "0";
    //        else
    //            ViewState["Degreeno"] = ds.Tables[0].Rows[0]["DEGREENO"].ToString();
    //        int Degree = int.Parse(ds.Tables[0].Rows[0]["DEGREENO"].ToString());
    //        int branch = int.Parse(ds.Tables[0].Rows[0]["BRANCHNO"].ToString());
    //        objCommon.FillDropDownList(ddlBranch, "ACD_COLLEGE_DEGREE_BRANCH CD INNER JOIN ACD_BRANCH B ON (B.BRANCHNO = CD.BRANCHNO)", "DISTINCT CD.BRANCHNO", "B.LONGNAME", "CD.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue), "B.LONGNAME");
    //        ViewState["Branchno"] = branch.ToString();
    //    }
    //}
}

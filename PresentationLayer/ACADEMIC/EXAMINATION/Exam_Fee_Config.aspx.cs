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
using System.IO;

public partial class ACADEMIC_EXAMINATION_Exam_Fee_Config : System.Web.UI.Page
{
    Common objCommon = new Common();
    ExamController Exm = new ExamController();
    UAIMS_Common objUCommon = new UAIMS_Common();
    LateFeeController objLFC = new LateFeeController();

    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

    //int LateFeesMode = 0;
    //int Loop = 0;

    #region Page Events

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
                    //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                //if (chkLateFee.Checked)
                //{
                //    txtLateFee.Visible = true;
                //}
                //else
                //{
                //    txtLateFee.Visible = false;
                //}

                // Page.ClientScript.RegisterStartupScript(this.GetType(), "Reg", "clickLateFee();", true);
                //ddlLateFee.SelectedIndex = 0;

                //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);
                //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Reg", "clickLateFee();", true);
            ViewState["uano"] = Convert.ToInt32(Session["userno"]);
            //txtLateFee.Visible = false;

            //objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER C INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CD ON (CD.COLLEGE_ID=C.COLLEGE_ID)", "DISTINCT (C.COLLEGE_ID)", "C.COLLEGE_NAME", "C.COLLEGE_ID > 0", "C.COLLEGE_NAME");
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_SCHEME_MAPPING SM INNER JOIN ACD_COLLEGE_DEGREE_BRANCH DB ON (SM.OrganizationId = DB.OrganizationId AND SM.DEGREENO = DB.DEGREENO AND SM.BRANCHNO = DB.BRANCHNO AND SM.COLLEGE_ID = DB.COLLEGE_ID)", "COSCHNO", "COL_SCHEME_NAME", "SM.COLLEGE_ID IN(" + Session["college_nos"] + ") AND COSCHNO>0 AND SM.COLLEGE_ID > 0 AND SM.OrganizationId=" + Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]) + " AND DB.DEPTNO IN(" + Session["userdeptno"].ToString() + ")", "");

            //objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");
            //objCommon.FillListBox(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO>0", "DEGREENAME");

            objCommon.FillListBox(lstSemester, "ACd_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
            objCommon.FillDropDownList(ddlExamType, "ACD_EXAM_TYPE", "EXAM_TYPENO", "EXAM_TYPE", "EXAM_TYPENO>=0", "EXAM_TYPENO");
            ddlExamType.SelectedItem.Value = "-1";

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");
            //objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO desc");

            Load();

            ddlCollege.Focus();
        }

        //ScriptManager.RegisterStartupScript(this, GetType(), "Reg", "clickLateFee();", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Reg", "clickLateFee();", true);
        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);

        ViewState["IPADDRESS"] = Request.ServerVariables["REMOTE_ADDR"];
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page

            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=Exam_Fee_Config.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Exam_Fee_Config.aspx");
        }
    }

    #endregion

    protected void btnDel_Click(object sender, ImageClickEventArgs e)
    {
        //lstSemester.SelectedValue = null;
        ImageButton btnDelete = sender as ImageButton;
        int FID = int.Parse(btnDelete.CommandArgument);
        SqlDataReader dr = Exm.GetFeeDetails(FID);
        if (dr != null)
        {
            if (dr.Read())
            {

                int ClgId = Convert.ToInt32(dr["college_id"]);
                //ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                int Sessionno = Convert.ToInt32(dr["sessionno"]);
                int FeeType = Convert.ToInt32(dr["FEETYPE"]);
                int Degreeno = Convert.ToInt32(dr["degreeno"]);
                string txt = dr["ApplicableFee"].ToString();
                int FeeStru = Convert.ToInt32(dr["FEESTRUCTURE_TYPE"]);
                if (txtconformmessageValue.Value == "Yes")
                {
                    CustomStatus cs = (CustomStatus)Exm.FeeDelete(ClgId, Sessionno, FeeType, Degreeno, FeeStru);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {

                        objCommon.DisplayMessage(this, "Record Cancel Successfully...!!!", this.Page);
                        Load();
                        lvFee.Visible = false;
                        divCredit.Visible = false;
                        divCourse.Visible = false;
                        lvSem.Visible = false;
                        divRenge.Visible = false;
                        return;


                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Something Went Wrong...!!!", this.Page);
                        Load();
                        lvFee.Visible = false;
                        divCredit.Visible = false;
                        divCourse.Visible = false;
                        lvSem.Visible = false;
                        divRenge.Visible = false;
                        return;

                    }


                }
                else
                {
                    //objCommon.DisplayMessage(this, "Something went Wrong..", this.Page);
                    Load();
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                    divRenge.Visible = false;
                    return;

                }
            }
        }



    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        lstSemester.SelectedValue = null;
        ImageButton btnEdit = sender as ImageButton;
        int FID = int.Parse(btnEdit.CommandArgument);

        SqlDataReader dr = Exm.GetFeeDetails(FID);

        if (dr != null)
        {
            if (dr.Read())
            {
                ddlCollege.SelectedValue = dr["college_id"].ToString();
                ddlCollege_SelectedIndexChanged(new object(), new EventArgs());
                ddlSession.SelectedValue = dr["sessionno"].ToString();
                ddlExamType.SelectedValue = dr["FEETYPE"].ToString();
                ddlDegree.SelectedValue = dr["degreeno"].ToString();
                txtYes.Text = dr["ApplicableFee"].ToString();
                ddlFeesStructure.SelectedValue = dr["FEESTRUCTURE_TYPE"].ToString();

                if (Convert.ToBoolean(dr["IsFeesApplicable"]) == true)
                {
                    rdActive.Checked = true;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "onoff(true);", true);
                }
                else
                {
                    rdActive.Checked = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "onoff(false);", true);
                }

                if (Convert.ToBoolean(dr["IsProFeesApplicable"]) == true)
                {
                    test.Checked = true;
                    // txtProcess.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "document.getElementById('ctl00_ContentPlaceHolder1_txtProcess').style.display = 'block'", true);

                }
                else
                {
                    test.Checked = false;
                    // txtProcess.Visible = false; 
                    ScriptManager.RegisterStartupScript(this, GetType(), "Src", "document.getElementById('ctl00_ContentPlaceHolder1_txtProcess').style.display = 'none'", true);
                }

                if (ddlFeesStructure.SelectedIndex != 4)
                {
                    string[] Tempsemester = dr["semesterno"].ToString().Split(',');

                    foreach (ListItem items in lstSemester.Items)
                    {
                        foreach (string Semester in Tempsemester)
                        {
                            if (items.Value == Semester)
                            {
                                items.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    fsem.Visible = false;
                }

                if (ddlFeesStructure.SelectedIndex == 1)
                {
                    BindListView();
                    btnSubmit.Visible = true;
                    //lvFee.Visible = true;
                }
                else if (ddlFeesStructure.SelectedIndex == 2)
                {
                    //divCredit.Visible = true;
                    btnSubmit.Visible = true;
                    string Chk1 = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDITFEE>0 and isnull(CANCEL,0) = 0");

                    if ((Chk1 != null || Chk1 != string.Empty) && Chk1 != "0")
                    {
                        DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "CREDITFEE", "ApplicableFee", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDITFEE>0 and isnull(CANCEL,0) = 0", "");

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtCredit.Text = Convert.ToString(ds.Tables[0].Rows[0]["CREDITFEE"]);
                            txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                        }
                    }
                    else
                    {
                        divCredit.Visible = true;
                        txtCredit.Text = "0";
                    }

                    divCredit.Visible = true;
                    divCourse.Visible = false;
                    divFix.Visible = false;
                    lvSem.Visible = false;
                }
                else if (ddlFeesStructure.SelectedIndex == 3)
                {
                    btnSubmit.Visible = true;
                    string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COURSEFEE>0 and isnull(CANCEL,0) = 0");

                    if ((Chk != null || Chk != string.Empty) && Chk != "0")
                    {
                        DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "COURSEFEE", "ApplicableFee", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COURSEFEE>0 and isnull(CANCEL,0) = 0", "");

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtCourse.Text = Convert.ToString(ds.Tables[0].Rows[0]["COURSEFEE"]);
                            txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                        }
                    }
                    else
                    {
                        divCourse.Visible = true;
                        txtCourse.Text = "0";
                    }

                    divCourse.Visible = true;
                    divCredit.Visible = false;
                    divFix.Visible = false;
                    lvSem.Visible = false;
                    //divCourse.Visible = true;
                }
                else
                {
                    BindListView();
                    btnSubmit.Visible = true;
                    // lvSem.Visible = true;
                }
            }
        }
        if (dr != null) dr.Close();
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        if (ddlFeesStructure.SelectedIndex != 0)
        {
            if (ddlDegree.SelectedIndex == -1)
            {
                objCommon.DisplayMessage(this, "Please Select Degree.", this.Page);
                ddlDegree.Focus();

                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);

                return;
            }
        }

        if (ddlFeesStructure.SelectedIndex != 4)
        {
            if (lstSemester.SelectedIndex == -1)
            {
                objCommon.DisplayMessage(this, "Please Select Semester.", this.Page);
                lstSemester.Focus();

                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);

                return;
            }
        }

        btnSubmit.Visible = true;
        //pnlCopySession.Visible = true;
        //btnCopyData.Visible = true;
        lvFee.Visible = false;

        if (Convert.ToInt32(ddlFeesStructure.SelectedValue) == 1)
        {
            #region Course Type Wise

            BindListView();

            string Chk2 = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SUBID>0 and isnull(CANCEL,0) = 0");

            if ((Chk2 != null || Chk2 != string.Empty) && Chk2 != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "FID,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "ApplicableFee,CertificateFee,IsLateFeesApplicable,LateFeeMode,LateFeeDate,LateFeeAmount,ValuationFee,ValuationMaxFee,PAYMENT_MODE", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND SUBID>0 and isnull(CANCEL,0) = 0", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                    txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);

                    rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsFeesApplicable"]);
                    test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
                    chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);

                    chkLateFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsLateFeesApplicable"]);
                    ddlLateFee.SelectedValue = ds.Tables[0].Rows[0]["LateFeeMode"].ToString();
                    txtLateFeeDate.Text = ds.Tables[0].Rows[0]["LateFeeDate"].ToString();
                    txtLateFeeAmount.Text = ds.Tables[0].Rows[0]["LateFeeAmount"].ToString();
                    txtValuationFee.Text = ds.Tables[0].Rows[0]["ValuationFee"].ToString();
                    txtValuationMaxFee.Text = ds.Tables[0].Rows[0]["ValuationMaxFee"].ToString();
                    ddlPaymentMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

                    pnlCopySession.Visible = true;
                    btnCopyData.Visible = true;
                }
                else
                {
                    // txtCourse.Visible = false;
                    txtYes.Text = "0";
                    txtCerFee.Text = "0";
                }
            }
            else
            {
                pnlCopySession.Visible = false;
                btnCopyData.Visible = false;
            }

            //lvFee.Visible = false;
            divCredit.Visible = false;
            divCourse.Visible = false;
            divFix.Visible = false;
            lvSem.Visible = false;
            divRenge.Visible = false;

            #endregion
        }
        else if (Convert.ToInt32(ddlFeesStructure.SelectedValue) == 2)
        {
            #region Cedit Wise

            string Chk1 = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDITFEE>0 and isnull(CANCEL,0) = 0");

            if ((Chk1 != null || Chk1 != string.Empty) && Chk1 != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "CREDITFEE,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "ApplicableFee,CertificateFee,IsLateFeesApplicable,LateFeeMode,LateFeeDate,LateFeeAmount,ValuationFee,ValuationMaxFee,PAYMENT_MODE", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDITFEE>0 and isnull(CANCEL,0) = 0 ", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCredit.Text = Convert.ToString(ds.Tables[0].Rows[0]["CREDITFEE"]);
                    txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                    txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);
                    rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"]);
                    test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
                    chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);

                    chkLateFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsLateFeesApplicable"]);
                    ddlLateFee.SelectedValue = ds.Tables[0].Rows[0]["LateFeeMode"].ToString();
                    txtLateFeeDate.Text = ds.Tables[0].Rows[0]["LateFeeDate"].ToString();
                    txtLateFeeAmount.Text = ds.Tables[0].Rows[0]["LateFeeAmount"].ToString();
                    txtValuationFee.Text = ds.Tables[0].Rows[0]["ValuationFee"].ToString();
                    txtValuationMaxFee.Text = ds.Tables[0].Rows[0]["ValuationMaxFee"].ToString();
                    ddlPaymentMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

                    pnlCopySession.Visible = true;
                    btnCopyData.Visible = true;
                }
            }
            else
            {
                divCredit.Visible = true;
                txtCredit.Text = "0";
                //txtYes.Text = "0";
                //txtProcess.Visible = false;

                pnlCopySession.Visible = false;
                btnCopyData.Visible = false;
            }

            divCredit.Visible = true;
            divCourse.Visible = false;
            divFix.Visible = false;
            lvSem.Visible = false;
            divRenge.Visible = false;

            #endregion
        }
        else if (Convert.ToInt32(ddlFeesStructure.SelectedValue) == 3)
        {
            #region Course Wise

            string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COURSEFEE>0 and isnull(CANCEL,0) = 0"); //College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + "

            if ((Chk != null || Chk != string.Empty) && Chk != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "COURSEFEE,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "ApplicableFee,CertificateFee,IsLateFeesApplicable,LateFeeMode,LateFeeDate,LateFeeAmount,ValuationFee,ValuationMaxFee,PAYMENT_MODE", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND COURSEFEE>0 and isnull(CANCEL,0) = 0", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCourse.Text = Convert.ToString(ds.Tables[0].Rows[0]["COURSEFEE"]);
                    txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                    txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);
                    rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"]);
                    test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
                    chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);

                    chkLateFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsLateFeesApplicable"]);
                    ddlLateFee.SelectedValue = ds.Tables[0].Rows[0]["LateFeeMode"].ToString();
                    txtLateFeeDate.Text = ds.Tables[0].Rows[0]["LateFeeDate"].ToString();
                    txtLateFeeAmount.Text = ds.Tables[0].Rows[0]["LateFeeAmount"].ToString();
                    txtValuationFee.Text = ds.Tables[0].Rows[0]["ValuationFee"].ToString();
                    txtValuationMaxFee.Text = ds.Tables[0].Rows[0]["ValuationMaxFee"].ToString();
                    ddlPaymentMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

                    pnlCopySession.Visible = true;
                    btnCopyData.Visible = true;
                }
            }
            else
            {
                divCourse.Visible = true;
                txtCourse.Text = "0";
                //txtYes.Text = "0";
                //txtCerFee.Text = "0";

                pnlCopySession.Visible = false;
                btnCopyData.Visible = false;
            }


            divCourse.Visible = true;
            divCredit.Visible = false;
            divFix.Visible = false;
            lvSem.Visible = false;
            divRenge.Visible = false;

            #endregion
        }
        else if (Convert.ToInt32(ddlFeesStructure.SelectedValue) == 4)
        {
            #region Fix

            BindListView();

            string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND FEE>0 and FEESTRUCTURE_TYPE=4 and isnull(CANCEL,0) = 0");

            if ((Chk != null || Chk != string.Empty) && Chk != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "FID,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "ApplicableFee,CertificateFee,IsLateFeesApplicable,LateFeeMode,LateFeeDate,LateFeeAmount,ValuationFee,ValuationMaxFee,PAYMENT_MODE", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND FEE>0 and FEESTRUCTURE_TYPE=4 and isnull(CANCEL,0) = 0 ", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                    txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);
                    rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"]);
                    test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
                    chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);

                    chkLateFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsLateFeesApplicable"]);
                    ddlLateFee.SelectedValue = ds.Tables[0].Rows[0]["LateFeeMode"].ToString();
                    txtLateFeeDate.Text = ds.Tables[0].Rows[0]["LateFeeDate"].ToString();
                    txtLateFeeAmount.Text = ds.Tables[0].Rows[0]["LateFeeAmount"].ToString();
                    txtValuationFee.Text = ds.Tables[0].Rows[0]["ValuationFee"].ToString();
                    txtValuationMaxFee.Text = ds.Tables[0].Rows[0]["ValuationMaxFee"].ToString();
                    ddlPaymentMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

                    pnlCopySession.Visible = true;
                    btnCopyData.Visible = true;
                }
            }
            else
            {
                //txtYes.Text = "0";
                //txtCerFee.Text = "0";

                pnlCopySession.Visible = false;
                btnCopyData.Visible = false;
            }

            lvSem.Visible = true;
            divCourse.Visible = false;
            divCredit.Visible = false;
            lvFee.Visible = false;
            divRenge.Visible = false;

            #endregion
        }
        else
        {
            #region Credit Range Wise

            int College = Convert.ToInt32(ddlCollege.SelectedValue);
            int Session = Convert.ToInt32(ddlSession.SelectedValue);
            int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
            int Structure = Convert.ToInt32(ddlFeesStructure.SelectedValue);
            int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);
            string Fee = txtYes.Text.Trim();

            //********************Display Cert fee and Processing fee added on 15122022**************************

            string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " and FEESTRUCTURE_TYPE=5 and isnull(CANCEL,0) = 0");

            if ((Chk != null || Chk != string.Empty) && Chk != "0")
            {
                DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "FID,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "ApplicableFee,CertificateFee,IsLateFeesApplicable,LateFeeMode,LateFeeDate,LateFeeAmount,ValuationFee,ValuationMaxFee,PAYMENT_MODE", "College_id =" + Convert.ToInt32(ViewState["college_id"]) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  FEESTRUCTURE_TYPE=5 and isnull(CANCEL,0) = 0 ", "");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
                    txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);
                    rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"]);
                    test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
                    chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);

                    chkLateFee.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsLateFeesApplicable"] == DBNull.Value ? false : ds.Tables[0].Rows[0]["IsLateFeesApplicable"]);
                    ddlLateFee.SelectedValue = ds.Tables[0].Rows[0]["LateFeeMode"].ToString();
                    txtLateFeeDate.Text = ds.Tables[0].Rows[0]["LateFeeDate"].ToString();
                    txtLateFeeAmount.Text = ds.Tables[0].Rows[0]["LateFeeAmount"].ToString();
                    txtValuationFee.Text = ds.Tables[0].Rows[0]["ValuationFee"].ToString();
                    txtValuationMaxFee.Text = ds.Tables[0].Rows[0]["ValuationMaxFee"].ToString();
                    ddlPaymentMode.SelectedValue = ds.Tables[0].Rows[0]["PAYMENT_MODE"].ToString();

                    pnlCopySession.Visible = true;
                    btnCopyData.Visible = true;
                }
            }
            else
            {
                //txtYes.Text = "0";
                //txtCerFee.Text = "0";

                pnlCopySession.Visible = false;
                btnCopyData.Visible = false;
            }

            lvSem.Visible = false;
            divCourse.Visible = false;
            divCredit.Visible = false;
            lvFee.Visible = false;
            divRenge.Visible = false;

            //***************************************************************************************

            DataSet ds2 = Exm.GetRange(College, degreeno, ExamType, Session);

            if (ds2.Tables[0] != null)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lvrange.DataSource = ds2;
                    lvrange.DataBind();
                    divRenge.Visible = true;
                    lvrange.Visible = true;
                    btnadd.Visible = true;
                    // SetInitialRow();
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                }
                else
                {
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                    divRenge.Visible = true;
                    lvrange.Visible = true;
                    btnadd.Visible = true;
                    SetInitialRow();
                }
            }
            else
            {
                lvFee.Visible = false;
                divCredit.Visible = false;
                divCourse.Visible = false;
                lvSem.Visible = false;
                divRenge.Visible = true;
                lvrange.Visible = true;
                btnadd.Visible = true;
                SetInitialRow();
            }

            #endregion

            #region Comment

            //     string Chk = objCommon.LookUp("ACD_EXAM_FEE_DEFINATION", "COUNT(1)", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDIT_RANGE_AMOUNT>0 and isnull(CANCEL,0) = 0");

            //    if ((Chk != null || Chk != string.Empty) && Chk != "0")
            //    {
            //        DataSet ds = objCommon.FillDropDown("ACD_EXAM_FEE_DEFINATION", "CREDIT_RANGE_AMOUNT,IsFeesApplicable,IsProFeesApplicable,IsCertiFeesApplicable", "MAXRANGE,MINRANGE,ApplicableFee,CertificateFee", "College_id =" + Convert.ToInt32(ddlCollege.SelectedValue) + " AND SESSIONNO = " + Convert.ToInt32(ddlSession.SelectedValue) + " AND FEETYPE = " + Convert.ToInt32(ddlExamType.SelectedValue) + " AND DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND CREDIT_RANGE_AMOUNT>0 and isnull(CANCEL,0) = 0", "");

            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            txtMaxRange.Text = Convert.ToString(ds.Tables[0].Rows[0]["MAXRANGE"]);
            //            txtMinRange.Text = Convert.ToString(ds.Tables[0].Rows[0]["MINRANGE"]);
            //            txtAmount.Text = Convert.ToString(ds.Tables[0].Rows[0]["CREDIT_RANGE_AMOUNT"]);
            //            txtYes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ApplicableFee"]);
            //            txtCerFee.Text = Convert.ToString(ds.Tables[0].Rows[0]["CertificateFee"]);
            //            rdActive.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsFeesApplicable"]);
            //            test.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProFeesApplicable"]);
            //            chkFeeCer.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCertiFeesApplicable"]);
            //        }
            //    }
            //    else
            //    {
            //        divRenge.Visible = true;
            //        txtMaxRange.Text = "0";
            //        txtMinRange.Text = "0";
            //        txtAmount.Text = "0";
            //        txtYes.Text = "0";
            //        txtCerFee.Text = string.Empty;
            //    }

            //    divCourse.Visible = false;
            //    divCredit.Visible = false;
            //    divFix.Visible = false;
            //    lvSem.Visible = false;
            //    divRenge.Visible = true;

            #endregion
        }

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
    }

    private void SetInitialRow()
    {
        //new 
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("FID", typeof(int)));
        dt.Columns.Add(new DataColumn("Minmark", typeof(string)));
        dt.Columns.Add(new DataColumn("Maxmark", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        // dt.Columns.Add(new DataColumn("Fix", typeof(string)));

        dr = dt.NewRow();
        dr["FID"] = 0;
        dr["Minmark"] = string.Empty;
        dr["Maxmark"] = string.Empty;
        dr["Amount"] = string.Empty;
        // dr["Fix"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvrange.DataSource = dt;
        lvrange.DataBind();
    }

    private void BindListView()
    {
        try
        {
            if (ddlFeesStructure.SelectedIndex == 1)
            {
                //int College = Convert.ToInt32(ddlCollege.SelectedValue); // Commented By Sagar M on Date 18052023 for college_id
                int College = Convert.ToInt32(ViewState["college_id"]);
                int Session = Convert.ToInt32(ddlSession.SelectedValue);
                int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
                int Structure = Convert.ToInt32(ddlFeesStructure.SelectedValue);
                int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);

                string Fee = txtYes.Text.Trim();

                DataSet ds = Exm.GetFeeConfig(College, degreeno, ExamType, Session);

                if (ds.Tables[0] != null || ds.Tables[0].Rows.Count > 0)
                {
                    lvFee.DataSource = ds;
                    lvFee.DataBind();
                    lvFee.Visible = true;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                }
                else
                {
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                }
            }
            else
            {
                //int College = Convert.ToInt32(ddlCollege.SelectedValue); // Commented By Sagar M on Date 18052023 for college_id
                int College = Convert.ToInt32(ViewState["college_id"]);
                int Session = Convert.ToInt32(ddlSession.SelectedValue);
                int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
                int Structure = Convert.ToInt32(ddlFeesStructure.SelectedValue);
                int degreeno = Convert.ToInt32(ddlDegree.SelectedValue);

                string Fee = txtYes.Text.Trim();

                DataSet ds = Exm.GetSemFeeConfig(College, degreeno, ExamType, Session);

                if (ds.Tables[0] != null || ds.Tables[0].Rows.Count > 0)
                {
                    lvSem.DataSource = ds;
                    lvSem.DataBind();
                    lvSem.Visible = true;
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                }
                else
                {
                    lvSem.Visible = false;
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_Exam_Fee_Config.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void Load()
    {
        DataSet ds = Exm.GetFeeConfig();

        if (ds.Tables[0] != null || ds.Tables[0].Rows.Count > 0)
        {
            lvLoad.DataSource = ds;
            lvLoad.DataBind();

            //lvFee.Visible = true;
            //divCredit.Visible = false;
            //divCourse.Visible = false;
        }
        // if (ds.Tables[1] != null || ds.Tables[1].Rows.Count > 0)
        //{
        //    lvLoad.DataSource = ds;
        //   // lvLoad.DataBind();
        //    //lvFee.Visible = true;
        //    //divCredit.Visible = false;
        //    //divCourse.Visible = false;
        //}
    }

    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
                {
                    ViewState["degreeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["DEGREENO"]).ToString();
                    ViewState["branchno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["BRANCHNO"]).ToString();
                    ViewState["college_id"] = Convert.ToInt32(ds.Tables[0].Rows[0]["COLLEGE_ID"]).ToString();
                    ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
                    // FILL DROPDOWN  ddlSession_SelectedIndexChanged
                }
            }

            //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", " DISTINCT SESSIONNO ", "SESSION_NAME", "COLLEGE_ID=" + Convert.ToInt32(ViewState["college_id"]) + "AND ISNULL (IS_ACTIVE,0)= 1", "SESSIONNO DESC");

            //objCommon.FillListBox(ddlDegree, "ACd_SCHEME AM inner join ACD_DEGREE AD on (AM.DEGREENO=AD.DEGREENO)", "AD.DEGREENO", "AD.DEGREENAME", "AD.DEGREENO>0 and SCHEMENO=" + ViewState["schemeno"] + "", "DEGREENAME"); // Commented by Sagar M on date 17052023 as per RCPIPER || Reval & Photocopy Fee define one time for multiple Degree & Branches

            objCommon.FillListBox(ddlDegree, "ACD_COLLEGE_SCHEME_MAPPING AM INNER JOIN ACD_DEGREE AD ON (AM.DEGREENO=AD.DEGREENO)", "DISTINCT AD.DEGREENO", "AD.DEGREENAME", "AD.DEGREENO > 0 and AM.COLLEGE_ID =" + ViewState["college_id"] + " AND ISNULL(ACTIVESTATUS,0)=1", "AD.DEGREENAME"); // Added by Sagar M on date 17052023 as per RCPIPER || Reval & Photocopy Fee define one time for multiple Degree & Branches
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_ExamAssesment.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

        //objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO desc");
        //objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue), "SESSIONNO desc");
        objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_NAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString(), "SESSIONNO desc");

        ddlSession.SelectedIndex = 0;
        ddlDegree.SelectedIndex = -1;
        ddlExamType.SelectedIndex = -1;
        ddlFeesStructure.SelectedIndex = 0;
        lstSemester.SelectedIndex = -1;

        ddlLateFee.SelectedIndex = 0;
        txtLateFeeDate.Text = "";
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        //ScriptManager.RegisterClientScriptBlock(UpdatePanel2, UpdatePanel2.GetType(), "Src", "Setdate('" + hdnDate.Value + "');", true);
        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);
        //Loop = 1;

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowProcessingFeeDropDown();", true);

        //txtLateFee.Visible = false;

        //if (hidLatefeeChecked.Value == "1")
        //{
        //    txtLateFee.Visible = true;
        //}
        //else
        //{
        //    txtLateFee.Visible = false; hidLatefeeChecked
        //}

        //if (hidLatefeeChecked.Value == "True")
        //{
        //    //txtLateFee.Visible = true;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "Reg", "clickLateFee();", true);
        //}
        //else
        //{
        //txtLateFee.Visible = false;
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Reg", "clickLateFee();", true);
        //}

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "src", "clickLateFee();", false);

        lvFee.Visible = false;
        divCredit.Visible = false;
        divCourse.Visible = false;
        divFix.Visible = false;
        txtYes.Text = string.Empty;
        txtCerFee.Text = string.Empty;
        btnSubmit.Visible = false;
        lvSem.Visible = false;
        divRenge.Visible = false;

        ddlSession.Focus();
        txtLateFeeAmount.Text = string.Empty;
    }

    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlCsession, "ACD_SESSION_MASTER WITH (NOLOCK)", "SESSIONNO", "SESSION_PNAME", "SESSIONNO > 0 AND ISNULL(IS_ACTIVE,0)=1 AND COLLEGE_ID=" + ViewState["college_id"].ToString() + " and SESSIONNO not in (" + Convert.ToInt32(ddlSession.SelectedValue) + ")", "SESSIONNO desc");
        //objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");
        //objCommon.FillListBox(lstSemester, "ACD_SEMESTER S WITH (NOLOCK) INNER JOIN ACD_STUDENT_RESULT SR WITH (NOLOCK) ON (SR.SEMESTERNO = S.SEMESTERNO)", " DISTINCT S.SEMESTERNO", "S.SEMESTERNAME", "S.SEMESTERNO > 0 AND SR.SESSIONNO = " + ddlSession.SelectedValue + " AND SR.SCHEMENO =" + Convert.ToInt32(ViewState["schemeno"]), "S.SEMESTERNO");

        ddlDegree.SelectedIndex = -1;
        ddlExamType.SelectedIndex = -1;
        ddlFeesStructure.SelectedIndex = 0;
        lstSemester.SelectedIndex = -1;

        ddlLateFee.SelectedIndex = 0;
        txtLateFeeDate.Text = "";
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowProcessingFeeDropDown();", true);

        lvFee.Visible = false;
        divCredit.Visible = false;
        divCourse.Visible = false;
        //divFix.Visible = false;
        txtYes.Text = string.Empty;
        txtCerFee.Text = string.Empty;
        btnSubmit.Visible = false;
        lvSem.Visible = false;
        divRenge.Visible = false;

        ddlExamType.Focus();
        txtLateFeeAmount.Text = string.Empty;

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowProcessingFeeDropDown();", true);
    }

    protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFeesStructure.SelectedIndex = 0;
        ddlDegree.SelectedIndex = -1;
        lstSemester.SelectedIndex = -1;

        ddlLateFee.SelectedIndex = 0;
        txtLateFeeDate.Text = "";
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);

        lvFee.Visible = false;
        divCredit.Visible = false;
        divCourse.Visible = false;
        // divFix.Visible = false;
        txtYes.Text = string.Empty;
        txtCerFee.Text = string.Empty;
        btnSubmit.Visible = false;
        lvSem.Visible = false;
        divRenge.Visible = false;

        ddlFeesStructure.Focus();
        txtLateFeeAmount.Text = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (chkLateFee.Checked)
        {
            if (ddlLateFee.SelectedIndex != 0 && txtLateFeeDate.Text != "" && txtLateFeeAmount.Text != "")
            {
                //if (ddlCsession.SelectedIndex > 0)
                //{
                //    CopyRuleToSession();
                //}
                //else
                //{
                ShowData();
                //}
            }
            else
            {
                if (ddlLateFee.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this, "Please Select the Late Fee Mode.", this.Page);
                    ddlLateFee.Focus();
                }
                else if (txtLateFeeDate.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Select the Late Fee Date.", this.Page);
                    txtLateFeeDate.Focus();
                }
                else if (txtLateFeeAmount.Text == "")
                {
                    objCommon.DisplayMessage(this, "Please Enter the Late Fee Amount.", this.Page);
                    txtLateFeeAmount.Focus();
                }

                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
            }
        }
        else if (rdActive.Checked)
        {
            if (ddlPaymentMode.SelectedIndex != 0)
            {
                //if (ddlCsession.SelectedIndex > 0)
                //{
                //    CopyRuleToSession();
                //}
                //else
                //{
                ShowData();
                //}
            }
            else
            {
                if (ddlPaymentMode.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(this, "Please Select the Payment Mode.", this.Page);
                    ddlPaymentMode.Focus();
                }

                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
            }
        }
        else
        {
            //if (ddlCsession.SelectedIndex > 0)
            //{
            //    CopyRuleToSession();
            //}
            //else
            //{
            ShowData();
            //}

            ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        }
    }

    private void CopyToSession()
    {
        DataSet ds = objCommon.GetCollegeSchemeMappingDetails(Convert.ToInt32(ddlCollege.SelectedValue));
        //ViewState["degreeno"]

        if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0] != null)
        {
            ViewState["schemeno"] = Convert.ToInt32(ds.Tables[0].Rows[0]["SCHEMENO"]).ToString();
        }

        int College = Convert.ToInt32(ddlCollege.SelectedValue);
        //int scheme = Convert.ToInt32(ViewState["schemeno"]);
        int session = Convert.ToInt32(ddlSession.SelectedValue);
        int Degree = Convert.ToInt32(ddlDegree.SelectedValue); // ddlsubjecttype.SelectedValue
        //int sem = Convert.ToInt32(lstSemester.SelectedValue); // lstSemester ddlSem
        int FeesStructure = Convert.ToInt32(ddlFeesStructure.SelectedValue); // lstSemester ddlSem
        int CopySession = Convert.ToInt32(ddlCsession.SelectedValue);

        CustomStatus cs = (CustomStatus)Exm.CopyToSession(College, session, Degree, FeesStructure, CopySession); // objSReg

        if (cs.Equals(CustomStatus.RecordSaved))
        {
            objCommon.DisplayMessage(this, "Record Copied Successfully.", this.Page);
            //ScriptManager.RegisterStartupScript(this, GetType(), "Src", "Record Insert Successfully", true);

            return;
        }
        else if (cs.Equals(CustomStatus.RecordExist))
        {
            objCommon.DisplayMessage(this, "Record Already Exist.", this.Page);
        }
    }

    private void ShowData()
    {
        //DataSet ds = (objCommon.FillDropDown("ACD_REFUND_APPLICATION", "Distinct isnull(ANNUAL_INC_CERT_FILENAME,'')ANNUAL_INC_CERT_FILENAME", "isnull(ANNUAL_INC_CERT_FILEPATH,'')ANNUAL_INC_CERT_FILEPATH,IDNO,ANNUAL_INC,STUDENTYEAR", "IDNO=" + ViewState["IDNO"].ToString() + "ANd REFUND_TYPE=4", ""));
        //if(rdActive.Checked==true)

        bool ActiveStatus;

        //if (hdFeeApplicable.Value == "true")

        if (rdActive.Checked == true)
        {
            ActiveStatus = true;
        }
        else
        {
            ActiveStatus = false;
        }

        bool FeeProcAppli;

        // if (hdFeeProcessApplicable.Value == "true")

        if (test.Checked == true)
        {
            FeeProcAppli = true;
        }
        else
        {
            FeeProcAppli = false;
        }

        bool IsFeeCertificate;

        if (chkFeeCer.Checked == true)
        {
            IsFeeCertificate = true;
        }
        else
        {
            IsFeeCertificate = false;
        }

        bool IsCheckLateFee;

        if (chkLateFee.Checked)
        {
            IsCheckLateFee = true;
        }
        else
        {
            IsCheckLateFee = false;
        }

        int College = Convert.ToInt32(ViewState["college_id"]);  //ddlCollege.SelectedValue
        int Session = Convert.ToInt32(ddlSession.SelectedValue);
        int ExamType = Convert.ToInt32(ddlExamType.SelectedValue);
        int FeesStructure = Convert.ToInt32(ddlFeesStructure.SelectedValue);

        int degreeno = 0; //= Convert.ToInt32(ddlDegree.SelectedValue);

        string Fee = txtYes.Text.Trim();
        String Sem = String.Empty;
        String Semname = String.Empty;

        string ApplicableFee = txtYes.Text.Trim() == string.Empty ? "0" : txtYes.Text.Trim();
        string CertificateFee = txtCerFee.Text.Trim() == string.Empty ? "0" : txtCerFee.Text.Trim();

        //int userno =Convert.ToInt32(Session["userno"].ToString());
        int userno = Convert.ToInt32(ViewState["uano"]);

        int LateFeesMode;
        decimal LateFeeAmount;
        DateTime LateFeeDate;

        int PaymentMode;

        if (chkLateFee.Checked)
        {
            LateFeesMode = Convert.ToInt32(ddlLateFee.SelectedValue);
            LateFeeAmount = Convert.ToDecimal(txtLateFeeAmount.Text);
            LateFeeDate = Convert.ToDateTime(txtLateFeeDate.Text);
        }
        else
        {
            LateFeesMode = 0;
            LateFeeAmount = 0;
            LateFeeDate = DateTime.Parse(System.DateTime.Now.ToString());
        }

        if (rdActive.Checked)
        {
            PaymentMode = Convert.ToInt32(ddlPaymentMode.SelectedValue);
        }
        else
        {
            PaymentMode = 0;
        }

        decimal valuationFee = Convert.ToDecimal(txtValuationFee.Text);
        decimal valuationMaxFee = Convert.ToDecimal(txtValuationMaxFee.Text);

        if (ddlFeesStructure.SelectedIndex == 2)
        {
            #region Cedit Wise

            foreach (ListItem items in lstSemester.Items)
            {
                if (items.Selected == true)
                {
                    Sem += items.Value + ",";
                    Semname += items.Text + ",";
                }
            }

            foreach (ListItem degree in ddlDegree.Items)
            {
                if (degree.Selected == true)
                {
                    degreeno = Convert.ToInt32(degree.Value);

                    string Creditfee = txtCredit.Text.Trim();

                    if (decimal.Parse(txtCredit.Text) > 0)
                    {
                        CustomStatus cs = (CustomStatus)Exm.FeeCredit(College, Session, ExamType, degreeno, Sem, ActiveStatus, FeeProcAppli, ApplicableFee, Creditfee, FeesStructure, Semname, userno, IsFeeCertificate, CertificateFee, IsCheckLateFee, LateFeesMode, LateFeeDate, LateFeeAmount, valuationFee, valuationMaxFee, PaymentMode);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                            Load();

                            lvFee.Visible = false;
                            divCredit.Visible = true;

                            //return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Record Successfully Saved.", this.Page);
                            Load();

                            lvFee.Visible = false;
                            divCredit.Visible = true;

                            //return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Enter the Fee Amount.", this.Page);
                        txtCredit.Focus();

                        //return;
                    }
                }
            }

            //Sem = Sem.Remove(Sem.Length - 1);
            //Semname = Semname.Remove(Semname.Length - 1);

            #endregion
        }
        else if (ddlFeesStructure.SelectedIndex == 3)
        {
            #region Course Wise

            foreach (ListItem items in lstSemester.Items)
            {
                if (items.Selected == true)
                {

                    Sem += items.Value + ",";
                    Semname += items.Text + ",";
                }
            }

            //Sem = Sem.Remove(Sem.Length - 1);
            //Semname = Semname.Remove(Semname.Length - 1);

            foreach (ListItem degree in ddlDegree.Items)
            {
                if (degree.Selected == true)
                {
                    degreeno = Convert.ToInt32(degree.Value);

                    if (decimal.Parse(txtCourse.Text) > 0)
                    {
                        string CourseFee = txtCourse.Text.Trim();

                        CustomStatus cs = (CustomStatus)Exm.FeeCourse(College, Session, ExamType, degreeno, Sem, ActiveStatus, FeeProcAppli, ApplicableFee, CourseFee, FeesStructure, Semname, userno, IsFeeCertificate, CertificateFee, IsCheckLateFee, LateFeesMode, LateFeeDate, LateFeeAmount, valuationFee, valuationMaxFee, PaymentMode);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                            Load();

                            divCourse.Visible = true;
                            lvFee.Visible = false;
                            divCredit.Visible = false;

                            //return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Record Successfully Saved.", this.Page);
                            Load();

                            divCourse.Visible = true;
                            lvFee.Visible = false;
                            divCredit.Visible = false;

                            //return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Please Enter the Fee Amount.", this.Page);
                        txtCourse.Focus();

                        //return;
                    }
                }
            }

            #endregion
        }
        else if (ddlFeesStructure.SelectedIndex == 1)
        {
            #region Course Type Wise

            foreach (ListItem items in lstSemester.Items)
            {
                if (items.Selected == true)
                {
                    Sem += items.Value + ",";
                    Semname += items.Text + ",";
                }
            }

            foreach (ListItem degree in ddlDegree.Items)
            {
                if (degree.Selected == true)
                {
                    degreeno = Convert.ToInt32(degree.Value);

                    foreach (ListViewDataItem item in lvFee.Items)
                    {
                        Label Sub = item.FindControl("lblSubType") as Label;
                        TextBox Fees = item.FindControl("txtFee") as TextBox;

                        String SubjectName = Sub.Text.Trim();
                        int SUBID = Convert.ToInt32(Sub.ToolTip);
                        String FeeAmt = Fees.Text.Trim();

                        if (decimal.Parse(Fees.Text) > 0)
                        {
                            CustomStatus cs = (CustomStatus)Exm.FeeConfig(College, Session, ExamType, SUBID, SubjectName, FeeAmt, degreeno, Sem, ActiveStatus, FeeProcAppli, ApplicableFee, FeesStructure, Semname, userno, IsFeeCertificate, CertificateFee, IsCheckLateFee, LateFeesMode, LateFeeDate, LateFeeAmount, valuationFee, valuationMaxFee, PaymentMode);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                                BindListView();
                                Load();

                                lvFee.Visible = true;
                                divCredit.Visible = false;
                                divCourse.Visible = false;
                                //return;
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Record Successfully Saved.", this.Page);
                                BindListView();
                                Load();

                                lvFee.Visible = true;
                                divCredit.Visible = false;
                                divCourse.Visible = false;
                                //return;
                            }
                        }
                        //else
                        //{
                        //    if (lvFee.Items.Count < 1)
                        //    {
                        //        objCommon.DisplayMessage(this, "Please Enter the Fee Amount ...!!!", this.Page);
                        //        TextBox1.Focus();

                        //        return;
                        //    }
                        //}
                    }

                }
            }

            #endregion
        }
        else if (ddlFeesStructure.SelectedIndex == 4)
        {
            #region Fix

            foreach (ListItem degree in ddlDegree.Items)
            {
                if (degree.Selected == true)
                {
                    degreeno = Convert.ToInt32(degree.Value);
                    foreach (ListViewDataItem item in lvSem.Items)
                    {
                        Label Semester = item.FindControl("lblSem") as Label;
                        TextBox Fees = item.FindControl("txtFee") as TextBox;
                        String FeeAmt = Fees.Text.Trim();

                        string TempSem = Semester.Text;
                        string Semestername = Convert.ToString(Semester.ToolTip);

                        if (decimal.Parse(Fees.Text) > 0)
                        {
                            CustomStatus cs = (CustomStatus)Exm.SemFeeConfig(College, Session, ExamType, TempSem, Semestername, FeeAmt, degreeno, ActiveStatus, FeeProcAppli, ApplicableFee, FeesStructure, userno, IsFeeCertificate, CertificateFee, IsCheckLateFee, LateFeesMode, LateFeeDate, LateFeeAmount, valuationFee, valuationMaxFee, PaymentMode);

                            if (cs.Equals(CustomStatus.RecordSaved))
                            {
                                objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                                BindListView();
                                Load();

                                lvSem.Visible = true;
                                lvFee.Visible = false;
                                divCredit.Visible = false;
                                divCourse.Visible = false;
                                //return;
                            }
                            else
                            {
                                objCommon.DisplayMessage(this, "Record Successfully Saved.", this.Page);
                                BindListView();
                                Load();

                                lvSem.Visible = true;
                                lvFee.Visible = false;
                                divCredit.Visible = false;
                                divCourse.Visible = false;
                                //return;
                            }
                        }
                    }
                }
            }

            #endregion
        }
        else
        {
            #region Comment

            //String MINRANGE = txtMinRange.Text.Trim();
            //String MAXRANGE = txtMaxRange.Text.Trim();
            //String AMOUNT = txtAmount.Text.Trim();
            //foreach (ListItem items in lstSemester.Items)
            //{
            //    if (items.Selected == true)
            //    {

            //        Sem += items.Value + ",";
            //        Semname += items.Text + ",";
            //    }
            //}
            //Sem = Sem.Remove(Sem.Length - 1);
            //Semname = Semname.Remove(Semname.Length - 1);

            //if((decimal.Parse(txtMaxRange.Text) <= decimal.Parse(txtMinRange.Text)))
            //{
            //    objCommon.DisplayMessage(this, "MinRange Should Not Greater Than MaxRange ", this.Page);
            //}
            //else if (decimal.Parse(txtAmount.Text) <= 0)
            //{
            //    objCommon.DisplayMessage(this, "Plese Enter Fee Amount ", this.Page);
            //}
            //else
            //{

            //    if (decimal.Parse(txtAmount.Text) > 0 && (decimal.Parse(txtMaxRange.Text) >= decimal.Parse(txtMinRange.Text)))
            //    {


            //        CustomStatus cs = (CustomStatus)Exm.CreditRange(College, Session, ExamType, degreeno, Sem, ActiveStatus, FeeProcAppli, ApplicableFee, MINRANGE, MAXRANGE, AMOUNT, FeesStructure, Semname, userno, IsFeeCertificate, CertificateFee);
            //        if (cs.Equals(CustomStatus.RecordSaved))
            //        {

            //            objCommon.DisplayMessage(this, "Record Saved Successfully!", this.Page);
            //            Load();
            //            divRenge.Visible = true;
            //            lvSem.Visible = false;
            //            lvFee.Visible = false;
            //            divCredit.Visible = false;
            //            divCourse.Visible = false;
            //            return;


            //        }
            //        else
            //        {
            //            objCommon.DisplayMessage(this, "Record  Successfully Saved!", this.Page);
            //            Load();
            //            divRenge.Visible = true;
            //            lvSem.Visible = false;
            //            lvFee.Visible = false;
            //            divCredit.Visible = false;
            //            divCourse.Visible = false;
            //            return;

            //        }


            //    }
            //}

            #endregion

            #region Credit Range Wise

            foreach (ListItem items in lstSemester.Items)
            {
                if (items.Selected == true)
                {

                    Sem += items.Value + ",";
                    Semname += items.Text + ",";
                }
            }

            //Sem = Sem.Remove(Sem.Length - 1);
            //Semname = Semname.Remove(Semname.Length - 1);

            foreach (ListItem degree in ddlDegree.Items)
            {
                if (degree.Selected == true)
                {
                    degreeno = Convert.ToInt32(degree.Value);
                    foreach (ListViewDataItem item in lvrange.Items)
                    {
                        TextBox txtmax = item.FindControl("txtMaxRange") as TextBox;
                        TextBox txtmin = item.FindControl("txtMinRange") as TextBox;
                        TextBox txtamount = item.FindControl("txtAmount") as TextBox;

                        HiddenField hdfield = item.FindControl("hfsrno") as HiddenField;

                        string Max = Convert.ToString(txtmax.Text);
                        string Min = Convert.ToString(txtmin.Text);
                        string amount = Convert.ToString(txtamount.Text);

                        //if (decimal.Parse(Min) > 0 && decimal.Parse(Max) > 0 && decimal.Parse(amount) > 0)
                        //{
                        int hdf = Convert.ToInt32(hdfield.Value);
                        CustomStatus cs = (CustomStatus)Exm.CreditRange(College, Session, ExamType, degreeno, Sem, ActiveStatus, FeeProcAppli, ApplicableFee, Min, Max, amount, FeesStructure, Semname, userno, IsFeeCertificate, CertificateFee, hdf, IsCheckLateFee, LateFeesMode, LateFeeDate, LateFeeAmount, valuationFee, valuationMaxFee, PaymentMode);

                        if (cs.Equals(CustomStatus.RecordSaved))
                        {
                            objCommon.DisplayMessage(this, "Record Saved Successfully.", this.Page);
                            Load();

                            divRenge.Visible = true;
                            lvSem.Visible = false;
                            lvFee.Visible = false;
                            divCredit.Visible = false;
                            divCourse.Visible = false;
                            //return;
                        }
                        else
                        {
                            objCommon.DisplayMessage(this, "Record Successfully Saved.", this.Page);
                            Load();

                            divRenge.Visible = true;
                            lvSem.Visible = false;
                            lvFee.Visible = false;
                            divCredit.Visible = false;
                            divCourse.Visible = false;
                            //return;
                        }
                        //}
                        //else
                        //{
                        //    if (decimal.Parse(Min) > 0)
                        //    {
                        //        objCommon.DisplayMessage(this, "Please Enter the Min Amount ...!!!", this.Page);
                        //        txtCourse.Focus();
                        //    }
                        //    else if (decimal.Parse(Max) > 0)
                        //    {
                        //        objCommon.DisplayMessage(this, "Please Enter the Max Amount ...!!!", this.Page);
                        //        txtCourse.Focus();
                        //    }
                        //    else if (decimal.Parse(amount) > 0)
                        //    {
                        //        objCommon.DisplayMessage(this, "Please Enter the Amount ...!!!", this.Page);
                        //        txtCourse.Focus();
                        //    }

                        //    return;
                        //}
                    }
                }
            }

            #endregion
        }

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
    }

    protected void ddlFeesStructure_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDegree.SelectedIndex = -1;
        ddlLateFee.SelectedIndex = 0;
        txtLateFeeDate.Text = "";
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);

        lvFee.Visible = false;
        divCredit.Visible = false;
        divCourse.Visible = false;
        divFix.Visible = false;
        txtYes.Text = string.Empty;
        txtCerFee.Text = string.Empty;
        btnSubmit.Visible = false;
        lvSem.Visible = false;
        divRenge.Visible = false;

        if (ddlFeesStructure.SelectedIndex == 4)
        {
            fsem.Visible = false;
        }
        else
        {
            fsem.Visible = true;
            lstSemester.SelectedIndex = -1;
        }

        //objCommon.FillListBox(lstSemester, "ACd_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

        ddlDegree.Focus();
        txtLateFeeAmount.Text = string.Empty;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstSemester.SelectedIndex = -1;
        ddlLateFee.SelectedIndex = 0;
        txtLateFeeDate.Text = "";
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        //ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "clickLateFee();", false);

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);

        lvFee.Visible = false;
        divCredit.Visible = false;
        divCourse.Visible = false;
        divFix.Visible = false;
        txtYes.Text = string.Empty;
        txtCerFee.Text = string.Empty;
        btnSubmit.Visible = false;
        lvSem.Visible = false;
        divRenge.Visible = false;

        //objCommon.FillListBox(lstSemester, "ACd_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");

        lstSemester.Focus();
        txtLateFeeAmount.Text = string.Empty;
    }

    protected void txtYes_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        // DataSet dtCurrentTable = (DataSet)ViewState["CurrentTable"];

        DataRow drCurrentRow = null;
        //if (dtCurrentTable.Rows.Count > 0 )

        if (lvrange.Items.Count > 0)
        {
            DataTable dtNewTable = new DataTable();

            dtNewTable.Columns.Add(new DataColumn("FID", typeof(int)));
            dtNewTable.Columns.Add(new DataColumn("Maxmark", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("Minmark", typeof(string)));
            dtNewTable.Columns.Add(new DataColumn("Amount", typeof(string)));

            // dtNewTable.Columns.Add(new DataColumn("Fix", typeof(string)));

            drCurrentRow = dtNewTable.NewRow();

            drCurrentRow["FID"] = 0;
            drCurrentRow["Maxmark"] = string.Empty;
            drCurrentRow["Minmark"] = string.Empty;
            drCurrentRow["Amount"] = string.Empty;

            // drCurrentRow["Fix"] = "0";

            int i = 0;

            for (i = 0; i < lvrange.Items.Count; i++)
            {
                if (i <= 7)
                {
                    HiddenField hfd = (HiddenField)lvrange.Items[rowIndex].FindControl("hfsrno");
                    TextBox min = (TextBox)lvrange.Items[rowIndex].FindControl("txtMinRange");
                    TextBox max = (TextBox)lvrange.Items[rowIndex].FindControl("txtMaxRange");
                    TextBox total = (TextBox)lvrange.Items[rowIndex].FindControl("txtAmount");

                    //if (decimal.Parse(min.Text) <= 0 || Convert.ToString(min.Text) ==String.Empty)

                    if (Convert.ToString(min.Text) != String.Empty)
                    {
                        if (i == 0)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) >= 2)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Greater than 1 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 1)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 6 || decimal.Parse(min.Text) >= 7)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 6 OR Greater than 6 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 2)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 13 || decimal.Parse(min.Text) >= 14)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 13 OR Greater than 13 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 3)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 17 || decimal.Parse(min.Text) >= 18)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 17 OR Greater than 17 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 4)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 23 || decimal.Parse(min.Text) >= 24)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 23 OR Greater than 23 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 5)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 26 || decimal.Parse(min.Text) >= 27)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 26 OR Greater than 26 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 6)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 29 || decimal.Parse(min.Text) >= 30)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 29 OR Greater than 29 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 7)
                        {
                            if (decimal.Parse(min.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(min.Text) < 33 || decimal.Parse(min.Text) >= 34)
                            {
                                objCommon.DisplayMessage(this, "Min Range should not be Less than 33 OR Greater than 33 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Min Range should not be Null ...!!!", this.Page);

                        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                        return;
                    }

                    if (Convert.ToString(max.Text) != String.Empty)
                    {
                        if (i == 0)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) >= 6 || decimal.Parse(max.Text) < 5)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 5 OR Greater than 5 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 1)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 12 || decimal.Parse(max.Text) >= 13)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 12 OR Greater than 12 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 2)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 16 || decimal.Parse(max.Text) >= 17)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 16 OR Greater than 16 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 3)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 22 || decimal.Parse(max.Text) >= 23)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 22 OR Greater than 22 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 4)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 25 || decimal.Parse(max.Text) >= 26)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 25 OR Greater than 25 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 5)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 28 || decimal.Parse(max.Text) >= 29)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 28 OR Greater than 28 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 6)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) < 32 || decimal.Parse(max.Text) >= 33)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 32 OR Greater than 32 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                        else if (i == 7)
                        {
                            if (decimal.Parse(max.Text) <= 0)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be 0 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                            else if (decimal.Parse(max.Text) <= 33)
                            {
                                objCommon.DisplayMessage(this, "Max Range should not be Less than 33 ...!!!", this.Page);

                                ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Max Range should not be Null ...!!!", this.Page);

                        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                        return;
                    }

                    if (Convert.ToString(total.Text) != String.Empty)
                    {
                        if (decimal.Parse(total.Text) <= 0)
                        {
                            objCommon.DisplayMessage(this, "Amount should not be 0 ...!!!", this.Page);

                            ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Amount should not be Null ...!!!", this.Page);

                        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
                        return;
                    }

                    drCurrentRow = dtNewTable.NewRow();
                    drCurrentRow["FID"] = hfd.Value;
                    drCurrentRow["Maxmark"] = max.Text;
                    drCurrentRow["Minmark"] = min.Text;
                    drCurrentRow["Amount"] = total.Text;

                    rowIndex++;
                    dtNewTable.Rows.Add(drCurrentRow);
                }
                else
                {
                    btnadd.Visible = false;
                }
            }

            if (i < 8)
            {
                drCurrentRow = dtNewTable.NewRow();

                drCurrentRow["FID"] = 0;
                drCurrentRow["Maxmark"] = string.Empty;
                drCurrentRow["Minmark"] = string.Empty;
                drCurrentRow["Amount"] = string.Empty;

                dtNewTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtNewTable;
                lvrange.DataSource = dtNewTable;
                lvrange.DataBind();

                lvrange.Visible = true;
                divRenge.Visible = true;
            }
            else
            {
                btnadd.Visible = false;
            }
        }

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
    }

    protected void del_Click(object sender, ImageClickEventArgs e)
    {
        //lstSemester.SelectedValue = null;
        ImageButton btnDelete = sender as ImageButton;
        int FID = int.Parse(btnDelete.CommandArgument);
        SqlDataReader dr = Exm.GetFeeDetails(FID);

        if (dr != null)
        {
            if (dr.Read())
            {
                //int ClgId = Convert.ToInt32(dr["college_id"]);
                //int ClgId = (dr["college_id"] != null && Convert.ToInt32(dr["college_id"]) > 0) ? Convert.ToInt32(dr["college_id"]) : 0;
                //int ClgId = (dr["college_id"] != null && Convert.ToInt32(dr["college_id"]) > 0) ? 0 : Convert.ToInt32(dr["college_id"]);
                int ClgId = Convert.ToInt32(dr["college_id"] == DBNull.Value ? 0 : Convert.ToInt32(dr["college_id"]));

                //ddlCollege_SelectedIndexChanged(new object(), new EventArgs());

                //int Sessionno = Convert.ToInt32(dr["sessionno"]);
                int Sessionno = Convert.ToInt32(dr["sessionno"] == DBNull.Value ? 0 : Convert.ToInt32(dr["sessionno"]));

                int FeeType = Convert.ToInt32(dr["FEETYPE"]);
                int Degreeno = Convert.ToInt32(dr["degreeno"]);
                string txt = dr["ApplicableFee"].ToString();

                //int FeeStru = Convert.ToInt32(dr["FEESTRUCTURE_TYPE"]);
                int FeeStru = Convert.ToInt32(dr["FEESTRUCTURE_TYPE"] == DBNull.Value ? 0 : Convert.ToInt32(dr["FEESTRUCTURE_TYPE"]));

                if (txtconformmessageValue.Value == "Yes")
                {
                    CustomStatus cs = (CustomStatus)Exm.FeeDelete(ClgId, Sessionno, FeeType, Degreeno, FeeStru);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        objCommon.DisplayMessage(this, "Record Cancel Successfully...!!!", this.Page);
                        Load();
                        lvFee.Visible = false;
                        divCredit.Visible = false;
                        divCourse.Visible = false;
                        lvSem.Visible = false;
                        divRenge.Visible = false;
                        return;
                    }
                    else
                    {
                        objCommon.DisplayMessage(this, "Something Went Wrong...!!!", this.Page);
                        Load();
                        lvFee.Visible = false;
                        divCredit.Visible = false;
                        divCourse.Visible = false;
                        lvSem.Visible = false;
                        divRenge.Visible = false;
                        return;
                    }
                }
                else
                {
                    //objCommon.DisplayMessage(this, "Something went Wrong..", this.Page);
                    Load();
                    lvFee.Visible = false;
                    divCredit.Visible = false;
                    divCourse.Visible = false;
                    lvSem.Visible = false;
                    divRenge.Visible = false;
                    return;
                }
            }
        }
    }

    protected void txtLateFeeDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtLateFeeDate.Text != string.Empty)
        //{
        //    if (ddlLateFee.SelectedIndex == 0)
        //    {
        //        objCommon.DisplayMessage(this, "Please Select Fee Mode ...!!!", this.Page);

        //        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //        ddlLateFee.Focus();
        //        return;
        //    }
        //    else if (ddlLateFee.SelectedIndex == 1)
        //    {
        //        if (txtLateFeeAmount.Text == string.Empty)
        //        {
        //            objCommon.DisplayMessage(this, "Please Enter the Amount ...!!!", this.Page);

        //            ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //            txtLateFeeAmount.Focus();
        //            return;
        //        }
        //        //else
        //        //{
        //        //    Amount = Convert.ToDecimal(txtLateFeeAmount.Text);
        //        //    TotalAmount = TotalAmount + Amount;
        //        //    txtLateFeeAmount.Text = TotalAmount.ToString();
        //        //}
        //    }
        //    else if (ddlLateFee.SelectedIndex == 2)
        //    {
        //        if (txtLateFeeAmount.Text != string.Empty)
        //        {
        //            objCommon.DisplayMessage(this, "Please Enter the Amount ...!!!", this.Page);

        //            ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //            txtLateFeeAmount.Focus();
        //            return;
        //        }
        //        else
        //        {

        //        }
        //    }
        //    else if (ddlLateFee.SelectedIndex == 3)
        //    {
        //        if (txtLateFeeAmount.Text != string.Empty)
        //        {
        //            objCommon.DisplayMessage(this, "Please Enter the Amount ...!!!", this.Page);

        //            ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
        //            txtLateFeeAmount.Focus();
        //            return;
        //        }
        //        else
        //        {

        //        }
        //    }
        //}

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
    }

    protected void lstSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtValuationFee.Text = "0";
        txtValuationMaxFee.Text = "0";

        pnlCopySession.Visible = false;
        btnCopyData.Visible = false;

        ddlCsession.SelectedIndex = 0;

        ScriptManager.RegisterClientScriptBlock(updFee, updFee.GetType(), "Src", "ShowDropDown();", true);
    }

    protected void btnCopyData_Click(object sender, EventArgs e)
    {
        //pnlCopySession.Visible = true;

        if (ddlCsession.SelectedIndex > 0)
        {
            //btnSubmit.Visible = true;
            btnCopyData.Visible = true;

            CopyToSession();
        }
        else
        {
            //btnSubmit.Visible = false;
            btnCopyData.Visible = false;

            objCommon.DisplayMessage(this, "Please Select Copy To Session.", this.Page);
            ddlCsession.Focus();
        }
    }

    protected void ddlCsession_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCsession.SelectedIndex > 0)
        {
            //btnSubmit.Visible = true;
            btnCopyData.Visible = true;
        }
        else
        {
            //btnSubmit.Visible = false;
            btnCopyData.Visible = false;
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //ShowReport("Exam Fee Config", "rptShowExamFeeConfig.rpt");

        if (ddlCollege.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlCollege.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        GridView GVStudData = new GridView();
        DataSet ds = objCommon.DynamicSPCall_Select("PKG_Exam_Show_ExamFeeConfig", "@P_SESSIONNO,@P_COLLEGE_ID", "" + Convert.ToInt32(ddlSession.SelectedValue) + "," + Convert.ToInt32(ViewState["college_id"]) + "");  //ddlCollege.SelectedValue

        if (ds.Tables[0].Rows.Count > 0)
        {
            GVStudData.DataSource = ds;
            GVStudData.DataBind();

            string attachment = "attachment;filename=ExamFeeConfigReport.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GVStudData.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            objCommon.DisplayMessage("No Data Found for current selection.", this.Page);
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        if (ddlCollege.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlColgScheme.Text + ".');", true);
            ddlCollege.Focus();
            return;
        }
        else if (ddlSession.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Please Select " + lblDYddlSession.Text + ".');", true);
            ddlSession.Focus();
            return;
        }

        try
        {
            int Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
            //int branchno = Convert.ToInt32(ViewState["branchno"]);
            //int CollegeId = Convert.ToInt32(ViewState["college_id"]);
            int CollegeId = Convert.ToInt32(ddlCollege.SelectedValue);
            //int degreeno = Convert.ToInt32(ViewState["degreeno"]);

            string SP_Name = "PKG_Exam_Show_ExamFeeConfig";
            string SP_Parameters = "@P_SESSIONNO,@P_COLLEGE_ID";
            string Call_Values = "" + Sessionno + "," + CollegeId + "";

            DataSet ds = null;
            ds = objCommon.DynamicSPCall_Select(SP_Name, SP_Parameters, Call_Values);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
                url += "Reports/CommonReport.aspx?";
                url += "pagetitle=" + reportTitle;
                url += "&path=~,Reports,Academic," + rptFileName;
                url += "&param=@P_SESSIONNO=" + Convert.ToInt32(ddlSession.SelectedValue) + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
                sb.Append(@"window.open('" + url + "','','" + features + "');");

                ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
            }
            else
            {
                objCommon.DisplayMessage(updFee, "No Data Found for current selection.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_CourseRegistration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
}
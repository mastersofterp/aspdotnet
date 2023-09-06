//=========================================================================
// PRODUCT NAME  : UAIMS
// MODULE NAME   : VEHICLE MANAGEMENT
// CREATE BY     : MRUNAL SINGH
// CREATED DATE  : 14-APR-2020
// DESCRIPTION   : USE TO SEND SMS TO TRANSPORT STUDENT.
//=========================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.IO;

public partial class VEHICLE_MAINTENANCE_Transaction_SendSMSToTransportStudent : System.Web.UI.Page
{
    Common objCommon = new Common();

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


                Studpanel.Enabled = true;
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE", "DEGREENO", "DEGREENAME", "DEGREENO > 0", "DEGREENAME");
                objCommon.FillDropDownList(ddlsemester, "ACD_SEMESTER", "SEMESTERNO", "SEMESTERNAME", "SEMESTERNO>0", "SEMESTERNO");
                objCommon.FillDropDownList(ddlRoute, "VEHICLE_ROUTEMASTER", "ROUTEID", "ROUTENAME", "ROUTEID>0", "ROUTEID");
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
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }


    protected void btnShowStudent_Click(object sender, EventArgs e)
    {
        trStudent.Visible = true;
        lvStudent.Visible = true;
        pnlstud.Visible = true;

        //if (ddlBranch.SelectedValue != "0" || ddlDegree.SelectedValue != "0")
        //{
        DataSet ds = objCommon.FillDropDown("VEHICLE_USER_ROUTEALLOT V INNER JOIN ACD_STUDENT S ON (V.IDNO=S.IDNO)", " S.REGNO,S.ENROLLNO,(S.STUDNAME+' '+ISNULL(S.STUDLASTNAME,'')) STUDNAME", "S.EMAILID,S.STUDENTMOBILE, V.IDNO", "USER_TYPE='S' AND V.CANCEL_STATUS IS NULL AND (S.BRANCHNO=" + ddlBranch.SelectedValue + " OR " + ddlBranch.SelectedValue + "=0) AND (S.DEGREENO=" + ddlDegree.SelectedValue + " OR " + ddlDegree.SelectedValue + "= 0) AND ISNULL(S.ADMCAN,0)=0 AND (S.SEMESTERNO=" + ddlsemester.SelectedValue + " OR " + ddlsemester.SelectedValue + "= 0) AND (V.ROUTEID IN (SELECT A.VALUE FROM DBO.SPLIT(('" + hdnRoute.Value.ToString() + "'),',')A)   OR '" + hdnRoute.Value.ToString() + "'='0')", "S.REGNO,S.STUDNAME");
        lvStudent.DataSource = ds;
        lvStudent.DataBind();
        pnlstud.Visible = true;
        divMsgSMS.Visible = true;
        //}
        //else
        //{
        //    lvStudent.DataSource = null; 
        //    lvStudent.DataBind();
        //    pnlstud.Visible = false;
        //    divMsgSMS.Visible =false;
        //}
    }

    protected void btnSndSms_Click(object sender, EventArgs e)
    {

        foreach (ListViewDataItem item in lvStudent.Items)
        {
            try
            {
                CheckBox chek = item.FindControl("chkSelect") as CheckBox;
                Label lblStudmobile = item.FindControl("lblStudmobile") as Label;

                if (chek.Checked)
                {
                    string Message = txtMessage.Text;
                    if (lblStudmobile.Text != string.Empty)
                    {
                        this.SendSMS(lblStudmobile.Text, Message);
                        objCommon.DisplayUserMessage(this.Page, "SMS send succesfully", this.Page);
                    }

                    else
                    {
                        objCommon.DisplayMessage("Sorry..! Dont find Mobile no. for some students", this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        cancel();
    }

    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }

        }
        catch
        {

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cancel();


    }

    public void cancel()
    {
        ddlBranch.SelectedIndex = 0;
        ddlDegree.SelectedIndex = 0;
        ddlsemester.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
        txtMessage.Text = string.Empty.Trim();
        lvStudent.DataSource = null;
        lvStudent.DataBind();
        pnlstud.Visible = false;
        divMsgSMS.Visible = false;
    }


    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "A.BRANCHNO", "A.LONGNAME", "B.DEGREENO=" + ddlDegree.SelectedValue + " AND A.BRANCHNO>0", "A.BRANCHNO");


        //DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue + " AND B.DEPTNO =" + Session["userdeptno"].ToString(), "A.LONGNAME");

        DataSet ds = objCommon.FillDropDown("ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = '" + Convert.ToInt32(ddlDegree.SelectedValue) + "' AND B.DEPTNO IN (SELECT CAST(VALUE AS INT) FROM DBO.SPLIT('" + Session["userdeptno"].ToString() + "',','))", "A.LONGNAME");

        string BranchNos = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            BranchNos += ds.Tables[0].Rows[i]["BranchNo"].ToString() + ",";
        }
        DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
        //on faculty login to get only those dept which is related to logged in faculty
        objCommon.FilterDataByBranch(ddlBranch, dsReff.Tables[0].Rows[0]["FILTER_USER_TYPE"].ToString(), BranchNos, Convert.ToInt32(dsReff.Tables[0].Rows[0]["BRANCH_FILTER"].ToString()), Convert.ToInt32(Session["usertype"]));
        ddlBranch.Focus();
    }
}
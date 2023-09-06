//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAY ROLL
// PAGE NAME     : PayRoll_Appointment.ASPX                                                    
// CREATION DATE : 27-march-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Net.NetworkInformation;
using System.Diagnostics; 

public partial class ADMINISTRATION_Mis_Account_Signing : System.Web.UI.Page
{
    //CREATING OBJECTS OF CLASS FILES COMMON,UAIMS_COMMON,PAYCONTROLLER
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();
        
    User_AccController objmis = new User_AccController();

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
                //CheckPageAuthorization();

                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();

            }
            //Session["idno"]

            this.EmployeeDetails();


        }

    }

    private void EmployeeDetails()
    {
        DataSet ds = null;

        if (Session["usertype"].ToString() == "2")
        {
            ds = objmis.GetMisStudentInformation(Convert.ToInt32(Session["idno"].ToString()));
            lblDepartment.Text = ds.Tables[0].Rows[0]["longname"].ToString();
            lblDesignation.Text = "STUDENT";
            lblIDCardNumber.Text = ds.Tables[0].Rows[0]["rollNo"].ToString();
            lblName.Text = ds.Tables[0].Rows[0]["studName"].ToString();
        }
        else
        {
            ds = objmis.GetEmployeeInformation(Convert.ToInt32(Session["idno"].ToString()));
            lblDepartment.Text = ds.Tables[0].Rows[0]["SUBDEPT"].ToString();
            lblDesignation.Text = ds.Tables[0].Rows[0]["SUBDESIG"].ToString();
            lblIDCardNumber.Text = ds.Tables[0].Rows[0]["PFILENO"].ToString();
            lblName.Text = ds.Tables[0].Rows[0]["NAME"].ToString();
        }
    }

    //protected void butsubmit_Click(object sender, EventArgs e)
    //{
    //    UserAcc objmisUserAcc = new UserAcc();
    //    string IP = Request.ServerVariables["REMOTE_HOST"];
    //    objmisUserAcc.COLLEGE_CODE = Session["colcode"].ToString();
    //    objmisUserAcc.UA_IDNo = Convert.ToInt32(Session["idno"].ToString());
    //    objmisUserAcc.AUDIT_DATE = System.DateTime.Now;
    //    objmisUserAcc.IP_ADDRESS = IP;
    //    objmisUserAcc.SIGNED = true;
    //    objmisUserAcc.USER_ID = Session["username"].ToString();
    //    objmisUserAcc.SIGNED_DATE = System.DateTime.Now;

    //    CustomStatus cs = (CustomStatus)objmis.AddMisAccountSigning(objmisUserAcc);
    //    if (cs.Equals(CustomStatus.RecordSaved))
    //    {
    //        //Response.Redirect("~/home.aspx");
    //        Response.Redirect("~/changePassword.aspx?status=firstlog");
    //    }
    //}

    
    protected void imgSubmit_Click(object sender, ImageClickEventArgs e)
    {

        UserAcc objmisUserAcc = new UserAcc();
        string IP = Request.ServerVariables["REMOTE_HOST"];
        objmisUserAcc.COLLEGE_CODE = Session["colcode"].ToString();
        objmisUserAcc.UA_IDNo = Convert.ToInt32(Session["idno"].ToString());
        objmisUserAcc.AUDIT_DATE = System.DateTime.Now;
        objmisUserAcc.IP_ADDRESS = IP;
        objmisUserAcc.MAC_ADDRESS = GetMacAddress(IP);
        objmisUserAcc.SIGNED = true;
        objmisUserAcc.USER_ID = Session["username"].ToString();
        objmisUserAcc.SIGNED_DATE = System.DateTime.Now;

        if (chkSign.Checked == true)
        {

            CustomStatus cs = (CustomStatus)objmis.AddMisAccountSigning(objmisUserAcc);
            if (cs.Equals(CustomStatus.RecordSaved))
            {
                //Response.Redirect("~/home.aspx");
                Response.Redirect("~/changePassword.aspx?status=firstlog");
            }
        }
        else
        {

            objCommon.DisplayMessage(updmain,"Please accept all terms and conditions", this);
        
        }
    }
    

    public string GetMacAddress(string IPAddress)
    {
        string strMacAddress = string.Empty;
        try
        {
            string strTempMacAddress = string.Empty;
            ProcessStartInfo objProcessStartInfo = new ProcessStartInfo();
            Process objProcess = new Process();
            objProcessStartInfo.FileName = "nbtstat";
            objProcessStartInfo.RedirectStandardInput = false;
            objProcessStartInfo.RedirectStandardOutput = true;
            objProcessStartInfo.Arguments = "-A " + IPAddress;
            objProcessStartInfo.UseShellExecute = false;
            objProcess = Process.Start(objProcessStartInfo);
            int Counter = -1;
            while (Counter <= -1)
            {
                Counter = strTempMacAddress.Trim().ToLower().IndexOf("mac address", 0);
                if (Counter > -1)
                {
                    break;
                }
                strTempMacAddress = objProcess.StandardOutput.ReadLine();
            }
            objProcess.WaitForExit();
            strMacAddress = strTempMacAddress.Trim();
        }
        catch (Exception Ex)
        {
            //Console.WriteLine(Ex.ToString());
            //Console.ReadLine();
        }
        return strMacAddress;
    } 



}

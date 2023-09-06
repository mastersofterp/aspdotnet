//==================================================//
//Created By  : Shrikant Bharne.//
//Created Date: 29-09-2022 .//
//Description : Purpose to Download Atlas Machine Bio metric Data.//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class ESTABLISHMENT_LEAVES_Master_attendance : System.Web.UI.Page
{
    string MachineName = "ATLAS";
    string deviceid = "1";
    private string _constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //btnReports.Visible = true;
                //btnRegistrationSlip.Visible = true;
                //string[] responseArrary = Convert.ToString(HttpContext.Current.Request["QUERY_STRING"]).Split('|');
                //string res = orig.Replace("\",\"", ";");
                //string res = Convert.ToString(HttpContext.Current.Request["QUERY_STRING"]).Replace("\",\"", ";");
                string res = Convert.ToString(HttpContext.Current.Request["QUERY_STRING"]).Replace("$", " ");
                res = res.Replace("*","");
                string[] A = res.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
               // $qs = str_replace('*', '', $qs); 
                //string[] A = res.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                //string[] responseArrary = Convert.ToString(HttpContext.Current.Request["QUERY_STRING"]).Split('$');
                //string abc = ;

                //string[] A = responseArrary.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

                string SID = "";
                string MID = "";
                string RFID ="";
                string DOT ="";

                //for ($i = 0; $i < count($submissions); $i++) {
                //$sections = explode('&', $submissions[$i]);                
                //if($i == 0) {
                //    $SID = $sections[0];
                //    $MID = $sections[1];
                //    $RFID = $sections[2];
                //    $DOT = $sections[3];
                //} else {
                //    $RFID = $sections[0];
                //     $DOT = $sections[1];
                   
                //}

                for (int i = 0; i < A.Length; i++ )
                {
                    string [] sections = A[i].Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries); //A[i].(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                    //A[i] += "  dad";
                    //Console.WriteLine(A[i]);

                    if(i == 0)
                    {
                        SID = sections[0];
                        MID = sections[1];
                        RFID = sections[2];
                        DOT = sections[3];
                    }
                    else
                    {
                        RFID = sections[0];
                        DOT = sections[1];
                    }

                    string dod = DOT;
                    //27122013113700
                    //27122213113722

                    string day = dod.Substring(0, 2);
                    string month = dod.Substring(2, 2);
                    string year = dod.Substring(4, 4);

                    string hour = dod.Substring(8, 2);
                    string min = dod.Substring(10, 2);
                    string sec = dod.Substring(12, 2);

                    //2022-08-22 17:35:40.000
                    

                    //string result_date = day+ '-'+month+'-'+year+' '+hour+':'+min+':'+sec;
                    string result_date = year+'-'+month+'-'+day+' '+hour+':'+min+':'+sec+'.'+"000";

                    string insquery1 = "insert into EMP_BIOATTENDANCE([UserID],[LogTime],[MachineName],[MachineNo],[SLogTime])  ";
                    //insquery1 += " select top 1 '" + RFID + "','" + result_date + "','" + MachineName + "','" + deviceid + "','" + Convert.ToDateTime(DateTime.Now.Date)+"','1','1','1'  from CHECKINOUTVJTI where  " + empid + "  not in (select USERID from CHECKINOUTVJTI where CHECKTIME='" + punchtime + "' and USERID=" + empid + ") ";
                    insquery1 += " select top 1 '" + RFID + "','" + result_date + "','" + MachineName + "','" + deviceid + "','" + Convert.ToString(Convert.ToDateTime(DateTime.Now.Date).ToString("yyyy-MM-dd")) + "'   from EMP_BIOATTENDANCE where  " + RFID + "  not in (select USERID from EMP_BIOATTENDANCE where LogTime='" + result_date + "' and USERID=" + RFID + ") ";


                    //DBCom.putData(insquery1);
                    int a = ExecuteNonQuery(insquery1); 
                    

                }


               
                
            }
        }
        catch (Exception ex)
        {

        }


    }

    /// <summary>
    /// To Execute Queries (Insert, Update or Delete) 
    /// </summary>
    /// <param name="connectionstring"></param>
    /// <returns>No. of affected records</returns>
    public int ExecuteNonQuery(String query)
    {
        SqlConnection cnn = new SqlConnection(_constr);
        SqlCommand cmd = new SqlCommand(query, cnn);
        if (query.StartsWith("INSERT") | query.StartsWith("insert") | query.StartsWith("UPDATE") | query.StartsWith("update") | query.StartsWith("DELETE") | query.StartsWith("delete"))
            cmd.CommandType = CommandType.Text;
        else
            cmd.CommandType = CommandType.StoredProcedure;

        int retval;
        try
        {
            cnn.Open();

            cmd.CommandTimeout = 600;
            retval = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            retval = 0;
            //throw new IITMSException("IITMS.SQLServer.SQLDAL.SQLHelper.ExecuteNonQuery-> " + ex.ToString());
        }
        finally
        {
            if (cnn.State == ConnectionState.Open) cnn.Close();
        }
        return retval;
    }
}
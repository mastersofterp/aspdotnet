//============================================================
// Namespace      : IITMS                                     
// Class          : Common                                    
// Description    : This Class consits of Common methods      
// Creation Date  : 08-Apr-2009                               
// Modifying Date :                                           
// Reason         :                                           
//============================================================

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Reflection;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using mastersofterp_MAKAUAT;

namespace IITMS
{
    namespace UAIMS
    {
        /// <summary>
        /// Summary description for Common Methods
        /// </summary>
        public class Common : IDisposable
        {
            //System.Data.SqlClient.SqlParameter

            /// <summary>
            /// ConnectionStrings
            /// </summary>
            string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            private string rfcString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS_RFCCONFIG"].ConnectionString;
            //report start date 
            public static DateTime reportStartDate = DateTime.Today.AddDays(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ReportDays"].ToString()));

            //for crystal reports
            //private static bool isServer = false;
            private static string isServer = System.Configuration.ConfigurationManager.AppSettings["isServer"].ToString();

            /// <summary>
            /// This method gets the common details from REFF Table
            /// </summary>
            /// <returns>SqlDataReader dr</returns>
            public SqlDataReader GetCommonDetails()
            {
                SqlDataReader dr = null;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                    string sql = "SELECT * FROM REFF";
                    dr = objSqlHelper.ExecuteReader(sql);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.Common.GetCommonDetails-> " + ex.ToString());
                }
                return dr;
            }

            public static string ToDescriptionString(CustomStatus val)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                   .GetType()
                   .GetField(val.ToString())
                   .GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : string.Empty;
            }

            public void DisplayMessage(Control UpdatePanelId, string Message, Page pg)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " alert('" + Message + "');", true);

            }
            public void DisplayUserMessage(Control UpdatePanelId, string Message, Page pg)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " alert('" + Message + "');", true);
                //string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
                //string message = string.Format(prompt, Message);
                //ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", message, false);
            }

            public void ConfirmMessage(Control UpdatePanelId, string Message, Page pg)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " confirm('" + Message + "');", true);

            }


            public void DisplayMessage(string Message, Page pg)
            {
                pg.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<Script language='javascript' type='text/javascript'> alert('" + Message + "'); </Script>");
            }


            // Added for library
            public byte[] GetNoPhotoImageData()
            {

                FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
                int ImageSize = (int)ff.Length;
                byte[] ImageContent = new byte[ff.Length];
                ff.Read(ImageContent, 0, ImageSize);
                ff.Close();
                ff.Dispose();
                return ImageContent;
            }

            public void DisplayMessage(Control UpdatePanelId, Message msg, Page pg)
            {
                string strMessage = GetMessages(msg);

                //ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", " alert('" + strMessage + "');", true);



                //add by jit on 
                string prompt = "<script>$(document).ready(function(){{$.prompt('{0}!');}});</script>";
                string message = string.Format(prompt, strMessage);

                ScriptManager.RegisterClientScriptBlock(UpdatePanelId, UpdatePanelId.GetType(), "Message", message, false);


            }

            public int UpdateUserinfo(UserAcc objUserAcc)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[7];

                    // objParams[0] = new SqlParameter("@OLDPASS", oldpass);
                    objParams[0] = new SqlParameter("@OLDPASS", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_OldPwd));
                    // objParams[1] = new SqlParameter("@NEWPASS", newpass);
                    objParams[1] = new SqlParameter("@NEWPASS", clsTripleLvlEncyrpt.ThreeLevelEncrypt(objUserAcc.UA_Pwd));
                    objParams[2] = new SqlParameter("@EMAIL", objUserAcc.EMAIL);
                    objParams[3] = new SqlParameter("@MOBNO", objUserAcc.MOBILE);
                    objParams[4] = new SqlParameter("@UA_NO", objUserAcc.UA_No);
                    objParams[5] = new SqlParameter("@Ipaddress", objUserAcc.IP_ADDRESS);
                    objParams[6] = new SqlParameter("@MacAddress", objUserAcc.MAC_ADDRESS);

                    objSQLHelper.ExecuteNonQuerySP("SP_USERINFO_UPDATE", objParams, false);
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateByDate -> " + ex.ToString());
                }

                return Convert.ToInt32(retStatus);
            }

            public int UpdateUserinfo(String oldpass, String newpass, String Email, int Userid, string Mobno, string IpAddress, string macAddress)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[7];

                    objParams[0] = new SqlParameter("@OLDPASS", oldpass);
                    objParams[1] = new SqlParameter("@NEWPASS", newpass);
                    objParams[2] = new SqlParameter("@EMAIL", Email);
                    objParams[3] = new SqlParameter("@MOBNO", Mobno);
                    objParams[4] = new SqlParameter("@UA_NO", Userid);
                    objParams[5] = new SqlParameter("@Ipaddress", IpAddress);
                    objParams[6] = new SqlParameter("@MacAddress", macAddress);

                    objSQLHelper.ExecuteNonQuerySP("SP_USERINFO_UPDATE", objParams, false);
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.UpdateByDate -> " + ex.ToString());
                }

                return Convert.ToInt32(retStatus);
            }

            public int RecordActivity(int UserNo, int PageNo, int Status)
            {
                int ret = 0;
                try
                {
                    SQLHelper objDataAccess;

                    if (System.Web.HttpContext.Current.Session["rfcconfig"] == null || System.Web.HttpContext.Current.Session["rfcconfig"].ToString() == "")
                    {
                        objDataAccess = new SQLHelper(_UAIMS_constr);
                    }
                    else
                    {
                        objDataAccess = new SQLHelper(rfcString);
                    }
                    //SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_LOG_ID", UserNo),
                            new SqlParameter("@P_PAGE_NO", PageNo),
                            new SqlParameter("@P_LOG_TIME", System.DateTime.Now),
                            new SqlParameter("@P_STATUS", Status),
                            new SqlParameter("@P_ACT_ID", SqlDbType.Int) 
                        };
                    sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                    ret = Convert.ToInt32(objDataAccess.ExecuteNonQuerySP("PKG_ACAD_RECORD_USER_ACTIVITY", sqlParams, true));
                    if (ret == -99)
                        ret = 0;

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Common.RecordActivity() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ret;
            }
            /// <summary>
            /// Sets the MasterPage and its Theme
            /// </summary>
            /// <param name="pg">Parameter pg is used to get page</param>
            /// <param name="mpage">Parameter mpage is used to retrieve name of masterpage</param>
            public void SetMasterPage(Page pg, string mpage)
            {
                if (mpage != "")
                {
                    //Set the masterpage
                    string masterpage = "~/" + mpage;
                    pg.MasterPageFile = masterpage;
                    if (pg.MasterPageFile.Contains("SiteMasterPage2"))
                    {
                        pg.Theme = "Theme2";
                    }
                    else
                    {
                        pg.Theme = "Theme1";
                    }
                }
                else
                {
                    //Set the original masterpage
                    string masterpage = "~/SiteMasterPage.master";
                    pg.MasterPageFile = masterpage;
                    pg.Theme = "Theme1";
                }
            }

            /// <summary>
            /// This method gets the help for page from Prm_Help table.
            /// </summary>
            /// <param name="helpid">Get page help as per this helpid.</param>
            /// <returns>Page Help in String format</returns>                 
            public String GetPageHelp(int helpid)
            {
                string pageHelp = string.Empty;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                    String sql = "SELECT * FROM PRM_HELP WHERE HAL_No = " + helpid;
                    SqlDataReader dr = objSqlHelper.ExecuteReader(sql);

                    if (dr != null)
                    {
                        if (dr.Read())
                        {
                            pageHelp = dr["HelpDesc"] == null ? "" : dr["HelpDesc"].ToString();
                        }
                    }
                    if (dr != null) dr.Close();
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.GetCommonDetails-> " + ex.ToString());
                }
                return pageHelp;
            }

            /// <summary>
            /// This method is used to get the data for filling any drop down list        
            /// </summary>
            /// <param name="storedprocedure">Get this storedprocedure to fill dropdownlist</param>
            /// <returns>DataSet</returns>
            public DataSet GetDropDownData(string storedprocedure)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objsqlhelper.ExecuteDataSetSP(storedprocedure, objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.GetDropDownData-> " + ex.ToString());
                }
                return ds;
            }
            public int sendEmail(string message, string emailid, string subject)
            {
                int ret = 0;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //SmtpClient SmtpServer = new SmtpClient("smtp-relay.gmail.com");



                DataSet dsconfig = null;
                dsconfig = FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,FASCILITY", string.Empty, string.Empty);
                string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
                string emailpass = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                int fascility = Convert.ToInt32(dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString());
                if (fascility == 1 || fascility == 3)
                {
                    if (emailfrom != "" && emailpass != "")
                    {

                        mail.From = new MailAddress(emailfrom);
                        string MailFrom = emailfrom;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, emailpass);
                        SmtpServer.EnableSsl = true;
                        string aa = string.Empty;
                        mail.Subject = subject;
                        mail.To.Clear();
                        mail.To.Add(emailid);

                        mail.IsBodyHtml = true;
                        mail.Body = message;
                        SmtpServer.UseDefaultCredentials = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };


                        SmtpServer.Send(mail);
                        if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
                        {
                            return ret = 1;

                            //Storing the details of sent email


                        }

                    }
                    return ret = 0;
                }
                return ret = 0;
            }
            //public int sendEmail(string message, string emailid, string subject)
            //{
            //    int ret = 0;
            //    try
            //    {
            //        DataSet dsconfig = null;
            //        dsconfig = FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,FASCILITY", string.Empty, string.Empty);
            //        string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            //        string emailpass = Common.DecryptPassword(dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString());
            //        int fascility = Convert.ToInt32(dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString());

            //        string smtpAddress = "smtp.gmail.com";
            //        int port = 587;

            //        if (emailfrom.ToLower().Contains("gmail.com"))
            //        {
            //            smtpAddress = "smtp.gmail.com";
            //        }
            //        else if (emailfrom.ToLower().Contains("yahoo.com") || emailfrom.ToLower().Contains("yahoo.in") || emailfrom.ToLower().Contains("yahoo.co.in"))
            //        {
            //            smtpAddress = "smtp.mail.yahoo.com";
            //        }
            //        else if (emailfrom.ToLower().Contains("live.com") || emailfrom.ToLower().Contains("hotmail.com"))
            //        {
            //            smtpAddress = "smtp.live.com";
            //        }

            //        ///Added By Mr.Manish Walde on 19Nov2015
            //        if (fascility == 1 || fascility == 3)
            //        {
            //            if (emailfrom != "" && emailpass != "")
            //            {
            //                SmtpClient smtp = new SmtpClient
            //                {
            //                    Host = smtpAddress, // smtp server address here...
            //                    Port = port,
            //                    EnableSsl = true,
            //                    DeliveryMethod = SmtpDeliveryMethod.Network,
            //                    Credentials = new System.Net.NetworkCredential(emailfrom, emailpass),
            //                    Timeout = 30000
            //                };
            //                MailMessage mailmsg = new MailMessage(emailfrom, emailid, subject, message);
            //                mailmsg.IsBodyHtml = true;
            //                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //                smtp.Send(mailmsg);
            //                if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
            //                {
            //                    return ret = 1;
            //                    //Storing the details of sent email
            //                }
            //            }
            //            return ret = 0;
            //        }
            //        return ret = 0;
            //    }
            //    catch (Exception ex)
            //    {
            //        return ret = 0;
            //    }

            //    #region Previous Logic Commented by Mr.Manish Walde on Dt.19-Nov-2015
            //    //int ret = 0;
            //    //MailMessage mail = new MailMessage();
            //    //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //    //DataSet dsconfig = null;
            //    //dsconfig = FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,FASCILITY", string.Empty, string.Empty);
            //    //string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
            //    //string emailpass = Common.DecryptPassword(dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString());
            //    //int fascility = Convert .ToInt32 (dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString());
            //    //if (fascility == 1 || fascility == 3)
            //    //{
            //    //    if (emailfrom != "" && emailpass!= "")
            //    //    {
            //    //        mail.From = new MailAddress(emailfrom);
            //    //        string MailFrom = emailfrom;
            //    //        SmtpServer.Port = 587;
            //    //        SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, emailpass);
            //    //        SmtpServer.EnableSsl = true;
            //    //        string aa = string.Empty;
            //    //        mail.Subject = subject;
            //    //        mail.To.Clear();
            //    //        mail.To.Add(emailid);

            //    //        mail.IsBodyHtml = true;
            //    //        mail.Body = message;
            //    //        SmtpServer.Send(mail);

            //    //        if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
            //    //        {
            //    //           return  ret = 1;
            //    //            //Storing the details of sent email
            //    //        }

            //    //    }
            //    //    return ret = 0;
            //    //}
            //    //return ret = 0;
            //    #endregion
            //}
            /// <summary>
            /// This method reads the selected links from the treeview
            /// </summary>
            /// <param name="tv">tv is the object of TreeView</param>
            /// <returns>links in the form of string</returns>
            public string GetLinks(System.Web.UI.WebControls.TreeView tv)
            {
                string links = "0,500";
                //Get the Selected Links
                for (int i = 0; i < tv.Nodes.Count - 1; i++)
                {
                    for (int j = 0; j < tv.Nodes[i].ChildNodes.Count; j++)
                    {
                        if (tv.Nodes[i].ChildNodes[j].Checked == true)
                            links += "," + tv.Nodes[i].ChildNodes[j].Value;

                        if (tv.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                        {
                            for (int k = 0; k < tv.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                                    links += "," + tv.Nodes[i].ChildNodes[j].ChildNodes[k].Value;


                                if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count > 0)
                                {
                                    for (int l = 0; l < tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                    {
                                        if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked == true)
                                            links += "," + tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value;
                                    }
                                }


                            }
                        }
                    }
                }
                return links;
            }

            /// <summary>
            /// This method shows error in a ModalBox
            /// </summary>
            /// <param name="pg">Parameter pg is used to get page</param>
            /// <param name="errormessage">This string parameter is used to display error message</param>
            public void ShowError(Page pg, string errormessage)
            {
                //////Get Reference for ajaxToolkit:ModalPopupExtender
                //AjaxControlToolkit.ModalPopupExtender modal = pg.Master.FindControl("programmaticModalPopup") as AjaxControlToolkit.ModalPopupExtender;
                //System.Web.UI.WebControls.Label lblError = pg.Master.FindControl("lblError") as System.Web.UI.WebControls.Label;
                //lblError.Text = errormessage;
                //modal.Show();
                try
                {
                }
                catch
                {
                    throw;
                }
            }
            public void ShowErrorMessage(Panel Panel_ErrorList, Label Label_Message, Message msg, MessageType mt)
            {
                string msgt = string.Empty;
                string strMessage = GetMessages(msg);
                if (MessageType.Alert == mt)
                {
                    msgt = "<strong>Alert:" + strMessage + " </strong>";

                }
                if (MessageType.Success == mt)
                {
                    msgt = "<strong>SUCCESS:" + strMessage + "  </strong>";

                }
                if (MessageType.Error == mt)
                {
                    msgt = "<strong>ERROR:" + strMessage + "  </strong>";

                }

                Panel_ErrorList.Visible = true;
                Label_Message.Text = msgt;
            }
            public enum MessageType
            {
                Success = 1,
                Alert = 2,
                Error = 3,
            }
            public enum Message
            {
                Saved = 1,
                NotSaved = 2,
                Deleted = 3,
                Updated = 4,
                DuplicateEntry = 5,
                ExceptionOccured = 6,
                NoFound = 7,
                NoRights = 8,
                NoReport = 9,
                NoAdd = 10,
                NoModify = 11,
                PhotoUploaded = 12,
                AlreadyLocked = 13,
                FileUploaded = 14,
                NoDelete = 15,
                SelectFromList = 16,
                MisMatched = 17,
                CautionMoneyExist = 18,
                ZeroReceipt = 19,
                CautionMoney = 20,
                AmtGreater = 21,
                NoBalanceAmt = 22

            }
            public string GetMessages(Message msg)
            {
                switch (msg)
                {
                    case Message.Saved:
                        return "Record Saved Successfully!";
                    case Message.NotSaved:
                        return "Record Not Saved!";
                    case Message.Deleted:
                        return "Record Deleted Successfully!";
                    case Message.Updated:
                        return "Record Updated Successfully!";
                    case Message.DuplicateEntry:
                        return "Record Already Exists!";
                    case Message.ExceptionOccured:
                        return "Exception Occured, Transaction Failed!";
                    case Message.NoFound:
                        return "Record Not Found!";
                    case Message.NoRights:
                        return "User Has No Right For Add, Modify And Report Generation. Contact To Administrator!";
                    case Message.NoAdd:
                        return "User Has No Right For Add. Contact To Administrator!";
                    case Message.NoModify:
                        return "User Has No Right For Modification. Contact To Administrator!";
                    case Message.NoReport:
                        return "User Has No Right For Report Generation. Contact To Administrator!";
                    case Message.PhotoUploaded:
                        return "Photo Uploaded Successfully!";
                    case Message.AlreadyLocked:
                        return "Record is Locked and can't be modified!";
                    case Message.FileUploaded:
                        return "File Uploaded Successfully!";
                    case Message.NoDelete:
                        return "User Has No Right For Deletion. Contact To Administrator!";
                    case Message.SelectFromList:
                        return "Select Data From The List!";
                    case Message.MisMatched:
                        return "Heads are Mismatching!";
                    case Message.CautionMoneyExist:
                        return "Caution Money Receipt Allready Generated For This Student!";
                    case Message.ZeroReceipt:
                        return "Zero Amount Receipt Cannot Be Generated!";
                    case Message.CautionMoney:
                        return "Want to Save Caution Money!";
                    case Message.AmtGreater:
                        return "Amount Should Not Greater Than Balance Amount!";
                    case Message.NoBalanceAmt:
                        return "No Balance Amount For The Selected Student!";
                }

                return "No Messages Available!";


            }
            /// <summary>
            /// Encrypts the Password
            /// </summary>
            /// <param name="password">Encrypt password in the form of string as per this password</param>
            /// <returns>Return string type encrypted password</returns>
            public static string EncryptPassword(string password)
            {
                string mchar = string.Empty;
                string pvalue = string.Empty;

                for (int i = 1; i <= password.Length; i++)
                {
                    mchar = password.Substring(i - 1, 1);
                    byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
                    int no = int.Parse(bt[0].ToString());
                    char ch = Convert.ToChar(no + i);
                    pvalue += ch.ToString();
                }
                return pvalue;
            }

            /// <summary>
            /// Decrypts the Password
            /// </summary>
            /// <param name="password">Decrypt password in the form of string as per this password</param>
            /// <returns>Return decrypted password in the form of string</returns>
            public static string DecryptPassword(string password)
            {
                string mchar = string.Empty;
                string pvalue = string.Empty;

                for (int i = 1; i <= password.Length; i++)
                {
                    mchar = password.Substring(i - 1, 1);
                    byte[] bt = System.Text.Encoding.Unicode.GetBytes(mchar);
                    int no = int.Parse(bt[0].ToString());
                    char ch = Convert.ToChar(no - i);
                    pvalue += ch.ToString();
                }
                return pvalue;
            }

            ///// <summary>
            ///// Generates the Scrolling News using Marque
            ///// </summary>
            ///// <param name="path">Used to get path in the form of string</param>
            ///// <returns>String</returns>
            //public string ScrollingNews(string path)
            //{
            //    string retNews = string.Empty;

            //    SqlDataReader dr = null;
            //    try
            //    {
            //        StringBuilder newshtml = new StringBuilder();
            //        //string con_str = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
            //        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = new SqlParameter[1];
            //        objParams[0] = new SqlParameter("COMMON_CURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

            //        dr = objSqlHelper.ExecuteReaderSP("Pkg_News.SP_ACTIVE_NEWS", objParams);

            //        if (dr != null)
            //        {
            //            StringBuilder str = new StringBuilder();
            //            while (dr.Read())
            //            {
            //                //listview.txtMANAGEMENT</a
            //                str.Append(dr["TITLE"].ToString() + "<br>");
            //                string url = ".." + path + "\\upload_files\\";

            //                if (dr["link"] != null && dr["link"].ToString() != string.Empty)
            //                    if (dr["filename"] != null && dr["filename"].ToString() != string.Empty)
            //                        str.Append("<p><a Target=_blank href=" + url + dr["filename"].ToString() + ">" + dr["link"].ToString() + "</a></p>");


            //                str.Append("<br>");
            //                str.Append(dr["NEWSDESC"].ToString());
            //                str.Append("<br>");
            //            }
            //            newshtml.Append("<P><FONT SIZE='2' COLOR='Black'>" + str.ToString() + "</Font></P>");

            //            int iSpeed = 5;    //Speed of Marquee (higher = slower)
            //            int iWidth = 260;
            //            int iHeight = 300;

            //            retNews = "<MARQUEE onmouseover='this.stop();' ";
            //            retNews += "onmouseout='this.start();'direction='up' scrollamount='1' ";
            //            retNews += "scrolldelay='" + iSpeed + "'";
            //            retNews += "width='" + iWidth + "' height='" + iHeight + "'>" + newshtml.ToString() + "</MARQUEE>";
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IITMSException(ex.ToString());
            //    }
            //    if (dr != null) dr.Close();

            //    return retNews;
            //}

            /// <summary>
            /// This method is used to fill Odd/Even session in the dropdownlist.
            /// </summary>dified by Rishabh on 27.07.2022
            /// Mo
            /// <param name="ddl">Used to get name of dropdownlist</param>
            public void FillOddEven(DropDownList ddl)
            {
                ddl.Items.Add(new ListItem("Please Select", "0"));
                ddl.Items.Add(new ListItem("Odd", "1"));
                ddl.Items.Add(new ListItem("Even", "2"));
                ddl.Items.Add(new ListItem("Summer", "3"));
                ddl.Items.Add(new ListItem("Year", "4"));
                ddl.Items.Add(new ListItem("Trimester", "5"));
            }

            /// <summary>
            /// This method is used to fill Exam Status in the dropdownlist.
            /// </summary>
            /// <param name="ddl">Used to get name of dropdownlist</param>
            public void FillExamStatus(DropDownList ddl)
            {
                ddl.Items.Add(new ListItem("Please Select", "0"));
                ddl.Items.Add(new ListItem("Regular Exam", "1"));
                ddl.Items.Add(new ListItem("Summer Term Exam", "2"));
                ddl.Items.Add(new ListItem("Special Supplementary", "3"));
            }

            /// <summary>
            /// This method is used to write error details in text file.
            /// </summary>
            /// <param name="errormessage">Used to get error message</param>
            public void WriteErrorDetails(string errormessage)
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/error/");

                string filename = DateTime.Now.Day.ToString() + "_enrollmentno" + DateTime.Now.Month.ToString() + "_enrollmentno" + DateTime.Now.Year.ToString() + ".txt";

                if (File.Exists(path + filename))
                {
                    StreamWriter sw = File.AppendText(path + filename);
                    sw.WriteLine();
                    sw.WriteLine("Error : " + errormessage);
                    sw.Close();
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    string[] fl = Directory.GetFiles(path);

                    File.Delete(fl[0]);

                    //create new file [StreamWriter sw = File.CreateText(filename)].
                    StreamWriter sw = File.CreateText(path + filename);
                    sw.WriteLine("Error : " + errormessage);
                    sw.Close();
                }
            }

            //public static ConnectionInfo GetCrystalConnection()
            //{
            //    //SET Login Details
            //    //DB DETAILS
            //    ConnectionInfo connectionInfo = new ConnectionInfo();

            //    if (isServer == "true")
            //    {
            //        //Following for Remote Server
            //        connectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();
            //        connectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
            //        connectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"].ToString();
            //        connectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();
            //    }
            //    else
            //    {
            //        //Following for Local
            //        connectionInfo.ServerName = "ANUP\\SQLEXPRESS";
            //        connectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["LocalServer"].ToString();
            //        connectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();
            //        connectionInfo.IntegratedSecurity = true;
            //    }

            //    return connectionInfo;
            //}

            // modified by S.Patil - 09 Jan 2020
            public static ConnectionInfo GetCrystalConnection()
            {
                //SET Login Details
                //DB DETAILS
                ConnectionInfo connectionInfo = new ConnectionInfo();

                //if (isServer == "true")
                //{
                //Following for Remote Server
                connectionInfo.UserID = System.Configuration.ConfigurationManager.AppSettings["UserID"].ToString();
                connectionInfo.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
                connectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["Server"].ToString();
                connectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();
                //}
                //else
                //{
                //    //Following for Local
                //    connectionInfo.ServerName = "ANUP\\SQLEXPRESS";
                //    connectionInfo.ServerName = System.Configuration.ConfigurationManager.AppSettings["LocalServer"].ToString();
                //    connectionInfo.DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DataBase"].ToString();
                //    connectionInfo.IntegratedSecurity = true;
                //}

                return connectionInfo;
            }


            public static void SetDBLogonForReport(ConnectionInfo connectionInfo, ReportDocument reportDocument)
            {
                Tables tables = reportDocument.Database.Tables;
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
                {
                    TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                    tableLogonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(tableLogonInfo);
                }
            }

            public DataSet FillDropDown(string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                }
                return ds;
            }

            public void FillDropDownList(DropDownList ddlList, string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                    ddlList.Items.Clear();
                    ddlList.Items.Add("Please Select");
                    ddlList.SelectedItem.Value = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlList.DataSource = ds;
                        ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlList.DataBind();
                        ddlList.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                }
                // return ddlList;
            }

            public void FillDropDownList(string initialText, DropDownList ddlList, string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                    ddlList.Items.Clear();
                    ddlList.Items.Add(initialText);
                    ddlList.SelectedItem.Value = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlList.DataSource = ds;
                        ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlList.DataBind();
                        ddlList.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                }
                // return ddlList;
            }
            public void FillDropDownList(string initialValue, string initialText, DropDownList ddlList, string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                    ddlList.Items.Clear();
                    ddlList.Items.Add(initialText);
                    ddlList.SelectedItem.Value = initialValue;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlList.DataSource = ds;
                        ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlList.DataBind();
                        ddlList.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                }
                // return ddlList;
            }

            public string LookUp(string tablename, string columnname, string wherecondition)
            {
                string ret = string.Empty;

                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_TABLENAME", tablename);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME", columnname);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[2] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[2] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);


                    ret = Convert.ToString(objsqlhelper.ExecuteScalarSP("PKG_UTILS_SP_LOOKUP", objParams));
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.lookup-> " + ex.ToString());
                }
                return ret;
            }


            public static bool CheckPage(int userno, string pageid, int loginid, int status)
            {
                try
                {
                    User_AccController objUC = new User_AccController();
                    UserAcc objUA = objUC.GetSingleRecordByUANo(userno);
                    Common objCommon = new Common();
                    objCommon.RecordActivity(loginid, Convert.ToInt32(pageid), status);
                    if (objUA.UA_No != 0)
                    {
                        char sp = ',';
                        string[] pageids = objUA.UA_Acc.Split(sp);

                        for (int i = 0; i < pageids.Length; i++)
                        {
                            if (pageid.Equals(pageids[i]))
                            {
                                return true;
                            }
                        }
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.CheckPage-> " + ex.ToString());
                }
            }
            public void ReportPopUp(System.Web.UI.WebControls.WebControl opener, string PagePath, string windowName)
            {
                string clientScript = string.Empty;
                string webservername = System.Configuration.ConfigurationManager.AppSettings["WebServer"].ToString();
                string virtualDirectory = System.Configuration.ConfigurationManager.AppSettings["VirtualDirectory"].ToString();
                string clientReportPath = System.Configuration.ConfigurationManager.AppSettings["clientReportPath"].ToString();

                if (isServer == "true")
                    //clientScript = "window.open('" + webservername + "/reports/commonreport.aspx?" + PagePath + "','" + windowName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');return false;";

                    clientScript = "window.open('" + webservername + "/" + virtualDirectory + "/reports/commonreport.aspx?" + PagePath + "','" + windowName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');return false;";
                else
                    clientScript = "window.open('" + clientReportPath + "Reports/commonreport.aspx?" + PagePath + "','" + windowName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');return false;";

                //clientScript = "window.open('http://localhost:2246/PresentationLayer/reports/commonreport.aspx?" + PagePath + "','" + windowName + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');return false;";

                opener.Attributes.Add("onClick", clientScript);
            }

            public byte[] GetImageData(FileUpload fu)
            {
                if (fu.HasFile)
                {
                    int ImageSize = fu.PostedFile.ContentLength;
                    Stream ImageStream = fu.PostedFile.InputStream as Stream;
                    byte[] ImageContent = new byte[ImageSize];
                    int intStatus = ImageStream.Read(ImageContent, 0, ImageSize);
                    ImageStream.Close();
                    ImageStream.Dispose();
                    return ImageContent;
                }
                else
                {
                    FileStream ff = new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/images/nophoto.jpg"), FileMode.Open);
                    int ImageSize = (int)ff.Length;
                    byte[] ImageContent = new byte[ff.Length];
                    ff.Read(ImageContent, 0, ImageSize);
                    ff.Close();
                    ff.Dispose();
                    return ImageContent;
                }
            }

            public void FillListBox(ListBox lstbox, string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);

                    lstbox.Items.Clear();
                    //lstbox.Items.Add("Please Select");
                    //lstbox.SelectedItem.Value = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lstbox.DataSource = ds;
                        lstbox.DataValueField = ds.Tables[0].Columns[0].ToString();
                        lstbox.DataTextField = ds.Tables[0].Columns[1].ToString();
                        lstbox.DataBind();
                        //lstbox.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillListBox-> " + ex.ToString());
                }
                // return ddlList;
            }

            public object ReadTextFile(FileUpload ful)
            {

                object specDoc = null;

                if (ful.HasFile)
                {
                    if (ful.PostedFile.ContentType == "text/plain")
                    {
                        if (ful.PostedFile.ContentLength <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TextFileSize"]))
                        {
                            TextReader trs = new StreamReader(ful.FileContent);
                            specDoc = Convert.ToString(trs.ReadToEnd());
                        }
                        else
                        {
                            /*If specDoc = "S" then fire the validation uploaded file size is greate than 4mb */
                            specDoc = Convert.ToInt32(CustomStatus.TextFileSize);
                        }

                    }
                    else
                    {
                        /*If specDoc = "T" then fire the validation uploaded file is not text file */
                        specDoc = Convert.ToInt32(CustomStatus.TextFileCheck);
                    }
                }
                else
                {

                    specDoc = Convert.ToInt32(CustomStatus.FileNotExists);
                }

                return specDoc;
            }

            public void FillListBoxWithOutClear(ListBox lstbox, string TableName, string Column1, string Column2, string wherecondition, string orderby)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);
                    objParams[1] = new SqlParameter("@P_COLUMNNAME_1", Column1);
                    objParams[2] = new SqlParameter("@P_COLUMNNAME_2", Column2);
                    if (!wherecondition.Equals(string.Empty))
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", wherecondition);
                    else
                        objParams[3] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);
                    if (!orderby.Equals(string.Empty))
                        objParams[4] = new SqlParameter("@P_ORDERBY", orderby);
                    else
                        objParams[4] = new SqlParameter("@P_ORDERBY", DBNull.Value);

                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_UTILS_SP_DROPDOWN", objParams);


                    //lstbox.Items.Add("Please Select");
                    //lstbox.SelectedItem.Value = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lstbox.DataSource = ds;
                        lstbox.DataValueField = ds.Tables[0].Columns[0].ToString();
                        lstbox.DataTextField = ds.Tables[0].Columns[1].ToString();
                        lstbox.DataBind();
                        //lstbox.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillListBox-> " + ex.ToString());
                }
                // return ddlList;
            }



            public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink)
            {
                DataTableReader dtr = null;

                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                    objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);

                    DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY", objParams);
                    if (ds.Tables.Count > 0)
                        dtr = ds.Tables[0].CreateDataReader();
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.CheckActivity-> " + ex.ToString());
                }
                return dtr;
            }
            public DataSet GetCEPCurrSession()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_CEP_GET_CURRENT_SESSION", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.GetCEPCurrSession-> " + ex.ToString());
                }
                return ds;
            }

            public void FillSalfileDropDownList(DropDownList ddlList)
            {
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];
                    DataSet ds = null;
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_Fill_SALFILE_DROPDOWN", objParams);

                    ddlList.Items.Clear();
                    ddlList.Items.Add("Please Select");
                    ddlList.SelectedItem.Value = "0";

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlList.DataSource = ds;
                        ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                        ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                        ddlList.DataBind();
                        ddlList.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.FillSalfileDropDownList-> " + ex.ToString());
                }
                // return ddlList;
            }

            /// <summary>
            /// Added By Aayushi on Dated 11/01/2019 for Time Table
            /// </summary>
            /// <returns></returns>
            public DataSet GetAllCategort()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_CATEGORY", objParams);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAllConfig-> " + ex.ToString());
                }
                return ds;
            }

            /// <summary>
            /// To extract the idno from AutoComplete TextBox
            /// </summary>
            /// <param name="txt"></param>
            /// <returns></returns>
            public string GetIDNo(TextBox txt)
            {
                string idno = "0";
                if (txt.Text.Trim() != string.Empty && txt.Text.Contains("["))
                {
                    char[] sp = { '[' };
                    string[] data = txt.Text.Trim().Split(sp);
                    //idno value
                    idno = data[1].Replace("]", "");
                }
                return idno;
            }

            /// <summary>
            /// Get the data to be filled in the AutoComplete Textbox by idno
            /// </summary>
            /// <param name="idno"></param>
            /// <param name="tablename"></param>
            /// <param name="col1"></param>
            /// <param name="col2"></param>
            /// <returns></returns>
            public string GetDataByIDNo(int idno, string tablename, string col1, string col2)
            {
                string data = string.Empty;
                DataSet ds = this.FillDropDown(tablename, col1, col2, col1 + " = " + idno, string.Empty);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        data = ds.Tables[0].Rows[0][1].ToString() + " [" + ds.Tables[0].Rows[0][0].ToString() + "]";
                    }
                }

                return data;
            }

            //This method is used to add the master table data and
            //retun the idno of that master entry
            public int AddMasterTableData(string tablename, string col1, string col2, string col2data, int idno)
            {
                int ret = 0;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_TABLENAME", tablename),
                            new SqlParameter("@P_COL1", col1),
                            new SqlParameter("@P_COL2", col2),
                            new SqlParameter("@P_COL2VALUE", col2data),
                            new SqlParameter("@P_IDNO", SqlDbType.Int)                            
                        };
                    sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                    ret = Convert.ToInt32(objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_MASTER", sqlParams, true));
                    if (ret != -99)
                        ret = Convert.ToInt32(sqlParams[sqlParams.Length - 1].Value);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Common.AddMasterTableData() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ret;
            }

            // USED IN ITLE ONLINE TEST FORM 17/02/2014

            public string _client_constr = string.Empty; //System.Configuration.ConfigurationManager.ConnectionStrings["CLIENT"].ConnectionString;

            public Common()
            {

                _client_constr = _UAIMS_constr;
                //Constructor without parameter

            }
            // for sending sms 
            //public void SendSMSToClient(string MessageSender, string MobileNo, string Message160Char, string collegeCode)
            //{
            //    //string userName = "demovfnrtxml41";
            //    //string password = "pKmqr16wK";

            //    string userName = "sms-nita";
            //    string password = "n!t@gartala";
            //    string tag = "";
            //    string url = string.Empty;
            //    //string msgsender = MessageSender;// "IITMS";
            //    string msgsender = "IITMS";
            //    //919372775556
            //    //919822736330
            //    //919823114430
            //    //919422290083
            //    string receiver = "91" + MobileNo; //single receiver hardcoded// somani sir's no.
            //    StringBuilder data = new StringBuilder();
            //    data.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            //    data.Append("<!DOCTYPE MESSAGE SYSTEM \"http://127.0.0.1/psms/dtd/message.dtd\" >");
            //    data.Append("<MESSAGE>");
            //    data.Append("<USER USERNAME=\"" + userName + "\" PASSWORD=\"" + password + "\"/>");
            //    data.Append("<SMS UDH=\"0\" CODING=\"1\" TEXT=\"" + Message160Char + "\" PROPERTY=\"\" ID=\"1\">");
            //    data.Append("<ADDRESS FROM=\"" + msgsender + "\" TO=\"" + receiver + "\" SEQ=\"1\" TAG=\"" + tag + "\" />");
            //    data.Append("</SMS>");
            //    data.Append("</MESSAGE>&action=sms");

            //    url = "http://api.myvaluefirst.com/psms/servlet/psms.Eservice2?data=" + encodeMessage(ref data);

            //    WebRequest wbRequest = WebRequest.Create(url);
            //    WebResponse wbResponse = wbRequest.GetResponse();
            //    Stream ReceiveStream = wbResponse.GetResponseStream();
            //    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            //    StreamReader readStream = new StreamReader(ReceiveStream, encode);
            //    string strResponse = readStream.ReadToEnd();

            //}

            public string encodeMessage(ref StringBuilder data)
            {
                return data.ToString().Replace("%", "%25").Replace("*", "%2A").Replace("#", "%23").Replace("<", "%3C").Replace(">", "%3E").Replace("+", "%2B").Replace("\n", "%0D%0A").Replace(" ", "%20").Replace("\"", "%22");
            }
            public int DeleteClientTableRow(string TableName, string Wherecondition)
            {
                int ret;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_client_constr);
                    SqlParameter[] objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);

                    if (!Wherecondition.Equals(string.Empty))
                        objParams[1] = new SqlParameter("@P_WHERECONDITION", Wherecondition);
                    else
                        objParams[1] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);

                    objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[2].Direction = ParameterDirection.Output;

                    object obj = objsqlhelper.ExecuteNonQuerySP("PKG_UTILS_SP_DELETE", objParams, true);

                    if (obj != null && obj.ToString().Equals("-99"))
                    {
                        ret = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    else
                    {
                        ret = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return ret;
            }

            public int DeleteRow(string TableName, string Wherecondition)
            {
                int ret;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_TABLENAME", TableName);

                    if (!Wherecondition.Equals(string.Empty))
                        objParams[1] = new SqlParameter("@P_WHERECONDITION", Wherecondition);
                    else
                        objParams[1] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);

                    objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[2].Direction = ParameterDirection.Output;

                    object obj = objsqlhelper.ExecuteNonQuerySP("PKG_UTILS_SP_DELETE", objParams, true);

                    if (obj != null && obj.ToString().Equals("-99"))
                    {
                        ret = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    else
                    {
                        ret = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return ret;
            }


            public string GetCatLinks(System.Web.UI.WebControls.TreeView tv)
            {
                string links = string.Empty;
                //Get the Selected Links
                for (int i = 0; i < tv.Nodes.Count; i++)
                {
                    if (tv.Nodes[i].Checked == true)
                        links += tv.Nodes[i].Value + ",";
                    for (int j = 0; j < tv.Nodes[i].ChildNodes.Count; j++)
                    {
                        if (tv.Nodes[i].ChildNodes[j].Checked == true)
                            links += tv.Nodes[i].ChildNodes[j].Value + ",";

                        if (tv.Nodes[i].ChildNodes[j].ChildNodes.Count > 0)
                        {
                            for (int k = 0; k < tv.Nodes[i].ChildNodes[j].ChildNodes.Count; k++)
                            {
                                if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].Checked == true)
                                    links += tv.Nodes[i].ChildNodes[j].ChildNodes[k].Value + ",";

                                for (int l = 0; l < tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes.Count; l++)
                                {
                                    if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Checked == true)
                                        links += tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].Value + ",";

                                    for (int m = 0; m < tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes.Count; m++)
                                    {
                                        if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Checked == true)
                                            links += tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].Value + ",";

                                        for (int n = 0; n < tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes.Count; n++)
                                        {
                                            if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Checked == true)
                                                links += tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].Value + ",";

                                            for (int o = 0; o < tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes.Count; o++)
                                            {
                                                if (tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Checked == true)
                                                    links += tv.Nodes[i].ChildNodes[j].ChildNodes[k].ChildNodes[l].ChildNodes[m].ChildNodes[n].ChildNodes[o].Value + ",";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (links != string.Empty)
                    links = links.Remove(links.Length - 1, 1);
                return links;
            }

            public DataSet GetAllConfig()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_CONFIG", objParams);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAllConfig-> " + ex.ToString());
                }
                return ds;
            }

            public SqlDataReader GetSingleConfig(int ConfigID)
            {
                SqlDataReader dr = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CONFIGID", ConfigID);
                    dr = objSQLHelper.ExecuteReaderSP("PKG_GET_CONFIG_BY_CONFIGID", objParams);
                }
                catch (Exception ex)
                {
                    return dr;
                    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleConfig-> " + ex.ToString());
                }
                return dr;
            }

            //Categorywise Fees 12/01/2019
            public DataSet GetCategoryWiseFeesConfig(int ConfigID)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CONFIGID", ConfigID);
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COTEGORYWISEFEES_BY_CONFIGID", objParams);
                }
                catch (Exception ex)
                {
                    return ds;
                    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetCategoryWiseFeesConfig-> " + ex.ToString());
                }
                return ds;
            }

            //public int AddOnline(Config objConfig)
            //{
            //    //int status = Convert.ToInt32(CustomStatus.Others);
            //    int status = 0;
            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = null;

            //        //Add New Adm
            //        objParams = new SqlParameter[12];

            //        objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
            //        objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
            //        objParams[2] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);
            //        objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
            //        objParams[4] = new SqlParameter("@P_STARTTIME", objConfig.Config_STime);
            //        objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
            //        objParams[6] = new SqlParameter("@P_ENDTIME", objConfig.Config_ETime);
            //        objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
            //        objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
            //        objParams[9] = new SqlParameter("@P_COLLEGE_ID", objConfig.College_Id);
            //        objParams[10] = new SqlParameter("@P_BulkCateAmtOnlTbl", objConfig.dtOnlineAdmConfi);
            //        objParams[11] = new SqlParameter("@P_CONFIGID", SqlDbType.Int);
            //        objParams[11].Direction = ParameterDirection.Output;

            //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_ADMISSION_CONFIG", objParams, true);

            //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
            //            status = Convert.ToInt32(CustomStatus.RecordSaved);
            //        else
            //            status = Convert.ToInt32(CustomStatus.Error);
            //    }
            //    catch (Exception ex)
            //    {
            //        status = Convert.ToInt32(CustomStatus.Error);
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.AddSession-> " + ex.ToString());
            //    }
            //    return status;
            //}

            /// <summary>
            /// Updated By Dileep Kare
            /// Date:17-01-2020
            /// </summary>
            /// <param name="objConfig"></param>
            /// <param name="form_category"></param>
            /// <param name="STime"></param>
            /// <param name="ETime"></param>
            /// <param name="collegeId"></param>
            /// <param name="UgPg"></param>
            /// <returns></returns>

            public int AddOnline(Config objConfig, int form_category, string STime, string ETime, int UgPg, int orgId, int activeStatus)
            {
                //int status = Convert.ToInt32(CustomStatus.Others);
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    //Add New Adm
                    objParams = new SqlParameter[19];
                    objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
                    objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);

                    objParams[2] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);     //Added By Yogesh K. on Date: 25-01-2023
                    
                    objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
                    objParams[4] = new SqlParameter("@P_STARTTIME", STime);
                    objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
                    objParams[6] = new SqlParameter("@P_ENDTIME", ETime);
                    objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
                    objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
                    objParams[9] = new SqlParameter("@P_FORM_CATEGORY", form_category);
                    objParams[10] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
                    objParams[11] = new SqlParameter("@P_UGPG", UgPg);
                    objParams[12] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);
                    
                    objParams[13] = new SqlParameter("@P_CDBNO", objConfig.Cdbno);
                    objParams[14] = new SqlParameter("@P_DEPTNO", objConfig.Deptno);
                    
                    objParams[15] = new SqlParameter("@P_AGE", objConfig.Age);          //Added by Nikhil L. on 27/12/2021
                    objParams[16] = new SqlParameter("@P_ORGANIZATION_ID", orgId);          //Added by Nikhil L. on 05/01/2022 to maintain organization id.
                    objParams[17] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);  // Added by Nikhil L. on 14/02/2022 to maintain the status of admission notification.
                    objParams[18] = new SqlParameter("@P_CONFIGID", SqlDbType.Int);
                    objParams[18].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_ADMISSION_CONFIG", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.AddSession-> " + ex.ToString());
                }
                return status;
            }
            //public int UpdateOnline(Config objConfig)
            //{
            //    int retStatus = Convert.ToInt32(CustomStatus.Others);

            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = null;

            //        //update
            //        objParams = new SqlParameter[12];
            //        objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
            //        objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
            //        objParams[2] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);
            //        objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
            //        objParams[4] = new SqlParameter("@P_STARTTIME", objConfig.Config_STime);
            //        objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
            //        objParams[6] = new SqlParameter("@P_ENDTIME", objConfig.Config_ETime);
            //        objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
            //        objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
            //        objParams[9] = new SqlParameter("@P_COLLEGE_ID", objConfig.College_Id);
            //        objParams[10] = new SqlParameter("@P_CONFIGID", objConfig.ConfigID);
            //        objParams[11] = new SqlParameter("@P_BulkCateAmtOnlTbl", objConfig.dtOnlineAdmConfi);

            //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMISSION_CONFIG", objParams, false) != null)
            //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            //    }
            //    catch (Exception ex)
            //    {
            //        retStatus = Convert.ToInt32(CustomStatus.Error);
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdateCT-> " + ex.ToString());
            //    }
            //    return retStatus;
            //}


            /// <summary>
            /// Updated By Dileep Kare
            /// date:17-01-2020
            /// </summary>
            /// <param name="objConfig"></param>
            /// <param name="form_category"></param>
            /// <param name="STime"></param>
            /// <param name="ETime"></param>
            /// <param name="collegeId"></param>
            /// <param name="UgPg"></param>
            /// <returns></returns>
            //public int UpdateOnline(Config objConfig, int form_category, string STime, string ETime, int UgPg)
            //{
            //    int retStatus = Convert.ToInt32(CustomStatus.Others);

            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = null;

            //        //update
            //        objParams = new SqlParameter[17];
            //        objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
            //        objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
            //        //objParams[2] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);
            //        objParams[2] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
            //        objParams[3] = new SqlParameter("@P_STARTTIME", STime);
            //        objParams[4] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
            //        objParams[5] = new SqlParameter("@P_ENDTIME", ETime);
            //        objParams[6] = new SqlParameter("@P_DETAILS", objConfig.Details);
            //        objParams[7] = new SqlParameter("@P_FEES", objConfig.Fees);
            //        objParams[8] = new SqlParameter("@P_FORM_CATEGORY", form_category);
            //        objParams[9] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
            //        objParams[10] = new SqlParameter("@P_UGPG", UgPg);
            //        objParams[11] = new SqlParameter("@P_CONFIGID", objConfig.ConfigID);
            //        objParams[12] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);
            //        //objParams[14] = new SqlParameter("@P_CDBNO", objConfig.Cdbno);
            //        //objParams[15] = new SqlParameter("@P_DEPTNO", objConfig.Deptno);
            //        objParams[13] = new SqlParameter("@P_AGE", objConfig.Age);          //Added by Nikhil L. on 27/12/2021

            //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMISSION_CONFIG", objParams, false) != null)
            //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            //    }
            //    catch (Exception ex)
            //    {
            //        retStatus = Convert.ToInt32(CustomStatus.Error);
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdateCT-> " + ex.ToString());
            //    }
            //    return retStatus;
            //}


            //FOR RESERVATION CONFIGURATION --[13-JUNE-2016]

            public int UpdateOnline(Config objConfig, int form_category, string STime, string ETime, int UgPg, int activeStatus)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    //update
                    objParams = new SqlParameter[18];
                    objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
                    objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);

                    objParams[2] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);      //Added By Yogesh K. on Date: 25-01-2023
                    
                    objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
                    objParams[4] = new SqlParameter("@P_STARTTIME", STime);
                    objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
                    objParams[6] = new SqlParameter("@P_ENDTIME", ETime);
                    objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
                    objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
                    objParams[9] = new SqlParameter("@P_FORM_CATEGORY", form_category);
                    objParams[10] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
                    objParams[11] = new SqlParameter("@P_UGPG", UgPg);
                    objParams[12] = new SqlParameter("@P_CONFIGID", objConfig.ConfigID);
                    objParams[13] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);
                    
                    objParams[14] = new SqlParameter("@P_CDBNO", objConfig.Cdbno);
                    objParams[15] = new SqlParameter("@P_DEPTNO", objConfig.Deptno);
                    
                    objParams[16] = new SqlParameter("@P_AGE", objConfig.Age);          //Added by Nikhil L. on 27/12/2021
                    objParams[17] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);          //Added by Nikhil L. on 14/02/2022 to maintain the status of admission notification.

                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMISSION_CONFIG", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdateCT-> " + ex.ToString());
                }
                return retStatus;
            }

            public int UpdateResevationConfig(Config objConfig)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    //update
                    objParams = new SqlParameter[8];
                    objParams[0] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
                    objParams[1] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);
                    objParams[2] = new SqlParameter("@P_INTAKE", objConfig.Intake);
                    objParams[3] = new SqlParameter("@P_SCQUOTA", objConfig.SCQuota);
                    objParams[4] = new SqlParameter("@P_STQUOTA", objConfig.STQuota);
                    objParams[5] = new SqlParameter("@P_GENQUOTA", objConfig.GENQuota);
                    objParams[6] = new SqlParameter("@P_OBCQUOTA", objConfig.OBCQuota);

                    objParams[7] = new SqlParameter("@P_CNFNO", objConfig.Cnfno);

                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_RESERVATION_CONFIG", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdateCT-> " + ex.ToString());
                }

                return retStatus;
            }

            public int AddResevationConfig(Config objConfig)
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[8];
                    objParams[0] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
                    objParams[1] = new SqlParameter("@P_BRANCHNO", objConfig.BranchNo);
                    objParams[2] = new SqlParameter("@P_INTAKE", objConfig.Intake);
                    objParams[3] = new SqlParameter("@P_SCQUOTA", objConfig.SCQuota);
                    objParams[4] = new SqlParameter("@P_STQUOTA", objConfig.STQuota);
                    objParams[5] = new SqlParameter("@P_GENQUOTA", objConfig.GENQuota);

                    objParams[6] = new SqlParameter("@P_OBCQUOTA", objConfig.OBCQuota);
                    objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[7].Direction = System.Data.ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_RESERVATION_CONFIG", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32((CustomStatus.Error));
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.AddResevationConfig-> " + ex.ToString());
                }
                return retStatus;
            }

            public SqlDataReader GetSingleReservationConfig(Config objConfig)
            {
                SqlDataReader dr = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CNFNO", objConfig.Cnfno);
                    dr = objSQLHelper.ExecuteReaderSP("PKG_GET_RESERVATION_CONFIG_BY_CNFNO", objParams);
                }
                catch (Exception ex)
                {
                    return dr;
                    throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleReservationConfig-> " + ex.ToString());
                }
                return dr;
            }

            //FOR PAYMENT TYPE MODIFICATION --[16-JUNE-2016]


            ////public DataSet GetListOfStudents(string USERNAME, int ADMBATCH)
            ////{
            ////    DataSet ds = null;
            ////    try
            ////    {
            ////        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            ////        SqlParameter[] objParams = null;
            ////        objParams = new SqlParameter[2];
            ////        objParams[0] = new SqlParameter("@USERNAME", USERNAME);
            ////        objParams[1] = new SqlParameter("@ADMBATCH", ADMBATCH);


            ////        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT-TYPE_MODIFICATION", objParams);

            ////    }
            ////    catch (Exception ex)
            ////    {

            ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
            ////    }
            ////    return ds;
            ////}

            //public DataSet GetListOfStudents(string ENROLLMENTNO)
            //{
            //    DataSet ds = null;
            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = null;
            //        objParams = new SqlParameter[1];
            //        objParams[0] = new SqlParameter("@ENROLLMENTNO", ENROLLMENTNO);
            //        //objParams[1] = new SqlParameter("@ADMBATCH", ADMBATCH);


            //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT-TYPE_MODIFICATION", objParams);

            //    }
            //    catch (Exception ex)
            //    {

            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
            //    }
            //    return ds;
            //}

            //FOR PAYMENT TYPE MODIFICATION --[02-AUG-2019]
            public DataSet GetListOfStudents(string ENROLLMENTNO, int SessionNo)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@ENROLLMENTNO", ENROLLMENTNO);
                    objParams[1] = new SqlParameter("@SESSIONNO", SessionNo);
                    //objParams[1] = new SqlParameter("@ADMBATCH", ADMBATCH);


                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT-TYPE_MODIFICATION", objParams);

                }
                catch (Exception ex)
                {

                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
                }
                return ds;
            }

            public int SENDMSG(string MSG, string MOBILENO)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);
                try
                {
                    int ret = 0;
                    int smsfasc = Convert.ToInt32(LookUp("reff", "FASCILITY", string.Empty));
                    if (smsfasc == 2 || smsfasc == 3)
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MSG", MSG);
                        objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SENDSMS", objParams, true);
                        return ret = 1;
                    }
                    else
                    {
                        return ret = 0;

                    }

                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
                }
            }
            //public int UpdatePaymentType(int PTYPE, string FIRSTNAME, int BRANCHNO, int DEGREENO)
            //{
            //    int retStatus = Convert.ToInt32(CustomStatus.Others);S
            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = new SqlParameter[]
            //            {

            //            new SqlParameter("@P_PTYPE", PTYPE),
            //            new SqlParameter("@P_FIRSTNAME", FIRSTNAME),
            //            new SqlParameter("@P_BRANCHNO", BRANCHNO),
            //            new SqlParameter("@P_DEGREENO", DEGREENO),

            //            new SqlParameter("@P_OUT", SqlDbType.Int)

            //            };

            //        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

            //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_TYPE_MODIFICATION", objParams, true);

            //        if (ret != null && ret.ToString() != "-99")

            //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            //        else
            //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

            //    }
            //    catch (Exception ex)
            //    {

            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            //    }
            //    return retStatus;
            //}


            //public int UpdatePaymentType(string PTYPE, string IDNO)//string FIRSTNAME, string REGNO)// int BRANCHNO, int DEGREENO)
            //{
            //    int retStatus = Convert.ToInt32(CustomStatus.Others);
            //    try
            //    {
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = new SqlParameter[3];
            //        {
            //            objParams[0] = new SqlParameter("@P_PTYPE", PTYPE);
            //            objParams[1] = new SqlParameter("@P_IDNO", IDNO);
            //            //objParams[2] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
            //            //objParams[3] = new SqlParameter("@P_DEGREENO", DEGREENO);
            //            //objParams[2] = new SqlParameter("@P_REGNO", REGNO);
            //            objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
            //            objParams[2].Direction = ParameterDirection.Output;
            //        };

            //        if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_TYPE_MODIFICATION", objParams, false) != null)
            //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdatePaymentType-> " + ex.ToString());
            //    }
            //    return retStatus;
            //}

            /// <summary>
            /// Addded By Rita M.. on Date 07082019..........
            /// </summary>
            /// <param name="PTYPE"></param>
            /// <param name="IDNO"></param>
            /// <param name="remark"></param>
            /// <param name="uano"></param>
            /// <param name="ipaddress"></param>
            /// <returns></returns>
            public int UpdatePaymentType(string PTYPE, string IDNO, string remark, int uano, string ipaddress)//string FIRSTNAME, string REGNO)// int BRANCHNO, int DEGREENO)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[6];
                    {
                        objParams[0] = new SqlParameter("@P_PTYPE", PTYPE);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_REMARK", remark);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                    };

                    if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_TYPE_MODIFICATION", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdatePaymentType-> " + ex.ToString());
                }
                return retStatus;
            }

            //for count of registered count
            //public DataSet GetAdmRegisteredCountForEXCEL(int ADMBATCH,int COLLEGE_ID, int DEPARTMENTNO, int DEGREENO, int BRANCHNO, int SEMESTERNO,int UA_NO)
            //{
            //    DataSet ds = null;
            //    try
            //    {
            //        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] sqlParams = new SqlParameter[]
            //    { 

            //        new SqlParameter("@P_ADMBATCH",ADMBATCH),
            //        new SqlParameter("@P_COLLEGE_ID",COLLEGE_ID),
            //        new SqlParameter("@P_DEPTNO", DEPARTMENTNO),
            //        new SqlParameter("@P_DEGREENO", DEGREENO),
            //        new SqlParameter("@P_BRANCHNO", BRANCHNO),
            //        new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
            //        new SqlParameter("@P_UA_NO", UA_NO),
            //        //new SqlParameter("@P_FROMDATE", FROMDATE),
            //        //new SqlParameter("@P_TODATE", TODATE)

            //    };
            //        ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_ADM_REGISTER_REPORT_FOR_EXCEL", sqlParams);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
            //    }
            //    return ds;
            //}

            private int SmsLogDetails(int userNo, string mobileNo, string message, string status)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_USERNO", userNo);
                    objParams[1] = new SqlParameter("@P_MOBILENO", mobileNo);
                    objParams[2] = new SqlParameter("@P_TEXTDETAILS", message);
                    objParams[3] = new SqlParameter("@P_SMSSTATUS", status);

                    objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[4].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SMS_LOG_DETAILS", objParams, true);


                    if (Convert.ToInt32(obj) == -99)
                        retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                    if (Convert.ToInt32(obj) != 99)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SmsLogDetails" + ex.ToString());
                }
                return retStatus;
            }

            //public string SendSMS(string Mobile, string text)
            //{
            //    string status = "";
            //    try
            //    {
            //        string Message = string.Empty;
            //        DataSet ds = FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD,ISNULL(fascility,0)fascility", "", "");
            //        int fascility = Convert.ToInt32(ds.Tables[0].Rows[0]["FASCILITY"].ToString());
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            if (fascility == 2 || fascility == 3)
            //            {
            //                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
            //                request.ContentType = "text/xml; charset=utf-8";
            //                request.Method = "POST";

            //                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
            //                postDate += "&";
            //                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
            //                postDate += "&";
            //                postDate += "PhNo=91" + Mobile;
            //                postDate += "&";
            //                postDate += "Text=" + text;

            //                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
            //                request.ContentType = "application/x-www-form-urlencoded";

            //                request.ContentLength = byteArray.Length;
            //                Stream dataStream = request.GetRequestStream();
            //                dataStream.Write(byteArray, 0, byteArray.Length);
            //                dataStream.Close();
            //                WebResponse _webresponse = request.GetResponse();
            //                dataStream = _webresponse.GetResponseStream();
            //                StreamReader reader = new StreamReader(dataStream);
            //                status = reader.ReadToEnd();


            //                this.SmsLogDetails(Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]), Mobile, text, status);
            //            }
            //            else
            //            {
            //                status = "0";
            //            }
            //        }
            //        else
            //        {
            //            status = "0";
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SendSMS() --> " + ex.Message + " " + ex.StackTrace);
            //    }
            //    return status;
            //}

            public string SendSMS(string Mobile, string text)
            {
                string status = "";
                try
                {
                    string Message = string.Empty;
                    DataSet ds = FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD,ISNULL(fascility,0)fascility", "", "");
                    int fascility = Convert.ToInt32(ds.Tables[0].Rows[0]["FASCILITY"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (fascility == 1 || fascility == 3)
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


                            this.SmsLogDetails(Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]), Mobile, text, status);
                        }
                        else
                        {
                            status = "0";
                        }
                    }
                    else
                    {
                        status = "0";
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SendSMS() --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }

            public DataSet GetFailedTransactionDataOfStudent(string ENROLLMENTNO)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@ENROLLMENTNO", ENROLLMENTNO);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FAILED_TRANSACTION_REQUEST_DATA", objParams);

                }
                catch (Exception ex)
                {

                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetFailedTransactionDataOfStudent-> " + ex.ToString());
                }
                return ds;
            }

            //public DataSet GetAttendanceData(int sessionno, int semesterno, int schemeno, DateTime fromdate, DateTime todate, int sectionno, string condition, double per, int subid, string percentageFrom, string percentageTo, int selector)
            //{
            //    DataSet ds = null;
            //    try
            //    {
            //        selector = 1;
            //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
            //        SqlParameter[] objParams = null;
            //        objParams = new SqlParameter[12];
            //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
            //        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
            //        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
            //        objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
            //        objParams[4] = new SqlParameter("@P_TODATE", todate);
            //        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
            //        objParams[6] = new SqlParameter("@P_CONDITIONS", condition);
            //        objParams[7] = new SqlParameter("@P_PERCENTAGE", per);
            //        objParams[8] = new SqlParameter("@P_SUBID", subid);
            //        objParams[9] = new SqlParameter("@P_PERCENTAGEFROM", percentageFrom);
            //        objParams[10] = new SqlParameter("@P_PERCENTAGETO", percentageTo);
            //        objParams[11] = new SqlParameter("@P_SELECTOR", selector);


            //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN", objParams);
            //        //*************************************************************************************************************************
            //    }
            //    catch (Exception ex)
            //    {

            //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
            //    }
            //    return ds;
            //}


            public DataSet GetAttendanceData(int sessionno, int semesterno, int schemeno, DateTime fromdate, DateTime todate, int sectionno, string condition, double per, int subid, string percentageFrom, string percentageTo, int selector)
            {
                DataSet ds = null;
                try
                {

                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[12];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                    objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                    objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                    objParams[4] = new SqlParameter("@P_TODATE", todate);
                    objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                    objParams[6] = new SqlParameter("@P_CONDITIONS", condition);
                    objParams[7] = new SqlParameter("@P_PERCENTAGE", per);
                    objParams[8] = new SqlParameter("@P_SUBID", subid);
                    objParams[9] = new SqlParameter("@P_PERCENTAGEFROM", percentageFrom);
                    objParams[10] = new SqlParameter("@P_PERCENTAGETO", percentageTo);
                    objParams[11] = new SqlParameter("@P_SELECTOR", selector);


                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN", objParams);
                    //*************************************************************************************************************************
                }
                catch (Exception ex)
                {

                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData-> " + ex.ToString());
                }
                return ds;
            }

            //FOR Filling Paperset student count for subject 23092014
            public DataSet getPapersetStudentcount(int Session, int Uano)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                    objParams[1] = new SqlParameter("@P_UANO", Uano);

                    ds = objSQLHelper.ExecuteDataSetSP("GET_REMUNERATION_SUBJECT_COUNT", objParams);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.getPapersetStudentcount-> " + ex.ToString());
                }
                return ds;
            }

            public int UpdateLockbill(int uano, int sessionno)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_UANO", uano);
                    objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[2].Direction = ParameterDirection.Output;

                    object ret = objSQLHelper.ExecuteNonQuerySP("ACD_UNLOCK_BILL_AMOUNT", objParams, true);
                    retStatus = Convert.ToInt32(ret);

                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add_Update_ExamInvigilator -> " + ex.ToString());
                }
                return retStatus;
            }
            /// <summary>
            /// Added By rishabh on 15/12/2021 - dynamic label
            /// </summary>
            /// <param name="collegeid"></param>
            /// <param name="OrgId"></param>
            /// <param name="UA_NO"></param>
            /// <returns></returns>
            public DataSet SetLabelData(string collegeid, int OrgId, int UA_NO)
            {
                DataSet ds = null;
                var pageHandler = System.Web.HttpContext.Current.CurrentHandler;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_COLLEGEID", collegeid);
                    objParams[1] = new SqlParameter("@P_ORG_ID", OrgId);
                    objParams[2] = new SqlParameter("@P_UANO", UA_NO);


                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DYNAMIC_LABEL", objParams);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ContentPlaceHolder cph = ((Page)pageHandler).Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
                            if (cph != null)
                            {
                                Label NewLabel = cph.FindControl(ds.Tables[0].Rows[i]["LABELID"].ToString()) as Label;
                                if (NewLabel != null)
                                {
                                    NewLabel.Text = ds.Tables[0].Rows[i]["LABELNAME"].ToString();
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SetLabelData-> " + ex.ToString());
                }
                return ds;
            }

            // dynamic page header addded by Rishabh 15/12/2021 start

            public DataSet SetHeaderLabelData(string AL_NO)
            {
                var pageHandler = System.Web.HttpContext.Current.CurrentHandler;

                DataSet dsTitle = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_AL_NO", AL_NO);


                    dsTitle = objSQLHelper.ExecuteDataSetSP("PKG_DYNAMIC_PAGE_HEADER_TITLE", objParams);

                    if (dsTitle != null && dsTitle.Tables.Count > 0 && dsTitle.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsTitle.Tables[0].Rows.Count; i++)
                        {
                            ContentPlaceHolder cph = ((Page)pageHandler).Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
                            if (cph != null)
                            {
                                Label NewLabel = cph.FindControl(dsTitle.Tables[0].Rows[i]["DYNAMIC_HEADER_NAME"].ToString()) as Label;
                                if (NewLabel != null)
                                {
                                    NewLabel.Text = dsTitle.Tables[0].Rows[i]["AL_LINK"].ToString();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SetHeaderLabelData-> " + ex.ToString());
                }
                return dsTitle;
            }
            ////public string SendSMS(string Mobile, string text)
            ////{
            ////    string status = "";
            ////    try
            ////    {
            ////        string Message = string.Empty;
            ////        DataSet ds = FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD,ISNULL(fascility,0)fascility", "", "");
            ////        int fascility = Convert.ToInt32(ds.Tables[0].Rows[0]["FASCILITY"].ToString());
            ////        if (ds.Tables[0].Rows.Count > 0)
            ////        {
            ////            if (fascility == 2 || fascility == 3)
            ////            {
            ////                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
            ////                request.ContentType = "text/xml; charset=utf-8";
            ////                request.Method = "POST";

            ////                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
            ////                postDate += "&";
            ////                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
            ////                postDate += "&";
            ////                postDate += "PhNo=91" + Mobile;
            ////                postDate += "&";
            ////                postDate += "Text=" + text;

            ////                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
            ////                request.ContentType = "application/x-www-form-urlencoded";

            ////                request.ContentLength = byteArray.Length;
            ////                Stream dataStream = request.GetRequestStream();
            ////                dataStream.Write(byteArray, 0, byteArray.Length);
            ////                dataStream.Close();
            ////                WebResponse _webresponse = request.GetResponse();
            ////                dataStream = _webresponse.GetResponseStream();
            ////                StreamReader reader = new StreamReader(dataStream);
            ////                status = reader.ReadToEnd();
            ////            }
            ////            else
            ////            {
            ////                status = "0";
            ////            }
            ////        }
            ////        else
            ////        {
            ////            status = "0";
            ////        }
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.SendSMS() --> " + ex.Message + " " + ex.StackTrace);
            ////    }
            ////    return status;
            ////}

            /// <summary>
            /// Added by Nidhi Gour on 02/10/2019
            /// Modified on 21/10/2019
            /// for authority sign upload
            /// </summary>               
            public int AddSign(byte[] img, string athorityno, int uano)
            {
                int retStatus = Convert.ToInt32(CustomStatus.Others);

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[4];
                    objParams[0] = new SqlParameter("@P_SIGNA", img);
                    objParams[1] = new SqlParameter("@P_ATHORITYNO", athorityno);
                    objParams[2] = new SqlParameter("@P_UANO", uano);
                    objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[3].Direction = ParameterDirection.Output;

                    object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SIGN_UPLOAD", objParams, true);
                    retStatus = Convert.ToInt32(ret);

                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add_Update_ExamInvigilator -> " + ex.ToString());
                }
                return retStatus;

            }

            // This Method is added by Abhinay Lad [18-09-2019]  -- Dynamic Call for Stored Procedure
            public DataSet DynamicSPCall_Select(string SP_Name, string SP_Parameters, string Call_Values)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[SP_Parameters.Split(',').Length];

                    for (int i = 0; i < SP_Parameters.Split(',').Length; i++)
                    {
                        objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), Call_Values.Split(',')[i].Trim());
                    }

                    ds = objSQLHelper.ExecuteDataSetSP(SP_Name, objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                }
                return ds;
            }

            // This Method is added by Abhinay Lad [24-09-2019]  -- Dynamic Call for Stored Procedure
            public string DynamicSPCall_IUD(string SP_Name, string SP_Parameters, string Call_Values, bool ft)
            {
                string retStatus = "0";
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[SP_Parameters.Split(',').Length];

                    for (int i = 0; i < SP_Parameters.Split(',').Length; i++)
                    {
                        objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), Call_Values.Split(',')[i].Trim());
                    }
                    objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                    retStatus = (string)objSQLHelper.ExecuteNonQuerySP(SP_Name, objParams, ft);
                }
                catch (Exception ex)
                {
                    retStatus = "-99";
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                }
                return retStatus;
            }

            //METHOD TO DISPLAY PATH(BREADCRUMB)
            //SRIKANTH P  24-09-2019

            public string GetPath(int p)
            {
                string path = string.Empty;

                DataSet ds = null;

                SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@PAGENO", p);

                ds = objsqlhelper.ExecuteDataSetSP("PKG_DISPLAYPATH", param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        path = ds.Tables[0].Rows[0][0].ToString();
                    }
                }


                return path;
            }

            //METHOD TO DISPLAY SUBMENU HEADER NAME(BREADCRUMB)
            //Swapnil P  05-01-2022

            public string GetPathSubMenu(int p)
            {
                string path = string.Empty;

                DataSet ds = null;

                SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@PAGENO", p);

                ds = objsqlhelper.ExecuteDataSetSP("PKG_DISPLAYPATH_SUBMENU", param);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        path = ds.Tables[0].Rows[0][0].ToString();
                    }
                }


                return path;
            }


            //Add by Deepali [19-08-2020]
            public static bool CheckPage(int userno, string pageid)
            {
                try
                {
                    User_AccController objUC = new User_AccController();
                    UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

                    if (objUA.UA_No != 0)
                    {
                        char sp = ',';
                        string[] pageids = objUA.UA_Acc.Split(sp);

                        for (int i = 0; i < pageids.Length; i++)
                        {
                            if (pageid.Equals(pageids[i]))
                            {
                                return true;
                            }
                        }
                    }

                    return false;

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.CheckPage-> " + ex.ToString());
                }
            }


            public string GetUserPassword(int Ua_no)
            {
                string ret = string.Empty;
                try
                {
                    SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                    string sql = "SELECT UA_PWD FROM USER_ACC WHERE UA_NO=" + Ua_no;
                    SqlDataReader dr = objSqlHelper.ExecuteReader(sql);
                    if (dr != null)
                    {
                        if (dr.Read())
                        {
                            ret = dr["UA_PWD"] == null ? "" : dr["UA_PWD"].ToString();
                        }
                    }
                    if (dr != null) dr.Close();
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.Common.GetCommonDetails-> " + ex.ToString());
                }
                return ret;
            }

            public DropDownList FilterDataByBranch(DropDownList ddl, string UserType, string BranchNo, int Branch_Filter, int CurrentUserType)
            {
                BranchNo = BranchNo.Replace(",,", ",");
                string[] arr = BranchNo.Split(',');
                int x = 0;

                if (UserType.Contains(CurrentUserType.ToString()) && Branch_Filter == 1)
                {
                    if (arr.Length == 1)
                    {
                        ddl.Items.Clear();
                        ddl.Items.Add(new ListItem("Please Select", "0"));
                        return ddl;
                    }

                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        while (x < ddl.Items.Count)
                        {
                            ddl.Items[x].Enabled = false;
                            x++;
                        }
                        for (int j = 0; j < ddl.Items.Count; j++)
                        {
                            if (Convert.ToInt32(ddl.Items[j].Value) == Convert.ToInt32(arr[i]) || Convert.ToInt32(ddl.Items[j].Value) == 0)
                            {
                                ddl.Items[j].Enabled = true;
                            }
                        }
                    }
                }
                return ddl;
            }

            // FOR FILTER DATATABLEREADER BY Multiple BRANCH AND MULTIPLE USERTYPE
            public DataTableReader FilterDataByBranch(ref DataTableReader dtr, string UserType, string BranchNo, int Branch_Filter, int CurrentUserType)
            {
                BranchNo = BranchNo.Replace(",,", ",");
                string[] arr = BranchNo.Split(',');
                DataTable dt = new DataTable();
                DataRow dr;
                try
                {
                    if (UserType.Contains(CurrentUserType.ToString()) && Branch_Filter == 1)
                    {
                        int x = 0;
                        dt.TableName = "demoTable";
                        while (x < dtr.FieldCount)
                        {
                            dt.Columns.Add(dtr.GetName(x));
                            x++;
                        }

                        while (dtr.Read())
                        {
                            for (int i = 0; i < arr.Length - 1; i++)
                            {
                                if (Convert.ToInt32(dtr["BRANCHNO"]) == Convert.ToInt32(arr[i]))
                                {
                                    dr = dt.NewRow();
                                    for (int j = 0; j < dtr.FieldCount; j++)
                                    {
                                        dr[j] = Convert.ToString(dtr[dtr.GetName(j).ToString()]);
                                    }
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                        dtr.Close();
                        dtr = dt.CreateDataReader();
                    }
                    return dtr;
                }
                catch (Exception e)
                {
                    dtr.Close();
                    dtr = null;
                    return dtr;
                }
            }

            /// <summary>
            /// Added By Dileep Kare
            /// 11.06.2021
            /// </summary>
            /// <param name="sessionno"></param>
            /// <param name="semesterno"></param>
            /// <param name="schemeno"></param>
            /// <param name="fromdate"></param>
            /// <param name="todate"></param>
            /// <param name="sectionno"></param>
            /// <param name="condition"></param>
            /// <param name="per"></param>
            /// <param name="subid"></param>
            /// <param name="percentageFrom"></param>
            /// <param name="percentageTo"></param>
            /// <param name="selector"></param>
            /// <returns></returns>
            public DataSet GetAttendanceData1(int sessionno, int semesterno, int schemeno, DateTime fromdate, DateTime todate, int sectionno, string condition, double per, int subid, string percentageFrom, string percentageTo, int selector)
            {
                DataSet ds = null;
                try
                {

                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[12];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                    objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                    objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                    objParams[4] = new SqlParameter("@P_TODATE", todate);
                    objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                    objParams[6] = new SqlParameter("@P_CONDITIONS", condition);
                    objParams[7] = new SqlParameter("@P_PERCENTAGE", per);
                    objParams[8] = new SqlParameter("@P_SUBID", subid);
                    objParams[9] = new SqlParameter("@P_PERCENTAGEFROM", percentageFrom);
                    objParams[10] = new SqlParameter("@P_PERCENTAGETO", percentageTo);
                    objParams[11] = new SqlParameter("@P_SELECTOR", selector);


                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN_FORMAT_II", objParams);
                    //*************************************************************************************************************************
                }
                catch (Exception ex)
                {

                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAttendanceData1-> " + ex.ToString());
                }
                return ds;
            }


            //Added Mahesh Malve on Dated 23/06/2021
            /// <summary>
            /// Get Admin Leve User Detail to allow Marks Entry by Admin Level.
            /// </summary>
            /// <returns>DataSet object</returns>
            public DataSet GetAdminUserDetail()
            {
                try
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GetAdminTypeUserDetail", objParams);

                    return ds;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }

            #region ---------Call Dispose Method to Release Resourse Forcefully-Added Mahesh on Dated 23-06-2021.

            private bool Disposed;
            ~Common()
            {
                this.Dispose(false);
            }

            public void Dispose()
            {
                this.Dispose(true);

                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool Disposing)
            {
                if (!Disposed)
                {
                    if (Disposing)
                    {
                        //Release managed resourses here
                    }
                }
                Disposed = true;
            }

            #endregion  ---------Call Dispose Method to Release Resourse Forcefully-Added Mahesh on Dated 23-06-2021.

            // Added by Pranita Hiradkar for Sub links menu tab on 05/10/2021
            public DataSet GetUserSubLinks(int Uano, int alno, int mastno)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_UANO", Uano);
                    objParams[1] = new SqlParameter("@P_PAGENO", alno);
                    objParams[2] = new SqlParameter("@P_MASTNO", mastno);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_TREEVIEW_SP_RET_USERSUBLINKS", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.getPapersetStudentcount-> " + ex.ToString());
                }
                return ds;
            }

            /// <summary>
            /// Modified by Saurabh S on 27.07.2022
            /// </summary>
            /// <param name="ADMBATCH"></param>
            /// <param name="COLLEGE_ID"></param>
            /// <param name="DEPARTMENTNO"></param>
            /// <param name="DEGREENO"></param>
            /// <param name="BRANCHNO"></param>
            /// <param name="SEMESTERNO"></param>
            /// <param name="UA_NO"></param>
            /// <param name="acadmicyearid"></param>
            /// <returns></returns>
            public DataSet GetAdmRegisteredCountForEXCEL(int ADMBATCH, int COLLEGE_ID, int DEPARTMENTNO, int DEGREENO, int BRANCHNO, int SEMESTERNO, int UA_NO, int acadmicyearid)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_COLLEGE_ID",COLLEGE_ID),
                    new SqlParameter("@P_DEPTNO", DEPARTMENTNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_UA_NO", UA_NO),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",acadmicyearid),
                    //new SqlParameter("@P_FROMDATE", FROMDATE),
                    //new SqlParameter("@P_TODATE", TODATE)
               
                };
                    ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_ADM_REGISTER_REPORT_FOR_EXCEL", sqlParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }


            /// <summary>
            /// Modifide by Saurabh S on 27.07.2022
            /// </summary>
            /// <param name="ADMBATCH"></param>
            /// <param name="DEGREENO"></param>
            /// <param name="ROUNDNO"></param>
            /// <param name="acadmicyearid"></param>
            /// <returns></returns>
            public DataSet GetAdmRegisteredCountForEXCEL(int ADMBATCH, int DEGREENO, int ROUNDNO, int acadmicyearid, int BranchNo, int CollegeID, int SemesterNo)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_ACADEMIC_YEAR_ID",acadmicyearid),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_ADMROUND", ROUNDNO),
                    new SqlParameter("@P_BRANCHNO",BranchNo),
                    new SqlParameter("@P_COLLEGE_ID",CollegeID),
                    new SqlParameter("@P_SEMESTERNO",SemesterNo),
                            
                };
                    //ds = objDataAccess.ExecuteDataSetSP("PKG_BRANCH_WISE_ADM_COUNT", sqlParams);
                    ds = objDataAccess.ExecuteDataSetSP("PKG_BRANCH_WISE_ADM_COUNT_2", sqlParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }

            // Added by Harshal Raipure to remove  DEPARTMENTNO on 24/11/2021
            public DataSet GetAdmRegisteredCountForEXCEL(int ADMBATCH, int COLLEGE_ID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int UA_NO)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_COLLEGE_ID",COLLEGE_ID),
                    //new SqlParameter("@P_DEPTNO", DEPARTMENTNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_UA_NO", UA_NO),
                    //new SqlParameter("@P_FROMDATE", FROMDATE),
                    //new SqlParameter("@P_TODATE", TODATE)
               
                };
                    ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_ADM_REGISTER_REPORT_FOR_EXCEL", sqlParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }


            //To Get All the Details Related to the Selected College Scheme 
            public DataSet GetCollegeSchemeMappingDetails(int ColSchemeno)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_COLSCHEMENO", ColSchemeno);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", objParams);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetCollegeSchemeMappingDetails-> " + ex.ToString());
                }
                return ds;
            }

            /// <summary>
            /// Added by Swapnil P dated on 29112021 for Crescent Admission Registered report excel
            /// </summary>
            /// <param name="ADMBATCH"></param>
            /// <param name="COLLEGE_ID"></param>
            /// <param name="DEPARTMENTNO"></param>
            /// <param name="DEGREENO"></param>
            /// <param name="BRANCHNO"></param>
            /// <param name="SEMESTERNO"></param>
            /// <param name="UA_NO"></param>
            /// <returns></returns>
            public DataSet GetAdmRegisteredCountForEXCEL_Crescent(int ADMBATCH, int COLLEGE_ID, int DEPARTMENTNO, int DEGREENO, int BRANCHNO, int SEMESTERNO, int UA_NO)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_COLLEGE_ID",COLLEGE_ID),
                    new SqlParameter("@P_DEPTNO", DEPARTMENTNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_UA_NO", UA_NO),
                    //new SqlParameter("@P_FROMDATE", FROMDATE),
                    //new SqlParameter("@P_TODATE", TODATE)
               
                };
                    ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_ADM_REGISTER_REPORT_FOR_EXCEL_CRESCENT", sqlParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }


            public DataSet GetAdmCountForEXCEL(int ADMBATCH, int COLLEGE_ID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int ADMROUND)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                { 

                    new SqlParameter("@P_ADMBATCH",ADMBATCH),
                    new SqlParameter("@P_COLLEGE_ID",COLLEGE_ID),
                    //new SqlParameter("@P_DEPTNO", DEPARTMENTNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_ROUND_NO", ADMROUND),
                    //new SqlParameter("@P_FROMDATE", FROMDATE),
                    //new SqlParameter("@P_TODATE", TODATE)
               
                };
                    ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_ADM_REGISTER_REPORT_FOR_EXCEL", sqlParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetMeritList() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }

            //Added Swapnil P on Dated 10/01/2022
            /// <summary>
            /// Get Search Criteria Dropdown Data
            /// </summary>
            /// <returns>DataSet object</returns>
            public DataSet GetSearchDropdownDetails(string ddlname)
            {
                try
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_DDLNAME", ddlname);

                    ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GET_SEARCH_DROPDOWN_DETAILS", objParams);

                    return ds;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }



            //Added jay T on Dated 11/01/2023
            /// <summary>
            /// Get Search Criteria Dropdown Data for phd
            /// </summary>
            /// <returns>DataSet object</returns>
            public DataSet GetSearchDropdownDetails_Phd(string ddlname)
            {
                try
                {
                    DataSet ds = null;
                    SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_DDLNAME", ddlname);

                    ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GET_SEARCH_DROPDOWN_DETAILS_PHD", objParams);

                    return ds;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message);
                }
            }


            /// <summary>
            /// Added by Nikhil L. on 04/02/2022 to add degree subject mapping.
            /// </summary>
            /// <param name="degree"></param>
            /// <param name="subName"></param>
            /// <param name="isCompulsory"></param>
            /// <param name="isCutOff"></param>
            /// <param name="isOthers"></param>
            /// <returns></returns>
            public int Add_Degree_Sub_Mapping(int degree, string subName, int isCompulsory, int isCutOff, int isOthers)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    //Add New Adm
                    objParams = new SqlParameter[6];
                    objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                    objParams[1] = new SqlParameter("@P_SUB_NAME", subName);
                    objParams[2] = new SqlParameter("@P_IS_COMPULSORY", isCompulsory);
                    objParams[3] = new SqlParameter("@P_IS_CUT_OFF", isCutOff);
                    objParams[4] = new SqlParameter("@P_IS_OTHERS", isOthers);
                    objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[5].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_DEGREE_SUBJECT_MAPPING", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-1")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else if (obj.ToString() == "-1")
                    {
                        status = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.Add_Degree_Sub_Mapping-> " + ex.ToString());
                }
                return status;
            }
            /// <summary>
            /// Added by Nikhil L. on 04/02/2022 to bind degree subject.
            /// </summary>
            /// <returns></returns>
            public DataSet GetDegreeSubjectMapping()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];
                    ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_DEGREE_SUBJECT_MAPPING", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.GetDegreeSubjectMapping-> " + ex.ToString());
                }
                return ds;
            }
            /// <summary>
            /// Added by Nikhil L. on 04/02/2022 to update degree subject mapping.
            /// </summary>
            /// <param name="degree"></param>
            /// <param name="subName"></param>
            /// <param name="isCompulsory"></param>
            /// <param name="isCutOff"></param>
            /// <param name="isOthers"></param>
            /// <param name="subID"></param>
            /// <returns></returns>
            public int Update_Degree_Sub_Mapping(int degree, string subName, int isCompulsory, int isCutOff, int isOthers, int subID)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    //Add New Adm
                    objParams = new SqlParameter[7];
                    objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                    objParams[1] = new SqlParameter("@P_SUB_NAME", subName);
                    objParams[2] = new SqlParameter("@P_IS_COMPULSORY", isCompulsory);
                    objParams[3] = new SqlParameter("@P_IS_CUT_OFF", isCutOff);
                    objParams[4] = new SqlParameter("@P_IS_OTHERS", isOthers);
                    objParams[5] = new SqlParameter("@P_SUB_ID", subID);
                    objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[6].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_DEGREE_SUBJECT_MAPPING", objParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.Update_Degree_Sub_Mapping-> " + ex.ToString());
                }
                return status;
            }
            /// <summary>
            /// Added by Nikhil L. on 04/02/2022 to get single subject map.
            /// </summary>
            /// <param name="subID"></param>
            /// <returns></returns>
            public DataSet Get_Single_DegreeSubjectMapping(int subID)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_SUB_ID", subID);

                    ds = objsqlhelper.ExecuteDataSetSP("PKG_GET_SINGLE_DEGREE_SUBJECT_MAPPING", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.Common.Get_Single_DegreeSubjectMapping-> " + ex.ToString());
                }
                return ds;
            }
            /// <summary>
            /// Added By Rishabh on 09/02/2022 to get Admission Summary - Excel
            /// </summary>
            /// <param name="session"></param>
            /// <returns></returns>
            public DataSet GetAllAdmissionSummeryforExcel(int AdmBatch, int Schl, int deg, int branch, int sem)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                    objParams[1] = new SqlParameter("@P_COLLEGEID", Schl);
                    objParams[2] = new SqlParameter("@P_DEGREENO", deg);
                    objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                    objParams[4] = new SqlParameter("@P_SEMESTERNO", sem);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_REG_MF_COUNT_EXCEL", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAllAdmissionSummeryforExcel-> " + ex.ToString());
                }
                return ds;
            }

            public DataSet FillDropDownDepartmentUserWise(int uatype, string deptnos)
            {
                //int status = Convert.ToInt32(CustomStatus.Others);s
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;

                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_UA_TYPE", uatype);
                    objParams[1] = new SqlParameter("@P_UA_DEPTNO", deptnos);
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_FILL_DRPDOWN_FOR_DEPARTMENT_USER_WISE", objParams);

                }
                catch (Exception ex)
                {

                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.AdmInsertFeeDeduction-> " + ex.ToString());
                }
                return ds;
            }
            /// <summary>
            ///  - dynamic label for ListView
            /// </summary>
            /// <param name="collegeid"></param>
            /// <param name="OrgId"></param>
            /// <param name="UA_NO"></param>
            /// <returns></returns>
            public void SetListViewLabel(string collegeid, int OrgId, int UA_NO, Control ctrl)
            {
                DataSet ds = null;
                var pageHandler = System.Web.HttpContext.Current.CurrentHandler;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_COLLEGEID", collegeid);
                    objParams[1] = new SqlParameter("@P_ORG_ID", OrgId);
                    objParams[2] = new SqlParameter("@P_UANO", UA_NO);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DYNAMIC_LABEL", objParams);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ContentPlaceHolder cph = ((Page)pageHandler).Master.FindControl("ContentPlaceHolder1") as ContentPlaceHolder;
                            if (cph != null)
                            {
                                if (ctrl != null)
                                {
                                    string id = ctrl.ID;
                                    //var ctrlType;
                                    Control newCtrl = new Control();
                                    GridView gv = new GridView();

                                    if (ctrl.GetType() == typeof(ListView))
                                    {
                                        newCtrl = (ListView)cph.FindControl(id);
                                    }
                                    if (ctrl.GetType() == typeof(Repeater))
                                    {
                                        newCtrl = (Repeater)cph.FindControl(id);
                                    }
                                    if (ctrl.GetType() == typeof(GridView))
                                    {
                                        gv = (GridView)cph.FindControl(id);
                                        if (gv.HeaderRow.RowType == DataControlRowType.Header)
                                        {
                                            gv.HeaderRow.Cells[0].Text = ds.Tables[0].Rows[i]["LABELID"].ToString();
                                        }
                                    }
                                    if (newCtrl != null)
                                    {
                                        Label lvLabel = (Label)newCtrl.FindControl(ds.Tables[0].Rows[i]["LABELID"].ToString());
                                        //Label lvLabel = newCtrl.FindControl(ds.Tables[0].Rows[i]["LABELID"].ToString()) as Label;
                                        if (lvLabel != null)
                                        {
                                            lvLabel.Text = ds.Tables[0].Rows[i]["LABELNAME"].ToString();
                                        }
                                    }
                                }
                                Label NewLabel = cph.FindControl(ds.Tables[0].Rows[i]["LABELID"].ToString()) as Label;

                                if (NewLabel != null)
                                {
                                    NewLabel.Text = ds.Tables[0].Rows[i]["LABELNAME"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.SetLabelData-> " + ex.ToString());
                }
            }
            public DataSet GetInternalMarksExcel(int schemeno, int sessionno, int semesterno, int sectionno)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParam = new SqlParameter[4];
                    objParam[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                    objParam[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                    objParam[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                    objParam[3] = new SqlParameter("@P_SECTIONNO", sectionno);

                    ds = objhelper.ExecuteDataSetSP("PKG_INTERNAL_MARKS_EXCEL_REPORT", objParam);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            /// <summary>
            /// Modifide by Saurabh S on 27.07.2022
            /// </summary>
            /// <param name="admbatch"></param>
            /// <param name="academicyearid"></param>
            /// <returns></returns>
            public DataSet Get_Admission_Batch_Wise_Student_Data(int admbatch, int academicyearid, int BranchNo, int CollegeID, int SemesterNo, int DegreeNo)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParam = new SqlParameter[6];
                    objParam[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                    objParam[1] = new SqlParameter("@P_BRANCHNO", BranchNo);
                    objParam[2] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                    objParam[3] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                    objParam[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                    objParam[5] = new SqlParameter("P_ACADEMIC_YEAR_ID", academicyearid);

                    ds = objhelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_COMPLETE_INFO_ADMBATCHWISE", objParam);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


            //Added DistrictNo by Ssachin L on 07-07-2022
            public int AddCity(string CityName, int StateNo, string CollegeCode, int ActiveStatus, int DistrictNo)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_CITY", CityName),
                    new SqlParameter("@P_STATENO",StateNo),
                    new SqlParameter("@P_DISTRICTNO",DistrictNo),  //Added by Sachin on 06-07-2022
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                    new SqlParameter("@P_CITYNO", status)
                    
                };
                    sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_CITY_INSERT", sqlParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }

            //Added DistrictNo by Ssachin L on 07-07-2022
            public int UpdateCity(string CityName, int StateNo, int DistrictNo, string CollegeCode, int ActiveStatus, int CityNo)
            {
                int status;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_CITY",CityName),
                    new SqlParameter("@P_STATENO", StateNo),
                    new SqlParameter("@P_DISTRICTNO",DistrictNo),  //Added by Sachin on 06-07-2022
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                    new SqlParameter("@P_CITYNO",  CityNo)
                };
                    sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_CITY_UPDATE", sqlParams, true);

                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    else
                        status = Convert.ToInt32(CustomStatus.Error);
                }
                catch (Exception ex)
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
                }
                return status;
            }



            public SqlDataReader GetBatchByNo(int CityNo)
            {
                SqlDataReader dr = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_CITYNO", CityNo) };

                    dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_CITY_GET_BY_NO", objParams);

                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
                }
                return dr;
            }


            public DataSet GetAllCity()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_CITY_GET_ALL", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }

            /// <summary>
            /// Added By Dileep on 17.09.2021
            /// for User Count Details getting in Excel
            /// </summary>
            /// <param name="User_Status"></param>
            /// <param name="Status"></param>
            /// <param name="Date"></param>
            /// <param name="From_Date"></param>
            /// <param name="To_Date"></param>
            /// <returns></returns>
            public DataSet getUser_Wise_Count_for_Excel_Report(int User_Status, int Status, string Date, string From_Date, string To_Date)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_USERSTATUS", User_Status);
                    objParams[1] = new SqlParameter("@P_STATUS", Status);
                    objParams[2] = new SqlParameter("@P_DATE", Date);
                    objParams[3] = new SqlParameter("@P_FROMDATE", From_Date);
                    objParams[4] = new SqlParameter("@P_TODATE", To_Date);
                    ds = objHelper.ExecuteDataSetSP("PKG_USERS_COUNT_FOR_EXCEL_REPORT", objParams);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

            /// <summary>
            /// Added By Dileep on 30.08.2021
            /// for User Details getting in Excel
            /// </summary>
            /// <param name="User_Status"></param>
            /// <param name="Status"></param>
            /// <param name="Date"></param>
            /// <param name="From_Date"></param>
            /// <param name="To_Date"></param>
            /// <returns></returns>
            public DataSet getUserReportsDetails(int User_Status, int Status, string Date, string From_Date, string To_Date)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[5];
                    objParams[0] = new SqlParameter("@P_USERSTATUS", User_Status);
                    objParams[1] = new SqlParameter("@P_STATUS", Status);
                    objParams[2] = new SqlParameter("@P_DATE", Date);
                    objParams[3] = new SqlParameter("@P_FROMDATE", From_Date);
                    objParams[4] = new SqlParameter("@P_TODATE", To_Date);
                    ds = objHelper.ExecuteDataSetSP("PKG_USER_ACC_INFO_REGISTERED_FOR_EXCEL_REPORT", objParams);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

            /// <summary>
            /// Added by Dileep Kare on 21.07.2022
            /// </summary>
            /// <returns></returns>
            public DataSet GetModuleConfig(int OrgID)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrgID);
                    ds = objHelper.ExecuteDataSetSP("PKG_GET_MODULE_CONFIG_DETAILS", objParams);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }

            /// <summary>
            /// Added by Saurabh S.
            /// </summary>
            /// <param name="admbatch"></param>
            /// <returns></returns>
            public DataSet Get_Mother_Father_Alive_Excel_Report(int admbatch, int academicYear, int Branchno, int CollegeID, int SemesterNo, int DegreeNo)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParam = new SqlParameter[6];
                    objParam[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                    objParam[1] = new SqlParameter("@P_BRANCHNO", Branchno);
                    objParam[2] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                    objParam[3] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                    objParam[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                    objParam[5] = new SqlParameter("@P_ACADEMIC_YEAR", academicYear);



                    ds = objhelper.ExecuteDataSetSP("PKG_GET_STUD_MOTHER_FATHER_IS_NOT_ALIVE_EXCEL_REPORT", objParam);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            /// Added by Nikhil L. on 03/08/2022 to get the payment activity for double verification.
            /// </summary>
            /// <returns></returns>
            public DataSet Populate_PaymentActivity()
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParam = new SqlParameter[0];
                    //objParam[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                    //objParam[1] = new SqlParameter("@P_ACADEMIC_YEAR", academicYear);
                    ds = objhelper.ExecuteDataSetSP("PKG_ACD_FILL_PAYMENT_ACTIVITY_DROPDOWN", objParam);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            public DataSet GetSemesterSessionWise(int SchemeNo, int SessionNo, int mode)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objhelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParam = new SqlParameter[3];
                    objParam[0] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                    objParam[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                    objParam[2] = new SqlParameter("@P_MODE", mode);
                    ds = objhelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_SEMESTER_SESSIONWISE", objParam);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            public DataSet GetUGPG(int degree, int college)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                    objParams[1] = new SqlParameter("@P_COLLEGE_ID", college);
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_SELECT_UGPGOT_GRADE_RELEASE", objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
                }
                return ds;
            }

            // This Method is added by Added By Sachin A dt on 24012023  -- For Dynamically Pass Datatable to SP
            public string DynamicSPCall_IUD(string SP_Name, string SP_Parameters, string Call_Values, DataTable D_T, bool T_F, int OutPut)
            {
                /*
                        SP_Name = Stored Procedure Name.
                        SP_Parameters = Parameter Name.
                        Call_Values =  Parameter Value.
                        T_F = Want Output or NotFiniteNumberException.
                        OutPut = Output Type i.e 1 - output parameter and 2 - return variable
                 */

                string retStatus = "0";
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    int PrmCnt = 0;

                    if (T_F)
                    {
                        if (OutPut == 1)
                        {
                            PrmCnt = SP_Parameters.Split(',').Length;
                        }
                        else if (OutPut == 2)
                        {
                            PrmCnt = SP_Parameters.Split(',').Length + 1;
                            SP_Parameters += ",Parameter1";
                            Call_Values += ",0";
                        }
                    }
                    else
                    {
                        PrmCnt = SP_Parameters.Split(',').Length;
                    }

                    SqlParameter[] objParams = new SqlParameter[PrmCnt];

                    for (int i = 0; i < PrmCnt; i++)
                    {
                        if (i == 0)
                        {
                            objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), D_T);
                        }
                        else
                        {
                            objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), Call_Values.Split(',')[i].Trim());
                        }
                    }

                    if (T_F)
                    {
                        if (OutPut == 1)
                        {
                            objParams[PrmCnt - 1].Direction = ParameterDirection.Output;
                        }
                        else if (OutPut == 2)
                        {
                            objParams[PrmCnt - 1] = new SqlParameter();
                            objParams[PrmCnt - 1].Direction = ParameterDirection.ReturnValue;
                        }
                    }

                    retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP(SP_Name, objParams, T_F));
                }
                catch (Exception ex)
                {
                    retStatus = "-99";
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                }
                return retStatus;
            }

            // Added By Sachin A dt on 24012023
            public DataSet DynamicSPCall_Select(string SP_Name, string SP_Parameters, string Call_Values, DataTable D_T)
            {
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[SP_Parameters.Split(',').Length];

                    for (int i = 0; i < SP_Parameters.Split(',').Length; i++)
                    {
                        if (i == 0)
                        {
                            objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), D_T);
                        }
                        else
                        {
                            objParams[i] = new SqlParameter(SP_Parameters.Split(',')[i].Trim(), Call_Values.Split(',')[i].Trim());
                        }
                    }

                    ds = objSQLHelper.ExecuteDataSetSP(SP_Name, objParams);
                }
                catch (Exception ex)
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                }
                return ds;
            }

            //-------------------------------------Added by Vinay Mishra on dated 30052023----------------------------------------------------------

            // Added By Vinay Mishra on 16/05/2023
            public int Mcreate(string ddlClgProg, string minr, int crdt, string ua_no, string ipaddress, string main_ua_no, int mnr_gpr_no)
            {
                //    DataSet ds = null;
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[7];
                    objParams[0] = new SqlParameter("@P_CLGPROGNO", ddlClgProg);
                    objParams[1] = new SqlParameter("@P_MINOR", minr);
                    objParams[2] = new SqlParameter("@P_CREDIT", crdt);
                    objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                    objParams[4] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    objParams[5] = new SqlParameter("@P_MAIN_UA_NO", main_ua_no);
                    objParams[6] = new SqlParameter("@P_MNR_GRP_NO", mnr_gpr_no);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_MINOR", objParams, true);
                    if (obj != null)
                    {
                        status = 1;
                        return status;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return status;
            }



            // Added By Vinay Mishra on 16/05/2023
            public DataSet GetMEdit(int mnrGrpNo)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_MNR_GRP_NO", mnrGrpNo);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MINOR", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }



            // Added By Vinay Mishra on 16/05/2023
            public DataSet GetMcreate()
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MINOR_GROUP", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 19/05/2023
            public int Ccreate(string ddlClgProg, string ddlmnr, string ddlscheme, string ddlcourse, string ccode, string cname, string ua_no, string ipaddress, string main_ua_no, int mnr_gpr_no)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[10];
                    objParams[0] = new SqlParameter("@P_CDB_NO", ddlClgProg);
                    objParams[1] = new SqlParameter("@P_MNR_GRP_NO", ddlmnr);
                    objParams[2] = new SqlParameter("@P_SCHEMENO", ddlscheme);
                    objParams[3] = new SqlParameter("@P_COURSENO", ddlcourse);
                    objParams[4] = new SqlParameter("@P_CCODE", ccode);
                    objParams[5] = new SqlParameter("@P_COURSE_NAME", cname);
                    objParams[6] = new SqlParameter("@P_UA_NO", ua_no);
                    objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                    objParams[8] = new SqlParameter("@P_MAIN_UA_NO", main_ua_no);
                    objParams[9] = new SqlParameter("@P_MNR_OFFERED_COURSE_NO", mnr_gpr_no);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_COURSE", objParams, true);
                    if (obj != null)
                    {
                        status = 1;
                        return status;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return status;
            }



            // Added By Vinay Mishra on 19/05/2023
            public DataSet GetCEdit(int mnrGrpNo)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_MNR_OFFERED_COURSE_NO", mnrGrpNo);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }



            // Added By Vinay Mishra on 19/05/2023
            public DataSet GetCcreate()
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_GROUP", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 22/05/2023
            public DataSet GetMinorList(string cdbNo)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CDB_NO", cdbNo);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MINOR_GROUP_CDB", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 22/05/2023
            public DataSet GetMAllot()
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALLOTED_MINOR_GROUP", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 22/05/2023
            public DataSet GetMinorStudentList(string clgId, string degNo, string branchNo, string semNo)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[4];
                    objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgId);
                    objParams[1] = new SqlParameter("@P_DEGREE_NO", degNo);
                    objParams[2] = new SqlParameter("@P_BRANCH_NO", branchNo);
                    objParams[3] = new SqlParameter("@P_SEMESTERNO", semNo);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_LISTFORMINOR", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 22/05/2023
            public int AllotMinor(string clgId, string degNo, string branchNo, string semNo, string idno, string minorlst, int minorallot, string cdbNo, string uaNo, string ipAddress, string mainuaNo)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[11];
                    objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgId);
                    objParams[1] = new SqlParameter("@P_DEGREE_NO", degNo);
                    objParams[2] = new SqlParameter("@P_BRANCH_NO", branchNo);
                    objParams[3] = new SqlParameter("@P_SEMESTER_NO", semNo);
                    objParams[4] = new SqlParameter("@P_IDNO", idno);
                    objParams[5] = new SqlParameter("@P_MINOR_LIST", minorlst);
                    objParams[6] = new SqlParameter("@P_MINOR_ALLOT", minorallot);
                    objParams[7] = new SqlParameter("@P_CDB_NO", cdbNo);
                    objParams[8] = new SqlParameter("@P_UA_NO", uaNo);
                    objParams[9] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    objParams[10] = new SqlParameter("@P_MAIN_UA_NO", mainuaNo);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ALLOT_MINOR", objParams, true);
                    if (obj != null)
                    {
                        status = 1;
                        return status;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return status;
            }



            // Added By Vinay Mishra on 23/05/2023
            public DataSet GetMAllotMent()
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_COMMONLIST", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 24/05/2023
            public DataSet GetStudentListForCourse(string mnrGrp)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    //objParams[0] = new SqlParameter("@P_CDB_NO", cdbNo);
                    objParams[0] = new SqlParameter("@P_MNR_GRP_NO", mnrGrp);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MINOR_STUDENT_LIST", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 24/05/2023
            public DataSet Get_Minor_List(string clgId)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_COLLEGE_PROGRAM_ID", clgId);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MINOR_GROUP_LIST", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }




            // Added By Vinay Mishra on 25/05/2023
            public int MinorCourseRegistration(string sessionNo, string clgBranch, string admBatch, string sem, string section, string minorlst, int stdTotal, string idno, string courseList, string uaNo, string ipAddress, string mainuaNo)
            {
                int status = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[12];
                    objParams[0] = new SqlParameter("@P_SESSION_NO", sessionNo);
                    objParams[1] = new SqlParameter("@P_COLLEGE_BRANCH", clgBranch);
                    objParams[2] = new SqlParameter("@P_ADM_BATCH", admBatch);
                    objParams[3] = new SqlParameter("@P_SEMESTER_NO", sem);
                    objParams[4] = new SqlParameter("@P_SECTION_NO", section);
                    objParams[5] = new SqlParameter("@P_MINOR_LIST", minorlst);
                    objParams[6] = new SqlParameter("@P_STUDENT_COUNT", stdTotal);
                    objParams[7] = new SqlParameter("@P_IDNO", idno);
                    objParams[8] = new SqlParameter("@P_COURSE_LIST", courseList);
                    objParams[9] = new SqlParameter("@P_UA_NO", uaNo);
                    objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    objParams[11] = new SqlParameter("@P_MAIN_UA_NO", mainuaNo);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MINOR_COURSE_REGISTRATION", objParams, true);
                    if (obj != null)
                    {
                        status = 1;
                        return status;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return status;
            }




            // Added By Vinay Mishra on 26/05/2023
            public int AddExit(string cName, int stat, string eFrom, string eLevel, int crdt, string eSem, string mcgpa, string cond)
            {
                int St = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[8];
                    objParams[0] = new SqlParameter("@P_CRITERIA_NAME", cName);
                    objParams[1] = new SqlParameter("@P_STATUS", stat);
                    objParams[2] = new SqlParameter("@P_EFFECT_FROM", eFrom);
                    objParams[3] = new SqlParameter("@P_EXIT_LEVEL", eLevel);
                    objParams[4] = new SqlParameter("@P_CREDIT", crdt);
                    objParams[5] = new SqlParameter("@P_EXIT_SEMESTER", eSem);
                    objParams[6] = new SqlParameter("@P_MIN_CGPA", mcgpa);
                    objParams[7] = new SqlParameter("@P_CONDITION", cond);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_UPDATE_CRITERIA", objParams, true);
                    if (obj != null)
                    {
                        St = 1;
                        return St;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return St;
            }


            // Added By Vinay Mishra on 30/05/2023
            public DataSet GetExitCriteriaDetailsByCrtID(int crtId)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CRITERIA_NO", crtId);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXIT_CRITERIA_DETAILS", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }


            // Added By Vinay Mishra on 01/05/2023
            public DataSet GetExitCriteriaDetails()
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[0];

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXIT_CRITERIA_DETAILS_ALL", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }



            // Added By Vinay Mishra on 01/05/2023
            public int AllotExitCriteria(string clgSch, string criteria, string act)
            {
                int St = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_COLLEGE_SCHEME", clgSch);
                    objParams[1] = new SqlParameter("@P_CRITERIA_NAME", criteria);
                    objParams[2] = new SqlParameter("@P_ACTION", act);

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXIT_CRITERIA_ALLOTMENT", objParams, true);
                    if (obj != null)
                    {
                        St = 1;
                        return St;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return St;
            }



            // Added By Vinay Mishra on 01/05/2023
            public DataSet GetAllotedExitCriteria(int crtId)
            {
                DataSet ds = null;

                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                    SqlParameter[] objParams = new SqlParameter[1];
                    objParams[0] = new SqlParameter("@P_CRITERIA_ALLOT_NO", crtId);

                    ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALLOTED_CRITERIA", objParams);
                    //return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }


            /// <summary>
            /// Added by Saurabh S.
            /// Get the data to be filled in the Auto Complete Textbox by IDNO
            /// </summary>
            /// <param name="idno"></param>
            /// <param name="tablename"></param>
            /// <param name="col1"></param>
            /// <param name="col2"></param>
            /// <returns></returns>


            public string GetDataByIDNoForDualDegree(int idno, string tablename, string col1, string col2)
            {
                string data = string.Empty;
                DataSet ds = this.FillDropDown(tablename, col1, col2, col1 + " = " + idno, string.Empty);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        data = ds.Tables[0].Rows[0][1].ToString();
                    }
                }

                return data;
            }

        }//END Class Common
    }//END namespace UAIMS
}//END namespace IITMS
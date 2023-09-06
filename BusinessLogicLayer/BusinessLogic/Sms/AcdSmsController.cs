//======================================================================================
// PROJECT NAME  : SMS-SENDER                                                                
// MODULE NAME   : SMS                                                             
// PAGE NAME     : SMS CONTROLLER                                              
// CREATION DATE : 23-OCTO-2012                                                      
// CREATED BY    : PAVAN RAUT                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS;
using System.Text;
using System.Messaging;
using System.Web.Mail;
using System.Xml;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class AcdSmsController
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        AcdSms objsmsCl = new AcdSms();

        public AcdSmsController()
        {
        
        }

        public AcdSmsController(string ConString)
        {
            connectionString = ConString;
        }
        Common objcommon = new Common();

        #region SendSms

        public string SendSms(AcdSms objSms)
        {
            string status = string.Empty;
            try
            {
                string validmobileno = CheckMobileNo(objSms);
                if (validmobileno != string.Empty)
                {
                    status = validmobileno;
                }
                else
                {
                    string smsSend = PrepareSms(objSms);
                    objSms.Msg_content = smsSend.ToString();
                    if (HasConnection() == false)
                    {
                        objSms.Status = 0;
                        objSms.PendingSmsID = SaveSms(objSms);
                        AddPendingSms(objSms);
                        status = "Sending Fail. Message Stored in Pending list";
                    }
                    else
                    {
                        // check if sms is saved then send sms
                        string url = SetAuthentication(objSms, smsSend); // url to send sms
                        status = url;
                        // code to send sms
                        HttpStatusCode result = default(HttpStatusCode);

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.SMSnMMS.co.in/sms.aspx?"));
                        request.ContentType = "text/xml; charset=utf-8";
                        request.Method = "POST";

                        string postDate = "ID=" + objSms.Usename;
                        postDate += "&";
                        postDate += "Pwd=" + objSms.Password;
                        postDate += "&";
                        postDate += "PhNo=" + objSms.Mobileno;
                        postDate += "&";
                        postDate += "Text=" + smsSend;

                        byte[] byteArray = Encoding.UTF8.GetBytes(postDate);
                        request.ContentType = "application/x-www-form-urlencoded";

                        request.ContentLength = byteArray.Length;
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        WebResponse _webresponse = request.GetResponse();
                        dataStream = _webresponse.GetResponseStream();
                        StreamReader reader = new StreamReader(dataStream);
                        status = reader.ReadToEnd();
                        if (status.ToString().Contains("Message Submitted"))
                        {
                            objSms.Status = 1;
                            int saveStatus = SaveSms(objSms);
                            if (saveStatus == 0)
                                status = status + " - Message not save in database";
                        }
                        else
                        {
                            objSms.Status = 0;
                            objSms.PendingSmsID = SaveSms(objSms);
                            if (objSms.PendingSmsID == 0)
                                status = status + "Message not save in database";
                            int pendingStatus = AddPendingSms(objSms);
                            if (pendingStatus == 0)
                                status = status + " - Pending Message not save in database";
                        }
                        //Response.Write(responseFromServer);
                        reader.Close();
                        dataStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //status = Convert.ToInt32(CustomStatus.Error);
                //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;           
        }

        public string SendBulkSms(AcdSms objSms)//, string msg)
        {
            string status = string.Empty;
            try
            {
                string validmobileno = CheckMobileNo(objSms);
                if (validmobileno != string.Empty)
                {
                    status = validmobileno;
                }
                else
                {                    
                    string mobilno = objSms.Mobileno;
                    string[] mobilenos = mobilno.Split(',');
                    for (int i = 0; i < mobilenos.Length; i++)
                    {
                        objSms.Mobileno = mobilenos[i];
                       
                            // check if sms is saved then send sms
                            string url = SetAuthentication(objSms, objSms.Msg_content); // url to send sms
                            status = url;
                            // code to send sms
                            HttpStatusCode result = default(HttpStatusCode);

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://www.SMSnMMS.co.in/sms.aspx?"));
                            request.ContentType = "text/xml; charset=utf-8";
                            request.Method = "POST";

                            string postDate = "ID=" + objSms.Usename;
                            postDate += "&";


                            postDate += "Pwd=" + objSms.Password;
                            postDate += "&";
                            postDate += "PhNo=" + objSms.Mobileno;
                            postDate += "&";
                            postDate += "Text=" + objSms.Msg_content;

                            byte[] byteArray = Encoding.UTF8.GetBytes(postDate);
                            request.ContentType = "application/x-www-form-urlencoded";

                            //request.Headers.Add("APIToken", "API Token");// Signup for a free trial to get one
                            request.ContentLength = byteArray.Length;
                            Stream dataStream = request.GetRequestStream();
                            dataStream.Write(byteArray, 0, byteArray.Length);
                            dataStream.Close();
                            WebResponse _webresponse = request.GetResponse();
                            dataStream = _webresponse.GetResponseStream();
                            StreamReader reader = new StreamReader(dataStream);
                            status = reader.ReadToEnd();
                            if (status.ToString().Contains("Message Submitted"))
                            {
                                objSms.Status = 1;
                                int saveStatus = SaveSms(objSms);
                                if (saveStatus == 0)
                                    status = status + " - Message not save in database";
                            }
                            else
                            {
                                objSms.Status = 0;
                                objSms.PendingSmsID = SaveSms(objSms);
                                if (objSms.PendingSmsID == 0)
                                    status = status + "Message not save in database";
                                int pendingStatus = AddPendingSms(objSms);
                                if (pendingStatus == 0)
                                    status = status + " - Pending Message not save in database";
                            }
                            //Response.Write(responseFromServer);
                            reader.Close();
                            dataStream.Close();
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                //status = Convert.ToInt32(CustomStatus.Error);
                //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;

        }   

        

        public static bool HasConnection()
        {
            try
            {
                //System.Net.IPHostEntry ipHostEntryFromIp = System.Net.Dns.GetHostEntry("www.google.co.in");

                System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("http://www.google.co.in/");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string PrepareSms(AcdSms objSms)
        {
            string smsTemplate = string.Empty;
            try
            {
                DataSet ds = objcommon.FillDropDown("SMS_TEMPLATE_MASTER", "TEMPLATE", "V1,V2,V3,V4,V5,V6,V7,V8,V9,V10,PARAMETER_COUNT", "SMSCODE='" + objSms.Smscode+"'","");
                smsTemplate = ds.Tables[0].Rows[0]["TEMPLATE"].ToString();
                int pareCount = Convert.ToInt32(ds.Tables[0].Rows[0]["PARAMETER_COUNT"].ToString());
                for (int i = 1; i <= pareCount; i++)
                {
                    if (i == 1)
                        smsTemplate = smsTemplate.Replace("V1", objSms.V1.ToString());
                    else if (i == 2)
                        smsTemplate = smsTemplate.Replace("V2", objSms.V2.ToString());
                    else if (i == 3)
                        smsTemplate = smsTemplate.Replace("V3", objSms.V3.ToString());
                    else if (i == 4)
                        smsTemplate = smsTemplate.Replace("V4", objSms.V4.ToString());
                    else if (i == 5)
                        smsTemplate = smsTemplate.Replace("V5", objSms.V5.ToString());
                    else if (i == 6)
                        smsTemplate = smsTemplate.Replace("V6", objSms.V6.ToString());
                    else if (i == 7)
                        smsTemplate = smsTemplate.Replace("V7", objSms.V7.ToString());
                    else if (i == 8)
                        smsTemplate = smsTemplate.Replace("V8", objSms.V8.ToString());
                    else if (i == 9)
                        smsTemplate = smsTemplate.Replace("V9", objSms.V9.ToString());
                    else if (i == 10)
                        smsTemplate = smsTemplate.Replace("V10", objSms.V10.ToString());
                }
            }
            catch (Exception ex)
            {                
                //status = Convert.ToInt32(CustomStatus.Error);
                //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return smsTemplate;
        }

        public int SaveSms(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_UA_NO", objSms.Ua_no);
                objParams[1] = new SqlParameter("@P_MSG_CONTENT", objSms.Msg_content);
                objParams[2] = new SqlParameter("@P_MOBILENO", objSms.Mobileno);
                //objParams[3] = new SqlParameter("@P_SENDING_DATE", objSms.SendingDate);
                objParams[3] = new SqlParameter("@P_MODULE_CODE", objSms.Module_code);
                objParams[4] = new SqlParameter("@P_STATUS", objSms.Status);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_INSERT_SMS", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        //public object SetSmsParameter(SmsLogicLayer.IITMSMessageServer.Sms objSmsWS, Sms objsmsCl)
        //{
        //    objSmsWS.Ua_no = objsmsCl.Ua_no;
        //    objSmsWS.V1 = objsmsCl.V1;
        //    objSmsWS.V2 = objsmsCl.V2;
        //    objSmsWS.V3 = objsmsCl.V3;
        //    objSmsWS.V4 = objsmsCl.V4;
        //    objSmsWS.V5 = objsmsCl.V5;
        //    objSmsWS.V6 = objsmsCl.V6;
        //    objSmsWS.V7 = objsmsCl.V7;
        //    objSmsWS.V8 = objsmsCl.V8;
        //    objSmsWS.V9 = objsmsCl.V9;
        //    objSmsWS.V10 = objsmsCl.V10;
        //    objSmsWS.Smscode = objsmsCl.Smscode;
        //    objSmsWS.Mobileno = objsmsCl.Mobileno;
        //    objSmsWS.Msg_content = objsmsCl.Msg_content;
        //    objSmsWS.Template = objsmsCl.Template;
        //    objSmsWS.Status = objsmsCl.Status;
        //    objSmsWS.SendingDate = objsmsCl.SendingDate;
        //    objSmsWS.Usename = objsmsCl.Usename;
        //    objSmsWS.Password = objsmsCl.Password;
        //    return objSmsWS;
        //}

        public int AddPendingSms(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_UA_NO", objSms.Ua_no);
                objParams[1] = new SqlParameter("@P_MSG_CONTENT", objSms.Msg_content);
                objParams[2] = new SqlParameter("@P_MOBILENO", objSms.Mobileno);
                objParams[3] = new SqlParameter("@P_SENDING_DATE", objSms.SendingDate);
                objParams[4] = new SqlParameter("@P_MODULE_CODE", objSms.Module_code);
                objParams[5] = new SqlParameter("@P_PENDING_MSGID", objSms.PendingSmsID);
                objParams[6] = new SqlParameter("@P_STATUS", objSms.Status);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_INSERT_SMS_PENDING", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        public string SetAuthentication(AcdSms objsms, string sms)
        {
            string url = string.Empty;
            try
            {
                //http://SMSnMMS.co.in/sms.aspx?ID=XXXX&Pwd=YYYYY&PhNo=919899999999,919898989999&Text=MessageText&ScheduleAt=25/03/2009+04%3A00+PM 
                DataSet ds = objcommon.FillDropDown("SMS_SERVICE_MASTER", "ServiceName,Active", "DisplayName,USERNAME,PASSWORD", "Active = 1", "");
                objsms.ServiceName = ds.Tables[0].Rows[0]["ServiceName"].ToString();
                objsms.Usename = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                objsms.Password = ds.Tables[0].Rows[0]["PASSWORD"].ToString();

                url = objsms.ServiceName + "?ID=" + objsms.Usename + "&Pwd=" + objsms.Password+"&pHnO="+objsms.Mobileno + "&Text=" + sms.ToString();
                
            }
            catch (Exception ex)
            {
                //status = Convert.ToInt32(CustomStatus.Error);
                //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return url;
        }

        public string CreateUrl_Balance(AcdSms objsms, string sms)
        {
            string url = string.Empty;
            try
            {
                //http://SMSnMMS.co.in/sms.aspx?ID=XXXX&Pwd=YYYYY&PhNo=919899999999,919898989999&Text=MessageText&ScheduleAt=25/03/2009+04%3A00+PM 
                DataSet ds = objcommon.FillDropDown("SMS_SERVICE_MASTER", "ServiceName,Active", "DisplayName,USERNAME,PASSWORD", "Active = 1", "");
                objsms.ServiceName = ds.Tables[0].Rows[0]["ServiceName"].ToString();
                objsms.Usename = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                objsms.Password = ds.Tables[0].Rows[0]["PASSWORD"].ToString();

                url = objsms.ServiceName + "?ID=" + objsms.Usename + "&Pwd=" + objsms.Password;

            }
            catch (Exception ex)
            {
                //status = Convert.ToInt32(CustomStatus.Error);
                //throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return url;
        }
        public string CheckMobileNo(AcdSms objSms)
        {
            string Msg = string.Empty;
            try
            {
                string mobilno = objSms.Mobileno;
                string[] mobilenos = mobilno.Split(',');
                for (int i = 0; i < mobilenos.Length; i++)
                {
                    Regex objRegx = new Regex("^91[0-9]{10}$");
                    bool result = objRegx.IsMatch(mobilenos[i]);                    
                    if (result == false)
                        Msg = "Please Enter Valid Mobile No(e.g. 91XXXXXXXXXX)";
                }
            }
            catch (Exception ex)
            {                
                //throw;
            }
            return Msg;
        }

        #endregion Send Sms

        #region SmsTemplate

        public int AddSmsTemplate(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_SMSCODE", objSms.Smscode);
                objParams[1] = new SqlParameter("@P_TEMPLATE", objSms.Template);
                objParams[2] = new SqlParameter("@P_V1", objSms.V1);
                objParams[3] = new SqlParameter("@P_V2", objSms.V2);
                objParams[4] = new SqlParameter("@P_V3", objSms.V3);
                objParams[5] = new SqlParameter("@P_V4", objSms.V4);
                objParams[6] = new SqlParameter("@P_V5", objSms.V5);
                objParams[7] = new SqlParameter("@P_V6", objSms.V6);
                objParams[8] = new SqlParameter("@P_V7", objSms.V7);
                objParams[9] = new SqlParameter("@P_V8", objSms.V8);
                objParams[10] = new SqlParameter("@P_V9", objSms.V9);
                objParams[11] = new SqlParameter("@P_V10", objSms.V10);                
                objParams[12] = new SqlParameter("@P_PARACOUNT", objSms.PareCount);                
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_INSERT_SMS_TEMPLATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateSmsTemplate(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[25];
                objParams[0] = new SqlParameter("@P_SMSCODE", objSms.Smscode);
                objParams[1] = new SqlParameter("@P_TEMPLATE", objSms.Template);
                objParams[2] = new SqlParameter("@P_V1", objSms.V1);
                objParams[3] = new SqlParameter("@P_V2", objSms.V2);
                objParams[4] = new SqlParameter("@P_V3", objSms.V3);
                objParams[5] = new SqlParameter("@P_V4", objSms.V4);
                objParams[6] = new SqlParameter("@P_V5", objSms.V5);
                objParams[7] = new SqlParameter("@P_V6", objSms.V6);
                objParams[8] = new SqlParameter("@P_V7", objSms.V7);
                objParams[9] = new SqlParameter("@P_V8", objSms.V8);
                objParams[10] = new SqlParameter("@P_V9", objSms.V9);
                objParams[11] = new SqlParameter("@P_V10", objSms.V10);
                objParams[12] = new SqlParameter("@P_V1_TYPE", objSms.V1_type);
                objParams[13] = new SqlParameter("@P_V2_TYPE", objSms.V2_type);
                objParams[14] = new SqlParameter("@P_V3_TYPE", objSms.V3_type);
                objParams[15] = new SqlParameter("@P_V4_TYPE", objSms.V4_type);
                objParams[16] = new SqlParameter("@P_V5_TYPE", objSms.V5_type);
                objParams[17] = new SqlParameter("@P_V6_TYPE", objSms.V6_type);
                objParams[18] = new SqlParameter("@P_V7_TYPE", objSms.V7_type);
                objParams[19] = new SqlParameter("@P_V8_TYPE", objSms.V8_type);
                objParams[20] = new SqlParameter("@P_V9_TYPE", objSms.V9_type);
                objParams[21] = new SqlParameter("@P_V10_TYPE", objSms.V10_type);
                objParams[22] = new SqlParameter("@P_PARACOUNT", objSms.PareCount);
                objParams[23] = new SqlParameter("@P_TEMPLATEID", objSms.TemplateID);
                objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[24].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_UPDATE_SMS_TEMPLATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion SmsTemplate

        #region Service Master

        public int AddSmsService(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SERVICENAME", objSms.ServiceName);
                objParams[1] = new SqlParameter("@P_ACTIVE", objSms.Active);
                objParams[2] = new SqlParameter("@P_DISPLAYNAME", objSms.DisplayName);
                objParams[3] = new SqlParameter("@P_USERNAME", objSms.Usename);
                objParams[4] = new SqlParameter("@P_PASSWORD", objSms.Password);               
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_INSERT_SMS_SERVICE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateSmsService(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SERVICENAME", objSms.ServiceName);
                objParams[1] = new SqlParameter("@P_ACTIVE", objSms.Active);
                objParams[2] = new SqlParameter("@P_DISPLAYNAME", objSms.DisplayName);
                objParams[3] = new SqlParameter("@P_USERNAME", objSms.Usename);
                objParams[4] = new SqlParameter("@P_PASSWORD", objSms.Password);
                objParams[5] = new SqlParameter("@P_SERVICEid", objSms.ServiceID);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_UPDATE_SMS_SERVICE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }        

        #endregion Service Master

        #region MessegeServer

        public int AddMsgServer(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SERVICENAME", objSms.MsgServerName);
                objParams[1] = new SqlParameter("@P_ACTIVEFLAG", objSms.Activeflag);
                objParams[2] = new SqlParameter("@P_SERVERIP", objSms.MsgServerIP);
                objParams[3] = new SqlParameter("@P_SERVERPORT", objSms.MsgPort);
                objParams[4] = new SqlParameter("@P_PENDINGSMS", objSms.PendingSms);
                objParams[5] = new SqlParameter("@P_NOFORETRY", objSms.NoOfRetry);
                objParams[6] = new SqlParameter("@P_WEBSERVICE", objSms.MsgWebService);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_INSERT_SMS_MSGSERVER", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateMsgServer(AcdSms objSms)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                //Add New ExamName
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SERVICENAME", objSms.MsgServerName);
                objParams[1] = new SqlParameter("@P_ACTIVEFLAG", objSms.Activeflag);
                objParams[2] = new SqlParameter("@P_SERVERIP", objSms.ServiceID);
                objParams[3] = new SqlParameter("@P_SERVERPORT", objSms.MsgPort);
                objParams[4] = new SqlParameter("@P_PENDINGSMS", objSms.PendingSms);
                objParams[5] = new SqlParameter("@P_NOFORETRY", objSms.NoOfRetry);
                objParams[6] = new SqlParameter("@P_MSGSERVERID", objSms.MsgServerID);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SMS_UPDATE_SMS_MSGSERVER", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.SmsServer.AddSmsService.Add-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion MessegeServer



        
    }
}

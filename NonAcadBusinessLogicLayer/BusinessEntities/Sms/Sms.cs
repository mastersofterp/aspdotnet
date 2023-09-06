using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            [Serializable]
            public class Sms
            {
                #region smsTemplate

                #region Private Memeber smsTemplate

                private string smscode = string.Empty;
                private string template = string.Empty;
                private string v1 = string.Empty;
                private string v2 = string.Empty;
                private string v3 = string.Empty;
                private string v4 = string.Empty;
                private string v5 = string.Empty;
                private string v6 = string.Empty;
                private string v7 = string.Empty;
                private string v8 = string.Empty;
                private string v9 = string.Empty;
                private string v10 = string.Empty;
                private string v1_type = string.Empty;
                private string v2_type = string.Empty;
                private string v3_type = string.Empty;
                private string v4_type = string.Empty;
                private string v5_type = string.Empty;
                private string v6_type = string.Empty;
                private string v7_type = string.Empty;
                private string v8_type = string.Empty;
                private string v9_type = string.Empty;
                private string v10_type = string.Empty;
                private int pareCount = 0;
                private int templateID = 0;


                #endregion Private Memeber smsTemplate

                #region Private Properity field smsTemplate

                public string Smscode
                {
                    get { return smscode; }
                    set { smscode = value; }
                }


                public string Template
                {
                    get { return template; }
                    set { template = value; }
                }


                public string V1
                {
                    get { return v1; }
                    set { v1 = value; }
                }


                public string V2
                {
                    get { return v2; }
                    set { v2 = value; }
                }


                public string V3
                {
                    get { return v3; }
                    set { v3 = value; }
                }


                public string V4
                {
                    get { return v4; }
                    set { v4 = value; }
                }

                public string V5
                {
                    get { return v5; }
                    set { v5 = value; }
                }


                public string V6
                {
                    get { return v6; }
                    set { v6 = value; }
                }


                public string V7
                {
                    get { return v7; }
                    set { v7 = value; }
                }


                public string V8
                {
                    get { return v8; }
                    set { v8 = value; }
                }


                public string V9
                {
                    get { return v9; }
                    set { v9 = value; }
                }


                public string V10
                {
                    get { return v10; }
                    set { v10 = value; }
                }


                public string V1_type
                {
                    get { return v1_type; }
                    set { v1_type = value; }
                }

                public string V2_type
                {
                    get { return v2_type; }
                    set { v2_type = value; }
                }

                public string V3_type
                {
                    get { return v3_type; }
                    set { v3_type = value; }
                }

                public string V4_type
                {
                    get { return v4_type; }
                    set { v4_type = value; }
                }

                public string V5_type
                {
                    get { return v5_type; }
                    set { v5_type = value; }
                }

                public string V6_type
                {
                    get { return v6_type; }
                    set { v6_type = value; }
                }

                public string V7_type
                {
                    get { return v7_type; }
                    set { v7_type = value; }
                }

                public string V8_type
                {
                    get { return v8_type; }
                    set { v8_type = value; }
                }

                public string V9_type
                {
                    get { return v9_type; }
                    set { v9_type = value; }
                }

                public string V10_type
                {
                    get { return v10_type; }
                    set { v10_type = value; }
                }

                public int PareCount
                {
                    get { return pareCount; }
                    set { pareCount = value; }
                }

                public int TemplateID
                {
                    get { return templateID; }
                    set { templateID = value; }
                }

                #endregion Private Propertye field smsTemplate

                #endregion smsTemplate

                #region Service Master

                // private Member
                private string serviceName = string.Empty;
                private bool active = false;
                private string displayName = string.Empty;
                private string usename = string.Empty;
                private string password = string.Empty;
                private int serviceID = 0;


                // private Property field
                public string ServiceName
                {
                    get { return serviceName; }
                    set { serviceName = value; }
                }


                public bool Active
                {
                    get { return active; }
                    set { active = value; }
                }


                public string DisplayName
                {
                    get { return displayName; }
                    set { displayName = value; }
                }
                public string Usename
                {
                    get { return usename; }
                    set { usename = value; }
                }
                public string Password
                {
                    get { return password; }
                    set { password = value; }
                }
                public int ServiceID
                {
                    get { return serviceID; }
                    set { serviceID = value; }
                }

                #endregion Service Master

                #region Messenger Server

                // private member
                private string msgServerName = string.Empty;
                private string msgServerIP = string.Empty;
                private int msgPort = 0;
                private bool activeflag = false;
                private int pendingSms = 0;
                private int noOfRetry = 0;
                private int msgServerID = 0;
                private string msgWebService = string.Empty;
                private int pendingSmsID = 0;

                public int PendingSmsID
                {
                    get { return pendingSmsID; }
                    set { pendingSmsID = value; }
                }



                // Private Property field
                public string MsgServerName
                {
                    get { return msgServerName; }
                    set { msgServerName = value; }
                }


                public string MsgServerIP
                {
                    get { return msgServerIP; }
                    set { msgServerIP = value; }
                }


                public int MsgPort
                {
                    get { return msgPort; }
                    set { msgPort = value; }
                }


                public bool Activeflag
                {
                    get { return activeflag; }
                    set { activeflag = value; }
                }


                public int PendingSms
                {
                    get { return pendingSms; }
                    set { pendingSms = value; }
                }


                public int NoOfRetry
                {
                    get { return noOfRetry; }
                    set { noOfRetry = value; }
                }

                public int MsgServerID
                {
                    get { return msgServerID; }
                    set { msgServerID = value; }
                }

                public string MsgWebService
                {
                    get { return msgWebService; }
                    set { msgWebService = value; }
                }

                #endregion Messenger Server

                #region Save Sms

                // private Member
                private int ua_no = 0;
                private string msg_content = string.Empty;
                private string mobileno = string.Empty;
                private DateTime sendingDate = DateTime.MinValue;
                private string module_code = string.Empty;
                private int status = 0;


                // private Property fields

                public int Ua_no
                {
                    get { return ua_no; }
                    set { ua_no = value; }
                }


                public string Msg_content
                {
                    get { return msg_content; }
                    set { msg_content = value; }
                }


                public string Mobileno
                {
                    get { return mobileno; }
                    set { mobileno = value; }
                }


                public DateTime SendingDate
                {
                    get { return sendingDate; }
                    set { sendingDate = value; }
                }


                public string Module_code
                {
                    get { return module_code; }
                    set { module_code = value; }
                }


                public int Status
                {
                    get { return status; }
                    set { status = value; }
                }

                #endregion Save Sms
            }
        }
    }
}

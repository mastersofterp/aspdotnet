using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITLE_Chat
            {
                #region Private Members

                private string _Operation = string.Empty;
                private int _ID;
                private string _USERID;


                private int _NetwordID;
                private int _UA_IDNO;
                private string _UA_IDNO_NETWORKMEMBERID = string.Empty;
                private bool _ISActiveApply;
                private bool _ISActiveFriendRequest;
                private System.Nullable<System.DateTime> _RegistrationDate = DateTime.Now;


                private int _ChatRequestID;
                private System.Nullable<System.DateTime> _ChatRquestDate = DateTime.Now;
                private int _UA_IDNORequestSender;
                private int _UA_IDNORequestReceiver;

              #endregion

                #region Public Properties

                public string Operation
                {
                    get { return this._Operation; }
                    set { this._Operation = value; }
                }

               public int ID
                {
                    get {return this._ID;}
                    set{this._ID = value;}
                }


               public string USERID
                {
                    get{return this._USERID;}
                    set{this._USERID = value;}
                }

               public int NetwordID
               {
                   get { return _NetwordID; }
                   set { _NetwordID = value; }
               }

               public int UA_IDNO
               {
                   get { return _UA_IDNO; }
                   set { _UA_IDNO = value; }
               }

               public string UA_IDNO_NETWORKMEMBERID
               {
                   get { return _UA_IDNO_NETWORKMEMBERID; }
                   set { _UA_IDNO_NETWORKMEMBERID = value; }
               }

               public bool ISActiveApply
               {
                   get { return _ISActiveApply; }
                   set { _ISActiveApply = value; }
               }

               public bool ISActiveFriendRequest
               {
                   get { return _ISActiveFriendRequest; }
                   set { _ISActiveFriendRequest = value; }
               }

               public System.Nullable<System.DateTime> RegistrationDate
               {
                   get { return _RegistrationDate; }
                   set { _RegistrationDate = value; }
               }

               public int ChatRequestID
               {
                   get { return _ChatRequestID; }
                   set { _ChatRequestID = value; }
               }

               public System.Nullable<System.DateTime> ChatRquestDate
               {
                   get { return _ChatRquestDate; }
                   set { _ChatRquestDate = value; }
               }

               public int UA_IDNORequestSender
               {
                   get { return _UA_IDNORequestSender; }
                   set { _UA_IDNORequestSender = value; }
               }

               public int UA_IDNORequestReceiver
               {
                   get { return _UA_IDNORequestReceiver; }
                   set { _UA_IDNORequestReceiver = value; }
               }

                #endregion

            }
        }
    }
}





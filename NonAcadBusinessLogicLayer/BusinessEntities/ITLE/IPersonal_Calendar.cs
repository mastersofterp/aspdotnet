using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IPersonal_Calendar
            {
                #region Private Members

                private string _Operation = string.Empty;
                private int _ID;
                private string _USERID;
                private string _HEADER = string.Empty;
                private string _DESCRIPTION = string.Empty;
                private string _EventForeColor= string.Empty;
                private string _EventBackColor = string.Empty;
                private System.Nullable<System.DateTime> _MAINDATE = DateTime.Now; 

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

             public string DESCRIPTION
                {
                    get{return this._DESCRIPTION;}
                    set{this._DESCRIPTION = value;}
               }

                public string HEADER
                {
                    get{return this._HEADER;}
                    set{this._HEADER = value;}
                }

                public string EventForeColor
                {
                    get{return this._EventForeColor;}
                    set{this._EventForeColor = value;}
                }

                public string EventBackColor
                {
                    get{return this._EventBackColor;}
                    set{this._EventBackColor = value;}
                }

               public System.Nullable<System.DateTime> MAINDATE
                {
                    get { return this._MAINDATE; }
                    set { this._MAINDATE = value; }
                }
                
                #endregion

            }
        }
    }
}





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
            public class WithheldWithDrawn
            {
                #region Private Members
                private System.Nullable<int> _WITHHELDID;

                public System.Nullable<int> WITHHELDID
                {
                    get { return _WITHHELDID; }
                    set { _WITHHELDID = value; }
                }


                private string _WITHHELDIDNOS;

                public string WITHHELDIDNOS
                {
                    get { return _WITHHELDIDNOS; }
                    set { _WITHHELDIDNOS = value; }
                }

                private System.Nullable<int> _IDNO;

                public System.Nullable<int> IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                private string _IDNOS;

                public string IDNOS
                {
                    get { return _IDNOS; }
                    set { _IDNOS = value; }
                }

                private System.Nullable<int> _COLLEGENO;

                public System.Nullable<int> COLLEGENO
                {
                    get { return _COLLEGENO; }
                    set { _COLLEGENO = value; }
                }
                private System.Nullable<int> _STAFFNO;

                public System.Nullable<int> STAFFNO
                {
                    get { return _STAFFNO; }
                    set { _STAFFNO = value; }
                }
                private System.Nullable<bool> _WITHHELDSTATUS;

                public System.Nullable<bool> WITHHELDSTATUS
                {
                    get { return _WITHHELDSTATUS; }
                    set { _WITHHELDSTATUS = value; }
                }
                private string _MONTHYEAR;

                public string MONTHYEAR
                {
                    get { return _MONTHYEAR; }
                    set { _MONTHYEAR = value; }
                }
                private string _WITHHELDREMARK;

                public string WITHHELDREMARK
                {
                    get { return _WITHHELDREMARK; }
                    set { _WITHHELDREMARK = value; }
                }
                private System.Nullable<System.DateTime> _WITHHELDDATE = DateTime.MinValue;

                public System.Nullable<System.DateTime> WITHHELDDATE
                {
                    get { return _WITHHELDDATE; }
                    set { _WITHHELDDATE = value; }
                }

                private string _ORDERBY;
                public string ORDERBY
                {
                    get { return _ORDERBY; }
                    set { _ORDERBY = value; }
                }

                #endregion
            }
        }
    }
}

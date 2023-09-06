using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Faq
            {
                #region Private Members
                private int _fid = 0;
                private int _parentid = 0;
                private int _idno = 0;
                private string _title = string.Empty;
                private DateTime _fdate = DateTime.Now;
                private string _fname = string.Empty;
                private int _del = 0;
                private string _status = string.Empty;
                #endregion

                #region Public Property
                public int FID
                {
                    get { return _fid; }
                    set { _fid = value; }
                }

                public int PARENTID
                {
                    get { return _parentid; }
                    set { _parentid = value; }
                }

                public int IDNO
                {
                    get { return _idno; }
                    set { _idno = value; }
                }

                public string TITLE
                {
                    get { return _title; }
                    set { _title = value; }
                }

                public DateTime FDATE
                {
                    get { return _fdate; }
                    set { _fdate = value; }
                }

                public string FNAME
                {
                    get { return _fname; }
                    set { _fname = value; }
                }

                public int DEL
                {
                    get { return _del; }
                    set { _del = value; }
                }

                public string STATUS
                {
                    get { return _status; }
                    set { _status = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS
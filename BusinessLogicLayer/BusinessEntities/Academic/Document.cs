using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
          public class Document
            {
                #region Private members
                private int _USERNO= 0;
                private string _DOCNAME;
                private string _PATH;
                private int _DOCNO;
                private int _SESSIONNO = 0;
                private string _FILENAME;
                private string _DOCNOS;

               
                #endregion

                #region Public Property Fields
                public string FILENAME
                {
                    get { return _FILENAME; }
                    set { _FILENAME = value; }
                }
                public int USERNO
                {
                    get { return _USERNO; }
                    set { _USERNO = value; }
                }
                public string DOCNAME
                {
                    get { return _DOCNAME; }
                    set { _DOCNAME = value; }
                }
                public string PATH
                {
                    get { return _PATH; }
                    set { _PATH = value; }
                }

                public int DOCNO
                {
                    get { return _DOCNO; }
                    set { _DOCNO = value; }
                }
                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }

                public string DOCNOS
                {
                    get { return _DOCNOS; }
                    set { _DOCNOS = value; }
                }
                #endregion
            }
        }
    }
}

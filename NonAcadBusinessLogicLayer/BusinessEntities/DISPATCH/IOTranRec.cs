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
            public  class IOTranRec
    {
        #region Private Members
        int _cctranno = 0;


        string _ioto = string.Empty;
        string _remarks = string.Empty;





        #endregion

        #region Public Properties


        public int Cctranno1
        {
            get { return _cctranno; }
            set { _cctranno = value; }
        }
        public string Ioto
        {
            get { return _ioto; }
            set { _ioto = value; }
        }
        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }



        #endregion
    }
        }
    }
}
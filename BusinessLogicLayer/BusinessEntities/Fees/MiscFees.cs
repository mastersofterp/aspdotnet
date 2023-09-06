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
            //THIS ENTITY CLASS IS USED TO ADD MISELLANEOUS DATA INTO THE TABLE

            
            public class MiscFees
            {
                #region private region
                //int _mischeadno = 0;
                int _miscCasHbook = 0;

                int _miscSrno = 0;

                string _misccode = string.Empty;
                string _mischead = string.Empty;
                string _userid = string.Empty;
                string _ipaddress = string.Empty;
                DateTime _auditdate = DateTime.MinValue;
                int _collegeCode = 0;
                string _amount = string.Empty;
                string _id = string.Empty;
             

                #endregion

                #region public
              
                public string Amount
                {
                    get { return _amount; }
                    set { _amount = value; }
                }
                public string Id
                {
                    get { return _id; }
                    set { _id = value; }
                }
                public int MiscSrno
                {
                    get { return _miscSrno; }
                    set { _miscSrno = value; }
                }
                public int MiscCasHbook
                {
                    get { return _miscCasHbook; }
                    set { _miscCasHbook = value; }
                }


                public string Misccode
                {
                    get { return _misccode; }
                    set { _misccode = value; }
                }
                public string Mischead
                {
                    get { return _mischead; }
                    set { _mischead = value; }
                }
                public string Userid
                {
                    get { return _userid; }
                    set { _userid = value; }
                }
                public string Ipaddress
                {
                    get { return _ipaddress; }
                    set { _ipaddress = value; }
                }
                public DateTime Auditdate
                {
                    get { return _auditdate; }
                    set { _auditdate = value; }
                }
                public int CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                #endregion
            }
        }
    }
}

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
            public class MessAdvance
            {
                #region Private Members
                private int _MESSRNO = 0;
              
                private int _MESS_NO = 0;
                private decimal _ADV_AMOUNT = 0;
                private DateTime _ADV_DATE = DateTime.MinValue;
                private string _ADV_REMARK = string.Empty;
                private int _comm_member = 0;
                private int _Session_no = 0;
                private string collegeCode = string.Empty;

              
               

               
                #endregion

                #region Public Property Fields
                public int MESSRNO
                {
                    get { return _MESSRNO; }
                    set { _MESSRNO = value; }
                }
                    public int MESS_NO
                    {
                        get { return _MESS_NO; }
                        set { _MESS_NO = value; }
                    }
                    public decimal ADV_AMOUNT
                    {
                        get { return _ADV_AMOUNT; }
                        set { _ADV_AMOUNT = value; }
                    }
                    public DateTime ADV_DATE
                   {
                       get { return _ADV_DATE; }
                       set { _ADV_DATE = value; }
                   }
                    public string ADV_REMARK
                    {
                        get { return _ADV_REMARK; }
                        set { _ADV_REMARK = value; }
                    }
                    public int Comm_Member
                    {
                        get { return _comm_member; }
                        set { _comm_member = value; }
                    }
                    public int Session_No
                    {
                        get { return _Session_no; }
                        set { _Session_no = value; }
                    }
                    public string CollegeCode
                    {
                        get { return collegeCode; }
                        set { collegeCode = value; }
                    }

                          
                #endregion
            }
        }
    }
}

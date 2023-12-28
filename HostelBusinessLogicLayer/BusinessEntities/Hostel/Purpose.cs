using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Purpose
            {
                #region Private Members
                private int _purposeNo = 0;
                private string _purpose_Name = string.Empty;
                private bool _isactive;
                private string _college_Code = string.Empty;
                private string _organizationid = string.Empty;
                #endregion

                #region Public Property Fields
                public int PurposeNo
                {
                    get { return _purposeNo; }
                    set { _purposeNo = value; }
                }
                public string PurposeName
                {
                    get { return _purpose_Name; }
                    set { _purpose_Name = value; }
                }
                public bool IsActive
                {
                    get { return _isactive; }
                    set { _isactive = value; }
                }
                public string CollegeCode
                {
                    get { return _college_Code; }
                    set { _college_Code = value; }
                }
                public string organizationid
                {
                    get { return _organizationid; }
                    set { _organizationid = value; }
                }
                #endregion
            }
        }
    }
}

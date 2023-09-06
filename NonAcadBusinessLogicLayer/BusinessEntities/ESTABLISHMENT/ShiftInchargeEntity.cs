using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ShiftInchargeEntity
            {
                #region Private members

                private int _INCHARGEMASTERID;
                private int _INCHARGEEMPLOYEEIDNO;
                private int _COLLEGE_NO;
                private int _STNO;
                private int _DEPTNO;
                private System.Nullable<bool> _IsTempRemove;
                private System.Nullable<bool> _IsPermanentRemove;

                private string _WARD_NO;

                private int _Created_By;

                #endregion

                #region Public Region

                public int INCHARGEMASTERID
                {
                    get { return _INCHARGEMASTERID; }
                    set { _INCHARGEMASTERID = value; }
                }

                public int INCHARGEEMPLOYEEIDNO
                {
                    get { return _INCHARGEEMPLOYEEIDNO; }
                    set { _INCHARGEEMPLOYEEIDNO = value; }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }
                public int STNO
                {
                    get { return _STNO; }
                    set { _STNO = value; }
                }
                //
                public int DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }
                public System.Nullable<bool> IsTempRemove
                {
                    get { return _IsTempRemove; }
                    set { _IsTempRemove = value; }
                }
                public System.Nullable<bool> IsPermanentRemove
                {
                    get { return _IsPermanentRemove; }
                    set { _IsPermanentRemove = value; }
                }
                public string WARD_NO
                {
                    get { return _WARD_NO; }
                    set { _WARD_NO = value; }
                }

                public int Created_By
                {
                    get { return _Created_By; }
                    set { _Created_By = value; }
                }
                #endregion
            }
        }
    }
}

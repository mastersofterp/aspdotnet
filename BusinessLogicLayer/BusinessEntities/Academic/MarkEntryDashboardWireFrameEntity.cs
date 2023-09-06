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
            public class MarkEntryDashboardWireFrameEntity
            {
                #region Define & Initialize Variable

                private int _SESSIONNO = 0;
                private int _REPORT_TYPE = 0;
                private int _TEMP_EXAMNO = 0;
                private int _COLLEGE_ID = 0;
                private int _DEPTNO = 0;


                #endregion Define & Initialize Variable

                #region Define define getter & setter.

                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }

                public int REPORT_TYPE
                {
                    get { return _REPORT_TYPE; }
                    set { _REPORT_TYPE = value; }
                }

                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }

                public int DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }

                public int TEMP_EXAMNO
                {
                    get { return _TEMP_EXAMNO; }
                    set { _TEMP_EXAMNO = value; }
                }

                #endregion Define define getter & setter.

            }
        }
    }

}

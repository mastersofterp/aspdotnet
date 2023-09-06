using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.ESTATE
{
    public class Temp_Employee
    {
        #region Private Member
        private int _TempEmpNo = 0;
        private string _TempEmployeeName = string.Empty;

        #endregion

        #region Private Properties filed
        public int TempEmpNo 
        {
            get { return _TempEmpNo; }
            set { _TempEmpNo = value; }
        }

        public string TempEmployeeName
        {
            get { return _TempEmployeeName; }
            set { _TempEmployeeName = value; }
        }
        #endregion
    }
}

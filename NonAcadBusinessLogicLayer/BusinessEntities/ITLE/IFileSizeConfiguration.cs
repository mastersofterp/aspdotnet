using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace IITMS
{
    namespace UAIMS
    {
    namespace  BusinessLayer.BusinessEntities
    
{
   public class IFileSizeConfiguration
    {
        #region Private 

        private int _PAGE_ID;

        private int _UA_TYPE;

        private decimal _FILE_SIZE;

        private string _UNIT;

        
        

        #endregion




        #region Public Members

        public int PAGE_ID
        {
            get { return _PAGE_ID; }
            set { _PAGE_ID = value; }
        }


        public int UA_TYPE
        {
            get { return _UA_TYPE; }
            set { _UA_TYPE = value; }
        }


        public decimal FILE_SIZE
        {
            get { return _FILE_SIZE; }
            set { _FILE_SIZE = value; }
        }


        public string UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }


        #endregion
    }

}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class SeatQuota
            {
                #region Private Member
                int _AllIndiaSQNo = 0;
                int _StateLevelSQNo = 0;
                int _BatchNo = 0;
                private string _collegeCode = string.Empty;

                private decimal _AllIndiaPer = 0.0m;
                private decimal _StateLevelPer = 0.0m;

                private decimal _AllIndia_GENPer = 0.0m;
                private decimal _StateLevel_GENPer = 0.0m;

                private decimal _AllIndia_OBCPer = 0.0m;
                private decimal _StateLevel_OBCPer = 0.0m;

                private decimal _AllIndia_SCPer = 0.0m;
                private decimal _StateLevel_SCPer = 0.0m;

                private decimal _AllIndia_STPer = 0.0m;
                private decimal _StateLevel_STPer = 0.0m;

                private decimal _AllIndia_NTPer = 0.0m;
                private decimal _StateLevel_NTPer = 0.0m;
                #endregion

                #region Property Fields
                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                public int AllIndiaSQNo
                {
                    get { return _AllIndiaSQNo; }
                    set { _AllIndiaSQNo = value; }
                }
                public int StateLevelSQNo
                {
                    get { return _StateLevelSQNo; }
                    set { _StateLevelSQNo = value; }
                }
                public int BatchNo
                {
                    get { return _BatchNo; }
                    set { _BatchNo = value; }
                }

                public decimal AllIndiaPer
                {
                    get { return _AllIndiaPer; }
                    set { _AllIndiaPer = value; }
                }
                public decimal StateLevelPer
                {
                    get { return _StateLevelPer; }
                    set { _StateLevelPer = value; }
                }
                public decimal AllIndia_GENPer
                {
                    get { return _AllIndia_GENPer; }
                    set { _AllIndia_GENPer = value; }
                }
                public decimal StateLevel_GENPer
                {
                    get { return _StateLevel_GENPer; }
                    set { _StateLevel_GENPer = value; }
                }
                public decimal AllIndia_OBCPer
                {
                    get { return _AllIndia_OBCPer; }
                    set { _AllIndia_OBCPer = value; }
                }
                public decimal StateLevel_OBCPer
                {
                    get { return _StateLevel_OBCPer; }
                    set { _StateLevel_OBCPer = value; }
                }
                public decimal AllIndia_SCPer
                {
                    get { return _AllIndia_SCPer; }
                    set { _AllIndia_SCPer = value; }
                }
                public decimal StateLevel_SCPer
                {
                    get { return _StateLevel_SCPer; }
                    set { _StateLevel_SCPer = value; }
                }
                public decimal AllIndia_STPer
                {
                    get { return _AllIndia_STPer; }
                    set { _AllIndia_STPer = value; }
                }
                public decimal StateLevel_STPer
                {
                    get { return _StateLevel_STPer; }
                    set { _StateLevel_STPer = value; }
                }
                public decimal AllIndia_NTPer
                {
                    get { return _AllIndia_NTPer; }
                    set { _AllIndia_NTPer = value; }
                }
                public decimal StateLevel_NTPer
                {
                    get { return _StateLevel_NTPer; }
                    set { _StateLevel_NTPer = value; }
                }
           
                #endregion
            }
        }
    }
}


using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class SectionHead
            {
                #region Private Members

                private int _SectionNo = 0;
                private string _SectionName = string.Empty;
                private double _SectionPer = 0;

                #endregion

                #region Public Members

                public int SectionNo
                {
                    get { return _SectionNo; }
                    set { _SectionNo = value; }
                }

                public string SectionName
                {
                    get { return _SectionName; }
                    set { _SectionName = value; }
                }

                public double SectionPer
                {
                    get { return _SectionPer; }
                    set { _SectionPer = value; }
                }

                #endregion
            }
        }
    }
}
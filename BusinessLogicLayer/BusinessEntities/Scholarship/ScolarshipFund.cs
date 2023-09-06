//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES [Scholarship Fund]                     
// CREATION DATE : 27-NOV-2013                                                          
// CREATED BY    : UMESH GANORKAR                                                  
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
            public class ScolarshipFund
            {
                #region Private Members for SCHOLARSHIP FUND

                private int _Srno = 0;

                public int Srno
                {
                    get { return _Srno; }
                    set { _Srno = value; }
                }

                private int _sfno = 0;
                
                public int Sfno
                {
                    get { return _sfno; }
                    set { _sfno = value; }
                }
                private int _admyear = 0;

                public int Admyear
                {
                    get { return _admyear; }
                    set { _admyear = value; }
                }
                private int _concessionno = 0;

                public int Concessionno
                {
                    get { return _concessionno; }
                    set { _concessionno = value; }
                }

                private string _sfchequeno = string.Empty;

                public string Sfchequeno
                {
                    get { return _sfchequeno; }
                    set { _sfchequeno = value; }
                }
                private string _sfchequeDate = string.Empty;

                public string SfchequeDate
                {
                    get { return _sfchequeDate; }
                    set { _sfchequeDate = value; }
                }
                private string _sfbillno = string.Empty;

                public string Sfbillno
                {
                    get { return _sfbillno; }
                    set { _sfbillno = value; }
                }
                double _sfamt = 0.00;

                public double Sfamt
                {
                    get { return _sfamt; }
                    set { _sfamt = value; }
                }
                private string _sfDate = string.Empty;

                public string SfDate
                {
                    get { return _sfDate; }
                    set { _sfDate = value; }
                }
                private string _collegecode = string.Empty;

                public string Collegecode
                {
                    get { return _collegecode; }
                    set { _collegecode = value; }
                }
                private int _degree = 0;

                public int Degree
                {
                    get { return _degree; }
                    set { _degree = value; }
                }

                private int _Branch =0 ;

                public int Branch
                {
                    get { return _Branch; }
                    set { _Branch = value; }
                }

                private string _sfremark = string.Empty;

                public string Sfremark
                {
                    get { return _sfremark; }
                    set { _sfremark = value; }
                }

                private int _schno = 0;

                public int ScholarshipNo
                {
                    get { return _schno; }
                    set { _schno = value; }
                }

                private int _catno = 0;

                public int CategoryNo
                {
                    get { return _catno; }
                    set { _catno = value; }
                }

                private int _shiftno = 0;

                public int ShiiftNo
                {
                    get { return _shiftno; }
                    set { _shiftno = value; }
                }

                private int _studCount = 0;

                public int StudCount
                {
                    get { return _studCount; }
                    set { _studCount = value; }
                }

                private double _Monamt1 = 0;

                public double Monamt1
                {
                    get { return _Monamt1; }
                    set { _Monamt1 = value; }
                }

                private double _Monamt2 = 0;

                public double Monamt2
                {
                    get { return _Monamt2; }
                    set { _Monamt2 = value; }
                }

                private double _Monamt3 = 0;

                public double Monamt3
                {
                    get { return _Monamt3; }
                    set { _Monamt3 = value; }
                }

                private string _Remark = string.Empty;

                public string Remark
                {
                    get { return _Remark; }
                    set { _Remark = value; }
                }

                private double _Nongateamt=0;

                public double Nongateamt
                {
                    get { return _Nongateamt; }
                    set { _Nongateamt = value; }
                }


                private string _stDate = string.Empty;

                public string stDate 
                {
                    get { return _stDate; }
                    set { _stDate = value; }
                }

                #endregion
            }
        }
    }
}

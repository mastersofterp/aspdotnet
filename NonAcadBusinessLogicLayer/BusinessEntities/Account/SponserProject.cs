using System;
using System.Collections;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class SponserProject
            {
                #region Private Members

                private int _ProjectId;

                private string _ProjectShortName;

                private string _ProjectName;

                private int _ProjectSubHeadAllocationId;

                private int _ProjectSubId;

                private double _TotAmtReceived;

                private double _TotAmtSpent;

                private double _TotAmtRemain;

                private string _ProjectSubHeadShort;

                private string _ProjectSubHeadName;

                private int _Department;

                private string _SanctionBy;

                private string _Scheme;

                private string _Coordinator;

                private double _Value;

                private string _ResType;

                private int _ExpHeadtype;               

                private int _SrNo ;

                private int _partyno;
                #endregion

                #region Public Members

                public string SanctionBy
                {
                    get { return _SanctionBy; }
                    set { _SanctionBy = value; }
                }

                public string Scheme
                {
                    get { return _Scheme; }
                    set { _Scheme = value; }
                }

                public string Coordinator
                {
                    get { return _Coordinator; }
                    set { _Coordinator = value; }
                }

                public int Department
                {
                    get { return _Department; }
                    set { _Department = value; }
                }
                
                public double Value
                {
                    get { return _Value; }
                    set { _Value = value; }
                }
                public string SanctionLetter
                {
                    get { return _SanctionLetter; }
                    set { _SanctionLetter = value; }
                }

                public DateTime Date
                {
                    get { return _Date; }
                    set { _Date = value; }
                }

                public int ProjectId
                {
                    get { return _ProjectId; }
                    set { _ProjectId = value; }
                }

                public int ExpHeadType
                {
                    get { return _ExpHeadtype; }
                    set { _ExpHeadtype = value; }
                }
                public int SRNO
                {
                    get { return _SrNo; }
                    set { _SrNo = value; }
                }
                public int Party_No
                {
                    get { return _partyno; }
                    set { _partyno = value; }
                }

                public string ProjectShortName
                {
                    get { return _ProjectShortName; }
                    set { _ProjectShortName = value; }
                }

                public string ProjectName
                {
                    get { return _ProjectName; }
                    set { _ProjectName = value; }
                }

                public int ProjectSubHeadAllocationId
                {
                    get { return _ProjectSubHeadAllocationId; }
                    set { _ProjectSubHeadAllocationId = value; }
                }

                public int ProjectSubId
                {
                    get { return _ProjectSubId; }
                    set { _ProjectSubId = value; }
                }

                public double TotAmtReceived
                {
                    get { return _TotAmtReceived; }
                    set { _TotAmtReceived = value; }
                }

                public double TotAmtSpent
                {
                    get { return _TotAmtSpent; }
                    set { _TotAmtSpent = value; }
                }

                public double TotAmtRemain
                {
                    get { return _TotAmtRemain; }
                    set { _TotAmtRemain = value; }
                }

                public string ProjectSubHeadShort
                {
                    get { return _ProjectSubHeadShort; }
                    set { _ProjectSubHeadShort = value; }
                }

                public string ProjectSubHeadName
                {
                    get { return _ProjectSubHeadName; }
                    set { _ProjectSubHeadName = value; }
                }

                public string ResType
                {
                    get { return _ResType; }
                    set { _ResType = value; }
                }

                #endregion

                public string _SanctionLetter { get; set; }

                public DateTime _Date { get; set; }
            }
        }
    }
}

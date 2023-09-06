using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class RulesCreation
    {
        #region Private Member
        private int _college_Id = 0;
        private int _degreeNo = 0;
        private int _branchNo = 0;
        private int _schemeNo = 0;
        private int _durationRegular = 0;
        private int _durationLateral = 0;
        private int _academicPattern = 0;
        private int _spanPeriod = 0;
        private int _max_attemp_per_sub = 0;
        private int _max_fail_sub = 0;
        private int _max_fail_sub_saperately_TH_PR = 0;

        private int _min_aggregate_per = 0;
        private int _min_aggregate_per_saperately_TH_PR_IA = 0;
        private int _max_backlog_paper = 0;
        private int _max_backlog_with_lab = 0;
        private int _max_backlog_non_credit_paper = 0;

        // ADDED BY: M. REHBAR SHEIKH ON 04-08-2018
        private int _backlog_Sem1_Sem2 = 0;
        private int _backlog_Sem2_Sem3 = 0;
        private int _backlog_Sem3_Sem4 = 0;
        private int _backlog_Sem4_Sem5 = 0;
        private int _backlog_Sem5_Sem6 = 0;
        private int _backlog_Sem6_Sem7 = 0;
        private int _backlog_Sem7_Sem8 = 0;

        //*******ADDED BY: M. REHBAR SHEIKH ON 04-09-2018 | FOR YEAR PATTERN RULES CREATION********
        private int _backlog_Year1_Year2 = 0;
        private int _backlog_Year2_Year3 = 0;
        private int _backlog_Year3_Year4 = 0;
        private int _backlog_Year4_Year5 = 0;
        //*****************************************************************************************

        //Added by S.Patil 29112018
        private string _ipaddress = string.Empty;
        private int _ua_no = 0;
        //end

        private string _remark = string.Empty;
        #endregion

        #region Public Member
        public int college_Id
        {
            get { return _college_Id; }
            set { _college_Id = value; }
        }

        public int degreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int SchemeNo
        {
            get { return _schemeNo; }
            set { _schemeNo = value; }
        }

        public int durationRegular
        {
            get { return _durationRegular; }
            set { _durationRegular = value; }
        }

        public int durationLateral
        {
            get { return _durationLateral; }
            set { _durationLateral = value; }
        }

        public int academicPattern
        {
            get { return _academicPattern; }
            set { _academicPattern = value; }
        }

        public int spanPeriod
        {
            get { return _spanPeriod; }
            set { _spanPeriod = value; }
        }

        public int max_attemp_per_sub
        {
            get { return _max_attemp_per_sub; }
            set { _max_attemp_per_sub = value; }
        }

        public int max_fail_sub
        {
            get { return _max_fail_sub; }
            set { _max_fail_sub = value; }
        }

        public int max_fail_sub_saperately_TH_PR
        {
            get { return _max_fail_sub_saperately_TH_PR; }
            set { _max_fail_sub_saperately_TH_PR = value; }
        }

        public int min_aggregate_per
        {
            get { return _min_aggregate_per; }
            set { _min_aggregate_per = value; }
        }

        public int min_aggregate_per_saperately_TH_PR_IA
        {
            get { return _min_aggregate_per_saperately_TH_PR_IA; }
            set { _min_aggregate_per_saperately_TH_PR_IA = value; }
        }

        public int max_backlog_paper
        {
            get { return _max_backlog_paper; }
            set { _max_backlog_paper = value; }
        }

        public int max_backlog_with_lab
        {
            get { return _max_backlog_with_lab; }
            set { _max_backlog_with_lab = value; }
        }

        public int max_backlog_non_credit_paper
        {
            get { return _max_backlog_non_credit_paper; }
            set { _max_backlog_non_credit_paper = value; }
        }

        // ADDED BY: M. REHBAR SHEIKH ON 04-08-2018
        public int Backlog_Sem1_Sem2
        {
            get { return _backlog_Sem1_Sem2; }
            set { _backlog_Sem1_Sem2 = value; }
        }

        public int Backlog_Sem2_Sem3
        {
            get { return _backlog_Sem2_Sem3; }
            set { _backlog_Sem2_Sem3 = value; }
        }

        public int Backlog_Sem3_Sem4
        {
            get { return _backlog_Sem3_Sem4; }
            set { _backlog_Sem3_Sem4 = value; }
        }

        public int Backlog_Sem4_Sem5
        {
            get { return _backlog_Sem4_Sem5; }
            set { _backlog_Sem4_Sem5 = value; }
        }

        public int Backlog_Sem5_Sem6
        {
            get { return _backlog_Sem5_Sem6; }
            set { _backlog_Sem5_Sem6 = value; }
        }

        public int Backlog_Sem6_Sem7
        {
            get { return _backlog_Sem6_Sem7; }
            set { _backlog_Sem6_Sem7 = value; }
        }

        public int Backlog_Sem7_Sem8
        {
            get { return _backlog_Sem7_Sem8; }
            set { _backlog_Sem7_Sem8 = value; }
        }

        //*******ADDED BY: M. REHBAR SHEIKH ON 04-09-2018 | FOR YEAR PATTERN RULES CREATION********
        public int Backlog_Year1_Year2
        {
            get { return _backlog_Year1_Year2; }
            set { _backlog_Year1_Year2 = value; }
        }

        public int Backlog_Year2_Year3
        {
            get { return _backlog_Year2_Year3; }
            set { _backlog_Year2_Year3 = value; }
        }

        public int Backlog_Year3_Year4
        {
            get { return _backlog_Year3_Year4; }
            set { _backlog_Year3_Year4 = value; }
        }

        public int Backlog_Year4_Year5
        {
            get { return _backlog_Year4_Year5; }
            set { _backlog_Year4_Year5 = value; }
        }
        //***************************************************************

        //Added by S.patil
        public string IPaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        public int UA_NO
        {
            get { return _ua_no; }
            set { _ua_no = value; }
        }
        //end

        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Str_ConfigurationEnt
    {
        private int _MDNO = 0;
        private int _DEPTUSER = 0;
        private string _PRE_DSR_YEAR;
        private string _CUR_DSR_YEAR;
        private int _MAIL_SEND;
        private char _SANCTION_AUTH;
        private int _COMP_STMNT_AUTH_UANO;
        private string _PHONE;
        private string _EMAIL;
        private string _GSTNO;
        private int _PO_APPROVAL;
        private int _DSR_CREATION;
        private int _DEPT_WISE_ITEM;
        private int _COLCODE;
        private string _COLLEGE_NAME;
        private string _CODE_STANDARD;
        private int _IS_COMPARATIVE_STAT_APPROVAL;

        private int _STATENO;

        private int _IS_SECGP = 0;
        //---------------------27/01/2023-----Added by shabina For Making Budget Head optional--------//
        private char _IsAvailableQty;
       
        public char IsAvailableQty
        {
            get { return _IsAvailableQty; }
            set { _IsAvailableQty = value; }      
        }


        private char _IsAuthorityShowOnQuot;
        public char IsAuthorityShowOnQuot
        {
            get { return _IsAuthorityShowOnQuot; }
            set { _IsAuthorityShowOnQuot = value; }
        }





        //---------------------27/01/2023-------------//
        //---26/09/2022 Added by shabina For Making Budget Head optional
        private int _IS_BUDGET_HEAD = 0;        
        public int IS_BUDGET_HEAD
        {
            get { return _IS_BUDGET_HEAD; }
            set { _IS_BUDGET_HEAD = value; }
        }
        //---26/09/2022 end--------------------------//


        public int IS_SECGP
        {
            get { return _IS_SECGP; }
            set { _IS_SECGP = value; }
        }

        public int STATENO
        {
            get { return _STATENO; }
            set { _STATENO = value; }
        }


        public string CODE_STANDARD
        {
            get { return _CODE_STANDARD; }
            set { _CODE_STANDARD = value; }
        }
        public string COLLEGE_NAME
        {
            get { return _COLLEGE_NAME; }
            set { _COLLEGE_NAME = value; }
        }

        public int COLCODE
        {
            get { return _COLCODE; }
            set { _COLCODE = value; }
        }
        public int COMP_STMNT_AUTH_UANO
        {
            get { return _COMP_STMNT_AUTH_UANO; }
            set { _COMP_STMNT_AUTH_UANO = value; }
        }
        public char SANCTION_AUTH
        {
            get { return _SANCTION_AUTH; }
            set { _SANCTION_AUTH = value; }
        }
        public string PHONE
        {
            get { return _PHONE; }
            set { _PHONE = value; }
        }
        public string EMAIL
        {
            get { return _EMAIL; }
            set { _EMAIL = value; }
        }

        public string GSTNO
        {
            get { return _GSTNO; }
            set { _GSTNO = value; }
        }

        public string PRE_DSR_YEAR
        {
            get { return _PRE_DSR_YEAR; }
            set { _PRE_DSR_YEAR = value; }
        }
        public string CUR_DSR_YEAR
        {
            get { return _CUR_DSR_YEAR; }
            set { _CUR_DSR_YEAR = value; }
        }
        public int PO_APPROVAL
        {
            get { return _PO_APPROVAL; }
            set { _PO_APPROVAL = value; }
        }
        public int DSR_CREATION
        {
            get { return _DSR_CREATION; }
            set { _DSR_CREATION = value; }
        }
        public int DEPT_WISE_ITEM
        {
            get { return _DEPT_WISE_ITEM; }
            set { _DEPT_WISE_ITEM = value; }
        }
        public int MDNO
        {
            get { return _MDNO; }
            set { _MDNO = value; }
        }
        public int DEPTUSER
        {
            get { return _DEPTUSER; }
            set { _DEPTUSER = value; }
        }
        public int MAIL_SEND
        {
            get { return _MAIL_SEND; }
            set { _MAIL_SEND = value; }
        }
        public int IS_COMPARATIVE_STAT_APPROVAL
        {
            get { return _IS_COMPARATIVE_STAT_APPROVAL; }
            set { _IS_COMPARATIVE_STAT_APPROVAL = value; }
        }
    }
}

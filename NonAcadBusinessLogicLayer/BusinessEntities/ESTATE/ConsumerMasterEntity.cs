using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ConsumerMasterEntity
    {
        private int _ConsumerIDNO = 0;

        public int ConsumerIDNO
        {
            get { return _ConsumerIDNO; }
            set { _ConsumerIDNO = value; }
        }
        private int _consumertype = 0;

        public int Consumertype
        {
            get { return _consumertype; }
            set { _consumertype = value; }
        }
        private string _COLLEGECODE = string.Empty;

        public string COLLEGECODE
        {
            get { return _COLLEGECODE; }
            set { _COLLEGECODE = value; }
        }

         public string _title  = string.Empty;
         public string Title
         {
             get { return _title; }
             set { _title = value; }

         }

         public string _Firstname  = string.Empty;
         public string Firstname
         {
             get { return _Firstname; }
             set { _Firstname = value; }

         }
         public string _Middlename = string.Empty;
         public string Middlename
         {

             get { return _Middlename; }
             set { _Middlename = value; }
         }

         public string _lastname   = string.Empty;

         public string Lastname
         {
             get { return _lastname; }
             set { _lastname = value; }
         }
         private char _gender;

         public char Gender
         {
             get { return _gender; }
             set { _gender = value; }
         }

         public char _martial;

         public char Martial
         {
             get { return _martial; }
             set { _martial = value; }
         }

         public DateTime  _dateofbirth= DateTime.MinValue;
         public DateTime Dateofbirth
         {
             get { return _dateofbirth; }
             set { _dateofbirth = value; }
         }

         public DateTime _dateofjoining = DateTime.MinValue;


         public DateTime Dateofjoining
         {
             get { return _dateofjoining; }
             set { _dateofjoining = value; }
         }
         public int _deptno       = 0;

         public int Deptno
         {
             get { return _deptno; }
             set { _deptno = value; }
         }

         public int _dessignation = 0;

         public int Dessignation
         {
             get { return _dessignation; }
             set { _dessignation = value; }
         }
         public int _department   = 0;
         public int Department
         {
             get { return _department; }
             set { _department = value; }
         }
         public string _loccaladdress      = string.Empty;
         public string Loccaladdress
         {
             get { return _loccaladdress; }
             set { _loccaladdress = value; }
         }

         public string _permanentAddress   = string.Empty;

         public string PermanentAddress
         {
             get { return _permanentAddress; }
             set { _permanentAddress = value; }
         }
         public string _phonenumber        = string.Empty;
         public string Phonenumber
         {
             get { return _phonenumber; }
             set { _phonenumber = value; }
         }
         public string _emailaddress       = string.Empty;
         public string Emailaddress
         {
             get { return _emailaddress; }
             set { _emailaddress = value; }
         }
         public string _PANnumber = string.Empty;
         public string PANnumber
         {
             get { return _PANnumber; }
             set { _PANnumber = value; }
         }
         public string _consumerfullname = string.Empty;

         public string Consumerfullname
         {
             get { return _consumerfullname; }
             set { _consumerfullname = value; }
         }
         public char _checkstatus;

         public char Checkstatus
         {
             get { return _checkstatus; }
             set { _checkstatus = value; }
         }

    }
}

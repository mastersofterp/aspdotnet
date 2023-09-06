using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public  class weight
    {
        #region Weight Master

        #region Private Members

        private int _weightno = 0;
        private double _weightfrom = 0.00;
        private double _weightto = 0.00;
        private int _unit = 0;
        private double _cost = 0.00;
        private int _posttype = 0;
        private string _creator = string.Empty;
        private DateTime _created_date = DateTime.MinValue;
        private string _college_code = string.Empty;

        #endregion

        #region Public Member

        public int WeightNo
        {
            get { return _weightno; }
            set { _weightno = value; }
        }
        public double WeightFrom
        {
            get { return _weightfrom; }
            set { _weightfrom = value; }
        }
        public double WeightTo
        {
            get { return _weightto; }
            set { _weightto = value; }
        }
        public int Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public int PostType
        {
            get { return _posttype; }
            set { _posttype = value; }
        }
        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }
        public DateTime Created_Date
        {
            get { return _created_date; }
            set { _created_date = value; }
        }
        public string College_code
        {
            get { return _college_code; }
            set { _college_code = value; }
        }

        #endregion
        #endregion



    }

        }
    }
}
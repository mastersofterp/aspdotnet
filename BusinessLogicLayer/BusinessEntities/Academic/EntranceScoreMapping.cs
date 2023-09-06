using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic
{
    public class EntranceScoreMapping
    {
        #region Private Member

        private int _degreeNo = 0;

        private int _examNo = 0;

        private int _categoryNo = 0;

        private double _score = 0.00;

        private int _ent_NO = 0;

        #endregion

        #region Private Property Fields

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int ExamNo
        {
            get { return _examNo; }
            set { _examNo = value; }
        }

        public int CategoryNo
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }

        public double Score
        {
            get { return _score; }
            set { _score = value; }
        }

        public int Ent_NO
        {
            get { return _ent_NO; }
            set { _ent_NO = value; }
        }

        #endregion
    }
}

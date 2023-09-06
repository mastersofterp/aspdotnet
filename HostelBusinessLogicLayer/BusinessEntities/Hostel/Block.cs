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
            public class Block
            {
                #region Private Members
                    private int _blockNo = 0;
                    private string _block_Code = string.Empty;
                    private string _block_Name = string.Empty;
                    private int _hostel_No = 0;
                    private int _floor_No = 0;
                    private int _room_Capacity = 0;
                    private string _college_Code = string.Empty;
                #endregion

                #region Public Property Fields
                    public int BlockNo
                    {
                        get { return _blockNo; }
                        set { _blockNo = value; }
                    }
                    public string BlockCode
                    {
                        get { return _block_Code; }
                        set { _block_Code = value; }
                    }
                    public string BlockName
                   {
                        get { return _block_Name; }
                        set { _block_Name = value; }
                   }
                    public int HostelNo
                    {
                        get { return _hostel_No; }
                        set { _hostel_No = value; }
                    }

                    public int Floor_No
                    {
                        get { return _floor_No; }
                        set { _floor_No = value; }
                    }
                    public int RoomCapacity
                    {
                        get { return _room_Capacity; }
                        set { _room_Capacity = value; }
                    }
                    public string CollegeCode
                     {
                        get { return _college_Code; }
                        set { _college_Code = value; }
                     }        
                #endregion
            }
        }
    }
}

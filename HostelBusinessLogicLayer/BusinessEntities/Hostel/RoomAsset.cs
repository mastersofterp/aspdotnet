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
            public class RoomAsset
            {
                #region Private Member
                    private int _assetNo = 0;
                    private int _hostelNo = 0;
                    private int _blockNo = 0;
                    private int _roomNo = 0;
                    private string _assetName = string.Empty;
                    private int _quantity = 0;
                    private string _collegeCode = string.Empty;
                #endregion
                
                #region Public Property Fields
                    public int AssetNo
                {
                    get { return _assetNo; }
                    set { _assetNo = value; }
                }
                    public int HostelNo
                {
                    get { return _hostelNo; }
                    set { _hostelNo = value; }
                }
                    public int BlockNo
                {
                    get { return _blockNo; }
                    set { _blockNo = value; }
                }
                    public int RoomNo
                {
                    get { return _roomNo; }
                    set { _roomNo = value; }
                }
                    public string AssetName
                {
                    get { return _assetName; }
                    set { _assetName = value; }
                }
                    public int Quantity
                {
                    get { return _quantity; }
                    set { _quantity = value; }
                }
                    public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                #endregion
            }
        }
    }
}

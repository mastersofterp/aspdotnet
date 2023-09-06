//======================================================================================
// PROJECT NAME  : UAIMS (GPM)                                                               
// MODULE NAME   : BUSINESS LOGIC FILE [BLOCK INFO]                                  
// CREATION DATE : 10-AUG-2009                                                        
// CREATED BY    : SANJAY RATNAPARKHI                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
using System;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class BlockController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddBlockInfo(Block objBlock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_BLK_NO", objBlock.BlockName);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", objBlock.HostelNo);
                        objParams[2] = new SqlParameter("@P_NO_OF_FLOORS", objBlock.Floor_No);
                        objParams[3] = new SqlParameter("@P_ROOM_CAPACITY", objBlock.RoomCapacity);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objBlock.CollegeCode);
                        objParams[5] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //OrganizationId  added by Saurabh L on 23/05/2022
                        objParams[6] = new SqlParameter("@P_BLOCK_NO", objBlock.BlockNo);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BLOCK_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.AddBlockInfo-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int AddBlock(Block objBlock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_BLOCK_CODE", objBlock.BlockCode);
                        objParams[1] = new SqlParameter("@P_BLOCK_NAME", objBlock.BlockName);
                        objParams[2] = new SqlParameter("@P_HOSTEL_NO", objBlock.HostelNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objBlock.CollegeCode);
                        objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //OrganizationId  added by Saurabh L on 23/05/2022
                        objParams[5] = new SqlParameter("@P_BL_NO", objBlock.BlockNo);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BLOCK_MASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.AddBlock-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllBlock()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                           new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_BLOCK_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetAllBlock-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllBlk()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_BLOCK_MASTER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetAllBlock-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateBlockInfo(Block objBlock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update Block info
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_BLK_NO", objBlock.BlockName);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", objBlock.HostelNo);
                        objParams[2] = new SqlParameter("@P_NO_OF_FLOORS", objBlock.Floor_No);
                        objParams[3] = new SqlParameter("@P_ROOM_CAPACITY", objBlock.RoomCapacity);
                        objParams[4] = new SqlParameter("@P_BLOCK_NO", objBlock.BlockNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BLOCK_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.UpdateBlock-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateBlock(Block objBlock)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update Block info
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BLOCK_CODE", objBlock.BlockCode);
                        objParams[1] = new SqlParameter("@P_BLOCK_NAME", objBlock.BlockName);
                        objParams[2] = new SqlParameter("@P_HOSTEL_NO", objBlock.HostelNo);
                        objParams[3] = new SqlParameter("@P_BL_NO", objBlock.BlockNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_BLOCK_MASTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.UpdateBlockMaster-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetBlockType(int block_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BLOCK_NO", block_no);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_BLOCK_GET_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetBlockType-> " + ex.ToString());
                    }
                    return dr;
                }
                
                public SqlDataReader GetBlock(int bl_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BL_NO", bl_no);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_BLOCK_MASTER_GET_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BlockController.GetBlock-> " + ex.ToString());
                    }
                    return dr;
                }
            }
        }
    }
}

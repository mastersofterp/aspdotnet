using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.DAC.System.DAC.Command;
/// <summary>
/// Summary description for DAC
/// </summary>

namespace System.DAC
{
    public class DAC
    {
       
        private static string ConnString = "";

        public string StoredProcedure = "";
        
        public System.DAC.Command.Parameters Params = null;

        public static List<DAC> Commands = new List<DAC>();

        public DAC()
	    {
            Params = new global::System.DAC.System.DAC.Command.Parameters();
	    }

        public static void Execute(string StoredProcName)
        {
            List<Parameters.DacParameter>  ParameterPairs = Parameters.MyParameters;
            #region Validation for Parameter collection
                try
                {
                    if (ParameterPairs.Count <= 0)
                    {
                        throw new Exception("Invalid parameter supply. Check your BL Method");
                    }
                }
                catch (Exception Ex)
                {
                    throw (Ex);
                }
            #endregion

            SetConnectionString();

            // Use Transaction Here
            SqlTransaction Trans;

            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                Conn.Open();
                Trans = Conn.BeginTransaction();
                try
                {
                    SqlParameter[] arparams = new SqlParameter[ParameterPairs.Count];

                    int Count = 0;
                    foreach (Parameters.DacParameter pObject in ParameterPairs)
                    {
                        arparams[Count] = new SqlParameter(pObject.Name, pObject.Type);
                        arparams[Count].Value = pObject.Value;

                        //Increment counter
                        Count++;
                    }
                    
                    // Execute stored procedure
                    

                    // Commit Here
                    Trans.Commit();
                }
                catch (Exception Ex)
                {
                    Trans.Rollback();
                    throw (Ex);
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                    Parameters.MyParameters.Clear();
                }
            }
        }

        private static void SetConnectionString()
        {

            ConnString = Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
            
        }

        public static DataSet ExecuteDataSet(string StoredProcName)
        {
            List<Parameters.DacParameter> ParameterPairs = Parameters.MyParameters;
            // Get connection String
            SetConnectionString();

            // Use Transaction Here
            SqlTransaction Trans;
            DataSet ds;
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                SQLHelper objSQLHelper = new SQLHelper(ConnString );
                Conn.Open();
                Trans = Conn.BeginTransaction();
                try
                {
                    SqlParameter[] arparams = new SqlParameter[ParameterPairs.Count];

                    int Count = 0;
                    if (ParameterPairs.Count > 0)
                    {
                        foreach (Parameters.DacParameter pObject in ParameterPairs)
                        {
                            arparams[Count] = new SqlParameter(pObject.Name, pObject.Type);
                            arparams[Count].Value = pObject.Value;

                            //Increment counter
                            Count++;
                        }
                        // Execute stored procedure
                       ds=objSQLHelper.ExecuteDataSetSP(StoredProcName,arparams );
                     
                    }
                    else
                    {
                        ds = objSQLHelper.ExecuteDataSetSP(StoredProcName, arparams);
                    }

                    

                    // Commit Here
                    Trans.Commit();
                }
                catch (Exception Ex)
                {
                    Trans.Rollback();
                    throw (Ex);
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                    Parameters.MyParameters.Clear();
                }
                return ds;
            }
        }

        public static void CopyDataSet(DataSet dsSource, DataSet dsTarget)
        {
            //if (Utility.IsValidDataset(dsSource))
            //{
            //    foreach (DataRow dr in dsSource.Tables[0].Rows)
            //    {
            //        dsTarget.Tables[0].Columns[InvInput.SourceExcel.CURRENT_USER_COLUMN_NAME].DefaultValue = Utility.GetCurrentUser().ToUpper();
            //        DataRow drNew = dsTarget.Tables[0].NewRow();
            //        drNew.ItemArray = dr.ItemArray;
            //        dsTarget.Tables[0].Rows.Add(drNew);
            //    }
            //}
        }

        public static void FlushInvoiceData(DataSet dsSource)
        {
            //List<Parameters.DacParameter> ParameterPairs = Parameters.MyParameters;

            //SetConnectionString();

            //DataSet dsTarget = new DataSet();
            //SqlDataAdapter da = null;
            //SqlCommand cmd = null;
            //SqlCommandBuilder cb = null;
            //using (SqlConnection Conn = new SqlConnection(ConnString))
            //{
            //    Conn.Open();
            //    try
            //    {
            //        cmd = new SqlCommand("select * from TempSourceExcelData(nolock)", Conn);
            //        da = new SqlDataAdapter(cmd);
            //        cb = new SqlCommandBuilder(da);
            //        da.Fill(dsTarget, "Posted");

            //        CopyDataSet(dsSource, dsTarget);
                    
            //        //remove all existing data
            //        DAC.Parameters.Add("@CurrentUser", SqlDbType.VarChar, Utility.GetCurrentUser().ToUpper());
            //        DAC.Execute("nProc_DeleteExcelData");

            //        da.Update(dsTarget, "Posted");
            //    }
            //    catch (Exception Ex)
            //    {
            //        throw (Ex);
            //    }
            //    finally
            //    {
            //        if (Conn.State == ConnectionState.Open)
            //        {
            //            Conn.Close();
            //            Conn.Dispose();
            //        }
            //        Parameters.MyParameters.Clear();
            //    }
            //}
        }

        public static void ExecuteBatch()
        {
            SQLHelper objSQLHelper = new SQLHelper(ConnString);
            if (Commands.Count <= 0)
            {
                throw new Exception("No batch commands found");
            }

            SetConnectionString();

            // Use Transaction Here
            SqlTransaction Trans;

            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                Conn.Open();
                Trans = Conn.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    foreach (DAC oDACCommand in Commands)
                    {
                        List<System.DAC.Command.Parameters.DacParameter> ParameterPairs = oDACCommand.Params.MyParameters;
                        SqlParameter[] arparams = new SqlParameter[ParameterPairs.Count];

                        int Count = 0;
                        foreach (System.DAC.Command.Parameters.DacParameter pObject in ParameterPairs)
                        {
                            arparams[Count] = new SqlParameter(pObject.Name, pObject.Type);
                            arparams[Count].Value = pObject.Value;

                            //Increment counter
                            Count++;
                        }

                        objSQLHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, oDACCommand.StoredProcedure, arparams);
                        // Execute stored procedure
                       // SqlHelper.ExecuteNonQuery(Trans, CommandType.StoredProcedure, oDACCommand.StoredProcedure, arparams);

                    }

                    // Commit Here
                    Trans.Commit ();
                }
                catch (Exception Ex)
                {
                    Trans.Rollback();
                    throw (Ex);
                }
                finally
                {
                    if (Conn.State == ConnectionState.Open)
                    {
                        Conn.Close();
                        Conn.Dispose();
                    }
                    Parameters.MyParameters.Clear();
                }
            }
        }
        
        public class Parameters
        {
            protected internal static List<DacParameter> MyParameters = new List<DacParameter>();

            public static void Add(string Name, SqlDbType dbtype, object value)
            {
                DacParameter KeyValuePair = new DacParameter();
                KeyValuePair.Name = Name;
                KeyValuePair.Type = dbtype;
                KeyValuePair.Value = value;
                MyParameters.Add(KeyValuePair);
            }

            // class param, type , value class
            public class DacParameter
            {
                private string _ParameterName;
                public string Name
                {
                    get { return _ParameterName; }
                    set { _ParameterName = value; }
                }

                private SqlDbType _Type;
                public SqlDbType Type
                {
                    get { return _Type; }
                    set { _Type = value; }
                }

                private object _value;
                public object Value
                {
                    get { return _value; }
                    set { _value = value; }
                }
            }
        }
    }

    namespace System.DAC.Command
    {
        public class Parameters
        {
            protected internal List<DacParameter> MyParameters = new List<DacParameter>();

            public void Add(string Name, SqlDbType dbtype, object value)
            {
                DacParameter KeyValuePair = new DacParameter();
                KeyValuePair.Name = Name;
                KeyValuePair.Type = dbtype;
                KeyValuePair.Value = value;
                MyParameters.Add(KeyValuePair);
            }

            // class param, type , value class
            public class DacParameter
            {
                private string _ParameterName;
                public string Name
                {
                    get { return _ParameterName; }
                    set { _ParameterName = value; }
                }

                private SqlDbType _Type;
                public  SqlDbType Type
                {
                    get { return _Type; }
                    set { _Type = value; }
                }

                private object _value;
                public object Value
                {
                    get { return _value; }
                    set { _value = value; }
                }
            }
        }
    }
}
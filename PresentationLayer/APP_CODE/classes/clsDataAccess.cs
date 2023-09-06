//*****************************************************************************************************************
//Class name : clsDataAccess 
//Defination : This class is the most important class as it does all the updating,Inserting,deleting,Retrieving
//part in the database.The methods in this class is called by all the other methods in other classes.Connection
//with the database is set up here only.We have used Granth SQL database.We added this class after our analysis
//phase.As you will go through the code we have not used any attributes in any class the only thing we have used is
//a query and nothing else. 
//Date Added : 11/11/05
//Please keep the comments if you distribute
//Author :        Quartz (Rajesh Lal - connectrajesh@hotmail.com)
//*****************************************************************************************************************

using System;
using System.Configuration;
using System.Data;
using System.Data.Common ;
using System.Data.SqlClient; 
namespace JumpyForum
{
	public class clsDataAccess // Class defination
	{
		public clsDataAccess()
		{	}
        SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString);  
 		
		public bool openConnection() // Opens database connection with Granth in SQL SERVER
		{
			if (mycon.State == ConnectionState.Closed)
			{
				mycon.Open();
			}

 		return true;
		}
		public void closeConnection() // Closes database connection with Granth in SQL SERVER
		{

		mycon.Close(); 
		mycon = null;
		}
		public SqlDataReader getData(string query) // Getdata from the table required(given in query)in datareader
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			SqlDataReader myr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return myr;
			 
		}
		public SqlDataReader getForumData(int ArticleId, int courseno, int sessionno) // Getdata from the table required(given in query)in datareader
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandType= CommandType.StoredProcedure;
            sqlCommand.CommandText = "ITLE_ShowHierarchyForum";

			SqlParameter newSqlParam = new SqlParameter();
            newSqlParam.ParameterName = "@ArticleId";
            newSqlParam.SqlDbType = SqlDbType.Int ;
            newSqlParam.Direction = ParameterDirection.Input;  
			newSqlParam.Value = ArticleId;
            sqlCommand.Parameters.Add(newSqlParam);

			SqlParameter newSqlParam2 = new SqlParameter();
			newSqlParam2.ParameterName = "@Root";
			newSqlParam2.SqlDbType = SqlDbType.Int ;
			newSqlParam2.Direction = ParameterDirection.Input;  
			newSqlParam2.Value = 0;
			sqlCommand.Parameters.Add(newSqlParam2);

            SqlParameter newSqlParam3 = new SqlParameter();
            newSqlParam3.ParameterName = "@P_COURSENO";
            newSqlParam3.SqlDbType = SqlDbType.Int;
            newSqlParam3.Direction = ParameterDirection.Input;
            newSqlParam3.Value = courseno;
            sqlCommand.Parameters.Add(newSqlParam3);

            SqlParameter newSqlParam4 = new SqlParameter();
            newSqlParam4.ParameterName = "@P_SESSIONNO";
            newSqlParam4.SqlDbType = SqlDbType.Int;
            newSqlParam4.Direction = ParameterDirection.Input;
            newSqlParam4.Value = sessionno;
            sqlCommand.Parameters.Add(newSqlParam4);

			sqlCommand.Connection=mycon;

			SqlDataReader myr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
			return myr;
			 
		}
		public bool DeleteForumData(int ArticleId, int root) // Delete from the table required(given in query)in datareader
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandType= CommandType.StoredProcedure;

            sqlCommand.CommandText = "ITLE_DeleteHierarchyForum";
			SqlParameter newSqlParam = new SqlParameter();
			newSqlParam.ParameterName = "@ArticleId";
			newSqlParam.SqlDbType = SqlDbType.Int ;
			newSqlParam.Direction = ParameterDirection.Input;  
			newSqlParam.Value = ArticleId;
			sqlCommand.Parameters.Add(newSqlParam);

			SqlParameter newSqlParam2 = new SqlParameter();
			newSqlParam2.ParameterName = "@Root";
			newSqlParam2.SqlDbType = SqlDbType.Int ;
			newSqlParam2.Direction = ParameterDirection.Input;  
			newSqlParam2.Value = root;
			sqlCommand.Parameters.Add(newSqlParam2);
			sqlCommand.Connection=mycon;

			int i = sqlCommand.ExecuteNonQuery ();
			if (i == 0)
				return true;
			else
				return false;
			 
		}
		public void saveData(string query) // Save data usually,inserts and updates the data in table given in query
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();  
			
		}
		public bool saveNewData(string query) // Save data usually,inserts and updates the data in table given in query
		{
			bool stat = false;
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.CommandText= query;
				sqlCommand.Connection=mycon;
				sqlCommand.ExecuteNonQuery();
				sqlCommand.Dispose();  
				stat = true;
			}
			catch
			{
				stat=false;
			}
			return stat;
			
		}
		
		public int DeleteData(string query) // Delete data in database depending on the tablename given in query.
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			return sqlCommand.ExecuteNonQuery();
			
		}
		public SqlDataAdapter getDataforUpdate(string query) // Get data by paging using datagrid which returns the dataset in datagris
		{
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query,mycon);
			DataSet dataSet = new DataSet();
			//sqlDataAdapter.Fill(dataSet,"NewData");
			return sqlDataAdapter;
		}
		public DataSet getDatabyPaging(string query) // Get data by paging using datagrid which returns the dataset in datagris
		{
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query,mycon);
			DataSet dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet);
			return dataSet;
			
		}
		public int getCheck(string query) // check a particular value to see the validity of mediaid and userid.This method is called in media and user class.
		{
			int i;
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			i = Convert.ToInt32(sqlCommand.ExecuteScalar());
			return i;
		}
		public string getValue(string query,int j) // Get a value of limit from the database table Employees to check before issuing media.
		{
			string i ="0";
			
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			SqlDataReader myReader = sqlCommand.ExecuteReader();
			
			if( myReader.Read()==true)
			{
				
				i=myReader.GetValue(j).ToString();
				
			}
			return i;
		}
		
		public SqlDataReader Login(string query)//Log in method for LA and Client.
		{
			clsDataAccess myclass = new clsDataAccess();
			myclass.openConnection();
			SqlDataReader dr = myclass.getData(query); //Class Data Access is called here
			return dr; 	
		}
		public DataTable getTablenames()
		{
			SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Information_Schema.Tables where Table_Type = 'BASE TABLE'", mycon);
			DataTable dt = new DataTable();
			da.Fill(dt);
			return dt;

		}

		public int TableWrite(string query) 
		{
			SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandText= query;
			sqlCommand.Connection=mycon;
			return sqlCommand.ExecuteNonQuery();
		}



	}
}

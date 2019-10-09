using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace AutoImports.Inventory.DBAccess
{
    public class DBAccess
    {
        private string connectionString = string.Empty;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlParameter param = null;
        SqlDataAdapter da = null;

        public DBAccess()
        {
            connectionString = WebConfigurationManager.AppSettings["Con"].ToString();
        }

        public DataSet ExecuteDataSet(string spName, string keyFieldName, string keyFieldValue)
        {
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                param = new SqlParameter();
                param.Direction = ParameterDirection.Input;
                param.ParameterName = keyFieldName;
                param.Value = keyFieldValue;
                cmd.Parameters.Add(param);
                da.SelectCommand = cmd;
                da.Fill(ds, "tb");

            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                da.Dispose();

            }
            return ds;
        }

        public DataSet ExecuteDataSet(string spName)
        {
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da.SelectCommand = cmd;
                da.Fill(ds, "tb");

                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Dispose();
                da.Dispose();
            }
        }

        public DataSet ExecuteDataSet(string spName, string[] paramNames, object[] paramValues)
        {
            con = new SqlConnection(connectionString);
            da = new SqlDataAdapter();
            cmd = new SqlCommand();
            DataSet ds = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                for (int i = 0; i <= paramNames.Length - 1; i++)
                {
                    param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = paramNames[i];
                    param.Value = paramValues[i];
                    cmd.Parameters.Add(param);
                }
                da.SelectCommand = cmd;
                da.Fill(ds, "tb");
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                da.Dispose();

            }
        }

        public int InsertData(string spName, string[] paramNames, object[] paramValues, DataTable[] tableValues = null, string[] tableValueTypes = null, string[] tableValueParamNames = null, bool hasReturnValue = false)
        {
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            SqlParameter returnParameter = default(SqlParameter);
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                for (int i = 0; i <= paramNames.Length - 1; i++)
                {
                    param = new SqlParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = paramNames[i];
                    param.Value = paramValues[i];
                    cmd.Parameters.Add(param);
                }

                if (hasReturnValue)
                {
                    returnParameter = new SqlParameter("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(returnParameter);

                }

                if (tableValues != null && tableValues.Length > 0)
                {
                    for (int i = 0; i < tableValues.Length; i++)
                    {
                        param = new SqlParameter();
                        param.Value = tableValues[i];
                        param.ParameterName = tableValueParamNames[i];
                        param.TypeName = tableValueTypes[i];
                        param.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(param);
                    }
                }

                var res = cmd.ExecuteNonQuery();

                if (hasReturnValue)
                {
                    res = returnParameter.Value != DBNull.Value ? Convert.ToInt32(returnParameter.Value) : 0;
                }

                return res;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        public int InsertData(string spName,  string parameterName, DataTable paramterValue)
        {
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                param = new SqlParameter();
                param.Value = paramterValue;
                param.ParameterName = parameterName;
                param.SqlDbType = SqlDbType.Structured;
                cmd.Parameters.Add(param);

                var res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception ex)
            {
                string sss = ex.Message.ToString();
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        public int InsertData(string spName, DataTable tableValues = null, string tableValueTypes = null, string tableValueParamNames = null)
        {
            con = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;

                param = new SqlParameter();
                param.Value = tableValues;
                param.ParameterName = tableValueParamNames;
                param.TypeName = tableValueTypes;
                param.SqlDbType = SqlDbType.Structured;
                cmd.Parameters.Add(param);

                var res = cmd.ExecuteNonQuery();
                return res;
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
            }
        }
        public bool CopyInventoryFromExcel(string strExcelConnection)
        {
            OleDbConnection connection = new OleDbConnection(strExcelConnection);
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                string oledbCommandText = string.Format("select 1,* from [{0}$A1:AC1000]", "Sheet1");
                OleDbCommand cmd = new OleDbCommand(oledbCommandText, connection);
                connection.Open();
                OleDbDataReader dataReader;
                dataReader = cmd.ExecuteReader();
                dt.Load(dataReader);
                if (dt.Rows.Count > 0)
                {
                    dt.Columns.RemoveAt(0);
                    System.Data.DataTable datatable = new System.Data.DataTable();
                    datatable.Columns.Add("PurchaseDate", typeof(DateTime));
                    datatable.Columns.Add("PurchaseFrom", typeof(string));
                    datatable.Columns.Add("VIN", typeof(string));
                    datatable.Columns.Add("Color", typeof(string));
                    datatable.Columns.Add("LocationFrom", typeof(string));
                    datatable.Columns.Add("Year", typeof(Int32));
                    datatable.Columns.Add("Make", typeof(string));
                    datatable.Columns.Add("Model", typeof(string));
                    datatable.Columns.Add("OriginalCost", typeof(decimal));

                    foreach (DataRow dr in dt.Rows)
                        if (dr["PurchaseDate"] != null && dr["PurchaseDate"].ToString().Length != 0)
                            datatable.ImportRow(dr);

                    SqlConnection con = new SqlConnection();
                    //con.ConnectionString = ConfigurationManager.AppSettings["Con"].ToString();
                    con.ConnectionString = connectionString;
                    SqlCommand sqlcmd = new SqlCommand("BulkInventoryUpload", con);
                    SqlParameter parameter = new SqlParameter();
                    //The parameter for the SP must be of SqlDbType.Structured 
                    parameter.ParameterName = "@bulkInventory";
                    parameter.SqlDbType = System.Data.SqlDbType.Structured;
                    parameter.Value = datatable;
                    sqlcmd.Parameters.Add(parameter);
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    int result = sqlcmd.ExecuteNonQuery();
                    con.Close();
                    connection.Close();
                    dataReader = null;
                    dt.Rows.Clear();
                    dt.Columns.Clear();
                    return true;
                }
                else
                    return false;
            }
            catch { }
            finally { }
            return false;
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Configuration;


namespace FileUploadApi.Controllers
{
    public class MasterFunctions
    {



        SqlDataAdapter adp;
        SqlConnection con;
        //SqlDataReader datareader;
        SqlCommand cmd;
        // public string Constrg = ConfigurationManager.ConnectionStrings["ConStrng"].ToString();
        public string connection_string = "Server=KIRAT\\SQLEXPRESS;Database=Tutorial;MultipleActiveResultSets=True;Trusted_Connection=True;uid=sa;pwd=spic123#;";
       // System.Configuration.ConfigurationManager.AppSettings["ConStrng"].ToString();   
        #region sqlconnection
        //create sqlconnection
        public void Sqlconnection()
        {
            con = new SqlConnection(connection_string);
            if (con.State.ToString() == "Closed")
                con.Open();
        }
        public string Execute_Nonquery(string sql_query)
        {
            string ex_str = "yes";
            try
            {
                con = new SqlConnection(connection_string);
                if (con.State.ToString() == "Closed")
                    con.Open();
                cmd = new SqlCommand(sql_query, con);
                cmd.CommandText = sql_query;
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ex_str = SqlExceptionMsg(ex.Number);
            }
            finally
            {
                con.Close();
            }
            return ex_str;
        }

        public DataTable ExecuteDataTable(string cmdText, SqlParameter[] prms, CommandType type)
        {
            using (con = new SqlConnection(connection_string))
            {
                DataTable dt = new DataTable();
                using (cmd = new SqlCommand(cmdText, con))
                {
                    cmd.CommandType = type;
                    cmd.CommandTimeout = 80;
                    if (prms != null)
                    {
                        foreach (SqlParameter p in prms)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                    adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    return dt;
                }
            }
        }

        public string Execute_Transaction(string Sql_qry1, string Sql_qry2)
        {
            string ex_str = "yes";
            try
            {
                con = new SqlConnection(connection_string);
                if (con.State.ToString() == "Closed")
                    con.Open();
                SqlTransaction SqlTrans;
                SqlTrans = con.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = SqlTrans;
                cmd.CommandText = Sql_qry1;
                cmd.ExecuteNonQuery();
                cmd.CommandText = Sql_qry2;
                cmd.ExecuteNonQuery();
                SqlTrans.Commit();
            }
            catch (Exception ex)
            {
                ex_str = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ex_str;
        }
        public string Execute_Transaction(params string[] Query)
        {
            string ex_str = "yes";
            SqlTransaction SqlTrans;
            con = new SqlConnection(connection_string);
            if (con.State.ToString() == "Closed")
                con.Open();
            SqlTrans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = SqlTrans;
                for (int i = 0; i < Query.Length; i++)
                {
                    cmd.CommandText = Query[i];
                    cmd.ExecuteNonQuery();
                }
                SqlTrans.Commit();
            }
            catch (Exception ex)
            {
                SqlTrans.Rollback();
                ex_str = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ex_str;
        }
        public string Execute_Transaction(ArrayList Query)
        {
            string ex_str = "yes";
            SqlTransaction SqlTrans;
            con = new SqlConnection(connection_string);
            if (con.State.ToString() == "Closed")
                con.Open();
            SqlTrans = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.Transaction = SqlTrans;
                for (int i = 0; i < Query.Count; i++)
                {
                    cmd.CommandText = Query[i].ToString();
                    cmd.ExecuteNonQuery();
                }
                SqlTrans.Commit();
            }
            catch (Exception ex)
            {
                SqlTrans.Rollback();
                ex_str = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return ex_str;
        }

        public Int32 Exceute_Sp(string SpName, params string[] para)
        {
            try
            {
                string[] outpara;
                con = new SqlConnection(connection_string);
                if (con.State.ToString() == "Closed")
                    con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = SpName;
                SqlParameter P = new SqlParameter("@Rval", SqlDbType.Int);
                cmd.Parameters.Add("@Rval", SqlDbType.Int);
                cmd.Parameters["@Rval"].Direction = ParameterDirection.ReturnValue;
                for (int i = 0; i < para.Length - 1; i++)
                {
                    if (para[i + 1].ToString() == "")
                    {
                        cmd.Parameters.AddWithValue(para[i].ToString(), System.DBNull.Value);
                    }
                    else if (para[i + 1].ToString() == "Output")
                    {
                        cmd.Parameters.Add(para[i].ToString(), SqlDbType.VarChar, 50);
                        cmd.Parameters[para[i].ToString()].Direction = ParameterDirection.Output;

                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(para[i].ToString(), para[i + 1].ToString());
                    }
                    i = i + 1;
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {


            }
            finally
            {
                con.Close();
            }
            return Convert.ToInt32(cmd.Parameters["@Rval"].Value.ToString());
        }
        #endregion

        #region DataTable

        public DataTable Return_DataTable_Sp(string sql_query)
        {
            SqldataAdater_Connection(sql_query);
            DataSet ds = new DataSet();
            //fill dataset
            adp.Fill(ds);
            DataTable table = ds.Tables[0];
            return table;
        }

        //funtion with returning datatable
        public DataTable Return_DataTable(string sql_query)
        {
            SqldataAdater_Connection(sql_query);
            DataSet ds = new DataSet();
            //fill dataset
            adp.Fill(ds);
            DataTable table = ds.Tables[0];
            return table;
        }
        //function return dataset
        public DataSet Return_Dataset(string sql_query, out string err)
        {
            DataSet ds = new DataSet();
            err = "yes";
            try
            {
                SqldataAdater_Connection(sql_query);
                //fill dataset
                adp.Fill(ds, "user");
                SqldataAdater_Connection("Select * from session where active='1'");
                adp.Fill(ds, "session");
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return ds;
        }

        //function to create connection with database
        public void SqldataAdater_Connection(string sql_query)
        {
            adp = new SqlDataAdapter(sql_query, connection_string);
        }
        public Int64 Execute_Scalar(string sql_query)
        {
            Int64 Id = 0;
            try
            {
                con = new SqlConnection(connection_string);
                if (con.State.ToString() == "Closed")
                    con.Open();
                cmd = new SqlCommand(sql_query, con);
                cmd.CommandText = sql_query;
                Id = Convert.ToInt64(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                con.Close();
            }
            return Id;
        }


        //public static Exception STSThrowSqlException(SqlException ex)
        //{
        //    string Msg="";
        //    Exception STSsqlException = new Exception();
        //    switch (ex.Number)
        //    {
        //        case -2:
        //            {
        //                STSsqlException = new Exception(STSWebConstants.STS_RequestTimeOut);
        //                break;
        //            }
        //        case 18456:
        //        case 1326:
        //            {
        //                STSsqlException = new Exception(STSWebConstants.STS_LoginFailed);
        //                break;
        //            }
        //        case 4060:
        //            {
        //                STSsqlException = new Exception(STSWebConstants.STS_DataBaseNotAvailable); break;
        //            }
        //        case 229:
        //            {
        //                STSsqlException = newException(STSWebConstants.STS_LoginCredentials);
        //                break;
        //            }
        //        case 2601:
        //        case 2627:
        //            {
        //                STSsqlException = newException(STSWebConstants.STS_UniqueKeyConstraint);
        //                break;
        //            }
        //        case 547:
        //            {
        //                STSsqlException = newException(STSWebConstants.STS_ForeignKeyConstraint);
        //                break;
        //            }


        //        default:
        //            {
        //                STSsqlException = newException(STSWebConstants.STS_UnKnowException);
        //                break;
        //            }
        //    }
        //    return STSsqlException;
        //}
        public string SqlExceptionMsg(Int32 ex_Number)
        {
            string Msg = "";
            switch (ex_Number)
            {
                case -2:
                    {
                        Msg = "RequesttimeOut";
                        break;
                    }
                case 18456:
                case 1326:
                    {
                        Msg = "LoginFailed";
                        break;
                    }
                case 4060:
                    {
                        Msg = "DataBaseNotAvailable";
                        break;
                    }
                case 229:
                    {
                        Msg = "LoginCredentials";
                        break;
                    }
                case 2601:
                case 2627:
                    {
                        Msg = "Duplicate Entry Not allowed";
                        break;
                    }
                case 547:
                    {
                        Msg = "ForeignKeyConstraint";
                        break;
                    }
                default:
                    {
                        Msg = "UnKnowException";
                        break;
                    }
            }
            return Msg;
        }
        #endregion

        public int ExecuteSp(string cmdText, SqlParameter[] prms, CommandType type)
        {
            try
            {
                using (con = new SqlConnection(connection_string))
                {
                    using (var cmd = new SqlCommand(cmdText, con))
                    {
                        cmd.CommandType = type;
                        cmd.CommandTimeout = 0;
                        if (prms != null)
                        {
                            foreach (SqlParameter p in prms)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message.ToString();
                con.Close();
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
    }
}

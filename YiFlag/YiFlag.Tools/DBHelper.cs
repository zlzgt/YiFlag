using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace YiFlag.Tools
{
    public class DBHelper
    {
        private static string connString;
        static DBHelper()
        {
            connString = ConfigurationManager.AppSettings["connString"];
            //connString = ConfigurationManager.ConnectionStrings["connString"].ToString();
        }
        /// <summary>
        /// 对连接执行Transact-Sql语句并返回受影响的行数
        /// </summary>
        /// <param name="cmdText">sql语句或存储过程</param>
        /// <param name="cmdType">选择类型sql或存储过程</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static int ExcuteNonQuery(string cmdText,CommandType cmdType, params SqlParameter [] parameters)
        {
            using(SqlConnection connection=new SqlConnection(connString))
            {
                using(SqlCommand cmd=new SqlCommand(cmdText,connection))
                {
                    connection.Open();
                    cmd.CommandType = cmdType;
                    if(parameters.Length>0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 执行查询，并返回查询所返回结果集的第一行的第一列，忽略其他行或列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdText">执行sql语句或者存储过程</param>
        /// <param name="cmdType">选择类型sql或存储过程</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T ExcuteScalar<T>(string cmdText,CommandType cmdType, params SqlParameter [] parameters)
        {
            using(SqlConnection connection=new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    cmd.CommandType = cmdType;
                    if(parameters.Length>0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return (T)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 获取结果集SqlDataReader
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExcuteReader(string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    connection.Open();
                    cmd.CommandType = cmdType;
                    if(parameters.Length>0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }

        /// <summary>
        /// 获取结果DataSet
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using(SqlConnection connection=new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = cmdType;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        if (parameters.Length > 0)
                        {
                            adapter.SelectCommand.Parameters.AddRange(parameters);
                        }
                        adapter.Fill(ds);
                    }
                }
            }
            return ds;
        }
        /// <summary>
        /// 获取结果表
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>

        public static DataTable GetDataTable(string cmdText,CommandType cmdType, params SqlParameter[] parameters)
        {
               return GetDataSet(cmdText,cmdType, parameters).Tables[0];
        }
    }
}

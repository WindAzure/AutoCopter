using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// 此類為抽像類，不允許實例化，在應用時直接調用即可
/// </summary>
public abstract class SqlHelper
{
    /// <summary>
    /// 數據庫連接字符串
    /// </summary>
    public static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ConnectionString.ToString().Trim();

    // Hashtable to store cached parameters
    private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

    /// <summary>
    ///執行一個不需要返回值的SqlCommand命令，通過指定專用的連接字符串。
    /// 使用參數數組形式提供參數列表
    /// </summary>
    /// <param name="connectionString">一個有效的數據庫連接字符串</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個數值表示此SqlCommand命令執行後影響的行數</returns>
    public static int ExecteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            //通過PrePareCommand方法將參數逐個加入到SqlCommand的參數集合中
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            //清空SqlCommand中的參數列表
            cmd.Parameters.Clear();
            return val;
        }
    }

    /// <summary>
    ///執行一個不需要返回值的SqlCommand命令，通過指定專用的連接字符串。
    /// 使用參數數組形式提供參數列表
    /// </summary>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個數值表示此SqlCommand命令執行後影響的行數</returns>
    public static int ExecteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecteNonQuery(_connectionString, cmdType, cmdText, commandParameters);
    }

    /// <summary>
    ///存儲過程專用
    /// </summary>
    /// <param name="cmdText">存儲過程的名字</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個數值表示此SqlCommand命令執行後影響的行數</returns>
    public static int ExecteNonQueryProducts(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
    }

    /// <summary>
    ///Sql語句專用
    /// </summary>
    /// <param name="cmdText">T_Sql語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個數值表示此SqlCommand命令執行後影響的行數</returns>
    public static int ExecteNonQueryText(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecteNonQuery(CommandType.Text, cmdText, commandParameters);
    }

    /// <summary>
    /// 執行一條返回結果集的SqlCommand，通過一個已經存在的數據庫連接
    /// 使用參數數組提供參數
    /// </summary>
    /// <param name="connecttionString">一個現有的數據庫連接</param>
    /// <param name="cmdTye">SqlCommand命令類型</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個表集合(DataTableCollection)表示查詢得到的數據集</returns>
    public static DataTableCollection GetTable(string connecttionString, CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection(connecttionString))
        {
            PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
        }
        DataTableCollection table = ds.Tables;
        return table;
    }

    /// <summary>
    /// 執行一條返回結果集的SqlCommand，通過一個已經存在的數據庫連接
    /// 使用參數數組提供參數
    /// </summary>
    /// <param name="cmdTye">SqlCommand命令類型</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個表集合(DataTableCollection)表示查詢得到的數據集</returns>
    public static DataTableCollection GetTable(CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
    {
        return GetTable(_connectionString,cmdTye, cmdText, commandParameters);
    }

    /// <summary>
    /// 存儲過程專用
    /// </summary>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個表集合(DataTableCollection)表示查詢得到的數據集</returns>
    public static DataTableCollection GetTableProducts(string cmdText, SqlParameter[] commandParameters)
    {
        return GetTable(CommandType.StoredProcedure, cmdText, commandParameters);
    }

    /// <summary>
    /// Sql語句專用
    /// </summary>
    /// <param name="cmdText"> T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個表集合(DataTableCollection)表示查詢得到的數據集</returns>
    public static DataTableCollection GetTableText(string cmdText, SqlParameter[] commandParameters)
    {
        return GetTable(CommandType.Text, cmdText, commandParameters);
    }

    /// <summary>
    /// 為執行命令準備參數
    /// </summary>
    /// <param name="cmd">SqlCommand 命令</param>
    /// <param name="conn">已經存在的數據庫連接</param>
    /// <param name="trans">數據庫事物處理</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">Command text，T-SQL語句例如Select * from Products</param>
    /// <param name="cmdParms">返回帶參數的命令</param>
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
    {
        //判斷數據庫連接狀態
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        //判斷是否需要事物處理
        if (trans != null)
            cmd.Transaction = trans;
        cmd.CommandType = cmdType;
        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }
    }

    /// <summary>
    /// Execute a SqlCommand that returns a resultset against the database specified in the connection string
    /// using the provided parameters.
    /// </summary>
    /// <param name="connectionString">一個有效的數據庫連接字符串</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>A SqlDataReader containing the results</returns>
    public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(connectionString);
        // we use a try/catch here because if the method throws an exception we want to
        // close the connection throw code, because no datareader will exist, hence the
        // commandBehaviour.CloseConnection will not work
        try
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }

    /// <summary>
    /// return a dataset
    /// </summary>
    /// <param name="connectionString">一個有效的數據庫連接字符串</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>return a dataset</returns>
    public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }


    /// <summary>
    /// 返回一個DataSet
    /// </summary>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>return a dataset</returns>
    public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteDataSet(_connectionString, cmdType, cmdText, commandParameters);
    }

    /// <summary>
    /// 返回一個DataSet
    /// </summary>
    /// <param name="cmdText">存儲過程的名字</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>return a dataset</returns>
    public static DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteDataSet(_connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
    }

    /// <summary>
    /// 返回一個DataSet
    /// </summary>

    /// <param name="cmdText">T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>return a dataset</returns>
    public static DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteDataSet(_connectionString, CommandType.Text, cmdText, commandParameters);
    }


    public static DataView ExecuteDataSet(string connectionString, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = sortExpression + " " + direction;
            return dv;
        }
        catch
        {
            conn.Close();
            throw;
        }
    }

    /// <summary>
    /// 返回第一行的第一列
    /// </summary>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個對象</returns>
    public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteScalar(SqlHelper._connectionString, cmdType, cmdText, commandParameters);
    }

    /// <summary>
    /// 返回第一行的第一列存儲過程專用
    /// </summary>
    /// <param name="cmdText">存儲過程的名字</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個對象</returns>
    public static object ExecuteScalarProducts(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteScalar(SqlHelper._connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
    }

    /// <summary>
    /// 返回第一行的第一列Sql語句專用
    /// </summary>
    /// <param name="cmdText">者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>返回一個對象</returns>
    public static object ExecuteScalarText(string cmdText, params SqlParameter[] commandParameters)
    {
        return ExecuteScalar(SqlHelper._connectionString, CommandType.Text, cmdText, commandParameters);
    }

    /// <summary>
    /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:
    /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="connectionString">一個有效的數據庫連接字符串</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
    }

    /// <summary>
    /// Execute a SqlCommand that returns the first column of the first record against an existing database connection
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:
    /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="connectionString">一個有效的數據庫連接字符串</param>
    /// <param name="cmdType">SqlCommand命令類型(存儲過程， T-SQL語句， 等等。)</param>
    /// <param name="cmdText">存儲過程的名字或者T-SQL 語句</param>
    /// <param name="commandParameters">以數組形式提供SqlCommand命令中用到的參數列表</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();
        PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
        object val = cmd.ExecuteScalar();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// add parameter array to the cache
    /// </summary>
    /// <param name="cacheKey">Key to the parameter cache</param>
    /// <param name="cmdParms">an array of SqlParamters to be cached</param>
    public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
    {
        parmCache[cacheKey] = commandParameters;
    }

    /// <summary>
    /// Retrieve cached parameters
    /// </summary>
    /// <param name="cacheKey">key used to lookup parameters</param>
    /// <returns>Cached SqlParamters array</returns>
    public static SqlParameter[] GetCachedParameters(string cacheKey)
    {
        SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
        if (cachedParms == null)
            return null;
        SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
        for (int i = 0, j = cachedParms.Length; i < j; i++)
            clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
        return clonedParms;
    }

    /// <summary>
    /// 檢查是否存在
    /// </summary>
    /// <param name="strSql">Sql語句</param>
    /// <returns>bool結果</returns>
    public static bool Exists(string strSql)
    {
        int cmdresult = Convert.ToInt32(ExecuteScalar(_connectionString, CommandType.Text, strSql, null));
        if (cmdresult == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 檢查是否存在
    /// </summary>
    /// <param name="strSql">Sql語句</param>
    /// <param name="cmdParms">參數</param>
    /// <returns>bool結果</returns>
    public static bool Exists(string strSql, params SqlParameter[] cmdParms)
    {
        int cmdresult = Convert.ToInt32(ExecuteScalar(_connectionString, CommandType.Text, strSql, cmdParms));
        if (cmdresult == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
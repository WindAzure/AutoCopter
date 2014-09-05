<%@ WebHandler Language="C#" Class="StateHttpHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public class StateHttpHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {;
        context.Response.ContentType = "text/plain";
        String account = context.Request["Account"];
        SqlParameter accountParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = SqlDbType.VarChar, Value = account };
        int count = 0;

        if (!String.IsNullOrEmpty(account))
        {
            try
            {
                count=Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM [dbo].[Floor] WHERE [FloorId] IN (SELECT [FKFloor] FROM [dbo].[Register] WHERE [FKAccount]=@Account) and [FloorState]='false'", new SqlParameter[] { accountParam }));   
            }
            catch (Exception e)
            {
            }
        }
        context.Response.Write(count.ToString());
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
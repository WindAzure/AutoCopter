<%@ WebHandler Language="C#" Class="LocationImageHttpHandler" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class LocationImageHttpHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "image/png";
        String account = context.Request["Account"];
        Byte[] imageByte = null;
        SqlParameter accountParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = System.Data.SqlDbType.VarChar, Value = account };

        if (!String.IsNullOrEmpty(account))
        {
            try
            {
                imageByte = SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [Picture] FROM [dbo].[Floor] WHERE [FloorId] IN (SELECT [FKFloor] FROM [dbo].[Register] WHERE [FKAccount]=@Account) and [FloorState]='false'  ORDER BY [StateChangedDate] Desc", new SqlParameter[] { accountParam }) as Byte[];
                context.Response.BinaryWrite(imageByte);
            }
            catch (Exception e)
            {
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
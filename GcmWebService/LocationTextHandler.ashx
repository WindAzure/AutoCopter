<%@ WebHandler Language="C#" Class="LocationTextHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class LocationTextHandler : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        String floorName = "";
        String account = context.Request["Account"];
        SqlParameter param = new SqlParameter() { ParameterName="@Account",SqlDbType=SqlDbType.VarChar,Value=account};
        
        if (!String.IsNullOrEmpty(account))
        {
            try
            {
                floorName = Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT top 1 [FloorName] FROM [dbo].[Floor] WHERE [FloorId] IN (SELECT [FKFloor] FROM [dbo].[Register] WHERE [FKAccount]=@Account) and [FloorState]='false'  ORDER BY [StateChangedDate] Desc", param));
            }
            catch (Exception e)
            {
            }
        }
        context.Response.Write(floorName);
    }
 
    public bool IsReusable 
    {
        get 
        {
            return false;
        }
    }

}
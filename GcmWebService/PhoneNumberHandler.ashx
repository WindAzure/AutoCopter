<%@ WebHandler Language="C#" Class="PhoneNumberHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class PhoneNumberHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        String phoneNumber = "";
        String account = context.Request["Account"];
        SqlParameter param = new SqlParameter() { ParameterName="@Account",SqlDbType=SqlDbType.VarChar,Value=account};
        if (!String.IsNullOrEmpty(account))
        {
            try
            {
                phoneNumber = Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT [TelNumber] FROM [dbo].[CenterInformation]", null));
            }
            catch (Exception e)
            {
            }
        }
        context.Response.Write(phoneNumber);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
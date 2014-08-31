<%@ WebHandler Language="C#" Class="DeviceIdHttpHandler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class DeviceIdHttpHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string json = string.Empty;

        Boolean validate = false;
        string Account = context.Request["Account"];
        string Password = context.Request["Password"];
        string RegistrationID = context.Request["RegistrationID"];
        
        SqlParameter idParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = SqlDbType.VarChar, Value = Account };
        SqlParameter passwordParam = new SqlParameter() { ParameterName="@Password",SqlDbType=SqlDbType.VarChar,Value=Password};
        SqlParameter phoneIdParam = new SqlParameter() { ParameterName = "@RegistrationID", SqlDbType = SqlDbType.VarChar, Value = RegistrationID };

        if (!string.IsNullOrEmpty(Account) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(RegistrationID))
        {
            try
            {
                if (Convert.ToBoolean(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM Member WHERE Account=@Account and Password=@Password", new SqlParameter[] { idParam, passwordParam })))
                {
                    validate = true;
                    if (!Convert.ToBoolean(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM MemberMultiValue WHERE FKAccount=@Account and PhoneRegistId=@RegistrationID", new SqlParameter[] { idParam, phoneIdParam })))
                    {
                        SqlHelper.ExecteNonQuery(CommandType.Text, "INSERT INTO MemberMultiValue(FKAccount,PhoneRegistId) VALUES (@Account,@RegistrationID)", new SqlParameter[] { idParam, phoneIdParam });
                    }
                }
            }
            catch (Exception ex)
            {
                validate = false; 
            }
        }
        
        
        if (validate)
        {
            json = "Welcome";
        }
        else
        {
            json = "Invalid ID or password";
        }

        context.Response.Write(json);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
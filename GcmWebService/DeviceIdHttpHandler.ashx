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
        
        //App傳送的RegistrationID
        string RegistrationID = context.Request["RegistrationID"];
        string Del = context.Request["Del"];//是否刪除RegistrationID
        SqlParameter[] param = new SqlParameter[] { new SqlParameter() { ParameterName = "@RegistrationID", SqlDbType = SqlDbType.VarChar, Value = RegistrationID } };
        string json = string.Empty;//輸出結果的json字串
        bool validate = false;

        if (!string.IsNullOrEmpty(RegistrationID))//防呆
        {
            try
            {
                if (!string.IsNullOrEmpty(Del) && "true".Equals(Del))
                {
                    //從DB把RegistrationID刪除
                    SqlHelper.ExecteNonQuery(CommandType.Text, "Delete From AndroidDeviceTable Where RegistrationID =@RegistrationID", param);
                }
                else
                {
                    int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "Select count(*) From AndroidDeviceTable Where RegistrationID =@RegistrationID", param));
                    if (count == 0)
                    {
                        //DB無此RegistrationID，//新增RegistrationID到DB
                        SqlHelper.ExecteNonQuery(CommandType.Text, "Insert into AndroidDeviceTable (RegistrationID) values (@RegistrationID)", param);
                    }
                }
                validate = true;//執行成功
            }
            catch (Exception ex)
            {
                validate = false;//執行失敗
            }

        }
        else
        {
            validate = false;
        }


        if (validate)
        {
            json = @"{""Success"":true}";
        }
        else
        {
            json = @"{""Success"":false}";
        }
        
        context.Response.Write(json);//輸出訊息
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
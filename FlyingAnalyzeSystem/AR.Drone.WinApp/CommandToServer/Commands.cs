using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AR.Drone.WinApp.CommandToServer
{
    public class Commands
    {
        public static void SendGCM(String planeId, Boolean state)
        {
            String currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlParameter planeIdParam = new SqlParameter() { ParameterName = "@PlaneId", SqlDbType = SqlDbType.NVarChar, Value = planeId };
            String floorId = Convert.ToString(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT [FKFloor] FROM [dbo].[Charge] WHERE [FKPlaneId]=@PlaneId", new SqlParameter[] { planeIdParam }));
            if (!String.IsNullOrEmpty(floorId))
            {
                SqlParameter floorIdParam = new SqlParameter() { ParameterName = "@FloorId", SqlDbType = SqlDbType.NVarChar, Value = floorId };
                SqlParameter stateParam = new SqlParameter() { ParameterName = "@FloorState", SqlDbType = SqlDbType.Bit, Value = state };
                SqlParameter dateTimeParam = new SqlParameter() { ParameterName = "@StateChangedDate", SqlDbType = SqlDbType.NVarChar, Value = currentDateTime };
                SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[Floor] SET [FloorState]=@FloorState, [StateChangedDate] = @StateChangedDate WHERE [FloorId] = @FloorId", new SqlParameter[] { stateParam, dateTimeParam, floorIdParam });

                DataSet accountResult = SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT [FKAccount] FROM [dbo].[Register] WHERE [FKFloor]=@FloorId", new SqlParameter[] { floorIdParam });
                int accountRows = accountResult.Tables[0].Rows.Count;
                for (int i = 0; i < accountRows; i++)
                {
                    String account = accountResult.Tables[0].Rows[i].ItemArray[0].ToString();
                    SqlParameter accountParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = SqlDbType.NVarChar, Value = account };
                    DataSet phoneIdResult = SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT [PhoneRegistId] FROM [dbo].[MemberMultiValue] WHERE [FKAccount]=@Account", new SqlParameter[] { accountParam });
                    int idRows = phoneIdResult.Tables[0].Rows.Count;
                    for (int j = 0; j < idRows; j++)
                    {
                        Debug.WriteLine(phoneIdResult.Tables[0].Rows[j].ItemArray[0].ToString());
                        AndroidGcmNotificationer gcmSender = new AndroidGcmNotificationer();
                        gcmSender.SendNotification(account, phoneIdResult.Tables[0].Rows[j].ItemArray[0].ToString(), state.ToString());
                    }
                }
            }
        }

        public static DataSet GetFloorInformation()
        {
            return SqlHelper.ExecuteDataSet(CommandType.Text, "SELECT  [FloorName],[Picture],[MileageRecord],[FloorId],[MileageLine] FROM [dbo].[Floor]", null);
        }
        public static void UpdatePatrolFloor(String planeId, String floorId)
        {
            SqlParameter planeIdParam = new SqlParameter() { ParameterName = "@PlaneId", SqlDbType = SqlDbType.NVarChar, Value = planeId };
            SqlParameter floorIdParam = new SqlParameter() { ParameterName = "@FloorId", SqlDbType = SqlDbType.NVarChar, Value = floorId };
            SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[Charge] SET [FKFloor] = @FloorId WHERE [FKPlaneId] =@PlaneId", new SqlParameter[] { floorIdParam, planeIdParam });
        }

        public static void UpdateMileage(String floorId, String mileage)
        {
            SqlParameter floorIdParam = new SqlParameter() { ParameterName = "@FloorId", SqlDbType = SqlDbType.NVarChar, Value = floorId };
            SqlParameter mileageParam = new SqlParameter() { ParameterName = "@Mileage", SqlDbType = SqlDbType.NVarChar, Value = mileage };
            SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[Floor] SET [MileageRecord] = @Mileage WHERE [FloorId] =@FloorId", new SqlParameter[] { floorIdParam, mileageParam });
        }

        public static void UpdateMileageLine(String floorId, String mileageLine)
        {
            SqlParameter floorIdParam = new SqlParameter() { ParameterName = "@FloorId", SqlDbType = SqlDbType.NVarChar, Value = floorId };
            SqlParameter mileageLineParam = new SqlParameter() { ParameterName = "@MileageLine", SqlDbType = SqlDbType.NVarChar, Value = mileageLine };
            SqlHelper.ExecuteScalar(CommandType.Text, "UPDATE [dbo].[Floor] SET [MileageLine] = @Mileage WHERE [FloorId] =@FloorId", new SqlParameter[] { floorIdParam, mileageLineParam });
        }

        public static void RegistFloor(String imagePath)
        {
            byte[] data = File.ReadAllBytes(imagePath);
            SqlParameter floorNameParam = new SqlParameter() { ParameterName = "@FloorName", SqlDbType = SqlDbType.NVarChar, Value = "aaa" };
            SqlParameter floorStateParam = new SqlParameter() { ParameterName = "@State", SqlDbType = SqlDbType.Bit, Value = true };
            SqlParameter floorImagePathParam = new SqlParameter() { ParameterName = "@ImagePath", SqlDbType = SqlDbType.Image, Value = data };
            SqlParameter floorStateChangedDate = new SqlParameter() { ParameterName = "@CurrentDate", SqlDbType = SqlDbType.DateTime, Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            SqlHelper.ExecuteScalar(CommandType.Text, "INSERT INTO [dbo].[Floor] ([FloorId],[Picture],[FloorName],[FloorState],[StateChangedDate]) VALUES(NEWID(),@ImagePath,@FloorName,@State,@CurrentDate)", new SqlParameter[] { floorImagePathParam, floorNameParam, floorStateParam, floorStateChangedDate });
        }

        public static bool IsAdmin(String account, String password)
        {
            if (String.IsNullOrEmpty(account) || String.IsNullOrEmpty(password) || account.Length > 10 || password.Length > 10)
            {
                return false;
            }

            SqlParameter accountParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = SqlDbType.NVarChar, Value = account };
            SqlParameter passwordParam = new SqlParameter() { ParameterName = "@Password", SqlDbType = SqlDbType.NVarChar, Value = password };
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM [dbo].[Member] WHERE [Account]=@Account and [Password]=@Password and [IsAdmin]='true'", new SqlParameter[] { accountParam, passwordParam }));
            return count == 1;
        }

        public static bool IsInBeaconTable(String mac)
        {
            SqlParameter macParam = new SqlParameter() { ParameterName = "@Mac", SqlDbType = SqlDbType.NVarChar, Value = mac };
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, "SELECT count(*) FROM [dbo].[Beacon] WHERE [Mac]=@Mac", new SqlParameter[] { macParam }));
            return count == 1;
        }
    }
}

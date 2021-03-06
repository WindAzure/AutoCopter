﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AR.Drone.WinApp
{
    public class AndroidGcmNotificationer
    {
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;
        private String GoogleApiKey = "AIzaSyA5Fwe7JGaBdtOgUPTinUe662InlXpVHxA";

        private static String AssembleDataIntoJson(String deviceId, String data)
        {
            return "{\"registration_ids\":[\"" + deviceId + "\"],\"data\":{\"Msg\":" + "\"" + data + "\"}}";
        }

        private void Send(string data)
        {
            byte[] dataByteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = dataByteArray.Length;

            using (Stream payloadStream = request.GetRequestStream())
            {
                payloadStream.Write(dataByteArray, 0, dataByteArray.Length);
                payloadStream.Close();
            }
        }

        private string ReadResponse(HttpWebResponse r)
        {
            StreamReader responseReader = new StreamReader(r.GetResponseStream());
            return responseReader.ReadToEnd();
        }

        public AndroidGcmNotificationer()
        {
            request = WebRequest.Create("https://android.googleapis.com/gcm/send") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization: key=" + GoogleApiKey);
            request.UserAgent = "Android GCM Message Sender Client 1.0";
        }

        public void UnRegister(String account,String deviceId)
        {
            SqlParameter accountParam = new SqlParameter() { ParameterName = "@Account", SqlDbType = SqlDbType.NVarChar, Value = account };
            SqlParameter registionIdParam = new SqlParameter() { ParameterName = "@RegistionId", SqlDbType = SqlDbType.NVarChar, Value = deviceId };
            SqlHelper.ExecuteScalar(CommandType.Text, "DELETE FROM [dbo].[MemberMultiValue] WHERE [FKAccount]=@Account and [PhoneRegistId]=@RegistionId",new SqlParameter[]{ accountParam, registionIdParam});
        }

        public void SendNotification(String account,String deviceId, String data)
        {
            data = AssembleDataIntoJson(deviceId, data);
            Send(data);

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                Debug.WriteLine("[ERROR] There is a problem within processing GCM message \n" + e.Message);
            }

            data = ReadResponse(response);
            if (data.Contains("NotRegistered"))
            {
                UnRegister(account,deviceId);
            }
        }

        public void SendNotification(String deviceId, String data)
        {
            data = AssembleDataIntoJson(deviceId, data);
            Send(data);

            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException e)
            {
                Debug.WriteLine("[ERROR] There is a problem within processing GCM message \n" + e.Message);
            }

            data = ReadResponse(response);
        }
    }
}

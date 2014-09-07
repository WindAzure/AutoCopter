package com.example.http.to.server;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;

import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;

import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.util.Log;

public class GetSystemPhoneNumberTask extends AsyncTask<Void,Void,String>
{
	private GetSystemTelTaskEventRegister _event;
	
	public GetSystemPhoneNumberTask(GetSystemTelTaskEventRegister event)
	{
		_event=event;
	}
	
	@Override
	protected String doInBackground(Void... params) 
	{
		String phoneNumber="";
		HttpClient client=new DefaultHttpClient();
		HttpPost post=new HttpPost("http://1.34.139.73/PhoneNumberHandler.ashx");
		try
		{
			SharedPreferences pref=UsualMethod.GetSharedPreferences();
			List<NameValuePair> nameValuePairs=new ArrayList<NameValuePair>(2);
			nameValuePairs.add(new BasicNameValuePair("Account",pref.getString(ConstValue.SHARE_PREFERENCES_LOGIN_ACCOUNT, "")));
			post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
			HttpResponse response=client.execute(post);
			phoneNumber=EntityUtils.toString(response.getEntity());
		}
		catch(Exception e)
		{
    		Log.v("Exception",e.toString());
		}
		return phoneNumber;
	}
	
	@Override
	protected void onPostExecute(String phoneNumber)
	{
		_event.AsyncTaskFinished(phoneNumber);
	}
}

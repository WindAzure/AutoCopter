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

import com.example.autocopter.MainActivity;
import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.util.Log;

public class GetSystemPhoneNumberTask extends AsyncTask<Void,Void,String>
{
	private String _account;
	private Context _context=null;
	
	public GetSystemPhoneNumberTask(Context context,String account)
	{
		_context=context;
		_account=account;
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
		ConstValue.SYSTEM_PHONE_NUMBER=phoneNumber;
		new CheckServerStateTask(_context,_account).execute();
	}
}

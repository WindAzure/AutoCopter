package com.example.http.to.server;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.NameValuePair;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;

import com.example.flowicon.FlowIconSingleton;
import com.example.flowicon.UnNormalService;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.util.Log;

public class GetLocationTextTask extends AsyncTask<String,Void,String>
{
	private Context _context=null;
	
	public GetLocationTextTask(Context context)
	{
		_context=context;
	}
	
	@Override
	protected String doInBackground(String... params) 
	{
		String location="";
		HttpClient client=new DefaultHttpClient();
		HttpPost post=new HttpPost("http://1.34.139.73/LocationTextHandler.ashx");
		try
		{
			List<NameValuePair> nameValuePairs=new ArrayList<NameValuePair>(2);
			nameValuePairs.add(new BasicNameValuePair("Account",params[0]));
			post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
			HttpResponse response=client.execute(post);
			location=EntityUtils.toString(response.getEntity());
		}
		catch(Exception e)
		{
    		Log.v("Exception",e.toString());
		}
		return location;
	}
	
	@Override
	protected void onPostExecute(String location)
	{
		FlowIconSingleton._locationText=location;
        _context.startService(new Intent(_context, UnNormalService.class));
        ((Activity)(_context)).finish();
	}
}
package com.example.http.to.server;

import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.util.EntityUtils;

import com.example.flowicon.NormalService;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.os.AsyncTask;
import android.util.Log;

public class CheckServerStateTask extends AsyncTask<Void,Void,String>
{
	private String _account;
	private Context _context=null;
	
	public CheckServerStateTask(Context context,String account)
	{
		_context=context;
		_account=account;
	}
	
	@Override
	protected String doInBackground(Void... params) 
	{
		String count="0";
		HttpClient client=new DefaultHttpClient();
		HttpPost post=new HttpPost("http://1.34.139.73/StateHttpHandler.ashx");
		try
		{
			List<NameValuePair> nameValuePairs=new ArrayList<NameValuePair>(2);
			nameValuePairs.add(new BasicNameValuePair("Account",_account));
			post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
			
			HttpResponse response=client.execute(post);
			count=EntityUtils.toString(response.getEntity());
		}
		catch(Exception e)
		{
    		Log.v("Exception",e.toString());
		}
		return count;
	}
	
	@Override
	protected void onPostExecute(String count)
	{
		if(count=="0")
		{
	        _context.startService(new Intent(_context, NormalService.class));
	        ((Activity)(_context)).finish();
		}
		else
		{
			new GetLocationImageTask(_context).execute(_account);
		}
	}
}

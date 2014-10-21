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

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import com.example.flowicon.FlowIconSingleton;

public class GetLocationPointTask extends AsyncTask<Void,Void,String>
{
	private Context _context=null;
	private String _account="";
	
	public GetLocationPointTask(Context context,String account)
	{
		_context=context;
		_account=account;
	}
	
	@Override
	protected String doInBackground(Void... params) 
	{
		String location="";
		HttpClient client=new DefaultHttpClient();
		HttpPost post=new HttpPost("http://1.34.139.73/LocationPointHandler.ashx");
		try
		{
			List<NameValuePair> nameValuePairs=new ArrayList<NameValuePair>(2);
			nameValuePairs.add(new BasicNameValuePair("Account",_account));
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
	protected void onPostExecute(String point)
	{
		FlowIconSingleton._locationPoint=point;
		Log.v("asdf",point);
		new GetLocationTextTask(_context).execute(_account);
	}
}

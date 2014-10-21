package com.example.http.to.server;

import java.io.InputStream;
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

import com.example.flowicon.FlowIconSingleton;
import com.example.flowicon.UnNormalService;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.util.Log;

public class GetLocationImageTask extends AsyncTask<Void,Void,Bitmap>
{
	private Context _context=null;
	private String _account="";
	
	public GetLocationImageTask(Context context,String account)
	{
		_context=context;
		_account=account;
	}
	
	@Override
	protected Bitmap doInBackground(Void... params) 
	{
		Bitmap bitmap=null;
		HttpClient client=new DefaultHttpClient();
		HttpPost post=new HttpPost("http://1.34.139.73/LocationImageHttpHandler.ashx");
		try
		{
			List<NameValuePair> nameValuePairs=new ArrayList<NameValuePair>(2);
			nameValuePairs.add(new BasicNameValuePair("Account",_account));
			post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
			
			HttpResponse response=client.execute(post);
			HttpEntity entity=response.getEntity();
			InputStream stream=entity.getContent();
			bitmap=BitmapFactory.decodeStream(stream);
		}
		catch(Exception e)
		{
    		Log.v("Exception",e.toString());
		}
		return bitmap;
	}
	
	@Override
	protected void onPostExecute(Bitmap bitmap)
	{
		FlowIconSingleton._locationBitmap=bitmap;
		new GetLocationPointTask(_context,_account).execute();
	}
}

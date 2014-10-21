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

import android.os.AsyncTask;
import android.util.Log;

public class SendRegistIdTask extends AsyncTask<String,Void,String>
{
	private SendRegistIdTaskEventRegister _event;
	
	public SendRegistIdTask(SendRegistIdTaskEventRegister event)
	{
		_event=event;
	}
	
	@Override
	protected String doInBackground(String... params) 
	{
		String httpResponse="false";
    	HttpClient client=new DefaultHttpClient();
    	HttpPost post=new HttpPost("http://1.34.139.73/DeviceIdHttpHandler.ashx");
    	try
    	{
	    	List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
	    	nameValuePairs.add(new BasicNameValuePair("RegistrationID",params[0]));
	    	nameValuePairs.add(new BasicNameValuePair("Account",params[1]));
	    	nameValuePairs.add(new BasicNameValuePair("Password",params[2]));
	    	post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
	    	HttpResponse response=client.execute(post);
	    	httpResponse=EntityUtils.toString(response.getEntity());
    	}
    	catch(Exception e)
    	{
    		Log.v("Exception",e.toString());
    	}
    	return httpResponse;
	}

    @Override
    protected void onPostExecute(String response) 
    {
    	_event.AsyncTaskFinished(response);
    }
}

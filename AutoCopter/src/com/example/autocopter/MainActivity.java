package com.example.autocopter;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.atomic.AtomicInteger;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpRequestBase;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import com.example.stable.value.ConstValue;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.gcm.GoogleCloudMessaging;

import android.support.v7.app.ActionBarActivity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Toast;


public class MainActivity extends ActionBarActivity 
{
    private String _regId;
    private Context _context;
    private GoogleCloudMessaging _gcm;
    
    @Override
    protected void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        
        _context = getApplicationContext();
    	if (CheckPlayServices()) 
    	{
    		_gcm = GoogleCloudMessaging.getInstance(this);
            _regId = GetRegistrationId(_context);

            if (_regId.isEmpty())
            {
                RegisterInBackground();
            }
        } 
    	else 
    	{
            Log.i(ConstValue.LOG_VERBOSE_GCM_TAG, "No valid Google Play Services APK found.");
        }
    }
    
    @Override
    protected void onResume()
    {
        super.onResume();
        // Check device for Play Services APK.
        CheckPlayServices();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) 
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) 
    {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private boolean CheckPlayServices() 
    {
        int resultCode = GooglePlayServicesUtil.isGooglePlayServicesAvailable(this);
        if (resultCode != ConnectionResult.SUCCESS) 
        {
            if (GooglePlayServicesUtil.isUserRecoverableError(resultCode)) 
            {
                GooglePlayServicesUtil.getErrorDialog(resultCode, this, ConstValue.PLAY_SERVICES_RESOLUTION_REQUEST).show();
            } 
            else 
            {
                Log.i(ConstValue.LOG_VERBOSE_GCM_TAG, "This device is not supported.");
                finish();
            }
            return false;
        }
        return true;
    }
    
    private void RegisterInBackground() 
    {
        new AsyncTask<Void, Void, String>() 
        {
            @Override
            protected String doInBackground(Void... params) 
            {
                try
                {
                    if (_gcm == null) 
                    {
                    	_gcm = GoogleCloudMessaging.getInstance(_context);
                    }
                    _regId = _gcm.register(ConstValue.PROJECT_NUMBER);
                    SendRegIdToServer(_regId);
                    StoreRegistrationId(_context, _regId);
                } 
                catch (IOException ex) 
                {
                	Log.v(ConstValue.LOG_VERBOSE_GCM_TAG,ex.getMessage());
                }
                return null;
            }
        }.execute(null, null, null);
    }

    private SharedPreferences GetGcmPreferences(Context context) 
    {
        return getSharedPreferences(MainActivity.class.getSimpleName(),Context.MODE_PRIVATE);
    }
    
    private String GetRegistrationId(Context context) 
    {
        final SharedPreferences prefs = GetGcmPreferences(context);
        String registrationId = prefs.getString(ConstValue.SHARE_PREFERENCES_REGISTER_ID, "");
        if (registrationId.isEmpty()) 
        {
            Log.i(ConstValue.LOG_VERBOSE_GCM_TAG, "Registration not found.");
            return "";
        }
        
        int registeredVersion = prefs.getInt(ConstValue.SHARE_PREFERENCES_APP_VERSION, Integer.MIN_VALUE);
        int currentVersion = GetAppVersion(context);
        if (registeredVersion != currentVersion) 
        {
            Log.i(ConstValue.LOG_VERBOSE_GCM_TAG, "App version changed.");
            return "";
        }
        return registrationId;
    }
    
    private void StoreRegistrationId(Context context, String regId) 
    {
        final SharedPreferences prefs = GetGcmPreferences(context);
        int appVersion = GetAppVersion(context);
        SharedPreferences.Editor editor = prefs.edit();
        editor.putString(ConstValue.SHARE_PREFERENCES_REGISTER_ID, regId);
        editor.putInt(ConstValue.SHARE_PREFERENCES_APP_VERSION, appVersion);
        editor.commit();
    }
    
    private static int GetAppVersion(Context context) 
    {
        try 
        {
            PackageInfo packageInfo = context.getPackageManager().getPackageInfo(context.getPackageName(), 0);
            return packageInfo.versionCode;
        } 
        catch (NameNotFoundException e) 
        {
            // should never happen
            throw new RuntimeException("Could not get package name: " + e);
        }
    }

    private void SendRegIdToServer(String registerationId) 
    {
    	new AsyncTask<String,Void,String>() 
    	{
    	    @Override
    	    protected String doInBackground(String... id) 
    	    {
    	    	HttpClient client=new DefaultHttpClient();
    	    	HttpPost post=new HttpPost("http://1.34.139.73/DeviceIdHttpHandler.ashx");
    	    	try
    	    	{
	    	    	List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
	    	    	nameValuePairs.add(new BasicNameValuePair("RegistrationID",id[0]));
	    	    	nameValuePairs.add(new BasicNameValuePair("Del","false"));
	    	    	post.setEntity(new UrlEncodedFormEntity(nameValuePairs));
	    	    	HttpResponse response=client.execute(post);
    	    	}
    	    	catch(Exception e)
    	    	{
    	    		Log.v("Exception",e.toString());
    	    	}
    	    	return id[0];
    	    }
    	 
    	    @Override
    	    protected void onPostExecute(String msg) 
    	    {
    	    	Log.v("asdf",msg+" is done");
    	    }
    	 }.execute(registerationId);
    }
    
    public void ClickTest(View view)
    {
    	SendRegIdToServer(_regId);
    }
}

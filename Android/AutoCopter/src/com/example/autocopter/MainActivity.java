 package com.example.autocopter;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import com.example.flowicon.TestService;
import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;
import com.google.android.gms.gcm.GoogleCloudMessaging;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;


public class MainActivity extends Activity 
{
	public static MainActivity _currentMain=null;
	
    private String _regId;
    private GoogleCloudMessaging _gcm;
	private Animation _animation;
	private ImageView _backGroundImageView;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _currentMain=this;
        /*
        startService(new Intent(this, TestService.class));
        finish();*/

		ConstValue.APPLICATION_CONTEXT = getApplicationContext();
		_gcm = GoogleCloudMessaging.getInstance(this);
        _regId = GetRegistrationId(ConstValue.APPLICATION_CONTEXT);
        
        _backGroundImageView=(ImageView)findViewById(R.id.backgroundImageView);
        _animation=AnimationUtils.loadAnimation(getApplicationContext(), R.anim.background_fade_out);
        _animation.setAnimationListener(new Animation.AnimationListener() 
        {
			@Override
			public void onAnimationStart(Animation animation)
			{
			}
			
			@Override
			public void onAnimationRepeat(Animation animation) 
			{
			}
			
			@Override
			public void onAnimationEnd(Animation animation) 
			{
		    	if (CheckPlayServices()) 
		    	{
		            if (_regId.isEmpty())
		            {
		                RegisterInBackground();
		            }
		            else
		            {
		            	SwitchPage();
		            }
		        } 
		    	else 
		    	{
		            Log.i(ConstValue.LOG_VERBOSE_GCM_TAG, "No valid Google Play Services APK found.");
		        }
			}
		});
        _backGroundImageView.startAnimation(_animation);
    }
    
    @Override
    protected void onResume()
    {
        super.onResume();
        // Check device for Play Services APK.
        CheckPlayServices();
    }
    
    private boolean CheckLoginState()
    {
    	final SharedPreferences prefs=UsualMethod.GetSharedPreferences();
    	return prefs.getBoolean(ConstValue.SHARE_PREFERENCES_LOGIN_STATE, false);
    }
    
    private boolean CheckNormal()
    {
    	final SharedPreferences prefs=UsualMethod.GetSharedPreferences();
    	return prefs.getBoolean(ConstValue.SHARE_PREFERENCES_SYSTEM_STATE, true);
    }
    
    private void SwitchPage()
    {
    	if(!CheckLoginState())
    	{
    		startActivity(new Intent(this,LoginActivity.class));
    	}
    	else
    	{
    		if(!CheckNormal())
    		{
    			
    		}
    		else
    		{
    	        startService(new Intent(this, TestService.class));
    		}
    	}
    	finish();
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
                    	_gcm = GoogleCloudMessaging.getInstance(ConstValue.APPLICATION_CONTEXT);
                    }
                    _regId = _gcm.register(ConstValue.PROJECT_NUMBER);
                    SendRegIdToServer(_regId);
                    StoreRegistrationId(ConstValue.APPLICATION_CONTEXT, _regId);
                } 
                catch (IOException ex) 
                {
                	Log.v(ConstValue.LOG_VERBOSE_GCM_TAG,ex.getMessage());
                }
                return null;
            }
            
            @Override
            protected void onPostExecute(String s)
            {
            	SwitchPage();
            }
        }.execute(null, null, null);
    }

    private String GetRegistrationId(Context context) 
    {
    	final SharedPreferences prefs=UsualMethod.GetSharedPreferences();
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
    	final SharedPreferences prefs=UsualMethod.GetSharedPreferences();
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
}

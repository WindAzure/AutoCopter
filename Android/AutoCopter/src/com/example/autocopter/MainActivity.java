 package com.example.autocopter;

import com.example.flowicon.NormalService;
import com.example.flowicon.UnNormalService;
import com.example.http.to.server.CheckServerStateTask;
import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GooglePlayServicesUtil;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;


public class MainActivity extends Activity 
{
	private Animation _animation;
	private ImageView _backGroundImageView;
	public static MainActivity _currentMain=null;
	
    @Override
    protected void onCreate(Bundle savedInstanceState) 
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        _currentMain=this;

		ConstValue.APPLICATION_CONTEXT = getApplicationContext();
        
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
	            	SwitchPage();
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
        
    private void SwitchPage()
    {
    	if(!CheckLoginState())
    	{
    		startActivity(new Intent(this,LoginActivity.class));
        	finish();
    	}
    	else
    	{
    		SharedPreferences pref=UsualMethod.GetSharedPreferences();
    		new CheckServerStateTask(MainActivity.this,pref.getString(ConstValue.SHARE_PREFERENCES_LOGIN_ACCOUNT, "")).execute();
    	}
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
}

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
import org.apache.http.util.EntityUtils;

import com.example.http.to.server.CheckServerStateTask;
import com.example.http.to.server.SendRegistIdTask;
import com.example.http.to.server.SendRegistIdTaskEventRegister;
import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;
import com.google.android.gms.gcm.GoogleCloudMessaging;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.Toast;

public class LoginActivity extends Activity 
{
    private String _regId;
    private GoogleCloudMessaging _gcm;
    private ImageView _loginImageView;
    private EditText _accountEditText;
    private EditText _passwordEditText;
    private ProgressBar _progressBar;
    public static LoginActivity _currentLogin=null;
    
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		_currentLogin=this;

		_progressBar=(ProgressBar)findViewById(R.id.loginProgressBar);
		_accountEditText=(EditText)findViewById(R.id.accountEditText);
		_passwordEditText=(EditText)findViewById(R.id.passwordEditText);
		_loginImageView=(ImageView)findViewById(R.id.loginImageView);
		_loginImageView.setOnClickListener(TwoStateImageViewClickListener);
		
		_gcm = GoogleCloudMessaging.getInstance(this);
        _regId = GetRegistrationId(ConstValue.APPLICATION_CONTEXT);
        if (_regId.isEmpty())
        {
            RegisterInBackground();
        }
	}

	public View.OnClickListener TwoStateImageViewClickListener=new View.OnClickListener() 
	{
		@Override
		public void onClick(View v) 
		{
			String account=_accountEditText.getText().toString();
			String password=_passwordEditText.getText().toString();
			if(account.length()<50 && password.length()<50 && (!account.equals("")) && (!password.equals("")))
			{
				_loginImageView.setEnabled(false);
				_accountEditText.setEnabled(false);
				_passwordEditText.setEnabled(false);
				_progressBar.setVisibility(View.VISIBLE);
				new SendRegistIdTask(_sendRegistIdTaskEventRegister).execute(_regId,account,password);
				//SendRegIdToServer(_regId,account,password);
			}
		}
	};
	
	private void RegisterInBackground() 
    {
        new AsyncTask<Void, Void, Object>() 
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
                    StoreRegistrationId(ConstValue.APPLICATION_CONTEXT, _regId);
                } 
                catch (IOException ex) 
                {
                	Log.v(ConstValue.LOG_VERBOSE_GCM_TAG,ex.getMessage());
                }
                return null;
            }
        }.execute();
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

    /*private void SendRegIdToServer(String registerationId,String account,String password) 
    {
    	Log.v("asdf",registerationId);
    	new AsyncTask<String,Void,String>() 
    	{
    	    @Override
    	    protected String doInBackground(String... data) 
    	    {
    	    	String httpResponse="false";
    	    	HttpClient client=new DefaultHttpClient();
    	    	HttpPost post=new HttpPost("http://1.34.139.73/DeviceIdHttpHandler.ashx");
    	    	try
    	    	{
	    	    	List<NameValuePair> nameValuePairs = new ArrayList<NameValuePair>(2);
	    	    	nameValuePairs.add(new BasicNameValuePair("RegistrationID",data[0]));
	    	    	nameValuePairs.add(new BasicNameValuePair("Account",data[1]));
	    	    	nameValuePairs.add(new BasicNameValuePair("Password",data[2]));
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
    	    	if(response.equals("Welcome"))
    	    	{
    	    		SharedPreferences pref=UsualMethod.GetSharedPreferences();
    	    		SharedPreferences.Editor editor=pref.edit();
    	    		editor.putBoolean(ConstValue.SHARE_PREFERENCES_LOGIN_STATE, true);
    	    		editor.commit();
    	    	}
    	    	else
    	    	{
    	    		Toast.makeText(LoginActivity.this, response, Toast.LENGTH_SHORT).show();
    	    		_accountEditText.setText("");
    	    		_passwordEditText.setText("");
    	    	}
    	    }
    	 }.execute(registerationId,account,password);
    }*/
    
    private SendRegistIdTaskEventRegister _sendRegistIdTaskEventRegister=new SendRegistIdTaskEventRegister()
    {
    	/* 照理來說應該要先等 CheckServerStateTask 結束後再開啟元件的 enable
    	 * 這邊不用的原因是反正 CheckServerStateTask 成立就會跳轉頁面
    	 * 一旦跳轉，關不關就沒差了*/
		@Override
		public void AsyncTaskFinished(String response) 
		{				
			if(response.equals("Welcome"))
	    	{
	    		SharedPreferences pref=UsualMethod.GetSharedPreferences();
	    		SharedPreferences.Editor editor=pref.edit();
	    		editor.putBoolean(ConstValue.SHARE_PREFERENCES_LOGIN_STATE, true);
	    		editor.putString(ConstValue.SHARE_PREFERENCES_LOGIN_ACCOUNT, _accountEditText.getText().toString());
	    		editor.commit();
	    		new CheckServerStateTask(LoginActivity.this,_accountEditText.getText().toString()).execute();
	    	}
	    	else
	    	{
				_loginImageView.setEnabled(true);
				_accountEditText.setEnabled(true);
				_passwordEditText.setEnabled(true);
				_progressBar.setVisibility(View.INVISIBLE);
	    		Toast.makeText(LoginActivity.this, response, Toast.LENGTH_SHORT).show();
	    		_accountEditText.setText("");
	    		_passwordEditText.setText("");
	    	}
		}
	};
}

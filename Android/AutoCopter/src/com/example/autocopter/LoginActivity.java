package com.example.autocopter;

import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;

import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;

public class LoginActivity extends Activity 
{
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
	}
	
	public void ClickTest(View view)
	{
		SharedPreferences pref=UsualMethod.GetSharedPreferences();
		SharedPreferences.Editor editor=pref.edit();
		editor.putBoolean(ConstValue.SHARE_PREFERENCES_LOGIN_STATE, true);
		editor.commit();
	}
}

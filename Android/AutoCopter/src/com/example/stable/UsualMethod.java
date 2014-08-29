package com.example.stable;

import android.content.Context;
import android.content.SharedPreferences;

public class UsualMethod 
{
	public static int ChangeDpiIntoPixel(int dpi)
	{
		return (int)(ConstValue.APPLICATION_CONTEXT.getResources().getDisplayMetrics().density*dpi+0.5f);
	}
	
	public static SharedPreferences GetSharedPreferences()
	{
		return ConstValue.APPLICATION_CONTEXT.getSharedPreferences(ConstValue.SHARE_PREFERENCES_NAME,Context.MODE_PRIVATE);
	}
}

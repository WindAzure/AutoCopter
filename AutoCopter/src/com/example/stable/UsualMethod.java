package com.example.stable;

import android.content.Context;
import android.content.SharedPreferences;
import android.view.MotionEvent;

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
	
	public static float GetTwoActionPointerDistanse(MotionEvent event)
	{
		double x=event.getX(1)-event.getX(0);
		double y=event.getY(1)-event.getY(0);
		return (float)Math.sqrt(x*x+y*y);
	}
}

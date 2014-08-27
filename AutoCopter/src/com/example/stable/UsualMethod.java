package com.example.stable;

import com.example.flowicon.FlowIconSingleton;


public class UsualMethod 
{
	public static int ChangeDpiIntoPixel(int dpi)
	{
		return (int)(FlowIconSingleton._context.getResources().getDisplayMetrics().density*dpi+0.5f);
	}
}

package com.example.stable;

import com.example.flowicon.FlowIconSingleton;
import com.example.flowicon.NormalService;
import com.example.flowicon.UnNormalService;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.telephony.TelephonyManager;
import android.util.Log;
import android.view.View;

public class CallReceiver extends BroadcastReceiver
{
	@Override
	public void onReceive(Context context, Intent intent) 
	{
		if(intent.getStringExtra(TelephonyManager.EXTRA_STATE).equals(TelephonyManager.EXTRA_STATE_IDLE) ||
		   intent.getStringExtra(TelephonyManager.EXTRA_STATE).equals(TelephonyManager.CALL_STATE_OFFHOOK))
		{
			try
			{
				Thread.sleep(3000);
			}
			catch(Exception e)
			{
				
			}
			
			if(FlowIconSingleton._fullLayout!=null)
			{
				FlowIconSingleton._fullLayout.setVisibility(View.VISIBLE);
			}
			if(FlowIconSingleton._singleLayout!=null)
			{
				FlowIconSingleton._singleLayout.setVisibility(View.VISIBLE);	
			}
		}
	}
}

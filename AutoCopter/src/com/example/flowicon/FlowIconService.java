package com.example.flowicon;

import com.example.autocopter.R;

import android.app.Notification;
import android.app.PendingIntent;
import android.app.Service;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.os.IBinder;
import android.view.Gravity;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.ImageView;

public class FlowIconService extends Service
{
	private WindowManager _windowManager;
	private WindowManager.LayoutParams _params;
	private ImageView _icon;
	
	@Override
	public IBinder onBind(Intent intent) 
	{
		return null;
	}
	
	private View.OnTouchListener aaa= new View.OnTouchListener(){
		  private int initialX;
		  private int initialY;
		  private float initialTouchX;
		  private float initialTouchY;

		  @Override 
		  public boolean onTouch(View v, MotionEvent event) {
			  stopSelf();
		   /* switch (event.getAction()) {
		      case MotionEvent.ACTION_DOWN:
		        initialX = params.x;
		        initialY = params.y;
		        initialTouchX = event.getRawX();
		        initialTouchY = event.getRawY();
		        return true;
		      case MotionEvent.ACTION_UP:
		        return true;
		      case MotionEvent.ACTION_MOVE:
		        params.x = initialX + (int) (event.getRawX() - initialTouchX);
		        params.y = initialY + (int) (event.getRawY() - initialTouchY);
		        windowManager.updateViewLayout(chatHead, params);
		        return true;
		    }*/
		    return false;
		  }
		};
	
	@Override
	public void onCreate()
	{
		super.onCreate();
		_windowManager=(WindowManager)getSystemService(WINDOW_SERVICE);
		_icon=new ImageView(this);
		_icon.setImageResource(R.drawable.home);
		_icon.setOnTouchListener(aaa);
		
		
		_params=new WindowManager.LayoutParams(
				WindowManager.LayoutParams.WRAP_CONTENT,
				WindowManager.LayoutParams.WRAP_CONTENT,
				WindowManager.LayoutParams.TYPE_PHONE,
				WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,
				PixelFormat.TRANSLUCENT);
		
		_params.gravity=Gravity.TOP|Gravity.LEFT;
		_params.x=0;
		_params.y=100;
		
		_windowManager.addView(_icon, _params);
	}
	
	@Override
	public void onDestroy()
	{
		super.onDestroy();
		if(_icon!=null)
		{
			_windowManager.removeView(_icon);
		}
	}
	
	
	
	/*
	@Override
	public int onStartCommand(Intent intent, int flags, int startId) {
		
		
		if (intent.getBooleanExtra("stop_service", false)){
			// If it's a call from the notification, stop the service.
			stopSelf();
		}else{
			// Make the service run in foreground so that the system does not shut it down.
			Intent notificationIntent = new Intent(this, FlowIconService.class);
			notificationIntent.putExtra("stop_service", true);
			PendingIntent pendingIntent = PendingIntent.getService(this, 0, notificationIntent, 0);
			Notification notification = new Notification(
					R.drawable.ic_launcher, 
					"Spotify tray launched",
			        System.currentTimeMillis());
			notification.setLatestEventInfo(
					this, 
					"Spotify tray",
			        "Tap to close the widget.", 
			        pendingIntent);
			startForeground(86, notification);
		}
		return START_STICKY;
	}*/
}

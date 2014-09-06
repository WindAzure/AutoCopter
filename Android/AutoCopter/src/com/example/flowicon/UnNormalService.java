package com.example.flowicon;

import com.example.autocopter.R;
import com.example.stable.MapImageView;
import com.example.stable.UsualMethod;

import android.app.Service;
import android.content.Intent;
import android.graphics.PixelFormat;
import android.net.Uri;
import android.os.Handler;
import android.os.IBinder;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

public class UnNormalService extends Service
{
	public static UnNormalService _currentService=null; 
	
	private int _currentX=0;
	private int _currentY=0;
	private Handler _notifyHandler=new Handler();
	private boolean _isFullPageZoomOuted=true;
	private boolean _canMove=false;
	private boolean _isClickActive=false;
	
	private SingleIconTranslateAnimation _singleIconTranslateAnimation;
	private MainPanelScaleAnimation _mainPanelScaleAnimation;
	private CloseIconTranslateAnimation _closeIconTranslateAnimation;
	
	private ImageView _callImageView;
	private MapImageView _mapImageView;
	
	private SingleIconEventRegister _translateRegister=new SingleIconEventRegister()
	{
		@Override
		public void OnTranslateZeroEnd() 
		{
			FlowIconSingleton._fullLayout.setVisibility(View.VISIBLE);
			_closeIconTranslateAnimation.InitializeTimerAndTask();
			_closeIconTranslateAnimation.TranslateToOrderPosition();
		}

		@Override
		public void OnTranslateBeforeEnd() 
		{
			_canMove=true;
		}
	};
	
	
	private CloseIconEventRegister _translate2Register=new CloseIconEventRegister()
	{
		@Override
		public void OnTranslateZeroEnd() 
		{
			FlowIconSingleton._fullLayout.setVisibility(View.INVISIBLE);
			_singleIconTranslateAnimation.InitializeTimerAndTask();
			_singleIconTranslateAnimation.TranslateToBefore(_currentX, _currentY);
		}

		@Override
		public void OnTranslateOrderPositionEnd() 
		{
			_mainPanelScaleAnimation.InitializeTimerAndTask();
			_mainPanelScaleAnimation.ZoomOut();
		}
	};
	
	
	private MainPanelEventRegister _scaleRegister=new MainPanelEventRegister()
	{
		@Override
		public void OnZoomInEnd() 
		{
			_closeIconTranslateAnimation.InitializeTimerAndTask();
			_closeIconTranslateAnimation.TranslateToZero();
		}

		@Override
		public void OnZoomOutEnd() 
		{
			//_canMove=false;
		}
	};
	
	
	@Override
	public IBinder onBind(Intent intent) 
	{
		return null;
	}
	
	private void CreateSingleLayoutParams()
	{
		FlowIconSingleton._singlePageParams=new WindowManager.LayoutParams(
				WindowManager.LayoutParams.WRAP_CONTENT,
				WindowManager.LayoutParams.WRAP_CONTENT,
				WindowManager.LayoutParams.TYPE_PHONE,
				WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,
				PixelFormat.TRANSLUCENT);
		
		FlowIconSingleton._singlePageParams.gravity=Gravity.TOP|Gravity.LEFT;
		FlowIconSingleton._singlePageParams.x=UsualMethod.ChangeDpiIntoPixel(10);
		FlowIconSingleton._singlePageParams.y=UsualMethod.ChangeDpiIntoPixel(10);
		_currentX=FlowIconSingleton._singlePageParams.x;
		_currentY=FlowIconSingleton._singlePageParams.y;
	}
	
	private void CreateFullLayoutParams()
	{
		FlowIconSingleton._fullPageParams=new WindowManager.LayoutParams(
				WindowManager.LayoutParams.MATCH_PARENT,
				WindowManager.LayoutParams.MATCH_PARENT,
				WindowManager.LayoutParams.TYPE_PHONE,
				WindowManager.LayoutParams.FLAG_NOT_FOCUSABLE,
				PixelFormat.TRANSLUCENT);
		
		FlowIconSingleton._fullPageParams.gravity=Gravity.TOP|Gravity.LEFT;
		FlowIconSingleton._fullPageParams.x=0;
		FlowIconSingleton._fullPageParams.y=0;
	}
	
	private void InitializeAllAnimation()
	{
		_singleIconTranslateAnimation.InitializeTimerAndTask();
		_closeIconTranslateAnimation.InitializeTimerAndTask();
		_mainPanelScaleAnimation.InitializeTimerAndTask();
	}
	
	public View.OnTouchListener homeIconTouchListener=new View.OnTouchListener()
	{	
		private int startX;
		private int startY;
		private float startRawX;
		private float startRawY;
		
		@Override
		public boolean onTouch(View v, MotionEvent event) 
		{
			if(_canMove)
			{
				if(event.getAction()==MotionEvent.ACTION_DOWN)
				{
					startX=FlowIconSingleton._singlePageParams.x;
					startY=FlowIconSingleton._singlePageParams.y;
					startRawX=event.getRawX();
					startRawY=event.getRawY();
					_isClickActive=true;
				}
				else if(event.getAction()==MotionEvent.ACTION_MOVE)
				{
					if(Math.abs(event.getRawX()-startRawX)>5 && Math.abs(event.getRawY()-startRawY)>5)
					{
						_isClickActive=false;
						int newX=(int)(startX+(event.getRawX()-startRawX));
						int newY=(int)(startY+(event.getRawY()-startRawY));
						FlowIconSingleton._singlePageParams.x=newX;
						FlowIconSingleton._singlePageParams.y=newY;
						_currentX=FlowIconSingleton._singlePageParams.x;
						_currentY=FlowIconSingleton._singlePageParams.y;
						FlowIconSingleton._windowManager.updateViewLayout(FlowIconSingleton._singleLayout, FlowIconSingleton._singlePageParams);
					}
				}
			}
			return false;
		}
	};
	
	public View.OnClickListener homeIconClickListener=new View.OnClickListener() 
	{
		@Override
		public void onClick(View v) 
		{
			if(_isFullPageZoomOuted)
			{
				_mainPanelScaleAnimation.InitializeTimerAndTask();
				_mainPanelScaleAnimation.ZoomIn();
				_isFullPageZoomOuted=false;
			}
			else
			{
				if(_isClickActive)
				{
					_canMove=false;
					_singleIconTranslateAnimation.InitializeTimerAndTask();
					_singleIconTranslateAnimation.TranslateToZero();
					_isFullPageZoomOuted=true;
				}
			}
		}
	};

	public View.OnClickListener TwoStateImageViewClickListener=new View.OnClickListener() 
	{
		@Override
		public void onClick(View v) 
		{
			Intent dial=new Intent();
			dial.setAction("android.intent.action.CALL");
			dial.setData(Uri.parse("tel:0977354265"));
			dial.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
			dial.addFlags(Intent.FLAG_FROM_BACKGROUND);
			UnNormalService.this.startActivities(new Intent[]{dial});
			stopSelf();
		}
	};
	
	public View.OnClickListener closeIconClickListener=new View.OnClickListener() 
	{	
		@Override
		public void onClick(View v) 
		{
			stopSelf();
		}
	};
	
	@Override
	public void onCreate()
	{
		super.onCreate();
		_currentService=this;
		
		FlowIconSingleton._windowManager=(WindowManager)getSystemService(WINDOW_SERVICE);
		
		CreateSingleLayoutParams();
		CreateFullLayoutParams();

		FlowIconSingleton._singleLayout=(FrameLayout)LayoutInflater.from(this).inflate(R.layout.un_normal_layout1, null);
		FlowIconSingleton._fullLayout=(RelativeLayout)LayoutInflater.from(this).inflate(R.layout.un_normal_layout2, null);

		FlowIconSingleton._closeIcon=(ImageView)FlowIconSingleton._fullLayout.findViewById(R.id.closeIcon);
		FlowIconSingleton._mainPanel=(LinearLayout)FlowIconSingleton._fullLayout.findViewById(R.id.mainPanel);
		FlowIconSingleton._mainPanel.setVisibility(View.INVISIBLE);
		
		_callImageView=(ImageView)FlowIconSingleton._fullLayout.findViewById(R.id.callImageView);
		_callImageView.setOnClickListener(TwoStateImageViewClickListener);
		
		_mapImageView=(MapImageView)FlowIconSingleton._fullLayout.findViewById(R.id.mapImageView);
		_mapImageView.setImageBitmap(FlowIconSingleton._locationBitmap);
		
		FlowIconSingleton._singleLayout.setOnTouchListener(homeIconTouchListener);
		FlowIconSingleton._singleLayout.setOnClickListener(homeIconClickListener);
		FlowIconSingleton._closeIcon.setOnClickListener(closeIconClickListener);
		
		FlowIconSingleton._windowManager.addView(FlowIconSingleton._fullLayout, FlowIconSingleton._fullPageParams);
		FlowIconSingleton._windowManager.addView(FlowIconSingleton._singleLayout, FlowIconSingleton._singlePageParams);

		_singleIconTranslateAnimation=new SingleIconTranslateAnimation(_translateRegister);
		_mainPanelScaleAnimation=new MainPanelScaleAnimation(_scaleRegister);
		_closeIconTranslateAnimation=new CloseIconTranslateAnimation(_translate2Register);
		
		_notifyHandler.post(new Runnable()
		{
			@Override
			public void run() 
			{
				_closeIconTranslateAnimation.InitializePosition();
				_closeIconTranslateAnimation.TranslateToOrderPosition();
				_mapImageView.Initialize(FlowIconSingleton._locationBitmap);
			}
		});
	}
	
    @Override
	public int onStartCommand(Intent intent, int flags, int startId) 
    {
		return START_NOT_STICKY;
    }
	
	@Override
	public void onDestroy()
	{
		super.onDestroy();
		
		if(FlowIconSingleton._singleLayout!=null)
		{
			FlowIconSingleton._windowManager.removeView(FlowIconSingleton._singleLayout);
		}
		if(FlowIconSingleton._fullLayout!=null)
		{
			FlowIconSingleton._windowManager.removeView(FlowIconSingleton._fullLayout);
		}
	}
}

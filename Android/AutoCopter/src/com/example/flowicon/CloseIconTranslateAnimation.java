package com.example.flowicon;

import java.util.Timer;
import java.util.TimerTask;

import com.example.autocopter.R;
import com.example.flowicon.SingleIconTranslateAnimation.AnimationTask;
import com.example.stable.UsualMethod;

import android.os.Handler;
import android.util.Log;
import android.view.ViewGroup.LayoutParams;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class CloseIconTranslateAnimation 
{
	private Timer _animationTimer=null;
	private AnimationTask _animationTask=null;
	private CloseIconEventRegister _event;
	private Handler _notifyHandler;
	private int _zeroPoint;
	private int _goalPoint;
	
	public CloseIconTranslateAnimation(CloseIconEventRegister event)
	{
		_event=event;
		_notifyHandler=new Handler();
	}
	
	public void InitializePosition()
	{
		_zeroPoint=((ImageView)FlowIconSingleton._fullLayout.findViewById(R.id.homeIcon)).getLeft();
		_goalPoint=FlowIconSingleton._closeIcon.getLeft();

		RelativeLayout.LayoutParams params=new RelativeLayout.LayoutParams(LayoutParams.WRAP_CONTENT,LayoutParams.WRAP_CONTENT);
		params.setMargins(_zeroPoint, UsualMethod.ChangeDpiIntoPixel(10), 0, 0);
		FlowIconSingleton._closeIcon.setLayoutParams(params);
		FlowIconSingleton._windowManager.updateViewLayout(FlowIconSingleton._fullLayout, FlowIconSingleton._fullPageParams);
	}
	
	public class AnimationTask extends TimerTask
	{
		private int _destX;
		private boolean _flag;
		
		public AnimationTask(int destX,boolean flag)
		{
			_destX=destX;
			_flag=flag;
		}
		
		@Override
		public void run() 
		{
			_notifyHandler.post(new Runnable()
			{
				@Override
				public void run() 
				{ 
					RelativeLayout.LayoutParams params=new RelativeLayout.LayoutParams(LayoutParams.WRAP_CONTENT,LayoutParams.WRAP_CONTENT);
					params.setMargins(2*(FlowIconSingleton._closeIcon.getLeft()-_destX)/3+_destX, UsualMethod.ChangeDpiIntoPixel(10), 0, 0);
					FlowIconSingleton._closeIcon.setLayoutParams(params);
					FlowIconSingleton._windowManager.updateViewLayout(FlowIconSingleton._fullLayout, FlowIconSingleton._fullPageParams);
					
					if(Math.abs(FlowIconSingleton._closeIcon.getLeft()-_destX)<=2)
					{
						AnimationTask.this.cancel();
						_animationTimer.cancel();
						
						if(_flag)
						{
							_event.OnTranslateZeroEnd();
						}
						else
						{
							_event.OnTranslateOrderPositionEnd();
						}
					}
				}	
			});
		}
	}
	
	public void InitializeTimerAndTask()
	{
		if(_animationTask!=null)
		{
			_animationTask.cancel();
		}
		if(_animationTimer!=null)
		{
			_animationTimer.cancel();
		}
		_animationTask=null;
		_animationTimer=null;
	}
	
	public void TranslateToZero()
	{
		InitializeTimerAndTask();
		_animationTask=new AnimationTask(_zeroPoint,true);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
	
	public void TranslateToOrderPosition()
	{
		InitializeTimerAndTask();
		_animationTask=new AnimationTask(_goalPoint,false);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
}

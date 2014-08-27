package com.example.flowicon;

import java.util.Timer;
import java.util.TimerTask;

import com.example.stable.UsualMethod;

import android.os.Handler;
import android.util.Log;

public class SingleIconTranslateAnimation 
{	
	private Timer _animationTimer=null;
	private AnimationTask _animationTask=null;
	private SingleIconEventRegister _event;
	private Handler _notifyHandler;
	
	public SingleIconTranslateAnimation(SingleIconEventRegister event)
	{
		_event=event;
		_notifyHandler=new Handler();
	}
	
	public class AnimationTask extends TimerTask
	{
		private float _destX;
		private float _destY;
		private boolean _flag;
		
		public AnimationTask(float destX,float destY,boolean flag)
		{
			_destX=destX;
			_destY=destY;
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
					//Log.v("asdf","destX="+Float.toString(_destX)+" destY="+Float.toString(_destY)+" X="+Float.toString(FlowIconSingleton._singlePageParams.x)+" Y="+Float.toString(FlowIconSingleton._singlePageParams.y));
					FlowIconSingleton._singlePageParams.x = (int)((2*(FlowIconSingleton._singlePageParams.x-_destX))/3 + _destX);
					FlowIconSingleton._singlePageParams.y = (int)((2*(FlowIconSingleton._singlePageParams.y-_destY))/3 + _destY);
					FlowIconSingleton._windowManager.updateViewLayout(FlowIconSingleton._singleLayout, FlowIconSingleton._singlePageParams);
					
					if (Math.abs(FlowIconSingleton._singlePageParams.x-_destX)<=2 && Math.abs(FlowIconSingleton._singlePageParams.y-_destY)<=2)
					{
						AnimationTask.this.cancel();
						_animationTimer.cancel();
						
						if(_flag)
						{
							_event.OnTranslateZeroEnd();
						}
						else
						{
							_event.OnTranslateBeforeEnd();
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
		_animationTask=new AnimationTask(UsualMethod.ChangeDpiIntoPixel(10),UsualMethod.ChangeDpiIntoPixel(10),true);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
	
	public void TranslateToBefore(float x,float y)
	{
		InitializeTimerAndTask();
		_animationTask=new AnimationTask(x,y,false);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
}

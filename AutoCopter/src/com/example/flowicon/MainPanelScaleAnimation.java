package com.example.flowicon;

import java.util.Timer;
import java.util.TimerTask;

import com.example.autocopter.R;
import com.example.http.to.server.MainPanelEventRegister;
import com.example.stable.ConstValue;
import com.example.stable.UsualMethod;

import android.os.Handler;
import android.util.Log;
import android.view.animation.AccelerateInterpolator;
import android.view.animation.Animation;
import android.view.animation.ScaleAnimation;
import android.widget.ImageView;

public class MainPanelScaleAnimation
{
	private Timer _animationTimer=null;
	private AnimationTask _animationTask=null;
	private MainPanelEventRegister _event;
	private Handler _notifyHandler;
	private float _zoomRate=ConstValue.ZOOM_ANIMATION_LOWER_BOUND;
	private float _alphaRate=ConstValue.ALPHA_ANIMATION_LOWER_BOUND;
	
	public MainPanelScaleAnimation(MainPanelEventRegister event)
	{
		_event=event;
		_notifyHandler=new Handler();
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
	
	public void ZoomOut()
	{
		InitializeTimerAndTask();
		_animationTask=new AnimationTask(false);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
	
	public void ZoomIn()
	{
		InitializeTimerAndTask();
		_animationTask=new AnimationTask(true);
		_animationTimer=new Timer();
		_animationTimer.schedule(_animationTask, 0,30);
	}
	
	public class PlayAnimationRunnable implements Runnable
	{
		private float _zRate;
		private float _aRate;
		
		@Override
		public void run() 
		{
			ImageView homeIcon=(ImageView)FlowIconSingleton._fullLayout.findViewById(R.id.homeIcon);
			Log.v("asdf",Float.toString(_zRate));
			float value=(float)(_zRate)/ConstValue.ZOOM_ANIMATION_UPPER_BOUND;
			Animation animation=new ScaleAnimation(value,value,value,value,homeIcon.getWidth()/2-UsualMethod.ChangeDpiIntoPixel(10),0);
			animation.setInterpolator(new AccelerateInterpolator());
			animation.setFillAfter(true);
			FlowIconSingleton._mainPanel.startAnimation(animation);
			FlowIconSingleton._fullLayout.getBackground().setAlpha((int)_aRate);
		}
		
		public PlayAnimationRunnable(float zoomRate,float alphaRate)
		{
			_zRate=zoomRate;
			_aRate=alphaRate;
		}
	}
	
	public class AnimationTask extends TimerTask
	{
		private boolean _flag; 
		
		public AnimationTask(boolean flag)
		{
			_flag=flag;
		}
		
		@Override
		public void run() 
		{
			if(_flag)
			{
				_alphaRate=_alphaRate-Math.abs(_alphaRate-ConstValue.ALPHA_ANIMATION_LOWER_BOUND)/3;
				_zoomRate=_zoomRate-Math.abs(_zoomRate-ConstValue.ZOOM_ANIMATION_LOWER_BOUND)/3;
				_zoomRate=Math.max(_zoomRate, ConstValue.ZOOM_ANIMATION_LOWER_BOUND);
				_alphaRate=Math.max(_alphaRate, ConstValue.ALPHA_ANIMATION_LOWER_BOUND);
				_notifyHandler.post(new PlayAnimationRunnable(_zoomRate,_alphaRate));
				if(Math.abs(_zoomRate-ConstValue.ZOOM_ANIMATION_LOWER_BOUND)<=ConstValue.ZOOM_ANIMATION_DIFFERENCE)
				{
					_zoomRate=ConstValue.ZOOM_ANIMATION_LOWER_BOUND;
					_alphaRate=ConstValue.ALPHA_ANIMATION_LOWER_BOUND;
					_notifyHandler.post(new PlayAnimationRunnable(_zoomRate,_alphaRate));
					AnimationTask.this.cancel();
					_animationTimer.cancel();
					_event.OnZoomInEnd();
				}
			}
			else
			{
				_alphaRate=_alphaRate+Math.abs(_alphaRate-ConstValue.ALPHA_ANIMATION_UPPER_BOUND)/3;
				_zoomRate=_zoomRate+Math.abs(_zoomRate-ConstValue.ZOOM_ANIMATION_UPPER_BOUND)/3;
				_zoomRate=Math.min(_zoomRate, ConstValue.ZOOM_ANIMATION_UPPER_BOUND);
				_alphaRate=Math.min(_alphaRate, ConstValue.ALPHA_ANIMATION_UPPER_BOUND);
				_notifyHandler.post(new PlayAnimationRunnable(_zoomRate,_alphaRate));
				if(Math.abs(_zoomRate-ConstValue.ZOOM_ANIMATION_UPPER_BOUND)<=ConstValue.ZOOM_ANIMATION_DIFFERENCE)
				{
					_zoomRate=ConstValue.ZOOM_ANIMATION_UPPER_BOUND;
					_alphaRate=ConstValue.ALPHA_ANIMATION_UPPER_BOUND;
					_notifyHandler.post(new PlayAnimationRunnable(_zoomRate,_alphaRate));
					AnimationTask.this.cancel();
					_animationTimer.cancel();
					_event.OnZoomOutEnd();
				}
			}
		}
	}
}
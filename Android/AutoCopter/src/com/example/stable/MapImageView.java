package com.example.stable;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Matrix;
import android.graphics.PointF;
import android.graphics.Rect;
import android.graphics.RectF;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

public class MapImageView extends ImageView
{
	private enum MapMode{NONE,DRAG,ZOOM};
	Matrix _matrix=new Matrix();
	Matrix _lastStateMatrix=new Matrix();
	private Bitmap _map;
	private MapMode _mode=MapMode.NONE;
	private float _startZoomRate;
	private float _width;
	private float _height;
	
	public View.OnTouchListener mapImageViewTouchListener=new View.OnTouchListener()
	{
		PointF movePoint=new PointF();
		PointF zoomPoint=new PointF();
		
		@Override
		public boolean onTouch(View v, MotionEvent event) 
		{
			int action=event.getAction() & MotionEvent.ACTION_MASK;
			if(action==MotionEvent.ACTION_DOWN)
			{
				_mode=MapMode.DRAG;
				_lastStateMatrix.set(_matrix);
				movePoint.set(event.getX(), event.getY());
			}
			else if(action==MotionEvent.ACTION_POINTER_DOWN && event.getPointerCount()==2)
			{
				_startZoomRate=UsualMethod.GetTwoActionPointerDistanse(event);
				if(_startZoomRate>10f)
				{
					_mode=MapMode.ZOOM;
					_lastStateMatrix.set(_matrix);
					zoomPoint.set((event.getX(0)+event.getX(1))/2,(event.getY(0)+event.getY(1))/2);
				}
			}
			else if(action==MotionEvent.ACTION_MOVE)
			{
				if(_mode==MapMode.DRAG)
				{
					_matrix.set(_lastStateMatrix);
					_matrix.postTranslate(event.getX()-movePoint.x, event.getY()-movePoint.y);
				}
				else if(_mode==MapMode.ZOOM)
				{
					float currentZoomRate=UsualMethod.GetTwoActionPointerDistanse(event);
					if(currentZoomRate>10f)
					{
						_matrix.set(_lastStateMatrix);
						float zoomRate=currentZoomRate/_startZoomRate;
						_matrix.postScale(zoomRate, zoomRate,zoomPoint.x,zoomPoint.y);
						CheckAndSetImageZoomMatrix(zoomPoint.x,zoomPoint.y);
					}
				}
			}
			else if(action==MotionEvent.ACTION_UP || action==MotionEvent.ACTION_POINTER_UP)
			{
				_mode=MapMode.NONE;
			}
			MapImageView.this.setImageMatrix(_matrix);
			return true;
		}
	};
	
	
	public View.OnClickListener mapImageViewClickListener=new View.OnClickListener() 
	{	
		@Override
		public void onClick(View v) 
		{
			_matrix=new Matrix();
			_lastStateMatrix=new Matrix();
			float scaleWidth=_width/_map.getWidth();
			float scaleHeight=_height/_map.getHeight();
			_matrix.postScale(scaleWidth, scaleHeight);
			MapImageView.this.setImageMatrix(_matrix);
		}
	};
	
	public void CheckAndSetImageZoomMatrix(float x,float y)
	{
		Matrix m=new Matrix();
		m.set(_matrix);
		RectF rect=new RectF(0,0,_map.getWidth(),_map.getHeight());
		m.mapRect(rect);
		if(rect.width()<_width && rect.height()<_height)
		{
			float scaleWidth=_width/rect.width();
			float scaleHeight=_height/rect.height();
			_matrix.postScale(scaleWidth, scaleHeight,x,y);
		}
	}
	
	public void Iniatilize(Bitmap map)
	{
		_map=map;
		_width=MapImageView.this.getWidth();
		_height=MapImageView.this.getHeight();
		float scaleWidth=_width/_map.getWidth();
		float scaleHeight=_height/_map.getHeight();
		_matrix.postScale(scaleWidth, scaleHeight);
		this.setImageMatrix(_matrix);
	}
		

	public MapImageView(Context context, AttributeSet attrs, int defStyle) 
	{
		super(context, attrs, defStyle);
		this.setOnTouchListener(mapImageViewTouchListener);
		this.setOnClickListener(mapImageViewClickListener);
	}
	
	public MapImageView(Context context)
	{
		super(context);
		this.setOnTouchListener(mapImageViewTouchListener);
		this.setOnClickListener(mapImageViewClickListener);
	}
	
	public MapImageView(Context context, AttributeSet attrs) 
	{
		super(context, attrs);
		this.setOnTouchListener(mapImageViewTouchListener);
		this.setOnClickListener(mapImageViewClickListener);
	}
}

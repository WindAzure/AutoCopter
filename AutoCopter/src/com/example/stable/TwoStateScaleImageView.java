package com.example.stable;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

public class TwoStateScaleImageView extends ImageView
{

	public TwoStateScaleImageView(Context context)
	{
		super(context);
	}
	
	public TwoStateScaleImageView(Context context, AttributeSet attrs) 
	{
		super(context, attrs);
		
		this.setOnTouchListener(new View.OnTouchListener() 
		{	
			@Override
			public boolean onTouch(View v, MotionEvent event) 
			{
				if(event.getAction()==MotionEvent.ACTION_DOWN)
				{
					TwoStateScaleImageView.this.setScaleX(0.9f);
					TwoStateScaleImageView.this.setScaleY(0.9f);
				}
				else if(event.getAction()==MotionEvent.ACTION_UP)
				{
					TwoStateScaleImageView.this.setScaleX(1f);
					TwoStateScaleImageView.this.setScaleY(1f);
				}
				return false;
			}
		});
	}
}

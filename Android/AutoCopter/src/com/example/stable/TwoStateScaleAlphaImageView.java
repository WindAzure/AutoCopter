package com.example.stable;

import android.content.Context;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

public class TwoStateScaleAlphaImageView extends ImageView
{
	public TwoStateScaleAlphaImageView(Context context)
	{
		super(context);
	}
	
	public TwoStateScaleAlphaImageView(Context context, AttributeSet attrs) 
	{
		super(context, attrs);
		
		this.setOnTouchListener(new View.OnTouchListener() 
		{	
			@Override
			public boolean onTouch(View v, MotionEvent event) 
			{
				if(event.getAction()==MotionEvent.ACTION_DOWN)
				{
					TwoStateScaleAlphaImageView.this.setAlpha(0.7f);
					TwoStateScaleAlphaImageView.this.setScaleX(0.9f);
					TwoStateScaleAlphaImageView.this.setScaleY(0.9f);
				}
				else if(event.getAction()==MotionEvent.ACTION_UP)
				{
					TwoStateScaleAlphaImageView.this.setAlpha(1f);
					TwoStateScaleAlphaImageView.this.setScaleX(1f);
					TwoStateScaleAlphaImageView.this.setScaleY(1f);
				}
				return false;
			}
		});
	}
}

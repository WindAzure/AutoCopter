package com.example.flowicon;

import android.content.Context;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.RelativeLayout;

public class FlowIconSingleton 
{
	public static Context _context;
	public static FrameLayout _singleLayout;
	public static RelativeLayout _fullLayout;
	public static WindowManager _windowManager;
	public static WindowManager.LayoutParams _fullPageParams;
	public static WindowManager.LayoutParams _singlePageParams;
	public static ImageView _closeIcon;
	public static ImageView _mainPanel;
	
}

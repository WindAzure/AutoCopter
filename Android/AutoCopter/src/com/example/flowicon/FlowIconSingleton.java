package com.example.flowicon;

import android.graphics.Bitmap;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;

public class FlowIconSingleton 
{
	public static FrameLayout _singleLayout=null;
	public static RelativeLayout _fullLayout=null;
	public static WindowManager _windowManager=null;
	public static WindowManager.LayoutParams _fullPageParams=null;
	public static WindowManager.LayoutParams _singlePageParams=null;
	public static ImageView _closeIcon=null;
	public static LinearLayout _mainPanel=null;
	public static Bitmap _locationBitmap=null;
	public static String _locationText="";
	public static String _locationPoint="";
}

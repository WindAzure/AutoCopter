package com.example.stable;

import android.content.Context;
import android.widget.Toast;

public class ToastRunnable implements Runnable
{
	private Context _context;
	private String _content;
	
	public ToastRunnable(Context context,String content)
	{
		_context=context;
		_content=content;
	}
	
	@Override
	public void run() 
	{
    	Toast.makeText(_context, _content, Toast.LENGTH_LONG).show();
	}
}
package com.example.autocopter;

import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import com.example.flowicon.FlowIconSingleton;
import com.example.flowicon.NormalService;
import com.example.flowicon.UnNormalService;
import com.example.http.to.server.CheckServerStateTask;
import com.example.http.to.server.GetLocationImageTask;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;

public class MainActivityaaaaa extends Activity 
{
	private ImageView _testImageView;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) 
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main_activityaaaaa);
		_testImageView=(ImageView)findViewById(R.id.imageView1);
	}
	
	public void ClickTest(View view)
	{
		//HttpToServer sever=new HttpToServer();
		//sever.CheckSeverState("Azure", _testImageView);
		//new GetLocationImageTask(_getLocationImageEventRegister).execute("Azure");
		new CheckServerStateTask(MainActivityaaaaa.this,"Azure").execute();
	}
}

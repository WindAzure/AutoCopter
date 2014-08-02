package com.example.autocopter;

import android.support.v7.app.ActionBarActivity;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore.Audio;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.RemoteViews;


public class MainActivity extends ActionBarActivity {
	//BaseNotification  
    private Button bt01;  
      
    //UpdateBaseNotification  
    private Button bt02;  
      
    //ClearBaseNotification  
    private Button bt03;  
      
    //MediaNotification  
    private Button bt04;  
      
    //ClearMediaNotification  
    private Button bt05;  
      
    //ClearALL  
    private Button bt06;  
      
    //CustomNotification  
    private Button bt07;  
      
    //通知管理器  
    private NotificationManager nm;  
      
    //通知?示?容  
    private PendingIntent pd;  
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        init();  
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
    
OnClickListener onclick = new OnClickListener() {  
        
        //BASE Notification ID  
        private int Notification_ID_BASE = 110;  
          
        private Notification baseNF;  
          
        //Notification ID  
        private int Notification_ID_MEDIA = 119;  
          
        private Notification mediaNF;  
          
        public void onClick(View v) {  
            switch(v.getId()) {  
                case R.id.le10bt01:  
                    //新建???通知  
                    baseNF = new Notification();  
                       
                    //?置通知在????示的??  
                    baseNF.icon = R.drawable.ic_launcher;  
                      
                    //通知?在????示的?容  
                    baseNF.tickerText = "You clicked BaseNF!";  
                      
                    //通知的默??? DEFAULT_SOUND, DEFAULT_VIBRATE, DEFAULT_LIGHTS.   
                    //如果要全部采用默?值, 用 DEFAULT_ALL.  
                    //此?采用默??音  
                    baseNF.defaults |= Notification.DEFAULT_SOUND;  
                    baseNF.defaults |= Notification.DEFAULT_VIBRATE;  
                    baseNF.defaults |= Notification.DEFAULT_LIGHTS;  
                      
                    //??音、振??限循?，直到用???  
                    baseNF.flags |= Notification.FLAG_INSISTENT;  
                      
                    //通知被??后，自?消失  
                    baseNF.flags |= Notification.FLAG_AUTO_CANCEL;  
                      
                    //??'Clear'?，不清楚?通知(QQ的通知?法清除，就是用的??)  
                    baseNF.flags |= Notification.FLAG_NO_CLEAR;  
                      
                      
                    //第二??? ：下拉?????示的消息?? expanded message title  
                    //第三???：下拉?????示的消息?容 expanded message text  
                    //第四???：???通知??行?面跳?  
                    baseNF.setLatestEventInfo(MainActivity.this, "Title01", "Content01", pd);  
                      
                    //?出???通知  
                    //The first parameter is the unique ID for the Notification   
                    // and the second is the Notification object.  
                    nm.notify(Notification_ID_BASE, baseNF);  
                      
                    break;  
                      
                case R.id.le10bt02:  
                    //更新通知  
                    //比如???提示有一?新短信，???得及查看，又?一?新短信的提示。  
                    //此?采用更新原?通知的方式比?。  
                    //(再重新?一?通知也可以，但是???造成通知的混?，而且?示多?通知?用?，?用?也不友好)  
                    baseNF.setLatestEventInfo(MainActivity.this, "Title02", "Content02", pd);  
                    nm.notify(Notification_ID_BASE, baseNF);  
                    break;  
                      
                case R.id.le10bt03:  
                      
                    //清除 baseNF  
                    nm.cancel(Notification_ID_BASE);  
                    break;  
                      
                case R.id.le10bt04:  
                    mediaNF = new Notification();  
                    mediaNF.icon = R.drawable.ic_launcher;  
                    mediaNF.tickerText = "You clicked MediaNF!";  
                      
                    //自定??音  
                    mediaNF.sound = Uri.withAppendedPath(Audio.Media.INTERNAL_CONTENT_URI, "6");  
                      
                    //通知??出的振?  
                    //第一???: 振?前等待的??  
                    //第二???： 第一次振?的??、以此?推  
                    long[] vir = {0,100,200,300};  
                    mediaNF.vibrate = vir;  
                      
                    mediaNF.setLatestEventInfo(MainActivity.this, "Title03", "Content03", pd);  
                      
                    nm.notify(Notification_ID_MEDIA, mediaNF);  
                    break;  
                      
                case R.id.le10bt05:  
                    //清除 mediaNF  
                    nm.cancel(Notification_ID_MEDIA);  
                    break;  
                      
                case R.id.le10bt06:  
                    nm.cancelAll();  
                    break;  
                      
                case R.id.le10bt07:  
                    //自定?下拉??，比如下??件?，?示的?度?。  
                    Notification notification = new Notification();  
                      
                    notification.icon = R.drawable.ic_launcher;  
                    notification.tickerText = "Custom!";  
                      
                    RemoteViews contentView = new RemoteViews(getPackageName(), R.layout.aaa);  
                    contentView.setImageViewResource(R.id.image, R.drawable.ic_launcher);  
                    contentView.setTextViewText(R.id.text, "Hello, this message is in a custom expanded view");  
                    notification.contentView = contentView;  
                      
                    //使用自定?下拉???，不需要再?用setLatestEventInfo()方法  
                    //但是必?定? contentIntent  
                    notification.contentIntent = pd;  
                      
                    nm.notify(3, notification);  
                    break;  
            }  
        }  
    };  
    
    private void init() {  
        bt01 = (Button)findViewById(R.id.le10bt01);  
        bt02 = (Button)findViewById(R.id.le10bt02);  
        bt03 = (Button)findViewById(R.id.le10bt03);  
        bt04 = (Button)findViewById(R.id.le10bt04);  
        bt05 = (Button)findViewById(R.id.le10bt05);  
        bt06 = (Button)findViewById(R.id.le10bt06);  
        bt07 = (Button)findViewById(R.id.le10bt07);  
          
        bt01.setOnClickListener(onclick);  
        bt02.setOnClickListener(onclick);  
        bt03.setOnClickListener(onclick);  
        bt04.setOnClickListener(onclick);  
        bt05.setOnClickListener(onclick);  
        bt06.setOnClickListener(onclick);     
        bt07.setOnClickListener(onclick);  
          
        nm = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);  
          
        Intent intent = new Intent(this,MainActivity.class);  
          
        pd = PendingIntent.getActivity(MainActivity.this, 0, intent, 0);  
    }  
}

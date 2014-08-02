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
      
    //�q���޲z��  
    private NotificationManager nm;  
      
    //�q��?��?�e  
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
                    //�s��???�q��  
                    baseNF = new Notification();  
                       
                    //?�m�q���b????�ܪ�??  
                    baseNF.icon = R.drawable.ic_launcher;  
                      
                    //�q��?�b????�ܪ�?�e  
                    baseNF.tickerText = "You clicked BaseNF!";  
                      
                    //�q�����q??? DEFAULT_SOUND, DEFAULT_VIBRATE, DEFAULT_LIGHTS.   
                    //�p�G�n���������q?��, �� DEFAULT_ALL.  
                    //��?�����q??��  
                    baseNF.defaults |= Notification.DEFAULT_SOUND;  
                    baseNF.defaults |= Notification.DEFAULT_VIBRATE;  
                    baseNF.defaults |= Notification.DEFAULT_LIGHTS;  
                      
                    //??���B��??���`?�A�����???  
                    baseNF.flags |= Notification.FLAG_INSISTENT;  
                      
                    //�q���Q??�Z�A��?����  
                    baseNF.flags |= Notification.FLAG_AUTO_CANCEL;  
                      
                    //??'Clear'?�A���M��?�q��(QQ���q��?�k�M���A�N�O�Ϊ�??)  
                    baseNF.flags |= Notification.FLAG_NO_CLEAR;  
                      
                      
                    //�ĤG??? �G�U��?????�ܪ�����?? expanded message title  
                    //�ĤT???�G�U��?????�ܪ�����?�e expanded message text  
                    //�ĥ|???�G???�q��??��?����?  
                    baseNF.setLatestEventInfo(MainActivity.this, "Title01", "Content01", pd);  
                      
                    //?�X???�q��  
                    //The first parameter is the unique ID for the Notification   
                    // and the second is the Notification object.  
                    nm.notify(Notification_ID_BASE, baseNF);  
                      
                    break;  
                      
                case R.id.le10bt02:  
                    //��s�q��  
                    //��p???���ܦ��@?�s�u�H�A???�o�άd�ݡA�S?�@?�s�u�H�����ܡC  
                    //��?���Χ�s��?�q�����覡��?�C  
                    //(�A���s?�@?�q���]�i�H�A���O???�y���q�����V?�A�ӥB?�ܦh?�q��?��?�A?��?�]���ͦn)  
                    baseNF.setLatestEventInfo(MainActivity.this, "Title02", "Content02", pd);  
                    nm.notify(Notification_ID_BASE, baseNF);  
                    break;  
                      
                case R.id.le10bt03:  
                      
                    //�M�� baseNF  
                    nm.cancel(Notification_ID_BASE);  
                    break;  
                      
                case R.id.le10bt04:  
                    mediaNF = new Notification();  
                    mediaNF.icon = R.drawable.ic_launcher;  
                    mediaNF.tickerText = "You clicked MediaNF!";  
                      
                    //�۩w??��  
                    mediaNF.sound = Uri.withAppendedPath(Audio.Media.INTERNAL_CONTENT_URI, "6");  
                      
                    //�q��??�X����?  
                    //�Ĥ@???: ��?�e���ݪ�??  
                    //�ĤG???�G �Ĥ@����?��??�B�H��?��  
                    long[] vir = {0,100,200,300};  
                    mediaNF.vibrate = vir;  
                      
                    mediaNF.setLatestEventInfo(MainActivity.this, "Title03", "Content03", pd);  
                      
                    nm.notify(Notification_ID_MEDIA, mediaNF);  
                    break;  
                      
                case R.id.le10bt05:  
                    //�M�� mediaNF  
                    nm.cancel(Notification_ID_MEDIA);  
                    break;  
                      
                case R.id.le10bt06:  
                    nm.cancelAll();  
                    break;  
                      
                case R.id.le10bt07:  
                    //�۩w?�U��??�A��p�U??��?�A?�ܪ�?��?�C  
                    Notification notification = new Notification();  
                      
                    notification.icon = R.drawable.ic_launcher;  
                    notification.tickerText = "Custom!";  
                      
                    RemoteViews contentView = new RemoteViews(getPackageName(), R.layout.aaa);  
                    contentView.setImageViewResource(R.id.image, R.drawable.ic_launcher);  
                    contentView.setTextViewText(R.id.text, "Hello, this message is in a custom expanded view");  
                    notification.contentView = contentView;  
                      
                    //�ϥΦ۩w?�U��???�A���ݭn�A?��setLatestEventInfo()��k  
                    //���O��?�w? contentIntent  
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

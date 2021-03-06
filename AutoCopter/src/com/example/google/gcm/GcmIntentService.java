/*
 * Copyright (C) 2013 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.example.google.gcm;

import com.example.autocopter.MainActivity;
import com.example.flowicon.NormalService;
import com.example.flowicon.UnNormalService;
import com.example.autocopter.R;
import com.example.stable.ConstValue;
import com.example.stable.ToastRunnable;
import com.example.stable.UsualMethod;
import com.google.android.gms.gcm.GoogleCloudMessaging;

import android.app.IntentService;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.NotificationCompat;
import android.util.Log;
import android.widget.Toast;

/**
 * This {@code IntentService} does the actual handling of the GCM message.
 * {@code GcmBroadcastReceiver} (a {@code WakefulBroadcastReceiver}) holds a
 * partial wake lock for this service while the service does its work. When the
 * service is finished, it calls {@code completeWakefulIntent()} to release the
 * wake lock.
 */
public class GcmIntentService extends IntentService 
{
	private Handler _handler=new Handler();
    private NotificationManager _notificationManager;

    public GcmIntentService() 
    {
        super(ConstValue.PROJECT_NUMBER);
    }

    @Override
    protected void onHandleIntent(Intent intent) 
    {
        Bundle extras = intent.getExtras();
        GoogleCloudMessaging gcm = GoogleCloudMessaging.getInstance(this);
        String messageType = gcm.getMessageType(intent);

        if (!extras.isEmpty()) // has effect of unparcelling Bundle 
        {  
            if (GoogleCloudMessaging.MESSAGE_TYPE_SEND_ERROR.equals(messageType)) 
            {
              //  sendNotification("Send error: " + extras.toString());
            } 
            else if (GoogleCloudMessaging.MESSAGE_TYPE_DELETED.equals(messageType)) 
            {
              //  sendNotification("Deleted messages on server: " + extras.toString());
            } 
            else if (GoogleCloudMessaging.MESSAGE_TYPE_MESSAGE.equals(messageType)) 
            {
                sendNotification(extras.getString("Msg"));
            }
        }
        // Release the wake lock provided by the WakefulBroadcastReceiver.
        GcmBroadcastReceiver.completeWakefulIntent(intent);
    }

    private void ClearAllPage()
    {
		if(!MainActivity._currentMain.isFinishing())
		{
			MainActivity._currentMain.finish();
		}
		if(NormalService._currentService!=null)
		{
			NormalService._currentService.stopSelf();
		}
		if(UnNormalService._currentService!=null)
		{
			UnNormalService._currentService.stopSelf();
		}
    }
    
    private void sendNotification(String msg) 
    {
    	if(UsualMethod.GetSharedPreferences().getBoolean(ConstValue.SHARE_PREFERENCES_LOGIN_STATE, false))
    	{
    		String title="";
        	String content="";
        	ClearAllPage();

        	if(msg.equals("False"))
        	{
        		title=ConstValue.NOTIFY_MANAGER_TITLE_WARNING;
        		content=ConstValue.NOTIFY_MANAGER_CONTENT_WARNING;
        	}
        	else if(msg.equals("True"))
        	{
            	title=ConstValue.NOTIFY_MANAGER_TITLE_NORMAL;
            	content=ConstValue.NOTIFY_MANAGER_CONTENT_NORMAL;
        	}
        	
        	_handler.post(new ToastRunnable(this,title+"\n"+content));
        	
        	_notificationManager = (NotificationManager)this.getSystemService(Context.NOTIFICATION_SERVICE);
            PendingIntent contentIntent = PendingIntent.getActivity(this, 0, new Intent(this, MainActivity.class), 0);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
            .setSmallIcon(R.drawable.ic_launcher)
            .setContentTitle(title)
            .setStyle(new NotificationCompat.BigTextStyle().bigText(msg))
            .setContentText(content)
            .setDefaults(Notification.DEFAULT_ALL);
            builder.setAutoCancel(true);

            builder.setContentIntent(contentIntent);
            _notificationManager.notify(ConstValue.NOTIFICATIONMANAGER_NOTIFY_ID, builder.build());
    	}
    	Log.v("asdf","gcm is arrive");
    }
}

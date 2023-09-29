using Firebase.Messaging;
using Firebase.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif
using UnityEngine;
namespace SU
{
    public class SuNotification : BaseSUUnit
    {
        private string AndroidChanelName;
        private string AndroidChanelID;
        LocalNotificationDataModule LocalNotificationData;
        public static string DeviceTokenID;
        public override void Init()
        {
            //TextAsset ta = Resources.Load<TextAsset>("LocalNotificationData");
            //LocalNotificationData = JsonUtility.FromJson<LocalNotificationDataModule>(ta.text);
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

#if UNITY_ANDROID
            SendAndroidNotification();
#elif UNITY_IOS
        InitAndSendIOSNotification();
#endif
        }


#if UNITY_IOS
    void InitAndSendIOSNotification()
    {
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
        iOSNotificationCenter.RemoveAllScheduledNotifications();
        
        for (int i = 0; i < LocalNotificationData.Notifications.Count; i++)
        {
            LocalNotificationDataItemModule notiItem = LocalNotificationData.Notifications[i];
            DateTime timePush = DateTime.Now.AddDays(notiItem.DaysAdd).AddHours(notiItem.HoursAdd).AddMinutes(notiItem.MinutesAdd).AddSeconds(notiItem.SecsAdd);
            SendIOSLocalNotification(notiItem.title, notiItem.mess, timePush);
        }        

    }

    void SendIOSLocalNotification(string title, string mes, DateTime time)
    {
        var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = time - DateTime.Now,
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            // You can specify a custom identifier which can be used to manage the notification later.
            // If you don't provide one, a unique string will be generated automatically.
            Identifier = "_notification_01",
            Title = title,
            Body = mes,
            Subtitle = "Cái này là subtitlt, bên android không có",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);
    }
    
    IEnumerator RequestAuthorization()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };

            string res = "\n RequestAuthorization:";
            res += "\n finished: " + req.IsFinished;
            res += "\n granted :  " + req.Granted;
            res += "\n error:  " + req.Error;
            res += "\n deviceToken:  " + req.DeviceToken;
            Debug.Log(res);
        }
    }
#endif


#if UNITY_ANDROID
        void SendAndroidNotification()
        {
            AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelAllNotifications();
            // lấy data của notification lần trước push
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();
            if (notificationIntentData != null)
            {

                var id = notificationIntentData.Id;
                var channel = notificationIntentData.Channel;
                var notification = notificationIntentData.Notification;
                //
                // Nếu muốn làm gì khi user mở app bằng local notification thì code ở đây
                //
            }
            InitLocalNotification();
            // send local notification
            /*
            for (int i = 0; i < LocalNotificationData.Notifications.Count; i++)
            {
                LocalNotificationDataItemModule notiItem = LocalNotificationData.Notifications[i];
                DateTime timePush = DateTime.Now.AddDays(notiItem.DaysAdd).AddHours(notiItem.HoursAdd).AddMinutes(notiItem.MinutesAdd).AddSeconds(notiItem.SecsAdd);
                SendAndroidLocalNotification(notiItem.title, notiItem.mess, timePush);
            }
            */
            //Debug.Log("thiet lap");
            DateTime D2_12h = DateTime.Now.Date.AddDays(1).AddHours(12);
            SendAndroidLocalNotification("🤪 Couldn't see how to reach the center? 🤪", "🕵️ Come a take a CLOSER look! 🕵️", D2_12h);
            DateTime D2_21h = DateTime.Now.Date.AddDays(1).AddHours(21);
            SendAndroidLocalNotification("Only 1% can beat this level 😱😱", "Can YOU do it? 👌", D2_21h);
            DateTime D3_12h = DateTime.Now.Date.AddDays(2).AddHours(12);
            SendAndroidLocalNotification("Choose one ↗️", "↗️1️⃣2️⃣3️⃣🎁🎁🎁", D3_12h);
            DateTime D3_21h = DateTime.Now.Date.AddDays(2).AddHours(21);
            SendAndroidLocalNotification("Tap Away Banned 🔥🔥🔥", "Do not even try ro move a block! 😠😡😠", D3_21h);
            DateTime D4_12h = DateTime.Now.Date.AddDays(3).AddHours(12);
            SendAndroidLocalNotification("Your are getting better! 🤗", "How many levels can you beat?", D4_12h);
            DateTime D4_21h = DateTime.Now.Date.AddDays(3).AddHours(21);
            SendAndroidLocalNotification("A bunch of happiness? 😁", "T_AP AW_Y", D4_21h);
        }

        void InitLocalNotification()
        {
            AndroidChanelName = Application.productName;
            AndroidChanelID = Application.identifier;
            var channel = new AndroidNotificationChannel()
            {
                Id = AndroidChanelID,
                Name = AndroidChanelName,
                Importance = Importance.High,
                Description = Application.productName + " chanel",
                CanShowBadge = true,
                EnableLights = true,
                LockScreenVisibility = LockScreenVisibility.Public,
                EnableVibration = false,
                CanBypassDnd = true,


            };
            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }

        public void SendAndroidLocalNotification(string title, string mes, DateTime time)
        {
            var notification = new AndroidNotification
            {
                Title = title,
                Text = mes,
                FireTime = time,
                LargeIcon = "icon_0_large",
                SmallIcon = "small_icon",
                Style = NotificationStyle.BigTextStyle,
                // set thêm color để trên android abcxyz nào đó không bị hiện ô vuông hay hình tròn background trùng màu với small icon
                Color = Color.red

            };
            AndroidNotificationCenter.SendNotification(notification, AndroidChanelID);
        }
#endif



        void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
            DeviceTokenID = token.Token;
            //
            // lưu token hay làm gì đó với token thì code ở đây 
            //
        }

        void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            foreach (string key in e.Message.Data.Keys)
            {
                Debug.Log("Data của notification nhận được là " + key + " : " + e.Message.Data[key]);
                string data = e.Message.Data[key];
                // cần làm gì đó với data gửi từ firebase notification thì code ở đây

            }
        }

    }

    [System.Serializable]
    public class LocalNotificationDataModule
    {
        public List<LocalNotificationDataItemModule> Notifications;
    }

    [System.Serializable]
    public class LocalNotificationDataItemModule
    {
        public string title;
        public string mess;
        public int HoursAdd, MinutesAdd, DaysAdd, SecsAdd;
    }
}
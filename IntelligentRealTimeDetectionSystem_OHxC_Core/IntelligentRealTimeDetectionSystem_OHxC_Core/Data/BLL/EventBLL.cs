using com.mirle.iibg3k0.ids.ohxc.App;
using com.mirle.iibg3k0.ids.ohxc.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.mirle.iibg3k0.ids.ohxc.Data.BLL
{
    public class EventBLL
    {
        RedisCacheManager RedisCacheManager = null;
        NatsManager NatsManager = null;
        public EventBLL(CSApplication _app)
        {
            RedisCacheManager = _app.RedisCacheManager;
            NatsManager = _app.NatsManager;
        }

        public void SetEvent2Redis(string event_code, string vh_id, List<string> message)
        {
            string event_key = CSAppConstants.REDIS_EVENT_KEY;
            string time = DateTime.Now.ToLongTimeString();
            string smessage = string.Join("-", message);

            string event_value = $"{vh_id},{event_code},{time},{smessage}";

            RedisCacheManager.ListRightPushAsync(event_key, event_value);
            RedisCacheManager.PublishEvent(event_key, event_value);
        }
        public void SetEvent2Redis(string event_code, string vh_id, string message)
        {
            string event_key = CSAppConstants.REDIS_EVENT_KEY;
            string time = DateTime.Now.ToLongTimeString();

            string event_value = $"{vh_id},{event_code},{time},{message}";

            RedisCacheManager.ListRightPushAsync(event_key, event_value);
            RedisCacheManager.PublishEvent(event_key, event_value);
        }

        public void PublishEvent(string event_code, string vh_id, List<string> message)
        {
            string smessage = string.Join("-", message);
            PublishEvent(event_code, vh_id, smessage);
        }

        TimeSpan event_effective_time = new TimeSpan(0, 0, 3);
        public void PublishEvent(string event_code, string vh_id, string message)
        {
            PublishEvent(event_code, vh_id, message, event_effective_time);
        }
        public void PublishEvent(string event_code, string vh_id, string message, TimeSpan event_effective_time)
        {
            string event_key = CSAppConstants.REDIS_EVENT_KEY;
            string time = DateTime.Now.ToLongTimeString();

            string event_value = $"{vh_id},{event_code},{time},{message}";

            string vh_event_key = $"{event_code}_{vh_id}";
            if (!RedisCacheManager.KeyExists(vh_event_key))
            {
                RedisCacheManager.stringSetAsync(vh_event_key, "", event_effective_time);
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(event_value);
                NatsManager.PublishAsync(event_key, byteArray);
            }
        }


    }
}

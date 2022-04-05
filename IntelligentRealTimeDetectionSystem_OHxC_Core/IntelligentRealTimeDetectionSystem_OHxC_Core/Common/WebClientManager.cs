using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.iibg3k0.ids.ohxc.Common
{
    public class WebClientManager
    {

        public enum HTTP_METHOD
        {
            GET,
            POST,
            DELET,
            PUT,
            PATCH
        }

        ConcurrentDictionary<string, HttpWebRequest> httpWebRequests = null;


        private static Object _lock = new Object();
        private static WebClientManager manager;
        public static WebClientManager getInstance()
        {
            if (manager == null)
            {
                lock (_lock)
                {
                    if (manager == null)
                    {
                        manager = new WebClientManager();
                    }
                }
            }
            return manager;
        }

        private WebClientManager()
        {
            httpWebRequests = new ConcurrentDictionary<string, HttpWebRequest>();
            //httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3280");
        }


        public string GetInfoFromServer(string[] action_targets, string param)
        {
            string result = string.Empty;
            string action_target = string.Join("/", action_targets);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://ohxcv.ha.ohxc.mirle.com.tw:3280/{action_target}/{param}");
            httpWebRequest.Method = HTTP_METHOD.GET.ToString();
            //指定 request 的 content type
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                httpResponse.Close();
            }
            httpWebRequest.Abort();
            return result;
        }

        public string PostInfoToServer(string[] action_targets, HTTP_METHOD methed, byte[] byteArray)
        {
            string result = string.Empty;
            string action_target = string.Join("/", action_targets);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://ohxc2.ohxc.mirle.com.tw:3280/{action_target}");
            httpWebRequest.Method = methed.ToString();
            httpWebRequest.ContentLength = byteArray.Length;
            //指定 request 的 content type
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            using (Stream reqStream = httpWebRequest.GetRequestStream())
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
                reqStream.Close();
            }
            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                httpResponse.Close();
            }
            httpWebRequest.Abort();
            return result;
        }

        private HttpWebRequest getWebRequest(string[] action_targets)
        {
            string action_target = string.Join("/", action_targets);
            return httpWebRequests.GetOrAdd(action_target, (HttpWebRequest)WebRequest.Create($"http://ohxc2.ohxc.mirle.com.tw:3280/{action_target}"));

        }
    }
}

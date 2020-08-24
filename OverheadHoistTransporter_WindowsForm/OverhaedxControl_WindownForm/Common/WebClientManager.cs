using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace com.mirle.ibg3k0.ohxc.winform.Common
{
    public class WebClientManager
    {
        public static string OHxC_CONTROL_URI = "http://ohxcv.ha.ohxc.mirle.com.tw:3280";
        public static string OHxC_SYSEXCUTEQUALITY_URI = "http://ohxc.query.mirle.com.tw:5000";
        HttpClient client = new HttpClient();
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
            client.BaseAddress = new Uri(OHxC_CONTROL_URI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            //httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:3280");
        }


        public string GetInfoFromServer(string uri, string[] action_targets, string param)
        {
            string result = string.Empty;
            string action_target = string.Join("/", action_targets);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{uri}/{action_target}/{param}");
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

        //public string GetInfoFromServer(string uri, string[] action_targets)
        //{
        //    string result = string.Empty;
        //    string action_target = string.Join("/", action_targets);
        //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{uri}/{action_target}");
        //    httpWebRequest.Method = HTTP_METHOD.GET.ToString();
        //    //指定 request 的 content type
        //    httpWebRequest.ContentType = "application/x-www-form-urlencoded";

        //    using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
        //    {

        //        Stream resStream = httpResponse.GetResponseStream();
        //        using (var streamReader = new Stream(httpResponse.GetResponseStream()))
        //        {
        //            result = streamReader.ReadToEnd();
        //        }
        //        httpResponse.Close();
        //    }
        //    httpWebRequest.Abort();
        //    return result;
        //}
        public async Task<byte[]> GetByteArrayFromServerAsync(string[] actionTargets)
        {
            string path = string.Join("/", actionTargets);
            byte[] get_result = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                get_result = await response.Content.ReadAsByteArrayAsync();
            }
            return get_result;
        }

        public System.Drawing.Image GetImageFromServerByCondition(string uri, string[] conditions, string param)
        {
            string result = string.Empty;
            string condition = string.Join("&", conditions);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{uri}/get_image?{condition}");
            httpWebRequest.Method = HTTP_METHOD.GET.ToString();
            //指定 request 的 content type
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            System.Drawing.Image img;
            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    img = System.Drawing.Image.FromStream(streamReader.BaseStream);
                }
                httpResponse.Close();
            }
            httpWebRequest.Abort();
            return img;
        }

        public string PostInfoToServer(string uri, string[] action_targets, HTTP_METHOD methed, byte[] byteArray)
        {
            string result = string.Empty;
            string action_target = string.Join("/", action_targets);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{uri}/{action_target}");
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

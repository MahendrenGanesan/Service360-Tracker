using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GioPo.Utilities
{
    public static class HttpWebRequestHelper
    {
        private static string _formContentType = "application/x-www-form-urlencoded";
        private static string _xmlContentType = "text/xml";

        public static async Task<CustomWebResponse> GetHttpWebResponse(string url)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.ContentType = _formContentType;
            request.Method = "Get";
            request.Timeout = 50000;

            HttpWebResponse httpWebresponse;
            try
            {
                httpWebresponse = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException ex)
            {
                httpWebresponse = (HttpWebResponse)ex.Response;
            }

            var response = new CustomWebResponse();
            response.Response = httpWebresponse;
            using (httpWebresponse)
            {
                using (var res = httpWebresponse.GetResponseStream())
                {
                    var reader = new StreamReader(res);
                    response.ResponseData = reader.ReadToEnd();
                    reader.Close();
                    httpWebresponse.Close();
                }
            }

            return response;
        }

        public static async Task<CustomWebResponse> PostHttpWebResponse(string url, object postData)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json;charset=UTF-8";
            //request.Accept = "application/json, text/plain, */*";
            request.Method = "Post";
            request.Timeout = 50000;

            var data = JsonConvert.SerializeObject(postData);
            var buffer = Encoding.ASCII.GetBytes(data);

            request.ContentLength = data.Length;

            using (Stream req = request.GetRequestStream())
            {
                req.Write(buffer, 0, buffer.Length);
                req.Close();
            }

            HttpWebResponse httpWebresponse;
            try
            {
                httpWebresponse = (HttpWebResponse)await request.GetResponseAsync();
            }
            catch (WebException ex)
            {
                httpWebresponse = (HttpWebResponse)ex.Response;
            }

            var response = new CustomWebResponse();
            response.Response = httpWebresponse;
            using (httpWebresponse)
            {
                using (var res = httpWebresponse.GetResponseStream())
                {
                    var reader = new StreamReader(res);
                    response.ResponseData = reader.ReadToEnd();
                    reader.Close();
                    httpWebresponse.Close();
                }
            }

            return response;
        }
    }
}

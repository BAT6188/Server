using Infrastructure.Utility;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientEx
{
    /// <summary>
    /// 备注：在Framework4.6调用时，需将lib\net46\System.Console.dll ,System.Net.Http.dll等调用的.netcore dll更新到framework46的运行目录
    /// 
    /// </summary>
    public class HttpClientHelper
    {
        /// <summary>
        /// 全局
        /// </summary>
        static HttpClient s_httpClient = null;

        public static ILogger<HttpClientHelper> s_logger;

        static HttpClientHelper()
        {
            ILoggerFactory loggerFactory = new LoggerFactory().AddConsole().AddNLog();
            s_logger = loggerFactory.CreateLogger<HttpClientHelper>();
            s_httpClient = new HttpClient();
            s_httpClient.DefaultRequestHeaders.Clear();
            s_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            s_httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="url"></param>
        public static IEnumerable<T> Get<T>(string url)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //    var response = httpClient.GetAsync(url);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("Get Ok....");
            //        string r = response.Result.Content.ReadAsStringAsync().Result;
            //        JsonSerializerSettings jsSetting = new JsonSerializerSettings();
            //        jsSetting.TypeNameHandling = TypeNameHandling.All;
            //        return JsonConvert.DeserializeObject<IEnumerable<T>>(r, jsSetting);
            //    }
            //    else
            //    {
            //        return new List<T>();
            //    }
            //}

            s_logger.LogInformation("HttpGet {0} begin", url);
            var result = s_httpClient.GetAsync(url).ContinueWith<IEnumerable<T>>((Task<HttpResponseMessage> task) => {
                if (task.Result.IsSuccessStatusCode)
                {
                    s_logger.LogInformation("HttpGet  Read string");
                    string r = task.Result.Content.ReadAsStringAsync().Result;
                    JsonSerializerSettings jsSetting = new JsonSerializerSettings();
                    jsSetting.TypeNameHandling = TypeNameHandling.All;
                    s_logger.LogInformation("HttpGet  return");
                    return JsonConvert.DeserializeObject<IEnumerable<T>>(r, jsSetting);
                }
                else
                {
                    return new List<T>();
                }
            });
            return result.Result;
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="url"></param>
        public static T GetOne<T>(string url)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //    var response = httpClient.GetAsync(url);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("Get Ok....");
            //        string r = response.Result.Content.ReadAsStringAsync().Result;
            //        JsonSerializerSettings jsSetting = new JsonSerializerSettings();
            //        jsSetting.TypeNameHandling = TypeNameHandling.All;
            //        return JsonConvert.DeserializeObject<T>(r, jsSetting);
            //    }
            //    else
            //    {
            //        return default(T);
            //    }
            //}

            s_logger.LogInformation("HttpGet {0} begin", url);
            var result = s_httpClient.GetAsync(url).ContinueWith<T>(
                (Task<HttpResponseMessage> task) =>
                {
                    s_logger.LogInformation("HttpGet  end");
                    var response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        s_logger.LogInformation("HttpGet  Read string");
                        string r = response.Content.ReadAsStringAsync().Result;
                        JsonSerializerSettings jsSetting = new JsonSerializerSettings();
                        jsSetting.TypeNameHandling = TypeNameHandling.All;
                        s_logger.LogInformation("HttpGet  return");
                        return JsonConvert.DeserializeObject<T>(r, jsSetting);
                    }
                    else
                    {
                        return default(T);
                    }
                });
            return result.Result;
        }

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="newObj">添加对象</param>
        /// <param name="url"></param>
        public static HttpRequestResult Post<T>(T newObj, string url, bool camelCase = true)
        {
            //bool result = false;
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));

            //    string body = "";
            //    if (camelCase)
            //    {
            //        body = JsonUtility.CamelCaseSerializeObject(newObj);
            //    }
            //    else
            //    {
            //        body = JsonConvert.SerializeObject(newObj);
            //    }
            //    var content = new StringContent(body, Encoding.UTF8, "application/json");
            //    var response = httpClient.PostAsync(url, content);
            //    result = response.Result.IsSuccessStatusCode;
            //}
            //return result;

            s_logger.LogInformation("Http Post {0} begin", url);
            string   body = camelCase?JsonUtility.CamelCaseSerializeObject(newObj) : JsonConvert.SerializeObject(newObj);
            s_logger.LogInformation("Http Post Json序列化");
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var r = s_httpClient.PostAsync(url, content).ContinueWith<HttpRequestResult>((Task<HttpResponseMessage> task) =>
            {
                //result = task.Result.IsSuccessStatusCode;
                var response = task.Result;
                s_logger.LogInformation("Http Post 响应完成，结果{0}", response.IsSuccessStatusCode);
                return GetHttpRequestMessage(response);
            });
            return r.Result;
        }

        public static HttpRequestResult Post<T>(T newObj, string url )
        {
            return Post<T>(newObj, url, Encoding.UTF8);
        }

        public static HttpRequestResult Post<T>(T newObj, string url, Encoding encoding,bool camelCase=true)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //    string body = "";
            //    if (camelCase)
            //        body = JsonUtility.CamelCaseSerializeObject(newObj);
            //    else
            //        body = JsonConvert.SerializeObject(newObj);
            //    var content = new StringContent(body, encoding, "application/json");
            //    var response = httpClient.PostAsync(url, content);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("Post Ok....");
            //        responseContent = response.Result.Content.ReadAsStringAsync().Result;
            //        return true;
            //    }
            //    else
            //    {
            //        responseContent = response.Result.ReasonPhrase;
            //        Console.WriteLine("Post Error ....StatusCode:{0},", response.Result.StatusCode);
            //        return false;
            //    }
            //}

            s_logger.LogInformation("Http Post {0} begin", url);
            string body = camelCase ? JsonUtility.CamelCaseSerializeObject(newObj) : JsonConvert.SerializeObject(newObj);
            s_logger.LogInformation("Http Post Json序列化:{0}",body);
            var content = new StringContent(body, encoding, "application/json");
            var r = s_httpClient.PostAsync(url, content).ContinueWith<HttpRequestResult>(
                (Task<HttpResponseMessage> task) =>
                {
                    var response = task.Result;
                    s_logger.LogInformation("Http Post 响应完成，结果{0},Return Code{1}", response.IsSuccessStatusCode, response.StatusCode);
                    return GetHttpRequestMessage(response);
                });
            return r.Result;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="editObj"></param>
        /// <param name="url"></param>
        /// <param name="camelCase"></param>
        public static HttpRequestResult Put<T>(T editObj, string url, bool camelCase = true)
        {
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //    var content = new StringContent(JsonUtility.CamelCaseSerializeObject(editObj), Encoding.UTF8, "application/json");
            //    var response = httpClient.PutAsync(url, content);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        responseContent = response.Result.Content.ReadAsStringAsync().Result;
            //        Console.WriteLine("Put Ok....");
            //    }
            //    else
            //    {
            //        responseContent = response.Result.ReasonPhrase;
            //        Console.WriteLine("Put Error ....StatusCode:{0},", response.Result.StatusCode);
            //    }
            //    return response.Result.IsSuccessStatusCode;
            //}


            s_logger.LogInformation("Http Put {0} begin", url);
            string body = camelCase ? JsonUtility.CamelCaseSerializeObject(editObj) : JsonConvert.SerializeObject(editObj);
            s_logger.LogInformation("Http Put Json序列化");
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var r = s_httpClient.PutAsync(url, content).ContinueWith<HttpRequestResult>((Task<HttpResponseMessage > task) 
                =>
            {
                var response = task.Result;
                s_logger.LogInformation("Http Put 响应完成，结果{0},Return Code{1}", response.IsSuccessStatusCode, response.StatusCode);
                return GetHttpRequestMessage(response);
            });
            return r.Result;
        }

        private static HttpRequestResult GetHttpRequestMessage(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return new HttpRequestResult()
                {
                    Success = true,
                    ResultText = response.Content.ReadAsStringAsync().Result
                };
            }
            else
            {
                return new HttpRequestResult()
                {
                    Success = false,
                    ResultText = response.ReasonPhrase
                };
            }
        }

        ///// <summary>
        ///// 更新操作
        ///// </summary>
        ///// <param name="editObj"></param>
        ///// <param name="url"></param>
        //public static bool Put(string url,ref string result)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        try
        //        {
        //            httpClient.DefaultRequestHeaders.Clear();
        //            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
        //            var response = httpClient.PutAsync(url, null);
        //            if (response.Result.IsSuccessStatusCode)
        //            {
        //                Console.WriteLine("Put Ok....");
        //                result = response.Result.Content.ReadAsStringAsync().Result;
        //            }
        //            else
        //            {
        //                Console.WriteLine("Put Error ....StatusCode:{0},", response.Result.StatusCode);
        //                result = response.Result.ReasonPhrase;
        //            }
        //            return response.Result.IsSuccessStatusCode;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Put Exception ....Message:{0}\r\nStackTrace:{1}", ex.Message, ex.StackTrace);
        //            result = ex.Message;
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="url"></param>
        public static bool Delete(string url, ref string responseContent)
        {
            //using (var httpClient = new HttpClient())
            //{

            //    httpClient.DefaultRequestHeaders.Clear();
            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            //    var response = httpClient.DeleteAsync(url);
            //    if (response.Result.IsSuccessStatusCode)
            //    {
            //        Console.WriteLine("Delete Ok....");
            //        responseContent = response.Result.Content.ReadAsStringAsync().Result;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Delete Error ....StatusCode:{0},", response.Result.StatusCode);
            //        responseContent = response.Result.ReasonPhrase;
            //    }
            //    return response.Result.IsSuccessStatusCode;
            //}

            s_logger.LogInformation("Http Delete {0} begin", url);
            var r = s_httpClient.DeleteAsync(url).ContinueWith<bool>((Task<HttpResponseMessage> task) =>
            {
                var response = task.Result;
                s_logger.LogInformation("Http Delete 响应完成，结果{0},Return Code{1}", response.IsSuccessStatusCode, response.StatusCode);
                return response.IsSuccessStatusCode;
            });
            return r.Result;
        }

    }
}

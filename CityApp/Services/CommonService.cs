﻿using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Text;

namespace CityApp
{
    public class CommonService<T>
    {
        public const string BaseUrl = "";

        public static async Task<Model<T>> HttpPostOperation(string url, MultipartFormDataContent content = null,string body=null)
        {
            try
            {
                if (await CrossConnectivity.Current.IsRemoteReachable("www.google.com", msTimeout: 10000))
                {
                    using (var client = new HttpClient(new NativeMessageHandler()))
                    {
                        client.BaseAddress = new Uri(BaseUrl);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //if (!string.IsNullOrWhiteSpace(key))
                        //{
                        //    client.DefaultRequestHeaders.Add("key", key);
                        //}
                        HttpResponseMessage response = null;
                        if (content != null)
                        {
                            response = await client.PostAsync(url, content);
                        }
                        else if (body != null)
                        {
                            HttpContent hc = new StringContent(body, Encoding.UTF8, "application/json");
                            response = await client.PostAsync(url, hc);
                        }
                        else
                        {
                            var stat = new Model<T>();
                            stat.message = "No Data!";
                            return stat;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Model<T>>(json));
                            return result;
                        }
                        else
                        {
                            //if (!url.Contains("User/Login"))
                            //{
                            //    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                            //    {
                            //        Logout.Current.Logout();
                            //    }
                            //}
                            return new Model<T>() { status = (int)response.StatusCode };
                        }
                    }
                }
                else
                {
                    var stat = new Model<T>();
                    stat.message = "Cannot Connect to Internet!";
                    return stat;
                }
            }
            catch (JsonException e)
            {
                var stat = new Model<T>();
                stat.message = e.ToString();
                return stat;
            }
            catch (Exception e)
            {
                var stat = new Model<T>();
                stat.message = e.ToString();
                return stat;
            }
        }
    }
}

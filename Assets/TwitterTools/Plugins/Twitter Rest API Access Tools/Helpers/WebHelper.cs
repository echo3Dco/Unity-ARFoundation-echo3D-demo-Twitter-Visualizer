using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Web.Helpers
{
    public static class WebHelper
    {
        public async static Task<string> HttpRequestAsync(string URL, string accessToken)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "Authorization" , "Bearer " + accessToken }
            };

            Debug.Log("Request to " + URL + " started");
            float startTime = Time.time;
            WWW web = await new WWW(URL, null, headers);


            if (web.error != null)
            {
                Debug.LogError("Failed to complete request to " + URL +
                    " | Elapsed time: " + (Time.time - startTime) + " seconds");
                Debug.LogError(web.error);
                return string.Empty;
            }
            else
            {
                Debug.Log("Request to " + URL + " sucessfully completed." +
                    " Elapsed time: " + (Time.time - startTime));
                return web.text;
            }
        }
        
        public static WebAccessToken GetTwitterApiAccessToken(string consumerKey, string consumerSecret)
        {
            string encodeMe = consumerKey.Trim() + ":" + consumerSecret.Trim();
            string URL_ENCODED_KEY_AND_SECRET = Convert.ToBase64String(Encoding.UTF8.GetBytes(encodeMe));

            byte[] webRequestBody = Encoding.UTF8.GetBytes("grant_type=client_credentials");

            Dictionary<string, string> webRequestHeaders = new Dictionary<string, string>();
            webRequestHeaders["Authorization"] = "Basic " + URL_ENCODED_KEY_AND_SECRET;
            string url = "https://api.twitter.com/oauth2/token";

            WWW webRequest = new WWW(url, webRequestBody, webRequestHeaders);
            Debug.Log("Request for access token sent to " + url + ", using consumer key:" + consumerKey + " and consumer secret:" + consumerSecret);
            while (!webRequest.isDone)
            {
                Debug.Log("Retrieving access token...");
            }
            if (webRequest.error != null)
            {
                Debug.Log("Web error: " + webRequest.error);
                return null;
            }
            else
            {
                Debug.Log("Access token retrieved successfully");
                WebAccessToken accessToken = JsonUtility.FromJson<WebAccessToken>(webRequest.text);
                return accessToken;
            }
        }

    }
}
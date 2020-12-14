using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Web.Twitter.DataStructures;
using Web.Helpers;
using System.Threading.Tasks;

namespace Web.Twitter.API
{
    public class TwitterRestApiHelper : MonoBehaviour
    {
        //https://developer.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-users-show.html
        public async static Task<UserProfile> GetUserProfileByUsername(string username, string AccessToken)
        {
            string url = "https://api.twitter.com/1.1/users/show.json";
            url += "?screen_name=" + username;
            url += "&include_entities=true";

            string webResponse = await WebHelper.HttpRequestAsync(url, AccessToken);

            UserProfile userProfiles = JsonUtility.FromJson<UserProfile>(webResponse);
            return userProfiles;
        }

        //https://developer.twitter.com/en/docs/accounts-and-users/follow-search-get-users/api-reference/get-users-show.html
        public async static Task<UserProfile> GetUserProfileByUserId(string userId, string AccessToken)
        {
            string url = "https://api.twitter.com/1.1/users/show.json";
            url += "?user_id=" + userId;
            url += "&include_entities=true";

            string webResponse = await WebHelper.HttpRequestAsync(url, AccessToken);

            UserProfile userProfiles = JsonUtility.FromJson<UserProfile>(webResponse);
            return userProfiles;
        }

        //https://developer.twitter.com/en/docs/trends/trends-for-location/api-reference/get-trends-place
        public async static Task<Trend[]> GetGlobalTrends(string accessToken)
        {
            //WOEID of 1 returns global trends
            return await GetLocalTrends(1, accessToken);
        }

        //https://developer.twitter.com/en/docs/trends/trends-for-location/api-reference/get-trends-place
        public async static Task<Trend[]> GetLocalTrends(long whereOnEarthId, string accessToken)
        {
            //Where On Earth Id (WOEID)
            //https://en.wikipedia.org/wiki/WOEID

            string url = "https://api.twitter.com/1.1/trends/place.json?id=" + whereOnEarthId;
            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);

            webResponse = webResponse.Remove(0, 1);
            webResponse = webResponse.Remove(webResponse.Length - 1, 1);

            TrendSearchResults trendSearchResults = JsonUtility.FromJson<TrendSearchResults>(webResponse);
            return trendSearchResults.trends;
        }

        //https://developer.twitter.com/en/docs/tweets/post-and-engage/api-reference/get-statuses-show-id
        public async static Task<Tweet> GetTweetByID(string ID, string accessToken)
        {
            string url = "https://api.twitter.com/1.1/statuses/show.json?&id=" + ID + "&include_entities=true&include_my_retweet=true";
            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);

            Tweet tweet = JsonUtility.FromJson<Tweet>(webResponse);
            return tweet;
        }

        //https://developer.twitter.com/en/docs/tweets/search/api-reference/get-search-tweets
        public async static Task<Tweet[]> SearchForTweets(string searchQuery,
            string accessToken,
			string language,                //https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes
			string geocode,
			int? maxTweetsToReturn = null,
            SearchResultType searchType = SearchResultType.recent,
            DateTime? tweetTimeStampCutoff = null,  //Don't return any tweets posted after this time
            long? minimumTweetId = null)            //For retriveing tweets with ids greater than the value given here
        {
            string url = "https://api.twitter.com/1.1/search/tweets.json?q=" + searchQuery;

            if (maxTweetsToReturn != null)      url += "&count=" + maxTweetsToReturn;
            if (language != null)               url += "&lang=" + language;
            if (geocode != null)                url += "&geocode=" + geocode;
            if (tweetTimeStampCutoff != null)   url += "&until=" + tweetTimeStampCutoff.Value.Year + tweetTimeStampCutoff.Value.Month + tweetTimeStampCutoff.Value.Day;
            if (minimumTweetId != null)         url += "&since_id" + minimumTweetId;

            url += "&result_type=" + searchType.ToString();
            url += "&include_entites=true";

            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);

            SearchResults searchResults = JsonUtility.FromJson<SearchResults>(webResponse);
            return searchResults.statuses;
        }

        //https://developer.twitter.com/en/docs/tweets/timelines/api-reference/get-statuses-user_timeline.html
        public async static Task<Tweet[]> GetLatestTweetsFromUserByUserId(string userID, string accessToken, int maximumTweetsToGet = 10, bool includeRetweets=false)
        {
            string url = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id=" + userID + "&count=" + maximumTweetsToGet + "&include_rts=" + includeRetweets + "&include_entities=true";
            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);
            SearchResults searchResults = JsonUtility.FromJson<SearchResults>("{\"statuses\":" + webResponse + "}");
            return searchResults.statuses;
        }

        //https://developer.twitter.com/en/docs/tweets/timelines/api-reference/get-statuses-user_timeline.html
        public async static Task<Tweet[]> GetLatestTweetsFromUserByScreenName(string screenName, string accessToken, int maximumTweetsToGet = 1, bool includeRetweets = false)
        {
            string url = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name=" + screenName + "&count=" + maximumTweetsToGet + "&include_rts=" + includeRetweets;
            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);
            SearchResults searchResults = JsonUtility.FromJson<SearchResults>("{\"statuses\":" + webResponse + "}");
            return searchResults.statuses;
        }

        //https://developer.twitter.com/en/docs/tweets/post-and-engage/api-reference/get-favorites-list
        public async static Task<Tweet[]> GetLikedTweetsOfUserByUserID(string userId,
            string accessToken,
            int maxTweetsToReturn = 1,
            string minimumTweetId = null,
            string maximumTweetId = null)
        {
            string url = "https://api.twitter.com/1.1/favorites/list.json?user_id=" + userId + "&count=" + maxTweetsToReturn;
            if (minimumTweetId != null) url += "&since_id=" + minimumTweetId;
            if (maximumTweetId != null) url += "&max_id=" + maximumTweetId;

            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);

            SearchResults searchResults = JsonUtility.FromJson<SearchResults>("{\"statuses\":" + webResponse + "}");
            return searchResults.statuses;
        }

        //https://developer.twitter.com/en/docs/tweets/post-and-engage/api-reference/get-favorites-list
        public async static Task<Tweet[]> GetLikedTweetsOfUserByUsername(string username,
            string accessToken,
            int maxTweetsToReturn = 1,
            string minimumTweetId = null,
            string maximumTweetId = null)
        {
            string url = "https://api.twitter.com/1.1/favorites/list.json?screen_name=" + username + "&count=" + maxTweetsToReturn;
            if (minimumTweetId != null) url += "&since_id=" + minimumTweetId;
            if (maximumTweetId != null) url += "&max_id=" + maximumTweetId;

            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);

            SearchResults searchResults = JsonUtility.FromJson<SearchResults>("{\"statuses\":" + webResponse + "}");
            return searchResults.statuses;
        }

        //https://developer.twitter.com/en/docs/tweets/post-and-engage/api-reference/get-statuses-retweeters-ids
        public async static Task<string[]> GetUserIdsOfRetweeters(string tweetId,
            string accessToken,
            int maxTweetsToReturn = 1)
        {
            throw new NotImplementedException();
            /*string url = "GET https://api.twitter.com/1.1/statuses/retweeters/ids.json";
            url+="?id=" + tweetId;
            url+= "&count=" + maxTweetsToReturn;
            url += "&stringify_ids=true";

            string webResponse = await WebHelper.HttpRequestAsync(url, accessToken);
            webResponse = webResponse.Replace("[", "{");
            webResponse = webResponse.Replace("]", "}");

            RetweeterSearchResults retweeterSearchResults = JsonUtility.FromJson<RetweeterSearchResults>(webResponse);
            return retweeterSearchResults.ids;*/
        }
    }
}
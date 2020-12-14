using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using Web.Helpers;
using Web.Twitter.API;
using Web.Twitter.DataStructures;

[RequireComponent(typeof(TweetGenerator))]
public class TweetManager : MonoBehaviour
{

    public string TwitterApiConsumerKey;
    public string TwitterApiConsumerSecret;

    TweetGenerator tg;
    public GameObject inputField;
    public string searchQuery, language, geocode;
    private float latitude, longitude;
    public int radius;
    public bool isTweetFound;

    public WebAccessToken TwitterApiAccessToken;
   

    [Header("SearchForTweets")]
    public Tweet[] SearchResults;

    public Text debugText;

    private void Start()
    {
        // query for Twitter Access token
        TwitterApiAccessToken = WebHelper.GetTwitterApiAccessToken(TwitterApiConsumerKey,TwitterApiConsumerSecret);

        // assign TweetGenerator Component
        tg = GetComponent<TweetGenerator>();

        // get Debug Text game object
        //debugText = GameObject.Find("DebugText").GetComponent<Text>();
    }

    // prints search results
    private void PrintTweets()
    {
        foreach(Tweet tweet in SearchResults)
        {
            Debug.Log(tweet.text);
        }
    }

    // Called when UI "Search" button is pressed
    public void GetTweets()
    {
        // Destroy any tweet objects currently in the scene
        tg.DestroyTweets();

        //Initialize Location services and Search Tweets
        StartCoroutine(GetLocation());
    }

    // Find the local tweets about the regarding the search
    private async void SearchTweets(){
        // prep search
        if(latitude != 0 || longitude != 0) geocode = latitude.ToString() + "," + longitude.ToString() + "," + radius.ToString() + "mi";
        searchQuery = NewQuery();
        //Debug.Log(searchQuery);
        //debugText.text = searchQuery;

        // search
        SearchResults = await TwitterRestApiHelper.SearchForTweets(searchQuery, this.TwitterApiAccessToken.access_token, language, geocode);   
        if (SearchResults != null)
        {
            isTweetFound = true;
            Debug.Log("Tweets Found");
            //PrintTweets();
            //debugText.text = "Tweets Found";
            tg.GenerateTweets();
        }
        else
        {
            Debug.Log("Tweets not Found");
            //debugText.text = "Tweets Not Found";
        }
        
    }

    IEnumerator GetLocation(){

        //check if user has enable location
        if (!Input.location.isEnabledByUser)
        {
            //if location is not enabled use default values
            //debugText.text = "Location is not enabled";
            SearchTweets();
            yield break;
        }

        //Start location service
        Input.location.Start();
        debugText.text = "Location Start";

        //wait until service initializes
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0){
            yield return new WaitForSeconds(1);
            //debugText.text = "Location initializing";
            maxWait --; 
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            //debugText.text = "Location timed out";
            yield break;
        }

        // Service failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            //debugText.text = "Location failed";
            yield break;
        } else
        {
            //log Longitude and Latitude and search Tweets
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            //debugText.text = "Location Found:" + latitude.ToString() +", " + longitude.ToString();
            SearchTweets();
        }

        //Stop retrieving location data
        Input.location.Stop();
    }

    // update search term from input field
    public string NewQuery()
    {
        string newQuery = inputField.GetComponent<InputField>().text;
        newQuery = ReplaceHastag(newQuery);

        //if nothing is entered in Input field return default value
        return (String.IsNullOrEmpty(newQuery)) ? searchQuery : newQuery;
    }

    //replaces "#" with "%23" url appropriate value
    public string ReplaceHastag(string query)
    {
        
        string pattern = "#";
        string replacement = "%23";

        Regex r = new Regex(pattern);
        string result = r.Replace(query, replacement);
        return result;
    }

}

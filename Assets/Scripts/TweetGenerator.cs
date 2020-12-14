using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Web.Helpers;
using Web.Twitter.API;
using Web.Twitter.DataStructures;


public class TweetGenerator : MonoBehaviour
{
    public TweetManager tweetManager;
    [SerializeField]
    private GameObject tweetPrefab; // prefab that includes tweet text and objectToSpawn
    [SerializeField]
    private GameObject objectToSpawn; // echoAR object to spawn
    [SerializeField]
    private GameObject spawnPoint; // location to spawn tweet objects
    [Range(0, 10)]
    public float distance; // distance in front of spawn point to spawn tweet objects

    [Range(0, 10)]
    public float power; // translational power for Tweet Object

    [Range(0, 5)]
    public float torquePower = 3; // rotation power for Tweet object

    private Text screenText;

    private void Start()
    {
        //screenText = tweetManager.debugText;
    }
    public void GenerateTweets()
    {
        //iterate through search results
        //screenText.text = "Generating Tweets";
        foreach(Tweet tweet in tweetManager.SearchResults)
        {
            // create Tweet gameobject in front of spawnPoint (camera)
            Vector3 spawnPosition = spawnPoint.transform.position + spawnPoint.transform.forward * distance;
            GameObject tweetObject = Instantiate(tweetPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Instantiated Tweet Object");

            // create echo AR game object as child
            GameObject spawnObject = Instantiate(objectToSpawn, new Vector3(tweetObject.transform.position.x, tweetObject.transform.position.y, tweetObject.transform.position.z), Quaternion.identity);
            spawnObject.transform.parent = tweetObject.transform;
            Debug.Log("Instantiated echoAR Object");

            // wait for child (and text) to load 
            StartCoroutine(LoadEchoARPrefab(tweetObject, spawnObject, tweet));
            //screenText.text = "echoAR object loaded";
        }
    }

    IEnumerator LoadEchoARPrefab(GameObject tweetObj, GameObject spawnObj, Tweet tweet)
    {
        // wait for echo AR object to load
        int maxWait = 20;
        while(spawnObj.transform.childCount == 0)
        {
            //screenText.text = "Waiting for echoAR to load";
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // loading time out
        if(maxWait < 0)
        {
            Debug.Log("Loading EchoAR Asset timed out");
            //screenText.text = "Loading EchoAR Asset timed out";
            yield break;
        }

        // add the tweet text
        GenerateText(tweetObj, tweet);
        AddMovement(tweetObj);
        
    }


    // assign tweet data to game object text
    private void GenerateText(GameObject tweetObj, Tweet tweet)
    {
        //screenText.text = "Generating Text";

        //grab canvas and panel reference
        GameObject canvas = tweetObj.gameObject.transform.Find("Canvas").gameObject;
        GameObject panel = canvas.gameObject.transform.Find("Panel").gameObject;

        // assign name
        GameObject tweetNameObj = panel.gameObject.transform.Find("Text-Name").gameObject;
        Text tweetNameText = tweetNameObj.GetComponent<Text>();
        tweetNameText.text = tweet.user.name;

        // assign screen name
        GameObject tweetScreenNameObj = panel.gameObject.transform.Find("Text-ScreenName").gameObject;
        Text tweetScreenNameText = tweetScreenNameObj.GetComponent<Text>();
        tweetScreenNameText.text = "@" + tweet.user.screen_name;

        // assign content
        GameObject tweetContentObj = panel.gameObject.transform.Find("Text-Content").gameObject;
        Text tweetContentText = tweetContentObj.GetComponent<Text>();
        tweetContentText.text = tweet.text;
    }

    private void AddMovement(GameObject tweetObj)
    {
        //screenText.text = "Adding Movement";

           //send tweet floating in front of camera
           // get rigid body
           Rigidbody rb = tweetObj.GetComponent<Rigidbody>();

        //create semi-random direction to float
        Vector3 spawnDirection = spawnPoint.transform.forward + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        Vector3 spawnAngle = new Vector3(Random.Range(-200f, 200f), Random.Range(-200f, 200f), Random.Range(-200, 200f));
        rb.AddForce(spawnDirection * power);
        rb.AddTorque(spawnAngle * Random.Range(-1, 1) * torquePower);
        
    }

    public void DestroyTweets()
    {
        // find all the tweet objects in the scene
        GameObject[] tweetsToDestroy = GameObject.FindGameObjectsWithTag("tweet");

        // if tweet objects exist, destroy them
        if(tweetsToDestroy != null)
        {
            foreach (GameObject tweet in tweetsToDestroy)
            {
                Destroy(tweet);
            }
        }
        
    }
}



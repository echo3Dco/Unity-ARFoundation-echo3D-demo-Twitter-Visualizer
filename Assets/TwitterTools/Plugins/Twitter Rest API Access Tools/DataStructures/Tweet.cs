using System;

namespace Web.Twitter.DataStructures
{
    [Serializable]
    public class Tweet
    {
        public string created_at;
        public long id;
        public string text;
        public bool truncated;
        public string source;
        public long in_reply_to_status_id;
        public long in_reply_to_user_id;
        public string in_reply_to_screen_name;
        public UserProfile user;
        public Entities entities;
        public Entities extended_entities;
        public Tweet retweeted_status;
        public bool is_quote_status;
        public long quoted_status_id;
        public QuotedStatus quoted_status;
        public string geo;
        public string coordinates;
        public string place;
        public string contributors;
        public int retweet_count;
        public int favorite_count;
        public bool retweeted;
        public bool favourited;
    }

    [Serializable]
    public class QuotedStatus
    {
        public string created_at;
        public long id;
        public string text;
        public string source;
        public bool truncated;
        public Entities entities;
    }
}
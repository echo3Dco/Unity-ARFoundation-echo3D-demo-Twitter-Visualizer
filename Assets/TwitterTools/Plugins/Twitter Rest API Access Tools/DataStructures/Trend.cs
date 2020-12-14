using System;

namespace Web.Twitter.DataStructures
{
    [Serializable]
    public class Trend
    {
        public string name;
        public string url;
        public string promoted_content;
        public string query;
        public string tweet_volume;
    }
}
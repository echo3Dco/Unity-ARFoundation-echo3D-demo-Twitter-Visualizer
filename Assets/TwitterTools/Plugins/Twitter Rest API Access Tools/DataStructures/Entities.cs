using System;

namespace Web.Twitter.DataStructures
{
    [Serializable]
    public class Entities
    {
        public Hashtag[] hashtags;
        public Media[] media;
        public Url[] urls;
        public User_Mention[] user_mentions;
        public string[] symbols;
        public Poll[] polls;
    }

    [Serializable]
    public class Media
    {
        public long id;
        public int[] indices;
        public string media_url;
        public string media_url_https;
        public string url;
        public string display_url;
        public string expanded_url;
        public string type;
        public Size sizes;
        public VideoInfo video_info;
        public object features;
        public long source_status_id;
    }

    [Serializable]
    public class VideoInfo
    {
        public int[] aspect_ratio;
        public Variant[] variants;
    }

    [Serializable]
    public class Variant
    {
        public int bitrate;
        public string content_type;
        public string url;
    }

    [Serializable]
    public class Size
    {
        public Dimensions thumb;
        public Dimensions large;
        public Dimensions medium;
        public Dimensions small;
    }

    [Serializable]
    public class Dimensions
    {
        public int h;
        public string resize;
        public int w;
    }

    [Serializable]
    public class Hashtag
    {
        public int[] indices;
        public string text;
    }

    [Serializable]
    public class Url
    {
        public int[] indices;
        public string url;
        public string expanded_url;
        public string display_url;
    }

    [Serializable]
    public class User_Mention
    {
        public string screen_name;
        public string name;
        public long id;
        public int[] indices;
    }

    [Serializable]
    public class Poll
    {
        public PollOption[] options;
        public string end_datetime;
        public int duration_minutes;
    }

    [Serializable]
    public class PollOption
    {
        public int position;
        public string text;
    }
}
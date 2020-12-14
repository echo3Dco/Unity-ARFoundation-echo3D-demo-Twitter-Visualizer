using System;

namespace Web.Twitter.DataStructures
{
    [Serializable]
    public class UserProfile
    {
        public long id;
        public string name;
        public string screen_name;
        public string location;
        public string description;
        public string url;
        public string @protected;
        public int followers_count;
        public int friends_count;
        public int listed_count;
        public string created_at;
        public int favourites_count;
        public string utc_offset;
        public string time_zone;
        public bool geo_enabled;
        public bool verified;
        public int statuses_count;
        public string lang;
        public bool contributors_enabled;
        public string profile_background_color;
        public string profile_image_url;
        public bool profile_background_tile;
        public string profile_banner_url;
        public string profile_link_color;
        public string profile_sidebar_border_color;
        public string profile_sidebar_fill_color;
        public string profile_text_color;
        public bool profile_use_background_image;
        public bool has_extended_profile;
        public bool default_profile;
        public bool default_profile_image;
        public bool follow_request_sent;
        public string translator_type;
        public string[] withheld_in_countries;

        // Deprecated
        public bool following;
        public bool notifications;
    }
}
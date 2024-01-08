using Newtonsoft.Json;

namespace SpotifyRestApi.SpotifyModels
{
    public class SpotifyUserInfoResponse
    {
        [JsonProperty("country")]
        public string Country
        {
            get; set;
        }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("href")]
        public string HREF { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("images")]
        public ProfileImage[] Images { get; set; }
        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("followers")]
        public Followers Followers { get; set; }
    }

    public class ProfileImage
    {
        [JsonProperty("url")]
        public string ImageUrl { get; set; }
        [JsonProperty("height")]
        public string ImageHeight { get; set; }
        [JsonProperty("width")]
        public string ImageWidth { get; set; }

    }

    public class Followers
    {
        [JsonProperty("href")]
        public string HREF { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}

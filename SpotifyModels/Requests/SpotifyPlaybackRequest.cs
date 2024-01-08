using Newtonsoft.Json;

namespace SpotifyRestApi.SpotifyModels.Requests
{
    public class SpotifyPlaybackRequest
    {
        [JsonIgnore]
        public string? device_id { get; set; }
        public string? context_uri { get; set; }
        public string? [] uris { get; set; }
        public object offset { get; set; }

        public int position_ms { get; set; }    


    }
}

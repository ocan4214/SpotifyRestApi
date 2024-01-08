using Newtonsoft.Json;
using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyModels.Enums;
using SpotifyRestApi.SpotifyModels.Requests;
using SpotifyRestApi.Containers;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace SpotifyRestApi.SpotifyRepositories
{
    public class SpotifyPlayerRepository : IApiPlayerRepository
    {
        private static HttpClient _httpClient;
        public SpotifyPlayerRepository()
        {

            _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

        }

        public async Task<bool> PauseSong(string? device_id)
        {

            try
            {
                Uri uri;


                if (String.IsNullOrEmpty(device_id))
                {
                    uri = new Uri(SpotifyConstants.PauseCall);

                }
                else
                {
                    uri = new Uri(SpotifyConstants.PauseCall + "?device_id=" + device_id);

                }

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = uri,
                    Headers =
                    {

                        { "Authorization",Utilities.ReturnBearerAccessToken() },
                    }
                };

                var response = await _httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PlaySong(SpotifyPlaybackRequest? spotifyPlaybackRequest)
        {
            try
            {

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(SpotifyConstants.PlaybackCall),
                    Headers =
                            {
                                { "Authorization",Utilities.ReturnBearerAccessToken() }
                            }
                };

                if (spotifyPlaybackRequest != null )
                {
                    if(!String.IsNullOrEmpty(spotifyPlaybackRequest.device_id))
                    {
                        requestMessage.RequestUri = new Uri(SpotifyConstants.PlaybackCall + "?device_id=" + spotifyPlaybackRequest.device_id);

                    }
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(spotifyPlaybackRequest), System.Text.Encoding.UTF8, "application/json");
                }

                var response = await _httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SearchAndPlay(string songName)
        {

            try
            {

                var query = new Dictionary<string, string?>()
                {
                    { "q", songName },
                    { "type", SpotifySearchTypeEnum.Track.GetEnumMemberValue() },
                    { "market", SpotifyMarkets.TR },
                    { "limit", "1"},
                    { "offset", "0" },
                    { "include_external", "audio" }

                };

                Uri uri = new Uri(QueryHelpers.AddQueryString(SpotifyConstants.SearchSong, query));



                HttpRequestMessage requestSearchSongMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = uri,
                    Headers =
                    {
                        { "Authorization",Utilities.ReturnBearerAccessToken() }
                    }
                };

                var response = await _httpClient.SendAsync(requestSearchSongMessage);
                response.EnsureSuccessStatusCode();
                JObject jObject = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
                if (jObject != null)
                {
                    JArray tracksJArray = JArray.Parse(jObject.SelectToken("tracks.items").ToString());
                    string[,] tracks = new string[tracksJArray.Count, 2];
                    for (int i = 0; i < tracksJArray.Count; i++)
                    {
                        tracks[i, 0] ="Artist name = " + tracksJArray[i].SelectToken("artists[0].name").ToString();
                        tracks[i, 1] = tracksJArray[i]["uri"].ToString();

                    }
                    SpotifyPlaybackRequest spotifyPlaybackRequest = new SpotifyPlaybackRequest { uris = new string[1] { tracks[0, 1] } };
                    var playResponse = await PlaySong(spotifyPlaybackRequest);


                    return true;

                }
                else
                {
                    throw new Exception("PlayandSearchError");
                }


            }
            catch (Exception ex)
            {
                return false;
            }



        }

        public async Task<bool> SkipToNextSong(string? device_id)
        {
            try
            {
                Uri uri;


                if (String.IsNullOrEmpty(device_id))
                {
                    uri = new Uri(SpotifyConstants.SkipToNextSong);

                }
                else
                {
                    uri = new Uri(SpotifyConstants.SkipToNextSong + "?device_id=" + device_id);

                }

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Headers =
                    {
                        { "Authorization",Utilities.ReturnBearerAccessToken() }
                    }
                };

                var response = await _httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SkipToPreviousSong(string? device_id)
        {


            try
            {
                Uri uri;


                if (String.IsNullOrEmpty(device_id))
                {
                    uri = new Uri(SpotifyConstants.SkipToPreviousSong);

                }
                else
                {
                    uri = new Uri(SpotifyConstants.SkipToPreviousSong + "?device_id=" + device_id);

                }

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri,
                    Headers =
                    {
                        { "Authorization",Utilities.ReturnBearerAccessToken() }
                    }
                };

                var response = await _httpClient.SendAsync(requestMessage);

                response.EnsureSuccessStatusCode();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

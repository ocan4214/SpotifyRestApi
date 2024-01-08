using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyModels;
using System.Text;

namespace SpotifyRestApi.SpotifyRepositories
{
    public class SpotifyPlaylistRepository : IApiPlaylistRepository
    {
        public async Task<SpotifyUserPlaylistResponse> GetUserPlaylist(string userId, int? limit, int? offset)
        {
            HttpClient httpClient = new HttpClient();
            SpotifyUserPlaylistResponse modelResponse = null;
            try
            {
                var url = String.Format(SpotifyConstants.GetUserPlaylist, userId);

                Dictionary<string, string?> queryParams = new Dictionary<string, string?>
                {
                    { "userId", userId },
                    { "limit", limit.ToString() },
                    { "offset", offset.ToString() }
                };

                HttpRequestMessage request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(QueryHelpers.AddQueryString(url, queryParams)),
                    Headers =
                    {

                        { "Authorization",Utilities.ReturnBearerAccessToken() },
                        { "Accept", "application/json" }
                    }
                };


                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                if(!String.IsNullOrEmpty(jsonResponse))
                modelResponse = JsonConvert.DeserializeObject<SpotifyUserPlaylistResponse>(jsonResponse);

                return modelResponse;

            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }

        }


        public async Task<string> AddCustomPlaylistCover(string playlistID, string base64Image)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var url = String.Format(SpotifyConstants.AddCustomPlaylistCover, playlistID);


                HttpRequestMessage request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(url),
                    Headers =
                    {

                        { "Authorization", Utilities.ReturnBearerAccessToken() },
                        { "Content-Type", "image/jpeg" },
                    },
                    Content = new StringContent(base64Image, Encoding.UTF8, "application/json")
                    
                };

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return "Image Updated";
            }
            catch (Exception ex)
            {
                var exception = ex;
                return "Image Update Failed";
            }
        }
    }
}

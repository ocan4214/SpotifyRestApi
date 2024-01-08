using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyModels;
using SpotifyRestApi.SpotifyModels.Enums;
using SpotifyRestApi.Containers;
using Microsoft.AspNetCore.WebUtilities;

namespace SpotifyRestApi.SpotifyRepositories
{
    public class SpotifyUserRepository : IApiUserRepository
    {

        private static HttpClient _httpClient;
        public SpotifyUserRepository()
        {

            _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) };

        }

        public async Task<SpotifyUserInfoResponse> GetCurrentUserProfile()
        {
            try
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(SpotifyConstants.CurrentUserApiCall),
                    Headers =
                    {

                        { "Authorization",Utilities.ReturnBearerAccessToken() },
                        { "Accept", "application/json" }
                    }


                };


                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                SpotifyUserInfoResponse profileResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SpotifyUserInfoResponse>(jsonResponse);
                return profileResponse;
            }
            catch (Exception ex)
            {
                var excep = ex;
                return null;
            }

        }

        public async Task<string> GetUsersTopItems(SpotifyEntityTypeEnum type, SpotifyTimeRangeEnum rangeEnum , int limit, int offset)
        {
            try
            {
                var queryString = new Dictionary<string, string?>()
                {
                    { "time_range", rangeEnum.GetEnumMemberValue() },
                    { "limit", limit.ToString() },
                    { "offset", offset.ToString()},
                };


                HttpRequestMessage requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(QueryHelpers.AddQueryString(String.Format(SpotifyConstants.CurrentUserTopItem,type.GetEnumMemberValue()),queryString)),
                    Headers =
                    {

                        { "Authorization",Utilities.ReturnBearerAccessToken() },
                        { "Accept", "application/json" }
                    }
                };


                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return jsonResponse;
            }
            catch (Exception ex)
            {
                var excep = ex;
                return null;
            }
        }
    }
}

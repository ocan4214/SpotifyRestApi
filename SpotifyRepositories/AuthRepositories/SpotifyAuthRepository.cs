using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using SpotifyRestApi.Containers;
using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyModels;

namespace SpotifyRestApi.SpotifyRepositories
{
    public class SpotifyAuthRepository : IApiAuthenticationRepository
    {
        //Constants


        public SpotifyTokenResponseModel returnToken(string json)
        {

            var token = JsonConvert.DeserializeObject<SpotifyTokenResponseModel>(json);
            if (token != null)
            {
                Config.Application["access_token"] = token.AccessToken;
                Config.Application["refresh_token"] = token.RefreshToken;

            }

            return token;
        }


        public string GetAuthenticationUrl()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            var queryString = new Dictionary<string, string?>()
            {
                { "client_id", SpotifyConstants.Client_id },
                { "response_type", "code" },
                { "redirect_uri", SpotifyConstants.RedirectURI },
                { "scope", "user-modify-playback-state playlist-modify-public playlist-modify-private playlist-read-private playlist-read-collaborative  user-top-read user-read-private user-read-email user-read-recently-played ugc-image-upload" },
            };

            try
            {

                //response = await client.GetAsync(new Uri(QueryHelpers.AddQueryString(baseAuthURL + authorize, queryString)));

                //response.EnsureSuccessStatusCode();


                return new Uri(QueryHelpers.AddQueryString(SpotifyConstants.BaseAuthURL + SpotifyConstants.Authorize, queryString)).AbsoluteUri;

            }
            catch (Exception ex)
            {
                var exception = ex;

                return ex.Message;
            }
        }


        public async Task<SpotifyTokenResponseModel> GetAuthenticationToken(string code)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                byte[] base64code = System.Text.Encoding.UTF8.GetBytes($"{SpotifyConstants.Client_id}:{SpotifyConstants.Client_secret}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(base64code));

                var jsonBody = new Dictionary<string, string?>()
                {
                    { "grant_type", "authorization_code" },
                    { "code", code },
                    { "redirect_uri", SpotifyConstants.RedirectURI },
                };

                var content = new FormUrlEncodedContent(jsonBody);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                response = await client.PostAsync(SpotifyConstants.BaseAuthURL + SpotifyConstants.GetTokenCall, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var tokenModel = JsonConvert.DeserializeObject<SpotifyTokenResponseModel>(jsonResponse);
                Config.Application["access_token"] = tokenModel.AccessToken;
                Config.Application["refresh_token"] = tokenModel.RefreshToken;


                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                return tokenModel;
            }
            catch (Exception ex)
            {
                var exception = ex;

                return null;
            }
        }

        public async Task<SpotifyTokenResponseModel> RefreshToken(string refreshToken)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                byte[] base64code = System.Text.Encoding.UTF8.GetBytes($"{SpotifyConstants.Client_id}:{SpotifyConstants.Client_secret}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(base64code));

                var queryBody = new Dictionary<string, string?>()
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", refreshToken },
                };

                var content = new FormUrlEncodedContent(queryBody);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

                response = await client.PostAsync(SpotifyConstants.BaseAuthURL + SpotifyConstants.RefreshTokenCall, content);

                string jsonResponse = await response.Content.ReadAsStringAsync();

                var tokenModel = JsonConvert.DeserializeObject<SpotifyTokenResponseModel>(jsonResponse);
                tokenModel.RefreshToken = refreshToken;
                Config.Application["access_token"] = tokenModel.AccessToken;
                Config.Application["refresh_token"] = tokenModel.RefreshToken;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                return tokenModel;


            }
            catch (Exception ex)
            {
                var exception = ex;
                return null;
            }
        }
    }

}

using SpotifyRestApi.Containers;

namespace SpotifyRestApi
{
    public static class Utilities
    {
        public static string ReturnBearerAccessToken()
        {
            return String.Format("Bearer {0}", Config.Application["access_token"]);
        }

    }
}

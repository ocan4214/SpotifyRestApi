namespace SpotifyRestApi
{
    public struct SpotifyConstants
    {
        public const string BaseURL = "https://api.spotify.com/";
        public const string BaseAuthURL = "https://accounts.spotify.com/";
        public const string Authorize = "authorize?";
        public const string GetTokenCall = "api/token";
        public const string RefreshTokenCall = "api/token";

        public const string RedirectURI = "https://localhost:3034/";
        //Fill them  yourself will be changed to Auth with PKCE
        public const string Client_id = "";
        public const string Client_secret = "";


        //User
        public const string CurrentUserApiCall = "https://api.spotify.com/v1/me";
        public const string CurrentUserTopItem = "https://api.spotify.com/v1/me/top/{0}";

        //Playlist
        public const string GetUserPlaylist = "https://api.spotify.com/v1/users/{0}/playlists";
        public const string AddCustomPlaylistCover = "https://api.spotify.com/v1/playlists/{0}/images";



        //Player
        public const string PlaybackCall = "https://api.spotify.com/v1/me/player/play";
        public const string PauseCall = "https://api.spotify.com/v1/me/player/pause";
        public const string SkipToPreviousSong = "https://api.spotify.com/v1/me/player/next";
        public const string SkipToNextSong = "https://api.spotify.com/v1/me/player/previous";
        public const string SearchSong = "https://api.spotify.com/v1/search";



    }
}

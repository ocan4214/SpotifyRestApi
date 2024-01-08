using SpotifyRestApi.SpotifyModels;

namespace SpotifyRestApi.Interfaces
{
    public interface IApiPlaylistRepository
    {
        /// <summary>
        /// HTTPGET
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        Task<SpotifyUserPlaylistResponse> GetUserPlaylist(string userId, int? limit = 2, int? offset=0);
        /// <summary>
        /// HTTPPUT
        /// </summary>
        /// <param name="playlistID"></param>
        /// <param name="base64Image"></param>
        /// <returns></returns>
        Task<string> AddCustomPlaylistCover(string playlistID, string base64Image);



    }
}

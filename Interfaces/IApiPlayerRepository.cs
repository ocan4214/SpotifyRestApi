using SpotifyRestApi.SpotifyModels.Requests;

namespace SpotifyRestApi.Interfaces
{
    public interface IApiPlayerRepository
    {
        /// <summary>
        /// HTTPPOST
        /// </summary>
        /// <param name="playlistID"></param>
        /// <returns></returns>
        Task<bool> SkipToNextSong(string? device_id);
        /// HTTPPOST
        /// </summary>
        /// <param name="playlistID"></param>
        /// <returns></returns>
        Task<bool> SkipToPreviousSong(string? device_id);
        /// <summary>
        ///  HTTPPUT
        /// </summary>
        /// <param name="spotifyPlaybackRequest"></param>
        /// <returns></returns>
        Task<bool> PlaySong(SpotifyPlaybackRequest? spotifyPlaybackRequest);
        /// <summary>
        ///  HTTPPUT
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        Task<bool> PauseSong(string? device_id);


        Task<bool> SearchAndPlay(string? device_id);


    }
}

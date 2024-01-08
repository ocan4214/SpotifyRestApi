using SpotifyRestApi.SpotifyModels;
using SpotifyRestApi.SpotifyModels.Enums;

namespace SpotifyRestApi.Interfaces
{
    public interface IApiUserRepository
    {

        public Task<SpotifyUserInfoResponse> GetCurrentUserProfile();

        public Task<string> GetUsersTopItems(SpotifyEntityTypeEnum type=SpotifyEntityTypeEnum.artists, SpotifyTimeRangeEnum rangeEnum = SpotifyTimeRangeEnum.Medium, int limit = 1, int offset = 0);

    }
}

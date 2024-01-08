using SpotifyRestApi.SpotifyModels;

namespace SpotifyRestApi.Interfaces
{
    public interface IApiAuthenticationRepository
    {
        string GetAuthenticationUrl();

        Task<SpotifyTokenResponseModel> GetAuthenticationToken(string code);

        Task<SpotifyTokenResponseModel> RefreshToken(string refreshToken);


    }
}

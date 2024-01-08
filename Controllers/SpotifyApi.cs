using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyRestApi.Containers;
using SpotifyRestApi.Interfaces;
using SpotifyRestApi.SpotifyModels;
using SpotifyRestApi.SpotifyModels.Requests;

namespace SpotifyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SpotifyApi : ControllerBase
    {
        private IApiAuthenticationRepository _authenticationRepository;
        private IApiUserRepository _userRepository;
        private IApiPlaylistRepository _playlistRepository;
        private IApiPlayerRepository _playerRepository;
        public SpotifyApi(IApiAuthenticationRepository authenticationRepository, IApiUserRepository userRepository, IApiPlaylistRepository playlistRepository, IApiPlayerRepository playerRepository)
        {
            _authenticationRepository = authenticationRepository;
            _userRepository = userRepository;
            _playlistRepository = playlistRepository;
            _playerRepository = playerRepository;
        }

        [HttpPost(Name = "Login")]
        public string Login()
        {
            var url = _authenticationRepository.GetAuthenticationUrl();

            Config.Application["AuthUrl"] = url;


            return url;
        }

        [HttpPost(Name = "FetchToken")]
        public async Task<IActionResult> FetchToken([FromBody] JObject token)
        {
            if (ModelState.IsValid)
            {
                SpotifyTokenResponseModel tokenResponse = new SpotifyTokenResponseModel();
                HttpContext.Session.SetString("spotify_token", token.ToString());
                try
                {
                    tokenResponse = await _authenticationRepository.GetAuthenticationToken(token["token"].ToString());
                    HttpContext.Session.SetString("spotify_access_token", tokenResponse.AccessToken);
                    HttpContext.Session.SetString("spotify_refresh_token", tokenResponse.RefreshToken);
                }
                catch(Exception ex)
                {
                    if (token["refreshToken"].ToString() != null)
                    {
                        tokenResponse = await _authenticationRepository.RefreshToken(token["refreshToken"].ToString());
                        HttpContext.Session.SetString("spotify_access_token", tokenResponse.AccessToken);
                        HttpContext.Session.SetString("spotify_refresh_token", tokenResponse.RefreshToken);
                        return Ok(tokenResponse);
                    }
                    return BadRequest("Fetch Refresh failed");
                }



                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest("Fetch Outer failed");
            }
        }

        [HttpPost(Name = "RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (ModelState.IsValid)
            {
                var tokenResponse = await _authenticationRepository.RefreshToken(refreshToken);

                HttpContext.Session.SetString("spotify_access_token", tokenResponse.AccessToken);
                HttpContext.Session.SetString("spotify_refresh_token", refreshToken);
                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest("Check Body");
            }
        }


        [HttpPost(Name = "GetUserProfile")]
        public async Task<IActionResult> GetUserProfile()
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _userRepository.GetCurrentUserProfile();
                if(response != null)
                {
                    HttpContext.Session.SetString("spotify_current_user_id", response.ID);

                }

                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }
        [HttpPost(Name = "GetUserTopItems")]
        public async Task<IActionResult> GetUserTopItems([FromBody]SpotifyCurrentUserTopItemsRequest request)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "" && request!=null)
            {
                var response = await _userRepository.GetUsersTopItems(request.SpotifyEntityTypeEnum,request.SpotifyTimeRangeEnum,request.Limit,request.Offset);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }

        [HttpPost(Name = "GetUserPlayList")]
        public async Task<IActionResult> GetUserPlayList([FromBody] string userID)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _playlistRepository.GetUserPlaylist(userID);


                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }

        [HttpPost(Name = "AddCustomPlaylistCover")]
        public async Task<IActionResult> AddCustomPlaylistCover(string playlistID, string base64Image)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _playlistRepository.AddCustomPlaylistCover(playlistID, base64Image);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }

        [HttpPost(Name = "PlaySong")]
        public async Task<IActionResult> PlaySong([FromBody] SpotifyPlaybackRequest? spotifyPlaybackRequest)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "" && spotifyPlaybackRequest != null)
            {
                var response = await _playerRepository.PlaySong(spotifyPlaybackRequest);
                return Ok(response);
            }
            else
            {
                return BadRequest("Unable To play");
            }
        }

        [HttpPost("{songName}")]
        public async Task<IActionResult> SearchAndPlaySong(string songName)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "" )
            {
                var response = await _playerRepository.SearchAndPlay(songName);
                return Ok(response);
            }
            else
            {
                return BadRequest("Unable To play");
            }
        }


        [HttpPost(Name = "PauseSong")]
        public async Task<IActionResult> PauseSong(string? device_id)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _playerRepository.PauseSong(device_id);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }



        [HttpPost(Name = "SkipToNextSong")]
        public async Task<IActionResult> SkipToNextSong(string? device_id)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _playerRepository.SkipToNextSong(device_id);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }


        [HttpPost(Name = "SkipToPreviousSong")]
        public async Task<IActionResult> SkipToPreviousSong(string? device_id)
        {
            if (Config.Application["access_token"] != null || Config.Application["access_token"] != "")
            {
                var response = await _playerRepository.SkipToPreviousSong(device_id);
                return Ok(response);
            }
            else
            {
                return BadRequest("No Token for User");
            }
        }

    }
}

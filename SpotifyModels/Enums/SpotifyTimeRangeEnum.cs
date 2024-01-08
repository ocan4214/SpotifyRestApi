using System.Runtime.Serialization;

namespace SpotifyRestApi.SpotifyModels.Enums
{
    public enum SpotifySearchTypeEnum
    {
        [EnumMember(Value = "album")]
        Album,
        [EnumMember(Value = "artist")]
        Artist,
        [EnumMember(Value = "playlist")]
        Playlist,
        [EnumMember(Value = "track")]
        Track,
        [EnumMember(Value = "show")]
        Show,
        [EnumMember(Value = "episode")]
        Episode,
        [EnumMember(Value = "audiobook")]
        Audiobook,
    }
}

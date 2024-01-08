using System.Runtime.Serialization;

namespace SpotifyRestApi.SpotifyModels.Enums
{
    public enum SpotifyEntityTypeEnum
    {
        [EnumMember(Value = "artists")]
        artists,
        [EnumMember(Value = "tracks")]
        tracks,
    }
}

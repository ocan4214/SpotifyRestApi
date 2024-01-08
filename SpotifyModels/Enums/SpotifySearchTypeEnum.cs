using System.Runtime.Serialization;

namespace SpotifyRestApi.SpotifyModels.Enums
{
    public enum SpotifyTimeRangeEnum
    {
        [EnumMember(Value = "short_term")]
        Short,
        [EnumMember(Value = "medium_term")]
        Medium,
        [EnumMember(Value = "long_term")]
        Long,
    }
}

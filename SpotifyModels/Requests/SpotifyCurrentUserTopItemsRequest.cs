using Microsoft.AspNetCore.Mvc;
using SpotifyRestApi.SpotifyModels.Enums;

namespace SpotifyRestApi.SpotifyModels
{
    public class SpotifyCurrentUserTopItemsRequest
    {

        [ModelBinder(Name = "rangeEnum")]
        public SpotifyTimeRangeEnum SpotifyTimeRangeEnum { get; set; }

        [ModelBinder(Name = "typeEnum")]
        public SpotifyEntityTypeEnum SpotifyEntityTypeEnum { get; set; }
        [ModelBinder(Name = "limit")]
        public int Limit { get; set; }

        [ModelBinder(Name = "offset")]
        public int Offset { get; set; }

    }
}

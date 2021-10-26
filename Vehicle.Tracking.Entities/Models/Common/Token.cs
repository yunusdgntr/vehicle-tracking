using Newtonsoft.Json;

namespace Vehicle.Tracking.Entities.Models.Common
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}

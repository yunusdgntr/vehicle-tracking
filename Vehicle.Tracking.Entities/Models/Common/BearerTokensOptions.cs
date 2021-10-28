namespace Vehicle.Tracking.Entities.Models.Common
{
    public class BearerTokensOptions
    {
        public string Key { set; get; }

        public string Issuer { set; get; }

        public string Audience { set; get; }

        public int AccessTokenExpirationDates { set; get; }

        public int RefreshTokenExpirationDates { set; get; }

        public bool AllowMultipleLoginsFromTheSameUser { set; get; }

        public bool AllowSignoutAllUserActiveClients { set; get; }
    }
}

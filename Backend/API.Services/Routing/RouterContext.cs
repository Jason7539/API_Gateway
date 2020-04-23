namespace API.Services
{
    public class RouterContext
    {
        public string ConnectionString { get; set; }
        public string ApiKey { get; set; }
        public string Team { get; set; }
        public string EndpointUrl { get; set; }
        public RouterContext(string connectionString, string apiKey, string team, string endpointUrl)
        {
            ConnectionString = connectionString;
            ApiKey = apiKey;
            Team = team;
            EndpointUrl = endpointUrl;
        }
    }
}


namespace IdentityServer4.DapperExtension.Entities
{
    public class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public Client Client { get; set; }
    }
}
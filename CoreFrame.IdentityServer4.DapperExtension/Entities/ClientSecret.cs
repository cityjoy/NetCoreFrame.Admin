

namespace IdentityServer4.DapperExtension.Entities
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
    }
}
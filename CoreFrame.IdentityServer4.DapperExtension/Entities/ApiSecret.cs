namespace IdentityServer4.DapperExtension.Entities
{
    public class ApiSecret : Secret
    {
        public ApiResource ApiResource { get; set; }
    }
}
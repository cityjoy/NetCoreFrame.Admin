﻿

namespace IdentityServer4.DapperExtension.Entities
{
    public class ClientProperty
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Client Client { get; set; }
    }
}
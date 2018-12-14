using AutoMapper;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreFrame.IdentityServer.Models
{
    public class ClientMapperProfile : Profile
    {

        public ClientMapperProfile()
        {

            CreateMap<Entity.ClientProperties, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<Entity.Clients, IdentityServer4.Models.Client>()
                .ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

            CreateMap<Entity.ClientCorsOrigins, string>()
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientIdPRestrictions, string>()
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientClaims, Claim>(MemberList.None)
                .ConstructUsing(src => new Claim(src.Type, src.Value))
                .ReverseMap();

            CreateMap<Entity.ClientScopes, string>()
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientPostLogoutRedirectUris, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientRedirectUris, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientGrantTypes, string>()
                .ConstructUsing(src => src.GrantType)
                .ReverseMap()
                .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientSecrets, IdentityServer4.Models.Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

        }
    }

    public class ApiResourcesMapperProfile : Profile
    {

        public ApiResourcesMapperProfile()
        {

            CreateMap<Entity.ClientProperties, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<Entity.Clients, IdentityServer4.Models.Client>()
                .ForMember(dest => dest.ProtocolType, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

            CreateMap<Entity.ClientCorsOrigins, string>()
                .ConstructUsing(src => src.Origin)
                .ReverseMap()
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientIdPRestrictions, string>()
                .ConstructUsing(src => src.Provider)
                .ReverseMap()
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientClaims, Claim>(MemberList.None)
                .ConstructUsing(src => new Claim(src.Type, src.Value))
                .ReverseMap();

            CreateMap<Entity.ClientScopes, string>()
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientPostLogoutRedirectUris, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientRedirectUris, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientGrantTypes, string>()
                .ConstructUsing(src => src.GrantType)
                .ReverseMap()
                .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            CreateMap<Entity.ClientSecrets, IdentityServer4.Models.Secret>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

        }
    }
    public static class ClientMappers
    {
        private static IMapper Mapper { get; }

        static ClientMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }


        public static Client ToModel(this Entity.Clients entity)
        {
            return Mapper.Map<Client>(entity);
        }

        public static Entity.Clients ToEntity(this Client model)
        {
            return Mapper.Map<Entity.Clients>(model);
        }
    }
    public static class ApiResourcesMappers
    {
        private static IMapper Mapper { get; }

        static ApiResourcesMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        public static ApiResource ToModel(this Entity.ApiResources entity)
        {
            return Mapper.Map<ApiResource>(entity);
        }

        public static Entity.ApiResources ToEntity(this ApiResource model)
        {
            return Mapper.Map<Entity.ApiResources>(model);
        }
    }
    public static class IdentityResourcesMappers
    {
        static IdentityResourcesMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static IdentityResource ToModel(this Entity.IdentityResources entity)
        {
            return Mapper.Map<IdentityResource>(entity);
        }

        public static Entity.IdentityResources ToEntity(this IdentityResource model)
        {
            return Mapper.Map<Entity.IdentityResources>(model);
        }
    }  
    public static class PersistedGrantsMappers
    {
        static PersistedGrantsMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static PersistedGrant ToModel(this Entity.PersistedGrants entity)
        {
            return Mapper.Map<PersistedGrant>(entity);
        }

        public static Entity.PersistedGrants ToEntity(this PersistedGrant model)
        {
            return Mapper.Map<Entity.PersistedGrants>(model);
        }
    }
}

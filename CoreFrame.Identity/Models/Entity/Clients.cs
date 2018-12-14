using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// Clients
    /// </summary>
	public class Clients 
	{
	
      #region Columns
      	/// <summary>
		/// Id
        /// </summary>				
       public int Id
        {
            get;
            set;
        }        
		/// <summary>
		/// AbsoluteRefreshTokenLifetime
        /// </summary>				
       public int AbsoluteRefreshTokenLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// AccessTokenLifetime
        /// </summary>				
       public int AccessTokenLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// AccessTokenType
        /// </summary>				
       public int AccessTokenType
        {
            get;
            set;
        }        
		/// <summary>
		/// AllowAccessTokensViaBrowser
        /// </summary>				
       public bool AllowAccessTokensViaBrowser
        {
            get;
            set;
        }        
		/// <summary>
		/// AllowOfflineAccess
        /// </summary>				
       public bool AllowOfflineAccess
        {
            get;
            set;
        }        
		/// <summary>
		/// AllowPlainTextPkce
        /// </summary>				
       public bool AllowPlainTextPkce
        {
            get;
            set;
        }        
		/// <summary>
		/// AllowRememberConsent
        /// </summary>				
       public bool AllowRememberConsent
        {
            get;
            set;
        }        
		/// <summary>
		/// AlwaysIncludeUserClaimsInIdToken
        /// </summary>				
       public bool AlwaysIncludeUserClaimsInIdToken
        {
            get;
            set;
        }        
		/// <summary>
		/// AlwaysSendClientClaims
        /// </summary>				
       public bool AlwaysSendClientClaims
        {
            get;
            set;
        }        
		/// <summary>
		/// AuthorizationCodeLifetime
        /// </summary>				
       public int AuthorizationCodeLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// BackChannelLogoutSessionRequired
        /// </summary>				
       public bool BackChannelLogoutSessionRequired
        {
            get;
            set;
        }        
		/// <summary>
		/// BackChannelLogoutUri
        /// </summary>				
       public string BackChannelLogoutUri
        {
            get;
            set;
        }        
		/// <summary>
		/// ClientClaimsPrefix
        /// </summary>				
       public string ClientClaimsPrefix
        {
            get;
            set;
        }        
		/// <summary>
		/// ClientId
        /// </summary>				
       public string ClientId
        {
            get;
            set;
        }        
		/// <summary>
		/// ClientName
        /// </summary>				
       public string ClientName
        {
            get;
            set;
        }        
		/// <summary>
		/// ClientUri
        /// </summary>				
       public string ClientUri
        {
            get;
            set;
        }        
		/// <summary>
		/// ConsentLifetime
        /// </summary>				
       public int ConsentLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// Description
        /// </summary>				
       public string Description
        {
            get;
            set;
        }        
		/// <summary>
		/// EnableLocalLogin
        /// </summary>				
       public bool EnableLocalLogin
        {
            get;
            set;
        }        
		/// <summary>
		/// Enabled
        /// </summary>				
       public bool Enabled
        {
            get;
            set;
        }        
		/// <summary>
		/// FrontChannelLogoutSessionRequired
        /// </summary>				
       public bool FrontChannelLogoutSessionRequired
        {
            get;
            set;
        }        
		/// <summary>
		/// FrontChannelLogoutUri
        /// </summary>				
       public string FrontChannelLogoutUri
        {
            get;
            set;
        }        
		/// <summary>
		/// IdentityTokenLifetime
        /// </summary>				
       public int IdentityTokenLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// IncludeJwtId
        /// </summary>				
       public bool IncludeJwtId
        {
            get;
            set;
        }        
		/// <summary>
		/// LogoUri
        /// </summary>				
       public string LogoUri
        {
            get;
            set;
        }        
		/// <summary>
		/// PairWiseSubjectSalt
        /// </summary>				
       public string PairWiseSubjectSalt
        {
            get;
            set;
        }        
		/// <summary>
		/// ProtocolType
        /// </summary>				
       public string ProtocolType
        {
            get;
            set;
        }        
		/// <summary>
		/// RefreshTokenExpiration
        /// </summary>				
       public int RefreshTokenExpiration
        {
            get;
            set;
        }        
		/// <summary>
		/// RefreshTokenUsage
        /// </summary>				
       public int RefreshTokenUsage
        {
            get;
            set;
        }        
		/// <summary>
		/// RequireClientSecret
        /// </summary>				
       public bool RequireClientSecret
        {
            get;
            set;
        }        
		/// <summary>
		/// RequireConsent
        /// </summary>				
       public bool RequireConsent
        {
            get;
            set;
        }        
		/// <summary>
		/// RequirePkce
        /// </summary>				
       public bool RequirePkce
        {
            get;
            set;
        }        
		/// <summary>
		/// SlidingRefreshTokenLifetime
        /// </summary>				
       public int SlidingRefreshTokenLifetime
        {
            get;
            set;
        }        
		/// <summary>
		/// UpdateAccessTokenClaimsOnRefresh
        /// </summary>				
       public bool UpdateAccessTokenClaimsOnRefresh
        {
            get;
            set;
        }
        public List<ClientGrantTypes> AllowedGrantTypes { get; internal set; }
        public List<ClientRedirectUris> RedirectUris { get; internal set; }
        public List<ClientScopes> AllowedScopes { get; internal set; }
        public List<ClientSecrets> ClientSecrets { get; internal set; }
        #endregion


    }
}


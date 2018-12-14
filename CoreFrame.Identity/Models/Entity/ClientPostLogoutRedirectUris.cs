using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientPostLogoutRedirectUris
    /// </summary>
	public class ClientPostLogoutRedirectUris 
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
		/// ClientId
        /// </summary>				
       public int ClientId
        {
            get;
            set;
        }        
		/// <summary>
		/// PostLogoutRedirectUri
        /// </summary>				
       public string PostLogoutRedirectUri
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientRedirectUris
    /// </summary>
	public class ClientRedirectUris 
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
		/// RedirectUri
        /// </summary>				
       public string RedirectUri
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


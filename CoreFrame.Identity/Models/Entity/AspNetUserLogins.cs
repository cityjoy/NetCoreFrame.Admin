using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// AspNetUserLogins
    /// </summary>
	public class AspNetUserLogins 
	{
	
      #region Columns
      	/// <summary>
		/// LoginProvider
        /// </summary>				
       public string LoginProvider
        {
            get;
            set;
        }        
		/// <summary>
		/// ProviderKey
        /// </summary>				
       public string ProviderKey
        {
            get;
            set;
        }        
		/// <summary>
		/// ProviderDisplayName
        /// </summary>				
       public string ProviderDisplayName
        {
            get;
            set;
        }        
		/// <summary>
		/// UserId
        /// </summary>				
       public string UserId
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


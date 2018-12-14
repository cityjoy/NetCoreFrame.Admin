using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// AspNetUserTokens
    /// </summary>
	public class AspNetUserTokens 
	{
	
      #region Columns
      	/// <summary>
		/// UserId
        /// </summary>				
       public string UserId
        {
            get;
            set;
        }        
		/// <summary>
		/// LoginProvider
        /// </summary>				
       public string LoginProvider
        {
            get;
            set;
        }        
		/// <summary>
		/// Name
        /// </summary>				
       public string Name
        {
            get;
            set;
        }        
		/// <summary>
		/// Value
        /// </summary>				
       public string Value
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// AspNetUserClaims
    /// </summary>
	public class AspNetUserClaims 
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
		/// ClaimType
        /// </summary>				
       public string ClaimType
        {
            get;
            set;
        }        
		/// <summary>
		/// ClaimValue
        /// </summary>				
       public string ClaimValue
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


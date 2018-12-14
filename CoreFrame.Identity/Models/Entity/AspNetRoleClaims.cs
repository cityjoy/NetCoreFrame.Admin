using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// AspNetRoleClaims
    /// </summary>
	public class AspNetRoleClaims 
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
		/// RoleId
        /// </summary>				
       public string RoleId
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// AspNetRoles
    /// </summary>
	public class AspNetRoles 
	{
	
      #region Columns
      	/// <summary>
		/// Id
        /// </summary>				
       public string Id
        {
            get;
            set;
        }        
		/// <summary>
		/// ConcurrencyStamp
        /// </summary>				
       public string ConcurrencyStamp
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
		/// NormalizedName
        /// </summary>				
       public string NormalizedName
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


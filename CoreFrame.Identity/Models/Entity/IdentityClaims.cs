using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// IdentityClaims
    /// </summary>
	public class IdentityClaims 
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
		/// IdentityResourceId
        /// </summary>				
       public int IdentityResourceId
        {
            get;
            set;
        }        
		/// <summary>
		/// Type
        /// </summary>				
       public string Type
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


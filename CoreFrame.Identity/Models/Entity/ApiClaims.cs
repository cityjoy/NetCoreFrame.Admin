using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ApiClaims
    /// </summary>
	public class ApiClaims 
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
		/// ApiResourceId
        /// </summary>				
       public int ApiResourceId
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


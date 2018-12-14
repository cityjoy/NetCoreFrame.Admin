using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ApiSecrets
    /// </summary>
	public class ApiSecrets 
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
		/// Description
        /// </summary>				
       public string Description
        {
            get;
            set;
        }        
		/// <summary>
		/// Expiration
        /// </summary>				
       public DateTime Expiration
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


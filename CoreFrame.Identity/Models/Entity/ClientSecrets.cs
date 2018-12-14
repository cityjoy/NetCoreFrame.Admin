using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientSecrets
    /// </summary>
	public class ClientSecrets 
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


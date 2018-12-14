using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ApiScopes
    /// </summary>
	public class ApiScopes 
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
		/// DisplayName
        /// </summary>				
       public string DisplayName
        {
            get;
            set;
        }        
		/// <summary>
		/// Emphasize
        /// </summary>				
       public bool Emphasize
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
		/// Required
        /// </summary>				
       public bool Required
        {
            get;
            set;
        }        
		/// <summary>
		/// ShowInDiscoveryDocument
        /// </summary>				
       public bool ShowInDiscoveryDocument
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


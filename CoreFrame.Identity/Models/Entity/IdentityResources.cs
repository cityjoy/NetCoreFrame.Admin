using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// IdentityResources
    /// </summary>
	public class IdentityResources 
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
		/// Enabled
        /// </summary>				
       public bool Enabled
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


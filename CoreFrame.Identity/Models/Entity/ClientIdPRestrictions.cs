using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientIdPRestrictions
    /// </summary>
	public class ClientIdPRestrictions 
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
		/// Provider
        /// </summary>				
       public string Provider
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


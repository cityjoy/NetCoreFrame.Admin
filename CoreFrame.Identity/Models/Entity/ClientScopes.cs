using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientScopes
    /// </summary>
	public class ClientScopes 
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
		/// Scope
        /// </summary>				
       public string Scope
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


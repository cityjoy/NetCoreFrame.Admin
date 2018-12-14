using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientGrantTypes
    /// </summary>
	public class ClientGrantTypes 
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
		/// GrantType
        /// </summary>				
       public string GrantType
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


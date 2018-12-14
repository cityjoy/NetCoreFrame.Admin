using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientCorsOrigins
    /// </summary>
	public class ClientCorsOrigins 
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
		/// Origin
        /// </summary>				
       public string Origin
        {
            get;
            set;
        }        
		#endregion
       
 
	}
}


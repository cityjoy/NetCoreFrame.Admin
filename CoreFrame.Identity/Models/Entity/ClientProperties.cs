using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ClientProperties
    /// </summary>
	public class ClientProperties 
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
		/// Key
        /// </summary>				
       public string Key
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


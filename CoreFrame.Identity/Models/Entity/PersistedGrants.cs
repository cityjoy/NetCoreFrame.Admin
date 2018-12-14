using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// PersistedGrants
    /// </summary>
	public class PersistedGrants 
	{
	
      #region Columns
      	/// <summary>
		/// Key
        /// </summary>				
       public string Key
        {
            get;
            set;
        }        
		/// <summary>
		/// ClientId
        /// </summary>				
       public string ClientId
        {
            get;
            set;
        }        
		/// <summary>
		/// CreationTime
        /// </summary>				
       public DateTime CreationTime
        {
            get;
            set;
        }        
		/// <summary>
		/// Data
        /// </summary>				
       public string Data
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
		/// SubjectId
        /// </summary>				
       public string SubjectId
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


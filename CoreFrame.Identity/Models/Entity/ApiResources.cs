using System;
using System.Collections.Generic;
using System.Text;
namespace CoreFrame.IdentityServer.Models.Entity
{
	/// <summary>
    /// ApiResources
    /// </summary>
	public class ApiResources 
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
        /// 
        /// </summary>
        public List<ApiScopes> Scopes { get; internal set; }
        #endregion


    }
}


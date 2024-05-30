using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.ApiModels
{
    /// <summary>
    /// The response for all Web API calls made
    /// </summary>
    public class ApiResponseModel
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the API call was successful
        /// </summary>
        public bool Successful => ErrorMessage == null;
        
        /// <summary>
        /// The error message for a failed API call
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The API response object
        /// </summary>
        public object Response { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiResponseModel()
        {

        }

        #endregion

    }

    /// <summary>
    /// The response for all Web API calls made with a specific type of known response
    /// </summary>
    /// <typeparam name="T">The specific type of server response</typeparam>
    public class ApiResponseModel<T> : ApiResponseModel
    {
        /// <summary>
        /// The API response object as type T
        /// </summary>
        public new T Response 
        {
            get => (T) base.Response;
            set => base.Response = value;
        }
    }
}

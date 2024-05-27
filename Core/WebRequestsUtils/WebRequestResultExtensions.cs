using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;

using ASPNet_WPF_ChatApp.Core.InversionOfControl.Base;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.Core.ViewModels.Dialogs;


namespace ASPNet_WPF_ChatApp.Core.WebRequestUtils
{
    /// <summary>
    /// Extension methods for the <see cref="ApiResponseModel{T}"/> class
    /// </summary>
    public static class WebRequestResultExtensions
    {
        /// <summary>
        /// Checks the web request result for any errors, displaying themif there are any.
        /// </summary>
        /// <typeparam name="T">The type of the API response</typeparam>
        /// <param name="response">The resposne to check</param>
        /// <param name="title">The title of the error dialog if there is an error</param>
        /// <returns>True if there were errors, or false if all was ok</returns>
        public static async Task<bool> DisplayErrorIfFailedAsync<T>(this WebRequestResult<ApiResponseModel<T>> response, string title)
        {
            // If there was no response, bad data, or a response with an error message...
            if (response == null || response.ServerResponse == null || !response.ServerResponse.Successful)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unknown error from server call.";

                // If we got a response from the server...
                if (response?.ServerResponse != null)
                {
                    // Set message to the server's response
                    message = response.ServerResponse.ErrorMessage;
                }

                // If we have a response, but deserialization failed...
                else if (!string.IsNullOrWhiteSpace(response?.RawServerResponse))
                {
                    // Set message to the raw server response
                    message = $"Unexpected response from server: \"{response.RawServerResponse}\"";
                }

                // If we have a response, but no server response details at all...
                else if (response != null)
                {
                    // Set message to standard HTTP server response details
                    message = $"Failed to communicate with server. Status code {response.StatusCode}. \"{response.StatusDescription}\"";
                }


                // Display error
                await IoC.UI.ShowMessage(new MessageBoxDialogViewModel
                {
                    // TODO: Localize strings
                    Title = title,
                    Message = message
                });

                // Return that we had an error
                return true;
            }

            // All was ok, so return false for no error
            return false;
        }
    }
}

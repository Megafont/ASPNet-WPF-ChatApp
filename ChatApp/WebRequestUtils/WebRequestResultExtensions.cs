using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dna;

using ASPNet_WPF_ChatApp.Core.DependencyInjection;
using ASPNet_WPF_ChatApp.Core.ApiModels;
using ASPNet_WPF_ChatApp.ViewModels.Dialogs;

// This makes it so we can access members on this static class without needing to write "ChatAppDI." first.
using static ASPNet_WPF_ChatApp.DependencyInjection.ChatAppDI;

namespace ASPNet_WPF_ChatApp.WebRequestUtils
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
        public static async Task<bool> DisplayErrorIfFailedAsync(this WebRequestResult response, string title)
        {
            // If there was no response, bad data, or a response with an error message...
            if (response == null || response.ServerResponse == null || (response.ServerResponse as ApiResponseModel)?.Successful == false)
            {
                // Default error message
                // TODO: Localize strings
                var message = "Unknown error from server call.";

                // If we got a response from the server...
                if (response?.ServerResponse is ApiResponseModel apiResponse)
                {
                    // Set message to the server's response
                    message = apiResponse.ErrorMessage;
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
                    message = response.ErrorMessage ?? $"{response.StatusDescription} - Status code: {response.StatusCode}";
                }


                // Display error
                await UI.ShowMessage(new MessageBoxDialogViewModel
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

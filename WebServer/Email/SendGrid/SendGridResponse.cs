namespace ASPNet_WPF_ChatApp.WebServer.Email.SendGrid
{
    /// <summary>
    /// A response to a SendMessageAsync() call
    /// </summary>
    public class SendGridResponse
    {
        /// <summary>
        /// Any errors from a response
        /// </summary>
        public List<SendGridResponseError> Errors { get; set; }


    }
}

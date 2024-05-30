using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNet_WPF_ChatApp.Core.Email
{
    /// <summary>
    /// The details of an email to be sent
    /// </summary>
    public class SendEmailDetails
    {
        /// <summary>
        /// The name of the sender
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// The email of the sender
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// The name of the recipient
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// The email of the recipient
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// The subject of the email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The email body content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Indicates if the content is an HTML email
        /// </summary>
        public bool IsHTML { get; set; }
    }
}

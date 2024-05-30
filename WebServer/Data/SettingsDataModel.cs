using System.ComponentModel.DataAnnotations;

namespace ASPNet_WPF_ChatApp.WebServer.Data
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class SettingsDataModel
    {
        /// <summary>
        /// The unique Id for this entry
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// The setting's name
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// The setting's value
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public string Value { get; set; }
    }
}

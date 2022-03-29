using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CongestionTaxInputModel
    {
        /// <summary>
        ///     Vehicle Registration number.
        /// </summary>
        [Required]
        public string VehicleRegistration { get; set; }

        /// <summary>
        ///     An array of timestamps.
        /// </summary>
        [Required]
        public string[] Timestamp { get; set; }
    }
}
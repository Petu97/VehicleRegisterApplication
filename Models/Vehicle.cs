using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegisterApplication.Models
{
    public abstract class Vehicle
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("brand")]
        [Required]
        [StringLength(20)]
        public string vehicleBrand { get; set; }

        [JsonProperty("model")]
        [StringLength(20)]
        public string vehicleModel { get; set; }

        [JsonProperty("register")]
        [StringLength(10)]
        public string lisencePlateNumber { get; set; }

        [JsonProperty("vehicleOwner")]
        [StringLength(30)]
        public string owner { get; set; }

        [JsonProperty("wheels")]
        [Range(2, 16)]
        public int numOfWheels { get; set; }
    }
}

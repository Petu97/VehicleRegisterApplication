using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegisterApplication.Models
{
    public class Truck : Vehicle
    {
        [JsonProperty("truckType")]
        [Required]
        public TruckType truckType;

        [JsonProperty("cargoCapacity")]
        [Required]
        [Range(0, 500)] //yea i got no idea how big trucks can get
        public float cargoCapacity; 
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VehicleRegisterApplication.Models
{
    public class Vehicle
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("brand")]
        public string vehicleBrand { get; set; }

        [JsonProperty("model")]
        public string vehicleModel { get; set; }

        [JsonProperty("register")]
        public string lisencePlateNumber { get; set; }

        [JsonProperty("vehicleOwner")]
        public string owner { get; set; }

        [JsonProperty("wheels")]
        public int numOfWheels { get; set; }
    }
}

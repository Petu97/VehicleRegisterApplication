using Newtonsoft.Json;

namespace VehicleRegisterApplication.Models
{
    public class Truck : Vehicle
    {
        [JsonProperty("truckType")]
        public TruckType truckType;

        [JsonProperty("cargoCapacity")]
        public float cargoCapacity; 
    }
}

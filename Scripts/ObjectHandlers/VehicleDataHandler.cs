using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegisterApplication.Models;
using Newtonsoft.Json;
//using VehicleRegisterApplication.Scripts;

namespace VehicleRegisterApplication.Scripts.ObjectHandlers
{
    public class VehicleDataHandler
    {
        private readonly VehicleIdHandler vehicleIdHandler;

        public VehicleDataHandler(VehicleIdHandler vehicleIdHandler)
        {
            this.vehicleIdHandler = vehicleIdHandler;
        }

        public async Task<List<Car>> GetCars() => JsonConvert.DeserializeObject<List<Car>>(System.IO.File.ReadAllText(Data.carJSONPath));
        public async Task<List<Truck>> GetTrucks() => JsonConvert.DeserializeObject<List<Truck>>(System.IO.File.ReadAllText(Data.truckJSONPath));
        public async Task<List<Motorcycle>> GetMotorcycles() => JsonConvert.DeserializeObject<List<Motorcycle>>(System.IO.File.ReadAllText(Data.motorcycleJSONPath));

        public async Task<bool> CreateCar(Car newCar) //create a new car and add it to the dbfile
        {
            List<Car> carList = await GetCars();
            newCar.numOfWheels = 4;
            newCar.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newCar is null)
            {
                return false;
            }

            else
            {
                vehicleIdHandler.AddId();
                carList.Add(newCar);
                File.WriteAllText(Data.carJSONPath, JsonConvert.SerializeObject(carList));
                return true;
            }
        }

        public async Task<bool> CreateMotorcycle(Motorcycle newMotorcycle)
        {
            List<Motorcycle> motorcyclesList = await GetMotorcycles();
            newMotorcycle.numOfWheels = 4;
            newMotorcycle.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newMotorcycle is null)
            {
                return false;
            }

            else
            {
                vehicleIdHandler.AddId();
                motorcyclesList.Add(newMotorcycle);
                File.WriteAllText(Data.motorcycleJSONPath, JsonConvert.SerializeObject(motorcyclesList));
                return true;
            }
        }

        public async Task<bool> CreateTruck(Truck newTruck)
        {
            List<Truck> truckList = await GetTrucks();
            newTruck.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newTruck is null)
            {
                return false;
            }

            else
            {
                vehicleIdHandler.AddId();
                truckList.Add(newTruck);
                File.WriteAllText(Data.truckJSONPath, JsonConvert.SerializeObject(truckList));
                return true;
            }
        }

        private void SaveVehicle()
        {

        }
    }
}

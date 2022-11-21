using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegisterApplication.Models;
using Newtonsoft.Json;

namespace VehicleRegisterApplication.Scripts.ObjectHandlers
{
    /*Class for handling all requests that require access to vehicle datafiles. */
    public class VehicleDataHandler
    {
        private readonly VehicleIdHandler vehicleIdHandler;

        //inject dataidhandler class for managing vehicle ids
        public VehicleDataHandler(VehicleIdHandler vehicleIdHandler)
        {
            this.vehicleIdHandler = vehicleIdHandler;
        }

        //next 3 methods return vehicledata from respective datafiles. Returns data as a list of derived vehicle type 
        public async Task<List<Car>> GetCars() => JsonConvert.DeserializeObject<List<Car>>(System.IO.File.ReadAllText(Data.carJSONPath));
        public async Task<List<Truck>> GetTrucks() => JsonConvert.DeserializeObject<List<Truck>>(System.IO.File.ReadAllText(Data.truckJSONPath));
        public async Task<List<Motorcycle>> GetMotorcycles() => JsonConvert.DeserializeObject<List<Motorcycle>>(System.IO.File.ReadAllText(Data.motorcycleJSONPath));


        //Creates a new car and adds it to datafile. Returns true if vehicle is created successfully
        public async Task<bool> CreateCar(Car newCar) //create a new car and add it to the dbfile
        {
            List<Car> carList = await GetCars();
            newCar.numOfWheels = 4;
            newCar.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newCar is null) //check vehicle values for errors
            {
                return false;
            }

            else //adds the new vehicle to list and saves it
            {
                vehicleIdHandler.AddId();   
                carList.Add(newCar);
                File.WriteAllText(Data.carJSONPath, JsonConvert.SerializeObject(carList));
                return true;
            }
        }

        //Creates a new motorcycle and adds it to datafile. Returns true if vehicle is created successfully
        public async Task<bool> CreateMotorcycle(Motorcycle newMotorcycle)
        {
            List<Motorcycle> motorcyclesList = await GetMotorcycles();
            newMotorcycle.numOfWheels = 4;
            newMotorcycle.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newMotorcycle is null) //check vehicle values for errors
            {
                return false;
            }

            else //adds the new vehicle to list and saves it
            {
                vehicleIdHandler.AddId();
                motorcyclesList.Add(newMotorcycle);
                File.WriteAllText(Data.motorcycleJSONPath, JsonConvert.SerializeObject(motorcyclesList));
                return true;
            }
        }

        //Creates a new truck and adds it to datafile. Returns true if vehicle is created successfully
        public async Task<bool> CreateTruck(Truck newTruck)
        {
            List<Truck> truckList = await GetTrucks();
            newTruck.id = vehicleIdHandler.GetCurrentId() + 1;
            if (newTruck is null) //check vehicle values for errors
            {
                return false;
            }

            else //adds the new vehicle to list and saves it
            {
                vehicleIdHandler.AddId();
                truckList.Add(newTruck);
                File.WriteAllText(Data.truckJSONPath, JsonConvert.SerializeObject(truckList));
                return true;
            }
        }

        //TODO: Edit and delete methods. 
    }
}

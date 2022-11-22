using System;
using System.Collections.Generic;
using System.Text;
using VehicleRegisterApplication.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;

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

        //next 3 methods override vehicledata in datafiles. Used for modifying datafiles
        private async Task SaveCarData(List<Car> carData) => File.WriteAllText(Data.carJSONPath, JsonConvert.SerializeObject(carData));
        private async Task SaveTruckData(List<Truck> truckData) => File.WriteAllText(Data.truckJSONPath, JsonConvert.SerializeObject(truckData));
        private async Task SaveMotorcycleData(List<Motorcycle> motorcycleData) => File.WriteAllText(Data.motorcycleJSONPath, JsonConvert.SerializeObject(motorcycleData));


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
                await SaveCarData(carList);
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
                await SaveMotorcycleData(motorcyclesList);
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
                await SaveTruckData(truckList);
                return true;
            }
        }

        public async Task<bool> DeleteVehicle(int vehicleId) //search for vehicle in data files by given id and delete it
        {
            List<Car> carData = await GetCars(); //searches for vehicle in carlist by id
            Car? car = carData.Find(veh => veh.id == vehicleId);
            if (car is not null) //delete car from list if it's found
            {
                carData.Remove(car);
                await SaveCarData(carData);
                return true;
            }

            List<Truck> truckData = await GetTrucks(); //searches for vehicle in trucklist by id
            Truck? truck = truckData.Find(veh => veh.id == vehicleId);
            if (truck is not null) //delete truck from list if it's found
            {
                truckData.Remove(truck);
                await SaveTruckData(truckData);
                return true;
            }
         
            List<Motorcycle> motorcycleData = await GetMotorcycles(); //searches for vehicle in motorcyclelist by id
            Motorcycle? motorcycle = motorcycleData.Find(veh => veh.id == vehicleId);
            if (motorcycle is not null) //delete motorcycle from list if it's found
            {
                motorcycleData.Remove(motorcycle);
                await SaveMotorcycleData(motorcycleData);
                return true;
            }
                
            else return false;
        }

        //TODO: Edit methods. 
    }
}

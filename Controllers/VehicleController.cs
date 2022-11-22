using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRegisterApplication.Models;
using Newtonsoft.Json;
using VehicleRegisterApplication.Scripts.ObjectHandlers;
using VehicleRegisterApplication.Scripts;
using System.Threading.Tasks;


namespace VehicleRegisterApplication.Controllers
{
    //mvc controller for vehicle subjected calls
    public class VehicleController : Controller
    {
        private readonly VehicleDataHandler vehicleDataHandler; //Handler-object that handles requests that require access to vehicledatafiles 
        public VehicleController(VehicleDataHandler vehicleCreator)
        {
            this.vehicleDataHandler = vehicleCreator;
        }

        [HttpGet]
        [Route("Vehicle/Index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("Vehicle/DeleteVehicle")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await vehicleDataHandler.DeleteVehicle(id);
            if (result) return View();
            else return NotFound();
        }

        [HttpGet]
        [Route("Vehicle/CreateCar")]
        public async Task<IActionResult> CreateCar()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCar([Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner")] Car car) 
        {

            try
            {
                vehicleDataHandler.CreateCar(car);
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Vehicle/CreateMotorcycle")]
        public async Task<IActionResult> CreateMotorcycle()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMotorcycle([Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner")] Motorcycle motorcycle)
        {

            try
            {
                vehicleDataHandler.CreateMotorcycle(motorcycle);
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("Vehicle/CreateTruck")]
        public async Task<IActionResult> CreateTruck()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTruck([Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner,numOfWheels,truckType,cargoCapacity")] Truck truck)
        {

            try
            {
                vehicleDataHandler.CreateTruck(truck);
                return RedirectToAction("Index");
            }
            catch
            {
                return NotFound();
            }
        }

        /* methods for printing out list of vehicles of type
         * these next 3 methods could probably be implemented as one, but i prefer static addresses for each of them for easier access for the user*/
        [HttpGet]
        [Route("Vehicle/Cars")]
        public async Task<IActionResult> Cars() //returns list of cars to client
        {
            List<Car> vehicleData = await vehicleDataHandler.GetCars();
            if (vehicleData is not null) return View(vehicleData);

            else return NotFound();
        }

        [HttpGet]
        [Route("Vehicle/Trucks")]
        public async Task<IActionResult> Trucks() //returns list of trucks to client
        {
            List<Truck> vehicleData = await vehicleDataHandler.GetTrucks();
            if (vehicleData is not null) return View(vehicleData);

            else return NotFound();
        }

        [HttpGet]
        [Route("Vehicle/Motorcycles")]
        public async Task<IActionResult> Motorcycles() //returns list of motorcycles to client
        {
            List<Motorcycle> vehicleData = await vehicleDataHandler.GetMotorcycles();
            if (vehicleData is not null) return View(vehicleData);

            else return NotFound();
        }


        [Route("Vehicle/VehicleInfo/{id?}")]
        public async Task<IActionResult> VehicleInfo(int? id) //searches and returns specific vehicledata to client. This method is obviously inefficient and dum because we're not working with a proper db.
        {
            List<Car>? carData = await vehicleDataHandler.GetCars(); //searches for vehicle with given id from car data
            Car? car = carData.Find(veh => veh.id == id);
            if (car is not null)
                return View(car);

            List<Truck>? truckData = await vehicleDataHandler.GetTrucks(); //searches for vehicle with given id from truck data
            Truck? truck = truckData.Find(veh => veh.id == id);
            if (truck is not null)
                return View(truck);

            List<Motorcycle>? motorcycleData = await vehicleDataHandler.GetMotorcycles(); //searches for vehicle with given id from truck data
            Motorcycle? motorcycle = motorcycleData.Find(veh => veh.id == id);
            if (motorcycle is not null)
                return View(motorcycle);

            else
                return NotFound(); //return vehicle not found
        }
    }
}

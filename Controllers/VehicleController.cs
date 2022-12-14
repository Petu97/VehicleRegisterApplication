using Microsoft.AspNetCore.Mvc;
using System;
using VehicleRegisterApplication.Models;
using Newtonsoft.Json;
using VehicleRegisterApplication.Scripts.ObjectHandlers;
using VehicleRegisterApplication.Scripts;
using System.Threading.Tasks;
using System.Configuration;

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
        [Route("Vehicle/EditVehicle")]
        public async Task<IActionResult> EditVehicle(int id) //edit vehicle method. Can only pass a single model to any given view so each type of edit viwe is different. Wish these could be fused together in one view
        {
            List<Car>? carData = await vehicleDataHandler.GetCars(); //searches for vehicle with given id from car data
            Car? car = carData.Find(veh => veh.id == id);
            if (car is not null)
                return View("EditCar", car);

            List<Truck>? truckData = await vehicleDataHandler.GetTrucks(); //searches for vehicle with given id from truck data
            Truck? truck = truckData.Find(veh => veh.id == id);
            if (truck is not null)
                return View("EditTruck", truck);

            List<Motorcycle>? motorcycleData = await vehicleDataHandler.GetMotorcycles(); //searches for vehicle with given id from truck data
            Motorcycle? motorcycle = motorcycleData.Find(veh => veh.id == id);
            if (motorcycle is not null)
                return View("EditMotorcycle", motorcycle);

            else
                return NotFound(); //return vehicle not found
        }

        [HttpPost]
        [Route("Vehicle/EditCar")]
        public async Task<IActionResult> EditCar(int id, [Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner")] Car updatedCar)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await vehicleDataHandler.EditCar(id, updatedCar);
            if (result) return View("Index");

            else return NotFound();
        }

        [HttpPost]
        [Route("Vehicle/EditMotorcycle")]
        public async Task<IActionResult> EditMotorcycle(int id, [Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner")] Motorcycle updatedMotorcycle)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await vehicleDataHandler.EditMotorcycle(id, updatedMotorcycle);
            if (result) return View("Index");

            else return NotFound();
        }

        [HttpPost]
        [Route("Vehicle/EditTruck")]
        public async Task<IActionResult> EditTruck(int id, [Bind("vehicleBrand,vehicleModel,lisencePlateNumber,owner,numOfWheels,truckType,cargoCapacity")] Truck updatedTruck)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await vehicleDataHandler.EditTruck(id, updatedTruck);
            if (result) return View("Index");

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
            if (ModelState.IsValid)
            {
                var result = await vehicleDataHandler.CreateCar(car);
                if (result)
                    return CreatedAtAction("Car created successfully", car); //code 201 created at action
                else
                    return StatusCode(500, "Server error: failed to fetch truckdata"); //error: 500. internal server error
            }
            else
                return BadRequest("User input not valid. Check input for any forbidden characters"); //error 400. Bad request
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
                return Content("");
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

            else return StatusCode(500, "Server error: failed to fetch cardata"); //error: 500. internal server error
        }

        [HttpGet]
        [Route("Vehicle/Trucks")]
        public async Task<IActionResult> Trucks() //returns list of trucks to client
        {
            List<Truck> vehicleData = await vehicleDataHandler.GetTrucks();
            if (vehicleData is not null) return View(vehicleData);

            else return StatusCode(500, "Server error: failed to fetch truckdata"); //error: 500. internal server error
        }

        [HttpGet]
        [Route("Vehicle/Motorcycles")]
        public async Task<IActionResult> Motorcycles() //returns list of motorcycles to client
        {
            List<Motorcycle> vehicleData = await vehicleDataHandler.GetMotorcycles();
            if (vehicleData is not null) return View(vehicleData);

            else return StatusCode(500, "Server error: failed to fetch motorcycledata"); //error: 500. internal server error
        }

        [HttpGet]
        [Route("Vehicle/VehicleInfo/{id?}")]
        public async Task<IActionResult> VehicleInfo(int? id) //searches and returns specific vehicledata to client. This method is obviously inefficient and dum because we're not working with a proper db.
        {
            if (id is not int)
                BadRequest();

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
                return NotFound("Sorry, couldn't find any item with id of: " + id); //return vehicle not found error, error: 404
        }
    }
}

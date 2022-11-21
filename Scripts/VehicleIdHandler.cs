using System;

namespace VehicleRegisterApplication.Scripts
{
    /*this class handles the identetity number for the vehicles. Vehicles cannot share the same id 
    with other vehicles.*/
    public class VehicleIdHandler
    {
        public int GetCurrentId() //returns current id from the savedid file
        {
            string currentId = System.IO.File.ReadAllText(Data.VehicleIdSaveFile);
            int id = Int32.Parse(currentId);
            return id;
        }

        public void AddId() //adds and saves current id with 1 higher value on savefile. Called after a new vehicle is added. 
        {
            string currentId = File.ReadAllText(Data.VehicleIdSaveFile);
            int newId = Int32.Parse(currentId);
            newId++;
            File.WriteAllText(Data.VehicleIdSaveFile, newId.ToString());
        }
    }
}

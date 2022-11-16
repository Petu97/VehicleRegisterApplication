using System;

namespace VehicleRegisterApplication.Scripts
{
    public class VehicleIdHandler
    {
        public int GetCurrentId()
        {
            string currentId = System.IO.File.ReadAllText(Data.VehicleIdSaveFile);
            int id = Int32.Parse(currentId);
            return id;
        }

        public void AddId()
        {
            string currentId = File.ReadAllText(Data.VehicleIdSaveFile);
            int newId = Int32.Parse(currentId);
            newId++;
            File.WriteAllText(Data.VehicleIdSaveFile, newId.ToString());
        }
    }
}

using ComarchBootcamp1.App.Cars.Model;
using System.Text.Json;

namespace ComarchBootcamp1.App.Cars.Repositories;

/// <summary>
/// CRUD
/// </summary>
internal class VehicleRepository
{
    private static List<Vehicle> data = [];

    public List<Vehicle> GetAll()
    {
        if (File.Exists("vehicles.json"))
        {
            string jsonString = File.ReadAllText("vehicles.json");
            data = JsonSerializer.Deserialize<List<Vehicle>>(jsonString)!;
        }
        return data;
    }

    public Vehicle GetVehicle(int id)
    {
        return data.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Vehicle vehicle)
    {
        int id = 0;
        if (data.Any())
            id = data.OrderByDescending(x => x.Id).First().Id;
        vehicle.Id = id + 1;
        vehicle.Type = vehicle.GetType().Name; // Set the vehicle type
        data.Add(vehicle);
        
        string jsonString = JsonSerializer.Serialize(data);
        File.WriteAllText("vehicles.json", jsonString);
    }

    public void Remove(int id)
    {
        Vehicle vehicle = GetVehicle(id);
        data.Remove(vehicle);
    }
}

using ComarchBootcamp1.App.Cars.Model;
using System.Text.Json;

namespace ComarchBootcamp1.App.Cars.Repositories;

/// <summary>
/// CRUD
/// </summary>
internal class VehicleRepository
{
    private static List<Vehicle> _vehicles = [];

    public List<Vehicle> GetAll()
    {
        if (File.Exists("vehicles.json"))
        {
            string jsonString = File.ReadAllText("vehicles.json");
            _vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonString)!;
        }

        return _vehicles;
    }

    private Vehicle? GetVehicle(int id)
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.Id == id);
    }

    public void Add(Vehicle vehicle)
    {
        int id = 0;
        if (_vehicles.Count > 0)
            id = _vehicles.OrderByDescending(x => x.Id).First().Id;
        vehicle.Id = id + 1;
        vehicle.Type = vehicle.GetType().Name; // Set the vehicle type
        _vehicles.Add(vehicle);

        string jsonString = JsonSerializer.Serialize(_vehicles);
        File.WriteAllText("vehicles.json", jsonString);
    }

    public void Remove(int id)
    {
        var vehicle = GetVehicle(id);
        if (vehicle != null)
        {
            _vehicles.Remove(vehicle);

            string jsonString = JsonSerializer.Serialize(_vehicles);
            File.WriteAllText("vehicles.json", jsonString);
            Console.WriteLine("Usunięto pojazd o id: " + id);
        }
        else
        {
            Console.WriteLine("Nie znaleziono pojazdu o podanym id.");
        }
    }
}
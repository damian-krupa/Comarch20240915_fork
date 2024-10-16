﻿using ComarchBootcamp1.App.Cars.Model;
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
        if (!File.Exists("vehicles.json")) return _vehicles;
        var jsonString = File.ReadAllText("vehicles.json");
        _vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonString)!;

        return _vehicles;
    }

    public Vehicle? GetVehicle(int id)
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.Id == id);
    }

    public void Add(Vehicle vehicle)
    {
        var id = 0;
        if (_vehicles.Count > 0)
            id = _vehicles.OrderByDescending(x => x.Id).First().Id;
        vehicle.Id = id + 1;
        vehicle.Type = vehicle.GetType().Name; // Set the vehicle type
        _vehicles.Add(vehicle);

        var jsonString = JsonSerializer.Serialize(_vehicles);
        File.WriteAllText("vehicles.json", jsonString);
    }

    public void Modify(int id, Vehicle updatedVehicle)
    {
        var vehicle = GetVehicle(id);
        if (vehicle == null) return;

        vehicle.Maker = updatedVehicle.Maker;
        vehicle.Model = updatedVehicle.Model;
        vehicle.GasType = updatedVehicle.GasType;
        vehicle.Capacity = updatedVehicle.Capacity;
        vehicle.Type = updatedVehicle.Type;

        var jsonString = JsonSerializer.Serialize(_vehicles);
        File.WriteAllText("vehicles.json", jsonString);
        Console.WriteLine("Zaktualizowano pojazd o id: " + id);
    }

    public void Remove(int id)
    {
        var vehicle = GetVehicle(id);
        if (vehicle != null)
        {
            _vehicles.Remove(vehicle);

            var jsonString = JsonSerializer.Serialize(_vehicles);
            File.WriteAllText("vehicles.json", jsonString);
            Console.WriteLine("Usunięto pojazd o id: " + id);
        }
        else
        {
            Console.WriteLine("Nie znaleziono pojazdu o podanym id.");
        }
    }
    
    public void Rent()
    {
        Console.Write("Podaj id pojazdu do wypożyczenia: ");
        // TODO: ID validation could be extracted to a method
        var isCorrectIdFormat = int.TryParse(Console.ReadLine(), out var id);
        if (!isCorrectIdFormat)
        {
            Console.WriteLine("Nieprawidłowy format id.");
            Console.ReadKey();
            return;
        }
        var vehicle = GetVehicle(id);
        if (vehicle != null)
        {
            if (vehicle.IsRented)
            {
                Console.WriteLine("Pojazd o podanym id jest już wypożyczony.");
                return;
            }
            Console.Write("Podaj swoje imię i nazwisko: ");
            var renter = Console.ReadLine();
            if (string.IsNullOrEmpty(renter))
            {
                Console.WriteLine("Nieprawidłowe imię i nazwisko.");
                Console.ReadKey();
                return;
            }
            vehicle.IsRented = true;
            vehicle.Renter = renter;

            var jsonString = JsonSerializer.Serialize(_vehicles);
            File.WriteAllText("vehicles.json", jsonString);
            Console.WriteLine("Wypożyczono pojazd o id: " + id);
        }
        else
        {
            Console.WriteLine("Nie znaleziono pojazdu o podanym id.");
        }
    }
    
    public void ReturnRented()
    {
        Console.Write("Podaj id pojazdu do oddania: ");
        var isCorrectIdFormat = int.TryParse(Console.ReadLine(), out var id);
        if (!isCorrectIdFormat)
        {
            Console.WriteLine("Nieprawidłowy format id.");
            Console.ReadKey();
            return;
        }
        var vehicle = GetVehicle(id);
        if (vehicle != null)
        {
            if (!vehicle.IsRented)
            {
                Console.WriteLine("Pojazd o podanym id nie jest wypożyczony.");
                return;
            }
            vehicle.IsRented = false;
            vehicle.Renter = null;

            var jsonString = JsonSerializer.Serialize(_vehicles);
            File.WriteAllText("vehicles.json", jsonString);
            Console.WriteLine("Zwrócono pojazd o id: " + id);
        }
        else
        {
            Console.WriteLine("Nie znaleziono pojazdu o podanym id.");
        }
    }
}
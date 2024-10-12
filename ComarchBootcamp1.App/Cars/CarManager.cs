using ComarchBootcamp1.App.Cars.Model;
using ComarchBootcamp1.App.Cars.Repositories;
using ConsoleTables;

namespace ComarchBootcamp1.App.Cars;

internal class CarManager
{
    public void Start()
    {
        int choice;
        do
        {
            ShowMenu();

            Console.Write("Wybierz pozycję: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowCars();
                        break;
                    case 2:
                        AddNewCar();
                        break;
                    case 3:
                        ModifyCar();
                        break;
                    case 4:
                        DeleteCar();
                        break;
                    case 5:
                        BorrowCar();
                        break;
                }
            }
        } while (choice != 0);

        Console.WriteLine("Koniec programu.");
    }

    private static void DeleteCar()
    {
        Console.Write("Podaj id: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var repository = new VehicleRepository();
            repository.Remove(id);
        }
        else
        {
            Console.WriteLine("Nieprawidłowy format id.");
        }

        Console.ReadKey();
    }

    private static void AddNewCar()
    {
        Console.WriteLine("Tworzenie nowego pojazdu.");
        Console.WriteLine("Wybierz typ auta: ");
        Console.WriteLine(" 1. Car");
        Console.WriteLine(" 2. Bus");
        Console.WriteLine(" 3. Truck");
        int carTypeUser = int.Parse(Console.ReadLine() ?? "1");
        CarTypes carType = (CarTypes)carTypeUser;

        Vehicle? vehicle = null;

        switch (carType)
        {
            case CarTypes.Car:
                vehicle = new Car();
                break;
            case CarTypes.Bus:
                vehicle = new Bus();
                break;
            case CarTypes.Truck:
                vehicle = new Truck();
                break;
            default:
                Console.WriteLine("Nieprawidłowa opcja");
                break;
        }

        if (vehicle == null) return;

        Console.Write("Podaj markę: ");
        vehicle.Maker = Console.ReadLine() ?? "";

        Console.Write("Podaj model: ");
        vehicle.Model = Console.ReadLine() ?? "";

        Console.Write("Podaj rodzaj paliwa: ");
        vehicle.GasType = Console.ReadLine() ?? "";

        Console.Write("Podaj pojemność silnika: ");
        vehicle.Capacity = int.Parse(Console.ReadLine() ?? "0");

        var repository = new VehicleRepository();
        repository.Add(vehicle);
    }

    private static void ModifyCar()
    {
        Console.Write("Podaj id pojazdu do modyfikacji: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var repository = new VehicleRepository();
            var vehicle = repository.GetVehicle(id);
            if (vehicle != null)
            {
                Console.Write("Podaj nową markę: ");
                vehicle.Maker = Console.ReadLine() ?? "";

                Console.Write("Podaj nowy model: ");
                vehicle.Model = Console.ReadLine() ?? "";

                Console.Write("Podaj nowy rodzaj paliwa: ");
                vehicle.GasType = Console.ReadLine() ?? "";

                Console.Write("Podaj nową pojemność silnika: ");
                vehicle.Capacity = int.Parse(Console.ReadLine() ?? "0");

                repository.Modify(id, vehicle);
            }
            else
            {
                Console.WriteLine("Nie znaleziono pojazdu o podanym id.");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowy format id.");
        }

        Console.ReadKey();
    }

    private static void ShowCars()
    {
        var repository = new VehicleRepository();
        var carList = repository.GetAll();

        ConsoleTable
            .From(carList)
            .Write();
        Console.ReadKey();
    }

    private static void BorrowCar()
    {
        Vehicle vehicle = new Car();
        vehicle.Borrow("");

        Car car = new Car();
        car.Borrow("");
    }

    private static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("CAR MANAGER 1.0");
        Console.WriteLine("  1. Lista aut");
        Console.WriteLine("  2. Dodaj auto");
        Console.WriteLine("  3. Edytuj auto");
        Console.WriteLine("  4. Usuń auto");
        Console.WriteLine("  5. Wypożycz");
        Console.WriteLine("  0. Zakończ");
    }
}
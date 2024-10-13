namespace ComarchBootcamp1.App.Cars.Model;

public class Vehicle
{
    public int Id { get; set; }

    public string Maker { get; set; }

    public string Model { get; set; }

    public string GasType { get; set; }

    public int Capacity { get; set; }

    public string Type { get; set; }
    
    public bool IsRented { get; set; }
    
    public string? Renter { get; set; }
}
namespace ComarchBootcamp1.App.Cars.Model;

internal class Car : Vehicle
{
    public void Refuel(int count)
    {
        throw new NotImplementedException();
    }

    public override void Borrow(string Borrower)
    {
        Console.WriteLine("Metoda Borrow w klasie Car");
    }
}

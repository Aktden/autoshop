public abstract class Vehicle
{
    public int Id { get; protected set; }
    public string Brand { get; protected set; }
    public string Model { get; protected set; }
    public int Year { get; protected set; }
    public decimal Price { get; protected set; }

    protected Vehicle(string brand, string model, int year, decimal price)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
    }

    public abstract string GetVehicleType();

    public virtual string GetFullInfo()
    {
        return $"{GetVehicleType()}: {Brand} {Model}, {Year} год, Цена: {Price:C}";
    }
}
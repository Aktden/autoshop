public class Truck : Vehicle
{
    public decimal LoadCapacity { get; private set; }
    public string AxleConfiguration { get; private set; }

    public Truck(string brand, string model, int year, decimal price,
                decimal loadCapacity, string axleConfiguration)
        : base(brand, model, year, price)
    {
        LoadCapacity = loadCapacity;
        AxleConfiguration = axleConfiguration;
    }

    public override string GetVehicleType() => "Грузовик";

    public override string GetFullInfo()
    {
        return base.GetFullInfo() + $", Грузоподъемность: {LoadCapacity}т, Ось: {AxleConfiguration}";
    }
}
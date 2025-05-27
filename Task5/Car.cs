public class Car : Vehicle
{
    public int DoorsCount { get; private set; }
    public string BodyType { get; private set; }

    public Car(string brand, string model, int year, decimal price,
              int doorsCount, string bodyType)
        : base(brand, model, year, price)
    {
        DoorsCount = doorsCount;
        BodyType = bodyType;
    }

    public override string GetVehicleType() => "Автомобиль";

    public override string GetFullInfo()
    {
        return base.GetFullInfo() + $", Кузов: {BodyType}, Дверей: {DoorsCount}";
    }
}
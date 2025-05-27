public static class VehicleValidator
{
    public static void Validate(Vehicle vehicle)
    {
        if (vehicle == null)
            throw new ArgumentNullException(nameof(vehicle));

        if (string.IsNullOrWhiteSpace(vehicle.Brand))
            throw new ArgumentException("Марка не может быть пустой");

        if (string.IsNullOrWhiteSpace(vehicle.Model))
            throw new ArgumentException("Модель не может быть пустой");

        if (vehicle.Year < 1886 || vehicle.Year > DateTime.Now.Year + 1)
            throw new ArgumentException("Некорректный год выпуска");

        if (vehicle.Price <= 0)
            throw new ArgumentException("Цена должна быть положительной");
    }
}
using ConsoleApp10;

public class VehicleService
{
    private readonly IVehicleRepository _repository;

    public VehicleService(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _repository.GetAll();
    }

    public Vehicle GetVehicleById(int id)
    {
        var vehicle = _repository.GetById(id);
        return vehicle ?? throw new KeyNotFoundException("Транспортное средство не найдено");
    }

    public void AddVehicle(Vehicle vehicle)
    {
        VehicleValidator.Validate(vehicle);
        _repository.Add(vehicle);
    }

    public void UpdateVehicle(Vehicle vehicle)
    {
        VehicleValidator.Validate(vehicle);
        if (_repository.GetById(vehicle.Id) == null)
            throw new KeyNotFoundException("Транспортное средство не найдено");

        _repository.Update(vehicle);
    }

    public void DeleteVehicle(int id)
    {
        if (_repository.GetById(id) == null)
            throw new KeyNotFoundException("Транспортное средство не найдено");

        _repository.Delete(id);
    }

    public IEnumerable<Vehicle> SearchVehicles(Func<Vehicle, bool> predicate)
    {
        return _repository.GetAll().Where(predicate);
    }

    public decimal CalculateTotalValue()
    {
        return _repository.GetAll().Sum(v => v.Price);
    }
}
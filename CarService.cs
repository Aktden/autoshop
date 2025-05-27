public class CarService
{
    private readonly ICarRepository _repository;

    public CarService(ICarRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Car> GetAllCars() => _repository.GetAll();

    public Car GetCarById(int id) => _repository.GetById(id);

    public void AddCar(Car car)
    {
        ValidateCar(car);
        _repository.Add(car);
    }

    public void UpdateCar(Car car)
    {
        ValidateCar(car);
        if (_repository.GetById(car.Id) == null)
            throw new ArgumentException("Автомобиль не найден.");

        _repository.Update(car);
    }

    public void DeleteCar(int id)
    {
        if (_repository.GetById(id) == null)
            throw new ArgumentException("Автомобиль не найден.");

        _repository.Delete(id);
    }

    private void ValidateCar(Car car)
    {
        if (car == null) throw new ArgumentNullException(nameof(car));
        if (string.IsNullOrWhiteSpace(car.Brand)) throw new ArgumentException("Марка не может быть пустой.");
        if (string.IsNullOrWhiteSpace(car.Model)) throw new ArgumentException("Модель не может быть пустой.");
        if (car.Year < 1886 || car.Year > DateTime.Now.Year + 1)
            throw new ArgumentException("Неверный год выпуска.");
        if (car.Price <= 0) throw new ArgumentException("Цена должна быть положительной.");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var carShopUI = new CarShopUI();
            carShopUI.Run();
        }
    }

    public class CarShopUI
    {
        private readonly CarService _carService = new CarService();
        private bool _isRunning = true;

        public void Run()
        {
            while (_isRunning)
            {
                ShowMainMenu();
                HandleUserInput();
            }
        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== АВТОМАГАЗИН ===");
            Console.WriteLine("1. Просмотреть все автомобили");
            Console.WriteLine("2. Добавить автомобиль");
            Console.WriteLine("3. Найти автомобиль по ID");
            Console.WriteLine("4. Обновить информацию об автомобиле");
            Console.WriteLine("5. Удалить автомобиль");
            Console.WriteLine("6. Выход");
            Console.Write("Выберите действие: ");
        }

        private void HandleUserInput()
        {
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowAllCars();
                    break;
                case "2":
                    AddCar();
                    break;
                case "3":
                    FindCarById();
                    break;
                case "4":
                    UpdateCar();
                    break;
                case "5":
                    DeleteCar();
                    break;
                case "6":
                    _isRunning = false;
                    break;
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowAllCars()
        {
            Console.Clear();
            Console.WriteLine("=== СПИСОК АВТОМОБИЛЕЙ ===");

            var cars = _carService.GetAllCars();

            if (!cars.Any())
            {
                Console.WriteLine("Автомобили не найдены.");
            }
            else
            {
                foreach (var car in cars)
                {
                    Console.WriteLine($"ID: {car.Id}, Марка: {car.Brand}, Модель: {car.Model}, Год: {car.Year}, Цена: {car.Price:C}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void AddCar()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ АВТОМОБИЛЯ ===");

            try
            {
                Console.Write("Марка: ");
                var brand = Console.ReadLine();

                Console.Write("Модель: ");
                var model = Console.ReadLine();

                Console.Write("Год выпуска: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    throw new ArgumentException("Неверный формат года.");
                }

                Console.Write("Цена: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    throw new ArgumentException("Неверный формат цены.");
                }

                var newCar = new Car
                {
                    Brand = brand,
                    Model = model,
                    Year = year,
                    Price = price
                };

                _carService.AddCar(newCar);
                Console.WriteLine("Автомобиль успешно добавлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void FindCarById()
        {
            Console.Clear();
            Console.WriteLine("=== ПОИСК АВТОМОБИЛЯ ПО ID ===");

            try
            {
                Console.Write("Введите ID автомобиля: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    throw new ArgumentException("Неверный формат ID.");
                }

                var car = _carService.GetCarById(id);

                if (car == null)
                {
                    Console.WriteLine("Автомобиль не найден.");
                }
                else
                {
                    Console.WriteLine($"Найден автомобиль: {car.Brand} {car.Model}, {car.Year} год, Цена: {car.Price:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void UpdateCar()
        {
            Console.Clear();
            Console.WriteLine("=== ОБНОВЛЕНИЕ ИНФОРМАЦИИ ОБ АВТОМОБИЛЕ ===");

            try
            {
                Console.Write("Введите ID автомобиля для обновления: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    throw new ArgumentException("Неверный формат ID.");
                }

                var car = _carService.GetCarById(id);
                if (car == null)
                {
                    Console.WriteLine("Автомобиль не найден.");
                    return;
                }

                Console.WriteLine($"Текущие данные: {car.Brand} {car.Model}, {car.Year} год, Цена: {car.Price:C}");
                Console.WriteLine("Введите новые данные (оставьте пустым, чтобы не изменять):");

                Console.Write("Марка: ");
                var brand = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(brand)) car.Brand = brand;

                Console.Write("Модель: ");
                var model = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(model)) car.Model = model;

                Console.Write("Год выпуска: ");
                var yearInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int year))
                {
                    car.Year = year;
                }

                Console.Write("Цена: ");
                var priceInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
                {
                    car.Price = price;
                }

                _carService.UpdateCar(car);
                Console.WriteLine("Автомобиль успешно обновлен!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        private void DeleteCar()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ АВТОМОБИЛЯ ===");

            try
            {
                Console.Write("Введите ID автомобиля для удаления: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    throw new ArgumentException("Неверный формат ID.");
                }

                _carService.DeleteCar(id);
                Console.WriteLine("Автомобиль успешно удален!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
    }

    public class CarService
    {
        private readonly List<Car> _cars = new List<Car>();
        private int _nextId = 1;

        public IEnumerable<Car> GetAllCars() => _cars;

        public Car GetCarById(int id) => _cars.FirstOrDefault(c => c.Id == id);

        public void AddCar(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));
            if (string.IsNullOrWhiteSpace(car.Brand)) throw new ArgumentException("Марка не может быть пустой.");
            if (string.IsNullOrWhiteSpace(car.Model)) throw new ArgumentException("Модель не может быть пустой.");
            if (car.Year < 1886 || car.Year > DateTime.Now.Year + 1)
                throw new ArgumentException("Неверный год выпуска.");
            if (car.Price <= 0) throw new ArgumentException("Цена должна быть положительной.");

            car.Id = _nextId++;
            _cars.Add(car);
        }

        public void UpdateCar(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));

            var existingCar = GetCarById(car.Id);
            if (existingCar == null) throw new ArgumentException("Автомобиль не найден.");

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
        }

        public void DeleteCar(int id)
        {
            var car = GetCarById(id);
            if (car == null) throw new ArgumentException("Автомобиль не найден.");

            _cars.Remove(car);
        }
    }
}

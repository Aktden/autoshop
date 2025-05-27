public class CarRepository : ICarRepository
{
    public IEnumerable<Car> GetAll()
    {
        using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            return connection.Query<Car>("SELECT * FROM Cars");
        }
    }

    public Car GetById(int id)
    {
        using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            return connection.QueryFirstOrDefault<Car>(
                "SELECT * FROM Cars WHERE Id = @Id",
                new { Id = id });
        }
    }

    public void Add(Car car)
    {
        using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            car.Id = connection.ExecuteScalar<int>(
                @"INSERT INTO Cars (Brand, Model, Year, Price) 
                VALUES (@Brand, @Model, @Year, @Price);
                SELECT CAST(SCOPE_IDENTITY() as int)",
                car);
        }
    }

    public void Update(Car car)
    {
        using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            connection.Execute(
                @"UPDATE Cars 
                SET Brand = @Brand, Model = @Model, Year = @Year, Price = @Price 
                WHERE Id = @Id",
                car);
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(DatabaseConfig.ConnectionString))
        {
            connection.Open();
            connection.Execute(
                "DELETE FROM Cars WHERE Id = @Id",
                new { Id = id });
        }
    }
}
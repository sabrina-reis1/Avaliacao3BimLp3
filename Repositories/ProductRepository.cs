using Avaliacao3BimLp3.Database;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Repositories;

class ProductRepository
{
    private DatabaseConfig databaseConfig;

    public ProductRepository(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
    }

    public Product Save(Product product)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        
        connection.Execute("INSERT INTO products VALUES(@Id, @Name, @Price, @Active)",product);

        return product;
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM products WHERE Id = @id", new{id = id});
    }    
    
    public Boolean ExitsById(int id){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        return connection.ExecuteScalar<Boolean>("SELECT count(id) FROM Products WHERE Id = @id;",new {id = id});       
    }

    public void Enable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE products SET active = True WHERE Id = @id",new {id = id});
    }

    public List<Product> GetAll()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var products = connection.Query<Product>("SELECT * FROM products").ToList();
        return products;
    }   

    public void Disable(int id)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE products SET active = False WHERE Id = @id",new {id = id});
    }


    public List<Product> GetAllWithPriceBetween(double initialPrice, double endPrice)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var products = connection.Query<Product>("SELECT * FROM products WHERE price > @initialPrice AND price < @endPrice", new {initialPrice = initialPrice, endPrice = endPrice}).ToList();
        return products;
    }

    public List<Product> GetAllWithPriceHigherThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var products = connection.Query<Product>("SELECT * FROM products WHERE price > @price", new {price = price}).ToList();
        return products;
    }

    public List<Product> GetAllWithPriceLowerThan(double price)
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();
        var products = connection.Query<Product>("SELECT * FROM products WHERE price < @price", new {price = price}).ToList();
        return products;
    }

    public double GetAveragePrice(){
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();                          
        double avarage = connection.ExecuteScalar<double>("SELECT AVG(price) FROM products");
        return avarage;
    }
}
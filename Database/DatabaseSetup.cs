using Dapper;
using Microsoft.Data.Sqlite;

namespace Avaliacao3BimLp3.Database;

class DatabaseSetup
{
    private DatabaseConfig databaseConfig;

    public DatabaseSetup(DatabaseConfig databaseConfig)
    {
        this.databaseConfig = databaseConfig;
        CreateProductTable();
    }

    public void CreateProductTable()
    {
        using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute(@"CREATE TABLE IF NOT EXISTS Products(
            id int not null primary key,
            name varchar(100) not null,
            price double not null,
            active boolean not null);"
        );
      
    }

}
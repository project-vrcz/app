using Microsoft.Data.Sqlite;

namespace VRCZ.Core.Services.Database;

public class MockConnectionStringProvider : IConnectionStringProvider
{
    public string GetConnectionString()
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = ":memory:"
        };

        return builder.ToString();
    }
}

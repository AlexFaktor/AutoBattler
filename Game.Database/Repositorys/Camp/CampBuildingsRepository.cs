using Game.Core.Database.Records.Camp;
using Game.Core.Resources.Enums.Game;
using Npgsql;
using System.Data;

namespace Game.Database.Service.Camp;

public class CampBuildingsRepository
{
    private readonly string _connectionString;

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public CampBuildingsRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // CRUD
}

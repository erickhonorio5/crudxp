using System.Data;
using Dapper;
using Npgsql;

namespace CASEXP.CaseXP.Application.Shared.Infrastructure.Postgres;

public class BaseRepository
{
    public int _commandTimeout { get; set; }

    private readonly IConfiguration _configuration;

    public BaseRepository(IConfiguration configuracoes)
    {
        _configuration = configuracoes;

        //_commandTimeout = configuracoes.Value.Configuracoes.TimeOutDefault;

        if (_commandTimeout == 0) _commandTimeout = 900;
    }

    protected IDbConnection GerarConexaoConnect() => new NpgsqlConnection(_configuration.GetValue<String>("ConnectionStrings:DefaultConnection"));

    public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(IDbConnection dbCon, string sql, object parameters = null)
    {
        return await dbCon.QueryAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<T> DbQuerySingleAsync<T>(IDbConnection dbCon, string sql, object parameters)
    {
        return await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<bool> DbExecuteAsync<T>(IDbConnection dbCon, string sql, object parameters, CommandType commandType = CommandType.Text)
    {
        return await dbCon.ExecuteAsync(sql, parameters, commandTimeout: _commandTimeout, commandType: commandType) > 0;
    }

    public virtual async Task<bool> DbExecuteScalarAsync(IDbConnection dbCon, string sql, object parameters)
    {
        return await dbCon.ExecuteScalarAsync<bool>(sql, parameters, commandTimeout: _commandTimeout);
    }

    public virtual async Task<T> DbExecuteScalarDynamicAsync<T>(IDbConnection dbCon, string sql, object parameters = null)
    {
        return await dbCon.ExecuteScalarAsync<T>(sql, parameters, commandTimeout: _commandTimeout);
    }
}
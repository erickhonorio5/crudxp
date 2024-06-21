using CASEXP.CaseXP.Application.Shared.Infrastructure.Postgres;
using CASEXP.CaseXP.Domain.Client;

using Dapper;
using Npgsql;


namespace CASEXP.CaseXP.Application.UseCases.DataAccess;

public class ClientRepository : BaseRepository, IClientRepository
{
    private readonly string _connectionString;

    
    public ClientRepository (IConfiguration configuration) : base(configuration)
    {
        
    }

    public IEnumerable<Client> GetAll()
    {
        var clients = DbQueryAsync<Client>(GerarConexaoConnect(), "select * from client").Result;
        return clients;
    }

    public Client GetById(int id)
    {
        var query = "SELECT * FROM client WHERE Id = @ClientId";
        var parameters = new { ClientId = id };
        var client = DbQueryAsync<Client>(GerarConexaoConnect(), query, parameters).Result.FirstOrDefault();
        return client;
        
    }

    public void Add(Client client)
    {
        var query = @"INSERT INTO client (Name, CPF, Email, Phone, Address) 
                  VALUES (@Name, @CPF, @Email, @Phone, @Address)";
        var parameters = new
        {
            client.Name,
            client.CPF,
            client.Email,
            client.Phone,
            client.Address
        };

        var result = DbExecuteAsync<Client>(GerarConexaoConnect(), query, parameters).Result;
    }


    public void Update(Client client)
    {
        var query = @"UPDATE Client 
                    SET Name = @Name, 
                    CPF = @CPF, 
                    Email = @Email, 
                    Phone = @Phone, 
                    Address = @Address 
                    WHERE Id = @Id";

        using (var connection = GerarConexaoConnect())
        {
            connection.Open();
            connection.Execute(query, client);
        }
    }



    public void Delete(int id)
    {
        var query = "DELETE FROM Client WHERE Id = @Id";

        using (var connection = GerarConexaoConnect())
        {
            connection.Open();
            connection.Execute(query, new { Id = id });
        }
    }


}
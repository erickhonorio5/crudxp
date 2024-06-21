using CASEXP.CaseXP.Application.Shared.Infrastructure.Postgres;
using CASEXP.CaseXP.Domain.ProductsInvestment;
using CASEXP.CaseXP.Application.Shared.Infrastructure.Postgres;
using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;

namespace CASEXP.CaseXP.Application.UseCases.DataAccess
{
    public class InvestmentRepository : BaseRepository, IProductInvestmentRepository
    {
        public InvestmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<ProductInvestiment> GetAll()
        {
            var investments = DbQueryAsync<ProductInvestiment>(GerarConexaoConnect(), "SELECT * FROM investment").Result;
            return investments;
        }

        public ProductInvestiment GetById(int id)
        {
            var query = "SELECT * FROM investment WHERE Id = @InvestmentId";
            var parameters = new { InvestmentId = id };
            var investment = DbQueryAsync<ProductInvestiment>(GerarConexaoConnect(), query, parameters).Result.FirstOrDefault();
            return investment;
        }

        public void Add(ProductInvestiment investment)
        {
            var query = @"INSERT INTO investment (Name, Description, Type, CurrentValue, MaturityDate, ClientId) 
                          VALUES (@Name, @Description, @Type, @CurrentValue, @MaturityDate, @ClientId)";
            var parameters = new
            {
                investment.Name,
                investment.Description,
                investment.Type,
                investment.CurrentValue,
                investment.MaturityDate,
                investment.ClientId
            };

            var result = DbExecuteAsync<ProductInvestiment>(GerarConexaoConnect(), query, parameters).Result;
        }

        public void Update(ProductInvestiment investment)
        {
            var query = @"UPDATE investment 
                          SET Name = @Name, 
                              Description = @Description, 
                              Type = @Type, 
                              CurrentValue = @CurrentValue, 
                              MaturityDate = @MaturityDate, 
                              ClientId = @ClientId 
                          WHERE Id = @Id";

            using (var connection = GerarConexaoConnect())
            {
                connection.Open();
                connection.Execute(query, investment);
            }
        }

        public void Delete(int id)
        {
            var query = "DELETE FROM investment WHERE Id = @Id";

            using (var connection = GerarConexaoConnect())
            {
                connection.Open();
                connection.Execute(query, new { Id = id });
            }
        }
    }
}

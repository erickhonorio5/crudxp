using CASEXP.CaseXP.Application.UseCases.DataAccess;
using CASEXP.CaseXP.Domain.Client;
using CASEXP.CaseXP.Domain.ProductsInvestment;

namespace CASEXP;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método é usado para adicionar serviços ao contêiner.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração do banco de dados PostgreSQL
        var connectionString = Configuration.GetConnectionString("DefaultConnection");

        // Registro do serviço de conexão com o banco de dados PostgreSQL
        services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

        // Registro dos repositórios e outros serviços necessários
        services.AddScoped<IClientRepository, ClientRepository>();

        // Adiciona controllers para suportar as APIs
        services.AddControllers();
    }

    // Este método é usado para configurar o pipeline de solicitação HTTP.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Middleware de tratamento de erros em produção
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Middleware para redirecionar HTTP para HTTPS
        app.UseHttpsRedirection();
        

        // Middleware para suportar roteamento e endpoints
        app.UseRouting();

        // Middleware para autenticação, autorização e manipulação de erros
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            // Mapeia os controllers para os endpoints da API
            endpoints.MapControllers();
        });
    }
}

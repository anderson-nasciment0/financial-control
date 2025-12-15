using FinancialControl.Application.Interfaces;
using FinancialControl.Application.Mappings;
using FinancialControl.Application.Services;
using FinancialControl.Domain.Interfaces;
using FinancialControl.Infrastructure.Context;
using FinancialControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialControl.CrossCutting.IoC
{
    public static class DependencyInjectionAPI
    {
        //Clase responsavel pela injeção de dependencia
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            //Recupera a string de conexão do MYSQL e detecta sua versão
            string mySqlConnection = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

            //Repositórios para o acesso aos dados e services com as regras de negócio
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            //AutoMapper para registrar os perfis de mapeamento
            services.AddAutoMapper(cfg => { }, typeof(DtoMappingProfile).Assembly);

            return services;
        }
    }
}

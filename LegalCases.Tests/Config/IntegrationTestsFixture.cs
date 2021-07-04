using Bogus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Api;
using ThomsonReuters.Application;
using ThomsonReuters.Business.Entities;
using Xunit;

namespace LegalCases.Tests.Config
{

    // Poderia ser criado um outro arquivo de startup usado para teste, mas como o conteudo é praticamente o mesmo não quis me repetir
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Startup>> { }
    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly LegalCasesFactory<IStartup> Factory;
        public HttpClient Client;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };
            Factory = new LegalCasesFactory<IStartup>();
            Client = Factory.CreateClient(clientOptions);
        }

        public LegalCase GenerateValidLegalCase()
        {
            return GenerateManyValidLegalCases(1).FirstOrDefault();
        }

        public LegalCase GenerateInvalidLegalCase()
        {
            return GenerateManyInvalidLegalCases(1).FirstOrDefault();
        }

        public IEnumerable<LegalCase> GenerateManyValidLegalCases(int amount)
        {
            var cases = new Faker<LegalCase>("pt_BR")
                .CustomInstantiator(f => new LegalCase(
                    String.Join(',',f.Random.Digits(20)),
                    f.Company.CompanyName(1),
                    f.Name.FirstName(null),
                    f.Date.Recent(20)
                ));


            return cases.Generate(amount);
        }

        public IEnumerable<LegalCase> GenerateManyInvalidLegalCases(int amount)
        {
            var cases = new Faker<LegalCase>("pt_BR")
                .CustomInstantiator(f => new LegalCase(
                    "",
                    "",
                    f.Name.FirstName(null),
                    f.Date.Recent(20)
                ));


            return cases.Generate(amount);
        }



        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }
    }
}

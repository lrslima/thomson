using LegalCases.Tests.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Api;
using ThomsonReuters.Business.Entities;
using Xunit;

namespace LegalCases.Tests.Integration
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class LegalCasesApiTests
    {

        private readonly IntegrationTestsFixture<Startup> _testsFixture;

        public LegalCasesApiTests(IntegrationTestsFixture<Startup> testsFixture)
        {
            _testsFixture = testsFixture;
            _testsFixture.Client.Login();

        }


        [Fact(DisplayName = "Create Legal Case")]
        [Trait("Category", "API Integration - Legal Case")]
        public async Task AdicionarNovoProduto_DeveRetornarComSucesso()
        {
            // Arrange
            var legalCase = _testsFixture.GenerateValidLegalCase();

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/produto/create", legalCase);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Read all Legal Case")]
        [Trait("Category", "API Integration - Legal Case")]
        public async Task ListarProdutos_DeveRetornarComSucesso()
        {
            // Arrange
            var legalCase = _testsFixture.GenerateValidLegalCase();

            // Act
            var postResponse = await _testsFixture.Client.GetAsync("api/legalcase");

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Update Legal Case")]
        [Trait("Category", "API Integration - Legal Case")]
        public async Task AtualizarProduto_DeveRetornarComSucesso()
        {
            // Arrange
            var produto = _testsFixture.GenerateValidLegalCase();

            // cadastrar o produto
            var postResponse = await _testsFixture.Client.PutAsJsonAsync("api/legalcase/create", produto);

            // get the legal created legal case content
            var legalCaseContent = JsonConvert.DeserializeObject<LegalCase>(await postResponse.Content.ReadAsStringAsync());

            legalCaseContent.CourtName = "Court name updated test";

            // Act
            var result = await _testsFixture.Client.PutAsJsonAsync("api/legalcase/update", legalCaseContent);

            // Assert
            result.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Delete Legal Case")]
        [Trait("Category", "API Integration - Legal Case")]
        public async Task ExcluirProduto_DeveRetornarComSucesso()
        {
            // Arrange
            var legalCase = _testsFixture.GenerateValidLegalCase();

            // cadastrar o produto
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/legalcase/create", legalCase);

            // get the legal created legal case content
            var produtoContent = JsonConvert.DeserializeObject<LegalCase>(await postResponse.Content.ReadAsStringAsync());

            // Act
            var response = await _testsFixture.Client.DeleteAsync($"api/legalcase/delete/{produtoContent.CaseNumber}");

            // Assert
            response.EnsureSuccessStatusCode();
        }


    }
}

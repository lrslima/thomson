using FluentAssertions;
using LegalCases.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Api;
using Xunit;
using Xunit.Abstractions;

namespace LegalCases.Tests.Unit
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class LegalCasesValidation
    {
        private readonly IntegrationTestsFixture<Startup> _testsFixture;
        readonly ITestOutputHelper _outputHelper;


        public LegalCasesValidation(IntegrationTestsFixture<Startup> testsFixture,
                            ITestOutputHelper outputHelper)
        {
            _testsFixture = testsFixture;
            _outputHelper = outputHelper;
        }

        [Fact(DisplayName = "New Valid Legal Case")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var legalCase = _testsFixture.GenerateValidLegalCase();

            // Act
            var result = legalCase.IsValid();

            // Assert 
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "New Invalid Legal Case")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _testsFixture.GenerateInvalidLegalCase();

            // Act
            var result = cliente.IsValid();

            // Assert 
            result.Should().BeFalse();
        }
    }
}

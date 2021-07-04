using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThomsonReuters.Application.ViewModels;
using ThomsonReuters.Business.Entities;
using ThomsonReuters.CrossCutting;

namespace LegalCases.Tests.Config
{
    public static class TestExtensions
    {

        public static void Login(this HttpClient client)
        {

            Mock<IJwtUtils> mock = new Mock<IJwtUtils>();
            var user = new User
            {
                FirstName = "test",
                LastName = "test",
                Email = "test",
                Password = "test"
            };

            var token = mock.Setup(x => x.GenerateJwtToken(user));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Convert.ToString(token));
        }
    }
}

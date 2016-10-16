using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDeveloperExamen.Repository;
using Xunit;
using FluentAssertions;

namespace WebDeveloperExamen.Tests.Repository
{
    public class WebDeveloperContextTest
    {
        private WebContextDb dbContext;
        public WebDeveloperContextTest()
        {
            dbContext = new WebContextDb();
        }
        [Fact(DisplayName = "CoberturaRepositorio")]
        public void CoberturaRepositorioTest()
        {
            dbContext.Customer.Should().NotBeNull();
            dbContext.Product.Should().NotBeNull();
            dbContext.Supplier.Should().NotBeNull();
        }
    }
}
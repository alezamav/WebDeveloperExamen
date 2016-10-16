using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebDeveloperExamen.Areas.DoFactory.Controllers;
using WebDeveloperExamen.Model;
using WebDeveloperExamen.Repository;
using Xunit;

namespace WebDeveloperExamen.Tests.Controllers
{
    public class ClienteControllerTest
    {
        private ClienteController controller;
        private IRepository<Customer> _repository;
        private Mock<DbSet<Customer>> personDbSetMock;
        private Mock<WebContextDb> webContextMock;

        [Fact(DisplayName = "EnvioParametrosVaciosListado")]
        private void EnvioParametrosVaciosListado()
        {
            ConfiguracionListado_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Lista(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_Lista");

            var modelCount = (IEnumerable<Customer>)result.Model;
            modelCount.Count().Should().Be(10);
        }

        [Fact(DisplayName = "CrearClienteVista")]
        private void CrearClienteVista()
        {
            ConfiguracionBasica_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Crear() as PartialViewResult;
            result.ViewName.Should().Be("_Crear");

            var personModelCreate = (Customer)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "CrearClienteGrabar")]
        private void CrearClienteGrabar()
        {
            ConfiguracionBasica_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Crear(DataCliente()) as PartialViewResult;
            result.Should().BeNull();

            personDbSetMock.Verify(s => s.Add(It.IsAny<Customer>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "EditarClienteVista")]
        private void EditarClienteVista()
        {
            ConfiguracionListado_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Editar(2) as PartialViewResult;
            result.ViewName.Should().Be("_Editar");

            var personModelCreate = (Product)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "EditarProductoActualizar")]
        private void EditarProductoActualizar()
        {
            ConfiguracionListado_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Editar(DataClienteEdicion()) as PartialViewResult;
            result.Should().BeNull();
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "EliminarCliente")]
        private void EliminarCliente()
        {
            ConfiguracionListado_MockData();
            controller = new ClienteController(_repository);
            var result = controller.Eliminar(DataClienteEdicion()) as PartialViewResult;
            result.Should().BeNull();
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        #region Configuration Values
        public void ListaCliente()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Customer
            {
                Id = i,
                FirstName = $"alex{i}",
                LastName = $"lez{i}"
            }).AsQueryable();
            personDbSetMock = new Mock<DbSet<Customer>>();
            personDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }

        private Customer DataCliente()
        {
            var producto = new Customer
            {
                FirstName = "juan",
                LastName = "lopez"
            };
           
            return producto;
        }

        private Customer DataClienteEdicion()
        {
            var producto = new Customer
            {
                FirstName = "juan",
                LastName = "lopez vega"
            };
            producto.Id = 1;
          
            return producto;
        }

        private void ConfiguracionListado_MockData()
        {
            personDbSetMock = new Mock<DbSet<Customer>>();
            ListaCliente();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Customer).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Customer>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Customer>(webContextMock.Object);
            controller = new ClienteController(_repository);
        }

        private void ConfiguracionBasica_MockData()
        {
            personDbSetMock = new Mock<DbSet<Customer>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Customer).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Customer>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Customer>(webContextMock.Object);
            controller = new ClienteController(_repository);
        }


        #endregion




    }
}
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
    public class ProductoControllerTest
    {
        private ProductoController controller;
        private IRepository<Product> _repository;
        private Mock<DbSet<Product>> personDbSetMock;
        private Mock<WebContextDb> webContextMock;

        [Fact(DisplayName = "EnvioParametrosVaciosListado")]
        private void EnvioParametrosVaciosListado()
        {
            ConfiguracionListado_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Lista(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_Lista");

            var modelCount = (IEnumerable<Product>)result.Model;
            modelCount.Count().Should().Be(10);
        }

        [Fact(DisplayName = "CrearProductoVista")]
        private void CrearProductoVista()
        {
            ConfiguracionBasica_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Crear() as PartialViewResult;
            result.ViewName.Should().Be("_Crear");

            var personModelCreate = (Product)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "CrearProductoGrabar")]
        private void CrearProductoGrabar()
        {
            ConfiguracionBasica_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Crear(DataProducto()) as PartialViewResult;
            result.Should().BeNull();

            personDbSetMock.Verify(s => s.Add(It.IsAny<Product>()), Times.Once());
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "EditarProductoVista")]
        private void EditarProductoVista()
        {
            ConfiguracionListado_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Editar(2) as PartialViewResult;
            result.ViewName.Should().Be("_Editar");

            var personModelCreate = (Product)result.Model;
            personModelCreate.Should().NotBeNull();
        }

        [Fact(DisplayName = "EditarProductoActualizar")]
        private void EditarProductoActualizar()
        {
            ConfiguracionListado_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Editar(DataProductoEdicion()) as PartialViewResult;
            result.Should().BeNull();
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        [Fact(DisplayName = "EliminarProducto")]
        private void EliminarProducto()
        {
            ConfiguracionListado_MockData();
            controller = new ProductoController(_repository);
            var result = controller.Eliminar(DataProductoEdicion()) as PartialViewResult;
            result.Should().BeNull();
            webContextMock.Verify(c => c.SaveChanges(), Times.Once());
        }

        #region Configuration Values
        public void ListaProducto()
        {
            var persons = Enumerable.Range(1, 10).Select(i => new Product
            {
                Id = i,
                ProductName = $"Producto{i}",
                SupplierId = 1,
                UnitPrice= 10,
                Package = $"Package{i}"
            }).AsQueryable();
            personDbSetMock = new Mock<DbSet<Product>>();
            personDbSetMock.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(persons.Provider);
            personDbSetMock.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(persons.Expression);
            personDbSetMock.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(persons.ElementType);
            personDbSetMock.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => persons.GetEnumerator());
        }

        private Product DataProducto()
        {
            var producto = new Product
            {
                ProductName = "Producto1",
                SupplierId = 1,
                UnitPrice = 10,
                Package = "Package22"
            };
           
            return producto;
        }

        private Product DataProductoEdicion()
        {
            var producto = new Product
            {
                ProductName = "Producto22",
                SupplierId = 1,
                UnitPrice = 10,
                Package = "Package22"
            };
            producto.Id = 1;
          
            return producto;
        }

        private void ConfiguracionListado_MockData()
        {
            personDbSetMock = new Mock<DbSet<Product>>();
            ListaProducto();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Product).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Product>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Product>(webContextMock.Object);
            controller = new ProductoController(_repository);
        }

        private void ConfiguracionBasica_MockData()
        {
            personDbSetMock = new Mock<DbSet<Product>>();

            webContextMock = new Mock<WebContextDb>();
            webContextMock.Setup(m => m.Product).Returns(personDbSetMock.Object);
            webContextMock.Setup(m => m.Set<Product>()).Returns(personDbSetMock.Object);

            _repository = new BaseRepository<Product>(webContextMock.Object);
            controller = new ProductoController(_repository);
        }


        #endregion




    }
}
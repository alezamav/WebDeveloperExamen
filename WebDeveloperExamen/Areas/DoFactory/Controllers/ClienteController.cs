﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebDeveloperExamen.Model;
using WebDeveloperExamen.Repository;

namespace WebDeveloperExamen.Areas.DoFactory.Controllers
{
    public class ClienteController : BaseController<Customer>
    {
        public ClienteController(IRepository<Customer> repository) : base(repository)
        {
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Lista(int? page, int? size)
        {
            if (!page.HasValue || !size.HasValue)
            {
                page = 1;
                size = 20;
            }
            return PartialView("_Lista", _repository.ListaPaginada((x => x.Id), page.Value, size.Value));
        }

        public int CantidadPagina(int pageSize)
        {
            var totalRecords = _repository.ObtenerLista().Count();
            return totalRecords % pageSize > 0 ? (totalRecords / pageSize) + 1 : totalRecords / pageSize;
        }

        public ActionResult Crear()
        {
            var model = new Customer();
            return PartialView("_Crear", model);
        }

        [HttpPost]
        public ActionResult Crear(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var model = new Customer();
                return PartialView("_Crear", model);
            }
            _repository.Agregar(customer);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        public ActionResult Detalle(int id)
        {
            var customer = _repository.ObtenerPorCodigo(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return PartialView("_Detalle", customer);
        }

        public ActionResult Editar(int id)
        {
            var customer = _repository.ObtenerPorCodigo(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return PartialView("_Editar", customer);
        }

        [HttpPost]
        public ActionResult Editar(Customer customer)
        {
            if (!ModelState.IsValid) return PartialView("_Editar", customer);
            _repository.Actualizar(customer);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        public ActionResult Eliminar(int id)
        {
            var customer = _repository.ObtenerPorCodigo(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return PartialView("_Eliminar", customer);
        }

        [HttpPost]
        public ActionResult Eliminar(Customer customer)
        {
            _repository.Eliminar(customer);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


    }
}
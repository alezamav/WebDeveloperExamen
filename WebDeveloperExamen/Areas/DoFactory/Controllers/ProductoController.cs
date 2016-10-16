using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
//using WebDeveloperExamen.Areas.DoFactory.Models;
using WebDeveloperExamen.Model;
using WebDeveloperExamen.Repository;
namespace WebDeveloperExamen.Areas.DoFactory.Controllers
{
    public class ProductoController : BaseController<Product>
    {
        public ProductoController(IRepository<Product> repository) : base(repository)
        {
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List(int? page, int? size)
        {
            if (!page.HasValue || !size.HasValue)
            {
                page = 1;
                size = 20;
            }
            return PartialView("_List", _repository.PaginatedList((x => x.Id), page.Value, size.Value));
        }

        public int PageSize(int pageSize)
        {
            var totalRecords = _repository.GetList().Count;
            return totalRecords % pageSize > 0 ? (totalRecords / pageSize) + 1 : totalRecords / pageSize;
        }

    }
}
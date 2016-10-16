using System.Web.Mvc;
using WebDeveloperExamen.Filters;
using WebDeveloperExamen.Repository;

namespace WebDeveloperExamen.Areas.DoFactory.Controllers
{   
    [Authorize] 
    [ExceptionControl]
    [OutputCache(Duration =0)]
    public class BaseController<T> : Controller where T: class 
    {
        protected IRepository<T>  _repository;
        public BaseController(IRepository<T> repository)
        {
            _repository = repository;
        }
    }
}
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        public int Pagesize = 4;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            var model = new ProductsListViewModel()
            {
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = Pagesize,
                    TotalItems = category == null ?
                                _repo.Products.Count() :
                                _repo.Products.Count(x => x.Category == category)
                },
                Products = _repo.Products
                    .OrderBy(x => x.ProductID)
                    .Where(x => (category == null || x.Category == category))
                    .Skip((page - 1) * Pagesize)
                    .Take(Pagesize)
                    .ToList(),
                CurrentCategory = category
            };

            return View(model);
        }
    }
}
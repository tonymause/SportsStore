using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repo;

        public AdminController(IProductRepository repo)
        {
            _repo = repo;
        }

        public ViewResult Index()
        {
            return View(_repo.Products);
        }

        public ViewResult Edit(int productId)
        {
            return View(_repo.Products.FirstOrDefault(x => x.ProductID == productId));
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            _repo.SaveProduct(product);
            TempData["message"] = $"{product.Name} has been saved";
            return RedirectToAction("Index");
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product product = _repo.DeleteProduct(productId);
            if (product != null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }

            return RedirectToAction("Index");
        }
    }
}
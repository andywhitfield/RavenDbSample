using Raven.Client;
using RavenDbSample.Models;
using RavenDbSample.Models.Home;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace RavenDbSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession session;

        public HomeController(IDocumentSession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            return View(new IndexViewModel
            {
                Products = session.Query<Product>().OrderBy(p => p.QuantityInStock).ToList()
            });
        }

        public ActionResult ProductDetails(int? id, string description)
        {
            if (!id.HasValue) return RedirectToAction("Index");
            return View(new ProductDetailsViewModel(session.Load<Product>(id)));
        }

        [HttpPost]
        public ActionResult AddProduct(string productName)
        {
            Trace.TraceInformation("Adding new product: {0}", productName);
            if (!string.IsNullOrWhiteSpace(productName))
                session.Store(new Product { Name = productName });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult BuyProduct(int? productId, int? qty)
        {
            if (productId.HasValue && qty.HasValue)
            {
                var product = session.Load<Product>(productId);
                if (product.QuantityInStock >= qty)
                {
                    product.QuantityInStock -= qty.Value;
                    session.Store(product);
                }
            }

            return RedirectToAction("ProductDetails", new { id = productId });
        }

        [HttpPost]
        public ActionResult RestockProduct(int? productId, int? qty)
        {
            if (productId.HasValue && qty.HasValue)
            {
                var product = session.Load<Product>(productId);
                product.QuantityInStock += qty.Value;
                session.Store(product);
            }

            return RedirectToAction("ProductDetails", new { id = productId });
        }

        [HttpPost]
        public ActionResult DeleteProduct(int? productId)
        {
            Trace.TraceInformation("Deleting product: {0}", productId);
            if (productId.HasValue)
                session.Delete<Product>(session.Load<Product>(productId));

            return RedirectToAction("Index");
        }
    }
}

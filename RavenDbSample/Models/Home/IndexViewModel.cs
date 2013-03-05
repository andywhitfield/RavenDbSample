using System.Collections.Generic;
using System.Linq;

namespace RavenDbSample.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Products = Enumerable.Empty<Product>();
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
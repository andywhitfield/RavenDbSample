namespace RavenDbSample.Models.Home
{
    public class ProductDetailsViewModel
    {
        private readonly Product product;

        public ProductDetailsViewModel(Product product)
        {
            this.product = product;
        }

        public Product Product { get { return product; } }
    }
}
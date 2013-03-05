using System.Web;

namespace RavenDbSample.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NameForUrl
        {
            get
            {
                return HttpUtility.UrlPathEncode(Name.ToLower());
            }
        }
        public int QuantityInStock { get; set; }
    }
}
using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories;

public class FakeProductRepository : IProductRepository
{
    public IList<Product> Get(IEnumerator<Guid> ids)
    {
        IList<Product> products = new List<Product>();
        products.Add(new Product("Product 1", 10, true));
        products.Add(new Product("Product 2", 10, true));
        products.Add(new Product("Product 3", 10, true));
        products.Add(new Product("Product 4", 10, false));
        products.Add(new Product("Product 4", 10, false));

        return products;
    }
}
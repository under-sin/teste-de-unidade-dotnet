using Store.Domain.Entities;

namespace Store.Domain.Repositories;

public interface IProductRepository
{
    IList<Product> Get(IEnumerator<Guid> ids);
}
﻿using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries;

[TestClass]
public class ProductQueriesTests
{
    private IList<Product> _products;

    public ProductQueriesTests()
    {
        _products = new List<Product>();
        _products.Add(new Product("Product 1", 10, true));
        _products.Add(new Product("Product 2", 20, true));
        _products.Add(new Product("Product 3", 30, true));
        _products.Add(new Product("Product 4", 40, false));
        _products.Add(new Product("Product 5", 50, false));
    }

    [TestMethod]
    [TestCategory("Queries")]
    public void DadoAConsultaDeProdutosAtivosDeveRetornar3()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
        Assert.AreEqual(result.Count(), 3);
    }
    
    [TestMethod]
    [TestCategory("Queries")]
    public void DadoAConsultaDeProdutosInatibosDeveRetornar2()
    {
        var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
        Assert.AreEqual(result.Count(), 2);
    }
}
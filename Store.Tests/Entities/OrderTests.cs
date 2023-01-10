﻿using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Customer _customer = new Customer("Anderson", "under-sin@outlook.com");
    private readonly Product _product = new Product("Livro Ruff Ghanor 4", 10, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoPedidoValidoEleDeveGerarUmNumeroCom8Caracteres()
    {
        var order = new Order(_customer, 0, _discount);
        Assert.AreEqual(8, order.Number.Length);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoPagamento()
    {
        var order = new Order(_customer, 0, _discount);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoEntrega()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 1);
        order.Pay(10);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoCanceladoSeuStatusDeveSerCancelado()
    {
        var order = new Order(_customer, 0, null);
        order.Cancel();
        
        Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoItemSemProdutoOMesmoNaoDeveSerAdicionado()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(null, 10);
        Assert.AreEqual(order.Items.Count, 0);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoItemComAQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 0);
        Assert.AreEqual(order.Items.Count, 0);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmDescontoExpiradoOValorDoPedidoDeveSer60()
    {
        var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
        var order = new Order(_customer, 10, expiredDiscount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmDescontoDe10OValorDoPedidoDeveSer50()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmaTaxaDeEntregaDe10OValorDoPedidoDeveSer60()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }
    
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoSemClienteOMesmoDeveSerInvalido()
    {
        var order = new Order(null, 10, null);
        order.AddItem(_product, 5);
        Assert.IsFalse(order.IsValid);
    }
}
using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Order : Entity
{
    private IList<OrderItem> _items;
    public Order(Customer customer, decimal deliveryFree, Discount discount)
    {
        AddNotifications(
            new Contract<Order>()
                .Requires()
                .IsNotNull(customer, "Customer", "Cliente inválido"));
        
        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString().Substring(0, 8);
        DeliveryFree = deliveryFree;
        Discount = discount;
        Status = EOrderStatus.WaitingPayment;
        _items = new List<OrderItem>();
    }

    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
    public decimal DeliveryFree { get; private set; }
    public Discount Discount { get; private set; }
    public EOrderStatus Status { get; private set; }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);
        if(item.IsValid)
            _items.Add(item);
    }

    public decimal Total()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total = item.Total();
        }

        total += DeliveryFree;
        total -= Discount != null ? Discount.Value() : 0;

        return total;
    }

    public void Pay(decimal amount)
    {
        if (amount == Total())
            this.Status = EOrderStatus.WaitingDelivery;
    }

    public void Cancel() => Status = EOrderStatus.Canceled;
}
namespace Store.Domain.Entities;

public class Discount : Entity
{
    public Discount(decimal amount, DateTime expireDate)
    {
        Amount = amount;
        ExpireDate = expireDate;
    }

    public decimal Amount { get; private set; }
    public DateTime ExpireDate { get; private set; }
    
    //Sempre que possível, criar um método para facilitarmos o uso em outros lugares
    public bool IsValid() => DateTime.Compare(DateTime.Now, ExpireDate) < 0;

    public decimal Value()
    {
        return IsValid() ? Amount : 0;
    }
}
﻿using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
{
    public CreateOrderItemCommand()
    {
        
    }

    public CreateOrderItemCommand(Guid product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
    
    public Guid Product { get; set; }
    public int Quantity { get; set; }
    
    public void Valideta()
    {
        AddNotifications(new Contract<CreateOrderItemCommand>()
            .Requires()
            .IsMinValue(32, "Quantity", "Produto inválido")
            .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida"));
    }
}
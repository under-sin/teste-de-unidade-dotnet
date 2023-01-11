﻿using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Interfaces;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}
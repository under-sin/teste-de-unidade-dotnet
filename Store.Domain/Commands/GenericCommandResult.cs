﻿using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class GenericCommandResult : ICommandResult
{
    public GenericCommandResult(bool success, string menssage, object data)
    {
        Success = success;
        Menssage = menssage;
        Data = data;
    }

    public bool Success { get; set; }
    public string Menssage { get; set; }
    public object Data { get; set; }
}
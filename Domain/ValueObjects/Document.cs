using Portly.Domain.Exceptions;

namespace Portly.Domain.ValueObjects;

public sealed class Document
{
    public string Value { get; }

    private Document(string value)
    {
        Value = value;
    }

    public static Document Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessRuleException("O documento é obrigatório");

        if (!value.All(char.IsDigit))
            throw new BusinessRuleException("O documento deve conter apenas números");

        if (value.Length != 11 && value.Length != 14)
            throw new BusinessRuleException("O documento deve ter 11 dígitos (CPF) ou 14 dígitos (CNPJ)");

        return new Document(value);
    }

    public bool IsCpf => Value.Length == 11;
    public bool IsCnpj => Value.Length == 14;

    public override string ToString() => Value;
}


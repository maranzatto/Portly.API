namespace Portly.Domain.Exceptions;

public class InvalidResidentEmailException : DomainException
{
    public InvalidResidentEmailException() : base("Email de resident inválido.") { }
}

public class InvalidResidentPhoneException : DomainException
{
    public InvalidResidentPhoneException() : base("Telefone de resident inválido.") { }
}

public class InvalidResidentApartmentException : DomainException
{
    public InvalidResidentApartmentException() : base("Apartamento de resident inválido.") { }
}

public class InvalidResidentBlockException : DomainException
{
    public InvalidResidentBlockException() : base("Bloco de resident inválido.") { }
}

public class ResidentAlreadyDeletedException : BusinessRuleException
{
    public ResidentAlreadyDeletedException() : base("Resident já está excluído.") { }
}

public class ResidentNotFoundException : BusinessRuleException
{
    public ResidentNotFoundException() : base("Resident não encontrado.") { }
}

public class ResidentAlreadyRestoredException : BusinessRuleException
{
    public ResidentAlreadyRestoredException() : base("Resident já está restaurado.") { }
}

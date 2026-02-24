namespace Portly.Domain.Exceptions;

public class InvalidUserEmailException : DomainException
{
    public InvalidUserEmailException() : base("Email inválido.") { }
}

public class InvalidUserPasswordException : DomainException
{
    public InvalidUserPasswordException() : base("Senha inválida.") { }
}

public class UserAlreadyInactiveException : BusinessRuleException
{
    public UserAlreadyInactiveException() : base("Usuário já está inativo.") { }
}

public class UserAlreadyActiveException : BusinessRuleException
{
    public UserAlreadyActiveException() : base("Usuário já está ativo.") { }
}

public class InvalidIdException : DomainException
{
    public InvalidIdException(string entity) : base($"ID inválido para entidade {entity}.") { }
}

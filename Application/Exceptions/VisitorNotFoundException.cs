namespace Portly.Application.Exceptions;

public sealed class VisitorNotFoundException : ApplicationException
{
    public VisitorNotFoundException(Guid visitorId)
        : base($"Visitante com ID '{visitorId}' não foi encontrado.")
    {
    }
}


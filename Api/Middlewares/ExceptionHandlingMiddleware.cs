using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portly.Application.Exceptions;
using Portly.Domain.Exceptions;
using Npgsql;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (VisitorNotFoundException ex)
        {
            _logger.LogWarning("Visitante não encontrado: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            _logger.LogWarning("Violação de regra de negócio: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.UnprocessableEntity, ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Acesso não autorizado: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.Unauthorized, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Operação inválida: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning("Argumento nulo: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Argumento inválido: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            _logger.LogError(ex, "Erro de atualização no banco de dados: {SqlState}", pgEx.SqlState);
            var message = ParsePostgresException(pgEx);
            await WriteError(context, HttpStatusCode.Conflict, message);
        }
        catch (System.ApplicationException ex)
        {
            _logger.LogError(ex, "Erro de aplicação: {Message}", ex.Message);
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteError(context, HttpStatusCode.InternalServerError, "Erro interno do servidor");
        }
    }

    private static string ParsePostgresException(PostgresException pgEx)
    {
        if (pgEx.SqlState == "23505")
        {
            return "Registro já existe.";
        }
        if (pgEx.SqlState == "23503")
        {
            return "Referência inválida.";
        }
        if (pgEx.SqlState == "23514")
        {
            return "Violação de restrição.";
        }
        return "Erro de banco de dados.";
    }

    private static async Task WriteError(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            Success = false,
            Message = message,
            StatusCode = (int)statusCode,
            Timestamp = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}

using Core.Exceptions;
using Core.Exceptions.ProblemDetailModels;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace WebApi.GlobalExceptionHandler;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        Type typeExc = exception.GetType();

        string? exceptionDetail = "Empty Exception Detail...";
        if (typeof(ValidationException) == typeExc)
        {
            httpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            exceptionDetail = CreateValidationException(exception);
        }
        else if (typeof(BusinessException) == typeExc)
        {
            httpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
            exceptionDetail = CreateBusinessException(exception);
        }
        else
        {
            httpContext.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
            exceptionDetail = CreateInternalException(exception);
        }
        await httpContext.Response.WriteAsync(exceptionDetail);


        return true;
    }

    private string CreateValidationException(Exception exception)
    {
        IEnumerable<ValidationFailure> errors = ((ValidationException)exception).Errors;
        return new ValidationProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailTypes.Validation.ToString(),
            Title = "Validation Error(s)",
            Detail = "",
            Errors = errors
        }.ToString();
    }

    private string CreateBusinessException(Exception exception)
    {
        return new BusinessProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = ProblemDetailTypes.Business.ToString(),
            Detail = exception.InnerException != null ? exception.Message + exception.InnerException.Message : exception.Message,
            Title = "Business Wrok Flow Excepiton",
        }.ToString();
    }
    private string CreateInternalException(Exception exception)
    {
        //JsonConvert.SerializeObject
        return JsonSerializer.Serialize(new Microsoft.AspNetCore.Mvc.ProblemDetails()
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = ProblemDetailTypes.General.ToString(),
            Detail = exception.InnerException != null ? exception.Message + exception.InnerException.Message : exception.Message,
            Title = "Internal exception",
        });
    }
}

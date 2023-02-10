using System.Net;
using NotissimusTest.Core.Exceptions;

namespace NotissimusTest.Web.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await httpContext.Response.WriteAsync(exception.Message);
        }
        catch (ConfigurationException exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(exception.Message);
        }
        catch (XmlException exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
            await httpContext.Response.WriteAsJsonAsync(exception.Message);
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            Console.WriteLine(exception.Message);
        }
    }
}
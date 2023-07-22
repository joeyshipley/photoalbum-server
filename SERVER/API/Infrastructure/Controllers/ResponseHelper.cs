using System.Net;
using Application.Infrastructure.RequestResponse;
using Microsoft.AspNetCore.Mvc;

namespace API.Infrastructure.Controllers;

public static class ResponseHelper
{
    public static JsonResult Respond(ResultBase result)
    {
        return result.Errors.Any() 
            ? ResponseHelper.Fail(result.Errors) 
            : ResponseHelper.Success(result);
    }

    public static JsonResult Success() => 
        Success(new {});

    public static JsonResult Success(dynamic data) => 
        new JsonResult(new { Result = data }) { StatusCode = (int) HttpStatusCode.OK };

    public static JsonResult Fail(Exception exception) => 
        Fail(new List<string> { exception.Message });

    public static JsonResult Fail(List<string> errors) => 
        Fail(HttpStatusCode.InternalServerError, errors);

    public static JsonResult Fail(List<(string Key, string ErrorMessage)> errors) => 
        Fail(HttpStatusCode.InternalServerError, errors.Select(x => x.ErrorMessage).ToList());

    public static JsonResult Fail(HttpStatusCode statusCode, List<string> errors) => 
        new JsonResult(new { Errors = errors }) { StatusCode = (int) statusCode };
}
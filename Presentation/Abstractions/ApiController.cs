﻿using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;


[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult HandleFailure(Result result) =>
   result switch
   {
       //we throw exception because we do not want this
       //method to be called if the result is succcessfull
       { IsSuccess: true } => throw new InvalidOperationException(),
       IValidationResult validationResult =>
           BadRequest(
               CreateProblemDetails(
                   "Validation Error", StatusCodes.Status400BadRequest,
                   result.Error,
                   validationResult.Errors)),
       _ =>
           BadRequest(
               CreateProblemDetails(
                   "Bad Request",
                   StatusCodes.Status400BadRequest,
                   result.Error))
   };

    private static ProblemDetails CreateProblemDetails(
        string title,
        int status,
        Error error,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
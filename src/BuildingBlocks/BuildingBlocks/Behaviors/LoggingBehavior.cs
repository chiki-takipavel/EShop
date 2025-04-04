﻿using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle Request={RequestName} - Response={ResponseName} - RequestData={@RequestData}.",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        var response = await next();
        timer.Stop();

        var takenTime = timer.Elapsed;
        if (takenTime.Milliseconds > 500)
        {
            logger.LogWarning("[PERFOMANCE] The request {Request} took {TimeTaken} ms.",
                typeof(TRequest).Name, takenTime.Milliseconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}.",
            typeof(TRequest).Name, typeof(TResponse).Name);

        return response;
    }
}

using Lab5.exceptions;
using Lab5.request;
using Lab5.service;

namespace Lab5.controller;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

[ApiController]
[Route("hall/{attemptId:int}")]
public class HallController : ControllerBase
{
    private readonly Simulator _simulator;

    public HallController(Simulator simulator)
    {
        _simulator = simulator;
    }

    [HttpPost("get-next")]
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public int GetNext(int attemptId)
    {
        try
        {
            Response.StatusCode = 200;
            _simulator.GenerateByAttempt(out var _, out var hall, attemptId);
            var next = hall.GetNext();
            _simulator.UpdateAttempt(hall, attemptId);
            return next;
        }
        catch (EndOfQueueException e)
        {
            Response.StatusCode = 200;
            return -1;
        }
    }

    [HttpPost("skip")]
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public void SkipContenders([FromBody] SkipContendersRequest request, int attemptId)
    {
        _simulator.GenerateByAttempt(out var _, out var hall, attemptId);
        hall.SkipContenders(request.NumberToSkip);
        _simulator.UpdateAttempt(hall, attemptId);
    }
}
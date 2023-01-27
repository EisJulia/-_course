using System.Diagnostics.CodeAnalysis;
using Lab5.exceptions;
using Lab5.request;
using Lab5.service;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace Lab5.controller;

[ApiController]
[Route("friend/{attemptId:int}")]
public class FriendController : ControllerBase
{
    private readonly Simulator _simulator;

    public FriendController(Simulator simulator)
    {
        _simulator = simulator;
    }

    [HttpPost("get-rate")]
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public int GetRate([FromBody] GetRateRequest getRateRequest, int attemptId)
    {
        try
        {
            _simulator.GenerateByAttempt(out var friend, out _, attemptId);
            return friend.GetRate(getRateRequest.ContenderNumber);
        }
        catch (ContenderStillInHallException e)
        {
            Response.StatusCode = 400;
            return -1;
        }
    }

    [HttpPost("is-he-the-best")]
    [SuppressMessage("ReSharper.DPA", "DPA0000: DPA issues")]
    public bool IsHeTheBest([FromBody] GetRateRequest getRateRequest, int attemptId)
    {
        try
        {
            _simulator.GenerateByAttempt(out var friend, out _, attemptId);
            return friend.IsHeTheBest(getRateRequest.ContenderNumber);
        }
        catch (ContenderStillInHallException e)
        {
            Response.StatusCode = 400;
            return false;
        }
    }
}
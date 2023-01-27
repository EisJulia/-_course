using Lab5.request;
using Lab5.service;

namespace Lab5.controller;

using Microsoft.AspNetCore.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

[ApiController]
[Route("simulator")]
public class SimulatorController : ControllerBase
{
    private readonly Simulator _simulator;

    public SimulatorController(Simulator simulator)
    {
        _simulator = simulator;
    }
    
    [HttpPost("generate-attempt")]
    public int GenerateAttempt()
    {
        Response.StatusCode = 200;
        return _simulator.GenerateAttempt();
    }
    
    [HttpPost("{attemptId:int}/regenerate-attempt")]
    public void RegenerateAttempt(int attemptId)
    {
        _simulator.RegenerateAttempt(attemptId);
    }

    [HttpPost("{attemptId:int}/princess-chose")]
    public void PrincessChose([FromBody] PrincessChoseRequest request, int attemptId)
    {
        _simulator.PrincessChose(request.ContenderNum, request.Happiness, attemptId);
    }

    [HttpGet("get-avr-happiness")]
    public int? CalcAvrHappiness()
    {
        return _simulator.CalcAvrHappiness();
    }

    [HttpGet("ping")]
    public int Ping()
    {
        return 1;
    }
}
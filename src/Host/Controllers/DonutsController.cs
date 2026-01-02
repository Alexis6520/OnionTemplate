using Application.Features.Donuts;
using Application.ResultPattern;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

public class DonutsController(DonutsService donutsService) : CustomController
{
    private readonly DonutsService _donutsService = donutsService;

    /// <summary>
    /// Crea una donita
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType<Result<int>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAsync(CreateDonutRequest request)
    {
        return BuildResponse(await _donutsService.CreateAsync(request));
    }

    /// <summary>
    /// Lista de donas
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType<Result<List<Donut>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return BuildResponse(await _donutsService.GetAllAsync(cancellationToken));
    }
}

using Application.ResultPattern;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Donuts;

public class DonutsService(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<Result<int>> CreateAsync(CreateDonutRequest request)
    {
        // Comprobamos que no exista otra dona con el mismo nombre
        var existingDonut = await _dbContext.Donuts
            .AnyAsync(d => d.Name == request.Name);

        if (existingDonut) return Result.Failure<int>(Errors.DONUT_NAME_DUPLICATED);

        // Creamos la dona
        var donut = new Donut
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        _dbContext.Donuts.Add(donut);
        await _dbContext.SaveChangesAsync();
        return Result.Success(donut.Id, HttpStatusCode.Created);
    }

    public async Task<List<Donut>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Donuts
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }
}

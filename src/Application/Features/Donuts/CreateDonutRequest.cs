namespace Application.Features.Donuts;

public class CreateDonutRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}

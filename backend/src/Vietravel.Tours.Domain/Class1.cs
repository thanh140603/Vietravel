namespace Vietravel.Tours.Domain;

public sealed class Tour
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string City { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

namespace Core.Entities;

public interface IAddress
{
    int Id { get; set; }
    string? City { get; set; }
    string? Street { get; set; }
    string? Index { get; set; }
}
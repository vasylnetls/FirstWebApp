using Core.Enums;

namespace Core.Interfaces;

public interface IDay
{
    int Id { get; set; }
    Langs Lang { get; set; }
    WeekDay WeekDay { get; set; }
    string? Value { get; set; }
}
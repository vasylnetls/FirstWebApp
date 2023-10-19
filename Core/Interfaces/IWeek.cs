using Core.Enums;

namespace Core.Entities;

public interface IWeek
{
    Day this[int lang, int index] { get; }
    Day this[string lang, string dayName] { get; }
    Day this[Langs lang, WeekDay day] { get; }
    void AddWeek(Langs lang, List<Day> days);
}
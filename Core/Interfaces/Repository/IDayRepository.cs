using Core.Enums;

namespace Core.Interfaces.Repository;

public interface IDayRepository
{
    IEnumerable<KeyValuePair<WeekDay, string?>> GetDaysByLanguage(Langs lang);
}
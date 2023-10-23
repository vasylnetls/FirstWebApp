using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Interfaces.Repository;

namespace DataBase
{
    public class DayRepository : IDayRepository
    {
        protected readonly MyAppDbContext _dbContext;

        public DayRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<KeyValuePair<WeekDay, string?>> GetDaysByLanguage(Langs lang)
        {
            var days = _dbContext.Days;

            return days.Where(day => day.Lang == lang).ToDictionary(day => day.WeekDay, day => day.Value);
        }
    }
}

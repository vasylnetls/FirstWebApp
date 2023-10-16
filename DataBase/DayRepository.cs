using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace DataBase
{
    public class DayRepository : IDayRepository
    {
        protected readonly MyAppDbContext _dbContext;

        public DayRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<KeyValuePair<WeekDay, string>> GetDaysByLanguage(Langs lang)
        {
            var days = _dbContext.Days;

            var dictionary = new Dictionary<WeekDay, string>();
            foreach (var day in days)
            {
                if (day.Lang == lang)
                {
                    dictionary.Add(day.WeekDay, day.Value);
                }
            }
            return dictionary;
        }
    }
}

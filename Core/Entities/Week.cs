using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Week : IWeek
    {
        public Dictionary<Langs, List<Day>> DaysDictionary;

        public Day this[int lang, int index]
        {
            get
            {
                var arr = DaysDictionary[(Langs)lang];
                return arr[index - 1];
            }
        }

        public Day this[string lang, string dayName]
        {
            get
            {
                Langs lg = Enum.Parse<Langs>(lang);
                WeekDay d = Enum.Parse<WeekDay>(dayName);

                return DaysDictionary[lg].ElementAt((int)d - 1);
            }
        }

        public Day this[Langs lang, WeekDay day]
        {
            get
            {
                var arr = DaysDictionary[lang];
                return arr[(int)day - 1];
            }
        }

        public void AddWeek(Langs lang, List<Day> days)
        {
            DaysDictionary.Add(lang, days);

        }
    }
}

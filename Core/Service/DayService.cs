using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Repository;

namespace Core.Service
{
    public class DayService : IDayService
    {
        protected readonly IDayRepository _dayRepository;

        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }
    }
}

using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class AddressRepository : IAddressRepository
    {
        protected readonly MyAppDbContext _myDbContext;
        public AddressRepository(MyAppDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
    }
}

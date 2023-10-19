using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class AddressService : IAddressService
    {
        protected readonly IAddressRepository addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
    }
}

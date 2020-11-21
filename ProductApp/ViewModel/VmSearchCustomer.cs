using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmSearchCustomerParams
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
    }
}

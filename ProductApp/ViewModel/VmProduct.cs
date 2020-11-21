using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmProduct
    {
     
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPicturePath { get; set; }
        public byte[] ProductByteImage { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }


    }
}

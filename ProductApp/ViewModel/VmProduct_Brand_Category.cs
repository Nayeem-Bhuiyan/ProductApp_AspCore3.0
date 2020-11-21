using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApp.ViewModel
{
    public class VmProduct_Brand_Category
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductPicturePath { get; set; }
        public byte[] ProductByteImage { get; set; }
        
        public IFormFile ProductImage { get; set; }
    }
}

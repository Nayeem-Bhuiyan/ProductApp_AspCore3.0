using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApp.ViewModel;


namespace ProductApp.Models
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCostInfo> ProductCostInfo { get; set; }
        public virtual DbSet<ProductSellInfo> ProductSellInfo { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Thana> Thana { get; set; }
        public virtual DbSet<Gender>Gender { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

    }
}

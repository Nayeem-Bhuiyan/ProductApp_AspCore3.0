using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductApp.DAL;
using ProductApp.Models;
using ProductApp.ViewModel;

namespace ProductApp.Controllers
{
    public class CustomersController : Controller
    {
        //private readonly ProductDbContext _context;

        //public CustomersController(ProductDbContext context)
        //{
        //    _context = context;
        //}

        private readonly IRepoproduct db;

        public CustomersController(IRepoproduct _db)
        {
            this.db = _db;
        }


        // GET: Customers
        public IActionResult Index()
        {
            return View(db.CustomerList());
        }


        // POST: Customers/FindCustomer
        [HttpPost]
        public IActionResult FindCustomer([FromBody] Customer vm)
        {
            return Json(db.SearchCustomerByCustomerMobileNo(vm));
        }


        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = db.CustomerByCustomerId(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,CustomerName,CustomerMobile,CustomerAddress")] Customer customer)
        {
                db.CreateNewCustomer(customer);
                return RedirectToAction(nameof(Index));
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =db.CustomerByCustomerId(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerId,CustomerName,CustomerMobile,CustomerAddress")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.EditPostCustomer(id, customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (db.CustomerByCustomerId(id)==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer =db.CustomerByCustomerId(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            db.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }


    }
}

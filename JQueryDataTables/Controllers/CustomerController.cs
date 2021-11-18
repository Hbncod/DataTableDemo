using JQueryDataTables.Controllers;
using JQueryDataTables.Data;
using JQueryDataTables.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Datatables.ServerSide.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : GridController<Customer>
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> GetCustomers()
        {
            var query = await _context.Customers.ToListAsync();

            var searchByName = Request.Form["searchByName"];

            if (!string.IsNullOrEmpty(searchByName))
            {
                query = query.Where(x => x.FirstName.ToLower().Contains(searchByName.ToString().ToLower())).ToList();
            }

            return base.Demo(query.AsQueryable());
        }
    }
}
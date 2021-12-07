using JQueryDataTables.Controllers;
using JQueryDataTables.Data;
using JQueryDataTables.Extensions;
using JQueryDataTables.Models;
using JQueryDataTables.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datatables.ServerSide.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : GridController<Customer>
    {
        private readonly ApplicationDbContext _context;
        private ICustomerExportProvider _customerExportProvider;
        public CustomerController(ApplicationDbContext context, ICustomerExportProvider customerExportProvider)
        {
            _context = context;
            _customerExportProvider = customerExportProvider;
        }
        [HttpPost]
        [Route("grid")]
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

        [HttpPost("ExportTable")]
        public async Task<IActionResult> ExportTable([FromQuery] string format, [FromForm] string dtParametersJson)
        {
            DtParameters dtParameters = new();
            if (!string.IsNullOrEmpty(dtParametersJson))
            {
                dtParameters = JsonConvert.DeserializeObject<DtParameters>(dtParametersJson);
            }

            var result = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(dtParameters.SearchByName))
            {
                result = result.Where(x => x.FirstName.ToLower().Contains(dtParameters.SearchByName.ToLower()));
            }

            var sortOrder = dtParameters.SortOrder.Split(" ");
            if (!string.IsNullOrEmpty(dtParameters.SortOrder))
            {
                result = result.OrderByDynamic(sortOrder[0], dtParameters.Order[0].Dir);
            }

            result = result.Skip(dtParameters.Start).Take(dtParameters.Length);

            switch (format)
            {
                case "csv":
                    return await GerarCSV(result.ToList());
            }

            return Ok();
        }

        private async Task<IActionResult> GerarCSV(List<Customer> customers)
        {
            string contentType;
            string fileDowloadName;

            contentType = "text/csv";
            fileDowloadName = $"{Guid.NewGuid()}.csv";

            using var memory = await _customerExportProvider.Export(customers);

            return File(memory.ToArray(), contentType, fileDowloadName);
        }
    }
}
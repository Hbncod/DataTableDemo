using JQueryDataTables.Extensions;
using JQueryDataTables.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JQueryDataTables.Controllers
{
    public abstract class GridController<Tentity> : Controller where Tentity : class
    {
        public virtual IActionResult Demo(IQueryable<Tentity> tentities)
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault() == "asc" ? DtOrderDir.Asc : DtOrderDir.Desc;
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = tentities.Count();

            if (!string.IsNullOrEmpty(sortColumn))
            {
                tentities = tentities.OrderByDynamic(sortColumn, sortColumnDirection);
            }

            var recordsFiltered = tentities.Count();
            var data = tentities.Skip(skip).Take(pageSize).ToList();

            return Ok(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data });
        }
    }
}

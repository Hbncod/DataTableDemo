using CsvHelper;
using CsvHelper.Configuration;
using JQueryDataTables.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace JQueryDataTables.Services
{
    public class CustomerExportProvider : ICustomerExportProvider
    {
        public async Task<MemoryStream> Export(List<Customer> items)
        {
            var memory = new MemoryStream();
            var stw = new StreamWriter(memory);
            var csv = new CsvWriter(stw, new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter = ";" });

            //csv.Context.RegisterClassMap<PropostaCsvHelperMapping>();
            //csv.Context.RegisterClassMap<PropostaBeneficiarioCsvHelperMapping>();

            await csv.WriteRecordsAsync(items);

            //csv.WriteHeader<PropostaBeneficiario>();

            await csv.FlushAsync();

            return memory;
        }
    }

    public interface ICustomerExportProvider : IExportProvider<Customer>
    {
    }
}

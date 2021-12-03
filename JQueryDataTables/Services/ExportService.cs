using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JQueryDataTables.Services
{
    public interface IExportProvider<T> where T : class
    {
        public Task<MemoryStream> Export(List<T> items);
    }

}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace JQueryDataTables.Models
{
    public class DataTableResponse<T>
    {
        [JsonProperty("draw")]
        public int Draw { get; set; }
        [JsonProperty("recordsTotal")]
        public int RecordsTotal { get; set; }
        [JsonProperty("recordsFiltered")]
        public int RecordsFiltered { get; set; }
        [JsonProperty("data")]
        public IList<T> Data { get; set; }
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
    }
}

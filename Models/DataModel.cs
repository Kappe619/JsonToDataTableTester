using Newtonsoft.Json.Linq;

namespace JsonToDataTableTester.Models
{
    public class DataModel
    {
        public JObject JsonData { get; set; }

        public DataModel(JObject jsonData)
        {
            JsonData = jsonData;
        }
    }
}

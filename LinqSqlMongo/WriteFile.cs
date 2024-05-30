using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LinqSqlMongo
{
    public class WriteFile
    {
        public static string GetData(List<Penalty> lst) => JsonConvert.SerializeObject(lst);
    }
}
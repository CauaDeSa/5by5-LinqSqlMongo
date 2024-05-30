using Newtonsoft.Json;

namespace LinqSqlMongo
{
    public class AppliedPenalties
    {
        [JsonProperty("penalidades_aplicadas")]
        public List<Penalty> appliedPenaltys { get; set; }

        public static void PrintData(List<Penalty> lst)
        {
            foreach (var p in lst)
                Console.WriteLine(p + "\n");
        }
    }
}
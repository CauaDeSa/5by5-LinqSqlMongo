using MongoDB.Driver;

namespace LinqSqlMongo.DB
{
    public class MyMongo
    {
        const string _connectionUri = "mongodb://root:Mongo%402024%23@localhost:27017/";
        readonly MongoClient _client;

        public MyMongo() => _client = new MongoClient(_connectionUri);

        public void InsertPenalties(List<Penalty> lst)
        {
            var db = _client.GetDatabase("DBPenalty");
            var collection = db.GetCollection<Penalty>("Penalty");

            collection.InsertMany(lst);
        }

        public bool IsEmpty()
        {
            var db = _client.GetDatabase("DBPenalty");
            var collection = db.GetCollection<Penalty>("Penalty");

            return collection.CountDocuments(FilterDefinition<Penalty>.Empty) == 0;
        }
    }
}
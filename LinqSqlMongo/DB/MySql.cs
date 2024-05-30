using LinqSqlMongo;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LinqSqlMongo.DB
{
    public class MySql
    {
        readonly string _connection = "Data Source=127.0.0.1; Initial Catalog=DBPenalties; User Id=sa; Password=SqlServer2019!; TrustServerCertificate=Yes";
        readonly SqlConnection _sqlConnection;
        readonly SqlCommand _sqlCommand;

        public MySql()
        {
            _sqlConnection = new(_connection);
            _sqlCommand = new()
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = _sqlConnection,
                CommandText = "spInitialize"
            };

            _sqlConnection.Open();
            _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();

            _sqlCommand.Parameters.Clear();
        }

        public void InsertProcessControl(string description, int numberOfRecords)
        {
            _sqlCommand.CommandText = ("spInsertProcessControl");
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            _sqlCommand.Parameters.AddRange(

            new SqlParameter[]
            {
                new SqlParameter("@Description", description),
                new SqlParameter("@Date", DateTime.Now),
                new SqlParameter("@NumberOfRecords", numberOfRecords)
            }) ;

            _sqlCommand.Connection = _sqlConnection;

            _sqlConnection.Open();
            _sqlCommand.ExecuteNonQuery();
            _sqlConnection.Close();

            _sqlCommand.Parameters.Clear();
        }

        public void InsertPenalties(List<Penalty> lst)
        {
            _sqlCommand.CommandText = ("spInsertPenalty");
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (var item in lst)
            {
                int count = 0;

                _sqlCommand.Parameters.AddRange(

                new SqlParameter[]
                {
                    new SqlParameter("@CompanyName", item.CompanyName),
                    new SqlParameter("@CNPJ", item.Cnpj),
                    new SqlParameter("@DriverName", item.DriverName),
                    new SqlParameter("@CPF", item.Cpf),
                    new SqlParameter("@VigencyDate", item.VigencyDate)
                });

                _sqlCommand.Connection = _sqlConnection;

                _sqlConnection.Open();
                _sqlCommand.ExecuteNonQuery();
                _sqlConnection.Close();

                _sqlCommand.Parameters.Clear();

                Console.WriteLine($"{count}° register inserted");
            }
        }

        public List<Penalty> RetrieveAll()
        {
            List<Penalty> lst = new();

            _sqlCommand.CommandText = ("spGetAllPenalties");
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            _sqlCommand.Connection = _sqlConnection;
            _sqlConnection.Open();

            using SqlDataReader reader = _sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                lst.Add(new()
                {
                    Id = reader.GetInt32(0),
                    CompanyName = reader.GetString(1),
                    Cnpj = reader.GetString(2),
                    DriverName = reader.GetString(3),
                    Cpf = reader.GetString(4),
                    VigencyDate = reader.GetDateTime(5)
                });
            }

            _sqlCommand.Parameters.Clear();
            _sqlConnection.Close();

            return lst;
        }

        public bool IsEmpty()
        {
            _sqlCommand.CommandText = ("spGetAllPenalties");
            _sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            _sqlCommand.Connection = _sqlConnection;
            _sqlConnection.Open();

            using SqlDataReader reader = _sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                _sqlCommand.Parameters.Clear();
                _sqlConnection.Close();
                return false;
            }

            _sqlCommand.Parameters.Clear();
            _sqlConnection.Close();

            return true;
        }
    }
}
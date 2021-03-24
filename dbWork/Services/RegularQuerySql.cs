using System;
using Npgsql;

namespace dbWork.Services
{
    public class RegularQuerySql
    {
        private string DbConnectString { get; set; }
        private NpgsqlConnection Connection { get; set; }

        public RegularQuerySql(string dbConnectString)
        {
            DbConnectString = dbConnectString;
            Connection = new NpgsqlConnection(DbConnectString);
        }
        public bool CheckTableExists(string table)
        {
            try
            {
                var command = new NpgsqlCommand($"SELECT * FROM {table}", Connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public void OpenConnection()
        {
            Connection.Open();
        }
        
        public void CloseConnection()
        {
            Connection.Dispose();
        }

        public bool ExecSqlFile(string filename)
        {
            try
            {
                var strText = System.IO.File.ReadAllText(filename, System.Text.Encoding.UTF8);
                var command = new NpgsqlCommand(strText, Connection);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public int ExecSqlNonQuery(string querystring)
        {
            var command = new NpgsqlCommand(querystring, Connection);
            return command.ExecuteNonQuery();
        }
        public NpgsqlDataReader ExecReadSqlQuery(string querystring)
        {
            var command = new NpgsqlCommand(querystring, Connection);
            return command.ExecuteReader();
        }
        
    }
}
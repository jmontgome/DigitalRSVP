using System.Data;
using DigitalRSVP.Core.Options;
using Microsoft.Data.SqlClient;

namespace DigitalRSVP.WAPI.Repositories
{
    public class DatabaseRepository
    {
        private readonly ILogger<DatabaseRepository> Logger;

        private readonly string CONNECTION_STRING;

        public DatabaseRepository(ConnectionOptions options,
            ILogger<DatabaseRepository> logger)
        {
            CONNECTION_STRING = options.DR_ConnString;
            Logger = logger;
        }

        protected async Task ExecuteCommandAsync(string storedprocedure,
            IEnumerable<SqlParameter> parameters)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(storedprocedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (SqlParameter pam in parameters)
                    {
                        command.Parameters.Add(pam);
                    }
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        protected async Task<DataTable> ExecuteQueryAsync(string storedprocedure, string tablename, IEnumerable<SqlParameter>? param = null, bool AllowNull = true)
        {
            DataTable table = new DataTable(tablename);
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand command = new SqlCommand(storedprocedure, connection)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (param != null)
                    {
                        foreach (SqlParameter pam in param)
                        {
                            command.Parameters.Add(pam);
                        }
                    }
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        table.Load(reader);
                        if (table.Rows.Count > 0 && table.Columns.Count > 1)
                            return table;
                    }
                }
            }
            return null;
        }
    }
}

using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Options;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DigitalRSVP.WAPI.Repositories
{
    public class EventRepository : DatabaseRepository, IEventRepository
    {
        public EventRepository(ConnectionOptions options,
            ILogger<EventRepository> logger): base(options, logger)
        {

        }

        private Event HydrateEvent(DataRow data)
        {
            Event retEvent = new Event();
            retEvent.Id = Guid.Parse(data["WorkItemId"].ToString()!);
            retEvent.Name = data["Name"].ToString()!;
            retEvent.ExpiryDate = DateTime.Parse(data["ExpiryDate"].ToString()!);
            return retEvent;
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", id)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.Event_Get_ById, KnownSQL.KnownTables.Event, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateEvent(table.Rows[0]);
                }
            }
            return null;
        }
        public async Task<Event> GetEventByEmailAsync(string email)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@email", email)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.Event_Get_ByEmail, KnownSQL.KnownTables.Email, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateEvent(table.Rows[0]);
                }
            }
            return null;
        }
    }
}

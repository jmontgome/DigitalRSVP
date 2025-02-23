using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Options;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DigitalRSVP.WAPI.Repositories
{
    public class GuestRepository : DatabaseRepository, IGuestRepository
    {
        public GuestRepository(ConnectionOptions options,
            ILogger<GuestRepository> logger): base(options, logger)
        {

        }

        private Guest HydrateGuest(DataRow data)
        {
            Guest guest = new Guest();
            guest.Id = Guid.Parse(data["WorkItemId"].ToString()!);
            guest.RSVPId = Guid.Parse(data["RsvpId"].ToString()!);
            guest.Name = data["Name"].ToString()!;
            guest.Age = (Age)int.Parse(data["Age"].ToString()!);
            guest.AttendingWedding = bool.Parse(data["AttendingWedding"].ToString()!);
            guest.AttendingReception = bool.Parse(data["AttendingReception"].ToString()!);
            return guest;
        }
        private IEnumerable<Guest> HydrateGuests(DataTable table)
        {
            List<Guest> guests = new List<Guest>();
            foreach (DataRow row in table.Rows)
            {
                guests.Add(HydrateGuest(row));
            }
            return guests;
        }

        public async Task UpsertGuest(Guest guest)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", guest.Id),
                new SqlParameter("@Name", guest.Name),
                new SqlParameter("@Age", guest.Age),
                new SqlParameter("@AttendingWedding", guest.AttendingWedding),
                new SqlParameter("@AttendingReception", guest.AttendingReception),
                new SqlParameter("@Rsvpid", guest.RSVPId)
            };
            await this.ExecuteCommandAsync(KnownSQL.KnownStoredProcedures.Guest_Upsert, param.ToArray());
        }

        public async Task<IEnumerable<Guest>> GetGuestsByRSVPIdAsync(Guid id)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@RsvpId", id)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.Guest_Get_ByRsvpId, KnownSQL.KnownTables.Guest, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateGuests(table);
                }
            }
            return null;
        }
    }
}

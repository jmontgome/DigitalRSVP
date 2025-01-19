using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Options;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace DigitalRSVP.WAPI.Repositories
{
    public class RSVPRepository : DatabaseRepository, IRSVPRepository
    {
        public RSVPRepository(ConnectionOptions options,
            ILogger<RSVPRepository> logger): base(options, logger)
        {

        }

        private RSVP HydrateRSVP(DataRow data)
        {
            RSVP rsvp = new RSVP();
            rsvp.Id = Guid.Parse(data["WorkItemId"].ToString()!);
            rsvp.InviteeId = Guid.Parse(data["InviteeId"].ToString()!);
            rsvp.DateTime = DateTime.Parse(data["DateTime"].ToString()!);
            rsvp.Guests = JsonConvert.DeserializeObject<IEnumerable<Guest>>(data["GuestsData"].ToString()!);
            rsvp.AttendingWedding = bool.Parse(data["AttendingWedding"].ToString()!);
            rsvp.AttendingReception = bool.Parse(data["AttendingReception"].ToString()!);
            rsvp.Note = data["Note"].ToString();
            rsvp.Created_Date = DateTime.Parse(data["Created_Date"].ToString()!);
            rsvp.Updated_Date = DateTime.Parse(data["Updated_Date"].ToString()!);
            return rsvp;
        }

        public async Task<RSVP> GetRSVPAsync(Guid rsvpId)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", rsvpId)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.RSVP_Get_ById, KnownSQL.KnownTables.RSVP, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateRSVP(table.Rows[0]);
                }
            }
            return null;
        }
        public async Task<RSVP> GetRSVPByInviteeAsync(Guid inviteeId)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@InvId", inviteeId)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.RSVP_Get_ByInvitationID, KnownSQL.KnownTables.RSVP, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateRSVP(table.Rows[0]);
                }
            }
            return null;
        }

        public async Task SubmitRSVPAsync(RSVP rsvp)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", rsvp.Id),
                new SqlParameter("@InviteeId", rsvp.InviteeId),
                new SqlParameter("@DateTime", rsvp.DateTime),
                new SqlParameter("@GuestsData", JsonConvert.SerializeObject(rsvp.Guests)),
                new SqlParameter("@AttendingWedding", rsvp.AttendingWedding == true ? 1 : 0),
                new SqlParameter("@AttendingReception", rsvp.AttendingReception == true ? 1 : 0),
                new SqlParameter("@Note", rsvp.Note)
            };
            await this.ExecuteCommandAsync(KnownSQL.KnownStoredProcedures.RSVP_Submit, param.ToArray());
        }
        public async Task EditRSVPAsync(RSVP rsvp)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", rsvp.Id),
                new SqlParameter("@InviteeId", rsvp.InviteeId),
                new SqlParameter("@DateTime", rsvp.DateTime),
                new SqlParameter("@GuestsData", JsonConvert.SerializeObject(rsvp.Guests)),
                new SqlParameter("@AttendingWedding", rsvp.AttendingWedding == true ? 1 : 0),
                new SqlParameter("@AttendingReception", rsvp.AttendingReception == true ? 1 : 0),
                new SqlParameter("@Note", rsvp.Note)
            };
            await this.ExecuteCommandAsync(KnownSQL.KnownStoredProcedures.RSVP_Update, param.ToArray());
        }
    }
}

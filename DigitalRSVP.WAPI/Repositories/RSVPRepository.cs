﻿using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Options;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
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
            rsvp.EventId = Guid.Parse(data["EventId"].ToString()!);
            rsvp.InviteeId = Guid.Parse(data["InviteeId"].ToString()!);
            rsvp.DateTime = DateTime.Parse(data["DateTime"].ToString()!);
            rsvp.Note = data["Note"].ToString();
            rsvp.Created_Date = DateTime.Parse(data["Created_Date"].ToString()!);
            rsvp.Updated_Date = DateTime.Parse(data["Updated_Date"].ToString()!);
            return rsvp;
        }
        private IEnumerable<RSVP> HydrateRSVPs(DataTable table)
        {
            List<RSVP> rsvps = new List<RSVP>();
            foreach (DataRow row in table.Rows)
            {
                rsvps.Add(HydrateRSVP(row));
            }
            return rsvps;
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
        public async Task<IEnumerable<RSVP>> GetRSVPsByEventIdAsync(Guid eventId)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@EventId", eventId)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.RSVP_Get_ByEventId, KnownSQL.KnownTables.Event, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateRSVPs(table);
                }
            }
            return null;
        }

        public async Task SubmitRSVPAsync(RSVP rsvp)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", rsvp.Id),
                new SqlParameter("@EventId", rsvp.EventId),
                new SqlParameter("@InviteeId", rsvp.InviteeId),
                new SqlParameter("@DateTime", rsvp.DateTime),
                new SqlParameter("@Note", rsvp.Note)
            };
            await this.ExecuteCommandAsync(KnownSQL.KnownStoredProcedures.RSVP_Submit, param.ToArray());
        }
        public async Task EditRSVPAsync(RSVP rsvp)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", rsvp.Id),
                new SqlParameter("@EventId", rsvp.EventId),
                new SqlParameter("@InviteeId", rsvp.InviteeId),
                new SqlParameter("@DateTime", rsvp.DateTime),
                new SqlParameter("@Note", rsvp.Note)
            };
            await this.ExecuteCommandAsync(KnownSQL.KnownStoredProcedures.RSVP_Update, param.ToArray());
        }
    }
}

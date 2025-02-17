using DigitalRSVP.Core.Models;
using DigitalRSVP.Core.Options;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DigitalRSVP.WAPI.Repositories
{
    public class InvitationRepository : DatabaseRepository, IInvitationRepository
    {
        public InvitationRepository(ConnectionOptions options,
            ILogger<InvitationRepository> logger) : base(options, logger)
        {
        }

        private Invitation HydrateInvitation(DataRow data)
        {
            Invitation inv = new Invitation();
            inv.Id = Guid.Parse(data["WorkItemId"].ToString()!);
            inv.EventId = Guid.Parse(data["EventId"].ToString()!);
            inv.Name = data["Name"].ToString()!;
            inv.WeddingParty = bool.Parse(data["WeddingParty"].ToString()!);
            inv.DesignatedSeating = bool.Parse(data["DesignatedSeating"].ToString()!);
            inv.NoteToInvitee = data["NoteToInvitee"].ToString()!;
            inv.Created_Date = DateTime.Parse(data["Created_Date"].ToString()!);
            inv.Updated_Date = DateTime.Parse(data["Updated_Date"].ToString()!);
            return inv;
        }

        public async Task<Invitation> GetInvitationAsync(Guid id)
        {
            List<SqlParameter> param = new List<SqlParameter>()
            {
                new SqlParameter("@Id", id)
            };
            DataTable table = await this.ExecuteQueryAsync(KnownSQL.KnownStoredProcedures.Invitation_Get_ById, KnownSQL.KnownTables.Invitation, param.ToArray());
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    return HydrateInvitation(table.Rows[0]);
                }
            }
            return null;
        }
    }
}

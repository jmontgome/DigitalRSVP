using DigitalRSVP.Core.Models;
using System.Text;

namespace DigitalRSVP.WAPI.Reporting
{
    public class RSVPReportFactory<CallingType> : ReportingFactory<CallingType>, IDisposable
    {
        private readonly IEnumerable<RSVP> _rsvps;
        private readonly IEnumerable<Invitation> _invitations;
        
        public RSVPReportFactory(ILogger<CallingType> logger, IEnumerable<RSVP> rsvps,
            IEnumerable<Invitation> invs) : base(logger)
        {
            this._rsvps = rsvps;
            this._invitations = invs;
        }

        public override string GenerateEmailReportBody()
        {
            if (this._rsvps != null && this._invitations != null)
            {
                if (this._rsvps.Count() > 0 && this._invitations.Count() > 0)
                {
                    return $"Hello!\r\n\tA report on RSVP's and Invitations for an event that your contact information is attached to was requested. Please check the file attached to this email" +
                        $"to view your RSVP's to your invitations that was sent out.\r\nThank you!\r\n\t-The eRSVP Team";
                }
                else
                {
                    throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
                }
            }
            else
            {
                throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
            }
        }

        public override MemoryStream GenerateCSVFile()
        {
            if (this._rsvps != null)
            {
                if (this._rsvps.Count() > 0)
                {
                    string fileContent = $"Name on Invitation,\tGuest,\tAge,\tAttending Wedding,\tAttending Reception,\t\r\n";
                    foreach (RSVP rsvp in this._rsvps)
                    {
                        Invitation matchingInv = null;
                        foreach (Invitation inv in this._invitations)
                        {
                            if (inv.Id == rsvp.InviteeId)
                            {
                                matchingInv = inv;
                                break;
                            }
                        }
                        if (matchingInv != null)
                        {
                            if (rsvp.Guests != null)
                            {
                                if (rsvp.Guests.Count() > 0)
                                {
                                    foreach (Guest guest in rsvp.Guests)
                                    {
                                        fileContent += $"{matchingInv.Name},\t{guest.Name},\t{guest.AgeAsString()},\t{guest.AttendingWedding},\t{guest.AttendingReception},\t\r\n";
                                    }
                                }
                                else
                                {
                                    fileContent += $"{matchingInv.Name},\tNo Guests Listed...,\t-,\t-,\t-,\t\r\n";
                                }
                            }
                        }
                    }
                    return new MemoryStream(Encoding.UTF8.GetBytes(fileContent));
                }
                else
                {
                    throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
                }
            }
            else
            {
                throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
            }
        }

        #region Disposable Interface

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RSVPReportFactory()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

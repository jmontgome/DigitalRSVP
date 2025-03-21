
using DigitalRSVP.Core.Models;

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

                }
            }
            throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
        }

        public override MemoryStream GenerateCSVFile()
        {
            if (this._rsvps != null)
            {
                if (this._rsvps.Count() > 0)
                {

                }
            }
            throw new InvalidDataException($"No RSVPs or Invitations were found to make a report from!");
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

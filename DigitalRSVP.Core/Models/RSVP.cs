namespace DigitalRSVP.Core.Models
{
    public class RSVP
    {
        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private Guid _eventId;
        public Guid EventId
        {
            get
            {
                return this._eventId;
            }
            set
            {
                this._eventId = value;
            }
        }

        private Guid _inviteeId;
        public Guid InviteeId
        {
            get
            {
                return _inviteeId;
            }
            set
            {
                _inviteeId = value;
            }
        }

        private DateTime _datetime;
        public DateTime DateTime
        {
            get
            {
                return _datetime;
            }
            set
            {
                if (value > DateTime.Now.AddMinutes(-10))
                {
                    _datetime = value;
                }
            }
        }

        private IEnumerable<Guest>? _guests;
        public IEnumerable<Guest>? Guests
        {
            get
            {
                return this._guests;
            }
            set
            {
                this._guests = value;
            }
        }

        private bool _attendingWedding;
        public bool AttendingWedding
        {
            get
            {
                return this._attendingWedding;
            }
            set
            {
                this._attendingWedding = value;
            }
        }

        private bool _attendingReception;
        public bool AttendingReception
        {
            get
            {
                return this._attendingReception;
            }
            set
            {
                this._attendingReception = value;
            }
        }

        private string? _note;
        public string? Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        private DateTime _createdDate;
        public DateTime Created_Date
        {
            get
            {
                return this._createdDate;
            }
            set
            {
                if (value > new DateTime(2025, 1, 1))
                {
                    this._createdDate = value;
                }
            }
        }

        private DateTime _updatedDate;
        public DateTime Updated_Date
        {
            get
            {
                return this._updatedDate;
            }
            set
            {
                if (value > new DateTime(2025, 1, 1))
                {
                    this._createdDate = value;
                }
            }
        }

        public RSVP() { }
        public RSVP(Guid id)
        {
            this._id = id;
            this._datetime = DateTime.Now;
        }
    }
}
